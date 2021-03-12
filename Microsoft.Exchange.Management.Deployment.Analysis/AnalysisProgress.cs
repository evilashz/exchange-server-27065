using System;

namespace Microsoft.Exchange.Management.Deployment.Analysis
{
	// Token: 0x0200000B RID: 11
	public sealed class AnalysisProgress
	{
		// Token: 0x060000A7 RID: 167 RVA: 0x0000416C File Offset: 0x0000236C
		public AnalysisProgress(int totalConclusions, int completedConclusions)
		{
			this.totalConclusions = totalConclusions;
			this.completedConclusions = completedConclusions;
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x00004182 File Offset: 0x00002382
		public int TotalConclusions
		{
			get
			{
				return this.totalConclusions;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x0000418A File Offset: 0x0000238A
		public int CompletedConclusions
		{
			get
			{
				return this.completedConclusions;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000AA RID: 170 RVA: 0x00004192 File Offset: 0x00002392
		public int PercentageComplete
		{
			get
			{
				if (this.totalConclusions == 0)
				{
					return 0;
				}
				return this.completedConclusions * 100 / this.totalConclusions;
			}
		}

		// Token: 0x060000AB RID: 171 RVA: 0x000041AE File Offset: 0x000023AE
		internal static AnalysisProgress Resolve(AnalysisProgress originalValue, AnalysisProgress currentValue, AnalysisProgress updatedValue)
		{
			return new AnalysisProgress(Math.Max(updatedValue.TotalConclusions, currentValue.TotalConclusions), Math.Max(updatedValue.CompletedConclusions, currentValue.CompletedConclusions));
		}

		// Token: 0x0400003C RID: 60
		private readonly int totalConclusions;

		// Token: 0x0400003D RID: 61
		private readonly int completedConclusions;
	}
}
