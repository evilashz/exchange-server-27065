using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Notifications.Broker
{
	// Token: 0x02000013 RID: 19
	public class SubscriptionsForHandlersSnapshot
	{
		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x00005DF7 File Offset: 0x00003FF7
		// (set) Token: 0x060000D3 RID: 211 RVA: 0x00005DFF File Offset: 0x00003FFF
		[XmlArrayItem("Handler")]
		[XmlArray("Handlers")]
		public List<SubscriptionsForHandlersSnapshot.SubscriptionsForHandlerSnapshot> Handlers { get; set; }

		// Token: 0x060000D4 RID: 212 RVA: 0x00005E08 File Offset: 0x00004008
		public static SubscriptionsForHandlersSnapshot Create(Guid databaseGuid, Guid mailboxGuid, string handlerName)
		{
			return Generator.Singleton.GetSubscriptionsForHandlerDiagnostics(databaseGuid, mailboxGuid, handlerName);
		}

		// Token: 0x02000014 RID: 20
		public class SubscriptionsForHandlerSnapshot
		{
			// Token: 0x1700002E RID: 46
			// (get) Token: 0x060000D6 RID: 214 RVA: 0x00005E1F File Offset: 0x0000401F
			// (set) Token: 0x060000D7 RID: 215 RVA: 0x00005E27 File Offset: 0x00004027
			[XmlAttribute("Name")]
			public string Handler { get; set; }

			// Token: 0x1700002F RID: 47
			// (get) Token: 0x060000D8 RID: 216 RVA: 0x00005E30 File Offset: 0x00004030
			// (set) Token: 0x060000D9 RID: 217 RVA: 0x00005E38 File Offset: 0x00004038
			[XmlAttribute("SubscriptionCount")]
			public int SubscriptionCount { get; set; }

			// Token: 0x17000030 RID: 48
			// (get) Token: 0x060000DA RID: 218 RVA: 0x00005E41 File Offset: 0x00004041
			// (set) Token: 0x060000DB RID: 219 RVA: 0x00005E49 File Offset: 0x00004049
			[XmlArrayItem("Subscription")]
			[XmlArray("Subscriptions")]
			public List<BrokerSubscription> Subscriptions { get; set; }
		}
	}
}
