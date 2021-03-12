using System;
using System.Runtime.InteropServices;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020002C5 RID: 709
	internal struct NATIVE_SETINFO
	{
		// Token: 0x0400084A RID: 2122
		public static readonly int Size = Marshal.SizeOf(typeof(NATIVE_SETINFO));

		// Token: 0x0400084B RID: 2123
		public uint cbStruct;

		// Token: 0x0400084C RID: 2124
		public uint ibLongValue;

		// Token: 0x0400084D RID: 2125
		public uint itagSequence;
	}
}
