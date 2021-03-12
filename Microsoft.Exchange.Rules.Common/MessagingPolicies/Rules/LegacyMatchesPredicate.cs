using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x02000011 RID: 17
	internal class LegacyMatchesPredicate : PredicateCondition
	{
		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00002E65 File Offset: 0x00001065
		public List<string> Patterns
		{
			get
			{
				return this.patterns;
			}
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002E6D File Offset: 0x0000106D
		public LegacyMatchesPredicate(Property property, ShortList<string> entries, RulesCreationContext creationContext) : base(property, entries, creationContext)
		{
			this.patterns = new List<string>(entries);
			this.matchesRegexPredicate = new MatchesRegexPredicate(property, RegexUtils.ConvertLegacyRegexToTpl(entries), creationContext);
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00002E97 File Offset: 0x00001097
		public override string Name
		{
			get
			{
				return "matches";
			}
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002E9E File Offset: 0x0000109E
		public override bool Evaluate(RulesEvaluationContext context)
		{
			return this.matchesRegexPredicate.Evaluate(context);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002EAC File Offset: 0x000010AC
		protected override Value BuildValue(ShortList<string> entries, RulesCreationContext creationContext)
		{
			return Value.CreateValue(string.Empty.GetType(), entries);
		}

		// Token: 0x04000020 RID: 32
		private MatchesRegexPredicate matchesRegexPredicate;

		// Token: 0x04000021 RID: 33
		private List<string> patterns;
	}
}
