using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x0200025F RID: 607
	internal static class ObjectOutputHelper
	{
		// Token: 0x06001470 RID: 5232 RVA: 0x00040128 File Offset: 0x0003E328
		internal static bool TryConvertOutputProperty(object value, ConfigurableObject configurableObject, PropertyDefinition property, string propertyInStr, Delegate[] handlers, out object convertedValue)
		{
			convertedValue = null;
			bool result = false;
			object value2 = value;
			foreach (ConvertOutputPropertyDelegate convertOutputPropertyDelegate in handlers)
			{
				ConvertOutputPropertyEventArgs args = new ConvertOutputPropertyEventArgs(value2, configurableObject, property, propertyInStr);
				object obj;
				if (convertOutputPropertyDelegate(args, out obj))
				{
					value2 = obj;
					convertedValue = obj;
					result = true;
				}
			}
			return result;
		}
	}
}
