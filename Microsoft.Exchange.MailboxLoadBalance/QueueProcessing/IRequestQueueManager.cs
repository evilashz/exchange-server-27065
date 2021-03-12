using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Data;
using Microsoft.Exchange.MailboxLoadBalance.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Directory;

namespace Microsoft.Exchange.MailboxLoadBalance.QueueProcessing
{
	// Token: 0x020000E0 RID: 224
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal interface IRequestQueueManager
	{
		// Token: 0x17000229 RID: 553
		// (get) Token: 0x060006E8 RID: 1768
		IRequestQueue MainProcessingQueue { get; }

		// Token: 0x060006E9 RID: 1769
		IRequestQueue GetInjectionQueue(DirectoryDatabase database);

		// Token: 0x060006EA RID: 1770
		IRequestQueue GetProcessingQueue(LoadEntity loadObject);

		// Token: 0x060006EB RID: 1771
		IRequestQueue GetProcessingQueue(DirectoryObject directoryObject);

		// Token: 0x060006EC RID: 1772
		QueueManagerDiagnosticData GetDiagnosticData(bool includeRequestDetails, bool includeRequestVerboseData);

		// Token: 0x060006ED RID: 1773
		void Clean();
	}
}
