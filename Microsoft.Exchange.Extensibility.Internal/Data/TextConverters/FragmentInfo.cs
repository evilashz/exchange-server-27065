using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.Exchange.Data.TextConverters
{
	// Token: 0x0200001F RID: 31
	internal class FragmentInfo
	{
		// Token: 0x060000C3 RID: 195 RVA: 0x00005410 File Offset: 0x00003610
		internal FragmentInfo(ConversationBodyScanner bodyScanner, int startLineIndex, int endLineIndex)
		{
			this.bodyScanner = bodyScanner;
			this.startLineIndex = startLineIndex;
			this.endLineIndex = endLineIndex;
			if (bodyScanner == null)
			{
				this.startWordIndex = (this.endWordIndex = 0);
				return;
			}
			this.startWordIndex = this.GetFirstWordIndex(this.StartLineIndex);
			this.endWordIndex = this.GetFirstWordIndex(this.EndLineIndex);
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00005470 File Offset: 0x00003670
		internal FragmentInfo(ConversationBodyScanner bodyScanner)
		{
			this.startLineIndex = 0;
			this.startWordIndex = 0;
			this.endLineIndex = bodyScanner.Lines.Count;
			this.endWordIndex = bodyScanner.Words.Count;
			this.bodyScanner = bodyScanner;
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x000054AF File Offset: 0x000036AF
		public int StartLineIndex
		{
			get
			{
				return this.startLineIndex;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x000054B7 File Offset: 0x000036B7
		public int EndLineIndex
		{
			get
			{
				return this.endLineIndex;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x000054BF File Offset: 0x000036BF
		public int StartWordIndex
		{
			get
			{
				return this.startWordIndex;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x000054C7 File Offset: 0x000036C7
		public int EndWordIndex
		{
			get
			{
				return this.endWordIndex;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x000054CF File Offset: 0x000036CF
		public ConversationBodyScanner BodyScanner
		{
			get
			{
				return this.bodyScanner;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000CA RID: 202 RVA: 0x000054D7 File Offset: 0x000036D7
		public bool IsEmpty
		{
			get
			{
				return this.StartLineIndex >= this.EndLineIndex;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000CB RID: 203 RVA: 0x000054EA File Offset: 0x000036EA
		public FragmentInfo FragmentWithoutQuotedText
		{
			get
			{
				this.SeparateQuotedTextFragment();
				return this.fragmentWithoutQuotedText;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000CC RID: 204 RVA: 0x000054F8 File Offset: 0x000036F8
		public FragmentInfo QuotedTextFragment
		{
			get
			{
				this.SeparateQuotedTextFragment();
				return this.quotedTextFragment;
			}
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00005508 File Offset: 0x00003708
		public string GetSummaryText()
		{
			if (this.IsEmpty)
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder(255);
			int i = this.GetFirstWordIndex(this.StartLineIndex);
			int firstWordIndex = this.GetFirstWordIndex(this.EndLineIndex);
			int num = this.StartLineIndex;
			while (i < firstWordIndex)
			{
				if (i == this.GetFirstWordIndex(num))
				{
					while (i < firstWordIndex && this.BodyScanner.Words[i].WordLength == 1)
					{
						if (this.BodyScanner.Words[i].GetWordChar(0) != '>')
						{
							break;
						}
						i++;
					}
				}
				while (num < this.EndLineIndex - 1 && this.GetFirstWordIndex(num) <= i)
				{
					num++;
				}
				if (i >= this.bodyScanner.Words.Count)
				{
					break;
				}
				stringBuilder.Append(this.bodyScanner.Words[i].ToString());
				if (stringBuilder.Length > 255)
				{
					break;
				}
				stringBuilder.Append(' ');
				i++;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00005620 File Offset: 0x00003820
		private void SeparateQuotedTextFragment()
		{
			if (this.quotedTextSeparated)
			{
				return;
			}
			this.quotedTextSeparated = true;
			this.fragmentWithoutQuotedText = this;
			this.quotedTextFragment = FragmentInfo.Empty;
			if (this.IsEmpty)
			{
				return;
			}
			if (this.BodyScanner == null)
			{
				return;
			}
			foreach (ConversationBodyScanner.Scanner.FragmentInfo fragmentInfo in this.BodyScanner.Fragments)
			{
				if ((int)fragmentInfo.FirstLine >= this.StartLineIndex)
				{
					if ((int)fragmentInfo.FirstLine > this.EndLineIndex)
					{
						break;
					}
					if (fragmentInfo.Category == ConversationBodyScanner.Scanner.FragmentCategory.MsHeader || fragmentInfo.Category == ConversationBodyScanner.Scanner.FragmentCategory.NonMsHeader)
					{
						this.fragmentWithoutQuotedText = new FragmentInfo(this.bodyScanner, this.StartLineIndex, (int)fragmentInfo.FirstLine);
						this.quotedTextFragment = new FragmentInfo(this.bodyScanner, (int)fragmentInfo.FirstLine, this.EndLineIndex);
						break;
					}
				}
			}
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00005710 File Offset: 0x00003910
		public void WriteHtml(HtmlWriter streamWriter)
		{
			if (!this.IsEmpty)
			{
				this.bodyScanner.WriteLines(streamWriter, this.startLineIndex, this.endLineIndex - 1);
			}
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00005734 File Offset: 0x00003934
		public FragmentInfo Trim()
		{
			int num = this.StartLineIndex;
			int num2 = this.EndLineIndex;
			FragmentInfo.TrimBoundary(this.bodyScanner, ref num, ref num2);
			if (num != this.StartLineIndex || num2 != this.EndLineIndex)
			{
				return new FragmentInfo(this.BodyScanner, num, num2);
			}
			return this;
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x000057C8 File Offset: 0x000039C8
		public bool IsMatchFound(IList<string> words)
		{
			if (words == null || words.Count < 1)
			{
				return false;
			}
			if (this.IsEmpty)
			{
				return false;
			}
			int firstWordIndex = this.GetFirstWordIndex(this.StartLineIndex);
			int firstWordIndex2 = this.GetFirstWordIndex(this.EndLineIndex);
			while (firstWordIndex < firstWordIndex2)
			{
				if ((from c in words
				where string.Compare(this.bodyScanner.Words[firstWordIndex].ToString(), c, StringComparison.CurrentCultureIgnoreCase) == 0
				select c).Any<string>())
				{
					return true;
				}
				firstWordIndex++;
			}
			return false;
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00005855 File Offset: 0x00003A55
		protected int GetFirstWordIndex(int lineIndex)
		{
			if (lineIndex >= this.BodyScanner.Lines.Count)
			{
				return this.BodyScanner.Words.Count;
			}
			return (int)this.BodyScanner.Lines[lineIndex].FirstWordIndex;
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00005891 File Offset: 0x00003A91
		internal static void TrimBoundary(ConversationBodyScanner bodyScanner, ref int startLineIndex, ref int endLineIndex)
		{
			while (endLineIndex > startLineIndex)
			{
				if (!FragmentInfo.IsBlankLine(bodyScanner, startLineIndex))
				{
					break;
				}
				startLineIndex++;
			}
			while (endLineIndex > startLineIndex && FragmentInfo.IsBlankLine(bodyScanner, endLineIndex - 1))
			{
				endLineIndex--;
			}
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x000058C8 File Offset: 0x00003AC8
		internal static bool IsBlankLine(ConversationBodyScanner bodyScanner, int lineIndex)
		{
			if (lineIndex < 0 || lineIndex >= bodyScanner.Lines.Count)
			{
				return false;
			}
			ConversationBodyScanner.Scanner.LineCategory category = bodyScanner.Lines[lineIndex].Category;
			return category == ConversationBodyScanner.Scanner.LineCategory.Blank || category == ConversationBodyScanner.Scanner.LineCategory.HorizontalLineDelimiter || category == ConversationBodyScanner.Scanner.LineCategory.Skipped;
		}

		// Token: 0x0400010C RID: 268
		private readonly ConversationBodyScanner bodyScanner;

		// Token: 0x0400010D RID: 269
		private readonly int startLineIndex;

		// Token: 0x0400010E RID: 270
		private readonly int startWordIndex;

		// Token: 0x0400010F RID: 271
		private readonly int endLineIndex;

		// Token: 0x04000110 RID: 272
		private readonly int endWordIndex;

		// Token: 0x04000111 RID: 273
		private FragmentInfo fragmentWithoutQuotedText;

		// Token: 0x04000112 RID: 274
		private FragmentInfo quotedTextFragment;

		// Token: 0x04000113 RID: 275
		private bool quotedTextSeparated;

		// Token: 0x04000114 RID: 276
		public static readonly FragmentInfo Empty = new FragmentInfo(null, 0, 0);
	}
}
