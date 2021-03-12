using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x020002E3 RID: 739
	[ClassAccessLevel(AccessLevel.Implementation)]
	[StructLayout(LayoutKind.Explicit)]
	internal struct SRow
	{
		// Token: 0x06000F96 RID: 3990 RVA: 0x0003801E File Offset: 0x0003621E
		internal static PropValue[] Unmarshal(IntPtr array)
		{
			return SRow.Unmarshal(array, false);
		}

		// Token: 0x06000F97 RID: 3991 RVA: 0x00038028 File Offset: 0x00036228
		internal static PropValue[] Unmarshal(IntPtr array, bool retainAnsiStrings)
		{
			int num = Marshal.ReadInt32(array, SRow.CountOffset);
			PropValue[] array2 = new PropValue[num];
			IntPtr intPtr = Marshal.ReadIntPtr(array, SRow.DataOffset);
			for (int i = 0; i < num; i++)
			{
				array2[i] = SPropValue.Unmarshal(intPtr, retainAnsiStrings);
				intPtr = (IntPtr)((long)intPtr + (long)SPropValue.SizeOf);
			}
			return array2;
		}

		// Token: 0x04001229 RID: 4649
		public static readonly int SizeOf = Marshal.SizeOf(typeof(SRow));

		// Token: 0x0400122A RID: 4650
		private static readonly int CountOffset = (int)Marshal.OffsetOf(typeof(SRow), "cValues");

		// Token: 0x0400122B RID: 4651
		internal static readonly int DataOffset = (int)Marshal.OffsetOf(typeof(SRow), "lpProps");

		// Token: 0x0400122C RID: 4652
		[FieldOffset(4)]
		internal int cValues;

		// Token: 0x0400122D RID: 4653
		[FieldOffset(8)]
		internal IntPtr lpProps;
	}
}
