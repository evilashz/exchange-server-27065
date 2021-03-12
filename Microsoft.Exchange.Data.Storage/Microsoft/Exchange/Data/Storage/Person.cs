using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000524 RID: 1316
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class Person : IPerson
	{
		// Token: 0x06003824 RID: 14372 RVA: 0x000E5EC5 File Offset: 0x000E40C5
		private Person(PersonId id, IStorePropertyBag aggregatedProperties, List<IStorePropertyBag> contacts, StoreSession session)
		{
			this.personId = id;
			this.aggregatedProperties = aggregatedProperties;
			this.contacts = contacts;
			this.storeSession = session;
		}

		// Token: 0x06003825 RID: 14373 RVA: 0x000E5EEA File Offset: 0x000E40EA
		public static Person Load(MailboxSession session, PersonId personId, ICollection<PropertyDefinition> properties)
		{
			return Person.Load(session, personId, properties, null, null);
		}

		// Token: 0x06003826 RID: 14374 RVA: 0x000E5EF8 File Offset: 0x000E40F8
		public static Person Load(StoreSession session, PersonId personId, ICollection<PropertyDefinition> properties, PropertyDefinition[] extendedProperties, StoreId folderId = null)
		{
			Person.Tracer.TraceDebug<string>(PersonId.TraceId(personId), "Person.Load: Entering, with personId = {0}", (personId == null) ? "(null)" : personId.ToString());
			Util.ThrowOnNullArgument(session, "session");
			Util.ThrowOnNullArgument(personId, "personId");
			PropertyDefinition[] propertiesToLoad = Person.GetPropertiesToLoad(properties, extendedProperties);
			List<IStorePropertyBag> contactsWithPersonId = Person.GetContactsWithPersonId(session, personId, propertiesToLoad, folderId);
			Person result = null;
			if (contactsWithPersonId != null && contactsWithPersonId.Count > 0)
			{
				Person.Tracer.TraceDebug<int>(PersonId.TraceId(personId), "Person.Load: Found {0} contacts in this Person", contactsWithPersonId.Count);
				result = Person.LoadFromContacts(personId, contactsWithPersonId, session, properties, extendedProperties);
			}
			Person.Tracer.TraceDebug(PersonId.TraceId(personId), "Person.Load: Exiting");
			return result;
		}

		// Token: 0x06003827 RID: 14375 RVA: 0x000E5FA0 File Offset: 0x000E41A0
		public static List<IStorePropertyBag> LoadContactsFromPublicFolder(PublicFolderSession session, PersonId personId, StoreId folderId, PropertyDefinition[] columns)
		{
			ComparisonFilter queryFilter = new ComparisonFilter(ComparisonOperator.Equal, InternalSchema.PersonId, personId);
			List<IStorePropertyBag> result;
			using (Folder folder = Folder.Bind(session, folderId, null))
			{
				using (QueryResult queryResult = folder.ItemQuery(ItemQueryType.None, queryFilter, null, columns))
				{
					IStorePropertyBag[] propertyBags = queryResult.GetPropertyBags(1);
					result = propertyBags.ToList<IStorePropertyBag>();
				}
			}
			return result;
		}

		// Token: 0x06003828 RID: 14376 RVA: 0x000E6014 File Offset: 0x000E4214
		public static Person LoadWithGALAggregation(MailboxSession session, PersonId personId, ICollection<PropertyDefinition> personProperties, PropertyDefinition[] extendedProperties)
		{
			Person.Tracer.TraceDebug<string>(PersonId.TraceId(personId), "Person.LoadWithGALAggregation: Entering, with personId = {0}", (personId == null) ? "(null)" : personId.ToString());
			Util.ThrowOnNullArgument(session, "session");
			Util.ThrowOnNullArgument(personId, "personId");
			PropertyDefinition[] propertiesToLoad = Person.GetPropertiesToLoad(personProperties, extendedProperties);
			HashSet<PropertyDefinition> contactPropertiesToLoad = Person.GetContactPropertiesToLoad(propertiesToLoad, Person.GALAggregationRequiredStoreProperties);
			AllPersonContactsEnumerator contactsEnumerator = AllPersonContactsEnumerator.Create(session, personId, contactPropertiesToLoad);
			List<IStorePropertyBag> list = Person.LoadFromEnumerator(contactsEnumerator);
			Person result = null;
			if (list.Count > 0)
			{
				Person.Tracer.TraceDebug<int>(PersonId.TraceId(personId), "Person.LoadWithGALAggregation: Found {0} contacts in this Person", list.Count);
				Person.LoadGALDataIfPersonIsGALLinked(session, personId, propertiesToLoad, list, contactPropertiesToLoad);
				result = Person.LoadFromContacts(personId, list, session, personProperties, extendedProperties);
			}
			Person.Tracer.TraceDebug(PersonId.TraceId(personId), "Person.LoadWithGALAggregation: Exiting");
			return result;
		}

		// Token: 0x06003829 RID: 14377 RVA: 0x000E60D4 File Offset: 0x000E42D4
		public static Person FindPersonLinkedToADEntry(MailboxSession session, ADRawEntry adRawEntry, ICollection<PropertyDefinition> properties)
		{
			ArgumentValidator.ThrowIfNull("session", session);
			ArgumentValidator.ThrowIfNull("adRawEntry", adRawEntry);
			Person.Tracer.TraceDebug<Guid>((long)adRawEntry.GetHashCode(), "Person.FindPersonLinkedToADEntry: with ADObjectId = {0}.", adRawEntry.Id.ObjectGuid);
			PersonId personId = Person.FindPersonIdByGALLinkID(session, adRawEntry.Id.ObjectGuid);
			if (personId == null)
			{
				Person.Tracer.TraceDebug((long)adRawEntry.GetHashCode(), "Person.FindPersonLinkedToADEntry: No Person found with the matching GALLinkID.");
				return null;
			}
			Person.Tracer.TraceDebug<PersonId>((long)adRawEntry.GetHashCode(), "Person.FindPersonLinkedToADEntry: Person {0} found with the matching GALLinkID.", personId);
			PropertyDefinition[] propertiesToLoad = Person.GetPropertiesToLoad(properties, null);
			AllPersonContactsEnumerator contactsEnumerator = AllPersonContactsEnumerator.Create(session, personId, propertiesToLoad);
			List<IStorePropertyBag> list = Person.LoadFromEnumerator(contactsEnumerator);
			if (list.Count == 0)
			{
				Person.Tracer.TraceDebug((long)adRawEntry.GetHashCode(), "Person.FindPersonLinkedToADEntry: No personal contacts loaded for the given person Id.");
				return null;
			}
			Person.Tracer.TraceDebug<int>((long)adRawEntry.GetHashCode(), "Person.FindPersonLinkedToADEntry: Loaded {0} personal contacts for the given personId.", list.Count);
			IStorePropertyBag item = Person.ConvertADRawEntryToContact(adRawEntry, personId, properties);
			list.Add(item);
			return Person.LoadFromContacts(personId, list, session, properties, null);
		}

		// Token: 0x0600382A RID: 14378 RVA: 0x000E61CC File Offset: 0x000E43CC
		public static PersonId FindPersonIdByEmailAddress(IMailboxSession session, IXSOFactory xsoFactory, string emailAddress)
		{
			return Person.FindPersonIdByEmailAddress(session, xsoFactory, session.GetDefaultFolderId(DefaultFolderType.MyContacts), emailAddress);
		}

		// Token: 0x0600382B RID: 14379 RVA: 0x000E61E0 File Offset: 0x000E43E0
		public static PersonId FindPersonIdByEmailAddress(IMailboxSession session, IXSOFactory xsoFactory, StoreObjectId folderId, string emailAddress)
		{
			Util.ThrowOnNullArgument(session, "session");
			Util.ThrowOnNullArgument(xsoFactory, "xsoFactory");
			Util.ThrowOnNullArgument(folderId, "folderId");
			Util.ThrowOnNullOrEmptyArgument(emailAddress, "emailAddress");
			Person.Tracer.TraceDebug<string, StoreObjectId>((long)emailAddress.GetHashCode(), "Person.FindPersonIdByEmailAddress: Find match for EmailAddress = {0} in {1}.", emailAddress, folderId);
			using (IFolder folder = xsoFactory.BindToFolder(session, folderId))
			{
				foreach (IStorePropertyBag storePropertyBag in new ContactsByEmailAddressEnumerator(folder, Person.PersonIdProperty, emailAddress))
				{
					PersonId valueOrDefault = storePropertyBag.GetValueOrDefault<PersonId>(ContactSchema.PersonId, null);
					if (valueOrDefault != null)
					{
						Person.Tracer.TraceDebug<PersonId>((long)emailAddress.GetHashCode(), "Person.FindPersonIdByEmailAddress: Match found - Person Id : {0}.", valueOrDefault);
						return valueOrDefault;
					}
				}
			}
			Person.Tracer.TraceDebug((long)emailAddress.GetHashCode(), "Person.FindPersonIdByEmailAddress: No Match found.");
			return null;
		}

		// Token: 0x0600382C RID: 14380 RVA: 0x000E62D8 File Offset: 0x000E44D8
		public static Person LoadFromContacts(PersonId personId, List<IStorePropertyBag> contacts, StoreSession session, ICollection<PropertyDefinition> requestedProperties, PropertyDefinition[] extendedProperties)
		{
			Util.ThrowOnNullArgument(contacts, "contacts");
			Util.ThrowOnNullArgument(session, "session");
			Util.ThrowOnNullArgument(personId, "personId");
			Util.ThrowOnArgumentOutOfRangeOnLessThan(contacts.Count, 1, "contacts");
			Person.Tracer.TraceDebug<string>(PersonId.TraceId(personId), "Person.LoadFromContacts: Entering, with personId = {0}", personId.ToString());
			Person.Tracer.TraceDebug<int>(PersonId.TraceId(personId), "Person.LoadFromContacts: Found {0} contacts in this Person", contacts.Count);
			PersonPropertyAggregationContext aggregationContext = new PersonPropertyAggregationContext(contacts, session.ContactFolders, session.ClientInfoString);
			IStorePropertyBag storePropertyBag = Person.InternalGetAggregatedProperties(aggregationContext, requestedProperties, extendedProperties);
			Person result = new Person(personId, storePropertyBag, contacts, session);
			Person.Tracer.TraceDebug(PersonId.TraceId(personId), "Person.LoadFromContacts: Exiting");
			return result;
		}

		// Token: 0x0600382D RID: 14381 RVA: 0x000E638C File Offset: 0x000E458C
		public static Person LoadNotes(StoreSession session, PersonId personId, int requestedBytesToFetch, StoreId folderId = null)
		{
			Util.ThrowOnNullArgument(session, "session");
			Util.ThrowOnNullArgument(personId, "personId");
			Util.ThrowOnNullArgument(requestedBytesToFetch, "requestedBytesToFetch");
			int maxBytesToReadFromStore = Math.Min(requestedBytesToFetch, Person.MaxNotesBytes);
			List<IStorePropertyBag> contactsWithPersonId = Person.GetContactsWithPersonId(session, personId, Person.PropertiesToLoadForNotes, folderId);
			MemoryPropertyBag memoryPropertyBag = new MemoryPropertyBag();
			memoryPropertyBag[ContactSchema.PersonId] = personId;
			List<AttributedValue<PersonNotes>> list = new List<AttributedValue<PersonNotes>>(contactsWithPersonId.Count);
			for (int i = 0; i < contactsWithPersonId.Count; i++)
			{
				VersionedId versionedIdForPropertyBag = Person.GetVersionedIdForPropertyBag(contactsWithPersonId[i]);
				PersonId valueOrDefault = contactsWithPersonId[i].GetValueOrDefault<PersonId>(ContactSchema.PersonId, null);
				if (personId.Equals(valueOrDefault))
				{
					using (Item item = Item.Bind(session, versionedIdForPropertyBag, Person.PropertiesToLoadForNotes))
					{
						Body body = item.Body;
						PersonNotes personNotes = Person.ReadPersonNotes(body, maxBytesToReadFromStore);
						if (personNotes != null)
						{
							AttributedValue<PersonNotes> item2 = new AttributedValue<PersonNotes>(personNotes, new string[]
							{
								i.ToString(CultureInfo.InvariantCulture)
							});
							list.Add(item2);
						}
					}
				}
			}
			MailboxSession mailboxSession = session as MailboxSession;
			if (mailboxSession != null)
			{
				PersonNotes personNotes2 = Person.LoadGALNotesIfPersonIsGALLinked(mailboxSession, personId, requestedBytesToFetch, contactsWithPersonId);
				if (personNotes2 != null)
				{
					list.Add(new AttributedValue<PersonNotes>(personNotes2, new string[]
					{
						contactsWithPersonId.Count.ToString(CultureInfo.InvariantCulture)
					}));
				}
			}
			memoryPropertyBag[Person.SimpleVirtualPersonaBodiesProperty] = list;
			return new Person(personId, memoryPropertyBag.AsIStorePropertyBag(), contactsWithPersonId, session);
		}

		// Token: 0x0600382E RID: 14382 RVA: 0x000E6518 File Offset: 0x000E4718
		public static PersonId CreatePerson(StoreSession session, PersonId personId, ICollection<StoreObjectPropertyChange> propertyChanges, StoreId parentFolder, bool isGroup)
		{
			Person.Tracer.TraceDebug<string, bool>(PersonId.TraceId(personId), "Person.CreatePerson: Entering, with personId = {0} and isGroup = {1}", (personId == null) ? "(null)" : personId.ToString(), isGroup);
			Util.ThrowOnNullArgument(session, "session");
			Util.ThrowOnNullArgument(session, "propertyChanges");
			Util.ThrowOnNullArgument(personId, "personId");
			Person person = new Person(personId, null, new List<IStorePropertyBag>(12), null);
			PersonId result;
			if (!isGroup)
			{
				result = person.CreateContact(session, propertyChanges, parentFolder);
			}
			else
			{
				result = person.CreateGroup(session, propertyChanges, parentFolder);
			}
			Person.Tracer.TraceDebug<bool>(PersonId.TraceId(personId), "Person.CreatePerson: Exiting, with isGroup = {0}", isGroup);
			return result;
		}

		// Token: 0x0600382F RID: 14383 RVA: 0x000E65B0 File Offset: 0x000E47B0
		internal static PersonId FindPersonIdByGALLinkID(MailboxSession session, Guid galLinkID)
		{
			ArgumentValidator.ThrowIfNull("session", session);
			ArgumentValidator.ThrowIfEmpty("galLinkID", galLinkID);
			Person.Tracer.TraceDebug<Guid>((long)galLinkID.GetHashCode(), "Person.FindPersonIdByGALLinkID: Find match for GALLinkID = {0}.", galLinkID);
			ContactsByGALLinkIdEnumerator contactsByGALLinkIdEnumerator = new ContactsByGALLinkIdEnumerator(session, DefaultFolderType.MyContacts, galLinkID, Person.PersonIdProperty);
			foreach (IStorePropertyBag storePropertyBag in contactsByGALLinkIdEnumerator)
			{
				PersonId valueOrDefault = storePropertyBag.GetValueOrDefault<PersonId>(ContactSchema.PersonId, null);
				if (valueOrDefault != null)
				{
					Person.Tracer.TraceDebug<PersonId>((long)galLinkID.GetHashCode(), "Person.FindPersonIdByGALLinkID: Match found - Person Id : {0}.", valueOrDefault);
					return valueOrDefault;
				}
			}
			Person.Tracer.TraceDebug((long)galLinkID.GetHashCode(), "Person.FindPersonIdByGALLinkID: No Match found.");
			return null;
		}

		// Token: 0x06003830 RID: 14384 RVA: 0x000E6694 File Offset: 0x000E4894
		public PersonId UpdatePerson(StoreSession session, ICollection<StoreObjectPropertyChange> propertyChanges, bool isGroup)
		{
			Person.Tracer.TraceDebug<PersonId, bool>(PersonId.TraceId(this.PersonId), "Person.UpdatePerson: Entering, this.PersonId = {0} and isGroup = {1}", this.PersonId, isGroup);
			Util.ThrowOnNullArgument(session, "session");
			Util.ThrowOnNullArgument(propertyChanges, "propertyChanges");
			IEnumerable<IStorePropertyBag> enumerable = from c in this.contacts
			where Person.IsContactWriteable(c)
			select c;
			PersonId result;
			if (enumerable.Any<IStorePropertyBag>())
			{
				Dictionary<VersionedId, List<StoreObjectPropertyChange>> dictionary = new Dictionary<VersionedId, List<StoreObjectPropertyChange>>();
				Dictionary<string, int> dictionary2 = new Dictionary<string, int>();
				foreach (StoreObjectPropertyChange storeObjectPropertyChange in propertyChanges)
				{
					if (!isGroup)
					{
						storeObjectPropertyChange.PropertyDefinition = Person.GetValidatedPropertyDefinition(storeObjectPropertyChange.PropertyDefinition, Person.personPropertiesToContactPropertiesMap);
					}
					else
					{
						storeObjectPropertyChange.PropertyDefinition = Person.GetValidatedPropertyDefinition(storeObjectPropertyChange.PropertyDefinition, Person.personPropertiesToGroupPropertiesMap);
					}
					storeObjectPropertyChange.IsPropertyValidated = true;
					bool flag = false;
					int num = 0;
					if (!dictionary2.ContainsKey(storeObjectPropertyChange.PropertyDefinition.Name))
					{
						dictionary2.Add(storeObjectPropertyChange.PropertyDefinition.Name, num);
					}
					foreach (IStorePropertyBag storePropertyBag in enumerable)
					{
						if (storeObjectPropertyChange.PropertyDefinition.Name == PersonSchema.Bodies.Name || storeObjectPropertyChange.PropertyDefinition.Name == PersonSchema.Members.Name)
						{
							VersionedId versionedIdForPropertyBag = Person.GetVersionedIdForPropertyBag(storePropertyBag);
							StoreObjectPropertyChange change = new StoreObjectPropertyChange((storeObjectPropertyChange.PropertyDefinition.Name == PersonSchema.Bodies.Name) ? ItemSchema.RtfBody : DistributionListSchema.Members, storeObjectPropertyChange.OldValue, storeObjectPropertyChange.NewValue);
							Person.AddToContactChanges(dictionary, versionedIdForPropertyBag, change);
							flag = true;
						}
						else
						{
							bool flag2 = false;
							bool flag3 = Person.IsValidContactForUpdate(storePropertyBag, storeObjectPropertyChange.PropertyDefinition, storeObjectPropertyChange.OldValue, num, dictionary2, out flag2);
							if (flag3)
							{
								VersionedId versionedIdForPropertyBag2 = Person.GetVersionedIdForPropertyBag(storePropertyBag);
								Person.AddToContactChanges(dictionary, versionedIdForPropertyBag2, storeObjectPropertyChange);
								flag = true;
								if (Person.IsEmptyValue(storeObjectPropertyChange.OldValue))
								{
									dictionary2[storeObjectPropertyChange.PropertyDefinition.Name] = num + 1;
									break;
								}
							}
							num++;
							if (!flag3 && Person.IsEmptyValue(storeObjectPropertyChange.OldValue) && !flag2)
							{
								dictionary2[storeObjectPropertyChange.PropertyDefinition.Name] = num;
							}
						}
					}
					if (!flag)
					{
						Person.Tracer.TraceDebug(PersonId.TraceId(this.PersonId), "Person.UpdatePerson: No valid contact found for update. Checking if we can find space in Outlook contact.");
						IStorePropertyBag propertyBag = null;
						if (!Person.FindEmptyPropertyContact(enumerable, storeObjectPropertyChange.PropertyDefinition, out propertyBag))
						{
							throw new SaveConflictException(ServerStrings.ExSaveFailedBecauseOfConflicts(this.PersonId));
						}
						VersionedId versionedIdForPropertyBag3 = Person.GetVersionedIdForPropertyBag(propertyBag);
						Person.AddToContactChanges(dictionary, versionedIdForPropertyBag3, storeObjectPropertyChange);
						flag = true;
					}
				}
				if (!isGroup)
				{
					result = Person.UpdateContacts(session, dictionary, this.personId);
				}
				else
				{
					result = Person.UpdateGroup(session, dictionary, this.personId);
				}
			}
			else
			{
				if (isGroup)
				{
					throw new NotImplementedException("Cannot Save a GAL DL as a PDL");
				}
				this.CheckAndAddNamePropertiesForNewContact(propertyChanges);
				result = this.CreateContact(session, propertyChanges, session.GetDefaultFolderId(DefaultFolderType.Contacts));
			}
			Person.Tracer.TraceDebug<bool>(PersonId.TraceId(this.PersonId), "Person.UpdatePerson: Exiting with isGroup = {0}", isGroup);
			return result;
		}

		// Token: 0x06003831 RID: 14385 RVA: 0x000E6A34 File Offset: 0x000E4C34
		public void Delete(StoreSession session, DeleteItemFlags deleteFlags, StoreId deleteInFolder)
		{
			Person.Tracer.TraceDebug<PersonId, StoreId>(PersonId.TraceId(this.PersonId), "Person.Delete: Entering, this.PersonId = {0} and DeleteInFolder = {1}", this.PersonId, deleteInFolder);
			Util.ThrowOnNullArgument(session, "session");
			EnumValidator.ThrowIfInvalid<DeleteItemFlags>(deleteFlags, "deleteFlags");
			if (this.contacts == null || this.contacts.Count == 0)
			{
				Person.Tracer.TraceDebug(PersonId.TraceId(this.PersonId), "Person.Delete: Exiting (no contacts to delete)");
				return;
			}
			IEnumerable<IStorePropertyBag> source = from contact in this.contacts
			where Person.CanContactBeDeleted(contact, deleteInFolder)
			select contact;
			VersionedId[] array = (from contact in source
			select contact.GetValueOrDefault<VersionedId>(ItemSchema.Id, null) into id
			where id != null
			select id).ToArray<VersionedId>();
			if (array.Length == 0)
			{
				Person.Tracer.TraceDebug(PersonId.TraceId(this.PersonId), "Person.Delete: Exiting (no contacts to delete)");
				return;
			}
			AggregateOperationResult aggregateOperationResult = session.Delete(deleteFlags, array);
			if (aggregateOperationResult.OperationResult == OperationResult.Succeeded)
			{
				try
				{
					MailboxSession mailboxSession = session as MailboxSession;
					if (mailboxSession != null && mailboxSession.LogonType != LogonType.Delegated && mailboxSession.Capabilities.CanHaveJunkEmailRule && !mailboxSession.MailboxOwner.ObjectId.IsNullOrEmpty())
					{
						JunkEmailRule junkEmailRule = mailboxSession.JunkEmailRule;
						if (junkEmailRule.IsContactsFolderTrusted)
						{
							junkEmailRule.SynchronizeContactsCache();
							junkEmailRule.Save();
						}
					}
				}
				catch (Exception ex)
				{
					Person.Tracer.TraceDebug(PersonId.TraceId(this.PersonId), string.Format("Person.Delete: Hit exception when update contacts cache. {0}", ex.Message));
				}
			}
			switch (aggregateOperationResult.OperationResult)
			{
			case OperationResult.Succeeded:
				Person.Tracer.TraceDebug(PersonId.TraceId(this.PersonId), "Person.Delete: Exiting (success)");
				return;
			case OperationResult.Failed:
			case OperationResult.PartiallySucceeded:
				throw aggregateOperationResult.GroupOperationResults.First((GroupOperationResult singleResult) => singleResult.OperationResult == OperationResult.Failed).Exception;
			default:
				Person.Tracer.TraceDebug(PersonId.TraceId(this.PersonId), "Person.Delete: Exiting (unexpected exit path)");
				return;
			}
		}

		// Token: 0x06003832 RID: 14386 RVA: 0x000E6C64 File Offset: 0x000E4E64
		public string CalculateChangeKey()
		{
			Person.Tracer.TraceDebug<PersonId>(PersonId.TraceId(this.PersonId), "Person.CalculateChangeKey: Entering, this.PersonId = {0}", this.PersonId);
			List<byte[]> list = new List<byte[]>();
			int num = 0;
			for (int i = 0; i < this.contacts.Count; i++)
			{
				VersionedId valueOrDefault = this.contacts[i].GetValueOrDefault<VersionedId>(ItemSchema.Id, null);
				if (valueOrDefault != null)
				{
					byte[] array = valueOrDefault.ChangeKeyAsByteArray();
					if (array != null)
					{
						if (array.Length > num)
						{
							num = array.Length;
						}
						list.Add(array);
					}
				}
			}
			if (list.Count == 0)
			{
				Person.Tracer.TraceDebug(PersonId.TraceId(this.PersonId), "Person.CalculateChangeKey: Exiting (returning null; found no usable changekeys even though at least one contact is part of this Person)");
				return null;
			}
			byte[] array2 = new byte[num + 8];
			BitConverter.GetBytes(Convert.ToInt32(StoreObjectType.Person)).CopyTo(array2, 0);
			BitConverter.GetBytes(num).CopyTo(array2, 4);
			for (int j = 0; j < list.Count; j++)
			{
				for (int k = 0; k < list[j].Length; k++)
				{
					byte[] array3 = array2;
					int num2 = k + 8;
					array3[num2] ^= list[j][k];
				}
			}
			string text = Convert.ToBase64String(array2);
			Person.Tracer.TraceDebug<string>(PersonId.TraceId(this.PersonId), "Person.CalculateChangeKey: Exiting (returning {0})", text);
			return text;
		}

		// Token: 0x06003833 RID: 14387 RVA: 0x000E6DB8 File Offset: 0x000E4FB8
		public Stream GetAttachedPhoto(out string partnerNetworkId, out StoreObjectId contactId)
		{
			Person.Tracer.TraceDebug<PersonId>(PersonId.TraceId(this.PersonId), "Person.GetAttachedPhoto: Entering, this.PersonId = {0}", this.PersonId);
			contactId = null;
			Stream stream = null;
			partnerNetworkId = null;
			StoreObjectId valueOrDefault = this.aggregatedProperties.GetValueOrDefault<StoreObjectId>(PersonSchema.PhotoContactEntryId, null);
			if (valueOrDefault != null)
			{
				using (Contact contact = Item.Bind(this.storeSession, valueOrDefault, ItemBindOption.None, new PropertyDefinition[]
				{
					ContactSchema.PartnerNetworkId
				}) as Contact)
				{
					stream = contact.GetPhotoStream();
					if (stream != null)
					{
						contactId = valueOrDefault;
						partnerNetworkId = contact.PartnerNetworkId;
					}
				}
			}
			Person.Tracer.TraceDebug<string>(PersonId.TraceId(this.PersonId), "Person.GetAttachedPhoto: Exiting (returning {0})", (stream == null || stream.Length == 0L) ? "no photo" : "photo");
			return stream;
		}

		// Token: 0x1700115F RID: 4447
		// (get) Token: 0x06003834 RID: 14388 RVA: 0x000E6E8C File Offset: 0x000E508C
		public IStorePropertyBag PropertyBag
		{
			get
			{
				return this.aggregatedProperties;
			}
		}

		// Token: 0x17001160 RID: 4448
		// (get) Token: 0x06003835 RID: 14389 RVA: 0x000E6E94 File Offset: 0x000E5094
		public PersonId PersonId
		{
			get
			{
				return this.personId;
			}
		}

		// Token: 0x17001161 RID: 4449
		// (get) Token: 0x06003836 RID: 14390 RVA: 0x000E6E9C File Offset: 0x000E509C
		public PersonType PersonType
		{
			get
			{
				return this.aggregatedProperties.GetValueOrDefault<PersonType>(PersonSchema.PersonType, PersonType.Unknown);
			}
		}

		// Token: 0x17001162 RID: 4450
		// (get) Token: 0x06003837 RID: 14391 RVA: 0x000E6EAF File Offset: 0x000E50AF
		public Guid GALLinkId
		{
			get
			{
				return this.aggregatedProperties.GetValueOrDefault<Guid>(PersonSchema.GALLinkID, Guid.Empty);
			}
		}

		// Token: 0x17001163 RID: 4451
		// (get) Token: 0x06003838 RID: 14392 RVA: 0x000E6EC6 File Offset: 0x000E50C6
		public ExDateTime CreationTime
		{
			get
			{
				return this.aggregatedProperties.GetValueOrDefault<ExDateTime>(PersonSchema.CreationTime, ExDateTime.MinValue);
			}
		}

		// Token: 0x17001164 RID: 4452
		// (get) Token: 0x06003839 RID: 14393 RVA: 0x000E6EDD File Offset: 0x000E50DD
		public bool IsFavorite
		{
			get
			{
				return this.aggregatedProperties.GetValueOrDefault<bool>(PersonSchema.IsFavorite, false);
			}
		}

		// Token: 0x17001165 RID: 4453
		// (get) Token: 0x0600383A RID: 14394 RVA: 0x000E6EF0 File Offset: 0x000E50F0
		public string DisplayName
		{
			get
			{
				return this.aggregatedProperties.GetValueOrDefault<string>(PersonSchema.DisplayName, null);
			}
		}

		// Token: 0x17001166 RID: 4454
		// (get) Token: 0x0600383B RID: 14395 RVA: 0x000E6F03 File Offset: 0x000E5103
		public string DisplayNameFirstLast
		{
			get
			{
				return this.aggregatedProperties.GetValueOrDefault<string>(PersonSchema.DisplayNameFirstLast, null);
			}
		}

		// Token: 0x17001167 RID: 4455
		// (get) Token: 0x0600383C RID: 14396 RVA: 0x000E6F16 File Offset: 0x000E5116
		public string DisplayNameLastFirst
		{
			get
			{
				return this.aggregatedProperties.GetValueOrDefault<string>(PersonSchema.DisplayNameLastFirst, null);
			}
		}

		// Token: 0x17001168 RID: 4456
		// (get) Token: 0x0600383D RID: 14397 RVA: 0x000E6F29 File Offset: 0x000E5129
		public string FileAs
		{
			get
			{
				return this.aggregatedProperties.GetValueOrDefault<string>(PersonSchema.FileAs, null);
			}
		}

		// Token: 0x17001169 RID: 4457
		// (get) Token: 0x0600383E RID: 14398 RVA: 0x000E6F3C File Offset: 0x000E513C
		public string FileAsId
		{
			get
			{
				return this.aggregatedProperties.GetValueOrDefault<string>(PersonSchema.FileAsId, null);
			}
		}

		// Token: 0x1700116A RID: 4458
		// (get) Token: 0x0600383F RID: 14399 RVA: 0x000E6F4F File Offset: 0x000E514F
		public string DisplayNamePrefix
		{
			get
			{
				return this.aggregatedProperties.GetValueOrDefault<string>(PersonSchema.DisplayNamePrefix, null);
			}
		}

		// Token: 0x1700116B RID: 4459
		// (get) Token: 0x06003840 RID: 14400 RVA: 0x000E6F62 File Offset: 0x000E5162
		public string GivenName
		{
			get
			{
				return this.aggregatedProperties.GetValueOrDefault<string>(PersonSchema.GivenName, null);
			}
		}

		// Token: 0x1700116C RID: 4460
		// (get) Token: 0x06003841 RID: 14401 RVA: 0x000E6F75 File Offset: 0x000E5175
		public string MiddleName
		{
			get
			{
				return this.aggregatedProperties.GetValueOrDefault<string>(PersonSchema.MiddleName, null);
			}
		}

		// Token: 0x1700116D RID: 4461
		// (get) Token: 0x06003842 RID: 14402 RVA: 0x000E6F88 File Offset: 0x000E5188
		public string Surname
		{
			get
			{
				return this.aggregatedProperties.GetValueOrDefault<string>(PersonSchema.Surname, null);
			}
		}

		// Token: 0x1700116E RID: 4462
		// (get) Token: 0x06003843 RID: 14403 RVA: 0x000E6F9B File Offset: 0x000E519B
		public string Generation
		{
			get
			{
				return this.aggregatedProperties.GetValueOrDefault<string>(PersonSchema.Generation, null);
			}
		}

		// Token: 0x1700116F RID: 4463
		// (get) Token: 0x06003844 RID: 14404 RVA: 0x000E6FAE File Offset: 0x000E51AE
		public string Nickname
		{
			get
			{
				return this.aggregatedProperties.GetValueOrDefault<string>(PersonSchema.Nickname, null);
			}
		}

		// Token: 0x17001170 RID: 4464
		// (get) Token: 0x06003845 RID: 14405 RVA: 0x000E6FC1 File Offset: 0x000E51C1
		public string Alias
		{
			get
			{
				return this.aggregatedProperties.GetValueOrDefault<string>(PersonSchema.Alias, null);
			}
		}

		// Token: 0x17001171 RID: 4465
		// (get) Token: 0x06003846 RID: 14406 RVA: 0x000E6FD4 File Offset: 0x000E51D4
		public string YomiCompanyName
		{
			get
			{
				return this.aggregatedProperties.GetValueOrDefault<string>(PersonSchema.YomiCompanyName, null);
			}
		}

		// Token: 0x17001172 RID: 4466
		// (get) Token: 0x06003847 RID: 14407 RVA: 0x000E6FE7 File Offset: 0x000E51E7
		public string YomiFirstName
		{
			get
			{
				return this.aggregatedProperties.GetValueOrDefault<string>(PersonSchema.YomiFirstName, null);
			}
		}

		// Token: 0x17001173 RID: 4467
		// (get) Token: 0x06003848 RID: 14408 RVA: 0x000E6FFA File Offset: 0x000E51FA
		public string YomiLastName
		{
			get
			{
				return this.aggregatedProperties.GetValueOrDefault<string>(PersonSchema.YomiLastName, null);
			}
		}

		// Token: 0x17001174 RID: 4468
		// (get) Token: 0x06003849 RID: 14409 RVA: 0x000E700D File Offset: 0x000E520D
		public string Title
		{
			get
			{
				return this.aggregatedProperties.GetValueOrDefault<string>(PersonSchema.Title, null);
			}
		}

		// Token: 0x17001175 RID: 4469
		// (get) Token: 0x0600384A RID: 14410 RVA: 0x000E7020 File Offset: 0x000E5220
		public string Department
		{
			get
			{
				return this.aggregatedProperties.GetValueOrDefault<string>(PersonSchema.Department, null);
			}
		}

		// Token: 0x17001176 RID: 4470
		// (get) Token: 0x0600384B RID: 14411 RVA: 0x000E7033 File Offset: 0x000E5233
		public string CompanyName
		{
			get
			{
				return this.aggregatedProperties.GetValueOrDefault<string>(PersonSchema.CompanyName, null);
			}
		}

		// Token: 0x17001177 RID: 4471
		// (get) Token: 0x0600384C RID: 14412 RVA: 0x000E7046 File Offset: 0x000E5246
		public string Location
		{
			get
			{
				return this.aggregatedProperties.GetValueOrDefault<string>(PersonSchema.Location, null);
			}
		}

		// Token: 0x17001178 RID: 4472
		// (get) Token: 0x0600384D RID: 14413 RVA: 0x000E7059 File Offset: 0x000E5259
		public Participant EmailAddress
		{
			get
			{
				return this.aggregatedProperties.GetValueOrDefault<Participant>(PersonSchema.EmailAddress, null);
			}
		}

		// Token: 0x17001179 RID: 4473
		// (get) Token: 0x0600384E RID: 14414 RVA: 0x000E706C File Offset: 0x000E526C
		public PhoneNumber PhoneNumber
		{
			get
			{
				return this.aggregatedProperties.GetValueOrDefault<PhoneNumber>(PersonSchema.PhoneNumber, null);
			}
		}

		// Token: 0x1700117A RID: 4474
		// (get) Token: 0x0600384F RID: 14415 RVA: 0x000E707F File Offset: 0x000E527F
		public string ImAddress
		{
			get
			{
				return this.aggregatedProperties.GetValueOrDefault<string>(PersonSchema.IMAddress, null);
			}
		}

		// Token: 0x1700117B RID: 4475
		// (get) Token: 0x06003850 RID: 14416 RVA: 0x000E7092 File Offset: 0x000E5292
		public string HomeCity
		{
			get
			{
				return this.aggregatedProperties.GetValueOrDefault<string>(PersonSchema.HomeCity, null);
			}
		}

		// Token: 0x1700117C RID: 4476
		// (get) Token: 0x06003851 RID: 14417 RVA: 0x000E70A5 File Offset: 0x000E52A5
		public string WorkCity
		{
			get
			{
				return this.aggregatedProperties.GetValueOrDefault<string>(PersonSchema.WorkCity, null);
			}
		}

		// Token: 0x1700117D RID: 4477
		// (get) Token: 0x06003852 RID: 14418 RVA: 0x000E70B8 File Offset: 0x000E52B8
		public IEnumerable<AttributedValue<PersonNotes>> Bodies
		{
			get
			{
				return this.aggregatedProperties.GetValueOrDefault<IEnumerable<AttributedValue<PersonNotes>>>(PersonSchema.Bodies, null);
			}
		}

		// Token: 0x1700117E RID: 4478
		// (get) Token: 0x06003853 RID: 14419 RVA: 0x000E70CB File Offset: 0x000E52CB
		public int RelevanceScore
		{
			get
			{
				return this.aggregatedProperties.GetValueOrDefault<int>(PersonSchema.RelevanceScore, int.MaxValue);
			}
		}

		// Token: 0x1700117F RID: 4479
		// (get) Token: 0x06003854 RID: 14420 RVA: 0x000E70E2 File Offset: 0x000E52E2
		public IEnumerable<Participant> EmailAddresses
		{
			get
			{
				return this.aggregatedProperties.GetValueOrDefault<Participant[]>(PersonSchema.EmailAddresses, null);
			}
		}

		// Token: 0x17001180 RID: 4480
		// (get) Token: 0x06003855 RID: 14421 RVA: 0x000E70F5 File Offset: 0x000E52F5
		public IEnumerable<StoreObjectId> FolderIds
		{
			get
			{
				return this.aggregatedProperties.GetValueOrDefault<IEnumerable<StoreObjectId>>(PersonSchema.FolderIds, null);
			}
		}

		// Token: 0x17001181 RID: 4481
		// (get) Token: 0x06003856 RID: 14422 RVA: 0x000E7108 File Offset: 0x000E5308
		public IEnumerable<Attribution> Attributions
		{
			get
			{
				return this.aggregatedProperties.GetValueOrDefault<IEnumerable<Attribution>>(PersonSchema.Attributions, null);
			}
		}

		// Token: 0x17001182 RID: 4482
		// (get) Token: 0x06003857 RID: 14423 RVA: 0x000E711B File Offset: 0x000E531B
		public IEnumerable<Participant> Members
		{
			get
			{
				return this.aggregatedProperties.GetValueOrDefault<Participant[]>(PersonSchema.Members, null);
			}
		}

		// Token: 0x17001183 RID: 4483
		// (get) Token: 0x06003858 RID: 14424 RVA: 0x000E712E File Offset: 0x000E532E
		public IEnumerable<AttributedValue<string>> DisplayNames
		{
			get
			{
				return this.aggregatedProperties.GetValueOrDefault<IEnumerable<AttributedValue<string>>>(PersonSchema.DisplayNames, null);
			}
		}

		// Token: 0x17001184 RID: 4484
		// (get) Token: 0x06003859 RID: 14425 RVA: 0x000E7141 File Offset: 0x000E5341
		public IEnumerable<AttributedValue<string>> FileAses
		{
			get
			{
				return this.aggregatedProperties.GetValueOrDefault<IEnumerable<AttributedValue<string>>>(PersonSchema.FileAses, null);
			}
		}

		// Token: 0x17001185 RID: 4485
		// (get) Token: 0x0600385A RID: 14426 RVA: 0x000E7154 File Offset: 0x000E5354
		public IEnumerable<AttributedValue<string>> FileAsIds
		{
			get
			{
				return this.aggregatedProperties.GetValueOrDefault<IEnumerable<AttributedValue<string>>>(PersonSchema.FileAsIds, null);
			}
		}

		// Token: 0x17001186 RID: 4486
		// (get) Token: 0x0600385B RID: 14427 RVA: 0x000E7167 File Offset: 0x000E5367
		public IEnumerable<AttributedValue<string>> DisplayNamePrefixes
		{
			get
			{
				return this.aggregatedProperties.GetValueOrDefault<IEnumerable<AttributedValue<string>>>(PersonSchema.DisplayNamePrefixes, null);
			}
		}

		// Token: 0x17001187 RID: 4487
		// (get) Token: 0x0600385C RID: 14428 RVA: 0x000E717A File Offset: 0x000E537A
		public IEnumerable<AttributedValue<string>> GivenNames
		{
			get
			{
				return this.aggregatedProperties.GetValueOrDefault<IEnumerable<AttributedValue<string>>>(PersonSchema.GivenNames, null);
			}
		}

		// Token: 0x17001188 RID: 4488
		// (get) Token: 0x0600385D RID: 14429 RVA: 0x000E718D File Offset: 0x000E538D
		public IEnumerable<AttributedValue<string>> MiddleNames
		{
			get
			{
				return this.aggregatedProperties.GetValueOrDefault<IEnumerable<AttributedValue<string>>>(PersonSchema.MiddleNames, null);
			}
		}

		// Token: 0x17001189 RID: 4489
		// (get) Token: 0x0600385E RID: 14430 RVA: 0x000E71A0 File Offset: 0x000E53A0
		public IEnumerable<AttributedValue<string>> Surnames
		{
			get
			{
				return this.aggregatedProperties.GetValueOrDefault<IEnumerable<AttributedValue<string>>>(PersonSchema.Surnames, null);
			}
		}

		// Token: 0x1700118A RID: 4490
		// (get) Token: 0x0600385F RID: 14431 RVA: 0x000E71B3 File Offset: 0x000E53B3
		public IEnumerable<AttributedValue<string>> Generations
		{
			get
			{
				return this.aggregatedProperties.GetValueOrDefault<IEnumerable<AttributedValue<string>>>(PersonSchema.Generations, null);
			}
		}

		// Token: 0x1700118B RID: 4491
		// (get) Token: 0x06003860 RID: 14432 RVA: 0x000E71C6 File Offset: 0x000E53C6
		public IEnumerable<AttributedValue<string>> Nicknames
		{
			get
			{
				return this.aggregatedProperties.GetValueOrDefault<IEnumerable<AttributedValue<string>>>(PersonSchema.Nicknames, null);
			}
		}

		// Token: 0x1700118C RID: 4492
		// (get) Token: 0x06003861 RID: 14433 RVA: 0x000E71D9 File Offset: 0x000E53D9
		public IEnumerable<AttributedValue<string>> Initials
		{
			get
			{
				return this.aggregatedProperties.GetValueOrDefault<IEnumerable<AttributedValue<string>>>(PersonSchema.Initials, null);
			}
		}

		// Token: 0x1700118D RID: 4493
		// (get) Token: 0x06003862 RID: 14434 RVA: 0x000E71EC File Offset: 0x000E53EC
		public IEnumerable<AttributedValue<string>> YomiCompanyNames
		{
			get
			{
				return this.aggregatedProperties.GetValueOrDefault<IEnumerable<AttributedValue<string>>>(PersonSchema.YomiCompanyNames, null);
			}
		}

		// Token: 0x1700118E RID: 4494
		// (get) Token: 0x06003863 RID: 14435 RVA: 0x000E71FF File Offset: 0x000E53FF
		public IEnumerable<AttributedValue<string>> YomiFirstNames
		{
			get
			{
				return this.aggregatedProperties.GetValueOrDefault<IEnumerable<AttributedValue<string>>>(PersonSchema.YomiFirstNames, null);
			}
		}

		// Token: 0x1700118F RID: 4495
		// (get) Token: 0x06003864 RID: 14436 RVA: 0x000E7212 File Offset: 0x000E5412
		public IEnumerable<AttributedValue<string>> YomiLastNames
		{
			get
			{
				return this.aggregatedProperties.GetValueOrDefault<IEnumerable<AttributedValue<string>>>(PersonSchema.YomiLastNames, null);
			}
		}

		// Token: 0x17001190 RID: 4496
		// (get) Token: 0x06003865 RID: 14437 RVA: 0x000E7225 File Offset: 0x000E5425
		public IEnumerable<AttributedValue<PhoneNumber>> BusinessPhoneNumbers
		{
			get
			{
				return this.aggregatedProperties.GetValueOrDefault<IEnumerable<AttributedValue<PhoneNumber>>>(PersonSchema.BusinessPhoneNumbers, null);
			}
		}

		// Token: 0x17001191 RID: 4497
		// (get) Token: 0x06003866 RID: 14438 RVA: 0x000E7238 File Offset: 0x000E5438
		public IEnumerable<AttributedValue<PhoneNumber>> BusinessPhoneNumbers2
		{
			get
			{
				return this.aggregatedProperties.GetValueOrDefault<IEnumerable<AttributedValue<PhoneNumber>>>(PersonSchema.BusinessPhoneNumbers2, null);
			}
		}

		// Token: 0x17001192 RID: 4498
		// (get) Token: 0x06003867 RID: 14439 RVA: 0x000E724B File Offset: 0x000E544B
		public IEnumerable<AttributedValue<PhoneNumber>> HomePhones
		{
			get
			{
				return this.aggregatedProperties.GetValueOrDefault<IEnumerable<AttributedValue<PhoneNumber>>>(PersonSchema.HomePhones, null);
			}
		}

		// Token: 0x17001193 RID: 4499
		// (get) Token: 0x06003868 RID: 14440 RVA: 0x000E725E File Offset: 0x000E545E
		public IEnumerable<AttributedValue<PhoneNumber>> HomePhones2
		{
			get
			{
				return this.aggregatedProperties.GetValueOrDefault<IEnumerable<AttributedValue<PhoneNumber>>>(PersonSchema.HomePhones2, null);
			}
		}

		// Token: 0x17001194 RID: 4500
		// (get) Token: 0x06003869 RID: 14441 RVA: 0x000E7271 File Offset: 0x000E5471
		public IEnumerable<AttributedValue<PhoneNumber>> MobilePhones
		{
			get
			{
				return this.aggregatedProperties.GetValueOrDefault<IEnumerable<AttributedValue<PhoneNumber>>>(PersonSchema.MobilePhones, null);
			}
		}

		// Token: 0x17001195 RID: 4501
		// (get) Token: 0x0600386A RID: 14442 RVA: 0x000E7284 File Offset: 0x000E5484
		public IEnumerable<AttributedValue<PhoneNumber>> MobilePhones2
		{
			get
			{
				return this.aggregatedProperties.GetValueOrDefault<IEnumerable<AttributedValue<PhoneNumber>>>(PersonSchema.MobilePhones2, null);
			}
		}

		// Token: 0x17001196 RID: 4502
		// (get) Token: 0x0600386B RID: 14443 RVA: 0x000E7297 File Offset: 0x000E5497
		public IEnumerable<AttributedValue<PhoneNumber>> AssistantPhoneNumbers
		{
			get
			{
				return this.aggregatedProperties.GetValueOrDefault<IEnumerable<AttributedValue<PhoneNumber>>>(PersonSchema.AssistantPhoneNumbers, null);
			}
		}

		// Token: 0x17001197 RID: 4503
		// (get) Token: 0x0600386C RID: 14444 RVA: 0x000E72AA File Offset: 0x000E54AA
		public IEnumerable<AttributedValue<PhoneNumber>> CallbackPhones
		{
			get
			{
				return this.aggregatedProperties.GetValueOrDefault<IEnumerable<AttributedValue<PhoneNumber>>>(PersonSchema.CallbackPhones, null);
			}
		}

		// Token: 0x17001198 RID: 4504
		// (get) Token: 0x0600386D RID: 14445 RVA: 0x000E72BD File Offset: 0x000E54BD
		public IEnumerable<AttributedValue<PhoneNumber>> CarPhones
		{
			get
			{
				return this.aggregatedProperties.GetValueOrDefault<IEnumerable<AttributedValue<PhoneNumber>>>(PersonSchema.CarPhones, null);
			}
		}

		// Token: 0x17001199 RID: 4505
		// (get) Token: 0x0600386E RID: 14446 RVA: 0x000E72D0 File Offset: 0x000E54D0
		public IEnumerable<AttributedValue<PhoneNumber>> HomeFaxes
		{
			get
			{
				return this.aggregatedProperties.GetValueOrDefault<IEnumerable<AttributedValue<PhoneNumber>>>(PersonSchema.HomeFaxes, null);
			}
		}

		// Token: 0x1700119A RID: 4506
		// (get) Token: 0x0600386F RID: 14447 RVA: 0x000E72E3 File Offset: 0x000E54E3
		public IEnumerable<AttributedValue<PhoneNumber>> OrganizationMainPhones
		{
			get
			{
				return this.aggregatedProperties.GetValueOrDefault<IEnumerable<AttributedValue<PhoneNumber>>>(PersonSchema.OrganizationMainPhones, null);
			}
		}

		// Token: 0x1700119B RID: 4507
		// (get) Token: 0x06003870 RID: 14448 RVA: 0x000E72F6 File Offset: 0x000E54F6
		public IEnumerable<AttributedValue<PhoneNumber>> OtherFaxes
		{
			get
			{
				return this.aggregatedProperties.GetValueOrDefault<IEnumerable<AttributedValue<PhoneNumber>>>(PersonSchema.OtherFaxes, null);
			}
		}

		// Token: 0x1700119C RID: 4508
		// (get) Token: 0x06003871 RID: 14449 RVA: 0x000E7309 File Offset: 0x000E5509
		public IEnumerable<AttributedValue<PhoneNumber>> OtherTelephones
		{
			get
			{
				return this.aggregatedProperties.GetValueOrDefault<IEnumerable<AttributedValue<PhoneNumber>>>(PersonSchema.OtherTelephones, null);
			}
		}

		// Token: 0x1700119D RID: 4509
		// (get) Token: 0x06003872 RID: 14450 RVA: 0x000E731C File Offset: 0x000E551C
		public IEnumerable<AttributedValue<PhoneNumber>> OtherPhones2
		{
			get
			{
				return this.aggregatedProperties.GetValueOrDefault<IEnumerable<AttributedValue<PhoneNumber>>>(PersonSchema.OtherPhones2, null);
			}
		}

		// Token: 0x1700119E RID: 4510
		// (get) Token: 0x06003873 RID: 14451 RVA: 0x000E732F File Offset: 0x000E552F
		public IEnumerable<AttributedValue<PhoneNumber>> Pagers
		{
			get
			{
				return this.aggregatedProperties.GetValueOrDefault<IEnumerable<AttributedValue<PhoneNumber>>>(PersonSchema.Pagers, null);
			}
		}

		// Token: 0x1700119F RID: 4511
		// (get) Token: 0x06003874 RID: 14452 RVA: 0x000E7342 File Offset: 0x000E5542
		public IEnumerable<AttributedValue<PhoneNumber>> RadioPhones
		{
			get
			{
				return this.aggregatedProperties.GetValueOrDefault<IEnumerable<AttributedValue<PhoneNumber>>>(PersonSchema.RadioPhones, null);
			}
		}

		// Token: 0x170011A0 RID: 4512
		// (get) Token: 0x06003875 RID: 14453 RVA: 0x000E7355 File Offset: 0x000E5555
		public IEnumerable<AttributedValue<PhoneNumber>> TelexNumbers
		{
			get
			{
				return this.aggregatedProperties.GetValueOrDefault<IEnumerable<AttributedValue<PhoneNumber>>>(PersonSchema.TelexNumbers, null);
			}
		}

		// Token: 0x170011A1 RID: 4513
		// (get) Token: 0x06003876 RID: 14454 RVA: 0x000E7368 File Offset: 0x000E5568
		public IEnumerable<AttributedValue<PhoneNumber>> TTYTDDPhoneNumbers
		{
			get
			{
				return this.aggregatedProperties.GetValueOrDefault<IEnumerable<AttributedValue<PhoneNumber>>>(PersonSchema.TtyTddPhoneNumbers, null);
			}
		}

		// Token: 0x170011A2 RID: 4514
		// (get) Token: 0x06003877 RID: 14455 RVA: 0x000E737B File Offset: 0x000E557B
		public IEnumerable<AttributedValue<PhoneNumber>> WorkFaxes
		{
			get
			{
				return this.aggregatedProperties.GetValueOrDefault<IEnumerable<AttributedValue<PhoneNumber>>>(PersonSchema.WorkFaxes, null);
			}
		}

		// Token: 0x170011A3 RID: 4515
		// (get) Token: 0x06003878 RID: 14456 RVA: 0x000E738E File Offset: 0x000E558E
		public IEnumerable<AttributedValue<Participant>> Emails1
		{
			get
			{
				return this.aggregatedProperties.GetValueOrDefault<IEnumerable<AttributedValue<Participant>>>(PersonSchema.Emails1, null);
			}
		}

		// Token: 0x170011A4 RID: 4516
		// (get) Token: 0x06003879 RID: 14457 RVA: 0x000E73A1 File Offset: 0x000E55A1
		public IEnumerable<AttributedValue<Participant>> Emails2
		{
			get
			{
				return this.aggregatedProperties.GetValueOrDefault<IEnumerable<AttributedValue<Participant>>>(PersonSchema.Emails2, null);
			}
		}

		// Token: 0x170011A5 RID: 4517
		// (get) Token: 0x0600387A RID: 14458 RVA: 0x000E73B4 File Offset: 0x000E55B4
		public IEnumerable<AttributedValue<Participant>> Emails3
		{
			get
			{
				return this.aggregatedProperties.GetValueOrDefault<IEnumerable<AttributedValue<Participant>>>(PersonSchema.Emails3, null);
			}
		}

		// Token: 0x170011A6 RID: 4518
		// (get) Token: 0x0600387B RID: 14459 RVA: 0x000E73C7 File Offset: 0x000E55C7
		public IEnumerable<AttributedValue<string>> BusinessHomePages
		{
			get
			{
				return this.aggregatedProperties.GetValueOrDefault<IEnumerable<AttributedValue<string>>>(PersonSchema.BusinessHomePages, null);
			}
		}

		// Token: 0x170011A7 RID: 4519
		// (get) Token: 0x0600387C RID: 14460 RVA: 0x000E73DA File Offset: 0x000E55DA
		public IEnumerable<AttributedValue<string>> Schools
		{
			get
			{
				return this.aggregatedProperties.GetValueOrDefault<IEnumerable<AttributedValue<string>>>(PersonSchema.Schools, null);
			}
		}

		// Token: 0x170011A8 RID: 4520
		// (get) Token: 0x0600387D RID: 14461 RVA: 0x000E73ED File Offset: 0x000E55ED
		public IEnumerable<AttributedValue<string>> PersonalHomePages
		{
			get
			{
				return this.aggregatedProperties.GetValueOrDefault<IEnumerable<AttributedValue<string>>>(PersonSchema.PersonalHomePages, null);
			}
		}

		// Token: 0x170011A9 RID: 4521
		// (get) Token: 0x0600387E RID: 14462 RVA: 0x000E7400 File Offset: 0x000E5600
		public IEnumerable<AttributedValue<string>> OfficeLocations
		{
			get
			{
				return this.aggregatedProperties.GetValueOrDefault<IEnumerable<AttributedValue<string>>>(PersonSchema.OfficeLocations, null);
			}
		}

		// Token: 0x170011AA RID: 4522
		// (get) Token: 0x0600387F RID: 14463 RVA: 0x000E7413 File Offset: 0x000E5613
		public IEnumerable<AttributedValue<string>> ImAddresses
		{
			get
			{
				return this.aggregatedProperties.GetValueOrDefault<IEnumerable<AttributedValue<string>>>(PersonSchema.IMAddresses, null);
			}
		}

		// Token: 0x170011AB RID: 4523
		// (get) Token: 0x06003880 RID: 14464 RVA: 0x000E7426 File Offset: 0x000E5626
		public IEnumerable<AttributedValue<string>> ImAddresses2
		{
			get
			{
				return this.aggregatedProperties.GetValueOrDefault<IEnumerable<AttributedValue<string>>>(PersonSchema.IMAddresses2, null);
			}
		}

		// Token: 0x170011AC RID: 4524
		// (get) Token: 0x06003881 RID: 14465 RVA: 0x000E7439 File Offset: 0x000E5639
		public IEnumerable<AttributedValue<string>> ImAddresses3
		{
			get
			{
				return this.aggregatedProperties.GetValueOrDefault<IEnumerable<AttributedValue<string>>>(PersonSchema.IMAddresses3, null);
			}
		}

		// Token: 0x170011AD RID: 4525
		// (get) Token: 0x06003882 RID: 14466 RVA: 0x000E744C File Offset: 0x000E564C
		public IEnumerable<AttributedValue<PostalAddress>> BusinessAddresses
		{
			get
			{
				return this.aggregatedProperties.GetValueOrDefault<IEnumerable<AttributedValue<PostalAddress>>>(PersonSchema.BusinessAddresses, null);
			}
		}

		// Token: 0x170011AE RID: 4526
		// (get) Token: 0x06003883 RID: 14467 RVA: 0x000E745F File Offset: 0x000E565F
		public IEnumerable<AttributedValue<PostalAddress>> HomeAddresses
		{
			get
			{
				return this.aggregatedProperties.GetValueOrDefault<IEnumerable<AttributedValue<PostalAddress>>>(PersonSchema.HomeAddresses, null);
			}
		}

		// Token: 0x170011AF RID: 4527
		// (get) Token: 0x06003884 RID: 14468 RVA: 0x000E7472 File Offset: 0x000E5672
		public IEnumerable<AttributedValue<PostalAddress>> OtherAddresses
		{
			get
			{
				return this.aggregatedProperties.GetValueOrDefault<IEnumerable<AttributedValue<PostalAddress>>>(PersonSchema.OtherAddresses, null);
			}
		}

		// Token: 0x170011B0 RID: 4528
		// (get) Token: 0x06003885 RID: 14469 RVA: 0x000E7485 File Offset: 0x000E5685
		public IEnumerable<AttributedValue<string>> Titles
		{
			get
			{
				return this.aggregatedProperties.GetValueOrDefault<IEnumerable<AttributedValue<string>>>(PersonSchema.Titles, null);
			}
		}

		// Token: 0x170011B1 RID: 4529
		// (get) Token: 0x06003886 RID: 14470 RVA: 0x000E7498 File Offset: 0x000E5698
		public IEnumerable<AttributedValue<string>> Departments
		{
			get
			{
				return this.aggregatedProperties.GetValueOrDefault<IEnumerable<AttributedValue<string>>>(PersonSchema.Departments, null);
			}
		}

		// Token: 0x170011B2 RID: 4530
		// (get) Token: 0x06003887 RID: 14471 RVA: 0x000E74AB File Offset: 0x000E56AB
		public IEnumerable<AttributedValue<string>> CompanyNames
		{
			get
			{
				return this.aggregatedProperties.GetValueOrDefault<IEnumerable<AttributedValue<string>>>(PersonSchema.CompanyNames, null);
			}
		}

		// Token: 0x170011B3 RID: 4531
		// (get) Token: 0x06003888 RID: 14472 RVA: 0x000E74BE File Offset: 0x000E56BE
		public IEnumerable<AttributedValue<string>> Managers
		{
			get
			{
				return this.aggregatedProperties.GetValueOrDefault<IEnumerable<AttributedValue<string>>>(PersonSchema.Managers, null);
			}
		}

		// Token: 0x170011B4 RID: 4532
		// (get) Token: 0x06003889 RID: 14473 RVA: 0x000E74D1 File Offset: 0x000E56D1
		public IEnumerable<AttributedValue<string>> AssistantNames
		{
			get
			{
				return this.aggregatedProperties.GetValueOrDefault<IEnumerable<AttributedValue<string>>>(PersonSchema.AssistantNames, null);
			}
		}

		// Token: 0x170011B5 RID: 4533
		// (get) Token: 0x0600388A RID: 14474 RVA: 0x000E74E4 File Offset: 0x000E56E4
		public IEnumerable<AttributedValue<string>> Professions
		{
			get
			{
				return this.aggregatedProperties.GetValueOrDefault<IEnumerable<AttributedValue<string>>>(PersonSchema.Professions, null);
			}
		}

		// Token: 0x170011B6 RID: 4534
		// (get) Token: 0x0600388B RID: 14475 RVA: 0x000E74F7 File Offset: 0x000E56F7
		public IEnumerable<AttributedValue<string>> SpouseNames
		{
			get
			{
				return this.aggregatedProperties.GetValueOrDefault<IEnumerable<AttributedValue<string>>>(PersonSchema.SpouseNames, null);
			}
		}

		// Token: 0x170011B7 RID: 4535
		// (get) Token: 0x0600388C RID: 14476 RVA: 0x000E750A File Offset: 0x000E570A
		public IEnumerable<AttributedValue<string[]>> Children
		{
			get
			{
				return this.aggregatedProperties.GetValueOrDefault<IEnumerable<AttributedValue<string[]>>>(PersonSchema.Children, null);
			}
		}

		// Token: 0x170011B8 RID: 4536
		// (get) Token: 0x0600388D RID: 14477 RVA: 0x000E751D File Offset: 0x000E571D
		public IEnumerable<AttributedValue<string>> Hobbies
		{
			get
			{
				return this.aggregatedProperties.GetValueOrDefault<IEnumerable<AttributedValue<string>>>(PersonSchema.Hobbies, null);
			}
		}

		// Token: 0x170011B9 RID: 4537
		// (get) Token: 0x0600388E RID: 14478 RVA: 0x000E7530 File Offset: 0x000E5730
		public IEnumerable<AttributedValue<ExDateTime>> WeddingAnniversaries
		{
			get
			{
				return this.aggregatedProperties.GetValueOrDefault<IEnumerable<AttributedValue<ExDateTime>>>(PersonSchema.WeddingAnniversaries, null);
			}
		}

		// Token: 0x170011BA RID: 4538
		// (get) Token: 0x0600388F RID: 14479 RVA: 0x000E7543 File Offset: 0x000E5743
		public IEnumerable<AttributedValue<ExDateTime>> Birthdays
		{
			get
			{
				return this.aggregatedProperties.GetValueOrDefault<IEnumerable<AttributedValue<ExDateTime>>>(PersonSchema.Birthdays, null);
			}
		}

		// Token: 0x170011BB RID: 4539
		// (get) Token: 0x06003890 RID: 14480 RVA: 0x000E7556 File Offset: 0x000E5756
		public IEnumerable<AttributedValue<ExDateTime>> WeddingAnniversariesLocal
		{
			get
			{
				return this.aggregatedProperties.GetValueOrDefault<IEnumerable<AttributedValue<ExDateTime>>>(PersonSchema.WeddingAnniversariesLocal, null);
			}
		}

		// Token: 0x170011BC RID: 4540
		// (get) Token: 0x06003891 RID: 14481 RVA: 0x000E7569 File Offset: 0x000E5769
		public IEnumerable<AttributedValue<ExDateTime>> BirthdaysLocal
		{
			get
			{
				return this.aggregatedProperties.GetValueOrDefault<IEnumerable<AttributedValue<ExDateTime>>>(PersonSchema.BirthdaysLocal, null);
			}
		}

		// Token: 0x170011BD RID: 4541
		// (get) Token: 0x06003892 RID: 14482 RVA: 0x000E757C File Offset: 0x000E577C
		public IEnumerable<AttributedValue<string>> Locations
		{
			get
			{
				return this.aggregatedProperties.GetValueOrDefault<IEnumerable<AttributedValue<string>>>(PersonSchema.Locations, null);
			}
		}

		// Token: 0x170011BE RID: 4542
		// (get) Token: 0x06003893 RID: 14483 RVA: 0x000E758F File Offset: 0x000E578F
		public IEnumerable<AttributedValue<ContactExtendedPropertyData>> ExtendedProperties
		{
			get
			{
				return this.aggregatedProperties.GetValueOrDefault<IEnumerable<AttributedValue<ContactExtendedPropertyData>>>(PersonSchema.ExtendedProperties, null);
			}
		}

		// Token: 0x170011BF RID: 4543
		// (get) Token: 0x06003894 RID: 14484 RVA: 0x000E75A2 File Offset: 0x000E57A2
		public IEnumerable<AttributedValue<string>> ThirdPartyPhotoUrls
		{
			get
			{
				return this.aggregatedProperties.GetValueOrDefault<IEnumerable<AttributedValue<string>>>(PersonSchema.ThirdPartyPhotoUrls, null);
			}
		}

		// Token: 0x170011C0 RID: 4544
		// (get) Token: 0x06003895 RID: 14485 RVA: 0x000E75C0 File Offset: 0x000E57C0
		public string PreferredThirdPartyPhotoUrl
		{
			get
			{
				IEnumerable<AttributedValue<string>> thirdPartyPhotoUrls = this.ThirdPartyPhotoUrls;
				if (thirdPartyPhotoUrls == null)
				{
					return string.Empty;
				}
				return (from attributedUrl in thirdPartyPhotoUrls
				select attributedUrl.Value).FirstOrDefault<string>() ?? string.Empty;
			}
		}

		// Token: 0x06003896 RID: 14486 RVA: 0x000E7610 File Offset: 0x000E5810
		public IEnumerable<PersonId> GetSuggestions()
		{
			return Person.GetSuggestions((MailboxSession)this.storeSession, this.contacts);
		}

		// Token: 0x06003897 RID: 14487 RVA: 0x000E7638 File Offset: 0x000E5838
		private static List<IStorePropertyBag> GetContactsWithPersonId(StoreSession session, PersonId personId, PropertyDefinition[] propertiesToLoad, StoreId folderId)
		{
			List<IStorePropertyBag> result;
			if (session.IsPublicFolderSession)
			{
				if (folderId == null)
				{
					throw new LocalizedException(ServerStrings.NeedFolderIdForPublicFolder);
				}
				result = Person.LoadContactsFromPublicFolder((PublicFolderSession)session, personId, folderId, propertiesToLoad);
			}
			else
			{
				AllPersonContactsEnumerator contactsEnumerator = AllPersonContactsEnumerator.Create((MailboxSession)session, personId, propertiesToLoad);
				result = Person.LoadFromEnumerator(contactsEnumerator);
			}
			return result;
		}

		// Token: 0x06003898 RID: 14488 RVA: 0x000E7684 File Offset: 0x000E5884
		private static IEnumerable<PersonId> GetSuggestions(MailboxSession mailboxSession, IList<IStorePropertyBag> contacts)
		{
			Person.Tracer.TraceDebug(0L, "Person.GetSuggestions: Entering");
			IList<ContactInfoForSuggestion> personContacts = ContactInfoForSuggestion.ConvertAll(contacts);
			IEnumerable<ContactInfoForSuggestion> contactsEnumerator = ContactInfoForSuggestion.GetContactsEnumerator(mailboxSession);
			IList<ContactLinkingSuggestion> suggestions = ContactLinkingSuggestion.GetSuggestions(mailboxSession.Culture, personContacts, contactsEnumerator);
			List<PersonId> list = new List<PersonId>(suggestions.Count);
			foreach (ContactLinkingSuggestion contactLinkingSuggestion in suggestions)
			{
				list.Add(contactLinkingSuggestion.PersonId);
			}
			Person.Tracer.TraceDebug<int>(0L, "Person.GetSuggestions: Exiting (returning {0} suggestions)", list.Count);
			return list;
		}

		// Token: 0x06003899 RID: 14489 RVA: 0x000E772C File Offset: 0x000E592C
		private static bool IsEmptyValue(object value)
		{
			if (value == null)
			{
				return true;
			}
			if (value is PropertyError)
			{
				return PropertyError.IsPropertyNotFound(value);
			}
			return value is string && string.IsNullOrEmpty(value as string);
		}

		// Token: 0x0600389A RID: 14490 RVA: 0x000E7758 File Offset: 0x000E5958
		private static PropertyDefinition GetValidatedPropertyDefinition(PropertyDefinition propertyDefinition, Dictionary<string, PropertyDefinition> propertiesMap)
		{
			if (propertyDefinition == null)
			{
				throw new ObjectNotFoundException(ServerStrings.ExGetPropsFailed);
			}
			PropertyDefinition result = null;
			if (!propertiesMap.TryGetValue(propertyDefinition.Name, out result))
			{
				result = propertyDefinition;
			}
			return result;
		}

		// Token: 0x0600389B RID: 14491 RVA: 0x000E7788 File Offset: 0x000E5988
		private static bool IsValidContactForUpdate(IStorePropertyBag contact, PropertyDefinition propertyDefinition, object oldValue, int contactNumber, Dictionary<string, int> propertyToContact, out bool isCurrentValueEmpty)
		{
			isCurrentValueEmpty = false;
			if (!Person.IsContactWriteable(contact))
			{
				return false;
			}
			object obj = contact.TryGetProperty(propertyDefinition);
			isCurrentValueEmpty = Person.IsEmptyValue(obj);
			if (isCurrentValueEmpty && Person.IsEmptyValue(oldValue))
			{
				return propertyToContact[propertyDefinition.Name] == contactNumber;
			}
			if (isCurrentValueEmpty != Person.IsEmptyValue(oldValue))
			{
				return false;
			}
			if (oldValue is string && obj is string)
			{
				string text = oldValue.ToString().ToLower();
				string value = obj.ToString().ToLower();
				return text.Equals(value);
			}
			return oldValue.Equals(obj);
		}

		// Token: 0x0600389C RID: 14492 RVA: 0x000E7818 File Offset: 0x000E5A18
		private static bool FindEmptyPropertyContact(IEnumerable<IStorePropertyBag> writableContacts, PropertyDefinition propertyDefinition, out IStorePropertyBag result)
		{
			result = null;
			foreach (IStorePropertyBag storePropertyBag in writableContacts)
			{
				object value = storePropertyBag.TryGetProperty(propertyDefinition);
				if (Person.IsEmptyValue(value))
				{
					result = storePropertyBag;
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600389D RID: 14493 RVA: 0x000E7878 File Offset: 0x000E5A78
		private static bool IsValidForBodyUpdate(Body body, object oldValue, out bool isCurrentValueEmpty)
		{
			Util.ThrowOnNullArgument(oldValue, "oldValue");
			Util.ThrowOnNullArgument(body, "body");
			bool result = false;
			isCurrentValueEmpty = false;
			PersonNotes personNotes = Person.ReadPersonNotes(body, Person.MaxNotesBytes);
			if (personNotes == null)
			{
				if (Person.IsEmptyValue(oldValue))
				{
					isCurrentValueEmpty = true;
					return true;
				}
				return false;
			}
			else
			{
				if (personNotes.IsTruncated)
				{
					return false;
				}
				if (personNotes.NotesBody != null && (string)oldValue == personNotes.NotesBody)
				{
					result = true;
				}
				if (Person.IsEmptyValue(personNotes.NotesBody) && Person.IsEmptyValue(oldValue))
				{
					isCurrentValueEmpty = true;
				}
				return result;
			}
		}

		// Token: 0x0600389E RID: 14494 RVA: 0x000E7900 File Offset: 0x000E5B00
		private static PersonNotes ReadPersonNotes(Body contactBody, int maxBytesToReadFromStore)
		{
			bool isTruncated;
			string text;
			using (TextReader textReader = contactBody.OpenTextReader(BodyFormat.TextPlain))
			{
				long size = contactBody.Size;
				if (size < (long)maxBytesToReadFromStore)
				{
					isTruncated = false;
					text = textReader.ReadToEnd();
				}
				else
				{
					isTruncated = true;
					char[] array = new char[maxBytesToReadFromStore];
					textReader.Read(array, 0, maxBytesToReadFromStore);
					text = new string(array);
				}
				text = text.TrimEnd(Person.BodyTrimChars);
			}
			PersonNotes result = null;
			if (!string.IsNullOrWhiteSpace(text))
			{
				result = new PersonNotes(text, isTruncated);
			}
			return result;
		}

		// Token: 0x0600389F RID: 14495 RVA: 0x000E7988 File Offset: 0x000E5B88
		private static void AddToContactChanges(Dictionary<VersionedId, List<StoreObjectPropertyChange>> contactChanges, VersionedId id, StoreObjectPropertyChange change)
		{
			List<StoreObjectPropertyChange> list;
			if (contactChanges.ContainsKey(id))
			{
				list = contactChanges[id];
			}
			else
			{
				list = new List<StoreObjectPropertyChange>();
				contactChanges.Add(id, list);
			}
			list.Add(change);
		}

		// Token: 0x060038A0 RID: 14496 RVA: 0x000E79C0 File Offset: 0x000E5BC0
		private static PersonId UpdateContacts(StoreSession session, Dictionary<VersionedId, List<StoreObjectPropertyChange>> contactChanges, PersonId personIdOfContact)
		{
			Person.Tracer.TraceDebug(0L, "Person.UpdateContacts: Entering");
			PersonId result = null;
			bool flag = false;
			bool flag2 = false;
			List<StoreObjectPropertyChange> list = new List<StoreObjectPropertyChange>();
			foreach (VersionedId versionedId in contactChanges.Keys)
			{
				PropertyDefinition[] changedProperties = Person.GetChangedProperties(contactChanges[versionedId]);
				using (Contact contact = Item.Bind(session, versionedId, ItemBindOption.None, changedProperties) as Contact)
				{
					bool flag3 = false;
					List<StoreObjectPropertyChange> list2 = new List<StoreObjectPropertyChange>();
					foreach (StoreObjectPropertyChange storeObjectPropertyChange in contactChanges[versionedId])
					{
						if (storeObjectPropertyChange.PropertyDefinition.Name == ItemSchema.RtfBody.Name)
						{
							flag = true;
							bool flag4 = false;
							if (!flag3 && !Person.IsPropertyChangeInList(list, storeObjectPropertyChange) && Person.IsValidForBodyUpdate(contact.Body, storeObjectPropertyChange.OldValue, out flag4))
							{
								list2.Add(storeObjectPropertyChange);
								flag2 = true;
								if (flag4)
								{
									flag3 = flag4;
									list.Add(storeObjectPropertyChange);
								}
							}
						}
						else
						{
							list2.Add(storeObjectPropertyChange);
						}
					}
					result = Person.ApplyContactChangesAndSave(session, contact, list2);
				}
			}
			if (flag && !flag2)
			{
				throw new SaveConflictException(ServerStrings.ExSaveFailedBecauseOfConflicts(personIdOfContact));
			}
			Person.Tracer.TraceDebug(0L, "Person.UpdateContacts: Exiting");
			return result;
		}

		// Token: 0x060038A1 RID: 14497 RVA: 0x000E7B78 File Offset: 0x000E5D78
		private static PersonId UpdateGroup(StoreSession session, Dictionary<VersionedId, List<StoreObjectPropertyChange>> groupChanges, PersonId personIdOfGroup)
		{
			Person.Tracer.TraceDebug(0L, "Person.UpdateGroup: Entering");
			PersonId result = null;
			bool flag = false;
			bool flag2 = false;
			foreach (VersionedId versionedId in groupChanges.Keys)
			{
				PropertyDefinition[] changedProperties = Person.GetChangedProperties(groupChanges[versionedId]);
				using (DistributionList distributionList = Item.Bind(session, versionedId, ItemBindOption.None, changedProperties) as DistributionList)
				{
					bool flag3 = false;
					List<StoreObjectPropertyChange> list = new List<StoreObjectPropertyChange>();
					foreach (StoreObjectPropertyChange storeObjectPropertyChange in groupChanges[versionedId])
					{
						if (storeObjectPropertyChange.PropertyDefinition.Name == ItemSchema.RtfBody.Name)
						{
							flag = true;
							bool flag4 = false;
							if (!flag3 && Person.IsValidForBodyUpdate(distributionList.Body, storeObjectPropertyChange.OldValue, out flag4))
							{
								list.Add(storeObjectPropertyChange);
								flag2 = true;
								if (flag4)
								{
									flag3 = flag4;
								}
							}
						}
						else
						{
							list.Add(storeObjectPropertyChange);
						}
					}
					result = Person.ApplyGroupChangesAndSave(distributionList, list);
				}
			}
			if (flag && !flag2)
			{
				throw new SaveConflictException(ServerStrings.ExSaveFailedBecauseOfConflicts(personIdOfGroup));
			}
			Person.Tracer.TraceDebug(0L, "Person.UpdateGroup: Exiting");
			return result;
		}

		// Token: 0x060038A2 RID: 14498 RVA: 0x000E7CEC File Offset: 0x000E5EEC
		private static bool IsPropertyChangeInList(List<StoreObjectPropertyChange> changeList, StoreObjectPropertyChange changeToCheck)
		{
			foreach (StoreObjectPropertyChange storeObjectPropertyChange in changeList)
			{
				if (storeObjectPropertyChange.PropertyDefinition.Name == changeToCheck.PropertyDefinition.Name && storeObjectPropertyChange.OldValue == changeToCheck.OldValue && storeObjectPropertyChange.NewValue == changeToCheck.NewValue)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060038A3 RID: 14499 RVA: 0x000E7D74 File Offset: 0x000E5F74
		private static PersonId ApplyContactChangesAndSave(StoreSession session, Contact contact, ICollection<StoreObjectPropertyChange> changes)
		{
			Person.Tracer.TraceDebug(0L, "Person.ApplyContactChangesAndSave: Entering");
			string valueOrDefault = contact.GetValueOrDefault<string>(ContactSchema.Email1EmailAddress, string.Empty);
			string valueOrDefault2 = contact.GetValueOrDefault<string>(ContactSchema.Email2EmailAddress, string.Empty);
			string valueOrDefault3 = contact.GetValueOrDefault<string>(ContactSchema.Email3EmailAddress, string.Empty);
			foreach (StoreObjectPropertyChange storeObjectPropertyChange in changes)
			{
				if (!storeObjectPropertyChange.IsPropertyValidated)
				{
					storeObjectPropertyChange.PropertyDefinition = Person.GetValidatedPropertyDefinition(storeObjectPropertyChange.PropertyDefinition, Person.personPropertiesToContactPropertiesMap);
					storeObjectPropertyChange.IsPropertyValidated = true;
				}
				if (Person.IsEmptyValue(storeObjectPropertyChange.NewValue))
				{
					contact.Delete(storeObjectPropertyChange.PropertyDefinition);
				}
				else if (storeObjectPropertyChange.PropertyDefinition.Type == typeof(ExDateTime))
				{
					if (storeObjectPropertyChange.PropertyDefinition.Name == ContactSchema.BirthdayLocal.Name || storeObjectPropertyChange.PropertyDefinition.Name == ContactSchema.WeddingAnniversaryLocal.Name)
					{
						contact[storeObjectPropertyChange.PropertyDefinition] = storeObjectPropertyChange.NewValue;
						ExDateTime exDateTime = ExDateTime.Parse(session.ExTimeZone, storeObjectPropertyChange.NewValue.ToString());
						if (storeObjectPropertyChange.PropertyDefinition.Name == ContactSchema.BirthdayLocal.Name)
						{
							contact[ContactSchema.Birthday] = exDateTime;
						}
						if (storeObjectPropertyChange.PropertyDefinition.Name == ContactSchema.WeddingAnniversaryLocal.Name)
						{
							contact[ContactSchema.WeddingAnniversary] = exDateTime;
						}
					}
					else
					{
						contact[storeObjectPropertyChange.PropertyDefinition] = ExDateTime.Parse(storeObjectPropertyChange.NewValue.ToString());
					}
				}
				else
				{
					if (storeObjectPropertyChange.PropertyDefinition.Name == PersonSchema.Bodies.Name || storeObjectPropertyChange.PropertyDefinition.Name == ItemSchema.RtfBody.Name)
					{
						Body body = contact.Body;
						Util.ThrowOnNullArgument(body, "contactBody");
						using (TextWriter textWriter = body.OpenTextWriter(BodyFormat.TextPlain))
						{
							textWriter.Write(storeObjectPropertyChange.NewValue);
							continue;
						}
					}
					contact[storeObjectPropertyChange.PropertyDefinition] = storeObjectPropertyChange.NewValue;
				}
			}
			Person.SetEmailAddressRelatedFields(contact, valueOrDefault, valueOrDefault2, valueOrDefault3);
			ConflictResolutionResult conflictResolutionResult = contact.Save(SaveMode.ResolveConflicts);
			if (conflictResolutionResult.SaveStatus == SaveResult.IrresolvableConflict)
			{
				throw new SaveConflictException(ServerStrings.ExSaveFailedBecauseOfConflicts(contact.ParentId));
			}
			contact.Load(Person.LoadPropertiesAfterSave);
			PersonId valueOrDefault4 = contact.GetValueOrDefault<PersonId>(ContactSchema.PersonId, null);
			Person.Tracer.TraceDebug(0L, "Person.ApplyContactChangesAndSave: Exiting");
			return valueOrDefault4;
		}

		// Token: 0x060038A4 RID: 14500 RVA: 0x000E8058 File Offset: 0x000E6258
		private static PersonId ApplyGroupChangesAndSave(DistributionList group, ICollection<StoreObjectPropertyChange> changes)
		{
			Person.Tracer.TraceDebug(0L, "Person.ApplyGroupChangesAndSave: Entering");
			foreach (StoreObjectPropertyChange storeObjectPropertyChange in changes)
			{
				if (!storeObjectPropertyChange.IsPropertyValidated)
				{
					storeObjectPropertyChange.PropertyDefinition = Person.GetValidatedPropertyDefinition(storeObjectPropertyChange.PropertyDefinition, Person.personPropertiesToGroupPropertiesMap);
					storeObjectPropertyChange.IsPropertyValidated = true;
				}
				if (storeObjectPropertyChange.PropertyDefinition.Name == PersonSchema.Members.Name)
				{
					if (!Person.IsEmptyValue(storeObjectPropertyChange.NewValue) && Person.IsEmptyValue(storeObjectPropertyChange.OldValue))
					{
						Participant participant = (Participant)storeObjectPropertyChange.NewValue;
						bool flag = false;
						foreach (DistributionListMember distributionListMember in group)
						{
							Participant participant2 = distributionListMember.Participant;
							if (participant2 != null && participant2.AreAddressesEqual(participant))
							{
								flag = true;
								break;
							}
						}
						if (!flag)
						{
							group.Add(participant);
							continue;
						}
						continue;
					}
					else
					{
						if (!Person.IsEmptyValue(storeObjectPropertyChange.NewValue) || Person.IsEmptyValue(storeObjectPropertyChange.OldValue))
						{
							continue;
						}
						Participant v = (Participant)storeObjectPropertyChange.OldValue;
						using (IEnumerator<DistributionListMember> enumerator3 = group.GetEnumerator())
						{
							while (enumerator3.MoveNext())
							{
								DistributionListMember distributionListMember2 = enumerator3.Current;
								Participant participant3 = distributionListMember2.Participant;
								if (participant3 != null && participant3.AreAddressesEqual(v))
								{
									group.Remove(distributionListMember2);
									break;
								}
							}
							continue;
						}
					}
				}
				if (Person.IsEmptyValue(storeObjectPropertyChange.NewValue))
				{
					group.Delete(storeObjectPropertyChange.PropertyDefinition);
				}
				else
				{
					if (storeObjectPropertyChange.PropertyDefinition.Name == PersonSchema.Bodies.Name || storeObjectPropertyChange.PropertyDefinition.Name == ItemSchema.RtfBody.Name)
					{
						Body body = group.Body;
						Util.ThrowOnNullArgument(body, "groupBody");
						using (TextWriter textWriter = body.OpenTextWriter(BodyFormat.TextPlain))
						{
							textWriter.Write(storeObjectPropertyChange.NewValue);
							continue;
						}
					}
					group[storeObjectPropertyChange.PropertyDefinition] = storeObjectPropertyChange.NewValue;
				}
			}
			ConflictResolutionResult conflictResolutionResult = group.Save(SaveMode.ResolveConflicts);
			if (conflictResolutionResult.SaveStatus == SaveResult.IrresolvableConflict)
			{
				throw new SaveConflictException(ServerStrings.ExSaveFailedBecauseOfConflicts(group.ParentId));
			}
			group.Load(Person.LoadPropertiesAfterSave);
			PersonId valueOrDefault = group.GetValueOrDefault<PersonId>(ContactSchema.PersonId, null);
			Person.Tracer.TraceDebug(0L, "Person.ApplyGroupChangesAndSave: Exiting");
			return valueOrDefault;
		}

		// Token: 0x060038A5 RID: 14501 RVA: 0x000E8348 File Offset: 0x000E6548
		private static void SetEmailAddressRelatedFields(Contact contact, string oldEmail1EmailAddress, string oldEmail2EmailAddress, string oldEmail3EmailAddress)
		{
			Person.AdjustEmailAddressRelatedProperties(contact, EmailAddressProperties.Email1, oldEmail1EmailAddress);
			Person.AdjustEmailAddressRelatedProperties(contact, EmailAddressProperties.Email2, oldEmail2EmailAddress);
			Person.AdjustEmailAddressRelatedProperties(contact, EmailAddressProperties.Email3, oldEmail3EmailAddress);
		}

		// Token: 0x060038A6 RID: 14502 RVA: 0x000E8370 File Offset: 0x000E6570
		private static void AdjustEmailAddressRelatedProperties(Contact contact, EmailAddressProperties emailAddressProperties, string oldEmailEmailAddress)
		{
			string valueOrDefault = contact.GetValueOrDefault<string>(emailAddressProperties.Address, null);
			if (!string.IsNullOrEmpty(valueOrDefault))
			{
				string valueOrDefault2 = contact.GetValueOrDefault<string>(emailAddressProperties.DisplayName, null);
				if (valueOrDefault2 == null || valueOrDefault2.Equals(oldEmailEmailAddress))
				{
					contact[emailAddressProperties.DisplayName] = valueOrDefault;
				}
				Participant participant;
				if (Participant.TryParse(valueOrDefault, out participant))
				{
					if (participant.RoutingType != null)
					{
						contact[emailAddressProperties.RoutingType] = participant.RoutingType;
					}
					else
					{
						contact[emailAddressProperties.OriginalDisplayName] = participant.OriginalDisplayName;
						contact[emailAddressProperties.RoutingType] = string.Empty;
						contact[emailAddressProperties.Address] = string.Empty;
					}
					ParticipantEntryId participantEntryId = ParticipantEntryId.FromParticipant(participant, ParticipantEntryIdConsumer.ContactEmailSlot);
					if (participantEntryId == null)
					{
						contact.Delete(emailAddressProperties.OriginalEntryId);
						return;
					}
					contact[emailAddressProperties.OriginalEntryId] = participantEntryId.ToByteArray();
				}
			}
		}

		// Token: 0x060038A7 RID: 14503 RVA: 0x000E8444 File Offset: 0x000E6644
		private static PropertyDefinition[] GetChangedProperties(List<StoreObjectPropertyChange> changes)
		{
			HashSet<PropertyDefinition> hashSet = new HashSet<PropertyDefinition>();
			foreach (StoreObjectPropertyChange storeObjectPropertyChange in changes)
			{
				if (!hashSet.Contains(storeObjectPropertyChange.PropertyDefinition))
				{
					hashSet.Add(storeObjectPropertyChange.PropertyDefinition);
				}
			}
			return hashSet.ToArray<PropertyDefinition>();
		}

		// Token: 0x060038A8 RID: 14504 RVA: 0x000E84B4 File Offset: 0x000E66B4
		private static bool IsContactFromExternalNetwork(IStorePropertyBag contact)
		{
			if (contact == null)
			{
				return false;
			}
			string valueOrDefault = contact.GetValueOrDefault<string>(ContactSchema.PartnerNetworkId, string.Empty);
			return !string.Equals(valueOrDefault, WellKnownNetworkNames.RecipientCache, StringComparison.OrdinalIgnoreCase) && !string.Equals(valueOrDefault, WellKnownNetworkNames.QuickContacts, StringComparison.OrdinalIgnoreCase) && !string.IsNullOrEmpty(valueOrDefault);
		}

		// Token: 0x060038A9 RID: 14505 RVA: 0x000E8500 File Offset: 0x000E6700
		private static bool IsContactWriteable(IStorePropertyBag contact)
		{
			if (contact == null)
			{
				return false;
			}
			string valueOrDefault = contact.GetValueOrDefault<string>(ContactSchema.PartnerNetworkId, string.Empty);
			return string.IsNullOrEmpty(valueOrDefault);
		}

		// Token: 0x060038AA RID: 14506 RVA: 0x000E852C File Offset: 0x000E672C
		private static bool CanContactBeDeleted(IStorePropertyBag contact, StoreId deleteInFolder)
		{
			bool flag = Person.IsContactFromExternalNetwork(contact);
			if (deleteInFolder == null)
			{
				return !flag;
			}
			return !flag && Person.BelongsToFolder(contact, deleteInFolder);
		}

		// Token: 0x060038AB RID: 14507 RVA: 0x000E8554 File Offset: 0x000E6754
		private static bool BelongsToFolder(IStorePropertyBag contact, StoreId targetFolder)
		{
			if (contact == null || targetFolder == null)
			{
				return false;
			}
			StoreObjectId valueOrDefault = contact.GetValueOrDefault<StoreObjectId>(StoreObjectSchema.ParentItemId, null);
			return valueOrDefault != null && valueOrDefault.Equals(targetFolder);
		}

		// Token: 0x060038AC RID: 14508 RVA: 0x000E8588 File Offset: 0x000E6788
		private static List<IStorePropertyBag> LoadFromEnumerator(AllPersonContactsEnumerator contactsEnumerator)
		{
			List<IStorePropertyBag> list = new List<IStorePropertyBag>(12);
			foreach (IStorePropertyBag item in contactsEnumerator)
			{
				list.Add(item);
			}
			return list;
		}

		// Token: 0x060038AD RID: 14509 RVA: 0x000E8604 File Offset: 0x000E6804
		private static PropertyDefinition[] GetPropertiesToLoad(ICollection<PropertyDefinition> properties, PropertyDefinition[] extendedProperties)
		{
			if (properties != null)
			{
				if (properties.Any((PropertyDefinition property) => !PersonSchema.Instance.AllProperties.Contains(property)))
				{
					throw new ArgumentException("properties must be from PersonSchema", "properties");
				}
			}
			if (extendedProperties != null)
			{
				if (extendedProperties.Any((PropertyDefinition property) => PersonSchema.Instance.AllProperties.Contains(property)))
				{
					throw new ArgumentException("extendedProperties cannot be from PersonSchema", "extendedProperties");
				}
			}
			return PropertyDefinitionCollection.Merge<PropertyDefinition>(new IEnumerable<PropertyDefinition>[]
			{
				Person.RequiredProperties,
				properties,
				extendedProperties
			});
		}

		// Token: 0x060038AE RID: 14510 RVA: 0x000E86A0 File Offset: 0x000E68A0
		private static HashSet<PropertyDefinition> GetContactPropertiesToLoad(ICollection<PropertyDefinition> personProperties, IEnumerable<PropertyDefinition> additionalContactProperties)
		{
			HashSet<PropertyDefinition> hashSet = new HashSet<PropertyDefinition>();
			foreach (PropertyDefinition propertyDefinition in personProperties)
			{
				ApplicationAggregatedProperty applicationAggregatedProperty = propertyDefinition as ApplicationAggregatedProperty;
				if (applicationAggregatedProperty != null)
				{
					foreach (PropertyDependency propertyDependency in applicationAggregatedProperty.Dependencies)
					{
						hashSet.Add(propertyDependency.Property);
					}
				}
			}
			if (additionalContactProperties != null)
			{
				foreach (PropertyDefinition item in additionalContactProperties)
				{
					hashSet.Add(item);
				}
			}
			return hashSet;
		}

		// Token: 0x060038AF RID: 14511 RVA: 0x000E8768 File Offset: 0x000E6968
		private static VersionedId GetVersionedIdForPropertyBag(IStorePropertyBag propertyBag)
		{
			VersionedId valueOrDefault = propertyBag.GetValueOrDefault<VersionedId>(ItemSchema.Id, null);
			if (valueOrDefault == null)
			{
				throw new ObjectNotFoundException(ServerStrings.ExInvalidItemId);
			}
			return valueOrDefault;
		}

		// Token: 0x060038B0 RID: 14512 RVA: 0x000E87EC File Offset: 0x000E69EC
		private void CheckAndAddNamePropertiesForNewContact(ICollection<StoreObjectPropertyChange> propertyChanges)
		{
			IEnumerable<StoreObjectPropertyChange> source = from c in propertyChanges
			where (c.PropertyDefinition.Name == ContactSchema.GivenName.Name || c.PropertyDefinition.Name == ContactSchema.Surname.Name) && !Person.IsEmptyValue(c.NewValue)
			select c;
			if (source.Any<StoreObjectPropertyChange>())
			{
				return;
			}
			if (!Person.IsEmptyValue(this.GivenName))
			{
				propertyChanges.Add(new StoreObjectPropertyChange(ContactSchema.GivenName, null, this.GivenName, true));
			}
			if (!Person.IsEmptyValue(this.Surname))
			{
				propertyChanges.Add(new StoreObjectPropertyChange(ContactSchema.Surname, null, this.Surname, true));
			}
		}

		// Token: 0x060038B1 RID: 14513 RVA: 0x000E8870 File Offset: 0x000E6A70
		private PersonId CreateContact(StoreSession session, ICollection<StoreObjectPropertyChange> propertyChanges, StoreId parentFolder)
		{
			Person.Tracer.TraceDebug(PersonId.TraceId(this.PersonId), "Person.CreateContact: Entering");
			PersonId result = null;
			this.AddDefaultStoreObjectPropertyChanges(session, propertyChanges, parentFolder);
			using (Contact contact = Contact.Create(session, parentFolder))
			{
				result = Person.ApplyContactChangesAndSave(session, contact, propertyChanges);
			}
			Person.Tracer.TraceDebug(PersonId.TraceId(this.PersonId), "Person.CreateContact: Exiting");
			return result;
		}

		// Token: 0x060038B2 RID: 14514 RVA: 0x000E88EC File Offset: 0x000E6AEC
		private PersonId CreateGroup(StoreSession session, ICollection<StoreObjectPropertyChange> propertyChanges, StoreId parentFolder)
		{
			Person.Tracer.TraceDebug(PersonId.TraceId(this.PersonId), "Person.CreateGroup: Entering");
			this.AddDefaultStoreObjectPropertyChanges(session, propertyChanges, parentFolder);
			PersonId result;
			using (DistributionList distributionList = DistributionList.Create(session, parentFolder))
			{
				result = Person.ApplyGroupChangesAndSave(distributionList, propertyChanges);
			}
			Person.Tracer.TraceDebug(PersonId.TraceId(this.PersonId), "Person.CreateGroup: Exiting");
			return result;
		}

		// Token: 0x060038B3 RID: 14515 RVA: 0x000E8964 File Offset: 0x000E6B64
		private void AddDefaultStoreObjectPropertyChanges(StoreSession session, ICollection<StoreObjectPropertyChange> propertyChanges, StoreId parentFolder)
		{
			propertyChanges.Add(new StoreObjectPropertyChange(ContactSchema.PersonId, null, this.PersonId, true));
			propertyChanges.Add(new StoreObjectPropertyChange(ContactSchema.FileAsId, null, FileAsMapping.LastCommaFirst, true));
			MailboxSession mailboxSession = session as MailboxSession;
			if (mailboxSession != null && mailboxSession.IsDefaultFolderType(parentFolder) == DefaultFolderType.RecipientCache)
			{
				propertyChanges.Add(new StoreObjectPropertyChange(ContactSchema.PartnerNetworkId, null, WellKnownNetworkNames.RecipientCache, true));
				propertyChanges.Add(new StoreObjectPropertyChange(ContactSchema.RelevanceScore, null, 2147483646, true));
			}
		}

		// Token: 0x060038B4 RID: 14516 RVA: 0x000E89F0 File Offset: 0x000E6BF0
		public static IStorePropertyBag InternalGetAggregatedProperties(PersonPropertyAggregationContext aggregationContext, ICollection<PropertyDefinition> requestedProperties, PropertyDefinition[] extendedProperties)
		{
			PersonSchemaProperties personSchemaProperties = new PersonSchemaProperties(extendedProperties, new IEnumerable<PropertyDefinition>[]
			{
				new List<PropertyDefinition>(Person.RequiredProperties),
				requestedProperties
			});
			return ApplicationAggregatedProperty.Aggregate(aggregationContext, personSchemaProperties.All);
		}

		// Token: 0x060038B5 RID: 14517 RVA: 0x000E8A2C File Offset: 0x000E6C2C
		private static void LoadGALDataIfPersonIsGALLinked(MailboxSession session, PersonId personId, ICollection<PropertyDefinition> personProperties, List<IStorePropertyBag> contacts, HashSet<PropertyDefinition> contactProperties)
		{
			Guid valueOrDefault = contacts[0].GetValueOrDefault<Guid>(ContactSchema.GALLinkID, Guid.Empty);
			if (valueOrDefault == Guid.Empty)
			{
				return;
			}
			Person.Tracer.TraceDebug<Guid>(PersonId.TraceId(personId), "Person is GAL Linked, load information from AD - GAL Link ID: {0}.", valueOrDefault);
			ADPersonToContactConverterSet adpersonToContactConverterSet = ADPersonToContactConverterSet.FromContactProperties(personProperties.ToArray<PropertyDefinition>(), contactProperties);
			PropertyDefinition[] adProperties = PropertyDefinitionCollection.Merge<ADPropertyDefinition>(adpersonToContactConverterSet.ADProperties, ContactInfoForLinkingFromDirectory.RequiredADProperties);
			string[] valueOrDefault2 = contacts[0].GetValueOrDefault<string[]>(ContactSchema.SmtpAddressCache, Array<string>.Empty);
			GALLinkingFixer gallinkingFixer = new GALLinkingFixer(session, personId, valueOrDefault, contacts, contactProperties);
			DirectoryPersonSearcher directoryPersonSearcher = new DirectoryPersonSearcher(session.MailboxOwner);
			ADRawEntry adrawEntry = directoryPersonSearcher.FindByAdObjectIdGuidOrSmtpAddressCache(valueOrDefault, valueOrDefault2, adProperties);
			if (adrawEntry == null)
			{
				Person.Tracer.TraceDebug<Guid>(PersonId.TraceId(personId), "No AD contact found by ADObjectId {0} or by SmptAddressCache, it is likely deleted, unlink it from the person.", valueOrDefault);
				gallinkingFixer.TryUnlinkContactsFromGAL();
				return;
			}
			gallinkingFixer.TryUpdateGALLinkingPropertiesIfChanged(adrawEntry);
			IStorePropertyBag storePropertyBag = adpersonToContactConverterSet.Convert(adrawEntry);
			storePropertyBag[ContactSchema.PersonId] = personId;
			contacts.Add(storePropertyBag);
		}

		// Token: 0x060038B6 RID: 14518 RVA: 0x000E8B1C File Offset: 0x000E6D1C
		private static PersonNotes LoadGALNotesIfPersonIsGALLinked(MailboxSession session, PersonId personId, int requestedBytesToFetch, List<IStorePropertyBag> contactPropertyBags)
		{
			Util.ThrowOnNullArgument(contactPropertyBags, "contactPropertyBags");
			if (contactPropertyBags.Count == 0)
			{
				return null;
			}
			Guid valueOrDefault = contactPropertyBags[0].GetValueOrDefault<Guid>(ContactSchema.GALLinkID, Guid.Empty);
			if (valueOrDefault == Guid.Empty)
			{
				return null;
			}
			Person.Tracer.TraceDebug<Guid>(PersonId.TraceId(personId), "Person is GAL Linked, load notes from AD - GAL Link ID: {0}.", valueOrDefault);
			DirectoryPersonSearcher directoryPersonSearcher = new DirectoryPersonSearcher(session.MailboxOwner);
			ADRecipient adrecipient = directoryPersonSearcher.FindADRecipientByObjectId(valueOrDefault);
			if (adrecipient == null)
			{
				return null;
			}
			IADOrgPerson iadorgPerson = adrecipient as IADOrgPerson;
			if (iadorgPerson == null || string.IsNullOrWhiteSpace(iadorgPerson.Notes))
			{
				return null;
			}
			PersonNotes result;
			if (iadorgPerson.Notes.Length > requestedBytesToFetch)
			{
				result = new PersonNotes(iadorgPerson.Notes.Substring(0, requestedBytesToFetch), true);
			}
			else
			{
				result = new PersonNotes(iadorgPerson.Notes, false);
			}
			return result;
		}

		// Token: 0x060038B7 RID: 14519 RVA: 0x000E8BE4 File Offset: 0x000E6DE4
		private static IStorePropertyBag ConvertADRawEntryToContact(ADRawEntry adRawEntry, PersonId personId, ICollection<PropertyDefinition> properties)
		{
			ADPersonToContactConverterSet adpersonToContactConverterSet = ADPersonToContactConverterSet.FromPersonProperties(properties.ToArray<PropertyDefinition>(), null);
			IStorePropertyBag storePropertyBag = adpersonToContactConverterSet.Convert(adRawEntry);
			storePropertyBag[ContactSchema.PersonId] = personId;
			return storePropertyBag;
		}

		// Token: 0x060038C0 RID: 14528 RVA: 0x000E8C14 File Offset: 0x000E6E14
		// Note: this type is marked as 'beforefieldinit'.
		static Person()
		{
			char[] bodyTrimChars = new char[1];
			Person.BodyTrimChars = bodyTrimChars;
			Person.LoadPropertiesAfterSave = new StorePropertyDefinition[]
			{
				ContactSchema.PersonId
			};
			Person.PropertiesToLoadForNotes = new PropertyDefinition[]
			{
				ContactSchema.PersonId,
				ItemSchema.Id,
				ItemSchema.RtfBody,
				PersonSchema.Attributions
			};
			Person.SimpleVirtualPersonaBodiesProperty = new SimpleVirtualPropertyDefinition("InternalStorage:" + PersonSchema.Bodies.Name, PersonSchema.Bodies.Type, PersonSchema.Bodies.PropertyFlags, new PropertyDefinitionConstraint[0]);
			Person.RequiredProperties = new PropertyDefinition[]
			{
				PersonSchema.Id,
				PersonSchema.Attributions
			};
			Person.PersonIdProperty = new PropertyDefinition[]
			{
				ContactSchema.PersonId
			};
			Person.personPropertiesToContactPropertiesMap = new Dictionary<string, PropertyDefinition>(StringComparer.OrdinalIgnoreCase)
			{
				{
					PersonSchema.DisplayName.Name,
					StoreObjectSchema.DisplayName
				},
				{
					PersonSchema.GivenName.Name,
					ContactSchema.GivenName
				},
				{
					PersonSchema.Surname.Name,
					ContactSchema.Surname
				},
				{
					PersonSchema.CompanyName.Name,
					ContactSchema.CompanyName
				},
				{
					PersonSchema.FileAs.Name,
					ContactBaseSchema.FileAs
				}
			};
			Person.personPropertiesToGroupPropertiesMap = new Dictionary<string, PropertyDefinition>(StringComparer.OrdinalIgnoreCase)
			{
				{
					PersonSchema.DisplayName.Name,
					StoreObjectSchema.DisplayName
				}
			};
			Person.GALAggregationRequiredStoreProperties = ContactInfoForLinking.Properties;
		}

		// Token: 0x04001DFE RID: 7678
		public const int CommonContactsPerPerson = 12;

		// Token: 0x04001DFF RID: 7679
		public const int RelevanceScoreForIrrelevantEntries = 2147483647;

		// Token: 0x04001E00 RID: 7680
		private static readonly Trace Tracer = ExTraceGlobals.PersonTracer;

		// Token: 0x04001E01 RID: 7681
		public static readonly PropertyDefinition[] SuggestionProperties = ContactInfoForSuggestion.Properties;

		// Token: 0x04001E02 RID: 7682
		private static int MaxNotesBytes = 2097152;

		// Token: 0x04001E03 RID: 7683
		private static readonly char[] BodyTrimChars;

		// Token: 0x04001E04 RID: 7684
		private static readonly StorePropertyDefinition[] LoadPropertiesAfterSave;

		// Token: 0x04001E05 RID: 7685
		private static readonly PropertyDefinition[] PropertiesToLoadForNotes;

		// Token: 0x04001E06 RID: 7686
		private static readonly SimpleVirtualPropertyDefinition SimpleVirtualPersonaBodiesProperty;

		// Token: 0x04001E07 RID: 7687
		public static readonly PropertyDefinition[] RequiredProperties;

		// Token: 0x04001E08 RID: 7688
		private static readonly PropertyDefinition[] PersonIdProperty;

		// Token: 0x04001E09 RID: 7689
		private static readonly Dictionary<string, PropertyDefinition> personPropertiesToContactPropertiesMap;

		// Token: 0x04001E0A RID: 7690
		private static readonly Dictionary<string, PropertyDefinition> personPropertiesToGroupPropertiesMap;

		// Token: 0x04001E0B RID: 7691
		private readonly List<IStorePropertyBag> contacts;

		// Token: 0x04001E0C RID: 7692
		private readonly IStorePropertyBag aggregatedProperties;

		// Token: 0x04001E0D RID: 7693
		private readonly PersonId personId;

		// Token: 0x04001E0E RID: 7694
		private readonly StoreSession storeSession;

		// Token: 0x04001E0F RID: 7695
		private static readonly PropertyDefinition[] GALAggregationRequiredStoreProperties;
	}
}
