using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.InfoWorker.Common;

namespace Microsoft.Exchange.Data.Storage.Infoworker.MailboxSearch
{
	// Token: 0x02000D2A RID: 3370
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MailboxDataProvider : MailboxStoreDataProvider
	{
		// Token: 0x0600745E RID: 29790 RVA: 0x00204EC4 File Offset: 0x002030C4
		private MailboxDataStore OpenMailboxStore()
		{
			return new MailboxDataStore(base.ADUser);
		}

		// Token: 0x0600745F RID: 29791 RVA: 0x00204EE0 File Offset: 0x002030E0
		public static ADUser GetDiscoveryMailbox(IRecipientSession session)
		{
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			IRecipientSession recipientSession = session;
			if (recipientSession.ConfigScope != ConfigScopes.TenantLocal)
			{
				recipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(session.DomainController, true, ConsistencyMode.PartiallyConsistent, session.NetworkCredential, session.SessionSettings, 104, "GetDiscoveryMailbox", "f:\\15.00.1497\\sources\\dev\\data\\src\\storage\\Search\\MailboxSearch\\MailboxDataProvider.cs");
				recipientSession.UseGlobalCatalog = true;
				recipientSession.EnforceDefaultScope = session.EnforceDefaultScope;
			}
			ADRecipient[] array = recipientSession.Find(null, QueryScope.SubTree, MailboxDataProvider.DiscoverySystemMailboxFilter, null, 2);
			switch (array.Length)
			{
			case 0:
				throw new ObjectNotFoundException(ServerStrings.DiscoveryMailboxNotFound);
			case 1:
				return array[0] as ADUser;
			default:
				throw new NonUniqueRecipientException(array[0], new NonUniqueAddressError(ServerStrings.DiscoveryMailboxIsNotUnique(array[0].Id.ToString(), array[1].Id.ToString()), array[0].Id, "DiscoveryMailbox"));
			}
		}

		// Token: 0x06007460 RID: 29792 RVA: 0x00204FB8 File Offset: 0x002031B8
		public static ADUser GetDiscoveryUserMailbox(IRecipientSession session)
		{
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			IRecipientSession recipientSession = session;
			if (recipientSession.ConfigScope != ConfigScopes.TenantLocal)
			{
				recipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(session.DomainController, true, ConsistencyMode.PartiallyConsistent, session.NetworkCredential, session.SessionSettings, 156, "GetDiscoveryUserMailbox", "f:\\15.00.1497\\sources\\dev\\data\\src\\storage\\Search\\MailboxSearch\\MailboxDataProvider.cs");
				recipientSession.UseGlobalCatalog = true;
				recipientSession.EnforceDefaultScope = session.EnforceDefaultScope;
			}
			ADPagedReader<ADRecipient> adpagedReader = recipientSession.FindPaged(null, QueryScope.SubTree, MailboxDataProvider.DiscoveryUserMailboxFilter, null, 10);
			foreach (ADRecipient adrecipient in adpagedReader)
			{
				ADUser aduser = (ADUser)adrecipient;
				ADUser aduser2 = aduser;
				if (ExchangePrincipal.FromADUser(recipientSession.SessionSettings, aduser2, RemotingOptions.AllowCrossSite).MailboxInfo.Location.ServerVersion >= Server.E14SP1MinVersion)
				{
					return aduser2;
				}
			}
			throw new ObjectNotFoundException(ServerStrings.UserDiscoveryMailboxNotFound);
		}

		// Token: 0x06007461 RID: 29793 RVA: 0x002050A8 File Offset: 0x002032A8
		public static void IncrementDiscoveryCopyItemsRatePerfCounter(int numberOfItems)
		{
			NamedPropMap.GetPerfCounters().DiscoveryCopyItemsRate.IncrementBy((long)numberOfItems);
		}

		// Token: 0x06007462 RID: 29794 RVA: 0x002050BC File Offset: 0x002032BC
		public static void IncrementDiscoveryMailboxSearchQueuePerfCounters()
		{
			NamedPropMap.GetPerfCounters().DiscoveryMailboxSearchesQueued.Increment();
		}

		// Token: 0x06007463 RID: 29795 RVA: 0x002050CE File Offset: 0x002032CE
		public static void DecrementDiscoveryMailboxSearchQueuePerfCounters()
		{
			NamedPropMap.GetPerfCounters().DiscoveryMailboxSearchesQueued.Decrement();
		}

		// Token: 0x06007464 RID: 29796 RVA: 0x002050E0 File Offset: 0x002032E0
		public static void IncrementDiscoveryMailboxSearchPerfCounters(int numberOfMailboxes)
		{
			NamedPropMap.GetPerfCounters().DiscoveryMailboxSearchesActive.Increment();
			NamedPropMap.GetPerfCounters().DiscoveryMailboxSearchSourceMailboxesActive.IncrementBy((long)numberOfMailboxes);
		}

		// Token: 0x06007465 RID: 29797 RVA: 0x00205104 File Offset: 0x00203304
		public static void DecrementDiscoveryMailboxSearchPerfCounters(int numberOfMailboxes)
		{
			NamedPropMap.GetPerfCounters().DiscoveryMailboxSearchesActive.Decrement();
			NamedPropMap.GetPerfCounters().DiscoveryMailboxSearchSourceMailboxesActive.IncrementBy((long)(-1 * numberOfMailboxes));
		}

		// Token: 0x17001F04 RID: 7940
		// (get) Token: 0x06007466 RID: 29798 RVA: 0x0020512A File Offset: 0x0020332A
		public IRecipientSession RecipientSession
		{
			get
			{
				return this.recipientSession;
			}
		}

		// Token: 0x06007467 RID: 29799 RVA: 0x00205132 File Offset: 0x00203332
		public MailboxDataProvider(ADUser adUser, IRecipientSession recipientSession) : base(adUser)
		{
			this.recipientSession = recipientSession;
		}

		// Token: 0x06007468 RID: 29800 RVA: 0x00205142 File Offset: 0x00203342
		public MailboxDataProvider(ADUser adUser) : this(adUser, null)
		{
		}

		// Token: 0x06007469 RID: 29801 RVA: 0x0020514C File Offset: 0x0020334C
		public MailboxDataProvider(IRecipientSession recipientSession) : this(MailboxDataProvider.GetDiscoveryMailbox(recipientSession), recipientSession)
		{
		}

		// Token: 0x0600746A RID: 29802 RVA: 0x0020515C File Offset: 0x0020335C
		private void Save(SearchObjectBase instance)
		{
			if (instance == null)
			{
				throw new ArgumentNullException("instance");
			}
			MailboxDataProvider.Tracer.TraceDebug<SearchObjectId>((long)this.GetHashCode(), "Saving search object {0}", instance.Id);
			if (instance.ObjectState == ObjectState.Deleted)
			{
				throw new InvalidOperationException("Calling Save() on a deleted object is not permitted. Delete() should be used instead.");
			}
			if (instance.ObjectState == ObjectState.Unchanged)
			{
				return;
			}
			bool flag = false;
			if (instance.ObjectState == ObjectState.New && (instance.Id == null || instance.Id.IsEmpty))
			{
				instance.SetId(base.ADUser.Id, Guid.NewGuid());
				flag = true;
			}
			ValidationError[] array = instance.Validate();
			if (array.Length > 0)
			{
				throw new DataValidationException(array[0]);
			}
			using (MailboxDataStore mailboxDataStore = this.OpenMailboxStore())
			{
				if (flag)
				{
					while (mailboxDataStore.Exists(instance.Id))
					{
						instance.SetId(base.ADUser.Id, Guid.NewGuid());
					}
				}
				instance.OnSaving();
				mailboxDataStore.Save(instance);
			}
			instance.ResetChangeTracking(true);
			this.LogSaveEvent(instance);
		}

		// Token: 0x0600746B RID: 29803 RVA: 0x00205268 File Offset: 0x00203468
		private void LogSaveEvent(SearchObjectBase obj)
		{
			SearchEventLogger.PropertyLogData propertyLogData = new SearchEventLogger.PropertyLogData();
			if (base.ADUser.OrganizationId != null && base.ADUser.OrganizationId.ConfigurationUnit != null)
			{
				propertyLogData.AddOrganization(base.ADUser.OrganizationId.ConfigurationUnit.ToString());
			}
			switch (obj.ObjectType)
			{
			case ObjectType.SearchObject:
				break;
			case ObjectType.SearchStatus:
			{
				SearchStatus searchStatus = (SearchStatus)obj;
				propertyLogData.AddSearchStatus(searchStatus);
				SearchEventLogger.Instance.LogSearchStatusSavedEvent(propertyLogData);
				if (searchStatus.Errors == null)
				{
					return;
				}
				using (MultiValuedProperty<string>.Enumerator enumerator = searchStatus.Errors.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						string errorMsg = enumerator.Current;
						SearchEventLogger.Instance.LogSearchErrorEvent(obj.Id.ToString(), errorMsg);
					}
					return;
				}
				break;
			}
			default:
				return;
			}
			propertyLogData.AddSearchObject((SearchObject)obj);
			SearchEventLogger.Instance.LogSearchObjectSavedEvent(propertyLogData);
		}

		// Token: 0x0600746C RID: 29804 RVA: 0x00205360 File Offset: 0x00203560
		internal void Delete(SearchObjectBase instance)
		{
			if (instance == null)
			{
				throw new ArgumentNullException("instance");
			}
			MailboxDataProvider.Tracer.TraceDebug<SearchObjectId>((long)this.GetHashCode(), "Deleting search object {0}", instance.Id);
			if (instance.ObjectState == ObjectState.Deleted)
			{
				throw new InvalidOperationException("The object has already been deleted");
			}
			using (MailboxDataStore mailboxDataStore = this.OpenMailboxStore())
			{
				mailboxDataStore.Delete(instance);
			}
			instance.MarkAsDeleted();
		}

		// Token: 0x0600746D RID: 29805 RVA: 0x002053DC File Offset: 0x002035DC
		public bool Exists(SearchObjectId identity)
		{
			MailboxDataProvider.Tracer.TraceDebug<SearchObjectId>((long)this.GetHashCode(), "Querying existence of search object {0}", identity);
			bool result;
			using (MailboxDataStore mailboxDataStore = this.OpenMailboxStore())
			{
				result = mailboxDataStore.Exists(identity);
			}
			return result;
		}

		// Token: 0x0600746E RID: 29806 RVA: 0x0020542C File Offset: 0x0020362C
		public bool Exists<T>(string name) where T : IConfigurable, new()
		{
			MailboxDataProvider.Tracer.TraceDebug<string>((long)this.GetHashCode(), "Querying existence of search object {0}", name);
			bool result;
			using (MailboxDataStore mailboxDataStore = this.OpenMailboxStore())
			{
				result = mailboxDataStore.Exists<T>(name);
			}
			return result;
		}

		// Token: 0x0600746F RID: 29807 RVA: 0x0020547C File Offset: 0x0020367C
		public override IConfigurable Read<T>(ObjectId identity)
		{
			MailboxDataProvider.Tracer.TraceDebug<ObjectId>((long)this.GetHashCode(), "Reading search object with identity {0}", identity);
			IConfigurable result;
			using (MailboxDataStore mailboxDataStore = this.OpenMailboxStore())
			{
				result = mailboxDataStore.Read<T>(identity as SearchObjectId);
			}
			return result;
		}

		// Token: 0x06007470 RID: 29808 RVA: 0x002054D4 File Offset: 0x002036D4
		public override IConfigurable[] Find<T>(QueryFilter filter, ObjectId rootId, bool deepSearch, SortBy sortBy)
		{
			MailboxDataProvider.Tracer.TraceDebug<QueryFilter>((long)this.GetHashCode(), "Finding search object that match filter '{0}'", filter);
			if (filter != null && !(filter is TextFilter))
			{
				throw new ArgumentException("filter");
			}
			IEnumerable<T> enumerable = null;
			using (MailboxDataStore mailboxDataStore = this.OpenMailboxStore())
			{
				enumerable = mailboxDataStore.FindPaged<T>(filter as TextFilter, 0);
			}
			LinkedList<IConfigurable> linkedList = new LinkedList<IConfigurable>();
			foreach (T t in enumerable)
			{
				IConfigurable value = t;
				linkedList.AddLast(value);
				if (linkedList.Count >= 1000)
				{
					break;
				}
			}
			IConfigurable[] array = new IConfigurable[linkedList.Count];
			linkedList.CopyTo(array, 0);
			return array;
		}

		// Token: 0x06007471 RID: 29809 RVA: 0x002055B0 File Offset: 0x002037B0
		public override IEnumerable<T> FindPaged<T>(QueryFilter filter, ObjectId rootId, bool deepSearch, SortBy sortBy, int pageSize)
		{
			MailboxDataProvider.Tracer.TraceDebug<QueryFilter>((long)this.GetHashCode(), "Finding search object that match filter {0}", filter);
			if (filter != null && !(filter is TextFilter))
			{
				throw new ArgumentException("filter");
			}
			IEnumerable<T> result;
			using (MailboxDataStore mailboxDataStore = this.OpenMailboxStore())
			{
				result = mailboxDataStore.FindPaged<T>(filter as TextFilter, pageSize);
			}
			return result;
		}

		// Token: 0x06007472 RID: 29810 RVA: 0x00205620 File Offset: 0x00203820
		public override void Save(IConfigurable instance)
		{
			this.Save(instance as SearchObjectBase);
		}

		// Token: 0x06007473 RID: 29811 RVA: 0x0020562E File Offset: 0x0020382E
		public override void Delete(IConfigurable instance)
		{
			this.Delete(instance as SearchObjectBase);
		}

		// Token: 0x0400513C RID: 20796
		public const string Ediscovery = "SystemMailbox{e0dc1c29-89c3-4034-b678-e6c29d823ed9}";

		// Token: 0x0400513D RID: 20797
		protected static readonly Trace Tracer = ExTraceGlobals.SearchTracer;

		// Token: 0x0400513E RID: 20798
		private readonly IRecipientSession recipientSession;

		// Token: 0x0400513F RID: 20799
		internal static readonly QueryFilter DiscoverySystemMailboxFilter = new AndFilter(new QueryFilter[]
		{
			new TextFilter(ADObjectSchema.Name, "SystemMailbox{e0dc1c29-89c3-4034-b678-e6c29d823ed9}", MatchOptions.FullString, MatchFlags.IgnoreCase),
			new TextFilter(ADRecipientSchema.Alias, "SystemMailbox{e0dc1c29-89c3-4034-b678-e6c29d823ed9}", MatchOptions.FullString, MatchFlags.IgnoreCase),
			new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.RecipientType, RecipientType.UserMailbox)
		});

		// Token: 0x04005140 RID: 20800
		internal static readonly QueryFilter DiscoveryUserMailboxFilter = new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.RecipientTypeDetails, RecipientTypeDetails.DiscoveryMailbox);
	}
}
