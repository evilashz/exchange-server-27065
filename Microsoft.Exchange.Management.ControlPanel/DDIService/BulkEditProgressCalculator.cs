using System;
using System.Collections;
using System.Linq;
using Microsoft.Exchange.Management.ControlPanel;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x02000155 RID: 341
	public sealed class BulkEditProgressCalculator : ProgressCalculatorBase
	{
		// Token: 0x0600219B RID: 8603 RVA: 0x000652B8 File Offset: 0x000634B8
		public override void CalculateProgressImpl(ProgressReportEventArgs e)
		{
			if (e.Errors == null || e.Errors.Count == 0)
			{
				base.ProgressRecord.SucceededCount++;
			}
			else
			{
				base.ProgressRecord.FailedCount++;
				base.ProgressRecord.Errors = ((base.ProgressRecord.Errors == null) ? e.Errors.ToArray<ErrorRecord>() : base.ProgressRecord.Errors.Concat(e.Errors.ToArray<ErrorRecord>() ?? new ErrorRecord[0]).ToArray<ErrorRecord>());
			}
			base.ProgressRecord.Percent = (base.ProgressRecord.SucceededCount + base.ProgressRecord.FailedCount) * 100 / base.ProgressRecord.MaxCount;
		}

		// Token: 0x0600219C RID: 8604 RVA: 0x00065382 File Offset: 0x00063582
		public override void SetPipelineInput(IEnumerable pipelineInput)
		{
			if (pipelineInput != null)
			{
				base.ProgressRecord.MaxCount += pipelineInput.OfType<object>().Count<object>();
			}
		}
	}
}
