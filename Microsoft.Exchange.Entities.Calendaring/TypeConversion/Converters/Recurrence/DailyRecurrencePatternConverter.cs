using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Entities.DataModel.Calendaring.Recurrence;

namespace Microsoft.Exchange.Entities.Calendaring.TypeConversion.Converters.Recurrence
{
	// Token: 0x02000082 RID: 130
	internal struct DailyRecurrencePatternConverter
	{
		// Token: 0x06000335 RID: 821 RVA: 0x0000B9AB File Offset: 0x00009BAB
		public DailyRecurrencePattern ConvertEntitiesToStorage(DailyRecurrencePattern value)
		{
			if (value != null)
			{
				return new DailyRecurrencePattern(value.Interval);
			}
			return null;
		}

		// Token: 0x06000336 RID: 822 RVA: 0x0000B9C0 File Offset: 0x00009BC0
		public RecurrencePattern ConvertStorageToEntities(DailyRecurrencePattern value)
		{
			if (value != null)
			{
				return new DailyRecurrencePattern
				{
					Interval = value.RecurrenceInterval
				};
			}
			return null;
		}
	}
}
