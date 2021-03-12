using System;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.AirSync.SchemaConverter.XSO
{
	// Token: 0x02000231 RID: 561
	[Serializable]
	internal class XsoReminderOffsetProperty : XsoIntegerProperty
	{
		// Token: 0x060014F5 RID: 5365 RVA: 0x0007AE58 File Offset: 0x00079058
		public XsoReminderOffsetProperty() : base(ItemSchema.ReminderMinutesBeforeStart, new PropertyDefinition[]
		{
			ItemSchema.ReminderIsSet,
			ItemSchema.ReminderMinutesBeforeStart
		})
		{
		}

		// Token: 0x17000753 RID: 1875
		// (get) Token: 0x060014F6 RID: 5366 RVA: 0x0007AE88 File Offset: 0x00079088
		public override int IntegerData
		{
			get
			{
				CalendarItemBase calendarItemBase = base.XsoItem as CalendarItemBase;
				if (!calendarItemBase.Reminder.IsSet)
				{
					return -1;
				}
				int minutesBeforeStart = calendarItemBase.Reminder.MinutesBeforeStart;
				if (minutesBeforeStart > 20160)
				{
					return 15;
				}
				return minutesBeforeStart;
			}
		}

		// Token: 0x060014F7 RID: 5367 RVA: 0x0007AEC8 File Offset: 0x000790C8
		protected override void InternalCopyFromModified(IProperty srcProperty)
		{
			CalendarItemBase calendarItemBase = (CalendarItemBase)base.XsoItem;
			calendarItemBase[ItemSchema.ReminderIsSet] = true;
			calendarItemBase[base.PropertyDef] = ((IIntegerProperty)srcProperty).IntegerData;
		}

		// Token: 0x060014F8 RID: 5368 RVA: 0x0007AF10 File Offset: 0x00079110
		protected override void InternalSetToDefault(IProperty srcProperty)
		{
			CalendarItemBase calendarItemBase = (CalendarItemBase)base.XsoItem;
			if (!(calendarItemBase.TryGetProperty(base.PropertyDef) is PropertyError))
			{
				calendarItemBase[ItemSchema.ReminderIsSet] = false;
			}
		}
	}
}
