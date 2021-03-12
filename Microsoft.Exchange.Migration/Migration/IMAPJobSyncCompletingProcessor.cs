using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000121 RID: 289
	internal class IMAPJobSyncCompletingProcessor : MigrationJobSyncCompletingProcessor
	{
		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x06000F05 RID: 3845 RVA: 0x00040832 File Offset: 0x0003EA32
		protected override string LicensingHelpUrl
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06000F06 RID: 3846 RVA: 0x00040838 File Offset: 0x0003EA38
		protected override string GetEmailSubject(bool errorsPresent)
		{
			if (base.Job.IsCancelled)
			{
				switch (base.Job.JobCancellationStatus)
				{
				case JobCancellationStatus.NotCancelled:
					break;
				case JobCancellationStatus.CancelledByUserRequest:
					return Strings.BatchCancelledByUser(base.Job.JobName);
				case JobCancellationStatus.CancelledDueToHighFailureCount:
					return Strings.BatchCancelledBySystem(base.Job.JobName);
				default:
					throw new InvalidOperationException("Unsupported job cancellation status " + base.Job.JobCancellationStatus);
				}
			}
			if (errorsPresent)
			{
				return Strings.BatchCompletionReportMailErrorHeader(base.Job.JobName);
			}
			return Strings.BatchCompletionReportMailHeader(base.Job.JobName);
		}

		// Token: 0x06000F07 RID: 3847 RVA: 0x000408EC File Offset: 0x0003EAEC
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<IMAPJobSyncCompletingProcessor>(this);
		}
	}
}
