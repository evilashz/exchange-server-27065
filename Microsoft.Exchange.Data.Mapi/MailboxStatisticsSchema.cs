using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Mapi
{
	// Token: 0x02000023 RID: 35
	internal sealed class MailboxStatisticsSchema : MapiObjectSchema
	{
		// Token: 0x040000AA RID: 170
		public static readonly MapiPropertyDefinition AssociatedItemCount = MapiPropertyDefinitions.AssociatedItemCount;

		// Token: 0x040000AB RID: 171
		public static readonly MapiPropertyDefinition DeletedItemCount = MapiPropertyDefinitions.DeletedItemCount;

		// Token: 0x040000AC RID: 172
		public static readonly MapiPropertyDefinition DisconnectDate = MapiPropertyDefinitions.DisconnectDate;

		// Token: 0x040000AD RID: 173
		public static readonly MapiPropertyDefinition DisplayName = MapiPropertyDefinitions.DisplayName;

		// Token: 0x040000AE RID: 174
		public static readonly MapiPropertyDefinition ItemCount = MapiPropertyDefinitions.ItemCount;

		// Token: 0x040000AF RID: 175
		public static readonly MapiPropertyDefinition LastLoggedOnUserAccount = MapiPropertyDefinitions.LastLoggedOnUserAccount;

		// Token: 0x040000B0 RID: 176
		public static readonly MapiPropertyDefinition LastLogoffTime = MapiPropertyDefinitions.LastLogoffTime;

		// Token: 0x040000B1 RID: 177
		public static readonly MapiPropertyDefinition LastLogonTime = MapiPropertyDefinitions.LastLogonTime;

		// Token: 0x040000B2 RID: 178
		public static readonly MapiPropertyDefinition LegacyDN = MapiPropertyDefinitions.LegacyDN;

		// Token: 0x040000B3 RID: 179
		public static readonly MapiPropertyDefinition MailboxGuid = MapiPropertyDefinitions.MailboxGuid;

		// Token: 0x040000B4 RID: 180
		internal static readonly MapiPropertyDefinition MailboxMiscFlags = MapiPropertyDefinitions.MailboxMiscFlags;

		// Token: 0x040000B5 RID: 181
		public static readonly MapiPropertyDefinition ObjectClass = MapiPropertyDefinitions.ObjectClass;

		// Token: 0x040000B6 RID: 182
		public static readonly MapiPropertyDefinition StorageLimitStatus = MapiPropertyDefinitions.StorageLimitStatus;

		// Token: 0x040000B7 RID: 183
		public static readonly MapiPropertyDefinition TotalDeletedItemSize = MapiPropertyDefinitions.TotalDeletedItemSize;

		// Token: 0x040000B8 RID: 184
		public static readonly MapiPropertyDefinition TotalItemSize = MapiPropertyDefinitions.TotalItemSize;

		// Token: 0x040000B9 RID: 185
		public static readonly MapiPropertyDefinition IsQuarantined = MapiPropertyDefinitions.IsQuarantined;

		// Token: 0x040000BA RID: 186
		public static readonly MapiPropertyDefinition QuarantineDescription = MapiPropertyDefinitions.QuarantineDescription;

		// Token: 0x040000BB RID: 187
		public static readonly MapiPropertyDefinition QuarantineLastCrash = MapiPropertyDefinitions.QuarantineLastCrash;

		// Token: 0x040000BC RID: 188
		public static readonly MapiPropertyDefinition QuarantineEnd = MapiPropertyDefinitions.QuarantineEnd;

		// Token: 0x040000BD RID: 189
		public static readonly MapiPropertyDefinition MailboxRootEntryId = new MapiPropertyDefinition("MailboxRootEntryId", typeof(long?), PropTag.MsgHeaderFid, MapiPropertyDefinitionFlags.ReadOnly, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040000BE RID: 190
		public static readonly MapiPropertyDefinition StoreMailboxType = new MapiPropertyDefinition("MailboxType", typeof(StoreMailboxType), PropTag.MailboxType, MapiPropertyDefinitionFlags.ReadOnly, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040000BF RID: 191
		public static readonly MapiPropertyDefinition CurrentSchemaVersion = new MapiPropertyDefinition("CurrentSchemaVersion", typeof(int?), PropTag.MailboxDatabaseVersion, MapiPropertyDefinitionFlags.ReadOnly, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040000C0 RID: 192
		public static readonly MapiPropertyDefinition PersistableTenantPartitionHint = MapiPropertyDefinitions.PersistableTenantPartitionHint;

		// Token: 0x040000C1 RID: 193
		public static readonly MapiPropertyDefinition DatabaseName = new MapiPropertyDefinition("DatabaseName", typeof(string), PropTag.Null, MapiPropertyDefinitionFlags.ReadOnly, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040000C2 RID: 194
		public static readonly MapiPropertyDefinition MailboxMessagesPerFolderCountWarningQuota = MapiPropertyDefinitions.MailboxMessagesPerFolderCountWarningQuota;

		// Token: 0x040000C3 RID: 195
		public static readonly MapiPropertyDefinition MailboxMessagesPerFolderCountReceiveQuota = MapiPropertyDefinitions.MailboxMessagesPerFolderCountReceiveQuota;

		// Token: 0x040000C4 RID: 196
		public static readonly MapiPropertyDefinition DumpsterMessagesPerFolderCountWarningQuota = MapiPropertyDefinitions.DumpsterMessagesPerFolderCountWarningQuota;

		// Token: 0x040000C5 RID: 197
		public static readonly MapiPropertyDefinition DumpsterMessagesPerFolderCountReceiveQuota = MapiPropertyDefinitions.DumpsterMessagesPerFolderCountReceiveQuota;

		// Token: 0x040000C6 RID: 198
		public static readonly MapiPropertyDefinition FolderHierarchyChildrenCountWarningQuota = MapiPropertyDefinitions.FolderHierarchyChildrenCountWarningQuota;

		// Token: 0x040000C7 RID: 199
		public static readonly MapiPropertyDefinition FolderHierarchyChildrenCountReceiveQuota = MapiPropertyDefinitions.FolderHierarchyChildrenCountReceiveQuota;

		// Token: 0x040000C8 RID: 200
		public static readonly MapiPropertyDefinition FolderHierarchyDepthWarningQuota = MapiPropertyDefinitions.FolderHierarchyDepthWarningQuota;

		// Token: 0x040000C9 RID: 201
		public static readonly MapiPropertyDefinition FolderHierarchyDepthReceiveQuota = MapiPropertyDefinitions.FolderHierarchyDepthReceiveQuota;

		// Token: 0x040000CA RID: 202
		public static readonly MapiPropertyDefinition FoldersCountWarningQuota = MapiPropertyDefinitions.FoldersCountWarningQuota;

		// Token: 0x040000CB RID: 203
		public static readonly MapiPropertyDefinition FoldersCountReceiveQuota = MapiPropertyDefinitions.FoldersCountReceiveQuota;

		// Token: 0x040000CC RID: 204
		public static readonly MapiPropertyDefinition NamedPropertiesCountQuota = MapiPropertyDefinitions.NamedPropertiesCountQuota;

		// Token: 0x040000CD RID: 205
		public static readonly MapiPropertyDefinition MessageTableTotalSize = MapiPropertyDefinitions.MessageTableTotalSize;

		// Token: 0x040000CE RID: 206
		public static readonly MapiPropertyDefinition MessageTableAvailableSize = MapiPropertyDefinitions.MessageTableAvailableSize;

		// Token: 0x040000CF RID: 207
		public static readonly MapiPropertyDefinition AttachmentTableTotalSize = MapiPropertyDefinitions.AttachmentTableTotalSize;

		// Token: 0x040000D0 RID: 208
		public static readonly MapiPropertyDefinition AttachmentTableAvailableSize = MapiPropertyDefinitions.AttachmentTableAvailableSize;

		// Token: 0x040000D1 RID: 209
		public static readonly MapiPropertyDefinition OtherTablesTotalSize = MapiPropertyDefinitions.OtherTablesTotalSize;

		// Token: 0x040000D2 RID: 210
		public static readonly MapiPropertyDefinition OtherTablesAvailableSize = MapiPropertyDefinitions.OtherTablesAvailableSize;
	}
}
