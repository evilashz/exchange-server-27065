using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020003ED RID: 1005
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public sealed class UnsubscribeToNotificationRequest
	{
		// Token: 0x170007D0 RID: 2000
		// (get) Token: 0x06002068 RID: 8296 RVA: 0x00078A42 File Offset: 0x00076C42
		// (set) Token: 0x06002069 RID: 8297 RVA: 0x00078A4A File Offset: 0x00076C4A
		[DataMember(Name = "subscriptionData", IsRequired = true)]
		public SubscriptionData[] SubscriptionData { get; set; }
	}
}
