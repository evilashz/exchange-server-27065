using System;
using Microsoft.Exchange.Management.Deployment.Analysis;

namespace Microsoft.Exchange.Management.Deployment.PrereqAnalysisSample
{
	// Token: 0x02000073 RID: 115
	internal class PrereqRuleConclusion : PrereqConclusion, IRuleConclusion
	{
		// Token: 0x06000A7B RID: 2683 RVA: 0x000266E5 File Offset: 0x000248E5
		public PrereqRuleConclusion()
		{
		}

		// Token: 0x06000A7C RID: 2684 RVA: 0x000266F0 File Offset: 0x000248F0
		public PrereqRuleConclusion(RuleResult ruleResult) : base(ruleResult)
		{
			this.severity = ruleResult.Source.Features.GetFeature<SeverityFeature>().Severity;
			this.message = ruleResult.Source.Features.GetFeature<MessageFeature>().TextFunction(ruleResult);
		}

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x06000A7D RID: 2685 RVA: 0x00026740 File Offset: 0x00024940
		// (set) Token: 0x06000A7E RID: 2686 RVA: 0x0002674D File Offset: 0x0002494D
		public bool IsConditionMet
		{
			get
			{
				return (bool)base.Value;
			}
			set
			{
				base.ThrowIfReadOnly();
				base.Value = value;
			}
		}

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x06000A7F RID: 2687 RVA: 0x00026761 File Offset: 0x00024961
		// (set) Token: 0x06000A80 RID: 2688 RVA: 0x00026769 File Offset: 0x00024969
		public Severity Severity
		{
			get
			{
				return this.severity;
			}
			set
			{
				base.ThrowIfReadOnly();
				this.severity = value;
			}
		}

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x06000A81 RID: 2689 RVA: 0x00026778 File Offset: 0x00024978
		// (set) Token: 0x06000A82 RID: 2690 RVA: 0x00026780 File Offset: 0x00024980
		public string Message
		{
			get
			{
				return this.message;
			}
			set
			{
				base.ThrowIfReadOnly();
				this.message = value;
			}
		}

		// Token: 0x040005C3 RID: 1475
		private Severity severity;

		// Token: 0x040005C4 RID: 1476
		private string message;
	}
}
