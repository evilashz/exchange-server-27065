using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.PushNotifications;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000493 RID: 1171
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class UnsubscribeToPushNotificationRequest : BaseSubscribeToPushNotificationRequest
	{
		// Token: 0x060022E9 RID: 8937 RVA: 0x000A3641 File Offset: 0x000A1841
		public UnsubscribeToPushNotificationRequest()
		{
		}

		// Token: 0x060022EA RID: 8938 RVA: 0x000A3649 File Offset: 0x000A1849
		public UnsubscribeToPushNotificationRequest(PushNotificationSubscription subscription) : base(subscription)
		{
		}

		// Token: 0x060022EB RID: 8939 RVA: 0x000A3652 File Offset: 0x000A1852
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new UnsubscribeToPushNotification(callContext, this);
		}
	}
}
