using System;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x0200010E RID: 270
	internal static class ExtensionMethods
	{
		// Token: 0x06000B6D RID: 2925 RVA: 0x00034EE0 File Offset: 0x000330E0
		public static int GetHashCodeCaseInsensitive(this string value)
		{
			int length = value.Length;
			int num = 5381;
			for (int i = 0; i < length; i++)
			{
				int num2 = ExtensionMethods.ToLower((int)value[i]);
				num = (num << 5) + num + (num >> 27);
				num ^= num2 << 16 * (i & 1);
			}
			return num;
		}

		// Token: 0x06000B6E RID: 2926 RVA: 0x00034F30 File Offset: 0x00033130
		private static byte[] ComputeToLowerTable()
		{
			byte[] array = new byte[128];
			for (int i = 0; i < 128; i++)
			{
				array[i] = (byte)char.ToLowerInvariant((char)i);
			}
			return array;
		}

		// Token: 0x06000B6F RID: 2927 RVA: 0x00034F64 File Offset: 0x00033164
		private static int ToLower(int ch)
		{
			if (ch >= 128)
			{
				return (int)char.ToLowerInvariant((char)ch);
			}
			return (int)ExtensionMethods.toLower[ch];
		}

		// Token: 0x040005C1 RID: 1473
		private static readonly byte[] toLower = ExtensionMethods.ComputeToLowerTable();
	}
}
