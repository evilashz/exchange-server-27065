using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000AF1 RID: 2801
	[Cmdlet("Set", "OrganizationFlags", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetOrganizationFlags : SetSystemConfigurationObjectTask<OrganizationIdParameter, ExchangeConfigurationUnit>
	{
		// Token: 0x17001E28 RID: 7720
		// (get) Token: 0x06006375 RID: 25461 RVA: 0x001A0374 File Offset: 0x0019E574
		// (set) Token: 0x06006376 RID: 25462 RVA: 0x001A039A File Offset: 0x0019E59A
		[Parameter(Mandatory = false)]
		public SwitchParameter IsFederated
		{
			get
			{
				return (SwitchParameter)(base.Fields[OrganizationSchema.IsFederated] ?? false);
			}
			set
			{
				base.Fields[OrganizationSchema.IsFederated] = value;
			}
		}

		// Token: 0x17001E29 RID: 7721
		// (get) Token: 0x06006377 RID: 25463 RVA: 0x001A03B2 File Offset: 0x0019E5B2
		// (set) Token: 0x06006378 RID: 25464 RVA: 0x001A03D8 File Offset: 0x0019E5D8
		[Parameter(Mandatory = false)]
		public SwitchParameter HideAdminAccessWarning
		{
			get
			{
				return (SwitchParameter)(base.Fields[OrganizationSchema.HideAdminAccessWarning] ?? false);
			}
			set
			{
				base.Fields[OrganizationSchema.HideAdminAccessWarning] = value;
			}
		}

		// Token: 0x17001E2A RID: 7722
		// (get) Token: 0x06006379 RID: 25465 RVA: 0x001A03F0 File Offset: 0x0019E5F0
		// (set) Token: 0x0600637A RID: 25466 RVA: 0x001A0416 File Offset: 0x0019E616
		[Parameter(Mandatory = false)]
		public SwitchParameter SkipToUAndParentalControlCheck
		{
			get
			{
				return (SwitchParameter)(base.Fields[OrganizationSchema.SkipToUAndParentalControlCheck] ?? false);
			}
			set
			{
				base.Fields[OrganizationSchema.SkipToUAndParentalControlCheck] = value;
			}
		}

		// Token: 0x17001E2B RID: 7723
		// (get) Token: 0x0600637B RID: 25467 RVA: 0x001A042E File Offset: 0x0019E62E
		// (set) Token: 0x0600637C RID: 25468 RVA: 0x001A0454 File Offset: 0x0019E654
		[Parameter(Mandatory = false)]
		public SwitchParameter IsUpgradingOrganization
		{
			get
			{
				return (SwitchParameter)(base.Fields[OrganizationSchema.IsUpgradingOrganization] ?? false);
			}
			set
			{
				base.Fields[OrganizationSchema.IsUpgradingOrganization] = value;
			}
		}

		// Token: 0x17001E2C RID: 7724
		// (get) Token: 0x0600637D RID: 25469 RVA: 0x001A046C File Offset: 0x0019E66C
		// (set) Token: 0x0600637E RID: 25470 RVA: 0x001A0492 File Offset: 0x0019E692
		[Parameter(Mandatory = false)]
		public SwitchParameter IsPilotingOrganization
		{
			get
			{
				return (SwitchParameter)(base.Fields[OrganizationSchema.IsPilotingOrganization] ?? false);
			}
			set
			{
				base.Fields[OrganizationSchema.IsPilotingOrganization] = value;
			}
		}

		// Token: 0x17001E2D RID: 7725
		// (get) Token: 0x0600637F RID: 25471 RVA: 0x001A04AA File Offset: 0x0019E6AA
		// (set) Token: 0x06006380 RID: 25472 RVA: 0x001A04D0 File Offset: 0x0019E6D0
		[Parameter(Mandatory = false)]
		public SwitchParameter IsTemplateTenant
		{
			get
			{
				return (SwitchParameter)(base.Fields[OrganizationSchema.IsTemplateTenant] ?? false);
			}
			set
			{
				base.Fields[OrganizationSchema.IsTemplateTenant] = value;
			}
		}

		// Token: 0x17001E2E RID: 7726
		// (get) Token: 0x06006381 RID: 25473 RVA: 0x001A04E8 File Offset: 0x0019E6E8
		// (set) Token: 0x06006382 RID: 25474 RVA: 0x001A050E File Offset: 0x0019E70E
		[Parameter(Mandatory = false)]
		public SwitchParameter IsUpgradeOperationInProgress
		{
			get
			{
				return (SwitchParameter)(base.Fields[OrganizationSchema.IsUpgradeOperationInProgress] ?? false);
			}
			set
			{
				base.Fields[OrganizationSchema.IsUpgradeOperationInProgress] = value;
			}
		}

		// Token: 0x17001E2F RID: 7727
		// (get) Token: 0x06006383 RID: 25475 RVA: 0x001A0526 File Offset: 0x0019E726
		// (set) Token: 0x06006384 RID: 25476 RVA: 0x001A054C File Offset: 0x0019E74C
		[Parameter(Mandatory = false)]
		public SwitchParameter SMTPAddressCheckWithAcceptedDomain
		{
			get
			{
				return (SwitchParameter)(base.Fields[OrganizationSchema.SMTPAddressCheckWithAcceptedDomain] ?? true);
			}
			set
			{
				base.Fields[OrganizationSchema.SMTPAddressCheckWithAcceptedDomain] = value;
			}
		}

		// Token: 0x17001E30 RID: 7728
		// (get) Token: 0x06006385 RID: 25477 RVA: 0x001A0564 File Offset: 0x0019E764
		// (set) Token: 0x06006386 RID: 25478 RVA: 0x001A058A File Offset: 0x0019E78A
		[Parameter(Mandatory = false)]
		public SwitchParameter IsLicensingEnforced
		{
			get
			{
				return (SwitchParameter)(base.Fields[OrganizationSchema.IsLicensingEnforced] ?? false);
			}
			set
			{
				base.Fields[OrganizationSchema.IsLicensingEnforced] = value;
			}
		}

		// Token: 0x17001E31 RID: 7729
		// (get) Token: 0x06006387 RID: 25479 RVA: 0x001A05A2 File Offset: 0x0019E7A2
		// (set) Token: 0x06006388 RID: 25480 RVA: 0x001A05C8 File Offset: 0x0019E7C8
		[Parameter(Mandatory = false)]
		public SwitchParameter IsTenantAccessBlocked
		{
			get
			{
				return (SwitchParameter)(base.Fields[OrganizationSchema.IsTenantAccessBlocked] ?? false);
			}
			set
			{
				base.Fields[OrganizationSchema.IsTenantAccessBlocked] = value;
			}
		}

		// Token: 0x17001E32 RID: 7730
		// (get) Token: 0x06006389 RID: 25481 RVA: 0x001A05E0 File Offset: 0x0019E7E0
		// (set) Token: 0x0600638A RID: 25482 RVA: 0x001A0606 File Offset: 0x0019E806
		[Parameter(Mandatory = false)]
		public SwitchParameter AllowDeleteOfExternalIdentityUponRemove
		{
			get
			{
				return (SwitchParameter)(base.Fields[OrganizationSchema.AllowDeleteOfExternalIdentityUponRemove] ?? false);
			}
			set
			{
				base.Fields[OrganizationSchema.AllowDeleteOfExternalIdentityUponRemove] = value;
			}
		}

		// Token: 0x17001E33 RID: 7731
		// (get) Token: 0x0600638B RID: 25483 RVA: 0x001A061E File Offset: 0x0019E81E
		// (set) Token: 0x0600638C RID: 25484 RVA: 0x001A0644 File Offset: 0x0019E844
		[Parameter(Mandatory = false)]
		public SwitchParameter UseServicePlanAsCounterInstanceName
		{
			get
			{
				return (SwitchParameter)(base.Fields[OrganizationSchema.UseServicePlanAsCounterInstanceName] ?? false);
			}
			set
			{
				base.Fields[OrganizationSchema.UseServicePlanAsCounterInstanceName] = value;
			}
		}

		// Token: 0x17001E34 RID: 7732
		// (get) Token: 0x0600638D RID: 25485 RVA: 0x001A065C File Offset: 0x0019E85C
		// (set) Token: 0x0600638E RID: 25486 RVA: 0x001A0673 File Offset: 0x0019E873
		[Parameter(Mandatory = false)]
		public SoftDeletedFeatureStatusFlags SoftDeletedFeatureStatus
		{
			get
			{
				return (SoftDeletedFeatureStatusFlags)base.Fields[OrganizationSchema.SoftDeletedFeatureStatus];
			}
			set
			{
				base.Fields[OrganizationSchema.SoftDeletedFeatureStatus] = value;
			}
		}

		// Token: 0x0600638F RID: 25487 RVA: 0x001A068C File Offset: 0x0019E88C
		protected override IConfigurable PrepareDataObject()
		{
			this.tenantCU = (ExchangeConfigurationUnit)base.PrepareDataObject();
			if (base.Fields.IsModified(OrganizationSchema.IsFederated) && !this.IsFederated && this.tenantCU.IsFederated)
			{
				base.WriteError(new CannotDisableFederationException(), ErrorCategory.InvalidOperation, this.DataObject);
			}
			if (base.Fields.IsModified(OrganizationSchema.IsFederated))
			{
				this.tenantCU.IsFederated = this.IsFederated;
			}
			if (base.Fields.IsModified(OrganizationSchema.SkipToUAndParentalControlCheck))
			{
				this.tenantCU.SkipToUAndParentalControlCheck = this.SkipToUAndParentalControlCheck;
			}
			if (base.Fields.IsModified(OrganizationSchema.HideAdminAccessWarning))
			{
				this.tenantCU.HideAdminAccessWarning = this.HideAdminAccessWarning;
			}
			if (base.Fields.IsModified(OrganizationSchema.IsUpgradingOrganization))
			{
				this.tenantCU.IsUpgradingOrganization = this.IsUpgradingOrganization;
			}
			if (base.Fields.IsModified(OrganizationSchema.IsPilotingOrganization))
			{
				this.tenantCU.IsPilotingOrganization = this.IsPilotingOrganization;
			}
			if (base.Fields.IsModified(OrganizationSchema.IsUpgradeOperationInProgress))
			{
				this.tenantCU.IsUpgradeOperationInProgress = this.IsUpgradeOperationInProgress;
			}
			if (base.Fields.IsModified(OrganizationSchema.SMTPAddressCheckWithAcceptedDomain))
			{
				this.tenantCU.SMTPAddressCheckWithAcceptedDomain = this.SMTPAddressCheckWithAcceptedDomain;
			}
			if (base.Fields.IsModified(OrganizationSchema.IsLicensingEnforced))
			{
				this.tenantCU.IsLicensingEnforced = this.IsLicensingEnforced;
			}
			if (base.Fields.IsModified(OrganizationSchema.AllowDeleteOfExternalIdentityUponRemove))
			{
				this.tenantCU.AllowDeleteOfExternalIdentityUponRemove = this.AllowDeleteOfExternalIdentityUponRemove;
			}
			if (base.Fields.IsModified(OrganizationSchema.UseServicePlanAsCounterInstanceName))
			{
				this.tenantCU.UseServicePlanAsCounterInstanceName = this.UseServicePlanAsCounterInstanceName;
			}
			if (base.Fields.IsModified(OrganizationSchema.SoftDeletedFeatureStatus))
			{
				this.tenantCU.SoftDeletedFeatureStatus = this.SoftDeletedFeatureStatus;
			}
			if (base.Fields.IsModified(OrganizationSchema.IsTenantAccessBlocked))
			{
				this.tenantCU.IsTenantAccessBlocked = this.IsTenantAccessBlocked;
			}
			if (base.Fields.IsModified(OrganizationSchema.IsTemplateTenant))
			{
				this.tenantCU.IsTemplateTenant = this.IsTemplateTenant;
			}
			return this.tenantCU;
		}

		// Token: 0x17001E35 RID: 7733
		// (get) Token: 0x06006390 RID: 25488 RVA: 0x001A08F1 File Offset: 0x0019EAF1
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetOrganizationFlags(this.tenantCU.Name);
			}
		}

		// Token: 0x040035EF RID: 13807
		private ExchangeConfigurationUnit tenantCU;
	}
}
