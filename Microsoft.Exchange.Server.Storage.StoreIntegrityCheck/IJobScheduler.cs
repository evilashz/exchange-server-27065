using System;
using System.Collections.Generic;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.StoreIntegrityCheck
{
	// Token: 0x02000024 RID: 36
	public interface IJobScheduler
	{
		// Token: 0x060000BB RID: 187
		void ScheduleJob(IntegrityCheckJob job);

		// Token: 0x060000BC RID: 188
		void RemoveJob(IntegrityCheckJob job);

		// Token: 0x060000BD RID: 189
		void ExecuteJob(Context context, IntegrityCheckJob job);

		// Token: 0x060000BE RID: 190
		IEnumerable<IntegrityCheckJob> GetReadyJobs(JobPriority priority);
	}
}
