using System;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.AirSync.SyncStateConverter
{
	// Token: 0x0200027F RID: 639
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	internal struct TagNodeStruct
	{
		// Token: 0x04000E75 RID: 3701
		public IntPtr Next;

		// Token: 0x04000E76 RID: 3702
		public ushort NameSpace;

		// Token: 0x04000E77 RID: 3703
		public ushort Tag;
	}
}
