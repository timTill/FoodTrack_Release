using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodTracker.Data;
using FoodTracker.Models;
using FoodTracker.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Module1Training.Controllers
{
	[Produces("application/json")]
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

	public PortDBViewModel Get()
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

			return DB_VM;
		}

	}
}