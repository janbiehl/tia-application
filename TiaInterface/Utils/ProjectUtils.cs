using System;
using System.IO;
using System.Linq;
using Siemens.Engineering;
using Siemens.Engineering.HW;

namespace TiaInterface.Utils
{
	public class ProjectUtils
	{
		/// <summary>
		/// Create a project archive for a TIA-Portal project.
		/// Projects archives are used as backup or to easily share projects with others
		/// </summary>
		/// <param name="project">The project used for the archive</param>
		/// <param name="archiveDirectory">The directory where the archive file should be saved</param>
		/// <param name="fileName">
		/// The name of the archive file. Feel free to use any file extension you want.
		/// But keep in mind that the TIA Portal can only open ones that look similar to xxx.zap15_1 or xxx.zap16
		/// </param>
		/// <param name="saveChanges">Weather or not to save unsaved changes</param>
		/// <exception cref="ArgumentNullException"></exception>
		/// <exception cref="ArgumentException"></exception>
		/// <exception cref="TiaException"></exception>
		/// <exception cref="InvalidOperationException"></exception>
		public static void CreateArchive(Project project, string archiveDirectory, string fileName,
			bool saveChanges = false)
		{
			if (project is null)
				throw new ArgumentNullException(nameof(project));

			if (string.IsNullOrWhiteSpace(archiveDirectory))
				throw new ArgumentException("Value cannot be null or whitespace.", nameof(archiveDirectory));

			if (string.IsNullOrWhiteSpace(fileName))
				throw new ArgumentException("Value cannot be null or whitespace.", nameof(fileName));

			if (project.IsModified)
			{
				if (saveChanges)
				{
					try
					{
						project.Save();
					}
					catch (Exception e)
					{
						throw new TiaException("There was an error saving the project changes", e);
					}
				}
				else
				{
					throw new TiaException("There are unsaved changes, cannot generate a project archive");
				}
			}

			var archiveDirectoryInfo = new DirectoryInfo(archiveDirectory);

			if (!archiveDirectoryInfo.Exists)
			{
				// Create the directory when it does not exist
				archiveDirectoryInfo.Create();
			}
			else
			{
				// when it does exist, check if the file is already present
				var fullFilePath = Path.Combine(archiveDirectory, fileName);

				if (File.Exists(fullFilePath))
					throw new InvalidOperationException(
						$"There is already a file '{fileName}' in the directory '{archiveDirectoryInfo.FullName}'");
			}

			try
			{
				project.Archive(archiveDirectoryInfo, fileName,
					ProjectArchivationMode.DiscardRestorableDataAndCompressed);
			}
			catch (Exception e)
			{
				throw new TiaException("There was an error while archiving the project", e);
			}
		}

		/// <summary>
		/// Retrieve a project archive into a fully accessible TIA-Portal project. 
		/// </summary>
		/// <param name="projectComposition">The project composition to retrieve it to</param>
		/// <param name="archiveFilePath">The path to the archive file</param>
		/// <param name="projectTargetDirectory">The directory where the retrieved project will be stored at</param>
		/// <returns>The created Project or null</returns>
		/// <exception cref="ArgumentNullException"></exception>
		/// <exception cref="ArgumentException"></exception>
		/// <exception cref="InvalidOperationException"></exception>
		/// <exception cref="TiaException"></exception>
		public static Project RetrieveArchive(ProjectComposition projectComposition, string archiveFilePath,
			string projectTargetDirectory)
		{
			if (projectComposition == null)
				throw new ArgumentNullException(nameof(projectComposition));

			if (string.IsNullOrWhiteSpace(archiveFilePath))
				throw new ArgumentException("Value cannot be null or whitespace.", nameof(archiveFilePath));

			if (string.IsNullOrWhiteSpace(projectTargetDirectory))
				throw new ArgumentException("Value cannot be null or whitespace.", nameof(projectTargetDirectory));

			var archiveFileInfo = new FileInfo(archiveFilePath);
			var projectTargetInfo = new DirectoryInfo(projectTargetDirectory);

			if (!archiveFileInfo.Exists)
				throw new InvalidOperationException($"There is no archive file on path '{archiveFileInfo.FullName}'");

			try
			{
				return projectComposition.RetrieveWithUpgrade(archiveFileInfo, projectTargetInfo);
			}
			catch (Exception e)
			{
				throw new TiaException("There was an error retrieving the project archive", e);
			}
		}

		public static Device CreateDevice(Project project, string typeIdentifier, string deviceName, string itemName)
		{
			if (project == null)
				throw new ArgumentNullException(nameof(project));

			if (string.IsNullOrWhiteSpace(typeIdentifier))
				throw new ArgumentException("Value cannot be null or whitespace.", nameof(typeIdentifier));

			if (string.IsNullOrWhiteSpace(deviceName))
				throw new ArgumentException("Value cannot be null or whitespace.", nameof(deviceName));

			if (string.IsNullOrWhiteSpace(itemName))
				throw new ArgumentException("Value cannot be null or whitespace.", nameof(itemName));
			
			//if (!Utils.ValidateTypeIdentifier(typeIdentifier))
			//	throw new ArgumentException("The type identifier is not valid", nameof(typeIdentifier));

			if (DeviceExists(project, deviceName))
				throw new InvalidOperationException($"There is already a device named '{deviceName}'");

			return project.Devices.CreateWithItem("OrderNumber:6ES7 510-1DJ01-0AB0/V2.0", "PLC_1", "NewDevice");
			return project.Devices.CreateWithItem(typeIdentifier, itemName, deviceName);
		}

		public static bool DeviceExists(Project project, string deviceName)
		{
			return project.Devices.Any(device => device.Name.Equals(deviceName));
		}
	}
}