using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x02000090 RID: 144
	internal class ConfigurationChangedEventArgs : EventArgs
	{
		// Token: 0x060004CC RID: 1228 RVA: 0x0000FB98 File Offset: 0x0000DD98
		public ConfigurationChangedEventArgs(PushNotificationPublisherConfiguration updatedConfiguration)
		{
			ArgumentValidator.ThrowIfNull("updatedConfiguration", updatedConfiguration);
			this.UpdatedConfiguration = updatedConfiguration;
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x060004CD RID: 1229 RVA: 0x0000FBB2 File Offset: 0x0000DDB2
		// (set) Token: 0x060004CE RID: 1230 RVA: 0x0000FBBA File Offset: 0x0000DDBA
		public PushNotificationPublisherConfiguration UpdatedConfiguration { get; private set; }
	}
}
