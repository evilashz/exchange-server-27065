using System;
using System.Runtime.InteropServices;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020002BA RID: 698
	internal struct NATIVE_RETINFO
	{
		// Token: 0x0400080C RID: 2060
		public static readonly int Size = Marshal.SizeOf(typeof(NATIVE_RETINFO));

		// Token: 0x0400080D RID: 2061
		public uint cbStruct;

		// Token: 0x0400080E RID: 2062
		public uint ibLongValue;

		// Token: 0x0400080F RID: 2063
		public uint itagSequence;

		// Token: 0x04000810 RID: 2064
		public uint columnidNextTagged;
	}
}
