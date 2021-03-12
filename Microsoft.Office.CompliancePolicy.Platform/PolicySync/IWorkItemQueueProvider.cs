using System;
using System.Collections.Generic;

namespace Microsoft.Office.CompliancePolicy.PolicySync
{
	// Token: 0x02000133 RID: 307
	public interface IWorkItemQueueProvider
	{
		// Token: 0x060008F9 RID: 2297
		void Enqueue(WorkItemBase item);

		// Token: 0x060008FA RID: 2298
		IList<WorkItemBase> Dequeue(int maxCount);

		// Token: 0x060008FB RID: 2299
		IList<WorkItemBase> GetAll();

		// Token: 0x060008FC RID: 2300
		bool IsEmpty();

		// Token: 0x060008FD RID: 2301
		void Update(WorkItemBase item);

		// Token: 0x060008FE RID: 2302
		void Delete(WorkItemBase item);

		// Token: 0x060008FF RID: 2303
		void OnWorkItemCompleted(WorkItemBase item);

		// Token: 0x06000900 RID: 2304
		void OnAllWorkItemDispatched();
	}
}
