using System;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020002E6 RID: 742
	internal static class StringCache
	{
		// Token: 0x06000D82 RID: 3458 RVA: 0x0001B175 File Offset: 0x00019375
		public static string TryToIntern(string s)
		{
			return string.IsInterned(s) ?? s;
		}

		// Token: 0x06000D83 RID: 3459 RVA: 0x0001B184 File Offset: 0x00019384
		public unsafe static string GetString(byte[] value, int startIndex, int count)
		{
			fixed (byte* ptr = value)
			{
				char* value2 = (char*)ptr + startIndex / 2;
				return StringCache.GetString(value2, 0, count / 2);
			}
		}

		// Token: 0x06000D84 RID: 3460 RVA: 0x0001B1BC File Offset: 0x000193BC
		private unsafe static string GetString(char* value, int startIndex, int count)
		{
			string text;
			if (count == 0)
			{
				text = string.Empty;
			}
			else if (count < 128)
			{
				uint num = StringCache.CalculateHash(value, startIndex, count);
				int num2 = (int)(num % 1031U);
				text = StringCache.CachedStrings[num2];
				if (text == null || !StringCache.AreEqual(text, value, startIndex, count))
				{
					text = StringCache.CreateNewString(value, startIndex, count);
					StringCache.CachedStrings[num2] = text;
				}
			}
			else
			{
				text = StringCache.CreateNewString(value, startIndex, count);
			}
			return text;
		}

		// Token: 0x06000D85 RID: 3461 RVA: 0x0001B220 File Offset: 0x00019420
		private unsafe static uint CalculateHash(char* value, int startIndex, int count)
		{
			uint num = 0U;
			for (int i = 0; i < count; i++)
			{
				num ^= (uint)value[startIndex + i];
				num *= 33U;
			}
			return num;
		}

		// Token: 0x06000D86 RID: 3462 RVA: 0x0001B250 File Offset: 0x00019450
		private unsafe static bool AreEqual(string s, char* value, int startIndex, int count)
		{
			if (s.Length != count)
			{
				return false;
			}
			for (int i = 0; i < s.Length; i++)
			{
				if (s[i] != value[startIndex + i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000D87 RID: 3463 RVA: 0x0001B28E File Offset: 0x0001948E
		private unsafe static string CreateNewString(char* value, int startIndex, int count)
		{
			return new string(value, startIndex, count);
		}

		// Token: 0x04000928 RID: 2344
		private const int MaxLengthToCache = 128;

		// Token: 0x04000929 RID: 2345
		private const int NumCachedBoxedValues = 1031;

		// Token: 0x0400092A RID: 2346
		private static readonly string[] CachedStrings = new string[1031];
	}
}
