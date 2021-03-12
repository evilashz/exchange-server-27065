using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Notifications.Broker
{
	// Token: 0x02000021 RID: 33
	internal interface IBrokerMailboxData
	{
		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000C3 RID: 195
		string DisplayName { get; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000C4 RID: 196
		Guid DatabaseGuid { get; }

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000C5 RID: 197
		Guid MailboxGuid { get; }

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000C6 RID: 198
		TenantPartitionHint TenantPartitionHint { get; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000C7 RID: 199
		IDictionary<Guid, BrokerSubscription> Subscriptions { get; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000C8 RID: 200
		IBrokerDatabaseData DatabaseData { get; }
	}
}
