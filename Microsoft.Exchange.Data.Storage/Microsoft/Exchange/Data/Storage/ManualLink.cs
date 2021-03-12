using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020004FA RID: 1274
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class ManualLink : ContactLink
	{
		// Token: 0x06003756 RID: 14166 RVA: 0x000DE15E File Offset: 0x000DC35E
		public ManualLink(MailboxInfoForLinking mailboxInfo, IExtensibleLogger logger, IContactLinkingPerformanceTracker performanceTracker) : base(mailboxInfo, logger, performanceTracker)
		{
		}

		// Token: 0x06003757 RID: 14167 RVA: 0x000DE16C File Offset: 0x000DC36C
		public void Link(MailboxSession session, IRecipientSession adSession, PersonId linkingPersonId, ADObjectId linkToADObjectId)
		{
			Util.ThrowOnNullArgument(session, "session");
			Util.ThrowOnNullArgument(adSession, "adSession");
			Util.ThrowOnNullArgument(linkingPersonId, "linkingPersonId");
			Util.ThrowOnNullArgument(linkToADObjectId, "linkToADObjectId");
			base.PerformanceTracker.Start();
			try
			{
				IList<ContactInfoForLinking> list = this.QueryPersonContacts(session, linkingPersonId);
				if (list.Count == 0)
				{
					ContactLink.Tracer.TraceError<PersonId>((long)this.GetHashCode(), "ManualLink.Link: no contacts for PersonId={0} can be found", linkingPersonId);
					throw new ObjectNotFoundException(ServerStrings.ExItemNotFound);
				}
				bool flag = ManualLink.IsGALLinked(list);
				if (flag)
				{
					if (!(list[0].GALLinkID == linkToADObjectId.ObjectGuid))
					{
						throw new InvalidOperationException(ServerStrings.PersonIsAlreadyLinkedWithGALContact);
					}
					ContactLink.Tracer.TraceDebug<PersonId, ADObjectId>((long)this.GetHashCode(), "ManualLink.Link: Linking PersonId {0} is already linked to GAL object {1}", linkingPersonId, linkToADObjectId);
				}
				else
				{
					ContactInfoForLinkingFromDirectory contactInfoForLinkingFromDirectory = this.LoadADContact(adSession, linkToADObjectId);
					if (contactInfoForLinkingFromDirectory == null)
					{
						ContactLink.Tracer.TraceError<ADObjectId>((long)this.GetHashCode(), "ManualLink.Link: no AD contact for ADObjectId={0} can be found", linkToADObjectId);
						throw new ObjectNotFoundException(ServerStrings.ExItemNotFound);
					}
					base.LogEvent(new SchemaBasedLogEvent<ContactLinkingLogSchema.ContactLinking>
					{
						{
							ContactLinkingLogSchema.ContactLinking.LinkOperation,
							ContactLinkingOperation.ManualLinking
						},
						{
							ContactLinkingLogSchema.ContactLinking.LinkingPersonId,
							linkingPersonId
						},
						{
							ContactLinkingLogSchema.ContactLinking.LinkToPersonId,
							linkToADObjectId
						}
					});
					this.LinkContactsWithGAL(list, contactInfoForLinkingFromDirectory);
					base.Commit(list);
				}
			}
			finally
			{
				base.PerformanceTracker.Stop();
				base.LogEvent(base.PerformanceTracker.GetLogEvent());
			}
		}

		// Token: 0x06003758 RID: 14168 RVA: 0x000DE2F4 File Offset: 0x000DC4F4
		public void Link(MailboxSession session, PersonId linkingPersonId, PersonId linkToPersonId)
		{
			Util.ThrowOnNullArgument(session, "session");
			Util.ThrowOnNullArgument(linkingPersonId, "linkingPersonId");
			Util.ThrowOnNullArgument(linkToPersonId, "linkToPersonId");
			base.PerformanceTracker.Start();
			try
			{
				if (linkingPersonId.Equals(linkToPersonId))
				{
					ContactLink.Tracer.TraceDebug<PersonId>((long)this.GetHashCode(), "ManualLink.Link: ignoring link operation because linkingPersonId and linkToPerson are the same person: {0}", linkToPersonId);
				}
				else
				{
					IList<ContactInfoForLinking> list = this.QueryPersonContacts(session, linkingPersonId);
					if (list.Count == 0)
					{
						ContactLink.Tracer.TraceError<PersonId>((long)this.GetHashCode(), "ManualLink.Link: no contacts for PersonId={0} can be found", linkingPersonId);
						throw new ObjectNotFoundException(ServerStrings.ExItemNotFound);
					}
					IList<ContactInfoForLinking> list2 = this.QueryPersonContacts(session, linkToPersonId);
					if (list2.Count == 0)
					{
						ContactLink.Tracer.TraceError<PersonId>((long)this.GetHashCode(), "ManualLink.Link: no contacts for PersonId={0} can be found", linkToPersonId);
						throw new ObjectNotFoundException(ServerStrings.ExItemNotFound);
					}
					if (list.Count + list2.Count > AutomaticLink.MaximumNumberOfContactsPerPerson.Value)
					{
						ContactLink.Tracer.TraceError((long)this.GetHashCode(), "ManualLink.Link: Can't link Personas as their aggregated contact count exceeds maximum allowed. Persona {0}: {1}. Persona {2}: {3}", new object[]
						{
							linkingPersonId,
							list.Count,
							linkToPersonId,
							list2.Count
						});
						throw new InvalidOperationException(ServerStrings.ContactLinkingMaximumNumberOfContactsPerPersonError);
					}
					bool flag = ManualLink.IsGALLinked(list);
					bool flag2 = ManualLink.IsGALLinked(list2);
					if (flag && flag2 && list[0].GALLinkID != list2[0].GALLinkID)
					{
						throw new InvalidOperationException(ServerStrings.PersonIsAlreadyLinkedWithGALContact);
					}
					base.LogEvent(new SchemaBasedLogEvent<ContactLinkingLogSchema.ContactLinking>
					{
						{
							ContactLinkingLogSchema.ContactLinking.LinkOperation,
							ContactLinkingOperation.ManualLinking
						},
						{
							ContactLinkingLogSchema.ContactLinking.LinkingPersonId,
							linkingPersonId
						},
						{
							ContactLinkingLogSchema.ContactLinking.LinkToPersonId,
							linkToPersonId
						}
					});
					ContactInfoForLinking galContact = flag ? list[0] : (flag2 ? list2[0] : null);
					this.LinkContacts(list, list2, galContact);
					base.Commit(list);
					base.Commit(list2);
				}
			}
			finally
			{
				base.PerformanceTracker.Stop();
				base.LogEvent(base.PerformanceTracker.GetLogEvent());
			}
		}

		// Token: 0x06003759 RID: 14169 RVA: 0x000DE53C File Offset: 0x000DC73C
		public PersonId Unlink(MailboxSession session, PersonId personId, VersionedId contactId)
		{
			Util.ThrowOnNullArgument(session, "session");
			Util.ThrowOnNullArgument(personId, "personId");
			Util.ThrowOnNullArgument(contactId, "contactId");
			base.PerformanceTracker.Start();
			PersonId result;
			try
			{
				IList<ContactInfoForLinking> list = this.QueryPersonContacts(session, personId);
				if (list == null || list.Count == 0)
				{
					throw new ObjectNotFoundException(ServerStrings.ExItemNotFound);
				}
				base.LogEvent(new SchemaBasedLogEvent<ContactLinkingLogSchema.ContactUnlinking>
				{
					{
						ContactLinkingLogSchema.ContactUnlinking.ItemId,
						contactId.ObjectId
					},
					{
						ContactLinkingLogSchema.ContactUnlinking.PersonId,
						personId
					}
				});
				ContactInfoForLinking matchingContactByItemId = ManualLink.GetMatchingContactByItemId(list, contactId);
				if (matchingContactByItemId != null)
				{
					this.UnlinkContact(matchingContactByItemId, list);
					base.Commit(list);
					result = matchingContactByItemId.PersonId;
				}
				else
				{
					ContactLink.Tracer.TraceDebug<VersionedId, PersonId>((long)this.GetHashCode(), "ManualLink.Unlink: contact {0} not found in person set: {1}", contactId, personId);
					result = null;
				}
			}
			finally
			{
				base.PerformanceTracker.Stop();
				base.LogEvent(base.PerformanceTracker.GetLogEvent());
			}
			return result;
		}

		// Token: 0x0600375A RID: 14170 RVA: 0x000DE624 File Offset: 0x000DC824
		public void Unlink(MailboxSession session, PersonId personId, Guid galLinkId)
		{
			ArgumentValidator.ThrowIfNull("session", session);
			ArgumentValidator.ThrowIfNull("personId", personId);
			ArgumentValidator.ThrowIfEmpty("galLinkId", galLinkId);
			IList<ContactInfoForLinking> list = this.QueryPersonContacts(session, personId);
			if (list == null || list.Count == 0)
			{
				throw new ObjectNotFoundException(ServerStrings.ExItemNotFound);
			}
			if (list[0].GALLinkID != galLinkId)
			{
				ContactLink.Tracer.TraceDebug<PersonId, Guid>((long)this.GetHashCode(), "ManualLink.Unlink: Person {0} not linked with GAL Contact {1}.", personId, galLinkId);
				return;
			}
			base.LogEvent(new SchemaBasedLogEvent<ContactLinkingLogSchema.ContactUnlinking>
			{
				{
					ContactLinkingLogSchema.ContactUnlinking.PersonId,
					personId
				},
				{
					ContactLinkingLogSchema.ContactUnlinking.ADObjectIdGuid,
					galLinkId
				}
			});
			this.UnlinkContactFromGAL(list);
			base.Commit(list);
		}

		// Token: 0x0600375B RID: 14171 RVA: 0x000DE6E4 File Offset: 0x000DC8E4
		public void RejectSuggestion(MailboxSession session, PersonId personId, PersonId suggestionPersonId)
		{
			Util.ThrowOnNullArgument(session, "session");
			Util.ThrowOnNullArgument(personId, "personId");
			Util.ThrowOnNullArgument(suggestionPersonId, "suggestionPersonId");
			base.PerformanceTracker.Start();
			try
			{
				if (personId.Equals(suggestionPersonId))
				{
					throw new InvalidParamException(ServerStrings.RejectedSuggestionPersonIdSameAsPersonId);
				}
				base.LogEvent(new SchemaBasedLogEvent<ContactLinkingLogSchema.RejectSuggestion>
				{
					{
						ContactLinkingLogSchema.RejectSuggestion.PersonId,
						personId
					},
					{
						ContactLinkingLogSchema.RejectSuggestion.SuggestionPersonId,
						suggestionPersonId
					}
				});
				IList<ContactInfoForLinking> list = this.QueryPersonContacts(session, personId);
				IList<ContactInfoForLinking> list2 = this.QueryPersonContacts(session, suggestionPersonId);
				this.RejectSuggestion(personId, list, suggestionPersonId, list2);
				base.Commit(list);
				base.Commit(list2);
			}
			finally
			{
				base.PerformanceTracker.Stop();
				base.LogEvent(base.PerformanceTracker.GetLogEvent());
			}
		}

		// Token: 0x0600375C RID: 14172 RVA: 0x000DE7A8 File Offset: 0x000DC9A8
		internal void LinkContacts(IList<ContactInfoForLinking> contacts, IList<ContactInfoForLinking> otherContacts, ContactInfoForLinking galContact)
		{
			Util.ThrowOnNullArgument(contacts, "contacts");
			Util.ThrowOnArgumentOutOfRangeOnLessThan(contacts.Count, 1, "contacts");
			Util.ThrowOnNullArgument(otherContacts, "otherContacts");
			Util.ThrowOnArgumentOutOfRangeOnLessThan(otherContacts.Count, 1, "otherContacts");
			PersonId personId = otherContacts[0].PersonId;
			HashSet<PersonId> hashSet = new HashSet<PersonId>();
			ManualLink.MergeLinkRejectHistory(hashSet, contacts);
			ManualLink.MergeLinkRejectHistory(hashSet, otherContacts);
			hashSet.Remove(contacts[0].PersonId);
			hashSet.Remove(otherContacts[0].PersonId);
			ManualLink.UpdateLinkRejectHistory(contacts, hashSet);
			ManualLink.UpdateLinkRejectHistory(otherContacts, hashSet);
			GALLinkState galLinkState;
			Guid? galLinkId;
			byte[] addressBookEntryId;
			string[] smtpAddressCache;
			if (galContact != null)
			{
				galLinkState = GALLinkState.Linked;
				galLinkId = galContact.GALLinkID;
				addressBookEntryId = galContact.AddressBookEntryId;
				smtpAddressCache = galContact.SmtpAddressCache;
			}
			else
			{
				if (contacts[0].GALLinkState == GALLinkState.NotAllowed || otherContacts[0].GALLinkState == GALLinkState.NotAllowed)
				{
					galLinkState = GALLinkState.NotAllowed;
				}
				else
				{
					galLinkState = GALLinkState.NotLinked;
				}
				galLinkId = null;
				addressBookEntryId = null;
				smtpAddressCache = Array<string>.Empty;
			}
			foreach (ContactInfoForLinking contactInfoForLinking in contacts)
			{
				if (!contactInfoForLinking.Linked || !contactInfoForLinking.PersonId.Equals(personId))
				{
					contactInfoForLinking.Linked = true;
					contactInfoForLinking.PersonId = personId;
				}
				else
				{
					ContactLink.Tracer.TraceDebug<VersionedId, string>((long)this.GetHashCode(), "ManualLink.Link: contact is already linked with PersonId set: {0}, {1}", contactInfoForLinking.ItemId, contactInfoForLinking.GivenName);
				}
				contactInfoForLinking.UpdateGALLink(galLinkState, galLinkId, addressBookEntryId, smtpAddressCache);
				contactInfoForLinking.UserApprovedLink = true;
			}
			foreach (ContactInfoForLinking contactInfoForLinking2 in otherContacts)
			{
				if (!contactInfoForLinking2.Linked)
				{
					contactInfoForLinking2.Linked = true;
				}
				contactInfoForLinking2.UpdateGALLink(galLinkState, galLinkId, addressBookEntryId, smtpAddressCache);
				contactInfoForLinking2.UserApprovedLink = true;
			}
		}

		// Token: 0x0600375D RID: 14173 RVA: 0x000DE990 File Offset: 0x000DCB90
		internal void LinkContactsWithGAL(IList<ContactInfoForLinking> personalContacts, ContactInfoForLinkingFromDirectory galContact)
		{
			foreach (ContactInfoForLinking contactInfoForLinking in personalContacts)
			{
				contactInfoForLinking.SetGALLink(galContact);
				contactInfoForLinking.UserApprovedLink = true;
			}
		}

		// Token: 0x0600375E RID: 14174 RVA: 0x000DE9E0 File Offset: 0x000DCBE0
		internal void UnlinkContact(ContactInfoForLinking contactBeingUnlinked, IList<ContactInfoForLinking> linkedContacts)
		{
			Util.ThrowOnNullArgument(contactBeingUnlinked, "contactBeingUnlinked");
			Util.ThrowOnNullArgument(linkedContacts, "linkedContacts");
			Util.ThrowOnArgumentOutOfRangeOnLessThan(linkedContacts.Count, 1, "linkedContacts");
			bool flag = ManualLink.IsGALLinked(linkedContacts);
			if (linkedContacts.Count == 1 && flag)
			{
				this.UnlinkContactFromGAL(linkedContacts);
				return;
			}
			if (linkedContacts.Count > 1)
			{
				PersonId personId = linkedContacts[0].PersonId;
				contactBeingUnlinked.ClearGALLink(flag ? GALLinkState.NotAllowed : GALLinkState.NotLinked);
				contactBeingUnlinked.Linked = false;
				contactBeingUnlinked.PersonId = PersonId.CreateNew();
				contactBeingUnlinked.LinkRejectHistory = new HashSet<PersonId>(new PersonId[]
				{
					personId
				});
				contactBeingUnlinked.UserApprovedLink = false;
				HashSet<PersonId> hashSet = new HashSet<PersonId>();
				if (!contactBeingUnlinked.PersonId.Equals(personId))
				{
					hashSet.Add(contactBeingUnlinked.PersonId);
				}
				foreach (ContactInfoForLinking contactInfoForLinking in linkedContacts)
				{
					if (!contactInfoForLinking.ItemId.ObjectId.Equals(contactBeingUnlinked.ItemId.ObjectId))
					{
						hashSet.UnionWith(contactInfoForLinking.LinkRejectHistory);
					}
				}
				bool flag2 = linkedContacts.Count == 2 && !flag;
				using (IEnumerator<ContactInfoForLinking> enumerator2 = linkedContacts.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						ContactInfoForLinking contactInfoForLinking2 = enumerator2.Current;
						if (!contactInfoForLinking2.ItemId.ObjectId.Equals(contactBeingUnlinked.ItemId.ObjectId))
						{
							contactInfoForLinking2.UserApprovedLink = true;
							contactInfoForLinking2.LinkRejectHistory = hashSet;
							if (flag2)
							{
								contactInfoForLinking2.Linked = false;
							}
						}
					}
					return;
				}
			}
			ContactLink.Tracer.TraceDebug<VersionedId, string>((long)this.GetHashCode(), "ManualLink.Unlink: contact is not linked with other contacts", contactBeingUnlinked.ItemId, contactBeingUnlinked.GivenName);
		}

		// Token: 0x0600375F RID: 14175 RVA: 0x000DEBB0 File Offset: 0x000DCDB0
		internal void UnlinkContactFromGAL(IList<ContactInfoForLinking> linkedContacts)
		{
			Util.ThrowOnNullArgument(linkedContacts, "linkedContacts");
			Util.ThrowOnArgumentOutOfRangeOnLessThan(linkedContacts.Count, 1, "linkedContacts");
			bool flag = linkedContacts.Count == 1;
			foreach (ContactInfoForLinking contactInfoForLinking in linkedContacts)
			{
				contactInfoForLinking.ClearGALLink(GALLinkState.NotAllowed);
				contactInfoForLinking.UserApprovedLink = true;
				if (flag)
				{
					contactInfoForLinking.Linked = false;
				}
			}
		}

		// Token: 0x06003760 RID: 14176 RVA: 0x000DEC30 File Offset: 0x000DCE30
		internal void RejectSuggestion(PersonId personId, IList<ContactInfoForLinking> personContacts, PersonId suggestionPersonId, IList<ContactInfoForLinking> suggestionContacts)
		{
			if (personContacts.Count > 0)
			{
				this.AddPersonIdToLinkRejectHistoryOfContacts(suggestionPersonId, personContacts);
			}
			if (suggestionContacts.Count > 0)
			{
				this.AddPersonIdToLinkRejectHistoryOfContacts(personId, suggestionContacts);
			}
		}

		// Token: 0x06003761 RID: 14177 RVA: 0x000DEC58 File Offset: 0x000DCE58
		private static void MergeLinkRejectHistory(HashSet<PersonId> linkRejectHistory, IList<ContactInfoForLinking> contacts)
		{
			foreach (ContactInfoForLinking contactInfoForLinking in contacts)
			{
				linkRejectHistory.UnionWith(contactInfoForLinking.LinkRejectHistory);
			}
		}

		// Token: 0x06003762 RID: 14178 RVA: 0x000DECA8 File Offset: 0x000DCEA8
		private static void UpdateLinkRejectHistory(IList<ContactInfoForLinking> contacts, HashSet<PersonId> mergedLinkRejectHistory)
		{
			foreach (ContactInfoForLinking contactInfoForLinking in contacts)
			{
				if (!contactInfoForLinking.LinkRejectHistory.SetEquals(mergedLinkRejectHistory))
				{
					contactInfoForLinking.LinkRejectHistory = mergedLinkRejectHistory;
				}
			}
		}

		// Token: 0x06003763 RID: 14179 RVA: 0x000DED00 File Offset: 0x000DCF00
		private static ContactInfoForLinking GetMatchingContactByItemId(IEnumerable<ContactInfoForLinking> contacts, VersionedId contactId)
		{
			foreach (ContactInfoForLinking contactInfoForLinking in contacts)
			{
				if (contactInfoForLinking.ItemId.ObjectId.Equals(contactId.ObjectId))
				{
					return contactInfoForLinking;
				}
			}
			return null;
		}

		// Token: 0x06003764 RID: 14180 RVA: 0x000DED60 File Offset: 0x000DCF60
		private static bool IsGALLinked(IList<ContactInfoForLinking> contacts)
		{
			return contacts.Count != 0 && contacts[0].GALLinkID != null;
		}

		// Token: 0x06003765 RID: 14181 RVA: 0x000DED8C File Offset: 0x000DCF8C
		private IList<ContactInfoForLinking> QueryPersonContacts(MailboxSession mailboxSession, PersonId personId)
		{
			List<ContactInfoForLinking> list = new List<ContactInfoForLinking>();
			AllPersonContactsEnumerator allPersonContactsEnumerator = AllPersonContactsEnumerator.Create(mailboxSession, personId, ContactInfoForLinking.Properties);
			foreach (IStorePropertyBag propertyBag in allPersonContactsEnumerator)
			{
				base.PerformanceTracker.IncrementContactsRead();
				list.Add(ContactInfoForLinkingFromPropertyBag.Create(mailboxSession, propertyBag));
			}
			return list;
		}

		// Token: 0x06003766 RID: 14182 RVA: 0x000DEDFC File Offset: 0x000DCFFC
		private ContactInfoForLinkingFromDirectory LoadADContact(IRecipientSession adSession, ADObjectId adObjectId)
		{
			return ContactInfoForLinkingFromDirectory.Create(adSession, adObjectId);
		}

		// Token: 0x06003767 RID: 14183 RVA: 0x000DEE08 File Offset: 0x000DD008
		private void AddPersonIdToLinkRejectHistoryOfContacts(PersonId personId, IList<ContactInfoForLinking> linkedContacts)
		{
			Util.ThrowOnNullArgument(personId, "personId");
			Util.ThrowOnNullArgument(linkedContacts, "linkedContacts");
			Util.ThrowOnArgumentOutOfRangeOnLessThan(linkedContacts.Count, 1, "linkedContacts");
			HashSet<PersonId> hashSet = new HashSet<PersonId>();
			hashSet.Add(personId);
			ManualLink.MergeLinkRejectHistory(hashSet, linkedContacts);
			foreach (ContactInfoForLinking contactInfoForLinking in linkedContacts)
			{
				if (!contactInfoForLinking.LinkRejectHistory.SetEquals(hashSet))
				{
					contactInfoForLinking.LinkRejectHistory = hashSet;
				}
			}
		}
	}
}
