using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020004DC RID: 1244
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ContactStoreForBulkContactLinking : ContactStoreForContactLinking
	{
		// Token: 0x0600363B RID: 13883 RVA: 0x000DADAE File Offset: 0x000D8FAE
		public ContactStoreForBulkContactLinking(MailboxSession mailboxSession, IContactLinkingPerformanceTracker performanceTracker) : base(mailboxSession, performanceTracker)
		{
		}

		// Token: 0x0600363C RID: 13884 RVA: 0x000DADB8 File Offset: 0x000D8FB8
		public override IEnumerable<ContactInfoForLinking> GetAllContactsPerCriteria(IEnumerable<string> emailAddresses, string imAddress)
		{
			throw new InvalidOperationException("BulkContactLinking does not implement GetAllContactsPerCriteria.");
		}

		// Token: 0x0600363D RID: 13885 RVA: 0x000DADC4 File Offset: 0x000D8FC4
		public override IEnumerable<ContactInfoForLinking> GetPersonContacts(PersonId personId)
		{
			Util.ThrowOnNullArgument(personId, "personId");
			if (!this.initializedWorkingSet)
			{
				this.InitializeWorkingSet();
			}
			IList<ContactInfoForLinking> result;
			if (!this.contactsByPersonId.TryGetValue(personId, out result))
			{
				ContactStoreForBulkContactLinking.Tracer.TraceDebug<PersonId>((long)this.GetHashCode(), "ContactStoreForBulkContactLinking.GetPersonContacts: Couldn't find contact list for PersonId: {0}", personId);
				result = new List<ContactInfoForLinking>(0);
			}
			return result;
		}

		// Token: 0x0600363E RID: 13886 RVA: 0x000DAE19 File Offset: 0x000D9019
		public override IEnumerable<ContactInfoForLinking> GetAllContacts()
		{
			if (!this.initializedWorkingSet)
			{
				this.InitializeWorkingSet();
			}
			return this.workingSet;
		}

		// Token: 0x0600363F RID: 13887 RVA: 0x000DAE30 File Offset: 0x000D9030
		public override void ContactRemovedFromPerson(PersonId personId, ContactInfoForLinking contact)
		{
			Util.ThrowOnNullArgument(personId, "personId");
			Util.ThrowOnNullArgument(contact, "contact");
			if (contact is ContactInfoForLinkingFromCoreObject)
			{
				ContactStoreForBulkContactLinking.Tracer.TraceDebug<VersionedId, PersonId>((long)this.GetHashCode(), "ContactStoreForBulkContactLinking.ContactRemovedFromPerson: contact not removed from contactsByPersonId as it is object being saved. ItemId {0}, PersonId {1}", contact.ItemId, contact.PersonId);
				return;
			}
			if (!this.initializedWorkingSet)
			{
				this.InitializeWorkingSet();
			}
			IList<ContactInfoForLinking> list;
			if (this.contactsByPersonId.TryGetValue(personId, out list))
			{
				list.Remove(contact);
				return;
			}
			ContactStoreForBulkContactLinking.Tracer.TraceDebug<PersonId>((long)this.GetHashCode(), "ContactStoreForBulkContactLinking.ReportPersonIdUpdate: Couldn't find contact list for PersonId: {0}", personId);
		}

		// Token: 0x06003640 RID: 13888 RVA: 0x000DAEBC File Offset: 0x000D90BC
		public override void ContactAddedToPerson(PersonId personId, ContactInfoForLinking contact)
		{
			Util.ThrowOnNullArgument(personId, "personId");
			Util.ThrowOnNullArgument(contact, "contact");
			if (!personId.Equals(contact.PersonId))
			{
				ContactStoreForBulkContactLinking.Tracer.TraceDebug<PersonId, PersonId>((long)this.GetHashCode(), "ContactStoreForBulkContactLinking.ContactAddedToPerson: PersonId of the contact is not properly set. Found {0}. Expected {1}", contact.PersonId, personId);
				throw new InvalidOperationException("ContactStoreForBulkContactLinking.ContactAddedToPerson: PersonId of the contact is not properly set.");
			}
			if (contact is ContactInfoForLinkingFromCoreObject)
			{
				ContactStoreForBulkContactLinking.Tracer.TraceDebug<VersionedId, PersonId>((long)this.GetHashCode(), "ContactStoreForBulkContactLinking.ContactAddedToPerson: contact not added to contactsByPersonId as it is object being saved. ItemId {0}, PersonId {1}", contact.ItemId, contact.PersonId);
				return;
			}
			if (!this.initializedWorkingSet)
			{
				this.InitializeWorkingSet();
			}
			this.AddToContactsByPersonId(contact);
		}

		// Token: 0x06003641 RID: 13889 RVA: 0x000DAF58 File Offset: 0x000D9158
		public void PushContactOntoWorkingSet(IStorePropertyBag contact)
		{
			Util.ThrowOnNullArgument(contact, "contact");
			if (!this.initializedWorkingSet)
			{
				this.InitializeWorkingSet();
			}
			ContactInfoForLinking contact2 = base.CreateContactInfoForLinking(contact);
			this.AddContactToWorkingSet(contact2);
		}

		// Token: 0x06003642 RID: 13890 RVA: 0x000DAF90 File Offset: 0x000D9190
		private void InitializeWorkingSet()
		{
			this.contactsByPersonId = new Dictionary<PersonId, IList<ContactInfoForLinking>>(100);
			this.workingSet = new List<ContactInfoForLinking>(100);
			ContactsEnumerator<ContactInfoForLinking> contactsEnumerator = ContactsEnumerator<ContactInfoForLinking>.CreateContactsOnlyEnumerator(this.MailboxSession, DefaultFolderType.MyContactsExtended, ContactInfoForLinking.Properties, new Func<IStorePropertyBag, ContactInfoForLinking>(base.CreateContactInfoForLinking), new XSOFactory());
			foreach (ContactInfoForLinking contact in contactsEnumerator)
			{
				this.AddContactToWorkingSet(contact);
				if (this.workingSet.Count >= 5000)
				{
					ContactStoreForBulkContactLinking.Tracer.TraceDebug((long)this.GetHashCode(), "ContactStoreForBulkContactLinking.InitializeWorkingSet: reached contacts cap. Skipping remaining contacts in mailbox.");
					break;
				}
			}
			this.initializedWorkingSet = true;
		}

		// Token: 0x06003643 RID: 13891 RVA: 0x000DB048 File Offset: 0x000D9248
		private void AddContactToWorkingSet(ContactInfoForLinking contact)
		{
			this.workingSet.Add(contact);
			this.AddToContactsByPersonId(contact);
		}

		// Token: 0x06003644 RID: 13892 RVA: 0x000DB060 File Offset: 0x000D9260
		private void AddToContactsByPersonId(ContactInfoForLinking contact)
		{
			IList<ContactInfoForLinking> list;
			if (!this.contactsByPersonId.TryGetValue(contact.PersonId, out list))
			{
				list = new List<ContactInfoForLinking>(2);
				this.contactsByPersonId[contact.PersonId] = list;
			}
			list.Add(contact);
		}

		// Token: 0x04001D17 RID: 7447
		private const int MaxContactsLoadedFromMailbox = 5000;

		// Token: 0x04001D18 RID: 7448
		private static readonly Trace Tracer = ExTraceGlobals.ContactLinkingTracer;

		// Token: 0x04001D19 RID: 7449
		private bool initializedWorkingSet;

		// Token: 0x04001D1A RID: 7450
		private IList<ContactInfoForLinking> workingSet;

		// Token: 0x04001D1B RID: 7451
		private Dictionary<PersonId, IList<ContactInfoForLinking>> contactsByPersonId;
	}
}
