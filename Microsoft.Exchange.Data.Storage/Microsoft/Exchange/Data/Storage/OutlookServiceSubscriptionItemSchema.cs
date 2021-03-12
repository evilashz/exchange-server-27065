using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000936 RID: 2358
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class OutlookServiceSubscriptionItemSchema : ItemSchema
	{
		// Token: 0x17001862 RID: 6242
		// (get) Token: 0x060057E0 RID: 22496 RVA: 0x00169DF8 File Offset: 0x00167FF8
		public new static OutlookServiceSubscriptionItemSchema Instance
		{
			get
			{
				if (OutlookServiceSubscriptionItemSchema.instance == null)
				{
					lock (OutlookServiceSubscriptionItemSchema.syncObj)
					{
						if (OutlookServiceSubscriptionItemSchema.instance == null)
						{
							OutlookServiceSubscriptionItemSchema.instance = new OutlookServiceSubscriptionItemSchema();
						}
					}
				}
				return OutlookServiceSubscriptionItemSchema.instance;
			}
		}

		// Token: 0x04002FED RID: 12269
		[Autoload]
		public static readonly StorePropertyDefinition SubscriptionId = InternalSchema.OutlookServiceSubscriptionId;

		// Token: 0x04002FEE RID: 12270
		[Autoload]
		public static readonly StorePropertyDefinition LastUpdateTimeUTC = InternalSchema.OutlookServiceSubscriptionLastUpdateTimeUTC;

		// Token: 0x04002FEF RID: 12271
		[Autoload]
		public static readonly StorePropertyDefinition PackageId = InternalSchema.OutlookServicePackageId;

		// Token: 0x04002FF0 RID: 12272
		[Autoload]
		public static readonly StorePropertyDefinition AppId = InternalSchema.OutlookServiceAppId;

		// Token: 0x04002FF1 RID: 12273
		[Autoload]
		public static readonly StorePropertyDefinition DeviceNotificationId = InternalSchema.OutlookServiceDeviceNotificationId;

		// Token: 0x04002FF2 RID: 12274
		[Autoload]
		public static readonly StorePropertyDefinition ExpirationTime = InternalSchema.OutlookServiceExpirationTime;

		// Token: 0x04002FF3 RID: 12275
		[Autoload]
		public static readonly StorePropertyDefinition LockScreen = InternalSchema.OutlookServiceLockScreen;

		// Token: 0x04002FF4 RID: 12276
		private static readonly object syncObj = new object();

		// Token: 0x04002FF5 RID: 12277
		private static OutlookServiceSubscriptionItemSchema instance = null;
	}
}
