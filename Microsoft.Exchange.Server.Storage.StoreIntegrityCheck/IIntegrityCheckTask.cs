using System;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.StoreIntegrityCheck
{
	// Token: 0x02000002 RID: 2
	public interface IIntegrityCheckTask
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1
		string TaskName { get; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000002 RID: 2
		IJobExecutionTracker JobExecutionTracker { get; }

		// Token: 0x06000003 RID: 3
		ErrorCode Execute(Context context, Guid mailboxGuid, bool detectOnly, bool isScheduled, Func<bool> shouldContinue);
	}
}
