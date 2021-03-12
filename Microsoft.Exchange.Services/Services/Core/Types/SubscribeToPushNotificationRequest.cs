using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.PushNotifications;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000489 RID: 1161
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class SubscribeToPushNotificationRequest : BaseSubscribeToPushNotificationRequest
	{
		// Token: 0x0600227D RID: 8829 RVA: 0x000A322A File Offset: 0x000A142A
		public SubscribeToPushNotificationRequest()
		{
		}

		// Token: 0x0600227E RID: 8830 RVA: 0x000A3232 File Offset: 0x000A1432
		public SubscribeToPushNotificationRequest(PushNotificationSubscription subscription) : base(subscription)
		{
		}

		// Token: 0x17000572 RID: 1394
		// (get) Token: 0x0600227F RID: 8831 RVA: 0x000A323B File Offset: 0x000A143B
		// (set) Token: 0x06002280 RID: 8832 RVA: 0x000A3243 File Offset: 0x000A1443
		[DataMember(Name = "LastUnseenEmailCount", IsRequired = false)]
		public int LastUnseenEmailCount { get; set; }

		// Token: 0x06002281 RID: 8833 RVA: 0x000A324C File Offset: 0x000A144C
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new SubscribeToPushNotification(callContext, this);
		}
	}
}
