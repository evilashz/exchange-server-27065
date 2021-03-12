using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200009F RID: 159
	internal class ResumeJob : LightJobBase
	{
		// Token: 0x06000811 RID: 2065 RVA: 0x00037955 File Offset: 0x00035B55
		public ResumeJob(Guid requestGuid, Guid requestQueueGuid, MapiStore systemMailbox, byte[] messageId) : base(requestGuid, requestQueueGuid, systemMailbox, messageId)
		{
		}

		// Token: 0x06000812 RID: 2066 RVA: 0x00037964 File Offset: 0x00035B64
		protected override RequestState RelinquishAction(TransactionalRequestJob requestJob, ReportData report)
		{
			requestJob.TimeTracker.SetTimestamp(RequestJobTimestamp.Failure, null);
			requestJob.TimeTracker.SetTimestamp(RequestJobTimestamp.Suspended, null);
			requestJob.FailureCode = null;
			requestJob.FailureType = null;
			requestJob.FailureSide = null;
			requestJob.Message = LocalizedString.Empty;
			if (requestJob.SyncStage <= SyncStage.None)
			{
				requestJob.Status = RequestStatus.Queued;
				requestJob.TimeTracker.CurrentState = RequestState.Queued;
			}
			else
			{
				requestJob.Status = RequestStatus.InProgress;
				requestJob.TimeTracker.CurrentState = RequestState.InitialSeeding;
			}
			report.Append(MrsStrings.ReportJobResumed(requestJob.Status.ToString()));
			return RequestState.Relinquished;
		}

		// Token: 0x06000813 RID: 2067 RVA: 0x00037A1A File Offset: 0x00035C1A
		protected override void AfterRelinquishAction()
		{
			MRSService.Tickle(base.RequestGuid, this.RequestQueueGuid, MoveRequestNotification.Created);
		}

		// Token: 0x06000814 RID: 2068 RVA: 0x00037A2E File Offset: 0x00035C2E
		protected override void InternalDispose(bool disposing)
		{
			base.InternalDispose(disposing);
		}

		// Token: 0x06000815 RID: 2069 RVA: 0x00037A37 File Offset: 0x00035C37
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<ResumeJob>(this);
		}
	}
}
