
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodTracker.Models
{
	public interface IFoodRepository
	{
		Food GetFood(int Id);
		IEnumerable<Food> GetAllFood();
		IEnumerable<Food> GetFoodByCateg(int id);
		//List<int> GetCountByCategory();
		Food Add(Food Food);
		Food Update(Food FoodChanges);
		Food Delete(int id);
	}
}

