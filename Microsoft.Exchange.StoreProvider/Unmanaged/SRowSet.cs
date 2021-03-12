using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x020002E4 RID: 740
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal struct SRowSet
	{
		// Token: 0x06000F99 RID: 3993 RVA: 0x000380E5 File Offset: 0x000362E5
		internal static PropValue[][] Unmarshal(SafeHandle array)
		{
			return SRowSet.Unmarshal(array, false);
		}

		// Token: 0x06000F9A RID: 3994 RVA: 0x000380F0 File Offset: 0x000362F0
		internal static PropValue[][] Unmarshal(SafeHandle array, bool retainAnsiStrings)
		{
			if (array.IsInvalid)
			{
				return Array<PropValue[]>.Empty;
			}
			int num = Marshal.ReadInt32(array.DangerousGetHandle(), SRowSet.CountOffset);
			if (num == 0)
			{
				return Array<PropValue[]>.Empty;
			}
			PropValue[][] array2 = new PropValue[num][];
			IntPtr intPtr = (IntPtr)((long)array.DangerousGetHandle() + (long)SRowSet.DataOffset);
			for (int i = 0; i < num; i++)
			{
				array2[i] = SRow.Unmarshal(intPtr, retainAnsiStrings);
				intPtr = (IntPtr)((long)intPtr + (long)SRow.SizeOf);
			}
			return array2;
		}

		// Token: 0x06000F9B RID: 3995 RVA: 0x00038170 File Offset: 0x00036370
		[Conditional("ValidateMarshalled")]
		private unsafe static void Validate(IntPtr pointer, PropValue[][] expected)
		{
			SRowSet* ptr = (SRowSet*)pointer.ToPointer();
			if (ptr == null)
			{
				throw new InvalidOperationException("Should not be null");
			}
			PropValue[][] array = new PropValue[ptr->cRows][];
			SRow* ptr2 = &ptr->aRow;
			int i = 0;
			while (i < ptr->cRows)
			{
				array[i] = new PropValue[ptr2->cValues];
				SPropValue* ptr3 = (SPropValue*)ptr2->lpProps.ToPointer();
				for (int j = 0; j < ptr2->cValues; j++)
				{
					array[i][j] = new PropValue(ptr3 + j);
				}
				i++;
				ptr2++;
			}
			if (expected.Length != array.Length)
			{
				throw new InvalidOperationException("Lengthes don't match!");
			}
			for (int k = 0; k < expected.Length; k++)
			{
				if (expected[k].Length != array[k].Length)
				{
					throw new InvalidOperationException("Lengthes don't match!");
				}
				for (int l = 0; l < expected[k].Length; l++)
				{
					if (!expected[k][l].IsEqualTo(array[k][l]))
					{
						throw new InvalidOperationException("Property is not the same!");
					}
				}
			}
		}

		// Token: 0x0400122E RID: 4654
		public static readonly int SizeOf = Marshal.SizeOf(typeof(SRowSet));

		// Token: 0x0400122F RID: 4655
		private static readonly int CountOffset = (int)Marshal.OffsetOf(typeof(SRowSet), "cRows");

		// Token: 0x04001230 RID: 4656
		internal static readonly int DataOffset = (int)Marshal.OffsetOf(typeof(SRowSet), "aRow");

		// Token: 0x04001231 RID: 4657
		internal int cRows;

		// Token: 0x04001232 RID: 4658
		internal SRow aRow;
	}
}
