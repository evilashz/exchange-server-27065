using System;
using System.Text;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x020000FF RID: 255
	internal class DateFormatParser
	{
		// Token: 0x0600087A RID: 2170 RVA: 0x0003EAE4 File Offset: 0x0003CCE4
		internal static string GetPart(string dateFormat, DateFormatPart part)
		{
			if (dateFormat == null)
			{
				throw new ArgumentNullException("dateFormat");
			}
			DateFormatParser.State state = DateFormatParser.State.Scanning;
			StringBuilder stringBuilder = new StringBuilder(dateFormat.Length);
			int i = 0;
			while (i < dateFormat.Length)
			{
				char c = dateFormat[i];
				char c2 = c;
				if (c2 <= 'D')
				{
					if (c2 != '\'')
					{
						if (c2 != 'D')
						{
							goto IL_11A;
						}
						goto IL_A9;
					}
					else
					{
						switch (state)
						{
						case DateFormatParser.State.Scanning:
							state = DateFormatParser.State.InQuotedWord;
							break;
						case DateFormatParser.State.InQuotedWord:
							state = DateFormatParser.State.Scanning;
							break;
						case DateFormatParser.State.InWord:
						{
							state = DateFormatParser.State.Scanning;
							string text = DateFormatParser.EvaluateWord(stringBuilder.ToString(), part);
							if (text != null)
							{
								return text;
							}
							stringBuilder.Clear();
							break;
						}
						}
					}
				}
				else
				{
					if (c2 != 'H' && c2 != 'd' && c2 != 'h')
					{
						goto IL_11A;
					}
					goto IL_A9;
				}
				IL_14D:
				i++;
				continue;
				IL_A9:
				switch (state)
				{
				case DateFormatParser.State.Scanning:
				case DateFormatParser.State.InWord:
				{
					if (stringBuilder.Length == 0 || (stringBuilder.Length > 0 && c == stringBuilder[stringBuilder.Length - 1]))
					{
						state = DateFormatParser.State.InWord;
						stringBuilder.Append(c);
						goto IL_14D;
					}
					state = DateFormatParser.State.Scanning;
					string text = DateFormatParser.EvaluateWord(stringBuilder.ToString(), part);
					if (text != null)
					{
						return text;
					}
					stringBuilder.Clear();
					stringBuilder.Append(c);
					goto IL_14D;
				}
				case DateFormatParser.State.InQuotedWord:
					goto IL_14D;
				default:
					goto IL_14D;
				}
				IL_11A:
				switch (state)
				{
				case DateFormatParser.State.Scanning:
				case DateFormatParser.State.InQuotedWord:
					goto IL_14D;
				case DateFormatParser.State.InWord:
				{
					state = DateFormatParser.State.Scanning;
					string text = DateFormatParser.EvaluateWord(stringBuilder.ToString(), part);
					if (text != null)
					{
						return text;
					}
					stringBuilder.Clear();
					goto IL_14D;
				}
				default:
					goto IL_14D;
				}
			}
			if (state == DateFormatParser.State.InWord)
			{
				string text = DateFormatParser.EvaluateWord(stringBuilder.ToString(), part);
				if (text != null)
				{
					return text;
				}
			}
			return null;
		}

		// Token: 0x0600087B RID: 2171 RVA: 0x0003EC68 File Offset: 0x0003CE68
		private static string EvaluateWord(string word, DateFormatPart part)
		{
			string text = null;
			if (part == DateFormatPart.HoursFormat && (word == "h" || word == "H" || word == "hh" || word == "HH"))
			{
				text = word;
			}
			else if (part == DateFormatPart.DaysFormat && (word == "d" || word == "D" || word == "dd" || word == "DD"))
			{
				text = word;
			}
			if (text == null)
			{
				return null;
			}
			if (text.Length == 1)
			{
				return "%" + text;
			}
			return text;
		}

		// Token: 0x02000100 RID: 256
		private enum State
		{
			// Token: 0x0400060C RID: 1548
			Scanning,
			// Token: 0x0400060D RID: 1549
			InQuotedWord,
			// Token: 0x0400060E RID: 1550
			InWord,
			// Token: 0x0400060F RID: 1551
			End
		}
	}
}
