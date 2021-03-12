using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.PushNotifications;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x02000071 RID: 113
	internal sealed class AzureDeviceRegistrationPublisher : PushNotificationPublisher<AzureDeviceRegistrationNotification, AzureDeviceRegistrationChannel>
	{
		// Token: 0x0600040A RID: 1034 RVA: 0x0000DA5E File Offset: 0x0000BC5E
		public AzureDeviceRegistrationPublisher(AzureDeviceRegistrationPublisherSettings publisherSettings, List<IPushNotificationMapping<AzureDeviceRegistrationNotification>> mappings = null) : this(publisherSettings, ExTraceGlobals.AzureDeviceRegistrationPublisherTracer, mappings)
		{
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x0000DA6D File Offset: 0x0000BC6D
		public AzureDeviceRegistrationPublisher(AzureDeviceRegistrationPublisherSettings publisherSettings, ITracer tracer, List<IPushNotificationMapping<AzureDeviceRegistrationNotification>> mappings = null) : base(publisherSettings, tracer, null, mappings, null, null)
		{
			this.ChannelSettings = publisherSettings.ChannelSettings;
		}

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x0600040C RID: 1036 RVA: 0x0000DA88 File Offset: 0x0000BC88
		// (remove) Token: 0x0600040D RID: 1037 RVA: 0x0000DAC0 File Offset: 0x0000BCC0
		public event EventHandler<MissingHubEventArgs> MissingHubDetected;

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x0600040E RID: 1038 RVA: 0x0000DAF5 File Offset: 0x0000BCF5
		// (set) Token: 0x0600040F RID: 1039 RVA: 0x0000DAFD File Offset: 0x0000BCFD
		private AzureDeviceRegistrationChannelSettings ChannelSettings { get; set; }

		// Token: 0x06000410 RID: 1040 RVA: 0x0000DB06 File Offset: 0x0000BD06
		protected override AzureDeviceRegistrationChannel CreateNotificationChannel()
		{
			return new AzureDeviceRegistrationChannel(this.ChannelSettings, base.Tracer, this.MissingHubDetected, null, null, null, null);
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x0000DB23 File Offset: 0x0000BD23
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<AzureDeviceRegistrationPublisher>(this);
		}
	}
}
