using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Notifications.Broker
{
	// Token: 0x02000025 RID: 37
	public class MailboxSnapshot
	{
		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000189 RID: 393 RVA: 0x00009B18 File Offset: 0x00007D18
		// (set) Token: 0x0600018A RID: 394 RVA: 0x00009B20 File Offset: 0x00007D20
		public Guid MailboxGuid { get; set; }

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x0600018B RID: 395 RVA: 0x00009B29 File Offset: 0x00007D29
		// (set) Token: 0x0600018C RID: 396 RVA: 0x00009B31 File Offset: 0x00007D31
		public string DisplayName { get; set; }

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x0600018D RID: 397 RVA: 0x00009B3A File Offset: 0x00007D3A
		// (set) Token: 0x0600018E RID: 398 RVA: 0x00009B42 File Offset: 0x00007D42
		public int SubscriptionCount { get; set; }

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x0600018F RID: 399 RVA: 0x00009B4B File Offset: 0x00007D4B
		// (set) Token: 0x06000190 RID: 400 RVA: 0x00009B53 File Offset: 0x00007D53
		public bool SubscriptionsLoaded { get; set; }

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000191 RID: 401 RVA: 0x00009B5C File Offset: 0x00007D5C
		// (set) Token: 0x06000192 RID: 402 RVA: 0x00009B64 File Offset: 0x00007D64
		[XmlArrayItem("Subscription")]
		[XmlArray("Subscriptions")]
		public List<BrokerSubscription> Subscriptions { get; set; }
	}
}
