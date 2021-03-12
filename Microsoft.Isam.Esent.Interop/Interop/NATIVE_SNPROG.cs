using System;
using System.Runtime.InteropServices;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020002CA RID: 714
	internal struct NATIVE_SNPROG
	{
		// Token: 0x0400085F RID: 2143
		public static readonly int Size = Marshal.SizeOf(typeof(NATIVE_SNPROG));

		// Token: 0x04000860 RID: 2144
		public uint cbStruct;

		// Token: 0x04000861 RID: 2145
		public uint cunitDone;

		// Token: 0x04000862 RID: 2146
		public uint cunitTotal;
	}
}
