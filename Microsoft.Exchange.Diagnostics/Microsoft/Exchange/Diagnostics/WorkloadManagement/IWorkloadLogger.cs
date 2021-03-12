using System;

namespace Microsoft.Exchange.Diagnostics.WorkloadManagement
{
	// Token: 0x02000124 RID: 292
	internal interface IWorkloadLogger
	{
		// Token: 0x06000874 RID: 2164
		void LogActivityEvent(IActivityScope activityScope, ActivityEventType eventType);
	}
}
