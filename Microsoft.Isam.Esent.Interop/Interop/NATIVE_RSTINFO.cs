using System;
using System.Runtime.InteropServices;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020002BE RID: 702
	internal struct NATIVE_RSTINFO
	{
		// Token: 0x04000827 RID: 2087
		public static readonly int SizeOfRstinfo = Marshal.SizeOf(typeof(NATIVE_RSTINFO));

		// Token: 0x04000828 RID: 2088
		public uint cbStruct;

		// Token: 0x04000829 RID: 2089
		public unsafe NATIVE_RSTMAP* rgrstmap;

		// Token: 0x0400082A RID: 2090
		public uint crstmap;

		// Token: 0x0400082B RID: 2091
		public JET_LGPOS lgposStop;

		// Token: 0x0400082C RID: 2092
		public JET_LOGTIME logtimeStop;

		// Token: 0x0400082D RID: 2093
		public NATIVE_PFNSTATUS pfnStatus;
	}
}
