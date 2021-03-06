using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Entities.DataModel.Calendaring;
using Microsoft.Exchange.Entities.TypeConversion;

namespace Microsoft.Exchange.Entities.Calendaring.TypeConversion.PropertyTranslationRules
{
	// Token: 0x02000098 RID: 152
	internal class EventPopupReminderSettingsRules : ITranslationRule<ICalendarItemBase, IEvent>
	{
		// Token: 0x06000395 RID: 917 RVA: 0x0000D890 File Offset: 0x0000BA90
		public void FromRightToLeftType(ICalendarItemBase calendarItem, IEvent evt)
		{
			ExAssert.RetailAssert(evt != null, "evt is null");
			ExAssert.RetailAssert(calendarItem != null, "calendarItem is null");
			IList<EventPopupReminderSetting> popupReminderSettings = evt.PopupReminderSettings;
			if (popupReminderSettings != null)
			{
				ExAssert.RetailAssert(popupReminderSettings.Count == 1, "reminderSettings.Count is not 1, actual count is {0}", new object[]
				{
					popupReminderSettings.Count
				});
				EventPopupReminderSetting eventPopupReminderSetting = popupReminderSettings[0];
				calendarItem.IsReminderSet = eventPopupReminderSetting.IsReminderSet;
				calendarItem.ReminderMinutesBeforeStart = eventPopupReminderSetting.ReminderMinutesBeforeStart;
			}
		}

		// Token: 0x06000396 RID: 918 RVA: 0x0000D914 File Offset: 0x0000BB14
		public void FromLeftToRightType(ICalendarItemBase calendarItem, IEvent evt)
		{
			ExAssert.RetailAssert(evt != null, "evt is null");
			ExAssert.RetailAssert(calendarItem != null, "calendarItem is null");
			EventPopupReminderSetting item = new EventPopupReminderSetting
			{
				Id = EventPopupReminderSettingsRules.GetDefaultPopupReminderSettingId(evt),
				ChangeKey = evt.ChangeKey,
				IsReminderSet = calendarItem.IsReminderSet,
				ReminderMinutesBeforeStart = calendarItem.ReminderMinutesBeforeStart
			};
			evt.PopupReminderSettings = new List<EventPopupReminderSetting>
			{
				item
			};
		}

		// Token: 0x06000397 RID: 919 RVA: 0x0000D990 File Offset: 0x0000BB90
		internal static string GetDefaultPopupReminderSettingId(IEvent evt)
		{
			return evt.Id + EventPopupReminderSettingsRules.DefaultReminderId.ToString();
		}

		// Token: 0x04000150 RID: 336
		internal static readonly Guid DefaultReminderId = new Guid("5FAAB62C-B27D-4D7D-83C0-E45A3AB83CF9");
	}
}
