using System;

namespace Microsoft.Exchange.Data.TextConverters.Internal.Css
{
	// Token: 0x020001A5 RID: 421
	internal class CssParser : IDisposable
	{
		// Token: 0x060011F1 RID: 4593 RVA: 0x0008109C File Offset: 0x0007F29C
		public CssParser(ConverterInput input, int maxRuns, bool testBoundaryConditions)
		{
			this.input = input;
			this.tokenBuilder = new CssTokenBuilder(null, 256, 256, maxRuns, testBoundaryConditions);
			this.token = this.tokenBuilder.Token;
		}

		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x060011F2 RID: 4594 RVA: 0x000810D4 File Offset: 0x0007F2D4
		public CssToken Token
		{
			get
			{
				return this.token;
			}
		}

		// Token: 0x060011F3 RID: 4595 RVA: 0x000810DC File Offset: 0x0007F2DC
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060011F4 RID: 4596 RVA: 0x000810EB File Offset: 0x0007F2EB
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && this.input != null)
			{
				((IDisposable)this.input).Dispose();
			}
			this.input = null;
			this.parseBuffer = null;
			this.token = null;
		}

		// Token: 0x060011F5 RID: 4597 RVA: 0x00081118 File Offset: 0x0007F318
		public void Reset()
		{
			this.endOfFile = false;
			this.parseBuffer = null;
			this.parseStart = 0;
			this.parseCurrent = 0;
			this.parseEnd = 0;
			this.ruleDepth = 0;
		}

		// Token: 0x060011F6 RID: 4598 RVA: 0x00081144 File Offset: 0x0007F344
		public void SetParseMode(CssParseMode parseMode)
		{
			this.parseMode = parseMode;
		}

		// Token: 0x060011F7 RID: 4599 RVA: 0x00081150 File Offset: 0x0007F350
		public CssTokenId Parse()
		{
			if (this.endOfFile)
			{
				return CssTokenId.EndOfFile;
			}
			this.tokenBuilder.Reset();
			char[] array = this.parseBuffer;
			int num = this.parseCurrent;
			int num2 = this.parseEnd;
			if (num >= num2)
			{
				this.input.ReadMore(ref this.parseBuffer, ref this.parseStart, ref this.parseCurrent, ref this.parseEnd);
				if (this.parseEnd == 0)
				{
					return CssTokenId.EndOfFile;
				}
				this.tokenBuilder.BufferChanged(this.parseBuffer, this.parseStart);
				array = this.parseBuffer;
				num = this.parseCurrent;
				num2 = this.parseEnd;
			}
			char ch = array[num];
			CharClass charClass = ParseSupport.GetCharClass(ch);
			int num3 = num;
			if (this.parseMode == CssParseMode.StyleTag)
			{
				this.ScanStyleSheet(ch, ref charClass, ref num);
				if (num3 >= num)
				{
					this.tokenBuilder.Reset();
					return CssTokenId.EndOfFile;
				}
				if (this.tokenBuilder.Incomplete)
				{
					this.tokenBuilder.EndRuleSet();
				}
			}
			else
			{
				this.ScanDeclarations(ch, ref charClass, ref num);
				if (num < num2)
				{
					this.endOfFile = true;
					this.tokenBuilder.Reset();
					return CssTokenId.EndOfFile;
				}
				if (this.tokenBuilder.Incomplete)
				{
					this.tokenBuilder.EndDeclarations();
				}
			}
			this.endOfFile = (num == num2);
			this.parseCurrent = num;
			return this.token.CssTokenId;
		}

		// Token: 0x060011F8 RID: 4600 RVA: 0x00081291 File Offset: 0x0007F491
		private static bool IsNameCharacterNoEscape(char ch, CharClass charClass)
		{
			return CssParser.IsNameStartCharacterNoEscape(ch, charClass) || ParseSupport.NumericCharacter(charClass) || ch == '-';
		}

		// Token: 0x060011F9 RID: 4601 RVA: 0x000812AB File Offset: 0x0007F4AB
		private static bool IsNameStartCharacterNoEscape(char ch, CharClass charClass)
		{
			return ParseSupport.AlphaCharacter(charClass) || ch == '_' || ch > '\u007f';
		}

		// Token: 0x060011FA RID: 4602 RVA: 0x000812C1 File Offset: 0x0007F4C1
		private static bool IsUrlCharacterNoEscape(char ch, CharClass charClass)
		{
			return (ch >= '*' && ch != '\u007f') || (ch >= '#' && ch <= '&') || ch == '!';
		}

		// Token: 0x060011FB RID: 4603 RVA: 0x000812E0 File Offset: 0x0007F4E0
		private char ScanStyleSheet(char ch, ref CharClass charClass, ref int parseCurrent)
		{
			int num = this.parseEnd;
			char[] array = this.parseBuffer;
			for (;;)
			{
				int num2 = parseCurrent;
				ch = this.ScanWhitespace(ch, ref charClass, ref parseCurrent, true);
				if (parseCurrent == num)
				{
					break;
				}
				if (this.IsNameStartCharacter(ch, charClass, parseCurrent) || ch == '*' || ch == '.' || ch == ':' || ch == '#' || ch == '[')
				{
					ch = this.ScanRuleSet(ch, ref charClass, ref parseCurrent);
					if (parseCurrent == num)
					{
						return ch;
					}
					if (!this.isInvalid)
					{
						return ch;
					}
				}
				else if (ch == '@')
				{
					ch = this.ScanAtRule(ch, ref charClass, ref parseCurrent);
					if (parseCurrent == num)
					{
						return ch;
					}
					if (!this.isInvalid)
					{
						return ch;
					}
				}
				else if (ch == '/' && parseCurrent < num && array[parseCurrent + 1] == '*')
				{
					ch = this.ScanComment(ch, ref charClass, ref parseCurrent);
					if (parseCurrent == num)
					{
						return ch;
					}
				}
				else if (ch == '<')
				{
					ch = this.ScanCdo(ch, ref charClass, ref parseCurrent);
					if (parseCurrent == num)
					{
						return ch;
					}
				}
				else if (ch == '-')
				{
					ch = this.ScanCdc(ch, ref charClass, ref parseCurrent);
					if (parseCurrent == num)
					{
						return ch;
					}
				}
				else
				{
					this.isInvalid = true;
				}
				if (this.isInvalid)
				{
					this.isInvalid = false;
					this.tokenBuilder.Reset();
					ch = this.SkipToNextRule(ch, ref charClass, ref parseCurrent);
					if (parseCurrent == num)
					{
						return ch;
					}
				}
				if (num2 >= parseCurrent)
				{
					return ch;
				}
			}
			return ch;
		}

		// Token: 0x060011FC RID: 4604 RVA: 0x00081404 File Offset: 0x0007F604
		private char ScanCdo(char ch, ref CharClass charClass, ref int parseCurrent)
		{
			parseCurrent++;
			if (parseCurrent + 3 >= this.parseEnd)
			{
				parseCurrent = this.parseEnd;
				return ch;
			}
			if (this.parseBuffer[parseCurrent++] != '!' || this.parseBuffer[parseCurrent++] != '-' || this.parseBuffer[parseCurrent++] != '-')
			{
				return this.SkipToNextRule(ch, ref charClass, ref parseCurrent);
			}
			ch = this.parseBuffer[parseCurrent];
			charClass = ParseSupport.GetCharClass(ch);
			return ch;
		}

		// Token: 0x060011FD RID: 4605 RVA: 0x00081488 File Offset: 0x0007F688
		private char ScanCdc(char ch, ref CharClass charClass, ref int parseCurrent)
		{
			parseCurrent++;
			if (parseCurrent + 2 >= this.parseEnd)
			{
				parseCurrent = this.parseEnd;
				return ch;
			}
			if (this.parseBuffer[parseCurrent++] != '-' || this.parseBuffer[parseCurrent++] != '>')
			{
				return this.SkipToNextRule(ch, ref charClass, ref parseCurrent);
			}
			ch = this.parseBuffer[parseCurrent];
			charClass = ParseSupport.GetCharClass(ch);
			return ch;
		}

		// Token: 0x060011FE RID: 4606 RVA: 0x000814F8 File Offset: 0x0007F6F8
		private char ScanAtRule(char ch, ref CharClass charClass, ref int parseCurrent)
		{
			int num = this.parseEnd;
			char[] array = this.parseBuffer;
			int num2 = parseCurrent;
			ch = array[++parseCurrent];
			charClass = ParseSupport.GetCharClass(ch);
			if (!this.IsIdentStartCharacter(ch, charClass, parseCurrent))
			{
				this.isInvalid = true;
				return ch;
			}
			this.tokenBuilder.StartRuleSet(num2, CssTokenId.AtRule);
			if (!this.tokenBuilder.CanAddSelector())
			{
				parseCurrent = num;
				return ch;
			}
			this.tokenBuilder.StartSelectorName();
			this.PrepareAndAddRun(CssRunKind.AtRuleName, num2, ref parseCurrent);
			if (parseCurrent == num)
			{
				return ch;
			}
			int nameLength;
			ch = this.ScanIdent(CssRunKind.AtRuleName, ch, ref charClass, ref parseCurrent, out nameLength);
			this.tokenBuilder.EndSelectorName(nameLength);
			if (parseCurrent == num)
			{
				return ch;
			}
			if (this.IsNameEqual("page", num2 + 1, parseCurrent - num2 - 1))
			{
				ch = this.ScanPageSelector(ch, ref charClass, ref parseCurrent);
				if (parseCurrent == num)
				{
					return ch;
				}
			}
			else if (!this.IsNameEqual("font-face", num2 + 1, parseCurrent - num2 - 1))
			{
				this.isInvalid = true;
				return ch;
			}
			this.tokenBuilder.EndSimpleSelector();
			ch = this.ScanDeclarationBlock(ch, ref charClass, ref parseCurrent);
			if (parseCurrent == num)
			{
				return ch;
			}
			return ch;
		}

		// Token: 0x060011FF RID: 4607 RVA: 0x0008160C File Offset: 0x0007F80C
		private char ScanPageSelector(char ch, ref CharClass charClass, ref int parseCurrent)
		{
			ch = this.ScanWhitespace(ch, ref charClass, ref parseCurrent, false);
			if (parseCurrent == this.parseEnd)
			{
				return ch;
			}
			if (this.IsIdentStartCharacter(ch, charClass, parseCurrent))
			{
				this.tokenBuilder.EndSimpleSelector();
				this.tokenBuilder.StartSelectorName();
				int nameLength;
				ch = this.ScanIdent(CssRunKind.PageIdent, ch, ref charClass, ref parseCurrent, out nameLength);
				this.tokenBuilder.EndSelectorName(nameLength);
				if (parseCurrent == this.parseEnd)
				{
					return ch;
				}
				this.tokenBuilder.SetSelectorCombinator(CssSelectorCombinator.Descendant, false);
			}
			if (ch == ':')
			{
				ch = this.parseBuffer[++parseCurrent];
				charClass = ParseSupport.GetCharClass(ch);
				this.PrepareAndAddRun(CssRunKind.PagePseudoStart, parseCurrent - 1, ref parseCurrent);
				if (parseCurrent == this.parseEnd)
				{
					return ch;
				}
				if (!this.IsIdentStartCharacter(ch, charClass, parseCurrent))
				{
					this.tokenBuilder.InvalidateLastValidRun(CssRunKind.SelectorPseudoStart);
					return ch;
				}
				this.tokenBuilder.StartSelectorClass(CssSelectorClassType.Pseudo);
				int num;
				ch = this.ScanIdent(CssRunKind.PagePseudo, ch, ref charClass, ref parseCurrent, out num);
				this.tokenBuilder.EndSelectorClass();
				if (parseCurrent == this.parseEnd)
				{
					return ch;
				}
			}
			ch = this.ScanWhitespace(ch, ref charClass, ref parseCurrent, false);
			if (parseCurrent == this.parseEnd)
			{
				return ch;
			}
			return ch;
		}

		// Token: 0x06001200 RID: 4608 RVA: 0x00081738 File Offset: 0x0007F938
		private char ScanRuleSet(char ch, ref CharClass charClass, ref int parseCurrent)
		{
			this.tokenBuilder.StartRuleSet(parseCurrent, CssTokenId.RuleSet);
			ch = this.ScanSelectors(ch, ref charClass, ref parseCurrent);
			if (parseCurrent == this.parseEnd || this.isInvalid)
			{
				return ch;
			}
			ch = this.ScanDeclarationBlock(ch, ref charClass, ref parseCurrent);
			if (parseCurrent == this.parseEnd)
			{
				return ch;
			}
			return ch;
		}

		// Token: 0x06001201 RID: 4609 RVA: 0x0008178C File Offset: 0x0007F98C
		private char ScanDeclarationBlock(char ch, ref CharClass charClass, ref int parseCurrent)
		{
			ch = this.ScanWhitespace(ch, ref charClass, ref parseCurrent, false);
			if (parseCurrent == this.parseEnd)
			{
				return ch;
			}
			if (ch != '{')
			{
				this.isInvalid = true;
				return ch;
			}
			this.ruleDepth++;
			ch = this.parseBuffer[++parseCurrent];
			charClass = ParseSupport.GetCharClass(ch);
			this.PrepareAndAddRun(CssRunKind.Delimiter, parseCurrent - 1, ref parseCurrent);
			if (parseCurrent == this.parseEnd)
			{
				return ch;
			}
			ch = this.ScanWhitespace(ch, ref charClass, ref parseCurrent, true);
			if (parseCurrent == this.parseEnd)
			{
				return ch;
			}
			ch = this.ScanDeclarations(ch, ref charClass, ref parseCurrent);
			if (parseCurrent == this.parseEnd)
			{
				return ch;
			}
			if (ch != '}')
			{
				this.isInvalid = true;
				return ch;
			}
			this.ruleDepth--;
			ch = this.parseBuffer[++parseCurrent];
			charClass = ParseSupport.GetCharClass(ch);
			this.PrepareAndAddRun(CssRunKind.Delimiter, parseCurrent - 1, ref parseCurrent);
			if (parseCurrent == this.parseEnd)
			{
				return ch;
			}
			return ch;
		}

		// Token: 0x06001202 RID: 4610 RVA: 0x00081888 File Offset: 0x0007FA88
		private char ScanSelectors(char ch, ref CharClass charClass, ref int parseCurrent)
		{
			int num = this.parseEnd;
			char[] array = this.parseBuffer;
			int i = parseCurrent;
			ch = this.ScanSimpleSelector(ch, ref charClass, ref parseCurrent);
			if (parseCurrent == num || this.isInvalid)
			{
				return ch;
			}
			while (i < parseCurrent)
			{
				CssSelectorCombinator combinator = CssSelectorCombinator.None;
				bool flag = false;
				bool flag2 = false;
				i = parseCurrent;
				ch = this.ScanWhitespace(ch, ref charClass, ref parseCurrent, false);
				if (parseCurrent == num)
				{
					return ch;
				}
				if (i < parseCurrent)
				{
					flag = true;
					combinator = CssSelectorCombinator.Descendant;
				}
				if (ch == '+' || ch == '>' || ch == ',')
				{
					combinator = ((ch == '+') ? CssSelectorCombinator.Adjacent : ((ch == '>') ? CssSelectorCombinator.Child : CssSelectorCombinator.None));
					ch = array[++parseCurrent];
					charClass = ParseSupport.GetCharClass(ch);
					if (parseCurrent == num)
					{
						return ch;
					}
					this.PrepareAndAddRun(CssRunKind.SelectorCombinatorOrComma, parseCurrent - 1, ref parseCurrent);
					if (parseCurrent == num)
					{
						return ch;
					}
					flag2 = true;
					ch = this.ScanWhitespace(ch, ref charClass, ref parseCurrent, true);
					if (parseCurrent == num)
					{
						return ch;
					}
				}
				else if (i == parseCurrent)
				{
					break;
				}
				i = parseCurrent;
				ch = this.ScanSimpleSelector(ch, ref charClass, ref parseCurrent);
				if (i == parseCurrent)
				{
					if (flag2)
					{
						this.tokenBuilder.InvalidateLastValidRun(CssRunKind.SelectorCombinatorOrComma);
					}
					if (flag)
					{
						this.tokenBuilder.InvalidateLastValidRun(CssRunKind.Space);
						break;
					}
					break;
				}
				else
				{
					if (this.isInvalid)
					{
						return ch;
					}
					this.tokenBuilder.SetSelectorCombinator(combinator, true);
					if (parseCurrent == num)
					{
						return ch;
					}
				}
			}
			return ch;
		}

		// Token: 0x06001203 RID: 4611 RVA: 0x000819C0 File Offset: 0x0007FBC0
		private char ScanSimpleSelector(char ch, ref CharClass charClass, ref int parseCurrent)
		{
			int num = this.parseEnd;
			char[] array = this.parseBuffer;
			if (ch == '.' || ch == ':' || ch == '#' || ch == '[')
			{
				if (!this.tokenBuilder.CanAddSelector())
				{
					parseCurrent = num;
					return ch;
				}
				this.tokenBuilder.BuildUniversalSelector();
			}
			else
			{
				if (!this.IsIdentStartCharacter(ch, charClass, parseCurrent) && ch != '*')
				{
					return ch;
				}
				if (!this.tokenBuilder.CanAddSelector())
				{
					parseCurrent = num;
					return ch;
				}
				this.tokenBuilder.StartSelectorName();
				int nameLength;
				if (ch == '*')
				{
					nameLength = 1;
					ch = array[++parseCurrent];
					charClass = ParseSupport.GetCharClass(ch);
					this.PrepareAndAddRun(CssRunKind.SelectorName, parseCurrent - 1, ref parseCurrent);
				}
				else
				{
					ch = this.ScanIdent(CssRunKind.SelectorName, ch, ref charClass, ref parseCurrent, out nameLength);
				}
				this.tokenBuilder.EndSelectorName(nameLength);
				if (parseCurrent == num)
				{
					return ch;
				}
			}
			ch = this.ScanSelectorSuffix(ch, ref charClass, ref parseCurrent);
			this.tokenBuilder.EndSimpleSelector();
			return ch;
		}

		// Token: 0x06001204 RID: 4612 RVA: 0x00081AAC File Offset: 0x0007FCAC
		private char ScanSelectorSuffix(char ch, ref CharClass charClass, ref int parseCurrent)
		{
			if (ch == '[')
			{
				this.tokenBuilder.StartSelectorClass(CssSelectorClassType.Attrib);
				ch = this.ScanSelectorAttrib(ch, ref charClass, ref parseCurrent);
				this.tokenBuilder.EndSelectorClass();
				return ch;
			}
			int num = this.parseEnd;
			char[] array = this.parseBuffer;
			if (ch != ':')
			{
				if (ch == '.' || ch == '#')
				{
					bool flag = ch == '.';
					ch = array[++parseCurrent];
					charClass = ParseSupport.GetCharClass(ch);
					if (this.IsNameCharacter(ch, charClass, parseCurrent) && (!flag || this.IsIdentStartCharacter(ch, charClass, parseCurrent)))
					{
						this.PrepareAndAddRun(flag ? CssRunKind.SelectorClassStart : CssRunKind.SelectorHashStart, parseCurrent - 1, ref parseCurrent);
						if (parseCurrent == num)
						{
							return ch;
						}
						this.tokenBuilder.StartSelectorClass(flag ? CssSelectorClassType.Regular : CssSelectorClassType.Hash);
						ch = this.ScanName(flag ? CssRunKind.SelectorClass : CssRunKind.SelectorHash, ch, ref charClass, ref parseCurrent);
						this.tokenBuilder.EndSelectorClass();
						if (parseCurrent == num)
						{
							return ch;
						}
					}
					else
					{
						this.PrepareAndAddInvalidRun(CssRunKind.FunctionStart, ref parseCurrent);
					}
				}
				return ch;
			}
			ch = array[++parseCurrent];
			charClass = ParseSupport.GetCharClass(ch);
			this.PrepareAndAddRun(CssRunKind.SelectorPseudoStart, parseCurrent - 1, ref parseCurrent);
			if (parseCurrent == num)
			{
				return ch;
			}
			this.tokenBuilder.StartSelectorClass(CssSelectorClassType.Pseudo);
			ch = this.ScanSelectorPseudo(ch, ref charClass, ref parseCurrent);
			this.tokenBuilder.EndSelectorClass();
			return ch;
		}

		// Token: 0x06001205 RID: 4613 RVA: 0x00081BF8 File Offset: 0x0007FDF8
		private char ScanSelectorPseudo(char ch, ref CharClass charClass, ref int parseCurrent)
		{
			int num = this.parseEnd;
			char[] array = this.parseBuffer;
			if (!this.IsIdentStartCharacter(ch, charClass, parseCurrent))
			{
				this.tokenBuilder.InvalidateLastValidRun(CssRunKind.SelectorPseudoStart);
				return ch;
			}
			int start = parseCurrent;
			int num2;
			ch = this.ScanIdent(CssRunKind.SelectorPseudo, ch, ref charClass, ref parseCurrent, out num2);
			if (parseCurrent == num)
			{
				return ch;
			}
			if (ch == '(')
			{
				if (!this.IsSafeIdentifier(CssParser.SafePseudoFunctions, start, parseCurrent))
				{
					return ch;
				}
				ch = array[++parseCurrent];
				charClass = ParseSupport.GetCharClass(ch);
				if (parseCurrent == num)
				{
					return ch;
				}
				this.PrepareAndAddRun(CssRunKind.FunctionStart, parseCurrent - 1, ref parseCurrent);
				if (parseCurrent == num)
				{
					return ch;
				}
				ch = this.ScanWhitespace(ch, ref charClass, ref parseCurrent, true);
				if (parseCurrent == num)
				{
					return ch;
				}
				if (!this.IsIdentStartCharacter(ch, charClass, parseCurrent))
				{
					this.isInvalid = true;
					return ch;
				}
				ch = this.ScanIdent(CssRunKind.SelectorPseudoArg, ch, ref charClass, ref parseCurrent, out num2);
				if (parseCurrent == num)
				{
					return ch;
				}
				ch = this.ScanWhitespace(ch, ref charClass, ref parseCurrent, true);
				if (parseCurrent == num)
				{
					return ch;
				}
				if (ch != ')')
				{
					this.isInvalid = true;
					return ch;
				}
				ch = array[++parseCurrent];
				charClass = ParseSupport.GetCharClass(ch);
				this.PrepareAndAddRun(CssRunKind.FunctionEnd, parseCurrent - 1, ref parseCurrent);
				if (parseCurrent == num)
				{
					return ch;
				}
			}
			return ch;
		}

		// Token: 0x06001206 RID: 4614 RVA: 0x00081D30 File Offset: 0x0007FF30
		private char ScanSelectorAttrib(char ch, ref CharClass charClass, ref int parseCurrent)
		{
			int num = this.parseEnd;
			char[] array = this.parseBuffer;
			ch = array[++parseCurrent];
			charClass = ParseSupport.GetCharClass(ch);
			this.PrepareAndAddRun(CssRunKind.SelectorAttribStart, parseCurrent - 1, ref parseCurrent);
			if (parseCurrent == num)
			{
				return ch;
			}
			ch = this.ScanWhitespace(ch, ref charClass, ref parseCurrent, true);
			if (parseCurrent == num)
			{
				return ch;
			}
			if (!this.IsIdentStartCharacter(ch, charClass, parseCurrent))
			{
				this.isInvalid = true;
				return ch;
			}
			int num2;
			ch = this.ScanIdent(CssRunKind.SelectorAttribName, ch, ref charClass, ref parseCurrent, out num2);
			if (parseCurrent == num)
			{
				return ch;
			}
			ch = this.ScanWhitespace(ch, ref charClass, ref parseCurrent, true);
			if (parseCurrent == num)
			{
				return ch;
			}
			int num3 = parseCurrent;
			if (ch == '=')
			{
				ch = array[++parseCurrent];
				charClass = ParseSupport.GetCharClass(ch);
				this.PrepareAndAddRun(CssRunKind.SelectorAttribEquals, parseCurrent - 1, ref parseCurrent);
				if (parseCurrent == num)
				{
					return ch;
				}
			}
			else if ((ch == '~' || ch == '|') && array[parseCurrent + 1] == '=')
			{
				parseCurrent += 2;
				this.PrepareAndAddRun((ch == '~') ? CssRunKind.SelectorAttribIncludes : CssRunKind.SelectorAttribDashmatch, parseCurrent - 2, ref parseCurrent);
				if (parseCurrent == num)
				{
					return ch;
				}
				ch = array[parseCurrent];
				charClass = ParseSupport.GetCharClass(ch);
			}
			if (num3 < parseCurrent)
			{
				ch = this.ScanWhitespace(ch, ref charClass, ref parseCurrent, true);
				if (parseCurrent == num)
				{
					return ch;
				}
				if (this.IsIdentStartCharacter(ch, charClass, parseCurrent))
				{
					num3 = parseCurrent;
					ch = this.ScanIdent(CssRunKind.SelectorAttribIdentifier, ch, ref charClass, ref parseCurrent, out num2);
					if (parseCurrent == num)
					{
						return ch;
					}
				}
				else if (ch == '"' || ch == '\'')
				{
					num3 = parseCurrent;
					ch = this.ScanString(ch, ref charClass, ref parseCurrent, false);
					if (this.isInvalid)
					{
						return ch;
					}
					this.PrepareAndAddRun(CssRunKind.SelectorAttribString, num3, ref parseCurrent);
					if (parseCurrent == num)
					{
						return ch;
					}
				}
				ch = this.ScanWhitespace(ch, ref charClass, ref parseCurrent, true);
				if (parseCurrent == num)
				{
					return ch;
				}
			}
			if (ch != ']')
			{
				this.isInvalid = true;
				return ch;
			}
			ch = array[++parseCurrent];
			charClass = ParseSupport.GetCharClass(ch);
			this.PrepareAndAddRun(CssRunKind.SelectorAttribEnd, parseCurrent - 1, ref parseCurrent);
			if (parseCurrent == num)
			{
				return ch;
			}
			return ch;
		}

		// Token: 0x06001207 RID: 4615 RVA: 0x00081F1C File Offset: 0x0008011C
		private char ScanDeclarations(char ch, ref CharClass charClass, ref int parseCurrent)
		{
			int num = this.parseEnd;
			char[] array = this.parseBuffer;
			this.tokenBuilder.StartDeclarations(parseCurrent);
			for (;;)
			{
				int num2 = parseCurrent;
				ch = this.ScanWhitespace(ch, ref charClass, ref parseCurrent, true);
				if (parseCurrent == num)
				{
					break;
				}
				if (this.IsIdentStartCharacter(ch, charClass, parseCurrent))
				{
					if (!this.tokenBuilder.CanAddProperty())
					{
						goto Block_3;
					}
					this.tokenBuilder.StartPropertyName();
					int nameLength;
					ch = this.ScanIdent(CssRunKind.PropertyName, ch, ref charClass, ref parseCurrent, out nameLength);
					this.tokenBuilder.EndPropertyName(nameLength);
					if (parseCurrent == num)
					{
						return ch;
					}
					ch = this.ScanWhitespace(ch, ref charClass, ref parseCurrent, true);
					if (parseCurrent == num)
					{
						return ch;
					}
					if (ch != ':')
					{
						goto Block_6;
					}
					ch = array[++parseCurrent];
					charClass = ParseSupport.GetCharClass(ch);
					this.PrepareAndAddRun(CssRunKind.PropertyColon, parseCurrent - 1, ref parseCurrent);
					if (parseCurrent == num)
					{
						return ch;
					}
					ch = this.ScanWhitespace(ch, ref charClass, ref parseCurrent, true);
					if (parseCurrent == num)
					{
						return ch;
					}
					this.tokenBuilder.StartPropertyValue();
					ch = this.ScanPropertyValue(ch, ref charClass, ref parseCurrent);
					if (parseCurrent == num)
					{
						return ch;
					}
					this.tokenBuilder.EndPropertyValue();
					this.tokenBuilder.EndProperty();
					ch = this.ScanWhitespace(ch, ref charClass, ref parseCurrent, true);
					if (parseCurrent == num)
					{
						return ch;
					}
				}
				if (ch != ';')
				{
					goto Block_11;
				}
				ch = array[++parseCurrent];
				charClass = ParseSupport.GetCharClass(ch);
				this.PrepareAndAddRun(CssRunKind.Delimiter, parseCurrent - 1, ref parseCurrent);
				if (parseCurrent == num)
				{
					return ch;
				}
				if (num2 >= parseCurrent)
				{
					return ch;
				}
			}
			return ch;
			Block_3:
			parseCurrent = num;
			return ch;
			Block_6:
			this.tokenBuilder.MarkPropertyAsDeleted();
			return ch;
			Block_11:
			this.tokenBuilder.EndDeclarations();
			return ch;
		}

		// Token: 0x06001208 RID: 4616 RVA: 0x000820A4 File Offset: 0x000802A4
		private char ScanPropertyValue(char ch, ref CharClass charClass, ref int parseCurrent)
		{
			int num = this.parseEnd;
			char[] array = this.parseBuffer;
			ch = this.ScanExpr(ch, ref charClass, ref parseCurrent, 0);
			if (parseCurrent == num)
			{
				return ch;
			}
			if (ch == '!')
			{
				ch = array[++parseCurrent];
				charClass = ParseSupport.GetCharClass(ch);
				if (parseCurrent == num)
				{
					this.tokenBuilder.MarkPropertyAsDeleted();
					return ch;
				}
				this.PrepareAndAddRun(CssRunKind.ImportantStart, parseCurrent - 1, ref parseCurrent);
				if (parseCurrent == num)
				{
					return ch;
				}
				ch = this.ScanWhitespace(ch, ref charClass, ref parseCurrent, true);
				if (parseCurrent == num)
				{
					this.tokenBuilder.MarkPropertyAsDeleted();
					return ch;
				}
				if (!this.IsNameStartCharacter(ch, charClass, parseCurrent))
				{
					this.tokenBuilder.MarkPropertyAsDeleted();
					return ch;
				}
				int num2 = parseCurrent;
				ch = this.ScanName(CssRunKind.Important, ch, ref charClass, ref parseCurrent);
				if (parseCurrent == num)
				{
					return ch;
				}
				if (!this.IsNameEqual("important", num2, parseCurrent - num2))
				{
					this.tokenBuilder.MarkPropertyAsDeleted();
					return ch;
				}
			}
			return ch;
		}

		// Token: 0x06001209 RID: 4617 RVA: 0x0008218C File Offset: 0x0008038C
		private char ScanExpr(char ch, ref CharClass charClass, ref int parseCurrent, int level)
		{
			int num = this.parseEnd;
			char[] array = this.parseBuffer;
			int i = parseCurrent;
			ch = this.ScanTerm(ch, ref charClass, ref parseCurrent, level);
			if (parseCurrent == num)
			{
				return ch;
			}
			while (i < parseCurrent)
			{
				bool flag = false;
				bool flag2 = false;
				i = parseCurrent;
				ch = this.ScanWhitespace(ch, ref charClass, ref parseCurrent, false);
				if (parseCurrent == num)
				{
					return ch;
				}
				if (i < parseCurrent)
				{
					flag = true;
				}
				if (ch == '/' || ch == ',')
				{
					ch = array[++parseCurrent];
					charClass = ParseSupport.GetCharClass(ch);
					if (parseCurrent == num)
					{
						return ch;
					}
					this.PrepareAndAddRun(CssRunKind.Operator, parseCurrent - 1, ref parseCurrent);
					if (parseCurrent == num)
					{
						return ch;
					}
					flag2 = true;
					ch = this.ScanWhitespace(ch, ref charClass, ref parseCurrent, true);
					if (parseCurrent == num)
					{
						return ch;
					}
				}
				else if (i == parseCurrent)
				{
					break;
				}
				i = parseCurrent;
				ch = this.ScanTerm(ch, ref charClass, ref parseCurrent, level);
				if (parseCurrent == num)
				{
					return ch;
				}
				if (i == parseCurrent)
				{
					if (flag2)
					{
						this.tokenBuilder.InvalidateLastValidRun(CssRunKind.Operator);
					}
					if (flag)
					{
						this.tokenBuilder.InvalidateLastValidRun(CssRunKind.Space);
						break;
					}
					break;
				}
			}
			return ch;
		}

		// Token: 0x0600120A RID: 4618 RVA: 0x0008228C File Offset: 0x0008048C
		private char ScanTerm(char ch, ref CharClass charClass, ref int parseCurrent, int level)
		{
			int num = this.parseEnd;
			char[] array = this.parseBuffer;
			bool flag = false;
			if (ch == '-' || ch == '+')
			{
				ch = array[++parseCurrent];
				charClass = ParseSupport.GetCharClass(ch);
				if (parseCurrent == num)
				{
					this.tokenBuilder.MarkPropertyAsDeleted();
					return ch;
				}
				this.PrepareAndAddRun(CssRunKind.UnaryOperator, parseCurrent - 1, ref parseCurrent);
				if (parseCurrent == num)
				{
					return ch;
				}
				flag = true;
			}
			if (ParseSupport.NumericCharacter(charClass) || ch == '.')
			{
				ch = this.ScanNumeric(ch, ref charClass, ref parseCurrent);
				if (parseCurrent == num)
				{
					return ch;
				}
				if (ch == '.')
				{
					ch = array[++parseCurrent];
					charClass = ParseSupport.GetCharClass(ch);
					this.PrepareAndAddRun(CssRunKind.Dot, parseCurrent - 1, ref parseCurrent);
					if (parseCurrent == num)
					{
						return ch;
					}
					int num2 = parseCurrent;
					ch = this.ScanNumeric(ch, ref charClass, ref parseCurrent);
					if (parseCurrent == num)
					{
						return ch;
					}
					if (num2 == parseCurrent)
					{
						this.tokenBuilder.MarkPropertyAsDeleted();
					}
				}
				if (ch == '%')
				{
					ch = array[++parseCurrent];
					charClass = ParseSupport.GetCharClass(ch);
					this.PrepareAndAddRun(CssRunKind.Percent, parseCurrent - 1, ref parseCurrent);
					if (parseCurrent == num)
					{
						return ch;
					}
				}
				else if (this.IsIdentStartCharacter(ch, charClass, parseCurrent))
				{
					int num3;
					ch = this.ScanIdent(CssRunKind.Metrics, ch, ref charClass, ref parseCurrent, out num3);
					if (parseCurrent == num)
					{
						return ch;
					}
				}
			}
			else if (this.IsIdentStartCharacter(ch, charClass, parseCurrent))
			{
				int num2 = parseCurrent;
				int num4;
				ch = this.ScanIdent(CssRunKind.TermIdentifier, ch, ref charClass, ref parseCurrent, out num4);
				if (parseCurrent == num)
				{
					return ch;
				}
				int start = parseCurrent;
				if (ch == '+' && num2 + 1 == parseCurrent && (array[num2] == 'u' || array[num2] == 'U'))
				{
					ch = this.ScanUnicodeRange(ch, ref charClass, ref parseCurrent);
					this.PrepareAndAddRun(CssRunKind.UnicodeRange, start, ref parseCurrent);
					if (parseCurrent == num)
					{
						return ch;
					}
				}
				else if (ch == '(')
				{
					bool flag2 = false;
					if (!this.IsSafeIdentifier(CssParser.SafeTermFunctions, num2, parseCurrent))
					{
						this.tokenBuilder.MarkPropertyAsDeleted();
						if (this.IsNameEqual("url", num2, parseCurrent - num2))
						{
							flag2 = true;
						}
					}
					ch = array[++parseCurrent];
					charClass = ParseSupport.GetCharClass(ch);
					if (parseCurrent == num)
					{
						this.tokenBuilder.MarkPropertyAsDeleted();
						return ch;
					}
					this.PrepareAndAddRun(CssRunKind.FunctionStart, parseCurrent - 1, ref parseCurrent);
					if (parseCurrent == num)
					{
						this.tokenBuilder.MarkPropertyAsDeleted();
						return ch;
					}
					ch = this.ScanWhitespace(ch, ref charClass, ref parseCurrent, true);
					if (parseCurrent == num)
					{
						this.tokenBuilder.MarkPropertyAsDeleted();
						return ch;
					}
					if (flag2)
					{
						if (ch == '"' || ch == '\'')
						{
							num2 = parseCurrent;
							ch = this.ScanString(ch, ref charClass, ref parseCurrent, true);
							if (this.isInvalid)
							{
								return ch;
							}
							this.PrepareAndAddRun(CssRunKind.String, num2, ref parseCurrent);
							if (parseCurrent == num)
							{
								return ch;
							}
						}
						else
						{
							num2 = parseCurrent;
							ch = this.ScanUrl(ch, ref charClass, ref parseCurrent);
							if (parseCurrent == num)
							{
								return ch;
							}
						}
						ch = this.ScanWhitespace(ch, ref charClass, ref parseCurrent, true);
						if (parseCurrent == num)
						{
							return ch;
						}
					}
					else
					{
						if (++level > 16)
						{
							this.tokenBuilder.MarkPropertyAsDeleted();
							return ch;
						}
						ch = this.ScanExpr(ch, ref charClass, ref parseCurrent, level);
						if (parseCurrent == num)
						{
							this.tokenBuilder.MarkPropertyAsDeleted();
							return ch;
						}
					}
					if (ch != ')')
					{
						this.tokenBuilder.MarkPropertyAsDeleted();
					}
					ch = array[++parseCurrent];
					charClass = ParseSupport.GetCharClass(ch);
					this.PrepareAndAddRun(CssRunKind.FunctionEnd, parseCurrent - 1, ref parseCurrent);
					if (parseCurrent == num)
					{
						return ch;
					}
				}
				else if (flag)
				{
					this.tokenBuilder.MarkPropertyAsDeleted();
				}
			}
			else if (flag)
			{
				this.tokenBuilder.MarkPropertyAsDeleted();
			}
			else if (ch == '#')
			{
				ch = array[++parseCurrent];
				charClass = ParseSupport.GetCharClass(ch);
				this.PrepareAndAddRun(CssRunKind.HexColorStart, parseCurrent - 1, ref parseCurrent);
				if (parseCurrent == num)
				{
					return ch;
				}
				if (this.IsNameCharacter(ch, charClass, parseCurrent))
				{
					ch = this.ScanName(CssRunKind.HexColor, ch, ref charClass, ref parseCurrent);
					if (parseCurrent == num)
					{
						return ch;
					}
				}
				else
				{
					this.tokenBuilder.MarkPropertyAsDeleted();
				}
			}
			else if (ch == '"' || ch == '\'')
			{
				int num2 = parseCurrent;
				ch = this.ScanString(ch, ref charClass, ref parseCurrent, true);
				if (this.isInvalid)
				{
					return ch;
				}
				this.PrepareAndAddRun(CssRunKind.String, num2, ref parseCurrent);
				if (parseCurrent == num)
				{
					return ch;
				}
			}
			return ch;
		}

		// Token: 0x0600120B RID: 4619 RVA: 0x00082688 File Offset: 0x00080888
		private char ScanNumeric(char ch, ref CharClass charClass, ref int parseCurrent)
		{
			int start = parseCurrent;
			char[] array = this.parseBuffer;
			while (ParseSupport.NumericCharacter(charClass))
			{
				ch = array[++parseCurrent];
				charClass = ParseSupport.GetCharClass(ch);
			}
			this.PrepareAndAddRun(CssRunKind.Numeric, start, ref parseCurrent);
			return ch;
		}

		// Token: 0x0600120C RID: 4620 RVA: 0x000826D0 File Offset: 0x000808D0
		private char ScanString(char ch, ref CharClass charClass, ref int parseCurrent, bool inProperty)
		{
			int num = this.parseEnd;
			char[] array = this.parseBuffer;
			char c = ch;
			char c2 = '\0';
			char c3 = '\0';
			bool flag = false;
			for (;;)
			{
				ch = array[++parseCurrent];
				if (parseCurrent == num)
				{
					break;
				}
				if (CssToken.AttemptUnescape(array, num, ref ch, ref parseCurrent))
				{
					flag = true;
					if (parseCurrent == num)
					{
						goto Block_4;
					}
					if (ch == c)
					{
						goto IL_BA;
					}
					c2 = '\0';
					c3 = '\0';
				}
				else
				{
					if (ch == c || (ch == '\n' && c2 == '\r' && c3 != '\\') || (((ch == '\n' && c2 != '\r') || ch == '\r' || ch == '\f') && c2 != '\\'))
					{
						goto IL_BA;
					}
					c3 = c2;
					c2 = ch;
				}
			}
			if (inProperty)
			{
				this.tokenBuilder.MarkPropertyAsDeleted();
			}
			charClass = ParseSupport.GetCharClass(ch);
			return ch;
			Block_4:
			if (inProperty)
			{
				this.tokenBuilder.MarkPropertyAsDeleted();
			}
			charClass = ParseSupport.GetCharClass(array[parseCurrent]);
			return array[parseCurrent];
			IL_BA:
			ch = array[++parseCurrent];
			charClass = ParseSupport.GetCharClass(ch);
			if (flag)
			{
				if (inProperty)
				{
					this.tokenBuilder.MarkPropertyAsDeleted();
				}
				else
				{
					this.isInvalid = true;
				}
			}
			return ch;
		}

		// Token: 0x0600120D RID: 4621 RVA: 0x000827CC File Offset: 0x000809CC
		private char ScanName(CssRunKind runKind, char ch, ref CharClass charClass, ref int parseCurrent)
		{
			for (;;)
			{
				int num = parseCurrent;
				while (CssParser.IsNameCharacterNoEscape(ch, ParseSupport.GetCharClass(ch)) && parseCurrent != this.parseEnd)
				{
					ch = this.parseBuffer[++parseCurrent];
				}
				if (parseCurrent != num)
				{
					this.PrepareAndAddRun(runKind, num, ref parseCurrent);
				}
				if (parseCurrent == this.parseEnd)
				{
					goto IL_C7;
				}
				num = parseCurrent;
				if (ch != '\\')
				{
					goto IL_C7;
				}
				if (!CssToken.AttemptUnescape(this.parseBuffer, this.parseEnd, ref ch, ref parseCurrent) || !CssParser.IsNameCharacterNoEscape(ch, ParseSupport.GetCharClass(ch)))
				{
					break;
				}
				parseCurrent++;
				this.PrepareAndAddLiteralRun(runKind, num, ref parseCurrent, (int)ch);
				if (parseCurrent == this.parseEnd)
				{
					goto IL_C7;
				}
				ch = this.parseBuffer[parseCurrent];
			}
			ch = this.parseBuffer[++parseCurrent];
			this.PrepareAndAddInvalidRun(runKind, ref parseCurrent);
			IL_C7:
			charClass = ParseSupport.GetCharClass(ch);
			return ch;
		}

		// Token: 0x0600120E RID: 4622 RVA: 0x000828AC File Offset: 0x00080AAC
		private char ScanIdent(CssRunKind runKind, char ch, ref CharClass charClass, ref int parseCurrent, out int nameLength)
		{
			bool flag = false;
			nameLength = 0;
			for (;;)
			{
				int num = parseCurrent;
				while (CssParser.IsNameCharacterNoEscape(ch, ParseSupport.GetCharClass(ch)))
				{
					if (nameLength == 0 && ch == '-')
					{
						flag = true;
					}
					if (nameLength == 1 && flag && char.IsDigit(ch))
					{
						goto Block_5;
					}
					nameLength++;
					if (parseCurrent == this.parseEnd)
					{
						break;
					}
					ch = this.parseBuffer[++parseCurrent];
				}
				if (parseCurrent != num)
				{
					this.PrepareAndAddRun(runKind, num, ref parseCurrent);
				}
				if (parseCurrent == this.parseEnd)
				{
					goto IL_133;
				}
				num = parseCurrent;
				if (ch != '\\')
				{
					goto IL_133;
				}
				if (!CssToken.AttemptUnescape(this.parseBuffer, this.parseEnd, ref ch, ref parseCurrent) || !CssParser.IsNameCharacterNoEscape(ch, ParseSupport.GetCharClass(ch)))
				{
					goto IL_B9;
				}
				parseCurrent++;
				if (nameLength == 0 && ch == '-')
				{
					flag = true;
				}
				if (nameLength == 1 && flag && char.IsDigit(ch))
				{
					goto Block_15;
				}
				nameLength++;
				this.PrepareAndAddLiteralRun(runKind, num, ref parseCurrent, (int)ch);
				if (parseCurrent == this.parseEnd)
				{
					goto IL_133;
				}
				ch = this.parseBuffer[parseCurrent];
			}
			Block_5:
			nameLength = 0;
			charClass = ParseSupport.GetCharClass(ch);
			return ch;
			IL_B9:
			ch = this.parseBuffer[++parseCurrent];
			this.PrepareAndAddInvalidRun(runKind, ref parseCurrent);
			nameLength = 0;
			goto IL_133;
			Block_15:
			nameLength = 0;
			IL_133:
			charClass = ParseSupport.GetCharClass(ch);
			return ch;
		}

		// Token: 0x0600120F RID: 4623 RVA: 0x000829F8 File Offset: 0x00080BF8
		private char ScanUrl(char ch, ref CharClass charClass, ref int parseCurrent)
		{
			for (;;)
			{
				int num = parseCurrent;
				while (this.IsUrlCharacter(ch, ParseSupport.GetCharClass(ch), parseCurrent) && parseCurrent != this.parseEnd)
				{
					ch = this.parseBuffer[++parseCurrent];
				}
				if (parseCurrent != num)
				{
					this.PrepareAndAddRun(CssRunKind.Url, num, ref parseCurrent);
				}
				if (parseCurrent == this.parseEnd)
				{
					goto IL_BA;
				}
				num = parseCurrent;
				if (ch != '\\')
				{
					goto IL_BA;
				}
				if (!CssToken.AttemptUnescape(this.parseBuffer, this.parseEnd, ref ch, ref parseCurrent))
				{
					break;
				}
				parseCurrent++;
				this.PrepareAndAddLiteralRun(CssRunKind.Url, num, ref parseCurrent, (int)ch);
				if (parseCurrent == this.parseEnd)
				{
					goto IL_BA;
				}
				ch = this.parseBuffer[parseCurrent];
			}
			ch = this.parseBuffer[++parseCurrent];
			this.PrepareAndAddInvalidRun(CssRunKind.Url, ref parseCurrent);
			IL_BA:
			charClass = ParseSupport.GetCharClass(ch);
			return ch;
		}

		// Token: 0x06001210 RID: 4624 RVA: 0x00082AC8 File Offset: 0x00080CC8
		private char ScanUnicodeRange(char ch, ref CharClass charClass, ref int parseCurrent)
		{
			char[] array = this.parseBuffer;
			int num = parseCurrent + 1;
			int i = num;
			bool flag = true;
			char c;
			while (i < num + 6)
			{
				c = array[i];
				if ('?' == c)
				{
					flag = false;
					for (i++; i < num + 6; i++)
					{
						if ('?' != array[i])
						{
							break;
						}
					}
					break;
				}
				if (!ParseSupport.HexCharacter(ParseSupport.GetCharClass(c)))
				{
					if (i == num)
					{
						return ch;
					}
					break;
				}
				else
				{
					i++;
				}
			}
			c = array[i];
			if ('-' == c && flag)
			{
				i++;
				num = i;
				while (i < num + 6)
				{
					c = array[i];
					if (!ParseSupport.HexCharacter(ParseSupport.GetCharClass(c)))
					{
						if (i == num)
						{
							return ch;
						}
						break;
					}
					else
					{
						i++;
					}
				}
			}
			c = array[i];
			charClass = ParseSupport.GetCharClass(c);
			parseCurrent = i;
			return c;
		}

		// Token: 0x06001211 RID: 4625 RVA: 0x00082B74 File Offset: 0x00080D74
		private char ScanWhitespace(char ch, ref CharClass charClass, ref int parseCurrent, bool ignorable)
		{
			char[] array = this.parseBuffer;
			int num = this.parseEnd;
			IL_7F:
			while (ParseSupport.WhitespaceCharacter(charClass) || ch == '/')
			{
				if (ch != '/')
				{
					int start = parseCurrent;
					while (++parseCurrent != num)
					{
						ch = array[parseCurrent];
						charClass = ParseSupport.GetCharClass(ch);
						if (!ParseSupport.WhitespaceCharacter(charClass))
						{
							if (this.tokenBuilder.IsStarted)
							{
								this.PrepareAndAddRun(ignorable ? CssRunKind.Invalid : CssRunKind.Space, start, ref parseCurrent);
								goto IL_7F;
							}
							goto IL_7F;
						}
					}
					return ch;
				}
				if (parseCurrent >= num || array[parseCurrent + 1] != '*')
				{
					break;
				}
				ch = this.ScanComment(ch, ref charClass, ref parseCurrent);
				if (parseCurrent == num)
				{
					return ch;
				}
			}
			return ch;
		}

		// Token: 0x06001212 RID: 4626 RVA: 0x00082C10 File Offset: 0x00080E10
		private char ScanComment(char ch, ref CharClass charClass, ref int parseCurrent)
		{
			char[] array = this.parseBuffer;
			int num = this.parseEnd;
			int start = parseCurrent;
			ch = array[++parseCurrent];
			while (++parseCurrent != num)
			{
				if (array[parseCurrent] == '*' && parseCurrent + 1 != num && array[parseCurrent + 1] == '/')
				{
					parseCurrent++;
					if (++parseCurrent == num)
					{
						return ch;
					}
					if (this.tokenBuilder.IsStarted)
					{
						this.PrepareAndAddRun(CssRunKind.Space, start, ref parseCurrent);
					}
					ch = array[parseCurrent];
					charClass = ParseSupport.GetCharClass(ch);
					return ch;
				}
			}
			return ch;
		}

		// Token: 0x06001213 RID: 4627 RVA: 0x00082CA3 File Offset: 0x00080EA3
		private void PrepareAndAddRun(CssRunKind runKind, int start, ref int parseCurrent)
		{
			if (!this.tokenBuilder.PrepareAndAddRun(runKind, start, parseCurrent))
			{
				parseCurrent = this.parseEnd;
			}
		}

		// Token: 0x06001214 RID: 4628 RVA: 0x00082CBE File Offset: 0x00080EBE
		private void PrepareAndAddInvalidRun(CssRunKind runKind, ref int parseCurrent)
		{
			if (!this.tokenBuilder.PrepareAndAddInvalidRun(runKind, parseCurrent))
			{
				parseCurrent = this.parseEnd;
			}
		}

		// Token: 0x06001215 RID: 4629 RVA: 0x00082CD8 File Offset: 0x00080ED8
		private void PrepareAndAddLiteralRun(CssRunKind runKind, int start, ref int parseCurrent, int value)
		{
			if (!this.tokenBuilder.PrepareAndAddLiteralRun(runKind, start, parseCurrent, value))
			{
				parseCurrent = this.parseEnd;
			}
		}

		// Token: 0x06001216 RID: 4630 RVA: 0x00082CF8 File Offset: 0x00080EF8
		private char SkipToNextRule(char ch, ref CharClass charClass, ref int parseCurrent)
		{
			int num = this.parseEnd;
			char[] array = this.parseBuffer;
			for (;;)
			{
				if (ch == '"' || ch == '\'')
				{
					ch = this.ScanString(ch, ref charClass, ref parseCurrent, false);
					if (parseCurrent == num)
					{
						break;
					}
				}
				else
				{
					if (ch == '{')
					{
						this.ruleDepth++;
					}
					else if (ch == '}')
					{
						if (this.ruleDepth > 0)
						{
							this.ruleDepth--;
						}
						if (this.ruleDepth == 0)
						{
							goto Block_6;
						}
					}
					else if (ch == ';' && this.ruleDepth == 0)
					{
						goto Block_8;
					}
					if (++parseCurrent == num)
					{
						return ch;
					}
					ch = array[parseCurrent];
				}
			}
			return ch;
			Block_6:
			ch = array[++parseCurrent];
			charClass = ParseSupport.GetCharClass(ch);
			return ch;
			Block_8:
			ch = array[++parseCurrent];
			charClass = ParseSupport.GetCharClass(ch);
			return ch;
		}

		// Token: 0x06001217 RID: 4631 RVA: 0x00082DC0 File Offset: 0x00080FC0
		private bool IsSafeIdentifier(string[] table, int start, int end)
		{
			int length = end - start;
			for (int i = 0; i < table.Length; i++)
			{
				if (this.IsNameEqual(table[i], start, length))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001218 RID: 4632 RVA: 0x00082DEF File Offset: 0x00080FEF
		private bool IsNameEqual(string name, int start, int length)
		{
			return name.Equals(new string(this.parseBuffer, start, length), StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06001219 RID: 4633 RVA: 0x00082E05 File Offset: 0x00081005
		private bool IsNameCharacter(char ch, CharClass charClass, int parseCurrent)
		{
			return this.IsNameStartCharacter(ch, charClass, parseCurrent) || ParseSupport.NumericCharacter(charClass) || ch == '-';
		}

		// Token: 0x0600121A RID: 4634 RVA: 0x00082E24 File Offset: 0x00081024
		private bool IsIdentStartCharacter(char ch, CharClass charClass, int parseCurrent)
		{
			if (CssParser.IsNameStartCharacterNoEscape(ch, charClass))
			{
				return true;
			}
			if (ch == '-')
			{
				return true;
			}
			if (CssToken.AttemptUnescape(this.parseBuffer, this.parseEnd, ref ch, ref parseCurrent))
			{
				charClass = ParseSupport.GetCharClass(ch);
				return CssParser.IsNameStartCharacterNoEscape(ch, charClass) || ch == '-';
			}
			return false;
		}

		// Token: 0x0600121B RID: 4635 RVA: 0x00082E74 File Offset: 0x00081074
		private bool IsNameStartCharacter(char ch, CharClass charClass, int parseCurrent)
		{
			if (CssParser.IsNameStartCharacterNoEscape(ch, charClass))
			{
				return true;
			}
			if (CssToken.AttemptUnescape(this.parseBuffer, this.parseEnd, ref ch, ref parseCurrent))
			{
				charClass = ParseSupport.GetCharClass(ch);
				return CssParser.IsNameStartCharacterNoEscape(ch, charClass);
			}
			return false;
		}

		// Token: 0x0600121C RID: 4636 RVA: 0x00082EA9 File Offset: 0x000810A9
		private bool IsUrlCharacter(char ch, CharClass charClass, int parseCurrent)
		{
			return CssParser.IsUrlCharacterNoEscape(ch, charClass) || this.IsEscape(ch, parseCurrent);
		}

		// Token: 0x0600121D RID: 4637 RVA: 0x00082EBE File Offset: 0x000810BE
		private bool IsEscape(char ch, int parseCurrent)
		{
			return CssToken.AttemptUnescape(this.parseBuffer, this.parseEnd, ref ch, ref parseCurrent);
		}

		// Token: 0x04001281 RID: 4737
		internal const int MaxCssLength = 524288;

		// Token: 0x04001282 RID: 4738
		protected CssTokenBuilder tokenBuilder;

		// Token: 0x04001283 RID: 4739
		private static readonly string[] SafeTermFunctions = new string[]
		{
			"rgb",
			"counter"
		};

		// Token: 0x04001284 RID: 4740
		private static readonly string[] SafePseudoFunctions = new string[]
		{
			"lang"
		};

		// Token: 0x04001285 RID: 4741
		private ConverterInput input;

		// Token: 0x04001286 RID: 4742
		private bool endOfFile;

		// Token: 0x04001287 RID: 4743
		private CssParseMode parseMode;

		// Token: 0x04001288 RID: 4744
		private bool isInvalid;

		// Token: 0x04001289 RID: 4745
		private char[] parseBuffer;

		// Token: 0x0400128A RID: 4746
		private int parseStart;

		// Token: 0x0400128B RID: 4747
		private int parseCurrent;

		// Token: 0x0400128C RID: 4748
		private int parseEnd;

		// Token: 0x0400128D RID: 4749
		private int ruleDepth;

		// Token: 0x0400128E RID: 4750
		private CssToken token;
	}
}
