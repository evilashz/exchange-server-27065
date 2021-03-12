using System;
using System.Runtime.InteropServices;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020002B5 RID: 693
	internal struct NATIVE_RECPOS
	{
		// Token: 0x040007E7 RID: 2023
		public static readonly int Size = Marshal.SizeOf(typeof(NATIVE_RECPOS));

		// Token: 0x040007E8 RID: 2024
		public uint cbStruct;

		// Token: 0x040007E9 RID: 2025
		public uint centriesLT;

		// Token: 0x040007EA RID: 2026
		public uint centriesInRange;

		// Token: 0x040007EB RID: 2027
		public uint centriesTotal;
	}
}
