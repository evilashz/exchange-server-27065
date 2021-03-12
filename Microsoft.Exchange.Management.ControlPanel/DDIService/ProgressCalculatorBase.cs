using System;
using System.Collections;
using Microsoft.Exchange.Management.ControlPanel;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x02000154 RID: 340
	public abstract class ProgressCalculatorBase
	{
		// Token: 0x06002194 RID: 8596 RVA: 0x000651EB File Offset: 0x000633EB
		public ProgressCalculatorBase()
		{
			this.ProgressRecord = new ProgressRecord();
		}

		// Token: 0x06002195 RID: 8597 RVA: 0x00065200 File Offset: 0x00063400
		public void CalculateProgress(ProgressReportEventArgs e)
		{
			lock (this.ProgressRecord.SyncRoot)
			{
				this.CalculateProgressImpl(e);
			}
		}

		// Token: 0x06002196 RID: 8598
		public abstract void CalculateProgressImpl(ProgressReportEventArgs e);

		// Token: 0x17001A76 RID: 6774
		// (get) Token: 0x06002197 RID: 8599 RVA: 0x00065248 File Offset: 0x00063448
		// (set) Token: 0x06002198 RID: 8600 RVA: 0x00065250 File Offset: 0x00063450
		public ProgressRecord ProgressRecord { get; private set; }

		// Token: 0x06002199 RID: 8601 RVA: 0x00065259 File Offset: 0x00063459
		public virtual void SetPipelineInput(IEnumerable pipelineInput)
		{
		}

		// Token: 0x0600219A RID: 8602 RVA: 0x0006525C File Offset: 0x0006345C
		internal static int CalculatePercentageHelper(int defaultPercent, int currentIndex, int totalCount, Activity currentActivity)
		{
			double num = (double)defaultPercent / 100.0;
			if (totalCount != 0)
			{
				double num2 = 1.0 / (double)totalCount;
				num = num2 * (double)currentIndex;
				if (currentActivity != null)
				{
					num += num2 * (double)currentActivity.ProgressPercent / 100.0;
				}
			}
			return (int)Math.Floor(num * 100.0);
		}
	}
}
