using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Entities.DataModel.Calendaring.Recurrence;
using Microsoft.Exchange.Entities.TypeConversion.Converters;

namespace Microsoft.Exchange.Entities.Calendaring.TypeConversion.Converters.Recurrence
{
	// Token: 0x02000088 RID: 136
	internal class RangeConverter : IConverter<RecurrenceRange, RecurrenceRange>, IConverter<RecurrenceRange, RecurrenceRange>
	{
		// Token: 0x06000345 RID: 837 RVA: 0x0000BE10 File Offset: 0x0000A010
		public RecurrenceRange Convert(RecurrenceRange value)
		{
			if (value == null)
			{
				return null;
			}
			NoEndRecurrenceRange noEndRecurrenceRange = value as NoEndRecurrenceRange;
			if (noEndRecurrenceRange != null)
			{
				return new NoEndRecurrenceRange
				{
					StartDate = noEndRecurrenceRange.StartDate
				};
			}
			EndDateRecurrenceRange endDateRecurrenceRange = value as EndDateRecurrenceRange;
			if (endDateRecurrenceRange != null)
			{
				return new EndDateRecurrenceRange
				{
					StartDate = endDateRecurrenceRange.StartDate,
					EndDate = endDateRecurrenceRange.EndDate
				};
			}
			NumberedRecurrenceRange numberedRecurrenceRange = value as NumberedRecurrenceRange;
			if (numberedRecurrenceRange != null)
			{
				return new NumberedRecurrenceRange
				{
					StartDate = numberedRecurrenceRange.StartDate,
					NumberOfOccurrences = numberedRecurrenceRange.NumberOfOccurrences
				};
			}
			throw new ArgumentValueCannotBeParsedException("value", value.GetType().FullName, typeof(RecurrenceRange).FullName);
		}

		// Token: 0x06000346 RID: 838 RVA: 0x0000BEC0 File Offset: 0x0000A0C0
		public RecurrenceRange Convert(RecurrenceRange value)
		{
			if (value == null)
			{
				return null;
			}
			switch (value.Type)
			{
			case RecurrenceRangeType.EndDate:
			{
				EndDateRecurrenceRange endDateRecurrenceRange = (EndDateRecurrenceRange)value;
				return new EndDateRecurrenceRange(endDateRecurrenceRange.StartDate, endDateRecurrenceRange.EndDate);
			}
			case RecurrenceRangeType.NoEnd:
			{
				NoEndRecurrenceRange noEndRecurrenceRange = (NoEndRecurrenceRange)value;
				return new NoEndRecurrenceRange(noEndRecurrenceRange.StartDate);
			}
			case RecurrenceRangeType.Numbered:
			{
				NumberedRecurrenceRange numberedRecurrenceRange = (NumberedRecurrenceRange)value;
				return new NumberedRecurrenceRange(numberedRecurrenceRange.StartDate, numberedRecurrenceRange.NumberOfOccurrences);
			}
			default:
				throw new ArgumentValueCannotBeParsedException("value", value.Type.ToString(), value.GetType().FullName);
			}
		}
	}
}
