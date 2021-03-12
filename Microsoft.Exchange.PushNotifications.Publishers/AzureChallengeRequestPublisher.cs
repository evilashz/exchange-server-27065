using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.PushNotifications;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x02000064 RID: 100
	internal sealed class AzureChallengeRequestPublisher : PushNotificationPublisher<AzureChallengeRequestNotification, AzureChallengeRequestChannel>
	{
		// Token: 0x060003AC RID: 940 RVA: 0x0000C6A9 File Offset: 0x0000A8A9
		public AzureChallengeRequestPublisher(AzureChallengeRequestPublisherSettings publisherSettings, List<IPushNotificationMapping<AzureChallengeRequestNotification>> mappings = null) : this(publisherSettings, ExTraceGlobals.AzureChallengeRequestPublisherTracer, mappings)
		{
		}

		// Token: 0x060003AD RID: 941 RVA: 0x0000C6B8 File Offset: 0x0000A8B8
		public AzureChallengeRequestPublisher(AzureChallengeRequestPublisherSettings publisherSettings, ITracer tracer, List<IPushNotificationMapping<AzureChallengeRequestNotification>> mappings = null) : base(publisherSettings, tracer, null, mappings, null, null)
		{
			this.ChannelSettings = publisherSettings.ChannelSettings;
		}

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x060003AE RID: 942 RVA: 0x0000C6D4 File Offset: 0x0000A8D4
		// (remove) Token: 0x060003AF RID: 943 RVA: 0x0000C70C File Offset: 0x0000A90C
		public event EventHandler<MissingHubEventArgs> MissingHubDetected;

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x060003B0 RID: 944 RVA: 0x0000C741 File Offset: 0x0000A941
		// (set) Token: 0x060003B1 RID: 945 RVA: 0x0000C749 File Offset: 0x0000A949
		private AzureChallengeRequestChannelSettings ChannelSettings { get; set; }

		// Token: 0x060003B2 RID: 946 RVA: 0x0000C752 File Offset: 0x0000A952
		protected override AzureChallengeRequestChannel CreateNotificationChannel()
		{
			return new AzureChallengeRequestChannel(this.ChannelSettings, base.Tracer, this.MissingHubDetected, null, null);
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x0000C76D File Offset: 0x0000A96D
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<AzureChallengeRequestPublisher>(this);
		}
	}
}
