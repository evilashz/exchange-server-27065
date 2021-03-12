using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020002D7 RID: 727
	[Cmdlet("install", "OrganizationalUnit")]
	public sealed class InstallOrganizationalUnitTask : NewSystemConfigurationObjectTask<ADOrganizationalUnit>
	{
		// Token: 0x17000770 RID: 1904
		// (get) Token: 0x06001952 RID: 6482 RVA: 0x000712C0 File Offset: 0x0006F4C0
		// (set) Token: 0x06001953 RID: 6483 RVA: 0x000712E6 File Offset: 0x0006F4E6
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

		// Token: 0x17000771 RID: 1905
		// (get) Token: 0x06001954 RID: 6484 RVA: 0x000712FE File Offset: 0x0006F4FE
		// (set) Token: 0x06001955 RID: 6485 RVA: 0x00071324 File Offset: 0x0006F524
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

		// Token: 0x17000772 RID: 1906
		// (get) Token: 0x06001956 RID: 6486 RVA: 0x0007133C File Offset: 0x0006F53C
		// (set) Token: 0x06001957 RID: 6487 RVA: 0x00071361 File Offset: 0x0006F561
		[Parameter(Mandatory = true)]
		public Guid AccountPartition
		{
			get
			{
				return (Guid)(base.Fields["Partition"] ?? Guid.Empty);
			}
			set
			{
				base.Fields["Partition"] = value;
			}
		}

		// Token: 0x06001958 RID: 6488 RVA: 0x0007137C File Offset: 0x0006F57C
		protected override IConfigDataProvider CreateSession()
		{
			ITenantConfigurationSession tenantConfigurationSession = DirectorySessionFactory.Default.CreateTenantConfigurationSession(base.DomainController, false, ConsistencyMode.PartiallyConsistent, null, ADSessionSettings.FromAllTenantsPartitionId(new PartitionId(this.AccountPartition)), 67, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\DirectorySetup\\InstallOrganizationalUnitTask.cs");
			tenantConfigurationSession.UseConfigNC = false;
			return tenantConfigurationSession;
		}

		// Token: 0x06001959 RID: 6489 RVA: 0x000713C8 File Offset: 0x0006F5C8
		protected override IConfigurable PrepareDataObject()
		{
			ADOrganizationalUnit adorganizationalUnit = (ADOrganizationalUnit)base.PrepareDataObject();
			ADObjectId adobjectId = ((ITenantConfigurationSession)base.DataSession).GetHostedOrganizationsRoot();
			adobjectId = adobjectId.GetChildId("OU", base.Name);
			adorganizationalUnit.SetId(adobjectId);
			adorganizationalUnit.ConfigurationUnit = null;
			adorganizationalUnit.MSOSyncEnabled = this.MSOSyncEnabled;
			adorganizationalUnit.SMTPAddressCheckWithAcceptedDomain = this.SMTPAddressCheckWithAcceptedDomain;
			return adorganizationalUnit;
		}

		// Token: 0x0600195A RID: 6490 RVA: 0x00071438 File Offset: 0x0006F638
		protected override void InternalProcessRecord()
		{
			base.InternalProcessRecord();
			if (!base.HasErrors)
			{
				ADObjectId childId = this.DataObject.Id.GetChildId("OU", "Soft Deleted Objects");
				ADOrganizationalUnit adorganizationalUnit = new ADOrganizationalUnit();
				adorganizationalUnit.SetId(childId);
				base.DataSession.Save(adorganizationalUnit);
			}
		}
	}
}
