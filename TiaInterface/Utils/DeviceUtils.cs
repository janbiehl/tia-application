using System.Linq;
using Siemens.Engineering.HW;

namespace TiaInterface.Utils
{
	public static class DeviceUtils
	{
		public static bool DeviceItemExists(Device device, string itemName)
		{
			return device.DeviceItems.Any(item => item.Name.Equals(itemName));
		}
	}
}