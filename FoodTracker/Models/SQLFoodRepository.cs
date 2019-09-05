using FoodTracker.Data;
using FoodTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodTracker.Models
{
	public class SQLFoodRepository : IFoodRepository
	{
		private readonly ApplicationDbContext context;

		public SQLFoodRepository(ApplicationDbContext context)
		{
			this.context = context;
		}
		public Food Add(Food food)
		{
			context.Foods.Add(food);
			context.SaveChanges();
			return food;
		}

		public Food Delete(int id)
		{
			Food food = context.Foods.Find(id);
			if (food != null)
			{
				context.Foods.Remove(food);
				context.SaveChanges();
			}
			return food;
		}

		public IEnumerable<Food> GetAllFood()
		{
			return context.Foods;
		}

		//TBD
		/*
		public List<int> GetCountByCategory()
		{
			List<int> CountbyCateg = new List<int>();
			var enumCount = Enum.GetNames(typeof(Category)).Length;
			var foodGroups = context.Foods.GroupBy(ff => ff.Category).ToList();
			int FoodGroupPtr = 0;

			for (int i = 0; i < enumCount; i++)
			{
				if (foodGroups.Count > 0 && i == (int)foodGroups[FoodGroupPtr].Key)
				{
					CountbyCateg.Add(foodGroups[FoodGroupPtr].Count());
					if (FoodGroupPtr < foodGroups.Count() - 1)
					{
						FoodGroupPtr++;
					}
				}
				else
				{
					CountbyCateg.Add(0);
				}
			}			
			return CountbyCateg;
		}
		*/

		public Food GetFood(int Id)
		{
			return context.Foods.Find(Id);
		}

		public IEnumerable<Food> GetFoodByCateg(int id)
		{
			//TBD
			return null;
			//return context.Foods.Where(ff => (int)ff.Category == id);
		}

		public Food Update(Food foodChanges)
		{
			var ff = context.Foods.Attach(foodChanges);
			ff.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
			context.SaveChanges();
			return foodChanges;
		}
	}
}

/*
			var result = context.FridgeFoods.GroupBy(ff => (int)ff.Category)
									.Select(group => new
									{	Category = group.Key,
										Count = group.Count()}).
									OrderBy(ff => ff.Category).ToList();						

			for (int i = 0; i < enumCount; i++)
			{
				bool added = false;
				for (int j = 0; j < result.Count; j++)
				{
					if (result[j].Category > i)
					{
						break;
					}
					else if (result[j].Category == i)
					{
						CountbyCateg.Add(result[j].Count);
						added = true;
						break;
					}
				}
				if (!added)
				{
					CountbyCateg.Add(0);
				}
			}
			*/
