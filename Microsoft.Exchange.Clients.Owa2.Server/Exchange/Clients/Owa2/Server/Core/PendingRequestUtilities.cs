using System;
using System.IO;
using System.Text;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200019E RID: 414
	public class PendingRequestUtilities
	{
		// Token: 0x06000EF8 RID: 3832 RVA: 0x0003A39C File Offset: 0x0003859C
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
				if (s == null)
				{
					throw new ArgumentNullException("s");
				}
				if (stringWriter == null)
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
									goto IL_98;
								default:
									goto IL_D3;
								}
							}
							else
							{
								stringWriter.Write('\\');
								stringWriter.Write('r');
							}
						}
						else
						{
							stringWriter.Write('\\');
							stringWriter.Write('n');
						}
					}
					else if (c <= '/')
					{
						if (c != '\'' && c != '/')
						{
							goto IL_D3;
						}
						goto IL_98;
					}
					else
					{
						switch (c)
						{
						case '<':
						case '>':
							goto IL_98;
						case '=':
							goto IL_D3;
						default:
							if (c == '\\')
							{
								goto IL_98;
							}
							goto IL_D3;
						}
					}
					IL_107:
					i++;
					continue;
					IL_98:
					stringWriter.Write('\\');
					stringWriter.Write(s[i]);
					goto IL_107;
					IL_D3:
					if (escapeNonAscii && s[i] > '\u007f')
					{
						stringWriter.Write("\\u{0:x4}", (int)s[i]);
						goto IL_107;
					}
					stringWriter.Write(s[i]);
					goto IL_107;
				}
				result = stringWriter.ToString();
			}
			return result;
		}

		// Token: 0x06000EF9 RID: 3833 RVA: 0x0003A4F0 File Offset: 0x000386F0
		public static string JavascriptEncode(string s)
		{
			return PendingRequestUtilities.JavascriptEncode(s, true);
		}

		// Token: 0x06000EFA RID: 3834 RVA: 0x0003A4FC File Offset: 0x000386FC
		public static string JavascriptEscapeNonAscii(string s)
		{
			if (s == null)
			{
				return string.Empty;
			}
			StringBuilder sb = new StringBuilder();
			string result;
			using (StringWriter stringWriter = new StringWriter(sb))
			{
				if (s == null)
				{
					throw new ArgumentNullException("s");
				}
				if (stringWriter == null)
				{
					throw new ArgumentNullException("writer");
				}
				for (int i = 0; i < s.Length; i++)
				{
					if (s[i] > '\u007f')
					{
						stringWriter.Write("\\u{0:x4}", (int)s[i]);
					}
					else
					{
						stringWriter.Write(s[i]);
					}
				}
				result = stringWriter.ToString();
			}
			return result;
		}

		// Token: 0x06000EFB RID: 3835 RVA: 0x0003A5A4 File Offset: 0x000387A4
		internal static StreamWriter CreateStreamWriter(Stream stream)
		{
			UTF8Encoding encoding = new UTF8Encoding(false, false);
			return new StreamWriter(stream, encoding);
		}
	}
}
