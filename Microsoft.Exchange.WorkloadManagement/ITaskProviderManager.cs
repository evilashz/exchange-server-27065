using System;

namespace Microsoft.Exchange.WorkloadManagement
{
	// Token: 0x02000009 RID: 9
	internal interface ITaskProviderManager
	{
		// Token: 0x0600004D RID: 77
		ITaskProvider GetNextProvider();

		// Token: 0x0600004E RID: 78
		int GetProviderCount();
	}
}
