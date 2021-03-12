using System;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000CD4 RID: 3284
	[Flags]
	[Serializable]
	internal enum StorePropertyCapabilities
	{
		// Token: 0x04004F09 RID: 20233
		None = 0,
		// Token: 0x04004F0A RID: 20234
		CanQuery = 1,
		// Token: 0x04004F0B RID: 20235
		CanSortBy = 2,
		// Token: 0x04004F0C RID: 20236
		CanGroupBy = 4,
		// Token: 0x04004F0D RID: 20237
		All = 7
	}
}
