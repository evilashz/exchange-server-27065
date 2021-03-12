using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Exchange.Data.TextConverters;
using Microsoft.Exchange.Data.TextConverters.Internal.Format;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x02000F6C RID: 3948
	internal class QuotedText
	{
		// Token: 0x06006401 RID: 25601 RVA: 0x00137E70 File Offset: 0x00136070
		internal static QuotedTextResult ParseHtmlQuotedText(string html, bool reorderMsgs)
		{
			HtmlConversationBodyScanner htmlConversationBodyScanner = new HtmlConversationBodyScanner();
			htmlConversationBodyScanner.DetectEncodingFromMetaTag = false;
			StringReader stringReader = new StringReader(html);
			htmlConversationBodyScanner.Load(stringReader);
			IList<ConversationBodyScanner.Scanner.FragmentInfo> fragments = htmlConversationBodyScanner.Fragments;
			List<QuotedText.QuotedPartLineInfo> list = new List<QuotedText.QuotedPartLineInfo>();
			short num = -1;
			short num2 = -1;
			bool flag = false;
			ConversationBodyScanner.Scanner.FragmentCategory headerType = ConversationBodyScanner.Scanner.FragmentCategory.Blank;
			foreach (ConversationBodyScanner.Scanner.FragmentInfo fragmentInfo in fragments)
			{
				if (flag)
				{
					num2 = fragmentInfo.FirstLine - 1;
					flag = false;
				}
				if (fragmentInfo.Category == ConversationBodyScanner.Scanner.FragmentCategory.MsHeader || fragmentInfo.Category == ConversationBodyScanner.Scanner.FragmentCategory.NonMsHeader)
				{
					flag = true;
					if (num == -1)
					{
						num = fragmentInfo.FirstLine;
					}
					else
					{
						list.Add(new QuotedText.QuotedPartLineInfo(num, fragmentInfo.FirstLine - 1, num2, headerType));
						num = fragmentInfo.FirstLine;
						num2 = -1;
					}
					headerType = fragmentInfo.Category;
				}
			}
			if (num != -1)
			{
				short num3 = (short)(htmlConversationBodyScanner.Lines.Count - 1);
				if (num2 == -1)
				{
					num2 = num3;
				}
				list.Add(new QuotedText.QuotedPartLineInfo(num, num3, num2, headerType));
			}
			QuotedTextResult quotedTextResult = null;
			if (list.Count > 0)
			{
				quotedTextResult = new QuotedTextResult();
				if (list[0].Start - 1 >= 0)
				{
					using (StringWriter stringWriter = new StringWriter())
					{
						using (HtmlWriter htmlWriter = new HtmlWriter(stringWriter))
						{
							htmlConversationBodyScanner.WriteLines(htmlWriter, 0, (int)(list[0].Start - 1));
							quotedTextResult.NewMsg = stringWriter.ToString();
						}
						goto IL_17D;
					}
				}
				quotedTextResult.NewMsg = "<html></html>";
				IL_17D:
				StringBuilder stringBuilder = new StringBuilder();
				int num4 = reorderMsgs ? (list.Count - 1) : 0;
				bool flag2 = true;
				while (flag2)
				{
					while (QuotedText.IsPotentialDelimiter(htmlConversationBodyScanner.Lines[(int)list[num4].End]))
					{
						QuotedText.QuotedPartLineInfo quotedPartLineInfo = list[num4];
						quotedPartLineInfo.End -= 1;
					}
					while (QuotedText.IsPotentialDelimiter(htmlConversationBodyScanner.Lines[(int)list[num4].Start]))
					{
						QuotedText.QuotedPartLineInfo quotedPartLineInfo2 = list[num4];
						quotedPartLineInfo2.Start += 1;
					}
					if (!list[num4].IsValid())
					{
						return null;
					}
					using (StringWriter stringWriter2 = new StringWriter())
					{
						using (HtmlWriter htmlWriter2 = new HtmlWriter(stringWriter2))
						{
							htmlConversationBodyScanner.WriteLines(htmlWriter2, (int)list[num4].Start, (int)list[num4].End);
							stringBuilder.Append(stringWriter2.ToString());
							stringBuilder.Append("<br>");
						}
					}
					QuotedPart item = QuotedText.BuildQuotedPart(htmlConversationBodyScanner, list[num4]);
					quotedTextResult.QuotedParts.Add(item);
					if (reorderMsgs)
					{
						num4--;
						flag2 = (num4 >= 0);
					}
					else
					{
						num4++;
						flag2 = (num4 < list.Count);
					}
				}
				quotedTextResult.QuotedText = stringBuilder.ToString();
			}
			stringReader.Dispose();
			return quotedTextResult;
		}

		// Token: 0x06006402 RID: 25602 RVA: 0x001381A4 File Offset: 0x001363A4
		private static bool IsPotentialDelimiter(ConversationBodyScanner.Scanner.LineInfo line)
		{
			return line.Category == ConversationBodyScanner.Scanner.LineCategory.HorizontalLineDelimiter || line.Category == ConversationBodyScanner.Scanner.LineCategory.PotentialDelimiterLine || line.Category == ConversationBodyScanner.Scanner.LineCategory.Invalid || line.Category == ConversationBodyScanner.Scanner.LineCategory.Skipped || line.Category == ConversationBodyScanner.Scanner.LineCategory.Blank;
		}

		// Token: 0x06006403 RID: 25603 RVA: 0x001381D4 File Offset: 0x001363D4
		private static QuotedPart BuildQuotedPart(ConversationBodyScanner scanner, QuotedText.QuotedPartLineInfo partInfo)
		{
			QuotedPart quotedPart = new QuotedPart();
			using (StringWriter stringWriter = new StringWriter())
			{
				using (HtmlWriter htmlWriter = new HtmlWriter(stringWriter))
				{
					if (partInfo.HeaderEnd < partInfo.End)
					{
						scanner.WriteLines(htmlWriter, (int)partInfo.Start, (int)partInfo.HeaderEnd);
						quotedPart.Header = stringWriter.ToString();
						stringWriter.GetStringBuilder().Clear();
						scanner.WriteLines(htmlWriter, (int)(partInfo.HeaderEnd + 1), (int)partInfo.End);
						quotedPart.Body = stringWriter.ToString();
					}
					else
					{
						scanner.WriteLines(htmlWriter, (int)partInfo.Start, (int)partInfo.HeaderEnd);
						quotedPart.Header = stringWriter.ToString();
					}
				}
			}
			string sender = "";
			string sentTime = "";
			if (partInfo.HeaderType == ConversationBodyScanner.Scanner.FragmentCategory.MsHeader)
			{
				Tuple<string, string> tuple = QuotedText.ParseMsHeader(scanner, partInfo);
				sender = tuple.Item1;
				sentTime = tuple.Item2;
			}
			quotedPart.Sender = sender;
			quotedPart.SentTime = sentTime;
			return quotedPart;
		}

		// Token: 0x06006404 RID: 25604 RVA: 0x001382E4 File Offset: 0x001364E4
		private static Tuple<string, string> ParseMsHeader(ConversationBodyScanner scanner, QuotedText.QuotedPartLineInfo partInfo)
		{
			string item = "";
			string item2 = "";
			short start = partInfo.Start;
			uint textPositionAfterTextQuoting = scanner.Lines[(int)start].TextPositionAfterTextQuoting;
			TextRun textRun = scanner.FormatStore.GetTextRun(textPositionAfterTextQuoting);
			QuotedText.SkipLeadingSpace(ref textRun);
			ConversationBodyScanner.Scanner.MsHeading[] msHeadings = ConversationBodyScanner.Scanner.MsHeadings;
			short num = start;
			bool flag = false;
			int num2 = 0;
			while (num2 < msHeadings.Length && !flag)
			{
				if (scanner.IsHeading(textRun, msHeadings[num2].FromFields))
				{
					short num3 = (short)(scanner.Lines[(int)num].BlockQuotingLevel + scanner.Lines[(int)num].TextQuotingLevel);
					short num4 = num + 1;
					while ((int)num4 < Math.Min((int)(num + 10), (int)((short)scanner.Lines.Count)) && !flag && (short)(scanner.Lines[(int)num4].BlockQuotingLevel + scanner.Lines[(int)num4].TextQuotingLevel) == num3)
					{
						if (scanner.Lines[(int)num4].Category == ConversationBodyScanner.Scanner.LineCategory.PotentialMsHeader || scanner.Lines[(int)num4].Category == ConversationBodyScanner.Scanner.LineCategory.PotentialNonMsHeader)
						{
							TextRun textRun2 = scanner.FormatStore.GetTextRun(scanner.Lines[(int)num4].TextPositionAfterTextQuoting);
							QuotedText.SkipLeadingSpace(ref textRun2);
							for (int i = 0; i < msHeadings[num2].AdditionalHeadings.Length; i++)
							{
								if (scanner.IsHeading(textRun2, msHeadings[num2].AdditionalHeadings[i].SentFields))
								{
									flag = true;
									textRun.MoveNext();
									QuotedText.SkipLeadingSpace(ref textRun);
									item = QuotedText.GetStringRun(ref textRun);
									textRun2.MoveNext();
									QuotedText.SkipLeadingSpace(ref textRun2);
									item2 = QuotedText.GetStringRun(ref textRun2);
									break;
								}
							}
						}
						num4 += 1;
					}
				}
				num2++;
			}
			return new Tuple<string, string>(item, item2);
		}

		// Token: 0x06006405 RID: 25605 RVA: 0x001384CB File Offset: 0x001366CB
		private static void SkipLeadingSpace(ref TextRun run)
		{
			while (run.Type == TextRunType.Space || run.Type == TextRunType.NbSp || run.Type == TextRunType.Tabulation || run.Type == TextRunType.Invalid)
			{
				run.MoveNext();
			}
		}

		// Token: 0x06006406 RID: 25606 RVA: 0x00138504 File Offset: 0x00136704
		private static string GetStringRun(ref TextRun run)
		{
			string text = "";
			uint num = 0U;
			while (!run.IsEnd() && run.Type != TextRunType.NewLine && run.Type != TextRunType.BlockBoundary)
			{
				if (run.Type == TextRunType.Space)
				{
					text += " ";
				}
				else if (run.Type == TextRunType.Tabulation)
				{
					text += "\t";
				}
				else if (run.Position > num)
				{
					text += run.ToString();
				}
				uint num2 = run.Position + Convert.ToUInt32(run.WordLength);
				if (num2 > num)
				{
					num = num2;
				}
				run.MoveNext();
				run.SkipInvalid();
			}
			return text;
		}

		// Token: 0x02000F6D RID: 3949
		private class QuotedPartLineInfo
		{
			// Token: 0x06006408 RID: 25608 RVA: 0x001385C0 File Offset: 0x001367C0
			public QuotedPartLineInfo(short start, short end, short headerEnd, ConversationBodyScanner.Scanner.FragmentCategory headerType)
			{
				this.Start = start;
				this.End = end;
				this.HeaderEnd = headerEnd;
				this.HeaderType = headerType;
			}

			// Token: 0x170016A9 RID: 5801
			// (get) Token: 0x06006409 RID: 25609 RVA: 0x001385E5 File Offset: 0x001367E5
			// (set) Token: 0x0600640A RID: 25610 RVA: 0x001385ED File Offset: 0x001367ED
			public short Start { get; set; }

			// Token: 0x170016AA RID: 5802
			// (get) Token: 0x0600640B RID: 25611 RVA: 0x001385F6 File Offset: 0x001367F6
			// (set) Token: 0x0600640C RID: 25612 RVA: 0x001385FE File Offset: 0x001367FE
			public short End { get; set; }

			// Token: 0x170016AB RID: 5803
			// (get) Token: 0x0600640D RID: 25613 RVA: 0x00138607 File Offset: 0x00136807
			// (set) Token: 0x0600640E RID: 25614 RVA: 0x0013860F File Offset: 0x0013680F
			public short HeaderEnd { get; set; }

			// Token: 0x170016AC RID: 5804
			// (get) Token: 0x0600640F RID: 25615 RVA: 0x00138618 File Offset: 0x00136818
			// (set) Token: 0x06006410 RID: 25616 RVA: 0x00138620 File Offset: 0x00136820
			public ConversationBodyScanner.Scanner.FragmentCategory HeaderType { get; set; }

			// Token: 0x06006411 RID: 25617 RVA: 0x00138629 File Offset: 0x00136829
			public bool IsValid()
			{
				return this.End > this.Start && this.End >= this.HeaderEnd && (this.HeaderType == ConversationBodyScanner.Scanner.FragmentCategory.MsHeader || this.HeaderType == ConversationBodyScanner.Scanner.FragmentCategory.NonMsHeader);
			}
		}
	}
}
