using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodTracker.Models.ViewModels
{
	public class PortDBViewModel
	{
		public IEnumerable<Category> Categories { get; set; }
		//public List<Category> Categories { get; set; }
		public IEnumerable<SubCategory> Subcategories { get; set; }
		public IEnumerable<Food> Foods { get; set; }
	}
}
