using FoodTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using FoodTracker.Utility;
using FoodTracker.Models.ViewModels;

namespace FoodTracker.Extensions
{
	public static class ImportExportData
	{
		public static PortDBViewModel ParseXML(string XMLPath)
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

		public static PortDBViewModel normalizeIDs(PortDBViewModel input)
		{
			//Initialize data counts
			int CategoryCount = input.Categories.Count();
			int SubCategoryCount = input.Subcategories.Count();
			int FoodItemsCount = input.Foods.Count();

			//data cloning
			List<Category> newCategories = input.Categories.ToList();
			List<SubCategory> newsubCategories = input.Subcategories.ToList();
			List<Food> newFoods = input.Foods.ToList();
			int newIndex = 1;


			//Distinct original values of Categ/Subcateg
			var OriginalCategoryIds = input.Categories.GroupBy(c => c.Id).ToList();
			var OriginalSubCategoryIds = input.Subcategories.GroupBy(s => s.Id).ToList();


			//Normalize Category.ID
			foreach (var categIDToChange in OriginalCategoryIds)
			{
				foreach (var category in newCategories)
				{
					if (category.Id == categIDToChange.Key)
					{
						category.Id = newIndex;
						newIndex++;
						break;
					}
				}
			}

			newIndex = 1;
			//Normalize Subcategory.ID
			foreach (var subcategIDToChange in OriginalSubCategoryIds)
			{
				foreach (var subcategory in newsubCategories)
				{
					if (subcategory.Id == subcategIDToChange.Key)
					{
						subcategory.Id = newIndex;
						newIndex++;
						break;
					}
				}
			}


			//Normalize subcategory.CategoryId
			for (int i = 0; i < CategoryCount; i++)
			{
				foreach (var subcategory in newsubCategories)
				{
					if (subcategory.CategoryId == OriginalCategoryIds[i].Key)
					{
						subcategory.CategoryId = newCategories[i].Id;						
					}
				}
			}

			//normalize the food.SubCategoryId
			for (int i = 0; i < SubCategoryCount; i++)
			{
				foreach (var food in newFoods)
				{
					if (food.SubCategoryId == OriginalSubCategoryIds[i].Key)
					{
						food.SubCategoryId = newsubCategories[i].Id;
					}
				}
			}

			//normalize the Food.CategoryID
			for (int i = 0; i < CategoryCount; i++)
			{
				foreach (var food in newFoods)
				{
					if (food.CategoryId == OriginalCategoryIds[i].Key)
					{
						food.CategoryId = newCategories[i].Id;
					}
				}
			}

			PortDBViewModel output = new PortDBViewModel
			{
				Categories = newCategories,
				Subcategories = newsubCategories,
				Foods = newFoods
			};
			return output;
		}

		public static PortDBViewModel RemoveIDToDB(PortDBViewModel input)
		{
			//removing IDs for DB-identity
			List<Category> newCategories2DB = new List<Category>();
			List<SubCategory> newsubCategories2DB = new List<SubCategory>();
			List<Food> newFoods2DB = new List<Food>();

			//removing ID from category class
			foreach (var categitem in input.Categories)
			{
				Category newcatitem = new Category();
				newcatitem.Name = categitem.Name;
				newCategories2DB.Add(newcatitem);
			}

			// removing ID from subcategory
			foreach (var subcategitem in input.Subcategories)
			{
				SubCategory newsubcatitem = new SubCategory
				{
					Name = subcategitem.Name,
					CategoryId = subcategitem.CategoryId
				};
				newsubCategories2DB.Add(newsubcatitem);
			}

			//removind ID from Foods
			foreach (var fooditem in input.Foods)
			{
				Food newFooditem = new Food
				{
					Name = fooditem.Name,
					Description = fooditem.Description,
					CategoryId = fooditem.CategoryId,
					SubCategoryId = fooditem.SubCategoryId,
					BestBefore = fooditem.BestBefore,
					Unit = fooditem.Unit,
					Measurement = fooditem.Measurement,
					QuantityLeft = fooditem.QuantityLeft
				};
				newFoods2DB.Add(newFooditem);
			}

			PortDBViewModel output = new PortDBViewModel
			{
				Categories = newCategories2DB,
				Subcategories = newsubCategories2DB,
				Foods = newFoods2DB
			};
			return output;
		}


		public static void DumpDataToXML(PortDBViewModel DB_VM)
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
			//xmlDocument.Save(SD.XMlAfterNormPath);
		}


	}
}

/*
 * public static void DumpDataToXML(PortDBViewModel DB_VM)			
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
			xmlDocument.Save(SD.XMLoutPath);

		}
*/
