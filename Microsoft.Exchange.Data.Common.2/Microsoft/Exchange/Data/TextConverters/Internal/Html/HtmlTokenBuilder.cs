using System;
using System.Text;

namespace Microsoft.Exchange.Data.TextConverters.Internal.Html
{
	// Token: 0x02000224 RID: 548
	internal class HtmlTokenBuilder : TokenBuilder
	{
		// Token: 0x0600163B RID: 5691 RVA: 0x000AEF14 File Offset: 0x000AD114
		public HtmlTokenBuilder(char[] buffer, int maxRuns, int maxAttrs, bool testBoundaryConditions) : base(new HtmlToken(), buffer, maxRuns, testBoundaryConditions)
		{
			this.htmlToken = (HtmlToken)base.Token;
			int num = 8;
			if (maxAttrs != 0)
			{
				if (!testBoundaryConditions)
				{
					this.maxAttrs = maxAttrs;
				}
				else
				{
					num = 1;
					this.maxAttrs = 5;
				}
				this.htmlToken.AttributeList = new HtmlToken.AttributeEntry[num];
			}
			this.htmlToken.NameIndex = HtmlNameIndex._NOTANAME;
		}

		// Token: 0x170005A7 RID: 1447
		// (get) Token: 0x0600163C RID: 5692 RVA: 0x000AEF79 File Offset: 0x000AD179
		public new HtmlToken Token
		{
			get
			{
				return this.htmlToken;
			}
		}

		// Token: 0x170005A8 RID: 1448
		// (get) Token: 0x0600163D RID: 5693 RVA: 0x000AEF81 File Offset: 0x000AD181
		public bool IncompleteTag
		{
			get
			{
				return this.state >= 10 && this.state != 10;
			}
		}

		// Token: 0x0600163E RID: 5694 RVA: 0x000AEF9C File Offset: 0x000AD19C
		public static HtmlNameIndex LookupName(char[] nameBuffer, int nameOffset, int nameLength)
		{
			if (nameLength != 0 && nameLength <= 14)
			{
				short num = (short)((ulong)(HashCode.CalculateLowerCase(nameBuffer, nameOffset, nameLength) ^ 221) % 601UL);
				int num2 = (int)HtmlNameData.nameHashTable[(int)num];
				if (num2 > 0)
				{
					for (;;)
					{
						string name = HtmlNameData.Names[num2].Name;
						if (name.Length == nameLength && name[0] == ParseSupport.ToLowerCase(nameBuffer[nameOffset]))
						{
							int num3 = 0;
							while (++num3 < name.Length && ParseSupport.ToLowerCase(nameBuffer[nameOffset + num3]) == name[num3])
							{
							}
							if (num3 == name.Length)
							{
								break;
							}
						}
						num2++;
						if (HtmlNameData.Names[num2].Hash != num)
						{
							return HtmlNameIndex.Unknown;
						}
					}
					return (HtmlNameIndex)num2;
				}
			}
			return HtmlNameIndex.Unknown;
		}

		// Token: 0x0600163F RID: 5695 RVA: 0x000AF04E File Offset: 0x000AD24E
		public override void Reset()
		{
			if (this.state >= 6)
			{
				this.htmlToken.Reset();
				this.numCarryOverRuns = 0;
			}
			base.Reset();
		}

		// Token: 0x06001640 RID: 5696 RVA: 0x000AF071 File Offset: 0x000AD271
		public HtmlTokenId MakeEmptyToken(HtmlTokenId tokenId)
		{
			return (HtmlTokenId)base.MakeEmptyToken((TokenId)tokenId);
		}

		// Token: 0x06001641 RID: 5697 RVA: 0x000AF07A File Offset: 0x000AD27A
		public HtmlTokenId MakeEmptyToken(HtmlTokenId tokenId, int argument)
		{
			return (HtmlTokenId)base.MakeEmptyToken((TokenId)tokenId, argument);
		}

		// Token: 0x06001642 RID: 5698 RVA: 0x000AF084 File Offset: 0x000AD284
		public HtmlTokenId MakeEmptyToken(HtmlTokenId tokenId, Encoding tokenEncoding)
		{
			return (HtmlTokenId)base.MakeEmptyToken((TokenId)tokenId, tokenEncoding);
		}

		// Token: 0x06001643 RID: 5699 RVA: 0x000AF090 File Offset: 0x000AD290
		public void StartTag(HtmlNameIndex nameIndex, int baseOffset)
		{
			this.state = 20;
			this.htmlToken.HtmlTokenId = HtmlTokenId.Tag;
			this.htmlToken.PartMajor = HtmlToken.TagPartMajor.Begin;
			this.htmlToken.PartMinor = HtmlToken.TagPartMinor.Empty;
			this.htmlToken.NameIndex = nameIndex;
			this.htmlToken.TagIndex = HtmlNameData.Names[(int)nameIndex].TagIndex;
			this.htmlToken.Whole.HeadOffset = baseOffset;
			this.tailOffset = baseOffset;
		}

		// Token: 0x06001644 RID: 5700 RVA: 0x000AF108 File Offset: 0x000AD308
		public void AbortConditional(bool comment)
		{
			this.htmlToken.NameIndex = (comment ? HtmlNameIndex._COMMENT : HtmlNameIndex._BANG);
		}

		// Token: 0x06001645 RID: 5701 RVA: 0x000AF11C File Offset: 0x000AD31C
		public void SetEndTag()
		{
			HtmlToken htmlToken = this.htmlToken;
			htmlToken.Flags |= HtmlToken.TagFlags.EndTag;
		}

		// Token: 0x06001646 RID: 5702 RVA: 0x000AF133 File Offset: 0x000AD333
		public void SetEmptyScope()
		{
			HtmlToken htmlToken = this.htmlToken;
			htmlToken.Flags |= HtmlToken.TagFlags.EmptyScope;
		}

		// Token: 0x06001647 RID: 5703 RVA: 0x000AF14C File Offset: 0x000AD34C
		public void StartTagText()
		{
			this.state = 21;
			this.htmlToken.Unstructured.Initialize(this.htmlToken.Whole.Tail, this.tailOffset);
			this.htmlToken.UnstructuredPosition.Rewind(this.htmlToken.Unstructured);
		}

		// Token: 0x06001648 RID: 5704 RVA: 0x000AF1A2 File Offset: 0x000AD3A2
		public void EndTagText()
		{
			if (this.htmlToken.Unstructured.Head == this.htmlToken.Whole.Tail)
			{
				this.AddNullRun(HtmlRunKind.TagText);
			}
			this.state = 20;
		}

		// Token: 0x06001649 RID: 5705 RVA: 0x000AF1DC File Offset: 0x000AD3DC
		public void StartTagName()
		{
			this.state = 22;
			HtmlToken htmlToken = this.htmlToken;
			htmlToken.PartMinor |= HtmlToken.TagPartMinor.BeginName;
			this.htmlToken.NameInternal.Initialize(this.htmlToken.Whole.Tail, this.tailOffset);
			this.htmlToken.LocalName.Initialize(this.htmlToken.Whole.Tail, this.tailOffset);
			this.htmlToken.NamePosition.Rewind(this.htmlToken.NameInternal);
		}

		// Token: 0x0600164A RID: 5706 RVA: 0x000AF26C File Offset: 0x000AD46C
		public void EndTagNamePrefix()
		{
			this.htmlToken.LocalName.Initialize(this.htmlToken.Whole.Tail, this.tailOffset);
		}

		// Token: 0x0600164B RID: 5707 RVA: 0x000AF294 File Offset: 0x000AD494
		public void EndTagName(int nameLength)
		{
			if (this.htmlToken.LocalName.Head == this.htmlToken.Whole.Tail)
			{
				this.AddNullRun(HtmlRunKind.Name);
				if (this.htmlToken.LocalName.Head == this.htmlToken.NameInternal.Head)
				{
					HtmlToken htmlToken = this.htmlToken;
					htmlToken.Flags |= HtmlToken.TagFlags.EmptyTagName;
				}
			}
			HtmlToken htmlToken2 = this.htmlToken;
			htmlToken2.PartMinor |= HtmlToken.TagPartMinor.EndName;
			if (this.htmlToken.IsTagBegin)
			{
				base.AddSentinelRun();
				this.htmlToken.NameIndex = this.LookupName(nameLength, this.htmlToken.NameInternal);
				this.htmlToken.TagIndex = (this.htmlToken.OriginalTagIndex = HtmlNameData.Names[(int)this.htmlToken.NameIndex].TagIndex);
			}
			this.state = 23;
		}

		// Token: 0x0600164C RID: 5708 RVA: 0x000AF384 File Offset: 0x000AD584
		public void EndTagName(HtmlNameIndex resolvedNameIndex)
		{
			if (this.htmlToken.LocalName.Head == this.htmlToken.Whole.Tail)
			{
				this.AddNullRun(HtmlRunKind.Name);
				if (this.htmlToken.LocalName.Head == this.htmlToken.NameInternal.Head)
				{
					HtmlToken htmlToken = this.htmlToken;
					htmlToken.Flags |= HtmlToken.TagFlags.EmptyTagName;
				}
			}
			HtmlToken htmlToken2 = this.htmlToken;
			htmlToken2.PartMinor |= HtmlToken.TagPartMinor.EndName;
			if (this.htmlToken.IsTagBegin)
			{
				this.htmlToken.NameIndex = resolvedNameIndex;
				this.htmlToken.TagIndex = (this.htmlToken.OriginalTagIndex = HtmlNameData.Names[(int)resolvedNameIndex].TagIndex);
			}
			this.state = 23;
		}

		// Token: 0x0600164D RID: 5709 RVA: 0x000AF452 File Offset: 0x000AD652
		public bool CanAddAttribute()
		{
			return this.htmlToken.AttributeTail < this.maxAttrs;
		}

		// Token: 0x0600164E RID: 5710 RVA: 0x000AF468 File Offset: 0x000AD668
		public void StartAttribute()
		{
			if (this.htmlToken.AttributeTail == this.htmlToken.AttributeList.Length)
			{
				int num;
				if (this.maxAttrs / 2 > this.htmlToken.AttributeList.Length)
				{
					num = this.htmlToken.AttributeList.Length * 2;
				}
				else
				{
					num = this.maxAttrs;
				}
				HtmlToken.AttributeEntry[] array = new HtmlToken.AttributeEntry[num];
				Array.Copy(this.htmlToken.AttributeList, 0, array, 0, this.htmlToken.AttributeTail);
				this.htmlToken.AttributeList = array;
			}
			if (this.htmlToken.PartMinor == HtmlToken.TagPartMinor.Empty)
			{
				this.htmlToken.PartMinor = HtmlToken.TagPartMinor.BeginAttribute;
			}
			this.htmlToken.AttributeList[this.htmlToken.AttributeTail].NameIndex = HtmlNameIndex.Unknown;
			this.htmlToken.AttributeList[this.htmlToken.AttributeTail].PartMajor = HtmlToken.AttrPartMajor.Begin;
			this.htmlToken.AttributeList[this.htmlToken.AttributeTail].PartMinor = HtmlToken.AttrPartMinor.BeginName;
			this.htmlToken.AttributeList[this.htmlToken.AttributeTail].QuoteChar = 0;
			this.htmlToken.AttributeList[this.htmlToken.AttributeTail].Name.Initialize(this.htmlToken.Whole.Tail, this.tailOffset);
			this.htmlToken.AttributeList[this.htmlToken.AttributeTail].LocalName.Initialize(this.htmlToken.Whole.Tail, this.tailOffset);
			this.htmlToken.AttributeList[this.htmlToken.AttributeTail].Value.Reset();
			this.state = 24;
		}

		// Token: 0x0600164F RID: 5711 RVA: 0x000AF632 File Offset: 0x000AD832
		public void EndAttributeNamePrefix()
		{
			this.htmlToken.AttributeList[this.htmlToken.AttributeTail].LocalName.Initialize(this.htmlToken.Whole.Tail, this.tailOffset);
		}

		// Token: 0x06001650 RID: 5712 RVA: 0x000AF670 File Offset: 0x000AD870
		public void EndAttributeName(int nameLength)
		{
			HtmlToken.AttributeEntry[] attributeList = this.htmlToken.AttributeList;
			int attributeTail = this.htmlToken.AttributeTail;
			attributeList[attributeTail].PartMinor = (attributeList[attributeTail].PartMinor | HtmlToken.AttrPartMinor.EndName);
			if (this.htmlToken.AttributeList[this.htmlToken.AttributeTail].LocalName.Head == this.htmlToken.Whole.Tail)
			{
				this.AddNullRun(HtmlRunKind.Name);
				if (this.htmlToken.AttributeList[this.htmlToken.AttributeTail].LocalName.Head == this.htmlToken.AttributeList[this.htmlToken.AttributeTail].Name.Head)
				{
					HtmlToken.AttributeEntry[] attributeList2 = this.htmlToken.AttributeList;
					int attributeTail2 = this.htmlToken.AttributeTail;
					attributeList2[attributeTail2].PartMajor = (attributeList2[attributeTail2].PartMajor | HtmlToken.AttrPartMajor.EmptyName);
				}
			}
			if (this.htmlToken.AttributeList[this.htmlToken.AttributeTail].IsAttrBegin)
			{
				base.AddSentinelRun();
				this.htmlToken.AttributeList[this.htmlToken.AttributeTail].NameIndex = this.LookupName(nameLength, this.htmlToken.AttributeList[this.htmlToken.AttributeTail].Name);
			}
			this.state = 25;
		}

		// Token: 0x06001651 RID: 5713 RVA: 0x000AF7D8 File Offset: 0x000AD9D8
		public void StartValue()
		{
			this.htmlToken.AttributeList[this.htmlToken.AttributeTail].Value.Initialize(this.htmlToken.Whole.Tail, this.tailOffset);
			HtmlToken.AttributeEntry[] attributeList = this.htmlToken.AttributeList;
			int attributeTail = this.htmlToken.AttributeTail;
			attributeList[attributeTail].PartMinor = (attributeList[attributeTail].PartMinor | HtmlToken.AttrPartMinor.BeginValue);
			this.state = 26;
		}

		// Token: 0x06001652 RID: 5714 RVA: 0x000AF854 File Offset: 0x000ADA54
		public void SetValueQuote(char ch)
		{
			this.htmlToken.AttributeList[this.htmlToken.AttributeTail].IsAttrValueQuoted = true;
			this.htmlToken.AttributeList[this.htmlToken.AttributeTail].QuoteChar = (byte)ch;
		}

		// Token: 0x06001653 RID: 5715 RVA: 0x000AF8A4 File Offset: 0x000ADAA4
		public void SetBackquote()
		{
			HtmlToken.AttributeEntry[] attributeList = this.htmlToken.AttributeList;
			int attributeTail = this.htmlToken.AttributeTail;
			attributeList[attributeTail].DangerousCharacters = (attributeList[attributeTail].DangerousCharacters | 1);
		}

		// Token: 0x06001654 RID: 5716 RVA: 0x000AF8CF File Offset: 0x000ADACF
		public void SetBackslash()
		{
			HtmlToken.AttributeEntry[] attributeList = this.htmlToken.AttributeList;
			int attributeTail = this.htmlToken.AttributeTail;
			attributeList[attributeTail].DangerousCharacters = (attributeList[attributeTail].DangerousCharacters | 2);
		}

		// Token: 0x06001655 RID: 5717 RVA: 0x000AF8FC File Offset: 0x000ADAFC
		public void EndValue()
		{
			if (this.htmlToken.AttributeList[this.htmlToken.AttributeTail].Value.Head == this.htmlToken.Whole.Tail)
			{
				this.AddNullRun(HtmlRunKind.AttrValue);
			}
			HtmlToken.AttributeEntry[] attributeList = this.htmlToken.AttributeList;
			int attributeTail = this.htmlToken.AttributeTail;
			attributeList[attributeTail].PartMinor = (attributeList[attributeTail].PartMinor | HtmlToken.AttrPartMinor.EndValue);
			this.state = 27;
		}

		// Token: 0x06001656 RID: 5718 RVA: 0x000AF980 File Offset: 0x000ADB80
		public void EndAttribute()
		{
			HtmlToken.AttributeEntry[] attributeList = this.htmlToken.AttributeList;
			int attributeTail = this.htmlToken.AttributeTail;
			attributeList[attributeTail].PartMajor = (attributeList[attributeTail].PartMajor | HtmlToken.AttrPartMajor.End);
			this.htmlToken.AttributeTail++;
			if (this.htmlToken.AttributeTail < this.htmlToken.AttributeList.Length)
			{
				this.htmlToken.AttributeList[this.htmlToken.AttributeTail].PartMajor = HtmlToken.AttrPartMajor.None;
				this.htmlToken.AttributeList[this.htmlToken.AttributeTail].PartMinor = HtmlToken.AttrPartMinor.Empty;
			}
			if (this.htmlToken.PartMinor == HtmlToken.TagPartMinor.BeginAttribute)
			{
				this.htmlToken.PartMinor = HtmlToken.TagPartMinor.Attributes;
			}
			else if (this.htmlToken.PartMinor == HtmlToken.TagPartMinor.ContinueAttribute)
			{
				this.htmlToken.PartMinor = HtmlToken.TagPartMinor.EndAttribute;
			}
			else
			{
				HtmlToken htmlToken = this.htmlToken;
				htmlToken.PartMinor |= HtmlToken.TagPartMinor.Attributes;
			}
			this.state = 23;
		}

		// Token: 0x06001657 RID: 5719 RVA: 0x000AFA88 File Offset: 0x000ADC88
		public void EndTag(bool complete)
		{
			if (complete)
			{
				if (this.state != 23)
				{
					if (this.state == 21)
					{
						this.EndTagText();
					}
					else if (this.state == 22)
					{
						this.EndTagName(0);
					}
					else
					{
						if (this.state == 24)
						{
							this.EndAttributeName(0);
						}
						else if (this.state == 26)
						{
							this.EndValue();
						}
						if (this.state == 25 || this.state == 27)
						{
							this.EndAttribute();
						}
					}
				}
				base.AddSentinelRun();
				this.state = 6;
				HtmlToken htmlToken = this.htmlToken;
				htmlToken.PartMajor |= HtmlToken.TagPartMajor.End;
			}
			else if (this.state >= 24)
			{
				if (this.htmlToken.AttributeTail != 0 || this.htmlToken.NameInternal.Head != -1 || this.htmlToken.AttributeList[this.htmlToken.AttributeTail].Name.Head > 0)
				{
					base.AddSentinelRun();
					this.numCarryOverRuns = this.htmlToken.Whole.Tail - this.htmlToken.AttributeList[this.htmlToken.AttributeTail].Name.Head;
					this.carryOverRunsHeadOffset = this.htmlToken.AttributeList[this.htmlToken.AttributeTail].Name.HeadOffset;
					this.carryOverRunsLength = this.tailOffset - this.carryOverRunsHeadOffset;
					HtmlToken htmlToken2 = this.htmlToken;
					htmlToken2.Whole.Tail = htmlToken2.Whole.Tail - this.numCarryOverRuns;
				}
				else
				{
					if (this.state == 24)
					{
						if (this.htmlToken.AttributeList[this.htmlToken.AttributeTail].Name.Head == this.htmlToken.Whole.Tail)
						{
							this.AddNullRun(HtmlRunKind.Name);
						}
					}
					else if (this.state == 26 && this.htmlToken.AttributeList[this.htmlToken.AttributeTail].Value.Head == this.htmlToken.Whole.Tail)
					{
						this.AddNullRun(HtmlRunKind.AttrValue);
					}
					base.AddSentinelRun();
					this.htmlToken.AttributeTail++;
				}
			}
			else
			{
				if (this.state == 22)
				{
					if (this.htmlToken.NameInternal.Head == this.htmlToken.Whole.Tail)
					{
						this.AddNullRun(HtmlRunKind.Name);
					}
				}
				else if (this.state == 21 && this.htmlToken.Unstructured.Head == this.htmlToken.Whole.Tail)
				{
					this.AddNullRun(HtmlRunKind.TagText);
				}
				base.AddSentinelRun();
			}
			this.tokenValid = true;
		}

		// Token: 0x06001658 RID: 5720 RVA: 0x000AFD5C File Offset: 0x000ADF5C
		public int RewindTag()
		{
			if (this.state >= 24)
			{
				if (this.htmlToken.AttributeTail == 0 || this.htmlToken.AttributeList[this.htmlToken.AttributeTail - 1].IsAttrEnd)
				{
					int tail = this.htmlToken.Whole.Tail;
					Array.Copy(this.htmlToken.RunList, tail, this.htmlToken.RunList, 0, this.numCarryOverRuns);
					this.htmlToken.Whole.Head = 0;
					this.htmlToken.Whole.HeadOffset = this.carryOverRunsHeadOffset;
					this.htmlToken.Whole.Tail = this.numCarryOverRuns;
					this.numCarryOverRuns = 0;
					this.htmlToken.AttributeList[0] = this.htmlToken.AttributeList[this.htmlToken.AttributeTail];
					this.htmlToken.PartMinor = (HtmlToken.TagPartMinor)this.htmlToken.AttributeList[0].MajorPart;
					if (this.htmlToken.AttributeList[0].Name.Head != -1)
					{
						HtmlToken.AttributeEntry[] attributeList = this.htmlToken.AttributeList;
						int num = 0;
						attributeList[num].Name.Head = attributeList[num].Name.Head - tail;
					}
					if (this.htmlToken.AttributeList[0].LocalName.Head != -1)
					{
						HtmlToken.AttributeEntry[] attributeList2 = this.htmlToken.AttributeList;
						int num2 = 0;
						attributeList2[num2].LocalName.Head = attributeList2[num2].LocalName.Head - tail;
					}
					if (this.htmlToken.AttributeList[0].Value.Head != -1)
					{
						HtmlToken.AttributeEntry[] attributeList3 = this.htmlToken.AttributeList;
						int num3 = 0;
						attributeList3[num3].Value.Head = attributeList3[num3].Value.Head - tail;
					}
				}
				else
				{
					this.htmlToken.Whole.Initialize(0, this.tailOffset);
					this.htmlToken.AttributeList[0].NameIndex = this.htmlToken.AttributeList[this.htmlToken.AttributeTail - 1].NameIndex;
					this.htmlToken.AttributeList[0].PartMajor = HtmlToken.AttrPartMajor.Continue;
					HtmlToken.AttrPartMinor partMinor = this.htmlToken.AttributeList[this.htmlToken.AttributeTail - 1].PartMinor;
					if (partMinor == HtmlToken.AttrPartMinor.BeginName || partMinor == HtmlToken.AttrPartMinor.ContinueName)
					{
						this.htmlToken.AttributeList[0].PartMinor = HtmlToken.AttrPartMinor.ContinueName;
					}
					else if (partMinor == HtmlToken.AttrPartMinor.EndNameWithBeginValue || partMinor == HtmlToken.AttrPartMinor.CompleteNameWithBeginValue || partMinor == HtmlToken.AttrPartMinor.BeginValue || partMinor == HtmlToken.AttrPartMinor.ContinueValue)
					{
						this.htmlToken.AttributeList[0].PartMinor = HtmlToken.AttrPartMinor.ContinueValue;
					}
					else
					{
						this.htmlToken.AttributeList[0].PartMinor = HtmlToken.AttrPartMinor.Empty;
					}
					this.htmlToken.AttributeList[0].IsAttrDeleted = false;
					this.htmlToken.AttributeList[0].IsAttrValueQuoted = this.htmlToken.AttributeList[this.htmlToken.AttributeTail - 1].IsAttrValueQuoted;
					this.htmlToken.AttributeList[0].QuoteChar = this.htmlToken.AttributeList[this.htmlToken.AttributeTail - 1].QuoteChar;
					if (this.state == 24)
					{
						this.htmlToken.AttributeList[0].Name.Initialize(0, this.tailOffset);
						this.htmlToken.AttributeList[0].LocalName.Initialize(0, this.tailOffset);
					}
					else
					{
						this.htmlToken.AttributeList[0].Name.Reset();
						this.htmlToken.AttributeList[0].LocalName.Reset();
					}
					if (this.state == 26)
					{
						this.htmlToken.AttributeList[0].Value.Initialize(0, this.tailOffset);
					}
					else
					{
						this.htmlToken.AttributeList[0].Value.Reset();
					}
					this.htmlToken.PartMinor = (HtmlToken.TagPartMinor)this.htmlToken.AttributeList[0].MajorPart;
				}
			}
			else
			{
				this.htmlToken.Whole.Initialize(0, this.tailOffset);
				if (this.htmlToken.PartMinor == HtmlToken.TagPartMinor.BeginName || this.htmlToken.PartMinor == HtmlToken.TagPartMinor.ContinueName)
				{
					this.htmlToken.PartMinor = HtmlToken.TagPartMinor.ContinueName;
				}
				else
				{
					this.htmlToken.PartMinor = HtmlToken.TagPartMinor.Empty;
				}
				if (this.htmlToken.AttributeList != null)
				{
					this.htmlToken.AttributeList[0].PartMajor = HtmlToken.AttrPartMajor.None;
					this.htmlToken.AttributeList[0].PartMinor = HtmlToken.AttrPartMinor.Empty;
				}
			}
			if (this.state == 21)
			{
				this.htmlToken.Unstructured.Initialize(0, this.tailOffset);
			}
			else
			{
				this.htmlToken.Unstructured.Reset();
			}
			if (this.state == 22)
			{
				this.htmlToken.NameInternal.Initialize(0, this.tailOffset);
				this.htmlToken.LocalName.Initialize(0, this.tailOffset);
			}
			else
			{
				this.htmlToken.NameInternal.Reset();
				this.htmlToken.LocalName.Reset();
			}
			this.htmlToken.AttributeTail = 0;
			this.htmlToken.CurrentAttribute = -1;
			this.htmlToken.PartMajor = HtmlToken.TagPartMajor.Continue;
			this.tokenValid = false;
			return this.htmlToken.Whole.HeadOffset;
		}

		// Token: 0x06001659 RID: 5721 RVA: 0x000B0300 File Offset: 0x000AE500
		public HtmlNameIndex LookupName(int nameLength, Token.LexicalUnit unit)
		{
			if (nameLength != 0 && nameLength <= 14)
			{
				short num = (short)((ulong)(this.token.CalculateHashLowerCase(unit) ^ 221) % 601UL);
				int num2 = (int)HtmlNameData.nameHashTable[(int)num];
				if (num2 > 0)
				{
					for (;;)
					{
						string name = HtmlNameData.Names[num2].Name;
						if (name.Length == nameLength)
						{
							if (this.token.IsContiguous(unit))
							{
								if (name[0] == ParseSupport.ToLowerCase(this.token.Buffer[unit.HeadOffset]) && (nameLength == 1 || this.token.CaseInsensitiveCompareRunEqual(unit.HeadOffset + 1, name, 1)))
								{
									break;
								}
							}
							else if (this.token.CaseInsensitiveCompareEqual(unit, name))
							{
								goto Block_7;
							}
						}
						num2++;
						if (HtmlNameData.Names[num2].Hash != num)
						{
							return HtmlNameIndex.Unknown;
						}
					}
					return (HtmlNameIndex)num2;
					Block_7:
					return (HtmlNameIndex)num2;
				}
			}
			return HtmlNameIndex.Unknown;
		}

		// Token: 0x0600165A RID: 5722 RVA: 0x000B03DB File Offset: 0x000AE5DB
		public bool PrepareToAddMoreRuns(int numRuns, int start, HtmlRunKind skippedRunKind)
		{
			return base.PrepareToAddMoreRuns(numRuns, start, (uint)skippedRunKind);
		}

		// Token: 0x0600165B RID: 5723 RVA: 0x000B03E6 File Offset: 0x000AE5E6
		public void AddInvalidRun(int end, HtmlRunKind kind)
		{
			base.AddInvalidRun(end, (uint)kind);
		}

		// Token: 0x0600165C RID: 5724 RVA: 0x000B03F0 File Offset: 0x000AE5F0
		public void AddNullRun(HtmlRunKind kind)
		{
			base.AddNullRun((uint)kind);
		}

		// Token: 0x0600165D RID: 5725 RVA: 0x000B03F9 File Offset: 0x000AE5F9
		public void AddRun(RunTextType textType, HtmlRunKind kind, int start, int end)
		{
			base.AddRun((RunType)2147483648U, textType, (uint)kind, start, end, 0);
		}

		// Token: 0x0600165E RID: 5726 RVA: 0x000B040C File Offset: 0x000AE60C
		public void AddLiteralRun(RunTextType textType, HtmlRunKind kind, int start, int end, int literal)
		{
			base.AddRun((RunType)3221225472U, textType, (uint)kind, start, end, literal);
		}

		// Token: 0x0600165F RID: 5727 RVA: 0x000B0420 File Offset: 0x000AE620
		protected override void Rebase(int deltaOffset)
		{
			HtmlToken htmlToken = this.htmlToken;
			htmlToken.Unstructured.HeadOffset = htmlToken.Unstructured.HeadOffset + deltaOffset;
			HtmlToken htmlToken2 = this.htmlToken;
			htmlToken2.UnstructuredPosition.RunOffset = htmlToken2.UnstructuredPosition.RunOffset + deltaOffset;
			HtmlToken htmlToken3 = this.htmlToken;
			htmlToken3.NameInternal.HeadOffset = htmlToken3.NameInternal.HeadOffset + deltaOffset;
			HtmlToken htmlToken4 = this.htmlToken;
			htmlToken4.LocalName.HeadOffset = htmlToken4.LocalName.HeadOffset + deltaOffset;
			HtmlToken htmlToken5 = this.htmlToken;
			htmlToken5.NamePosition.RunOffset = htmlToken5.NamePosition.RunOffset + deltaOffset;
			for (int i = 0; i < this.htmlToken.AttributeTail; i++)
			{
				HtmlToken.AttributeEntry[] attributeList = this.htmlToken.AttributeList;
				int num = i;
				attributeList[num].Name.HeadOffset = attributeList[num].Name.HeadOffset + deltaOffset;
				HtmlToken.AttributeEntry[] attributeList2 = this.htmlToken.AttributeList;
				int num2 = i;
				attributeList2[num2].LocalName.HeadOffset = attributeList2[num2].LocalName.HeadOffset + deltaOffset;
				HtmlToken.AttributeEntry[] attributeList3 = this.htmlToken.AttributeList;
				int num3 = i;
				attributeList3[num3].Value.HeadOffset = attributeList3[num3].Value.HeadOffset + deltaOffset;
			}
			if (this.state >= 24)
			{
				HtmlToken.AttributeEntry[] attributeList4 = this.htmlToken.AttributeList;
				int attributeTail = this.htmlToken.AttributeTail;
				attributeList4[attributeTail].Name.HeadOffset = attributeList4[attributeTail].Name.HeadOffset + deltaOffset;
				HtmlToken.AttributeEntry[] attributeList5 = this.htmlToken.AttributeList;
				int attributeTail2 = this.htmlToken.AttributeTail;
				attributeList5[attributeTail2].LocalName.HeadOffset = attributeList5[attributeTail2].LocalName.HeadOffset + deltaOffset;
				HtmlToken.AttributeEntry[] attributeList6 = this.htmlToken.AttributeList;
				int attributeTail3 = this.htmlToken.AttributeTail;
				attributeList6[attributeTail3].Value.HeadOffset = attributeList6[attributeTail3].Value.HeadOffset + deltaOffset;
			}
			HtmlToken htmlToken6 = this.htmlToken;
			htmlToken6.AttrNamePosition.RunOffset = htmlToken6.AttrNamePosition.RunOffset + deltaOffset;
			HtmlToken htmlToken7 = this.htmlToken;
			htmlToken7.AttrValuePosition.RunOffset = htmlToken7.AttrValuePosition.RunOffset + deltaOffset;
			this.carryOverRunsHeadOffset += deltaOffset;
			base.Rebase(deltaOffset);
		}

		// Token: 0x0400192F RID: 6447
		protected const byte BuildStateEndedHtml = 6;

		// Token: 0x04001930 RID: 6448
		protected const byte BuildStateTagStarted = 20;

		// Token: 0x04001931 RID: 6449
		protected const byte BuildStateTagText = 21;

		// Token: 0x04001932 RID: 6450
		protected const byte BuildStateTagName = 22;

		// Token: 0x04001933 RID: 6451
		protected const byte BuildStateTagBeforeAttr = 23;

		// Token: 0x04001934 RID: 6452
		protected const byte BuildStateTagAttrName = 24;

		// Token: 0x04001935 RID: 6453
		protected const byte BuildStateTagEndAttrName = 25;

		// Token: 0x04001936 RID: 6454
		protected const byte BuildStateTagAttrValue = 26;

		// Token: 0x04001937 RID: 6455
		protected const byte BuildStateTagEndAttrValue = 27;

		// Token: 0x04001938 RID: 6456
		protected HtmlToken htmlToken;

		// Token: 0x04001939 RID: 6457
		protected int maxAttrs;

		// Token: 0x0400193A RID: 6458
		protected int numCarryOverRuns;

		// Token: 0x0400193B RID: 6459
		protected int carryOverRunsHeadOffset;

		// Token: 0x0400193C RID: 6460
		protected int carryOverRunsLength;
	}
}
