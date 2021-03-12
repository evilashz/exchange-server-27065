using System;
using Microsoft.Exchange.Data.GroupMailbox;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000095 RID: 149
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal interface IQueuedGroupJoinInvoker
	{
		// Token: 0x06000392 RID: 914
		void Enqueue(GroupMailbox group);

		// Token: 0x06000393 RID: 915
		bool ProcessQueue(UserMailboxLocator userMailbox, Guid parentActivityId);
	}
}
