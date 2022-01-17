using CommandLine;

namespace SampleConsoleApplication
{
	public class CommandLineOptions
	{
		[Option('p', "projectDirectory")]
		public string ProjectDirectory { get; private set; } = string.Empty;

		[Option('n', "projectName", Required = true, HelpText = "Der name der dem Projekt gegeben wird.")]
		public string ProjectName { get; private set; } = string.Empty;
    
		[Option('d', "deviceOrderNumber", Required = true, HelpText = "Der Siemens bestellcode für das plc gerät")]
		public string DeviceOrderNumber { get; private set; } = string.Empty;

		[Option("deviceName", Required = true, HelpText = "Der Name für das PLC Gerät in der Hardware")]
		public string DeviceName { get; private set; } = string.Empty;

		[Option("deviceItemName", Required = true, HelpText = "Der Name für das PLC Gerät, der in der Projekthierarchie angezeigt wird")]
		public string DeviceItemName { get; private set; } = string.Empty;	
	}
}