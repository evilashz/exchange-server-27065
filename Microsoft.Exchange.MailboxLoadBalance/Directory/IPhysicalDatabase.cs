using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Data;

namespace Microsoft.Exchange.MailboxLoadBalance.Directory
{
	// Token: 0x0200002F RID: 47
	internal interface IPhysicalDatabase : IDisposeTrackable, IDisposable
	{
		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000180 RID: 384
		Guid DatabaseGuid { get; }

		// Token: 0x06000181 RID: 385
		IEnumerable<IPhysicalMailbox> GetConsumerMailboxes();

		// Token: 0x06000182 RID: 386
		DatabaseSizeInfo GetDatabaseSpaceData();

		// Token: 0x06000183 RID: 387
		IPhysicalMailbox GetMailbox(Guid mailboxGuid);

		// Token: 0x06000184 RID: 388
		IEnumerable<IPhysicalMailbox> GetNonConnectedMailboxes();

		// Token: 0x06000185 RID: 389
		void LoadMailboxes();
	}
}
