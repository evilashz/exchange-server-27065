using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Entities.Calendaring.TypeConversion.PropertyAccessors.StorageAccessors;
using Microsoft.Exchange.Entities.DataModel.Calendaring;
using Microsoft.Exchange.Entities.TypeConversion;
using Microsoft.Exchange.Entities.TypeConversion.Translators;

namespace Microsoft.Exchange.Entities.Calendaring.TypeConversion.Translators
{
	// Token: 0x020000A2 RID: 162
	internal class CalendarGroupTranslator : StorageEntityTranslator<ICalendarGroup, CalendarGroup, CalendarGroupSchema>
	{
		// Token: 0x060003E3 RID: 995 RVA: 0x0000E5DF File Offset: 0x0000C7DF
		protected CalendarGroupTranslator(IEnumerable<ITranslationRule<ICalendarGroup, CalendarGroup>> additionalRules = null) : base(CalendarGroupTranslator.CreateTranslationRules().AddRules(additionalRules))
		{
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060003E4 RID: 996 RVA: 0x0000E5F2 File Offset: 0x0000C7F2
		public new static CalendarGroupTranslator Instance
		{
			get
			{
				return CalendarGroupTranslator.SingletonInstance;
			}
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x0000E5FC File Offset: 0x0000C7FC
		private static List<ITranslationRule<ICalendarGroup, CalendarGroup>> CreateTranslationRules()
		{
			return new List<ITranslationRule<ICalendarGroup, CalendarGroup>>
			{
				CalendarGroupAccessors.GroupClassId.MapTo(CalendarGroup.Accessors.ClassId),
				CalendarGroupAccessors.GroupName.MapTo(CalendarGroup.Accessors.Name)
			};
		}

		// Token: 0x0400016C RID: 364
		private static readonly CalendarGroupTranslator SingletonInstance = new CalendarGroupTranslator(null);
	}
}
