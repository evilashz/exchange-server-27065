using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Entities.BirthdayCalendar.TypeConversion.StorageAccessors;
using Microsoft.Exchange.Entities.DataModel.BirthdayCalendar;
using Microsoft.Exchange.Entities.TypeConversion;
using Microsoft.Exchange.Entities.TypeConversion.Translators;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Entities.BirthdayCalendar.TypeConversion.Translators
{
	// Token: 0x02000019 RID: 25
	internal class BirthdayContactTranslator : StorageEntityTranslator<IContact, BirthdayContact, BirthdayContactSchema>
	{
		// Token: 0x0600008F RID: 143 RVA: 0x00003AB1 File Offset: 0x00001CB1
		protected BirthdayContactTranslator(IEnumerable<ITranslationRule<IContact, BirthdayContact>> additionalRules = null) : base(BirthdayContactTranslator.CreateTranslationRules().AddRules(additionalRules))
		{
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000090 RID: 144 RVA: 0x00003AC4 File Offset: 0x00001CC4
		public new static BirthdayContactTranslator Instance
		{
			get
			{
				return BirthdayContactTranslator.SingletonInstance;
			}
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00003ACC File Offset: 0x00001CCC
		private static List<ITranslationRule<IContact, BirthdayContact>> CreateTranslationRules()
		{
			return new List<ITranslationRule<IContact, BirthdayContact>>
			{
				ContactAccessors.Birthday.MapTo(BirthdayContact.Accessors.Birthday),
				ContactAccessors.DisplayName.MapTo(BirthdayContact.Accessors.DisplayName),
				ContactAccessors.PersonId.MapTo(BirthdayContact.Accessors.PersonId),
				ContactAccessors.NotInBirthdayCalendar.MapTo(BirthdayContact.Accessors.ShouldHideBirthday),
				ContactAccessors.Attribution.MapTo(BirthdayContact.Accessors.Attribution),
				ContactAccessors.IsWritable.MapTo(BirthdayContact.Accessors.IsWritable)
			};
		}

		// Token: 0x04000026 RID: 38
		private static readonly BirthdayContactTranslator SingletonInstance = new BirthdayContactTranslator(null);
	}
}
