using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020002AD RID: 685
	[Flags]
	internal enum FastTransferCopyFolderFlag : byte
	{
		// Token: 0x040007BB RID: 1979
		None = 0,
		// Token: 0x040007BC RID: 1980
		Move = 1,
		// Token: 0x040007BD RID: 1981
		CopySubFolders = 16
	}
}
