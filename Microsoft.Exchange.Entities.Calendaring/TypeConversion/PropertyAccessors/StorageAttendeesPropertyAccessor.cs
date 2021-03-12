using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Entities.Calendaring.TypeConversion.Converters;
using Microsoft.Exchange.Entities.DataModel.Calendaring;
using Microsoft.Exchange.Entities.TypeConversion.PropertyAccessors;

namespace Microsoft.Exchange.Entities.Calendaring.TypeConversion.PropertyAccessors
{
	// Token: 0x02000094 RID: 148
	internal sealed class StorageAttendeesPropertyAccessor : StoragePropertyAccessor<ICalendarItemBase, IList<Attendee>>
	{
		// Token: 0x06000382 RID: 898 RVA: 0x0000D031 File Offset: 0x0000B231
		public StorageAttendeesPropertyAccessor() : base(false, null, null)
		{
		}

		// Token: 0x06000383 RID: 899 RVA: 0x0000D03C File Offset: 0x0000B23C
		protected override void PerformSet(ICalendarItemBase container, IList<Attendee> value)
		{
			AttendeeConverter converter = this.GetConverter(container);
			converter.ToXso(value, container);
		}

		// Token: 0x06000384 RID: 900 RVA: 0x0000D05C File Offset: 0x0000B25C
		protected override bool PerformTryGetValue(ICalendarItemBase container, out IList<Attendee> value)
		{
			AttendeeConverter converter = this.GetConverter(container);
			IEnumerable<Attendee> enumerable = converter.Convert(container.AttendeeCollection);
			value = ((enumerable == null) ? null : enumerable.ToList<Attendee>());
			return enumerable != null;
		}

		// Token: 0x06000385 RID: 901 RVA: 0x0000D093 File Offset: 0x0000B293
		private AttendeeConverter GetConverter(ICalendarItemBase calendarItem)
		{
			return new AttendeeConverter(StorageAttendeesPropertyAccessor.ResponseTypeConverter, StorageAttendeesPropertyAccessor.TypeConverter, calendarItem);
		}

		// Token: 0x0400014D RID: 333
		private static readonly ResponseTypeConverter ResponseTypeConverter = default(ResponseTypeConverter);

		// Token: 0x0400014E RID: 334
		private static readonly AttendeeTypeConverter TypeConverter = default(AttendeeTypeConverter);
	}
}
