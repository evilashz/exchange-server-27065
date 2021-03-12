using System;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020000A0 RID: 160
	internal class SuspendJob : LightJobBase
	{
		// Token: 0x06000816 RID: 2070 RVA: 0x00037A3F File Offset: 0x00035C3F
		public SuspendJob(Guid requestGuid, Guid requestQueueGuid, MapiStore systemMailbox, byte[] messageId) : base(requestGuid, requestQueueGuid, systemMailbox, messageId)
		{
		}

		// Token: 0x06000817 RID: 2071 RVA: 0x00037A4C File Offset: 0x00035C4C
		protected override RequestState RelinquishAction(TransactionalRequestJob requestJob, ReportData report)
		{
			requestJob.Status = RequestStatus.Suspended;
			requestJob.TimeTracker.SetTimestamp(RequestJobTimestamp.Suspended, new DateTime?(DateTime.UtcNow));
			report.Append(MrsStrings.ReportSuspendingJob);
			return RequestState.Suspended;
		}

		// Token: 0x06000818 RID: 2072 RVA: 0x00037A79 File Offset: 0x00035C79
		protected override void InternalDispose(bool disposing)
		{
			base.InternalDispose(disposing);
		}

		// Token: 0x06000819 RID: 2073 RVA: 0x00037A82 File Offset: 0x00035C82
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<SuspendJob>(this);
		}
	}
}
