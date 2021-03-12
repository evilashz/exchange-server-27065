using System;
using System.Diagnostics;

namespace Microsoft.Exchange.Data.TextConverters.Internal.Html
{
	// Token: 0x02000223 RID: 547
	internal struct HtmlAttribute
	{
		// Token: 0x06001623 RID: 5667 RVA: 0x000AEC3F File Offset: 0x000ACE3F
		internal HtmlAttribute(HtmlToken token)
		{
			this.token = token;
		}

		// Token: 0x17000592 RID: 1426
		// (get) Token: 0x06001624 RID: 5668 RVA: 0x000AEC48 File Offset: 0x000ACE48
		public bool IsNull
		{
			get
			{
				return this.token == null;
			}
		}

		// Token: 0x17000593 RID: 1427
		// (get) Token: 0x06001625 RID: 5669 RVA: 0x000AEC53 File Offset: 0x000ACE53
		public int Index
		{
			get
			{
				return this.token.CurrentAttribute;
			}
		}

		// Token: 0x17000594 RID: 1428
		// (get) Token: 0x06001626 RID: 5670 RVA: 0x000AEC60 File Offset: 0x000ACE60
		public HtmlToken.AttrPartMajor MajorPart
		{
			get
			{
				return this.token.AttributeList[this.token.CurrentAttribute].MajorPart;
			}
		}

		// Token: 0x17000595 RID: 1429
		// (get) Token: 0x06001627 RID: 5671 RVA: 0x000AEC82 File Offset: 0x000ACE82
		public HtmlToken.AttrPartMinor MinorPart
		{
			get
			{
				return this.token.AttributeList[this.token.CurrentAttribute].MinorPart;
			}
		}

		// Token: 0x17000596 RID: 1430
		// (get) Token: 0x06001628 RID: 5672 RVA: 0x000AECA4 File Offset: 0x000ACEA4
		public bool IsCompleteAttr
		{
			get
			{
				return this.token.AttributeList[this.token.CurrentAttribute].IsCompleteAttr;
			}
		}

		// Token: 0x17000597 RID: 1431
		// (get) Token: 0x06001629 RID: 5673 RVA: 0x000AECC6 File Offset: 0x000ACEC6
		public bool IsAttrBegin
		{
			get
			{
				return this.token.AttributeList[this.token.CurrentAttribute].IsAttrBegin;
			}
		}

		// Token: 0x17000598 RID: 1432
		// (get) Token: 0x0600162A RID: 5674 RVA: 0x000AECE8 File Offset: 0x000ACEE8
		public bool IsAttrEmptyName
		{
			get
			{
				return this.token.AttributeList[this.token.CurrentAttribute].IsAttrEmptyName;
			}
		}

		// Token: 0x17000599 RID: 1433
		// (get) Token: 0x0600162B RID: 5675 RVA: 0x000AED0A File Offset: 0x000ACF0A
		public bool IsAttrEnd
		{
			get
			{
				return this.token.AttributeList[this.token.CurrentAttribute].IsAttrEnd;
			}
		}

		// Token: 0x1700059A RID: 1434
		// (get) Token: 0x0600162C RID: 5676 RVA: 0x000AED2C File Offset: 0x000ACF2C
		public bool IsAttrNameEnd
		{
			get
			{
				return this.token.AttributeList[this.token.CurrentAttribute].IsAttrNameEnd;
			}
		}

		// Token: 0x1700059B RID: 1435
		// (get) Token: 0x0600162D RID: 5677 RVA: 0x000AED4E File Offset: 0x000ACF4E
		public bool IsDeleted
		{
			get
			{
				return this.token.AttributeList[this.token.CurrentAttribute].IsAttrDeleted;
			}
		}

		// Token: 0x1700059C RID: 1436
		// (get) Token: 0x0600162E RID: 5678 RVA: 0x000AED70 File Offset: 0x000ACF70
		public bool IsAttrValueBegin
		{
			get
			{
				return this.token.AttributeList[this.token.CurrentAttribute].IsAttrValueBegin;
			}
		}

		// Token: 0x1700059D RID: 1437
		// (get) Token: 0x0600162F RID: 5679 RVA: 0x000AED92 File Offset: 0x000ACF92
		public bool IsAttrValueQuoted
		{
			get
			{
				return this.token.AttributeList[this.token.CurrentAttribute].IsAttrValueQuoted;
			}
		}

		// Token: 0x1700059E RID: 1438
		// (get) Token: 0x06001630 RID: 5680 RVA: 0x000AEDB4 File Offset: 0x000ACFB4
		public HtmlNameIndex NameIndex
		{
			get
			{
				return this.token.AttributeList[this.token.CurrentAttribute].NameIndex;
			}
		}

		// Token: 0x1700059F RID: 1439
		// (get) Token: 0x06001631 RID: 5681 RVA: 0x000AEDD6 File Offset: 0x000ACFD6
		public char QuoteChar
		{
			get
			{
				return (char)this.token.AttributeList[this.token.CurrentAttribute].QuoteChar;
			}
		}

		// Token: 0x170005A0 RID: 1440
		// (get) Token: 0x06001632 RID: 5682 RVA: 0x000AEDF8 File Offset: 0x000ACFF8
		public bool AttributeValueContainsDangerousCharacter
		{
			get
			{
				return this.token.AttributeList[this.token.CurrentAttribute].DangerousCharacters != 0;
			}
		}

		// Token: 0x170005A1 RID: 1441
		// (get) Token: 0x06001633 RID: 5683 RVA: 0x000AEE20 File Offset: 0x000AD020
		public bool AttributeValueContainsBackquote
		{
			get
			{
				return (this.token.AttributeList[this.token.CurrentAttribute].DangerousCharacters & 1) != 0;
			}
		}

		// Token: 0x170005A2 RID: 1442
		// (get) Token: 0x06001634 RID: 5684 RVA: 0x000AEE4A File Offset: 0x000AD04A
		public bool AttributeValueContainsBackslash
		{
			get
			{
				return (this.token.AttributeList[this.token.CurrentAttribute].DangerousCharacters & 2) != 0;
			}
		}

		// Token: 0x170005A3 RID: 1443
		// (get) Token: 0x06001635 RID: 5685 RVA: 0x000AEE74 File Offset: 0x000AD074
		public bool HasNameFragment
		{
			get
			{
				return !this.token.IsFragmentEmpty(this.token.AttributeList[this.token.CurrentAttribute].Name);
			}
		}

		// Token: 0x170005A4 RID: 1444
		// (get) Token: 0x06001636 RID: 5686 RVA: 0x000AEEA4 File Offset: 0x000AD0A4
		public HtmlToken.AttributeNameTextReader Name
		{
			get
			{
				return new HtmlToken.AttributeNameTextReader(this.token);
			}
		}

		// Token: 0x170005A5 RID: 1445
		// (get) Token: 0x06001637 RID: 5687 RVA: 0x000AEEB1 File Offset: 0x000AD0B1
		public bool HasValueFragment
		{
			get
			{
				return !this.token.IsFragmentEmpty(this.token.AttributeList[this.token.CurrentAttribute].Value);
			}
		}

		// Token: 0x170005A6 RID: 1446
		// (get) Token: 0x06001638 RID: 5688 RVA: 0x000AEEE1 File Offset: 0x000AD0E1
		public HtmlToken.AttributeValueTextReader Value
		{
			get
			{
				return new HtmlToken.AttributeValueTextReader(this.token);
			}
		}

		// Token: 0x06001639 RID: 5689 RVA: 0x000AEEEE File Offset: 0x000AD0EE
		public void SetMinorPart(HtmlToken.AttrPartMinor newMinorPart)
		{
			this.token.AttributeList[this.token.CurrentAttribute].MinorPart = newMinorPart;
		}

		// Token: 0x0600163A RID: 5690 RVA: 0x000AEF11 File Offset: 0x000AD111
		[Conditional("DEBUG")]
		private void AssertCurrent()
		{
		}

		// Token: 0x0400192E RID: 6446
		private HtmlToken token;
	}
}
