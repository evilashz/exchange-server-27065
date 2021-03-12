using System;
using System.Runtime.InteropServices;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020002C8 RID: 712
	[Serializable]
	internal struct NATIVE_SIGNATURE
	{
		// Token: 0x04000853 RID: 2131
		public const int ComputerNameSize = 16;

		// Token: 0x04000854 RID: 2132
		public static readonly int Size = Marshal.SizeOf(typeof(NATIVE_SIGNATURE));

		// Token: 0x04000855 RID: 2133
		public uint ulRandom;

		// Token: 0x04000856 RID: 2134
		public JET_LOGTIME logtimeCreate;

		// Token: 0x04000857 RID: 2135
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
		public string szComputerName;
	}
}
