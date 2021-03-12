using System;
using System.Collections.Generic;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x02000095 RID: 149
	internal class RegistryConfigurationProvider : IPushNotificationPublisherConfigurationProvider
	{
		// Token: 0x0600050F RID: 1295 RVA: 0x000115A0 File Offset: 0x0000F7A0
		public IEnumerable<IPushNotificationRawSettings> LoadSettings(bool ignoreErrors = true)
		{
			RegistrySession registrySession = new RegistrySession(ignoreErrors);
			return registrySession.Find<RegistryPushNotificationApp>(RegistryPushNotificationApp.Schema.RootFolder);
		}
	}
}
