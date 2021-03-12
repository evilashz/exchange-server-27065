using System;

namespace Microsoft.Exchange.Management.Deployment.Analysis
{
	// Token: 0x0200000F RID: 15
	public sealed class ProgressUpdateEventArgs : EventArgs
	{
		// Token: 0x060000AD RID: 173 RVA: 0x000041D7 File Offset: 0x000023D7
		public ProgressUpdateEventArgs(AnalysisProgress progress)
		{
			this.progress = progress;
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000AE RID: 174 RVA: 0x000041E6 File Offset: 0x000023E6
		public AnalysisProgress Progress
		{
			get
			{
				return this.progress;
			}
		}

		// Token: 0x04000046 RID: 70
		private readonly AnalysisProgress progress;
	}
}
