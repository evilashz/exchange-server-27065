using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Common.LocStrings;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000273 RID: 627
	internal class AutoReportProgressModule : ITaskModule, ICriticalFeature
	{
		// Token: 0x0600159E RID: 5534 RVA: 0x000506E8 File Offset: 0x0004E8E8
		public AutoReportProgressModule(TaskContext context)
		{
			this.context = context;
		}

		// Token: 0x0600159F RID: 5535 RVA: 0x000506F7 File Offset: 0x0004E8F7
		bool ICriticalFeature.IsCriticalException(Exception ex)
		{
			return false;
		}

		// Token: 0x060015A0 RID: 5536 RVA: 0x000506FA File Offset: 0x0004E8FA
		public void Init(ITaskEvent task)
		{
			task.IterateCompleted += this.ReportProgress;
			task.Error += this.ReportProgressOnError;
		}

		// Token: 0x060015A1 RID: 5537 RVA: 0x00050720 File Offset: 0x0004E920
		public void Dispose()
		{
		}

		// Token: 0x060015A2 RID: 5538 RVA: 0x00050722 File Offset: 0x0004E922
		private void ReportProgressOnError(object sender, GenericEventArg<TaskErrorEventArg> e)
		{
			if (this.context.ErrorInfo.IsKnownError && !this.context.ErrorInfo.TerminatePipeline)
			{
				this.ReportProgress(sender, e);
			}
		}

		// Token: 0x060015A3 RID: 5539 RVA: 0x00050750 File Offset: 0x0004E950
		private void ReportProgress(object sender, EventArgs e)
		{
			if (this.context.CurrentObjectIndex >= 0)
			{
				ExProgressRecord exProgressRecord = new ExProgressRecord(this.context.CurrentObjectIndex, Strings.TaskCompleted, Strings.TaskCompleted);
				exProgressRecord.RecordType = ProgressRecordType.Completed;
				this.context.CommandShell.WriteProgress(exProgressRecord);
			}
		}

		// Token: 0x0400069D RID: 1693
		private readonly TaskContext context;
	}
}
