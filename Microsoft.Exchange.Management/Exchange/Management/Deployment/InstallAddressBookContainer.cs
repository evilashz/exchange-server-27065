using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x020001DA RID: 474
	[ClassAccessLevel(AccessLevel.Consumer)]
	public abstract class InstallAddressBookContainer : NewMultitenancyFixedNameSystemConfigurationObjectTask<AddressBookBase>
	{
		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x06001061 RID: 4193 RVA: 0x00048B16 File Offset: 0x00046D16
		protected bool IsContainerExisted
		{
			get
			{
				return this.isContainerExisted;
			}
		}

		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x06001062 RID: 4194
		protected abstract ADObjectId RdnContainerToOrganization { get; }

		// Token: 0x06001063 RID: 4195 RVA: 0x00048B20 File Offset: 0x00046D20
		protected sealed override IConfigurable PrepareDataObject()
		{
			ADObjectId descendantId = base.CurrentOrgContainerId.GetDescendantId(this.RdnContainerToOrganization);
			IConfigurationSession configurationSession = (IConfigurationSession)base.DataSession;
			AddressBookBase addressBookBase = configurationSession.Read<AddressBookBase>(descendantId);
			this.isContainerExisted = (null != addressBookBase);
			if (!this.IsContainerExisted)
			{
				addressBookBase = (AddressBookBase)base.PrepareDataObject();
				addressBookBase.SetId(descendantId);
				addressBookBase.DisplayName = this.RdnContainerToOrganization.Name;
				addressBookBase.OrganizationId = (base.CurrentOrganizationId ?? OrganizationId.ForestWideOrgId);
			}
			return addressBookBase;
		}

		// Token: 0x06001064 RID: 4196 RVA: 0x00048BA4 File Offset: 0x00046DA4
		protected override void InternalProcessRecord()
		{
			if (!this.IsContainerExisted)
			{
				base.InternalProcessRecord();
			}
		}

		// Token: 0x06001065 RID: 4197 RVA: 0x00048BB4 File Offset: 0x00046DB4
		internal IConfigurationSession CreateGlobalWritableConfigSession()
		{
			return DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(this.DataObject.OriginatingServer, false, ConsistencyMode.PartiallyConsistent, null, ADSessionSettings.FromAccountPartitionRootOrgScopeSet(this.DataObject.Id.GetPartitionId()), 90, "CreateGlobalWritableConfigSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\Deployment\\InstallAddressBookContainer.cs");
		}

		// Token: 0x0400077E RID: 1918
		private bool isContainerExisted;
	}
}
