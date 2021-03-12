using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000397 RID: 919
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class ADTenantConfigurationSession : ADConfigurationSession, ITenantConfigurationSession, IConfigurationSession, IDirectorySession, IConfigDataProvider
	{
		// Token: 0x060029E0 RID: 10720 RVA: 0x000AF87B File Offset: 0x000ADA7B
		public ADTenantConfigurationSession(ConsistencyMode consistencyMode, ADSessionSettings sessionSettings) : base(true, true, consistencyMode, null, sessionSettings)
		{
		}

		// Token: 0x060029E1 RID: 10721 RVA: 0x000AF888 File Offset: 0x000ADA88
		public ADTenantConfigurationSession(bool readOnly, ConsistencyMode consistencyMode, ADSessionSettings sessionSettings) : base(true, readOnly, consistencyMode, null, sessionSettings)
		{
		}

		// Token: 0x060029E2 RID: 10722 RVA: 0x000AF895 File Offset: 0x000ADA95
		public ADTenantConfigurationSession(string domainController, bool readOnly, ConsistencyMode consistencyMode, NetworkCredential networkCredential, ADSessionSettings sessionSettings) : base(true, readOnly, consistencyMode, networkCredential, sessionSettings)
		{
			base.DomainController = domainController;
		}

		// Token: 0x060029E3 RID: 10723 RVA: 0x000AF8AB File Offset: 0x000ADAAB
		public ADTenantConfigurationSession(string domainController, bool readOnly, ConsistencyMode consistencyMode, NetworkCredential networkCredential, ADSessionSettings sessionSettings, ConfigScopes configScope) : this(domainController, readOnly, consistencyMode, networkCredential, sessionSettings)
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

		// Token: 0x17000B9C RID: 2972
		// (get) Token: 0x060029E4 RID: 10724 RVA: 0x000AF8D8 File Offset: 0x000ADAD8
		public override ADObjectId DeletedObjectsContainer
		{
			get
			{
				ADObjectId namingContextId = ADSession.IsTenantConfigInDomainNC(base.SessionSettings.GetAccountOrResourceForestFqdn()) ? base.GetDomainNamingContext() : base.GetConfigurationNamingContext();
				return ADSession.GetDeletedObjectsContainer(namingContextId);
			}
		}

		// Token: 0x060029E5 RID: 10725 RVA: 0x000AF90C File Offset: 0x000ADB0C
		public AcceptedDomain[] FindAllAcceptedDomainsInOrg(ADObjectId organizationCU)
		{
			ADPagedReader<AcceptedDomain> adpagedReader = base.FindPaged<AcceptedDomain>(organizationCU, QueryScope.SubTree, null, null, 0);
			List<AcceptedDomain> list = new List<AcceptedDomain>();
			foreach (AcceptedDomain item in adpagedReader)
			{
				list.Add(item);
			}
			return list.ToArray();
		}

		// Token: 0x060029E6 RID: 10726 RVA: 0x000AF96C File Offset: 0x000ADB6C
		public ADRawEntry[] FindDeletedADRawEntryByUsnRange(ADObjectId lastKnownParentId, long startUsn, int sizeLimit, IEnumerable<PropertyDefinition> properties)
		{
			if (sizeLimit > ADDataSession.RangedValueDefaultPageSize)
			{
				throw new ArgumentOutOfRangeException("sizeLimit");
			}
			QueryFilter filter = new AndFilter(new QueryFilter[]
			{
				new ComparisonFilter(ComparisonOperator.GreaterThanOrEqual, ADRecipientSchema.UsnChanged, startUsn),
				new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.OrganizationalUnitRoot, lastKnownParentId)
			});
			ADObjectId rootId = ADSession.IsTenantConfigInDomainNC(base.SessionSettings.GetAccountOrResourceForestFqdn()) ? ADSession.GetDeletedObjectsContainer(lastKnownParentId.DomainId) : ADSession.GetDeletedObjectsContainer(base.ConfigurationNamingContext);
			return base.Find<ADRawEntry>(rootId, QueryScope.OneLevel, filter, ADDataSession.SortByUsn, sizeLimit, properties, true);
		}

		// Token: 0x060029E7 RID: 10727 RVA: 0x000AF9FC File Offset: 0x000ADBFC
		public ExchangeConfigurationUnit[] FindSharedConfiguration(SharedConfigurationInfo sharedConfigInfo, bool enabledSharedOrgOnly)
		{
			if (!base.SessionSettings.IsGlobal && base.ConfigScope != ConfigScopes.AllTenants)
			{
				throw new NotSupportedException("FindEnabledSharedConfiguration cannot be invoked on non Global sessions");
			}
			if (sharedConfigInfo == null)
			{
				throw new ArgumentNullException("sharedConfigInfo");
			}
			QueryFilter filter = new AndFilter(new QueryFilter[]
			{
				new ComparisonFilter(ComparisonOperator.Equal, OrganizationSchema.SharedConfigurationInfo, sharedConfigInfo),
				new ComparisonFilter(ComparisonOperator.Equal, ExchangeConfigurationUnitSchema.OrganizationStatus, OrganizationStatus.Active),
				new ComparisonFilter(ComparisonOperator.Equal, OrganizationSchema.ImmutableConfiguration, true),
				new ComparisonFilter(ComparisonOperator.Equal, OrganizationSchema.EnableAsSharedConfiguration, enabledSharedOrgOnly)
			});
			return base.Find<ExchangeConfigurationUnit>(null, QueryScope.SubTree, filter, null, 0);
		}

		// Token: 0x060029E8 RID: 10728 RVA: 0x000AFAA4 File Offset: 0x000ADCA4
		public ExchangeConfigurationUnit[] FindSharedConfigurationByOrganizationId(OrganizationId tinyTenantId)
		{
			if (!base.SessionSettings.IsGlobal && base.ConfigScope != ConfigScopes.AllTenants)
			{
				throw new NotSupportedException("FindSharedConfigurationByCU cannot be invoked on non Global sessions");
			}
			if (tinyTenantId == null)
			{
				throw new ArgumentNullException("tinyTenantId");
			}
			if (tinyTenantId == OrganizationId.ForestWideOrgId)
			{
				throw new ArgumentException("tinyTenantId cannot be ForestWideOrgId");
			}
			return base.Find<ExchangeConfigurationUnit>(null, QueryScope.SubTree, new ComparisonFilter(ComparisonOperator.Equal, OrganizationSchema.SupportedSharedConfigurationsBL, tinyTenantId.ConfigurationUnit), null, 0);
		}

		// Token: 0x060029E9 RID: 10729 RVA: 0x000AFB1C File Offset: 0x000ADD1C
		public T GetDefaultFilteringConfiguration<T>() where T : ADConfigurationObject, new()
		{
			QueryFilter filter;
			if (typeof(T) == typeof(HostedSpamFilterConfig))
			{
				filter = new ComparisonFilter(ComparisonOperator.Equal, HostedSpamFilterConfigSchema.IsDefault, true);
			}
			else if (typeof(T) == typeof(HostedContentFilterPolicy))
			{
				filter = new ComparisonFilter(ComparisonOperator.Equal, HostedContentFilterPolicySchema.IsDefault, true);
			}
			else if (typeof(T) == typeof(MalwareFilterPolicy))
			{
				filter = new ComparisonFilter(ComparisonOperator.Equal, MalwareFilterPolicySchema.IsDefault, true);
			}
			else if (typeof(T) == typeof(HostedConnectionFilterPolicy))
			{
				filter = new ComparisonFilter(ComparisonOperator.Equal, HostedConnectionFilterPolicySchema.IsDefault, true);
			}
			else
			{
				if (!(typeof(T) == typeof(HostedOutboundSpamFilterPolicy)))
				{
					throw new NotSupportedException(typeof(T).FullName);
				}
				filter = null;
			}
			T[] array = base.Find<T>(null, QueryScope.SubTree, filter, null, 1);
			if (array == null || array.Length == 0)
			{
				return default(T);
			}
			return array[0];
		}

		// Token: 0x060029EA RID: 10730 RVA: 0x000AFC44 File Offset: 0x000ADE44
		public bool IsTenantLockedOut()
		{
			if (!base.SessionSettings.IsTenantScoped)
			{
				throw new InvalidOperationException("IsTenantLockedOut() is only supported for tenant-scoped sessions");
			}
			return null != base.Find<ExchangeConfigurationUnit>(null, QueryScope.SubTree, new ComparisonFilter(ComparisonOperator.Equal, ExchangeConfigurationUnitSchema.OrganizationStatus, OrganizationStatus.LockedOut), null, 1).FirstOrDefault<ExchangeConfigurationUnit>();
		}

		// Token: 0x060029EB RID: 10731 RVA: 0x000AFC90 File Offset: 0x000ADE90
		public ExchangeConfigurationUnit GetExchangeConfigurationUnitByExternalId(string externalDirectoryOrganizationId)
		{
			ComparisonFilter filter = new ComparisonFilter(ComparisonOperator.Equal, ExchangeConfigurationUnitSchema.ExternalDirectoryOrganizationId, externalDirectoryOrganizationId);
			return base.FindPaged<ExchangeConfigurationUnit>(null, QueryScope.SubTree, filter, null, 1).FirstOrDefault<ExchangeConfigurationUnit>();
		}

		// Token: 0x060029EC RID: 10732 RVA: 0x000AFCBA File Offset: 0x000ADEBA
		public ExchangeConfigurationUnit GetExchangeConfigurationUnitByExternalId(Guid externalDirectoryOrganizationId)
		{
			return this.GetExchangeConfigurationUnitByExternalId(externalDirectoryOrganizationId.ToString());
		}

		// Token: 0x060029ED RID: 10733 RVA: 0x000AFCD0 File Offset: 0x000ADED0
		public ExchangeConfigurationUnit GetExchangeConfigurationUnitByName(string organizationName)
		{
			ADObjectId exchangeConfigurationUnitIdByName = this.GetExchangeConfigurationUnitIdByName(organizationName);
			return base.Read<ExchangeConfigurationUnit>(exchangeConfigurationUnitIdByName);
		}

		// Token: 0x060029EE RID: 10734 RVA: 0x000AFCEC File Offset: 0x000ADEEC
		public ADObjectId GetExchangeConfigurationUnitIdByName(string organizationName)
		{
			ADObjectId configurationUnitsRoot = base.GetConfigurationUnitsRoot();
			return configurationUnitsRoot.GetChildId("CN", organizationName).GetChildId("CN", "Configuration");
		}

		// Token: 0x060029EF RID: 10735 RVA: 0x000AFD1C File Offset: 0x000ADF1C
		public ExchangeConfigurationUnit GetExchangeConfigurationUnitByUserNetID(string userNetID)
		{
			ITenantRecipientSession tenantRecipientSession = DirectorySessionFactory.Default.CreateTenantRecipientSession(true, ConsistencyMode.IgnoreInvalid, base.SessionSettings, 352, "GetExchangeConfigurationUnitByUserNetID", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\SystemConfiguration\\ADTenantConfigurationSession.cs");
			ADRawEntry adrawEntry = tenantRecipientSession.FindByNetID(userNetID, new ADPropertyDefinition[]
			{
				ADObjectSchema.OrganizationalUnitRoot
			}).FirstOrDefault<ADRawEntry>();
			if (adrawEntry == null)
			{
				return null;
			}
			ADObjectId adobjectId = (ADObjectId)adrawEntry[ADObjectSchema.OrganizationalUnitRoot];
			return this.GetExchangeConfigurationUnitByName(adobjectId.Name);
		}

		// Token: 0x060029F0 RID: 10736 RVA: 0x000AFD8C File Offset: 0x000ADF8C
		public ExchangeConfigurationUnit[] FindAllOpenConfigurationUnits(bool excludeFull)
		{
			QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, ExchangeConfigurationUnitSchema.ResellerId, "MSOnline.BPOS_Unmanaged_Hydrated");
			return base.Find<ExchangeConfigurationUnit>(null, QueryScope.SubTree, filter, null, 0, null, false);
		}

		// Token: 0x060029F1 RID: 10737 RVA: 0x000AFDB8 File Offset: 0x000ADFB8
		public ExchangeConfigurationUnit GetExchangeConfigurationUnitByNameOrAcceptedDomain(string organizationName)
		{
			if (Datacenter.IsMultiTenancyEnabled())
			{
				ExchangeConfigurationUnit exchangeConfigurationUnitByName = this.GetExchangeConfigurationUnitByName(organizationName);
				if (exchangeConfigurationUnitByName != null)
				{
					return exchangeConfigurationUnitByName;
				}
			}
			QueryFilter filter = new AndFilter(new QueryFilter[]
			{
				new ComparisonFilter(ComparisonOperator.Equal, AcceptedDomainSchema.DomainName, organizationName),
				new ComparisonFilter(ComparisonOperator.NotEqual, AcceptedDomainSchema.AcceptedDomainType, AcceptedDomainType.ExternalRelay)
			});
			AcceptedDomain[] array = base.Find<AcceptedDomain>(null, QueryScope.SubTree, filter, null, 1);
			if (array == null || array.Length == 0)
			{
				return null;
			}
			ADObjectId entryId = array[0].Id.AncestorDN(3);
			return base.Read<ExchangeConfigurationUnit>(entryId);
		}

		// Token: 0x060029F2 RID: 10738 RVA: 0x000AFE3C File Offset: 0x000AE03C
		public OrganizationId GetOrganizationIdFromOrgNameOrAcceptedDomain(string domainName)
		{
			QueryFilter filter = new AndFilter(new QueryFilter[]
			{
				new ComparisonFilter(ComparisonOperator.Equal, AcceptedDomainSchema.DomainName, domainName),
				new ComparisonFilter(ComparisonOperator.NotEqual, AcceptedDomainSchema.AcceptedDomainType, AcceptedDomainType.ExternalRelay)
			});
			ADRawEntry[] array = base.FindADRawEntryWithDefaultFilters<AcceptedDomain>(null, QueryScope.SubTree, filter, null, 1, new PropertyDefinition[]
			{
				ADObjectSchema.OrganizationId
			});
			if (array == null || array.Count<ADRawEntry>() < 1)
			{
				return null;
			}
			return (OrganizationId)array[0][ADObjectSchema.OrganizationId];
		}

		// Token: 0x060029F3 RID: 10739 RVA: 0x000AFEBC File Offset: 0x000AE0BC
		public OrganizationId GetOrganizationIdFromExternalDirectoryOrgId(Guid externalDirectoryOrgId)
		{
			QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, ExchangeConfigurationUnitSchema.ExternalDirectoryOrganizationId, externalDirectoryOrgId);
			ADRawEntry[] array = base.FindADRawEntryWithDefaultFilters<ExchangeConfigurationUnit>(null, QueryScope.SubTree, filter, null, 1, new PropertyDefinition[]
			{
				ExchangeConfigurationUnitSchema.OrganizationId
			});
			if (array == null || array.Count<ADRawEntry>() < 1)
			{
				return null;
			}
			return (OrganizationId)array[0][ADObjectSchema.OrganizationId];
		}

		// Token: 0x060029F4 RID: 10740 RVA: 0x000AFF1C File Offset: 0x000AE11C
		public MsoTenantCookieContainer GetMsoTenantCookieContainer(Guid contextId)
		{
			QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, MsoTenantCookieContainerSchema.ExternalDirectoryOrganizationId, contextId.ToString());
			MsoTenantCookieContainer[] array = base.Find<MsoTenantCookieContainer>(null, QueryScope.SubTree, filter, null, 1);
			if (array == null || array.Length == 0)
			{
				return null;
			}
			return array[0];
		}

		// Token: 0x060029F5 RID: 10741 RVA: 0x000AFFF4 File Offset: 0x000AE1F4
		public Result<ADRawEntry>[] ReadMultipleOrganizationProperties(ADObjectId[] organizationOUIds, PropertyDefinition[] properties)
		{
			if (organizationOUIds == null)
			{
				throw new ArgumentNullException("organizationOUIds");
			}
			if (organizationOUIds.Length == 0)
			{
				return new Result<ADRawEntry>[0];
			}
			PropertyDefinition[] array;
			if (properties == null)
			{
				array = new ADPropertyDefinition[]
				{
					ADObjectSchema.OrganizationalUnitRoot
				};
			}
			else
			{
				array = new PropertyDefinition[properties.Length + 1];
				properties.CopyTo(array, 0);
				array[array.Length - 1] = ADObjectSchema.OrganizationalUnitRoot;
			}
			return base.ReadMultiple<ADObjectId, ADRawEntry>(organizationOUIds, (ADObjectId ouId) => new AndFilter(new QueryFilter[]
			{
				new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.OrganizationalUnitRoot, ouId),
				new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectClass, ExchangeConfigurationUnit.MostDerivedClass)
			}), delegate(Hashtable hash, ADRawEntry entry)
			{
				ADObjectId adobjectId = (ADObjectId)entry[ADObjectSchema.OrganizationalUnitRoot];
				hash.Add(adobjectId.DistinguishedName, new Result<ADRawEntry>(entry, null));
				hash.Add(adobjectId.ObjectGuid.ToString(), new Result<ADRawEntry>(entry, null));
			}, new ADDataSession.HashLookup<ADObjectId, ADRawEntry>(ADRecipientObjectSession.ADObjectIdHashLookup<ADRawEntry>), array, true);
		}
	}
}
