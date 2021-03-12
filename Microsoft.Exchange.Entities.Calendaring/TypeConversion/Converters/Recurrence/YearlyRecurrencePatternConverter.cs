using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Entities.DataModel.Calendaring.Recurrence;

namespace Microsoft.Exchange.Entities.Calendaring.TypeConversion.Converters.Recurrence
{
	// Token: 0x0200008C RID: 140
	internal struct YearlyRecurrencePatternConverter
	{
		// Token: 0x06000355 RID: 853 RVA: 0x0000C1EF File Offset: 0x0000A3EF
		public YearlyRecurrencePatternConverter(IDayOfWeekConverter dayOfWeekConverter, IWeekIndexConverter weekIndexConverter)
		{
			this = default(YearlyRecurrencePatternConverter);
			this.dayOfWeekConverter = dayOfWeekConverter;
			this.weekIndexConverter = weekIndexConverter;
		}

		// Token: 0x06000356 RID: 854 RVA: 0x0000C206 File Offset: 0x0000A406
		public RecurrencePattern ConvertEntitiesToStorage(AbsoluteYearlyRecurrencePattern value)
		{
			if (value != null)
			{
				return new YearlyRecurrencePattern(value.DayOfMonth, value.Month, value.Interval * 12);
			}
			return null;
		}

		// Token: 0x06000357 RID: 855 RVA: 0x0000C227 File Offset: 0x0000A427
		public RecurrencePattern ConvertEntitiesToStorage(RelativeYearlyRecurrencePattern value)
		{
			if (value != null)
			{
				return new YearlyThRecurrencePattern(this.dayOfWeekConverter.Convert(value.DaysOfWeek), this.weekIndexConverter.Convert(value.Index), value.Month, false, value.Interval, CalendarType.Default);
			}
			return null;
		}

		// Token: 0x06000358 RID: 856 RVA: 0x0000C264 File Offset: 0x0000A464
		public RecurrencePattern ConvertStorageToEntities(YearlyRecurrencePattern value)
		{
			if (value != null)
			{
				return new AbsoluteYearlyRecurrencePattern
				{
					DayOfMonth = value.DayOfMonth,
					Interval = value.RecurrenceInterval,
					Month = value.Month
				};
			}
			return null;
		}

		// Token: 0x06000359 RID: 857 RVA: 0x0000C2A4 File Offset: 0x0000A4A4
		public RecurrencePattern ConvertStorageToEntities(YearlyThRecurrencePattern value)
		{
			if (value != null)
			{
				return new RelativeYearlyRecurrencePattern
				{
					DaysOfWeek = this.dayOfWeekConverter.Convert(value.DaysOfWeek),
					Index = this.weekIndexConverter.Convert(value.Order),
					Interval = value.RecurrenceInterval,
					Month = value.Month
				};
			}
			return null;
		}

		// Token: 0x040000F8 RID: 248
		private readonly IDayOfWeekConverter dayOfWeekConverter;

		// Token: 0x040000F9 RID: 249
		private readonly IWeekIndexConverter weekIndexConverter;
	}
}
