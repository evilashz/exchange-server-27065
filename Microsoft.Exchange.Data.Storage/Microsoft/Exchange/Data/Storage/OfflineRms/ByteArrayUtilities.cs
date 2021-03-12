using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.OfflineRms
{
	// Token: 0x02000AD5 RID: 2773
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class ByteArrayUtilities
	{
		// Token: 0x060064B3 RID: 25779 RVA: 0x001AB46D File Offset: 0x001A966D
		private ByteArrayUtilities()
		{
		}

		// Token: 0x060064B4 RID: 25780 RVA: 0x001AB475 File Offset: 0x001A9675
		public static void Clear(byte[] array)
		{
			if (array != null)
			{
				Array.Clear(array, 0, array.Length);
			}
		}

		// Token: 0x060064B5 RID: 25781 RVA: 0x001AB484 File Offset: 0x001A9684
		public static byte[] CreateReversedArray(byte[] array)
		{
			if (array == null)
			{
				return null;
			}
			byte[] array2 = (byte[])array.Clone();
			Array.Reverse(array2);
			return array2;
		}

		// Token: 0x060064B6 RID: 25782 RVA: 0x001AB4A9 File Offset: 0x001A96A9
		public static uint ToUInt32(byte[] array)
		{
			return ByteArrayUtilities.ToUInt32(array, 0, array.Length);
		}

		// Token: 0x060064B7 RID: 25783 RVA: 0x001AB4B8 File Offset: 0x001A96B8
		public static uint ToUInt32(byte[] array, int offset, int length)
		{
			if (length > 4)
			{
				throw new ArgumentOutOfRangeException("length");
			}
			uint num = 0U;
			for (int i = offset + length - 1; i >= offset; i--)
			{
				num = (num << 8) + (uint)array[i];
			}
			return num;
		}
	}
}
