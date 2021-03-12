using System;
using System.Runtime.InteropServices;

namespace System.Security
{
	// Token: 0x020001F5 RID: 501
	[ComVisible(true)]
	[Serializable]
	public enum SecurityZone
	{
		// Token: 0x04000A96 RID: 2710
		MyComputer,
		// Token: 0x04000A97 RID: 2711
		Intranet,
		// Token: 0x04000A98 RID: 2712
		Trusted,
		// Token: 0x04000A99 RID: 2713
		Internet,
		// Token: 0x04000A9A RID: 2714
		Untrusted,
		// Token: 0x04000A9B RID: 2715
		NoZone = -1
	}
}
