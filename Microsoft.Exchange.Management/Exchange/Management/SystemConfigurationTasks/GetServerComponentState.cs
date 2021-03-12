using System;
using System.Collections;
using System.IO;
using System.Management.Automation;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Monitoring;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x0200093B RID: 2363
	[Cmdlet("Get", "ServerComponentState")]
	public sealed class GetServerComponentState : Task
	{
		// Token: 0x17001908 RID: 6408
		// (get) Token: 0x06005406 RID: 21510 RVA: 0x0015B0C0 File Offset: 0x001592C0
		// (set) Token: 0x06005407 RID: 21511 RVA: 0x0015B0C8 File Offset: 0x001592C8
		[Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
		public ServerIdParameter Identity
		{
			get
			{
				return this.serverId;
			}
			set
			{
				this.serverId = value;
			}
		}

		// Token: 0x17001909 RID: 6409
		// (get) Token: 0x06005408 RID: 21512 RVA: 0x0015B0D1 File Offset: 0x001592D1
		// (set) Token: 0x06005409 RID: 21513 RVA: 0x0015B0E8 File Offset: 0x001592E8
		[Parameter(Mandatory = false)]
		public string Component
		{
			get
			{
				return (string)base.Fields["Component"];
			}
			set
			{
				base.Fields["Component"] = value;
			}
		}

		// Token: 0x1700190A RID: 6410
		// (get) Token: 0x0600540A RID: 21514 RVA: 0x0015B0FB File Offset: 0x001592FB
		// (set) Token: 0x0600540B RID: 21515 RVA: 0x0015B103 File Offset: 0x00159303
		[Parameter(Mandatory = false)]
		public Fqdn DomainController
		{
			get
			{
				return this.domainController;
			}
			set
			{
				this.domainController = value;
			}
		}

		// Token: 0x0600540C RID: 21516 RVA: 0x0015B10C File Offset: 0x0015930C
		protected override void InternalValidate()
		{
			base.InternalValidate();
			if (this.Component != null && !ServerComponentStateManager.IsValidComponent(this.Component))
			{
				base.WriteError(new ArgumentException(Strings.ServerComponentStateInvalidComponentName(this.Component)), ErrorCategory.InvalidArgument, null);
			}
		}

		// Token: 0x0600540D RID: 21517 RVA: 0x0015B148 File Offset: 0x00159348
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter(new object[]
			{
				this.serverId,
				this.Component
			});
			ADComputer adcomputer = null;
			ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(this.DomainController, true, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 98, "InternalProcessRecord", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\SystemConfigurationTasks\\ComponentStates\\GetServerComponentState.cs");
			string text = (!string.IsNullOrWhiteSpace(this.serverId.Fqdn)) ? this.serverId.Fqdn : this.serverId.ToString();
			string text2 = text;
			int num = text.IndexOf('.');
			if (num > 0)
			{
				text2 = text.Substring(0, num);
			}
			Server server = topologyConfigurationSession.FindServerByName(text2);
			if (server == null)
			{
				topologyConfigurationSession.UseConfigNC = false;
				topologyConfigurationSession.UseGlobalCatalog = true;
				adcomputer = topologyConfigurationSession.FindComputerByHostName(text2);
				if (adcomputer == null)
				{
					base.WriteError(new ADServerNotFoundException(text), ErrorCategory.InvalidArgument, null);
				}
			}
			if (string.IsNullOrEmpty(this.Component))
			{
				using (IEnumerator enumerator = Enum.GetValues(typeof(ServerComponentEnum)).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						ServerComponentEnum serverComponent = (ServerComponentEnum)obj;
						if (ServerComponentStateManager.IsValidComponent(serverComponent))
						{
							base.WriteObject(new ServerComponentStatePresentationObject((server != null) ? server.Id : adcomputer.Id, (server != null) ? server.Fqdn : adcomputer.DnsHostName, ServerComponentStateManager.GetComponentId(serverComponent), (server != null) ? server.ComponentStates : adcomputer.ComponentStates));
						}
					}
					goto IL_1B2;
				}
			}
			base.WriteObject(new ServerComponentStatePresentationObject((server != null) ? server.Id : adcomputer.Id, (server != null) ? server.Fqdn : adcomputer.DnsHostName, this.Component, (server != null) ? server.ComponentStates : adcomputer.ComponentStates));
			IL_1B2:
			TaskLogger.LogExit();
		}

		// Token: 0x0600540E RID: 21518 RVA: 0x0015B31C File Offset: 0x0015951C
		protected override bool IsKnownException(Exception exception)
		{
			return base.IsKnownException(exception) || exception is IOException;
		}

		// Token: 0x0400310F RID: 12559
		private ServerIdParameter serverId;

		// Token: 0x04003110 RID: 12560
		private Fqdn domainController;
	}
}
