using System;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x0200010C RID: 268
	[Flags]
	internal enum DirectoryBackendType : byte
	{
		// Token: 0x040005BC RID: 1468
		None = 0,
		// Token: 0x040005BD RID: 1469
		AD = 1,
		// Token: 0x040005BE RID: 1470
		MServ = 2,
		// Token: 0x040005BF RID: 1471
		Mbx = 4,
		// Token: 0x040005C0 RID: 1472
		SQL = 8
	}
}
