using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Sync.Common.Properties.XSO
{
	// Token: 0x0200008F RID: 143
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IXSOPropertyManager
	{
		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060003CE RID: 974
		PropertyDefinition[] AllProperties { get; }

		// Token: 0x060003CF RID: 975
		void AddPropertyDefinition(PropertyDefinition propertyDefinition);
	}
}
