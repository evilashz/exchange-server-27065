using System;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.AirSync.SyncStateConverter
{
	// Token: 0x0200027E RID: 638
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	internal struct MappingNodeStruct
	{
		// Token: 0x04000E72 RID: 3698
		public IntPtr Next;

		// Token: 0x04000E73 RID: 3699
		public string ShortId;

		// Token: 0x04000E74 RID: 3700
		public string LongId;
	}
}
