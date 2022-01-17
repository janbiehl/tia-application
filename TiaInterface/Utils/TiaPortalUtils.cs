using System;
using System.IO;
using Siemens.Engineering;

namespace TiaInterface.Utils
{
	public class TiaPortalUtils
	{
		public static Project CreateProject(TiaPortal? portal, string projectDirectory, string projectName, 
			string author = "", string comment = "")
		{
			if (portal is null) 
				throw new ArgumentNullException(nameof(portal));
        
			if (string.IsNullOrWhiteSpace(projectDirectory))
				throw new ArgumentException("Value cannot be null or whitespace.", nameof(projectDirectory));
        
			if (string.IsNullOrWhiteSpace(projectName))
				throw new ArgumentException("Value cannot be null or whitespace.", nameof(projectName));

			var targetDirectory = new DirectoryInfo(projectDirectory);

			// if (targetDirectory.Exists)
			// 	throw new InvalidOperationException($"The directory at path '{targetDirectory.FullName}' does already exist");

			try
			{
				var project =  portal.Projects.Create(targetDirectory, projectName);

				if (project is null)
					throw new Exception("The project does not exist after creation, internal fault");
            
				if (author != string.Empty)
					project.SetAttribute("Author", author);

				if (comment != string.Empty)
					project.SetAttribute("Comment", comment);

				return project;
			}
			catch (Exception e)
			{
				throw new TiaException("There was an error creating the project", e); 
			}
		}
	}
}