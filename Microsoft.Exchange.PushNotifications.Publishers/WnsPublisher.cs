using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.PushNotifications;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x020000EE RID: 238
	internal sealed class WnsPublisher : PushNotificationPublisher<WnsNotification, WnsChannel>
	{
		// Token: 0x060007A6 RID: 1958 RVA: 0x00017ED2 File Offset: 0x000160D2
		public WnsPublisher(WnsPublisherSettings publisherSettings, IThrottlingManager throttlingManager, List<IPushNotificationMapping<WnsNotification>> mappings = null) : this(publisherSettings, throttlingManager, ExTraceGlobals.WnsPublisherTracer, mappings)
		{
		}

		// Token: 0x060007A7 RID: 1959 RVA: 0x00017EE2 File Offset: 0x000160E2
		public WnsPublisher(WnsPublisherSettings publisherSettings, IThrottlingManager throttlingManager, ITracer tracer, List<IPushNotificationMapping<WnsNotification>> mappings = null) : base(publisherSettings, tracer, throttlingManager, mappings, null, null)
		{
			this.ChannelSettings = publisherSettings.ChannelSettings;
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x060007A8 RID: 1960 RVA: 0x00017EFD File Offset: 0x000160FD
		// (set) Token: 0x060007A9 RID: 1961 RVA: 0x00017F05 File Offset: 0x00016105
		private WnsChannelSettings ChannelSettings { get; set; }

		// Token: 0x060007AA RID: 1962 RVA: 0x00017F0E File Offset: 0x0001610E
		protected override WnsChannel CreateNotificationChannel()
		{
			return new WnsChannel(this.ChannelSettings, base.Tracer, null, null);
		}

		// Token: 0x060007AB RID: 1963 RVA: 0x00017F23 File Offset: 0x00016123
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<WnsPublisher>(this);
		}
	}
}
