using System;
using Microsoft.Exchange.Data.TextConverters.Internal.Html;

namespace Microsoft.Exchange.Data.TextConverters.Internal.Css
{
	// Token: 0x020001B9 RID: 441
	internal class CssTokenBuilder : TokenBuilder
	{
		// Token: 0x06001297 RID: 4759 RVA: 0x000842D8 File Offset: 0x000824D8
		public CssTokenBuilder(char[] buffer, int maxProperties, int maxSelectors, int maxRuns, bool testBoundaryConditions) : base(new CssToken(), buffer, maxRuns, testBoundaryConditions)
		{
			this.cssToken = (CssToken)base.Token;
			int num = 16;
			int num2 = 16;
			if (!testBoundaryConditions)
			{
				this.maxProperties = maxProperties;
				this.maxSelectors = maxSelectors;
			}
			else
			{
				num = 1;
				num2 = 1;
				this.maxProperties = 5;
				this.maxSelectors = 5;
			}
			this.cssToken.PropertyList = new CssToken.PropertyEntry[num];
			this.cssToken.SelectorList = new CssToken.SelectorEntry[num2];
		}

		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x06001298 RID: 4760 RVA: 0x00084354 File Offset: 0x00082554
		public new CssToken Token
		{
			get
			{
				return this.cssToken;
			}
		}

		// Token: 0x17000520 RID: 1312
		// (get) Token: 0x06001299 RID: 4761 RVA: 0x0008435C File Offset: 0x0008255C
		public bool Incomplete
		{
			get
			{
				return this.state >= 10 && this.state != 10;
			}
		}

		// Token: 0x0600129A RID: 4762 RVA: 0x00084377 File Offset: 0x00082577
		public override void Reset()
		{
			if (this.state >= 6)
			{
				this.cssToken.Reset();
			}
			base.Reset();
		}

		// Token: 0x0600129B RID: 4763 RVA: 0x00084393 File Offset: 0x00082593
		public void StartRuleSet(int baseOffset, CssTokenId id)
		{
			this.state = 23;
			this.cssToken.TokenId = (TokenId)id;
			this.cssToken.Whole.HeadOffset = baseOffset;
			this.tailOffset = baseOffset;
		}

		// Token: 0x0600129C RID: 4764 RVA: 0x000843C1 File Offset: 0x000825C1
		public void EndRuleSet()
		{
			if (this.state >= 43)
			{
				this.EndDeclarations();
			}
			this.tokenValid = true;
			this.state = 6;
			this.token.WholePosition.Rewind(this.token.Whole);
		}

		// Token: 0x0600129D RID: 4765 RVA: 0x000843FC File Offset: 0x000825FC
		public void BuildUniversalSelector()
		{
			this.StartSelectorName();
			this.EndSelectorName(0);
		}

		// Token: 0x0600129E RID: 4766 RVA: 0x0008440B File Offset: 0x0008260B
		public bool CanAddSelector()
		{
			return this.cssToken.SelectorTail - this.cssToken.SelectorHead < this.maxSelectors;
		}

		// Token: 0x0600129F RID: 4767 RVA: 0x0008442C File Offset: 0x0008262C
		public void StartSelectorName()
		{
			if (this.cssToken.SelectorTail == this.cssToken.SelectorList.Length)
			{
				int num;
				if (this.maxSelectors / 2 > this.cssToken.SelectorList.Length)
				{
					num = this.cssToken.SelectorList.Length * 2;
				}
				else
				{
					num = this.maxSelectors;
				}
				CssToken.SelectorEntry[] array = new CssToken.SelectorEntry[num];
				Array.Copy(this.cssToken.SelectorList, 0, array, 0, this.cssToken.SelectorTail);
				this.cssToken.SelectorList = array;
			}
			this.cssToken.SelectorList[this.cssToken.SelectorTail].NameId = HtmlNameIndex.Unknown;
			this.cssToken.SelectorList[this.cssToken.SelectorTail].Name.Initialize(this.cssToken.Whole.Tail, this.tailOffset);
			this.cssToken.SelectorList[this.cssToken.SelectorTail].ClassName.Reset();
			this.state = 24;
		}

		// Token: 0x060012A0 RID: 4768 RVA: 0x00084540 File Offset: 0x00082740
		public void EndSelectorName(int nameLength)
		{
			this.cssToken.SelectorList[this.cssToken.SelectorTail].Name.Tail = this.cssToken.Whole.Tail;
			this.cssToken.SelectorList[this.cssToken.SelectorTail].NameId = this.LookupTagName(nameLength, this.cssToken.SelectorList[this.cssToken.SelectorTail].Name);
			this.state = 25;
		}

		// Token: 0x060012A1 RID: 4769 RVA: 0x000845D4 File Offset: 0x000827D4
		public void StartSelectorClass(CssSelectorClassType classType)
		{
			this.cssToken.SelectorList[this.cssToken.SelectorTail].ClassName.Initialize(this.cssToken.Whole.Tail, this.tailOffset);
			this.cssToken.SelectorList[this.cssToken.SelectorTail].ClassType = classType;
			this.state = 26;
		}

		// Token: 0x060012A2 RID: 4770 RVA: 0x00084645 File Offset: 0x00082845
		public void EndSelectorClass()
		{
			this.cssToken.SelectorList[this.cssToken.SelectorTail].ClassName.Tail = this.cssToken.Whole.Tail;
			this.state = 27;
		}

		// Token: 0x060012A3 RID: 4771 RVA: 0x00084684 File Offset: 0x00082884
		public void SetSelectorCombinator(CssSelectorCombinator combinator, bool previous)
		{
			int num = this.cssToken.SelectorTail;
			if (previous)
			{
				num--;
			}
			this.cssToken.SelectorList[num].Combinator = combinator;
		}

		// Token: 0x060012A4 RID: 4772 RVA: 0x000846BB File Offset: 0x000828BB
		public void EndSimpleSelector()
		{
			this.cssToken.SelectorTail++;
		}

		// Token: 0x060012A5 RID: 4773 RVA: 0x000846D0 File Offset: 0x000828D0
		public void StartDeclarations(int baseOffset)
		{
			this.state = 43;
			if (this.cssToken.TokenId == TokenId.None)
			{
				this.cssToken.TokenId = (TokenId)5;
			}
			this.cssToken.PartMajor = CssToken.PropertyListPartMajor.Begin;
			this.cssToken.PartMinor = CssToken.PropertyListPartMinor.Empty;
			this.cssToken.Whole.HeadOffset = baseOffset;
			this.tailOffset = baseOffset;
		}

		// Token: 0x060012A6 RID: 4774 RVA: 0x0008472E File Offset: 0x0008292E
		public bool CanAddProperty()
		{
			return this.cssToken.PropertyTail - this.cssToken.PropertyHead < this.maxProperties;
		}

		// Token: 0x060012A7 RID: 4775 RVA: 0x00084750 File Offset: 0x00082950
		public void StartPropertyName()
		{
			if (this.cssToken.PropertyTail == this.cssToken.PropertyList.Length)
			{
				int num;
				if (this.maxProperties / 2 > this.cssToken.PropertyList.Length)
				{
					num = this.cssToken.PropertyList.Length * 2;
				}
				else
				{
					num = this.maxProperties;
				}
				CssToken.PropertyEntry[] array = new CssToken.PropertyEntry[num];
				Array.Copy(this.cssToken.PropertyList, 0, array, 0, this.cssToken.PropertyTail);
				this.cssToken.PropertyList = array;
			}
			if (this.cssToken.PartMinor == CssToken.PropertyListPartMinor.Empty)
			{
				this.cssToken.PartMinor = CssToken.PropertyListPartMinor.BeginProperty;
			}
			this.cssToken.PropertyList[this.cssToken.PropertyTail].NameId = CssNameIndex.Unknown;
			this.cssToken.PropertyList[this.cssToken.PropertyTail].PartMajor = CssToken.PropertyPartMajor.Begin;
			this.cssToken.PropertyList[this.cssToken.PropertyTail].PartMinor = CssToken.PropertyPartMinor.BeginName;
			this.cssToken.PropertyList[this.cssToken.PropertyTail].QuoteChar = 0;
			this.cssToken.PropertyList[this.cssToken.PropertyTail].Name.Initialize(this.cssToken.Whole.Tail, this.tailOffset);
			this.cssToken.PropertyList[this.cssToken.PropertyTail].Value.Reset();
			this.state = 44;
		}

		// Token: 0x060012A8 RID: 4776 RVA: 0x000848E0 File Offset: 0x00082AE0
		public void EndPropertyName(int nameLength)
		{
			this.cssToken.PropertyList[this.cssToken.PropertyTail].Name.Tail = this.cssToken.Whole.Tail;
			CssToken.PropertyEntry[] propertyList = this.cssToken.PropertyList;
			int propertyTail = this.cssToken.PropertyTail;
			propertyList[propertyTail].PartMinor = (propertyList[propertyTail].PartMinor | CssToken.PropertyPartMinor.EndName);
			if (this.cssToken.PropertyList[this.cssToken.PropertyTail].IsPropertyBegin)
			{
				this.cssToken.PropertyList[this.cssToken.PropertyTail].NameId = this.LookupName(nameLength, this.cssToken.PropertyList[this.cssToken.PropertyTail].Name);
			}
			this.state = 45;
		}

		// Token: 0x060012A9 RID: 4777 RVA: 0x000849BC File Offset: 0x00082BBC
		public void StartPropertyValue()
		{
			this.cssToken.PropertyList[this.cssToken.PropertyTail].Value.Initialize(this.cssToken.Whole.Tail, this.tailOffset);
			CssToken.PropertyEntry[] propertyList = this.cssToken.PropertyList;
			int propertyTail = this.cssToken.PropertyTail;
			propertyList[propertyTail].PartMinor = (propertyList[propertyTail].PartMinor | CssToken.PropertyPartMinor.BeginValue);
			this.state = 46;
		}

		// Token: 0x060012AA RID: 4778 RVA: 0x00084A38 File Offset: 0x00082C38
		public void SetPropertyValueQuote(char ch)
		{
			this.cssToken.PropertyList[this.cssToken.PropertyTail].IsPropertyValueQuoted = true;
			this.cssToken.PropertyList[this.cssToken.PropertyTail].QuoteChar = (byte)ch;
		}

		// Token: 0x060012AB RID: 4779 RVA: 0x00084A88 File Offset: 0x00082C88
		public void EndPropertyValue()
		{
			this.cssToken.PropertyList[this.cssToken.PropertyTail].Value.Tail = this.cssToken.Whole.Tail;
			CssToken.PropertyEntry[] propertyList = this.cssToken.PropertyList;
			int propertyTail = this.cssToken.PropertyTail;
			propertyList[propertyTail].PartMinor = (propertyList[propertyTail].PartMinor | CssToken.PropertyPartMinor.EndValue);
			this.state = 47;
		}

		// Token: 0x060012AC RID: 4780 RVA: 0x00084AFC File Offset: 0x00082CFC
		public void EndProperty()
		{
			CssToken.PropertyEntry[] propertyList = this.cssToken.PropertyList;
			int propertyTail = this.cssToken.PropertyTail;
			propertyList[propertyTail].PartMajor = (propertyList[propertyTail].PartMajor | CssToken.PropertyPartMajor.End);
			this.cssToken.PropertyTail++;
			if (this.cssToken.PropertyTail < this.cssToken.PropertyList.Length)
			{
				this.cssToken.PropertyList[this.cssToken.PropertyTail].PartMajor = CssToken.PropertyPartMajor.None;
				this.cssToken.PropertyList[this.cssToken.PropertyTail].PartMinor = CssToken.PropertyPartMinor.Empty;
			}
			if (this.cssToken.PartMinor == CssToken.PropertyListPartMinor.BeginProperty)
			{
				this.cssToken.PartMinor = CssToken.PropertyListPartMinor.Properties;
			}
			else if (this.cssToken.PartMinor == CssToken.PropertyListPartMinor.ContinueProperty)
			{
				this.cssToken.PartMinor = CssToken.PropertyListPartMinor.EndProperty;
			}
			else
			{
				CssToken cssToken = this.cssToken;
				cssToken.PartMinor |= CssToken.PropertyListPartMinor.Properties;
			}
			this.state = 43;
		}

		// Token: 0x060012AD RID: 4781 RVA: 0x00084C00 File Offset: 0x00082E00
		public void EndDeclarations()
		{
			if (this.state != 20)
			{
				if (this.state == 44)
				{
					this.cssToken.PropertyList[this.cssToken.PropertyTail].Name.Tail = this.cssToken.Whole.Tail;
				}
				else if (this.state == 46)
				{
					this.cssToken.PropertyList[this.cssToken.PropertyTail].Value.Tail = this.cssToken.Whole.Tail;
				}
			}
			if (this.state == 44)
			{
				this.EndPropertyName(0);
			}
			else if (this.state == 46)
			{
				this.EndPropertyValue();
			}
			if (this.state == 45 || this.state == 47)
			{
				this.EndProperty();
			}
			this.state = 43;
			CssToken cssToken = this.cssToken;
			cssToken.PartMajor |= CssToken.PropertyListPartMajor.End;
			this.tokenValid = true;
		}

		// Token: 0x060012AE RID: 4782 RVA: 0x00084CFA File Offset: 0x00082EFA
		public bool PrepareAndAddRun(CssRunKind cssRunKind, int start, int end)
		{
			if (end != start)
			{
				if (!base.PrepareToAddMoreRuns(1))
				{
					return false;
				}
				base.AddRun((cssRunKind == CssRunKind.Invalid) ? RunType.Invalid : ((RunType)2147483648U), (cssRunKind == CssRunKind.Space) ? RunTextType.Space : RunTextType.NonSpace, (uint)cssRunKind, start, end, 0);
			}
			return true;
		}

		// Token: 0x060012AF RID: 4783 RVA: 0x00084D35 File Offset: 0x00082F35
		public bool PrepareAndAddInvalidRun(CssRunKind cssRunKind, int end)
		{
			if (!base.PrepareToAddMoreRuns(1))
			{
				return false;
			}
			base.AddInvalidRun(end, (uint)cssRunKind);
			return true;
		}

		// Token: 0x060012B0 RID: 4784 RVA: 0x00084D4B File Offset: 0x00082F4B
		public bool PrepareAndAddLiteralRun(CssRunKind cssRunKind, int start, int end, int value)
		{
			if (end != start)
			{
				if (!base.PrepareToAddMoreRuns(1))
				{
					return false;
				}
				base.AddRun((RunType)3221225472U, RunTextType.NonSpace, (uint)cssRunKind, start, end, value);
			}
			return true;
		}

		// Token: 0x060012B1 RID: 4785 RVA: 0x00084D74 File Offset: 0x00082F74
		public void InvalidateLastValidRun(CssRunKind kind)
		{
			int num = this.token.Whole.Tail;
			Token.RunEntry runEntry;
			for (;;)
			{
				num--;
				runEntry = this.token.RunList[num];
				if (runEntry.Type != RunType.Invalid)
				{
					break;
				}
				if (num <= 0)
				{
					return;
				}
			}
			if (kind == (CssRunKind)runEntry.Kind)
			{
				this.token.RunList[num].Initialize(RunType.Invalid, runEntry.TextType, runEntry.Kind, runEntry.Length, runEntry.Value);
				return;
			}
		}

		// Token: 0x060012B2 RID: 4786 RVA: 0x00084DF8 File Offset: 0x00082FF8
		public void MarkPropertyAsDeleted()
		{
			this.cssToken.PropertyList[this.cssToken.PropertyTail].IsPropertyDeleted = true;
		}

		// Token: 0x060012B3 RID: 4787 RVA: 0x00084E1B File Offset: 0x0008301B
		public CssTokenId MakeEmptyToken(CssTokenId tokenId)
		{
			return (CssTokenId)base.MakeEmptyToken((TokenId)tokenId);
		}

		// Token: 0x060012B4 RID: 4788 RVA: 0x00084E24 File Offset: 0x00083024
		public CssNameIndex LookupName(int nameLength, Token.Fragment fragment)
		{
			if (nameLength > 26)
			{
				return CssNameIndex.Unknown;
			}
			short num = (short)((ulong)(this.token.CalculateHashLowerCase(fragment) ^ 2) % 329UL);
			int num2 = (int)CssData.nameHashTable[(int)num];
			if (num2 > 0)
			{
				for (;;)
				{
					string name = CssData.names[num2].Name;
					if (name.Length == nameLength)
					{
						if (fragment.Tail == fragment.Head + 1)
						{
							if (name[0] == ParseSupport.ToLowerCase(this.token.Buffer[fragment.HeadOffset]) && (nameLength == 1 || this.token.CaseInsensitiveCompareRunEqual(fragment.HeadOffset + 1, name, 1)))
							{
								break;
							}
						}
						else if (this.token.CaseInsensitiveCompareEqual(ref fragment, name))
						{
							goto Block_6;
						}
					}
					num2++;
					if (CssData.names[num2].Hash != num)
					{
						return CssNameIndex.Unknown;
					}
				}
				return (CssNameIndex)num2;
				Block_6:
				return (CssNameIndex)num2;
			}
			return CssNameIndex.Unknown;
		}

		// Token: 0x060012B5 RID: 4789 RVA: 0x00084EFC File Offset: 0x000830FC
		public HtmlNameIndex LookupTagName(int nameLength, Token.Fragment fragment)
		{
			if (nameLength > 14)
			{
				return HtmlNameIndex.Unknown;
			}
			short num = (short)((ulong)(this.token.CalculateHashLowerCase(fragment) ^ 221) % 601UL);
			int num2 = (int)HtmlNameData.nameHashTable[(int)num];
			if (num2 > 0)
			{
				for (;;)
				{
					string name = HtmlNameData.Names[num2].Name;
					if (name.Length == nameLength)
					{
						if (fragment.Tail == fragment.Head + 1)
						{
							if (name[0] == ParseSupport.ToLowerCase(this.token.Buffer[fragment.HeadOffset]) && (nameLength == 1 || this.token.CaseInsensitiveCompareRunEqual(fragment.HeadOffset + 1, name, 1)))
							{
								break;
							}
						}
						else if (this.token.CaseInsensitiveCompareEqual(ref fragment, name))
						{
							goto Block_6;
						}
					}
					num2++;
					if (HtmlNameData.Names[num2].Hash != num)
					{
						return HtmlNameIndex.Unknown;
					}
				}
				return (HtmlNameIndex)num2;
				Block_6:
				return (HtmlNameIndex)num2;
			}
			return HtmlNameIndex.Unknown;
		}

		// Token: 0x04001315 RID: 4885
		protected const byte BuildStateEndedCss = 6;

		// Token: 0x04001316 RID: 4886
		protected const byte BuildStatePropertyListStarted = 20;

		// Token: 0x04001317 RID: 4887
		protected const byte BuildStateBeforeSelector = 23;

		// Token: 0x04001318 RID: 4888
		protected const byte BuildStateSelectorName = 24;

		// Token: 0x04001319 RID: 4889
		protected const byte BuildStateEndSelectorName = 25;

		// Token: 0x0400131A RID: 4890
		protected const byte BuildStateSelectorClass = 26;

		// Token: 0x0400131B RID: 4891
		protected const byte BuildStateEndSelectorClass = 27;

		// Token: 0x0400131C RID: 4892
		protected const byte BuildStateBeforeProperty = 43;

		// Token: 0x0400131D RID: 4893
		protected const byte BuildStatePropertyName = 44;

		// Token: 0x0400131E RID: 4894
		protected const byte BuildStateEndPropertyName = 45;

		// Token: 0x0400131F RID: 4895
		protected const byte BuildStatePropertyValue = 46;

		// Token: 0x04001320 RID: 4896
		protected const byte BuildStateEndPropertyValue = 47;

		// Token: 0x04001321 RID: 4897
		protected CssToken cssToken;

		// Token: 0x04001322 RID: 4898
		protected int maxProperties;

		// Token: 0x04001323 RID: 4899
		protected int maxSelectors;
	}
}
