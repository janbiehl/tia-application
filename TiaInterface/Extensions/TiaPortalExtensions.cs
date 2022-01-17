using Siemens.Engineering;
using TiaInterface.Utils;

namespace TiaInterface.Extensions
{
	public static class TiaPortalExtensions
	{
		public static Project CreateProject(this TiaPortal? portal, 
			string projectDirectory, 
			string projectName, 
			string author = "",
			string comment = "")
		{
			return TiaPortalUtils.CreateProject(portal, projectDirectory, projectName, author, comment);
		}
	}
}