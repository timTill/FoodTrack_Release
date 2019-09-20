using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodTracker.Utility
{
	public static class SD
	{
		public const string OwnerUser = "Owner";
		public const string AdminUser = "Admin";		
		public const string EndUser = "EndUser";
		//public const string XMLBeforeNormPath = @"C:\Users\Szabolcs\Source\Repos\FoodTracker\FoodTracker\FoodTracker\Port\Data_raw.xml";
		public const string XMLinPath = @"C:\Users\Szabolcs\Source\Repos\FoodTracker\FoodTracker\FoodTracker\Port\DataToImport.xml";
		//public const string XMlAfterNormPath = @"C:\Users\Szabolcs\Source\Repos\FoodTracker\FoodTracker\FoodTracker\Port\Data_out.xml";
		public const string constr = "Data Source = localhost\\SZABSQL;Initial Catalog = FT_TestImpExp_Serv; Persist Security Info=True;User ID = szabi; Password=sqlpassword";

	}
}
