using System;
using Microsoft.Exchange.Data.TextConverters.Internal.Html;

namespace Microsoft.Exchange.Data.TextConverters.Internal.Enriched
{
	// Token: 0x020001C1 RID: 449
	internal class EnrichedParser : IDisposable
	{
		// Token: 0x0600138C RID: 5004 RVA: 0x00089063 File Offset: 0x00087263
		public EnrichedParser(ConverterInput input, int maxRuns, bool testBoundaryConditions)
		{
			this.input = input;
			this.tokenBuilder = new HtmlTokenBuilder(null, maxRuns, 0, testBoundaryConditions);
			this.token = this.tokenBuilder.Token;
		}

		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x0600138D RID: 5005 RVA: 0x00089099 File Offset: 0x00087299
		public HtmlToken Token
		{
			get
			{
				return this.token;
			}
		}

		// Token: 0x0600138E RID: 5006 RVA: 0x000890A1 File Offset: 0x000872A1
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600138F RID: 5007 RVA: 0x000890B0 File Offset: 0x000872B0
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && this.input != null)
			{
				((IDisposable)this.input).Dispose();
			}
			this.input = null;
			this.parseBuffer = null;
			this.token = null;
			this.tokenBuilder = null;
		}

		// Token: 0x06001390 RID: 5008 RVA: 0x000890E4 File Offset: 0x000872E4
		public HtmlTokenId Parse()
		{
			HtmlTokenBuilder htmlTokenBuilder = this.tokenBuilder;
			if (htmlTokenBuilder.Valid)
			{
				if (htmlTokenBuilder.IncompleteTag)
				{
					int num = htmlTokenBuilder.RewindTag();
					this.input.ReportProcessed(num - this.parseStart);
					this.parseStart = num;
				}
				else
				{
					this.input.ReportProcessed(this.parseCurrent - this.parseStart);
					this.parseStart = this.parseCurrent;
					htmlTokenBuilder.Reset();
				}
			}
			char[] array = this.parseBuffer;
			int num2 = this.parseCurrent;
			int num3 = this.parseEnd;
			int num4 = this.parseThreshold;
			ConverterDecodingInput converterDecodingInput;
			int num5;
			bool flag2;
			int num6;
			for (;;)
			{
				bool flag = false;
				if (num2 + num4 > num3)
				{
					if (!this.endOfFile)
					{
						this.parseCurrent = num2;
						if (!this.input.ReadMore(ref this.parseBuffer, ref this.parseStart, ref this.parseCurrent, ref this.parseEnd))
						{
							break;
						}
						htmlTokenBuilder.BufferChanged(this.parseBuffer, this.parseStart);
						converterDecodingInput = (this.input as ConverterDecodingInput);
						if (converterDecodingInput != null && converterDecodingInput.EncodingChanged)
						{
							goto Block_7;
						}
						array = this.parseBuffer;
						num2 = this.parseCurrent;
						num3 = this.parseEnd;
						if (this.input.EndOfFile)
						{
							this.endOfFile = true;
						}
						if (!this.endOfFile && num3 - this.parseStart < this.input.MaxTokenSize)
						{
							continue;
						}
					}
					flag = true;
				}
				char c = array[num2];
				CharClass charClass = ParseSupport.GetCharClass(c);
				if (ParseSupport.InvalidUnicodeCharacter(charClass) || num4 > 1)
				{
					if (!this.SkipInvalidCharacters(ref c, ref charClass, ref num2))
					{
						num3 = this.parseEnd;
						if (!flag)
						{
							continue;
						}
						if (num2 == num3 && !htmlTokenBuilder.IsStarted && this.endOfFile)
						{
							goto IL_695;
						}
					}
					num3 = this.parseEnd;
					num4 = (this.parseThreshold = 1);
				}
				num5 = num2;
				switch (this.parseState)
				{
				case EnrichedParser.ParseState.Text:
					htmlTokenBuilder.StartText(num5);
					goto IL_1F9;
				case EnrichedParser.ParseState.Tag:
					if (this.parseEnd - this.parseCurrent < 17 && !flag)
					{
						num4 = (this.parseThreshold = 17);
						continue;
					}
					c = array[++num2];
					charClass = ParseSupport.GetCharClass(c);
					flag2 = false;
					num6 = 1;
					if (c == '/')
					{
						flag2 = true;
						num6++;
						c = array[++num2];
						charClass = ParseSupport.GetCharClass(c);
					}
					c = this.ScanTag(c, ref charClass, ref num2);
					this.nameLength = num2 - (num5 + num6);
					if (c == '>')
					{
						goto IL_366;
					}
					if (this.newLineState == EnrichedParser.NewLineState.OneNewLine)
					{
						goto Block_22;
					}
					htmlTokenBuilder.StartTag(HtmlNameIndex.Unknown, num5);
					if (flag2)
					{
						htmlTokenBuilder.SetEndTag();
					}
					htmlTokenBuilder.AddRun(RunTextType.NonSpace, HtmlRunKind.TagPrefix, num5, num5 + num6);
					htmlTokenBuilder.StartTagName();
					if (this.nameLength != 0)
					{
						htmlTokenBuilder.AddRun(RunTextType.NonSpace, HtmlRunKind.Name, num5 + num6, num2);
					}
					this.parseState = EnrichedParser.ParseState.LongTag;
					goto IL_502;
				case EnrichedParser.ParseState.LongTag:
					if (!htmlTokenBuilder.PrepareToAddMoreRuns(2, num5, HtmlRunKind.Name))
					{
						goto IL_545;
					}
					c = this.ScanTag(c, ref charClass, ref num2);
					if (num2 != num5)
					{
						this.nameLength += num2 - num5;
						htmlTokenBuilder.AddRun(RunTextType.NonSpace, HtmlRunKind.Name, num5, num2);
					}
					if (c == '>')
					{
						goto Block_46;
					}
					goto IL_502;
				}
				goto Block_15;
				IL_1F9:
				this.ParseText(c, charClass, ref num2);
				num4 = this.parseThreshold;
				if (this.token.IsEmpty && !flag)
				{
					htmlTokenBuilder.Reset();
					continue;
				}
				goto IL_227;
				IL_502:
				if (flag && num2 + num4 >= num3)
				{
					if (!this.endOfFile)
					{
						goto IL_545;
					}
					if (!this.token.IsTagBegin)
					{
						goto Block_43;
					}
					num2 = this.parseStart;
					htmlTokenBuilder.Reset();
					num5 = num2;
					htmlTokenBuilder.StartText(num5);
					c = array[++num2];
					charClass = ParseSupport.GetCharClass(c);
					this.PrepareToAddTextRun(num5);
					htmlTokenBuilder.AddTextRun(RunTextType.NonSpace, num5, num2);
					this.parseState = EnrichedParser.ParseState.Text;
					goto IL_1F9;
				}
			}
			return HtmlTokenId.None;
			Block_7:
			converterDecodingInput.EncodingChanged = false;
			return htmlTokenBuilder.MakeEmptyToken(HtmlTokenId.EncodingChange, converterDecodingInput.Encoding);
			Block_15:
			this.parseCurrent = num2;
			throw new TextConvertersException("internal error: invalid parse state");
			IL_227:
			htmlTokenBuilder.EndText();
			this.parseCurrent = num2;
			return this.token.HtmlTokenId;
			Block_22:
			this.newLineState = EnrichedParser.NewLineState.None;
			htmlTokenBuilder.StartText(num5);
			htmlTokenBuilder.AddLiteralRun(RunTextType.Space, HtmlRunKind.Text, num5, num5, 32);
			htmlTokenBuilder.EndText();
			this.parseCurrent = num5;
			return this.token.HtmlTokenId;
			IL_366:
			num2++;
			HtmlNameIndex htmlNameIndex = HtmlTokenBuilder.LookupName(array, num5 + num6, this.nameLength);
			if (htmlNameIndex == HtmlNameIndex.FlushLeft || htmlNameIndex == HtmlNameIndex.FlushRight || htmlNameIndex == HtmlNameIndex.FlushBoth || htmlNameIndex == HtmlNameIndex.Center || htmlNameIndex == HtmlNameIndex.Nofill || htmlNameIndex == HtmlNameIndex.ParaIndent || htmlNameIndex == HtmlNameIndex.Excerpt)
			{
				this.newLineState = EnrichedParser.NewLineState.EatTwoNewLines;
				if (htmlNameIndex == HtmlNameIndex.Nofill)
				{
					if (!flag2)
					{
						this.nofill++;
						this.newLineState = EnrichedParser.NewLineState.None;
					}
					else if (this.nofill != 0)
					{
						this.nofill--;
					}
				}
			}
			else
			{
				if (this.newLineState == EnrichedParser.NewLineState.OneNewLine)
				{
					this.newLineState = EnrichedParser.NewLineState.None;
					htmlTokenBuilder.StartText(num5);
					htmlTokenBuilder.AddLiteralRun(RunTextType.Space, HtmlRunKind.Text, num5, num5, 32);
					htmlTokenBuilder.EndText();
					this.parseCurrent = num5;
					return this.token.HtmlTokenId;
				}
				this.newLineState = EnrichedParser.NewLineState.None;
			}
			htmlTokenBuilder.StartTag(HtmlNameIndex.Unknown, num5);
			if (flag2)
			{
				htmlTokenBuilder.SetEndTag();
			}
			htmlTokenBuilder.AddRun(RunTextType.NonSpace, HtmlRunKind.TagPrefix, num5, num5 + num6);
			htmlTokenBuilder.StartTagName();
			if (this.nameLength != 0)
			{
				htmlTokenBuilder.AddRun(RunTextType.NonSpace, HtmlRunKind.Name, num5 + num6, num2 - 1);
			}
			htmlTokenBuilder.EndTagName(htmlNameIndex);
			htmlTokenBuilder.AddRun(RunTextType.NonSpace, HtmlRunKind.TagSuffix, num2 - 1, num2);
			htmlTokenBuilder.EndTag(true);
			if (array[num2] != '<' || num2 + 1 == num3 || array[num2 + 1] == '<' || ParseSupport.InvalidUnicodeCharacter(ParseSupport.GetCharClass(array[num2 + 1])))
			{
				this.parseState = EnrichedParser.ParseState.Text;
			}
			this.parseCurrent = num2;
			return this.token.HtmlTokenId;
			Block_43:
			htmlTokenBuilder.EndTag(true);
			this.parseCurrent = num2;
			return this.token.HtmlTokenId;
			IL_545:
			htmlTokenBuilder.EndTag(false);
			this.parseCurrent = num2;
			return this.token.HtmlTokenId;
			Block_46:
			htmlTokenBuilder.EndTagName(this.nameLength);
			num2++;
			htmlTokenBuilder.AddRun(RunTextType.NonSpace, HtmlRunKind.TagSuffix, num2 - 1, num2);
			htmlTokenBuilder.EndTag(true);
			if (array[num2] == '<' && num2 + 1 < num3 && array[num2 + 1] != '<' && !ParseSupport.InvalidUnicodeCharacter(ParseSupport.GetCharClass(array[num2 + 1])))
			{
				this.parseState = EnrichedParser.ParseState.Tag;
			}
			else
			{
				this.parseState = EnrichedParser.ParseState.Text;
			}
			this.parseCurrent = num2;
			return this.token.HtmlTokenId;
			IL_695:
			this.parseCurrent = num2;
			return htmlTokenBuilder.MakeEmptyToken(HtmlTokenId.EndOfFile);
		}

		// Token: 0x06001391 RID: 5009 RVA: 0x00089798 File Offset: 0x00087998
		private bool SkipInvalidCharacters(ref char ch, ref CharClass charClass, ref int parseCurrent)
		{
			int num = parseCurrent;
			int num2 = this.parseEnd;
			while (ParseSupport.InvalidUnicodeCharacter(charClass) && num < num2)
			{
				ch = this.parseBuffer[++num];
				charClass = ParseSupport.GetCharClass(ch);
			}
			if (this.parseThreshold > 1 && num + 1 < num2)
			{
				int num3 = num + 1;
				int num4 = num3;
				while (num4 < num2 && num3 < num + this.parseThreshold)
				{
					char c = this.parseBuffer[num4];
					CharClass charClass2 = ParseSupport.GetCharClass(c);
					if (!ParseSupport.InvalidUnicodeCharacter(charClass2))
					{
						if (num4 != num3)
						{
							this.parseBuffer[num3] = c;
							this.parseBuffer[num4] = '\0';
						}
						num3++;
					}
					num4++;
				}
				if (num4 == num2)
				{
					num2 = (this.parseEnd = this.input.RemoveGap(num3, num2));
				}
			}
			parseCurrent = num;
			return num + this.parseThreshold <= num2;
		}

		// Token: 0x06001392 RID: 5010 RVA: 0x00089864 File Offset: 0x00087A64
		private char ScanTag(char ch, ref CharClass charClass, ref int parseCurrent)
		{
			while ((parseCurrent < this.parseEnd || !ParseSupport.InvalidUnicodeCharacter(charClass)) && ch != '>')
			{
				ch = this.parseBuffer[++parseCurrent];
				charClass = ParseSupport.GetCharClass(ch);
			}
			return ch;
		}

		// Token: 0x06001393 RID: 5011 RVA: 0x000898A8 File Offset: 0x00087AA8
		private char ScanText(char ch, ref CharClass charClass, ref int parseCurrent)
		{
			char[] array = this.parseBuffer;
			while (ParseSupport.HtmlTextCharacter(charClass))
			{
				ch = array[++parseCurrent];
				charClass = ParseSupport.GetCharClass(ch);
			}
			return ch;
		}

		// Token: 0x06001394 RID: 5012 RVA: 0x000898E0 File Offset: 0x00087AE0
		private void ParseText(char ch, CharClass charClass, ref int parseCurrent)
		{
			int num = parseCurrent;
			int num2 = this.parseEnd;
			char[] array = this.parseBuffer;
			HtmlTokenBuilder htmlTokenBuilder = this.tokenBuilder;
			char c;
			CharClass charClass2;
			for (;;)
			{
				ch = this.ScanText(ch, ref charClass, ref parseCurrent);
				if (ParseSupport.WhitespaceCharacter(charClass))
				{
					if (parseCurrent != num)
					{
						this.PrepareToAddTextRun(num);
						htmlTokenBuilder.AddTextRun(RunTextType.NonSpace, num, parseCurrent);
					}
					num = parseCurrent;
					if (ch == ' ')
					{
						c = array[parseCurrent + 1];
						charClass2 = ParseSupport.GetCharClass(c);
						if (!ParseSupport.WhitespaceCharacter(charClass2))
						{
							ch = c;
							charClass = charClass2;
							parseCurrent++;
							this.PrepareToAddTextRun(num);
							htmlTokenBuilder.AddTextRun(RunTextType.Space, num, parseCurrent);
							num = parseCurrent;
							goto IL_23F;
						}
					}
					this.ParseWhitespace(ch, charClass, ref parseCurrent);
					if (this.parseThreshold > 1)
					{
						break;
					}
					num = parseCurrent;
					ch = array[parseCurrent];
					charClass = ParseSupport.GetCharClass(ch);
					goto IL_23C;
				}
				else if (ch == '<')
				{
					if (parseCurrent != num)
					{
						this.PrepareToAddTextRun(num);
						htmlTokenBuilder.AddTextRun(RunTextType.NonSpace, num, parseCurrent);
						num = parseCurrent;
					}
					if (array[parseCurrent + 1] != '<')
					{
						goto IL_12A;
					}
					this.PrepareToAddTextRun(num);
					htmlTokenBuilder.AddLiteralRun(RunTextType.NonSpace, HtmlRunKind.Text, num, parseCurrent, 60);
					parseCurrent += 2;
					num = parseCurrent;
					ch = array[parseCurrent];
					charClass = ParseSupport.GetCharClass(ch);
				}
				else if (ch == '&')
				{
					ch = array[++parseCurrent];
					charClass = ParseSupport.GetCharClass(ch);
				}
				else
				{
					if (ParseSupport.NbspCharacter(charClass))
					{
						if (parseCurrent != num)
						{
							this.PrepareToAddTextRun(num);
							htmlTokenBuilder.AddTextRun(RunTextType.NonSpace, num, parseCurrent);
						}
						num = parseCurrent;
						do
						{
							ch = array[++parseCurrent];
							charClass = ParseSupport.GetCharClass(ch);
						}
						while (ParseSupport.NbspCharacter(charClass));
						this.PrepareToAddTextRun(num);
						htmlTokenBuilder.AddTextRun(RunTextType.Nbsp, num, parseCurrent);
						goto IL_23C;
					}
					if (parseCurrent != num)
					{
						this.PrepareToAddTextRun(num);
						htmlTokenBuilder.AddTextRun(RunTextType.NonSpace, num, parseCurrent);
					}
					if (parseCurrent >= num2)
					{
						return;
					}
					for (;;)
					{
						ch = array[++parseCurrent];
						charClass = ParseSupport.GetCharClass(ch);
						if (!ParseSupport.InvalidUnicodeCharacter(charClass) || parseCurrent >= num2)
						{
							goto IL_23C;
						}
					}
				}
				IL_23F:
				if (!htmlTokenBuilder.PrepareToAddMoreRuns(3, num, HtmlRunKind.Text))
				{
					return;
				}
				continue;
				IL_23C:
				num = parseCurrent;
				goto IL_23F;
			}
			return;
			IL_12A:
			c = array[parseCurrent + 1];
			charClass2 = ParseSupport.GetCharClass(c);
			if (!ParseSupport.InvalidUnicodeCharacter(charClass2))
			{
				this.parseState = EnrichedParser.ParseState.Tag;
				return;
			}
			if (this.endOfFile && parseCurrent + 1 == num2)
			{
				parseCurrent++;
				htmlTokenBuilder.AddTextRun(RunTextType.NonSpace, num, parseCurrent);
				return;
			}
			this.parseThreshold = 2;
		}

		// Token: 0x06001395 RID: 5013 RVA: 0x00089B40 File Offset: 0x00087D40
		private void ParseWhitespace(char ch, CharClass charClass, ref int parseCurrent)
		{
			int num = parseCurrent;
			char[] array = this.parseBuffer;
			HtmlTokenBuilder htmlTokenBuilder = this.tokenBuilder;
			for (;;)
			{
				char c = ch;
				switch (c)
				{
				case '\t':
					do
					{
						ch = array[++parseCurrent];
					}
					while (ch == '\t');
					this.PrepareToAddTextRun(num);
					htmlTokenBuilder.AddTextRun(RunTextType.Tabulation, num, parseCurrent);
					goto IL_196;
				case '\n':
					goto IL_AD;
				case '\v':
				case '\f':
					break;
				case '\r':
				{
					if (array[parseCurrent + 1] == '\n')
					{
						parseCurrent++;
						goto IL_AD;
					}
					CharClass charClass2 = ParseSupport.GetCharClass(array[parseCurrent + 1]);
					if (ParseSupport.InvalidUnicodeCharacter(charClass2) && (!this.endOfFile || parseCurrent + 1 < this.parseEnd))
					{
						this.parseThreshold = 2;
						goto IL_196;
					}
					goto IL_AD;
				}
				default:
					if (c == ' ')
					{
						do
						{
							ch = array[++parseCurrent];
						}
						while (ch == ' ');
						this.PrepareToAddTextRun(num);
						htmlTokenBuilder.AddTextRun(RunTextType.Space, num, parseCurrent);
						goto IL_196;
					}
					break;
				}
				do
				{
					ch = array[++parseCurrent];
				}
				while (ch == '\v' || ch == '\f');
				this.PrepareToAddTextRun(num);
				htmlTokenBuilder.AddTextRun(RunTextType.UnusualWhitespace, num, parseCurrent);
				IL_196:
				charClass = ParseSupport.GetCharClass(ch);
				num = parseCurrent;
				if (!ParseSupport.WhitespaceCharacter(charClass) || !htmlTokenBuilder.PrepareToAddMoreRuns(2, parseCurrent, HtmlRunKind.Text) || this.parseThreshold != 1)
				{
					break;
				}
				continue;
				IL_AD:
				ch = array[++parseCurrent];
				if (this.newLineState == EnrichedParser.NewLineState.None && this.nofill == 0)
				{
					this.newLineState = EnrichedParser.NewLineState.OneNewLine;
					htmlTokenBuilder.AddInvalidRun(parseCurrent, HtmlRunKind.Text);
					goto IL_196;
				}
				if (this.newLineState == EnrichedParser.NewLineState.EatTwoNewLines)
				{
					htmlTokenBuilder.AddInvalidRun(parseCurrent, HtmlRunKind.Text);
					this.newLineState = EnrichedParser.NewLineState.EatOneNewLine;
					goto IL_196;
				}
				if (this.newLineState == EnrichedParser.NewLineState.EatOneNewLine)
				{
					htmlTokenBuilder.AddInvalidRun(parseCurrent, HtmlRunKind.Text);
					this.newLineState = EnrichedParser.NewLineState.ManyNewLines;
					goto IL_196;
				}
				htmlTokenBuilder.AddTextRun(RunTextType.NewLine, num, parseCurrent);
				this.newLineState = EnrichedParser.NewLineState.ManyNewLines;
				goto IL_196;
			}
		}

		// Token: 0x06001396 RID: 5014 RVA: 0x00089D12 File Offset: 0x00087F12
		private void PrepareToAddTextRun(int runStart)
		{
			if (this.newLineState != EnrichedParser.NewLineState.None)
			{
				this.FlushNewLineState(runStart);
			}
		}

		// Token: 0x06001397 RID: 5015 RVA: 0x00089D23 File Offset: 0x00087F23
		private void FlushNewLineState(int runStart)
		{
			if (this.newLineState == EnrichedParser.NewLineState.OneNewLine)
			{
				this.tokenBuilder.AddLiteralRun(RunTextType.Space, HtmlRunKind.Text, runStart, runStart, 32);
			}
			this.newLineState = EnrichedParser.NewLineState.None;
		}

		// Token: 0x04001368 RID: 4968
		private const int MaxValidTagLength = 17;

		// Token: 0x04001369 RID: 4969
		private ConverterInput input;

		// Token: 0x0400136A RID: 4970
		private bool endOfFile;

		// Token: 0x0400136B RID: 4971
		private EnrichedParser.ParseState parseState;

		// Token: 0x0400136C RID: 4972
		private char[] parseBuffer;

		// Token: 0x0400136D RID: 4973
		private int parseStart;

		// Token: 0x0400136E RID: 4974
		private int parseCurrent;

		// Token: 0x0400136F RID: 4975
		private int parseEnd;

		// Token: 0x04001370 RID: 4976
		private int parseThreshold = 1;

		// Token: 0x04001371 RID: 4977
		private int nameLength;

		// Token: 0x04001372 RID: 4978
		private HtmlTokenBuilder tokenBuilder;

		// Token: 0x04001373 RID: 4979
		private HtmlToken token;

		// Token: 0x04001374 RID: 4980
		private EnrichedParser.NewLineState newLineState;

		// Token: 0x04001375 RID: 4981
		private int nofill;

		// Token: 0x020001C2 RID: 450
		private enum NewLineState
		{
			// Token: 0x04001377 RID: 4983
			None,
			// Token: 0x04001378 RID: 4984
			OneNewLine,
			// Token: 0x04001379 RID: 4985
			ManyNewLines,
			// Token: 0x0400137A RID: 4986
			EatOneNewLine,
			// Token: 0x0400137B RID: 4987
			EatTwoNewLines
		}

		// Token: 0x020001C3 RID: 451
		protected enum ParseState : byte
		{
			// Token: 0x0400137D RID: 4989
			Text,
			// Token: 0x0400137E RID: 4990
			Tag,
			// Token: 0x0400137F RID: 4991
			LongTag
		}
	}
}
