using System;
using Microsoft.Exchange.Configuration.Common.LocStrings;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x0200028A RID: 650
	internal class ReportExceptionModule : ITaskModule, ICriticalFeature
	{
		// Token: 0x06001667 RID: 5735 RVA: 0x00054CFA File Offset: 0x00052EFA
		public ReportExceptionModule(TaskContext context)
		{
			this.context = context;
		}

		// Token: 0x06001668 RID: 5736 RVA: 0x00054D09 File Offset: 0x00052F09
		bool ICriticalFeature.IsCriticalException(Exception ex)
		{
			return false;
		}

		// Token: 0x06001669 RID: 5737 RVA: 0x00054D0C File Offset: 0x00052F0C
		public void Init(ITaskEvent task)
		{
			task.Error += this.ReportException;
		}

		// Token: 0x0600166A RID: 5738 RVA: 0x00054D20 File Offset: 0x00052F20
		public void Dispose()
		{
		}

		// Token: 0x0600166B RID: 5739 RVA: 0x00054D24 File Offset: 0x00052F24
		private void ReportException(object sender, GenericEventArg<TaskErrorEventArg> e)
		{
			if (e.Data.ExceptionHandled)
			{
				return;
			}
			TaskLogger.LogError(e.Data.Exception);
			bool flag;
			if (this.context != null && this.context.ErrorInfo.Exception != null && !this.context.ErrorInfo.IsKnownError && TaskHelper.ShouldReportException(e.Data.Exception, out flag))
			{
				if (!flag)
				{
					this.context.CommandShell.WriteWarning(Strings.UnhandledErrorMessage(e.Data.Exception.Message));
				}
				TaskLogger.SendWatsonReport(e.Data.Exception, this.context.InvocationInfo.CommandName, this.context.InvocationInfo.UserSpecifiedParameters);
			}
		}

		// Token: 0x040006DA RID: 1754
		private readonly TaskContext context;
	}
}
