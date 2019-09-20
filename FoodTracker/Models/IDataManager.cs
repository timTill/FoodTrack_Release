using FoodTracker.Models.ViewModels;

namespace FoodTracker.Models
{
	public interface IDataManager
	{
		void ExportXML(PortDBViewModel input);
		void ImportXML();
	}
}