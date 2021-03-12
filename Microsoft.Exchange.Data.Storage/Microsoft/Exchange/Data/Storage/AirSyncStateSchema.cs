using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C0A RID: 3082
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class AirSyncStateSchema : StoreObjectSchema
	{
		// Token: 0x17001DD3 RID: 7635
		// (get) Token: 0x06006E04 RID: 28164 RVA: 0x001D87F9 File Offset: 0x001D69F9
		public new static AirSyncStateSchema Instance
		{
			get
			{
				if (AirSyncStateSchema.instance == null)
				{
					AirSyncStateSchema.instance = new AirSyncStateSchema();
				}
				return AirSyncStateSchema.instance;
			}
		}

		// Token: 0x04003EB6 RID: 16054
		public static readonly StorePropertyDefinition PropertyGroupChangeMask = InternalSchema.PropertyGroupChangeMask;

		// Token: 0x04003EB7 RID: 16055
		public static readonly StorePropertyDefinition PropertyGroupMappingId = InternalSchema.PropertyGroupMappingId;

		// Token: 0x04003EB8 RID: 16056
		public static readonly StorePropertyDefinition ClientCategoryList = InternalSchema.ClientCategoryList;

		// Token: 0x04003EB9 RID: 16057
		public static readonly StorePropertyDefinition LastSeenClientIds = InternalSchema.LastSeenClientIds;

		// Token: 0x04003EBA RID: 16058
		public static readonly StorePropertyDefinition LastSyncAttemptTime = InternalSchema.LastSyncAttemptTime;

		// Token: 0x04003EBB RID: 16059
		public static readonly StorePropertyDefinition LastSyncSuccessTime = InternalSchema.LastSyncSuccessTime;

		// Token: 0x04003EBC RID: 16060
		public static readonly StorePropertyDefinition LastSyncUserAgent = InternalSchema.LastSyncUserAgent;

		// Token: 0x04003EBD RID: 16061
		public static readonly StorePropertyDefinition MetadataLastSyncTime = InternalSchema.AirSyncLastSyncTime;

		// Token: 0x04003EBE RID: 16062
		public static readonly StorePropertyDefinition MetadataLocalCommitTimeMax = InternalSchema.AirSyncLocalCommitTimeMax;

		// Token: 0x04003EBF RID: 16063
		public static readonly StorePropertyDefinition MetadataDeletedCountTotal = InternalSchema.AirSyncDeletedCountTotal;

		// Token: 0x04003EC0 RID: 16064
		public static readonly StorePropertyDefinition MetadataSyncKey = InternalSchema.AirSyncSyncKey;

		// Token: 0x04003EC1 RID: 16065
		public static readonly StorePropertyDefinition MetadataFilter = InternalSchema.AirSyncFilter;

		// Token: 0x04003EC2 RID: 16066
		public static readonly StorePropertyDefinition MetadataMaxItems = InternalSchema.AirSyncMaxItems;

		// Token: 0x04003EC3 RID: 16067
		public static readonly StorePropertyDefinition MetadataConversationMode = InternalSchema.AirSyncConversationMode;

		// Token: 0x04003EC4 RID: 16068
		public static readonly StorePropertyDefinition MetadataSettingsHash = InternalSchema.AirSyncSettingsHash;

		// Token: 0x04003EC5 RID: 16069
		public static readonly StorePropertyDefinition LastPingHeartbeatInterval = InternalSchema.LastPingHeartbeatInterval;

		// Token: 0x04003EC6 RID: 16070
		public static readonly StorePropertyDefinition DeviceBlockedUntil = InternalSchema.DeviceBlockedUntil;

		// Token: 0x04003EC7 RID: 16071
		public static readonly StorePropertyDefinition DeviceBlockedAt = InternalSchema.DeviceBlockedAt;

		// Token: 0x04003EC8 RID: 16072
		public static readonly StorePropertyDefinition DeviceBlockedReason = InternalSchema.DeviceBlockedReason;

		// Token: 0x04003EC9 RID: 16073
		private static AirSyncStateSchema instance = null;
	}
}
