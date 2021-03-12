using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Entities.DataModel.Calendaring.Recurrence;

namespace Microsoft.Exchange.Entities.Calendaring.TypeConversion.Converters.Recurrence
{
	// Token: 0x0200008B RID: 139
	internal struct WeeklyRecurrencePatternConverter
	{
		// Token: 0x06000352 RID: 850 RVA: 0x0000C163 File Offset: 0x0000A363
		public WeeklyRecurrencePatternConverter(DayOfWeekConverter dayOfWeekConverter)
		{
			this.dayOfWeekConverter = dayOfWeekConverter;
		}

		// Token: 0x06000353 RID: 851 RVA: 0x0000C16C File Offset: 0x0000A36C
		public RecurrencePattern ConvertEntitiesToStorage(WeeklyRecurrencePattern value)
		{
			if (value != null)
			{
				return new WeeklyRecurrencePattern(this.dayOfWeekConverter.Convert(value.DaysOfWeek), value.Interval, value.FirstDayOfWeek);
			}
			return null;
		}

		// Token: 0x06000354 RID: 852 RVA: 0x0000C1A4 File Offset: 0x0000A3A4
		public RecurrencePattern ConvertStorageToEntities(WeeklyRecurrencePattern value)
		{
			if (value != null)
			{
				return new WeeklyRecurrencePattern
				{
					DaysOfWeek = this.dayOfWeekConverter.Convert(value.DaysOfWeek),
					FirstDayOfWeek = value.FirstDayOfWeek,
					Interval = value.RecurrenceInterval
				};
			}
			return null;
		}

		// Token: 0x040000F7 RID: 247
		private readonly DayOfWeekConverter dayOfWeekConverter;
	}
}
