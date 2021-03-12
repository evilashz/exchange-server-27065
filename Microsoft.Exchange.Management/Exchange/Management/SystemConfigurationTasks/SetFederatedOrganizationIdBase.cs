using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020009D6 RID: 2518
	public abstract class SetFederatedOrganizationIdBase : SetMultitenancySingletonSystemConfigurationObjectTask<FederatedOrganizationId>
	{
		// Token: 0x17001AF0 RID: 6896
		// (get) Token: 0x06005A18 RID: 23064 RVA: 0x001798FE File Offset: 0x00177AFE
		protected MultiValuedProperty<ADObjectId> FederatedAcceptedDomains
		{
			get
			{
				if (this.federatedAcceptedDomains == null)
				{
					this.federatedAcceptedDomains = this.GetFederatedAcceptedDomains();
				}
				return this.federatedAcceptedDomains;
			}
		}

		// Token: 0x06005A19 RID: 23065 RVA: 0x0017991C File Offset: 0x00177B1C
		private MultiValuedProperty<ADObjectId> GetFederatedAcceptedDomains()
		{
			QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Guid, this.DataObject.Guid);
			ADRawEntry[] array = this.ConfigurationSession.Find(base.CurrentOrgContainerId, QueryScope.SubTree, filter, null, 1, new ADPropertyDefinition[]
			{
				FederatedOrganizationIdSchema.AcceptedDomainsBackLink
			});
			if (array == null || array.Length == 0)
			{
				base.WriteError(new InvalidOperationException(Strings.CannotLocateFedOrgId), ErrorCategory.InvalidOperation, null);
			}
			return array[0][FederatedOrganizationIdSchema.AcceptedDomainsBackLink] as MultiValuedProperty<ADObjectId>;
		}

		// Token: 0x06005A1A RID: 23066 RVA: 0x001799A0 File Offset: 0x00177BA0
		protected AcceptedDomain GetAcceptedDomain(SmtpDomain domain, bool suppressChecks)
		{
			QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, AcceptedDomainSchema.DomainName, domain.Domain);
			AcceptedDomain[] array = base.DataSession.Find<AcceptedDomain>(filter, base.CurrentOrgContainerId, true, null) as AcceptedDomain[];
			if (array == null || array.Length == 0)
			{
				filter = new ComparisonFilter(ComparisonOperator.Equal, AcceptedDomainSchema.DomainName, "*." + domain.Domain);
				array = (base.DataSession.Find<AcceptedDomain>(filter, base.CurrentOrgContainerId, true, null) as AcceptedDomain[]);
				if (array == null || array.Length == 0)
				{
					base.WriteError(new DomainNameNotAcceptedDomainException(domain.Domain), ErrorCategory.InvalidOperation, null);
				}
				else
				{
					base.WriteError(new AcceptedDomainsWithSubdomainsException(domain.Domain), ErrorCategory.InvalidOperation, null);
				}
			}
			AcceptedDomain acceptedDomain = array[0];
			if (suppressChecks)
			{
				return acceptedDomain;
			}
			if (acceptedDomain.DomainName.IncludeSubDomains)
			{
				base.WriteError(new AcceptedDomainsWithSubdomainsException(domain.Domain), ErrorCategory.InvalidOperation, null);
			}
			if (acceptedDomain.DomainType != AcceptedDomainType.Authoritative && AcceptedDomainType.InternalRelay != acceptedDomain.DomainType)
			{
				base.WriteError(new AcceptedDomainsInvalidTypeException(domain.Domain), ErrorCategory.InvalidOperation, null);
			}
			return acceptedDomain;
		}

		// Token: 0x06005A1B RID: 23067 RVA: 0x00179A98 File Offset: 0x00177C98
		protected void ZapDanglingDomainTrusts()
		{
			foreach (ADObjectId identity in this.FederatedAcceptedDomains)
			{
				AcceptedDomain acceptedDomain = (AcceptedDomain)base.DataSession.Read<AcceptedDomain>(identity);
				if (acceptedDomain != null && acceptedDomain.FederatedOrganizationLink != null)
				{
					base.WriteVerbose(Strings.SetFedAcceptedDomainCleanup(acceptedDomain.DomainName.Domain));
					acceptedDomain.FederatedOrganizationLink = null;
					base.DataSession.Save(acceptedDomain);
				}
			}
		}

		// Token: 0x040033AF RID: 13231
		private MultiValuedProperty<ADObjectId> federatedAcceptedDomains;
	}
}
