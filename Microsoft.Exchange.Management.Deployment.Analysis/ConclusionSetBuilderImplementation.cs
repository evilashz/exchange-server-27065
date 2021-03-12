using System;
using System.Collections.Generic;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Management.Deployment.Analysis
{
	// Token: 0x02000004 RID: 4
	public abstract class ConclusionSetBuilderImplementation<TConclusionSet, TConclusion, TSettingConclusion, TRuleConclusion> where TConclusionSet : ConclusionSetImplementation<TConclusion, TSettingConclusion, TRuleConclusion> where TConclusion : ConclusionImplementation<TConclusion, TSettingConclusion, TRuleConclusion> where TSettingConclusion : TConclusion where TRuleConclusion : TConclusion, IRuleConclusion
	{
		// Token: 0x06000041 RID: 65 RVA: 0x00002A74 File Offset: 0x00000C74
		public TConclusionSet Build(Analysis analysis)
		{
			if (analysis.Status != AnalysisStatus.Completed)
			{
				throw new InvalidOperationException(Strings.AnalysisMustBeCompleteToCreateConclusionSet);
			}
			Dictionary<TConclusion, Result> dictionary = new Dictionary<TConclusion, Result>();
			Dictionary<Result, TConclusion> dictionary2 = new Dictionary<Result, TConclusion>();
			List<TConclusion> list = new List<TConclusion>();
			foreach (AnalysisMember analysisMember in analysis.AnalysisMembers)
			{
				foreach (Result result in analysisMember.CachedResults)
				{
					TConclusion tconclusion = this.BuildConclusion(analysis, result);
					if (tconclusion != null)
					{
						list.Add(tconclusion);
						dictionary.Add(tconclusion, result);
						dictionary2.Add(result, tconclusion);
					}
				}
			}
			foreach (TConclusion tconclusion2 in list)
			{
				Result result2 = dictionary[tconclusion2];
				if (result2 != analysis.RootAnalysisMember.Result)
				{
					Result parent = result2.Parent;
					TConclusion parent2 = dictionary2[parent];
					tconclusion2.Parent = parent2;
					parent2.Children.Add(tconclusion2);
				}
			}
			TConclusion rootConclusion = dictionary2[analysis.RootAnalysisMember.Result];
			return this.BuildConclusionSet(analysis, rootConclusion);
		}

		// Token: 0x06000042 RID: 66
		protected abstract TConclusionSet BuildConclusionSet(Analysis analysis, TConclusion rootConclusion);

		// Token: 0x06000043 RID: 67
		protected abstract TSettingConclusion BuildSettingConclusion(Result result);

		// Token: 0x06000044 RID: 68
		protected abstract TRuleConclusion BuildRuleConclusion(RuleResult ruleResult);

		// Token: 0x06000045 RID: 69 RVA: 0x00002BF8 File Offset: 0x00000DF8
		private TConclusion BuildConclusion(Analysis analysis, Result result)
		{
			if (result.Source == null)
			{
				result = new Result<object>((Result<object>)result, analysis.RootAnalysisMember, null, ExDateTime.Now, ExDateTime.Now);
			}
			AnalysisMember source = result.Source;
			if (source == analysis.RootAnalysisMember || (source.GetType().IsGenericType && source.GetType().GetGenericTypeDefinition() == typeof(Setting<>)))
			{
				return (TConclusion)((object)this.BuildSettingConclusion(result));
			}
			if (source is Rule)
			{
				return (TConclusion)((object)this.BuildRuleConclusion((RuleResult)result));
			}
			return default(TConclusion);
		}
	}
}
