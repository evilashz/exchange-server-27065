using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C57 RID: 3159
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ReminderMessageSchema : MessageItemSchema
	{
		// Token: 0x17001E11 RID: 7697
		// (get) Token: 0x06006F6B RID: 28523 RVA: 0x001DFCA4 File Offset: 0x001DDEA4
		public new static ReminderMessageSchema Instance
		{
			get
			{
				if (ReminderMessageSchema.instance == null)
				{
					lock (ReminderMessageSchema.syncObj)
					{
						if (ReminderMessageSchema.instance == null)
						{
							ReminderMessageSchema.instance = new ReminderMessageSchema();
						}
					}
				}
				return ReminderMessageSchema.instance;
			}
		}

		// Token: 0x0400438B RID: 17291
		private static readonly object syncObj = new object();

		// Token: 0x0400438C RID: 17292
		public static readonly StorePropertyDefinition ReminderText = InternalSchema.ReminderText;

		// Token: 0x0400438D RID: 17293
		internal static readonly StorePropertyDefinition Location = InternalSchema.Location;

		// Token: 0x0400438E RID: 17294
		public static readonly StorePropertyDefinition ReminderStartTime = InternalSchema.ReminderStartTime;

		// Token: 0x0400438F RID: 17295
		public static readonly StorePropertyDefinition ReminderEndTime = InternalSchema.ReminderEndTime;

		// Token: 0x04004390 RID: 17296
		public static readonly StorePropertyDefinition ReminderId = InternalSchema.ReminderId;

		// Token: 0x04004391 RID: 17297
		public static readonly StorePropertyDefinition ReminderItemGlobalObjectId = InternalSchema.ReminderItemGlobalObjectId;

		// Token: 0x04004392 RID: 17298
		public static readonly StorePropertyDefinition ReminderOccurrenceGlobalObjectId = InternalSchema.ReminderOccurrenceGlobalObjectId;

		// Token: 0x04004393 RID: 17299
		private static ReminderMessageSchema instance = null;
	}
}
