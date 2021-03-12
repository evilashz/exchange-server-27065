using System;
using System.Runtime.InteropServices;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x0200002A RID: 42
	internal struct NATIVE_CHECKPOINTINFO
	{
		// Token: 0x0400007E RID: 126
		public static readonly int Size = Marshal.SizeOf(typeof(NATIVE_CHECKPOINTINFO));

		// Token: 0x0400007F RID: 127
		public uint genMin;

		// Token: 0x04000080 RID: 128
		public uint genMax;

		// Token: 0x04000081 RID: 129
		public JET_LOGTIME logtimeGenMaxCreate;
	}
}
