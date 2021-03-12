using System;
using System.Threading;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.PushNotifications.CrimsonEvents;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x020000AE RID: 174
	internal sealed class PendingGetChannel : PushNotificationChannel<PendingGetNotification>
	{
		// Token: 0x060005E7 RID: 1511 RVA: 0x000134AF File Offset: 0x000116AF
		public PendingGetChannel(string appId, IPendingGetConnectionCache connectionCache, ITracer tracer) : base(appId, tracer)
		{
			this.connectionCache = connectionCache;
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x000134C0 File Offset: 0x000116C0
		public override void Send(PendingGetNotification notification, CancellationToken cancelToken)
		{
			base.CheckDisposed();
			IPendingGetConnection pendingGetConnection = null;
			if (!this.connectionCache.TryGetConnection(notification.SubscriptionId, out pendingGetConnection))
			{
				PushNotificationsCrimsonEvents.PendingGetConnectionUnavailable.Log<string, string>(notification.Identifier, notification.SubscriptionId);
				base.Tracer.TraceDebug<PendingGetNotification>((long)this.GetHashCode(), "[Send] Skip to send notification '{0}' because no connection was found in the cache.", notification);
				return;
			}
			if (pendingGetConnection.FireUnseenEmailNotification(notification.Payload.EmailCount.Value, notification.SequenceNumber))
			{
				base.Tracer.TraceDebug<PendingGetNotification>((long)this.GetHashCode(), "[Send] Successfully sent notification '{0}'.", notification);
				PushNotificationTracker.ReportSent(notification, PushNotificationPlatform.None);
				return;
			}
			base.Tracer.TraceDebug<PendingGetNotification>((long)this.GetHashCode(), "[Send] Skip to send notification '{0}' because the pending get connection is not available yet.", notification);
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x00013572 File Offset: 0x00011772
		protected override void InternalDispose(bool disposing)
		{
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x00013574 File Offset: 0x00011774
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<PendingGetChannel>(this);
		}

		// Token: 0x040002F5 RID: 757
		private IPendingGetConnectionCache connectionCache;
	}
}
