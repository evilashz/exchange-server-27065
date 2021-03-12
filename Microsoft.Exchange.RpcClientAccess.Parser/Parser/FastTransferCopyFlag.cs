using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020002B2 RID: 690
	[Flags]
	internal enum FastTransferCopyFlag : uint
	{
		// Token: 0x040007D4 RID: 2004
		None = 0U,
		// Token: 0x040007D5 RID: 2005
		CopyMailboxPerUserData = 8U,
		// Token: 0x040007D6 RID: 2006
		CopyFolderPerUserData = 16U,
		// Token: 0x040007D7 RID: 2007
		MoveUser = 128U,
		// Token: 0x040007D8 RID: 2008
		CopySubfolders = 16U,
		// Token: 0x040007D9 RID: 2009
		SendEntryId = 32U,
		// Token: 0x040007DA RID: 2010
		Transport = 256U,
		// Token: 0x040007DB RID: 2011
		RecoverMode = 512U,
		// Token: 0x040007DC RID: 2012
		ForceUnicode = 1024U,
		// Token: 0x040007DD RID: 2013
		FastTrasferStream = 2048U,
		// Token: 0x040007DE RID: 2014
		BestBody = 8192U,
		// Token: 0x040007DF RID: 2015
		Unicode = 2147483648U
	}
}
