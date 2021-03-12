using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000027 RID: 39
	public interface IJobQuarantineProvider
	{
		// Token: 0x060001B1 RID: 433
		void QuarantineJob(Guid requestGuid, Exception ex);

		// Token: 0x060001B2 RID: 434
		void UnquarantineJob(Guid requestGuid);

		// Token: 0x060001B3 RID: 435
		IDictionary<Guid, FailureRec> GetQuarantinedJobs();
	}
}
