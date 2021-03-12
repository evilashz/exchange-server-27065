using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Authorization;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x02000181 RID: 385
	[ClassAccessLevel(AccessLevel.Consumer)]
	public abstract class ManageOrganizationUpgradeTaskBase : ManageOrganizationTaskBase
	{
		// Token: 0x06000E5D RID: 3677 RVA: 0x00041000 File Offset: 0x0003F200
		protected ManageOrganizationUpgradeTaskBase()
		{
			this.Identity = null;
			base.Fields["InstallationMode"] = InstallationModes.BuildToBuildUpgrade;
		}

		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x06000E5E RID: 3678 RVA: 0x00041025 File Offset: 0x0003F225
		// (set) Token: 0x06000E5F RID: 3679 RVA: 0x0004102D File Offset: 0x0003F22D
		[Parameter(Mandatory = true, ParameterSetName = "Identity", ValueFromPipeline = true, ValueFromPipelineByPropertyName = true, Position = 0)]
		public OrganizationIdParameter Identity { get; set; }

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x06000E60 RID: 3680 RVA: 0x00041036 File Offset: 0x0003F236
		protected override ExchangeRunspaceConfigurationSettings.ExchangeApplication ClientApplication
		{
			get
			{
				return ExchangeRunspaceConfigurationSettings.ExchangeApplication.LowPriorityScripts;
			}
		}

		// Token: 0x06000E61 RID: 3681 RVA: 0x0004103A File Offset: 0x0003F23A
		protected override void InternalStateReset()
		{
			TaskLogger.LogEnter();
			this.tenantCU = null;
			this.tenantFQDN = null;
			base.InternalStateReset();
			TaskLogger.LogExit();
		}

		// Token: 0x06000E62 RID: 3682 RVA: 0x0004105C File Offset: 0x0003F25C
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			Organization orgContainer = base.Session.GetOrgContainer();
			if (OrganizationId.ForestWideOrgId.Equals(orgContainer.OrganizationId) && orgContainer.ObjectVersion < Organization.OrgConfigurationVersion)
			{
				base.WriteError(new InvalidOperationException(Strings.ErrorEnterpriseOrgOutOfDate), (ErrorCategory)1000, null);
			}
			IEnumerable<ExchangeConfigurationUnit> objects = this.Identity.GetObjects<ExchangeConfigurationUnit>(null, base.Session);
			using (IEnumerator<ExchangeConfigurationUnit> enumerator = objects.GetEnumerator())
			{
				if (!enumerator.MoveNext())
				{
					throw new ManagementObjectNotFoundException(Strings.ErrorOrganizationNotFound(this.Identity.ToString()));
				}
				this.tenantCU = enumerator.Current;
				if (enumerator.MoveNext())
				{
					throw new ManagementObjectAmbiguousException(Strings.ErrorManagementObjectAmbiguous(this.Identity.ToString()));
				}
			}
			if (this.tenantCU != null)
			{
				base.CurrentOrganizationId = this.tenantCU.OrganizationId;
			}
			this.tenantFQDN = this.CreateTenantSession(this.tenantCU.OrganizationId, true, ConsistencyMode.FullyConsistent).GetDefaultAcceptedDomain();
			if (this.tenantFQDN == null)
			{
				throw new ManagementObjectNotFoundException(Strings.ErrorNoDefaultAcceptedDomainFound(this.Identity.ToString()));
			}
			if (this.tenantCU.OrganizationStatus != OrganizationStatus.Active && this.tenantCU.OrganizationStatus != OrganizationStatus.Suspended && this.tenantCU.OrganizationStatus != OrganizationStatus.LockedOut)
			{
				base.WriteError(new InvalidOperationException(Strings.ErrorOrganizationNotUpgradable(this.Identity.ToString(), this.tenantCU.OrganizationStatus.ToString())), ErrorCategory.InvalidOperation, null);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06000E63 RID: 3683 RVA: 0x000411F0 File Offset: 0x0003F3F0
		protected override void SetRunspaceVariables()
		{
			base.SetRunspaceVariables();
			this.monadConnection.RunspaceProxy.SetVariable("TargetProgramId", this.tenantCU.ProgramId);
			this.monadConnection.RunspaceProxy.SetVariable("TargetOfferId", this.TargetOfferId);
			this.monadConnection.RunspaceProxy.SetVariable("EnableUpdateThrottling", TenantUpgradeConfigImpl.GetConfig<bool>("EnableUpdateThrottling"));
		}

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x06000E64 RID: 3684 RVA: 0x00041264 File Offset: 0x0003F464
		protected virtual string TargetOfferId
		{
			get
			{
				if (this.upgradeOfferId == null && !ServicePlanConfiguration.GetInstance().TryGetReversePilotOfferId(this.tenantCU.ProgramId, this.tenantCU.OfferId, out this.upgradeOfferId))
				{
					this.upgradeOfferId = this.tenantCU.OfferId;
				}
				return this.upgradeOfferId;
			}
		}

		// Token: 0x06000E65 RID: 3685 RVA: 0x000412B8 File Offset: 0x0003F4B8
		protected override void PopulateContextVariables()
		{
			base.PopulateContextVariables();
			if (this.tenantCU != null)
			{
				string distinguishedName = this.tenantCU.OrganizationId.OrganizationalUnit.DistinguishedName;
				base.Fields["TenantOrganizationDN"] = distinguishedName;
				base.Fields["OrganizationHierarchicalPath"] = OrganizationIdParameter.GetHierarchicalIdentityFromDN(distinguishedName);
				base.Fields["TenantOrganizationObjectVersion"] = this.tenantCU.ObjectVersion;
				base.Fields["TenantDomainName"] = this.tenantFQDN.DomainName.ToString();
				base.Fields["TenantExternalDirectoryOrganizationId"] = this.tenantCU.ExternalDirectoryOrganizationId;
				base.Fields["Partition"] = ADAccountPartitionLocator.GetAccountPartitionGuidByPartitionId(new PartitionId(this.tenantCU.Id));
			}
		}

		// Token: 0x06000E66 RID: 3686 RVA: 0x00041398 File Offset: 0x0003F598
		internal IConfigurationSession CreateTenantSession(OrganizationId orgId, bool isReadonly, ConsistencyMode consistencyMode)
		{
			ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(base.RootOrgContainerId, orgId, base.ExecutingUserOrganizationId, false);
			return DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(base.DomainController, isReadonly, consistencyMode, sessionSettings, 221, "CreateTenantSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\Deployment\\ManageOrganizationUpgradeTaskBase.cs");
		}

		// Token: 0x040006C3 RID: 1731
		protected AcceptedDomain tenantFQDN;

		// Token: 0x040006C4 RID: 1732
		protected ExchangeConfigurationUnit tenantCU;

		// Token: 0x040006C5 RID: 1733
		private string upgradeOfferId;
	}
}
