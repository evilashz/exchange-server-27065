using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Data.Directory.GlobalLocatorService
{
	// Token: 0x0200011F RID: 287
	internal class FindTenantResult
	{
		// Token: 0x06000BF8 RID: 3064 RVA: 0x00036997 File Offset: 0x00034B97
		internal FindTenantResult(IDictionary<TenantProperty, PropertyValue> properties)
		{
			this.properties = properties;
		}

		// Token: 0x06000BF9 RID: 3065 RVA: 0x000369A8 File Offset: 0x00034BA8
		internal PropertyValue GetPropertyValue(TenantProperty property)
		{
			PropertyValue result;
			if (!this.properties.TryGetValue(property, out result))
			{
				return PropertyValue.Create(null, property);
			}
			return result;
		}

		// Token: 0x06000BFA RID: 3066 RVA: 0x000369CE File Offset: 0x00034BCE
		internal bool HasProperties()
		{
			return this.properties.Count > 0;
		}

		// Token: 0x04000633 RID: 1587
		private IDictionary<TenantProperty, PropertyValue> properties;
	}
}
