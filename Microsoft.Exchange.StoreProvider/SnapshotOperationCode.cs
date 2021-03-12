using System;

namespace Microsoft.Mapi
{
	// Token: 0x0200003D RID: 61
	internal enum SnapshotOperationCode : uint
	{
		// Token: 0x040003E9 RID: 1001
		None,
		// Token: 0x040003EA RID: 1002
		Prepare,
		// Token: 0x040003EB RID: 1003
		Freeze,
		// Token: 0x040003EC RID: 1004
		Thaw,
		// Token: 0x040003ED RID: 1005
		Truncate,
		// Token: 0x040003EE RID: 1006
		Stop,
		// Token: 0x040003EF RID: 1007
		Last
	}
}
