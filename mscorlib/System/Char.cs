using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;

namespace System
{
	// Token: 0x020000B6 RID: 182
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public struct Char : IComparable, IConvertible, IComparable<char>, IEquatable<char>
	{
		// Token: 0x06000A86 RID: 2694 RVA: 0x00021809 File Offset: 0x0001FA09
		private static bool IsLatin1(char ch)
		{
			return ch <= 'ÿ';
		}

		// Token: 0x06000A87 RID: 2695 RVA: 0x00021816 File Offset: 0x0001FA16
		private static bool IsAscii(char ch)
		{
			return ch <= '\u007f';
		}

		// Token: 0x06000A88 RID: 2696 RVA: 0x00021820 File Offset: 0x0001FA20
		private static UnicodeCategory GetLatin1UnicodeCategory(char ch)
		{
			return (UnicodeCategory)char.categoryForLatin1[(int)ch];
		}

		// Token: 0x06000A89 RID: 2697 RVA: 0x00021829 File Offset: 0x0001FA29
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			return (int)(this | (int)this << 16);
		}

		// Token: 0x06000A8A RID: 2698 RVA: 0x00021833 File Offset: 0x0001FA33
		[__DynamicallyInvokable]
		public override bool Equals(object obj)
		{
			return obj is char && this == (char)obj;
		}

		// Token: 0x06000A8B RID: 2699 RVA: 0x00021849 File Offset: 0x0001FA49
		[NonVersionable]
		[__DynamicallyInvokable]
		public bool Equals(char obj)
		{
			return this == obj;
		}

		// Token: 0x06000A8C RID: 2700 RVA: 0x00021850 File Offset: 0x0001FA50
		public int CompareTo(object value)
		{
			if (value == null)
			{
				return 1;
			}
			if (!(value is char))
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBeChar"));
			}
			return (int)(this - (char)value);
		}

		// Token: 0x06000A8D RID: 2701 RVA: 0x00021878 File Offset: 0x0001FA78
		[__DynamicallyInvokable]
		public int CompareTo(char value)
		{
			return (int)(this - value);
		}

		// Token: 0x06000A8E RID: 2702 RVA: 0x0002187E File Offset: 0x0001FA7E
		[__DynamicallyInvokable]
		public override string ToString()
		{
			return char.ToString(this);
		}

		// Token: 0x06000A8F RID: 2703 RVA: 0x00021887 File Offset: 0x0001FA87
		public string ToString(IFormatProvider provider)
		{
			return char.ToString(this);
		}

		// Token: 0x06000A90 RID: 2704 RVA: 0x00021890 File Offset: 0x0001FA90
		[__DynamicallyInvokable]
		public static string ToString(char c)
		{
			return new string(c, 1);
		}

		// Token: 0x06000A91 RID: 2705 RVA: 0x00021899 File Offset: 0x0001FA99
		[__DynamicallyInvokable]
		public static char Parse(string s)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (s.Length != 1)
			{
				throw new FormatException(Environment.GetResourceString("Format_NeedSingleChar"));
			}
			return s[0];
		}

		// Token: 0x06000A92 RID: 2706 RVA: 0x000218C9 File Offset: 0x0001FAC9
		[__DynamicallyInvokable]
		public static bool TryParse(string s, out char result)
		{
			result = '\0';
			if (s == null)
			{
				return false;
			}
			if (s.Length != 1)
			{
				return false;
			}
			result = s[0];
			return true;
		}

		// Token: 0x06000A93 RID: 2707 RVA: 0x000218E8 File Offset: 0x0001FAE8
		[__DynamicallyInvokable]
		public static bool IsDigit(char c)
		{
			if (char.IsLatin1(c))
			{
				return c >= '0' && c <= '9';
			}
			return CharUnicodeInfo.GetUnicodeCategory(c) == UnicodeCategory.DecimalDigitNumber;
		}

		// Token: 0x06000A94 RID: 2708 RVA: 0x0002190B File Offset: 0x0001FB0B
		internal static bool CheckLetter(UnicodeCategory uc)
		{
			return uc <= UnicodeCategory.OtherLetter;
		}

		// Token: 0x06000A95 RID: 2709 RVA: 0x00021914 File Offset: 0x0001FB14
		[__DynamicallyInvokable]
		public static bool IsLetter(char c)
		{
			if (!char.IsLatin1(c))
			{
				return char.CheckLetter(CharUnicodeInfo.GetUnicodeCategory(c));
			}
			if (char.IsAscii(c))
			{
				c |= ' ';
				return c >= 'a' && c <= 'z';
			}
			return char.CheckLetter(char.GetLatin1UnicodeCategory(c));
		}

		// Token: 0x06000A96 RID: 2710 RVA: 0x00021954 File Offset: 0x0001FB54
		private static bool IsWhiteSpaceLatin1(char c)
		{
			return c == ' ' || (c >= '\t' && c <= '\r') || c == '\u00a0' || c == '\u0085';
		}

		// Token: 0x06000A97 RID: 2711 RVA: 0x00021978 File Offset: 0x0001FB78
		[__DynamicallyInvokable]
		public static bool IsWhiteSpace(char c)
		{
			if (char.IsLatin1(c))
			{
				return char.IsWhiteSpaceLatin1(c);
			}
			return CharUnicodeInfo.IsWhiteSpace(c);
		}

		// Token: 0x06000A98 RID: 2712 RVA: 0x0002198F File Offset: 0x0001FB8F
		[__DynamicallyInvokable]
		public static bool IsUpper(char c)
		{
			if (!char.IsLatin1(c))
			{
				return CharUnicodeInfo.GetUnicodeCategory(c) == UnicodeCategory.UppercaseLetter;
			}
			if (char.IsAscii(c))
			{
				return c >= 'A' && c <= 'Z';
			}
			return char.GetLatin1UnicodeCategory(c) == UnicodeCategory.UppercaseLetter;
		}

		// Token: 0x06000A99 RID: 2713 RVA: 0x000219C4 File Offset: 0x0001FBC4
		[__DynamicallyInvokable]
		public static bool IsLower(char c)
		{
			if (!char.IsLatin1(c))
			{
				return CharUnicodeInfo.GetUnicodeCategory(c) == UnicodeCategory.LowercaseLetter;
			}
			if (char.IsAscii(c))
			{
				return c >= 'a' && c <= 'z';
			}
			return char.GetLatin1UnicodeCategory(c) == UnicodeCategory.LowercaseLetter;
		}

		// Token: 0x06000A9A RID: 2714 RVA: 0x000219F9 File Offset: 0x0001FBF9
		internal static bool CheckPunctuation(UnicodeCategory uc)
		{
			return uc - UnicodeCategory.ConnectorPunctuation <= 6;
		}

		// Token: 0x06000A9B RID: 2715 RVA: 0x00021A05 File Offset: 0x0001FC05
		[__DynamicallyInvokable]
		public static bool IsPunctuation(char c)
		{
			if (char.IsLatin1(c))
			{
				return char.CheckPunctuation(char.GetLatin1UnicodeCategory(c));
			}
			return char.CheckPunctuation(CharUnicodeInfo.GetUnicodeCategory(c));
		}

		// Token: 0x06000A9C RID: 2716 RVA: 0x00021A26 File Offset: 0x0001FC26
		internal static bool CheckLetterOrDigit(UnicodeCategory uc)
		{
			return uc <= UnicodeCategory.OtherLetter || uc == UnicodeCategory.DecimalDigitNumber;
		}

		// Token: 0x06000A9D RID: 2717 RVA: 0x00021A33 File Offset: 0x0001FC33
		[__DynamicallyInvokable]
		public static bool IsLetterOrDigit(char c)
		{
			if (char.IsLatin1(c))
			{
				return char.CheckLetterOrDigit(char.GetLatin1UnicodeCategory(c));
			}
			return char.CheckLetterOrDigit(CharUnicodeInfo.GetUnicodeCategory(c));
		}

		// Token: 0x06000A9E RID: 2718 RVA: 0x00021A54 File Offset: 0x0001FC54
		[__DynamicallyInvokable]
		public static char ToUpper(char c, CultureInfo culture)
		{
			if (culture == null)
			{
				throw new ArgumentNullException("culture");
			}
			return culture.TextInfo.ToUpper(c);
		}

		// Token: 0x06000A9F RID: 2719 RVA: 0x00021A70 File Offset: 0x0001FC70
		[__DynamicallyInvokable]
		public static char ToUpper(char c)
		{
			return char.ToUpper(c, CultureInfo.CurrentCulture);
		}

		// Token: 0x06000AA0 RID: 2720 RVA: 0x00021A7D File Offset: 0x0001FC7D
		[__DynamicallyInvokable]
		public static char ToUpperInvariant(char c)
		{
			return char.ToUpper(c, CultureInfo.InvariantCulture);
		}

		// Token: 0x06000AA1 RID: 2721 RVA: 0x00021A8A File Offset: 0x0001FC8A
		[__DynamicallyInvokable]
		public static char ToLower(char c, CultureInfo culture)
		{
			if (culture == null)
			{
				throw new ArgumentNullException("culture");
			}
			return culture.TextInfo.ToLower(c);
		}

		// Token: 0x06000AA2 RID: 2722 RVA: 0x00021AA6 File Offset: 0x0001FCA6
		[__DynamicallyInvokable]
		public static char ToLower(char c)
		{
			return char.ToLower(c, CultureInfo.CurrentCulture);
		}

		// Token: 0x06000AA3 RID: 2723 RVA: 0x00021AB3 File Offset: 0x0001FCB3
		[__DynamicallyInvokable]
		public static char ToLowerInvariant(char c)
		{
			return char.ToLower(c, CultureInfo.InvariantCulture);
		}

		// Token: 0x06000AA4 RID: 2724 RVA: 0x00021AC0 File Offset: 0x0001FCC0
		public TypeCode GetTypeCode()
		{
			return TypeCode.Char;
		}

		// Token: 0x06000AA5 RID: 2725 RVA: 0x00021AC3 File Offset: 0x0001FCC3
		[__DynamicallyInvokable]
		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", new object[]
			{
				"Char",
				"Boolean"
			}));
		}

		// Token: 0x06000AA6 RID: 2726 RVA: 0x00021AEA File Offset: 0x0001FCEA
		[__DynamicallyInvokable]
		char IConvertible.ToChar(IFormatProvider provider)
		{
			return this;
		}

		// Token: 0x06000AA7 RID: 2727 RVA: 0x00021AEE File Offset: 0x0001FCEE
		[__DynamicallyInvokable]
		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			return Convert.ToSByte(this);
		}

		// Token: 0x06000AA8 RID: 2728 RVA: 0x00021AF7 File Offset: 0x0001FCF7
		[__DynamicallyInvokable]
		byte IConvertible.ToByte(IFormatProvider provider)
		{
			return Convert.ToByte(this);
		}

		// Token: 0x06000AA9 RID: 2729 RVA: 0x00021B00 File Offset: 0x0001FD00
		[__DynamicallyInvokable]
		short IConvertible.ToInt16(IFormatProvider provider)
		{
			return Convert.ToInt16(this);
		}

		// Token: 0x06000AAA RID: 2730 RVA: 0x00021B09 File Offset: 0x0001FD09
		[__DynamicallyInvokable]
		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			return Convert.ToUInt16(this);
		}

		// Token: 0x06000AAB RID: 2731 RVA: 0x00021B12 File Offset: 0x0001FD12
		[__DynamicallyInvokable]
		int IConvertible.ToInt32(IFormatProvider provider)
		{
			return Convert.ToInt32(this);
		}

		// Token: 0x06000AAC RID: 2732 RVA: 0x00021B1B File Offset: 0x0001FD1B
		[__DynamicallyInvokable]
		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			return Convert.ToUInt32(this);
		}

		// Token: 0x06000AAD RID: 2733 RVA: 0x00021B24 File Offset: 0x0001FD24
		[__DynamicallyInvokable]
		long IConvertible.ToInt64(IFormatProvider provider)
		{
			return Convert.ToInt64(this);
		}

		// Token: 0x06000AAE RID: 2734 RVA: 0x00021B2D File Offset: 0x0001FD2D
		[__DynamicallyInvokable]
		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			return Convert.ToUInt64(this);
		}

		// Token: 0x06000AAF RID: 2735 RVA: 0x00021B36 File Offset: 0x0001FD36
		[__DynamicallyInvokable]
		float IConvertible.ToSingle(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", new object[]
			{
				"Char",
				"Single"
			}));
		}

		// Token: 0x06000AB0 RID: 2736 RVA: 0x00021B5D File Offset: 0x0001FD5D
		[__DynamicallyInvokable]
		double IConvertible.ToDouble(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", new object[]
			{
				"Char",
				"Double"
			}));
		}

		// Token: 0x06000AB1 RID: 2737 RVA: 0x00021B84 File Offset: 0x0001FD84
		[__DynamicallyInvokable]
		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", new object[]
			{
				"Char",
				"Decimal"
			}));
		}

		// Token: 0x06000AB2 RID: 2738 RVA: 0x00021BAB File Offset: 0x0001FDAB
		[__DynamicallyInvokable]
		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", new object[]
			{
				"Char",
				"DateTime"
			}));
		}

		// Token: 0x06000AB3 RID: 2739 RVA: 0x00021BD2 File Offset: 0x0001FDD2
		[__DynamicallyInvokable]
		object IConvertible.ToType(Type type, IFormatProvider provider)
		{
			return Convert.DefaultToType(this, type, provider);
		}

		// Token: 0x06000AB4 RID: 2740 RVA: 0x00021BE2 File Offset: 0x0001FDE2
		[__DynamicallyInvokable]
		public static bool IsControl(char c)
		{
			if (char.IsLatin1(c))
			{
				return char.GetLatin1UnicodeCategory(c) == UnicodeCategory.Control;
			}
			return CharUnicodeInfo.GetUnicodeCategory(c) == UnicodeCategory.Control;
		}

		// Token: 0x06000AB5 RID: 2741 RVA: 0x00021C04 File Offset: 0x0001FE04
		[__DynamicallyInvokable]
		public static bool IsControl(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			char ch = s[index];
			if (char.IsLatin1(ch))
			{
				return char.GetLatin1UnicodeCategory(ch) == UnicodeCategory.Control;
			}
			return CharUnicodeInfo.GetUnicodeCategory(s, index) == UnicodeCategory.Control;
		}

		// Token: 0x06000AB6 RID: 2742 RVA: 0x00021C5C File Offset: 0x0001FE5C
		[__DynamicallyInvokable]
		public static bool IsDigit(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			char c = s[index];
			if (char.IsLatin1(c))
			{
				return c >= '0' && c <= '9';
			}
			return CharUnicodeInfo.GetUnicodeCategory(s, index) == UnicodeCategory.DecimalDigitNumber;
		}

		// Token: 0x06000AB7 RID: 2743 RVA: 0x00021CB8 File Offset: 0x0001FEB8
		[__DynamicallyInvokable]
		public static bool IsLetter(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			char c = s[index];
			if (!char.IsLatin1(c))
			{
				return char.CheckLetter(CharUnicodeInfo.GetUnicodeCategory(s, index));
			}
			if (char.IsAscii(c))
			{
				c |= ' ';
				return c >= 'a' && c <= 'z';
			}
			return char.CheckLetter(char.GetLatin1UnicodeCategory(c));
		}

		// Token: 0x06000AB8 RID: 2744 RVA: 0x00021D30 File Offset: 0x0001FF30
		[__DynamicallyInvokable]
		public static bool IsLetterOrDigit(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			char ch = s[index];
			if (char.IsLatin1(ch))
			{
				return char.CheckLetterOrDigit(char.GetLatin1UnicodeCategory(ch));
			}
			return char.CheckLetterOrDigit(CharUnicodeInfo.GetUnicodeCategory(s, index));
		}

		// Token: 0x06000AB9 RID: 2745 RVA: 0x00021D88 File Offset: 0x0001FF88
		[__DynamicallyInvokable]
		public static bool IsLower(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			char c = s[index];
			if (!char.IsLatin1(c))
			{
				return CharUnicodeInfo.GetUnicodeCategory(s, index) == UnicodeCategory.LowercaseLetter;
			}
			if (char.IsAscii(c))
			{
				return c >= 'a' && c <= 'z';
			}
			return char.GetLatin1UnicodeCategory(c) == UnicodeCategory.LowercaseLetter;
		}

		// Token: 0x06000ABA RID: 2746 RVA: 0x00021DF3 File Offset: 0x0001FFF3
		internal static bool CheckNumber(UnicodeCategory uc)
		{
			return uc - UnicodeCategory.DecimalDigitNumber <= 2;
		}

		// Token: 0x06000ABB RID: 2747 RVA: 0x00021DFE File Offset: 0x0001FFFE
		[__DynamicallyInvokable]
		public static bool IsNumber(char c)
		{
			if (!char.IsLatin1(c))
			{
				return char.CheckNumber(CharUnicodeInfo.GetUnicodeCategory(c));
			}
			if (char.IsAscii(c))
			{
				return c >= '0' && c <= '9';
			}
			return char.CheckNumber(char.GetLatin1UnicodeCategory(c));
		}

		// Token: 0x06000ABC RID: 2748 RVA: 0x00021E38 File Offset: 0x00020038
		[__DynamicallyInvokable]
		public static bool IsNumber(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			char c = s[index];
			if (!char.IsLatin1(c))
			{
				return char.CheckNumber(CharUnicodeInfo.GetUnicodeCategory(s, index));
			}
			if (char.IsAscii(c))
			{
				return c >= '0' && c <= '9';
			}
			return char.CheckNumber(char.GetLatin1UnicodeCategory(c));
		}

		// Token: 0x06000ABD RID: 2749 RVA: 0x00021EA8 File Offset: 0x000200A8
		[__DynamicallyInvokable]
		public static bool IsPunctuation(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			char ch = s[index];
			if (char.IsLatin1(ch))
			{
				return char.CheckPunctuation(char.GetLatin1UnicodeCategory(ch));
			}
			return char.CheckPunctuation(CharUnicodeInfo.GetUnicodeCategory(s, index));
		}

		// Token: 0x06000ABE RID: 2750 RVA: 0x00021EFF File Offset: 0x000200FF
		internal static bool CheckSeparator(UnicodeCategory uc)
		{
			return uc - UnicodeCategory.SpaceSeparator <= 2;
		}

		// Token: 0x06000ABF RID: 2751 RVA: 0x00021F0B File Offset: 0x0002010B
		private static bool IsSeparatorLatin1(char c)
		{
			return c == ' ' || c == '\u00a0';
		}

		// Token: 0x06000AC0 RID: 2752 RVA: 0x00021F1C File Offset: 0x0002011C
		[__DynamicallyInvokable]
		public static bool IsSeparator(char c)
		{
			if (char.IsLatin1(c))
			{
				return char.IsSeparatorLatin1(c);
			}
			return char.CheckSeparator(CharUnicodeInfo.GetUnicodeCategory(c));
		}

		// Token: 0x06000AC1 RID: 2753 RVA: 0x00021F38 File Offset: 0x00020138
		[__DynamicallyInvokable]
		public static bool IsSeparator(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			char c = s[index];
			if (char.IsLatin1(c))
			{
				return char.IsSeparatorLatin1(c);
			}
			return char.CheckSeparator(CharUnicodeInfo.GetUnicodeCategory(s, index));
		}

		// Token: 0x06000AC2 RID: 2754 RVA: 0x00021F8A File Offset: 0x0002018A
		[__DynamicallyInvokable]
		public static bool IsSurrogate(char c)
		{
			return c >= '\ud800' && c <= '\udfff';
		}

		// Token: 0x06000AC3 RID: 2755 RVA: 0x00021FA1 File Offset: 0x000201A1
		[__DynamicallyInvokable]
		public static bool IsSurrogate(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			return char.IsSurrogate(s[index]);
		}

		// Token: 0x06000AC4 RID: 2756 RVA: 0x00021FD1 File Offset: 0x000201D1
		internal static bool CheckSymbol(UnicodeCategory uc)
		{
			return uc - UnicodeCategory.MathSymbol <= 3;
		}

		// Token: 0x06000AC5 RID: 2757 RVA: 0x00021FDD File Offset: 0x000201DD
		[__DynamicallyInvokable]
		public static bool IsSymbol(char c)
		{
			if (char.IsLatin1(c))
			{
				return char.CheckSymbol(char.GetLatin1UnicodeCategory(c));
			}
			return char.CheckSymbol(CharUnicodeInfo.GetUnicodeCategory(c));
		}

		// Token: 0x06000AC6 RID: 2758 RVA: 0x00022000 File Offset: 0x00020200
		[__DynamicallyInvokable]
		public static bool IsSymbol(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (char.IsLatin1(s[index]))
			{
				return char.CheckSymbol(char.GetLatin1UnicodeCategory(s[index]));
			}
			return char.CheckSymbol(CharUnicodeInfo.GetUnicodeCategory(s, index));
		}

		// Token: 0x06000AC7 RID: 2759 RVA: 0x0002205C File Offset: 0x0002025C
		[__DynamicallyInvokable]
		public static bool IsUpper(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			char c = s[index];
			if (!char.IsLatin1(c))
			{
				return CharUnicodeInfo.GetUnicodeCategory(s, index) == UnicodeCategory.UppercaseLetter;
			}
			if (char.IsAscii(c))
			{
				return c >= 'A' && c <= 'Z';
			}
			return char.GetLatin1UnicodeCategory(c) == UnicodeCategory.UppercaseLetter;
		}

		// Token: 0x06000AC8 RID: 2760 RVA: 0x000220C8 File Offset: 0x000202C8
		[__DynamicallyInvokable]
		public static bool IsWhiteSpace(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (char.IsLatin1(s[index]))
			{
				return char.IsWhiteSpaceLatin1(s[index]);
			}
			return CharUnicodeInfo.IsWhiteSpace(s, index);
		}

		// Token: 0x06000AC9 RID: 2761 RVA: 0x00022119 File Offset: 0x00020319
		public static UnicodeCategory GetUnicodeCategory(char c)
		{
			if (char.IsLatin1(c))
			{
				return char.GetLatin1UnicodeCategory(c);
			}
			return CharUnicodeInfo.InternalGetUnicodeCategory((int)c);
		}

		// Token: 0x06000ACA RID: 2762 RVA: 0x00022130 File Offset: 0x00020330
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
			if (char.IsLatin1(s[index]))
			{
				return char.GetLatin1UnicodeCategory(s[index]);
			}
			return CharUnicodeInfo.InternalGetUnicodeCategory(s, index);
		}

		// Token: 0x06000ACB RID: 2763 RVA: 0x00022181 File Offset: 0x00020381
		[__DynamicallyInvokable]
		public static double GetNumericValue(char c)
		{
			return CharUnicodeInfo.GetNumericValue(c);
		}

		// Token: 0x06000ACC RID: 2764 RVA: 0x00022189 File Offset: 0x00020389
		[__DynamicallyInvokable]
		public static double GetNumericValue(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			return CharUnicodeInfo.GetNumericValue(s, index);
		}

		// Token: 0x06000ACD RID: 2765 RVA: 0x000221B4 File Offset: 0x000203B4
		[__DynamicallyInvokable]
		public static bool IsHighSurrogate(char c)
		{
			return c >= '\ud800' && c <= '\udbff';
		}

		// Token: 0x06000ACE RID: 2766 RVA: 0x000221CB File Offset: 0x000203CB
		[__DynamicallyInvokable]
		public static bool IsHighSurrogate(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index < 0 || index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			return char.IsHighSurrogate(s[index]);
		}

		// Token: 0x06000ACF RID: 2767 RVA: 0x000221FF File Offset: 0x000203FF
		[__DynamicallyInvokable]
		public static bool IsLowSurrogate(char c)
		{
			return c >= '\udc00' && c <= '\udfff';
		}

		// Token: 0x06000AD0 RID: 2768 RVA: 0x00022216 File Offset: 0x00020416
		[__DynamicallyInvokable]
		public static bool IsLowSurrogate(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index < 0 || index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			return char.IsLowSurrogate(s[index]);
		}

		// Token: 0x06000AD1 RID: 2769 RVA: 0x0002224C File Offset: 0x0002044C
		[__DynamicallyInvokable]
		public static bool IsSurrogatePair(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index < 0 || index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			return index + 1 < s.Length && char.IsSurrogatePair(s[index], s[index + 1]);
		}

		// Token: 0x06000AD2 RID: 2770 RVA: 0x000222A1 File Offset: 0x000204A1
		[__DynamicallyInvokable]
		public static bool IsSurrogatePair(char highSurrogate, char lowSurrogate)
		{
			return highSurrogate >= '\ud800' && highSurrogate <= '\udbff' && lowSurrogate >= '\udc00' && lowSurrogate <= '\udfff';
		}

		// Token: 0x06000AD3 RID: 2771 RVA: 0x000222CC File Offset: 0x000204CC
		[__DynamicallyInvokable]
		public static string ConvertFromUtf32(int utf32)
		{
			if (utf32 < 0 || utf32 > 1114111 || (utf32 >= 55296 && utf32 <= 57343))
			{
				throw new ArgumentOutOfRangeException("utf32", Environment.GetResourceString("ArgumentOutOfRange_InvalidUTF32"));
			}
			if (utf32 < 65536)
			{
				return char.ToString((char)utf32);
			}
			utf32 -= 65536;
			return new string(new char[]
			{
				(char)(utf32 / 1024 + 55296),
				(char)(utf32 % 1024 + 56320)
			});
		}

		// Token: 0x06000AD4 RID: 2772 RVA: 0x00022354 File Offset: 0x00020554
		[__DynamicallyInvokable]
		public static int ConvertToUtf32(char highSurrogate, char lowSurrogate)
		{
			if (!char.IsHighSurrogate(highSurrogate))
			{
				throw new ArgumentOutOfRangeException("highSurrogate", Environment.GetResourceString("ArgumentOutOfRange_InvalidHighSurrogate"));
			}
			if (!char.IsLowSurrogate(lowSurrogate))
			{
				throw new ArgumentOutOfRangeException("lowSurrogate", Environment.GetResourceString("ArgumentOutOfRange_InvalidLowSurrogate"));
			}
			return (int)((highSurrogate - '\ud800') * 'Ѐ' + (lowSurrogate - '\udc00')) + 65536;
		}

		// Token: 0x06000AD5 RID: 2773 RVA: 0x000223B8 File Offset: 0x000205B8
		[__DynamicallyInvokable]
		public static int ConvertToUtf32(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index < 0 || index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
			}
			int num = (int)(s[index] - '\ud800');
			if (num < 0 || num > 2047)
			{
				return (int)s[index];
			}
			if (num > 1023)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidLowSurrogate", new object[]
				{
					index
				}), "s");
			}
			if (index >= s.Length - 1)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidHighSurrogate", new object[]
				{
					index
				}), "s");
			}
			int num2 = (int)(s[index + 1] - '\udc00');
			if (num2 >= 0 && num2 <= 1023)
			{
				return num * 1024 + num2 + 65536;
			}
			throw new ArgumentException(Environment.GetResourceString("Argument_InvalidHighSurrogate", new object[]
			{
				index
			}), "s");
		}

		// Token: 0x040003F7 RID: 1015
		internal char m_value;

		// Token: 0x040003F8 RID: 1016
		[__DynamicallyInvokable]
		public const char MaxValue = '￿';

		// Token: 0x040003F9 RID: 1017
		[__DynamicallyInvokable]
		public const char MinValue = '\0';

		// Token: 0x040003FA RID: 1018
		private static readonly byte[] categoryForLatin1 = new byte[]
		{
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			11,
			24,
			24,
			24,
			26,
			24,
			24,
			24,
			20,
			21,
			24,
			25,
			24,
			19,
			24,
			24,
			8,
			8,
			8,
			8,
			8,
			8,
			8,
			8,
			8,
			8,
			24,
			24,
			25,
			25,
			25,
			24,
			24,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			20,
			24,
			21,
			27,
			18,
			27,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			20,
			25,
			21,
			25,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			14,
			11,
			24,
			26,
			26,
			26,
			26,
			28,
			28,
			27,
			28,
			1,
			22,
			25,
			19,
			28,
			27,
			28,
			25,
			10,
			10,
			27,
			1,
			28,
			24,
			27,
			10,
			1,
			23,
			10,
			10,
			10,
			24,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			25,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			25,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1
		};

		// Token: 0x040003FB RID: 1019
		internal const int UNICODE_PLANE00_END = 65535;

		// Token: 0x040003FC RID: 1020
		internal const int UNICODE_PLANE01_START = 65536;

		// Token: 0x040003FD RID: 1021
		internal const int UNICODE_PLANE16_END = 1114111;

		// Token: 0x040003FE RID: 1022
		internal const int HIGH_SURROGATE_START = 55296;

		// Token: 0x040003FF RID: 1023
		internal const int LOW_SURROGATE_END = 57343;
	}
}
