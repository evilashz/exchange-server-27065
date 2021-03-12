using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020004F4 RID: 1268
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IPeopleIKnowPublisher
	{
		// Token: 0x060036F7 RID: 14071
		void Publish(IMailboxSession mailbox);
	}
}
