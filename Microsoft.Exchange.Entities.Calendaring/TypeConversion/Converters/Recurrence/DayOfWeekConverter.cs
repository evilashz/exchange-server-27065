using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Entities.TypeConversion.Converters;

namespace Microsoft.Exchange.Entities.Calendaring.TypeConversion.Converters.Recurrence
{
	// Token: 0x02000084 RID: 132
	internal struct DayOfWeekConverter : IDayOfWeekConverter, IConverter<DaysOfWeek, ISet<DayOfWeek>>, IConverter<ISet<DayOfWeek>, DaysOfWeek>
	{
		// Token: 0x06000337 RID: 823 RVA: 0x0000B9E8 File Offset: 0x00009BE8
		public DaysOfWeek Convert(ISet<DayOfWeek> value)
		{
			if (value == null)
			{
				throw new ExArgumentNullException("value");
			}
			DaysOfWeek result = DaysOfWeek.None;
			foreach (Tuple<DaysOfWeek, DayOfWeek> tuple in DayOfWeekConverter.MappingTuples)
			{
				DayOfWeekConverter.AddDayToSetIfPresent(value, tuple.Item2, tuple.Item1, ref result);
			}
			return result;
		}

		// Token: 0x06000338 RID: 824 RVA: 0x0000BA34 File Offset: 0x00009C34
		public ISet<DayOfWeek> Convert(DaysOfWeek value)
		{
			HashSet<DayOfWeek> result = new HashSet<DayOfWeek>();
			foreach (Tuple<DaysOfWeek, DayOfWeek> tuple in DayOfWeekConverter.MappingTuples)
			{
				DayOfWeekConverter.AddDayToSetIfPresent(value, tuple.Item1, tuple.Item2, result);
			}
			return result;
		}

		// Token: 0x06000339 RID: 825 RVA: 0x0000BA73 File Offset: 0x00009C73
		private static void AddDayToSetIfPresent(DaysOfWeek daysOfWeek, DaysOfWeek valueToSearch, DayOfWeek dayToAdd, ISet<DayOfWeek> result)
		{
			if ((daysOfWeek & valueToSearch) == valueToSearch)
			{
				result.Add(dayToAdd);
			}
		}

		// Token: 0x0600033A RID: 826 RVA: 0x0000BA83 File Offset: 0x00009C83
		private static void AddDayToSetIfPresent(ISet<DayOfWeek> daysOfWeek, DayOfWeek valueToSearch, DaysOfWeek dayToAdd, ref DaysOfWeek result)
		{
			if (daysOfWeek.Contains(valueToSearch))
			{
				result |= dayToAdd;
			}
		}

		// Token: 0x040000E7 RID: 231
		private static readonly Tuple<DaysOfWeek, DayOfWeek>[] MappingTuples = new Tuple<DaysOfWeek, DayOfWeek>[]
		{
			new Tuple<DaysOfWeek, DayOfWeek>(DaysOfWeek.Monday, DayOfWeek.Monday),
			new Tuple<DaysOfWeek, DayOfWeek>(DaysOfWeek.Tuesday, DayOfWeek.Tuesday),
			new Tuple<DaysOfWeek, DayOfWeek>(DaysOfWeek.Wednesday, DayOfWeek.Wednesday),
			new Tuple<DaysOfWeek, DayOfWeek>(DaysOfWeek.Thursday, DayOfWeek.Thursday),
			new Tuple<DaysOfWeek, DayOfWeek>(DaysOfWeek.Friday, DayOfWeek.Friday),
			new Tuple<DaysOfWeek, DayOfWeek>(DaysOfWeek.Saturday, DayOfWeek.Saturday),
			new Tuple<DaysOfWeek, DayOfWeek>(DaysOfWeek.Sunday, DayOfWeek.Sunday)
		};
	}
}
