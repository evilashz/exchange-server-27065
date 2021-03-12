using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020009F0 RID: 2544
	[Cmdlet("New", "SharingPolicy", SupportsShouldProcess = true)]
	public sealed class NewSharingPolicy : NewMultitenancySystemConfigurationObjectTask<SharingPolicy>
	{
		// Token: 0x17001B35 RID: 6965
		// (get) Token: 0x06005AF9 RID: 23289 RVA: 0x0017CBF3 File Offset: 0x0017ADF3
		protected override SharedTenantConfigurationMode SharedTenantConfigurationMode
		{
			get
			{
				return SharedTenantConfigurationMode.Dehydrateable;
			}
		}

		// Token: 0x17001B36 RID: 6966
		// (get) Token: 0x06005AFA RID: 23290 RVA: 0x0017CBF6 File Offset: 0x0017ADF6
		// (set) Token: 0x06005AFB RID: 23291 RVA: 0x0017CBFE File Offset: 0x0017ADFE
		public override SwitchParameter IgnoreDehydratedFlag { get; set; }

		// Token: 0x17001B37 RID: 6967
		// (get) Token: 0x06005AFC RID: 23292 RVA: 0x0017CC07 File Offset: 0x0017AE07
		// (set) Token: 0x06005AFD RID: 23293 RVA: 0x0017CC14 File Offset: 0x0017AE14
		[Parameter(Mandatory = true)]
		public MultiValuedProperty<SharingPolicyDomain> Domains
		{
			get
			{
				return this.DataObject.Domains;
			}
			set
			{
				this.DataObject.Domains = value;
			}
		}

		// Token: 0x17001B38 RID: 6968
		// (get) Token: 0x06005AFE RID: 23294 RVA: 0x0017CC22 File Offset: 0x0017AE22
		// (set) Token: 0x06005AFF RID: 23295 RVA: 0x0017CC2F File Offset: 0x0017AE2F
		[Parameter(Mandatory = false)]
		public bool Enabled
		{
			get
			{
				return this.DataObject.Enabled;
			}
			set
			{
				this.DataObject.Enabled = value;
			}
		}

		// Token: 0x17001B39 RID: 6969
		// (get) Token: 0x06005B00 RID: 23296 RVA: 0x0017CC3D File Offset: 0x0017AE3D
		// (set) Token: 0x06005B01 RID: 23297 RVA: 0x0017CC45 File Offset: 0x0017AE45
		[Parameter(Mandatory = false)]
		public SwitchParameter Default { get; set; }

		// Token: 0x17001B3A RID: 6970
		// (get) Token: 0x06005B02 RID: 23298 RVA: 0x0017CC4E File Offset: 0x0017AE4E
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageNewSharingPolicy(base.Name, base.FormatMultiValuedProperty(this.Domains));
			}
		}

		// Token: 0x06005B03 RID: 23299 RVA: 0x0017CC68 File Offset: 0x0017AE68
		protected override IConfigurable PrepareDataObject()
		{
			SharingPolicy sharingPolicy = (SharingPolicy)base.PrepareDataObject();
			sharingPolicy.SetId((IConfigurationSession)base.DataSession, base.Name);
			return sharingPolicy;
		}

		// Token: 0x06005B04 RID: 23300 RVA: 0x0017CC9C File Offset: 0x0017AE9C
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			if (SharedConfiguration.IsSharedConfiguration(this.DataObject.OrganizationId) && !base.ShouldContinue(Strings.ConfirmSharedConfiguration(this.DataObject.OrganizationId.OrganizationalUnit.Name)))
			{
				TaskLogger.LogExit();
				return;
			}
			base.InternalProcessRecord();
			if (this.Default)
			{
				SetSharingPolicy.SetDefaultSharingPolicy(this.ConfigurationSession, this.DataObject.OrganizationId, this.DataObject.Id);
			}
			if (this.DataObject.IsAllowedForAnyAnonymousFeature() && !SetSharingPolicy.IsDatacenter)
			{
				this.WriteWarning(Strings.AnonymousSharingEnabledWarning);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06005B05 RID: 23301 RVA: 0x0017CD40 File Offset: 0x0017AF40
		protected override void WriteResult(IConfigurable result)
		{
			SharingPolicy sharingPolicy = (SharingPolicy)result;
			sharingPolicy.Default = this.Default;
			base.WriteObject(result);
		}
	}
}
