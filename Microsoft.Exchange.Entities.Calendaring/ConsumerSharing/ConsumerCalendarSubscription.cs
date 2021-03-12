using System;

namespace Microsoft.Exchange.Entities.Calendaring.ConsumerSharing
{
	// Token: 0x02000019 RID: 25
	internal class ConsumerCalendarSubscription
	{
		// Token: 0x06000092 RID: 146 RVA: 0x0000320F File Offset: 0x0000140F
		public ConsumerCalendarSubscription(long consumerCalendarOwnerId, Guid consumerCalendarGuid)
		{
			this.ConsumerCalendarOwnerId = consumerCalendarOwnerId;
			this.ConsumerCalendarGuid = consumerCalendarGuid;
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000093 RID: 147 RVA: 0x00003225 File Offset: 0x00001425
		// (set) Token: 0x06000094 RID: 148 RVA: 0x0000322D File Offset: 0x0000142D
		public long ConsumerCalendarOwnerId { get; private set; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000095 RID: 149 RVA: 0x00003236 File Offset: 0x00001436
		// (set) Token: 0x06000096 RID: 150 RVA: 0x0000323E File Offset: 0x0000143E
		public Guid ConsumerCalendarGuid { get; private set; }
	}
}
