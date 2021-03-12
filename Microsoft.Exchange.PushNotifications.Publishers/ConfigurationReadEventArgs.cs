using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x02000091 RID: 145
	internal class ConfigurationReadEventArgs : EventArgs
	{
		// Token: 0x060004CF RID: 1231 RVA: 0x0000FBC3 File Offset: 0x0000DDC3
		public ConfigurationReadEventArgs(PushNotificationPublisherConfiguration configuration)
		{
			ArgumentValidator.ThrowIfNull("configuration", configuration);
			this.Configuration = configuration;
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x060004D0 RID: 1232 RVA: 0x0000FBDD File Offset: 0x0000DDDD
		// (set) Token: 0x060004D1 RID: 1233 RVA: 0x0000FBE5 File Offset: 0x0000DDE5
		public PushNotificationPublisherConfiguration Configuration { get; private set; }
	}
}
