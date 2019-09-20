using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodTracker.Data;
using FoodTracker.Models;
using FoodTracker.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodTracker.Areas.Admin.Controllers
{
	[Authorize]
	[Area("Admin")]
	public class FoodController : Controller
    {
		private readonly ApplicationDbContext _db;
		private readonly IHostingEnvironment _hostingEnvironment;

		[BindProperty]
		public FoodViewModel foodModel { get; set; }

		public FoodController(ApplicationDbContext db, IHostingEnvironment hostingEnvironment)
		{
			_db = db;
			_hostingEnvironment = hostingEnvironment;
			foodModel = new FoodViewModel()
			{
				Category = _db.Category,
				Food = new Models.Food()
			};
		}

		[HttpGet]
		public async Task<IActionResult> Index()
		{
			var foods = await _db.Foods.Include(m => m.Category).Include(m => m.SubCategory).ToListAsync();
			return View(foods);
		}

		[HttpGet]
		public IActionResult Create()
		{
			return View(foodModel);
		}

		[HttpPost, ActionName("Create")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> CreatePOST()
		{
			//foodModel.Food.BestBefore = DateTime.Now;
			if (!ModelState.IsValid)
			{
				return View(foodModel);
			}
			foodModel.Food.SubCategoryId = Convert.ToInt32(Request.Form["SubCategoryId"].ToString());
			_db.Foods.Add(foodModel.Food);
			await _db.SaveChangesAsync();			
			return RedirectToAction(nameof(Index));
		}

		[HttpGet]
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			foodModel.Food = await _db.Foods.Include(m => m.Category).Include(m => m.SubCategory)
							.SingleOrDefaultAsync(m => m.ID == id);
			foodModel.SubCategory = await _db.SubCategory.Where(s => s.CategoryId == foodModel.Food.CategoryId)
									.ToListAsync();

			if (foodModel.Food == null)
			{
				return NotFound();
			}
			return View(foodModel);
		}

		[HttpPost, ActionName("Edit")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> EditPOST(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}
			foodModel.Food.SubCategoryId = Convert.ToInt32(Request.Form["SubCategoryId"].ToString());

			if (!ModelState.IsValid)
			{
				foodModel.SubCategory = await _db.SubCategory.Where(s => s.CategoryId == foodModel.Food.CategoryId)
					.ToListAsync();
				return View(foodModel);
			}

			//var foodTypeFromDb = await _db.Foods.FindAsync(foodModel.Food.ID);
			var foodTypeFromDb = await _db.Foods.FindAsync(id);
			foodTypeFromDb.Name = foodModel.Food.Name;									
			foodTypeFromDb.CategoryId = foodModel.Food.CategoryId;
			foodTypeFromDb.SubCategoryId = foodModel.Food.SubCategoryId;
			foodTypeFromDb.Measurement = foodModel.Food.Measurement;
			foodTypeFromDb.Priority = foodModel.Food.Priority;
			await _db.SaveChangesAsync();

			return RedirectToAction(nameof(Index));
		}

		[HttpGet]
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			foodModel.Food= await _db.Foods.Include(m => m.Category).Include(m => m.SubCategory)
				.SingleOrDefaultAsync(m => m.ID == id);

			if (foodModel.Food== null)
			{
				return NotFound();
			}

			return View(foodModel);
		}

		[HttpGet]
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			foodModel.Food = await _db.Foods.Include(m => m.Category).Include(m => m.SubCategory)
				.SingleOrDefaultAsync(m => m.ID == id);

			if (foodModel.Food== null)
			{
				return NotFound();
			}

			return View(foodModel);
		}
		
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			
			Food food = await _db.Foods.FindAsync(id);

			if (food != null)
			{							
				_db.Foods.Remove(food);
				await _db.SaveChangesAsync();
			}

			return RedirectToAction(nameof(Index));
		}
	}
}
