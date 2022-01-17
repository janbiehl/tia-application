using System;
using CommandLine;
using Siemens.Engineering;
using TiaInterface.Extensions;
using TiaInterface.Utils;

namespace SampleConsoleApplication
{
	internal class Program
	{
		public static int Main(string[] args)
		{
			var commandLineOptions = new CommandLineOptions();

			Parser.Default.ParseArguments<CommandLineOptions>(args)
				.WithParsed(options =>
				{
					commandLineOptions = options;
				})
				.WithNotParsed(errors =>
				{
					Environment.Exit(-1);
				});

			TiaPortal tiaPortal;

			try
			{
				tiaPortal = new TiaPortal();
			}
			catch (TypeInitializationException)
			{
				Console.WriteLine("It seems, that the TIA-Portal or Openness is not installed. Or you are running on the wrong OS");
				return -1;
			}
			catch (Exception e)
			{
				Console.WriteLine("There was an error initializing the TIA-Portal");
				Console.WriteLine(e);
				return -1;
			}

			try
			{
				var project = tiaPortal.CreateProject(commandLineOptions.ProjectDirectory, commandLineOptions.ProjectName);

				var plcDevice = ProjectUtils.CreateDevice(project, $"OrderNumber:{commandLineOptions.DeviceOrderNumber}/V2.9", commandLineOptions.DeviceName, commandLineOptions.DeviceItemName);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
			finally
			{
				tiaPortal.Dispose();
			}

			Console.ReadKey();

			return 0;
		}
	}
}