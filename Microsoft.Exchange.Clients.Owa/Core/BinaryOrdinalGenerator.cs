using System;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x020000D6 RID: 214
	internal static class BinaryOrdinalGenerator
	{
		// Token: 0x0600077A RID: 1914 RVA: 0x00039368 File Offset: 0x00037568
		public static byte[] GetInbetweenOrdinalValue(byte[] beforeOrdinal, byte[] afterOrdinal)
		{
			int num = 0;
			int num2 = Math.Max((beforeOrdinal != null) ? beforeOrdinal.Length : 0, (afterOrdinal != null) ? afterOrdinal.Length : 0) + 1;
			byte[] array = new byte[num2];
			if (beforeOrdinal != null && afterOrdinal != null && Utilities.CompareByteArrays(beforeOrdinal, afterOrdinal) >= 0)
			{
				throw new OwaInvalidOperationException("Previous ordinal value is greater then after ordinal value");
			}
			if (beforeOrdinal != null && BinaryOrdinalGenerator.CheckAllZero(beforeOrdinal))
			{
				beforeOrdinal = null;
			}
			if (afterOrdinal != null && BinaryOrdinalGenerator.CheckAllZero(afterOrdinal))
			{
				afterOrdinal = null;
			}
			byte beforeVal;
			byte afterVal;
			for (;;)
			{
				beforeVal = BinaryOrdinalGenerator.GetBeforeVal(num, beforeOrdinal);
				afterVal = BinaryOrdinalGenerator.GetAfterVal(num, afterOrdinal);
				if (afterVal > beforeVal + 1)
				{
					break;
				}
				array[num++] = beforeVal;
				if (beforeVal + 1 == afterVal)
				{
					afterOrdinal = null;
				}
			}
			array[num++] = (afterVal + beforeVal) / 2;
			byte[] array2 = new byte[num];
			Array.Copy(array, array2, num);
			return array2;
		}

		// Token: 0x0600077B RID: 1915 RVA: 0x00039420 File Offset: 0x00037620
		private static bool CheckAllZero(byte[] bytes)
		{
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes");
			}
			foreach (byte b in bytes)
			{
				if (b != 0)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600077C RID: 1916 RVA: 0x00039459 File Offset: 0x00037659
		private static byte GetValEx(int index, byte newValue, byte[] ordinal)
		{
			if (index >= ordinal.Length)
			{
				return newValue;
			}
			return ordinal[index];
		}

		// Token: 0x0600077D RID: 1917 RVA: 0x00039466 File Offset: 0x00037666
		private static byte GetBeforeVal(int index, byte[] beforeOrdinal)
		{
			if (beforeOrdinal == null)
			{
				return 0;
			}
			return BinaryOrdinalGenerator.GetValEx(index, 0, beforeOrdinal);
		}

		// Token: 0x0600077E RID: 1918 RVA: 0x00039475 File Offset: 0x00037675
		private static byte GetAfterVal(int index, byte[] afterOrdinal)
		{
			if (afterOrdinal == null)
			{
				return byte.MaxValue;
			}
			return BinaryOrdinalGenerator.GetValEx(index, byte.MaxValue, afterOrdinal);
		}

		// Token: 0x0400051E RID: 1310
		private const byte MinValue = 0;

		// Token: 0x0400051F RID: 1311
		private const byte MaxValue = 255;
	}
}
