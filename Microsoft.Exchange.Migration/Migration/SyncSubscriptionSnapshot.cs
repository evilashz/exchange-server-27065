using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000176 RID: 374
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class SyncSubscriptionSnapshot : SubscriptionSnapshot
	{
		// Token: 0x060011B1 RID: 4529 RVA: 0x0004AE84 File Offset: 0x00049084
		public SyncSubscriptionSnapshot(ISubscriptionId id, SnapshotStatus status, bool isInitialSyncComplete, ExDateTime createTime, ExDateTime? lastUpdateTime, ExDateTime? lastSyncTime, LocalizedString? errorMessage, string batchName, SyncProtocol protocol, string userName, SmtpAddress emailAddress) : base(id, status, isInitialSyncComplete, createTime, lastUpdateTime, lastSyncTime, errorMessage, batchName)
		{
			this.Protocol = protocol;
			this.UserName = userName;
			this.EmailAddress = emailAddress;
		}

		// Token: 0x17000544 RID: 1348
		// (get) Token: 0x060011B2 RID: 4530 RVA: 0x0004AEBC File Offset: 0x000490BC
		// (set) Token: 0x060011B3 RID: 4531 RVA: 0x0004AEC4 File Offset: 0x000490C4
		public SyncProtocol Protocol { get; private set; }

		// Token: 0x17000545 RID: 1349
		// (get) Token: 0x060011B4 RID: 4532 RVA: 0x0004AECD File Offset: 0x000490CD
		// (set) Token: 0x060011B5 RID: 4533 RVA: 0x0004AED5 File Offset: 0x000490D5
		public string UserName { get; private set; }

		// Token: 0x17000546 RID: 1350
		// (get) Token: 0x060011B6 RID: 4534 RVA: 0x0004AEDE File Offset: 0x000490DE
		// (set) Token: 0x060011B7 RID: 4535 RVA: 0x0004AEE6 File Offset: 0x000490E6
		public SmtpAddress EmailAddress { get; private set; }
	}
}
