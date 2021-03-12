using System;
using System.Collections.Generic;
using Microsoft.Exchange.Management.Analysis.Builders;
using Microsoft.Exchange.Management.Analysis.Features;
using Microsoft.Exchange.Management.Deployment;

namespace Microsoft.Exchange.Management.Analysis
{
	// Token: 0x02000047 RID: 71
	internal sealed class Rule : AnalysisMember<bool>
	{
		// Token: 0x060001F2 RID: 498 RVA: 0x00007B1D File Offset: 0x00005D1D
		private Rule(Func<AnalysisMember> parent, ConcurrencyType runAs, Analysis analysis, IEnumerable<Feature> features, Func<Result, IEnumerable<Result<bool>>> setFunction) : base(parent, runAs, analysis, features, setFunction)
		{
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x00007B54 File Offset: 0x00005D54
		public static RuleParentBuilder Build()
		{
			RuleBuildContext context = new RuleBuildContext((RuleBuildContext x) => new Rule(x.Parent, x.RunAs, x.Analysis, x.Features, x.SetFunction));
			return new RuleParentBuilder(context);
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x00007B8C File Offset: 0x00005D8C
		public string GetHelpId()
		{
			HelpId helpId;
			if (!Enum.TryParse<HelpId>(base.Name, out helpId))
			{
				throw new ArgumentException(Strings.HelpIdNotDefined(base.Name));
			}
			return helpId.ToString();
		}
	}
}
