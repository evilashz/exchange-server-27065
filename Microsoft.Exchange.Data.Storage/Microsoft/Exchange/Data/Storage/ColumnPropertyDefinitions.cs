using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200079E RID: 1950
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class ColumnPropertyDefinitions
	{
		// Token: 0x0600499B RID: 18843 RVA: 0x00134436 File Offset: 0x00132636
		public ColumnPropertyDefinitions(ICollection<PropertyDefinition> propertyDefinitions, ICollection<PropertyDefinition> simplePropertyDefinitions, ICollection<PropTag> propertyTags)
		{
			this.PropertyDefinitions = propertyDefinitions;
			this.SimplePropertyDefinitions = simplePropertyDefinitions;
			this.PropertyTags = propertyTags;
		}

		// Token: 0x040027A0 RID: 10144
		public readonly ICollection<PropertyDefinition> PropertyDefinitions;

		// Token: 0x040027A1 RID: 10145
		public readonly ICollection<PropertyDefinition> SimplePropertyDefinitions;

		// Token: 0x040027A2 RID: 10146
		public readonly ICollection<PropTag> PropertyTags;
	}
}
