using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Security.Principal;
using Microsoft.Exchange.Data.Directory.EventLog;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Sync;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Directory;

namespace Microsoft.Exchange.Data.Directory.Recipient
{
	// Token: 0x02000204 RID: 516
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal abstract class ADRecipientObjectSession : ADDataSession, IRecipientSession, IDirectorySession, IConfigDataProvider
	{
		// Token: 0x06001ADB RID: 6875 RVA: 0x00070B34 File Offset: 0x0006ED34
		public ADRecipientObjectSession(bool useConfigNC, bool readOnly, ConsistencyMode consistencyMode, NetworkCredential networkCredential, ADSessionSettings sessionSettings) : base(useConfigNC, readOnly, consistencyMode, networkCredential, sessionSettings)
		{
		}

		// Token: 0x06001ADC RID: 6876 RVA: 0x00070B44 File Offset: 0x0006ED44
		public ADRecipientObjectSession(string domainController, ADObjectId searchRoot, int lcid, bool readOnly, ConsistencyMode consistencyMode, NetworkCredential networkCredential, ADSessionSettings sessionSettings) : base(false, readOnly, consistencyMode, networkCredential, sessionSettings)
		{
			base.DomainController = domainController;
			if (searchRoot != null && (searchRoot.IsDescendantOf(ADSession.GetConfigurationNamingContext(sessionSettings.GetAccountOrResourceForestFqdn())) || searchRoot.IsDescendantOf(ADSession.GetConfigurationUnitsRoot(sessionSettings.GetAccountOrResourceForestFqdn()))))
			{
				this.addressListMembershipFilter = new AndFilter(new QueryFilter[]
				{
					new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.AddressListMembership, searchRoot),
					new ExistsFilter(ADRecipientSchema.DisplayName)
				});
			}
			else
			{
				this.SetSearchRoot(searchRoot);
			}
			base.Lcid = lcid;
			base.UseGlobalCatalog = base.ReadOnly;
			base.EnforceContainerizedScoping = true;
		}

		// Token: 0x17000650 RID: 1616
		// (get) Token: 0x06001ADD RID: 6877 RVA: 0x00070BE4 File Offset: 0x0006EDE4
		public new ADObjectId SearchRoot
		{
			get
			{
				return base.SearchRoot;
			}
		}

		// Token: 0x06001ADE RID: 6878 RVA: 0x00070BEC File Offset: 0x0006EDEC
		protected void SetSearchRoot(ADObjectId searchRoot)
		{
			base.SearchRoot = searchRoot;
		}

		// Token: 0x06001ADF RID: 6879 RVA: 0x00070BF5 File Offset: 0x0006EDF5
		public bool IsReducedRecipientSession()
		{
			return this.isReducedRecipientSession;
		}

		// Token: 0x06001AE0 RID: 6880 RVA: 0x00070BFD File Offset: 0x0006EDFD
		protected void CheckConfigScopeParameter(ConfigScopes configScope)
		{
			if (ConfigScopes.TenantSubTree != configScope && ConfigScopes.TenantLocal != configScope)
			{
				throw new NotSupportedException("Only ConfigScopes.TenantSubTree or ConfigScopes.TenantLocal");
			}
		}

		// Token: 0x06001AE1 RID: 6881 RVA: 0x00070C12 File Offset: 0x0006EE12
		protected override ADObject CreateAndInitializeObject<TResult>(ADPropertyBag propertyBag, ADRawEntry dummyObject)
		{
			if (!this.isReducedRecipientSession)
			{
				return ADObjectFactory.CreateAndInitializeRecipientObject<TResult>(propertyBag, dummyObject, this);
			}
			return ADObjectFactory.CreateAndInitializeObject<TResult>(propertyBag, this);
		}

		// Token: 0x06001AE2 RID: 6882 RVA: 0x00070C2C File Offset: 0x0006EE2C
		public virtual object Clone()
		{
			return base.MemberwiseClone();
		}

		// Token: 0x06001AE3 RID: 6883 RVA: 0x00070C34 File Offset: 0x0006EE34
		public ADRecipient[] Find(ADObjectId rootId, QueryScope scope, QueryFilter filter, SortBy sortBy, int maxResults)
		{
			return base.Find<ADRecipient>(rootId, scope, filter, sortBy, maxResults, null, false);
		}

		// Token: 0x06001AE4 RID: 6884 RVA: 0x00070C45 File Offset: 0x0006EE45
		public ADUser[] FindADUser(ADObjectId rootId, QueryScope scope, QueryFilter filter, SortBy sortBy, int maxResults)
		{
			return base.Find<ADUser>(rootId, scope, filter, sortBy, maxResults, null, false);
		}

		// Token: 0x06001AE5 RID: 6885 RVA: 0x00070C58 File Offset: 0x0006EE58
		public ADUser FindADUserByObjectId(ADObjectId adObjectId)
		{
			ADUser[] array = this.FindADUser(adObjectId, QueryScope.Base, null, null, 1);
			if (array == null || array.Length != 1)
			{
				return null;
			}
			return array[0];
		}

		// Token: 0x06001AE6 RID: 6886 RVA: 0x00070C80 File Offset: 0x0006EE80
		public ADUser FindADUserByExternalDirectoryObjectId(string externalDirectoryObjectId)
		{
			QueryFilter filter = ADRecipientObjectSession.QueryFilterFromExternalDirectoryObjectId(externalDirectoryObjectId);
			ADUser[] array = base.Find<ADUser>(null, QueryScope.SubTree, filter, null, 1, null, false);
			if (array == null || array.Length != 1)
			{
				return null;
			}
			return array[0];
		}

		// Token: 0x06001AE7 RID: 6887 RVA: 0x00070CB0 File Offset: 0x0006EEB0
		public Result<ADRawEntry>[] FindByExternalDirectoryObjectIds(string[] externalDirectoryObjectIds, params PropertyDefinition[] properties)
		{
			return this.FindByExternalDirectoryObjectIds(externalDirectoryObjectIds, false, properties);
		}

		// Token: 0x06001AE8 RID: 6888 RVA: 0x00070CBC File Offset: 0x0006EEBC
		public Result<ADRawEntry>[] FindByExternalDirectoryObjectIds(string[] externalDirectoryObjectIds, bool includeDeletedObjects, params PropertyDefinition[] properties)
		{
			if (externalDirectoryObjectIds == null)
			{
				throw new ArgumentNullException("externalDirectoryObjectIds");
			}
			if (externalDirectoryObjectIds.Length == 0)
			{
				return new Result<ADRawEntry>[0];
			}
			if (properties == null)
			{
				properties = new PropertyDefinition[2];
			}
			else
			{
				Array.Resize<PropertyDefinition>(ref properties, properties.Length + 2);
			}
			properties[properties.Length - 2] = ADRecipientSchema.ExternalDirectoryObjectId;
			properties[properties.Length - 1] = ADObjectSchema.WhenCreatedUTC;
			return this.ReadMultipleRecipientsWithDeletedObjects<string>(externalDirectoryObjectIds, new Converter<string, QueryFilter>(ADRecipientObjectSession.QueryFilterFromExternalDirectoryObjectId), SyncRecipient.SyncRecipientObjectTypeFilter, new ADDataSession.HashInserter<ADRawEntry>(ADRecipientObjectSession.FindByExternalDirectoryObjectIdsHashInserter<ADRawEntry>), new ADDataSession.HashLookup<string, ADRawEntry>(ADRecipientObjectSession.FindByExternalDirectoryObjectIdsHashLookup<ADRawEntry>), properties, includeDeletedObjects);
		}

		// Token: 0x06001AE9 RID: 6889 RVA: 0x00070D46 File Offset: 0x0006EF46
		private static QueryFilter QueryFilterFromExternalDirectoryObjectId(string externalDirectoryObjectId)
		{
			return new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.ExternalDirectoryObjectId, externalDirectoryObjectId);
		}

		// Token: 0x06001AEA RID: 6890 RVA: 0x00070D54 File Offset: 0x0006EF54
		private static void FindByExternalDirectoryObjectIdsHashInserter<TResult>(Hashtable hash, TResult entry) where TResult : ADRawEntry
		{
			Result<TResult> result = new Result<TResult>(entry, null);
			string key = ((string)entry.propertyBag[ADRecipientSchema.ExternalDirectoryObjectId]).ToLowerInvariant();
			if (hash.ContainsKey(key))
			{
				if ((DateTime)entry.propertyBag[ADObjectSchema.WhenCreatedUTC] > (DateTime)((Result<TResult>)hash[key]).Data.propertyBag[ADObjectSchema.WhenCreatedUTC])
				{
					hash[key] = result;
					return;
				}
			}
			else
			{
				hash.Add(key, result);
			}
		}

		// Token: 0x06001AEB RID: 6891 RVA: 0x00070DFC File Offset: 0x0006EFFC
		private static Result<TResult> FindByExternalDirectoryObjectIdsHashLookup<TResult>(Hashtable hash, string key) where TResult : ADRawEntry
		{
			string key2 = key.ToLowerInvariant();
			if (hash.ContainsKey(key2))
			{
				return (Result<TResult>)hash[key2];
			}
			return new Result<TResult>(default(TResult), ProviderError.NotFound);
		}

		// Token: 0x06001AEC RID: 6892 RVA: 0x00070E39 File Offset: 0x0006F039
		public ADRawEntry[] FindRecipient(ADObjectId rootId, QueryScope scope, QueryFilter filter, SortBy sortBy, int maxResults, IEnumerable<PropertyDefinition> properties)
		{
			return base.Find<ADRawEntry>(rootId, scope, filter, sortBy, maxResults, properties, false);
		}

		// Token: 0x06001AED RID: 6893 RVA: 0x00070E4B File Offset: 0x0006F04B
		public MiniRecipient[] FindMiniRecipient(ADObjectId rootId, QueryScope scope, QueryFilter filter, SortBy sortBy, int maxResults, IEnumerable<PropertyDefinition> properties)
		{
			return base.Find<MiniRecipient>(rootId, scope, filter, sortBy, maxResults, properties, false);
		}

		// Token: 0x06001AEE RID: 6894 RVA: 0x00070E5D File Offset: 0x0006F05D
		public ADPagedReader<ADRecipient> FindPaged(ADObjectId rootId, QueryScope scope, QueryFilter filter, SortBy sortBy, int pageSize)
		{
			return base.FindPaged<ADRecipient>(rootId, scope, filter, sortBy, pageSize, null);
		}

		// Token: 0x06001AEF RID: 6895 RVA: 0x00070E6D File Offset: 0x0006F06D
		public ADPagedReader<TEntry> FindPagedMiniRecipient<TEntry>(ADObjectId rootId, QueryScope scope, QueryFilter filter, SortBy sortBy, int pageSize, IEnumerable<PropertyDefinition> properties) where TEntry : MiniRecipient, new()
		{
			return base.FindPaged<TEntry>(rootId, scope, filter, sortBy, pageSize, properties);
		}

		// Token: 0x06001AF0 RID: 6896 RVA: 0x00070E7E File Offset: 0x0006F07E
		public ADRecipient Read(ADObjectId entryId)
		{
			return base.InternalRead<ADRecipient>(entryId, null);
		}

		// Token: 0x06001AF1 RID: 6897 RVA: 0x00070E88 File Offset: 0x0006F088
		internal void EnableReducedRecipientSession()
		{
			this.isReducedRecipientSession = true;
		}

		// Token: 0x06001AF2 RID: 6898 RVA: 0x00070E91 File Offset: 0x0006F091
		public MiniRecipient ReadMiniRecipient(ADObjectId entryId, IEnumerable<PropertyDefinition> properties)
		{
			return this.ReadMiniRecipient<MiniRecipient>(entryId, properties);
		}

		// Token: 0x06001AF3 RID: 6899 RVA: 0x00070E9B File Offset: 0x0006F09B
		public TMiniRecipient ReadMiniRecipient<TMiniRecipient>(ADObjectId entryId, IEnumerable<PropertyDefinition> properties) where TMiniRecipient : ADObject, new()
		{
			if (!typeof(MiniRecipient).IsAssignableFrom(typeof(TMiniRecipient)))
			{
				throw new InvalidOperationException("Object should be minirecipient");
			}
			return base.InternalRead<TMiniRecipient>(entryId, properties);
		}

		// Token: 0x06001AF4 RID: 6900 RVA: 0x00070ECB File Offset: 0x0006F0CB
		public ADRawEntry FindUserBySid(SecurityIdentifier sId, IList<PropertyDefinition> properties)
		{
			if (properties != null)
			{
				return this.FindADRawEntryBySid(sId, properties);
			}
			return this.FindBySid(sId);
		}

		// Token: 0x06001AF5 RID: 6901 RVA: 0x00070EE0 File Offset: 0x0006F0E0
		public void Save(ADRecipient instanceToSave)
		{
			base.Save(instanceToSave, ADRecipientProperties.Instance.AllProperties);
		}

		// Token: 0x06001AF6 RID: 6902 RVA: 0x00070EF3 File Offset: 0x0006F0F3
		public void Save(ADRecipient instanceToSave, bool bypassValidation)
		{
			base.Save(instanceToSave, ADRecipientProperties.Instance.AllProperties, bypassValidation);
		}

		// Token: 0x06001AF7 RID: 6903 RVA: 0x00070F07 File Offset: 0x0006F107
		public void Delete(ADRecipient instanceToDelete)
		{
			base.Delete(instanceToDelete, instanceToDelete is ADUser);
		}

		// Token: 0x06001AF8 RID: 6904 RVA: 0x00070F19 File Offset: 0x0006F119
		IEnumerable<T> IConfigDataProvider.FindPaged<T>(QueryFilter filter, ObjectId rootId, bool deepSearch, SortBy sortBy, int pageSize)
		{
			return base.FindPaged<T>((ADObjectId)rootId, deepSearch ? QueryScope.SubTree : QueryScope.OneLevel, filter, sortBy, pageSize, null);
		}

		// Token: 0x06001AF9 RID: 6905 RVA: 0x00070F34 File Offset: 0x0006F134
		IConfigurable IConfigDataProvider.Read<T>(ObjectId identity)
		{
			if (this.isReducedRecipientSession)
			{
				return base.InternalRead<ReducedRecipient>((ADObjectId)identity, null);
			}
			if (!typeof(ADRecipient).IsAssignableFrom(typeof(T)))
			{
				throw new ArgumentException(DirectoryStrings.ErrorWrongTypeParameter);
			}
			ADRecipient adrecipient = this.Read((ADObjectId)identity);
			if (!(adrecipient is T))
			{
				return null;
			}
			return adrecipient;
		}

		// Token: 0x06001AFA RID: 6906 RVA: 0x00070F9C File Offset: 0x0006F19C
		IConfigurable[] IConfigDataProvider.Find<T>(QueryFilter filter, ObjectId rootId, bool deepSearch, SortBy sortBy)
		{
			if (this.isReducedRecipientSession)
			{
				return (IConfigurable[])this.Find((ADObjectId)rootId, deepSearch ? QueryScope.SubTree : QueryScope.OneLevel, filter, sortBy, 0);
			}
			if (!typeof(ADRecipient).IsAssignableFrom(typeof(T)))
			{
				throw new ArgumentException(DirectoryStrings.ErrorWrongTypeParameter);
			}
			QueryFilter queryFilter = filter;
			if (typeof(ADRecipient) != typeof(T))
			{
				ADObject adobject = ((default(T) == null) ? Activator.CreateInstance<T>() : default(T)) as ADObject;
				queryFilter = ((queryFilter == null) ? adobject.ImplicitFilter : new AndFilter(new QueryFilter[]
				{
					adobject.ImplicitFilter,
					queryFilter
				}));
			}
			return (IConfigurable[])this.Find((ADObjectId)rootId, deepSearch ? QueryScope.SubTree : QueryScope.OneLevel, queryFilter, sortBy, 0);
		}

		// Token: 0x06001AFB RID: 6907 RVA: 0x00071087 File Offset: 0x0006F287
		void IConfigDataProvider.Save(IConfigurable instance)
		{
			if (this.isReducedRecipientSession)
			{
				throw new NotSupportedException("The Reduced RecipientSession should never be used to save an object");
			}
			this.Save((ADRecipient)instance);
		}

		// Token: 0x06001AFC RID: 6908 RVA: 0x000710A8 File Offset: 0x0006F2A8
		void IConfigDataProvider.Delete(IConfigurable instance)
		{
			if (this.isReducedRecipientSession)
			{
				throw new NotSupportedException("The Reduced RecipientSession should never be used to delete an object");
			}
			this.Delete((ADRecipient)instance);
		}

		// Token: 0x17000651 RID: 1617
		// (get) Token: 0x06001AFD RID: 6909 RVA: 0x000710C9 File Offset: 0x0006F2C9
		string IConfigDataProvider.Source
		{
			get
			{
				return base.LastUsedDc;
			}
		}

		// Token: 0x06001AFE RID: 6910 RVA: 0x000710D1 File Offset: 0x0006F2D1
		public ADRecipient FindByProxyAddress(ProxyAddress proxyAddress)
		{
			return this.FindByProxyAddress<ADRecipient>(proxyAddress, null);
		}

		// Token: 0x06001AFF RID: 6911 RVA: 0x000710DB File Offset: 0x0006F2DB
		public ADRawEntry FindByProxyAddress(ProxyAddress proxyAddress, IEnumerable<PropertyDefinition> properties)
		{
			return this.FindByProxyAddress<ADRawEntry>(proxyAddress, properties);
		}

		// Token: 0x06001B00 RID: 6912 RVA: 0x000710E5 File Offset: 0x0006F2E5
		public TEntry FindByProxyAddress<TEntry>(ProxyAddress proxyAddress) where TEntry : ADObject, new()
		{
			return this.FindByProxyAddress<TEntry>(proxyAddress, null);
		}

		// Token: 0x06001B01 RID: 6913 RVA: 0x000710F0 File Offset: 0x0006F2F0
		public ADRecipient FindByLegacyExchangeDN(string legacyExchangeDN)
		{
			if (legacyExchangeDN == null)
			{
				throw new ArgumentNullException("legacyExchangeDN");
			}
			if (string.IsNullOrEmpty(legacyExchangeDN))
			{
				throw new ArgumentException(DirectoryStrings.ExEmptyStringArgumentException("legacyExchangeDN"), "legacyExchangeDN");
			}
			QueryFilter filter = ADRecipientObjectSession.FindByLegacyExchangeDNsFilterBuilder(legacyExchangeDN);
			ADRecipient[] array = this.Find(null, QueryScope.SubTree, filter, null, 2);
			switch (array.Length)
			{
			case 0:
				if (base.SessionSettings.ConfigReadScope == null || base.SessionSettings.ConfigReadScope.Root == null || this.SearchRoot != null)
				{
					return null;
				}
				array = this.Find(base.SessionSettings.ConfigReadScope.Root, QueryScope.SubTree, filter, null, 2);
				switch (array.Length)
				{
				case 0:
					return null;
				case 1:
					return array[0];
				default:
					throw new NonUniqueRecipientException(legacyExchangeDN, new NonUniqueLegacyExchangeDNError(DirectoryStrings.ErrorNonUniqueLegacyDN(legacyExchangeDN), array[0].Id, string.Empty));
				}
				break;
			case 1:
				return array[0];
			default:
				throw new NonUniqueRecipientException(legacyExchangeDN, new NonUniqueLegacyExchangeDNError(DirectoryStrings.ErrorNonUniqueLegacyDN(legacyExchangeDN), array[0].Id, string.Empty));
			}
		}

		// Token: 0x06001B02 RID: 6914 RVA: 0x000711F4 File Offset: 0x0006F3F4
		public ADRecipient[] FindByANR(string anrMatch, int maxResults, SortBy sortBy)
		{
			if (string.IsNullOrEmpty(anrMatch))
			{
				throw new ADFilterException(DirectoryStrings.InvalidAnrFilter);
			}
			QueryFilter filter = new AndFilter(new QueryFilter[]
			{
				new AmbiguousNameResolutionFilter(anrMatch),
				new ExistsFilter(ADRecipientSchema.AddressListMembership),
				new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.HiddenFromAddressListsEnabled, false)
			});
			return this.Find(null, QueryScope.SubTree, filter, sortBy, maxResults);
		}

		// Token: 0x06001B03 RID: 6915 RVA: 0x0007125C File Offset: 0x0006F45C
		public ADRawEntry[] FindByANR(string anrMatch, int maxResults, SortBy sortBy, IEnumerable<PropertyDefinition> properties)
		{
			if (string.IsNullOrEmpty(anrMatch))
			{
				throw new ADFilterException(DirectoryStrings.InvalidAnrFilter);
			}
			QueryFilter filter = new AndFilter(new QueryFilter[]
			{
				new AmbiguousNameResolutionFilter(anrMatch),
				new ExistsFilter(ADRecipientSchema.AddressListMembership),
				new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.HiddenFromAddressListsEnabled, false)
			});
			return base.Find(null, QueryScope.SubTree, filter, sortBy, maxResults, properties);
		}

		// Token: 0x06001B04 RID: 6916 RVA: 0x000712C4 File Offset: 0x0006F4C4
		public MiniRecipient[] FindMiniRecipientByANR(string anrMatch, int maxResults, SortBy sortBy, IEnumerable<PropertyDefinition> properties)
		{
			if (string.IsNullOrEmpty(anrMatch))
			{
				throw new ADFilterException(DirectoryStrings.InvalidAnrFilter);
			}
			QueryFilter filter = new AndFilter(new QueryFilter[]
			{
				new AmbiguousNameResolutionFilter(anrMatch),
				new ExistsFilter(ADRecipientSchema.AddressListMembership)
			});
			return base.Find<MiniRecipient>(null, QueryScope.SubTree, filter, sortBy, maxResults, properties, false);
		}

		// Token: 0x06001B05 RID: 6917 RVA: 0x00071318 File Offset: 0x0006F518
		public ADRawEntry[] FindADRawEntryByUsnRange(ADObjectId root, long startUsn, long endUsn, int sizeLimit, IEnumerable<PropertyDefinition> properties, QueryScope scope, QueryFilter additionalFilter)
		{
			if (sizeLimit > ADDataSession.RangedValueDefaultPageSize)
			{
				throw new ArgumentOutOfRangeException("sizeLimit");
			}
			if (endUsn < startUsn)
			{
				throw new ArgumentOutOfRangeException("endUsn");
			}
			List<QueryFilter> list = new List<QueryFilter>(3);
			list.Add(new ComparisonFilter(ComparisonOperator.GreaterThanOrEqual, ADRecipientSchema.UsnChanged, startUsn));
			if (endUsn != 9223372036854775807L)
			{
				list.Add(new ComparisonFilter(ComparisonOperator.LessThanOrEqual, ADRecipientSchema.UsnChanged, endUsn));
			}
			if (additionalFilter != null)
			{
				list.Add(additionalFilter);
			}
			return base.Find<ADRawEntry>(root, scope, (list.Count == 1) ? list[0] : new AndFilter(list.ToArray()), ADDataSession.SortByUsn, sizeLimit, properties, false);
		}

		// Token: 0x06001B06 RID: 6918 RVA: 0x000713C8 File Offset: 0x0006F5C8
		public ADRawEntry[] FindDeletedADRawEntryByUsnRange(ADObjectId lastKnownParentId, long startUsn, int sizeLimit, IEnumerable<PropertyDefinition> properties, QueryFilter additionalFilter)
		{
			if (sizeLimit > ADDataSession.RangedValueDefaultPageSize)
			{
				throw new ArgumentOutOfRangeException("sizeLimit");
			}
			QueryFilter queryFilter = new AndFilter(new QueryFilter[]
			{
				new ComparisonFilter(ComparisonOperator.GreaterThanOrEqual, ADRecipientSchema.UsnChanged, startUsn),
				new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.OrganizationalUnitRoot, lastKnownParentId)
			});
			if (additionalFilter != null)
			{
				queryFilter = new AndFilter(new QueryFilter[]
				{
					queryFilter,
					additionalFilter
				});
			}
			ADObjectId deletedObjectsContainer = ADSession.GetDeletedObjectsContainer(lastKnownParentId.DomainId);
			return base.Find<ADRawEntry>(deletedObjectsContainer, QueryScope.OneLevel, queryFilter, ADDataSession.SortByUsn, sizeLimit, properties, true);
		}

		// Token: 0x06001B07 RID: 6919 RVA: 0x00071452 File Offset: 0x0006F652
		public Result<ADRecipient>[] ReadMultiple(ADObjectId[] entryIds)
		{
			return base.ReadMultiple<ADObjectId, ADRecipient>(entryIds, new Converter<ADObjectId, QueryFilter>(ADRecipientObjectSession.ADObjectIdFilterBuilder), new ADDataSession.HashInserter<ADRecipient>(ADRecipientObjectSession.ADObjectIdHashInserter<ADRecipient>), new ADDataSession.HashLookup<ADObjectId, ADRecipient>(ADRecipientObjectSession.ADObjectIdHashLookup<ADRecipient>), null);
		}

		// Token: 0x06001B08 RID: 6920 RVA: 0x00071480 File Offset: 0x0006F680
		public Result<ADUser>[] ReadMultipleADUsers(ADObjectId[] userIds)
		{
			return base.ReadMultiple<ADObjectId, ADUser>(userIds, new Converter<ADObjectId, QueryFilter>(ADRecipientObjectSession.ADObjectIdFilterBuilder), new ADDataSession.HashInserter<ADUser>(ADRecipientObjectSession.ADObjectIdHashInserter<ADUser>), new ADDataSession.HashLookup<ADObjectId, ADUser>(ADRecipientObjectSession.ADObjectIdHashLookup<ADUser>), null);
		}

		// Token: 0x06001B09 RID: 6921 RVA: 0x000714AE File Offset: 0x0006F6AE
		public Result<ADRawEntry>[] ReadMultiple(ADObjectId[] entryIds, params PropertyDefinition[] properties)
		{
			return base.ReadMultiple<ADObjectId, ADRawEntry>(entryIds, new Converter<ADObjectId, QueryFilter>(ADRecipientObjectSession.ADObjectIdFilterBuilder), new ADDataSession.HashInserter<ADRawEntry>(ADRecipientObjectSession.ADObjectIdHashInserter<ADRawEntry>), new ADDataSession.HashLookup<ADObjectId, ADRawEntry>(ADRecipientObjectSession.ADObjectIdHashLookup<ADRawEntry>), properties);
		}

		// Token: 0x06001B0A RID: 6922 RVA: 0x000714DC File Offset: 0x0006F6DC
		public Result<ADRawEntry>[] ReadMultipleWithDeletedObjects(ADObjectId[] entryIds, params PropertyDefinition[] properties)
		{
			return this.ReadMultipleRecipientsWithDeletedObjects<ADObjectId>(entryIds, new Converter<ADObjectId, QueryFilter>(ADRecipientObjectSession.ADObjectIdFilterBuilder), null, new ADDataSession.HashInserter<ADRawEntry>(ADRecipientObjectSession.ADObjectIdHashInserter<ADRawEntry>), new ADDataSession.HashLookup<ADObjectId, ADRawEntry>(ADRecipientObjectSession.ADObjectIdHashLookup<ADRawEntry>), properties, true);
		}

		// Token: 0x06001B0B RID: 6923 RVA: 0x0007151C File Offset: 0x0006F71C
		protected Result<ADRawEntry>[] ReadMultipleRecipientsWithDeletedObjects<TKey>(TKey[] keys, Converter<TKey, QueryFilter> filterBuilder, QueryFilter commonFilter, ADDataSession.HashInserter<ADRawEntry> hashInserter, ADDataSession.HashLookup<TKey, ADRawEntry> hashLookup, PropertyDefinition[] properties, bool includeDeletedObjects)
		{
			Result<ADRawEntry>[] array = base.ReadMultiple<TKey, ADRawEntry>(keys, null, filterBuilder, commonFilter, hashInserter, hashLookup, properties, includeDeletedObjects, false);
			if (includeDeletedObjects && base.SessionSettings.ConfigScopes != ConfigScopes.RootOrg && (from x in array
			where x.Error == ProviderError.NotFound
			select x).Any<Result<ADRawEntry>>())
			{
				Result<ADRawEntry>[] array2 = base.ReadMultiple<TKey, ADRawEntry>(keys, ADSession.GetDeletedObjectsContainer(base.GetRootDomainNamingContext()), filterBuilder, commonFilter, hashInserter, hashLookup, properties, true, false);
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i].Data == null && array2[i].Data != null)
					{
						array[i] = array2[i];
					}
				}
			}
			return array;
		}

		// Token: 0x06001B0C RID: 6924 RVA: 0x000715CB File Offset: 0x0006F7CB
		public Result<ADGroup>[] ReadMultipleADGroups(ADObjectId[] entryIds)
		{
			return base.ReadMultiple<ADObjectId, ADGroup>(entryIds, new Converter<ADObjectId, QueryFilter>(ADRecipientObjectSession.ADObjectIdFilterBuilder), new ADDataSession.HashInserter<ADGroup>(ADRecipientObjectSession.ADObjectIdHashInserter<ADGroup>), new ADDataSession.HashLookup<ADObjectId, ADGroup>(ADRecipientObjectSession.ADObjectIdHashLookup<ADGroup>), null);
		}

		// Token: 0x06001B0D RID: 6925 RVA: 0x000715FC File Offset: 0x0006F7FC
		public ITableView Browse(ADObjectId addressListId, int rowCountSuggestion, params PropertyDefinition[] properties)
		{
			ADObjectId[] addressListIds = null;
			if (addressListId != null)
			{
				addressListIds = new ADObjectId[]
				{
					addressListId
				};
			}
			return new ADVirtualListView(this, this.SearchRoot, addressListIds, new SortBy(ADRecipientSchema.DisplayName, SortOrder.Ascending), rowCountSuggestion, properties);
		}

		// Token: 0x06001B0E RID: 6926 RVA: 0x00071638 File Offset: 0x0006F838
		public bool IsMemberOfGroupByWellKnownGuid(Guid wellKnownGuid, string containerDN, ADObjectId id)
		{
			QueryFilter restrictingFilter = new ComparisonFilter(ComparisonOperator.Equal, IADDistributionListSchema.Members, id);
			PropertyDefinition[] props = new PropertyDefinition[]
			{
				ADObjectSchema.Guid
			};
			ADRawEntry adrawEntry = base.ResolveWellKnownGuid<ADRawEntry>(wellKnownGuid, containerDN, restrictingFilter, props);
			return adrawEntry != null;
		}

		// Token: 0x06001B0F RID: 6927 RVA: 0x00071678 File Offset: 0x0006F878
		public bool IsRecipientInOrg(ProxyAddress proxyAddress)
		{
			QueryFilter filter = this.QueryFilterFromProxyAddress(proxyAddress);
			PropertyDefinition[] properties = new PropertyDefinition[]
			{
				ADRecipientSchema.RecipientType,
				ADRecipientSchema.MasterAccountSid
			};
			ADRawEntry[] array = base.Find<ADRawEntry>(null, QueryScope.SubTree, filter, null, 2, properties, false);
			switch (array.Length)
			{
			case 0:
				return false;
			case 1:
			{
				RecipientType recipientType = (RecipientType)array[0][ADRecipientSchema.RecipientType];
				SecurityIdentifier left = array[0][ADRecipientSchema.MasterAccountSid] as SecurityIdentifier;
				return (recipientType != RecipientType.MailContact && recipientType != RecipientType.Contact && recipientType != RecipientType.MailUser) || !(left == null);
			}
			default:
				throw new NonUniqueRecipientException(proxyAddress, new NonUniqueProxyAddressError(DirectoryStrings.ErrorNonUniqueProxy(proxyAddress.ToString()), array[0].Id, string.Empty));
			}
		}

		// Token: 0x06001B10 RID: 6928 RVA: 0x00071734 File Offset: 0x0006F934
		public Result<ADRawEntry>[] FindByProxyAddresses(ProxyAddress[] proxyAddresses, params PropertyDefinition[] properties)
		{
			if (proxyAddresses == null)
			{
				throw new ArgumentNullException("proxyAddresses");
			}
			if (proxyAddresses.Length == 0)
			{
				return new Result<ADRawEntry>[0];
			}
			if (properties == null)
			{
				properties = new PropertyDefinition[2];
			}
			else
			{
				Array.Resize<PropertyDefinition>(ref properties, properties.Length + 2);
			}
			properties[properties.Length - 1] = ADRecipientSchema.LegacyExchangeDN;
			properties[properties.Length - 2] = ADRecipientSchema.EmailAddresses;
			return this.FindByProxyAddresses<ADRawEntry>(proxyAddresses, properties);
		}

		// Token: 0x06001B11 RID: 6929 RVA: 0x00071794 File Offset: 0x0006F994
		public Result<ADRecipient>[] FindByProxyAddresses(ProxyAddress[] proxyAddresses)
		{
			if (proxyAddresses == null)
			{
				throw new ArgumentNullException("proxyAddresses");
			}
			if (proxyAddresses.Length == 0)
			{
				return new Result<ADRecipient>[0];
			}
			return this.FindByProxyAddresses<ADRecipient>(proxyAddresses, null);
		}

		// Token: 0x06001B12 RID: 6930 RVA: 0x000717B8 File Offset: 0x0006F9B8
		public Result<TEntry>[] FindByProxyAddresses<TEntry>(ProxyAddress[] proxyAddresses) where TEntry : ADObject, new()
		{
			if (proxyAddresses == null)
			{
				throw new ArgumentNullException("proxyAddresses");
			}
			if (proxyAddresses.Length == 0)
			{
				return new Result<TEntry>[0];
			}
			return this.FindByProxyAddresses<TEntry>(proxyAddresses, null);
		}

		// Token: 0x06001B13 RID: 6931 RVA: 0x000717DC File Offset: 0x0006F9DC
		public Result<ADRecipient>[] FindByExchangeGuidsIncludingArchive(Guid[] keys)
		{
			if (keys == null)
			{
				throw new ArgumentNullException("keys");
			}
			if (keys.Length == 0)
			{
				return new Result<ADRecipient>[0];
			}
			return this.FindByExchangeGuidsIncludingArchive<ADRecipient>(keys, null);
		}

		// Token: 0x06001B14 RID: 6932 RVA: 0x00071800 File Offset: 0x0006FA00
		private static QueryFilter QueryFilterFromExchangeGuid(Guid exchangeGuid, bool includeAlternative, bool includeArchive)
		{
			QueryFilter queryFilter = new ComparisonFilter(ComparisonOperator.Equal, ADMailboxRecipientSchema.ExchangeGuid, exchangeGuid);
			QueryFilter queryFilter2 = null;
			if (includeAlternative)
			{
				queryFilter2 = new OrFilter(new QueryFilter[]
				{
					new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.MailboxGuidsRaw, exchangeGuid.ToString()),
					new TextFilter(ADUserSchema.AggregatedMailboxGuids, exchangeGuid.ToString(), MatchOptions.Prefix, MatchFlags.Loose)
				});
			}
			QueryFilter queryFilter3 = null;
			if (includeArchive)
			{
				queryFilter3 = new ComparisonFilter(ComparisonOperator.Equal, ADUserSchema.ArchiveGuid, exchangeGuid);
			}
			QueryFilter result = queryFilter;
			if (queryFilter2 != null && queryFilter3 != null)
			{
				result = new OrFilter(new QueryFilter[]
				{
					queryFilter,
					queryFilter2,
					queryFilter3
				});
			}
			else if (queryFilter2 != null)
			{
				result = new OrFilter(new QueryFilter[]
				{
					queryFilter,
					queryFilter2
				});
			}
			else if (queryFilter3 != null)
			{
				result = new OrFilter(new QueryFilter[]
				{
					queryFilter,
					queryFilter3
				});
			}
			return result;
		}

		// Token: 0x06001B15 RID: 6933 RVA: 0x000718EC File Offset: 0x0006FAEC
		private static QueryFilter ConstructRecipientSidFilter(SecurityIdentifier sId)
		{
			return new AndFilter(new QueryFilter[]
			{
				new OrFilter(new QueryFilter[]
				{
					new ComparisonFilter(ComparisonOperator.Equal, ADMailboxRecipientSchema.Sid, sId),
					new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.MasterAccountSid, sId),
					new ComparisonFilter(ComparisonOperator.Equal, ADMailboxRecipientSchema.SidHistory, sId)
				}),
				new ComparisonFilter(ComparisonOperator.NotEqual, ADObjectSchema.ObjectClass, "foreignSecurityPrincipal")
			});
		}

		// Token: 0x06001B16 RID: 6934 RVA: 0x00071958 File Offset: 0x0006FB58
		private static void FindByProxyAddressesHashInserter<TResult>(Hashtable hash, TResult entry) where TResult : ADRawEntry
		{
			Result<TResult> result = new Result<TResult>(entry, null);
			string text = (string)entry.propertyBag[ADRecipientSchema.LegacyExchangeDN];
			if (hash.ContainsKey(text))
			{
				hash[text] = new Result<TResult>(default(TResult), new NonUniqueLegacyExchangeDNError(DirectoryStrings.ErrorNonUniqueLegacyDN(text), entry.Id, string.Empty));
				Globals.LogEvent(DirectoryEventLogConstants.Tuple_DSC_EVENT_NON_UNIQUE_RECIPIENT, text, new object[]
				{
					text
				});
			}
			else
			{
				hash.Add(text, result);
			}
			foreach (ProxyAddress proxyAddress in ((ProxyAddressCollection)entry.propertyBag[ADRecipientSchema.EmailAddresses]))
			{
				string text2 = proxyAddress.ToString();
				if (hash.ContainsKey(text2))
				{
					hash[text2] = new Result<TResult>(default(TResult), new NonUniqueProxyAddressError(DirectoryStrings.ErrorNonUniqueProxy(proxyAddress.ToString()), entry.Id, string.Empty));
					Globals.LogEvent(DirectoryEventLogConstants.Tuple_DSC_EVENT_NON_UNIQUE_RECIPIENT, text2, new object[]
					{
						text2
					});
				}
				else
				{
					hash.Add(text2, result);
				}
			}
		}

		// Token: 0x06001B17 RID: 6935 RVA: 0x00071AC4 File Offset: 0x0006FCC4
		private static Result<TResult> FindByProxyAddressesHashLookup<TResult>(Hashtable hash, ProxyAddress key) where TResult : ADRawEntry
		{
			if (key.Prefix == ProxyAddressPrefix.LegacyDN)
			{
				string valueString = key.ValueString;
				string key2 = "x500:" + valueString;
				object obj = hash[valueString];
				object obj2 = hash[key2];
				object obj3;
				if ((obj3 = (obj ?? obj2)) == null)
				{
					obj3 = new Result<TResult>(default(TResult), ProviderError.NotFound);
				}
				return (Result<TResult>)obj3;
			}
			string key3 = key.ToString();
			if (hash.ContainsKey(key3))
			{
				return (Result<TResult>)hash[key3];
			}
			return new Result<TResult>(default(TResult), ProviderError.NotFound);
		}

		// Token: 0x06001B18 RID: 6936 RVA: 0x00071B64 File Offset: 0x0006FD64
		private static QueryFilter FindByLegacyExchangeDNsFilterBuilder(string legDN)
		{
			return new OrFilter(new QueryFilter[]
			{
				new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.LegacyExchangeDN, legDN),
				new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.EmailAddresses, "x500:" + legDN)
			});
		}

		// Token: 0x06001B19 RID: 6937 RVA: 0x00071BA8 File Offset: 0x0006FDA8
		private static void FindByLegacyExchangeDNsHashInserter<T>(Hashtable hash, T entry) where T : ADRawEntry
		{
			Result<T> result = new Result<T>(entry, null);
			string text = ((string)entry.propertyBag[ADRecipientSchema.LegacyExchangeDN]).ToLowerInvariant();
			if (hash.ContainsKey(text))
			{
				hash[text] = new Result<T>(default(T), new NonUniqueLegacyExchangeDNError(DirectoryStrings.ErrorNonUniqueLegacyDN(text), entry.Id, string.Empty));
			}
			else
			{
				hash.Add(text, result);
			}
			foreach (ProxyAddress proxyAddress in ((ProxyAddressCollection)entry.propertyBag[ADRecipientSchema.EmailAddresses]))
			{
				string text2 = proxyAddress.ToString().ToLowerInvariant();
				if (proxyAddress.Prefix == ProxyAddressPrefix.X500)
				{
					if (hash.ContainsKey(text2))
					{
						hash[text2] = new Result<T>(default(T), new NonUniqueProxyAddressError(DirectoryStrings.ErrorNonUniqueProxy(proxyAddress.ToString()), entry.Id, string.Empty));
						Globals.LogEvent(DirectoryEventLogConstants.Tuple_DSC_EVENT_NON_UNIQUE_RECIPIENT, text2, new object[]
						{
							text2
						});
					}
					else
					{
						hash.Add(text2, result);
					}
				}
			}
		}

		// Token: 0x06001B1A RID: 6938 RVA: 0x00071D18 File Offset: 0x0006FF18
		private static Result<T> FindByLegacyExchangeDNsHashLookup<T>(Hashtable hash, string key) where T : ADRawEntry
		{
			string text = key.ToLowerInvariant();
			string key2 = "x500:" + text;
			object obj = hash[text];
			object obj2 = hash[key2];
			object obj3;
			if ((obj3 = (obj ?? obj2)) == null)
			{
				obj3 = new Result<T>(default(T), ProviderError.NotFound);
			}
			return (Result<T>)obj3;
		}

		// Token: 0x06001B1B RID: 6939 RVA: 0x00071D70 File Offset: 0x0006FF70
		public bool IsLegacyDNInUse(string legacyDN)
		{
			QueryFilter queryFilter = new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.LegacyExchangeDN, legacyDN);
			string proxyAddressString = "x500:" + legacyDN;
			QueryFilter queryFilter2 = new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.EmailAddresses, ProxyAddress.Parse(proxyAddressString));
			QueryFilter filter = new OrFilter(new QueryFilter[]
			{
				queryFilter,
				queryFilter2
			});
			PropertyDefinition[] properties = new PropertyDefinition[]
			{
				ADObjectSchema.Id,
				ADRecipientSchema.LegacyExchangeDN
			};
			ADRawEntry[] array = base.Find<ADRawEntry>(null, QueryScope.SubTree, filter, null, 1, properties, true);
			return array.Length != 0;
		}

		// Token: 0x06001B1C RID: 6940 RVA: 0x00071DFA File Offset: 0x0006FFFA
		internal static QueryFilter ADObjectIdFilterBuilder(ADObjectId id)
		{
			return new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Id, id);
		}

		// Token: 0x06001B1D RID: 6941 RVA: 0x00071E08 File Offset: 0x00070008
		internal static void ADObjectIdHashInserter<T>(Hashtable hash, T entry) where T : ADRawEntry
		{
			hash.Add(entry.Id.DistinguishedName, new Result<T>(entry, null));
			hash.Add(entry.Id.ObjectGuid.ToString(), new Result<T>(entry, null));
		}

		// Token: 0x06001B1E RID: 6942 RVA: 0x00071E6C File Offset: 0x0007006C
		internal static Result<T> ADObjectIdHashLookup<T>(Hashtable hash, ADObjectId key) where T : ADRawEntry
		{
			object obj;
			if (string.IsNullOrEmpty(key.DistinguishedName))
			{
				obj = hash[key.ObjectGuid.ToString()];
			}
			else
			{
				obj = hash[key.DistinguishedName];
			}
			object obj2;
			if ((obj2 = obj) == null)
			{
				obj2 = new Result<T>(default(T), ProviderError.NotFound);
			}
			return (Result<T>)obj2;
		}

		// Token: 0x06001B1F RID: 6943 RVA: 0x00071ED4 File Offset: 0x000700D4
		private TEntry InternalFindByExchangeGuid<TEntry>(Guid exchangeGuid, bool includeAlternative, bool includeArchive, IEnumerable<PropertyDefinition> properties) where TEntry : ADRawEntry, new()
		{
			QueryFilter filter = ADRecipientObjectSession.QueryFilterFromExchangeGuid(exchangeGuid, includeAlternative, includeArchive);
			IEnumerable<PropertyDefinition> enumerable = properties;
			if (includeArchive && enumerable != null)
			{
				enumerable = properties.Concat(new List<PropertyDefinition>
				{
					ADMailboxRecipientSchema.ExchangeGuid,
					SharedPropertyDefinitions.ProvisioningFlags
				});
			}
			TEntry[] array = base.Find<TEntry>(null, QueryScope.SubTree, filter, null, 2, enumerable, false);
			switch (array.Length)
			{
			case 0:
				return default(TEntry);
			case 1:
				return array[0];
			default:
				if (includeArchive)
				{
					foreach (TEntry result in array)
					{
						if (result[ADMailboxRecipientSchema.ExchangeGuid] != null && result[ADRecipientSchema.ProvisioningFlags] != null && ((Guid)result[ADMailboxRecipientSchema.ExchangeGuid]).Equals(exchangeGuid) && ((int)result[ADRecipientSchema.ProvisioningFlags] & 131072) == 131072)
						{
							return result;
						}
					}
				}
				throw new NonUniqueRecipientException(exchangeGuid, new ObjectValidationError(DirectoryStrings.ErrorNonUniqueExchangeGuid(exchangeGuid.ToString()), array[0].Id, string.Empty));
			}
		}

		// Token: 0x06001B20 RID: 6944 RVA: 0x00072030 File Offset: 0x00070230
		protected QueryFilter QueryFilterFromProxyAddress(ProxyAddress proxyAddress)
		{
			if (proxyAddress.Prefix == ProxyAddressPrefix.LegacyDN)
			{
				string addressString = proxyAddress.AddressString;
				string propertyValue = "x500:" + addressString;
				return new OrFilter(new QueryFilter[]
				{
					new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.EmailAddresses, propertyValue),
					new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.LegacyExchangeDN, addressString)
				});
			}
			return new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.EmailAddresses, proxyAddress.ToString());
		}

		// Token: 0x06001B21 RID: 6945 RVA: 0x000720A0 File Offset: 0x000702A0
		private TResult FindByProxyAddress<TResult>(ProxyAddress proxyAddress, IEnumerable<PropertyDefinition> properties) where TResult : ADRawEntry, new()
		{
			QueryFilter filter = this.QueryFilterFromProxyAddress(proxyAddress);
			TResult[] array = base.Find<TResult>(null, QueryScope.SubTree, filter, null, 2, properties, false);
			switch (array.Length)
			{
			case 0:
				if (base.SessionSettings.ConfigReadScope == null || base.SessionSettings.ConfigReadScope.Root == null || this.SearchRoot != null)
				{
					return default(TResult);
				}
				array = base.Find<TResult>(base.SessionSettings.ConfigReadScope.Root, QueryScope.SubTree, filter, null, 2, properties, false);
				switch (array.Length)
				{
				case 0:
					return default(TResult);
				case 1:
					return array[0];
				default:
					throw new NonUniqueRecipientException(proxyAddress, new NonUniqueProxyAddressError(DirectoryStrings.ErrorNonUniqueProxy(proxyAddress.ToString()), array[0].Id, string.Empty));
				}
				break;
			case 1:
				return array[0];
			default:
				throw new NonUniqueRecipientException(proxyAddress, new NonUniqueProxyAddressError(DirectoryStrings.ErrorNonUniqueProxy(proxyAddress.ToString()), array[0].Id, string.Empty));
			}
		}

		// Token: 0x06001B22 RID: 6946 RVA: 0x000721C4 File Offset: 0x000703C4
		private Result<TData>[] FindByExchangeGuidsIncludingArchive<TData>(Guid[] keys, IEnumerable<PropertyDefinition> properties) where TData : ADRawEntry, new()
		{
			Converter<Guid, QueryFilter> filterBuilder = (Guid g) => ADRecipientObjectSession.QueryFilterFromExchangeGuid(g, false, true);
			return base.ReadMultiple<Guid, TData>(keys, filterBuilder, null, null, properties);
		}

		// Token: 0x06001B23 RID: 6947 RVA: 0x000721EC File Offset: 0x000703EC
		private Result<TData>[] FindByProxyAddresses<TData>(ProxyAddress[] keys, IEnumerable<PropertyDefinition> properties) where TData : ADRawEntry, new()
		{
			Converter<ProxyAddress, QueryFilter> filterBuilder = new Converter<ProxyAddress, QueryFilter>(this.QueryFilterFromProxyAddress);
			ADDataSession.HashInserter<TData> hashInserter = new ADDataSession.HashInserter<TData>(ADRecipientObjectSession.FindByProxyAddressesHashInserter<TData>);
			ADDataSession.HashLookup<ProxyAddress, TData> hashLookup = new ADDataSession.HashLookup<ProxyAddress, TData>(ADRecipientObjectSession.FindByProxyAddressesHashLookup<TData>);
			Result<TData>[] array = base.ReadMultiple<ProxyAddress, TData>(keys, filterBuilder, hashInserter, hashLookup, properties);
			if (base.SessionSettings.ConfigReadScope != null && base.SessionSettings.ConfigReadScope.Root != null && this.SearchRoot == null)
			{
				List<ProxyAddress> list = new List<ProxyAddress>();
				List<int> list2 = new List<int>();
				for (int i = 0; i < array.Length; i++)
				{
					Result<TData> result = array[i];
					if (result.Error == ProviderError.NotFound)
					{
						list.Add(keys[i]);
						list2.Add(i);
					}
				}
				if (list.Count > 0)
				{
					Result<TData>[] array2 = base.ReadMultiple<ProxyAddress, TData>(list.ToArray(), base.SessionSettings.ConfigReadScope.Root, filterBuilder, null, hashInserter, hashLookup, properties);
					for (int j = 0; j < array2.Length; j++)
					{
						Result<TData> result2 = array2[j];
						if (result2.Error == null)
						{
							array[list2[j]] = result2;
						}
					}
				}
			}
			return array;
		}

		// Token: 0x06001B24 RID: 6948 RVA: 0x00072320 File Offset: 0x00070520
		public Result<ADRawEntry>[] FindByLegacyExchangeDNs(string[] legacyExchangeDNs, params PropertyDefinition[] properties)
		{
			if (legacyExchangeDNs == null)
			{
				throw new ArgumentNullException("legacyExchangeDNs");
			}
			if (legacyExchangeDNs.Length == 0)
			{
				return new Result<ADRawEntry>[0];
			}
			if (properties == null)
			{
				properties = new PropertyDefinition[2];
			}
			else
			{
				Array.Resize<PropertyDefinition>(ref properties, properties.Length + 2);
			}
			properties[properties.Length - 1] = ADRecipientSchema.LegacyExchangeDN;
			properties[properties.Length - 2] = ADRecipientSchema.EmailAddresses;
			return base.ReadMultiple<string, ADRawEntry>(legacyExchangeDNs, new Converter<string, QueryFilter>(ADRecipientObjectSession.FindByLegacyExchangeDNsFilterBuilder), new ADDataSession.HashInserter<ADRawEntry>(ADRecipientObjectSession.FindByLegacyExchangeDNsHashInserter<ADRawEntry>), new ADDataSession.HashLookup<string, ADRawEntry>(ADRecipientObjectSession.FindByLegacyExchangeDNsHashLookup<ADRawEntry>), properties);
		}

		// Token: 0x06001B25 RID: 6949 RVA: 0x000723A4 File Offset: 0x000705A4
		public Result<ADRecipient>[] FindADRecipientsByLegacyExchangeDNs(string[] legacyExchangeDNs)
		{
			if (legacyExchangeDNs == null)
			{
				throw new ArgumentNullException("legacyExchangeDNs");
			}
			if (legacyExchangeDNs.Length == 0)
			{
				return new Result<ADRecipient>[0];
			}
			return base.ReadMultiple<string, ADRecipient>(legacyExchangeDNs, new Converter<string, QueryFilter>(ADRecipientObjectSession.FindByLegacyExchangeDNsFilterBuilder), new ADDataSession.HashInserter<ADRecipient>(ADRecipientObjectSession.FindByLegacyExchangeDNsHashInserter<ADRecipient>), new ADDataSession.HashLookup<string, ADRecipient>(ADRecipientObjectSession.FindByLegacyExchangeDNsHashLookup<ADRecipient>), null);
		}

		// Token: 0x06001B26 RID: 6950 RVA: 0x000723F8 File Offset: 0x000705F8
		public ADRecipient[] FindNames(IDictionary<PropertyDefinition, object> dictionary, int limit)
		{
			return this.FindNames<ADRecipient>(dictionary, limit, null);
		}

		// Token: 0x06001B27 RID: 6951 RVA: 0x00072410 File Offset: 0x00070610
		public object[][] FindNamesView(IDictionary<PropertyDefinition, object> dictionary, int limit, params PropertyDefinition[] properties)
		{
			ADRawEntry[] recipients = this.FindNames<ADRawEntry>(dictionary, limit, properties);
			return ADSession.ConvertToView(recipients, properties);
		}

		// Token: 0x06001B28 RID: 6952 RVA: 0x00072430 File Offset: 0x00070630
		private TResult[] FindNames<TResult>(IDictionary<PropertyDefinition, object> dictionary, int limit, IEnumerable<PropertyDefinition> properties) where TResult : ADRawEntry, new()
		{
			if (dictionary == null)
			{
				throw new ArgumentNullException("dictionary");
			}
			int count = dictionary.Keys.Count;
			if (count == 0)
			{
				return new TResult[0];
			}
			int num = 0;
			QueryFilter[] array = new QueryFilter[count];
			foreach (PropertyDefinition propertyDefinition in dictionary.Keys)
			{
				if (dictionary[propertyDefinition] is string)
				{
					array[num] = new TextFilter(propertyDefinition, (string)dictionary[propertyDefinition], MatchOptions.Prefix, MatchFlags.Loose);
				}
				else
				{
					array[num] = new ComparisonFilter(ComparisonOperator.Equal, propertyDefinition, dictionary[propertyDefinition]);
				}
				num++;
			}
			QueryFilter filter = new AndFilter(array);
			SortBy sortBy = new SortBy(ADRecipientSchema.DisplayName, SortOrder.Ascending);
			return base.Find<TResult>(null, QueryScope.SubTree, filter, sortBy, limit, properties, false);
		}

		// Token: 0x06001B29 RID: 6953 RVA: 0x0007250C File Offset: 0x0007070C
		public ADObject FindByAccountName<T>(string domainName, string accountName) where T : IConfigurable, new()
		{
			IEnumerable<T> enumerable = this.FindByAccountName<T>(domainName, accountName, null, null);
			IEnumerator<T> enumerator = enumerable.GetEnumerator();
			if (!enumerator.MoveNext())
			{
				return null;
			}
			ADObject adobject = (ADObject)((object)enumerator.Current);
			if (enumerator.MoveNext())
			{
				throw new NonUniqueRecipientException(domainName + "\\" + accountName, new ObjectValidationError(DirectoryStrings.ErrorNonUniqueDomainAccount(domainName, accountName), adobject.Id, string.Empty));
			}
			return adobject;
		}

		// Token: 0x06001B2A RID: 6954 RVA: 0x00072B08 File Offset: 0x00070D08
		public IEnumerable<T> FindByAccountName<T>(string domain, string account, ADObjectId rootId, QueryFilter searchFilter) where T : IConfigurable, new()
		{
			if (account == null)
			{
				throw new ArgumentNullException("account");
			}
			if (account.Length == 0)
			{
				throw new ArgumentException(DirectoryStrings.ExceptionInvalidAccountName(account));
			}
			bool isNetbiosCompatible = account.Contains("\\");
			if (isNetbiosCompatible)
			{
				string[] array = account.Split(new char[]
				{
					'\\'
				});
				if (array.Length > 2)
				{
					isNetbiosCompatible = false;
				}
				else if (!string.IsNullOrEmpty(domain) && !domain.Equals(array[0], StringComparison.OrdinalIgnoreCase))
				{
					isNetbiosCompatible = false;
				}
				else
				{
					domain = array[0];
					account = array[1];
				}
			}
			else
			{
				isNetbiosCompatible = !string.IsNullOrEmpty(domain);
			}
			ComparisonFilter accountFilter = new ComparisonFilter(ComparisonOperator.Equal, IADSecurityPrincipalSchema.SamAccountName, account);
			searchFilter = QueryFilter.AndTogether(new QueryFilter[]
			{
				searchFilter,
				accountFilter
			});
			IEnumerable<T> results = ((IConfigDataProvider)this).FindPaged<T>(searchFilter, rootId, true, null, 0);
			IEnumerator<T> enumRes = results.GetEnumerator();
			if (!string.IsNullOrEmpty(domain))
			{
				bool found = false;
				bool isFqdn = domain.Contains(".");
				ADObjectId domainId = null;
				AdName domainRdn = null;
				if (isFqdn)
				{
					domainId = new ADObjectId(NativeHelpers.DistinguishedNameFromCanonicalName(domain));
				}
				else
				{
					domainRdn = new AdName("DC", domain);
				}
				while (enumRes.MoveNext())
				{
					!0 ! = enumRes.Current;
					ADObject recipient = (ADObject)((object)!);
					bool match = isFqdn ? ADObjectId.Equals(recipient.Id.DomainId, domainId) : (recipient.Id.DomainId.Rdn == domainRdn);
					if (match)
					{
						found = true;
						yield return enumRes.Current;
					}
				}
				if (!found)
				{
					ExTraceGlobals.ADReadDetailsTracer.TraceDebug<string>((long)this.GetHashCode(), "ADRecipientObjectSession::FindByAccountName - None of recipients matched specified domain. Trying to resolve domain name {0} using AD. This has performance impact. If you have domain with Netbios name that does not match left part of DNS name consider renaming it to improve performance.", domain);
					ITopologyConfigurationSession configSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(base.DomainController, base.ReadOnly, base.ConsistencyMode, base.NetworkCredential, ADSessionSettings.FromAccountPartitionRootOrgScopeSet(base.SessionSettings.PartitionId), 2145, "FindByAccountName", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\Recipient\\ADRecipientObjectSession.cs");
					ADCrossRef[] crossRefs = configSession.FindADCrossRefByNetBiosName(domain);
					if (crossRefs.Length > 0)
					{
						results = ((IConfigDataProvider)this).FindPaged<T>(searchFilter, rootId, true, null, 0);
						enumRes = results.GetEnumerator();
						ADCrossRef crossRef = crossRefs[0];
						while (enumRes.MoveNext())
						{
							!0 !2 = enumRes.Current;
							ADObject recipient2 = (ADObject)((object)!2);
							if (ADObjectId.Equals(recipient2.Id.DomainId, crossRef.NCName))
							{
								yield return enumRes.Current;
							}
						}
					}
				}
			}
			else
			{
				foreach (T item in results)
				{
					yield return item;
				}
			}
			yield break;
		}

		// Token: 0x06001B2B RID: 6955 RVA: 0x00072B44 File Offset: 0x00070D44
		public IEnumerable<ADGroup> FindRoleGroupsByForeignGroupSid(ADObjectId root, SecurityIdentifier sId)
		{
			if (sId == null)
			{
				throw new ArgumentNullException("sId");
			}
			QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, ADGroupSchema.ForeignGroupSid, sId);
			return base.FindPaged<ADGroup>(root, QueryScope.SubTree, filter, null, 0, null);
		}

		// Token: 0x06001B2C RID: 6956 RVA: 0x00072B80 File Offset: 0x00070D80
		public ADRawEntry FindADRawEntryBySid(SecurityIdentifier sId, IEnumerable<PropertyDefinition> properties)
		{
			QueryFilter filter = ADRecipientObjectSession.ConstructRecipientSidFilter(sId);
			ADPagedReader<ADRawEntry> adpagedReader = this.FindPagedADRawEntryWithDefaultFilters<ADRecipient>(null, QueryScope.SubTree, filter, null, 2, properties);
			ADRawEntry adrawEntry = null;
			foreach (ADRawEntry adrawEntry2 in adpagedReader)
			{
				if (adrawEntry != null)
				{
					throw new NonUniqueRecipientException(sId, new ObjectValidationError(DirectoryStrings.ErrorNonUniqueSid(sId.ToString()), adrawEntry.Id, string.Empty));
				}
				adrawEntry = adrawEntry2;
			}
			return adrawEntry;
		}

		// Token: 0x06001B2D RID: 6957 RVA: 0x00072C04 File Offset: 0x00070E04
		public TResult FindMiniRecipientBySid<TResult>(SecurityIdentifier sId, IEnumerable<PropertyDefinition> properties) where TResult : MiniRecipient, new()
		{
			QueryFilter filter = ADRecipientObjectSession.ConstructRecipientSidFilter(sId);
			ADPagedReader<TResult> adpagedReader = base.FindPaged<TResult>(null, QueryScope.SubTree, filter, null, 2, properties);
			TResult tresult = default(TResult);
			foreach (TResult tresult2 in adpagedReader)
			{
				if (tresult != null)
				{
					throw new NonUniqueRecipientException(sId, new ObjectValidationError(DirectoryStrings.ErrorNonUniqueSid(sId.ToString()), tresult.Id, string.Empty));
				}
				tresult = tresult2;
			}
			return tresult;
		}

		// Token: 0x06001B2E RID: 6958 RVA: 0x00072C9C File Offset: 0x00070E9C
		public TResult FindMiniRecipientByProxyAddress<TResult>(ProxyAddress proxyAddress, IEnumerable<PropertyDefinition> properties) where TResult : MiniRecipient, new()
		{
			return this.FindByProxyAddress<TResult>(proxyAddress, properties);
		}

		// Token: 0x06001B2F RID: 6959 RVA: 0x00072CA8 File Offset: 0x00070EA8
		public ADRecipient FindBySid(SecurityIdentifier sId)
		{
			QueryFilter filter = ADRecipientObjectSession.ConstructRecipientSidFilter(sId);
			ADRecipient[] array = this.Find(null, QueryScope.SubTree, filter, null, 2);
			switch (array.Length)
			{
			case 0:
				return null;
			case 1:
				return array[0];
			default:
				throw new NonUniqueRecipientException(sId, new ObjectValidationError(DirectoryStrings.ErrorNonUniqueSid(sId.ToString()), array[0].Id, string.Empty));
			}
		}

		// Token: 0x06001B30 RID: 6960 RVA: 0x00072D08 File Offset: 0x00070F08
		public ADRecipient FindByExchangeObjectId(Guid exchangeObjectId)
		{
			QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ExchangeObjectId, exchangeObjectId);
			ADRecipient[] array = this.Find(null, QueryScope.SubTree, filter, null, 2);
			switch (array.Length)
			{
			case 0:
				return null;
			case 1:
				return array[0];
			default:
				throw new NonUniqueRecipientException(exchangeObjectId, new ObjectValidationError(DirectoryStrings.ErrorNonUniqueExchangeObjectId(exchangeObjectId.ToString()), array[0].Id, string.Empty));
			}
		}

		// Token: 0x06001B31 RID: 6961 RVA: 0x00072D7C File Offset: 0x00070F7C
		public ADRecipient FindByExchangeGuid(Guid exchangeGuid)
		{
			return this.InternalFindByExchangeGuid<ADRecipient>(exchangeGuid, false, false, null);
		}

		// Token: 0x06001B32 RID: 6962 RVA: 0x00072D88 File Offset: 0x00070F88
		public Result<OWAMiniRecipient>[] FindOWAMiniRecipientByUserPrincipalName(string[] userPrincipalNames)
		{
			if (userPrincipalNames == null)
			{
				throw new ArgumentNullException("userPrincipalNames");
			}
			if (userPrincipalNames.Length == 0)
			{
				return new Result<OWAMiniRecipient>[0];
			}
			PropertyDefinition[] properties = new PropertyDefinition[]
			{
				ADRecipientSchema.EmailAddresses
			};
			return base.ReadMultiple<string, OWAMiniRecipient>(userPrincipalNames, new Converter<string, QueryFilter>(this.QueryFilterFromUserPrincipalName), null, null, properties);
		}

		// Token: 0x06001B33 RID: 6963 RVA: 0x00072DD8 File Offset: 0x00070FD8
		public ADRecipient FindByExchangeGuidIncludingAlternate(Guid exchangeGuid)
		{
			return this.InternalFindByExchangeGuid<ADRecipient>(exchangeGuid, true, true, null);
		}

		// Token: 0x06001B34 RID: 6964 RVA: 0x00072DE4 File Offset: 0x00070FE4
		public ADRawEntry FindByExchangeGuidIncludingAlternate(Guid exchangeGuid, IEnumerable<PropertyDefinition> properties)
		{
			return this.InternalFindByExchangeGuid<ADRawEntry>(exchangeGuid, true, true, properties);
		}

		// Token: 0x06001B35 RID: 6965 RVA: 0x00072DF0 File Offset: 0x00070FF0
		public TObject FindByExchangeGuidIncludingAlternate<TObject>(Guid exchangeGuid) where TObject : ADObject, new()
		{
			return this.InternalFindByExchangeGuid<TObject>(exchangeGuid, true, true, null);
		}

		// Token: 0x06001B36 RID: 6966 RVA: 0x00072DFC File Offset: 0x00070FFC
		public ADRecipient FindByExchangeGuidIncludingArchive(Guid exchangeGuid)
		{
			return this.InternalFindByExchangeGuid<ADRecipient>(exchangeGuid, false, true, null);
		}

		// Token: 0x06001B37 RID: 6967 RVA: 0x00072E08 File Offset: 0x00071008
		public TEntry FindByExchangeGuid<TEntry>(Guid exchangeGuid, IEnumerable<PropertyDefinition> properties) where TEntry : MiniRecipient, new()
		{
			return this.InternalFindByExchangeGuid<TEntry>(exchangeGuid, false, false, properties);
		}

		// Token: 0x06001B38 RID: 6968 RVA: 0x00072E14 File Offset: 0x00071014
		public ADRawEntry FindByExchangeGuid(Guid exchangeGuid, IEnumerable<PropertyDefinition> properties)
		{
			return this.InternalFindByExchangeGuid<ADRawEntry>(exchangeGuid, false, false, properties);
		}

		// Token: 0x06001B39 RID: 6969 RVA: 0x00072E20 File Offset: 0x00071020
		public bool IsThrottlingPolicyInUse(ADObjectId throttlingPolicyId)
		{
			if (base.ConfigScope == ConfigScopes.TenantSubTree || base.ConfigScope == ConfigScopes.None)
			{
				throw new InvalidOperationException("Default throttling policies can only be obtained when the Session has a ConfigScope of TenantLocal or Global.");
			}
			QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.ThrottlingPolicy, throttlingPolicyId);
			ADRawEntry[] array = base.Find<ADRawEntry>(null, QueryScope.SubTree, filter, null, 1, ADRecipientObjectSession.IsThrottlingPolicyInUseProperties, false);
			return array != null && array.Length > 0;
		}

		// Token: 0x06001B3A RID: 6970 RVA: 0x00072E74 File Offset: 0x00071074
		public ADRawEntry[] FindByCertificate(X509Identifier identifier, params PropertyDefinition[] properties)
		{
			QueryFilter certificateMatchFilter = ADUser.GetCertificateMatchFilter(identifier);
			return base.Find<ADRawEntry>(null, QueryScope.SubTree, certificateMatchFilter, null, 0, properties, false);
		}

		// Token: 0x06001B3B RID: 6971 RVA: 0x00072E98 File Offset: 0x00071098
		public ADRecipient FindByCertificate(X509Identifier identifier)
		{
			QueryFilter certificateMatchFilter = ADUser.GetCertificateMatchFilter(identifier);
			ADRecipient[] array = this.Find(null, QueryScope.SubTree, certificateMatchFilter, null, 2);
			switch (array.Length)
			{
			case 0:
				return null;
			case 1:
				return array[0];
			default:
				throw new NonUniqueRecipientException(identifier, new ObjectValidationError(DirectoryStrings.ErrorNonUniqueSid(identifier.ToString()), array[0].Id, string.Empty));
			}
		}

		// Token: 0x06001B3C RID: 6972 RVA: 0x00072EF5 File Offset: 0x000710F5
		public ADRecipient FindByObjectGuid(Guid guid)
		{
			return this.Read(new ADObjectId(null, guid));
		}

		// Token: 0x06001B3D RID: 6973 RVA: 0x00072F04 File Offset: 0x00071104
		public SecurityIdentifier GetWellKnownExchangeGroupSid(Guid wkguid)
		{
			if (!base.UseGlobalCatalog || base.UseConfigNC)
			{
				throw new InvalidOperationException(DirectoryStrings.ExceptionWKGuidNeedsGCSession(wkguid));
			}
			ADObjectId containerId = string.IsNullOrEmpty(base.DomainController) ? base.GetConfigurationNamingContext() : ADSession.GetConfigurationNamingContext(base.DomainController, base.NetworkCredential);
			ADGroup adgroup = base.ResolveWellKnownGuid<ADGroup>(wkguid, containerId);
			if (adgroup == null)
			{
				throw new ADExternalException(DirectoryStrings.ExceptionADTopologyCannotFindWellKnownExchangeGroup);
			}
			return adgroup.Sid;
		}

		// Token: 0x06001B3E RID: 6974 RVA: 0x00072F78 File Offset: 0x00071178
		public ADObjectId[] ResolveSidsToADObjectIds(string[] sids)
		{
			if (sids == null)
			{
				throw new ArgumentNullException("sids");
			}
			if (sids.Length == 0)
			{
				throw new ArgumentException("sids");
			}
			Result<ADRawEntry>[] array = base.ReadMultiple<string, ADRawEntry>(sids, new Converter<string, QueryFilter>(this.ObjectSidQueryFilter), null, null, new ProviderPropertyDefinition[0]);
			List<ADObjectId> list = new List<ADObjectId>(array.Length);
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i].Data != null)
				{
					list.Add(array[i].Data.Id);
				}
			}
			return list.ToArray();
		}

		// Token: 0x06001B3F RID: 6975 RVA: 0x00073004 File Offset: 0x00071204
		public MiniRecipientWithTokenGroups ReadTokenGroupsGlobalAndUniversal(ADObjectId id)
		{
			bool useGlobalCatalog = base.UseGlobalCatalog;
			MiniRecipientWithTokenGroups[] array;
			try
			{
				base.UseGlobalCatalog = false;
				base.EnforceDefaultScope = false;
				array = base.Find<MiniRecipientWithTokenGroups>(id, QueryScope.Base, null, null, 1);
			}
			finally
			{
				base.UseGlobalCatalog = useGlobalCatalog;
			}
			if (array.Length == 0)
			{
				return null;
			}
			return array[0];
		}

		// Token: 0x06001B40 RID: 6976 RVA: 0x00073058 File Offset: 0x00071258
		public List<string> GetTokenSids(ADRawEntry user, AssignmentMethod assignmentMethodFlags)
		{
			if (user == null)
			{
				throw new ArgumentNullException("user");
			}
			if (assignmentMethodFlags == AssignmentMethod.None)
			{
				return null;
			}
			ADObjectId userId = null;
			ADObjectId planId = null;
			SecurityIdentifier userSid = null;
			if ((assignmentMethodFlags & AssignmentMethod.SecurityGroup) != AssignmentMethod.None || (assignmentMethodFlags & AssignmentMethod.RoleGroup) != AssignmentMethod.None)
			{
				userId = (ADObjectId)user[ADObjectSchema.Id];
			}
			if ((assignmentMethodFlags & AssignmentMethod.MailboxPlan) != AssignmentMethod.None)
			{
				planId = (ADObjectId)user[ADRecipientSchema.MailboxPlan];
			}
			if ((assignmentMethodFlags & AssignmentMethod.Direct) != AssignmentMethod.None)
			{
				userSid = (SecurityIdentifier)user[IADSecurityPrincipalSchema.Sid];
			}
			return this.GetTokenSids(userId, userSid, planId, assignmentMethodFlags);
		}

		// Token: 0x06001B41 RID: 6977 RVA: 0x000730D0 File Offset: 0x000712D0
		public List<string> GetTokenSids(ADObjectId userId, AssignmentMethod assignmentMethodFlags)
		{
			if (userId == null)
			{
				throw new ArgumentNullException("userId");
			}
			return this.GetTokenSids(userId, null, null, assignmentMethodFlags);
		}

		// Token: 0x06001B42 RID: 6978 RVA: 0x000730EA File Offset: 0x000712EA
		protected QueryFilter QueryFilterFromUserPrincipalName(string userPrincipalName)
		{
			return new ComparisonFilter(ComparisonOperator.Equal, ADUserSchema.UserPrincipalName, userPrincipalName);
		}

		// Token: 0x06001B43 RID: 6979 RVA: 0x000730F8 File Offset: 0x000712F8
		private QueryFilter ObjectSidQueryFilter(string sidString)
		{
			SecurityIdentifier propertyValue = new SecurityIdentifier(sidString);
			return new ComparisonFilter(ComparisonOperator.Equal, ADMailboxRecipientSchema.Sid, propertyValue);
		}

		// Token: 0x06001B44 RID: 6980 RVA: 0x00073118 File Offset: 0x00071318
		private List<string> GetTokenSids(ADObjectId userId, SecurityIdentifier userSid, ADObjectId planId, AssignmentMethod assignmentMethodFlags)
		{
			if (assignmentMethodFlags == AssignmentMethod.None)
			{
				return null;
			}
			int num = 0;
			MultiValuedProperty<SecurityIdentifier> multiValuedProperty = null;
			if ((assignmentMethodFlags & AssignmentMethod.Direct) != AssignmentMethod.None)
			{
				num++;
			}
			if (((assignmentMethodFlags & AssignmentMethod.SecurityGroup) != AssignmentMethod.None || (assignmentMethodFlags & AssignmentMethod.RoleGroup) != AssignmentMethod.None) && userId != null)
			{
				bool flag = OrganizationId.ForestWideOrgId.Equals(base.SessionSettings.CurrentOrganizationId);
				bool flag2 = base.SessionSettings.ConfigScopes == ConfigScopes.AllTenants || (base.SessionSettings.ConfigScopes == ConfigScopes.TenantLocal && !flag);
				MiniRecipientWithTokenGroups miniRecipientWithTokenGroups;
				if (flag2)
				{
					ITenantRecipientSession tenantRecipientSession = DirectorySessionFactory.Default.CreateTenantRecipientSession(base.DomainController, null, CultureInfo.CurrentCulture.LCID, true, ConsistencyMode.PartiallyConsistent, null, base.SessionSettings, 2783, "GetTokenSids", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\Recipient\\ADRecipientObjectSession.cs");
					CompositeTenantRecipientSession compositeTenantRecipientSession = tenantRecipientSession as CompositeTenantRecipientSession;
					if (compositeTenantRecipientSession != null)
					{
						compositeTenantRecipientSession.CacheSessionForDeletingOnly = false;
					}
					miniRecipientWithTokenGroups = tenantRecipientSession.ReadTokenGroupsGlobalAndUniversal(userId);
				}
				else
				{
					miniRecipientWithTokenGroups = this.ReadTokenGroupsGlobalAndUniversal(userId);
				}
				if (miniRecipientWithTokenGroups != null)
				{
					multiValuedProperty = miniRecipientWithTokenGroups.TokenGroupsGlobalAndUniversal;
				}
				if (multiValuedProperty != null)
				{
					num += multiValuedProperty.Count;
				}
			}
			if (planId != null)
			{
				num++;
			}
			if ((assignmentMethodFlags & AssignmentMethod.ExtraDefaultSids) != AssignmentMethod.None)
			{
				num += 2;
			}
			List<string> list = new List<string>(num);
			if ((assignmentMethodFlags & AssignmentMethod.SecurityGroup) != AssignmentMethod.None || (assignmentMethodFlags & AssignmentMethod.RoleGroup) != AssignmentMethod.None)
			{
				if (userId != null && multiValuedProperty != null)
				{
					using (MultiValuedProperty<SecurityIdentifier>.Enumerator enumerator = multiValuedProperty.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							SecurityIdentifier securityIdentifier = enumerator.Current;
							list.Add(securityIdentifier.ToString());
							ExTraceGlobals.ADFindTracer.TraceDebug<string>(0L, "Adding group SID {0}", securityIdentifier.ToString());
						}
						goto IL_171;
					}
				}
				ExTraceGlobals.ADFindTracer.TraceError(0L, "User SID and/or groupSids are null");
			}
			IL_171:
			if ((assignmentMethodFlags & AssignmentMethod.Direct) != AssignmentMethod.None)
			{
				if (userSid != null)
				{
					list.Add(userSid.ToString());
					ExTraceGlobals.ADFindTracer.TraceDebug<string>(0L, "Adding user SID {0}", userSid.ToString());
				}
				else
				{
					ExTraceGlobals.ADFindTracer.TraceError(0L, "User SID is null!");
				}
			}
			if ((assignmentMethodFlags & AssignmentMethod.MailboxPlan) != AssignmentMethod.None && planId != null)
			{
				ADRawEntry adrawEntry = base.ReadADRawEntry(planId, new ADPropertyDefinition[]
				{
					IADSecurityPrincipalSchema.Sid
				});
				if (adrawEntry != null)
				{
					SecurityIdentifier securityIdentifier2 = (SecurityIdentifier)adrawEntry[IADSecurityPrincipalSchema.Sid];
					if (securityIdentifier2 != null)
					{
						list.Add(securityIdentifier2.ToString());
						ExTraceGlobals.ADFindTracer.TraceDebug<string>(0L, "Adding parent mailbox plan SID {0}", securityIdentifier2.ToString());
					}
					else
					{
						ExTraceGlobals.ADFindTracer.TraceError(0L, "Mailbox plan SID is null!");
					}
				}
			}
			if ((assignmentMethodFlags & AssignmentMethod.ExtraDefaultSids) != AssignmentMethod.None)
			{
				list.Add(ADRecipientObjectSession.EveryoneSid);
				list.Add(ADRecipientObjectSession.AuthenticatedUserSid);
			}
			return list;
		}

		// Token: 0x06001B45 RID: 6981 RVA: 0x0007338C File Offset: 0x0007158C
		public override QueryFilter ApplyDefaultFilters(QueryFilter clientFilter, ADScope scope, ADObject dummyObject, bool applyImplicitFilter)
		{
			applyImplicitFilter = !Filters.HasWellKnownRecipientTypeFilter(clientFilter);
			if (!applyImplicitFilter)
			{
				ExTraceGlobals.LdapFilterBuilderTracer.TraceDebug(0L, "ADRecipientObjectSession.ApplyDefaultFilters:  Filters.HasWellKnownRecipientTypeFilter found a well-known recipient type filter so default recipient filter will not be added");
			}
			QueryFilter queryFilter = base.ApplyDefaultFilters(clientFilter, scope, dummyObject, applyImplicitFilter);
			if (this.addressListMembershipFilter == null)
			{
				return queryFilter;
			}
			return new AndFilter(new QueryFilter[]
			{
				queryFilter,
				this.addressListMembershipFilter
			});
		}

		// Token: 0x04000BC8 RID: 3016
		private const string ForeignSecurityPrincipalClass = "foreignSecurityPrincipal";

		// Token: 0x04000BC9 RID: 3017
		internal static readonly int ReadMultipleMaxBatchSize = 20;

		// Token: 0x04000BCA RID: 3018
		private static readonly PropertyDefinition[] IsThrottlingPolicyInUseProperties = new PropertyDefinition[]
		{
			ADObjectSchema.Id
		};

		// Token: 0x04000BCB RID: 3019
		protected QueryFilter addressListMembershipFilter;

		// Token: 0x04000BCC RID: 3020
		private static readonly string EveryoneSid = new SecurityIdentifier(WellKnownSidType.WorldSid, null).ToString();

		// Token: 0x04000BCD RID: 3021
		private static readonly string AuthenticatedUserSid = new SecurityIdentifier(WellKnownSidType.AuthenticatedUserSid, null).ToString();

		// Token: 0x04000BCE RID: 3022
		protected bool isReducedRecipientSession;
	}
}
