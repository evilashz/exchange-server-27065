using System;
using System.Collections.Generic;
using Microsoft.Exchange.Clients.Owa.Server.LyncIMLogging;
using Microsoft.Exchange.InstantMessaging;
using Microsoft.Exchange.Services;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000139 RID: 313
	internal sealed class InstantMessageOCSNotifier : InstantMessageNotifier
	{
		// Token: 0x06000A96 RID: 2710 RVA: 0x0002459F File Offset: 0x0002279F
		internal InstantMessageOCSNotifier(UserContext userContext) : base(userContext)
		{
			this.deliverySuccessNotifications = new List<DeliverySuccessNotification>();
		}

		// Token: 0x06000A97 RID: 2711 RVA: 0x000245B4 File Offset: 0x000227B4
		public override IList<NotificationPayloadBase> ReadDataAndResetState()
		{
			IList<NotificationPayloadBase> result = base.ReadDataAndResetState();
			lock (this.deliverySuccessNotifications)
			{
				foreach (DeliverySuccessNotification deliverySuccessNotification in this.deliverySuccessNotifications)
				{
					ExTraceGlobals.InstantMessagingTracer.TraceDebug((long)this.GetHashCode(), "InstantMessageOCSNotifier.ReadDataAndResetState. BeginNotifyDeliverySuccess Message Id: {0}", new object[]
					{
						deliverySuccessNotification.MessageId
					});
					deliverySuccessNotification.Provider.NotifyDeliverySuccess(deliverySuccessNotification);
				}
				this.deliverySuccessNotifications.Clear();
			}
			return result;
		}

		// Token: 0x06000A98 RID: 2712 RVA: 0x00024678 File Offset: 0x00022878
		internal void RegisterDeliverySuccessNotification(InstantMessageOCSProvider provider, IIMModality context, int messageId, RequestDetailsLogger logger)
		{
			lock (this.deliverySuccessNotifications)
			{
				this.deliverySuccessNotifications.Add(new DeliverySuccessNotification(provider, context, messageId, logger));
			}
		}

		// Token: 0x06000A99 RID: 2713 RVA: 0x000246C8 File Offset: 0x000228C8
		protected override void Cancel()
		{
			base.Cancel();
			lock (this.deliverySuccessNotifications)
			{
				this.deliverySuccessNotifications.Clear();
			}
		}

		// Token: 0x040006E2 RID: 1762
		private List<DeliverySuccessNotification> deliverySuccessNotifications;
	}
}
