using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Entities.Calendaring.DataProviders;
using Microsoft.Exchange.Entities.Calendaring.TypeConversion.PropertyAccessors.StorageAccessors;
using Microsoft.Exchange.Entities.DataModel.Calendaring;
using Microsoft.Exchange.Entities.DataModel.PropertyBags;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Entities.Calendaring.EntitySets.EventCommands
{
	// Token: 0x02000049 RID: 73
	internal class EventTimeAdjuster
	{
		// Token: 0x060001CC RID: 460 RVA: 0x000076AA File Offset: 0x000058AA
		public EventTimeAdjuster(DateTimeHelper dateTimeHelper)
		{
			this.DateTimeHelper = dateTimeHelper;
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060001CD RID: 461 RVA: 0x000076B9 File Offset: 0x000058B9
		// (set) Token: 0x060001CE RID: 462 RVA: 0x000076C1 File Offset: 0x000058C1
		private protected virtual DateTimeHelper DateTimeHelper { protected get; private set; }

		// Token: 0x060001CF RID: 463 RVA: 0x000076CC File Offset: 0x000058CC
		public virtual void AdjustTimeProperties(ICalendarItemBase calendarItem)
		{
			EventTimeAdjuster.TimeProperties initialValues = new EventTimeAdjuster.TimeProperties
			{
				IsAllDay = this.GetValue<ICalendarItemBase, bool>(calendarItem, CalendarItemAccessors.IsAllDay),
				Start = this.GetValue<ICalendarItemBase, ExDateTime>(calendarItem, CalendarItemAccessors.StartTime),
				End = this.GetValue<ICalendarItemBase, ExDateTime>(calendarItem, CalendarItemAccessors.EndTime),
				IntendedStartTimeZone = this.GetValue<ICalendarItemBase, ExTimeZone>(calendarItem, CalendarItemAccessors.StartTimeZone),
				IntendedEndTimeZone = this.GetValue<ICalendarItemBase, ExTimeZone>(calendarItem, CalendarItemAccessors.EndTimeZone)
			};
			EventTimeAdjuster.TimeProperties timeProperties = this.AdjustTimeProperties(initialValues, calendarItem.Session.ExTimeZone);
			CalendarItemAccessors.IsAllDay.Set(calendarItem, timeProperties.IsAllDay);
			CalendarItemAccessors.StartTime.Set(calendarItem, timeProperties.Start);
			CalendarItemAccessors.EndTime.Set(calendarItem, timeProperties.End);
			CalendarItemAccessors.StartTimeZone.Set(calendarItem, timeProperties.IntendedStartTimeZone);
			CalendarItemAccessors.EndTimeZone.Set(calendarItem, timeProperties.IntendedEndTimeZone);
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x000077B0 File Offset: 0x000059B0
		public virtual Event AdjustTimeProperties(Event theEvent, ExTimeZone sessionTimeZone)
		{
			ExTimeZone timeZoneOrDefault = this.DateTimeHelper.GetTimeZoneOrDefault(theEvent.IntendedStartTimeZoneId, ExTimeZone.UtcTimeZone);
			ExTimeZone timeZoneOrDefault2 = this.DateTimeHelper.GetTimeZoneOrDefault(theEvent.IntendedEndTimeZoneId, timeZoneOrDefault);
			EventTimeAdjuster.TimeProperties initialValues = new EventTimeAdjuster.TimeProperties
			{
				IsAllDay = theEvent.IsAllDay,
				Start = theEvent.Start,
				End = theEvent.End,
				IntendedStartTimeZone = timeZoneOrDefault,
				IntendedEndTimeZone = timeZoneOrDefault2
			};
			EventTimeAdjuster.TimeProperties timeProperties = this.AdjustTimeProperties(initialValues, sessionTimeZone);
			theEvent.IsAllDay = timeProperties.IsAllDay;
			theEvent.Start = timeProperties.Start;
			theEvent.End = timeProperties.End;
			theEvent.IntendedStartTimeZoneId = timeProperties.IntendedStartTimeZone.Id;
			theEvent.IntendedEndTimeZoneId = timeProperties.IntendedEndTimeZone.Id;
			return theEvent;
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x00007880 File Offset: 0x00005A80
		public virtual ExDateTime FloatTime(ExDateTime time, ExTimeZone intendedTimeZone, ExTimeZone sessionTimeZone)
		{
			ExDateTime time2 = this.DateTimeHelper.ChangeTimeZone(time, intendedTimeZone, true);
			return this.DateTimeHelper.ChangeTimeZone(time2, sessionTimeZone, false);
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x000078AC File Offset: 0x00005AAC
		protected virtual TValue GetValue<TContainer, TValue>(TContainer container, IPropertyAccessor<TContainer, TValue> accessor)
		{
			TValue result;
			if (!accessor.TryGetValue(container, out result))
			{
				return default(TValue);
			}
			return result;
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x000078D0 File Offset: 0x00005AD0
		private EventTimeAdjuster.TimeProperties AdjustTimeProperties(EventTimeAdjuster.TimeProperties initialValues, ExTimeZone sessionTimeZone)
		{
			if (initialValues.IsAllDay)
			{
				ExDateTime time = this.FloatTime(initialValues.Start, initialValues.IntendedStartTimeZone, sessionTimeZone);
				initialValues.Start = EventDataProvider.EnforceMidnightTime(time, MidnightEnforcementOption.MoveBackward);
				ExDateTime time2 = this.FloatTime(initialValues.End, initialValues.IntendedEndTimeZone, sessionTimeZone);
				initialValues.End = EventDataProvider.EnforceMidnightTime(time2, MidnightEnforcementOption.MoveForward);
				initialValues.IntendedStartTimeZone = initialValues.Start.TimeZone;
				initialValues.IntendedEndTimeZone = initialValues.Start.TimeZone;
			}
			return initialValues;
		}

		// Token: 0x0200004A RID: 74
		private struct TimeProperties
		{
			// Token: 0x0400007F RID: 127
			public bool IsAllDay;

			// Token: 0x04000080 RID: 128
			public ExDateTime Start;

			// Token: 0x04000081 RID: 129
			public ExDateTime End;

			// Token: 0x04000082 RID: 130
			public ExTimeZone IntendedStartTimeZone;

			// Token: 0x04000083 RID: 131
			public ExTimeZone IntendedEndTimeZone;
		}
	}
}
