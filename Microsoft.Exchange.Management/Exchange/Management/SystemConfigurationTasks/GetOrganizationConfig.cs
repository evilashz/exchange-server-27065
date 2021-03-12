using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000AE7 RID: 2791
	[Cmdlet("Get", "OrganizationConfig", DefaultParameterSetName = "Identity")]
	public sealed class GetOrganizationConfig : GetMultitenancySingletonSystemConfigurationObjectTask<ADOrganizationConfig>
	{
		// Token: 0x17001E0E RID: 7694
		// (get) Token: 0x06006318 RID: 25368 RVA: 0x0019E195 File Offset: 0x0019C395
		// (set) Token: 0x06006319 RID: 25369 RVA: 0x0019E19D File Offset: 0x0019C39D
		[Parameter(Mandatory = false, ParameterSetName = "Identity", Position = 0)]
		public new OrganizationIdParameter Identity
		{
			get
			{
				return base.Identity;
			}
			set
			{
				base.Identity = value;
			}
		}

		// Token: 0x17001E0F RID: 7695
		// (get) Token: 0x0600631A RID: 25370 RVA: 0x0019E1A6 File Offset: 0x0019C3A6
		// (set) Token: 0x0600631B RID: 25371 RVA: 0x0019E1AE File Offset: 0x0019C3AE
		[Parameter(Mandatory = false, ParameterSetName = "PartitionWide")]
		public new AccountPartitionIdParameter AccountPartition
		{
			get
			{
				return base.AccountPartition;
			}
			set
			{
				base.AccountPartition = value;
			}
		}

		// Token: 0x17001E10 RID: 7696
		// (get) Token: 0x0600631C RID: 25372 RVA: 0x0019E1B7 File Offset: 0x0019C3B7
		protected override bool DeepSearch
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600631D RID: 25373 RVA: 0x0019E1BC File Offset: 0x0019C3BC
		protected override void WriteResult(IConfigurable dataObject)
		{
			TaskLogger.LogEnter(new object[]
			{
				dataObject.Identity,
				dataObject
			});
			ADOrganizationConfig adorganizationConfig = dataObject as ADOrganizationConfig;
			if (adorganizationConfig != null)
			{
				int? num = this.RetrieveSCLJunkThreshold(adorganizationConfig.Id);
				if (num != null)
				{
					adorganizationConfig.SCLJunkThreshold = num.Value;
				}
				this.FillTaskPopulatedFields(adorganizationConfig);
				MultiValuedProperty<OrganizationSummaryEntry> multiValuedProperty = new MultiValuedProperty<OrganizationSummaryEntry>();
				foreach (OrganizationSummaryEntry organizationSummaryEntry in adorganizationConfig.OrganizationSummary)
				{
					if (OrganizationSummaryEntry.IsValidKeyForCurrentRelease(organizationSummaryEntry.Key))
					{
						multiValuedProperty.Add(organizationSummaryEntry.Clone());
					}
				}
				multiValuedProperty.ResetChangeTracking();
				adorganizationConfig.OrganizationSummary = multiValuedProperty;
				adorganizationConfig.ResetChangeTracking();
			}
			ADSessionSettings sessionSettings;
			if (this.AccountPartition == null)
			{
				sessionSettings = ADSessionSettings.RescopeToSubtree(base.OrgWideSessionSettings);
			}
			else
			{
				PartitionId partitionId = RecipientTaskHelper.ResolvePartitionId(this.AccountPartition, new Task.TaskErrorLoggingDelegate(base.WriteError));
				sessionSettings = ADSessionSettings.FromAccountPartitionRootOrgScopeSet(partitionId);
			}
			IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(base.DomainController, false, ConsistencyMode.PartiallyConsistent, sessionSettings, 109, "WriteResult", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\SystemConfigurationTasks\\organization\\GetOrganization.cs");
			tenantOrRootOrgRecipientSession.UseConfigNC = true;
			ADMicrosoftExchangeRecipient admicrosoftExchangeRecipient = MailboxTaskHelper.FindMicrosoftExchangeRecipient(tenantOrRootOrgRecipientSession, (IConfigurationSession)base.DataSession);
			if (admicrosoftExchangeRecipient == null)
			{
				if (adorganizationConfig.SharedConfigurationInfo == null)
				{
					if (!this.IsOnlyGatewayServerRoleExist())
					{
						this.WriteError(new InvalidOperationException(Strings.ErrorMicrosoftExchangeRecipientNotFound), ErrorCategory.ReadError, adorganizationConfig.Identity, false);
					}
					else
					{
						base.WriteVerbose(Strings.MicrosoftExchangeRecipientNotFoundOnGatewayServerRole);
					}
				}
			}
			else
			{
				ValidationError[] array = admicrosoftExchangeRecipient.Validate();
				for (int i = 0; i < array.Length; i++)
				{
					this.WriteWarning(array[i].Description);
				}
				adorganizationConfig.MicrosoftExchangeRecipientEmailAddresses = admicrosoftExchangeRecipient.EmailAddresses;
				adorganizationConfig.MicrosoftExchangeRecipientReplyRecipient = admicrosoftExchangeRecipient.ForwardingAddress;
				adorganizationConfig.MicrosoftExchangeRecipientEmailAddressPolicyEnabled = admicrosoftExchangeRecipient.EmailAddressPolicyEnabled;
				adorganizationConfig.MicrosoftExchangeRecipientPrimarySmtpAddress = admicrosoftExchangeRecipient.PrimarySmtpAddress;
				adorganizationConfig.ResetChangeTracking();
			}
			bool flag = !OrganizationId.ForestWideOrgId.Equals(adorganizationConfig.OrganizationId);
			if (flag)
			{
				MultiValuedProperty<string> multiValuedProperty2 = this.AcceptedDomainNamesGetter(adorganizationConfig);
				if (multiValuedProperty2 != null)
				{
					adorganizationConfig.AcceptedDomainNames = multiValuedProperty2;
				}
			}
			if (VariantConfiguration.InvariantNoFlightingSnapshot.Global.MultiTenancy.Enabled)
			{
				adorganizationConfig.HierarchicalAddressBookRoot = this.GetHierarchicalAddressBookRootFromOU(adorganizationConfig.OrganizationId.OrganizationalUnit);
			}
			base.WriteResult(new OrganizationConfig(adorganizationConfig, flag));
			TaskLogger.LogExit();
		}

		// Token: 0x0600631E RID: 25374 RVA: 0x0019E430 File Offset: 0x0019C630
		private int? RetrieveSCLJunkThreshold(ObjectId rootId)
		{
			int? result = null;
			IConfigurable[] array = base.DataSession.Find<UceContentFilter>(null, rootId, true, null);
			if (array != null && array.Length > 0)
			{
				UceContentFilter uceContentFilter = array[0] as UceContentFilter;
				if (uceContentFilter != null)
				{
					result = new int?(uceContentFilter.SCLJunkThreshold);
				}
			}
			return result;
		}

		// Token: 0x0600631F RID: 25375 RVA: 0x0019E478 File Offset: 0x0019C678
		private MultiValuedProperty<string> AcceptedDomainNamesGetter(ADOrganizationConfig org)
		{
			MultiValuedProperty<string> multiValuedProperty = new MultiValuedProperty<string>();
			if (!string.IsNullOrEmpty(org.MicrosoftExchangeRecipientPrimarySmtpAddress.Domain))
			{
				IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.PartiallyConsistent, ADSessionSettings.RootOrgOrSingleTenantFromAcceptedDomainAutoDetect(org.MicrosoftExchangeRecipientPrimarySmtpAddress.Domain), 215, "AcceptedDomainNamesGetter", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\SystemConfigurationTasks\\organization\\GetOrganization.cs");
				ADPagedReader<AcceptedDomain> adpagedReader = tenantOrTopologyConfigurationSession.FindPaged<AcceptedDomain>(null, QueryScope.SubTree, null, null, 0);
				using (IEnumerator<AcceptedDomain> enumerator = adpagedReader.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						AcceptedDomain acceptedDomain = enumerator.Current;
						multiValuedProperty.Add(acceptedDomain.DomainName.Address);
					}
					return multiValuedProperty;
				}
			}
			if (org.SharedConfigurationInfo == null)
			{
				this.WriteWarning(Strings.WarningPrimaryExchangeRecipientNotSet(this.Identity.ToString()));
			}
			return multiValuedProperty;
		}

		// Token: 0x06006320 RID: 25376 RVA: 0x0019E54C File Offset: 0x0019C74C
		private void FillTaskPopulatedFields(ADOrganizationConfig organization)
		{
			RbacContainer rbacContainer = null;
			if (OrganizationId.ForestWideOrgId.Equals(organization.OrganizationId))
			{
				rbacContainer = this.ConfigurationSession.GetRbacContainer();
			}
			else
			{
				ExchangeConfigurationUnit exchangeConfigurationUnit = this.ConfigurationSession.GetOrgContainer() as ExchangeConfigurationUnit;
				if (exchangeConfigurationUnit != null)
				{
					organization.ServicePlan = exchangeConfigurationUnit.ServicePlan;
					organization.TargetServicePlan = exchangeConfigurationUnit.TargetServicePlan;
					if (exchangeConfigurationUnit.IsStaticConfigurationShared)
					{
						SharedConfiguration sharedConfiguration = SharedConfiguration.GetSharedConfiguration(organization.OrganizationId);
						rbacContainer = sharedConfiguration.GetRbacContainer();
					}
					else
					{
						rbacContainer = this.ConfigurationSession.GetRbacContainer();
					}
				}
			}
			if (rbacContainer != null)
			{
				organization.RBACConfigurationVersion = rbacContainer.ExchangeVersion;
			}
		}

		// Token: 0x06006321 RID: 25377 RVA: 0x0019E5E0 File Offset: 0x0019C7E0
		private bool IsOnlyGatewayServerRoleExist()
		{
			IConfigurationSession configurationSession = (IConfigurationSession)base.DataSession;
			QueryFilter filter = new BitMaskOrFilter(ServerSchema.CurrentServerRole, 16439UL);
			Server[] array = configurationSession.Find<Server>(null, QueryScope.SubTree, filter, null, 1);
			return array == null || array.Length == 0;
		}

		// Token: 0x06006322 RID: 25378 RVA: 0x0019E624 File Offset: 0x0019C824
		private ADObjectId GetHierarchicalAddressBookRootFromOU(ADObjectId ouId)
		{
			if (ouId == null)
			{
				return null;
			}
			ExchangeOrganizationalUnit exchangeOrganizationalUnit = (ExchangeOrganizationalUnit)base.GetDataObject<ExchangeOrganizationalUnit>(new OrganizationalUnitIdParameter(ouId), this.ConfigurationSession, null, new LocalizedString?(Strings.ErrorOrganizationalUnitNotFound(ouId.ToString())), new LocalizedString?(Strings.ErrorOrganizationalUnitNotUnique(ouId.ToString())));
			return exchangeOrganizationalUnit.HierarchicalAddressBookRoot;
		}
	}
}
