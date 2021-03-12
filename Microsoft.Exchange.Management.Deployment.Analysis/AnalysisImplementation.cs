using System;

namespace Microsoft.Exchange.Management.Deployment.Analysis
{
	// Token: 0x0200000A RID: 10
	public abstract class AnalysisImplementation<TDataSourceProvider, TMemberBuilder, TConclusionSetBuilder, TConclusionSet, TConclusion, TSettingConclusion, TRuleConclusion> : Analysis where TMemberBuilder : AnalysisMemberBuilder where TConclusionSetBuilder : ConclusionSetBuilderImplementation<TConclusionSet, TConclusion, TSettingConclusion, TRuleConclusion> where TConclusionSet : ConclusionSetImplementation<TConclusion, TSettingConclusion, TRuleConclusion> where TConclusion : ConclusionImplementation<TConclusion, TSettingConclusion, TRuleConclusion> where TSettingConclusion : TConclusion where TRuleConclusion : TConclusion, IRuleConclusion
	{
		// Token: 0x060000A3 RID: 163 RVA: 0x00004038 File Offset: 0x00002238
		protected AnalysisImplementation(TDataSourceProvider dataSourceProvider, TMemberBuilder memberBuilder, TConclusionSetBuilder conclusionSetBuilder, Func<AnalysisMember, bool> immediateEvaluationFilter, Func<AnalysisMember, bool> conclusionsFilter, AnalysisThreading threadMode) : base(immediateEvaluationFilter, conclusionsFilter, threadMode)
		{
			if (dataSourceProvider == null)
			{
				throw new ArgumentNullException("dataSourceProvider");
			}
			if (memberBuilder == null)
			{
				throw new ArgumentNullException("memberBuilder");
			}
			if (conclusionSetBuilder == null)
			{
				throw new ArgumentNullException("conclusionSetBuilder");
			}
			this.dataSourceProvider = dataSourceProvider;
			this.memberBuilder = memberBuilder;
			this.memberBuilder.SetAnalysis(this);
			this.conclusionSetBuilder = conclusionSetBuilder;
			this.conclusionSet = default(TConclusionSet);
			this.conclusionSetLock = new object();
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x000040C7 File Offset: 0x000022C7
		public TDataSourceProvider DataSources
		{
			get
			{
				return this.dataSourceProvider;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x000040D0 File Offset: 0x000022D0
		public TConclusionSet Conclusions
		{
			get
			{
				base.WaitUntilComplete();
				if (base.Status != AnalysisStatus.Completed)
				{
					throw new InvalidOperationException(Strings.AnalysisMustBeCompleteToCreateConclusionSet);
				}
				TConclusionSet result;
				lock (this.conclusionSetLock)
				{
					if (this.conclusionSet != null)
					{
						result = this.conclusionSet;
					}
					else
					{
						TConclusionSetBuilder tconclusionSetBuilder = this.conclusionSetBuilder;
						this.conclusionSet = tconclusionSetBuilder.Build(this);
						result = this.conclusionSet;
					}
				}
				return result;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x00004164 File Offset: 0x00002364
		protected TMemberBuilder Build
		{
			get
			{
				return this.memberBuilder;
			}
		}

		// Token: 0x04000037 RID: 55
		private readonly TDataSourceProvider dataSourceProvider;

		// Token: 0x04000038 RID: 56
		private readonly TMemberBuilder memberBuilder;

		// Token: 0x04000039 RID: 57
		private readonly TConclusionSetBuilder conclusionSetBuilder;

		// Token: 0x0400003A RID: 58
		private readonly object conclusionSetLock;

		// Token: 0x0400003B RID: 59
		private TConclusionSet conclusionSet;
	}
}
