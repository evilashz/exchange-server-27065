using System;
using Microsoft.Exchange.Transport;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x0200041A RID: 1050
	[Serializable]
	internal struct SmtpToken
	{
		// Token: 0x06003055 RID: 12373 RVA: 0x000C09DC File Offset: 0x000BEBDC
		static SmtpToken()
		{
			for (int i = 0; i < "[<(".Length; i++)
			{
				SmtpToken.charMapper[(int)"[<("[i]].ClosingDelimiter = "]>)"[i];
				SmtpToken.charMapper[(int)"[<("[i]].OpenDelimiter = true;
				SmtpToken.charMapper[(int)"]>)"[i]].CloseDelimiter = true;
			}
		}

		// Token: 0x06003056 RID: 12374 RVA: 0x000C0A68 File Offset: 0x000BEC68
		public static string SplitString(string input, char searchChar, out string tail)
		{
			int num = 0;
			bool flag = false;
			char[] array = new char[64];
			tail = null;
			int num2 = input.Length - 1;
			for (int i = 0; i <= num2; i++)
			{
				char c = input[i];
				if (c < '\u0080')
				{
					if (!flag && num == 0 && c == searchChar)
					{
						tail = input.Substring(i + 1);
						return input.Substring(0, i);
					}
					if (c == '\\')
					{
						i++;
						if (i > num2)
						{
							throw new FormatException(Strings.TrailingEscape);
						}
					}
					else
					{
						bool flag2 = true;
						if (flag)
						{
							flag2 = false;
							if (c == '"')
							{
								flag = false;
							}
						}
						else if (c == '"')
						{
							flag = true;
							flag2 = false;
						}
						else if (SmtpToken.charMapper[(int)c].OpenDelimiter)
						{
							flag2 = false;
							if (num == 64)
							{
								throw new FormatException(Strings.QuoteNestLevel);
							}
							array[num++] = SmtpToken.charMapper[(int)c].ClosingDelimiter;
						}
						if (flag2 && SmtpToken.charMapper[(int)c].CloseDelimiter)
						{
							if (num <= 0)
							{
								return input;
							}
							if (array[num - 1] != c)
							{
								throw new FormatException(Strings.IncorrectBrace);
							}
							num--;
							if (num == 0 && c == searchChar)
							{
								tail = input.Substring(i + 1);
								return input.Substring(0, i);
							}
						}
					}
				}
			}
			return input;
		}

		// Token: 0x06003057 RID: 12375 RVA: 0x000C0BBA File Offset: 0x000BEDBA
		internal static bool IsA(char ch)
		{
			return ch >= 'A' && ch <= 'z' && (ch <= 'Z' || ch >= 'a');
		}

		// Token: 0x06003058 RID: 12376 RVA: 0x000C0BD7 File Offset: 0x000BEDD7
		internal static bool IsC(char ch)
		{
			return ch <= '\u007f' && ch != ' ' && !SmtpToken.IsSpecialOrControl(ch);
		}

		// Token: 0x06003059 RID: 12377 RVA: 0x000C0BEE File Offset: 0x000BEDEE
		internal static bool IsD(char ch)
		{
			return ch >= '0' && ch <= '9';
		}

		// Token: 0x0600305A RID: 12378 RVA: 0x000C0BFD File Offset: 0x000BEDFD
		internal static bool IsQ(char ch)
		{
			return ch <= '\u007f' && ch != '"' && ch != '\\' && !SmtpToken.IsCrOrLf(ch);
		}

		// Token: 0x0600305B RID: 12379 RVA: 0x000C0C19 File Offset: 0x000BEE19
		private static bool IsCrOrLf(char ch)
		{
			return ch == '\r' || ch == '\n';
		}

		// Token: 0x0600305C RID: 12380 RVA: 0x000C0C27 File Offset: 0x000BEE27
		private static bool IsControl(char ch)
		{
			return (ch >= '\0' && ch <= '\u001f') || ch == '\u007f';
		}

		// Token: 0x0600305D RID: 12381 RVA: 0x000C0C3C File Offset: 0x000BEE3C
		private static bool IsSpecialOrControl(char ch)
		{
			if (ch <= '.')
			{
				if (ch != '"')
				{
					switch (ch)
					{
					case '(':
					case ')':
					case ',':
					case '.':
						break;
					case '*':
					case '+':
					case '-':
						goto IL_71;
					default:
						goto IL_71;
					}
				}
			}
			else
			{
				switch (ch)
				{
				case ':':
				case ';':
				case '<':
				case '>':
				case '@':
					break;
				case '=':
				case '?':
					goto IL_71;
				default:
					switch (ch)
					{
					case '[':
					case '\\':
					case ']':
						break;
					default:
						goto IL_71;
					}
					break;
				}
			}
			return true;
			IL_71:
			return SmtpToken.IsControl(ch);
		}

		// Token: 0x0400179E RID: 6046
		private static SmtpToken.CharMapper[] charMapper = new SmtpToken.CharMapper[128];

		// Token: 0x0200041B RID: 1051
		internal struct CharMapper
		{
			// Token: 0x0400179F RID: 6047
			public char ClosingDelimiter;

			// Token: 0x040017A0 RID: 6048
			public bool OpenDelimiter;

			// Token: 0x040017A1 RID: 6049
			public bool CloseDelimiter;
		}
	}
}
