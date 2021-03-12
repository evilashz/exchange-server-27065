using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020002BA RID: 698
	public class ManageManagedAvailabilityServerGroupMember : SetupTaskBase
	{
		// Token: 0x1700074C RID: 1868
		// (get) Token: 0x06001875 RID: 6261 RVA: 0x00067B9C File Offset: 0x00065D9C
		// (set) Token: 0x06001876 RID: 6262 RVA: 0x00067BB3 File Offset: 0x00065DB3
		[Parameter(Mandatory = false)]
		public string ServerName
		{
			get
			{
				return (string)base.Fields["ServerName"];
			}
			set
			{
				base.Fields["ServerName"] = value;
			}
		}

		// Token: 0x06001877 RID: 6263 RVA: 0x00067BC8 File Offset: 0x00065DC8
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			base.InternalBeginProcessing();
			this.gcSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(false, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 69, "InternalBeginProcessing", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\DirectorySetup\\ManageManagedAvailabilityServerGroupMember.cs");
			this.gcSession.UseConfigNC = false;
			this.gcSession.UseGlobalCatalog = true;
			if (string.IsNullOrEmpty(this.ServerName))
			{
				this.ServerName = Environment.MachineName;
			}
			this.server = this.gcSession.FindComputerByHostName(this.ServerName);
			if (this.server != null)
			{
				base.LogReadObject(this.server);
			}
			this.mas = base.ResolveExchangeGroupGuid<ADGroup>(WellKnownGuid.MaSWkGuid);
			TaskLogger.LogExit();
		}

		// Token: 0x04000AAB RID: 2731
		protected ADComputer server;

		// Token: 0x04000AAC RID: 2732
		protected ADGroup mas;

		// Token: 0x04000AAD RID: 2733
		internal ITopologyConfigurationSession gcSession;
	}
}
