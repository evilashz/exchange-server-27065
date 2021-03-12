using System;
using Microsoft.Exchange.Management.Deployment.Analysis;

namespace Microsoft.Exchange.Management.Deployment.PrereqAnalysisSample
{
	// Token: 0x02000072 RID: 114
	internal class PrereqConclusionSetBuilder : ConclusionSetBuilderImplementation<PrereqConclusionSet, PrereqConclusion, PrereqSettingConclusion, PrereqRuleConclusion>
	{
		// Token: 0x06000A77 RID: 2679 RVA: 0x000266AC File Offset: 0x000248AC
		protected override PrereqConclusionSet BuildConclusionSet(Analysis analysis, PrereqConclusion rootConclusion)
		{
			PrereqConclusionSet prereqConclusionSet = new PrereqConclusionSet((PrereqAnalysis)analysis, rootConclusion);
			prereqConclusionSet.MakeReadOnly();
			return prereqConclusionSet;
		}

		// Token: 0x06000A78 RID: 2680 RVA: 0x000266CD File Offset: 0x000248CD
		protected override PrereqSettingConclusion BuildSettingConclusion(Result result)
		{
			return new PrereqSettingConclusion(result);
		}

		// Token: 0x06000A79 RID: 2681 RVA: 0x000266D5 File Offset: 0x000248D5
		protected override PrereqRuleConclusion BuildRuleConclusion(RuleResult ruleResult)
		{
			return new PrereqRuleConclusion(ruleResult);
		}
	}
}
