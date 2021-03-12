using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.PushNotifications;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x02000050 RID: 80
	internal sealed class AzurePublisher : PushNotificationPublisher<AzureNotification, AzureChannel>
	{
		// Token: 0x0600030E RID: 782 RVA: 0x0000AD6F File Offset: 0x00008F6F
		public AzurePublisher(AzurePublisherSettings publisherSettings, List<IPushNotificationMapping<AzureNotification>> mappings = null) : this(publisherSettings, ExTraceGlobals.AzurePublisherTracer, mappings)
		{
		}

		// Token: 0x0600030F RID: 783 RVA: 0x0000AD7E File Offset: 0x00008F7E
		public AzurePublisher(AzurePublisherSettings publisherSettings, ITracer tracer, List<IPushNotificationMapping<AzureNotification>> mappings = null) : base(publisherSettings, tracer, null, mappings, null, null)
		{
			this.ChannelSettings = publisherSettings.ChannelSettings;
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000310 RID: 784 RVA: 0x0000AD98 File Offset: 0x00008F98
		// (set) Token: 0x06000311 RID: 785 RVA: 0x0000ADA0 File Offset: 0x00008FA0
		private AzureChannelSettings ChannelSettings { get; set; }

		// Token: 0x06000312 RID: 786 RVA: 0x0000ADA9 File Offset: 0x00008FA9
		protected override AzureChannel CreateNotificationChannel()
		{
			return new AzureChannel(this.ChannelSettings, base.Tracer, null, null);
		}

		// Token: 0x06000313 RID: 787 RVA: 0x0000ADBE File Offset: 0x00008FBE
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<AzurePublisher>(this);
		}
	}
}
