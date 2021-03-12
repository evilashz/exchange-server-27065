using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x0200030C RID: 780
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class GetImItemList
	{
		// Token: 0x06001612 RID: 5650 RVA: 0x000726BA File Offset: 0x000708BA
		public GetImItemList(ExtendedPropertyUri[] extendedProperties, UnifiedContactStoreUtilities unifiedContactStoreUtilities)
		{
			if (unifiedContactStoreUtilities == null)
			{
				throw new ArgumentNullException("unifiedContactStoreUtilities");
			}
			this.unifiedContactStoreUtilities = unifiedContactStoreUtilities;
			this.extendedProperties = extendedProperties;
		}

		// Token: 0x06001613 RID: 5651 RVA: 0x000726DE File Offset: 0x000708DE
		public ImItemList Execute()
		{
			return this.unifiedContactStoreUtilities.GetImItemList(this.extendedProperties);
		}

		// Token: 0x04000ED7 RID: 3799
		private readonly UnifiedContactStoreUtilities unifiedContactStoreUtilities;

		// Token: 0x04000ED8 RID: 3800
		private readonly ExtendedPropertyUri[] extendedProperties;
	}
}
