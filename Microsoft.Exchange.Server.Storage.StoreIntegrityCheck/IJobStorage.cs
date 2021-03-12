using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Server.Storage.StoreIntegrityCheck
{
	// Token: 0x02000014 RID: 20
	public interface IJobStorage
	{
		// Token: 0x0600005F RID: 95
		void AddJob(IntegrityCheckJob job);

		// Token: 0x06000060 RID: 96
		void RemoveJob(Guid jobGuid);

		// Token: 0x06000061 RID: 97
		IntegrityCheckJob GetJob(Guid jobGuid);

		// Token: 0x06000062 RID: 98
		IEnumerable<IntegrityCheckJob> GetJobsByRequestGuid(Guid requestGuid);

		// Token: 0x06000063 RID: 99
		IEnumerable<IntegrityCheckJob> GetJobsByMailboxGuid(Guid mailboxGuid);

		// Token: 0x06000064 RID: 100
		IEnumerable<IntegrityCheckJob> GetAllJobs();
	}
}
