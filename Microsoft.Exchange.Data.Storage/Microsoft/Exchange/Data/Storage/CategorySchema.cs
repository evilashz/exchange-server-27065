using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C3C RID: 3132
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class CategorySchema : Schema
	{
		// Token: 0x17001DF5 RID: 7669
		// (get) Token: 0x06006EEC RID: 28396 RVA: 0x001DD63C File Offset: 0x001DB83C
		public new static CategorySchema Instance
		{
			get
			{
				if (CategorySchema.instance == null)
				{
					CategorySchema.instance = new CategorySchema();
				}
				return CategorySchema.instance;
			}
		}

		// Token: 0x04004218 RID: 16920
		public static StorePropertyDefinition AllowRenameOnFirstUse = InternalSchema.CategoryAllowRenameOnFirstUse;

		// Token: 0x04004219 RID: 16921
		[Required]
		[Autoload]
		public static StorePropertyDefinition Name = InternalSchema.CategoryName;

		// Token: 0x0400421A RID: 16922
		public static StorePropertyDefinition Color = InternalSchema.CategoryColor;

		// Token: 0x0400421B RID: 16923
		public static StorePropertyDefinition KeyboardShortcut = InternalSchema.CategoryKeyboardShortcut;

		// Token: 0x0400421C RID: 16924
		public static StorePropertyDefinition LastTimeUsedNotes = InternalSchema.CategoryLastTimeUsedNotes;

		// Token: 0x0400421D RID: 16925
		public static StorePropertyDefinition LastTimeUsedJournal = InternalSchema.CategoryLastTimeUsedJournal;

		// Token: 0x0400421E RID: 16926
		public static StorePropertyDefinition LastTimeUsedContacts = InternalSchema.CategoryLastTimeUsedContacts;

		// Token: 0x0400421F RID: 16927
		public static StorePropertyDefinition LastTimeUsedTasks = InternalSchema.CategoryLastTimeUsedTasks;

		// Token: 0x04004220 RID: 16928
		public static StorePropertyDefinition LastTimeUsedCalendar = InternalSchema.CategoryLastTimeUsedCalendar;

		// Token: 0x04004221 RID: 16929
		public static StorePropertyDefinition LastTimeUsedMail = InternalSchema.CategoryLastTimeUsedMail;

		// Token: 0x04004222 RID: 16930
		public static StorePropertyDefinition LastTimeUsed = InternalSchema.CategoryLastTimeUsed;

		// Token: 0x04004223 RID: 16931
		public static StorePropertyDefinition LastSessionUsed = InternalSchema.CategoryLastSessionUsed;

		// Token: 0x04004224 RID: 16932
		[Autoload]
		[Required]
		public static StorePropertyDefinition Guid = InternalSchema.CategoryGuid;

		// Token: 0x04004225 RID: 16933
		private static CategorySchema instance = null;
	}
}
