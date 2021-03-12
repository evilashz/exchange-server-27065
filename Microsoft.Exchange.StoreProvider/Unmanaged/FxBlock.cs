using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x02000269 RID: 617
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal struct FxBlock
	{
		// Token: 0x040010E5 RID: 4325
		public static readonly int SizeOf = Marshal.SizeOf(typeof(FxBlock));

		// Token: 0x040010E6 RID: 4326
		public IntPtr buffer;

		// Token: 0x040010E7 RID: 4327
		public int bufferSize;

		// Token: 0x040010E8 RID: 4328
		public uint steps;

		// Token: 0x040010E9 RID: 4329
		public uint progress;

		// Token: 0x040010EA RID: 4330
		public ushort state;
	}
}
