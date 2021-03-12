using System;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200009D RID: 157
	internal class QuarantineJob : LightJobBase
	{
		// Token: 0x060007F5 RID: 2037 RVA: 0x00036F25 File Offset: 0x00035125
		public QuarantineJob(Guid requestGuid, Guid requestQueueGuid, MapiStore systemMailbox, byte[] messageId) : base(requestGuid, requestQueueGuid, systemMailbox, messageId)
		{
		}

		// Token: 0x060007F6 RID: 2038 RVA: 0x00036F34 File Offset: 0x00035134
		protected override RequestState RelinquishAction(TransactionalRequestJob requestJob, ReportData report)
		{
			FailureRec failureRec = QuarantinedJobs.Get(base.RequestGuid);
			if (failureRec == null)
			{
				return RequestState.Relinquished;
			}
			report.Append(MrsStrings.JobIsQuarantined, failureRec, ReportEntryFlags.Fatal);
			requestJob.Suspend = true;
			requestJob.Status = RequestStatus.Failed;
			requestJob.TimeTracker.SetTimestamp(RequestJobTimestamp.Failure, new DateTime?(DateTime.UtcNow));
			QuarantinedJobs.Remove(base.RequestGuid);
			RequestJobLog.Write(requestJob);
			return RequestState.Failed;
		}

		// Token: 0x060007F7 RID: 2039 RVA: 0x00036F9A File Offset: 0x0003519A
		protected override void InternalDispose(bool disposing)
		{
			base.InternalDispose(disposing);
		}

		// Token: 0x060007F8 RID: 2040 RVA: 0x00036FA3 File Offset: 0x000351A3
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<QuarantineJob>(this);
		}
	}
}
