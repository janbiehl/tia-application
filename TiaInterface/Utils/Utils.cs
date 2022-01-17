namespace TiaInterface.Utils
{
	public static class Utils
	{
		public static bool ValidateTypeIdentifier(string typeIdentifier)
		{
			if (TypeIdentifier.IsOrderNumberIdentifier(typeIdentifier))
				return TypeIdentifier.ValidateOrderNumber(typeIdentifier);
        
			if (TypeIdentifier.IsGsdIdentifier(typeIdentifier))
				return TypeIdentifier.ValidateGsd(typeIdentifier);
        
			if (TypeIdentifier.IsSystemIdentifier(typeIdentifier))
				return TypeIdentifier.ValidateSystem(typeIdentifier);

			return false;
		}
	}
}