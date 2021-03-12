using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000A08 RID: 2568
	[Cmdlet("Get", "GlobalLocatorServiceTenant", DefaultParameterSetName = "ExternalDirectoryOrganizationIdParameterSet")]
	public sealed class GetGlobalLocatorServiceTenant : ManageGlobalLocatorServiceBase
	{
		// Token: 0x17001B8E RID: 7054
		// (get) Token: 0x06005C0F RID: 23567 RVA: 0x00184567 File Offset: 0x00182767
		// (set) Token: 0x06005C10 RID: 23568 RVA: 0x0018457E File Offset: 0x0018277E
		[Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0, ParameterSetName = "DomainNameParameterSet")]
		[ValidateNotNullOrEmpty]
		public SmtpDomain DomainName
		{
			get
			{
				return (SmtpDomain)base.Fields["DomainName"];
			}
			set
			{
				base.Fields["DomainName"] = value;
			}
		}

		// Token: 0x17001B8F RID: 7055
		// (get) Token: 0x06005C11 RID: 23569 RVA: 0x00184591 File Offset: 0x00182791
		// (set) Token: 0x06005C12 RID: 23570 RVA: 0x001845B7 File Offset: 0x001827B7
		[Parameter(Mandatory = false, ParameterSetName = "ExternalDirectoryOrganizationIdParameterSet")]
		[Parameter(Mandatory = false, ParameterSetName = "DomainNameParameterSet")]
		public SwitchParameter ShowDomainNames
		{
			get
			{
				return (SwitchParameter)(base.Fields["ShowDomainNames"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["ShowDomainNames"] = value;
			}
		}

		// Token: 0x17001B90 RID: 7056
		// (get) Token: 0x06005C13 RID: 23571 RVA: 0x001845CF File Offset: 0x001827CF
		// (set) Token: 0x06005C14 RID: 23572 RVA: 0x001845F5 File Offset: 0x001827F5
		[Parameter(Mandatory = false, ParameterSetName = "ExternalDirectoryOrganizationIdParameterSet")]
		[Parameter(Mandatory = false, ParameterSetName = "DomainNameParameterSet")]
		public SwitchParameter UseOfflineGLS
		{
			get
			{
				return (SwitchParameter)(base.Fields["UseOfflineGLS"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["UseOfflineGLS"] = value;
			}
		}

		// Token: 0x06005C15 RID: 23573 RVA: 0x00184610 File Offset: 0x00182810
		protected override void InternalProcessRecord()
		{
			GlsDirectorySession glsDirectorySession = new GlsDirectorySession();
			GlobalLocatorServiceTenant globalLocatorServiceTenant;
			if (base.Fields.IsModified("ExternalDirectoryOrganizationId"))
			{
				Guid guid = (Guid)base.Fields["ExternalDirectoryOrganizationId"];
				if (this.UseOfflineGLS)
				{
					if (!glsDirectorySession.TryGetTenantInfoByOrgGuid(guid, out globalLocatorServiceTenant, GlsCacheServiceMode.CacheOnly))
					{
						base.WriteGlsTenantNotFoundError(guid);
					}
				}
				else if (!glsDirectorySession.TryGetTenantInfoByOrgGuid(guid, out globalLocatorServiceTenant))
				{
					base.WriteGlsTenantNotFoundError(guid);
				}
				if (this.ShowDomainNames)
				{
					globalLocatorServiceTenant.DomainNames = this.GetDomainNames(globalLocatorServiceTenant.ExternalDirectoryOrganizationId, glsDirectorySession, globalLocatorServiceTenant.AccountForest);
				}
			}
			else
			{
				SmtpDomain smtpDomain = (SmtpDomain)base.Fields["DomainName"];
				if (this.UseOfflineGLS)
				{
					if (!glsDirectorySession.TryGetTenantInfoByDomain(smtpDomain.Domain, out globalLocatorServiceTenant, GlsCacheServiceMode.CacheOnly))
					{
						base.WriteGlsTenantNotFoundError(smtpDomain.Domain);
					}
				}
				else if (!glsDirectorySession.TryGetTenantInfoByDomain(smtpDomain.Domain, out globalLocatorServiceTenant))
				{
					base.WriteGlsTenantNotFoundError(smtpDomain.Domain);
				}
				if (this.ShowDomainNames)
				{
					globalLocatorServiceTenant.DomainNames = this.GetDomainNames(globalLocatorServiceTenant.ExternalDirectoryOrganizationId, glsDirectorySession, globalLocatorServiceTenant.AccountForest, smtpDomain.Domain);
				}
			}
			base.WriteObject(globalLocatorServiceTenant);
		}

		// Token: 0x06005C16 RID: 23574 RVA: 0x00184740 File Offset: 0x00182940
		private AcceptedDomain[] GetAcceptedDomainsInOrg(Guid orgGuid, string accountPartitionFqdn)
		{
			AcceptedDomain[] result;
			try
			{
				PartitionId partitionId = new PartitionId(accountPartitionFqdn);
				ADSessionSettings sessionSettings = ADSessionSettings.FromAllTenantsPartitionId(partitionId);
				ITenantConfigurationSession tenantConfigurationSession = DirectorySessionFactory.Default.CreateTenantConfigurationSession(ConsistencyMode.PartiallyConsistent, sessionSettings, 133, "GetAcceptedDomainsInOrg", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\SystemConfigurationTasks\\GLS\\GetGlobalLocatorServiceTenant.cs");
				ExchangeConfigurationUnit[] array = tenantConfigurationSession.Find<ExchangeConfigurationUnit>(null, QueryScope.SubTree, new ComparisonFilter(ComparisonOperator.Equal, ExchangeConfigurationUnitSchema.ExternalDirectoryOrganizationId, orgGuid), null, 0);
				if (array != null && array.Length > 0)
				{
					result = tenantConfigurationSession.FindAllAcceptedDomainsInOrg(array[0].OrganizationId.ConfigurationUnit);
				}
				else
				{
					result = null;
				}
			}
			catch (Exception ex)
			{
				base.WriteVerbose(Strings.WarningCannotGetGlsTenantFromOrgId(orgGuid.ToString(), ex.Message));
				result = null;
			}
			return result;
		}

		// Token: 0x06005C17 RID: 23575 RVA: 0x001847F0 File Offset: 0x001829F0
		private MultiValuedProperty<string> GetDomainNames(Guid orgGuid, GlsDirectorySession glsSession, string accountPartitionFqdn)
		{
			return this.GetDomainNames(orgGuid, glsSession, accountPartitionFqdn, string.Empty);
		}

		// Token: 0x06005C18 RID: 23576 RVA: 0x00184800 File Offset: 0x00182A00
		private MultiValuedProperty<string> GetDomainNames(Guid orgGuid, GlsDirectorySession glsSession, string accountPartitionFqdn, string glsDomainName)
		{
			MultiValuedProperty<string> multiValuedProperty = new MultiValuedProperty<string>();
			AcceptedDomain[] acceptedDomainsInOrg = this.GetAcceptedDomainsInOrg(orgGuid, accountPartitionFqdn);
			bool flag = false;
			GlobalLocatorServiceDomain globalLocatorServiceDomain;
			if (acceptedDomainsInOrg != null)
			{
				foreach (AcceptedDomain acceptedDomain in acceptedDomainsInOrg)
				{
					if (!string.IsNullOrEmpty(glsDomainName) && acceptedDomain.Name.Equals(glsDomainName, StringComparison.OrdinalIgnoreCase))
					{
						multiValuedProperty.Add(string.Format("{0}:{1}", acceptedDomain.Name, "ADAndGLS"));
						flag = true;
					}
					else if (this.UseOfflineGLS)
					{
						if (!glsSession.TryGetTenantDomainFromDomainFqdn(acceptedDomain.Name, out globalLocatorServiceDomain, GlsCacheServiceMode.CacheOnly))
						{
							multiValuedProperty.Add(string.Format("{0}:{1}", acceptedDomain.Name, "ADOnly"));
						}
						else
						{
							multiValuedProperty.Add(string.Format("{0}:{1}", acceptedDomain.Name, "ADAndGLS"));
						}
					}
					else if (!glsSession.TryGetTenantDomainFromDomainFqdn(acceptedDomain.Name, out globalLocatorServiceDomain))
					{
						multiValuedProperty.Add(string.Format("{0}:{1}", acceptedDomain.Name, "ADOnly"));
					}
					else
					{
						multiValuedProperty.Add(string.Format("{0}:{1}", acceptedDomain.Name, "ADAndGLS"));
					}
				}
			}
			if (!flag && !string.IsNullOrEmpty(glsDomainName) && glsSession.TryGetTenantDomainFromDomainFqdn(glsDomainName, out globalLocatorServiceDomain))
			{
				multiValuedProperty.Add(string.Format("{0}:{1}", glsDomainName, "GlsOnly"));
			}
			return multiValuedProperty;
		}
	}
}
