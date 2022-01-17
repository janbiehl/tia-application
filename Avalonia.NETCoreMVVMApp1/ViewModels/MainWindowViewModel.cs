using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Siemens.Engineering;
using TiaInterface.Extensions;
using TiaInterface.Utils;

// https://cache.industry.siemens.com/dl/dl-media/533/109798533/att_1069909/v1/145532071563_en-US/en-US/index.html#treeId=e40f48dda865a961580015d588a10e04

namespace Avalonia.NETCoreMVVMApp1.ViewModels
{
	public class MainWindowViewModel : ViewModelBase
	{
		public string Greeting => "Welcome to Avalonia!";

		public MainWindowViewModel()
		{

			/*try
			{
				using TiaPortal? tiaPortal = new TiaPortal();

				try
				{
					var project = tiaPortal.CreateProject("C:/Generated", "GeneratedProject");
					
					var plcDevice = ProjectUtils.CreateDevice(project, "OrderNumber:6ES7 510-1DJ01-0AB0/V2.0", "PlcDevice_1", "PLC_1");

					project.Save();
				}
				catch (Exception e)
				{
					Console.WriteLine(e);
				}
				finally
				{
					tiaPortal.Dispose();
				}
			}
			catch (TypeInitializationException)
			{
				MessageBox.Show(
					"It seems, that the TIA-Portal or Openness is not installed. Or you are running on the wrong OS",
					"Error");
				Console.WriteLine(
					"It seems, that the TIA-Portal or Openness is not installed. Or you are running on the wrong OS");
			}
			catch (Exception e)
			{
				MessageBox.Show($"There was an error initializing the TIA-Portal: {e}");
				Console.WriteLine("There was an error initializing the TIA-Portal");
				Console.WriteLine(e);
			}*/
		}
	}
}