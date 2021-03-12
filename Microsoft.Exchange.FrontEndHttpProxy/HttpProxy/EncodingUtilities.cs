using System;
using System.IO;
using System.Text;
using System.Web.Security.AntiXss;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x0200009A RID: 154
	public static class EncodingUtilities
	{
		// Token: 0x06000486 RID: 1158 RVA: 0x0001A657 File Offset: 0x00018857
		public static string EncodeToBase64(string input)
		{
			return Convert.ToBase64String(Encoding.UTF8.GetBytes(input));
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x0001A669 File Offset: 0x00018869
		public static string DecodeFromBase64(string input)
		{
			return Encoding.UTF8.GetString(Convert.FromBase64String(input));
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x0001A67B File Offset: 0x0001887B
		public static string HtmlEncode(string textToEncode)
		{
			return AntiXssEncoder.HtmlEncode(textToEncode, false);
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x0001A684 File Offset: 0x00018884
		public static void HtmlEncode(string s, TextWriter writer, bool encodeSpaces)
		{
			if (s == null || s.Length == 0)
			{
				return;
			}
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			if (encodeSpaces)
			{
				for (int i = 0; i < s.Length; i++)
				{
					if (s[i] == ' ')
					{
						writer.Write("&nbsp;");
					}
					else
					{
						writer.Write(AntiXssEncoder.HtmlEncode(s.Substring(i, 1), false));
					}
				}
				return;
			}
			writer.Write(AntiXssEncoder.HtmlEncode(s, false));
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x0001A6F9 File Offset: 0x000188F9
		public static void HtmlEncode(string s, TextWriter writer)
		{
			EncodingUtilities.HtmlEncode(s, writer, false);
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x0001A703 File Offset: 0x00018903
		public static string JavascriptEncode(string s)
		{
			return EncodingUtilities.JavascriptEncode(s, false);
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x0001A70C File Offset: 0x0001890C
		public static string JavascriptEncode(string s, bool escapeNonAscii)
		{
			if (s == null)
			{
				return string.Empty;
			}
			StringBuilder sb = new StringBuilder();
			string result;
			using (StringWriter stringWriter = new StringWriter(sb))
			{
				EncodingUtilities.JavascriptEncode(s, stringWriter, escapeNonAscii);
				result = stringWriter.ToString();
			}
			return result;
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x0001A75C File Offset: 0x0001895C
		public static void JavascriptEncode(string s, TextWriter writer, bool escapeNonAscii)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			int i = 0;
			while (i < s.Length)
			{
				char c = s[i];
				if (c <= '"')
				{
					if (c != '\n')
					{
						if (c != '\r')
						{
							switch (c)
							{
							case '!':
							case '"':
								goto IL_78;
							default:
								goto IL_B3;
							}
						}
						else
						{
							writer.Write('\\');
							writer.Write('r');
						}
					}
					else
					{
						writer.Write('\\');
						writer.Write('n');
					}
				}
				else if (c <= '/')
				{
					if (c != '\'' && c != '/')
					{
						goto IL_B3;
					}
					goto IL_78;
				}
				else
				{
					switch (c)
					{
					case '<':
					case '>':
						goto IL_78;
					case '=':
						goto IL_B3;
					default:
						if (c == '\\')
						{
							goto IL_78;
						}
						goto IL_B3;
					}
				}
				IL_E7:
				i++;
				continue;
				IL_78:
				writer.Write('\\');
				writer.Write(s[i]);
				goto IL_E7;
				IL_B3:
				if (escapeNonAscii && s[i] > '\u007f')
				{
					writer.Write("\\u{0:x4}", (ushort)s[i]);
					goto IL_E7;
				}
				writer.Write(s[i]);
				goto IL_E7;
			}
		}
	}
}
