using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.Exchange.Cluster.Replay;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage.ActiveManager;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020008C8 RID: 2248
	[Cmdlet("Stop", "DatabaseAvailabilityGroup", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High, DefaultParameterSetName = "Identity")]
	public sealed class StopDatabaseAvailabilityGroup : DatabaseAvailabilityGroupAction
	{
		// Token: 0x170017D4 RID: 6100
		// (get) Token: 0x06004FC8 RID: 20424 RVA: 0x0014DB88 File Offset: 0x0014BD88
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				if (base.ActiveDirectorySite == null)
				{
					return Strings.ConfirmationMessageStopDatabaseAvailabilityGroupServer(this.m_dag.Name, base.MailboxServer.ToString());
				}
				return Strings.ConfirmationMessageStopDatabaseAvailabilityGroupADSite(this.m_dag.Name, base.ActiveDirectorySite.ToString());
			}
		}

		// Token: 0x170017D5 RID: 6101
		// (get) Token: 0x06004FC9 RID: 20425 RVA: 0x0014DBD4 File Offset: 0x0014BDD4
		protected override string TaskName
		{
			get
			{
				return "stop-databaseavailabilitygroup";
			}
		}

		// Token: 0x06004FCA RID: 20426 RVA: 0x0014DBDC File Offset: 0x0014BDDC
		private void LogCommandLineParameters()
		{
			string[] parametersToLog = new string[]
			{
				"Identity",
				"ActiveDirectorySite",
				"MailboxServer",
				"DomainController",
				"ConfigurationOnly",
				"QuorumOnly",
				"WhatIf"
			};
			DagTaskHelper.LogCommandLineParameters(this.m_output, base.MyInvocation.Line, parametersToLog, base.Fields);
		}

		// Token: 0x06004FCB RID: 20427 RVA: 0x0014DC47 File Offset: 0x0014BE47
		protected override void InternalValidate()
		{
			base.InternalValidate();
			this.LogCommandLineParameters();
		}

		// Token: 0x06004FCC RID: 20428 RVA: 0x0014DC6C File Offset: 0x0014BE6C
		protected override void InternalProcessRecord()
		{
			Dictionary<AmServerName, Server> dictionary = new Dictionary<AmServerName, Server>(16);
			if (this.m_mailboxServer != null)
			{
				AmServerName serverName = new AmServerName(this.m_mailboxServer.Fqdn);
				if (!this.m_serversInDag.Keys.Any((AmServerName serverInDag) => serverInDag.Equals(serverName)))
				{
					this.m_output.WriteErrorSimple(new DagTaskServerIsNotInDagException(serverName.Fqdn, this.m_dag.Name));
				}
				dictionary.Add(serverName, this.m_mailboxServer);
				if (base.NeedToUpdateAD)
				{
					if (!this.m_stoppedServers.ContainsKey(serverName))
					{
						this.m_stoppedServers.Add(serverName, this.m_mailboxServer);
						this.m_output.AppendLogMessage("{0} added to stopped list", new object[]
						{
							serverName.NetbiosName
						});
					}
					if (this.m_startedServers.ContainsKey(serverName))
					{
						this.m_startedServers.Remove(serverName);
						this.m_output.AppendLogMessage("{0} removed from started list", new object[]
						{
							serverName.NetbiosName
						});
					}
				}
			}
			else
			{
				dictionary = DatabaseAvailabilityGroupAction.GetServersInSite(this.m_output, this.m_serversInDag.Values, base.ActiveDirectorySite);
				if (base.NeedToUpdateAD)
				{
					foreach (AmServerName amServerName in dictionary.Keys)
					{
						if (!this.m_stoppedServers.ContainsKey(amServerName))
						{
							this.m_output.AppendLogMessage("add {0} to stopped list", new object[]
							{
								amServerName.NetbiosName
							});
							this.m_stoppedServers.Add(amServerName, this.m_serversInDag[amServerName]);
						}
						if (this.m_startedServers.ContainsKey(amServerName))
						{
							this.m_output.AppendLogMessage("Remove {0} from started list", new object[]
							{
								amServerName.NetbiosName
							});
							this.m_startedServers.Remove(amServerName);
						}
					}
				}
			}
			if (base.NeedToUpdateAD)
			{
				this.m_dag.StoppedMailboxServers = DatabaseAvailabilityGroupAction.ServerListToFqdnList(this.m_stoppedServers.Keys);
				this.m_dag.StartedMailboxServers = DatabaseAvailabilityGroupAction.ServerListToFqdnList(this.m_startedServers.Keys);
				this.m_adSession.Save(this.m_dag);
				this.m_output.AppendLogMessage("updated the started servers list in AD:", new object[0]);
				foreach (AmServerName amServerName2 in this.m_startedServers.Keys)
				{
					this.m_output.AppendLogMessage("\t{0}", new object[]
					{
						amServerName2.NetbiosName
					});
				}
				this.m_output.AppendLogMessage("updated the stopped servers list in AD:", new object[0]);
				foreach (AmServerName amServerName3 in this.m_stoppedServers.Keys)
				{
					this.m_output.AppendLogMessage("\t{0}", new object[]
					{
						amServerName3.NetbiosName
					});
				}
				base.ForceADReplication();
			}
			if (base.NeedToUpdateCluster)
			{
				if (DatabaseAvailabilityGroupAction.IsClusterUp(this.m_dag, this.m_output))
				{
					DatabaseAvailabilityGroupAction.EvictStoppedNodes(this.m_dag, dictionary.Values, this.m_output);
				}
				else
				{
					DatabaseAvailabilityGroupAction.ForceCleanupStoppedNodes(this.m_dag, dictionary.Values, TimeSpan.FromSeconds(15.0), this.m_output);
				}
				TimeSpan timeout = TimeSpan.FromSeconds(1.0);
				foreach (AmServerName amServerName4 in dictionary.Keys)
				{
					this.m_output.AppendLogMessage("notify replayservice on {0} with consensus state 0", new object[]
					{
						amServerName4.NetbiosName
					});
					AmRpcClientHelper.RpcchSetAutomountConsensusStateBestEffort(amServerName4.Fqdn, 0, timeout);
				}
			}
			base.InternalProcessRecord();
		}
	}
}
