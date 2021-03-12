using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x02000260 RID: 608
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal struct _SBinaryArray
	{
		// Token: 0x06000AA8 RID: 2728 RVA: 0x00033A39 File Offset: 0x00031C39
		internal static byte[][] Unmarshal(SafeExLinkedMemoryHandle array)
		{
			return _SBinaryArray.Unmarshal(array.DangerousGetHandle());
		}

		// Token: 0x06000AA9 RID: 2729 RVA: 0x00033A48 File Offset: 0x00031C48
		internal static byte[][] Unmarshal(IntPtr array)
		{
			int num = Marshal.ReadInt32(array, _SBinaryArray.CountOffset);
			byte[][] array2 = new byte[num][];
			IntPtr intPtr = Marshal.ReadIntPtr(array, _SBinaryArray.DataOffset);
			for (int i = 0; i < num; i++)
			{
				array2[i] = ((intPtr != IntPtr.Zero) ? _SBinary.Unmarshal(intPtr) : null);
				intPtr = (IntPtr)((long)intPtr + (long)_SBinary.SizeOf);
			}
			return array2;
		}

		// Token: 0x040010C0 RID: 4288
		public static readonly int SizeOf = Marshal.SizeOf(typeof(_SBinaryArray));

		// Token: 0x040010C1 RID: 4289
		private static readonly int CountOffset = (int)Marshal.OffsetOf(typeof(_SBinaryArray), "cValues");

		// Token: 0x040010C2 RID: 4290
		private static readonly int DataOffset = (int)Marshal.OffsetOf(typeof(_SBinaryArray), "lpbin");

		// Token: 0x040010C3 RID: 4291
		internal int cValues;

		// Token: 0x040010C4 RID: 4292
		internal unsafe _SBinary* lpbin;
	}
}
