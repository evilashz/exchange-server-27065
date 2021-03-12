using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.Wrappers
{
	// Token: 0x020002C7 RID: 711
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class SubscribeToNotificationRequestWrapper
	{
		// Token: 0x170005EB RID: 1515
		// (get) Token: 0x0600185E RID: 6238 RVA: 0x000543B9 File Offset: 0x000525B9
		// (set) Token: 0x0600185F RID: 6239 RVA: 0x000543C1 File Offset: 0x000525C1
		[DataMember(Name = "request")]
		public NotificationSubscribeJsonRequest Request { get; set; }

		// Token: 0x170005EC RID: 1516
		// (get) Token: 0x06001860 RID: 6240 RVA: 0x000543CA File Offset: 0x000525CA
		// (set) Token: 0x06001861 RID: 6241 RVA: 0x000543D2 File Offset: 0x000525D2
		[DataMember(Name = "subscriptionData")]
		public SubscriptionData[] SubscriptionData { get; set; }
	}
}
