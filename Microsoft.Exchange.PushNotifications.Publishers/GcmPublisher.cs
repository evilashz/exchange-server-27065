using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.PushNotifications;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x020000A4 RID: 164
	internal sealed class GcmPublisher : PushNotificationPublisher<GcmNotification, GcmChannel>
	{
		// Token: 0x060005B7 RID: 1463 RVA: 0x00012ED9 File Offset: 0x000110D9
		public GcmPublisher(GcmPublisherSettings publisherSettings, IThrottlingManager throttlingManager, List<IPushNotificationMapping<GcmNotification>> mappings = null) : this(publisherSettings, throttlingManager, ExTraceGlobals.GcmPublisherTracer, mappings)
		{
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x00012EE9 File Offset: 0x000110E9
		public GcmPublisher(GcmPublisherSettings publisherSettings, IThrottlingManager throttlingManager, ITracer tracer, List<IPushNotificationMapping<GcmNotification>> mappings = null) : base(publisherSettings, tracer, throttlingManager, mappings, null, null)
		{
			this.ChannelSettings = publisherSettings.ChannelSettings;
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x060005B9 RID: 1465 RVA: 0x00012F04 File Offset: 0x00011104
		// (set) Token: 0x060005BA RID: 1466 RVA: 0x00012F0C File Offset: 0x0001110C
		private GcmChannelSettings ChannelSettings { get; set; }

		// Token: 0x060005BB RID: 1467 RVA: 0x00012F15 File Offset: 0x00011115
		protected override GcmChannel CreateNotificationChannel()
		{
			return new GcmChannel(this.ChannelSettings, base.Tracer, null, null);
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x00012F2A File Offset: 0x0001112A
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<GcmPublisher>(this);
		}
	}
}
