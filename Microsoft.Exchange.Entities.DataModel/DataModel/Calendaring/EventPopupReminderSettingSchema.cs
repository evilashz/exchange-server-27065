using System;
using Microsoft.Exchange.Entities.DataModel.PropertyBags;

namespace Microsoft.Exchange.Entities.DataModel.Calendaring
{
	// Token: 0x0200004E RID: 78
	public sealed class EventPopupReminderSettingSchema : StorageEntitySchema
	{
		// Token: 0x060002A0 RID: 672 RVA: 0x00005B81 File Offset: 0x00003D81
		public EventPopupReminderSettingSchema()
		{
			base.RegisterPropertyDefinition(EventPopupReminderSettingSchema.StaticIsReminderSetProperty);
			base.RegisterPropertyDefinition(EventPopupReminderSettingSchema.StaticReminderMinutesBeforeStartProperty);
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060002A1 RID: 673 RVA: 0x00005B9F File Offset: 0x00003D9F
		public TypedPropertyDefinition<bool> IsReminderSetProperty
		{
			get
			{
				return EventPopupReminderSettingSchema.StaticIsReminderSetProperty;
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060002A2 RID: 674 RVA: 0x00005BA6 File Offset: 0x00003DA6
		public TypedPropertyDefinition<int> ReminderMinutesBeforeStartProperty
		{
			get
			{
				return EventPopupReminderSettingSchema.StaticReminderMinutesBeforeStartProperty;
			}
		}

		// Token: 0x04000121 RID: 289
		private static readonly TypedPropertyDefinition<bool> StaticIsReminderSetProperty = new TypedPropertyDefinition<bool>("EventPopupReminderSetting.IsReminderSet", false, true);

		// Token: 0x04000122 RID: 290
		private static readonly TypedPropertyDefinition<int> StaticReminderMinutesBeforeStartProperty = new TypedPropertyDefinition<int>("EventPopupReminderSetting.ReminderMinutesBeforeStart", 0, true);
	}
}
