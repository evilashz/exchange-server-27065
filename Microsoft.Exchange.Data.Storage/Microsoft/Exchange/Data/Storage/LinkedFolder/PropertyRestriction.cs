using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.LinkedFolder
{
	// Token: 0x02000981 RID: 2433
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class PropertyRestriction
	{
		// Token: 0x060059F0 RID: 23024 RVA: 0x00174D7D File Offset: 0x00172F7D
		public bool ShouldBlock(StorePropertyDefinition propertyDefinition, bool isLinked)
		{
			if (isLinked)
			{
				return this.BlockAfterLink.Contains(propertyDefinition);
			}
			return this.BlockBeforeLink.Contains(propertyDefinition);
		}

		// Token: 0x04003173 RID: 12659
		public readonly HashSet<StorePropertyDefinition> BlockAfterLink = new HashSet<StorePropertyDefinition>();

		// Token: 0x04003174 RID: 12660
		public readonly HashSet<StorePropertyDefinition> BlockBeforeLink = new HashSet<StorePropertyDefinition>();
	}
}
