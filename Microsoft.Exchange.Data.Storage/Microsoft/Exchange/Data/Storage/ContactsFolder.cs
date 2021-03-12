using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200047B RID: 1147
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ContactsFolder : Folder, IStorePropertyBag, IPropertyBag, IReadOnlyPropertyBag, IDisposable
	{
		// Token: 0x06003327 RID: 13095 RVA: 0x000CFEF0 File Offset: 0x000CE0F0
		internal ContactsFolder(CoreFolder coreFolder) : base(coreFolder)
		{
			if (base.IsNew)
			{
				this.Initialize();
			}
		}

		// Token: 0x06003328 RID: 13096 RVA: 0x000CFF07 File Offset: 0x000CE107
		public static ContactsFolder Create(StoreSession session, StoreId parentId)
		{
			return (ContactsFolder)Folder.Create(session, parentId, StoreObjectType.ContactsFolder);
		}

		// Token: 0x06003329 RID: 13097 RVA: 0x000CFF16 File Offset: 0x000CE116
		public new static ContactsFolder Create(StoreSession session, StoreId parentFolderId, StoreObjectType folderType)
		{
			EnumValidator.ThrowIfInvalid<StoreObjectType>(folderType, StoreObjectType.ContactsFolder);
			return ContactsFolder.Create(session, parentFolderId);
		}

		// Token: 0x0600332A RID: 13098 RVA: 0x000CFF26 File Offset: 0x000CE126
		public new static ContactsFolder Create(StoreSession session, StoreId parentFolderId, StoreObjectType folderType, string displayName, CreateMode createMode)
		{
			EnumValidator.ThrowIfInvalid<CreateMode>(createMode, "createMode");
			EnumValidator.ThrowIfInvalid<StoreObjectType>(folderType, StoreObjectType.ContactsFolder);
			return (ContactsFolder)Folder.Create(session, parentFolderId, StoreObjectType.ContactsFolder, displayName, createMode);
		}

		// Token: 0x0600332B RID: 13099 RVA: 0x000CFF4B File Offset: 0x000CE14B
		public new static ContactsFolder Bind(StoreSession session, StoreId folderId)
		{
			return ContactsFolder.Bind(session, folderId, null);
		}

		// Token: 0x0600332C RID: 13100 RVA: 0x000CFF55 File Offset: 0x000CE155
		public new static ContactsFolder Bind(StoreSession session, StoreId folderId, ICollection<PropertyDefinition> prefetchProperties)
		{
			prefetchProperties = InternalSchema.Combine<PropertyDefinition>(FolderSchema.Instance.AutoloadProperties, prefetchProperties);
			return Folder.InternalBind<ContactsFolder>(session, folderId, prefetchProperties);
		}

		// Token: 0x0600332D RID: 13101 RVA: 0x000CFF71 File Offset: 0x000CE171
		public static ContactsFolder Bind(MailboxSession session, DefaultFolderType defaultFolderType)
		{
			return ContactsFolder.Bind(session, defaultFolderType, null);
		}

		// Token: 0x0600332E RID: 13102 RVA: 0x000CFF7C File Offset: 0x000CE17C
		public static ContactsFolder Bind(MailboxSession session, DefaultFolderType defaultFolderType, ICollection<PropertyDefinition> prefetchProperties)
		{
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			EnumValidator.ThrowIfInvalid<DefaultFolderType>(defaultFolderType, DefaultFolderType.Contacts);
			StoreObjectId defaultFolderId = session.GetDefaultFolderId(defaultFolderType);
			if (defaultFolderId == null)
			{
				throw new ObjectNotFoundException(ServerStrings.ExDefaultFolderNotFound(defaultFolderType));
			}
			return ContactsFolder.Bind(session, defaultFolderId, prefetchProperties);
		}

		// Token: 0x0600332F RID: 13103 RVA: 0x000CFFC2 File Offset: 0x000CE1C2
		public override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<ContactsFolder>(this);
		}

		// Token: 0x06003330 RID: 13104 RVA: 0x000CFFCC File Offset: 0x000CE1CC
		public object[][] ResolveAmbiguousNameView(string ambiguousName, int limit, SortBy[] sortBy, params PropertyDefinition[] propsToReturn)
		{
			this.CheckDisposed("ResolveAmbiguousNameView");
			if (propsToReturn == null)
			{
				throw new ArgumentNullException("propsToReturn");
			}
			if (propsToReturn.Length <= 0)
			{
				throw new ArgumentException("propsToReturn contains no properties.");
			}
			if (!this.IsValidAmbiguousName(ambiguousName))
			{
				throw new ArgumentException("ambiguousName is invalid ambiguous name");
			}
			object[][] result;
			using (ForwardOnlyFilteredReader forwardOnlyFilteredReader = new ContactsFolder.AnrContactsReader(this, ambiguousName, sortBy, propsToReturn))
			{
				result = this.FetchFromReader("ResolveAmbiguousNameView", forwardOnlyFilteredReader, ContactsFolder.NormalizeLimit(limit, StorageLimits.Instance.AmbiguousNamesViewResultsLimit));
			}
			return result;
		}

		// Token: 0x06003331 RID: 13105 RVA: 0x000D0060 File Offset: 0x000CE260
		public bool IsValidAmbiguousName(string ambiguousName)
		{
			this.CheckDisposed("IsValidAmbiguousName");
			if (ambiguousName == null)
			{
				throw new ArgumentNullException("ambiguousName");
			}
			if (ambiguousName.Length == 0)
			{
				throw new ArgumentException("ambiguousName");
			}
			return new ContactsFolder.AnrCriteria(ambiguousName, base.Session.InternalPreferedCulture).IsValid;
		}

		// Token: 0x06003332 RID: 13106 RVA: 0x000D00B0 File Offset: 0x000CE2B0
		public object[][] FindSomeoneView(string ambiguousName, int limit, SortBy[] sortBy, params PropertyDefinition[] propsToReturn)
		{
			this.CheckDisposed("FindSomeoneView");
			if (propsToReturn == null)
			{
				throw new ArgumentNullException("propsToReturn");
			}
			if (propsToReturn.Length <= 0)
			{
				throw new ArgumentException("propsToReturn contains no properties");
			}
			if (!this.IsValidAmbiguousName(ambiguousName))
			{
				throw new ArgumentException("ambiguousName is invalid ambiguous name");
			}
			object[][] result;
			using (ForwardOnlyFilteredReader forwardOnlyFilteredReader = new ContactsFolder.FindSomeoneContactsReader(this, ambiguousName, sortBy, propsToReturn))
			{
				result = this.FetchFromReader("FindSomeone", forwardOnlyFilteredReader, ContactsFolder.NormalizeLimit(limit, StorageLimits.Instance.AmbiguousNamesViewResultsLimit));
			}
			return result;
		}

		// Token: 0x06003333 RID: 13107 RVA: 0x000D0144 File Offset: 0x000CE344
		public object[][] FindNamesView(IDictionary<PropertyDefinition, object> dictionary, int limit, SortBy[] sortBy, params PropertyDefinition[] propsToReturn)
		{
			this.CheckDisposed("FindNamesView");
			if (propsToReturn == null)
			{
				throw new ArgumentNullException("propsToReturn");
			}
			if (propsToReturn.Length <= 0)
			{
				throw new ArgumentException("propsToReturn contains no properties");
			}
			object[][] result;
			using (ForwardOnlyFilteredReader forwardOnlyFilteredReader = new ContactsFolder.FindNamesContactsReader(this, dictionary ?? new Dictionary<PropertyDefinition, object>(), sortBy, propsToReturn))
			{
				result = this.FetchFromReader("FindNamesView", forwardOnlyFilteredReader, ContactsFolder.NormalizeLimit(limit, StorageLimits.Instance.FindNamesViewResultsLimit));
			}
			return result;
		}

		// Token: 0x06003334 RID: 13108 RVA: 0x000D01CC File Offset: 0x000CE3CC
		public FindInfo<Contact> FindByEmailAddress(string emailAddress, params PropertyDefinition[] prefetchProperties)
		{
			this.CheckDisposed("FindByEmailAddress");
			if (emailAddress == null)
			{
				throw new ArgumentNullException("emailAddress");
			}
			IDictionary<PropertyDefinition, object> dictionary = new SortedDictionary<PropertyDefinition, object>();
			dictionary.Add(InternalSchema.EmailAddress, emailAddress);
			for (uint num = 3U; num > 0U; num -= 1U)
			{
				object[][] array = this.FindNamesView(dictionary, 2, new SortBy[]
				{
					new SortBy(InternalSchema.DisplayName, SortOrder.Ascending)
				}, new PropertyDefinition[]
				{
					InternalSchema.ItemId
				});
				if (array.Length > 0)
				{
					try
					{
						return new FindInfo<Contact>((array.Length > 1) ? FindStatus.FoundMultiple : FindStatus.Found, Contact.Bind(base.Session, (VersionedId)array[0][0], prefetchProperties));
					}
					catch (ObjectNotFoundException)
					{
						ExTraceGlobals.StorageTracer.Information<string>((long)this.GetHashCode(), "ContactsFolder::FindByEmailAddress. Race condition: an matching contact was deleted before we had a chance to bind to it, emailAddress=\"{0}\"", emailAddress);
						if (array.Length == 1)
						{
							break;
						}
					}
				}
			}
			return new FindInfo<Contact>(FindStatus.NotFound, null);
		}

		// Token: 0x06003335 RID: 13109 RVA: 0x000D02B4 File Offset: 0x000CE4B4
		private static int NormalizeLimit(int limit, int maxLimit)
		{
			if (limit == 0 || limit == -1 || limit > maxLimit)
			{
				return maxLimit;
			}
			if (limit > 0)
			{
				return limit;
			}
			throw new ArgumentOutOfRangeException("limit");
		}

		// Token: 0x06003336 RID: 13110 RVA: 0x000D02D4 File Offset: 0x000CE4D4
		private object[][] FetchFromReader(string callingMethod, ForwardOnlyFilteredReader reader, int rowsToFetch)
		{
			uint num = 3U;
			object[][] nextAsView;
			for (;;)
			{
				try
				{
					nextAsView = reader.GetNextAsView(rowsToFetch);
					break;
				}
				catch (ObjectNotFoundException)
				{
					ExTraceGlobals.StorageTracer.Information<string, uint>((long)this.GetHashCode(), "ContactsFolder::{0}. Race condition: a matching contact was deleted between the two iteration phases of a filter, attemptsLeft=\"{1}\"", callingMethod, num);
					if (num == 0U)
					{
						throw;
					}
				}
				num -= 1U;
			}
			return nextAsView;
		}

		// Token: 0x06003337 RID: 13111 RVA: 0x000D0324 File Offset: 0x000CE524
		private void Initialize()
		{
			this[InternalSchema.ContainerClass] = "IPF.Contact";
		}

		// Token: 0x04001B9D RID: 7069
		internal const uint NumberOfRetriesInARaceCondition = 2U;

		// Token: 0x04001B9E RID: 7070
		internal const float PrefetchModeThreshold = 2.1474836E+09f;

		// Token: 0x02000485 RID: 1157
		private class AnrContactsReader : ForwardOnlyFilteredReader
		{
			// Token: 0x06003365 RID: 13157 RVA: 0x000D0CFC File Offset: 0x000CEEFC
			internal AnrContactsReader(Folder contacts, string ambiguousName, SortBy[] sortBy, params PropertyDefinition[] propertiesToReturn) : base(ContactsFolder.AnrContactsReader.GetPropertySets(propertiesToReturn), (float)propertiesToReturn.Length / (float)ContactsFolder.AnrCriteria.AnrProperties.Count < 2.1474836E+09f)
			{
				this.contacts = contacts;
				this.filterCriteria = new Predicate<PropertyBag>(new ContactsFolder.AnrCriteria(ambiguousName, contacts.Session.InternalPreferedCulture).IsMatch);
				this.sortBy = sortBy;
				this.unfilteredPropertyBag = new QueryResultPropertyBag(this.contacts.Session, base.PropertySets.GetMergedSet());
			}

			// Token: 0x06003366 RID: 13158 RVA: 0x000D0D7F File Offset: 0x000CEF7F
			public override DisposeTracker GetDisposeTracker()
			{
				return DisposeTracker.Get<ContactsFolder.AnrContactsReader>(this);
			}

			// Token: 0x06003367 RID: 13159 RVA: 0x000D0DE0 File Offset: 0x000CEFE0
			internal override object[][] GetNextAsView(int rowcount)
			{
				byte[] lastEntryId = null;
				return this.GetNextAsView(delegate(object[] transformedForFilterRow)
				{
					if (transformedForFilterRow != null)
					{
						byte[] array = (byte[])transformedForFilterRow[0];
						bool result = rowcount-- > 0 || Util.ValueEquals(lastEntryId, array);
						lastEntryId = array;
						return result;
					}
					return rowcount > 0;
				});
			}

			// Token: 0x06003368 RID: 13160 RVA: 0x000D0E14 File Offset: 0x000CF014
			protected override object[][] TransformRow(object[] unfilteredRow)
			{
				this.unfilteredPropertyBag.SetQueryResultRow(unfilteredRow);
				List<Participant> list = new List<Participant>();
				string valueOrDefault = this.unfilteredPropertyBag.GetValueOrDefault<string>(InternalSchema.ItemClass);
				if (ObjectClass.IsDistributionList(valueOrDefault))
				{
					list.Add(Microsoft.Exchange.Data.Storage.PropertyBag.CheckPropertyValue<Participant>(InternalSchema.DistributionListParticipant, base.PropertySets.TryGetProperty(unfilteredRow, InternalSchema.DistributionListParticipant), null));
				}
				else if (ObjectClass.IsContact(valueOrDefault))
				{
					foreach (ContactEmailSlotParticipantProperty contactEmailSlotParticipantProperty in ContactEmailSlotParticipantProperty.AllInstances.Values)
					{
						list.Add(Microsoft.Exchange.Data.Storage.PropertyBag.CheckPropertyValue<Participant>(contactEmailSlotParticipantProperty, base.PropertySets.TryGetProperty(unfilteredRow, contactEmailSlotParticipantProperty), null));
					}
				}
				List<object[]> list2 = new List<object[]>();
				foreach (Participant participant in list)
				{
					if (!(participant == null) && !(participant.IsRoutable(this.contacts.Session) == false))
					{
						object[] array = (object[])unfilteredRow.Clone();
						foreach (ContactEmailSlotParticipantProperty contactEmailSlotParticipantProperty2 in ContactEmailSlotParticipantProperty.AllInstances.Values)
						{
							base.PropertySets.DeleteProperty(array, contactEmailSlotParticipantProperty2);
							foreach (NativeStorePropertyDefinition propertyDefinition in contactEmailSlotParticipantProperty2.EmailSlotProperties)
							{
								base.PropertySets.DeleteProperty(array, propertyDefinition);
							}
						}
						base.PropertySets.SetProperty(array, InternalSchema.AnrViewParticipant, participant);
						base.PropertySets.SetProperty(array, ParticipantSchema.DisplayName, participant.DisplayName);
						base.PropertySets.SetProperty(array, ParticipantSchema.EmailAddress, participant.EmailAddress);
						base.PropertySets.SetProperty(array, ParticipantSchema.RoutingType, participant.RoutingType);
						this.unfilteredPropertyBag.SetQueryResultRow(array);
						if (this.filterCriteria(this.unfilteredPropertyBag))
						{
							list2.Add(array);
						}
					}
				}
				return list2.ToArray();
			}

			// Token: 0x06003369 RID: 13161 RVA: 0x000D10B8 File Offset: 0x000CF2B8
			private static ForwardOnlyFilteredReader.PropertySetMixer GetPropertySets(params PropertyDefinition[] propertiesToReturn)
			{
				List<PropertyDefinition> list = new List<PropertyDefinition>();
				list.Add(ContactsFolder.AnrContactsReader.idProperty);
				list.AddRange(ContactsFolder.AnrCriteria.AnrProperties.Cast<PropertyDefinition>());
				Util.AddRange<PropertyDefinition, ContactEmailSlotParticipantProperty>(list, ContactEmailSlotParticipantProperty.AllInstances.Values);
				list.AddRange(ContactsFolder.AnrContactsReader.additionalProperties);
				ForwardOnlyFilteredReader.PropertySetMixer propertySetMixer = new ForwardOnlyFilteredReader.PropertySetMixer();
				propertySetMixer.AddSet(ForwardOnlyFilteredReader.PropertySet.Identification, new PropertyDefinition[]
				{
					ContactsFolder.AnrContactsReader.idProperty
				});
				propertySetMixer.AddSet(ForwardOnlyFilteredReader.PropertySet.ForFilter, list.ToArray());
				propertySetMixer.AddSet(ForwardOnlyFilteredReader.PropertySet.Requested, propertiesToReturn);
				return propertySetMixer;
			}

			// Token: 0x0600336A RID: 13162 RVA: 0x000D1134 File Offset: 0x000CF334
			protected override QueryResult MakeQuery(params PropertyDefinition[] propertiesToReturn)
			{
				return this.contacts.ItemQuery(ItemQueryType.None, null, this.sortBy, propertiesToReturn);
			}

			// Token: 0x04001BB6 RID: 7094
			private static readonly PropertyDefinition[] additionalProperties = new PropertyDefinition[]
			{
				InternalSchema.DistributionListParticipant,
				InternalSchema.ItemClass
			};

			// Token: 0x04001BB7 RID: 7095
			private static readonly PropertyDefinition idProperty = InternalSchema.EntryId;

			// Token: 0x04001BB8 RID: 7096
			private readonly Folder contacts;

			// Token: 0x04001BB9 RID: 7097
			private readonly Predicate<PropertyBag> filterCriteria;

			// Token: 0x04001BBA RID: 7098
			private readonly SortBy[] sortBy;

			// Token: 0x04001BBB RID: 7099
			private readonly QueryResultPropertyBag unfilteredPropertyBag;
		}

		// Token: 0x02000486 RID: 1158
		private class AnrCriteria
		{
			// Token: 0x0600336C RID: 13164 RVA: 0x000D1180 File Offset: 0x000CF380
			public AnrCriteria(string ambiguousName, CultureInfo culture)
			{
				this.culture = culture;
				this.anrChunks = this.GetAnrChunks(ambiguousName);
			}

			// Token: 0x0600336D RID: 13165 RVA: 0x000D119C File Offset: 0x000CF39C
			private static StorePropertyDefinition[] GetAnrProperties()
			{
				List<StorePropertyDefinition> list = new List<StorePropertyDefinition>(12);
				list.AddRange(new StorePropertyDefinition[]
				{
					StoreObjectSchema.DisplayName,
					ContactSchema.GivenName,
					ContactSchema.Surname,
					ParticipantSchema.DisplayName,
					ParticipantSchema.EmailAddress
				});
				foreach (ContactEmailSlotParticipantProperty contactEmailSlotParticipantProperty in ContactEmailSlotParticipantProperty.AllInstances.Values)
				{
					list.Add(contactEmailSlotParticipantProperty.DisplayNamePropertyDefinition);
					list.Add(contactEmailSlotParticipantProperty.EmailAddressPropertyDefinition);
				}
				return list.ToArray();
			}

			// Token: 0x0600336E RID: 13166 RVA: 0x000D1248 File Offset: 0x000CF448
			public bool IsMatch(PropertyBag propertyBag)
			{
				string[] array = (string[])this.anrChunks.Clone();
				int num = array.Length;
				foreach (StorePropertyDefinition propertyDefinition in ContactsFolder.AnrCriteria.AnrProperties)
				{
					string valueOrDefault = propertyBag.GetValueOrDefault<string>(propertyDefinition);
					if (valueOrDefault != null)
					{
						foreach (string source in this.GetAnrChunks(valueOrDefault))
						{
							for (int j = 0; j < array.Length; j++)
							{
								if (array[j] != null && this.culture.CompareInfo.IsPrefix(source, array[j], CompareOptions.IgnoreCase))
								{
									array[j] = null;
									if (--num == 0)
									{
										return true;
									}
								}
							}
						}
					}
				}
				return false;
			}

			// Token: 0x0600336F RID: 13167 RVA: 0x000D1320 File Offset: 0x000CF520
			private string[] GetAnrChunks(string ambiguousName)
			{
				return ambiguousName.Split(ContactsFolder.AnrCriteria.AnrChunkSeparators, StringSplitOptions.RemoveEmptyEntries);
			}

			// Token: 0x17001011 RID: 4113
			// (get) Token: 0x06003370 RID: 13168 RVA: 0x000D132E File Offset: 0x000CF52E
			public bool IsValid
			{
				get
				{
					return this.anrChunks.Length > 0;
				}
			}

			// Token: 0x04001BBC RID: 7100
			public static readonly char[] AnrChunkSeparators = new char[]
			{
				' ',
				','
			};

			// Token: 0x04001BBD RID: 7101
			public static readonly ReadOnlyCollection<StorePropertyDefinition> AnrProperties = new ReadOnlyCollection<StorePropertyDefinition>(ContactsFolder.AnrCriteria.GetAnrProperties());

			// Token: 0x04001BBE RID: 7102
			private readonly CultureInfo culture;

			// Token: 0x04001BBF RID: 7103
			private readonly string[] anrChunks;
		}

		// Token: 0x02000487 RID: 1159
		private class FindNamesContactsReader : ForwardOnlyFilteredReader
		{
			// Token: 0x06003372 RID: 13170 RVA: 0x000D1370 File Offset: 0x000CF570
			public FindNamesContactsReader(Folder contacts, IDictionary<PropertyDefinition, object> dictionary, SortBy[] sortBy, PropertyDefinition[] propertiesToReturn) : base(ContactsFolder.FindNamesContactsReader.GetPropertySets(dictionary, propertiesToReturn), (float)propertiesToReturn.Length / (float)dictionary.Count < 2.1474836E+09f)
			{
				this.contacts = contacts;
				this.sortBy = sortBy;
				this.culture = contacts.Session.InternalPreferedCulture;
				this.expectedValues = Util.CollectionToArray<object>(dictionary.Values);
				this.forFilterProperties = base.PropertySets.GetSet(ForwardOnlyFilteredReader.PropertySet.ForFilter);
			}

			// Token: 0x06003373 RID: 13171 RVA: 0x000D13E1 File Offset: 0x000CF5E1
			public override DisposeTracker GetDisposeTracker()
			{
				return DisposeTracker.Get<ContactsFolder.FindNamesContactsReader>(this);
			}

			// Token: 0x06003374 RID: 13172 RVA: 0x000D13EC File Offset: 0x000CF5EC
			protected override bool EvaluateFilterCriteria(object[] forFilterRow)
			{
				bool[] array = new bool[3];
				for (int i = 0; i < this.expectedValues.Length; i++)
				{
					if (ContactsFolder.FindNamesContactsReader.emailProperties.ContainsKey(this.forFilterProperties[i]))
					{
						for (int j = 0; j < 3; j++)
						{
							if (!array[j])
							{
								int num = Array.IndexOf<PropertyDefinition>(this.forFilterProperties, ContactsFolder.FindNamesContactsReader.emailProperties[this.forFilterProperties[i]][j]);
								array[j] = !this.IsMatch(forFilterRow[num], this.expectedValues[i]);
							}
						}
					}
					else if (!this.IsMatch(forFilterRow[i], this.expectedValues[i]))
					{
						return false;
					}
				}
				bool[] array2 = array;
				for (int k = 0; k < array2.Length; k++)
				{
					if (!array2[k])
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x06003375 RID: 13173 RVA: 0x000D14B4 File Offset: 0x000CF6B4
			private static ForwardOnlyFilteredReader.PropertySetMixer GetPropertySets(IDictionary<PropertyDefinition, object> dictionary, PropertyDefinition[] propertiesToReturn)
			{
				ForwardOnlyFilteredReader.PropertySetMixer propertySetMixer = new ForwardOnlyFilteredReader.PropertySetMixer();
				propertySetMixer.AddSet(ForwardOnlyFilteredReader.PropertySet.Identification, new PropertyDefinition[]
				{
					InternalSchema.EntryId
				});
				propertySetMixer.AddSet(ForwardOnlyFilteredReader.PropertySet.ForFilter, ContactsFolder.FindNamesContactsReader.GetForFilterSet(dictionary.Keys));
				propertySetMixer.AddSet(ForwardOnlyFilteredReader.PropertySet.Requested, propertiesToReturn);
				return propertySetMixer;
			}

			// Token: 0x06003376 RID: 13174 RVA: 0x000D14FC File Offset: 0x000CF6FC
			private static PropertyDefinition[] GetForFilterSet(IEnumerable<PropertyDefinition> forFilterPropertiesFromConsumer)
			{
				List<PropertyDefinition> list = new List<PropertyDefinition>(forFilterPropertiesFromConsumer);
				foreach (PropertyDefinition key in forFilterPropertiesFromConsumer)
				{
					if (ContactsFolder.FindNamesContactsReader.emailProperties.ContainsKey(key))
					{
						list.AddRange(ContactsFolder.FindNamesContactsReader.emailProperties[key]);
					}
				}
				return list.ToArray();
			}

			// Token: 0x06003377 RID: 13175 RVA: 0x000D1568 File Offset: 0x000CF768
			private bool IsMatch(object actualValue, object expectedValue)
			{
				if (actualValue is string && expectedValue is string)
				{
					return this.culture.CompareInfo.IsPrefix((string)actualValue, (string)expectedValue, CompareOptions.IgnoreCase);
				}
				return object.Equals(actualValue, expectedValue);
			}

			// Token: 0x06003378 RID: 13176 RVA: 0x000D159F File Offset: 0x000CF79F
			protected override QueryResult MakeQuery(params PropertyDefinition[] propertiesToReturn)
			{
				return this.contacts.ItemQuery(ItemQueryType.None, null, this.sortBy, propertiesToReturn);
			}

			// Token: 0x06003379 RID: 13177 RVA: 0x000D15B5 File Offset: 0x000CF7B5
			protected override bool ShouldIntercept(PropertyDefinition property)
			{
				return base.ShouldIntercept(property) || ContactsFolder.FindNamesContactsReader.emailProperties.ContainsKey(property);
			}

			// Token: 0x04001BC0 RID: 7104
			private readonly Folder contacts;

			// Token: 0x04001BC1 RID: 7105
			private readonly CultureInfo culture;

			// Token: 0x04001BC2 RID: 7106
			private static readonly Dictionary<PropertyDefinition, PropertyDefinition[]> emailProperties = Util.AddElements<Dictionary<PropertyDefinition, PropertyDefinition[]>, KeyValuePair<PropertyDefinition, PropertyDefinition[]>>(new Dictionary<PropertyDefinition, PropertyDefinition[]>(), new KeyValuePair<PropertyDefinition, PropertyDefinition[]>[]
			{
				Util.Pair<PropertyDefinition, PropertyDefinition[]>(InternalSchema.EmailDisplayName, new PropertyDefinition[]
				{
					InternalSchema.Email1DisplayName,
					InternalSchema.Email2DisplayName,
					InternalSchema.Email3DisplayName
				}),
				Util.Pair<PropertyDefinition, PropertyDefinition[]>(InternalSchema.EmailAddress, new PropertyDefinition[]
				{
					InternalSchema.Email1EmailAddress,
					InternalSchema.Email2EmailAddress,
					InternalSchema.Email3EmailAddress
				}),
				Util.Pair<PropertyDefinition, PropertyDefinition[]>(InternalSchema.EmailRoutingType, new PropertyDefinition[]
				{
					InternalSchema.Email1AddrType,
					InternalSchema.Email2AddrType,
					InternalSchema.Email3AddrType
				})
			});

			// Token: 0x04001BC3 RID: 7107
			private readonly object[] expectedValues;

			// Token: 0x04001BC4 RID: 7108
			private readonly PropertyDefinition[] forFilterProperties;

			// Token: 0x04001BC5 RID: 7109
			private readonly SortBy[] sortBy;
		}

		// Token: 0x02000488 RID: 1160
		private class FindSomeoneContactsReader : ForwardOnlyFilteredReader
		{
			// Token: 0x0600337B RID: 13179 RVA: 0x000D1698 File Offset: 0x000CF898
			internal FindSomeoneContactsReader(Folder contacts, string ambiguousName, SortBy[] sortBy, params PropertyDefinition[] propertiesToReturn) : base(ContactsFolder.FindSomeoneContactsReader.GetPropertySets(propertiesToReturn), (float)propertiesToReturn.Length / (float)ContactsFolder.AnrCriteria.AnrProperties.Count < 2.1474836E+09f)
			{
				this.contacts = contacts;
				this.filterCriteria = new Predicate<PropertyBag>(new ContactsFolder.AnrCriteria(ambiguousName, contacts.Session.InternalPreferedCulture).IsMatch);
				this.sortBy = sortBy;
				this.forFilterPropertyBag = new QueryResultPropertyBag(this.contacts.Session, base.PropertySets.GetSet(ForwardOnlyFilteredReader.PropertySet.ForFilter));
			}

			// Token: 0x0600337C RID: 13180 RVA: 0x000D171C File Offset: 0x000CF91C
			public override DisposeTracker GetDisposeTracker()
			{
				return DisposeTracker.Get<ContactsFolder.FindSomeoneContactsReader>(this);
			}

			// Token: 0x0600337D RID: 13181 RVA: 0x000D1724 File Offset: 0x000CF924
			protected override bool EvaluateFilterCriteria(object[] forFilterRow)
			{
				this.forFilterPropertyBag.SetQueryResultRow(forFilterRow);
				return this.filterCriteria(this.forFilterPropertyBag);
			}

			// Token: 0x0600337E RID: 13182 RVA: 0x000D1744 File Offset: 0x000CF944
			private static ForwardOnlyFilteredReader.PropertySetMixer GetPropertySets(params PropertyDefinition[] propertiesToReturn)
			{
				ForwardOnlyFilteredReader.PropertySetMixer propertySetMixer = new ForwardOnlyFilteredReader.PropertySetMixer();
				propertySetMixer.AddSet(ForwardOnlyFilteredReader.PropertySet.Identification, new PropertyDefinition[]
				{
					ContactsFolder.FindSomeoneContactsReader.idProperty
				});
				propertySetMixer.AddSet(ForwardOnlyFilteredReader.PropertySet.ForFilter, Util.CollectionToArray<StorePropertyDefinition>(ContactsFolder.AnrCriteria.AnrProperties));
				propertySetMixer.AddSet(ForwardOnlyFilteredReader.PropertySet.Requested, propertiesToReturn);
				return propertySetMixer;
			}

			// Token: 0x0600337F RID: 13183 RVA: 0x000D1788 File Offset: 0x000CF988
			protected override QueryResult MakeQuery(params PropertyDefinition[] propertiesToReturn)
			{
				return this.contacts.ItemQuery(ItemQueryType.None, null, this.sortBy, propertiesToReturn);
			}

			// Token: 0x04001BC6 RID: 7110
			private static readonly PropertyDefinition idProperty = InternalSchema.EntryId;

			// Token: 0x04001BC7 RID: 7111
			private readonly Folder contacts;

			// Token: 0x04001BC8 RID: 7112
			private readonly Predicate<PropertyBag> filterCriteria;

			// Token: 0x04001BC9 RID: 7113
			private readonly SortBy[] sortBy;

			// Token: 0x04001BCA RID: 7114
			private readonly QueryResultPropertyBag forFilterPropertyBag;
		}
	}
}
