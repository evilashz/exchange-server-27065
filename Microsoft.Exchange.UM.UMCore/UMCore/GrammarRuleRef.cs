using System;
using System.Globalization;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000155 RID: 341
	internal class GrammarRuleRef : GrammarItemBase
	{
		// Token: 0x06000A2E RID: 2606 RVA: 0x0002B881 File Offset: 0x00029A81
		internal GrammarRuleRef(CustomGrammarBase grammar, bool hoistVariables) : base(1f)
		{
			this.grammar = grammar;
			this.hoistVariables = hoistVariables;
		}

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06000A2F RID: 2607 RVA: 0x0002B89C File Offset: 0x00029A9C
		public override bool IsEmpty
		{
			get
			{
				return this.grammar.IsEmpty;
			}
		}

		// Token: 0x06000A30 RID: 2608 RVA: 0x0002B8AC File Offset: 0x00029AAC
		public override bool Equals(GrammarItemBase otherItemBase)
		{
			GrammarRuleRef grammarRuleRef = otherItemBase as GrammarRuleRef;
			return grammarRuleRef != null && string.Equals(grammarRuleRef.grammar.FileName, this.grammar.FileName, StringComparison.InvariantCultureIgnoreCase) && string.Equals(grammarRuleRef.grammar.Rule, this.grammar.Rule, StringComparison.InvariantCultureIgnoreCase);
		}

		// Token: 0x06000A31 RID: 2609 RVA: 0x0002B904 File Offset: 0x00029B04
		protected override string GetInnerItem()
		{
			if (!this.hoistVariables)
			{
				return string.Format(CultureInfo.InvariantCulture, "\r\n                <ruleref uri=\"{0}#{1}\"/>", new object[]
				{
					this.grammar.FileName,
					this.grammar.Rule
				});
			}
			return string.Format(CultureInfo.InvariantCulture, "\r\n                <ruleref uri=\"{0}#{1}\"/>\r\n                <tag>out=rules.latest();</tag>", new object[]
			{
				this.grammar.FileName,
				this.grammar.Rule
			});
		}

		// Token: 0x06000A32 RID: 2610 RVA: 0x0002B980 File Offset: 0x00029B80
		protected override int InternalGetHashCode()
		{
			return this.grammar.FileName.GetHashCode() ^ this.grammar.Rule.GetHashCode();
		}

		// Token: 0x0400093C RID: 2364
		private readonly CustomGrammarBase grammar;

		// Token: 0x0400093D RID: 2365
		private readonly bool hoistVariables;
	}
}
