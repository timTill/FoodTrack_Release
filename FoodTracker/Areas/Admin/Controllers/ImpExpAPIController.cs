using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using FoodTracker.Data;
using FoodTracker.Models;
using FoodTracker.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FoodTracker.Areas.Admin.Controllers
{
	//[Produces("application/xml")]
	[Route("api/[controller]")]
	[ApiController]
	public class ImpExpAPIController : ControllerBase
	{
		private readonly ApplicationDbContext applicationDbContext;
		private readonly IDataManager ImpExpMan;

		public ImpExpAPIController(ApplicationDbContext applicationDbContext, IDataManager ImpExpMan)
		{
			this.applicationDbContext = applicationDbContext;
			this.ImpExpMan = ImpExpMan;
		}

		[HttpGet]
		public string Get()		
		{
			//List<Category> CategoryItems = applicationDbContext.Category.ToList();
			var items = applicationDbContext.Category.Select(x => new
			{
				Id = x.Id,
				Name = x.Name
			});
			List<SubCategory> SubCategoryItems = applicationDbContext.SubCategory.ToList();

			var SubcategoryItems = applicationDbContext.SubCategory.Select(x => new
			{
				Id = x.Id,
				Name = x.Name,
				CategoryId = x.CategoryId
			});

			List<Food> FoodItems = applicationDbContext.Foods.ToList();
			var foodItems = applicationDbContext.Foods.Select(x => new
			{
				Id = x.ID,
				Name = x.Name,
				CategoryId = x.CategoryId,
				SubCategoryID = x.SubCategoryId
			});

			/*
			PortDBViewModel DB_VM = new PortDBViewModel
			{
				Categories = CategoryItems,
				Subcategories = SubCategoryItems,
				Foods = FoodItems
			};
			*/
			//var json = JsonConvert.SerializeObject(DB_VM.Subcategories.ToList());
			//
			//return Json(DB_VM.Categories.ToList());
			//XDocument xdoc = ImportExportManager.XMLSerializerFromDB(DB_VM);
			//return xdoc;
			/*
			var json1 = JsonConvert.SerializeObject(DB_VM);
			JsonConvert.SerializeObject(DB_VM.Subcategories.ToList()[0]);
			*/
			var json1 = JsonConvert.SerializeObject(foodItems);
			return json1;
		}
	}

}
