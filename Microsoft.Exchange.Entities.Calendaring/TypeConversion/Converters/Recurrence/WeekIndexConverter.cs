using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Entities.DataModel.Calendaring.Recurrence;
using Microsoft.Exchange.Entities.TypeConversion.Converters;

namespace Microsoft.Exchange.Entities.Calendaring.TypeConversion.Converters.Recurrence
{
	// Token: 0x0200008A RID: 138
	internal struct WeekIndexConverter : IWeekIndexConverter, IConverter<RecurrenceOrderType, WeekIndex>, IConverter<WeekIndex, RecurrenceOrderType>
	{
		// Token: 0x06000350 RID: 848 RVA: 0x0000C149 File Offset: 0x0000A349
		public WeekIndex Convert(RecurrenceOrderType value)
		{
			return WeekIndexConverter.mappingConverter.Convert(value);
		}

		// Token: 0x06000351 RID: 849 RVA: 0x0000C156 File Offset: 0x0000A356
		public RecurrenceOrderType Convert(WeekIndex value)
		{
			return WeekIndexConverter.mappingConverter.Reverse(value);
		}

		// Token: 0x040000F6 RID: 246
		private static SimpleMappingConverter<RecurrenceOrderType, WeekIndex> mappingConverter = SimpleMappingConverter<RecurrenceOrderType, WeekIndex>.CreateStrictConverter(new Tuple<RecurrenceOrderType, WeekIndex>[]
		{
			new Tuple<RecurrenceOrderType, WeekIndex>(RecurrenceOrderType.Last, WeekIndex.Last),
			new Tuple<RecurrenceOrderType, WeekIndex>(RecurrenceOrderType.First, WeekIndex.First),
			new Tuple<RecurrenceOrderType, WeekIndex>(RecurrenceOrderType.Second, WeekIndex.Second),
			new Tuple<RecurrenceOrderType, WeekIndex>(RecurrenceOrderType.Third, WeekIndex.Third),
			new Tuple<RecurrenceOrderType, WeekIndex>(RecurrenceOrderType.Fourth, WeekIndex.Fourth)
		});
	}
}
