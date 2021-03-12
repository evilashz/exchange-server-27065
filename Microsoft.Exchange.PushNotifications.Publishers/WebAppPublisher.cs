using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.PushNotifications;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x020000D2 RID: 210
	internal sealed class WebAppPublisher : PushNotificationPublisher<WebAppNotification, WebAppChannel>
	{
		// Token: 0x060006DC RID: 1756 RVA: 0x00015BEE File Offset: 0x00013DEE
		public WebAppPublisher(WebAppPublisherSettings publisherSettings, List<IPushNotificationMapping<WebAppNotification>> mappings = null) : this(publisherSettings, ExTraceGlobals.WebAppPublisherTracer, mappings)
		{
		}

		// Token: 0x060006DD RID: 1757 RVA: 0x00015BFD File Offset: 0x00013DFD
		public WebAppPublisher(WebAppPublisherSettings publisherSettings, ITracer tracer, List<IPushNotificationMapping<WebAppNotification>> mappings = null) : base(publisherSettings, tracer, null, mappings, null, null)
		{
			this.ChannelSettings = publisherSettings.ChannelSettings;
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x060006DE RID: 1758 RVA: 0x00015C17 File Offset: 0x00013E17
		// (set) Token: 0x060006DF RID: 1759 RVA: 0x00015C1F File Offset: 0x00013E1F
		private WebAppChannelSettings ChannelSettings { get; set; }

		// Token: 0x060006E0 RID: 1760 RVA: 0x00015C28 File Offset: 0x00013E28
		protected override WebAppChannel CreateNotificationChannel()
		{
			return new WebAppChannel(this.ChannelSettings, base.Tracer, null);
		}

		// Token: 0x060006E1 RID: 1761 RVA: 0x00015C3C File Offset: 0x00013E3C
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<WebAppPublisher>(this);
		}
	}
}
