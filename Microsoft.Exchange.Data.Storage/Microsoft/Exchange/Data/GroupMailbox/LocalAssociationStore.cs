using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.GroupMailbox;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.GroupMailbox
{
	// Token: 0x0200080C RID: 2060
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class LocalAssociationStore : IAssociationStore, IDisposeTrackable, IDisposable
	{
		// Token: 0x06004CBE RID: 19646 RVA: 0x0013DBA8 File Offset: 0x0013BDA8
		public LocalAssociationStore(IMailboxLocator mailboxLocator, IMailboxSession session, bool shouldDisposeSession, IXSOFactory xsoFactory, IMailboxAssociationPerformanceTracker performanceTracker, IExtensibleLogger logger)
		{
			ArgumentValidator.ThrowIfNull("mailboxLocator", mailboxLocator);
			ArgumentValidator.ThrowIfNull("session", session);
			ArgumentValidator.ThrowIfNull("xsoFactory", xsoFactory);
			ArgumentValidator.ThrowIfNull("performanceTracker", performanceTracker);
			ArgumentValidator.ThrowIfNull("logger", logger);
			this.mailboxLocator = mailboxLocator;
			this.session = session;
			this.shouldDisposeSession = shouldDisposeSession;
			this.xsoFactory = xsoFactory;
			this.performanceTracker = performanceTracker;
			this.logger = logger;
			this.disposeTracker = this.GetDisposeTracker();
			this.mailboxAssociationFolder = new LazilyInitialized<IFolder>(new Func<IFolder>(this.GetOrCreateDefaultAssociationsFolder));
		}

		// Token: 0x170015F4 RID: 5620
		// (get) Token: 0x06004CBF RID: 19647 RVA: 0x0013DC45 File Offset: 0x0013BE45
		public IMailboxLocator MailboxLocator
		{
			get
			{
				return this.mailboxLocator;
			}
		}

		// Token: 0x170015F5 RID: 5621
		// (get) Token: 0x06004CC0 RID: 19648 RVA: 0x0013DC50 File Offset: 0x0013BE50
		public string ServerFullyQualifiedDomainName
		{
			get
			{
				StoreSession storeSession = this.session as StoreSession;
				string text;
				if (storeSession != null)
				{
					text = storeSession.ServerFullyQualifiedDomainName;
					LocalAssociationStore.Tracer.TraceDebug<string>((long)this.GetHashCode(), "LocalAssociationStore::ServerFullyQualifiedDomainName. Returning server name found in session: {0}", text);
				}
				else
				{
					text = LocalServerCache.LocalServerFqdn;
					LocalAssociationStore.Tracer.TraceDebug<string>((long)this.GetHashCode(), "LocalAssociationStore::ServerFullyQualifiedDomainName. Unkown session type returning local server name. {0}", text);
				}
				return text;
			}
		}

		// Token: 0x170015F6 RID: 5622
		// (get) Token: 0x06004CC1 RID: 19649 RVA: 0x0013DCAA File Offset: 0x0013BEAA
		public MailboxAssociationProcessingFlags AssociationProcessingFlags
		{
			get
			{
				this.LoadReplicationState(false);
				return this.mailboxReplicationFlags;
			}
		}

		// Token: 0x170015F7 RID: 5623
		// (get) Token: 0x06004CC2 RID: 19650 RVA: 0x0013DCB9 File Offset: 0x0013BEB9
		public ExDateTime MailboxNextSyncTime
		{
			get
			{
				this.LoadReplicationState(false);
				return this.mailboxNextSyncTime;
			}
		}

		// Token: 0x170015F8 RID: 5624
		// (get) Token: 0x06004CC3 RID: 19651 RVA: 0x0013DCC8 File Offset: 0x0013BEC8
		public IExchangePrincipal MailboxOwner
		{
			get
			{
				this.CheckDisposed("MailboxOwner");
				return this.session.MailboxOwner;
			}
		}

		// Token: 0x170015F9 RID: 5625
		// (get) Token: 0x06004CC4 RID: 19652 RVA: 0x0013DCE0 File Offset: 0x0013BEE0
		internal IMailboxSession Session
		{
			get
			{
				this.CheckDisposed("Session");
				return this.session;
			}
		}

		// Token: 0x06004CC5 RID: 19653 RVA: 0x0013DCF4 File Offset: 0x0013BEF4
		public static void SaveMailboxSyncStatus(IMailboxSession session, ExDateTime? nextReplicationTime, MailboxAssociationProcessingFlags? mailboxAssociationProcessingFlags)
		{
			ArgumentValidator.ThrowIfNull("session", session);
			bool flag = false;
			if (nextReplicationTime != null)
			{
				LocalAssociationStore.Tracer.TraceDebug<ExDateTime?>(0L, "LocalAssociationStore::SaveMailboxSyncStatus. Setting NextReplicationTime = {0}", nextReplicationTime);
				session.Mailbox[MailboxSchema.MailboxAssociationNextReplicationTime] = nextReplicationTime;
				flag = true;
			}
			if (mailboxAssociationProcessingFlags != null)
			{
				LocalAssociationStore.Tracer.TraceDebug<MailboxAssociationProcessingFlags?>(0L, "LocalAssociationStore::SaveMailboxSyncStatus. Setting ProcessingFlags = {0}", mailboxAssociationProcessingFlags);
				session.Mailbox[MailboxSchema.MailboxAssociationProcessingFlags] = mailboxAssociationProcessingFlags;
				flag = true;
			}
			if (flag)
			{
				LocalAssociationStore.Tracer.TraceDebug(0L, "LocalAssociationStore::SaveMailboxSyncStatus. Saving and reloading mailbox table.");
				session.Mailbox.Save();
				session.Mailbox.Load();
				return;
			}
			LocalAssociationStore.Tracer.TraceDebug(0L, "LocalAssociationStore::SaveMailboxSyncStatus. No changes were detected.");
		}

		// Token: 0x06004CC6 RID: 19654 RVA: 0x0013DDB0 File Offset: 0x0013BFB0
		public void SaveMailboxAsOutOfSync()
		{
			if (!this.mailboxMarkedAsOutOfSync)
			{
				LocalAssociationStore.Tracer.TraceDebug((long)this.GetHashCode(), "LocalAssociationStore::SaveMailboxAsOutOfSync. Marking mailbox with next replication date set to now");
				this.SaveMailboxSyncStatus(ExDateTime.UtcNow);
				this.mailboxMarkedAsOutOfSync = true;
				return;
			}
			LocalAssociationStore.Tracer.TraceWarning((long)this.GetHashCode(), "LocalAssociationStore::SaveMailboxAsOutOfSync. Mailbox has previously been marked as out-of-sync. Skiping");
		}

		// Token: 0x06004CC7 RID: 19655 RVA: 0x0013DE04 File Offset: 0x0013C004
		public void SaveMailboxSyncStatus(ExDateTime nextReplicationTime)
		{
			this.SaveMailboxSyncStatusInternal(new ExDateTime?(nextReplicationTime), null);
		}

		// Token: 0x06004CC8 RID: 19656 RVA: 0x0013DE26 File Offset: 0x0013C026
		public void SaveMailboxSyncStatus(ExDateTime nextReplicationTime, MailboxAssociationProcessingFlags mailboxAssociationProcessingFlags)
		{
			this.SaveMailboxSyncStatusInternal(new ExDateTime?(nextReplicationTime), new MailboxAssociationProcessingFlags?(mailboxAssociationProcessingFlags));
		}

		// Token: 0x06004CC9 RID: 19657 RVA: 0x0013DE3C File Offset: 0x0013C03C
		public IMailboxAssociationGroup CreateGroupAssociation()
		{
			this.CheckDisposed("CreateGroupAssociation");
			this.performanceTracker.IncrementAssociationsCreated();
			IFolder value = this.mailboxAssociationFolder.Value;
			return this.xsoFactory.CreateMailboxAssociationGroup(this.session, value.Id);
		}

		// Token: 0x06004CCA RID: 19658 RVA: 0x0013DE84 File Offset: 0x0013C084
		public IMailboxAssociationUser CreateUserAssociation()
		{
			this.CheckDisposed("CreateUserAssociation");
			this.performanceTracker.IncrementAssociationsCreated();
			IFolder value = this.mailboxAssociationFolder.Value;
			return this.xsoFactory.CreateMailboxAssociationUser(this.session, value.Id);
		}

		// Token: 0x06004CCB RID: 19659 RVA: 0x0013DECC File Offset: 0x0013C0CC
		public void DeleteAssociation(IMailboxAssociationBaseItem associationItem)
		{
			ArgumentValidator.ThrowIfNull("associationItem", associationItem);
			this.CheckDisposed("DeleteAssociation");
			MailboxAssociationBaseItem mailboxAssociationBaseItem = (MailboxAssociationBaseItem)associationItem;
			this.DeleteAssociation(mailboxAssociationBaseItem.GetValueOrDefault<VersionedId>(ItemSchema.Id));
		}

		// Token: 0x06004CCC RID: 19660 RVA: 0x0013DF08 File Offset: 0x0013C108
		public void OpenAssociationAsReadWrite(IMailboxAssociationBaseItem associationItem)
		{
			ArgumentValidator.ThrowIfNull("associationItem", associationItem);
			this.CheckDisposed("OpenAssociationAsReadWrite");
			MailboxAssociationBaseItem mailboxAssociationBaseItem = (MailboxAssociationBaseItem)associationItem;
			mailboxAssociationBaseItem.OpenAsReadWrite();
		}

		// Token: 0x06004CCD RID: 19661 RVA: 0x0013DF38 File Offset: 0x0013C138
		public void SaveAssociation(IMailboxAssociationBaseItem association)
		{
			ArgumentValidator.ThrowIfNull("association", association);
			this.CheckDisposed("SaveAssociation");
			this.performanceTracker.IncrementAssociationsUpdated();
			MailboxAssociationBaseItem mailboxAssociationBaseItem = (MailboxAssociationBaseItem)association;
			mailboxAssociationBaseItem.Save(SaveMode.ResolveConflicts);
		}

		// Token: 0x06004CCE RID: 19662 RVA: 0x0013DF78 File Offset: 0x0013C178
		public IEnumerable<IPropertyBag> GetAssociationsByType(string associationItemClass, PropertyDefinition associationTypeProperty, params PropertyDefinition[] propertiesToRetrieve)
		{
			return this.GetAssociationsByType(associationItemClass, associationTypeProperty, null, propertiesToRetrieve);
		}

		// Token: 0x06004CCF RID: 19663 RVA: 0x0013DF98 File Offset: 0x0013C198
		public IEnumerable<IPropertyBag> GetAssociationsByType(string associationItemClass, PropertyDefinition associationTypeProperty, int? maxItems, params PropertyDefinition[] propertiesToRetrieve)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("associationItemClass", associationItemClass);
			ArgumentValidator.ThrowIfNull("associationTypeProperty", associationTypeProperty);
			this.CheckDisposed("GetAssociationsByType");
			if (!typeof(bool).IsAssignableFrom(associationTypeProperty.Type))
			{
				throw new InvalidOperationException("LocalAssociationStore::GetItemByAssociationType. Parameter associationTypeProperty should be a boolean property.");
			}
			AndFilter filter = new AndFilter(new QueryFilter[]
			{
				new ComparisonFilter(ComparisonOperator.Equal, StoreObjectSchema.ItemClass, associationItemClass),
				new ComparisonFilter(ComparisonOperator.Equal, associationTypeProperty, true)
			});
			return this.QueryAssociationFolder(associationTypeProperty.Name, filter, maxItems, propertiesToRetrieve);
		}

		// Token: 0x06004CD0 RID: 19664 RVA: 0x0013E028 File Offset: 0x0013C228
		public IEnumerable<IPropertyBag> GetAssociationsWithMembershipChangedAfter(ExDateTime date, params PropertyDefinition[] properties)
		{
			ArgumentValidator.ThrowIfNull("date", date);
			QueryFilter seekFilter = QueryFilter.AndTogether(new QueryFilter[]
			{
				LocalAssociationStore.ItemClassUserFilter,
				new ComparisonFilter(ComparisonOperator.GreaterThanOrEqual, MailboxAssociationBaseSchema.JoinDate, date)
			});
			return this.SortAndSeekAssociationsFolder(LocalAssociationStore.JoinedAfterDateSortBys, seekFilter, properties);
		}

		// Token: 0x06004CD1 RID: 19665 RVA: 0x0013E07C File Offset: 0x0013C27C
		public TValue GetValueOrDefault<TValue>(IPropertyBag propertyBag, PropertyDefinition propertyDefinition, TValue defaultValue)
		{
			IStorePropertyBag storePropertyBag = (IStorePropertyBag)propertyBag;
			return storePropertyBag.GetValueOrDefault<TValue>(propertyDefinition, defaultValue);
		}

		// Token: 0x06004CD2 RID: 19666 RVA: 0x0013E098 File Offset: 0x0013C298
		public IMailboxAssociationGroup GetGroupAssociationByIdProperty(PropertyDefinition idProperty, params object[] idValues)
		{
			ArgumentValidator.ThrowIfNull("idProperty", idProperty);
			ArgumentValidator.ThrowIfNull("idValues", idValues);
			this.CheckDisposed("GetGroupAssociationByIdProperty");
			return this.GetAssociationByIdProperty<IMailboxAssociationGroup>(new Func<VersionedId, IMailboxAssociationGroup>(this.GetGroupAssociationByItemId), "IPM.MailboxAssociation.Group", MailboxAssociationGroupSchema.Instance.AllProperties, idProperty, idValues);
		}

		// Token: 0x06004CD3 RID: 19667 RVA: 0x0013E0E9 File Offset: 0x0013C2E9
		public IMailboxAssociationGroup GetGroupAssociationByItemId(VersionedId itemId)
		{
			ArgumentValidator.ThrowIfNull("itemId", itemId);
			this.CheckDisposed("GetGroupAssociationByItemId");
			return this.xsoFactory.BindToMailboxAssociationGroup(this.session, itemId.ObjectId, MailboxAssociationGroupSchema.Instance.AllProperties);
		}

		// Token: 0x06004CD4 RID: 19668 RVA: 0x0013E124 File Offset: 0x0013C324
		public IMailboxAssociationUser GetUserAssociationByIdProperty(PropertyDefinition idProperty, params object[] idValues)
		{
			ArgumentValidator.ThrowIfNull("idProperty", idProperty);
			ArgumentValidator.ThrowIfNull("idValues", idValues);
			this.CheckDisposed("GetUserAssociationByIdProperty");
			return this.GetAssociationByIdProperty<IMailboxAssociationUser>(new Func<VersionedId, IMailboxAssociationUser>(this.GetUserAssociationByItemId), "IPM.MailboxAssociation.User", MailboxAssociationUserSchema.Instance.AllProperties, idProperty, idValues);
		}

		// Token: 0x06004CD5 RID: 19669 RVA: 0x0013E175 File Offset: 0x0013C375
		public IMailboxAssociationUser GetUserAssociationByItemId(VersionedId itemId)
		{
			ArgumentValidator.ThrowIfNull("itemId", itemId);
			this.CheckDisposed("GetUserAssociationByItemId");
			return this.xsoFactory.BindToMailboxAssociationUser(this.session, itemId.ObjectId, MailboxAssociationUserSchema.Instance.AllProperties);
		}

		// Token: 0x06004CD6 RID: 19670 RVA: 0x0013E1B0 File Offset: 0x0013C3B0
		public IEnumerable<IPropertyBag> GetAllAssociations(string associationItemClass, ICollection<PropertyDefinition> propertiesToRetrieve)
		{
			ArgumentValidator.ThrowIfNullOrWhiteSpace("associationItemClass", associationItemClass);
			LocalAssociationStore.Tracer.TraceDebug<string, Guid>((long)this.GetHashCode(), "LocalAssociationStore.GetAllAssociations: SeekInAssociationFolder for ItemClass={0}. Mailbox={1}", associationItemClass, this.session.MailboxGuid);
			ComparisonFilter seekFilter = new ComparisonFilter(ComparisonOperator.Equal, StoreObjectSchema.ItemClass, associationItemClass);
			return this.SortAndSeekAssociationsFolder(LocalAssociationStore.SortByItemClass, seekFilter, propertiesToRetrieve);
		}

		// Token: 0x06004CD7 RID: 19671 RVA: 0x0013E454 File Offset: 0x0013C654
		private IEnumerable<IPropertyBag> SortAndSeekAssociationsFolder(SortBy[] sortBys, QueryFilter seekFilter, ICollection<PropertyDefinition> propertiesToRetrieve)
		{
			IFolder folder = this.mailboxAssociationFolder.Value;
			using (IQueryResult queryResult = folder.IItemQuery(ItemQueryType.None, null, sortBys, propertiesToRetrieve))
			{
				IEnumerable<IPropertyBag> foundItems = this.SeekForValueInQueryResult(queryResult, seekFilter);
				foreach (IPropertyBag propertyBag in foundItems)
				{
					yield return propertyBag;
				}
			}
			yield break;
		}

		// Token: 0x06004CD8 RID: 19672 RVA: 0x0013E486 File Offset: 0x0013C686
		public DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<LocalAssociationStore>(this);
		}

		// Token: 0x06004CD9 RID: 19673 RVA: 0x0013E48E File Offset: 0x0013C68E
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x06004CDA RID: 19674 RVA: 0x0013E4A3 File Offset: 0x0013C6A3
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06004CDB RID: 19675 RVA: 0x0013E4B2 File Offset: 0x0013C6B2
		private void CheckDisposed(string methodName)
		{
			if (this.disposed)
			{
				LocalAssociationStore.Tracer.TraceError<string>((long)this.GetHashCode(), "LocalAssociationStore::{0}. Attempted to use disposed object.", methodName);
				throw new ObjectDisposedException(base.GetType().ToString());
			}
		}

		// Token: 0x06004CDC RID: 19676 RVA: 0x0013E4E4 File Offset: 0x0013C6E4
		private void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (disposing)
				{
					if (this.mailboxAssociationFolder.IsInitialized)
					{
						this.mailboxAssociationFolder.Value.Dispose();
					}
					if (this.shouldDisposeSession)
					{
						this.session.Dispose();
					}
					if (this.disposeTracker != null)
					{
						this.disposeTracker.Dispose();
					}
				}
				this.disposed = true;
			}
		}

		// Token: 0x06004CDD RID: 19677 RVA: 0x0013E548 File Offset: 0x0013C748
		private TMailboxAssociation GetAssociationByIdProperty<TMailboxAssociation>(Func<VersionedId, TMailboxAssociation> bindFunction, string associationItemClass, ICollection<PropertyDefinition> propertiesToRetrieve, PropertyDefinition idProperty, params object[] idValues) where TMailboxAssociation : class, IMailboxAssociationBaseItem
		{
			this.CheckDisposed("GetAssociationByIdProperty");
			if (idValues.Length == 0)
			{
				return default(TMailboxAssociation);
			}
			IPropertyBag[] array = this.SeekInAssociationFolder<object>(associationItemClass, idProperty, idValues, new PropertyDefinition[]
			{
				idProperty,
				StoreObjectSchema.ItemClass,
				ItemSchema.Id
			}).ToArray<IPropertyBag>();
			string text = null;
			if (LocalAssociationStore.Tracer.IsTraceEnabled(TraceType.DebugTrace) || LocalAssociationStore.Tracer.IsTraceEnabled(TraceType.ErrorTrace) || array.Length > 1)
			{
				text = string.Join(", ", idValues);
			}
			if (array.Length == 0)
			{
				LocalAssociationStore.Tracer.TraceDebug<string, string>((long)this.GetHashCode(), "LocalAssociationStore::GetAssociationByIdProperty. Found no association item searching by property {0} with values {1}.", idProperty.Name, text);
				this.performanceTracker.IncrementFailedAssociationsSearch();
				return default(TMailboxAssociation);
			}
			if (array.Length > 1)
			{
				this.performanceTracker.IncrementNonUniqueAssociationsFound();
				this.LogWarning("LocalAssociationStore::GetAssociationByIdProperty", string.Format("Found more than 1 association item searching by property {0} with values {1}.", idProperty.Name, text));
				this.LogWarning("LocalAssociationStore::GetAssociationByIdProperty", string.Format("Keeping association with ID: {0}.", array[0][ItemSchema.Id]));
				for (int i = 1; i < array.Length; i++)
				{
					VersionedId versionedId = array[i][ItemSchema.Id] as VersionedId;
					using (TMailboxAssociation tmailboxAssociation = bindFunction(versionedId))
					{
						if (tmailboxAssociation != null)
						{
							this.LogWarning("LocalAssociationStore::GetAssociationByIdProperty", string.Format("Keeping association with ID: {0}, Removing association {1}", array[0][ItemSchema.Id], tmailboxAssociation.ToString()));
							this.DeleteAssociation(versionedId);
						}
						else
						{
							this.LogError("LocalAssociationStore::GetAssociationByIdProperty", string.Format("Couldn't bind to association with ID: {0}.", versionedId.ToString()));
						}
					}
				}
			}
			VersionedId versionedId2 = array[0][ItemSchema.Id] as VersionedId;
			LocalAssociationStore.Tracer.TraceDebug((long)this.GetHashCode(), "LocalAssociationStore::GetAssociationByIdProperty. Found association item searching by property {0} with values {1}. ItemId: {2}. Found value: {3}.", new object[]
			{
				idProperty.Name,
				text,
				versionedId2,
				array[0][idProperty]
			});
			return bindFunction(versionedId2);
		}

		// Token: 0x06004CDE RID: 19678 RVA: 0x0013EAE8 File Offset: 0x0013CCE8
		private IEnumerable<IPropertyBag> SeekInAssociationFolder<T>(string itemClass, PropertyDefinition seekProperty, IEnumerable<T> seekValues, params PropertyDefinition[] properties)
		{
			ArgumentValidator.ThrowIfNullOrWhiteSpace("itemClass", itemClass);
			ArgumentValidator.ThrowIfNull("seekProperty", seekProperty);
			ArgumentValidator.ThrowIfNull("seekValues", seekValues);
			SortBy[] sortBys = new SortBy[]
			{
				new SortBy(seekProperty, SortOrder.Ascending),
				new SortBy(StoreObjectSchema.ItemClass, SortOrder.Ascending),
				new SortBy(StoreObjectSchema.LastModifiedTime, SortOrder.Descending)
			};
			IFolder folder = this.mailboxAssociationFolder.Value;
			using (IQueryResult queryResult = folder.IItemQuery(ItemQueryType.None, null, sortBys, properties))
			{
				foreach (T value in seekValues)
				{
					IEnumerable<IPropertyBag> foundItems = this.SeekForValueInQueryResult<T>(queryResult, seekProperty, value, itemClass);
					foreach (IPropertyBag propertyBag in foundItems)
					{
						yield return propertyBag;
					}
				}
			}
			yield break;
		}

		// Token: 0x06004CDF RID: 19679 RVA: 0x0013EB24 File Offset: 0x0013CD24
		private IEnumerable<IPropertyBag> SeekForValueInQueryResult<T>(IQueryResult queryResult, PropertyDefinition seekProperty, T seekValue, string itemClass)
		{
			LocalAssociationStore.Tracer.TraceDebug<string, T, Guid>((long)this.GetHashCode(), "LocalAssociationStore.SeekForValueInQueryResult: SeekInAssociationFolder for {0}={1}. Mailbox={2}", seekProperty.Name, seekValue, this.session.MailboxGuid);
			AndFilter seekFilter = new AndFilter(new QueryFilter[]
			{
				new ComparisonFilter(ComparisonOperator.Equal, seekProperty, seekValue),
				new ComparisonFilter(ComparisonOperator.Equal, StoreObjectSchema.ItemClass, itemClass)
			});
			return this.SeekForValueInQueryResult(queryResult, seekFilter);
		}

		// Token: 0x06004CE0 RID: 19680 RVA: 0x0013EE44 File Offset: 0x0013D044
		private IEnumerable<IPropertyBag> SeekForValueInQueryResult(IQueryResult queryResult, QueryFilter seekFilter)
		{
			if (queryResult.SeekToCondition(SeekReference.OriginBeginning, seekFilter, SeekToConditionFlags.AllowExtendedFilters))
			{
				bool itemsRemaining = true;
				while (itemsRemaining)
				{
					LocalAssociationStore.Tracer.TraceDebug<Guid>((long)this.GetHashCode(), "LocalAssociationStore.SeekForValueInQueryResult: Retrieving more items. Mailbox={0}", this.session.MailboxGuid);
					IStorePropertyBag[] items = queryResult.GetPropertyBags(100);
					if (items != null && items.Length > 0)
					{
						foreach (IStorePropertyBag item in items)
						{
							if (EvaluatableFilter.Evaluate(seekFilter, item))
							{
								LocalAssociationStore.Tracer.TraceDebug<Guid>((long)this.GetHashCode(), "LocalAssociationStore.SeekForValueInQueryResult: Returning found property bag. Mailbox={0}", this.session.MailboxGuid);
								this.performanceTracker.IncrementAssociationsRead();
								yield return item;
							}
							else
							{
								itemsRemaining = false;
							}
						}
					}
					else
					{
						itemsRemaining = false;
					}
				}
			}
			LocalAssociationStore.Tracer.TraceDebug<Guid>((long)this.GetHashCode(), "LocalAssociationStore.SeekForValueInQueryResult: No more property bags found. Mailbox={0}", this.session.MailboxGuid);
			yield break;
		}

		// Token: 0x06004CE1 RID: 19681 RVA: 0x0013F254 File Offset: 0x0013D454
		private IEnumerable<IPropertyBag> QueryAssociationFolder(string context, QueryFilter filter, int? maxItems, params PropertyDefinition[] properties)
		{
			if (maxItems != null)
			{
				ArgumentValidator.ThrowIfZeroOrNegative("maxItems", maxItems.Value);
			}
			IFolder folder = this.mailboxAssociationFolder.Value;
			properties = PropertyDefinitionCollection.Merge<PropertyDefinition>(properties, new PropertyDefinition[]
			{
				ItemSchema.Id
			});
			using (IQueryResult queryResult = folder.IItemQuery(ItemQueryType.None, filter, null, properties))
			{
				int itemsNumber = 0;
				bool fetchAllItems = maxItems == null;
				while (fetchAllItems || itemsNumber < maxItems.Value)
				{
					LocalAssociationStore.Tracer.TraceDebug<Guid>((long)this.GetHashCode(), "LocalAssociationStore.QueryAssociationFolder: Retrieving mailbox associations in mailbox {0}.", this.session.MailboxGuid);
					int fetchRowCount = fetchAllItems ? 100 : Math.Min(maxItems.Value - itemsNumber, 100);
					IStorePropertyBag[] items = queryResult.GetPropertyBags(fetchRowCount);
					if (items == null || items.Length == 0)
					{
						LocalAssociationStore.Tracer.TraceDebug((long)this.GetHashCode(), "LocalAssociationStore.QueryAssociationFolder: No more property bags found.");
						yield break;
					}
					foreach (IStorePropertyBag item in items)
					{
						LocalAssociationStore.Tracer.TraceDebug((long)this.GetHashCode(), "LocalAssociationStore.QueryAssociationFolder: Returning property bag with Id {0}.", new object[]
						{
							item[ItemSchema.Id]
						});
						this.performanceTracker.IncrementAssociationsRead();
						if (this.ValidateItem(context, item))
						{
							yield return item;
						}
					}
					itemsNumber += items.Length;
				}
			}
			yield break;
		}

		// Token: 0x06004CE2 RID: 19682 RVA: 0x0013F290 File Offset: 0x0013D490
		private void LoadReplicationState(bool reload = false)
		{
			if (reload || !this.mailboxReplicationStateLoaded)
			{
				LocalAssociationStore.Tracer.TraceDebug<bool>((long)this.GetHashCode(), "LocalAssociationStore::LoadReplicationState. Loading mailbox properties. (Reload={0})", reload);
				this.session.Mailbox.Load(LocalAssociationStore.MailboxProperties);
				this.mailboxReplicationFlags = this.session.Mailbox.GetValueOrDefault<MailboxAssociationProcessingFlags>(MailboxSchema.MailboxAssociationProcessingFlags, MailboxAssociationProcessingFlags.None);
				this.mailboxNextSyncTime = this.session.Mailbox.GetValueOrDefault<ExDateTime>(MailboxSchema.MailboxAssociationNextReplicationTime, ExDateTime.MinValue);
				this.mailboxReplicationStateLoaded = true;
				return;
			}
			LocalAssociationStore.Tracer.TraceWarning<bool>((long)this.GetHashCode(), "LocalAssociationStore::LoadReplicationState. Skipping mailbox properties loading. (Reload={0})", reload);
		}

		// Token: 0x06004CE3 RID: 19683 RVA: 0x0013F32F File Offset: 0x0013D52F
		private void SaveMailboxSyncStatusInternal(ExDateTime? nextReplicationTime, MailboxAssociationProcessingFlags? mailboxAssociationProcessingFlags)
		{
			LocalAssociationStore.SaveMailboxSyncStatus(this.session, nextReplicationTime, mailboxAssociationProcessingFlags);
			this.LoadReplicationState(true);
		}

		// Token: 0x06004CE4 RID: 19684 RVA: 0x0013F348 File Offset: 0x0013D548
		private IFolder GetOrCreateDefaultAssociationsFolder()
		{
			StoreObjectId storeObjectId = this.session.GetDefaultFolderId(DefaultFolderType.MailboxAssociation);
			if (storeObjectId == null)
			{
				LocalAssociationStore.Tracer.TraceDebug((long)this.GetHashCode(), "LocalAssociationStore.GetOrCreateDefaultAssociationsFolder: Default mailbox association folder was not found. Attempting to create it");
				storeObjectId = this.session.CreateDefaultFolder(DefaultFolderType.MailboxAssociation);
			}
			LocalAssociationStore.Tracer.TraceDebug<StoreObjectId>((long)this.GetHashCode(), "LocalAssociationStore.GetOrCreateDefaultAssociationsFolder: Mailbox association folder ID = {0}.", storeObjectId);
			IFolder result;
			try
			{
				result = this.xsoFactory.BindToFolder(this.session, storeObjectId);
			}
			catch (ObjectNotFoundException arg)
			{
				LocalAssociationStore.Tracer.TraceError<StoreObjectId, ObjectNotFoundException>((long)this.GetHashCode(), "LocalAssociationStore.GetOrCreateDefaultAssociationsFolder: Couldn't bind to Association folder {0}. Exception='{1}'", storeObjectId, arg);
				StoreObjectId folderId = null;
				if (!this.session.TryFixDefaultFolderId(DefaultFolderType.MailboxAssociation, out folderId))
				{
					this.LogError("LocalAssociationStore.GetOrCreateDefaultAssociationsFolder", string.Format("Failed to repair association folder for mailbox:{0}", this.session.MailboxGuid));
					throw;
				}
				IFolder folder = this.xsoFactory.BindToFolder(this.session, folderId);
				LocalAssociationStore.Tracer.TraceDebug((long)this.GetHashCode(), "Successfully repaired association folder");
				result = folder;
			}
			return result;
		}

		// Token: 0x06004CE5 RID: 19685 RVA: 0x0013F450 File Offset: 0x0013D650
		private void DeleteAssociation(VersionedId itemId)
		{
			if (itemId != null)
			{
				LocalAssociationStore.Tracer.TraceDebug<StoreObjectId>((long)this.GetHashCode(), "LocalAssociationStore::DeleteAssociation. Deleting association item with ID={0}.", itemId.ObjectId);
				this.session.Delete(DeleteItemFlags.HardDelete, new StoreId[]
				{
					itemId.ObjectId
				});
				this.performanceTracker.IncrementAssociationsDeleted();
				return;
			}
			LocalAssociationStore.Tracer.TraceDebug((long)this.GetHashCode(), "LocalAssociationStore::DeleteAssociation. Skipping association without item ID.");
		}

		// Token: 0x06004CE6 RID: 19686 RVA: 0x0013F4BC File Offset: 0x0013D6BC
		private bool ValidateItem(string context, IStorePropertyBag item)
		{
			object obj = item[MailboxAssociationBaseSchema.LegacyDN];
			if (obj == null || PropertyError.IsPropertyError(obj) || string.IsNullOrEmpty(obj as string))
			{
				this.performanceTracker.IncrementMissingLegacyDns();
				string arg = item[ItemSchema.Id].ToString();
				string valueOrDefault = item.GetValueOrDefault<string>(MailboxAssociationBaseSchema.ExternalId, "empty");
				PropertyError propertyError = obj as PropertyError;
				string arg2 = (propertyError == null) ? "null" : propertyError.ToString();
				string errorMessage = string.Format("LocalAssociationStore.QueryAssociationFolder: Missing LegacyDn for property bag with Id {0}, ExternalId {1}, PropertyError {2}", arg, valueOrDefault, arg2);
				this.LogError(context, errorMessage);
				return false;
			}
			return true;
		}

		// Token: 0x06004CE7 RID: 19687 RVA: 0x0013F550 File Offset: 0x0013D750
		private void LogError(string context, string errorMessage)
		{
			LocalAssociationStore.Tracer.TraceError((long)this.GetHashCode(), errorMessage);
			this.logger.LogEvent(new SchemaBasedLogEvent<MailboxAssociationLogSchema.Error>
			{
				{
					MailboxAssociationLogSchema.Error.Context,
					context
				},
				{
					MailboxAssociationLogSchema.Error.Exception,
					errorMessage
				}
			});
		}

		// Token: 0x06004CE8 RID: 19688 RVA: 0x0013F594 File Offset: 0x0013D794
		private void LogWarning(string context, string warningMessage)
		{
			LocalAssociationStore.Tracer.TraceWarning((long)this.GetHashCode(), warningMessage);
			this.logger.LogEvent(new SchemaBasedLogEvent<MailboxAssociationLogSchema.Warning>
			{
				{
					MailboxAssociationLogSchema.Warning.Context,
					context
				},
				{
					MailboxAssociationLogSchema.Warning.Message,
					warningMessage
				}
			});
		}

		// Token: 0x040029D9 RID: 10713
		private const int ItemBatchSize = 100;

		// Token: 0x040029DA RID: 10714
		private static readonly Trace Tracer = ExTraceGlobals.LocalAssociationStoreTracer;

		// Token: 0x040029DB RID: 10715
		private static readonly PropertyDefinition[] MailboxProperties = new PropertyDefinition[]
		{
			MailboxSchema.MailboxAssociationNextReplicationTime,
			MailboxSchema.MailboxAssociationProcessingFlags
		};

		// Token: 0x040029DC RID: 10716
		private static readonly SortBy[] SortByItemClass = new SortBy[]
		{
			new SortBy(StoreObjectSchema.ItemClass, SortOrder.Ascending)
		};

		// Token: 0x040029DD RID: 10717
		private static readonly SortBy[] JoinedAfterDateSortBys = new SortBy[]
		{
			new SortBy(StoreObjectSchema.ItemClass, SortOrder.Ascending),
			new SortBy(MailboxAssociationBaseSchema.JoinDate, SortOrder.Ascending)
		};

		// Token: 0x040029DE RID: 10718
		private static readonly ComparisonFilter ItemClassUserFilter = new ComparisonFilter(ComparisonOperator.Equal, StoreObjectSchema.ItemClass, "IPM.MailboxAssociation.User");

		// Token: 0x040029DF RID: 10719
		private readonly IMailboxLocator mailboxLocator;

		// Token: 0x040029E0 RID: 10720
		private readonly IMailboxSession session;

		// Token: 0x040029E1 RID: 10721
		private readonly bool shouldDisposeSession;

		// Token: 0x040029E2 RID: 10722
		private readonly IXSOFactory xsoFactory;

		// Token: 0x040029E3 RID: 10723
		private readonly LazilyInitialized<IFolder> mailboxAssociationFolder;

		// Token: 0x040029E4 RID: 10724
		private readonly IExtensibleLogger logger;

		// Token: 0x040029E5 RID: 10725
		private readonly IMailboxAssociationPerformanceTracker performanceTracker;

		// Token: 0x040029E6 RID: 10726
		private readonly DisposeTracker disposeTracker;

		// Token: 0x040029E7 RID: 10727
		private bool disposed;

		// Token: 0x040029E8 RID: 10728
		private bool mailboxReplicationStateLoaded;

		// Token: 0x040029E9 RID: 10729
		private MailboxAssociationProcessingFlags mailboxReplicationFlags;

		// Token: 0x040029EA RID: 10730
		private ExDateTime mailboxNextSyncTime;

		// Token: 0x040029EB RID: 10731
		private bool mailboxMarkedAsOutOfSync;
	}
}
