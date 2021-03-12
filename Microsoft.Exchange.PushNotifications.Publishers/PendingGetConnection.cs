using System;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Components.PushNotifications;
using Microsoft.Exchange.PushNotifications.CrimsonEvents;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x020000AF RID: 175
	internal sealed class PendingGetConnection : IPendingGetConnection
	{
		// Token: 0x060005EB RID: 1515 RVA: 0x0001357C File Offset: 0x0001177C
		public PendingGetConnection(string connectionId)
		{
			this.connectionId = connectionId;
			this.latestUnseenEmailNotification = new PendingGetConnection.UnseenEmailNotification();
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x00013598 File Offset: 0x00011798
		public void SubscribeToUnseenEmailNotification(PendingGetContext pendingGetContext, long timeoutInMilliseconds, int latestUnseenEmailNotificationId)
		{
			PendingGetConnection.UnseenEmailNotification unseenEmailNotification = this.latestUnseenEmailNotification;
			if (unseenEmailNotification.NotificationId > 0 && latestUnseenEmailNotificationId != unseenEmailNotification.NotificationId)
			{
				ExTraceGlobals.PendingGetPublisherTracer.TraceDebug<string, int, int>((long)this.GetHashCode(), "[SubscribeToUnseenEmailNotification] The latest Unseen Email notification did not deliver to client for PendingGet channel - '{0}'. Send it again as the client reconnects. The latest notification id in client is '{1}', the latest notification id in server is '{2}'", this.connectionId, latestUnseenEmailNotificationId, unseenEmailNotification.NotificationId);
				PushNotificationsCrimsonEvents.PendingGetPickupLostNotification.Log<string, int, int>(this.connectionId, latestUnseenEmailNotificationId, unseenEmailNotification.NotificationId);
				pendingGetContext.Response.Write(unseenEmailNotification.GetPayload(true));
				pendingGetContext.AsyncResult.CompletedSynchronously = true;
				pendingGetContext.AsyncResult.IsCompleted = true;
				return;
			}
			PendingGetContext pendingGetContext2 = Interlocked.Exchange<PendingGetContext>(ref this.context, pendingGetContext);
			if (pendingGetContext2 != null)
			{
				ExTraceGlobals.PendingGetPublisherTracer.TraceWarning<string>((long)this.GetHashCode(), "[SubscribeToUnseenEmailNotification] Client reconnects again while there is still connection on server for PendingGet channel - '{0}'. This means previous connection was dropped without server's awareness. Finish the previous request to cleanup.", this.connectionId);
				PushNotificationsCrimsonEvents.PendingGetCloseLostConnection.Log<string>(this.connectionId);
				pendingGetContext2.AsyncResult.IsCompleted = true;
			}
			ExTraceGlobals.PendingGetPublisherTracer.TraceDebug<long, string>((long)this.GetHashCode(), "[SubscribeToUnseenEmailNotification] Latest Unseen Email notification id matches. Setup a timer in {0} milliseconds to timeout the connection for PendingGet channel - '{1}'.", timeoutInMilliseconds, this.connectionId);
			this.SetupTimer(timeoutInMilliseconds);
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x00013690 File Offset: 0x00011890
		public bool FireUnseenEmailNotification(int unseenCount, int notificationId)
		{
			this.DisableTimer();
			ExTraceGlobals.PendingGetPublisherTracer.TraceDebug<string, int, int>((long)this.GetHashCode(), "[FireUnseenEmailNotification] PendingGet channel - '{0}' receives Unseen Email notification - unseenCount = '{1}', notificationId = '{2}'.", this.connectionId, unseenCount, notificationId);
			PendingGetConnection.UnseenEmailNotification unseenEmailNotification = new PendingGetConnection.UnseenEmailNotification
			{
				NotificationId = notificationId,
				UnseenEmailCount = unseenCount
			};
			this.latestUnseenEmailNotification = unseenEmailNotification;
			PendingGetContext pendingGetContext = Interlocked.Exchange<PendingGetContext>(ref this.context, null);
			if (pendingGetContext != null)
			{
				ExTraceGlobals.PendingGetPublisherTracer.TraceDebug<string, int, int>((long)this.GetHashCode(), "[FireUnseenEmailNotification] Found the connection for PendingGet channel '{0}' and writes the notification to the client - unseenCount = '{1}', notificationId = '{2}'.", this.connectionId, unseenCount, notificationId);
				pendingGetContext.Response.Write(unseenEmailNotification.GetPayload(false));
				pendingGetContext.AsyncResult.IsCompleted = true;
				return true;
			}
			ExTraceGlobals.PendingGetPublisherTracer.TraceWarning<string, int, int>((long)this.GetHashCode(), "[FireUnseenEmailNotification] PendingGet connection is not available for for channel '{0}'. Will try to deliver the notification (unseenCount = '{1}', notificationId = '{2}') once client connects again.", this.connectionId, unseenCount, notificationId);
			PushNotificationsCrimsonEvents.PendingGetArchiveNotifiction.Log<string, int, int>(this.connectionId, unseenCount, notificationId);
			return false;
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x00013760 File Offset: 0x00011960
		private void ElapsedConnectionTimeout(object state)
		{
			this.DisableTimer();
			PendingGetContext pendingGetContext = Interlocked.Exchange<PendingGetContext>(ref this.context, null);
			if (pendingGetContext != null)
			{
				ExTraceGlobals.PendingGetPublisherTracer.TraceDebug<string>((long)this.GetHashCode(), "[ElapsedConnectionTimeout] PendingGet connection times out in server. Complete the request for PendingGet channel '{0}'.", this.connectionId);
				pendingGetContext.AsyncResult.IsCompleted = true;
			}
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x000137AC File Offset: 0x000119AC
		private void SetupTimer(long timeoutInMilliseconds)
		{
			Timer value = new Timer(new TimerCallback(this.ElapsedConnectionTimeout), null, timeoutInMilliseconds, timeoutInMilliseconds);
			Interlocked.Exchange<Timer>(ref this.pendingRequestTimeoutTimer, value);
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x000137DC File Offset: 0x000119DC
		private void DisableTimer()
		{
			Timer timer = Interlocked.Exchange<Timer>(ref this.pendingRequestTimeoutTimer, null);
			if (timer != null)
			{
				timer.Change(-1, -1);
			}
		}

		// Token: 0x040002F6 RID: 758
		private readonly string connectionId;

		// Token: 0x040002F7 RID: 759
		private PendingGetContext context;

		// Token: 0x040002F8 RID: 760
		private PendingGetConnection.UnseenEmailNotification latestUnseenEmailNotification;

		// Token: 0x040002F9 RID: 761
		private Timer pendingRequestTimeoutTimer;

		// Token: 0x020000B0 RID: 176
		private class UnseenEmailNotification
		{
			// Token: 0x1700018E RID: 398
			// (get) Token: 0x060005F1 RID: 1521 RVA: 0x00013802 File Offset: 0x00011A02
			// (set) Token: 0x060005F2 RID: 1522 RVA: 0x0001380A File Offset: 0x00011A0A
			public int NotificationId { get; set; }

			// Token: 0x1700018F RID: 399
			// (get) Token: 0x060005F3 RID: 1523 RVA: 0x00013813 File Offset: 0x00011A13
			// (set) Token: 0x060005F4 RID: 1524 RVA: 0x0001381B File Offset: 0x00011A1B
			public int UnseenEmailCount { get; set; }

			// Token: 0x060005F5 RID: 1525 RVA: 0x00013824 File Offset: 0x00011A24
			public string GetPayload(bool isCached)
			{
				return string.Format("{{\"type\":\"UnseenEmail\",\"id\":{0},\"UnseenCount\":{1},\"IsCached\":{2}}}", this.NotificationId, this.UnseenEmailCount, isCached);
			}

			// Token: 0x040002FA RID: 762
			private const string UnseenEmailPayloadTemplate = "{{\"type\":\"UnseenEmail\",\"id\":{0},\"UnseenCount\":{1},\"IsCached\":{2}}}";
		}
	}
}
