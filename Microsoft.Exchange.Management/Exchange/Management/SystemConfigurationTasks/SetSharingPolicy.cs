using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020009FA RID: 2554
	[Cmdlet("Set", "SharingPolicy", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetSharingPolicy : SetSystemConfigurationObjectTask<SharingPolicyIdParameter, SharingPolicy>
	{
		// Token: 0x17001B6D RID: 7021
		// (get) Token: 0x06005B86 RID: 23430 RVA: 0x0017ED3C File Offset: 0x0017CF3C
		// (set) Token: 0x06005B87 RID: 23431 RVA: 0x0017ED44 File Offset: 0x0017CF44
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<SharingPolicyDomain> Domains
		{
			get
			{
				return this.domains;
			}
			set
			{
				this.domains = value;
			}
		}

		// Token: 0x17001B6E RID: 7022
		// (get) Token: 0x06005B88 RID: 23432 RVA: 0x0017ED4D File Offset: 0x0017CF4D
		// (set) Token: 0x06005B89 RID: 23433 RVA: 0x0017ED69 File Offset: 0x0017CF69
		[Parameter(Mandatory = false)]
		public bool Enabled
		{
			get
			{
				return this.enabled != null && this.enabled.Value;
			}
			set
			{
				this.enabled = new bool?(value);
			}
		}

		// Token: 0x17001B6F RID: 7023
		// (get) Token: 0x06005B8A RID: 23434 RVA: 0x0017ED77 File Offset: 0x0017CF77
		// (set) Token: 0x06005B8B RID: 23435 RVA: 0x0017ED84 File Offset: 0x0017CF84
		[Parameter(Mandatory = false)]
		public SwitchParameter Default
		{
			get
			{
				return this.isDefault;
			}
			set
			{
				this.isDefault = value;
			}
		}

		// Token: 0x17001B70 RID: 7024
		// (get) Token: 0x06005B8C RID: 23436 RVA: 0x0017ED92 File Offset: 0x0017CF92
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetSharingPolicy(this.Identity.ToString());
			}
		}

		// Token: 0x06005B8D RID: 23437 RVA: 0x0017EDA4 File Offset: 0x0017CFA4
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			SharedConfigurationTaskHelper.VerifyIsNotTinyTenant(base.CurrentOrgState, new Task.ErrorLoggerDelegate(base.WriteError));
			base.InternalValidate();
			if (this.isDefault)
			{
				this.ConfigurationSession.SessionSettings.IsSharedConfigChecked = true;
				FederatedOrganizationId federatedOrganizationId = this.ConfigurationSession.GetFederatedOrganizationId(this.DataObject.OrganizationId);
				if (federatedOrganizationId != null)
				{
					this.defaultChanged = !this.DataObject.Id.Equals(federatedOrganizationId.DefaultSharingPolicyLink);
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06005B8E RID: 23438 RVA: 0x0017EE2C File Offset: 0x0017D02C
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			if (SharedConfiguration.IsSharedConfiguration(this.DataObject.OrganizationId) && !base.ShouldContinue(Strings.ConfirmSharedConfiguration(this.DataObject.OrganizationId.OrganizationalUnit.Name)))
			{
				TaskLogger.LogExit();
				return;
			}
			base.InternalProcessRecord();
			if (this.isDefault)
			{
				SetSharingPolicy.SetDefaultSharingPolicy(this.ConfigurationSession, this.DataObject.OrganizationId, this.DataObject.Id);
			}
			if (((this.DataObject.Domains != null && this.DataObject.Domains.Count > 0) || this.enabled == true) && this.DataObject.IsAllowedForAnyAnonymousFeature() && !SetSharingPolicy.IsDatacenter)
			{
				this.WriteWarning(Strings.AnonymousSharingEnabledWarning);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06005B8F RID: 23439 RVA: 0x0017EF07 File Offset: 0x0017D107
		protected override bool IsObjectStateChanged()
		{
			return this.defaultChanged || base.IsObjectStateChanged();
		}

		// Token: 0x06005B90 RID: 23440 RVA: 0x0017EF1C File Offset: 0x0017D11C
		protected override IConfigurable PrepareDataObject()
		{
			SharingPolicy sharingPolicy = (SharingPolicy)base.PrepareDataObject();
			if (this.domains != null)
			{
				if (sharingPolicy.Domains != null && this.domains.IsChangesOnlyCopy)
				{
					sharingPolicy.Domains.CopyChangesFrom(this.domains);
				}
				else
				{
					sharingPolicy.Domains = this.domains;
				}
			}
			if (this.enabled != null)
			{
				sharingPolicy.Enabled = this.enabled.Value;
			}
			return sharingPolicy;
		}

		// Token: 0x06005B91 RID: 23441 RVA: 0x0017EF90 File Offset: 0x0017D190
		internal static void SetDefaultSharingPolicy(IConfigurationSession session, OrganizationId organizationId, ADObjectId sharingPolicyId)
		{
			IConfigurationSession tenantOrTopologyConfigurationSession;
			if (session.ConfigScope == ConfigScopes.TenantSubTree)
			{
				tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(session.DomainController, false, ConsistencyMode.FullyConsistent, session.NetworkCredential, session.SessionSettings, session.ConfigScope, 199, "SetDefaultSharingPolicy", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\SystemConfigurationTasks\\Federation\\SetSharingPolicy.cs");
			}
			else
			{
				tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(session.DomainController, false, ConsistencyMode.FullyConsistent, session.NetworkCredential, session.SessionSettings, 210, "SetDefaultSharingPolicy", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\SystemConfigurationTasks\\Federation\\SetSharingPolicy.cs");
			}
			tenantOrTopologyConfigurationSession.SessionSettings.IsSharedConfigChecked = true;
			FederatedOrganizationId federatedOrganizationId = tenantOrTopologyConfigurationSession.GetFederatedOrganizationId(organizationId);
			if (federatedOrganizationId != null)
			{
				federatedOrganizationId.DefaultSharingPolicyLink = sharingPolicyId;
				tenantOrTopologyConfigurationSession.Save(federatedOrganizationId);
			}
		}

		// Token: 0x17001B71 RID: 7025
		// (get) Token: 0x06005B92 RID: 23442 RVA: 0x0017F030 File Offset: 0x0017D230
		internal static bool IsDatacenter
		{
			get
			{
				return VariantConfiguration.InvariantNoFlightingSnapshot.Global.MultiTenancy.Enabled;
			}
		}

		// Token: 0x040033F8 RID: 13304
		private bool defaultChanged;

		// Token: 0x040033F9 RID: 13305
		private bool isDefault;

		// Token: 0x040033FA RID: 13306
		private bool? enabled;

		// Token: 0x040033FB RID: 13307
		private MultiValuedProperty<SharingPolicyDomain> domains;
	}
}
