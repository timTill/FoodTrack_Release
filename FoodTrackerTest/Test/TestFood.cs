using FoodTracker.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodTrackerTest.Test
{
	class TestFood : Food
	{
		public override bool Equals(object obj)
		{
			Food foodParam = (Food)obj;
			return ((this.Name == foodParam.Name)
				&& (this.ID == foodParam.ID)
				&& (this.CategoryId == foodParam.CategoryId)
				&& (this.SubCategoryId == foodParam.SubCategoryId)
				&& (this.BestBefore == foodParam.BestBefore)
				&& (this.Description == foodParam.Description)
				&& (this.Measurement == foodParam.Measurement)
				&& (this.QuantityLeft == foodParam.QuantityLeft)
				&& (this.Unit == foodParam.Unit)
				);
		}

	}
}
