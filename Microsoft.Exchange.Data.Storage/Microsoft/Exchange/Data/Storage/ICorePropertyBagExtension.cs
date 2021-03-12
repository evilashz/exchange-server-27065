using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000AF2 RID: 2802
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal static class ICorePropertyBagExtension
	{
		// Token: 0x060065C5 RID: 26053 RVA: 0x001B0A9B File Offset: 0x001AEC9B
		internal static void SetOrDeleteProperty(this ICorePropertyBag propertyBag, PropertyDefinition property, object newValue)
		{
			if (property == null)
			{
				throw new ArgumentNullException("property");
			}
			if (newValue == null || PropertyError.IsPropertyNotFound(newValue))
			{
				propertyBag.Delete(property);
				return;
			}
			propertyBag.SetProperty(property, newValue);
		}
	}
}
