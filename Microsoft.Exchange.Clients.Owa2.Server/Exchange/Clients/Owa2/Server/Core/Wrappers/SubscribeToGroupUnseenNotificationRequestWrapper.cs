using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.Wrappers
{
	// Token: 0x020002C6 RID: 710
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class SubscribeToGroupUnseenNotificationRequestWrapper
	{
		// Token: 0x170005E9 RID: 1513
		// (get) Token: 0x06001859 RID: 6233 RVA: 0x0005438F File Offset: 0x0005258F
		// (set) Token: 0x0600185A RID: 6234 RVA: 0x00054397 File Offset: 0x00052597
		[DataMember(Name = "request")]
		public NotificationSubscribeJsonRequest Request { get; set; }

		// Token: 0x170005EA RID: 1514
		// (get) Token: 0x0600185B RID: 6235 RVA: 0x000543A0 File Offset: 0x000525A0
		// (set) Token: 0x0600185C RID: 6236 RVA: 0x000543A8 File Offset: 0x000525A8
		[DataMember(Name = "subscriptionData")]
		public SubscriptionData[] SubscriptionData { get; set; }
	}
}
