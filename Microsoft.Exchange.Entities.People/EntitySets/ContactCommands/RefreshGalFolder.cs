using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Performance;
using Microsoft.Exchange.Entities.DataModel;
using Microsoft.Exchange.Entities.EntitySets;
using Microsoft.Exchange.Entities.EntitySets.Commands;
using Microsoft.Exchange.Entities.People.Converters;
using Microsoft.Exchange.Entities.People.DataProviders;
using Microsoft.Exchange.Entities.People.EntitySets.ResponseTypes;
using Microsoft.Exchange.Entities.People.Utilities;
using Microsoft.Exchange.Entities.TypeConversion.Converters;

namespace Microsoft.Exchange.Entities.People.EntitySets.ContactCommands
{
	// Token: 0x02000005 RID: 5
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class RefreshGalFolder : Command<RefreshGALFolderResponseEntity>, IStorageEntitySetScope<IMailboxSession>
	{
		// Token: 0x06000015 RID: 21 RVA: 0x000024F8 File Offset: 0x000006F8
		public RefreshGalFolder(IMailboxSession mailboxSession, IRecipientSession recipientSession, ITracer tracer, IPerformanceDataLogger perfLogger, IXSOFactory xsoFactory)
		{
			ArgumentValidator.ThrowIfNull("mailboxSession", mailboxSession);
			ArgumentValidator.ThrowIfNull("tracer", tracer);
			ArgumentValidator.ThrowIfNull("recipientSession", recipientSession);
			ArgumentValidator.ThrowIfNull("perfLogger", perfLogger);
			ArgumentValidator.ThrowIfNull("xsoFactory", xsoFactory);
			this.tracer = tracer;
			this.perfLogger = perfLogger;
			this.tracingId = this.GetHashCode();
			this.StoreSession = mailboxSession;
			this.RecipientSession = recipientSession;
			this.XsoFactory = xsoFactory;
			this.IdConverter = IdConverter.Instance;
			this.recipientCacheContactsDataProvider = new ContactDataProvider(this, this.Trace);
			this.recipientCacheContactsDataProvider.FolderInScope = DefaultFolderType.RecipientCache;
			this.recipientCacheContactsDataProvider.ContactProperties = GALContactsFolderSchema.ContactPropertyDefinitions;
			this.contactDataProvider = new ADContactDataProvider(recipientSession, this.Trace);
			this.exceptionList = new List<Exception>();
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000016 RID: 22 RVA: 0x000025D7 File Offset: 0x000007D7
		// (set) Token: 0x06000017 RID: 23 RVA: 0x000025DF File Offset: 0x000007DF
		public IMailboxSession StoreSession { get; private set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000018 RID: 24 RVA: 0x000025E8 File Offset: 0x000007E8
		// (set) Token: 0x06000019 RID: 25 RVA: 0x000025F0 File Offset: 0x000007F0
		public IXSOFactory XsoFactory { get; private set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600001A RID: 26 RVA: 0x000025F9 File Offset: 0x000007F9
		// (set) Token: 0x0600001B RID: 27 RVA: 0x00002601 File Offset: 0x00000801
		public IdConverter IdConverter { get; private set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600001C RID: 28 RVA: 0x0000260A File Offset: 0x0000080A
		// (set) Token: 0x0600001D RID: 29 RVA: 0x00002612 File Offset: 0x00000812
		public IRecipientSession RecipientSession { get; private set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600001E RID: 30 RVA: 0x0000261B File Offset: 0x0000081B
		protected override ITracer Trace
		{
			get
			{
				return this.tracer;
			}
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002623 File Offset: 0x00000823
		public new RefreshGALFolderResponseEntity Execute(CommandContext context)
		{
			return base.Execute(context);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x0000262C File Offset: 0x0000082C
		protected override RefreshGALFolderResponseEntity OnExecute()
		{
			using (new StopwatchPerformanceTracker("RefreshGALFolder", this.perfLogger))
			{
				this.InPlaceUpdateRecipientCacheFolder();
			}
			if (this.exceptionList.Count > 0)
			{
				throw new AggregateException(this.exceptionList);
			}
			return new RefreshGALFolderResponseEntity();
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002690 File Offset: 0x00000890
		private void InPlaceUpdateRecipientCacheFolder()
		{
			IEnumerable<IStorePropertyBag> allContacts = this.recipientCacheContactsDataProvider.GetAllContacts(RefreshGalFolder.ContactSortColumns);
			if (allContacts == null || !allContacts.Any<IStorePropertyBag>())
			{
				this.Trace.TraceDebug((long)this.tracingId, "RefreshGALContactsFolderCommand.InitialLoad: No contact found from Recipient cache folder, nothing to load in GAL folder, exiting");
				return;
			}
			IEnumerable<IStorePropertyBag> contactsFromAd = this.GetContactsFromAd(allContacts);
			if (contactsFromAd == null)
			{
				this.Trace.TraceDebug((long)this.tracingId, "RefreshGALContactsFolderCommand.InitialLoad: No contact found from Directory - no data to load, exiting");
				return;
			}
			this.PopulatePersonInfo(contactsFromAd, allContacts);
			this.DiffAndUpdateFolder(allContacts, contactsFromAd);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002704 File Offset: 0x00000904
		private void PopulatePersonInfo(IEnumerable<IStorePropertyBag> collectionToLoadInformation, IEnumerable<IStorePropertyBag> collectionWithInformation)
		{
			ListDiffCalculator<IStorePropertyBag, PropertyDefinition> listDiffCalculator = new ListDiffCalculator<IStorePropertyBag, PropertyDefinition>(StoreContactEmailAddressComparer.Instance, new ContactDeltaCalculator(RefreshGalFolder.RecipientCacheSpecificProperties));
			DiffResult<IStorePropertyBag, PropertyDefinition> diffResult = listDiffCalculator.DiffUnSortedLists(collectionToLoadInformation.ToList<IStorePropertyBag>(), collectionWithInformation.ToList<IStorePropertyBag>());
			if (diffResult.UpdateList.Count == 0)
			{
				return;
			}
			foreach (IStorePropertyBag storePropertyBag in diffResult.UpdateList.Keys)
			{
				foreach (Tuple<PropertyDefinition, object> tuple in diffResult.UpdateList[storePropertyBag])
				{
					storePropertyBag[tuple.Item1] = tuple.Item2;
				}
			}
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000027E0 File Offset: 0x000009E0
		private void AddContacts(ICollection<IStorePropertyBag> addCollection)
		{
			if (addCollection.Count == 0)
			{
				this.Trace.TraceDebug((long)this.GetHashCode(), "RefreshGalContactsFolderCommand.AddContacts - addList.Count is 0, nothing to add.");
				return;
			}
			foreach (IStorePropertyBag storePropertyBag in addCollection)
			{
				try
				{
					ConflictResolutionResult conflictResolutionResult = this.recipientCacheContactsDataProvider.AddContact(storePropertyBag);
					if (conflictResolutionResult.SaveStatus != SaveResult.Success)
					{
						this.Trace.TraceError<IStorePropertyBag, ConflictResolutionResult>((long)this.GetHashCode(), "Adding contact failed for contact {0}. ConflictResolutionResult = {1}.", storePropertyBag, conflictResolutionResult);
						this.AddToExceptionList(new SaveConflictException(ServerStrings.ExSaveFailedBecauseOfConflicts(storePropertyBag), conflictResolutionResult));
					}
				}
				catch (TransientException ex)
				{
					this.Trace.TraceError<IStorePropertyBag, TransientException>((long)this.GetHashCode(), "Adding contact failed for contact {0}. Exception = {1}.", storePropertyBag, ex);
					this.AddToExceptionList(ex);
				}
				catch (StoragePermanentException ex2)
				{
					this.Trace.TraceError<IStorePropertyBag, StoragePermanentException>((long)this.GetHashCode(), "Adding contact failed for contact {0}. Exception = {1}.", storePropertyBag, ex2);
					this.AddToExceptionList(ex2);
				}
			}
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000028EC File Offset: 0x00000AEC
		private void UpdateContacts(Dictionary<IStorePropertyBag, ICollection<Tuple<PropertyDefinition, object>>> updateList)
		{
			if (updateList.Count == 0)
			{
				this.Trace.TraceDebug((long)this.GetHashCode(), "RefreshGalContactsFolderCommand.UpdateContacts - updateList.Count is 0, nothing to updated.");
				return;
			}
			foreach (IStorePropertyBag storePropertyBag in updateList.Keys)
			{
				VersionedId valueOrDefault = storePropertyBag.GetValueOrDefault<VersionedId>(ItemSchema.Id, null);
				if (valueOrDefault == null)
				{
					this.Trace.TraceDebug<IStorePropertyBag>((long)this.GetHashCode(), "RefreshGalContactsFolderCommand.UpdateContacts - VersionedId is null for this contact {0}, thus skipping it.", storePropertyBag);
				}
				else
				{
					try
					{
						ConflictResolutionResult conflictResolutionResult = this.recipientCacheContactsDataProvider.UpdateContact(valueOrDefault, updateList[storePropertyBag]);
						if (conflictResolutionResult.SaveStatus != SaveResult.Success)
						{
							this.Trace.TraceError<IStorePropertyBag, ConflictResolutionResult>((long)this.GetHashCode(), "Updating contact failed for contact {0}. ConflictResolutionResult = {1}.", storePropertyBag, conflictResolutionResult);
							this.AddToExceptionList(new SaveConflictException(ServerStrings.ExSaveFailedBecauseOfConflicts(storePropertyBag), conflictResolutionResult));
						}
					}
					catch (TransientException ex)
					{
						this.Trace.TraceError<IStorePropertyBag, TransientException>((long)this.GetHashCode(), "Updating contact failed for contact {0}. Exception = {1}.", storePropertyBag, ex);
						this.AddToExceptionList(ex);
					}
					catch (StoragePermanentException ex2)
					{
						this.Trace.TraceError<IStorePropertyBag, StoragePermanentException>((long)this.GetHashCode(), "Updating contact failed for contact {0}. Exception = {1}.", storePropertyBag, ex2);
						this.AddToExceptionList(ex2);
					}
				}
			}
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002A38 File Offset: 0x00000C38
		private IEnumerable<IStorePropertyBag> GetContactsFromAd(IEnumerable<IStorePropertyBag> contacts)
		{
			ProxyAddress[] proxyAddresses = this.GetProxyAddresses(contacts);
			ProxyAddress[] array = null;
			if (proxyAddresses.Length > 1000)
			{
				this.tracer.TraceDebug<int, int>((long)this.GetHashCode(), "RefreshGalContactsFolderCommand.GetContactsFromAd: Number of proxy addresses {0} are more than {1}. Sending for only {1} entries to AD.", proxyAddresses.Length, 1000);
				array = new ProxyAddress[1000];
				Array.Copy(proxyAddresses, array, 1000);
			}
			IEnumerable<Result<ADRawEntry>> batchADObjects = this.contactDataProvider.GetBatchADObjects(array ?? proxyAddresses);
			if (batchADObjects.Count<Result<ADRawEntry>>() == 0)
			{
				this.Trace.TraceDebug<ProxyAddress[]>((long)this.tracingId, "RefreshGALContactsFolderCommand.GetContactsFromAd: No AD object found for the given set of proxyAddresses {0}", proxyAddresses);
				return Enumerable.Empty<IStorePropertyBag>();
			}
			ADObjectToIStorePropertyConverter adobjectToIStorePropertyConverter = new ADObjectToIStorePropertyConverter(this.Trace, GALContactsFolderSchema.ContactPropertyDefinitions);
			return adobjectToIStorePropertyConverter.ConvertAdObjectsToIStorePropertyBags(batchADObjects);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002AE4 File Offset: 0x00000CE4
		private ProxyAddress[] GetProxyAddresses(IEnumerable<IStorePropertyBag> contacts)
		{
			HashSet<ProxyAddress> hashSet = new HashSet<ProxyAddress>();
			foreach (IStorePropertyBag storePropertyBag in contacts)
			{
				string valueOrDefault = storePropertyBag.GetValueOrDefault<string>(ContactSchema.Email1EmailAddress, null);
				if (!string.IsNullOrEmpty(valueOrDefault))
				{
					ProxyAddress item;
					if (ProxyAddress.TryParse(valueOrDefault, out item))
					{
						hashSet.Add(item);
					}
					else
					{
						this.Trace.TraceDebug<string>((long)this.tracingId, "RefreshGALContactsFolderCommand.GetProxyAddresses: emailAddress - {0} failed in ProxyAddress.TryParse method, ignoring this email address", valueOrDefault);
					}
				}
			}
			ProxyAddress[] array = new ProxyAddress[hashSet.Count];
			hashSet.CopyTo(array);
			return array;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002B88 File Offset: 0x00000D88
		private void DiffAndUpdateFolder(IEnumerable<IStorePropertyBag> sourceContacts, IEnumerable<IStorePropertyBag> targetContacts)
		{
			ArgumentValidator.ThrowIfNull("sourceContacts", sourceContacts);
			ArgumentValidator.ThrowIfNull("targetContacts", targetContacts);
			ListDiffCalculator<IStorePropertyBag, PropertyDefinition> listDiffCalculator = new ListDiffCalculator<IStorePropertyBag, PropertyDefinition>(StoreContactPersonIdComparer.Instance, new ContactDeltaCalculator(GALContactsFolderSchema.ContactPropertyDefinitions));
			DiffResult<IStorePropertyBag, PropertyDefinition> diffResult = listDiffCalculator.DiffUnSortedLists(sourceContacts.ToList<IStorePropertyBag>(), targetContacts.ToList<IStorePropertyBag>());
			this.AddContacts(diffResult.AddList);
			this.UpdateContacts(diffResult.UpdateList);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002BEB File Offset: 0x00000DEB
		private void AddToExceptionList(Exception exception)
		{
			this.exceptionList.Add(exception);
		}

		// Token: 0x04000009 RID: 9
		private const int MaxEntriesForADQuery = 1000;

		// Token: 0x0400000A RID: 10
		private static readonly PropertyDefinition[] RecipientCacheSpecificProperties = new PropertyDefinition[]
		{
			ContactSchema.Email1EmailAddress,
			ContactSchema.PersonId,
			ContactSchema.RelevanceScore
		};

		// Token: 0x0400000B RID: 11
		private static readonly SortBy[] ContactSortColumns = new SortBy[]
		{
			new SortBy(ContactSchema.PersonId, SortOrder.Ascending)
		};

		// Token: 0x0400000C RID: 12
		private readonly IPerformanceDataLogger perfLogger = NullPerformanceDataLogger.Instance;

		// Token: 0x0400000D RID: 13
		private readonly int tracingId;

		// Token: 0x0400000E RID: 14
		private readonly ITracer tracer;

		// Token: 0x0400000F RID: 15
		private readonly List<Exception> exceptionList;

		// Token: 0x04000010 RID: 16
		private readonly ContactDataProvider recipientCacheContactsDataProvider;

		// Token: 0x04000011 RID: 17
		private readonly ADContactDataProvider contactDataProvider;
	}
}
