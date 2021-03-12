using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Entities.Calendaring.TypeConversion.PropertyAccessors.StorageAccessors;
using Microsoft.Exchange.Entities.DataModel.BirthdayCalendar;
using Microsoft.Exchange.Entities.TypeConversion;
using Microsoft.Exchange.Entities.TypeConversion.PropertyAccessors.StorageAccessors;
using Microsoft.Exchange.Entities.TypeConversion.Translators;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Entities.BirthdayCalendar.TypeConversion.Translators
{
	// Token: 0x0200001A RID: 26
	internal class BirthdayEventTranslator : StorageEntityTranslator<ICalendarItemBase, BirthdayEvent, BirthdayEventSchema>
	{
		// Token: 0x06000093 RID: 147 RVA: 0x00003B6D File Offset: 0x00001D6D
		protected BirthdayEventTranslator(IEnumerable<ITranslationRule<ICalendarItemBase, IBirthdayEvent>> additionalRules = null) : base(BirthdayEventTranslator.CreateTranslationRules().AddRules(additionalRules))
		{
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000094 RID: 148 RVA: 0x00003B80 File Offset: 0x00001D80
		public new static BirthdayEventTranslator Instance
		{
			get
			{
				return BirthdayEventTranslator.SingletonInstance;
			}
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00003B88 File Offset: 0x00001D88
		private static List<ITranslationRule<ICalendarItemBase, BirthdayEvent>> CreateTranslationRules()
		{
			return new List<ITranslationRule<ICalendarItemBase, BirthdayEvent>>
			{
				ItemAccessors<ICalendarItemBase>.Subject.MapTo(BirthdayEvent.Accessors.Subject),
				CalendarItemAccessors.BirthdayContactAttributionDisplayName.MapTo(BirthdayEvent.Accessors.Attribution),
				CalendarItemAccessors.Birthday.MapTo(BirthdayEvent.Accessors.Birthday),
				CalendarItemAccessors.BirthdayContactPersonId.MapTo(BirthdayEvent.Accessors.PersonId),
				CalendarItemAccessors.BirthdayContactId.MapTo(BirthdayEvent.Accessors.ContactId),
				CalendarItemAccessors.IsBirthdayContactWritable.MapTo(BirthdayEvent.Accessors.IsWritable)
			};
		}

		// Token: 0x04000027 RID: 39
		private static readonly BirthdayEventTranslator SingletonInstance = new BirthdayEventTranslator(null);
	}
}
