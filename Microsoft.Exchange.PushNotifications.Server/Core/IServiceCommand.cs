using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.PushNotifications.Server.Core
{
	// Token: 0x02000005 RID: 5
	internal interface IServiceCommand : ITask
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000017 RID: 23
		IAsyncResult CommandAsyncResult { get; }

		// Token: 0x06000018 RID: 24
		void Initialize(IBudget budget);

		// Token: 0x06000019 RID: 25
		void Complete(Exception error = null);
	}
}
