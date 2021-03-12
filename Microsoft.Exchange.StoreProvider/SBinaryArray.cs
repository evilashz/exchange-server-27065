using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi.Unmanaged;

namespace Microsoft.Mapi
{
	// Token: 0x0200024B RID: 587
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class SBinaryArray
	{
		// Token: 0x06000A6D RID: 2669 RVA: 0x00032D30 File Offset: 0x00030F30
		internal static int GetBytesToMarshal(SBinary[] sbins)
		{
			int num = _SBinaryArray.SizeOf + 7 & -8;
			for (int i = 0; i < sbins.Length; i++)
			{
				num += sbins[i].GetBytesToMarshal();
			}
			return num;
		}

		// Token: 0x06000A6E RID: 2670 RVA: 0x00032D64 File Offset: 0x00030F64
		internal unsafe static void MarshalToNative(byte* pb, SBinary[] sbins)
		{
			((_SBinaryArray*)pb)->cValues = sbins.Length;
			_SBinary* ptr = (_SBinary*)(pb + (_SBinaryArray.SizeOf + 7 & -8));
			((_SBinaryArray*)pb)->lpbin = ptr;
			byte* ptr2 = pb + (_SBinaryArray.SizeOf + 7 & -8) + (IntPtr)sbins.Length * (IntPtr)(_SBinary.SizeOf + 7 & -8);
			for (int i = 0; i < sbins.Length; i++)
			{
				sbins[i].MarshalToNative(ptr, ref ptr2);
				ptr++;
			}
		}

		// Token: 0x06000A6F RID: 2671 RVA: 0x00032DCF File Offset: 0x00030FCF
		public static byte[][] UnmarshalFromNative(IntPtr pEntryIds)
		{
			return _SBinaryArray.Unmarshal(pEntryIds);
		}
	}
}
