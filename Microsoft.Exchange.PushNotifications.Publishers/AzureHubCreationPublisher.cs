using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.PushNotifications;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x02000084 RID: 132
	internal sealed class AzureHubCreationPublisher : PushNotificationPublisher<AzureHubCreationNotification, AzureHubCreationChannel>
	{
		// Token: 0x0600049A RID: 1178 RVA: 0x0000F4E5 File Offset: 0x0000D6E5
		public AzureHubCreationPublisher(AzureHubCreationPublisherSettings publisherSettings, List<IPushNotificationMapping<AzureHubCreationNotification>> mappings = null) : this(publisherSettings, ExTraceGlobals.AzureHubCreationPublisherTracer, mappings)
		{
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x0000F4F4 File Offset: 0x0000D6F4
		public AzureHubCreationPublisher(AzureHubCreationPublisherSettings publisherSettings, ITracer tracer, List<IPushNotificationMapping<AzureHubCreationNotification>> mappings = null) : base(publisherSettings, tracer, null, mappings, null, null)
		{
			this.ChannelSettings = publisherSettings.ChannelSettings;
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x0600049C RID: 1180 RVA: 0x0000F50E File Offset: 0x0000D70E
		// (set) Token: 0x0600049D RID: 1181 RVA: 0x0000F516 File Offset: 0x0000D716
		private AzureHubCreationChannelSettings ChannelSettings { get; set; }

		// Token: 0x0600049E RID: 1182 RVA: 0x0000F51F File Offset: 0x0000D71F
		protected override AzureHubCreationChannel CreateNotificationChannel()
		{
			return new AzureHubCreationChannel(this.ChannelSettings, base.Tracer, null, null);
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x0000F534 File Offset: 0x0000D734
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<AzureHubCreationPublisher>(this);
		}
	}
}
