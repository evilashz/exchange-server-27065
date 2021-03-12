using System;
using System.Text;
using Microsoft.Exchange.Data.Globalization;

namespace Microsoft.Exchange.RpcClientAccess
{
	// Token: 0x02000091 RID: 145
	internal static class String8Encodings
	{
		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060003A2 RID: 930 RVA: 0x0000D4B8 File Offset: 0x0000B6B8
		public static Encoding ReducedUnicode
		{
			get
			{
				return String8Encodings.reducedUnicode;
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060003A3 RID: 931 RVA: 0x0000D4BF File Offset: 0x0000B6BF
		public static Encoding TemporaryDefault
		{
			get
			{
				return CTSGlobals.AsciiEncoding;
			}
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x0000D4C6 File Offset: 0x0000B6C6
		public static bool IsValidString8Encoding(Encoding encoding)
		{
			return encoding.GetByteCount(String8Encodings.emptyString) == 1;
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x0000D4D8 File Offset: 0x0000B6D8
		public static void ThrowIfInvalidString8Encoding(Encoding encoding)
		{
			if (!String8Encodings.IsValidString8Encoding(encoding))
			{
				string message = string.Format("{0} is not supported as a String8 codepage", encoding.WebName);
				throw new ArgumentException(message);
			}
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x0000D508 File Offset: 0x0000B708
		public static bool TryGetEncoding(int codePage, out Encoding encoding)
		{
			encoding = null;
			bool result;
			try
			{
				encoding = CodePageMap.GetEncoding(codePage);
				result = true;
			}
			catch (ArgumentException)
			{
				result = false;
			}
			catch (NotSupportedException)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x0000D54C File Offset: 0x0000B74C
		public static bool TryGetEncoding(int codePage, Encoding fallbackEncoding, out Encoding encoding)
		{
			encoding = null;
			if (fallbackEncoding == null)
			{
				throw new ArgumentNullException("fallbackEncoding");
			}
			if (codePage == 4095)
			{
				encoding = fallbackEncoding;
				return true;
			}
			return String8Encodings.TryGetEncoding(codePage, out encoding);
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x0000D574 File Offset: 0x0000B774
		// Note: this type is marked as 'beforefieldinit'.
		static String8Encodings()
		{
			char[] array = new char[1];
			String8Encodings.emptyString = array;
			String8Encodings.reducedUnicode = new ReducedUnicodeEncoding();
		}

		// Token: 0x0400020A RID: 522
		public const int CodePageUnspecified = 4095;

		// Token: 0x0400020B RID: 523
		private static readonly char[] emptyString;

		// Token: 0x0400020C RID: 524
		private static ReducedUnicodeEncoding reducedUnicode;
	}
}
