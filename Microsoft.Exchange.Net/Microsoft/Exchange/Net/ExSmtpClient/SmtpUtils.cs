using System;
using System.Globalization;
using Microsoft.Exchange.Data.Internal;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Net.ExSmtpClient
{
	// Token: 0x02000708 RID: 1800
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal static class SmtpUtils
	{
		// Token: 0x060021C7 RID: 8647 RVA: 0x00043CD4 File Offset: 0x00041ED4
		internal static string ProperlyTerminatedCommand(string input)
		{
			string result = string.Empty;
			if (input == null || input == string.Empty || input.EndsWith(SmtpConstants.CrLf, StringComparison.Ordinal))
			{
				result = input;
			}
			else if (input == null)
			{
				result = SmtpConstants.CrLf;
			}
			else if (input.EndsWith(SmtpConstants.Cr, StringComparison.Ordinal))
			{
				result = input + SmtpConstants.Lf;
			}
			else
			{
				result = input + SmtpConstants.CrLf;
			}
			return result;
		}

		// Token: 0x060021C8 RID: 8648 RVA: 0x00043D3D File Offset: 0x00041F3D
		internal static int StatusFromResponseString(string response)
		{
			return SmtpUtils.StatusFromString(response);
		}

		// Token: 0x060021C9 RID: 8649 RVA: 0x00043D48 File Offset: 0x00041F48
		internal static string ToXtextString(string input, bool allowUTF8 = false)
		{
			ArgumentValidator.ThrowIfNull("input", input);
			bool flag = !MimeString.IsPureASCII(input);
			int num = flag ? SmtpUtils.MaxEmbeddedUnicodeCharLength : SmtpUtils.MaxXtextEncodedCharLength;
			char[] array = new char[input.Length * num];
			int length = 0;
			for (int i = 0; i < input.Length; i++)
			{
				char c = input[i];
				if ((!allowUTF8 && c > '~') || c < '!' || c == '=' || c == '+')
				{
					int num2 = char.ConvertToUtf32(input, i);
					i = (char.IsSurrogatePair(input, i) ? (i + 1) : i);
					string text;
					if (flag)
					{
						text = string.Format("{0}{1}{2}", "\\x{", string.Format("{0:X}", num2), "}");
						if (text.Length > SmtpUtils.MaxEmbeddedUnicodeCharLength)
						{
							return string.Empty;
						}
					}
					else
					{
						text = string.Format("{0}{1}", "+", string.Format("{0:X}", num2));
						if (text.Length > SmtpUtils.MaxXtextEncodedCharLength)
						{
							return string.Empty;
						}
					}
					foreach (char c2 in text)
					{
						array[length++] = c2;
					}
				}
				else
				{
					array[length++] = c;
				}
			}
			return new string(array, 0, length);
		}

		// Token: 0x060021CA RID: 8650 RVA: 0x00043EA4 File Offset: 0x000420A4
		internal static string FromXtextString(byte[] input, int beginOffset, int length, bool allowUTF8 = false)
		{
			if (beginOffset + length > input.Length)
			{
				throw new ArgumentException("length");
			}
			char[] array = new char[length];
			bool flag = -1 != ByteString.IndexOf(input, (byte)"+"[0], beginOffset, length);
			int length2 = 0;
			for (int i = beginOffset; i < beginOffset + length; i++)
			{
				byte b = input[i];
				if (b < 33 || b == 61)
				{
					return null;
				}
				if (b > 126)
				{
					if (!allowUTF8 || flag)
					{
						return null;
					}
					int num;
					bool flag2;
					char c = ByteString.BytesToChar(input, i, out num, out flag2, true);
					if (flag2)
					{
						return null;
					}
					if (i + num > beginOffset + length)
					{
						return null;
					}
					array[length2++] = c;
					i += num - 1;
				}
				else if ((char)b == "+"[0])
				{
					if (i + 2 >= beginOffset + length)
					{
						return null;
					}
					string s = ByteString.BytesToString(input, i + 1, 2, false);
					int num2;
					if (!int.TryParse(s, NumberStyles.HexNumber, null, out num2))
					{
						return null;
					}
					array[length2++] = (char)num2;
					i += 2;
				}
				else
				{
					if ((char)b == "\\x{"[0] && !flag)
					{
						int length3 = length - (i - beginOffset);
						string text;
						int num3;
						if (SmtpUtils.GetEmbeddedUnicodeChar(input, i, length3, out text, out num3) && (allowUTF8 || MimeString.IsPureASCII(text)))
						{
							foreach (char c2 in text)
							{
								array[length2++] = c2;
							}
							i += num3 - 1;
							goto IL_150;
						}
					}
					array[length2++] = (char)b;
				}
				IL_150:;
			}
			return new string(array, 0, length2);
		}

		// Token: 0x060021CB RID: 8651 RVA: 0x00044018 File Offset: 0x00042218
		private static bool GetEmbeddedUnicodeChar(byte[] input, int startIndex, int length, out string embeddedUnicodeChar, out int bytesUsed)
		{
			embeddedUnicodeChar = string.Empty;
			bytesUsed = 0;
			if (input == null || input.Length < 1 || length > input.Length || SmtpUtils.MinEmbeddedUnicodeCharLength > length)
			{
				return false;
			}
			int count = (SmtpUtils.MaxEmbeddedUnicodeCharLength <= length) ? SmtpUtils.MaxEmbeddedUnicodeCharLength : length;
			embeddedUnicodeChar = ByteString.BytesToString(input, startIndex, count, false);
			if (embeddedUnicodeChar.Substring(0, "\\x{".Length) != "\\x{")
			{
				return false;
			}
			int num = embeddedUnicodeChar.IndexOf("}", StringComparison.Ordinal);
			if (num < SmtpUtils.MinEmbeddedUnicodeCharLength - "}".Length)
			{
				return false;
			}
			string text = embeddedUnicodeChar.Substring("\\x{".Length, num - "\\x{".Length);
			if (text == string.Empty || text.Length < 2 || text.Length > 6)
			{
				return false;
			}
			int utf;
			if (!int.TryParse(text, NumberStyles.HexNumber, null, out utf))
			{
				return false;
			}
			bytesUsed = "\\x{".Length + text.Length + "}".Length;
			embeddedUnicodeChar = char.ConvertFromUtf32(utf);
			return true;
		}

		// Token: 0x060021CC RID: 8652 RVA: 0x00044124 File Offset: 0x00042324
		private static int StatusFromString(string response)
		{
			string text = response;
			char[] trimChars = new char[1];
			response = text.TrimStart(trimChars);
			if (response.Length >= 3 && char.IsDigit(response[0]) && char.IsDigit(response[1]) && char.IsDigit(response[2]))
			{
				return int.Parse(response.Substring(0, 3), CultureInfo.InvariantCulture);
			}
			throw new UnexpectedSmtpServerResponseException(-1, -1, response);
		}

		// Token: 0x0400208E RID: 8334
		private const string EmbeddedUnicodeCharPrefix = "\\x{";

		// Token: 0x0400208F RID: 8335
		private const string EmbeddedUnicodeCharSuffix = "}";

		// Token: 0x04002090 RID: 8336
		private const int MaxHexpointLength = 6;

		// Token: 0x04002091 RID: 8337
		private const int MinHexpointLength = 2;

		// Token: 0x04002092 RID: 8338
		private const string XtextPrefix = "+";

		// Token: 0x04002093 RID: 8339
		private const int HexCharLength = 2;

		// Token: 0x04002094 RID: 8340
		private static readonly int MaxEmbeddedUnicodeCharLength = "\\x{".Length + 6 + "}".Length;

		// Token: 0x04002095 RID: 8341
		private static readonly int MinEmbeddedUnicodeCharLength = "\\x{".Length + 2 + "}".Length;

		// Token: 0x04002096 RID: 8342
		private static readonly int MaxXtextEncodedCharLength = "+".Length + 2;
	}
}
