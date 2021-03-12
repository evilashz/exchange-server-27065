using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x0200025F RID: 607
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal struct _SBinary
	{
		// Token: 0x06000AA6 RID: 2726 RVA: 0x000339A4 File Offset: 0x00031BA4
		internal static byte[] Unmarshal(IntPtr array)
		{
			int num = Marshal.ReadInt32(array, _SBinary.CountOffset);
			byte[] array2 = new byte[num];
			IntPtr source = Marshal.ReadIntPtr(array, _SBinary.DataOffset);
			Marshal.Copy(source, array2, 0, num);
			return array2;
		}

		// Token: 0x040010BB RID: 4283
		public static readonly int SizeOf = Marshal.SizeOf(typeof(_SBinary));

		// Token: 0x040010BC RID: 4284
		private static readonly int CountOffset = (int)Marshal.OffsetOf(typeof(_SBinary), "cb");

		// Token: 0x040010BD RID: 4285
		private static readonly int DataOffset = (int)Marshal.OffsetOf(typeof(_SBinary), "lpb");

		// Token: 0x040010BE RID: 4286
		internal int cb;

		// Token: 0x040010BF RID: 4287
		internal unsafe byte* lpb;
	}
}
