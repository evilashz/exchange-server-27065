using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Entities.Calendaring.TypeConversion.Converters.Recurrence;
using Microsoft.Exchange.Entities.Calendaring.TypeConversion.PropertyAccessors.StorageAccessors;
using Microsoft.Exchange.Entities.DataModel.Calendaring;
using Microsoft.Exchange.Entities.DataModel.Calendaring.Recurrence;
using Microsoft.Exchange.Entities.TypeConversion.Converters;
using Microsoft.Exchange.Entities.TypeConversion.PropertyTranslationRules;

namespace Microsoft.Exchange.Entities.Calendaring.TypeConversion.PropertyTranslationRules
{
	// Token: 0x0200009A RID: 154
	internal class PatternedRecurrenceRule : PropertyTranslationRule<ICalendarItem, IEvent, PropertyDefinition, Recurrence, PatternedRecurrence>
	{
		// Token: 0x060003A7 RID: 935 RVA: 0x0000DCAD File Offset: 0x0000BEAD
		public PatternedRecurrenceRule() : base(CalendarItemAccessors.Recurrence, Event.Accessors.PatternedRecurrence, null, null)
		{
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x0000DCC1 File Offset: 0x0000BEC1
		public override IConverter<Recurrence, PatternedRecurrence> GetLeftToRightConverter(ICalendarItem left, IEvent right)
		{
			return new RecurrenceConverter(left.Session.ExTimeZone);
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x0000DCD3 File Offset: 0x0000BED3
		public override IConverter<PatternedRecurrence, Recurrence> GetRightToLeftConverter(ICalendarItem left, IEvent right)
		{
			return new RecurrenceConverter(left.Session.ExTimeZone);
		}
	}
}
