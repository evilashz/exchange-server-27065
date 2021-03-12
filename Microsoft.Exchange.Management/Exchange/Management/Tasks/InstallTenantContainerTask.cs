using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020002DD RID: 733
	[Cmdlet("install", "TenantContainer")]
	public sealed class InstallTenantContainerTask : InstallContainerTaskBase<Container>
	{
		// Token: 0x17000781 RID: 1921
		// (get) Token: 0x06001988 RID: 6536 RVA: 0x00071C01 File Offset: 0x0006FE01
		// (set) Token: 0x06001989 RID: 6537 RVA: 0x00071C26 File Offset: 0x0006FE26
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

		// Token: 0x0600198A RID: 6538 RVA: 0x00071C40 File Offset: 0x0006FE40
		protected override IConfigDataProvider CreateSession()
		{
			base.CreateSession();
			return DirectorySessionFactory.Default.CreateTenantConfigurationSession(base.DomainController, false, ConsistencyMode.PartiallyConsistent, null, ADSessionSettings.FromAllTenantsPartitionId(new PartitionId(this.AccountPartition)), 41, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\DirectorySetup\\InstallTenantContainerTask.cs");
		}

		// Token: 0x0600198B RID: 6539 RVA: 0x00071C88 File Offset: 0x0006FE88
		protected override ADObjectId GetBaseContainer()
		{
			return ADSession.GetConfigurationUnitsRoot(new PartitionId(this.AccountPartition).ForestFQDN);
		}
	}
}
