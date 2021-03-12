using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x02000059 RID: 89
	internal class AzureSendRequest : AzureRequestBase
	{
		// Token: 0x06000360 RID: 864 RVA: 0x0000B8F0 File Offset: 0x00009AF0
		public AzureSendRequest(AzureNotification notification, AzureSasToken sasToken, string resourceUri, string azureTag = null) : base("application/json;charset=utf-8", "POST", sasToken, resourceUri, "")
		{
			ArgumentValidator.ThrowIfNull("notification", notification);
			base.Headers["ServiceBusNotification-Format"] = "template";
			base.Headers["TrackingId"] = notification.Identifier;
			this.Target = notification.RecipientId;
			base.RequestBody = notification.SerializedPaylod;
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000361 RID: 865 RVA: 0x0000B962 File Offset: 0x00009B62
		// (set) Token: 0x06000362 RID: 866 RVA: 0x0000B974 File Offset: 0x00009B74
		public string Target
		{
			get
			{
				return base.Headers["ServiceBusNotification-Tags"];
			}
			private set
			{
				base.Headers["ServiceBusNotification-Tags"] = value;
			}
		}

		// Token: 0x04000176 RID: 374
		public const string SendNotificationContentType = "application/json;charset=utf-8";

		// Token: 0x04000177 RID: 375
		public const string HeaderFormatName = "ServiceBusNotification-Format";

		// Token: 0x04000178 RID: 376
		public const string HeaderFormat = "template";

		// Token: 0x04000179 RID: 377
		public const string HeaderTagName = "ServiceBusNotification-Tags";

		// Token: 0x0400017A RID: 378
		public const string HeaderNotificationTrackingId = "TrackingId";
	}
}
