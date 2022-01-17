using System;

namespace TiaInterface
{
	// From Siemens Openness documentation
	// https://support.industry.siemens.com/cs/document/109798533/simatic-tia-portal-openness-api-for-automation-of-engineering-workflows?dti=0&lc=en-WW
	// 
	// OrderNumber
	//      Format: "<OrderNumber>"                                         Example: "OrderNumber:3RK1 200-0CE00-0AA2"
	//      Format: "<OrderNumber>/<FirmwareVersion>"                       Example: "OrderNumber:6ES7 510-1DJ01-0AB0/V2.0"
	//      Format: "<OrderNumber>//<AdditionalTypeIdentifier>              Example: "OrderNumber:6AV2 124-2DC01-0AX0//Landscape"
	// GSD
	//      Format: "<GsdName>/<GsdType>"                                   Example: "GSD:SIEM8139.GSD/DAP"
	//      Format: "<GsdName>/<GsdType>/<GsdId>"                           Example: "GSD:SIEM8139.GSD/M/4"
	// System
	//      Format: "<SystemTypeIdentifier>"                                Example: "System:Device.S7300"
	//      Format: "<SystemTypeIdentifier>/<AdditionalTypeIdentifier>"     Example: "GSD:SIEM8139.GSD/M/4"
	public readonly struct TypeIdentifier
	{
		public const string OrderNumberIdentifier = "OrderNumber:";
		public const string GsdIdentifier = "GSD:";
		public const string SystemIdentifier = "System:";

		private readonly string _identifier;

		public bool IsOrderNumber => IsOrderNumberIdentifier(_identifier);
		public bool IsGsd => IsGsdIdentifier(_identifier);
		public bool IsSystem => IsSystemIdentifier(SystemIdentifier);

		public bool IsValidOrderNumber => ValidateOrderNumber(_identifier);
		public bool IsValidGsd => false;
		public bool IsValidSystem => false;

		public TypeIdentifier(string identifier)
		{
			if (string.IsNullOrWhiteSpace(identifier))
				throw new ArgumentException("Value cannot be null or whitespace.", nameof(identifier));

			_identifier = identifier;
		}

		private static (string? orderNumber, string? firmwareVersion, string? additionalIdentifier)
			GetOrderNumberSubstrings(string typeIdentifier)
		{
			string? orderNumber;
			string? firmwareVersion = null;
			string? additionalIdentifier = null;

			typeIdentifier = typeIdentifier.Remove(0, OrderNumberIdentifier.Length);

			var separatorIndex = typeIdentifier.IndexOf('/');

			orderNumber = typeIdentifier.Substring(0, separatorIndex - 1);

			var additionalHeaderIndex = typeIdentifier.IndexOf("//", StringComparison.Ordinal);
			var firmwareVersionIndex = separatorIndex;

			if (additionalHeaderIndex > 0)
			{
				// Here it contains an additional type identifier
				// TODO: Validate this one
				additionalIdentifier = typeIdentifier.Substring(additionalHeaderIndex + 2,
					typeIdentifier.Length - (additionalHeaderIndex + 2));
			}
			else if (firmwareVersionIndex > 0)
			{
				// Here it contains a firmware version
				// TODO: Validate this one
				firmwareVersion = typeIdentifier.Substring(firmwareVersionIndex + 1,
					typeIdentifier.Length - (firmwareVersionIndex + 1));
			}

			return (orderNumber, firmwareVersion, additionalIdentifier);
		}

		public static bool ValidateOrderNumber(string typeIdentifier)
		{
			if (string.IsNullOrWhiteSpace(typeIdentifier))
				throw new ArgumentException("Value cannot be null or whitespace.", nameof(typeIdentifier));

			typeIdentifier = typeIdentifier.Trim();

			if (!IsOrderNumberIdentifier(typeIdentifier))
				return false;

			var values = GetOrderNumberSubstrings(typeIdentifier);

			if (string.IsNullOrWhiteSpace(values.orderNumber))
				throw new InvalidOperationException("The type identifier is not a valid order number");

			if (string.IsNullOrWhiteSpace(values.firmwareVersion))
			{
				// TODO: Validate firmware version
				return false;
			}

			if (string.IsNullOrWhiteSpace(values.additionalIdentifier))
			{
				// TODO: Validate additional identifier
				return false;
			}

			return false;
		}

		public static bool IsOrderNumberIdentifier(string typeIdentifier)
		{
			if (string.IsNullOrWhiteSpace(typeIdentifier))
				throw new ArgumentException("Value cannot be null or whitespace.", nameof(typeIdentifier));

			return typeIdentifier.StartsWith(OrderNumberIdentifier);
		}

		public static bool IsGsdIdentifier(string typeIdentifier)
		{
			if (string.IsNullOrWhiteSpace(typeIdentifier))
				throw new ArgumentException("Value cannot be null or whitespace.", nameof(typeIdentifier));

			return typeIdentifier.StartsWith(GsdIdentifier);
		}

		public static bool IsSystemIdentifier(string typeIdentifier)
		{
			if (string.IsNullOrWhiteSpace(typeIdentifier))
				throw new ArgumentException("Value cannot be null or whitespace.", nameof(typeIdentifier));

			return typeIdentifier.StartsWith(SystemIdentifier);
		}

		public static bool ValidateSystem(string typeIdentifier)
		{
			throw new NotImplementedException();
		}

		public static bool ValidateGsd(string typeIdentifier)
		{
			throw new NotImplementedException();
		}
	}
}