using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000B06 RID: 2822
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class PushNotificationSubscriptionItemSchema : ItemSchema
	{
		// Token: 0x17001C3A RID: 7226
		// (get) Token: 0x06006690 RID: 26256 RVA: 0x001B325C File Offset: 0x001B145C
		public new static PushNotificationSubscriptionItemSchema Instance
		{
			get
			{
				if (PushNotificationSubscriptionItemSchema.instance == null)
				{
					lock (PushNotificationSubscriptionItemSchema.syncObj)
					{
						if (PushNotificationSubscriptionItemSchema.instance == null)
						{
							PushNotificationSubscriptionItemSchema.instance = new PushNotificationSubscriptionItemSchema();
						}
					}
				}
				return PushNotificationSubscriptionItemSchema.instance;
			}
		}

		// Token: 0x04003A29 RID: 14889
		[Autoload]
		public static readonly StorePropertyDefinition SubscriptionId = InternalSchema.PushNotificationSubscriptionId;

		// Token: 0x04003A2A RID: 14890
		[Autoload]
		public static readonly StorePropertyDefinition LastUpdateTimeUTC = InternalSchema.PushNotificationSubscriptionLastUpdateTimeUTC;

		// Token: 0x04003A2B RID: 14891
		[Autoload]
		public static readonly StorePropertyDefinition SerializedNotificationSubscription = InternalSchema.PushNotificationSubscription;

		// Token: 0x04003A2C RID: 14892
		private static readonly object syncObj = new object();

		// Token: 0x04003A2D RID: 14893
		private static PushNotificationSubscriptionItemSchema instance = null;
	}
}
