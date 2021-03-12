using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020002B0 RID: 688
	[Flags]
	internal enum FastTransferCopyPropertiesFlag : byte
	{
		// Token: 0x040007C8 RID: 1992
		None = 0,
		// Token: 0x040007C9 RID: 1993
		Move = 1,
		// Token: 0x040007CA RID: 1994
		FastTrasferStream = 2,
		// Token: 0x040007CB RID: 1995
		CopyMailboxPerUserData = 8,
		// Token: 0x040007CC RID: 1996
		CopyFolderPerUserData = 16
	}
}
