using System;
using System.Collections.Generic;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000159 RID: 345
	[Serializable]
	public sealed class TenantRelocationRequestIdParameter : ADIdParameter
	{
		// Token: 0x06000C71 RID: 3185 RVA: 0x00027224 File Offset: 0x00025424
		public TenantRelocationRequestIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x06000C72 RID: 3186 RVA: 0x0002722D File Offset: 0x0002542D
		public TenantRelocationRequestIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x06000C73 RID: 3187 RVA: 0x00027236 File Offset: 0x00025436
		public TenantRelocationRequestIdParameter()
		{
		}

		// Token: 0x06000C74 RID: 3188 RVA: 0x0002723E File Offset: 0x0002543E
		public TenantRelocationRequestIdParameter(ADObjectId adObjectId) : base(adObjectId)
		{
		}

		// Token: 0x06000C75 RID: 3189 RVA: 0x00027247 File Offset: 0x00025447
		public TenantRelocationRequestIdParameter(TenantRelocationRequest tenant) : this(tenant.Name)
		{
		}

		// Token: 0x06000C76 RID: 3190 RVA: 0x00027255 File Offset: 0x00025455
		public TenantRelocationRequestIdParameter(OrganizationId organizationId) : base(organizationId.OrganizationalUnit)
		{
		}

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x06000C77 RID: 3191 RVA: 0x00027264 File Offset: 0x00025464
		protected override QueryFilter AdditionalQueryFilter
		{
			get
			{
				return QueryFilter.AndTogether(new QueryFilter[]
				{
					base.AdditionalQueryFilter,
					TenantRelocationRequest.TenantRelocationRequestFilter,
					new ExistsFilter(ADOrganizationalUnitSchema.ConfigurationUnitLink)
				});
			}
		}

		// Token: 0x06000C78 RID: 3192 RVA: 0x0002729C File Offset: 0x0002549C
		public static TenantRelocationRequestIdParameter Parse(string identity)
		{
			return new TenantRelocationRequestIdParameter(identity);
		}

		// Token: 0x06000C79 RID: 3193 RVA: 0x000272A4 File Offset: 0x000254A4
		internal OrganizationId ResolveOrganizationId()
		{
			if (string.IsNullOrEmpty(base.RawIdentity) || string.IsNullOrEmpty(base.RawIdentity.Trim()))
			{
				throw new ArgumentException("Cannot resolve tenant name - RawIdentity is null or empty");
			}
			ExchangeConfigurationUnit exchangeConfigurationUnit = null;
			try
			{
				PartitionId partitionId = ADAccountPartitionLocator.GetPartitionIdByAcceptedDomainName(base.RawIdentity);
				ADSessionSettings adsessionSettings = ADSessionSettings.FromAllTenantsPartitionId(partitionId);
				adsessionSettings.TenantConsistencyMode = TenantConsistencyMode.IncludeRetiredTenants;
				ITenantConfigurationSession tenantConfigurationSession = DirectorySessionFactory.Default.CreateTenantConfigurationSession(ConsistencyMode.PartiallyConsistent, adsessionSettings, 142, "ResolveOrganizationId", "f:\\15.00.1497\\sources\\dev\\Configuration\\src\\ObjectModel\\IdentityParameter\\TenantRelocationRequestIdParameter.cs");
				exchangeConfigurationUnit = tenantConfigurationSession.GetExchangeConfigurationUnitByName(base.RawIdentity);
			}
			catch (CannotResolveTenantNameException)
			{
			}
			if (exchangeConfigurationUnit == null)
			{
				foreach (PartitionId partitionId2 in ADAccountPartitionLocator.GetAllAccountPartitionIds())
				{
					ADSessionSettings adsessionSettings2 = ADSessionSettings.FromAllTenantsPartitionId(partitionId2);
					adsessionSettings2.TenantConsistencyMode = TenantConsistencyMode.IncludeRetiredTenants;
					ITenantConfigurationSession tenantConfigurationSession = DirectorySessionFactory.Default.CreateTenantConfigurationSession(ConsistencyMode.PartiallyConsistent, adsessionSettings2, 160, "ResolveOrganizationId", "f:\\15.00.1497\\sources\\dev\\Configuration\\src\\ObjectModel\\IdentityParameter\\TenantRelocationRequestIdParameter.cs");
					exchangeConfigurationUnit = tenantConfigurationSession.GetExchangeConfigurationUnitByName(base.RawIdentity);
					if (exchangeConfigurationUnit != null)
					{
						break;
					}
				}
			}
			if (exchangeConfigurationUnit != null && !string.IsNullOrEmpty(exchangeConfigurationUnit.RelocationSourceForestRaw))
			{
				PartitionId partitionId = new PartitionId(exchangeConfigurationUnit.RelocationSourceForestRaw);
				ADSessionSettings adsessionSettings = ADSessionSettings.FromAllTenantsPartitionId(partitionId);
				adsessionSettings.TenantConsistencyMode = TenantConsistencyMode.IncludeRetiredTenants;
				ITenantConfigurationSession tenantConfigurationSession = DirectorySessionFactory.Default.CreateTenantConfigurationSession(ConsistencyMode.PartiallyConsistent, adsessionSettings, 175, "ResolveOrganizationId", "f:\\15.00.1497\\sources\\dev\\Configuration\\src\\ObjectModel\\IdentityParameter\\TenantRelocationRequestIdParameter.cs");
				exchangeConfigurationUnit = tenantConfigurationSession.GetExchangeConfigurationUnitByName(base.RawIdentity);
			}
			if (exchangeConfigurationUnit != null)
			{
				return exchangeConfigurationUnit.OrganizationId;
			}
			throw new CannotResolveTenantNameException(DirectoryStrings.CannotResolveTenantRelocationRequestIdentity(base.RawIdentity));
		}

		// Token: 0x06000C7A RID: 3194 RVA: 0x00027404 File Offset: 0x00025604
		internal override IEnumerable<T> GetObjects<T>(ADObjectId rootId, IDirectorySession session, IDirectorySession subTreeSession, OptionalIdentityData optionalData, out LocalizedString? notFoundReason)
		{
			TaskLogger.LogEnter();
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			if (!(session is IConfigurationSession))
			{
				throw new ArgumentException(Strings.ErrorInvalidType(session.GetType().Name), "type");
			}
			notFoundReason = null;
			EnumerableWrapper<T> result = EnumerableWrapper<T>.Empty;
			if (this.IsWildcardDefined(base.RawIdentity))
			{
				notFoundReason = new LocalizedString?(Strings.ErrorOrganizationWildcard);
				return result;
			}
			OrganizationId organizationId = this.ResolveOrganizationId();
			if (!OrganizationId.ForestWideOrgId.Equals(organizationId))
			{
				ADSessionSettings adsessionSettings = ADSessionSettings.FromCustomScopeSet(ScopeSet.ResolveUnderScope(organizationId, session.SessionSettings.ScopeSet), session.SessionSettings.RootOrgId, organizationId, session.SessionSettings.ExecutingUserOrganizationId, false);
				adsessionSettings.TenantConsistencyMode = TenantConsistencyMode.IncludeRetiredTenants;
				ITenantConfigurationSession tenantConfigurationSession = DirectorySessionFactory.Default.CreateTenantConfigurationSession(session.DomainController, session.ReadOnly, session.ConsistencyMode, session.NetworkCredential, adsessionSettings, 257, "GetObjects", "f:\\15.00.1497\\sources\\dev\\Configuration\\src\\ObjectModel\\IdentityParameter\\TenantRelocationRequestIdParameter.cs");
				tenantConfigurationSession.UseConfigNC = session.UseConfigNC;
				tenantConfigurationSession.UseGlobalCatalog = session.UseGlobalCatalog;
				if (typeof(TenantRelocationRequest).Equals(typeof(T)) && organizationId.ConfigurationUnit != null)
				{
					List<TenantRelocationRequest> list = new List<TenantRelocationRequest>();
					TenantRelocationRequest[] array = tenantConfigurationSession.Find<TenantRelocationRequest>(organizationId.ConfigurationUnit, QueryScope.SubTree, TenantRelocationRequest.TenantRelocationRequestFilter, null, 1);
					if (array != null && array.Length > 0)
					{
						list.Add(array[0]);
						result = EnumerableWrapper<T>.GetWrapper((IEnumerable<T>)list, this.GetEnumerableFilter<T>());
					}
				}
			}
			TaskLogger.LogExit();
			return result;
		}
	}
}
