using System;

namespace Microsoft.Exchange.Data.TextConverters.Internal.Text
{
	// Token: 0x02000283 RID: 643
	internal class TextParser : IDisposable
	{
		// Token: 0x06001A09 RID: 6665 RVA: 0x000CE4A0 File Offset: 0x000CC6A0
		public TextParser(ConverterInput input, bool unwrapFlowed, bool unwrapDelSp, int maxRuns, bool testBoundaryConditions)
		{
			this.input = input;
			this.tokenBuilder = new TextTokenBuilder(null, maxRuns, testBoundaryConditions);
			this.token = this.tokenBuilder.Token;
			this.unwrapFlowed = unwrapFlowed;
			this.unwrapDelSpace = unwrapDelSp;
		}

		// Token: 0x1700068D RID: 1677
		// (get) Token: 0x06001A0A RID: 6666 RVA: 0x000CE4FE File Offset: 0x000CC6FE
		public TextToken Token
		{
			get
			{
				return this.token;
			}
		}

		// Token: 0x06001A0B RID: 6667 RVA: 0x000CE508 File Offset: 0x000CC708
		public void Initialize(string fragment)
		{
			(this.input as ConverterBufferInput).Initialize(fragment);
			this.endOfFile = false;
			this.parseBuffer = null;
			this.parseStart = 0;
			this.parseCurrent = 0;
			this.parseEnd = 0;
			this.parseThreshold = 1;
			this.tokenBuilder.Reset();
			this.lastSpace = false;
			this.lineCount = 0;
			this.quotingExpected = true;
			this.quotingLevel = 0;
			this.lastLineQuotingLevel = 0;
			this.lastLineFlowed = false;
			this.signaturePossible = true;
		}

		// Token: 0x06001A0C RID: 6668 RVA: 0x000CE58C File Offset: 0x000CC78C
		public TextTokenId Parse()
		{
			if (this.tokenBuilder.Valid)
			{
				this.input.ReportProcessed(this.parseCurrent - this.parseStart);
				this.parseStart = this.parseCurrent;
				this.tokenBuilder.Reset();
			}
			ConverterDecodingInput converterDecodingInput;
			for (;;)
			{
				bool flag = false;
				if (this.parseCurrent + this.parseThreshold > this.parseEnd)
				{
					if (!this.endOfFile)
					{
						if (!this.input.ReadMore(ref this.parseBuffer, ref this.parseStart, ref this.parseCurrent, ref this.parseEnd))
						{
							break;
						}
						this.tokenBuilder.BufferChanged(this.parseBuffer, this.parseStart);
						converterDecodingInput = (this.input as ConverterDecodingInput);
						if (converterDecodingInput != null && converterDecodingInput.EncodingChanged)
						{
							goto Block_6;
						}
						if (this.input.EndOfFile)
						{
							this.endOfFile = true;
						}
						if (!this.endOfFile && this.parseEnd - this.parseStart < this.input.MaxTokenSize)
						{
							continue;
						}
					}
					flag = true;
				}
				char c = this.parseBuffer[this.parseCurrent];
				CharClass charClass = ParseSupport.GetCharClass(c);
				if (!ParseSupport.InvalidUnicodeCharacter(charClass))
				{
					if (this.parseThreshold <= 1)
					{
						goto IL_2D2;
					}
				}
				while (ParseSupport.InvalidUnicodeCharacter(charClass) && this.parseCurrent < this.parseEnd)
				{
					c = this.parseBuffer[++this.parseCurrent];
					charClass = ParseSupport.GetCharClass(c);
				}
				if (this.parseThreshold > 1 && this.parseCurrent + 1 < this.parseEnd)
				{
					int num = this.parseCurrent + 1;
					int num2 = this.parseCurrent + 1;
					while (num < this.parseEnd && num2 < this.parseCurrent + this.parseThreshold)
					{
						char c2 = this.parseBuffer[num];
						CharClass charClass2 = ParseSupport.GetCharClass(c2);
						if (!ParseSupport.InvalidUnicodeCharacter(charClass2))
						{
							if (num != num2)
							{
								this.parseBuffer[num2] = c2;
								this.parseBuffer[num] = '\0';
							}
							num2++;
						}
						num++;
					}
					if (num == this.parseEnd && this.parseCurrent + this.parseThreshold > num2)
					{
						Array.Copy(this.parseBuffer, this.parseCurrent, this.parseBuffer, this.parseEnd - (num2 - this.parseCurrent), num2 - this.parseCurrent);
						this.parseCurrent = this.parseEnd - (num2 - this.parseCurrent);
						this.input.ReportProcessed(this.parseCurrent - this.parseStart);
						this.parseStart = this.parseCurrent;
					}
				}
				if (this.parseCurrent + this.parseThreshold > this.parseEnd)
				{
					if (!flag)
					{
						continue;
					}
					if (this.parseCurrent == this.parseEnd && !this.tokenBuilder.IsStarted && this.endOfFile)
					{
						goto IL_589;
					}
				}
				this.parseThreshold = 1;
				IL_2D2:
				int num3 = this.parseCurrent;
				this.tokenBuilder.StartText(num3);
				while (this.tokenBuilder.PrepareToAddMoreRuns(9, num3, RunKind.Text))
				{
					while (ParseSupport.TextUriCharacter(charClass))
					{
						c = this.parseBuffer[++this.parseCurrent];
						charClass = ParseSupport.GetCharClass(c);
					}
					if (ParseSupport.TextNonUriCharacter(charClass))
					{
						if (this.parseCurrent != num3)
						{
							this.AddTextRun(RunTextType.NonSpace, num3, this.parseCurrent);
						}
						num3 = this.parseCurrent;
						do
						{
							c = this.parseBuffer[++this.parseCurrent];
							charClass = ParseSupport.GetCharClass(c);
						}
						while (ParseSupport.NbspCharacter(charClass));
						this.AddTextRun(RunTextType.NonSpace, num3, this.parseCurrent);
					}
					else if (ParseSupport.WhitespaceCharacter(charClass))
					{
						if (this.parseCurrent != num3)
						{
							this.AddTextRun(RunTextType.NonSpace, num3, this.parseCurrent);
						}
						num3 = this.parseCurrent;
						if (c == ' ')
						{
							char c2 = this.parseBuffer[this.parseCurrent + 1];
							CharClass charClass2 = ParseSupport.GetCharClass(c2);
							if (!ParseSupport.WhitespaceCharacter(charClass2))
							{
								c = c2;
								charClass = charClass2;
								this.parseCurrent++;
								this.AddTextRun(RunTextType.Space, num3, this.parseCurrent);
								num3 = this.parseCurrent;
								continue;
							}
						}
						this.ParseWhitespace(c, charClass);
						if (this.parseThreshold > 1)
						{
							break;
						}
						num3 = this.parseCurrent;
						c = this.parseBuffer[this.parseCurrent];
						charClass = ParseSupport.GetCharClass(c);
					}
					else if (ParseSupport.NbspCharacter(charClass))
					{
						if (this.parseCurrent != num3)
						{
							this.AddTextRun(RunTextType.NonSpace, num3, this.parseCurrent);
						}
						num3 = this.parseCurrent;
						do
						{
							c = this.parseBuffer[++this.parseCurrent];
							charClass = ParseSupport.GetCharClass(c);
						}
						while (ParseSupport.NbspCharacter(charClass));
						this.AddTextRun(RunTextType.Nbsp, num3, this.parseCurrent);
					}
					else
					{
						if (this.parseCurrent != num3)
						{
							this.AddTextRun(RunTextType.NonSpace, num3, this.parseCurrent);
						}
						if (this.parseCurrent >= this.parseEnd)
						{
							break;
						}
						do
						{
							c = this.parseBuffer[++this.parseCurrent];
							charClass = ParseSupport.GetCharClass(c);
						}
						while (ParseSupport.InvalidUnicodeCharacter(charClass) && this.parseCurrent < this.parseEnd);
					}
					num3 = this.parseCurrent;
				}
				if (!this.token.IsEmpty)
				{
					goto IL_572;
				}
				this.tokenBuilder.Reset();
				this.input.ReportProcessed(this.parseCurrent - this.parseStart);
				this.parseStart = this.parseCurrent;
			}
			return TextTokenId.None;
			Block_6:
			converterDecodingInput.EncodingChanged = false;
			return this.tokenBuilder.MakeEmptyToken(TextTokenId.EncodingChange, converterDecodingInput.Encoding);
			IL_572:
			this.tokenBuilder.EndText();
			return (TextTokenId)this.token.TokenId;
			IL_589:
			return this.tokenBuilder.MakeEmptyToken(TextTokenId.EndOfFile);
		}

		// Token: 0x06001A0D RID: 6669 RVA: 0x000CEB2E File Offset: 0x000CCD2E
		void IDisposable.Dispose()
		{
			if (this.input != null)
			{
				((IDisposable)this.input).Dispose();
			}
			this.input = null;
			this.parseBuffer = null;
			this.token = null;
			this.tokenBuilder = null;
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001A0E RID: 6670 RVA: 0x000CEB68 File Offset: 0x000CCD68
		private void ParseWhitespace(char ch, CharClass charClass)
		{
			int num = this.parseCurrent;
			do
			{
				char c = ch;
				switch (c)
				{
				case '\t':
					do
					{
						ch = this.parseBuffer[++this.parseCurrent];
					}
					while (ch == '\t');
					this.AddTextRun(RunTextType.Tabulation, num, this.parseCurrent);
					goto IL_196;
				case '\n':
					ch = this.parseBuffer[++this.parseCurrent];
					this.AddTextRun(RunTextType.NewLine, num, this.parseCurrent);
					goto IL_196;
				case '\v':
				case '\f':
					break;
				case '\r':
					if (this.parseBuffer[this.parseCurrent + 1] != '\n')
					{
						CharClass charClass2 = ParseSupport.GetCharClass(this.parseBuffer[this.parseCurrent + 1]);
						if (ParseSupport.InvalidUnicodeCharacter(charClass2) && (!this.endOfFile || this.parseCurrent + 1 < this.parseEnd))
						{
							this.parseThreshold = 2;
							goto IL_196;
						}
					}
					else
					{
						this.parseCurrent++;
					}
					ch = this.parseBuffer[++this.parseCurrent];
					this.AddTextRun(RunTextType.NewLine, num, this.parseCurrent);
					goto IL_196;
				default:
					if (c == ' ')
					{
						do
						{
							ch = this.parseBuffer[++this.parseCurrent];
						}
						while (ch == ' ');
						this.AddTextRun(RunTextType.Space, num, this.parseCurrent);
						goto IL_196;
					}
					break;
				}
				do
				{
					ch = this.parseBuffer[++this.parseCurrent];
				}
				while (ch == '\v' || ch == '\f');
				this.AddTextRun(RunTextType.UnusualWhitespace, num, this.parseCurrent);
				IL_196:
				charClass = ParseSupport.GetCharClass(ch);
				num = this.parseCurrent;
			}
			while (ParseSupport.WhitespaceCharacter(charClass) && this.tokenBuilder.PrepareToAddMoreRuns(4, num, RunKind.Text) && this.parseThreshold == 1);
		}

		// Token: 0x06001A0F RID: 6671 RVA: 0x000CED42 File Offset: 0x000CCF42
		private void AddTextRun(RunTextType textType, int runStart, int runEnd)
		{
			if (!this.unwrapFlowed)
			{
				this.tokenBuilder.AddTextRun(textType, runStart, runEnd);
				return;
			}
			this.AddTextRunUnwrap(textType, runStart, runEnd);
		}

		// Token: 0x06001A10 RID: 6672 RVA: 0x000CED64 File Offset: 0x000CCF64
		private void AddTextRunUnwrap(RunTextType textType, int runStart, int runEnd)
		{
			if (textType <= RunTextType.Tabulation)
			{
				if (textType != RunTextType.Space)
				{
					if (textType == RunTextType.NewLine)
					{
						if (!this.lastSpace || (this.signaturePossible && this.lineCount == 3))
						{
							this.lastLineFlowed = false;
							this.tokenBuilder.AddTextRun(textType, runStart, runEnd);
						}
						else
						{
							this.lastLineFlowed = true;
						}
						this.lineCount = 0;
						this.lastSpace = false;
						this.signaturePossible = true;
						this.quotingExpected = true;
						this.lastLineQuotingLevel = this.quotingLevel;
						this.quotingLevel = 0;
						return;
					}
					if (textType != RunTextType.Tabulation)
					{
						return;
					}
					if (this.quotingExpected)
					{
						if (this.lastLineQuotingLevel != this.quotingLevel)
						{
							if (this.lastLineFlowed)
							{
								this.tokenBuilder.AddLiteralTextRun(RunTextType.NewLine, runStart, runStart, 10);
							}
							this.tokenBuilder.AddSpecialRun(TextRunKind.QuotingLevel, runStart, this.quotingLevel);
							this.lastLineQuotingLevel = this.quotingLevel;
						}
						this.quotingExpected = false;
					}
					this.tokenBuilder.AddTextRun(textType, runStart, runEnd);
					this.lineCount += runEnd - runStart;
					this.lastSpace = false;
					this.signaturePossible = false;
					return;
				}
			}
			else if (textType != RunTextType.UnusualWhitespace)
			{
				if (textType != RunTextType.Nbsp && textType != RunTextType.NonSpace)
				{
					return;
				}
				if (this.quotingExpected)
				{
					while (runStart != runEnd && this.parseBuffer[runStart] == '>')
					{
						this.quotingLevel++;
						runStart++;
					}
					this.tokenBuilder.SkipRunIfNecessary(runStart, RunKind.Text);
					if (runStart != runEnd)
					{
						if (this.lastLineQuotingLevel != this.quotingLevel)
						{
							if (this.lastLineFlowed)
							{
								this.tokenBuilder.AddLiteralTextRun(RunTextType.NewLine, runStart, runStart, 10);
							}
							this.tokenBuilder.AddSpecialRun(TextRunKind.QuotingLevel, runStart, this.quotingLevel);
							this.lastLineQuotingLevel = this.quotingLevel;
						}
						this.quotingExpected = false;
					}
				}
				if (runStart == runEnd)
				{
					return;
				}
				this.tokenBuilder.AddTextRun(textType, runStart, runEnd);
				this.lineCount += runEnd - runStart;
				this.lastSpace = false;
				if (this.lineCount > 2 || this.parseBuffer[runStart] != '-' || (runEnd - runStart == 2 && this.parseBuffer[runStart + 1] != '-'))
				{
					this.signaturePossible = false;
					return;
				}
				return;
			}
			if (this.quotingExpected)
			{
				runStart++;
				this.tokenBuilder.SkipRunIfNecessary(runStart, RunKind.Text);
				if (this.lastLineQuotingLevel != this.quotingLevel)
				{
					if (this.lastLineFlowed)
					{
						this.tokenBuilder.AddLiteralTextRun(RunTextType.NewLine, runStart, runStart, 10);
					}
					this.tokenBuilder.AddSpecialRun(TextRunKind.QuotingLevel, runStart, this.quotingLevel);
					this.lastLineQuotingLevel = this.quotingLevel;
				}
				this.quotingExpected = false;
			}
			if (runStart != runEnd)
			{
				this.lineCount += runEnd - runStart;
				this.lastSpace = true;
				this.tokenBuilder.AddTextRun(textType, runStart, runEnd);
				if (this.lineCount != 3 || runEnd - runStart != 1)
				{
					this.signaturePossible = false;
					return;
				}
			}
		}

		// Token: 0x04001F46 RID: 8006
		protected ConverterInput input;

		// Token: 0x04001F47 RID: 8007
		protected bool endOfFile;

		// Token: 0x04001F48 RID: 8008
		protected char[] parseBuffer;

		// Token: 0x04001F49 RID: 8009
		protected int parseStart;

		// Token: 0x04001F4A RID: 8010
		protected int parseCurrent;

		// Token: 0x04001F4B RID: 8011
		protected int parseEnd;

		// Token: 0x04001F4C RID: 8012
		protected int parseThreshold = 1;

		// Token: 0x04001F4D RID: 8013
		protected TextTokenBuilder tokenBuilder;

		// Token: 0x04001F4E RID: 8014
		protected TextToken token;

		// Token: 0x04001F4F RID: 8015
		protected bool unwrapFlowed;

		// Token: 0x04001F50 RID: 8016
		protected bool unwrapDelSpace;

		// Token: 0x04001F51 RID: 8017
		protected bool lastSpace;

		// Token: 0x04001F52 RID: 8018
		protected int lineCount;

		// Token: 0x04001F53 RID: 8019
		protected bool quotingExpected = true;

		// Token: 0x04001F54 RID: 8020
		protected int quotingLevel;

		// Token: 0x04001F55 RID: 8021
		protected int lastLineQuotingLevel;

		// Token: 0x04001F56 RID: 8022
		protected bool lastLineFlowed;

		// Token: 0x04001F57 RID: 8023
		protected bool signaturePossible = true;
	}
}
