using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Entities.BirthdayCalendar;
using Microsoft.Exchange.Entities.BirthdayCalendar.TypeConversion.Translators;
using Microsoft.Exchange.Entities.DataModel.BirthdayCalendar;
using Microsoft.Exchange.Entities.DataProviders;
using Microsoft.Exchange.Entities.EntitySets;
using Microsoft.Exchange.Entities.TypeConversion.Translators;

namespace Microsoft.Exchange.Entities.BirthdayCalendar.DataProviders
{
	// Token: 0x02000012 RID: 18
	internal class BirthdayContactDataProvider : StorageItemDataProvider<IStoreSession, BirthdayContact, IContact>
	{
		// Token: 0x06000064 RID: 100 RVA: 0x00002B81 File Offset: 0x00000D81
		public BirthdayContactDataProvider(IStorageEntitySetScope<IStoreSession> scope, StoreId containerFolderId) : base(scope, containerFolderId, ExTraceGlobals.BirthdayEventDataProviderTracer)
		{
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00002B90 File Offset: 0x00000D90
		protected override IStorageTranslator<IContact, BirthdayContact> Translator
		{
			get
			{
				return BirthdayContactTranslator.Instance;
			}
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00002E38 File Offset: 0x00001038
		public virtual IEnumerable<IBirthdayContact> GetLinkedContacts(PersonId personId)
		{
			base.Trace.TraceDebug<PersonId>((long)this.GetHashCode(), "GetLinkedContacts: the person ID is {0}", personId);
			foreach (IStorePropertyBag contactPropertyBag in AllPersonContactsEnumerator.Create(base.Session as MailboxSession, personId, BirthdayContactDataProvider.BirthdayContactPropertyBagProperties))
			{
				using (IContact contact = base.XsoFactory.BindToContact(base.Session, contactPropertyBag.GetValueOrDefault<StoreId>(CoreItemSchema.Id, null), BirthdayContactDataProvider.BirthdayContactProperties))
				{
					yield return this.ConvertToEntity(contact);
				}
			}
			base.Trace.TraceDebug<PersonId>((long)this.GetHashCode(), "GetLinkedContacts: no more contacts for person ID {0}", personId);
			yield break;
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00002E5C File Offset: 0x0000105C
		protected internal override IContact BindToStoreObject(StoreId id)
		{
			return base.XsoFactory.BindToContact(base.Session, id, BirthdayContactDataProvider.BirthdayContactProperties);
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00002E75 File Offset: 0x00001075
		protected override IContact CreateNewStoreObject()
		{
			throw new NotImplementedException();
		}

		// Token: 0x04000017 RID: 23
		internal static readonly PropertyDefinition[] BirthdayContactProperties = new PropertyDefinition[]
		{
			CoreItemSchema.Id,
			StoreObjectSchema.DisplayName,
			ContactSchema.BirthdayLocal,
			ContactSchema.NotInBirthdayCalendar,
			ContactSchema.PersonId,
			ContactSchema.PartnerNetworkId,
			ContactSchema.IsWritable,
			ItemSchema.ParentDisplayName
		};

		// Token: 0x04000018 RID: 24
		internal static readonly PropertyDefinition[] BirthdayContactPropertyBagProperties = new PropertyDefinition[]
		{
			CoreItemSchema.Id,
			ContactSchema.PersonId
		};
	}
}
