using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Entities.Calendaring.TypeConversion.PropertyAccessors.StorageAccessors;
using Microsoft.Exchange.Entities.DataModel.Calendaring;
using Microsoft.Exchange.Entities.TypeConversion;
using Microsoft.Exchange.Entities.TypeConversion.Translators;

namespace Microsoft.Exchange.Entities.Calendaring.TypeConversion.Translators
{
	// Token: 0x020000A1 RID: 161
	internal class CalendarGroupEntryTranslator : StorageEntityTranslator<ICalendarGroupEntry, Calendar, CalendarSchema>
	{
		// Token: 0x060003DF RID: 991 RVA: 0x0000E54D File Offset: 0x0000C74D
		protected CalendarGroupEntryTranslator(IEnumerable<ITranslationRule<ICalendarGroupEntry, Calendar>> additionalRules = null) : base(CalendarGroupEntryTranslator.CreateTranslationRules().AddRules(additionalRules))
		{
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060003E0 RID: 992 RVA: 0x0000E560 File Offset: 0x0000C760
		public new static CalendarGroupEntryTranslator Instance
		{
			get
			{
				return CalendarGroupEntryTranslator.SingletonInstance;
			}
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x0000E568 File Offset: 0x0000C768
		private static List<ITranslationRule<ICalendarGroupEntry, Calendar>> CreateTranslationRules()
		{
			return new List<ITranslationRule<ICalendarGroupEntry, Calendar>>
			{
				CalendarGroupEntryAccessors.CalendarColor.MapTo(Calendar.Accessors.Color),
				CalendarGroupEntryAccessors.CalendarId.MapTo(Calendar.Accessors.CalendarFolderStoreId),
				CalendarGroupEntryAccessors.CalendarName.MapTo(Calendar.Accessors.Name),
				CalendarGroupEntryAccessors.CalendarRecordKey.MapTo(Calendar.Accessors.RecordKey)
			};
		}

		// Token: 0x0400016B RID: 363
		private static readonly CalendarGroupEntryTranslator SingletonInstance = new CalendarGroupEntryTranslator(null);
	}
}
