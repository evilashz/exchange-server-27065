using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Entities.DataModel.Calendaring.Recurrence;

namespace Microsoft.Exchange.Entities.Calendaring.TypeConversion.Converters.Recurrence
{
	// Token: 0x02000086 RID: 134
	internal struct MonthlyRecurrencePatternConverter
	{
		// Token: 0x0600033C RID: 828 RVA: 0x0000BAF7 File Offset: 0x00009CF7
		public MonthlyRecurrencePatternConverter(IDayOfWeekConverter dayOfWeekConverter, IWeekIndexConverter weekIndexConverter)
		{
			this.dayOfWeekConverter = dayOfWeekConverter;
			this.weekIndexConverter = weekIndexConverter;
		}

		// Token: 0x0600033D RID: 829 RVA: 0x0000BB07 File Offset: 0x00009D07
		public RecurrencePattern ConvertEntitiesToStorage(AbsoluteMonthlyRecurrencePattern value)
		{
			if (value != null)
			{
				return new MonthlyRecurrencePattern(value.DayOfMonth, value.Interval);
			}
			return null;
		}

		// Token: 0x0600033E RID: 830 RVA: 0x0000BB1F File Offset: 0x00009D1F
		public RecurrencePattern ConvertEntitiesToStorage(RelativeMonthlyRecurrencePattern value)
		{
			if (value != null)
			{
				return new MonthlyThRecurrencePattern(this.dayOfWeekConverter.Convert(value.DaysOfWeek), this.weekIndexConverter.Convert(value.Index), value.Interval);
			}
			return null;
		}

		// Token: 0x0600033F RID: 831 RVA: 0x0000BB54 File Offset: 0x00009D54
		public RecurrencePattern ConvertStorageToEntities(MonthlyRecurrencePattern value)
		{
			if (value != null)
			{
				return new AbsoluteMonthlyRecurrencePattern
				{
					DayOfMonth = value.DayOfMonth,
					Interval = value.RecurrenceInterval
				};
			}
			return null;
		}

		// Token: 0x06000340 RID: 832 RVA: 0x0000BB88 File Offset: 0x00009D88
		public RecurrencePattern ConvertStorageToEntities(MonthlyThRecurrencePattern value)
		{
			if (value != null)
			{
				return new RelativeMonthlyRecurrencePattern
				{
					DaysOfWeek = this.dayOfWeekConverter.Convert(value.DaysOfWeek),
					Index = this.weekIndexConverter.Convert(value.Order),
					Interval = value.RecurrenceInterval
				};
			}
			return null;
		}

		// Token: 0x040000E8 RID: 232
		private readonly IDayOfWeekConverter dayOfWeekConverter;

		// Token: 0x040000E9 RID: 233
		private readonly IWeekIndexConverter weekIndexConverter;
	}
}
