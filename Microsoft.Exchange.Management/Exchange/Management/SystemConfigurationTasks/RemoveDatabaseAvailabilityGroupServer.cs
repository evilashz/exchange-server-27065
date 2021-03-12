using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Management.Automation;
using System.Net;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Cluster.ActiveManagerServer;
using Microsoft.Exchange.Cluster.ClusApi;
using Microsoft.Exchange.Cluster.Replay;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage.Cluster;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020008BC RID: 2236
	[Cmdlet("Remove", "DatabaseAvailabilityGroupServer", ConfirmImpact = ConfirmImpact.High, SupportsShouldProcess = true)]
	public sealed class RemoveDatabaseAvailabilityGroupServer : SystemConfigurationObjectActionTask<DatabaseAvailabilityGroupIdParameter, DatabaseAvailabilityGroup>, IDisposable
	{
		// Token: 0x170017AA RID: 6058
		// (get) Token: 0x06004F1E RID: 20254 RVA: 0x00148820 File Offset: 0x00146A20
		// (set) Token: 0x06004F1F RID: 20255 RVA: 0x00148837 File Offset: 0x00146A37
		[Parameter(Mandatory = true, ParameterSetName = "Identity", ValueFromPipelineByPropertyName = true, ValueFromPipeline = true, Position = 1)]
		public ServerIdParameter MailboxServer
		{
			get
			{
				return (ServerIdParameter)base.Fields["MailboxServer"];
			}
			set
			{
				base.Fields["MailboxServer"] = value;
			}
		}

		// Token: 0x170017AB RID: 6059
		// (get) Token: 0x06004F20 RID: 20256 RVA: 0x0014884A File Offset: 0x00146A4A
		// (set) Token: 0x06004F21 RID: 20257 RVA: 0x00148870 File Offset: 0x00146A70
		[Parameter(Mandatory = false)]
		public SwitchParameter ConfigurationOnly
		{
			get
			{
				return (SwitchParameter)(base.Fields["ConfigurationOnly"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["ConfigurationOnly"] = value;
			}
		}

		// Token: 0x170017AC RID: 6060
		// (get) Token: 0x06004F22 RID: 20258 RVA: 0x00148888 File Offset: 0x00146A88
		// (set) Token: 0x06004F23 RID: 20259 RVA: 0x001488AE File Offset: 0x00146AAE
		[Parameter(Mandatory = false)]
		public SwitchParameter SkipDagValidation
		{
			get
			{
				return (SwitchParameter)(base.Fields["SkipDagValidation"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["SkipDagValidation"] = value;
			}
		}

		// Token: 0x06004F24 RID: 20260 RVA: 0x001488C6 File Offset: 0x00146AC6
		protected override bool IsKnownException(Exception e)
		{
			return AmExceptionHelper.IsKnownClusterException(this, e) || base.IsKnownException(e);
		}

		// Token: 0x170017AD RID: 6061
		// (get) Token: 0x06004F25 RID: 20261 RVA: 0x001488DA File Offset: 0x00146ADA
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageRemoveDatabaseAvailabilityGroupServer(this.m_mailboxServerName, this.m_dagName);
			}
		}

		// Token: 0x06004F26 RID: 20262 RVA: 0x001488F0 File Offset: 0x00146AF0
		public RemoveDatabaseAvailabilityGroupServer()
		{
			this.m_output = new HaTaskOutputHelper("remove-databaseavailabiltygroupserver", new Task.TaskErrorLoggingDelegate(base.WriteError), new Task.TaskWarningLoggingDelegate(this.WriteWarning), new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.TaskProgressLoggingDelegate(base.WriteProgress), this.GetHashCode());
			this.m_output.CreateTempLogFile();
			this.m_output.AppendLogMessage("remove-dagserver started", new object[0]);
		}

		// Token: 0x06004F27 RID: 20263 RVA: 0x00148978 File Offset: 0x00146B78
		private void ResolveParameters()
		{
			this.m_output.WriteProgressSimple(Strings.DagTaskValidatingParameters);
			this.m_dag = DagTaskHelper.DagIdParameterToDag(this.Identity, this.ConfigurationSession);
			this.m_dagName = this.m_dag.Name;
			this.m_mailboxServer = (Server)base.GetDataObject<Server>(this.MailboxServer, base.DataSession, null, new LocalizedString?(Strings.ErrorServerNotFound(this.MailboxServer.ToString())), new LocalizedString?(Strings.ErrorServerNotUnique(this.MailboxServer.ToString())));
			DagTaskHelper.ServerToMailboxServer(new Task.TaskErrorLoggingDelegate(this.m_output.WriteError), this.m_mailboxServer);
			this.m_mailboxAmServerName = new AmServerName(this.m_mailboxServer.Fqdn);
			this.m_mailboxServerName = this.m_mailboxServer.Name;
			this.m_configurationOnly = this.ConfigurationOnly;
			this.m_skipDagValidation = this.SkipDagValidation;
			DagTaskHelper.LogMachineIpAddresses(this.m_output, this.m_dagName);
			DagTaskHelper.LogMachineIpAddresses(this.m_output, this.m_mailboxAmServerName.Fqdn);
		}

		// Token: 0x06004F28 RID: 20264 RVA: 0x00148A94 File Offset: 0x00146C94
		private void CheckFswSettings()
		{
			this.m_fsw = new FileShareWitness((ITopologyConfigurationSession)base.DataSession, this.m_dag.Name, this.m_dag.WitnessServer, this.m_dag.WitnessDirectory, this.m_clusDag);
			try
			{
				this.m_fsw.Initialize();
			}
			catch (LocalizedException error)
			{
				this.m_output.WriteErrorSimple(error);
			}
			if (this.m_dag.AlternateWitnessServer != null && this.m_dag.AlternateWitnessDirectory != null)
			{
				this.m_afsw = new FileShareWitness((ITopologyConfigurationSession)base.DataSession, this.m_dag.Name, this.m_dag.AlternateWitnessServer, this.m_dag.AlternateWitnessDirectory, this.m_clusDag);
				try
				{
					this.m_afsw.Initialize();
				}
				catch (LocalizedException)
				{
				}
			}
		}

		// Token: 0x06004F29 RID: 20265 RVA: 0x00148B8C File Offset: 0x00146D8C
		private void CheckDatabasesAreNotReplicated()
		{
			Database[] databases = this.m_mailboxServer.GetDatabases();
			foreach (Database database in databases)
			{
				if (database.ReplicationType != ReplicationType.None)
				{
					this.m_output.WriteErrorSimple(new RemoveDagServerDatabaseIsReplicatedException(this.m_mailboxServerName, database.Name));
				}
			}
		}

		// Token: 0x06004F2A RID: 20266 RVA: 0x00148BE0 File Offset: 0x00146DE0
		private void CheckClusterStateForDagServerRemoval()
		{
			this.m_output.AppendLogMessage("CheckClusterStateForDagServerRemoval entered. m_removeNode={0}, m_destroyCluster={1}", new object[]
			{
				this.m_removeNode,
				this.m_destroyCluster
			});
			try
			{
				this.m_clusDag = AmCluster.OpenDagClus(this.m_dag);
			}
			catch (AmClusterException ex)
			{
				this.m_output.AppendLogMessage("Trying to open the cluster on the servers in the DAG '{0}' failed with exception {1}", new object[]
				{
					this.m_dagName,
					ex
				});
				this.m_output.WriteErrorSimple(new DagTaskRemoveDagServerMustHaveQuorumException(this.m_dagName));
			}
			using (DumpClusterTopology dumpClusterTopology = new DumpClusterTopology(this.m_clusDag, this.m_output))
			{
				dumpClusterTopology.Dump();
			}
			this.m_output.AppendLogMessage("Trying to open the node on the cluster.", new object[0]);
			IAmClusterNode amClusterNode = null;
			try
			{
				amClusterNode = this.m_clusDag.OpenNode(this.m_mailboxAmServerName);
			}
			catch (ClusterException ex2)
			{
				this.m_output.AppendLogMessage("OpenNode threw an exception. It's probably because the server is no longer clustered. Proceeding with -configurationonly.", new object[0]);
				this.m_output.AppendLogMessage("For the records, the exception was {0}.", new object[]
				{
					ex2
				});
				this.m_configurationOnly = true;
				this.m_mailboxServerIsDown = true;
				return;
			}
			using (amClusterNode)
			{
				AmNodeState state = amClusterNode.GetState(false);
				this.m_output.AppendLogMessage("Node.GetState( {0} ) reports that it is {1}.", new object[]
				{
					this.m_mailboxAmServerName,
					state
				});
				if (!AmClusterNode.IsNodeUp(state))
				{
					this.m_mailboxServerIsDown = true;
				}
			}
			if (!this.m_skipDagValidation)
			{
				try
				{
					DagTaskHelper.ValidateDagClusterMembership(this.m_output, this.m_dag, this.m_clusDag, this.m_mailboxAmServerName);
				}
				catch (ClusterException ex3)
				{
					this.DagTrace("ValidateDagClusterMembership() for the mailbox server failed possibly with error {0}, ex = {1}. This is OK.", new object[]
					{
						LocalizedException.GenerateErrorCode(ex3.InnerException).ToString(),
						ex3.Message
					});
				}
			}
			int num = this.m_clusDag.EnumerateNodeNames().Count<AmServerName>();
			this.DagTrace(string.Format("There are {0} nodes in the cluster.", num), new object[0]);
			if (num == 1)
			{
				this.m_destroyCluster = true;
			}
			else
			{
				this.m_removeNode = true;
			}
			bool destroyCluster = this.m_destroyCluster;
			this.ReopenClusterIfNecessary();
			if ((this.m_removeNode || this.m_destroyCluster) && this.m_dag.DatacenterActivationMode != DatacenterActivationModeOption.Off)
			{
				DagTaskHelper.CheckDagCanBeActivatedInDatacenter(this.m_output, this.m_dag, (ADObjectId)this.m_mailboxServer.Identity, (ITopologyConfigurationSession)base.DataSession);
			}
			this.DagTrace("CheckClusterStateForDagServerRemoval left. m_removeNode={0}, m_destroyCluster={1}.", new object[]
			{
				this.m_removeNode,
				this.m_destroyCluster
			});
		}

		// Token: 0x06004F2B RID: 20267 RVA: 0x00148F10 File Offset: 0x00147110
		private void ReopenClusterIfNecessary()
		{
			if (this.m_removeNode)
			{
				this.m_clusDag.Dispose();
				this.CheckServerDagAdSettings();
				IEnumerable<AmServerName> enumerable = from server in this.m_serversInDag
				where !SharedHelper.StringIEquals(server.Fqdn, this.m_mailboxAmServerName.Fqdn)
				select new AmServerName(server.Fqdn);
				try
				{
					IEnumerable<string> source = from serverName in enumerable
					select serverName.NetbiosName;
					this.m_output.AppendLogMessage("Reopening a handle to the cluster using the names [{0}].", new object[]
					{
						string.Join(", ", source.ToArray<string>())
					});
					this.m_clusDag = AmCluster.OpenByNames(enumerable);
				}
				catch (ClusterException)
				{
					this.m_output.WriteErrorSimple(new DagTaskClusteringMustBeInstalledAndRunningException(this.m_mailboxServerName));
				}
			}
		}

		// Token: 0x06004F2C RID: 20268 RVA: 0x00149000 File Offset: 0x00147200
		private void CheckServerDagAdSettings()
		{
			this.DagTrace("DAG {0} has {1} servers:", new object[]
			{
				this.m_dagName,
				this.m_dag.Servers.Count
			});
			foreach (ADObjectId identity in this.m_dag.Servers)
			{
				Server server = (Server)base.DataSession.Read<Server>(identity);
				this.DagTrace("DAG {0} contains server {1}.", new object[]
				{
					this.m_dagName,
					server.Name
				});
				this.m_serversInDag.Add(server);
			}
		}

		// Token: 0x06004F2D RID: 20269 RVA: 0x001490D0 File Offset: 0x001472D0
		private void LogCommandLineParameters()
		{
			string[] parametersToLog = new string[]
			{
				"Identity",
				"MailboxServer",
				"ConfigurationOnly",
				"WhatIf"
			};
			DagTaskHelper.LogCommandLineParameters(this.m_output, base.MyInvocation.Line, parametersToLog, base.Fields);
		}

		// Token: 0x06004F2E RID: 20270 RVA: 0x00149123 File Offset: 0x00147323
		protected override void InternalStateReset()
		{
			base.InternalStateReset();
			this.m_mailboxServerIsDown = false;
			this.m_removeNode = false;
			this.m_destroyCluster = false;
			if (this.m_serversInDag != null)
			{
				this.m_serversInDag.Clear();
			}
		}

		// Token: 0x06004F2F RID: 20271 RVA: 0x00149154 File Offset: 0x00147354
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			this.LogCommandLineParameters();
			this.m_removeNode = false;
			this.m_destroyCluster = false;
			this.ResolveParameters();
			DagTaskHelper.VerifyDagAndServersAreWithinScopes<DatabaseAvailabilityGroup>(this, this.m_dag, true);
			this.m_output.WriteProgress(Strings.ProgressStatusInProgress, Strings.DagTaskRemoveDagServerRunningChecks(this.m_mailboxServerName, this.m_dagName), 0);
			this.CheckDatabasesAreNotReplicated();
			if (!this.m_configurationOnly)
			{
				DagTaskHelper.CheckServerDoesNotBelongToDifferentDag(new Task.TaskErrorLoggingDelegate(this.m_output.WriteError), base.DataSession, this.m_mailboxServer, this.m_dagName);
				this.CheckFswSettings();
				this.CheckClusterStateForDagServerRemoval();
				if (!this.m_configurationOnly)
				{
					using (IAmClusterGroup amClusterGroup = this.m_clusDag.FindCoreClusterGroup())
					{
						AmServerName ownerNode = amClusterGroup.OwnerNode;
						this.m_output.AppendLogMessage("Checking if msexchangerepl is running on {0}.", new object[]
						{
							ownerNode.NetbiosName
						});
						DatabaseTasksHelper.CheckReplayServiceRunningOnNode(ownerNode, new Task.TaskErrorLoggingDelegate(base.WriteError));
						if (this.m_clusDag.CnoName != string.Empty)
						{
							using (IAmClusterResource amClusterResource = amClusterGroup.FindResourceByTypeName("Network Name"))
							{
								DagTaskHelper.LogCnoState(this.m_output, this.m_dagName, amClusterResource);
							}
						}
					}
				}
			}
			if (DagTaskHelper.FindServerAdObjectIdInDag(this.m_dag, this.m_mailboxAmServerName) == null)
			{
				StringBuilder stringBuilder = new StringBuilder(string.Format("{0}. srvToRemove={1} dagSvrs=(", this.m_dagName, this.m_mailboxAmServerName.NetbiosName));
				bool flag = false;
				foreach (ADObjectId adobjectId in this.m_dag.Servers)
				{
					if (flag)
					{
						stringBuilder.Append(",");
					}
					else
					{
						flag = true;
					}
					stringBuilder.Append(adobjectId.Name);
				}
				stringBuilder.Append(")");
				this.m_output.WriteErrorSimple(new DagTaskServerIsNotInDagException(this.m_mailboxServerName, stringBuilder.ToString()));
			}
			base.InternalValidate();
			TaskLogger.LogExit();
		}

		// Token: 0x06004F30 RID: 20272 RVA: 0x001493AC File Offset: 0x001475AC
		private void RemoveNodeFromCluster()
		{
			this.m_output.WriteProgressSimple(Strings.DagTaskRemovedNodeToCluster(this.m_mailboxServerName));
			using (IAmClusterNode amClusterNode = this.m_clusDag.OpenNode(this.m_mailboxAmServerName))
			{
				bool flag = false;
				string empty = string.Empty;
				string remoteServerName = "<unknown>";
				try
				{
					using (IAmClusterGroup amClusterGroup = this.m_clusDag.FindCoreClusterGroup())
					{
						AmServerName ownerNode = amClusterGroup.OwnerNode;
						remoteServerName = ownerNode.Fqdn;
						ReplayRpcClientWrapper.RunEvictNodeFromCluster(ownerNode, this.m_mailboxAmServerName, out empty);
					}
				}
				catch (DagTaskOperationFailedException ex)
				{
					AmClusterEvictWithoutCleanupException ex2;
					if (ex.TryGetTypedInnerException(out ex2))
					{
						this.m_output.WriteWarning(ex2.LocalizedString);
					}
					else
					{
						DagTaskHelper.LogRemoteVerboseLog(this.m_output, remoteServerName, empty);
						this.m_output.WriteErrorSimple(ex);
					}
				}
				catch (LocalizedException error)
				{
					DagTaskHelper.LogRemoteVerboseLog(this.m_output, remoteServerName, empty);
					this.m_output.WriteErrorSimple(error);
				}
				DagTaskHelper.LogRemoteVerboseLog(this.m_output, remoteServerName, empty);
				if (flag)
				{
					this.m_output.WriteWarning(Strings.DagTaskRemoveNodeCleanupFailed(amClusterNode.Name.Fqdn));
				}
			}
			this.m_output.WriteProgressSimple(Strings.DagTaskRemovedNodeToCluster(this.m_mailboxServerName));
			if (this.m_clusDag.CnoName != string.Empty)
			{
				this.m_output.WriteProgressSimple(Strings.DagTaskFixingUpIpResources);
				List<AmServerName> source = this.m_clusDag.EnumerateNodeNames().ToList<AmServerName>();
				IEnumerable<AmServerName> enumerable = from name in source
				where name != this.m_mailboxAmServerName
				select name;
				IEnumerable<string> source2 = from serverName in enumerable
				select serverName.NetbiosName;
				this.m_output.AppendLogMessage("Refreshing the cluster using the names [{0}].", new object[]
				{
					string.Join(", ", source2.ToArray<string>())
				});
				this.m_clusDag.Dispose();
				MultiValuedProperty<IPAddress> databaseAvailabilityGroupIpv4Addresses = this.m_dag.DatabaseAvailabilityGroupIpv4Addresses;
				IPAddress[] array = new IPAddress[0];
				if (databaseAvailabilityGroupIpv4Addresses.Count > 0)
				{
					array = databaseAvailabilityGroupIpv4Addresses.ToArray();
				}
				string[] value = (from addr in array
				select addr.ToString()).ToArray<string>();
				this.m_output.AppendLogMessage("Got the following IP addresses for the DAG (blank means DHCP): {0}", new object[]
				{
					string.Join(",", value)
				});
				this.m_clusDag = AmCluster.OpenByNames(enumerable);
				using (IAmClusterGroup amClusterGroup2 = this.m_clusDag.FindCoreClusterGroup())
				{
					using (IAmClusterResource amClusterResource = amClusterGroup2.FindResourceByTypeName("Network Name"))
					{
						this.m_output.AppendLogMessage("Cluster group net name = '{0}'.", new object[]
						{
							amClusterResource.Name
						});
						LocalizedString value2 = AmClusterResourceHelper.FixUpIpAddressesForNetName(this.m_output, this.m_clusDag, (AmClusterGroup)amClusterGroup2, (AmClusterResource)amClusterResource, array);
						this.m_output.WriteProgressSimple(Strings.DagTaskFixedUpIpResources(value2));
					}
				}
			}
		}

		// Token: 0x06004F31 RID: 20273 RVA: 0x001496FC File Offset: 0x001478FC
		private void DestroyCluster()
		{
			bool flag = false;
			string verboseData = null;
			string serverName = "<unknown>";
			try
			{
				this.m_output.WriteProgressSimple(Strings.RemoveDagDestroyingCluster(this.m_dagName, this.m_mailboxServerName, this.m_dagName));
				try
				{
					using (IAmClusterGroup amClusterGroup = this.m_clusDag.FindCoreClusterGroup())
					{
						AmServerName ownerNode = amClusterGroup.OwnerNode;
						serverName = ownerNode.Fqdn;
						ReplayRpcClientWrapper.RunDestroyCluster(ownerNode, this.m_dagName, out verboseData);
					}
				}
				catch (LocalizedException error)
				{
					this.m_output.AppendLogMessage(ReplayStrings.DagTaskRemoteOperationLogBegin(serverName));
					this.m_output.AppendLogMessage(ReplayStrings.DagTaskRemoteOperationLogData(verboseData));
					this.m_output.WriteErrorSimple(error);
				}
				this.m_output.AppendLogMessage(ReplayStrings.DagTaskRemoteOperationLogBegin(serverName));
				this.m_output.AppendLogMessage(ReplayStrings.DagTaskRemoteOperationLogData(verboseData));
				this.m_output.AppendLogMessage(ReplayStrings.DagTaskRemoteOperationLogEnd(serverName));
				flag = true;
				this.m_output.WriteProgressSimple(Strings.RemoveDagDestroyedCluster(this.m_dagName, this.m_mailboxServerName, this.m_dagName));
			}
			catch (ClusCommonTransientException ex)
			{
				uint status = 0U;
				Win32Exception ex2 = ex.InnerException as Win32Exception;
				if (ex2 != null)
				{
					status = (uint)ex2.NativeErrorCode;
				}
				this.m_output.WriteErrorSimple(new RemoveDagFailedToDestroyClusterException(this.m_dagName, this.m_dagName, status));
			}
			finally
			{
				if (flag)
				{
					this.m_clusDag.Dispose();
					this.m_clusDag = null;
				}
			}
		}

		// Token: 0x06004F32 RID: 20274 RVA: 0x001498B8 File Offset: 0x00147AB8
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			this.m_output.WriteProgressSimple(Strings.DagTaskRemovingServerFromDag(this.m_mailboxServerName, this.m_dagName));
			if (!this.m_configurationOnly)
			{
				if (this.m_removeNode)
				{
					this.MovePamIfNeeded();
					bool flag = DagTaskHelper.ShouldBeFileShareWitness(this.m_output, this.m_dag, this.m_clusDag, true);
					if (flag)
					{
						try
						{
							this.m_fsw.Create();
						}
						catch (LocalizedException ex)
						{
							this.m_output.WriteWarning(ex.LocalizedString);
						}
						if (this.m_afsw != null && this.m_afsw.IsInitialized && !this.m_afsw.Equals(this.m_fsw))
						{
							try
							{
								this.m_afsw.Create();
							}
							catch (LocalizedException ex2)
							{
								this.m_output.WriteWarning(ex2.LocalizedString);
							}
						}
					}
					bool flag2 = DagTaskHelper.IsQuorumTypeFileShareWitness(this.m_output, this.m_clusDag);
					this.m_output.AppendLogMessage("SkipDagValidation = {0}; IsQuorumTypeFSW = {1}; ShouldbeFSW = {2}", new object[]
					{
						this.m_skipDagValidation,
						flag2,
						flag
					});
					if (!this.m_skipDagValidation || (flag && !flag2))
					{
						DagTaskHelper.ChangeQuorumToMnsOrFswAsAppropriate(this.m_output, this, this.m_dag, this.m_clusDag, this.m_fsw, this.m_afsw, flag, false);
					}
					else
					{
						this.m_output.AppendLogMessage("Skip setting Quorum type", new object[0]);
					}
					this.RemoveNodeFromCluster();
				}
				else if (this.m_destroyCluster)
				{
					this.DestroyCluster();
					try
					{
						this.m_fsw.Delete();
					}
					catch (LocalizedException ex3)
					{
						this.m_output.WriteWarning(ex3.LocalizedString);
					}
					if (this.m_afsw != null && this.m_afsw.IsInitialized && !this.m_afsw.Equals(this.m_fsw))
					{
						try
						{
							this.m_afsw.Delete();
						}
						catch (LocalizedException ex4)
						{
							this.m_output.WriteWarning(ex4.LocalizedString);
						}
					}
				}
			}
			this.m_output.WriteProgressSimple(Strings.DagTaskUpdatingAdDagMembership(this.m_mailboxServerName, this.m_dagName));
			base.InternalProcessRecord();
			this.UpdateAdSettings();
			this.m_output.WriteProgressSimple(Strings.DagTaskUpdatedAdDagMembership(this.m_mailboxServerName, this.m_dagName));
			this.m_output.WriteProgressSimple(Strings.DagTaskRemovedServerFromDag(this.m_mailboxServerName, this.m_dagName));
			if (!this.m_configurationOnly)
			{
				this.m_output.WriteProgressSimple(Strings.DagTaskSleepAfterNodeRemoval(60, this.m_dagName, this.m_mailboxServerName));
				Thread.Sleep(60000);
			}
			if (this.m_destroyCluster)
			{
				ITopologyConfigurationSession configSession = (ITopologyConfigurationSession)base.DataSession;
				string dagName = this.m_dagName;
				DagTaskHelper.DisableComputerAccount(this.m_output, configSession, dagName);
			}
			DagTaskHelper.NotifyServerOfConfigChange(this.m_mailboxAmServerName);
			TaskLogger.LogExit();
		}

		// Token: 0x06004F33 RID: 20275 RVA: 0x00149BB0 File Offset: 0x00147DB0
		private void MovePamIfNeeded()
		{
			try
			{
				using (IAmClusterGroup amClusterGroup = this.m_clusDag.FindCoreClusterGroup())
				{
					AmServerName ownerNode = amClusterGroup.OwnerNode;
					this.m_output.AppendLogMessage("The core cluster group '{0}' is currently on machine '{1}'.", new object[]
					{
						amClusterGroup.Name,
						ownerNode.NetbiosName
					});
					if (ownerNode.Equals(this.m_mailboxAmServerName))
					{
						this.m_output.WriteProgressSimple(ReplayStrings.DagTaskMovingPam(this.m_mailboxServerName));
						string resourceType = (this.m_clusDag.CnoName == string.Empty) ? string.Empty : "Network Name";
						string newPam;
						if (!amClusterGroup.MoveGroupToReplayEnabledNode((string targetNode) => AmHelper.IsReplayRunning(targetNode), resourceType, TimeSpan.FromMinutes(3.0), out newPam))
						{
							this.m_output.WriteWarning(ReplayStrings.DagTaskPamNotMovedSubsequentOperationsMayBeSlowOrUnreliable);
						}
						else
						{
							this.m_output.WriteProgressSimple(ReplayStrings.DagTaskMovedPam(newPam));
						}
					}
				}
			}
			catch (LocalizedException ex)
			{
				this.m_output.AppendLogMessage("MoveGroupToReplayEnabledNode encountered the following exception: {0}", new object[]
				{
					ex
				});
				this.m_output.WriteWarning(ReplayStrings.DagTaskPamNotMovedSubsequentOperationsMayBeSlowOrUnreliable);
			}
		}

		// Token: 0x06004F34 RID: 20276 RVA: 0x00149D00 File Offset: 0x00147F00
		protected override void InternalEndProcessing()
		{
			TaskLogger.LogEnter();
			if (this.m_output != null)
			{
				this.m_output.WriteProgress(Strings.ProgressStatusCompleted, Strings.DagTaskDone, 100);
				this.m_output.CloseTempLogFile();
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06004F35 RID: 20277 RVA: 0x00149D38 File Offset: 0x00147F38
		private void UpdateAdSettings()
		{
			this.m_output.WriteProgressSimple(Strings.DagTaskUpdatingAdDagMembership(this.m_mailboxServerName, this.m_dagName));
			this.m_mailboxServer.DatabaseAvailabilityGroup = null;
			base.DataSession.Save(this.m_mailboxServer);
			this.m_output.WriteProgressSimple(Strings.DagTaskUpdatedAdDagMembership(this.m_mailboxServerName, this.m_dagName));
			DagTaskHelper.RevertDagServersDatabasesToStandalone(this.ConfigurationSession, this.m_output, this.m_mailboxServer);
			if (this.m_dag.StartedMailboxServers.Contains(this.m_mailboxServer.Fqdn) || this.m_dag.StoppedMailboxServers.Contains(this.m_mailboxServer.Fqdn))
			{
				DatabaseAvailabilityGroup databaseAvailabilityGroup = ((IConfigurationSession)base.DataSession).Read<DatabaseAvailabilityGroup>(this.m_dag.Id);
				bool flag = false;
				MultiValuedProperty<string> multiValuedProperty = databaseAvailabilityGroup.StartedMailboxServers;
				flag = (flag || multiValuedProperty.Remove(this.m_mailboxServer.Fqdn));
				databaseAvailabilityGroup.StartedMailboxServers = multiValuedProperty;
				multiValuedProperty = databaseAvailabilityGroup.StoppedMailboxServers;
				flag = (flag || multiValuedProperty.Remove(this.m_mailboxServer.Fqdn));
				databaseAvailabilityGroup.StoppedMailboxServers = multiValuedProperty;
				if (!flag)
				{
					this.m_output.AppendLogMessage("The server was not present in either the Started nor Stopped mailbox servers list in AD, so AD does not need an update.", new object[0]);
					return;
				}
				try
				{
					this.m_output.WriteProgressSimple(Strings.DagTaskUpdatingAdDagStartedStoppedMembership(this.m_mailboxServerName, this.m_dagName));
					base.DataSession.Save(databaseAvailabilityGroup);
					this.m_output.WriteProgressSimple(Strings.DagTaskUpdatedAdDagStartedStoppedMembership(this.m_mailboxServerName, this.m_dagName));
				}
				catch (ADExternalException ex)
				{
					this.DagTrace("Failed to update AD StartedMailboxServers/StoppedMailboxServers (error={0})", new object[]
					{
						ex
					});
				}
				catch (ADTransientException ex2)
				{
					this.DagTrace("Failed to update AD StartedMailboxServers/StoppedMailboxServers (error={0})", new object[]
					{
						ex2
					});
				}
			}
		}

		// Token: 0x06004F36 RID: 20278 RVA: 0x00149F10 File Offset: 0x00148110
		private void DagTrace(string format, params object[] args)
		{
			this.m_output.AppendLogMessage(format, args);
		}

		// Token: 0x06004F37 RID: 20279 RVA: 0x00149F20 File Offset: 0x00148120
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (!this.m_fDisposed)
				{
					if (disposing && this.m_clusDag != null)
					{
						this.m_clusDag.Dispose();
						this.m_clusDag = null;
					}
					this.m_fDisposed = true;
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x04002EE6 RID: 12006
		private FileShareWitness m_fsw;

		// Token: 0x04002EE7 RID: 12007
		private FileShareWitness m_afsw;

		// Token: 0x04002EE8 RID: 12008
		private DatabaseAvailabilityGroup m_dag;

		// Token: 0x04002EE9 RID: 12009
		private string m_dagName;

		// Token: 0x04002EEA RID: 12010
		private bool m_configurationOnly;

		// Token: 0x04002EEB RID: 12011
		private bool m_skipDagValidation;

		// Token: 0x04002EEC RID: 12012
		private bool m_mailboxServerIsDown;

		// Token: 0x04002EED RID: 12013
		private Server m_mailboxServer;

		// Token: 0x04002EEE RID: 12014
		private string m_mailboxServerName;

		// Token: 0x04002EEF RID: 12015
		private AmServerName m_mailboxAmServerName;

		// Token: 0x04002EF0 RID: 12016
		private List<Server> m_serversInDag = new List<Server>(8);

		// Token: 0x04002EF1 RID: 12017
		private AmCluster m_clusDag;

		// Token: 0x04002EF2 RID: 12018
		private HaTaskOutputHelper m_output;

		// Token: 0x04002EF3 RID: 12019
		private bool m_removeNode;

		// Token: 0x04002EF4 RID: 12020
		private bool m_destroyCluster;

		// Token: 0x04002EF5 RID: 12021
		private bool m_fDisposed;
	}
}
