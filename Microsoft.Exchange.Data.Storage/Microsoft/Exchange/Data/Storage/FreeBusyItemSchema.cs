using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C64 RID: 3172
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class FreeBusyItemSchema : ItemSchema
	{
		// Token: 0x17001E19 RID: 7705
		// (get) Token: 0x06006FB0 RID: 28592 RVA: 0x001E0FD7 File Offset: 0x001DF1D7
		public new static FreeBusyItemSchema Instance
		{
			get
			{
				if (FreeBusyItemSchema.instance == null)
				{
					FreeBusyItemSchema.instance = new FreeBusyItemSchema();
				}
				return FreeBusyItemSchema.instance;
			}
		}

		// Token: 0x040043B2 RID: 17330
		[Autoload]
		public static readonly StorePropertyDefinition FreeBusyEntryIds = InternalSchema.FreeBusyEntryIds;

		// Token: 0x040043B3 RID: 17331
		[Autoload]
		public static readonly StorePropertyDefinition ScheduleInfoMonthsTentative = InternalSchema.ScheduleInfoMonthsTentative;

		// Token: 0x040043B4 RID: 17332
		[Autoload]
		public static readonly StorePropertyDefinition ScheduleInfoFreeBusyTentative = InternalSchema.ScheduleInfoFreeBusyTentative;

		// Token: 0x040043B5 RID: 17333
		[Autoload]
		public static readonly StorePropertyDefinition ScheduleInfoMonthsBusy = InternalSchema.ScheduleInfoMonthsBusy;

		// Token: 0x040043B6 RID: 17334
		[Autoload]
		public static readonly StorePropertyDefinition ScheduleInfoFreeBusyBusy = InternalSchema.ScheduleInfoFreeBusyBusy;

		// Token: 0x040043B7 RID: 17335
		[Autoload]
		public static readonly StorePropertyDefinition ScheduleInfoMonthsOof = InternalSchema.ScheduleInfoMonthsOof;

		// Token: 0x040043B8 RID: 17336
		[Autoload]
		public static readonly StorePropertyDefinition ScheduleInfoFreeBusyOof = InternalSchema.ScheduleInfoFreeBusyOof;

		// Token: 0x040043B9 RID: 17337
		[Autoload]
		public static readonly StorePropertyDefinition ScheduleInfoMonthsMerged = InternalSchema.ScheduleInfoMonthsMerged;

		// Token: 0x040043BA RID: 17338
		[Autoload]
		public static readonly StorePropertyDefinition ScheduleInfoFreeBusyMerged = InternalSchema.ScheduleInfoFreeBusyMerged;

		// Token: 0x040043BB RID: 17339
		[Autoload]
		public static readonly StorePropertyDefinition ScheduleInfoRecipientLegacyDn = InternalSchema.ScheduleInfoRecipientLegacyDn;

		// Token: 0x040043BC RID: 17340
		[Autoload]
		public static readonly StorePropertyDefinition OutlookFreeBusyMonthCount = InternalSchema.OutlookFreeBusyMonthCount;

		// Token: 0x040043BD RID: 17341
		[Autoload]
		public static readonly StorePropertyDefinition PublicFolderFreeBusy = InternalSchema.PublicFolderFreeBusy;

		// Token: 0x040043BE RID: 17342
		private static FreeBusyItemSchema instance = null;
	}
}
