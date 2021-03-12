using System;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.AirSync.SyncStateConverter
{
	// Token: 0x0200027D RID: 637
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	internal struct CommonInfo
	{
		// Token: 0x04000E6A RID: 3690
		public uint Version;

		// Token: 0x04000E6B RID: 3691
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 51)]
		public char[] SyncKey;

		// Token: 0x04000E6C RID: 3692
		public uint NumSupportedTags;

		// Token: 0x04000E6D RID: 3693
		public IntPtr Tagnodes;

		// Token: 0x04000E6E RID: 3694
		public uint NumMapping;

		// Token: 0x04000E6F RID: 3695
		public IntPtr Mappingnodes;

		// Token: 0x04000E70 RID: 3696
		public uint NumCommonNodes;

		// Token: 0x04000E71 RID: 3697
		public IntPtr Nodes;
	}
}
