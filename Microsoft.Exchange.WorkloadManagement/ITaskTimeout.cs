using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.WorkloadManagement
{
	// Token: 0x0200003D RID: 61
	internal interface ITaskTimeout
	{
		// Token: 0x0600024D RID: 589
		TimeSpan GetActionTimeout(CostType costType);
	}
}
