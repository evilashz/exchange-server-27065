using System;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.PushNotifications
{
	// Token: 0x0200003E RID: 62
	public class Constants
	{
		// Token: 0x0400007A RID: 122
		public const uint DefaultSubscriptionExpirationInHours = 72U;

		// Token: 0x0400007B RID: 123
		public const string PushNotificationNamespace = "http://schemas.datacontract.org/2004/07/Microsoft.Exchange.PushNotifications.Wcf";

		// Token: 0x0400007C RID: 124
		public const string PushNotificationsServiceRelativeUri = "PushNotifications/service.svc";

		// Token: 0x0400007D RID: 125
		public const string NamedPipeUri = "net.pipe://localhost/PushNotifications/service.svc";

		// Token: 0x0400007E RID: 126
		public const string PushNotificationsAppPoolId = "MSExchangePushNotificationsAppPool";

		// Token: 0x0400007F RID: 127
		public const string OnPremPublishNotificationsUriTemplate = "PublishOnPremNotifications";

		// Token: 0x04000080 RID: 128
		public const string OnPremPublishNotificationsRelativeUri = "PushNotifications/service.svc/PublishOnPremNotifications";

		// Token: 0x04000081 RID: 129
		public const string GetAppConfigDataAddress = "AppConfig";

		// Token: 0x04000082 RID: 130
		public const string GetAppConfigDataUriTemplate = "GetAppConfigData";

		// Token: 0x04000083 RID: 131
		public const string GetAppConfigDataRelativeUri = "PushNotifications/service.svc/AppConfig/GetAppConfigData";

		// Token: 0x04000084 RID: 132
		public const string RegistryRootPath = "SYSTEM\\CurrentControlSet\\Services\\MSExchange PushNotifications";

		// Token: 0x04000085 RID: 133
		public static readonly ExDateTime EpochBaseTime = new ExDateTime(ExTimeZone.UtcTimeZone, 1970, 1, 1);
	}
}
