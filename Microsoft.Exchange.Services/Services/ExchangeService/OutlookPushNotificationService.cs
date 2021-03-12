using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Services.Core;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.ExchangeService
{
	// Token: 0x02000DE5 RID: 3557
	internal class OutlookPushNotificationService : DisposeTrackableBase, IPushNotificationService, IDisposable
	{
		// Token: 0x06005C0B RID: 23563 RVA: 0x0011DDF7 File Offset: 0x0011BFF7
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<OutlookPushNotificationService>(this);
		}

		// Token: 0x06005C0C RID: 23564 RVA: 0x0011DDFF File Offset: 0x0011BFFF
		protected override void InternalDispose(bool disposing)
		{
		}

		// Token: 0x06005C0D RID: 23565 RVA: 0x0011DE01 File Offset: 0x0011C001
		public void EnqueuePendingOutlookPushNotification(ExchangeServiceTraceDelegate traceDelegate, IOutlookPushNotificationSubscriptionCache subscriptionCache, IOutlookPushNotificationSerializer serializer, string notificationContext, PendingOutlookPushNotification notification)
		{
			OutlookPushNotificationBatchManager.GetInstance(traceDelegate, subscriptionCache, serializer).Add(notificationContext, notification);
		}
	}
}
