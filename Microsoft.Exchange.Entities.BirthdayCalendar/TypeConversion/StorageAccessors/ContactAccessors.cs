using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Entities.TypeConversion.PropertyAccessors;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Entities.BirthdayCalendar.TypeConversion.StorageAccessors
{
	// Token: 0x02000018 RID: 24
	internal static class ContactAccessors
	{
		// Token: 0x0400001E RID: 30
		public static readonly IStoragePropertyAccessor<IContact, string> DisplayName = new DefaultStoragePropertyAccessor<IContact, string>(StoreObjectSchema.DisplayName, false);

		// Token: 0x0400001F RID: 31
		public static readonly IStoragePropertyAccessor<IContact, string> Attribution = new DefaultStoragePropertyAccessor<IContact, string>(ContactSchema.AttributionDisplayName, false);

		// Token: 0x04000020 RID: 32
		public static readonly IStoragePropertyAccessor<IContact, PersonId> PersonId = new DefaultStoragePropertyAccessor<IContact, PersonId>(ContactSchema.PersonId, false);

		// Token: 0x04000021 RID: 33
		public static readonly IStoragePropertyAccessor<IContact, bool> NotInBirthdayCalendar = new DefaultStoragePropertyAccessor<IContact, bool>(ContactSchema.NotInBirthdayCalendar, false);

		// Token: 0x04000022 RID: 34
		public static readonly IStoragePropertyAccessor<IContact, bool> IsWritable = new DefaultStoragePropertyAccessor<IContact, bool>(ContactSchema.IsWritable, false);

		// Token: 0x04000023 RID: 35
		public static readonly IStoragePropertyAccessor<IContact, ExDateTime?> Birthday = new DelegatedStoragePropertyAccessor<IContact, ExDateTime?>(delegate(IContact contact, out ExDateTime? birthday)
		{
			object obj = contact.TryGetProperty(ContactSchema.BirthdayLocal);
			if (obj is ExDateTime)
			{
				birthday = new ExDateTime?((ExDateTime)obj);
			}
			else
			{
				birthday = null;
			}
			return true;
		}, delegate(IContact contact, ExDateTime? newBirthday)
		{
			if (newBirthday != null)
			{
				contact[ContactSchema.BirthdayLocal] = newBirthday.Value;
			}
		}, null, null, new PropertyDefinition[0]);
	}
}
