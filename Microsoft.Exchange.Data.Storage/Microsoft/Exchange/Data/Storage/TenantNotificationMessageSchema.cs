using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000CE1 RID: 3297
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class TenantNotificationMessageSchema : MessageItemSchema
	{
		// Token: 0x17001E71 RID: 7793
		// (get) Token: 0x0600720D RID: 29197 RVA: 0x001F9022 File Offset: 0x001F7222
		public new static TenantNotificationMessageSchema Instance
		{
			get
			{
				if (TenantNotificationMessageSchema.instance == null)
				{
					TenantNotificationMessageSchema.instance = new TenantNotificationMessageSchema();
				}
				return TenantNotificationMessageSchema.instance;
			}
		}

		// Token: 0x04004F42 RID: 20290
		private static TenantNotificationMessageSchema instance;

		// Token: 0x04004F43 RID: 20291
		public static readonly StorePropertyDefinition MonitoringEventInstanceId = InternalSchema.MonitoringEventInstanceId;

		// Token: 0x04004F44 RID: 20292
		public static readonly StorePropertyDefinition MonitoringEventSource = InternalSchema.MonitoringEventSource;

		// Token: 0x04004F45 RID: 20293
		public static readonly StorePropertyDefinition MonitoringEventCategoryId = InternalSchema.MonitoringEventCategoryId;

		// Token: 0x04004F46 RID: 20294
		public static readonly StorePropertyDefinition MonitoringEventTimeUtc = InternalSchema.MonitoringEventTimeUtc;

		// Token: 0x04004F47 RID: 20295
		public static readonly StorePropertyDefinition MonitoringInsertionStrings = InternalSchema.MonitoringInsertionStrings;

		// Token: 0x04004F48 RID: 20296
		public static readonly StorePropertyDefinition MonitoringUniqueId = InternalSchema.MonitoringUniqueId;

		// Token: 0x04004F49 RID: 20297
		public static readonly StorePropertyDefinition MonitoringNotificationEmailSent = InternalSchema.MonitoringNotificationEmailSent;

		// Token: 0x04004F4A RID: 20298
		public static readonly StorePropertyDefinition MonitoringCreationTimeUtc = InternalSchema.MonitoringCreationTimeUtc;

		// Token: 0x04004F4B RID: 20299
		public static readonly StorePropertyDefinition MonitoringEventEntryType = InternalSchema.MonitoringEventEntryType;

		// Token: 0x04004F4C RID: 20300
		public static readonly PropertyDefinition MonitoringCountOfNotificationsSentInPast24Hours = InternalSchema.MonitoringCountOfNotificationsSentInPast24Hours;

		// Token: 0x04004F4D RID: 20301
		public static readonly PropertyDefinition MonitoringNotificationRecipients = InternalSchema.MonitoringNotificationRecipients;

		// Token: 0x04004F4E RID: 20302
		public static readonly PropertyDefinition MonitoringHashCodeForDuplicateDetection = InternalSchema.MonitoringHashCodeForDuplicateDetection;

		// Token: 0x04004F4F RID: 20303
		public static readonly PropertyDefinition MonitoringNotificationMessageIds = InternalSchema.MonitoringNotificationMessageIds;

		// Token: 0x04004F50 RID: 20304
		public static readonly PropertyDefinition MonitoringEventPeriodicKey = InternalSchema.MonitoringEventPeriodicKey;
	}
}
