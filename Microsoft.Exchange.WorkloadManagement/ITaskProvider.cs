using System;

namespace Microsoft.Exchange.WorkloadManagement
{
	// Token: 0x02000008 RID: 8
	internal interface ITaskProvider : IDisposable
	{
		// Token: 0x0600004C RID: 76
		SystemTaskBase GetNextTask();
	}
}
