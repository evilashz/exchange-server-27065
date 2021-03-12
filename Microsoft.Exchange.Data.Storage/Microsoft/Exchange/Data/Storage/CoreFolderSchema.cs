using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C52 RID: 3154
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class CoreFolderSchema : CoreObjectSchema
	{
		// Token: 0x06006F5E RID: 28510 RVA: 0x001DF873 File Offset: 0x001DDA73
		private CoreFolderSchema()
		{
		}

		// Token: 0x17001E0D RID: 7693
		// (get) Token: 0x06006F5F RID: 28511 RVA: 0x001DF87B File Offset: 0x001DDA7B
		public new static CoreFolderSchema Instance
		{
			get
			{
				if (CoreFolderSchema.instance == null)
				{
					CoreFolderSchema.instance = new CoreFolderSchema();
				}
				return CoreFolderSchema.instance;
			}
		}

		// Token: 0x04004347 RID: 17223
		private static CoreFolderSchema instance = null;

		// Token: 0x04004348 RID: 17224
		[Autoload]
		public static readonly StorePropertyDefinition AssociatedItemCount = InternalSchema.AssociatedItemCount;

		// Token: 0x04004349 RID: 17225
		[Autoload]
		public static readonly StorePropertyDefinition ChildCount = InternalSchema.ChildCount;

		// Token: 0x0400434A RID: 17226
		[Autoload]
		public static readonly StorePropertyDefinition Description = InternalSchema.Description;

		// Token: 0x0400434B RID: 17227
		[ConditionallyRequired(CustomConstraintDelegateEnum.IsNotConfigurationFolder)]
		[ConditionalStringLengthConstraint(1, 256, CustomConstraintDelegateEnum.IsNotConfigurationFolder)]
		[Autoload]
		[ConditionallyReadOnly(CustomConstraintDelegateEnum.DoesFolderHaveFixedDisplayName)]
		public static readonly StorePropertyDefinition DisplayName = InternalSchema.DisplayName;

		// Token: 0x0400434C RID: 17228
		[Autoload]
		public static readonly StorePropertyDefinition HasRules = InternalSchema.HasRules;

		// Token: 0x0400434D RID: 17229
		[Autoload]
		public static readonly StorePropertyDefinition Id = InternalSchema.FolderId;

		// Token: 0x0400434E RID: 17230
		[Autoload]
		public static readonly StorePropertyDefinition LinkedUrl = InternalSchema.LinkedUrl;

		// Token: 0x0400434F RID: 17231
		[Autoload]
		public static readonly StorePropertyDefinition LinkedSiteUrl = InternalSchema.LinkedSiteUrl;

		// Token: 0x04004350 RID: 17232
		[Autoload]
		public static readonly StorePropertyDefinition LinkedListId = InternalSchema.LinkedListId;

		// Token: 0x04004351 RID: 17233
		[Autoload]
		public static readonly StorePropertyDefinition ItemCount = InternalSchema.ItemCount;

		// Token: 0x04004352 RID: 17234
		[Autoload]
		public static readonly StorePropertyDefinition UnreadCount = InternalSchema.UnreadCount;

		// Token: 0x04004353 RID: 17235
		public static readonly PropertyTagPropertyDefinition MergeMidsetDeleted = InternalSchema.MergeMidsetDeleted;

		// Token: 0x04004354 RID: 17236
		[Autoload]
		public static readonly StorePropertyDefinition ReplicaList = InternalSchema.ReplicaList;

		// Token: 0x04004355 RID: 17237
		public static readonly StorePropertyDefinition LastMovedTimeStamp = InternalSchema.LastMovedTimeStamp;

		// Token: 0x04004356 RID: 17238
		[Autoload]
		public static readonly StorePropertyDefinition DeletedItemsEntryId = InternalSchema.DeletedItemsEntryId;

		// Token: 0x04004357 RID: 17239
		internal static readonly NativeStorePropertyDefinition MapiAclTable = InternalSchema.MapiAclTable;

		// Token: 0x04004358 RID: 17240
		internal static readonly NativeStorePropertyDefinition MapiRulesTable = InternalSchema.MapiRulesTable;

		// Token: 0x04004359 RID: 17241
		[Autoload]
		[FixedValueOnly(true)]
		internal static readonly NativeStorePropertyDefinition PermissionChangeBlocked = InternalSchema.PermissionChangeBlocked;

		// Token: 0x0400435A RID: 17242
		public static readonly NativeStorePropertyDefinition RawSecurityDescriptor = InternalSchema.RawSecurityDescriptor;

		// Token: 0x0400435B RID: 17243
		public static readonly NativeStorePropertyDefinition RawFreeBusySecurityDescriptor = InternalSchema.RawFreeBusySecurityDescriptor;

		// Token: 0x0400435C RID: 17244
		public static readonly NativeStorePropertyDefinition AclTableAndSecurityDescriptor = InternalSchema.AclTableAndSecurityDescriptor;

		// Token: 0x0400435D RID: 17245
		public static readonly StorePropertyDefinition SecurityDescriptor = InternalSchema.SecurityDescriptor;

		// Token: 0x0400435E RID: 17246
		public static readonly StorePropertyDefinition FreeBusySecurityDescriptor = InternalSchema.FreeBusySecurityDescriptor;

		// Token: 0x0400435F RID: 17247
		[Autoload]
		internal static readonly NativeStorePropertyDefinition RecentBindingHistory = InternalSchema.RecentBindingHistory;
	}
}
