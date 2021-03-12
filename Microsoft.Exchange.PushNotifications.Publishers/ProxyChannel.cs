using System;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.PushNotifications.Client;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x020000BC RID: 188
	internal class ProxyChannel : PushNotificationChannel<ProxyNotification>
	{
		// Token: 0x06000638 RID: 1592 RVA: 0x00013EA0 File Offset: 0x000120A0
		static ProxyChannel()
		{
			foreach (ExPerformanceCounter exPerformanceCounter in ProxyCounters.AllCounters)
			{
				exPerformanceCounter.Reset();
			}
		}

		// Token: 0x06000639 RID: 1593 RVA: 0x00013ECC File Offset: 0x000120CC
		public ProxyChannel(ProxyChannelSettings settings, ITracer tracer, OnPremPublisherServiceProxy onPremClient = null, AzureAppConfigDataServiceProxy onPremAppDataClient = null, ProxyErrorTracker errorTracker = null) : base(settings.AppId, tracer)
		{
			ArgumentValidator.ThrowIfNull("settings", settings);
			this.Settings = settings;
			this.LegacyChannel = new ProxyChannelLegacy(settings, tracer, onPremClient, errorTracker);
			this.AppDataChannel = new ProxyChannelAppData(settings, tracer, onPremAppDataClient, errorTracker, null);
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x0600063A RID: 1594 RVA: 0x00013F1A File Offset: 0x0001211A
		// (set) Token: 0x0600063B RID: 1595 RVA: 0x00013F22 File Offset: 0x00012122
		private ProxyChannelSettings Settings { get; set; }

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x0600063C RID: 1596 RVA: 0x00013F2B File Offset: 0x0001212B
		// (set) Token: 0x0600063D RID: 1597 RVA: 0x00013F33 File Offset: 0x00012133
		private ProxyChannelLegacy LegacyChannel { get; set; }

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x0600063E RID: 1598 RVA: 0x00013F3C File Offset: 0x0001213C
		// (set) Token: 0x0600063F RID: 1599 RVA: 0x00013F44 File Offset: 0x00012144
		private ProxyChannelAppData AppDataChannel { get; set; }

		// Token: 0x06000640 RID: 1600 RVA: 0x00013F50 File Offset: 0x00012150
		public override void Send(ProxyNotification notification, CancellationToken cancelToken)
		{
			base.CheckDisposed();
			ArgumentValidator.ThrowIfNull("notification", notification);
			if (notification.NotificationBatch != null && notification.NotificationBatch.Count > 0)
			{
				this.LegacyChannel.Send(notification, cancelToken);
				return;
			}
			this.AppDataChannel.Send(notification, cancelToken);
		}

		// Token: 0x06000641 RID: 1601 RVA: 0x00013F9F File Offset: 0x0001219F
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				base.Tracer.TraceDebug<string>((long)this.GetHashCode(), "[InternalDispose] Disposing the channel for '{0}'", base.AppId);
				this.LegacyChannel.Dispose();
				this.AppDataChannel.Dispose();
			}
		}

		// Token: 0x06000642 RID: 1602 RVA: 0x00013FD7 File Offset: 0x000121D7
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<ProxyChannel>(this);
		}
	}
}
