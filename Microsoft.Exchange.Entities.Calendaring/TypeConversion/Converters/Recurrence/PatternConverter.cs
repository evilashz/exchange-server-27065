using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Entities.DataModel.Calendaring.Recurrence;
using Microsoft.Exchange.Entities.TypeConversion.Converters;

namespace Microsoft.Exchange.Entities.Calendaring.TypeConversion.Converters.Recurrence
{
	// Token: 0x02000087 RID: 135
	internal class PatternConverter : IConverter<RecurrencePattern, RecurrencePattern>, IConverter<RecurrencePattern, RecurrencePattern>
	{
		// Token: 0x06000341 RID: 833 RVA: 0x0000BBDC File Offset: 0x00009DDC
		public RecurrencePattern Convert(RecurrencePattern value)
		{
			if (value == null)
			{
				return null;
			}
			DailyRecurrencePattern dailyRecurrencePattern = value as DailyRecurrencePattern;
			if (dailyRecurrencePattern != null)
			{
				return PatternConverter.dailyPatternConverter.ConvertStorageToEntities(dailyRecurrencePattern);
			}
			WeeklyRecurrencePattern weeklyRecurrencePattern = value as WeeklyRecurrencePattern;
			if (weeklyRecurrencePattern != null)
			{
				return PatternConverter.weeklyPatternConverter.ConvertStorageToEntities(weeklyRecurrencePattern);
			}
			MonthlyRecurrencePattern monthlyRecurrencePattern = value as MonthlyRecurrencePattern;
			if (monthlyRecurrencePattern != null)
			{
				return PatternConverter.monthlyPatternConverter.ConvertStorageToEntities(monthlyRecurrencePattern);
			}
			MonthlyThRecurrencePattern monthlyThRecurrencePattern = value as MonthlyThRecurrencePattern;
			if (monthlyThRecurrencePattern != null)
			{
				return PatternConverter.monthlyPatternConverter.ConvertStorageToEntities(monthlyThRecurrencePattern);
			}
			YearlyRecurrencePattern yearlyRecurrencePattern = value as YearlyRecurrencePattern;
			if (yearlyRecurrencePattern != null)
			{
				return PatternConverter.yearlyPatternConverter.ConvertStorageToEntities(yearlyRecurrencePattern);
			}
			YearlyThRecurrencePattern yearlyThRecurrencePattern = value as YearlyThRecurrencePattern;
			if (yearlyThRecurrencePattern != null)
			{
				return PatternConverter.yearlyPatternConverter.ConvertStorageToEntities(yearlyThRecurrencePattern);
			}
			if (value is DailyRegeneratingPattern || value is WeeklyRegeneratingPattern || value is MonthlyRegeneratingPattern || value is YearlyRegeneratingPattern)
			{
				throw new NotImplementedException("Regenerating tasks are not implemented in Entities yet.");
			}
			throw new ArgumentValueCannotBeParsedException("value", value.GetType().FullName, typeof(RecurrencePattern).FullName);
		}

		// Token: 0x06000342 RID: 834 RVA: 0x0000BCC8 File Offset: 0x00009EC8
		public RecurrencePattern Convert(RecurrencePattern value)
		{
			if (value == null)
			{
				return null;
			}
			switch (value.Type)
			{
			case RecurrencePatternType.Daily:
				return PatternConverter.dailyPatternConverter.ConvertEntitiesToStorage((DailyRecurrencePattern)value);
			case RecurrencePatternType.Weekly:
				return PatternConverter.weeklyPatternConverter.ConvertEntitiesToStorage((WeeklyRecurrencePattern)value);
			case RecurrencePatternType.AbsoluteMonthly:
				return PatternConverter.monthlyPatternConverter.ConvertEntitiesToStorage((AbsoluteMonthlyRecurrencePattern)value);
			case RecurrencePatternType.RelativeMonthly:
				return PatternConverter.monthlyPatternConverter.ConvertEntitiesToStorage((RelativeMonthlyRecurrencePattern)value);
			case RecurrencePatternType.AbsoluteYearly:
				return PatternConverter.yearlyPatternConverter.ConvertEntitiesToStorage((AbsoluteYearlyRecurrencePattern)value);
			case RecurrencePatternType.RelativeYearly:
				return PatternConverter.yearlyPatternConverter.ConvertEntitiesToStorage((RelativeYearlyRecurrencePattern)value);
			default:
				throw new ArgumentValueCannotBeParsedException("value.Type", value.Type.ToString(), value.GetType().FullName);
			}
		}

		// Token: 0x040000EA RID: 234
		private static readonly DayOfWeekConverter DayOfWeekConverter = default(DayOfWeekConverter);

		// Token: 0x040000EB RID: 235
		private static readonly WeekIndexConverter WeekIndexConverter = default(WeekIndexConverter);

		// Token: 0x040000EC RID: 236
		private static DailyRecurrencePatternConverter dailyPatternConverter = default(DailyRecurrencePatternConverter);

		// Token: 0x040000ED RID: 237
		private static WeeklyRecurrencePatternConverter weeklyPatternConverter = new WeeklyRecurrencePatternConverter(PatternConverter.DayOfWeekConverter);

		// Token: 0x040000EE RID: 238
		private static MonthlyRecurrencePatternConverter monthlyPatternConverter = new MonthlyRecurrencePatternConverter(PatternConverter.DayOfWeekConverter, PatternConverter.WeekIndexConverter);

		// Token: 0x040000EF RID: 239
		private static YearlyRecurrencePatternConverter yearlyPatternConverter = new YearlyRecurrencePatternConverter(PatternConverter.DayOfWeekConverter, PatternConverter.WeekIndexConverter);
	}
}
