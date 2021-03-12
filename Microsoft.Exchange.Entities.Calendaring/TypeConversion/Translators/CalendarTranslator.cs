using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Entities.Calendaring.TypeConversion.PropertyAccessors.StorageAccessors;
using Microsoft.Exchange.Entities.DataModel.Calendaring;
using Microsoft.Exchange.Entities.TypeConversion;
using Microsoft.Exchange.Entities.TypeConversion.PropertyAccessors.StorageAccessors;
using Microsoft.Exchange.Entities.TypeConversion.Translators;

namespace Microsoft.Exchange.Entities.Calendaring.TypeConversion.Translators
{
	// Token: 0x020000A3 RID: 163
	internal class CalendarTranslator : StorageEntityTranslator<ICalendarFolder, Calendar, CalendarSchema>
	{
		// Token: 0x060003E7 RID: 999 RVA: 0x0000E649 File Offset: 0x0000C849
		protected CalendarTranslator(IEnumerable<ITranslationRule<ICalendarFolder, Calendar>> additionalRules = null) : base(CalendarTranslator.CreateTranslationRules().AddRules(additionalRules))
		{
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060003E8 RID: 1000 RVA: 0x0000E65C File Offset: 0x0000C85C
		public new static CalendarTranslator Instance
		{
			get
			{
				return CalendarTranslator.SingletonInstance;
			}
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x0000E664 File Offset: 0x0000C864
		private static List<ITranslationRule<ICalendarFolder, Calendar>> CreateTranslationRules()
		{
			return new List<ITranslationRule<ICalendarFolder, Calendar>>
			{
				CalendarFolderAccessors.DisplayName.MapTo(Calendar.Accessors.Name),
				StoreObjectAccessors.RecordKey.MapTo(Calendar.Accessors.RecordKey)
			};
		}

		// Token: 0x0400016D RID: 365
		private static readonly CalendarTranslator SingletonInstance = new CalendarTranslator(null);
	}
}
