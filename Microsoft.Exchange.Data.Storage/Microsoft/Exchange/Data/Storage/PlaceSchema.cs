using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000CA3 RID: 3235
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class PlaceSchema : ContactSchema
	{
		// Token: 0x17001E3F RID: 7743
		// (get) Token: 0x060070D3 RID: 28883 RVA: 0x001F4897 File Offset: 0x001F2A97
		public new static PlaceSchema Instance
		{
			get
			{
				if (PlaceSchema.instance == null)
				{
					PlaceSchema.instance = new PlaceSchema();
				}
				return PlaceSchema.instance;
			}
		}

		// Token: 0x04004E6C RID: 20076
		private static PlaceSchema instance = null;

		// Token: 0x04004E6D RID: 20077
		[Autoload]
		public static readonly StorePropertyDefinition LocationRelevanceRank = InternalSchema.LocationRelevanceRank;
	}
}
