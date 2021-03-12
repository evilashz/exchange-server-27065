using System;

namespace Microsoft.Exchange.Entities.DataModel.Calendaring
{
	// Token: 0x0200004D RID: 77
	public sealed class EventPopupReminderSetting : StorageEntity<EventPopupReminderSettingSchema>
	{
		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x0600029B RID: 667 RVA: 0x00005B2B File Offset: 0x00003D2B
		// (set) Token: 0x0600029C RID: 668 RVA: 0x00005B3E File Offset: 0x00003D3E
		public bool IsReminderSet
		{
			get
			{
				return base.GetPropertyValueOrDefault<bool>(base.Schema.IsReminderSetProperty);
			}
			set
			{
				base.SetPropertyValue<bool>(base.Schema.IsReminderSetProperty, value);
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x0600029D RID: 669 RVA: 0x00005B52 File Offset: 0x00003D52
		// (set) Token: 0x0600029E RID: 670 RVA: 0x00005B65 File Offset: 0x00003D65
		public int ReminderMinutesBeforeStart
		{
			get
			{
				return base.GetPropertyValueOrDefault<int>(base.Schema.ReminderMinutesBeforeStartProperty);
			}
			set
			{
				base.SetPropertyValue<int>(base.Schema.ReminderMinutesBeforeStartProperty, value);
			}
		}
	}
}
