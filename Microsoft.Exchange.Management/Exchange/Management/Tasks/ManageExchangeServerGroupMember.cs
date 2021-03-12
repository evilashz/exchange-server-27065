using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020002B8 RID: 696
	public class ManageExchangeServerGroupMember : SetupTaskBase
	{
		// Token: 0x1700074A RID: 1866
		// (get) Token: 0x0600186C RID: 6252 RVA: 0x00067770 File Offset: 0x00065970
		// (set) Token: 0x0600186D RID: 6253 RVA: 0x00067787 File Offset: 0x00065987
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

		// Token: 0x0600186E RID: 6254 RVA: 0x0006779C File Offset: 0x0006599C
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			base.InternalBeginProcessing();
			if (string.IsNullOrEmpty(this.ServerName))
			{
				this.server = ((ITopologyConfigurationSession)this.domainConfigurationSession).FindLocalComputer();
			}
			else
			{
				ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 99, "InternalBeginProcessing", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\DirectorySetup\\ManageExchangeServerGroupMember.cs");
				topologyConfigurationSession.UseConfigNC = false;
				topologyConfigurationSession.UseGlobalCatalog = true;
				this.server = topologyConfigurationSession.FindComputerByHostName(this.ServerName);
			}
			if (this.server != null)
			{
				base.LogReadObject(this.server);
				this.serverDomain = this.domainConfigurationSession.Read<ADDomain>(this.server.Id.DomainId);
				if (this.serverDomain != null)
				{
					base.LogReadObject(this.serverDomain);
					this.meso = ((ITopologyConfigurationSession)this.domainConfigurationSession).FindMesoContainer(this.serverDomain);
					if (this.meso != null)
					{
						base.LogReadObject(this.meso);
						this.recipientSession.DomainController = this.meso.OriginatingServer;
						this.e12ds = DirectoryCommon.FindE12DomainServersGroup(this.recipientSession, this.meso);
						if (this.e12ds != null)
						{
							base.LogReadObject(this.e12ds);
						}
					}
				}
			}
			this.exs = base.ResolveExchangeGroupGuid<ADGroup>(WellKnownGuid.ExSWkGuid);
			this.ets = base.ResolveExchangeGroupGuid<ADGroup>(WellKnownGuid.EtsWkGuid);
			TaskLogger.LogExit();
		}

		// Token: 0x04000AA5 RID: 2725
		protected ADComputer server;

		// Token: 0x04000AA6 RID: 2726
		protected ADGroup exs;

		// Token: 0x04000AA7 RID: 2727
		protected ADGroup ets;

		// Token: 0x04000AA8 RID: 2728
		protected ADGroup e12ds;

		// Token: 0x04000AA9 RID: 2729
		protected ADDomain serverDomain;

		// Token: 0x04000AAA RID: 2730
		protected MesoContainer meso;
	}
}
