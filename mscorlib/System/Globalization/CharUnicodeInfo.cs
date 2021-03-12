using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Globalization
{
	// Token: 0x02000379 RID: 889
	[__DynamicallyInvokable]
	public static class CharUnicodeInfo
	{
		// Token: 0x06002CC9 RID: 11465 RVA: 0x000AB0EC File Offset: 0x000A92EC
		[SecuritySafeCritical]
		private unsafe static bool InitTable()
		{
			byte* globalizationResourceBytePtr = GlobalizationAssembly.GetGlobalizationResourceBytePtr(typeof(CharUnicodeInfo).Assembly, "charinfo.nlp");
			CharUnicodeInfo.UnicodeDataHeader* ptr = (CharUnicodeInfo.UnicodeDataHeader*)globalizationResourceBytePtr;
			CharUnicodeInfo.s_pCategoryLevel1Index = (ushort*)(globalizationResourceBytePtr + ptr->OffsetToCategoriesIndex);
			CharUnicodeInfo.s_pCategoriesValue = globalizationResourceBytePtr + ptr->OffsetToCategoriesValue;
			CharUnicodeInfo.s_pNumericLevel1Index = (ushort*)(globalizationResourceBytePtr + ptr->OffsetToNumbericIndex);
			CharUnicodeInfo.s_pNumericValues = globalizationResourceBytePtr + ptr->OffsetToNumbericValue;
			CharUnicodeInfo.s_pDigitValues = (CharUnicodeInfo.DigitValues*)(globalizationResourceBytePtr + ptr->OffsetToDigitValue);
			return true;
		}

		// Token: 0x06002CCA RID: 11466 RVA: 0x000AB15C File Offset: 0x000A935C
		internal static int InternalConvertToUtf32(string s, int index)
		{
			if (index < s.Length - 1)
			{
				int num = (int)(s[index] - '\ud800');
				if (num >= 0 && num <= 1023)
				{
					int num2 = (int)(s[index + 1] - '\udc00');
					if (num2 >= 0 && num2 <= 1023)
					{
						return num * 1024 + num2 + 65536;
					}
				}
			}
			return (int)s[index];
		}

		// Token: 0x06002CCB RID: 11467 RVA: 0x000AB1C4 File Offset: 0x000A93C4
		internal static int InternalConvertToUtf32(string s, int index, out int charLength)
		{
			charLength = 1;
			if (index < s.Length - 1)
			{
				int num = (int)(s[index] - '\ud800');
				if (num >= 0 && num <= 1023)
				{
					int num2 = (int)(s[index + 1] - '\udc00');
					if (num2 >= 0 && num2 <= 1023)
					{
						charLength++;
						return num * 1024 + num2 + 65536;
					}
				}
			}
			return (int)s[index];
		}

		// Token: 0x06002CCC RID: 11468 RVA: 0x000AB234 File Offset: 0x000A9434
		internal static bool IsWhiteSpace(string s, int index)
		{
			UnicodeCategory unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(s, index);
			return unicodeCategory - UnicodeCategory.SpaceSeparator <= 2;
		}

		// Token: 0x06002CCD RID: 11469 RVA: 0x000AB254 File Offset: 0x000A9454
		internal static bool IsWhiteSpace(char c)
		{
			UnicodeCategory unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
			return unicodeCategory - UnicodeCategory.SpaceSeparator <= 2;
		}

		// Token: 0x06002CCE RID: 11470 RVA: 0x000AB274 File Offset: 0x000A9474
		[SecuritySafeCritical]
		internal unsafe static double InternalGetNumericValue(int ch)
		{
			ushort num = CharUnicodeInfo.s_pNumericLevel1Index[ch >> 8];
			num = CharUnicodeInfo.s_pNumericLevel1Index[(int)num + (ch >> 4 & 15)];
			byte* ptr = (byte*)(CharUnicodeInfo.s_pNumericLevel1Index + num);
			byte* ptr2 = CharUnicodeInfo.s_pNumericValues + ptr[ch & 15] * 8;
			if (ptr2 % 8L != null)
			{
				double result;
				byte* dest = (byte*)(&result);
				Buffer.Memcpy(dest, ptr2, 8);
				return result;
			}
			return *(double*)(CharUnicodeInfo.s_pNumericValues + (IntPtr)ptr[ch & 15] * 8);
		}

		// Token: 0x06002CCF RID: 11471 RVA: 0x000AB2E8 File Offset: 0x000A94E8
		[SecuritySafeCritical]
		internal unsafe static CharUnicodeInfo.DigitValues* InternalGetDigitValues(int ch)
		{
			ushort num = CharUnicodeInfo.s_pNumericLevel1Index[ch >> 8];
			num = CharUnicodeInfo.s_pNumericLevel1Index[(int)num + (ch >> 4 & 15)];
			byte* ptr = (byte*)(CharUnicodeInfo.s_pNumericLevel1Index + num);
			return CharUnicodeInfo.s_pDigitValues + ptr[ch & 15];
		}

		// Token: 0x06002CD0 RID: 11472 RVA: 0x000AB338 File Offset: 0x000A9538
		[SecuritySafeCritical]
		internal unsafe static sbyte InternalGetDecimalDigitValue(int ch)
		{
			return CharUnicodeInfo.InternalGetDigitValues(ch)->decimalDigit;
		}

		// Token: 0x06002CD1 RID: 11473 RVA: 0x000AB345 File Offset: 0x000A9545
		[SecuritySafeCritical]
		internal unsafe static sbyte InternalGetDigitValue(int ch)
		{
			return CharUnicodeInfo.InternalGetDigitValues(ch)->digit;
		}

		// Token: 0x06002CD2 RID: 11474 RVA: 0x000AB352 File Offset: 0x000A9552
		[__DynamicallyInvokable]
		public static double GetNumericValue(char ch)
		{
			return CharUnicodeInfo.InternalGetNumericValue((int)ch);
		}

		// Token: 0x06002CD3 RID: 11475 RVA: 0x000AB35A File Offset: 0x000A955A
		[__DynamicallyInvokable]
		public static double GetNumericValue(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index < 0 || index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
			}
			return CharUnicodeInfo.InternalGetNumericValue(CharUnicodeInfo.InternalConvertToUtf32(s, index));
		}

		// Token: 0x06002CD4 RID: 11476 RVA: 0x000AB398 File Offset: 0x000A9598
		public static int GetDecimalDigitValue(char ch)
		{
			return (int)CharUnicodeInfo.InternalGetDecimalDigitValue((int)ch);
		}

		// Token: 0x06002CD5 RID: 11477 RVA: 0x000AB3A0 File Offset: 0x000A95A0
		public static int GetDecimalDigitValue(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index < 0 || index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
			}
			return (int)CharUnicodeInfo.InternalGetDecimalDigitValue(CharUnicodeInfo.InternalConvertToUtf32(s, index));
		}

		// Token: 0x06002CD6 RID: 11478 RVA: 0x000AB3DE File Offset: 0x000A95DE
		public static int GetDigitValue(char ch)
		{
			return (int)CharUnicodeInfo.InternalGetDigitValue((int)ch);
		}

		// Token: 0x06002CD7 RID: 11479 RVA: 0x000AB3E6 File Offset: 0x000A95E6
		public static int GetDigitValue(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index < 0 || index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
			}
			return (int)CharUnicodeInfo.InternalGetDigitValue(CharUnicodeInfo.InternalConvertToUtf32(s, index));
		}

		// Token: 0x06002CD8 RID: 11480 RVA: 0x000AB424 File Offset: 0x000A9624
		[__DynamicallyInvokable]
		public static UnicodeCategory GetUnicodeCategory(char ch)
		{
			return CharUnicodeInfo.InternalGetUnicodeCategory((int)ch);
		}

		// Token: 0x06002CD9 RID: 11481 RVA: 0x000AB42C File Offset: 0x000A962C
		[__DynamicallyInvokable]
		public static UnicodeCategory GetUnicodeCategory(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			return CharUnicodeInfo.InternalGetUnicodeCategory(s, index);
		}

		// Token: 0x06002CDA RID: 11482 RVA: 0x000AB457 File Offset: 0x000A9657
		internal static UnicodeCategory InternalGetUnicodeCategory(int ch)
		{
			return (UnicodeCategory)CharUnicodeInfo.InternalGetCategoryValue(ch, 0);
		}

		// Token: 0x06002CDB RID: 11483 RVA: 0x000AB460 File Offset: 0x000A9660
		[SecuritySafeCritical]
		internal unsafe static byte InternalGetCategoryValue(int ch, int offset)
		{
			ushort num = CharUnicodeInfo.s_pCategoryLevel1Index[ch >> 8];
			num = CharUnicodeInfo.s_pCategoryLevel1Index[(int)num + (ch >> 4 & 15)];
			byte* ptr = (byte*)(CharUnicodeInfo.s_pCategoryLevel1Index + num);
			byte b = ptr[ch & 15];
			return CharUnicodeInfo.s_pCategoriesValue[(int)(b * 2) + offset];
		}

		// Token: 0x06002CDC RID: 11484 RVA: 0x000AB4B0 File Offset: 0x000A96B0
		internal static BidiCategory GetBidiCategory(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			return (BidiCategory)CharUnicodeInfo.InternalGetCategoryValue(CharUnicodeInfo.InternalConvertToUtf32(s, index), 1);
		}

		// Token: 0x06002CDD RID: 11485 RVA: 0x000AB4E1 File Offset: 0x000A96E1
		internal static UnicodeCategory InternalGetUnicodeCategory(string value, int index)
		{
			return CharUnicodeInfo.InternalGetUnicodeCategory(CharUnicodeInfo.InternalConvertToUtf32(value, index));
		}

		// Token: 0x06002CDE RID: 11486 RVA: 0x000AB4EF File Offset: 0x000A96EF
		internal static UnicodeCategory InternalGetUnicodeCategory(string str, int index, out int charLength)
		{
			return CharUnicodeInfo.InternalGetUnicodeCategory(CharUnicodeInfo.InternalConvertToUtf32(str, index, out charLength));
		}

		// Token: 0x06002CDF RID: 11487 RVA: 0x000AB4FE File Offset: 0x000A96FE
		internal static bool IsCombiningCategory(UnicodeCategory uc)
		{
			return uc == UnicodeCategory.NonSpacingMark || uc == UnicodeCategory.SpacingCombiningMark || uc == UnicodeCategory.EnclosingMark;
		}

		// Token: 0x04001244 RID: 4676
		internal const char HIGH_SURROGATE_START = '\ud800';

		// Token: 0x04001245 RID: 4677
		internal const char HIGH_SURROGATE_END = '\udbff';

		// Token: 0x04001246 RID: 4678
		internal const char LOW_SURROGATE_START = '\udc00';

		// Token: 0x04001247 RID: 4679
		internal const char LOW_SURROGATE_END = '\udfff';

		// Token: 0x04001248 RID: 4680
		internal const int UNICODE_CATEGORY_OFFSET = 0;

		// Token: 0x04001249 RID: 4681
		internal const int BIDI_CATEGORY_OFFSET = 1;

		// Token: 0x0400124A RID: 4682
		private static bool s_initialized = CharUnicodeInfo.InitTable();

		// Token: 0x0400124B RID: 4683
		[SecurityCritical]
		private unsafe static ushort* s_pCategoryLevel1Index;

		// Token: 0x0400124C RID: 4684
		[SecurityCritical]
		private unsafe static byte* s_pCategoriesValue;

		// Token: 0x0400124D RID: 4685
		[SecurityCritical]
		private unsafe static ushort* s_pNumericLevel1Index;

		// Token: 0x0400124E RID: 4686
		[SecurityCritical]
		private unsafe static byte* s_pNumericValues;

		// Token: 0x0400124F RID: 4687
		[SecurityCritical]
		private unsafe static CharUnicodeInfo.DigitValues* s_pDigitValues;

		// Token: 0x04001250 RID: 4688
		internal const string UNICODE_INFO_FILE_NAME = "charinfo.nlp";

		// Token: 0x04001251 RID: 4689
		internal const int UNICODE_PLANE01_START = 65536;

		// Token: 0x02000B33 RID: 2867
		[StructLayout(LayoutKind.Explicit)]
		internal struct UnicodeDataHeader
		{
			// Token: 0x0400336A RID: 13162
			[FieldOffset(0)]
			internal char TableName;

			// Token: 0x0400336B RID: 13163
			[FieldOffset(32)]
			internal ushort version;

			// Token: 0x0400336C RID: 13164
			[FieldOffset(40)]
			internal uint OffsetToCategoriesIndex;

			// Token: 0x0400336D RID: 13165
			[FieldOffset(44)]
			internal uint OffsetToCategoriesValue;

			// Token: 0x0400336E RID: 13166
			[FieldOffset(48)]
			internal uint OffsetToNumbericIndex;

			// Token: 0x0400336F RID: 13167
			[FieldOffset(52)]
			internal uint OffsetToDigitValue;

			// Token: 0x04003370 RID: 13168
			[FieldOffset(56)]
			internal uint OffsetToNumbericValue;
		}

		// Token: 0x02000B34 RID: 2868
		[StructLayout(LayoutKind.Sequential, Pack = 2)]
		internal struct DigitValues
		{
			// Token: 0x04003371 RID: 13169
			internal sbyte decimalDigit;

			// Token: 0x04003372 RID: 13170
			internal sbyte digit;
		}
	}
}
