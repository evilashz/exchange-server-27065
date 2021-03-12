using System;
using System.Diagnostics;
using Microsoft.Exchange.Data.TextConverters.Internal.Html;

namespace Microsoft.Exchange.Data.TextConverters.Internal.Css
{
	// Token: 0x020001AA RID: 426
	internal struct CssSelector
	{
		// Token: 0x0600121F RID: 4639 RVA: 0x00082F17 File Offset: 0x00081117
		internal CssSelector(CssToken token)
		{
			this.token = token;
		}

		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x06001220 RID: 4640 RVA: 0x00082F20 File Offset: 0x00081120
		public int Index
		{
			get
			{
				return this.token.CurrentSelector;
			}
		}

		// Token: 0x170004E7 RID: 1255
		// (get) Token: 0x06001221 RID: 4641 RVA: 0x00082F2D File Offset: 0x0008112D
		public bool IsDeleted
		{
			get
			{
				return this.token.SelectorList[this.token.CurrentSelector].IsSelectorDeleted;
			}
		}

		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x06001222 RID: 4642 RVA: 0x00082F4F File Offset: 0x0008114F
		public HtmlNameIndex NameId
		{
			get
			{
				return this.token.SelectorList[this.token.CurrentSelector].NameId;
			}
		}

		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x06001223 RID: 4643 RVA: 0x00082F71 File Offset: 0x00081171
		public bool HasNameFragment
		{
			get
			{
				return !this.token.SelectorList[this.token.CurrentSelector].Name.IsEmpty;
			}
		}

		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x06001224 RID: 4644 RVA: 0x00082F9B File Offset: 0x0008119B
		public CssToken.SelectorNameTextReader Name
		{
			get
			{
				return new CssToken.SelectorNameTextReader(this.token);
			}
		}

		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x06001225 RID: 4645 RVA: 0x00082FA8 File Offset: 0x000811A8
		public bool HasClassFragment
		{
			get
			{
				return !this.token.SelectorList[this.token.CurrentSelector].ClassName.IsEmpty;
			}
		}

		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x06001226 RID: 4646 RVA: 0x00082FD2 File Offset: 0x000811D2
		public CssToken.SelectorClassTextReader ClassName
		{
			get
			{
				return new CssToken.SelectorClassTextReader(this.token);
			}
		}

		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x06001227 RID: 4647 RVA: 0x00082FDF File Offset: 0x000811DF
		public CssSelectorClassType ClassType
		{
			get
			{
				return this.token.SelectorList[this.token.CurrentSelector].ClassType;
			}
		}

		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x06001228 RID: 4648 RVA: 0x00083004 File Offset: 0x00081204
		public bool IsSimple
		{
			get
			{
				return this.token.SelectorList[this.token.CurrentSelector].Combinator == CssSelectorCombinator.None && (this.token.SelectorTail == this.token.CurrentSelector + 1 || this.token.SelectorList[this.token.CurrentSelector + 1].Combinator == CssSelectorCombinator.None);
			}
		}

		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x06001229 RID: 4649 RVA: 0x00083076 File Offset: 0x00081276
		public CssSelectorCombinator Combinator
		{
			get
			{
				return this.token.SelectorList[this.token.CurrentSelector].Combinator;
			}
		}

		// Token: 0x0600122A RID: 4650 RVA: 0x00083098 File Offset: 0x00081298
		[Conditional("DEBUG")]
		private void AssertCurrent()
		{
		}

		// Token: 0x040012CE RID: 4814
		private CssToken token;
	}
}
