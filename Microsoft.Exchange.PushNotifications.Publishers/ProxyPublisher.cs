using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.PushNotifications;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x020000C6 RID: 198
	internal sealed class ProxyPublisher : PushNotificationPublisher<ProxyNotification, ProxyChannel>
	{
		// Token: 0x0600069C RID: 1692 RVA: 0x0001534F File Offset: 0x0001354F
		public ProxyPublisher(ProxyPublisherSettings publisherSettings) : this(publisherSettings, ExTraceGlobals.ProxyPublisherTracer)
		{
		}

		// Token: 0x0600069D RID: 1693 RVA: 0x00015360 File Offset: 0x00013560
		public ProxyPublisher(ProxyPublisherSettings publisherSettings, ITracer tracer)
		{
			List<IPushNotificationMapping<ProxyNotification>> mappings = null;
			base..ctor(publisherSettings, tracer, null, mappings, null, null);
			this.ChannelSettings = publisherSettings.ChannelSettings;
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x0600069E RID: 1694 RVA: 0x00015387 File Offset: 0x00013587
		// (set) Token: 0x0600069F RID: 1695 RVA: 0x0001538F File Offset: 0x0001358F
		private ProxyChannelSettings ChannelSettings { get; set; }

		// Token: 0x060006A0 RID: 1696 RVA: 0x00015398 File Offset: 0x00013598
		protected override ProxyChannel CreateNotificationChannel()
		{
			return new ProxyChannel(this.ChannelSettings, base.Tracer, null, null, null);
		}

		// Token: 0x060006A1 RID: 1697 RVA: 0x000153AE File Offset: 0x000135AE
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<ProxyPublisher>(this);
		}
	}
}
