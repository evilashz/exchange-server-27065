using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000AED RID: 2797
	[Cmdlet("Set", "MServSyncConfigFlags", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetMServSyncConfigFlags : SetSystemConfigurationObjectTask<OrganizationIdParameter, ADOrganizationalUnit>
	{
		// Token: 0x17001E1C RID: 7708
		// (get) Token: 0x06006344 RID: 25412 RVA: 0x0019EEB4 File Offset: 0x0019D0B4
		// (set) Token: 0x06006345 RID: 25413 RVA: 0x0019EEDA File Offset: 0x0019D0DA
		[Parameter(Mandatory = false)]
		public SwitchParameter MSOSyncEnabled
		{
			get
			{
				return (SwitchParameter)(base.Fields[ADOrganizationalUnitSchema.MSOSyncEnabled] ?? false);
			}
			set
			{
				base.Fields[ADOrganizationalUnitSchema.MSOSyncEnabled] = value;
			}
		}

		// Token: 0x17001E1D RID: 7709
		// (get) Token: 0x06006346 RID: 25414 RVA: 0x0019EEF2 File Offset: 0x0019D0F2
		// (set) Token: 0x06006347 RID: 25415 RVA: 0x0019EF18 File Offset: 0x0019D118
		[Parameter(Mandatory = false)]
		public SwitchParameter SMTPAddressCheckWithAcceptedDomain
		{
			get
			{
				return (SwitchParameter)(base.Fields[ADOrganizationalUnitSchema.SMTPAddressCheckWithAcceptedDomain] ?? true);
			}
			set
			{
				base.Fields[ADOrganizationalUnitSchema.SMTPAddressCheckWithAcceptedDomain] = value;
			}
		}

		// Token: 0x17001E1E RID: 7710
		// (get) Token: 0x06006348 RID: 25416 RVA: 0x0019EF30 File Offset: 0x0019D130
		// (set) Token: 0x06006349 RID: 25417 RVA: 0x0019EF56 File Offset: 0x0019D156
		[Parameter(Mandatory = false)]
		public SwitchParameter SyncMBXAndDLToMserv
		{
			get
			{
				return (SwitchParameter)(base.Fields[ADOrganizationalUnitSchema.SyncMBXAndDLToMserv] ?? false);
			}
			set
			{
				base.Fields[ADOrganizationalUnitSchema.SyncMBXAndDLToMserv] = value;
			}
		}

		// Token: 0x0600634A RID: 25418 RVA: 0x0019EF70 File Offset: 0x0019D170
		protected override IConfigDataProvider CreateSession()
		{
			IConfigurationSession configurationSession = (IConfigurationSession)base.CreateSession();
			configurationSession.UseConfigNC = false;
			configurationSession.UseGlobalCatalog = true;
			configurationSession.EnforceDefaultScope = false;
			return configurationSession;
		}

		// Token: 0x0600634B RID: 25419 RVA: 0x0019EFA0 File Offset: 0x0019D1A0
		protected override IConfigurable PrepareDataObject()
		{
			ADOrganizationalUnit adorganizationalUnit = (ADOrganizationalUnit)base.PrepareDataObject();
			if (base.Fields.IsModified(ADOrganizationalUnitSchema.SMTPAddressCheckWithAcceptedDomain))
			{
				adorganizationalUnit.SMTPAddressCheckWithAcceptedDomain = this.SMTPAddressCheckWithAcceptedDomain;
			}
			if (base.Fields.IsModified(ADOrganizationalUnitSchema.MSOSyncEnabled))
			{
				adorganizationalUnit.MSOSyncEnabled = this.MSOSyncEnabled;
			}
			if (base.Fields.IsModified(ADOrganizationalUnitSchema.SyncMBXAndDLToMserv))
			{
				adorganizationalUnit.SyncMBXAndDLToMServ = this.SyncMBXAndDLToMserv;
			}
			return adorganizationalUnit;
		}
	}
}
