using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200009C RID: 156
	internal class ClearRehomeJob : LightJobBase
	{
		// Token: 0x060007F1 RID: 2033 RVA: 0x00036EFC File Offset: 0x000350FC
		public ClearRehomeJob(Guid requestGuid, Guid requestQueueGuid, MapiStore systemMailbox, byte[] messageId) : base(requestGuid, requestQueueGuid, systemMailbox, messageId)
		{
		}

		// Token: 0x060007F2 RID: 2034 RVA: 0x00036F09 File Offset: 0x00035109
		protected override RequestState RelinquishAction(TransactionalRequestJob requestJob, ReportData report)
		{
			requestJob.RehomeRequest = false;
			return RequestState.Relinquished;
		}

		// Token: 0x060007F3 RID: 2035 RVA: 0x00036F14 File Offset: 0x00035114
		protected override void InternalDispose(bool disposing)
		{
			base.InternalDispose(disposing);
		}

		// Token: 0x060007F4 RID: 2036 RVA: 0x00036F1D File Offset: 0x0003511D
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<ClearRehomeJob>(this);
		}
	}
}
