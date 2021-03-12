using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020004DA RID: 1242
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IContactStoreForContactLinking
	{
		// Token: 0x170010D1 RID: 4305
		// (get) Token: 0x0600362D RID: 13869
		StoreId[] FolderScope { get; }

		// Token: 0x0600362E RID: 13870
		IEnumerable<ContactInfoForLinking> GetPersonContacts(PersonId personId);

		// Token: 0x0600362F RID: 13871
		IEnumerable<ContactInfoForLinking> GetAllContacts();

		// Token: 0x06003630 RID: 13872
		IEnumerable<ContactInfoForLinking> GetAllContactsPerCriteria(IEnumerable<string> emailAddresses, string imAddress);

		// Token: 0x06003631 RID: 13873
		void ContactRemovedFromPerson(PersonId personId, ContactInfoForLinking contact);

		// Token: 0x06003632 RID: 13874
		void ContactAddedToPerson(PersonId personId, ContactInfoForLinking contact);
	}
}
