using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020009E9 RID: 2537
	[Cmdlet("Install", "DefaultSharingPolicy", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public class InstallDefaultSharingPolicy : NewMultitenancySystemConfigurationObjectTask<SharingPolicy>
	{
		// Token: 0x17001B14 RID: 6932
		// (get) Token: 0x06005A8C RID: 23180 RVA: 0x0017B2BF File Offset: 0x001794BF
		// (set) Token: 0x06005A8D RID: 23181 RVA: 0x0017B2C7 File Offset: 0x001794C7
		private new string Name
		{
			get
			{
				return base.Name;
			}
			set
			{
				base.Name = value;
			}
		}

		// Token: 0x06005A8E RID: 23182 RVA: 0x0017B2D0 File Offset: 0x001794D0
		protected override IConfigurable PrepareDataObject()
		{
			TaskLogger.LogEnter();
			this.ConfigurationSession.SessionSettings.IsSharedConfigChecked = true;
			FederatedOrganizationId federatedOrganizationId = this.ConfigurationSession.GetFederatedOrganizationId();
			string organization = (federatedOrganizationId.OrganizationId == null) ? "<No Org>" : federatedOrganizationId.OrganizationId.ToString();
			string cn;
			if (federatedOrganizationId.DefaultSharingPolicyLink != null)
			{
				this.skipProcessRecord = true;
				base.WriteVerbose(Strings.FoundDefaultSharingPolicy(federatedOrganizationId.DefaultSharingPolicyLink.DistinguishedName, organization));
				cn = Guid.NewGuid().ToString();
			}
			else
			{
				cn = this.GetDefaultSharingPolicyName();
				base.WriteVerbose(Strings.InstallDefaultSharingPolicy(organization));
			}
			SharingPolicy sharingPolicy = (SharingPolicy)base.PrepareDataObject();
			sharingPolicy.SetId(this.ConfigurationSession, cn);
			sharingPolicy.Enabled = true;
			sharingPolicy.Domains = new MultiValuedProperty<SharingPolicyDomain>(new SharingPolicyDomain[]
			{
				new SharingPolicyDomain("*", SharingPolicyAction.CalendarSharingFreeBusySimple)
			});
			sharingPolicy.Domains.Add(new SharingPolicyDomain("Anonymous", SharingPolicyAction.CalendarSharingFreeBusyReviewer));
			TaskLogger.LogExit();
			return sharingPolicy;
		}

		// Token: 0x06005A8F RID: 23183 RVA: 0x0017B3D0 File Offset: 0x001795D0
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			if (!this.skipProcessRecord)
			{
				base.InternalProcessRecord();
				SetSharingPolicy.SetDefaultSharingPolicy(this.ConfigurationSession, this.DataObject.OrganizationId, this.DataObject.Id);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06005A90 RID: 23184 RVA: 0x0017B40C File Offset: 0x0017960C
		private string GetDefaultSharingPolicyName()
		{
			string text = Strings.DefaultSharingPolicyName.ToString();
			QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Name, text);
			SharingPolicy[] array = this.ConfigurationSession.Find<SharingPolicy>(null, QueryScope.SubTree, filter, null, 1);
			if (array != null && array.Length > 0)
			{
				return text + "_" + Guid.NewGuid().ToString();
			}
			return text;
		}

		// Token: 0x040033D4 RID: 13268
		private bool skipProcessRecord;
	}
}
