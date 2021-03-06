using System;
using System.Collections.Generic;
using Microsoft.Exchange.MessagingPolicies.Rules;

namespace Microsoft.Exchange.TextMatching
{
	// Token: 0x02000046 RID: 70
	internal sealed class RegexParser
	{
		// Token: 0x060001DC RID: 476 RVA: 0x00007F71 File Offset: 0x00006171
		public RegexParser(string pattern) : this(pattern, false)
		{
		}

		// Token: 0x060001DD RID: 477 RVA: 0x00007F7B File Offset: 0x0000617B
		public RegexParser(string pattern, bool lowerCaseLiteral)
		{
			this.pattern = pattern;
			this.state = new Stack<RegexTreeNode>();
			this.charSet = new RegexCharSet();
			this.lowerCaseLiteral = lowerCaseLiteral;
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060001DE RID: 478 RVA: 0x00007FA7 File Offset: 0x000061A7
		public string Pattern
		{
			get
			{
				return this.pattern;
			}
		}

		// Token: 0x060001DF RID: 479 RVA: 0x00007FB0 File Offset: 0x000061B0
		public void Parse()
		{
			bool flag = false;
			while (this.CharsRight() > 0)
			{
				char c = this.RightChar();
				char c2 = c;
				RegexTreeNode regexTreeNode;
				switch (c2)
				{
				case '$':
					if (flag)
					{
						this.HandleLeafNormalCharacter(c);
						flag = false;
					}
					else
					{
						regexTreeNode = new RegexTreeNode(RegexCharacterClass.ValueType.EndCharacterClass, ++this.stateid);
						this.charSet.Add(regexTreeNode.CharClass, regexTreeNode);
						this.ProcessLeaf(regexTreeNode);
					}
					break;
				case '%':
				case '&':
				case '\'':
					goto IL_1CA;
				case '(':
					if (flag)
					{
						this.HandleLeafNormalCharacter(c);
						flag = false;
					}
					else
					{
						this.state.Push(new RegexTreeNode(RegexTreeNode.NodeType.LeftParen, -1));
					}
					break;
				case ')':
					if (flag)
					{
						this.HandleLeafNormalCharacter(c);
						flag = false;
					}
					else
					{
						this.state.Push(new RegexTreeNode(RegexTreeNode.NodeType.RightParen, -1));
						this.Reduce();
					}
					break;
				case '*':
					if (flag)
					{
						this.HandleLeafNormalCharacter(c);
						flag = false;
					}
					else
					{
						this.ProcessStar(new RegexTreeNode(RegexTreeNode.NodeType.Star, -1));
					}
					break;
				default:
					switch (c2)
					{
					case '\\':
						if (flag)
						{
							RegexTreeNode regexTreeNode2 = new RegexTreeNode(c, ++this.stateid);
							this.charSet.Add(regexTreeNode2.CharClass, regexTreeNode2);
							this.ProcessLeaf(regexTreeNode2);
							flag = false;
						}
						else
						{
							flag = true;
						}
						break;
					case ']':
						goto IL_1CA;
					case '^':
						if (flag)
						{
							this.HandleLeafNormalCharacter(c);
							flag = false;
						}
						else
						{
							regexTreeNode = new RegexTreeNode(RegexCharacterClass.ValueType.BeginCharacterClass, ++this.stateid);
							this.charSet.Add(regexTreeNode.CharClass, regexTreeNode);
							this.ProcessLeaf(regexTreeNode);
						}
						break;
					default:
						if (c2 != '|')
						{
							goto IL_1CA;
						}
						if (flag)
						{
							this.HandleLeafNormalCharacter(c);
							flag = false;
						}
						else
						{
							this.ProcessBar(new RegexTreeNode(RegexTreeNode.NodeType.Bar, -1));
						}
						break;
					}
					break;
				}
				IL_346:
				this.MoveRight();
				continue;
				IL_1CA:
				if (flag)
				{
					char c3 = c;
					if (c3 <= 'S')
					{
						if (c3 <= '?')
						{
							if (c3 != '.' && c3 != '?')
							{
								goto IL_2FF;
							}
							regexTreeNode = this.AddNode(c);
							this.charSet.Add(regexTreeNode.CharClass, regexTreeNode);
						}
						else if (c3 != 'D')
						{
							if (c3 != 'S')
							{
								goto IL_2FF;
							}
							regexTreeNode = new RegexTreeNode(RegexCharacterClass.ValueType.NonSpaceCharacterClass, ++this.stateid);
						}
						else
						{
							regexTreeNode = new RegexTreeNode(RegexCharacterClass.ValueType.NonDigitCharacterClass, ++this.stateid);
						}
					}
					else if (c3 <= 'd')
					{
						if (c3 != 'W')
						{
							if (c3 != 'd')
							{
								goto IL_2FF;
							}
							regexTreeNode = new RegexTreeNode(RegexCharacterClass.ValueType.DigitCharacterClass, ++this.stateid);
						}
						else
						{
							regexTreeNode = new RegexTreeNode(RegexCharacterClass.ValueType.NonWordCharacterClass, ++this.stateid);
						}
					}
					else if (c3 != 's')
					{
						if (c3 != 'w')
						{
							goto IL_2FF;
						}
						regexTreeNode = new RegexTreeNode(RegexCharacterClass.ValueType.WordCharacterClass, ++this.stateid);
					}
					else
					{
						regexTreeNode = new RegexTreeNode(RegexCharacterClass.ValueType.SpaceCharacterClass, ++this.stateid);
					}
					this.charSet.Add(regexTreeNode.CharClass, regexTreeNode);
					flag = false;
					goto IL_33F;
					IL_2FF:
					throw new TextMatchingParsingException(TextMatchingStrings.RegexUnSupportedMetaCharacter);
				}
				regexTreeNode = this.AddNode(c);
				this.charSet.Add(regexTreeNode.CharClass, regexTreeNode);
				IL_33F:
				this.ProcessLeaf(regexTreeNode);
				goto IL_346;
			}
			RegexTreeNode regexTreeNode3 = new RegexTreeNode(RegexParser.EOS, ++this.stateid);
			this.charSet.Add(regexTreeNode3.CharClass, regexTreeNode3);
			this.ProcessEnd(regexTreeNode3);
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x00008350 File Offset: 0x00006550
		private RegexTreeNode AddNode(char ch)
		{
			if (this.lowerCaseLiteral)
			{
				ch = char.ToLowerInvariant(ch);
			}
			return new RegexTreeNode(ch, ++this.stateid);
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x00008384 File Offset: 0x00006584
		public PatternMatcher Compile()
		{
			this.root.ComputeNFL();
			RegexStateFactory regexStateFactory = new RegexStateFactory();
			RegexStateSet regexStateSet = new RegexStateSet();
			RegexState regexState = regexStateFactory.CreateRegexState(this.root.FirstPos);
			regexState.IsStartState = true;
			regexStateSet.Add(regexState);
			DFACodeGenerator dfacodeGenerator = new DFACodeGenerator("matcher", this.stateid);
			dfacodeGenerator.BeginCompile();
			foreach (RegexState regexState2 in regexStateSet.UnmarkedStates())
			{
				regexState2.Marked = true;
				foreach (RegexCharacterClass cl in this.charSet.Chars())
				{
					RegexState regexState3 = this.ComputeTransitionState(regexState2, cl, regexStateFactory, regexStateSet);
					if (regexState3 != null)
					{
						if (!regexStateSet.Contains(regexState3))
						{
							regexStateSet.Add(regexState3);
						}
						dfacodeGenerator.Add(regexState2, regexState3, cl);
					}
				}
			}
			return dfacodeGenerator.EndCompile();
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x00008660 File Offset: 0x00006860
		private IEnumerable<RegexTreeNode> MatchingNodes(RegexState fromState, RegexCharacterClass cl)
		{
			foreach (RegexTreeNode node in this.charSet[cl])
			{
				if (fromState.Contains(node.State))
				{
					yield return node;
				}
			}
			yield break;
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x0000868C File Offset: 0x0000688C
		private RegexState ComputeTransitionState(RegexState fromState, RegexCharacterClass cl, RegexStateFactory dsf, RegexStateSet dstates)
		{
			RegexState regexState = null;
			foreach (RegexTreeNode regexTreeNode in this.MatchingNodes(fromState, cl))
			{
				if (regexTreeNode.FollowPos != null)
				{
					if (regexState == null)
					{
						regexState = dsf.CreateRegexState();
					}
					regexState.Add(regexTreeNode.FollowPos);
				}
			}
			RegexState regexState2 = dstates.MatchingState(regexState);
			if (regexState2 != null)
			{
				dsf.Destory();
				regexState = regexState2;
			}
			return regexState;
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x0000870C File Offset: 0x0000690C
		private void HandleLeafNormalCharacter(char ch)
		{
			RegexTreeNode regexTreeNode = new RegexTreeNode(ch, ++this.stateid);
			this.charSet.Add(regexTreeNode.CharClass, regexTreeNode);
			this.ProcessLeaf(regexTreeNode);
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x0000874C File Offset: 0x0000694C
		private void ProcessStar(RegexTreeNode star)
		{
			if (this.state.Count == 0)
			{
				throw new TextMatchingParsingException(TextMatchingStrings.RegexSyntaxForStar);
			}
			RegexTreeNode regexTreeNode = this.state.Peek();
			switch (regexTreeNode.Type)
			{
			case RegexTreeNode.NodeType.Leaf:
			case RegexTreeNode.NodeType.Cat:
				star.Left = regexTreeNode;
				this.state.Pop();
				this.state.Push(star);
				this.Reduce();
				return;
			case RegexTreeNode.NodeType.Bar:
				if (regexTreeNode.Right == null)
				{
					throw new TextMatchingParsingException(TextMatchingStrings.RegexSyntaxForStar);
				}
				star.Left = regexTreeNode;
				this.state.Pop();
				this.state.Push(star);
				this.Reduce();
				return;
			case RegexTreeNode.NodeType.LeftParen:
				throw new TextMatchingParsingException(TextMatchingStrings.RegexSyntaxForStar);
			case RegexTreeNode.NodeType.RightParen:
				this.Reduce();
				this.ProcessStar(star);
				return;
			}
			throw new TextMatchingParsingException(TextMatchingStrings.RegexInternalParsingError);
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x00008840 File Offset: 0x00006A40
		private void ProcessBar(RegexTreeNode bar)
		{
			if (this.state.Count == 0)
			{
				throw new TextMatchingParsingException(TextMatchingStrings.RegexSyntaxForBar);
			}
			RegexTreeNode regexTreeNode = this.state.Peek();
			switch (regexTreeNode.Type)
			{
			case RegexTreeNode.NodeType.Leaf:
			case RegexTreeNode.NodeType.Cat:
				this.Reduce();
				bar.Left = this.state.Pop();
				this.state.Push(bar);
				return;
			case RegexTreeNode.NodeType.Bar:
				if (regexTreeNode.Right == null)
				{
					throw new TextMatchingParsingException(TextMatchingStrings.RegexSyntaxForBar);
				}
				bar.Left = regexTreeNode;
				this.state.Pop();
				this.state.Push(bar);
				return;
			case RegexTreeNode.NodeType.Star:
				this.state.Push(bar);
				return;
			default:
				throw new TextMatchingParsingException(TextMatchingStrings.RegexInternalParsingError);
			}
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x00008910 File Offset: 0x00006B10
		private void ProcessEnd(RegexTreeNode node)
		{
			while (this.state.Count > 1)
			{
				int count = this.state.Count;
				this.Reduce();
				if (count == this.state.Count)
				{
					throw new TextMatchingParsingException(TextMatchingStrings.RegexMismatchingParenthesis);
				}
			}
			RegexTreeNode regexTreeNode = this.state.Peek();
			RegexTreeNode.NodeType type = regexTreeNode.Type;
			if (type == RegexTreeNode.NodeType.LeftParen)
			{
				throw new TextMatchingParsingException(TextMatchingStrings.RegexMismatchingParenthesis);
			}
			if (regexTreeNode.Type == RegexTreeNode.NodeType.Bar && (regexTreeNode.Left == null || regexTreeNode.Right == null))
			{
				throw new TextMatchingParsingException(TextMatchingStrings.RegexSyntaxForBar);
			}
			RegexTreeNode regexTreeNode2 = new RegexTreeNode(RegexTreeNode.NodeType.Cat, -1);
			regexTreeNode2.Left = regexTreeNode;
			regexTreeNode2.Right = node;
			this.state.Pop();
			this.root = regexTreeNode2;
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x000089D8 File Offset: 0x00006BD8
		private void ProcessLeaf(RegexTreeNode node)
		{
			if (this.state.Count == 0)
			{
				this.state.Push(node);
				return;
			}
			RegexTreeNode regexTreeNode = this.state.Peek();
			switch (regexTreeNode.Type)
			{
			case RegexTreeNode.NodeType.Leaf:
				this.state.Push(node);
				return;
			case RegexTreeNode.NodeType.Bar:
			case RegexTreeNode.NodeType.Star:
				this.state.Push(node);
				return;
			case RegexTreeNode.NodeType.Cat:
				this.CatNodeWithTOS(node);
				return;
			case RegexTreeNode.NodeType.LeftParen:
				this.state.Push(node);
				return;
			case RegexTreeNode.NodeType.RightParen:
				this.Reduce();
				this.ProcessLeaf(node);
				return;
			default:
				throw new TextMatchingParsingException(TextMatchingStrings.RegexInternalParsingError);
			}
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x00008A80 File Offset: 0x00006C80
		private void CatNodeWithTOS(RegexTreeNode node)
		{
			RegexTreeNode regexTreeNode = new RegexTreeNode(RegexTreeNode.NodeType.Cat, -1);
			regexTreeNode.Left = this.state.Peek();
			regexTreeNode.Right = node;
			this.state.Pop();
			this.state.Push(regexTreeNode);
		}

		// Token: 0x060001EA RID: 490 RVA: 0x00008AC8 File Offset: 0x00006CC8
		private void Reduce()
		{
			RegexTreeNode regexTreeNode = this.state.Peek();
			switch (regexTreeNode.Type)
			{
			case RegexTreeNode.NodeType.Leaf:
				if (this.state.Count > 1)
				{
					RegexTreeNode regexTreeNode2 = this.state.Pop();
					regexTreeNode = this.state.Peek();
					switch (regexTreeNode.Type)
					{
					case RegexTreeNode.NodeType.Leaf:
					case RegexTreeNode.NodeType.Cat:
					case RegexTreeNode.NodeType.Star:
						this.CatNodeWithTOS(regexTreeNode2);
						this.Reduce();
						return;
					case RegexTreeNode.NodeType.Bar:
						if (regexTreeNode.Right != null)
						{
							RegexTreeNode regexTreeNode3 = new RegexTreeNode(RegexTreeNode.NodeType.Cat, -1);
							regexTreeNode3.Left = this.state.Pop();
							regexTreeNode3.Right = regexTreeNode2;
							this.state.Push(regexTreeNode3);
							return;
						}
						regexTreeNode.Right = regexTreeNode2;
						return;
					default:
						this.state.Push(regexTreeNode2);
						return;
					}
				}
				break;
			case RegexTreeNode.NodeType.Bar:
			{
				RegexTreeNode regexTreeNode2 = this.state.Pop();
				if (this.state.Count <= 0)
				{
					throw new TextMatchingParsingException(TextMatchingStrings.RegexInternalParsingError);
				}
				regexTreeNode = this.state.Peek();
				if (regexTreeNode.Type == RegexTreeNode.NodeType.Bar)
				{
					regexTreeNode.Right = regexTreeNode2;
					return;
				}
				if (regexTreeNode.Type == RegexTreeNode.NodeType.LeftParen)
				{
					throw new TextMatchingParsingException(TextMatchingStrings.RegexMismatchingParenthesis);
				}
				regexTreeNode2.Left = regexTreeNode;
				this.state.Pop();
				this.state.Push(regexTreeNode2);
				return;
			}
			case RegexTreeNode.NodeType.Cat:
				if (this.state.Count > 1)
				{
					RegexTreeNode regexTreeNode2 = this.state.Pop();
					regexTreeNode = this.state.Peek();
					switch (regexTreeNode.Type)
					{
					case RegexTreeNode.NodeType.Leaf:
					case RegexTreeNode.NodeType.Cat:
					case RegexTreeNode.NodeType.Star:
						this.CatNodeWithTOS(regexTreeNode2);
						this.Reduce();
						return;
					case RegexTreeNode.NodeType.Bar:
						regexTreeNode.Right = regexTreeNode2;
						return;
					default:
						this.state.Push(regexTreeNode2);
						return;
					}
				}
				break;
			case RegexTreeNode.NodeType.Star:
				if (this.state.Count > 1)
				{
					RegexTreeNode regexTreeNode2 = this.state.Pop();
					regexTreeNode = this.state.Peek();
					if (regexTreeNode.Type == RegexTreeNode.NodeType.Leaf || regexTreeNode.Type == RegexTreeNode.NodeType.Cat || regexTreeNode.Type == RegexTreeNode.NodeType.Bar || regexTreeNode.Type == RegexTreeNode.NodeType.Star)
					{
						this.CatNodeWithTOS(regexTreeNode2);
						return;
					}
					this.state.Push(regexTreeNode2);
					return;
				}
				break;
			case RegexTreeNode.NodeType.LeftParen:
				break;
			case RegexTreeNode.NodeType.RightParen:
			{
				this.state.Pop();
				if (this.state.Count <= 1)
				{
					throw new TextMatchingParsingException(TextMatchingStrings.RegexInternalParsingError);
				}
				RegexTreeNode regexTreeNode2 = this.state.Pop();
				regexTreeNode = this.state.Peek();
				if (regexTreeNode.Type == RegexTreeNode.NodeType.Bar)
				{
					regexTreeNode.Right = regexTreeNode2;
					regexTreeNode2 = this.state.Pop();
				}
				else if (regexTreeNode.Type == RegexTreeNode.NodeType.Star || regexTreeNode.Type == RegexTreeNode.NodeType.Cat || regexTreeNode.Type == RegexTreeNode.NodeType.Leaf)
				{
					this.CatNodeWithTOS(regexTreeNode2);
					regexTreeNode2 = this.state.Pop();
				}
				bool flag = false;
				while (this.state.Count >= 1 && !flag)
				{
					regexTreeNode = this.state.Peek();
					switch (regexTreeNode.Type)
					{
					case RegexTreeNode.NodeType.Bar:
						regexTreeNode.Right = regexTreeNode2;
						regexTreeNode2 = this.state.Pop();
						continue;
					case RegexTreeNode.NodeType.Star:
						if (regexTreeNode2.Type == RegexTreeNode.NodeType.Bar && regexTreeNode2.Left == null)
						{
							regexTreeNode2.Left = this.state.Pop();
							continue;
						}
						this.CatNodeWithTOS(regexTreeNode2);
						regexTreeNode2 = this.state.Pop();
						continue;
					case RegexTreeNode.NodeType.LeftParen:
						this.state.Pop();
						flag = true;
						continue;
					}
					this.CatNodeWithTOS(regexTreeNode2);
					regexTreeNode2 = this.state.Pop();
				}
				this.state.Push(regexTreeNode2);
				return;
			}
			default:
				return;
			}
		}

		// Token: 0x060001EB RID: 491 RVA: 0x00008E4E File Offset: 0x0000704E
		private int CharsRight()
		{
			return this.pattern.Length - this.currentPos;
		}

		// Token: 0x060001EC RID: 492 RVA: 0x00008E62 File Offset: 0x00007062
		private char RightChar()
		{
			return this.pattern[this.currentPos];
		}

		// Token: 0x060001ED RID: 493 RVA: 0x00008E75 File Offset: 0x00007075
		private void MoveRight()
		{
			this.currentPos++;
		}

		// Token: 0x040000D7 RID: 215
		internal static char EOS = char.MaxValue;

		// Token: 0x040000D8 RID: 216
		private string pattern;

		// Token: 0x040000D9 RID: 217
		private int currentPos;

		// Token: 0x040000DA RID: 218
		private Stack<RegexTreeNode> state;

		// Token: 0x040000DB RID: 219
		private RegexTreeNode root;

		// Token: 0x040000DC RID: 220
		private int stateid;

		// Token: 0x040000DD RID: 221
		private RegexCharSet charSet;

		// Token: 0x040000DE RID: 222
		private bool lowerCaseLiteral;
	}
}
