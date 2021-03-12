using System;
using System.Collections.Generic;

namespace Microsoft.Office.CompliancePolicy.Dar
{
	// Token: 0x0200006D RID: 109
	public abstract class DarTaskQueue
	{
		// Token: 0x0600030E RID: 782
		public abstract IEnumerable<DarTask> Dequeue(int count, DarTaskCategory category, object availableResource = null);

		// Token: 0x0600030F RID: 783
		public abstract void Enqueue(DarTask darTask);

		// Token: 0x06000310 RID: 784
		public abstract IEnumerable<DarTask> GetRunningTasks(DateTime minExecutionStartTime, DateTime maxExecutionStartTime, string taskType = null, string tenantId = null);

		// Token: 0x06000311 RID: 785
		public abstract void UpdateTask(DarTask task);

		// Token: 0x06000312 RID: 786
		public abstract void DeleteTask(DarTask task);

		// Token: 0x06000313 RID: 787
		public abstract void DeleteCompletedTask(DateTime maxCompletionTime, string taskType = null, string tenantId = null);

		// Token: 0x06000314 RID: 788
		public abstract IEnumerable<DarTask> GetCompletedTasks(DateTime minCompletionTime, string taskType = null, string tenantId = null);

		// Token: 0x06000315 RID: 789
		public abstract IEnumerable<DarTask> GetTasks(string tenantId, string taskType = null, DarTaskState? taskState = null, DateTime? minScheduledTime = null, DateTime? maxScheduledTime = null, DateTime? minCompletedTime = null, DateTime? maxCompletedTime = null);

		// Token: 0x06000316 RID: 790
		public abstract IEnumerable<DarTask> GetLastScheduledTasks(string tenantId);
	}
}
