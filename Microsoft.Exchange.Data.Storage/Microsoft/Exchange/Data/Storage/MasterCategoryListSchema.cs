using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C3D RID: 3133
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class MasterCategoryListSchema : Schema
	{
		// Token: 0x17001DF6 RID: 7670
		// (get) Token: 0x06006EEF RID: 28399 RVA: 0x001DD6F1 File Offset: 0x001DB8F1
		public new static MasterCategoryListSchema Instance
		{
			get
			{
				if (MasterCategoryListSchema.instance == null)
				{
					MasterCategoryListSchema.instance = new MasterCategoryListSchema();
				}
				return MasterCategoryListSchema.instance;
			}
		}

		// Token: 0x04004226 RID: 16934
		public static StorePropertyDefinition DefaultCategory = InternalSchema.CategoryListDefaultCategory;

		// Token: 0x04004227 RID: 16935
		public static StorePropertyDefinition LastSavedTime = InternalSchema.CategoryListLastSavedTime;

		// Token: 0x04004228 RID: 16936
		private static MasterCategoryListSchema instance = null;
	}
}
