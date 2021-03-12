using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C21 RID: 3105
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class CalendarFolderSchema : FolderSchema
	{
		// Token: 0x17001DE4 RID: 7652
		// (get) Token: 0x06006E77 RID: 28279 RVA: 0x001DB1B3 File Offset: 0x001D93B3
		public new static CalendarFolderSchema Instance
		{
			get
			{
				if (CalendarFolderSchema.instance == null)
				{
					CalendarFolderSchema.instance = new CalendarFolderSchema();
				}
				return CalendarFolderSchema.instance;
			}
		}

		// Token: 0x0400412A RID: 16682
		public const int CurrentCalendarFolderVersion = 1;

		// Token: 0x0400412B RID: 16683
		public static readonly StorePropertyDefinition FreeBusySecurityDescriptor = InternalSchema.FreeBusySecurityDescriptor;

		// Token: 0x0400412C RID: 16684
		public static readonly StorePropertyDefinition ConsumerCalendarGuid = InternalSchema.ConsumerCalendarGuid;

		// Token: 0x0400412D RID: 16685
		public static readonly StorePropertyDefinition ConsumerCalendarOwnerId = InternalSchema.ConsumerCalendarOwnerId;

		// Token: 0x0400412E RID: 16686
		public static readonly StorePropertyDefinition ConsumerCalendarPrivateFreeBusyId = InternalSchema.ConsumerCalendarPrivateFreeBusyId;

		// Token: 0x0400412F RID: 16687
		public static readonly StorePropertyDefinition ConsumerCalendarPrivateDetailId = InternalSchema.ConsumerCalendarPrivateDetailId;

		// Token: 0x04004130 RID: 16688
		public static readonly StorePropertyDefinition ConsumerCalendarPublishVisibility = InternalSchema.ConsumerCalendarPublishVisibility;

		// Token: 0x04004131 RID: 16689
		public static readonly StorePropertyDefinition ConsumerCalendarSharingInvitations = InternalSchema.ConsumerCalendarSharingInvitations;

		// Token: 0x04004132 RID: 16690
		public static readonly StorePropertyDefinition ConsumerCalendarPermissionLevel = InternalSchema.ConsumerCalendarPermissionLevel;

		// Token: 0x04004133 RID: 16691
		public static readonly StorePropertyDefinition ConsumerCalendarSynchronizationState = InternalSchema.ConsumerCalendarSynchronizationState;

		// Token: 0x04004134 RID: 16692
		[Autoload]
		public static readonly StorePropertyDefinition CharmId = InternalSchema.CharmId;

		// Token: 0x04004135 RID: 16693
		public static readonly StorePropertyDefinition[] ConsumerCalendarProperties = new StorePropertyDefinition[]
		{
			CalendarFolderSchema.ConsumerCalendarGuid,
			CalendarFolderSchema.ConsumerCalendarOwnerId,
			CalendarFolderSchema.ConsumerCalendarPrivateFreeBusyId,
			CalendarFolderSchema.ConsumerCalendarPrivateDetailId,
			CalendarFolderSchema.ConsumerCalendarPublishVisibility,
			CalendarFolderSchema.ConsumerCalendarSharingInvitations,
			CalendarFolderSchema.ConsumerCalendarPermissionLevel,
			CalendarFolderSchema.ConsumerCalendarSynchronizationState
		};

		// Token: 0x04004136 RID: 16694
		private static CalendarFolderSchema instance = null;
	}
}
