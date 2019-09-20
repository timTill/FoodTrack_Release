using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodTracker.Data;
using FoodTracker.Models;
using Microsoft.AspNetCore.Mvc;
using FoodTracker.Extensions;
using FoodTracker.Models.ViewModels;
using FoodTracker.Utility;

namespace FoodTracker.Areas.Admin.Controllers
{
	[Area("Admin")]
    public class DataController : Controller
    {
		private readonly ApplicationDbContext applicationDbContext;
		private readonly IDataManager ImpExpMan;


		//public DataController(ApplicationDbContext applicationDbContext)
		//public DataController(ApplicationDbContext applicationDbContext, ImportExportManager iem)
		public DataController(ApplicationDbContext applicationDbContext, IDataManager ImpExpMan)
		{
			
			this.applicationDbContext = applicationDbContext;
			this.ImpExpMan = ImpExpMan;
		}

		public IActionResult Export()
		{
			List<Category> CategoryItems = applicationDbContext.Category.ToList();
			IEnumerable<SubCategory> SubCategoryItems = applicationDbContext.SubCategory.ToList();
			IEnumerable<Food> FoodItems = applicationDbContext.Foods.ToList();

			PortDBViewModel DB_VM = new PortDBViewModel
			{
				Categories = CategoryItems,
				Subcategories = SubCategoryItems,
				Foods = FoodItems
			};
			//PortDBViewModel normalized_DB_VM = ImportExportData.normalizeIDs(DB_VM);
			//ImportExportData.DumpDataToXML(DB_VM);			

			//dm.ExportXML(DB_VM);
			ImpExpMan.ExportXML(DB_VM);
			return RedirectToAction("Index", "Home", new { area = "User" });		
		}

		public IActionResult Import()
		{
			ImpExpMan.ImportXML();
			return RedirectToAction("Index", "Home", new { area = "User" });

			/* ez jóóóó!
			PortDBViewModel originalParsedXML = Testing.ParseXML(Testing.XMLinPath);
			PortDBViewModel normalizedXml = Testing.normalizeIDs(originalParsedXML);				

			Testing.AddManyCategoriesFromParamToDBTest(normalizedXml.Categories);
			Testing.AddManySubCategoriesFromParamToDBTest(normalizedXml.Subcategories);
			Testing.AddManyFoodItemFromParamToDBTest(normalizedXml.Foods);
			*/


			/*
			PortDBViewModel Model2DB = Testing.;

			List<Category> Categories2DB = Model2DB.Categories.ToList();
			List<SubCategory> Subcategories2DB = Model2DB.Subcategories.ToList();
			List<Food> Foods2DB = Model2DB.Foods.ToList();


			using (applicationDbContext)
			{
				//applicationDbContext.Category.AddRange(Categories2DB);							
			
				applicationDbContext.SubCategory.AddRange(Subcategories2DB);							
			
				//applicationDbContext.Foods.AddRange(Foods2DB);
				applicationDbContext.SaveChanges();
			}
			*/

			/*PortDBViewModel DB_VM_Normalized = ImportExportData.ParseXML(SD.XMlAfterNormPath);
			PortDBViewModel DB_VM_NoID = ImportExportData.RemoveIDToDB(DB_VM_Normalized);
			
			//DB_VM_Normalized.Categories.ToList()[0].Id = (int?)null;
			//applicationDbContext.Category.AddRange(DB_VM_Normalized.Categories.ToList());			
			
			List<Category> CategToAddToDB = new List<Category>();
			List<SubCategory> SubCategToAddToDB = new List<SubCategory>();
			List<Food> FoodToAddToDB = new List<Food>();

			foreach (var categitem in DB_VM_Normalized.Categories.ToList())
			{
				Category catitem = new Category
				{
					Name = categitem.Name
				};
				CategToAddToDB.Add(catitem);
			}

			
			foreach (var subcategitem in DB_VM_Normalized.Subcategories.ToList())
			{
				SubCategory subcatitem = new SubCategory
				{
					Name = subcategitem.Name,
					CategoryId = subcategitem.CategoryId
				};
				SubCategToAddToDB.Add(subcatitem);
			}


			foreach (var fooditem in DB_VM_Normalized.Foods.ToList())
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
				FoodToAddToDB.Add(newFooditem);
			}			

			/*
			foreach (var categoryToAdd in DB_VM_Normalized.Categories.ToList())
			{
				
				applicationDbContext.Category.Add(categoryToAdd);
			}
			*/

			/*foreach (var categoryToAdd in DB_VM_Normalized.Categories.ToList())
			{
				//le kell bontani a kapcsolatot using-gal nagyon hosszú!
				applicationDbContext.Category.Add(categoryToAdd);
				applicationDbContext.SaveChanges();
			}*/

			//applicationDbContext.Category.AddRange(DB_VM_NoID.Categories);
			//applicationDbContext.SubCategory.AddRange(DB_VM_NoID.Subcategories);
			//applicationDbContext.Foods.AddRange(DB_VM_NoID.Foods);
			/*
			applicationDbContext.Category.AddRange(CategToAddToDB);
			applicationDbContext.SubCategory.AddRange(SubCategToAddToDB);
			applicationDbContext.Foods.AddRange(FoodToAddToDB);
			applicationDbContext.SaveChanges();
			*/
		}

		public IActionResult Index()
        {
            return View();
        }
    }
}