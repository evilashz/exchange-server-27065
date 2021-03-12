using System;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x0200003F RID: 63
	public struct EventWatermark
	{
		// Token: 0x06000633 RID: 1587 RVA: 0x00038C58 File Offset: 0x00036E58
		public EventWatermark(Guid mailboxGuid, Guid consumerGuid, long eventCounter)
		{
			this.mailboxGuid = mailboxGuid;
			this.consumerGuid = consumerGuid;
			this.eventCounter = eventCounter;
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06000634 RID: 1588 RVA: 0x00038C6F File Offset: 0x00036E6F
		public Guid MailboxGuid
		{
			get
			{
				return this.mailboxGuid;
			}
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06000635 RID: 1589 RVA: 0x00038C77 File Offset: 0x00036E77
		public Guid ConsumerGuid
		{
			get
			{
				return this.consumerGuid;
			}
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x06000636 RID: 1590 RVA: 0x00038C7F File Offset: 0x00036E7F
		public long EventCounter
		{
			get
			{
				return this.eventCounter;
			}
		}

		// Token: 0x0400034C RID: 844
		private Guid mailboxGuid;

		// Token: 0x0400034D RID: 845
		private Guid consumerGuid;

		// Token: 0x0400034E RID: 846
		private long eventCounter;
	}
}
