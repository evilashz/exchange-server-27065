using System;
using System.Collections.Generic;
using Microsoft.Exchange.Common;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x0200008E RID: 142
	internal interface IPushNotificationPublisherConfigurationProvider
	{
		// Token: 0x060004C9 RID: 1225
		IEnumerable<IPushNotificationRawSettings> LoadSettings(bool ignoreErrors = true);
	}
}
