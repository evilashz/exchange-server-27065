using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020004DD RID: 1245
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ContactStoreForCowContactLinking : ContactStoreForContactLinking
	{
		// Token: 0x06003646 RID: 13894 RVA: 0x000DB0AE File Offset: 0x000D92AE
		public ContactStoreForCowContactLinking(MailboxSession mailboxSession, IContactLinkingPerformanceTracker performanceTracker) : base(mailboxSession, performanceTracker)
		{
		}

		// Token: 0x06003647 RID: 13895 RVA: 0x000DB0B8 File Offset: 0x000D92B8
		public override void ContactRemovedFromPerson(PersonId personId, ContactInfoForLinking contact)
		{
		}

		// Token: 0x06003648 RID: 13896 RVA: 0x000DB0BA File Offset: 0x000D92BA
		public override void ContactAddedToPerson(PersonId personId, ContactInfoForLinking contact)
		{
		}

		// Token: 0x06003649 RID: 13897 RVA: 0x000DB0BC File Offset: 0x000D92BC
		public override IEnumerable<ContactInfoForLinking> GetPersonContacts(PersonId personId)
		{
			Util.ThrowOnNullArgument(personId, "personId");
			List<ContactInfoForLinking> list = new List<ContactInfoForLinking>(10);
			IEnumerable<IStorePropertyBag> enumerable = AllPersonContactsEnumerator.Create(this.MailboxSession, personId, ContactInfoForLinking.Properties);
			foreach (IStorePropertyBag propertyBag in enumerable)
			{
				list.Add(base.CreateContactInfoForLinking(propertyBag));
			}
			return list;
		}

		// Token: 0x0600364A RID: 13898 RVA: 0x000DB130 File Offset: 0x000D9330
		public override IEnumerable<ContactInfoForLinking> GetAllContacts()
		{
			return ContactsEnumerator<ContactInfoForLinking>.CreateContactsOnlyEnumerator(this.MailboxSession, DefaultFolderType.MyContactsExtended, ContactInfoForLinking.Properties, new Func<IStorePropertyBag, ContactInfoForLinking>(base.CreateContactInfoForLinking), new XSOFactory());
		}

		// Token: 0x0600364B RID: 13899 RVA: 0x000DB158 File Offset: 0x000D9358
		public override IEnumerable<ContactInfoForLinking> GetAllContactsPerCriteria(IEnumerable<string> emailAddresses, string imAddress)
		{
			List<ContactInfoForLinking> list = new List<ContactInfoForLinking>(10);
			IEnumerable<ContactInfoForLinking> result;
			using (IFolder folder = new XSOFactory().BindToFolder(this.MailboxSession, DefaultFolderType.MyContacts))
			{
				IEnumerable<IStorePropertyBag> enumerable = new ContactsByEmailAddressEnumerator(folder, ContactInfoForLinking.Properties, emailAddresses);
				foreach (IStorePropertyBag propertyBag in enumerable)
				{
					list.Add(base.CreateContactInfoForLinking(propertyBag));
				}
				enumerable = new ContactsByPropertyValueEnumerator(folder, InternalSchema.IMAddress, this.GetIMAddressWithVariation(imAddress), ContactInfoForLinking.Properties);
				foreach (IStorePropertyBag propertyBag2 in enumerable)
				{
					list.Add(base.CreateContactInfoForLinking(propertyBag2));
				}
				result = list;
			}
			return result;
		}

		// Token: 0x0600364C RID: 13900 RVA: 0x000DB24C File Offset: 0x000D944C
		private IEnumerable<string> GetIMAddressWithVariation(string imAddress)
		{
			if (imAddress.StartsWith("sip:", StringComparison.OrdinalIgnoreCase))
			{
				return new string[]
				{
					imAddress,
					imAddress.Substring("sip:".Length)
				};
			}
			return new string[]
			{
				imAddress,
				"sip:" + imAddress
			};
		}
	}
}
