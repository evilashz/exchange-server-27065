using System;

namespace Microsoft.Exchange.WorkloadManagement
{
	// Token: 0x0200003E RID: 62
	internal interface IUserWorkloadManager
	{
		// Token: 0x0600024E RID: 590
		bool TrySubmitNewTask(ITask task);
	}
}
