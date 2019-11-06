using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodTracker.Data;
using FoodTracker.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodTracker.Areas.User.Controllers
{
	[Area("User")]
	public class CartController : Controller
    {
		private readonly ApplicationDbContext _db;
		public CartController(ApplicationDbContext db)
		{
			_db = db;
		}

		public async Task<IActionResult> Index()
		{
			IndexViewModel IndexVM = new IndexViewModel()
			{
				//FoodItem = await _db.Foods.Include(m => m.Category).Include(m => m.SubCategory).ToListAsync(),
				FoodItem = await _db.Foods.Where(m => m.IsInCart== true).Include(m => m.Category).
				Include(m => m.SubCategory).ToListAsync(),
				Category = await _db.Category.ToListAsync(),
			};
			return View(IndexVM);
		}
	}
}