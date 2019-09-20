using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using FoodTracker.Data;
using FoodTracker.Models.ViewModels;
using FoodTracker.Utility;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace FoodTracker.Models
{
	
	public class ImportExportManager : IDataManager
	{
		private readonly IHostingEnvironment env;
		private readonly IConfiguration _iconf;
		private string XMLFileDynamicPath;

		public ImportExportManager(IHostingEnvironment env, IConfiguration iconf)
		{
			this.env = env;
			_iconf = iconf;
			XMLFileDynamicPath =
				Path.Combine(env.WebRootPath, "Data","Data.xml");
		}
		//private const string XMLinPath = @"C:\Users\Szabolcs\Source\Repos\FoodTracker\FoodTracker\FoodTracker\Port\DataToImport.xml";
		public void ExportXML(PortDBViewModel input)
		{
			DumpDataToXML(input);
		}		

		public void ImportXML()
		{
			
			//PortDBViewModel originalParsedXML = ParseXML(SD.XMLinPath);			
			PortDBViewModel originalParsedXML = ParseXML(XMLFileDynamicPath);
			PortDBViewModel normalizedXml = normalizeIDs(originalParsedXML);
			
			AddManyCategoriesFromParamToDBTest(normalizedXml.Categories);
			AddManySubCategoriesFromParamToDBTest(normalizedXml.Subcategories);
			AddManyFoodItemFromParamToDBTest(normalizedXml.Foods);
		}

		private void AddManyCategoriesFromParamToDBTest(IEnumerable<Category> categList)
		{
			using (var context = new ImpExpDBContext(_iconf))
			{
				context.Category.AddRange(categList);
				context.SaveChanges();
			}
		}

		private void AddManySubCategoriesFromParamToDBTest(IEnumerable<SubCategory> subcategList)
		{
			using (var context = new ImpExpDBContext(_iconf))
			{
				context.SubCategory.AddRange(subcategList);
				context.SaveChanges();
			}
		}

		private void AddManyFoodItemFromParamToDBTest(IEnumerable<Food> foodList)
		{
			using (var context = new ImpExpDBContext(_iconf))
			{
				context.Foods.AddRange(foodList);
				context.SaveChanges();
			}
		}

		public XDocument XMLSerializerFromDB(PortDBViewModel DB_VM)
		{
			XDocument xmlDocument = new XDocument(
				new XDeclaration("1.0", "utf-8", "yes"),
				new XComment("Creating an XML Tree using LINQ to XML"),

				new XElement("Foods",
					new XElement("Categories",
						from category in DB_VM.Categories
						select new XElement("Category",
									new XElement("CategoryID", category.Id),
									new XElement("Name", category.Name)
									)),

					new XElement("Subcategories",
					from subcategory in DB_VM.Subcategories
					select new XElement("Subcategory",
								new XElement("SubcategoryID", subcategory.Id),
								new XElement("Name", subcategory.Name),
								new XElement("CategoryID", subcategory.CategoryId)
								)),

					new XElement("FoodItems",
					from foodItem in DB_VM.Foods
					select new XElement("FoodItem",
								new XElement("FoodID", foodItem.ID),
								new XElement("Name", foodItem.Name),
								new XElement("Description", foodItem.Description),
								new XElement("CategoryID", foodItem.CategoryId),
								new XElement("SubcategoryId", foodItem.SubCategoryId),
								new XElement("BestBefore", foodItem.BestBefore),
								new XElement("Unit", foodItem.Unit),
								new XElement("Measurement", foodItem.Measurement),
								new XElement("QuanityLeft", foodItem.QuantityLeft)
								))
							));
			return xmlDocument;
		}


		private void DumpDataToXML(PortDBViewModel DB_VM)
		{
			XDocument xmlDocument = XMLSerializerFromDB(DB_VM);
			xmlDocument.Save(XMLFileDynamicPath);
		}


		private PortDBViewModel normalizeIDs(PortDBViewModel input)
		{
			//Initialize data counts
			int CategoryCount = input.Categories.Count();
			int SubCategoryCount = input.Subcategories.Count();
			int FoodItemsCount = input.Foods.Count();

			List<Category> Categories2DB = input.Categories.ToList();
			List<SubCategory> SubCategories2DB = input.Subcategories.ToList();
			List<Food> Foods2DB = input.Foods.ToList();

			//Normalize subcategory.CategoryId to new Category.ID
			//1. Take all Categories
			for (int i = 0; i < CategoryCount; i++)
			{
				//2. Check all subcategories
				foreach (var subcategory in SubCategories2DB)
				{
					//if an "old" subcategory.categID mathes and "old" Category.ID... (foreign key ref.)
					if (subcategory.CategoryId == Categories2DB[i].Id)
					{
						//replace subcateg.Categ by the index number+1 of the categ
						// (cf. categ index+1 value will be the new ID in the new "clean" DB).						
						subcategory.CategoryId = i + 1;
					}
				}
			}

			//normalize the food.SubCategoryId to new SubCategory.ID
			//1. Take all Subcategories
			for (int i = 0; i < SubCategoryCount; i++)
			{
				//2. iterate all foods
				foreach (var food in Foods2DB)
				{
					//3. if an "old" food.subcategID matches the "old" subcateg.id (foreign key ref.)
					if (food.SubCategoryId == SubCategories2DB[i].Id)
					{
						//4. replace the food.subcateg with the index+1 (index+1 will be
						//the new subcat.ID in the "clean" DB).
						food.SubCategoryId = i + 1;
					}
				}
			}

			//normalize the Food.Category to new Category.ID
			//1. Take all categories
			for (int i = 0; i < CategoryCount; i++)
			{
				//2. Iterate through all the foods
				foreach (var food in Foods2DB)
				{
					//3. if an "old" food.Category mathes the "old" category.ID (foreign key ref.)
					if (food.CategoryId == Categories2DB[i].Id)
					{
						//4. update food.CategID with index+1 (cycle index+1 will be the new Categ.ID)
						food.CategoryId = i + 1;
						//5. set all food.ID to 0 (EF can then insert it to DB)
						food.ID = 0;
					}
				}
			}

			//set all Category ID to 0 (EF can then insert it to DB)
			foreach (var item in Categories2DB)
			{
				item.Id = 0;
			}

			//set all Subcategory ID to 0 (EF can then insert it to DB)
			foreach (var item in SubCategories2DB)
			{
				item.Id = 0;
			}

			PortDBViewModel output = new PortDBViewModel
			{
				Categories = Categories2DB,
				Subcategories = SubCategories2DB,
				Foods = Foods2DB
			};
			return output;
		}

		private PortDBViewModel ParseXML(string XMLPath)
		{
			PortDBViewModel DB_VM = new PortDBViewModel();

			XDocument doc = XDocument.Load(XMLPath);
			IEnumerable<Category> categories = from c in doc.Descendants("Category")
											   select new Category()
											   {
												   Id = (int)c.Element("CategoryID"),
												   Name = (string)c.Element("Name")
											   };

			IEnumerable<SubCategory> subCategories = from s in doc.Descendants("Subcategory")
													 select new SubCategory()
													 {
														 Id = (int)s.Element("SubcategoryID"),
														 Name = (string)s.Element("Name"),
														 CategoryId = (int)s.Element("CategoryID")
													 };

			IEnumerable<Food> Foods = from f in doc.Descendants("FoodItem")
									  select new Food()
									  {
										  ID = (int)f.Element("FoodID"),
										  Name = (string)f.Element("Name"),
										  Description = (string)f.Element("Description"),
										  CategoryId = (int)f.Element("CategoryID"),
										  SubCategoryId = (int)f.Element("SubcategoryId"),
										  BestBefore = string.IsNullOrEmpty((string)f.Element("BestBefore")) ? (DateTime?)null : (DateTime)f.Element("BestBefore"),
										  Unit = (int)f.Element("Unit"),
										  QuantityLeft = Enum.Parse<QuantityLeft>((string)f.Element("QuanityLeft")),
										  Measurement = Enum.Parse<MeasType>((string)f.Element("Measurement"))
									  };

			DB_VM.Categories = categories;
			DB_VM.Subcategories = subCategories;
			DB_VM.Foods = Foods;
			return DB_VM;
		}
	}
}
