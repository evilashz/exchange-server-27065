using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x020000F9 RID: 249
	internal class ToServiceObjectForPropertyBagCommandSettings : ToServiceObjectCommandSettingsBase
	{
		// Token: 0x060006E4 RID: 1764 RVA: 0x00022B33 File Offset: 0x00020D33
		public ToServiceObjectForPropertyBagCommandSettings()
		{
		}

		// Token: 0x060006E5 RID: 1765 RVA: 0x00022B3B File Offset: 0x00020D3B
		public ToServiceObjectForPropertyBagCommandSettings(PropertyPath propertyPath) : base(propertyPath)
		{
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x060006E6 RID: 1766 RVA: 0x00022B44 File Offset: 0x00020D44
		// (set) Token: 0x060006E7 RID: 1767 RVA: 0x00022B4C File Offset: 0x00020D4C
		public IDictionary<PropertyDefinition, object> PropertyBag { get; set; }
	}
}
