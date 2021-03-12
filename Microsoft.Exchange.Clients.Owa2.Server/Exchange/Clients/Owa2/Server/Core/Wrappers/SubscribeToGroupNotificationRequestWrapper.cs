using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.Wrappers
{
	// Token: 0x020002C5 RID: 709
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class SubscribeToGroupNotificationRequestWrapper
	{
		// Token: 0x170005E7 RID: 1511
		// (get) Token: 0x06001854 RID: 6228 RVA: 0x00054365 File Offset: 0x00052565
		// (set) Token: 0x06001855 RID: 6229 RVA: 0x0005436D File Offset: 0x0005256D
		[DataMember(Name = "request")]
		public NotificationSubscribeJsonRequest Request { get; set; }

		// Token: 0x170005E8 RID: 1512
		// (get) Token: 0x06001856 RID: 6230 RVA: 0x00054376 File Offset: 0x00052576
		// (set) Token: 0x06001857 RID: 6231 RVA: 0x0005437E File Offset: 0x0005257E
		[DataMember(Name = "subscriptionData")]
		public SubscriptionData[] SubscriptionData { get; set; }
	}
}
