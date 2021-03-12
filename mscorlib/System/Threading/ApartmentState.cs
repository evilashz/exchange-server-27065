using System;
using System.Runtime.InteropServices;

namespace System.Threading
{
	// Token: 0x02000509 RID: 1289
	[ComVisible(true)]
	[Serializable]
	public enum ApartmentState
	{
		// Token: 0x04001999 RID: 6553
		STA,
		// Token: 0x0400199A RID: 6554
		MTA,
		// Token: 0x0400199B RID: 6555
		Unknown
	}
}
