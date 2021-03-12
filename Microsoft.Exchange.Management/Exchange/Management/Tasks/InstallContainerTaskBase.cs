using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000076 RID: 118
	public class InstallContainerTaskBase<TDataObject> : NewMultitenancyFixedNameSystemConfigurationObjectTask<TDataObject> where TDataObject : ADConfigurationObject, new()
	{
		// Token: 0x17000172 RID: 370
		// (get) Token: 0x060003A4 RID: 932 RVA: 0x0000F773 File Offset: 0x0000D973
		// (set) Token: 0x060003A5 RID: 933 RVA: 0x0000F78A File Offset: 0x0000D98A
		[Parameter(Mandatory = true, Position = 0)]
		public string[] Name
		{
			get
			{
				return (string[])base.Fields["Name"];
			}
			set
			{
				base.Fields["Name"] = value;
			}
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x0000F79D File Offset: 0x0000D99D
		protected virtual ADObjectId GetBaseContainer()
		{
			return base.CurrentOrgContainerId;
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x0000F7A8 File Offset: 0x0000D9A8
		protected override IConfigDataProvider CreateSession()
		{
			base.CreateSession();
			if (base.Organization != null)
			{
				return DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(base.DomainController, false, ConsistencyMode.PartiallyConsistent, null, base.SessionSettings, 64, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\DirectorySetup\\InstallContainerTaskBase.cs");
			}
			return DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(base.DomainController, false, ConsistencyMode.PartiallyConsistent, null, base.SessionSettings, ConfigScopes.TenantSubTree, 74, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\DirectorySetup\\InstallContainerTaskBase.cs");
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x0000F81C File Offset: 0x0000DA1C
		protected override IConfigurable PrepareDataObject()
		{
			TDataObject tdataObject = (TDataObject)((object)base.PrepareDataObject());
			ADObjectId adobjectId = this.GetBaseContainer();
			foreach (string unescapedCommonName in this.Name)
			{
				adobjectId = adobjectId.GetChildId(unescapedCommonName);
			}
			tdataObject.SetId(adobjectId);
			return tdataObject;
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x0000F878 File Offset: 0x0000DA78
		protected override void InternalProcessRecord()
		{
			IConfigDataProvider dataSession = base.DataSession;
			TDataObject dataObject = this.DataObject;
			if (dataSession.Read<TDataObject>(dataObject.Id) == null)
			{
				base.InternalProcessRecord();
			}
		}
	}
}
