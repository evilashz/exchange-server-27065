using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.PushNotifications.CrimsonEvents;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x02000392 RID: 914
	internal class UnsubscribeToPushNotification : BaseSubscribeToPushNotificationCommand<UnsubscribeToPushNotificationRequest>
	{
		// Token: 0x0600198C RID: 6540 RVA: 0x000917AD File Offset: 0x0008F9AD
		public UnsubscribeToPushNotification(CallContext callContext, UnsubscribeToPushNotificationRequest request) : base(callContext, request)
		{
		}

		// Token: 0x0600198D RID: 6541 RVA: 0x000917B7 File Offset: 0x0008F9B7
		internal override IExchangeWebMethodResponse GetResponse()
		{
			return new UnsubscribeToPushNotificationResponse(base.Result.Code, base.Result.Error);
		}

		// Token: 0x0600198E RID: 6542 RVA: 0x000917D4 File Offset: 0x0008F9D4
		protected override void InternalExecute(string subscriptionId)
		{
			using (IPushNotificationStorage pushNotificationStorage = PushNotificationStorage.Find(base.MailboxIdentityMailboxSession))
			{
				if (pushNotificationStorage != null)
				{
					pushNotificationStorage.DeleteSubscription(subscriptionId);
				}
			}
		}

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x0600198F RID: 6543 RVA: 0x00091814 File Offset: 0x0008FA14
		protected override string ClassName
		{
			get
			{
				return "UnsubscribeToPushNotification";
			}
		}

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06001990 RID: 6544 RVA: 0x0009181B File Offset: 0x0008FA1B
		protected override PushNotificationsCrimsonEvent ArgumentNullEvent
		{
			get
			{
				return PushNotificationsCrimsonEvents.UnsubscriptionArgumentNull;
			}
		}

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06001991 RID: 6545 RVA: 0x00091822 File Offset: 0x0008FA22
		protected override PushNotificationsCrimsonEvent InvalidNotificationTypeEvent
		{
			get
			{
				return PushNotificationsCrimsonEvents.UnsubscriptionInvalidNotificationType;
			}
		}

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06001992 RID: 6546 RVA: 0x00091829 File Offset: 0x0008FA29
		protected override PushNotificationsCrimsonEvent UnexpectedErrorEvent
		{
			get
			{
				return PushNotificationsCrimsonEvents.UnsubscriptionUnexpectedError;
			}
		}

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x06001993 RID: 6547 RVA: 0x00091830 File Offset: 0x0008FA30
		protected override PushNotificationsCrimsonEvent RequestedEvent
		{
			get
			{
				return PushNotificationsCrimsonEvents.UnsubscriptionRequested;
			}
		}
	}
}
