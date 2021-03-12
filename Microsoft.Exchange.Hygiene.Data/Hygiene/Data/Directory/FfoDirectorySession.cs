using System;
using System.Collections.Generic;
using System.DirectoryServices.Protocols;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Security.AccessControl;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.Sync.TenantRelocationSync;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;
using Microsoft.Exchange.Hygiene.Data.DataProvider;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.Hygiene.Data.Directory
{
	// Token: 0x020000D4 RID: 212
	[Serializable]
	internal abstract class FfoDirectorySession : IDirectorySession
	{
		// Token: 0x1700026A RID: 618
		// (get) Token: 0x060006DE RID: 1758 RVA: 0x00015A6B File Offset: 0x00013C6B
		// (set) Token: 0x060006DF RID: 1759 RVA: 0x00015A73 File Offset: 0x00013C73
		public IActivityScope ActivityScope { get; set; }

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x060006E0 RID: 1760 RVA: 0x00015A7C File Offset: 0x00013C7C
		public string CallerInfo
		{
			get
			{
				FfoDirectorySession.LogNotSupportedInFFO(null);
				return null;
			}
		}

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x060006E1 RID: 1761 RVA: 0x00015A85 File Offset: 0x00013C85
		protected IConfigDataProvider DataProvider
		{
			get
			{
				return this.dataProvider;
			}
		}

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x060006E2 RID: 1762 RVA: 0x00015A8D File Offset: 0x00013C8D
		protected ADObjectId TenantId
		{
			get
			{
				return this.tenantId;
			}
		}

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x060006E3 RID: 1763 RVA: 0x00015A95 File Offset: 0x00013C95
		// (set) Token: 0x060006E4 RID: 1764 RVA: 0x00015A9D File Offset: 0x00013C9D
		protected ConfigScopes ConfigScope
		{
			get
			{
				return this.configScope;
			}
			set
			{
				this.configScope = value;
			}
		}

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x060006E5 RID: 1765 RVA: 0x00015AA6 File Offset: 0x00013CA6
		// (set) Token: 0x060006E6 RID: 1766 RVA: 0x00015AAE File Offset: 0x00013CAE
		protected string DomainController
		{
			get
			{
				return this.domainController;
			}
			set
			{
				this.domainController = value;
			}
		}

		// Token: 0x060006E7 RID: 1767 RVA: 0x00015AB8 File Offset: 0x00013CB8
		protected FfoDirectorySession(bool useConfigNC, bool readOnly, ConsistencyMode consistencyMode, NetworkCredential networkCredential, ADSessionSettings sessionSettings)
		{
			if (sessionSettings == null)
			{
				throw new ArgumentNullException("sessionSettings");
			}
			this.domainController = null;
			this.consistencyMode = consistencyMode;
			this.lcid = CultureInfo.CurrentCulture.LCID;
			this.useGlobalCatalog = false;
			this.enforceDefaultScope = true;
			this.useConfigNC = useConfigNC;
			this.readOnly = readOnly;
			this.networkCredential = networkCredential;
			this.sessionSettings = sessionSettings;
			this.enforceContainerizedScoping = false;
			this.configScope = sessionSettings.ConfigScopes;
			if (sessionSettings.CurrentOrganizationId != null)
			{
				this.tenantId = (sessionSettings.CurrentOrganizationId.OrganizationalUnit ?? sessionSettings.CurrentOrganizationId.ConfigurationUnit);
			}
			if (this.tenantId == null)
			{
				this.tenantId = sessionSettings.RootOrgId;
			}
			this.tenantId = this.ExtractTenantId(this.tenantId);
		}

		// Token: 0x060006E8 RID: 1768 RVA: 0x00015BA4 File Offset: 0x00013DA4
		protected FfoDirectorySession(ADObjectId tenantId)
		{
			this.tenantId = this.ExtractTenantId(tenantId);
			this.sessionSettings = new FfoSessionSettingsFactory().FromExternalDirectoryOrganizationId(this.tenantId.ObjectGuid);
		}

		// Token: 0x060006E9 RID: 1769 RVA: 0x00015BF0 File Offset: 0x00013DF0
		void IDirectorySession.AnalyzeDirectoryError(PooledLdapConnection connection, DirectoryRequest request, DirectoryException de, int totalRetries, int retriesOnServer)
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
		}

		// Token: 0x060006EA RID: 1770 RVA: 0x00015BF8 File Offset: 0x00013DF8
		QueryFilter IDirectorySession.ApplyDefaultFilters(QueryFilter filter, ADObjectId rootId, ADObject dummyObject, bool applyImplicitFilter)
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
			return filter;
		}

		// Token: 0x060006EB RID: 1771 RVA: 0x00015C01 File Offset: 0x00013E01
		QueryFilter IDirectorySession.ApplyDefaultFilters(QueryFilter filter, ADScope scope, ADObject dummyObject, bool applyImplicitFilter)
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
			return filter;
		}

		// Token: 0x060006EC RID: 1772 RVA: 0x00015C0A File Offset: 0x00013E0A
		void IDirectorySession.CheckFilterForUnsafeIdentity(QueryFilter filter)
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
		}

		// Token: 0x060006ED RID: 1773 RVA: 0x00015C12 File Offset: 0x00013E12
		void IDirectorySession.UnsafeExecuteModificationRequest(DirectoryRequest request, ADObjectId rootId)
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
		}

		// Token: 0x060006EE RID: 1774 RVA: 0x00015C1A File Offset: 0x00013E1A
		ADRawEntry[] IDirectorySession.Find(ADObjectId rootId, QueryScope scope, QueryFilter filter, SortBy sortBy, int maxResults, IEnumerable<PropertyDefinition> properties)
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
			return new ADRawEntry[0];
		}

		// Token: 0x060006EF RID: 1775 RVA: 0x00015C28 File Offset: 0x00013E28
		TResult[] IDirectorySession.Find<TResult>(ADObjectId rootId, QueryScope scope, QueryFilter filter, SortBy sortBy, int maxResults)
		{
			IEnumerable<IConfigurable> source = ((IConfigDataProvider)this).Find<TResult>(filter, rootId, false, sortBy);
			if (maxResults > 0)
			{
				source = source.Take(maxResults);
			}
			return source.Cast<TResult>().ToArray<TResult>();
		}

		// Token: 0x060006F0 RID: 1776 RVA: 0x00015C5F File Offset: 0x00013E5F
		ADRawEntry[] IDirectorySession.FindAllADRawEntriesByUsnRange(ADObjectId root, long startUsn, long endUsn, int sizeLimit, bool useAtomicFilter, IEnumerable<PropertyDefinition> properties)
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
			return new ADRawEntry[0];
		}

		// Token: 0x060006F1 RID: 1777 RVA: 0x00015C6D File Offset: 0x00013E6D
		Result<ADRawEntry>[] IDirectorySession.FindByADObjectIds(ADObjectId[] ids, params PropertyDefinition[] properties)
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
			return new Result<ADRawEntry>[0];
		}

		// Token: 0x060006F2 RID: 1778 RVA: 0x00015C7B File Offset: 0x00013E7B
		Result<TData>[] IDirectorySession.FindByADObjectIds<TData>(ADObjectId[] ids)
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
			return new Result<TData>[0];
		}

		// Token: 0x060006F3 RID: 1779 RVA: 0x00015C89 File Offset: 0x00013E89
		Result<ADRawEntry>[] IDirectorySession.FindByCorrelationIds(Guid[] correlationIds, ADObjectId configUnit, params PropertyDefinition[] properties)
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
			return new Result<ADRawEntry>[0];
		}

		// Token: 0x060006F4 RID: 1780 RVA: 0x00015C97 File Offset: 0x00013E97
		Result<ADRawEntry>[] IDirectorySession.FindByExchangeLegacyDNs(string[] exchangeLegacyDNs, params PropertyDefinition[] properties)
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
			return new Result<ADRawEntry>[0];
		}

		// Token: 0x060006F5 RID: 1781 RVA: 0x00015CA5 File Offset: 0x00013EA5
		Result<ADRawEntry>[] IDirectorySession.FindByObjectGuids(Guid[] objectGuids, params PropertyDefinition[] properties)
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
			return new Result<ADRawEntry>[0];
		}

		// Token: 0x060006F6 RID: 1782 RVA: 0x00015CB3 File Offset: 0x00013EB3
		ADRawEntry[] IDirectorySession.FindDeletedTenantSyncObjectByUsnRange(ADObjectId tenantOuRoot, long startUsn, int sizeLimit, IEnumerable<PropertyDefinition> properties)
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
			return new ADRawEntry[0];
		}

		// Token: 0x060006F7 RID: 1783 RVA: 0x00015CC1 File Offset: 0x00013EC1
		ADPagedReader<TResult> IDirectorySession.FindPaged<TResult>(ADObjectId rootId, QueryScope scope, QueryFilter filter, SortBy sortBy, int pageSize, IEnumerable<PropertyDefinition> properties)
		{
			return new FfoPagedReader<TResult>(this, rootId, scope, filter, sortBy, pageSize, properties, false);
		}

		// Token: 0x060006F8 RID: 1784 RVA: 0x00015CD3 File Offset: 0x00013ED3
		ADPagedReader<ADRawEntry> IDirectorySession.FindPagedADRawEntry(ADObjectId rootId, QueryScope scope, QueryFilter filter, SortBy sortBy, int pageSize, IEnumerable<PropertyDefinition> properties)
		{
			return new FfoPagedReader<ADRawEntry>(this, rootId, scope, filter, sortBy, pageSize, properties, false);
		}

		// Token: 0x060006F9 RID: 1785 RVA: 0x00015CE5 File Offset: 0x00013EE5
		ADPagedReader<ADRawEntry> IDirectorySession.FindPagedADRawEntryWithDefaultFilters<TResult>(ADObjectId rootId, QueryScope scope, QueryFilter filter, SortBy sortBy, int pageSize, IEnumerable<PropertyDefinition> properties)
		{
			return new FfoPagedReader<ADRawEntry>(this, rootId, scope, filter, sortBy, pageSize, properties, false);
		}

		// Token: 0x060006FA RID: 1786 RVA: 0x00015CF7 File Offset: 0x00013EF7
		ADPagedReader<TResult> IDirectorySession.FindPagedDeletedObject<TResult>(ADObjectId rootId, QueryScope scope, QueryFilter filter, SortBy sortBy, int pageSize)
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
			return null;
		}

		// Token: 0x060006FB RID: 1787 RVA: 0x00015D00 File Offset: 0x00013F00
		ADObjectId IDirectorySession.GetConfigurationNamingContext()
		{
			return DalHelper.FfoRootDN.Parent;
		}

		// Token: 0x060006FC RID: 1788 RVA: 0x00015D0C File Offset: 0x00013F0C
		ADObjectId IDirectorySession.GetDomainNamingContext()
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
			return null;
		}

		// Token: 0x060006FD RID: 1789 RVA: 0x00015D15 File Offset: 0x00013F15
		ADObjectId IDirectorySession.GetRootDomainNamingContext()
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
			return null;
		}

		// Token: 0x060006FE RID: 1790 RVA: 0x00015D1E File Offset: 0x00013F1E
		ADObjectId IDirectorySession.GetSchemaNamingContext()
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
			return null;
		}

		// Token: 0x060006FF RID: 1791 RVA: 0x00015D27 File Offset: 0x00013F27
		ADObjectId IDirectorySession.GetHostedOrganizationsRoot()
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
			return null;
		}

		// Token: 0x06000700 RID: 1792 RVA: 0x00015D30 File Offset: 0x00013F30
		ADObjectId IDirectorySession.GetConfigurationUnitsRoot()
		{
			ADObjectId configurationNamingContext = ((IDirectorySession)this).GetConfigurationNamingContext();
			return configurationNamingContext.GetChildId("CN", ADObject.ConfigurationUnits);
		}

		// Token: 0x06000701 RID: 1793 RVA: 0x00015D54 File Offset: 0x00013F54
		PooledLdapConnection IDirectorySession.GetReadConnection(string preferredServer, ref ADObjectId rootId)
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
			return null;
		}

		// Token: 0x06000702 RID: 1794 RVA: 0x00015D5D File Offset: 0x00013F5D
		PooledLdapConnection IDirectorySession.GetReadConnection(string preferredServer, string optionalBaseDN, ref ADObjectId rootId, ADRawEntry scopeDeteriminingObject)
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
			return null;
		}

		// Token: 0x06000703 RID: 1795 RVA: 0x00015D66 File Offset: 0x00013F66
		ADScope IDirectorySession.GetReadScope(ADObjectId rootId, ADRawEntry scopeDeterminingObject)
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
			return ADScope.Empty;
		}

		// Token: 0x06000704 RID: 1796 RVA: 0x00015D73 File Offset: 0x00013F73
		ADScope IDirectorySession.GetReadScope(ADObjectId rootId, ADRawEntry scopeDeterminingObject, bool isWellKnownGuidSearch, out ConfigScopes applicableScope)
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
			applicableScope = ConfigScopes.AllTenants;
			return new ADScope(rootId, null);
		}

		// Token: 0x06000705 RID: 1797 RVA: 0x00015D86 File Offset: 0x00013F86
		bool IDirectorySession.GetSchemaAndApplyFilter(ADRawEntry adRawEntry, ADScope scope, out ADObject dummyObject, out string[] ldapAttributes, ref QueryFilter filter, ref IEnumerable<PropertyDefinition> properties)
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
			dummyObject = null;
			ldapAttributes = null;
			return false;
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x00015D96 File Offset: 0x00013F96
		bool IDirectorySession.IsReadConnectionAvailable()
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
			return true;
		}

		// Token: 0x06000707 RID: 1799 RVA: 0x00015D9F File Offset: 0x00013F9F
		bool IDirectorySession.IsRootIdWithinScope<TObject>(ADObjectId rootId)
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
			return true;
		}

		// Token: 0x06000708 RID: 1800 RVA: 0x00015DA8 File Offset: 0x00013FA8
		bool IDirectorySession.IsTenantIdentity(ADObjectId id)
		{
			return id.DistinguishedName.Contains("FFO");
		}

		// Token: 0x06000709 RID: 1801 RVA: 0x00015DBA File Offset: 0x00013FBA
		TResult[] IDirectorySession.ObjectsFromEntries<TResult>(SearchResultEntryCollection entries, string originatingServerName, IEnumerable<PropertyDefinition> properties, ADRawEntry dummyInstance)
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
			return (TResult[])new ADRawEntry[0];
		}

		// Token: 0x0600070A RID: 1802 RVA: 0x00015DD0 File Offset: 0x00013FD0
		ADRawEntry IDirectorySession.ReadADRawEntry(ADObjectId entryId, IEnumerable<PropertyDefinition> properties)
		{
			ADRawEntry adrawEntry = new ADRawEntry();
			adrawEntry.SetId(entryId);
			return adrawEntry;
		}

		// Token: 0x0600070B RID: 1803 RVA: 0x00015DEB File Offset: 0x00013FEB
		RawSecurityDescriptor IDirectorySession.ReadSecurityDescriptor(ADObjectId id)
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
			return null;
		}

		// Token: 0x0600070C RID: 1804 RVA: 0x00015DF4 File Offset: 0x00013FF4
		SecurityDescriptor IDirectorySession.ReadSecurityDescriptorBlob(ADObjectId id)
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
			return null;
		}

		// Token: 0x0600070D RID: 1805 RVA: 0x00015DFD File Offset: 0x00013FFD
		string[] IDirectorySession.ReplicateSingleObject(ADObject instanceToReplicate, ADObjectId[] sites)
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
			return new string[0];
		}

		// Token: 0x0600070E RID: 1806 RVA: 0x00015E0B File Offset: 0x0001400B
		bool IDirectorySession.ReplicateSingleObjectToTargetDC(ADObject instanceToReplicate, string targetServerName)
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
			return false;
		}

		// Token: 0x0600070F RID: 1807 RVA: 0x00015E14 File Offset: 0x00014014
		TResult IDirectorySession.ResolveWellKnownGuid<TResult>(Guid wellKnownGuid, ADObjectId containerId)
		{
			return ((IDirectorySession)this).ResolveWellKnownGuid<TResult>(wellKnownGuid, containerId.DistinguishedName);
		}

		// Token: 0x06000710 RID: 1808 RVA: 0x00015E23 File Offset: 0x00014023
		TenantRelocationSyncObject IDirectorySession.RetrieveTenantRelocationSyncObject(ADObjectId entryId, IEnumerable<PropertyDefinition> properties)
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
			return null;
		}

		// Token: 0x06000711 RID: 1809 RVA: 0x00015E48 File Offset: 0x00014048
		TResult IDirectorySession.ResolveWellKnownGuid<TResult>(Guid wellKnownGuid, string containerDN)
		{
			RoleGroupInitInfo roleGroupInitInfo = FfoDirectorySession.SupportedRoleGroups.FirstOrDefault((RoleGroupInitInfo roleGroup) => roleGroup.WellKnownGuid == wellKnownGuid);
			if (roleGroupInitInfo.WellKnownGuid == Guid.Empty)
			{
				FfoDirectorySession.LogNotSupportedInFFO(null);
				return default(TResult);
			}
			string propertyValue = roleGroupInitInfo.Name.Replace(" ", string.Empty).Replace("-", string.Empty);
			QueryFilter filter = QueryFilter.AndTogether(new QueryFilter[]
			{
				new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.RecipientTypeDetailsValue, RecipientTypeDetails.RoleGroup),
				new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Name, propertyValue)
			});
			IEnumerable<IConfigurable> enumerable = ((IConfigDataProvider)this).Find<TResult>(filter, null, false, null);
			if (enumerable != null)
			{
				return enumerable.Cast<TResult>().FirstOrDefault<TResult>();
			}
			return default(TResult);
		}

		// Token: 0x06000712 RID: 1810 RVA: 0x00015F29 File Offset: 0x00014129
		ADOperationResultWithData<TResult>[] IDirectorySession.RunAgainstAllDCsInSite<TResult>(ADObjectId siteId, Func<TResult> methodToCall)
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
			return null;
		}

		// Token: 0x06000713 RID: 1811 RVA: 0x00015F32 File Offset: 0x00014132
		void IDirectorySession.SaveSecurityDescriptor(ADObjectId id, RawSecurityDescriptor sd)
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
		}

		// Token: 0x06000714 RID: 1812 RVA: 0x00015F3A File Offset: 0x0001413A
		void IDirectorySession.SaveSecurityDescriptor(ADObjectId id, RawSecurityDescriptor sd, bool modifyOwner)
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
		}

		// Token: 0x06000715 RID: 1813 RVA: 0x00015F42 File Offset: 0x00014142
		void IDirectorySession.SaveSecurityDescriptor(ADObject obj, RawSecurityDescriptor sd)
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
		}

		// Token: 0x06000716 RID: 1814 RVA: 0x00015F4A File Offset: 0x0001414A
		void IDirectorySession.SaveSecurityDescriptor(ADObject obj, RawSecurityDescriptor sd, bool modifyOwner)
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
		}

		// Token: 0x06000717 RID: 1815 RVA: 0x00015F52 File Offset: 0x00014152
		bool IDirectorySession.TryVerifyIsWithinScopes(ADObject entry, bool isModification, out ADScopeException exception)
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
			exception = null;
			return true;
		}

		// Token: 0x06000718 RID: 1816 RVA: 0x00015F5E File Offset: 0x0001415E
		void IDirectorySession.UpdateServerSettings(PooledLdapConnection connection)
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
		}

		// Token: 0x06000719 RID: 1817 RVA: 0x00015F66 File Offset: 0x00014166
		void IDirectorySession.VerifyIsWithinScopes(ADObject entry, bool isModification)
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
		}

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x0600071A RID: 1818 RVA: 0x00015F6E File Offset: 0x0001416E
		// (set) Token: 0x0600071B RID: 1819 RVA: 0x00015F76 File Offset: 0x00014176
		TimeSpan? IDirectorySession.ClientSideSearchTimeout
		{
			get
			{
				return this.clientSideSearchTimeout;
			}
			set
			{
				this.clientSideSearchTimeout = value;
			}
		}

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x0600071C RID: 1820 RVA: 0x00015F7F File Offset: 0x0001417F
		ConfigScopes IDirectorySession.ConfigScope
		{
			get
			{
				return this.configScope;
			}
		}

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x0600071D RID: 1821 RVA: 0x00015F87 File Offset: 0x00014187
		ConsistencyMode IDirectorySession.ConsistencyMode
		{
			get
			{
				return this.consistencyMode;
			}
		}

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x0600071E RID: 1822 RVA: 0x00015F8F File Offset: 0x0001418F
		// (set) Token: 0x0600071F RID: 1823 RVA: 0x00015F97 File Offset: 0x00014197
		string IDirectorySession.DomainController
		{
			get
			{
				return this.domainController;
			}
			set
			{
				this.domainController = value;
			}
		}

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06000720 RID: 1824 RVA: 0x00015FA0 File Offset: 0x000141A0
		// (set) Token: 0x06000721 RID: 1825 RVA: 0x00015FA8 File Offset: 0x000141A8
		bool IDirectorySession.EnforceContainerizedScoping
		{
			get
			{
				return this.enforceContainerizedScoping;
			}
			set
			{
				this.enforceContainerizedScoping = value;
			}
		}

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06000722 RID: 1826 RVA: 0x00015FB1 File Offset: 0x000141B1
		// (set) Token: 0x06000723 RID: 1827 RVA: 0x00015FB9 File Offset: 0x000141B9
		bool IDirectorySession.EnforceDefaultScope
		{
			get
			{
				return this.enforceDefaultScope;
			}
			set
			{
				this.enforceDefaultScope = value;
			}
		}

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06000724 RID: 1828 RVA: 0x00015FC2 File Offset: 0x000141C2
		string IDirectorySession.LastUsedDc
		{
			get
			{
				return ((IDirectorySession)this).ServerSettings.LastUsedDc(((IDirectorySession)this).SessionSettings.GetAccountOrResourceForestFqdn());
			}
		}

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06000725 RID: 1829 RVA: 0x00015FDA File Offset: 0x000141DA
		int IDirectorySession.Lcid
		{
			get
			{
				return this.lcid;
			}
		}

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06000726 RID: 1830 RVA: 0x00015FE2 File Offset: 0x000141E2
		// (set) Token: 0x06000727 RID: 1831 RVA: 0x00015FEA File Offset: 0x000141EA
		string IDirectorySession.LinkResolutionServer
		{
			get
			{
				return this.linkResolutionServer;
			}
			set
			{
				this.linkResolutionServer = value;
			}
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06000728 RID: 1832 RVA: 0x00015FF3 File Offset: 0x000141F3
		// (set) Token: 0x06000729 RID: 1833 RVA: 0x00015FFB File Offset: 0x000141FB
		bool IDirectorySession.LogSizeLimitExceededEvent
		{
			get
			{
				return this.logSizeLimitExceededEvent;
			}
			set
			{
				this.logSizeLimitExceededEvent = value;
			}
		}

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x0600072A RID: 1834 RVA: 0x00016004 File Offset: 0x00014204
		NetworkCredential IDirectorySession.NetworkCredential
		{
			get
			{
				return this.networkCredential;
			}
		}

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x0600072B RID: 1835 RVA: 0x0001600C File Offset: 0x0001420C
		bool IDirectorySession.ReadOnly
		{
			get
			{
				return this.readOnly;
			}
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x0600072C RID: 1836 RVA: 0x00016014 File Offset: 0x00014214
		ADServerSettings IDirectorySession.ServerSettings
		{
			get
			{
				return this.sessionSettings.ServerSettings;
			}
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x0600072D RID: 1837 RVA: 0x00016021 File Offset: 0x00014221
		// (set) Token: 0x0600072E RID: 1838 RVA: 0x00016029 File Offset: 0x00014229
		TimeSpan? IDirectorySession.ServerTimeout
		{
			get
			{
				return this.serverTimeout;
			}
			set
			{
				this.serverTimeout = value;
			}
		}

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x0600072F RID: 1839 RVA: 0x00016032 File Offset: 0x00014232
		ADSessionSettings IDirectorySession.SessionSettings
		{
			get
			{
				return this.sessionSettings;
			}
		}

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x06000730 RID: 1840 RVA: 0x0001603A File Offset: 0x0001423A
		// (set) Token: 0x06000731 RID: 1841 RVA: 0x00016042 File Offset: 0x00014242
		bool IDirectorySession.SkipRangedAttributes
		{
			get
			{
				return this.skipRangedAttributes;
			}
			set
			{
				this.skipRangedAttributes = value;
			}
		}

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x06000732 RID: 1842 RVA: 0x0001604B File Offset: 0x0001424B
		// (set) Token: 0x06000733 RID: 1843 RVA: 0x00016053 File Offset: 0x00014253
		string[] IDirectorySession.ExclusiveLdapAttributes
		{
			get
			{
				return this.exclusiveLdapAttributes;
			}
			set
			{
				this.exclusiveLdapAttributes = value;
			}
		}

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x06000734 RID: 1844 RVA: 0x0001605C File Offset: 0x0001425C
		// (set) Token: 0x06000735 RID: 1845 RVA: 0x00016064 File Offset: 0x00014264
		bool IDirectorySession.UseConfigNC
		{
			get
			{
				return this.useConfigNC;
			}
			set
			{
				this.useConfigNC = value;
			}
		}

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06000736 RID: 1846 RVA: 0x0001606D File Offset: 0x0001426D
		// (set) Token: 0x06000737 RID: 1847 RVA: 0x00016075 File Offset: 0x00014275
		bool IDirectorySession.UseGlobalCatalog
		{
			get
			{
				return this.useGlobalCatalog;
			}
			set
			{
				this.useGlobalCatalog = value;
			}
		}

		// Token: 0x06000738 RID: 1848 RVA: 0x00016080 File Offset: 0x00014280
		protected static void LogNotSupportedInFFO(Exception ex = null)
		{
			EventLogger.Logger.LogEvent(FfoHygineDataProviderEventLogConstants.Tuple_UnsupportedFFOAPICalled, null, new object[]
			{
				(ex != null) ? ex.ToString() : Environment.StackTrace
			});
		}

		// Token: 0x06000739 RID: 1849 RVA: 0x000160BC File Offset: 0x000142BC
		public static void FixDistinguishedName(ADObject adObject, string tenantDistinguishedName, Guid tenantGuid, Guid objectGuid, ADObjectId relativeConfigDN = null)
		{
			if (adObject == null || string.IsNullOrEmpty(tenantDistinguishedName))
			{
				return;
			}
			if (tenantGuid == Guid.Empty || objectGuid == Guid.Empty)
			{
				throw new InvalidOperationException(string.Format("Unable to fix distinguished name for ADObject = {0}, TenantGuid = {1}, ObjectGuid = {2}, objectName = {3}.", new object[]
				{
					adObject.GetType().Name,
					tenantGuid,
					objectGuid,
					adObject.Name
				}));
			}
			string unescapedCommonName = string.IsNullOrEmpty(adObject.Name) ? objectGuid.ToString() : adObject.Name;
			ADObjectId adobjectId = new ADObjectId(tenantDistinguishedName, tenantGuid);
			ADObjectId adobjectId2 = new ADObjectId(adobjectId.GetChildId("Configuration").DistinguishedName, tenantGuid);
			if (relativeConfigDN != null)
			{
				adobjectId2 = new ADObjectId(adobjectId2.GetDescendantId(relativeConfigDN).DistinguishedName, tenantGuid);
			}
			ADObjectId id = new ADObjectId(adobjectId2.GetChildId(unescapedCommonName).DistinguishedName, objectGuid);
			ADObjectId adobjectId3 = (ADObjectId)adObject[ADObjectSchema.ConfigurationUnit];
			if (adobjectId3 != null && adobjectId3.Name != null && string.Equals(adobjectId3.Name, adobjectId2.Name, StringComparison.InvariantCultureIgnoreCase))
			{
				return;
			}
			adObject[ADObjectSchema.OrganizationalUnitRoot] = adobjectId;
			adObject[ADObjectSchema.ConfigurationUnit] = adobjectId2;
			adObject.SetId(id);
			FfoDirectorySession.FixLegacyExchangeDN(adObject, tenantGuid);
			FfoDirectorySession.FixDistinguishedNameForADObjectIDs(adObject, tenantDistinguishedName);
		}

		// Token: 0x0600073A RID: 1850 RVA: 0x00016208 File Offset: 0x00014408
		public static ADObjectId GetUpdatedADObjectIdWithDN(ADObjectId adObject, string tenantDistinguishedName, Guid tenantGuid, ADObjectId relativeConfigDN = null)
		{
			if (adObject == null || string.IsNullOrEmpty(tenantDistinguishedName))
			{
				return null;
			}
			if (tenantGuid == Guid.Empty || adObject.ObjectGuid == Guid.Empty)
			{
				throw new InvalidOperationException(string.Format("Unable to fix distinguished name for ADObject = {0}, TenantGuid = {1}, ObjectGuid = {2}, objectName = {3}.", new object[]
				{
					adObject.GetType().Name,
					tenantGuid,
					adObject.ObjectGuid,
					adObject.Name
				}));
			}
			string unescapedCommonName = string.IsNullOrEmpty(adObject.Name) ? adObject.ObjectGuid.ToString() : adObject.Name;
			ADObjectId adobjectId = new ADObjectId(tenantDistinguishedName, tenantGuid);
			ADObjectId adobjectId2 = new ADObjectId(adobjectId.GetChildId("Configuration").DistinguishedName, tenantGuid);
			if (relativeConfigDN != null)
			{
				adobjectId2 = new ADObjectId(adobjectId2.GetDescendantId(relativeConfigDN).DistinguishedName, tenantGuid);
			}
			return new ADObjectId(adobjectId2.GetChildId(unescapedCommonName).DistinguishedName, adObject.ObjectGuid);
		}

		// Token: 0x0600073B RID: 1851 RVA: 0x00016318 File Offset: 0x00014518
		public static bool TryGetRoleGroupInfo(RoleGroup.RoleGroupTypeIds typeId, out RoleGroupInitInfo roleGroupInfo)
		{
			roleGroupInfo = FfoDirectorySession.SupportedRoleGroups.FirstOrDefault((RoleGroupInitInfo rg) => rg.Id == (int)typeId);
			return roleGroupInfo.WellKnownGuid != Guid.Empty;
		}

		// Token: 0x0600073C RID: 1852 RVA: 0x00016360 File Offset: 0x00014560
		private static void FixLegacyExchangeDN(ADObject adObject, Guid tenantGuid)
		{
			ADObjectId id = adObject.Id;
			if (adObject is ADRecipient || adObject is MiniRecipient)
			{
				adObject[ADRecipientSchema.LegacyExchangeDN] = id.DistinguishedName;
			}
			if (adObject is Organization || adObject is ADOrganizationalUnit || adObject is ExchangeConfigurationUnit)
			{
				adObject[OrganizationSchema.LegacyExchangeDN] = string.Format("/o={0}/ou={1}/cn={2}", tenantGuid.ToString(), tenantGuid.ToString(), id.ObjectGuid.ToString());
			}
		}

		// Token: 0x0600073D RID: 1853 RVA: 0x00016448 File Offset: 0x00014648
		private static void FixDistinguishedNameForADObjectIDs(ADObject adObject, string tenantDistinguishedName)
		{
			string[] stdIDs = new string[]
			{
				ADObjectSchema.Id.Name,
				ADObjectSchema.OrganizationalUnitRoot.Name,
				ADObjectSchema.ConfigurationUnit.Name
			};
			IEnumerable<PropertyDefinition> enumerable = adObject.Schema.AllProperties;
			enumerable = from propertyDefinition in enumerable
			where propertyDefinition != null
			select propertyDefinition;
			enumerable = from propertyDefinition in enumerable
			where propertyDefinition.Type == typeof(ADObjectId)
			select propertyDefinition;
			enumerable = from propertyDefinition in enumerable
			where propertyDefinition is ProviderPropertyDefinition && !((ProviderPropertyDefinition)propertyDefinition).IsReadOnly
			select propertyDefinition;
			enumerable = from propertyDefinition in enumerable
			where !stdIDs.Contains(propertyDefinition.Name)
			select propertyDefinition;
			foreach (PropertyDefinition propertyDefinition2 in enumerable)
			{
				ProviderPropertyDefinition providerPropertyDefinition = (ProviderPropertyDefinition)propertyDefinition2;
				object obj;
				if (adObject.TryGetValueWithoutDefault(providerPropertyDefinition, out obj) && obj != null)
				{
					if (providerPropertyDefinition.IsMultivalued)
					{
						MultiValuedProperty<ADObjectId> multiValuedProperty = obj as MultiValuedProperty<ADObjectId>;
						int num = 0;
						while (multiValuedProperty != null)
						{
							if (num >= multiValuedProperty.Count)
							{
								break;
							}
							ADObjectId adObjectId = multiValuedProperty[num];
							multiValuedProperty[num] = new ADObjectId(tenantDistinguishedName, Guid.Empty);
							multiValuedProperty[num] = FfoDirectorySession.ReplaceTenantDistinguishedName(tenantDistinguishedName, adObjectId);
							num++;
						}
					}
					else
					{
						ADObjectId adobjectId = obj as ADObjectId;
						if (adobjectId != null)
						{
							adObject[providerPropertyDefinition] = FfoDirectorySession.ReplaceTenantDistinguishedName(tenantDistinguishedName, adobjectId);
						}
					}
				}
			}
		}

		// Token: 0x0600073E RID: 1854 RVA: 0x000165E8 File Offset: 0x000147E8
		private static ADObjectId ReplaceTenantDistinguishedName(string tenantDistinguishedName, ADObjectId adObjectId)
		{
			return new ADObjectId(adObjectId.DistinguishedName.Replace(DalHelper.GetTenantDistinguishedName("TenantName"), tenantDistinguishedName), adObjectId.ObjectGuid);
		}

		// Token: 0x0600073F RID: 1855 RVA: 0x0001660C File Offset: 0x0001480C
		protected void FixOrganizationalUnitRoot(IConfigurable configurable)
		{
			ADObject adobject = configurable as ADObject;
			if (adobject != null)
			{
				if (adobject.OrganizationalUnitRoot == null)
				{
					adobject[ADObjectSchema.OrganizationalUnitRoot] = this.TenantId;
					return;
				}
			}
			else
			{
				ConfigurablePropertyBag configurablePropertyBag = configurable as ConfigurablePropertyBag;
				if (configurablePropertyBag != null)
				{
					configurablePropertyBag[ADObjectSchema.OrganizationalUnitRoot] = this.TenantId;
					return;
				}
				ConfigurableObject configurableObject = configurable as ConfigurableObject;
				if (configurableObject != null)
				{
					configurableObject[ADObjectSchema.OrganizationalUnitRoot] = this.TenantId;
				}
			}
		}

		// Token: 0x06000740 RID: 1856 RVA: 0x00016674 File Offset: 0x00014874
		protected void GenerateIdForObject(IConfigurable configurable)
		{
			ADObject adobject = configurable as ADObject;
			if (adobject != null)
			{
				if (adobject.Id != null && adobject.Id.ObjectGuid == Guid.Empty && !(adobject is ADConfigurationObject))
				{
					adobject.SetId(new ADObjectId(adobject.Id.DistinguishedName, CombGuidGenerator.NewGuid()));
				}
				if ((adobject is TransportRule || adobject is Container) && adobject.Id != null && string.IsNullOrEmpty(adobject.Name))
				{
					adobject.Name = (adobject.Id.Name ?? adobject.Id.ObjectGuid.ToString());
					return;
				}
			}
			else
			{
				ConfigurablePropertyBag configurablePropertyBag = configurable as ConfigurablePropertyBag;
				if (configurablePropertyBag != null)
				{
					if (configurablePropertyBag is UserCertificate)
					{
						configurablePropertyBag[UserCertificate.UserCertificateIdProp] = CombGuidGenerator.NewGuid();
						configurablePropertyBag[UserCertificate.CertificateIdProp] = CombGuidGenerator.NewGuid();
					}
					if (configurablePropertyBag is PartnerCertificate)
					{
						configurablePropertyBag[PartnerCertificate.PartnerCertificateIdDef] = CombGuidGenerator.NewGuid();
						configurablePropertyBag[PartnerCertificate.PartnerIdDef] = CombGuidGenerator.NewGuid();
						configurablePropertyBag[PartnerCertificate.CertificateIdDef] = CombGuidGenerator.NewGuid();
						return;
					}
				}
				else
				{
					ConfigurableObject configurableObject = configurable as ConfigurableObject;
					if (configurableObject != null)
					{
						return;
					}
					throw new ArgumentException("IConfigurable object must be driven from ADObject, ConfigurableObject, or ConfigurablePropertyBag");
				}
			}
		}

		// Token: 0x06000741 RID: 1857 RVA: 0x000167C7 File Offset: 0x000149C7
		protected ADObjectId GenerateLocalIdentifier()
		{
			return new ADObjectId(CombGuidGenerator.NewGuid());
		}

		// Token: 0x06000742 RID: 1858 RVA: 0x000167D4 File Offset: 0x000149D4
		protected IConfigurable[] FindTenantObject<T>(params object[] propNameValues) where T : IConfigurable, new()
		{
			QueryFilter[] array = new QueryFilter[propNameValues.Length / 2];
			for (int i = 0; i < propNameValues.Length; i += 2)
			{
				array[i / 2] = new ComparisonFilter(ComparisonOperator.Equal, (PropertyDefinition)propNameValues[i], propNameValues[i + 1]);
			}
			QueryFilter filter = QueryFilter.AndTogether(array);
			IConfigurable[] array2 = this.FindAndHandleException<T>(filter, null, false, null, int.MaxValue).Cast<IConfigurable>().ToArray<IConfigurable>();
			IConfigurable[] array3 = array2;
			for (int j = 0; j < array3.Length; j++)
			{
				T t = (T)((object)array3[j]);
				ADObject adobject = t as ADObject;
				if (adobject != null)
				{
					FfoDirectorySession.FixDistinguishedName(adobject, this.TenantId.DistinguishedName, this.TenantId.ObjectGuid, ((ADObjectId)adobject.Identity).ObjectGuid, null);
				}
			}
			return array2;
		}

		// Token: 0x06000743 RID: 1859 RVA: 0x00016898 File Offset: 0x00014A98
		protected IEnumerable<T> FindAndHandleException<T>(QueryFilter filter, ObjectId rootId, bool deepSearch, SortBy sortBy, int pageSize = 2147483647) where T : IConfigurable, new()
		{
			IEnumerable<T> result;
			try
			{
				IEnumerable<T> enumerable;
				if (pageSize == 2147483647)
				{
					enumerable = this.DataProvider.Find<T>(this.AddTenantIdFilter(filter), rootId, deepSearch, sortBy).Cast<T>();
				}
				else
				{
					enumerable = this.DataProvider.FindPaged<T>(this.AddTenantIdFilter(filter), rootId, deepSearch, sortBy, pageSize);
				}
				result = enumerable;
			}
			catch (DataProviderMappingException ex)
			{
				FfoDirectorySession.LogNotSupportedInFFO(ex);
				result = new T[0];
			}
			return result;
		}

		// Token: 0x06000744 RID: 1860 RVA: 0x0001690C File Offset: 0x00014B0C
		protected T ReadAndHandleException<T>(QueryFilter filter) where T : IConfigurable, new()
		{
			try
			{
				IConfigurable[] source = this.DataProvider.Find<T>(filter, null, false, null);
				return (T)((object)source.FirstOrDefault<IConfigurable>());
			}
			catch (DataProviderMappingException ex)
			{
				FfoDirectorySession.LogNotSupportedInFFO(ex);
			}
			return default(T);
		}

		// Token: 0x06000745 RID: 1861 RVA: 0x000169B0 File Offset: 0x00014BB0
		protected QueryFilter AddTenantIdFilter(QueryFilter filter)
		{
			if (this.TenantId == null)
			{
				return filter;
			}
			ADObjectId extractedId = null;
			bool orgFilterAlreadyPresent = false;
			if (filter != null)
			{
				DalHelper.ForEachProperty(filter, delegate(PropertyDefinition propertyDefinition, object value)
				{
					if (propertyDefinition == ExchangeConfigurationUnitSchema.ExternalDirectoryOrganizationId && value != null)
					{
						extractedId = new ADObjectId(DalHelper.GetTenantDistinguishedName(value.ToString()), Guid.Parse(value.ToString()));
						return;
					}
					if (propertyDefinition == ADObjectSchema.OrganizationalUnitRoot && value != null)
					{
						orgFilterAlreadyPresent = true;
					}
				});
			}
			if (!orgFilterAlreadyPresent)
			{
				QueryFilter queryFilter = new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.OrganizationalUnitRoot, extractedId ?? this.TenantId);
				filter = ((filter == null) ? queryFilter : QueryFilter.AndTogether(new QueryFilter[]
				{
					filter,
					queryFilter
				}));
			}
			return filter;
		}

		// Token: 0x06000746 RID: 1862 RVA: 0x00016A3C File Offset: 0x00014C3C
		protected T GetDefaultObject<T>()
		{
			Func<object> func;
			if (!FfoDirectorySession.defaultInstanceFactory.TryGetValue(typeof(T), out func))
			{
				return default(T);
			}
			T t = (T)((object)func());
			ADConfigurationObject adconfigurationObject = t as ADConfigurationObject;
			if (adconfigurationObject != null)
			{
				adconfigurationObject.Name = "Default";
				adconfigurationObject.SetId(new ADObjectId("CN=Default", FfoDirectorySession.defaultObjectId));
			}
			return t;
		}

		// Token: 0x06000747 RID: 1863 RVA: 0x00016AA8 File Offset: 0x00014CA8
		protected T[] GetDefaultArray<T>()
		{
			object obj = this.GetDefaultObject<T>();
			if (obj == null)
			{
				return new T[0];
			}
			return new T[]
			{
				(T)((object)obj)
			};
		}

		// Token: 0x06000748 RID: 1864 RVA: 0x00016AE0 File Offset: 0x00014CE0
		protected void ApplyAuditProperties(IConfigurable configurable)
		{
			if (this.sessionSettings == null || string.IsNullOrEmpty(this.sessionSettings.ExecutingUserIdentityName))
			{
				EventLogger.Logger.LogEvent(FfoHygineDataProviderEventLogConstants.Tuple_AuditUserIdentityMissing, null, new object[]
				{
					Environment.StackTrace
				});
				return;
			}
			AuditHelper.ApplyAuditProperties(configurable as IPropertyBag, Guid.NewGuid(), this.sessionSettings.ExecutingUserIdentityName);
		}

		// Token: 0x06000749 RID: 1865 RVA: 0x00016B44 File Offset: 0x00014D44
		protected ADObjectId ExtractTenantId(ADObjectId tenantDescendantId)
		{
			if (tenantDescendantId == null)
			{
				return null;
			}
			int num = DalHelper.FfoRootDN.Depth + 1;
			FfoTenant ffoTenant = null;
			if (string.IsNullOrEmpty(tenantDescendantId.DistinguishedName) && tenantDescendantId.ObjectGuid != Guid.Empty)
			{
				ffoTenant = this.ReadAndHandleException<FfoTenant>(new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.OrganizationalUnitRoot, new ADObjectId(tenantDescendantId.ObjectGuid)));
				if (ffoTenant != null)
				{
					string tenantDistinguishedName = DalHelper.GetTenantDistinguishedName(ffoTenant.Name);
					return new ADObjectId(tenantDistinguishedName, tenantDescendantId.ObjectGuid);
				}
			}
			if (tenantDescendantId.Depth <= num)
			{
				return tenantDescendantId;
			}
			ADObjectId adobjectId = tenantDescendantId.AncestorDN(tenantDescendantId.Depth - num);
			string name = adobjectId.Name;
			Guid guid;
			if (Guid.TryParse(name, out guid))
			{
				ffoTenant = this.ReadAndHandleException<FfoTenant>(new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.OrganizationalUnitRoot, new ADObjectId(guid)));
			}
			if (ffoTenant == null && tenantDescendantId.ObjectGuid == Guid.Empty)
			{
				GlobalConfigSession globalConfigSession = new GlobalConfigSession();
				ffoTenant = globalConfigSession.GetTenantByName(name);
			}
			if (ffoTenant == null)
			{
				return tenantDescendantId;
			}
			return ffoTenant.OrganizationalUnitRoot;
		}

		// Token: 0x0600074D RID: 1869 RVA: 0x00016D70 File Offset: 0x00014F70
		// Note: this type is marked as 'beforefieldinit'.
		static FfoDirectorySession()
		{
			Dictionary<Type, Func<object>> dictionary = new Dictionary<Type, Func<object>>();
			dictionary.Add(typeof(TransportConfigContainer), () => new TransportConfigContainer());
			dictionary.Add(typeof(PerimeterConfig), () => new PerimeterConfig());
			dictionary.Add(typeof(DomainContentConfig), () => new DomainContentConfig
			{
				DomainName = SmtpDomainWithSubdomains.StarDomain,
				IsInternal = false,
				TargetDeliveryDomain = false,
				CharacterSet = "iso-8859-1",
				NonMimeCharacterSet = "iso-8859-1",
				AllowedOOFType = AllowedOOFType.External,
				AutoReplyEnabled = true,
				AutoForwardEnabled = true,
				DeliveryReportEnabled = true,
				NDREnabled = true,
				MeetingForwardNotificationEnabled = false,
				ContentType = ContentType.MimeText,
				DisplaySenderName = true,
				PreferredInternetCodePageForShiftJis = PreferredInternetCodePageForShiftJisEnum.Undefined,
				RequiredCharsetCoverage = null,
				TNEFEnabled = null,
				LineWrapSize = Unlimited<int>.UnlimitedValue,
				TrustedMailOutboundEnabled = false,
				TrustedMailInboundEnabled = false,
				UseSimpleDisplayName = false,
				NDRDiagnosticInfoEnabled = true,
				MessageCountThreshold = int.MaxValue
			});
			dictionary.Add(typeof(AdminAuditLogConfig), () => new AdminAuditLogConfig
			{
				AdminAuditLogEnabled = true,
				AdminAuditLogCmdlets = new MultiValuedProperty<string>(new string[]
				{
					"*"
				}),
				AdminAuditLogParameters = new MultiValuedProperty<string>(new string[]
				{
					"*"
				}),
				CaptureDetailsEnabled = true
			});
			FfoDirectorySession.defaultInstanceFactory = dictionary;
			FfoDirectorySession.SupportedRoleGroups = new RoleGroupInitInfo[]
			{
				RoleGroup.ComplianceManagement_InitInfo,
				RoleGroup.HygieneManagement_InitInfo,
				RoleGroup.HelpDesk_InitInfo,
				RoleGroup.RecipientManagement_InitInfo,
				RoleGroup.RecordsManagement_InitInfo,
				RoleGroup.OrganizationManagement_InitInfo,
				RoleGroup.ViewOnlyOrganizationManagement_InitInfo
			};
			FfoDirectorySession.defaultObjectId = new Guid("08075A1F-B49E-4769-983D-BE2587651F3B");
		}

		// Token: 0x04000441 RID: 1089
		private static readonly Dictionary<Type, Func<object>> defaultInstanceFactory;

		// Token: 0x04000442 RID: 1090
		private static readonly RoleGroupInitInfo[] SupportedRoleGroups;

		// Token: 0x04000443 RID: 1091
		private static readonly Guid defaultObjectId;

		// Token: 0x04000444 RID: 1092
		private readonly IConfigDataProvider dataProvider = ConfigDataProviderFactory.Default.Create(DatabaseType.Directory);

		// Token: 0x04000445 RID: 1093
		private readonly ConsistencyMode consistencyMode;

		// Token: 0x04000446 RID: 1094
		private readonly bool readOnly;

		// Token: 0x04000447 RID: 1095
		private readonly NetworkCredential networkCredential;

		// Token: 0x04000448 RID: 1096
		private readonly ADSessionSettings sessionSettings;

		// Token: 0x04000449 RID: 1097
		private readonly int lcid;

		// Token: 0x0400044A RID: 1098
		private ConfigScopes configScope;

		// Token: 0x0400044B RID: 1099
		private string domainController;

		// Token: 0x0400044C RID: 1100
		private bool useGlobalCatalog;

		// Token: 0x0400044D RID: 1101
		private bool enforceDefaultScope;

		// Token: 0x0400044E RID: 1102
		private bool useConfigNC;

		// Token: 0x0400044F RID: 1103
		private bool enforceContainerizedScoping;

		// Token: 0x04000450 RID: 1104
		private TimeSpan? clientSideSearchTimeout;

		// Token: 0x04000451 RID: 1105
		private bool skipRangedAttributes;

		// Token: 0x04000452 RID: 1106
		private string[] exclusiveLdapAttributes;

		// Token: 0x04000453 RID: 1107
		private TimeSpan? serverTimeout;

		// Token: 0x04000454 RID: 1108
		private bool logSizeLimitExceededEvent;

		// Token: 0x04000455 RID: 1109
		private string linkResolutionServer;

		// Token: 0x04000456 RID: 1110
		private ADObjectId tenantId;
	}
}
