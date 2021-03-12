using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020004FC RID: 1276
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class NullPeopleIKnowPublisher : IPeopleIKnowPublisher
	{
		// Token: 0x06003775 RID: 14197 RVA: 0x000DF358 File Offset: 0x000DD558
		private NullPeopleIKnowPublisher()
		{
		}

		// Token: 0x06003776 RID: 14198 RVA: 0x000DF360 File Offset: 0x000DD560
		public void Publish(IMailboxSession mailbox)
		{
		}

		// Token: 0x04001D64 RID: 7524
		public static readonly NullPeopleIKnowPublisher Instance = new NullPeopleIKnowPublisher();
	}
}
