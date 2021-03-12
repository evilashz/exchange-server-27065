using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.PushNotifications;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x020000B6 RID: 182
	internal sealed class PendingGetPublisher : PushNotificationPublisher<PendingGetNotification, PendingGetChannel>
	{
		// Token: 0x06000618 RID: 1560 RVA: 0x00013BA9 File Offset: 0x00011DA9
		public PendingGetPublisher(PushNotificationPublisherSettings publisherSettings, IPendingGetConnectionCache connectionCache, ITracer tracer = null, IThrottlingManager throttlingManager = null, List<IPushNotificationMapping<PendingGetNotification>> mappings = null) : base(publisherSettings, tracer ?? ExTraceGlobals.PendingGetPublisherTracer, throttlingManager, mappings, null, null)
		{
			this.connectionCache = connectionCache;
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x00013BC9 File Offset: 0x00011DC9
		protected override PendingGetChannel CreateNotificationChannel()
		{
			return new PendingGetChannel(base.AppId, this.connectionCache, base.Tracer);
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x00013BE2 File Offset: 0x00011DE2
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<PendingGetPublisher>(this);
		}

		// Token: 0x04000309 RID: 777
		private IPendingGetConnectionCache connectionCache;
	}
}
