using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.TextConverters.Internal.Format;

namespace Microsoft.Exchange.Data.TextConverters
{
	// Token: 0x0200001E RID: 30
	internal class BodyDiffer
	{
		// Token: 0x060000AA RID: 170 RVA: 0x0000480A File Offset: 0x00002A0A
		public BodyDiffer(ConversationBodyScanner parentScanner, ConversationBodyScanner childScanner)
		{
			this.childBodyFragment = new BodyFragmentInfo(childScanner);
			this.parentBodyFragment = new BodyFragmentInfo(parentScanner);
			this.WordByWordDiff();
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00004846 File Offset: 0x00002A46
		public BodyDiffer(BodyFragmentInfo childBodyFragment, BodyFragmentInfo parentBodyFragment)
		{
			this.childBodyFragment = childBodyFragment;
			this.parentBodyFragment = parentBodyFragment;
			this.Diff();
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000AC RID: 172 RVA: 0x00004878 File Offset: 0x00002A78
		public FragmentInfo UniqueBodyPart
		{
			get
			{
				return this.uniqueBodyPart;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000AD RID: 173 RVA: 0x00004880 File Offset: 0x00002A80
		public FragmentInfo DisclaimerPart
		{
			get
			{
				return this.disclaimerPart;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000AE RID: 174 RVA: 0x00004888 File Offset: 0x00002A88
		internal ConversationBodyScanner ChildScanner
		{
			get
			{
				return this.childBodyFragment.BodyScanner;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000AF RID: 175 RVA: 0x00004895 File Offset: 0x00002A95
		internal ConversationBodyScanner ParentScanner
		{
			get
			{
				return this.parentBodyFragment.BodyScanner;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x000048A2 File Offset: 0x00002AA2
		internal int LastUniqueWordIndex
		{
			get
			{
				return this.lastUniqueWord;
			}
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x000048AA File Offset: 0x00002AAA
		public void WriteUniqueBody(HtmlWriter streamWriter)
		{
			this.UniqueBodyPart.WriteHtml(streamWriter);
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x000048B8 File Offset: 0x00002AB8
		private static bool IsSameWord(TextRun left, TextRun right)
		{
			return BodyDiffer.IsSameWord(left, right, 0, left.WordLength, 0, right.WordLength);
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x000048D4 File Offset: 0x00002AD4
		private static bool IsSameWord(TextRun left, TextRun right, int leftBegin, int leftEnd, int rightBegin, int rightEnd)
		{
			if (leftEnd - leftBegin != rightEnd - rightBegin)
			{
				return false;
			}
			if (leftEnd <= left.WordLength && rightEnd <= right.WordLength)
			{
				while (leftBegin < leftEnd && rightBegin < rightEnd)
				{
					if (!BodyDiffer.IsSameChar(left.GetWordChar(leftBegin), right.GetWordChar(rightBegin)))
					{
						return false;
					}
					leftBegin++;
					rightBegin++;
				}
				return true;
			}
			return false;
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00004938 File Offset: 0x00002B38
		private static bool IsSameChar(char left, char right)
		{
			if (left == right)
			{
				return true;
			}
			char c;
			if (!BodyDiffer.SameCharacters.TryGetValue(left, out c))
			{
				c = left;
			}
			char c2;
			if (!BodyDiffer.SameCharacters.TryGetValue(right, out c2))
			{
				c2 = right;
			}
			return c == c2;
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00004974 File Offset: 0x00002B74
		private static bool GetHrefTagIndex(TextRun word, out int beginIndex, out int endIndex)
		{
			char c = '<';
			beginIndex = (endIndex = -1);
			int i;
			for (i = word.WordLength - 1; i >= 0; i--)
			{
				if (word.GetWordChar(i) == '>')
				{
					c = '<';
					endIndex = i;
					break;
				}
				if (word.GetWordChar(i) == ']')
				{
					c = '[';
					endIndex = i;
					break;
				}
			}
			for (i -= 5; i >= 0; i--)
			{
				if (word.GetWordChar(i) == c)
				{
					beginIndex = i;
					return true;
				}
			}
			endIndex = -1;
			beginIndex = -1;
			return false;
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x000049F0 File Offset: 0x00002BF0
		private static bool IsListNumberOrBullet(TextRun textRun)
		{
			char wordChar = textRun.GetWordChar(0);
			switch (wordChar)
			{
			case '*':
			case '+':
				return textRun.WordLength == 1;
			case ',':
			case '-':
			case '.':
			case '/':
			case '0':
				break;
			case '1':
			case '2':
			case '3':
			case '4':
			case '5':
			case '6':
			case '7':
			case '8':
			case '9':
				if (textRun.WordLength == 2)
				{
					return textRun.GetWordChar(1) == '.' || textRun.GetWordChar(1) == ')' || textRun.GetWordChar(1) == ':';
				}
				return textRun.WordLength == 3 && char.IsDigit(textRun.GetWordChar(1)) && (textRun.GetWordChar(2) == '.' || textRun.GetWordChar(2) == ')' || textRun.GetWordChar(2) == ':');
			default:
				switch (wordChar)
				{
				case 'a':
				case 'b':
				case 'c':
				case 'd':
				case 'e':
				case 'f':
				case 'g':
				case 'h':
					return textRun.WordLength == 2 && (textRun.GetWordChar(1) == '.' || textRun.GetWordChar(1) == ')');
				}
				break;
			}
			return false;
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00004B28 File Offset: 0x00002D28
		private static int GetLastNormalFragmentIndex(BodyFragmentInfo bodyFragmentInfo, int beforeWordIndex)
		{
			int num = -1;
			int num2 = 0;
			while (num2 < bodyFragmentInfo.BodyScanner.Fragments.Count && (int)bodyFragmentInfo.BodyScanner.Fragments[num2].FirstLine < bodyFragmentInfo.EndLineIndex && bodyFragmentInfo.BodyScanner.Fragments[num2].FirstWord <= beforeWordIndex)
			{
				if (bodyFragmentInfo.BodyScanner.Fragments[num2].Category == ConversationBodyScanner.Scanner.FragmentCategory.Normal)
				{
					num = num2;
				}
				num2++;
			}
			if (num == -1)
			{
				num = num2;
			}
			return num;
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00004BAC File Offset: 0x00002DAC
		private static Dictionary<char, char> SameCharactersCollection()
		{
			return new Dictionary<char, char>
			{
				{
					'‘',
					'\''
				},
				{
					'“',
					'"'
				},
				{
					'”',
					'"'
				},
				{
					'’',
					'\''
				},
				{
					'–',
					'-'
				}
			};
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00004C04 File Offset: 0x00002E04
		private bool IsHrefBeginText(int iChildWordIndex, bool forwardDirection)
		{
			string text = forwardDirection ? "<<" : ">>";
			int wordLength = this.ChildScanner.Words[iChildWordIndex].WordLength;
			int length = text.Length;
			if (wordLength < length)
			{
				return false;
			}
			int num = forwardDirection ? 0 : (wordLength - length);
			int num2 = 0;
			while (num2 < length && num2 >= 0)
			{
				if (this.ChildScanner.Words[iChildWordIndex].GetWordChar(num) != text[num2])
				{
					return false;
				}
				num2++;
				num++;
			}
			return true;
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00004C94 File Offset: 0x00002E94
		private int HrefTextEndIndex(int iChildWordIndex, bool forwardDirection)
		{
			int num = forwardDirection ? 1 : -1;
			if (this.IsHrefBeginText(iChildWordIndex, forwardDirection))
			{
				int num2 = 1;
				while (num2 < 10 && iChildWordIndex > 0 && iChildWordIndex < this.ChildScanner.Words.Count)
				{
					if (this.IsHrefBeginText(iChildWordIndex, !forwardDirection))
					{
						return iChildWordIndex + num;
					}
					num2++;
					iChildWordIndex += num;
				}
			}
			return -1;
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00004CF0 File Offset: 0x00002EF0
		private bool MatchWordWithHref(int iChildWordIndex, int iParentWordIndex)
		{
			TextRun textRun = this.ChildScanner.Words[iChildWordIndex];
			TextRun right = this.ParentScanner.Words[iParentWordIndex];
			int num;
			int num2;
			return BodyDiffer.GetHrefTagIndex(textRun, out num, out num2) && BodyDiffer.IsSameWord(textRun, right, 0, num, 0, num) && BodyDiffer.IsSameWord(textRun, right, num2 + 1, textRun.WordLength, num, right.WordLength);
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00004D58 File Offset: 0x00002F58
		private bool IsHorizontalLine(TextRun word)
		{
			for (int i = word.WordLength - 1; i >= 0; i--)
			{
				if (word.GetWordChar(i) != '_' && word.GetWordChar(i) != '_')
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00004D94 File Offset: 0x00002F94
		private bool MatchWord(ref int childWordIndex, ref int parentWordIndex, bool forwardMatch, int recursionLevel)
		{
			int num = forwardMatch ? 1 : -1;
			if (recursionLevel > 5)
			{
				return false;
			}
			if (childWordIndex >= this.childBodyFragment.EndWordIndex || parentWordIndex >= this.parentBodyFragment.EndWordIndex || childWordIndex < 0 || parentWordIndex < 0)
			{
				return false;
			}
			if (BodyDiffer.IsSameWord(this.ParentScanner.Words[parentWordIndex], this.ChildScanner.Words[childWordIndex]))
			{
				return true;
			}
			int num5;
			int num6;
			if (this.IsHrefBeginText(childWordIndex, forwardMatch))
			{
				int num2 = this.HrefTextEndIndex(childWordIndex, forwardMatch);
				if (this.MatchWord(ref num2, ref parentWordIndex, forwardMatch, recursionLevel + 1))
				{
					childWordIndex = num2;
					return true;
				}
			}
			else if (this.IsHorizontalLine(this.ChildScanner.Words[childWordIndex]))
			{
				int num3 = childWordIndex + num;
				if (this.MatchWord(ref num3, ref parentWordIndex, forwardMatch, recursionLevel + 1))
				{
					childWordIndex = num3;
					return true;
				}
			}
			else if (this.IsHorizontalLine(this.ParentScanner.Words[parentWordIndex]))
			{
				int num4 = parentWordIndex + num;
				if (this.MatchWord(ref childWordIndex, ref num4, forwardMatch, recursionLevel + 1))
				{
					parentWordIndex = num4;
					return true;
				}
			}
			else if (BodyDiffer.GetHrefTagIndex(this.ChildScanner.Words[childWordIndex], out num5, out num6))
			{
				if (num5 == 0)
				{
					int num7 = childWordIndex + num;
					if (this.MatchWord(ref num7, ref parentWordIndex, forwardMatch, recursionLevel + 1))
					{
						childWordIndex = num7;
						return true;
					}
				}
				else if (this.MatchWordWithHref(childWordIndex, parentWordIndex))
				{
					return true;
				}
			}
			else if (this.ChildScanner.Words[childWordIndex].WordLength == 1 && this.ChildScanner.Words[childWordIndex].GetWordChar(0) == '>')
			{
				int num8 = childWordIndex + num;
				if (this.MatchWord(ref num8, ref parentWordIndex, forwardMatch, recursionLevel + 1))
				{
					childWordIndex = num8;
					return true;
				}
			}
			else if (BodyDiffer.IsListNumberOrBullet(this.ChildScanner.Words[childWordIndex]))
			{
				int num9 = childWordIndex + num;
				if (this.MatchWord(ref num9, ref parentWordIndex, forwardMatch, recursionLevel + 1))
				{
					childWordIndex = num9;
					return true;
				}
			}
			else if (BodyDiffer.IsListNumberOrBullet(this.ParentScanner.Words[parentWordIndex]))
			{
				int num10 = parentWordIndex + num;
				if (this.MatchWord(ref childWordIndex, ref num10, forwardMatch, recursionLevel + 1))
				{
					parentWordIndex = num10;
					return true;
				}
			}
			return false;
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00004FBC File Offset: 0x000031BC
		private void Diff()
		{
			BodyFragmentInfo bodyFragmentInfo;
			this.childBodyFragment.ExtractNestedBodyParts(this.parentBodyFragment.BodyTag, out bodyFragmentInfo, out this.uniqueBodyPart, out this.disclaimerPart);
			if (bodyFragmentInfo != null)
			{
				return;
			}
			this.uniqueBodyPart = FragmentInfo.Empty;
			this.disclaimerPart = FragmentInfo.Empty;
			this.WordByWordDiff();
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00005010 File Offset: 0x00003210
		private void WordByWordDiff()
		{
			if (this.parentBodyFragment.StartWordIndex == this.parentBodyFragment.EndWordIndex)
			{
				this.uniqueBodyPart = this.childBodyFragment.Trim();
				return;
			}
			if (this.childBodyFragment.IsEmpty)
			{
				return;
			}
			int lastNormalFragmentIndex = BodyDiffer.GetLastNormalFragmentIndex(this.parentBodyFragment, this.parentBodyFragment.EndWordIndex);
			int lastNormalFragmentIndex2 = BodyDiffer.GetLastNormalFragmentIndex(this.childBodyFragment, this.childBodyFragment.EndWordIndex);
			int num = this.childBodyFragment.StartWordIndex;
			int num2 = this.parentBodyFragment.StartWordIndex;
			int disclaimerWordStart = this.childBodyFragment.EndWordIndex;
			if (lastNormalFragmentIndex < this.ParentScanner.Fragments.Count && lastNormalFragmentIndex2 < this.ChildScanner.Fragments.Count && this.ParentScanner.Fragments[lastNormalFragmentIndex].FirstWord < this.parentBodyFragment.EndWordIndex && this.ChildScanner.Fragments[lastNormalFragmentIndex2].FirstWord < this.childBodyFragment.EndWordIndex)
			{
				num = this.ChildScanner.Fragments[lastNormalFragmentIndex2].FirstWord;
				num2 = this.ParentScanner.Fragments[lastNormalFragmentIndex].FirstWord;
				while (num < this.childBodyFragment.EndWordIndex && num2 < this.parentBodyFragment.EndWordIndex && this.MatchWord(ref num, ref num2, true, 0))
				{
					num++;
					num2++;
				}
			}
			if (num2 != this.parentBodyFragment.EndWordIndex)
			{
				num = this.childBodyFragment.EndWordIndex - 1;
				num2 = this.parentBodyFragment.EndWordIndex - 1;
			}
			else
			{
				disclaimerWordStart = num;
				num = this.ChildScanner.Fragments[lastNormalFragmentIndex2].FirstWord - 1;
				num2 = this.ParentScanner.Fragments[lastNormalFragmentIndex].FirstWord - 1;
			}
			while (num >= this.childBodyFragment.StartWordIndex && num2 >= this.parentBodyFragment.StartWordIndex && this.MatchWord(ref num, ref num2, false, 0))
			{
				num--;
				num2--;
			}
			this.InitializeUniquePart(num, num2 < 0);
			this.InitializeDisclaimerPart(disclaimerWordStart);
			if (this.uniqueBodyPart.IsEmpty && !this.disclaimerPart.IsEmpty)
			{
				this.uniqueBodyPart = this.disclaimerPart;
				this.disclaimerPart = FragmentInfo.Empty;
			}
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00005258 File Offset: 0x00003458
		private void InitializeUniquePart(int lastUniqueWordIndex, bool fullParentMatched)
		{
			this.lastUniqueWord = ((lastUniqueWordIndex < this.childBodyFragment.EndWordIndex) ? lastUniqueWordIndex : (this.childBodyFragment.EndWordIndex - 1));
			int lastNormalFragmentIndex = BodyDiffer.GetLastNormalFragmentIndex(this.childBodyFragment, lastUniqueWordIndex);
			int num = (this.ChildScanner.Fragments.Count > lastNormalFragmentIndex + 1) ? ((int)this.ChildScanner.Fragments[lastNormalFragmentIndex + 1].FirstLine) : this.childBodyFragment.EndLineIndex;
			num = ((num >= this.childBodyFragment.EndLineIndex) ? this.childBodyFragment.EndLineIndex : num);
			int startLineIndex = this.childBodyFragment.StartLineIndex;
			FragmentInfo.TrimBoundary(this.ChildScanner, ref startLineIndex, ref num);
			if (fullParentMatched)
			{
				for (int i = this.childBodyFragment.StartLineIndex; i < num; i++)
				{
					if ((ulong)this.ChildScanner.Lines[i].FirstWordIndex > (ulong)((long)this.lastUniqueWord))
					{
						num = i;
						FragmentInfo.TrimBoundary(this.ChildScanner, ref startLineIndex, ref num);
						if (num > 0 && this.ChildScanner.Lines[num - 1].Category == ConversationBodyScanner.Scanner.LineCategory.PotentialNonMsHeader)
						{
							num--;
						}
					}
				}
			}
			this.uniqueBodyPart = new FragmentInfo(this.ChildScanner, startLineIndex, num);
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x0000538C File Offset: 0x0000358C
		private void InitializeDisclaimerPart(int disclaimerWordStart)
		{
			int num = this.childBodyFragment.EndLineIndex;
			while (num > this.UniqueBodyPart.EndLineIndex && (long)disclaimerWordStart <= (long)((ulong)this.ChildScanner.Lines[num - 1].FirstWordIndex))
			{
				num--;
			}
			int endLineIndex = this.childBodyFragment.EndLineIndex;
			FragmentInfo.TrimBoundary(this.ChildScanner, ref num, ref endLineIndex);
			this.disclaimerPart = new FragmentInfo(this.ChildScanner, num, endLineIndex);
		}

		// Token: 0x04000102 RID: 258
		private const int MaxMatchWordRecursionLevel = 5;

		// Token: 0x04000103 RID: 259
		private const string BeginHrefTag = "<<";

		// Token: 0x04000104 RID: 260
		private const string EndHrefTag = ">>";

		// Token: 0x04000105 RID: 261
		private const char QuoteChar = '>';

		// Token: 0x04000106 RID: 262
		private static readonly Dictionary<char, char> SameCharacters = BodyDiffer.SameCharactersCollection();

		// Token: 0x04000107 RID: 263
		private BodyFragmentInfo childBodyFragment;

		// Token: 0x04000108 RID: 264
		private BodyFragmentInfo parentBodyFragment;

		// Token: 0x04000109 RID: 265
		private FragmentInfo uniqueBodyPart = FragmentInfo.Empty;

		// Token: 0x0400010A RID: 266
		private FragmentInfo disclaimerPart = FragmentInfo.Empty;

		// Token: 0x0400010B RID: 267
		private int lastUniqueWord;
	}
}
