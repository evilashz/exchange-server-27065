using System;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000CCC RID: 3276
	[Flags]
	internal enum PropertyDependencyType
	{
		// Token: 0x04004EFC RID: 20220
		None = 0,
		// Token: 0x04004EFD RID: 20221
		NeedForRead = 1,
		// Token: 0x04004EFE RID: 20222
		NeedToReadForWrite = 2,
		// Token: 0x04004EFF RID: 20223
		AllRead = 3,
		// Token: 0x04004F00 RID: 20224
		All = 3
	}
}
