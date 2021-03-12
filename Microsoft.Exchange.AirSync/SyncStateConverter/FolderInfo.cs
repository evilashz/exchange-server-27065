using System;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.AirSync.SyncStateConverter
{
	// Token: 0x02000274 RID: 628
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	internal struct FolderInfo
	{
		// Token: 0x04000E43 RID: 3651
		public uint Version;

		// Token: 0x04000E44 RID: 3652
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 51)]
		public char[] SyncKey;

		// Token: 0x04000E45 RID: 3653
		public uint NumItems;

		// Token: 0x04000E46 RID: 3654
		public IntPtr Foldernodes;
	}
}
