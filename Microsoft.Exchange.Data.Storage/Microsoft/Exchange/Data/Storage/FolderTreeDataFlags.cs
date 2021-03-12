using System;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000847 RID: 2119
	[Flags]
	internal enum FolderTreeDataFlags
	{
		// Token: 0x04002AF7 RID: 10999
		None = 0,
		// Token: 0x04002AF8 RID: 11000
		PublicFolder = 1,
		// Token: 0x04002AF9 RID: 11001
		PublicFolderFavorite = 2,
		// Token: 0x04002AFA RID: 11002
		ImapFolder = 4,
		// Token: 0x04002AFB RID: 11003
		DavFolder = 8,
		// Token: 0x04002AFC RID: 11004
		SharepointFolder = 16,
		// Token: 0x04002AFD RID: 11005
		RootFolder = 32,
		// Token: 0x04002AFE RID: 11006
		FATFolder = 64,
		// Token: 0x04002AFF RID: 11007
		WebFolder = 128,
		// Token: 0x04002B00 RID: 11008
		SharedOut = 256,
		// Token: 0x04002B01 RID: 11009
		SharedIn = 512,
		// Token: 0x04002B02 RID: 11010
		PersonFolder = 1024,
		// Token: 0x04002B03 RID: 11011
		ICalFolder = 2048,
		// Token: 0x04002B04 RID: 11012
		CalendarOverlaid = 4096,
		// Token: 0x04002B05 RID: 11013
		OneOffName = 8192,
		// Token: 0x04002B06 RID: 11014
		TodoFolder = 16384,
		// Token: 0x04002B07 RID: 11015
		IpfNote = 32768,
		// Token: 0x04002B08 RID: 11016
		IpfDocument = 65536,
		// Token: 0x04002B09 RID: 11017
		IsDefaultStore = 1048576
	}
}
