using System;
using System.Linq;
using Microsoft.Exchange.Management.ControlPanel;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x02000156 RID: 342
	public sealed class MaximumCountProgressCalculator : ProgressCalculatorBase
	{
		// Token: 0x0600219E RID: 8606 RVA: 0x000653AC File Offset: 0x000635AC
		public override void CalculateProgressImpl(ProgressReportEventArgs e)
		{
			this.totalReceivedEvents++;
			if (e.Errors != null && e.Errors.Count > 0)
			{
				base.ProgressRecord.FailedCount = 1;
				base.ProgressRecord.Errors = ((base.ProgressRecord.Errors == null) ? e.Errors.ToArray<ErrorRecord>() : base.ProgressRecord.Errors.Concat(e.Errors.ToArray<ErrorRecord>() ?? new ErrorRecord[0]).ToArray<ErrorRecord>());
			}
			if (this.totalReceivedEvents <= base.ProgressRecord.MaxCount)
			{
				base.ProgressRecord.Percent = this.totalReceivedEvents * 100 / base.ProgressRecord.MaxCount;
			}
		}

		// Token: 0x04001D38 RID: 7480
		private int totalReceivedEvents;
	}
}
