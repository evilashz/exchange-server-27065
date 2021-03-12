using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020004DB RID: 1243
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class ContactStoreForContactLinking : IContactStoreForContactLinking
	{
		// Token: 0x06003633 RID: 13875 RVA: 0x000DAD04 File Offset: 0x000D8F04
		public ContactStoreForContactLinking(MailboxSession mailboxSession, IContactLinkingPerformanceTracker performanceTracker)
		{
			Util.ThrowOnNullArgument(mailboxSession, "mailboxSession");
			Util.ThrowOnNullArgument(performanceTracker, "performanceTracker");
			this.MailboxSession = mailboxSession;
			this.performanceTracker = performanceTracker;
		}

		// Token: 0x170010D2 RID: 4306
		// (get) Token: 0x06003634 RID: 13876 RVA: 0x000DAD30 File Offset: 0x000D8F30
		public StoreId[] FolderScope
		{
			get
			{
				if (this.folderScope == null)
				{
					StoreObjectId[] value = this.MailboxSession.ContactFolders.MyContactFolders.Value;
					if (value == null || value.Length == 0)
					{
						this.folderScope = ContactsSearchFolderCriteria.MyContactsExtended.GetDefaultFolderScope(this.MailboxSession, false);
					}
					else
					{
						this.folderScope = ContactsSearchFolderCriteria.GetMyContactExtendedFolders(this.MailboxSession, value, false);
					}
				}
				return this.folderScope;
			}
		}

		// Token: 0x06003635 RID: 13877
		public abstract IEnumerable<ContactInfoForLinking> GetPersonContacts(PersonId personId);

		// Token: 0x06003636 RID: 13878
		public abstract IEnumerable<ContactInfoForLinking> GetAllContacts();

		// Token: 0x06003637 RID: 13879
		public abstract IEnumerable<ContactInfoForLinking> GetAllContactsPerCriteria(IEnumerable<string> emailAddresses, string imAddress);

		// Token: 0x06003638 RID: 13880
		public abstract void ContactRemovedFromPerson(PersonId personId, ContactInfoForLinking contact);

		// Token: 0x06003639 RID: 13881
		public abstract void ContactAddedToPerson(PersonId personId, ContactInfoForLinking contact);

		// Token: 0x0600363A RID: 13882 RVA: 0x000DAD95 File Offset: 0x000D8F95
		protected ContactInfoForLinking CreateContactInfoForLinking(IStorePropertyBag propertyBag)
		{
			this.performanceTracker.IncrementContactsRead();
			return ContactInfoForLinkingFromPropertyBag.Create(this.MailboxSession, propertyBag);
		}

		// Token: 0x04001D14 RID: 7444
		protected readonly MailboxSession MailboxSession;

		// Token: 0x04001D15 RID: 7445
		private StoreId[] folderScope;

		// Token: 0x04001D16 RID: 7446
		private IContactLinkingPerformanceTracker performanceTracker;
	}
}
