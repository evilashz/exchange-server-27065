using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020009E2 RID: 2530
	[Cmdlet("Get", "FederatedOrganizationIdentifier", DefaultParameterSetName = "Identity")]
	public sealed class GetFederatedOrganizationIdentifier : GetMultitenancySingletonSystemConfigurationObjectTask<FederatedOrganizationId>
	{
		// Token: 0x17001B08 RID: 6920
		// (get) Token: 0x06005A61 RID: 23137 RVA: 0x0017A436 File Offset: 0x00178636
		// (set) Token: 0x06005A62 RID: 23138 RVA: 0x0017A43E File Offset: 0x0017863E
		[Parameter(Mandatory = false)]
		public SwitchParameter IncludeExtendedDomainInfo { get; set; }

		// Token: 0x17001B09 RID: 6921
		// (get) Token: 0x06005A63 RID: 23139 RVA: 0x0017A447 File Offset: 0x00178647
		protected override bool DeepSearch
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06005A64 RID: 23140 RVA: 0x0017A44C File Offset: 0x0017864C
		protected override void WriteResult(IConfigurable dataObject)
		{
			TaskLogger.LogEnter(new object[]
			{
				dataObject.Identity,
				dataObject
			});
			FederatedOrganizationIdWithDomainStatus dataObject2 = this.CreatePresentationObject((FederatedOrganizationId)dataObject, this.IncludeExtendedDomainInfo);
			base.WriteResult(dataObject2);
			TaskLogger.LogExit();
		}

		// Token: 0x06005A65 RID: 23141 RVA: 0x0017A498 File Offset: 0x00178698
		private List<AcceptedDomain> GetAllFederatedDomains(FederatedOrganizationId fedOrgId)
		{
			QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Guid, fedOrgId.Guid);
			ADRawEntry[] array = this.ConfigurationSession.Find(base.CurrentOrgContainerId, QueryScope.SubTree, filter, null, 1, new ADPropertyDefinition[]
			{
				FederatedOrganizationIdSchema.AcceptedDomainsBackLink
			});
			if (array == null || array.Length == 0)
			{
				base.WriteError(new InvalidOperationException(Strings.CannotLocateFedOrgId), ErrorCategory.InvalidOperation, null);
			}
			MultiValuedProperty<ADObjectId> multiValuedProperty = array[0][FederatedOrganizationIdSchema.AcceptedDomainsBackLink] as MultiValuedProperty<ADObjectId>;
			List<AcceptedDomain> list = new List<AcceptedDomain>(multiValuedProperty.Count);
			foreach (ADObjectId entryId in multiValuedProperty)
			{
				AcceptedDomain acceptedDomain = this.ConfigurationSession.Read<AcceptedDomain>(entryId);
				if (acceptedDomain != null)
				{
					list.Add(acceptedDomain);
				}
			}
			return list;
		}

		// Token: 0x06005A66 RID: 23142 RVA: 0x0017A57C File Offset: 0x0017877C
		private FederatedOrganizationIdWithDomainStatus CreatePresentationObject(FederatedOrganizationId fedOrgId, bool includeExtendedDomainInfo)
		{
			FederatedOrganizationIdWithDomainStatus federatedOrganizationIdWithDomainStatus = new FederatedOrganizationIdWithDomainStatus(fedOrgId);
			if (fedOrgId.DelegationTrustLink == null)
			{
				return federatedOrganizationIdWithDomainStatus;
			}
			FederationTrust federationTrust = this.ConfigurationSession.Read<FederationTrust>(fedOrgId.DelegationTrustLink);
			if (federationTrust == null)
			{
				fedOrgId.DelegationTrustLink = ADObjectIdResolutionHelper.ResolveDN(fedOrgId.DelegationTrustLink);
				ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(ADSystemConfigurationSession.GetRootOrgContainerIdForLocalForest(), OrganizationId.ForestWideOrgId, null, false);
				ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, sessionSettings, 147, "CreatePresentationObject", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\SystemConfigurationTasks\\Federation\\GetFederatedOrganizationIdentifier.cs");
				federationTrust = topologyConfigurationSession.Read<FederationTrust>(fedOrgId.DelegationTrustLink);
				if (federationTrust == null)
				{
					return federatedOrganizationIdWithDomainStatus;
				}
			}
			List<AcceptedDomain> allFederatedDomains = this.GetAllFederatedDomains(fedOrgId);
			if (allFederatedDomains.Count == 0)
			{
				return federatedOrganizationIdWithDomainStatus;
			}
			foreach (AcceptedDomain acceptedDomain in allFederatedDomains)
			{
				if (acceptedDomain.IsDefaultFederatedDomain)
				{
					federatedOrganizationIdWithDomainStatus.DefaultDomain = new SmtpDomain(acceptedDomain.DomainName.Domain);
					break;
				}
			}
			MultiValuedProperty<FederatedDomain> multiValuedProperty = new MultiValuedProperty<FederatedDomain>();
			if (!includeExtendedDomainInfo)
			{
				foreach (AcceptedDomain acceptedDomain2 in allFederatedDomains)
				{
					multiValuedProperty.Add(new FederatedDomain(new SmtpDomain(acceptedDomain2.DomainName.Domain)));
				}
				federatedOrganizationIdWithDomainStatus.Domains = multiValuedProperty;
				return federatedOrganizationIdWithDomainStatus;
			}
			FederationProvision federationProvision = FederationProvision.Create(federationTrust, this);
			base.WriteVerbose(Strings.GetFedDomainStatusInfo(FederatedOrganizationId.AddHybridConfigurationWellKnownSubDomain(fedOrgId.AccountNamespace.Domain)));
			DomainState state = DomainState.Unknown;
			try
			{
				state = federationProvision.GetDomainState(FederatedOrganizationId.AddHybridConfigurationWellKnownSubDomain(fedOrgId.AccountNamespace.Domain));
			}
			catch (LocalizedException ex)
			{
				this.WriteError(new CannotGetDomainStatusFromPartnerSTSException(fedOrgId.AccountNamespace.ToString(), federationTrust.ApplicationIdentifier, ex.Message), ErrorCategory.ResourceUnavailable, null, false);
			}
			multiValuedProperty.Add(new FederatedDomain(fedOrgId.AccountNamespace, state));
			foreach (AcceptedDomain acceptedDomain3 in allFederatedDomains)
			{
				SmtpDomain smtpDomain = new SmtpDomain(acceptedDomain3.DomainName.Domain);
				if (!smtpDomain.Equals(fedOrgId.AccountNamespace))
				{
					multiValuedProperty.Add(new FederatedDomain(smtpDomain));
				}
			}
			federatedOrganizationIdWithDomainStatus.Domains = multiValuedProperty;
			return federatedOrganizationIdWithDomainStatus;
		}
	}
}
