using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Directory.UnifiedPolicy;

namespace Microsoft.Exchange.Hygiene.Data.Directory
{
	// Token: 0x020000D5 RID: 213
	[Serializable]
	internal abstract class FfoConfigurationSession : FfoDirectorySession, IConfigurationSession, IDirectorySession, IConfigDataProvider
	{
		// Token: 0x06000752 RID: 1874 RVA: 0x00016ED0 File Offset: 0x000150D0
		protected FfoConfigurationSession(bool useConfigNC, bool readOnly, ConsistencyMode consistencyMode, NetworkCredential networkCredential, ADSessionSettings sessionSettings) : base(useConfigNC, readOnly, consistencyMode, networkCredential, sessionSettings)
		{
		}

		// Token: 0x06000753 RID: 1875 RVA: 0x00016EDF File Offset: 0x000150DF
		protected FfoConfigurationSession(ADObjectId tenantId) : base(tenantId)
		{
		}

		// Token: 0x06000754 RID: 1876 RVA: 0x00016EE8 File Offset: 0x000150E8
		IConfigurable IConfigDataProvider.Read<T>(ObjectId identity)
		{
			IConfigurable configurable = this.ReadImpl<T>(identity);
			ConfigurableObject configurableObject = configurable as ConfigurableObject;
			if (configurableObject != null)
			{
				configurableObject.ResetChangeTracking();
			}
			return configurable;
		}

		// Token: 0x06000755 RID: 1877 RVA: 0x00016F0E File Offset: 0x0001510E
		IConfigurable[] IConfigDataProvider.Find<T>(QueryFilter filter, ObjectId rootId, bool deepSearch, SortBy sortBy)
		{
			return (IConfigurable[])((IConfigDataProvider)this).FindPaged<T>(filter, rootId, deepSearch, sortBy, int.MaxValue).ToArray<T>();
		}

		// Token: 0x06000756 RID: 1878 RVA: 0x0001714C File Offset: 0x0001534C
		IEnumerable<T> IConfigDataProvider.FindPaged<T>(QueryFilter filter, ObjectId rootId, bool deepSearch, SortBy sortBy, int pageSize)
		{
			foreach (T t in this.FindImpl<T>(base.AddTenantIdFilter(filter), rootId, deepSearch, sortBy, pageSize))
			{
				ConfigurableObject configurableObject = t as ConfigurableObject;
				if (configurableObject != null)
				{
					configurableObject.ResetChangeTracking();
				}
				yield return t;
			}
			yield break;
		}

		// Token: 0x06000757 RID: 1879 RVA: 0x0001718E File Offset: 0x0001538E
		void IConfigDataProvider.Save(IConfigurable configurable)
		{
			base.FixOrganizationalUnitRoot(configurable);
			base.GenerateIdForObject(configurable);
			base.ApplyAuditProperties(configurable);
			base.DataProvider.Save(configurable);
		}

		// Token: 0x06000758 RID: 1880 RVA: 0x000171B1 File Offset: 0x000153B1
		void IConfigDataProvider.Delete(IConfigurable configurable)
		{
			base.FixOrganizationalUnitRoot(configurable);
			base.GenerateIdForObject(configurable);
			base.ApplyAuditProperties(configurable);
			if (configurable is BindingStorage)
			{
				this.DeleteBindingStorage((BindingStorage)configurable);
				return;
			}
			base.DataProvider.Delete(configurable);
		}

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x06000759 RID: 1881 RVA: 0x000171E9 File Offset: 0x000153E9
		string IConfigDataProvider.Source
		{
			get
			{
				return "FfoConfigurationSession";
			}
		}

		// Token: 0x0600075A RID: 1882 RVA: 0x000171F0 File Offset: 0x000153F0
		OfflineAddressBook[] IConfigurationSession.FindOABsForWebDistributionPoint(ADOabVirtualDirectory vDir)
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
			return new OfflineAddressBook[0];
		}

		// Token: 0x0600075B RID: 1883 RVA: 0x000171FE File Offset: 0x000153FE
		public AvailabilityAddressSpace GetAvailabilityAddressSpace(string domainName)
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
			return null;
		}

		// Token: 0x0600075C RID: 1884 RVA: 0x00017207 File Offset: 0x00015407
		public AvailabilityConfig GetAvailabilityConfig()
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
			return null;
		}

		// Token: 0x0600075D RID: 1885 RVA: 0x00017210 File Offset: 0x00015410
		bool IConfigurationSession.CheckForRetentionPolicyWithConflictingRetentionId(Guid retentionId, out string duplicateName)
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
			duplicateName = null;
			return true;
		}

		// Token: 0x0600075E RID: 1886 RVA: 0x0001721C File Offset: 0x0001541C
		bool IConfigurationSession.CheckForRetentionPolicyWithConflictingRetentionId(Guid retentionId, string identity, out string duplicateName)
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
			duplicateName = null;
			return true;
		}

		// Token: 0x0600075F RID: 1887 RVA: 0x00017228 File Offset: 0x00015428
		bool IConfigurationSession.CheckForRetentionTagWithConflictingRetentionId(Guid retentionId, out string duplicateName)
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
			duplicateName = null;
			return true;
		}

		// Token: 0x06000760 RID: 1888 RVA: 0x00017234 File Offset: 0x00015434
		bool IConfigurationSession.CheckForRetentionTagWithConflictingRetentionId(Guid retentionId, string identity, out string duplicateName)
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
			duplicateName = null;
			return true;
		}

		// Token: 0x06000761 RID: 1889 RVA: 0x00017240 File Offset: 0x00015440
		void IConfigurationSession.DeleteTree(ADConfigurationObject instanceToDelete, TreeDeleteNotFinishedHandler handler)
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
			((IConfigDataProvider)this).Delete(instanceToDelete);
		}

		// Token: 0x06000762 RID: 1890 RVA: 0x0001724F File Offset: 0x0001544F
		AcceptedDomain[] IConfigurationSession.FindAcceptedDomainsByFederatedOrgId(FederatedOrganizationId federatedOrganizationId)
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
			return new AcceptedDomain[0];
		}

		// Token: 0x06000763 RID: 1891 RVA: 0x0001725D File Offset: 0x0001545D
		ADPagedReader<TResult> IConfigurationSession.FindAllPaged<TResult>()
		{
			return new FfoPagedReader<TResult>(this, null, null);
		}

		// Token: 0x06000764 RID: 1892 RVA: 0x00017267 File Offset: 0x00015467
		ExchangeRoleAssignment[] IConfigurationSession.FindAssignmentsForManagementScope(ManagementScope managementScope, bool returnAll)
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
			return new ExchangeRoleAssignment[0];
		}

		// Token: 0x06000765 RID: 1893 RVA: 0x00017278 File Offset: 0x00015478
		T IConfigurationSession.FindMailboxPolicyByName<T>(string name)
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
			return default(T);
		}

		// Token: 0x06000766 RID: 1894 RVA: 0x00017294 File Offset: 0x00015494
		MicrosoftExchangeRecipient IConfigurationSession.FindMicrosoftExchangeRecipient()
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
			return null;
		}

		// Token: 0x06000767 RID: 1895 RVA: 0x0001729D File Offset: 0x0001549D
		ThrottlingPolicy[] IConfigurationSession.FindOrganizationThrottlingPolicies(OrganizationId organizationId)
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
			return null;
		}

		// Token: 0x06000768 RID: 1896 RVA: 0x000172A6 File Offset: 0x000154A6
		ADPagedReader<TResult> IConfigurationSession.FindPaged<TResult>(ADObjectId rootId, QueryScope scope, QueryFilter filter, SortBy sortBy, int pageSize)
		{
			return new FfoPagedReader<TResult>(this, filter, rootId, pageSize);
		}

		// Token: 0x06000769 RID: 1897 RVA: 0x000172B2 File Offset: 0x000154B2
		Result<ExchangeRoleAssignment>[] IConfigurationSession.FindRoleAssignmentsByUserIds(ADObjectId[] securityPrincipalIds, bool partnerMode)
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
			return new Result<ExchangeRoleAssignment>[0];
		}

		// Token: 0x0600076A RID: 1898 RVA: 0x000172C0 File Offset: 0x000154C0
		Result<ExchangeRoleAssignment>[] IConfigurationSession.FindRoleAssignmentsByUserIds(ADObjectId[] securityPrincipalIds, QueryFilter additionalFilter)
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
			return new Result<ExchangeRoleAssignment>[0];
		}

		// Token: 0x0600076B RID: 1899 RVA: 0x000172CE File Offset: 0x000154CE
		ManagementScope[] IConfigurationSession.FindSimilarManagementScope(ManagementScope managementScope)
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
			return new ManagementScope[0];
		}

		// Token: 0x0600076C RID: 1900 RVA: 0x000172DC File Offset: 0x000154DC
		T IConfigurationSession.FindSingletonConfigurationObject<T>()
		{
			IConfigurable[] array = ((IConfigDataProvider)this).Find<T>(null, null, false, null);
			if (array == null || array.Length == 0)
			{
				return default(T);
			}
			return (T)((object)array[0]);
		}

		// Token: 0x0600076D RID: 1901 RVA: 0x00017310 File Offset: 0x00015510
		AcceptedDomain IConfigurationSession.GetAcceptedDomainByDomainName(string domainName)
		{
			return base.FindTenantObject<AcceptedDomain>(new object[]
			{
				ADObjectSchema.Name,
				domainName
			}).Cast<AcceptedDomain>().FirstOrDefault<AcceptedDomain>();
		}

		// Token: 0x0600076E RID: 1902 RVA: 0x00017341 File Offset: 0x00015541
		ADPagedReader<ManagementScope> IConfigurationSession.GetAllExclusiveScopes()
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
			return new FfoPagedReader<ManagementScope>(this, null, null);
		}

		// Token: 0x0600076F RID: 1903 RVA: 0x00017354 File Offset: 0x00015554
		ADPagedReader<ManagementScope> IConfigurationSession.GetAllScopes(OrganizationId organizationId, ScopeRestrictionType restrictionType)
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
			ComparisonFilter filter = new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.OrganizationalUnitRoot, organizationId.OrganizationalUnit);
			return new FfoPagedReader<ManagementScope>(this, filter, null);
		}

		// Token: 0x06000770 RID: 1904 RVA: 0x0001738C File Offset: 0x0001558C
		AcceptedDomain IConfigurationSession.GetDefaultAcceptedDomain()
		{
			IConfigurable[] source = ((IConfigDataProvider)this).Find<AcceptedDomain>(null, null, false, null);
			return source.Cast<AcceptedDomain>().FirstOrDefault((AcceptedDomain acceptedDomain) => acceptedDomain.Default);
		}

		// Token: 0x06000771 RID: 1905 RVA: 0x000173CC File Offset: 0x000155CC
		ExchangeConfigurationContainer IConfigurationSession.GetExchangeConfigurationContainer()
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
			return null;
		}

		// Token: 0x06000772 RID: 1906 RVA: 0x000173D5 File Offset: 0x000155D5
		ExchangeConfigurationContainerWithAddressLists IConfigurationSession.GetExchangeConfigurationContainerWithAddressLists()
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
			return null;
		}

		// Token: 0x06000773 RID: 1907 RVA: 0x000173DE File Offset: 0x000155DE
		FederatedOrganizationId IConfigurationSession.GetFederatedOrganizationId()
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
			return null;
		}

		// Token: 0x06000774 RID: 1908 RVA: 0x000173E7 File Offset: 0x000155E7
		FederatedOrganizationId IConfigurationSession.GetFederatedOrganizationId(OrganizationId organizationId)
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
			return null;
		}

		// Token: 0x06000775 RID: 1909 RVA: 0x000173F0 File Offset: 0x000155F0
		FederatedOrganizationId IConfigurationSession.GetFederatedOrganizationIdByDomainName(string domainName)
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
			return null;
		}

		// Token: 0x06000776 RID: 1910 RVA: 0x000173F9 File Offset: 0x000155F9
		NspiRpcClientConnection IConfigurationSession.GetNspiRpcClientConnection()
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
			return null;
		}

		// Token: 0x06000777 RID: 1911 RVA: 0x00017402 File Offset: 0x00015602
		OrganizationRelationship IConfigurationSession.GetOrganizationRelationship(string domainName)
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
			return null;
		}

		// Token: 0x06000778 RID: 1912 RVA: 0x0001740B File Offset: 0x0001560B
		ThrottlingPolicy IConfigurationSession.GetOrganizationThrottlingPolicy(OrganizationId organizationId)
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
			return null;
		}

		// Token: 0x06000779 RID: 1913 RVA: 0x00017414 File Offset: 0x00015614
		ThrottlingPolicy IConfigurationSession.GetOrganizationThrottlingPolicy(OrganizationId organizationId, bool logFailedLookup)
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
			return null;
		}

		// Token: 0x0600077A RID: 1914 RVA: 0x0001741D File Offset: 0x0001561D
		Organization IConfigurationSession.GetOrgContainer()
		{
			return ((IConfigurationSession)this).Read<Organization>(base.TenantId);
		}

		// Token: 0x0600077B RID: 1915 RVA: 0x0001742B File Offset: 0x0001562B
		ADObjectId IConfigurationSession.GetOrgContainerId()
		{
			return base.TenantId;
		}

		// Token: 0x0600077C RID: 1916 RVA: 0x00017433 File Offset: 0x00015633
		RbacContainer IConfigurationSession.GetRbacContainer()
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
			return null;
		}

		// Token: 0x0600077D RID: 1917 RVA: 0x0001743C File Offset: 0x0001563C
		bool IConfigurationSession.ManagementScopeIsInUse(ManagementScope managementScope)
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
			return false;
		}

		// Token: 0x0600077E RID: 1918 RVA: 0x00017448 File Offset: 0x00015648
		public TResult FindByExchangeObjectId<TResult>(Guid exchangeObjectId) where TResult : ADConfigurationObject, new()
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
			return default(TResult);
		}

		// Token: 0x0600077F RID: 1919 RVA: 0x00017464 File Offset: 0x00015664
		TResult IConfigurationSession.Read<TResult>(ADObjectId entryId)
		{
			return (TResult)((object)((IConfigDataProvider)this).Read<TResult>(entryId));
		}

		// Token: 0x06000780 RID: 1920 RVA: 0x00017474 File Offset: 0x00015674
		Result<TResult>[] IConfigurationSession.ReadMultiple<TResult>(ADObjectId[] identities)
		{
			int num = 0;
			Result<TResult>[] array = new Result<TResult>[identities.Length];
			foreach (ADObjectId entryId in identities)
			{
				TResult data = ((IConfigurationSession)this).Read<TResult>(entryId);
				array[num++] = new Result<TResult>(data, null);
			}
			return array;
		}

		// Token: 0x06000781 RID: 1921 RVA: 0x000174C8 File Offset: 0x000156C8
		MultiValuedProperty<ReplicationCursor> IConfigurationSession.ReadReplicationCursors(ADObjectId id)
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
			return null;
		}

		// Token: 0x06000782 RID: 1922 RVA: 0x000174D1 File Offset: 0x000156D1
		void IConfigurationSession.ReadReplicationData(ADObjectId id, out MultiValuedProperty<ReplicationCursor> replicationCursors, out MultiValuedProperty<ReplicationNeighbor> repsFrom)
		{
			replicationCursors = null;
			repsFrom = null;
			FfoDirectorySession.LogNotSupportedInFFO(null);
		}

		// Token: 0x06000783 RID: 1923 RVA: 0x000174DF File Offset: 0x000156DF
		void IConfigurationSession.Save(ADConfigurationObject instanceToSave)
		{
			if (instanceToSave is BindingStorage)
			{
				this.SaveBindingStorage((BindingStorage)instanceToSave);
				return;
			}
			((IConfigDataProvider)this).Save(instanceToSave);
		}

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x06000784 RID: 1924 RVA: 0x000174FD File Offset: 0x000156FD
		ADObjectId IConfigurationSession.ConfigurationNamingContext
		{
			get
			{
				FfoDirectorySession.LogNotSupportedInFFO(null);
				return null;
			}
		}

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x06000785 RID: 1925 RVA: 0x00017506 File Offset: 0x00015706
		ADObjectId IConfigurationSession.DeletedObjectsContainer
		{
			get
			{
				FfoDirectorySession.LogNotSupportedInFFO(null);
				return null;
			}
		}

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x06000786 RID: 1926 RVA: 0x0001750F File Offset: 0x0001570F
		ADObjectId IConfigurationSession.SchemaNamingContext
		{
			get
			{
				FfoDirectorySession.LogNotSupportedInFFO(null);
				return null;
			}
		}

		// Token: 0x06000787 RID: 1927 RVA: 0x00017518 File Offset: 0x00015718
		internal static ExchangeConfigurationUnit GetExchangeConfigurationUnit(FfoTenant ffoTenant)
		{
			if (ffoTenant == null)
			{
				return null;
			}
			ExchangeConfigurationUnit exchangeConfigurationUnit = new ExchangeConfigurationUnit();
			FfoConfigurationSession.SetTenantIds(ffoTenant, exchangeConfigurationUnit);
			exchangeConfigurationUnit.ExternalDirectoryOrganizationId = ffoTenant.TenantId.ObjectGuid.ToString();
			exchangeConfigurationUnit.CompanyTags = ffoTenant.CompanyTags;
			exchangeConfigurationUnit.DirSyncServiceInstance = ffoTenant.ServiceInstance;
			return exchangeConfigurationUnit;
		}

		// Token: 0x06000788 RID: 1928 RVA: 0x00017570 File Offset: 0x00015770
		protected static ADOrganizationalUnit GetADOrganizationalUnit(FfoTenant ffoTenant)
		{
			if (ffoTenant == null)
			{
				return null;
			}
			ADOrganizationalUnit adorganizationalUnit = new ADOrganizationalUnit();
			FfoConfigurationSession.SetTenantIds(ffoTenant, adorganizationalUnit);
			return adorganizationalUnit;
		}

		// Token: 0x06000789 RID: 1929 RVA: 0x00017590 File Offset: 0x00015790
		protected static Organization GetOrganization(FfoTenant ffoTenant)
		{
			if (ffoTenant == null)
			{
				return null;
			}
			Organization organization = new Organization();
			FfoConfigurationSession.SetTenantIds(ffoTenant, organization);
			return organization;
		}

		// Token: 0x0600078A RID: 1930 RVA: 0x000175B0 File Offset: 0x000157B0
		private static void SetTenantIds(FfoTenant ffoTenant, ADObject adTenantObject)
		{
			if (ffoTenant == null)
			{
				return;
			}
			adTenantObject.Name = ffoTenant.TenantName;
			FfoDirectorySession.FixDistinguishedName(adTenantObject, DalHelper.GetTenantDistinguishedName(ffoTenant.TenantName), ffoTenant.TenantId.ObjectGuid, ffoTenant.TenantId.ObjectGuid, null);
		}

		// Token: 0x0600078B RID: 1931 RVA: 0x000175EC File Offset: 0x000157EC
		private IConfigurable ReadImpl<T>(ObjectId id) where T : IConfigurable, new()
		{
			if (typeof(T) == typeof(ExchangeConfigurationUnit))
			{
				FfoTenant ffoTenant = base.ReadAndHandleException<FfoTenant>(base.AddTenantIdFilter(null));
				return FfoConfigurationSession.GetExchangeConfigurationUnit(ffoTenant);
			}
			if (typeof(T) == typeof(ADOrganizationalUnit))
			{
				FfoTenant ffoTenant2 = base.ReadAndHandleException<FfoTenant>(base.AddTenantIdFilter(null));
				return FfoConfigurationSession.GetADOrganizationalUnit(ffoTenant2);
			}
			if (typeof(T) == typeof(Organization))
			{
				FfoTenant ffoTenant3 = base.ReadAndHandleException<FfoTenant>(base.AddTenantIdFilter(null));
				return FfoConfigurationSession.GetOrganization(ffoTenant3);
			}
			T t = base.ReadAndHandleException<T>(base.AddTenantIdFilter(new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Id, id)));
			ADObject adobject = t as ADObject;
			if (adobject != null)
			{
				FfoDirectorySession.FixDistinguishedName(adobject, base.TenantId.DistinguishedName, base.TenantId.ObjectGuid, ((ADObjectId)adobject.Identity).ObjectGuid, null);
			}
			return t;
		}

		// Token: 0x0600078C RID: 1932 RVA: 0x00018194 File Offset: 0x00016394
		private IEnumerable<T> FindImpl<T>(QueryFilter filter, ObjectId rootId, bool deepSearch, SortBy sortBy, int pageSize) where T : IConfigurable, new()
		{
			if (base.TenantId == null)
			{
				FfoDirectorySession.LogNotSupportedInFFO(null);
			}
			else if (typeof(T) == typeof(ExchangeConfigurationUnit))
			{
				IEnumerable<FfoTenant> ffoTenants = base.FindAndHandleException<FfoTenant>(filter, rootId, deepSearch, sortBy, pageSize);
				foreach (FfoTenant ffoTenant in ffoTenants)
				{
					yield return (T)((object)FfoConfigurationSession.GetExchangeConfigurationUnit(ffoTenant));
				}
			}
			else if (typeof(T) == typeof(ADOrganizationalUnit))
			{
				IEnumerable<FfoTenant> ffoTenants2 = base.FindAndHandleException<FfoTenant>(filter, rootId, deepSearch, sortBy, pageSize);
				foreach (FfoTenant ffoTenant2 in ffoTenants2)
				{
					yield return (T)((object)FfoConfigurationSession.GetADOrganizationalUnit(ffoTenant2));
				}
			}
			else if (typeof(T) == typeof(Organization))
			{
				IEnumerable<FfoTenant> ffoTenants3 = base.FindAndHandleException<FfoTenant>(filter, rootId, deepSearch, sortBy, pageSize);
				foreach (FfoTenant ffoTenant3 in ffoTenants3)
				{
					yield return (T)((object)FfoConfigurationSession.GetOrganization(ffoTenant3));
				}
			}
			else if (typeof(T) == typeof(TransportRuleCollection))
			{
				IEnumerable<TransportRuleCollection> collections = this.FindTransportRuleCollections(filter, rootId, deepSearch, sortBy, pageSize);
				foreach (TransportRuleCollection coll in collections)
				{
					yield return (T)((object)coll);
				}
			}
			else if (typeof(T) == typeof(BindingStorage))
			{
				IEnumerable<BindingStorage> bindings = this.FindBindingStorage(filter, rootId, deepSearch, sortBy, pageSize, true);
				foreach (BindingStorage storage in bindings)
				{
					yield return (T)((object)storage);
				}
			}
			else if (typeof(T) == typeof(ExchangeRoleAssignment))
			{
				IEnumerable<ExchangeRoleAssignment> roleAssignments = this.FindExchangeRoleAssignments(filter, rootId, deepSearch, sortBy, pageSize, true);
				foreach (ExchangeRoleAssignment roleAssignment in roleAssignments)
				{
					yield return (T)((object)roleAssignment);
				}
			}
			else if (typeof(T) == typeof(ExchangeRole))
			{
				IEnumerable<ExchangeRole> exchangeRoles = base.FindAndHandleException<ExchangeRole>(filter, rootId, deepSearch, sortBy, pageSize);
				foreach (ExchangeRole exchangeRole in exchangeRoles)
				{
					FfoDirectorySession.FixDistinguishedName(exchangeRole, base.TenantId.DistinguishedName, base.TenantId.ObjectGuid, ((ADObjectId)exchangeRole.Identity).ObjectGuid, ExchangeRole.RdnContainer);
					this.UpdateImplictScope(exchangeRole);
					yield return (T)((object)exchangeRole);
				}
			}
			else
			{
				IEnumerable<T> configurables = null;
				try
				{
					configurables = base.FindAndHandleException<T>(filter, rootId, deepSearch, sortBy, pageSize);
				}
				catch (DataProviderMappingException ex)
				{
					FfoDirectorySession.LogNotSupportedInFFO(ex);
				}
				if (configurables == null || configurables.Count<T>() == 0)
				{
					configurables = base.GetDefaultArray<T>();
				}
				configurables = this.DoPostQueryFilter<T>(filter, configurables);
				foreach (T configurable in configurables)
				{
					ADObject adObject = configurable as ADObject;
					if (adObject != null)
					{
						FfoDirectorySession.FixDistinguishedName(adObject, base.TenantId.DistinguishedName, base.TenantId.ObjectGuid, ((ADObjectId)adObject.Identity).ObjectGuid, null);
					}
					yield return configurable;
				}
			}
			yield break;
		}

		// Token: 0x0600078D RID: 1933 RVA: 0x00018370 File Offset: 0x00016570
		private IEnumerable<T> DoPostQueryFilter<T>(QueryFilter filter, IEnumerable<T> configurables) where T : IConfigurable, new()
		{
			var func = null;
			var func2 = null;
			if (typeof(T) != typeof(DomainContentConfig) || filter == null)
			{
				return configurables;
			}
			string input = filter.ToString();
			Match match = FfoConfigurationSession.domainContentConfigDupCheckFilterRegex.Match(input);
			if (match.Success)
			{
				ReadOnlyCollection<QueryFilter> filters = ((AndFilter)filter).Filters;
				filters = ((AndFilter)filters[0]).Filters;
				Guid idGuid = (Guid)((ComparisonFilter)filters[0]).PropertyValue;
				ReadOnlyCollection<QueryFilter> filters2 = ((OrFilter)filters[1]).Filters;
				string domainName1 = (string)((ComparisonFilter)filters2[0]).PropertyValue;
				string domainName2 = (string)((ComparisonFilter)filters2[1]).PropertyValue;
				IEnumerable<DomainContentConfig> source = configurables.Cast<DomainContentConfig>();
				if (func == null)
				{
					func = ((DomainContentConfig dcc) => new
					{
						dcc = dcc,
						dccDomainName = ((dcc.DomainName != null) ? dcc.DomainName.Domain : null)
					});
				}
				var source2 = from <>h__TransparentIdentifier4b in source.Select(func)
				where <>h__TransparentIdentifier4b.dcc.Guid != idGuid && (string.Equals(<>h__TransparentIdentifier4b.dccDomainName, domainName1, StringComparison.InvariantCultureIgnoreCase) || string.Equals(<>h__TransparentIdentifier4b.dccDomainName, domainName2, StringComparison.InvariantCultureIgnoreCase))
				select <>h__TransparentIdentifier4b;
				if (func2 == null)
				{
					func2 = (<>h__TransparentIdentifier4b => (T)((object)<>h__TransparentIdentifier4b.dcc));
				}
				return source2.Select(func2);
			}
			return configurables;
		}

		// Token: 0x0600078E RID: 1934 RVA: 0x0001849C File Offset: 0x0001669C
		private IEnumerable<TransportRuleCollection> FindTransportRuleCollections(QueryFilter filter, ObjectId rootId, bool deepSearch, SortBy sortBy, int pageSize = 2147483647)
		{
			object obj;
			Guid objectGuid;
			IEnumerable<TransportRuleCollection> enumerable;
			if (DalHelper.TryFindPropertyValueByName(filter, ADObjectSchema.Name.Name, out obj) && obj is string && FfoConfigurationSession.builtInTransportRuleContainers.TryGetValue((string)obj, out objectGuid))
			{
				TransportRuleCollection transportRuleCollection = new TransportRuleCollection
				{
					Name = (string)obj
				};
				FfoDirectorySession.FixDistinguishedName(transportRuleCollection, base.TenantId.DistinguishedName, base.TenantId.ObjectGuid, objectGuid, null);
				enumerable = new TransportRuleCollection[]
				{
					transportRuleCollection
				};
			}
			else
			{
				enumerable = base.FindAndHandleException<TransportRuleCollection>(filter, rootId, deepSearch, sortBy, pageSize);
				foreach (TransportRuleCollection transportRuleCollection2 in enumerable)
				{
					FfoDirectorySession.FixDistinguishedName(transportRuleCollection2, base.TenantId.DistinguishedName, base.TenantId.ObjectGuid, ((ADObjectId)transportRuleCollection2.Identity).ObjectGuid, null);
				}
			}
			return enumerable;
		}

		// Token: 0x0600078F RID: 1935 RVA: 0x00018978 File Offset: 0x00016B78
		private IEnumerable<ExchangeRoleAssignment> FindExchangeRoleAssignments(QueryFilter filter, ObjectId rootId, bool deepSearch, SortBy sortBy, int pageSize = 2147483647, bool includeScopes = true)
		{
			IEnumerable<ExchangeRoleAssignment> roleAssignments = base.FindAndHandleException<ExchangeRoleAssignment>(filter, rootId, deepSearch, sortBy, pageSize);
			IEnumerable<string> roleNames = (from roleAssignment in roleAssignments
			select roleAssignment.Role.Name).Distinct(StringComparer.OrdinalIgnoreCase).ToArray<string>();
			Dictionary<string, ExchangeRole> cannedRoles = new Dictionary<string, ExchangeRole>(StringComparer.OrdinalIgnoreCase);
			foreach (string text in roleNames)
			{
				ExchangeRole exchangeRole = new ExchangeRole
				{
					Name = text
				};
				if (this.UpdateImplictScope(exchangeRole))
				{
					cannedRoles.Add(text, exchangeRole);
				}
			}
			foreach (ExchangeRoleAssignment roleAssignment2 in roleAssignments)
			{
				FfoDirectorySession.FixDistinguishedName(roleAssignment2, base.TenantId.DistinguishedName, base.TenantId.ObjectGuid, ((ADObjectId)roleAssignment2.Identity).ObjectGuid, ExchangeRoleAssignment.RdnContainer);
				roleAssignment2.Role = FfoDirectorySession.GetUpdatedADObjectIdWithDN(roleAssignment2.Role, base.TenantId.DistinguishedName, base.TenantId.ObjectGuid, ExchangeRole.RdnContainer);
				if (roleAssignment2.RoleAssigneeType == RoleAssigneeType.RoleGroup && cannedRoles.ContainsKey(roleAssignment2.Role.Name))
				{
					ExchangeRole exchangeRole2 = cannedRoles[roleAssignment2.Role.Name];
					roleAssignment2.RecipientReadScope = exchangeRole2.ImplicitRecipientReadScope;
					roleAssignment2.ConfigReadScope = exchangeRole2.ImplicitConfigReadScope;
					roleAssignment2.RecipientWriteScope = (RecipientWriteScopeType)exchangeRole2.ImplicitRecipientWriteScope;
					roleAssignment2.ConfigWriteScope = (ConfigWriteScopeType)exchangeRole2.ImplicitConfigWriteScope;
				}
				yield return roleAssignment2;
			}
			yield break;
		}

		// Token: 0x06000790 RID: 1936 RVA: 0x00018D14 File Offset: 0x00016F14
		private IEnumerable<BindingStorage> FindBindingStorage(QueryFilter filter, ObjectId rootId, bool deepSearch, SortBy sortBy, int pageSize = 2147483647, bool includeScopes = true)
		{
			IEnumerable<BindingStorage> configurables = base.FindAndHandleException<BindingStorage>(filter, rootId, deepSearch, sortBy, pageSize);
			if (configurables == null || configurables.Count<BindingStorage>() == 0)
			{
				configurables = base.GetDefaultArray<BindingStorage>();
			}
			configurables = this.DoPostQueryFilter<BindingStorage>(filter, configurables);
			foreach (BindingStorage configurable in configurables)
			{
				FfoDirectorySession.FixDistinguishedName(configurable, base.TenantId.DistinguishedName, base.TenantId.ObjectGuid, ((ADObjectId)configurable.Identity).ObjectGuid, null);
				IEnumerable<ScopeStorage> childScopes = base.FindAndHandleException<ScopeStorage>(QueryFilter.AndTogether(new QueryFilter[]
				{
					new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.OrganizationalUnitRoot, base.TenantId),
					new ComparisonFilter(ComparisonOperator.Equal, DalHelper.ContainerProp, configurable.Id.ObjectGuid)
				}), rootId, deepSearch, sortBy, pageSize);
				foreach (ScopeStorage scopeStorage in childScopes)
				{
					scopeStorage.ResetChangeTracking();
				}
				configurable.AppliedScopes = new MultiValuedProperty<ScopeStorage>(childScopes);
				yield return configurable;
			}
			yield break;
		}

		// Token: 0x06000791 RID: 1937 RVA: 0x00018E88 File Offset: 0x00017088
		private void SaveBindingStorage(BindingStorage bindingInstance)
		{
			BindingStorage existingStorage = this.FindBindingStorage(QueryFilter.AndTogether(new QueryFilter[]
			{
				new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.OrganizationalUnitRoot, base.TenantId),
				new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Id, bindingInstance.Id.ObjectGuid)
			}), null, false, null, int.MaxValue, true).Cast<BindingStorage>().FirstOrDefault<BindingStorage>();
			if (existingStorage == null)
			{
				using (MultiValuedProperty<ScopeStorage>.Enumerator enumerator = bindingInstance.AppliedScopes.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						ScopeStorage scopeStorage = enumerator.Current;
						scopeStorage[UnifiedPolicyStorageBaseSchema.ContainerProp] = bindingInstance.Id.ObjectGuid.ToString();
						scopeStorage[UnifiedPolicyStorageBaseSchema.WorkloadProp] = bindingInstance.Workload;
						((IConfigDataProvider)this).Save(scopeStorage);
					}
					goto IL_20A;
				}
			}
			IEnumerable<ScopeStorage> enumerable = from s in bindingInstance.AppliedScopes
			where !existingStorage.AppliedScopes.Any((ScopeStorage e) => e.Id.ObjectGuid == s.Id.ObjectGuid) || bindingInstance.AppliedScopes.Any((ScopeStorage e) => e.Id.ObjectGuid == s.Id.ObjectGuid && s.GetChangedPropertyDefinitions().Any<PropertyDefinition>())
			select s;
			IEnumerable<ScopeStorage> enumerable2 = from e in existingStorage.AppliedScopes
			where !bindingInstance.AppliedScopes.Any((ScopeStorage n) => n.Id.ObjectGuid == e.Id.ObjectGuid)
			select e;
			foreach (ScopeStorage instance in enumerable2)
			{
				((IConfigDataProvider)this).Delete(instance);
			}
			foreach (ScopeStorage scopeStorage2 in enumerable)
			{
				scopeStorage2[UnifiedPolicyStorageBaseSchema.ContainerProp] = bindingInstance.Id.ObjectGuid.ToString();
				scopeStorage2[UnifiedPolicyStorageBaseSchema.WorkloadProp] = bindingInstance.Workload;
				((IConfigDataProvider)this).Save(scopeStorage2);
			}
			IL_20A:
			bindingInstance[UnifiedPolicyStorageBaseSchema.ContainerProp] = bindingInstance.PolicyId.ToString();
			((IConfigDataProvider)this).Save(bindingInstance);
		}

		// Token: 0x06000792 RID: 1938 RVA: 0x00019100 File Offset: 0x00017300
		private void DeleteBindingStorage(BindingStorage bindingInstance)
		{
			foreach (ScopeStorage instance in bindingInstance.AppliedScopes)
			{
				((IConfigDataProvider)this).Delete(instance);
			}
			base.DataProvider.Delete(bindingInstance);
		}

		// Token: 0x06000793 RID: 1939 RVA: 0x00019160 File Offset: 0x00017360
		private bool UpdateImplictScope(ExchangeRole exchangeRole)
		{
			string value = exchangeRole.Name.Replace(" ", string.Empty).Replace("-", string.Empty);
			RoleType roleType;
			if (Enum.TryParse<RoleType>(value, true, out roleType))
			{
				exchangeRole.RoleType = roleType;
				exchangeRole.StampImplicitScopes();
				exchangeRole.StampIsEndUserRole();
				return true;
			}
			return false;
		}

		// Token: 0x0400045F RID: 1119
		private static readonly Dictionary<string, Guid> builtInTransportRuleContainers = new Dictionary<string, Guid>
		{
			{
				"MalwareFilterVersioned",
				Guid.Parse("66BF36AA-ECD6-404e-9CD8-F2A9C1037154")
			},
			{
				"HostedContentFilterVersioned",
				Guid.Parse("42A3E6E5-1048-4c22-8990-CFFE8ACDCFC1")
			}
		};

		// Token: 0x04000460 RID: 1120
		private static Regex domainContentConfigDupCheckFilterRegex = new Regex("\\(\\&\\(\\(\\&\\(\\(Guid NotEqual [^\\)]+\\)\\(\\|\\(\\(DomainName Equal [^\\)]+\\)\\(DomainName Equal [^\\)]+\\)\\)\\)\\)\\)\\(OrganizationalUnitRoot Equal [^\\)]+\\)\\)\\)");
	}
}
