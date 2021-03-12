using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Hygiene.Data.Directory
{
	// Token: 0x020000DB RID: 219
	[Serializable]
	internal class FfoTenantConfigurationSession : FfoConfigurationSession, ITenantConfigurationSession, IConfigurationSession, IDirectorySession, IConfigDataProvider
	{
		// Token: 0x06000849 RID: 2121 RVA: 0x0001ACEA File Offset: 0x00018EEA
		public FfoTenantConfigurationSession(ConsistencyMode consistencyMode, ADSessionSettings sessionSettings) : base(true, true, consistencyMode, null, sessionSettings)
		{
		}

		// Token: 0x0600084A RID: 2122 RVA: 0x0001ACF7 File Offset: 0x00018EF7
		public FfoTenantConfigurationSession(bool readOnly, ConsistencyMode consistencyMode, ADSessionSettings sessionSettings) : base(true, readOnly, consistencyMode, null, sessionSettings)
		{
		}

		// Token: 0x0600084B RID: 2123 RVA: 0x0001AD04 File Offset: 0x00018F04
		public FfoTenantConfigurationSession(string domainController, bool readOnly, ConsistencyMode consistencyMode, NetworkCredential networkCredential, ADSessionSettings sessionSettings) : base(true, readOnly, consistencyMode, networkCredential, sessionSettings)
		{
			base.DomainController = domainController;
		}

		// Token: 0x0600084C RID: 2124 RVA: 0x0001AD1A File Offset: 0x00018F1A
		public FfoTenantConfigurationSession(string domainController, bool readOnly, ConsistencyMode consistencyMode, NetworkCredential networkCredential, ADSessionSettings sessionSettings, ConfigScopes configScope) : this(domainController, readOnly, consistencyMode, networkCredential, sessionSettings)
		{
			if (ConfigScopes.TenantSubTree != configScope)
			{
				throw new NotSupportedException("Only ConfigScopes.TenantSubTree is supported by this constructor");
			}
			if (ConfigScopes.TenantSubTree == configScope)
			{
				base.ConfigScope = configScope;
			}
		}

		// Token: 0x0600084D RID: 2125 RVA: 0x0001AD46 File Offset: 0x00018F46
		public FfoTenantConfigurationSession(ADObjectId tenantId) : base(tenantId)
		{
		}

		// Token: 0x0600084E RID: 2126 RVA: 0x0001AD50 File Offset: 0x00018F50
		AcceptedDomain[] ITenantConfigurationSession.FindAllAcceptedDomainsInOrg(ADObjectId organizationCU)
		{
			ComparisonFilter filter = new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.OrganizationalUnitRoot, organizationCU);
			return base.DataProvider.Find<AcceptedDomain>(filter, null, false, null).Cast<AcceptedDomain>().ToArray<AcceptedDomain>();
		}

		// Token: 0x0600084F RID: 2127 RVA: 0x0001AD83 File Offset: 0x00018F83
		ExchangeConfigurationUnit[] ITenantConfigurationSession.FindSharedConfiguration(SharedConfigurationInfo sharedConfigInfo, bool enabledSharedOrgOnly)
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
			return new ExchangeConfigurationUnit[0];
		}

		// Token: 0x06000850 RID: 2128 RVA: 0x0001AD91 File Offset: 0x00018F91
		ExchangeConfigurationUnit[] ITenantConfigurationSession.FindSharedConfigurationByOrganizationId(OrganizationId tinyTenantId)
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
			return new ExchangeConfigurationUnit[0];
		}

		// Token: 0x06000851 RID: 2129 RVA: 0x0001AD9F File Offset: 0x00018F9F
		public ADRawEntry[] FindDeletedADRawEntryByUsnRange(ADObjectId lastKnownParentId, long startUsn, int sizeLimit, IEnumerable<PropertyDefinition> properties)
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
			return new ADRawEntry[0];
		}

		// Token: 0x06000852 RID: 2130 RVA: 0x0001ADB0 File Offset: 0x00018FB0
		ExchangeConfigurationUnit ITenantConfigurationSession.GetExchangeConfigurationUnitByExternalId(string externalDirectoryOrganizationId)
		{
			IEnumerable<FfoTenant> source = base.FindTenantObject<FfoTenant>(new object[]
			{
				ADObjectSchema.OrganizationalUnitRoot,
				new ADObjectId(new Guid(externalDirectoryOrganizationId))
			}).Cast<FfoTenant>();
			return source.Select(new Func<FfoTenant, ExchangeConfigurationUnit>(FfoConfigurationSession.GetExchangeConfigurationUnit)).FirstOrDefault<ExchangeConfigurationUnit>();
		}

		// Token: 0x06000853 RID: 2131 RVA: 0x0001ADFE File Offset: 0x00018FFE
		ExchangeConfigurationUnit ITenantConfigurationSession.GetExchangeConfigurationUnitByExternalId(Guid externalDirectoryOrganizationId)
		{
			return ((ITenantConfigurationSession)this).GetExchangeConfigurationUnitByExternalId(externalDirectoryOrganizationId.ToString());
		}

		// Token: 0x06000854 RID: 2132 RVA: 0x0001AE14 File Offset: 0x00019014
		ExchangeConfigurationUnit ITenantConfigurationSession.GetExchangeConfigurationUnitByName(string organizationName)
		{
			ComparisonFilter filter = new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Name, organizationName);
			FfoTenant ffoTenant = (FfoTenant)base.DataProvider.Find<FfoTenant>(filter, null, false, null).FirstOrDefault<IConfigurable>();
			if (ffoTenant == null)
			{
				return null;
			}
			return FfoConfigurationSession.GetExchangeConfigurationUnit(ffoTenant);
		}

		// Token: 0x06000855 RID: 2133 RVA: 0x0001AE55 File Offset: 0x00019055
		ADObjectId ITenantConfigurationSession.GetExchangeConfigurationUnitIdByName(string organizationName)
		{
			return ((ITenantConfigurationSession)this).GetExchangeConfigurationUnitByName(organizationName).Id;
		}

		// Token: 0x06000856 RID: 2134 RVA: 0x0001AE63 File Offset: 0x00019063
		ExchangeConfigurationUnit[] ITenantConfigurationSession.FindAllOpenConfigurationUnits(bool excludeFull)
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
			return null;
		}

		// Token: 0x06000857 RID: 2135 RVA: 0x0001AE6C File Offset: 0x0001906C
		ExchangeConfigurationUnit ITenantConfigurationSession.GetExchangeConfigurationUnitByNameOrAcceptedDomain(string organizationName)
		{
			return ((ITenantConfigurationSession)this).GetExchangeConfigurationUnitByName(organizationName);
		}

		// Token: 0x06000858 RID: 2136 RVA: 0x0001AE75 File Offset: 0x00019075
		OrganizationId ITenantConfigurationSession.GetOrganizationIdFromOrgNameOrAcceptedDomain(string domainName)
		{
			return ((ITenantConfigurationSession)this).GetOrganizationIdFromOrgNameOrAcceptedDomain(domainName);
		}

		// Token: 0x06000859 RID: 2137 RVA: 0x0001AE7E File Offset: 0x0001907E
		OrganizationId ITenantConfigurationSession.GetOrganizationIdFromExternalDirectoryOrgId(Guid externalDirectoryOrgId)
		{
			return ((ITenantConfigurationSession)this).GetOrganizationIdFromExternalDirectoryOrgId(externalDirectoryOrgId);
		}

		// Token: 0x0600085A RID: 2138 RVA: 0x0001AE87 File Offset: 0x00019087
		ExchangeConfigurationUnit ITenantConfigurationSession.GetExchangeConfigurationUnitByUserNetID(string userNetID)
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
			return null;
		}

		// Token: 0x0600085B RID: 2139 RVA: 0x0001AE90 File Offset: 0x00019090
		MsoTenantCookieContainer ITenantConfigurationSession.GetMsoTenantCookieContainer(Guid contextId)
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
			return null;
		}

		// Token: 0x0600085C RID: 2140 RVA: 0x0001AE99 File Offset: 0x00019099
		Result<ADRawEntry>[] ITenantConfigurationSession.ReadMultipleOrganizationProperties(ADObjectId[] organizationOUIds, PropertyDefinition[] properties)
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
			return null;
		}

		// Token: 0x0600085D RID: 2141 RVA: 0x0001AEA4 File Offset: 0x000190A4
		T ITenantConfigurationSession.GetDefaultFilteringConfiguration<T>()
		{
			string name;
			int num;
			if (typeof(T) == typeof(MalwareFilterPolicy))
			{
				name = MalwareFilterPolicySchema.MalwareFilterPolicyFlags.Name;
				num = 512;
			}
			else if (typeof(T) == typeof(HostedContentFilterPolicy))
			{
				name = HostedContentFilterPolicySchema.SpamFilteringFlags.Name;
				num = 64;
			}
			else if (typeof(T) == typeof(HostedConnectionFilterPolicy))
			{
				name = HostedConnectionFilterPolicySchema.ConnectionFilterFlags.Name;
				num = 1;
			}
			else
			{
				if (!(typeof(T) == typeof(HostedOutboundSpamFilterPolicy)))
				{
					throw new NotSupportedException(string.Format("Type {0} is not supporeted", typeof(T).Name));
				}
				name = HostedOutboundSpamFilterPolicySchema.OutboundSpamFilterFlags.Name;
				num = 0;
			}
			QueryFilter queryFilter = new ComparisonFilter(ComparisonOperator.Equal, DalHelper.PropertyNameProp, name);
			QueryFilter queryFilter2 = new ComparisonFilter(ComparisonOperator.Equal, DalHelper.PropertyValueIntegerProp, num);
			QueryFilter queryFilter3 = new ComparisonFilter(ComparisonOperator.Equal, DalHelper.PropertyValueIntegerMaskProp, num);
			QueryFilter filter = QueryFilter.AndTogether(new QueryFilter[]
			{
				queryFilter,
				queryFilter2,
				queryFilter3
			});
			return (T)((object)((IConfigDataProvider)this).Find<T>(filter, null, true, null).FirstOrDefault<IConfigurable>());
		}

		// Token: 0x0600085E RID: 2142 RVA: 0x0001AFE8 File Offset: 0x000191E8
		public bool IsTenantLockedOut()
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
			return false;
		}

		// Token: 0x0600085F RID: 2143 RVA: 0x0001AFF4 File Offset: 0x000191F4
		public void UndeleteTenant(Guid tenantId, DateTime deletedDatetime)
		{
			if (tenantId == Guid.Empty)
			{
				throw new ArgumentException("The tenantId must not be empty.");
			}
			base.DataProvider.Save(new TenantUndeleteRequest
			{
				TenantId = tenantId,
				DeletedDatetime = deletedDatetime
			});
		}
	}
}
