using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Management.Automation;
using System.Security;
using System.Text;
using Microsoft.Exchange.Cluster.ClusApi;
using Microsoft.Exchange.Cluster.Replay;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.HA;
using Microsoft.Exchange.Data.HA.DirectoryServices;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x020005E5 RID: 1509
	[ClassAccessLevel(AccessLevel.Consumer)]
	[Cmdlet("Test", "ReplicationHealth", SupportsShouldProcess = true)]
	public sealed class TestReplicationHealth : SystemConfigurationObjectActionTask<ServerIdParameter, Server>
	{
		// Token: 0x17000FF7 RID: 4087
		// (get) Token: 0x06003592 RID: 13714 RVA: 0x000DC122 File Offset: 0x000DA322
		// (set) Token: 0x06003593 RID: 13715 RVA: 0x000DC12A File Offset: 0x000DA32A
		[Alias(new string[]
		{
			"Server"
		})]
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = false, ParameterSetName = "Identity", ValueFromPipeline = true, ValueFromPipelineByPropertyName = true, Position = 0)]
		public override ServerIdParameter Identity
		{
			get
			{
				return base.Identity;
			}
			set
			{
				base.Identity = value;
			}
		}

		// Token: 0x17000FF8 RID: 4088
		// (get) Token: 0x06003594 RID: 13716 RVA: 0x000DC133 File Offset: 0x000DA333
		// (set) Token: 0x06003595 RID: 13717 RVA: 0x000DC159 File Offset: 0x000DA359
		[Parameter(Mandatory = false)]
		public SwitchParameter OutputObjects
		{
			get
			{
				return (SwitchParameter)(base.Fields["OutputObjects"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["OutputObjects"] = value;
			}
		}

		// Token: 0x17000FF9 RID: 4089
		// (get) Token: 0x06003596 RID: 13718 RVA: 0x000DC171 File Offset: 0x000DA371
		// (set) Token: 0x06003597 RID: 13719 RVA: 0x000DC192 File Offset: 0x000DA392
		[Parameter(Mandatory = false)]
		public bool MonitoringContext
		{
			get
			{
				return (bool)(base.Fields["MonitoringContext"] ?? false);
			}
			set
			{
				base.Fields["MonitoringContext"] = value;
			}
		}

		// Token: 0x17000FFA RID: 4090
		// (get) Token: 0x06003598 RID: 13720 RVA: 0x000DC1AA File Offset: 0x000DA3AA
		// (set) Token: 0x06003599 RID: 13721 RVA: 0x000DC1D6 File Offset: 0x000DA3D6
		[Parameter(Mandatory = false)]
		public uint TransientEventSuppressionWindow
		{
			get
			{
				object obj;
				if ((obj = base.Fields["TransientEventSuppressionWindow"]) == null)
				{
					obj = (this.MonitoringContext ? 3U : 0U);
				}
				return (uint)obj;
			}
			set
			{
				base.Fields["TransientEventSuppressionWindow"] = value;
			}
		}

		// Token: 0x17000FFB RID: 4091
		// (get) Token: 0x0600359A RID: 13722 RVA: 0x000DC1EE File Offset: 0x000DA3EE
		// (set) Token: 0x0600359B RID: 13723 RVA: 0x000DC210 File Offset: 0x000DA410
		[ValidateRange(1, 2147483647)]
		[Parameter(Mandatory = false)]
		public int ActiveDirectoryTimeout
		{
			get
			{
				return (int)(base.Fields["ActiveDirectoryTimeout"] ?? 15);
			}
			set
			{
				base.Fields["ActiveDirectoryTimeout"] = value;
			}
		}

		// Token: 0x17000FFC RID: 4092
		// (get) Token: 0x0600359D RID: 13725 RVA: 0x000DC230 File Offset: 0x000DA430
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageTestReplicationHealth(this.m_serverName);
			}
		}

		// Token: 0x0600359E RID: 13726 RVA: 0x000DC23D File Offset: 0x000DA43D
		protected override bool IsKnownException(Exception exception)
		{
			return base.IsKnownException(exception) || DataAccessHelper.IsDataAccessKnownException(exception) || MonitoringHelper.IsKnownExceptionForMonitoring(exception) || AmExceptionHelper.IsKnownClusterException(this, exception) || exception is ReplicationCheckException;
		}

		// Token: 0x0600359F RID: 13727 RVA: 0x000DC26C File Offset: 0x000DA46C
		protected override void InternalStateReset()
		{
			base.InternalStateReset();
			this.ResetPrivateState();
			ReplicationCheckGlobals.ResetState();
		}

		// Token: 0x060035A0 RID: 13728 RVA: 0x000DC27F File Offset: 0x000DA47F
		protected override void InternalBeginProcessing()
		{
			this.m_monitoringData = new MonitoringData();
			this.m_eventManager = new ReplicationEventManager();
			this.m_replayConfigs = new List<ReplayConfiguration>();
			base.InternalBeginProcessing();
		}

		// Token: 0x060035A1 RID: 13729 RVA: 0x000DC2A8 File Offset: 0x000DA4A8
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			try
			{
				this.ConfigurationSession.ServerTimeout = new TimeSpan?(TimeSpan.FromSeconds((double)this.ActiveDirectoryTimeout));
				((IDirectorySession)base.DataSession).ServerTimeout = new TimeSpan?(TimeSpan.FromSeconds((double)this.ActiveDirectoryTimeout));
				if (this.Identity == null)
				{
					this.m_serverName = Environment.MachineName;
					this.Identity = ServerIdParameter.Parse(this.m_serverName);
				}
				base.InternalValidate();
				if (base.HasErrors)
				{
					TaskLogger.LogExit();
				}
				else
				{
					ADServerWrapper server = ADObjectWrapperFactory.CreateWrapper(this.DataObject);
					ReplicationCheckGlobals.Server = server;
					this.m_serverName = this.DataObject.Name;
					ExTraceGlobals.CmdletsTracer.TraceDebug<string>((long)this.GetHashCode(), "serverName is '{0}'.", this.m_serverName ?? "null");
					this.m_isLocal = SharedHelper.StringIEquals(this.m_serverName, Environment.MachineName);
					if (!this.m_isLocal && this.MonitoringContext)
					{
						this.WriteErrorAndMonitoringEvent(new CannotRunMonitoringTaskRemotelyException(this.m_serverName), ErrorCategory.InvalidOperation, this.Identity, 10011, "MSExchange Monitoring ReplicationHealth");
					}
					ReplicationCheckGlobals.RunningInMonitoringContext = this.MonitoringContext;
					if (this.m_isLocal && !this.CheckLocalServerRegistryRoles())
					{
						ExTraceGlobals.CmdletsTracer.TraceDebug((long)this.GetHashCode(), "Local server does not have Exchange 2009 Mailbox Role in the registry.");
					}
					else
					{
						this.CheckServerObject();
						if (this.DataObject.DatabaseAvailabilityGroup != null)
						{
							this.m_serverConfigBitfield |= ServerConfig.DagMember;
							this.m_dag = this.ConfigurationSession.Read<DatabaseAvailabilityGroup>(this.DataObject.DatabaseAvailabilityGroup);
							if (this.m_dag.StoppedMailboxServers.Contains(new AmServerName(this.m_serverName).Fqdn))
							{
								this.m_serverConfigBitfield |= ServerConfig.Stopped;
							}
						}
						else
						{
							ExTraceGlobals.CmdletsTracer.TraceDebug<string>((long)this.GetHashCode(), "{0} is a Standalone non-DAG Mailbox server.", this.DataObject.Name);
						}
						try
						{
							this.BuildReplayConfigurations(this.m_dag, server);
						}
						catch (ClusterException exception)
						{
							this.WriteErrorAndMonitoringEvent(exception, ErrorCategory.InvalidOperation, null, 10003, "MSExchange Monitoring ReplicationHealth");
							return;
						}
						catch (TransientException exception2)
						{
							this.WriteErrorAndMonitoringEvent(exception2, ErrorCategory.InvalidOperation, null, 10003, "MSExchange Monitoring ReplicationHealth");
							return;
						}
						catch (DataSourceOperationException exception3)
						{
							this.WriteErrorAndMonitoringEvent(exception3, ErrorCategory.InvalidOperation, null, 10003, "MSExchange Monitoring ReplicationHealth");
							return;
						}
						catch (DataValidationException exception4)
						{
							this.WriteErrorAndMonitoringEvent(exception4, ErrorCategory.InvalidData, null, 10003, "MSExchange Monitoring ReplicationHealth");
							return;
						}
						ReplicationCheckGlobals.ServerConfiguration = this.m_serverConfigBitfield;
						if (this.DataObject != null)
						{
							this.m_useReplayRpc = ReplayRpcVersionControl.IsGetCopyStatusEx2RpcSupported(this.DataObject.AdminDisplayVersion);
						}
						this.CheckIfTaskCanRun();
					}
				}
			}
			finally
			{
				if (base.HasErrors)
				{
					if (this.MonitoringContext)
					{
						this.WriteMonitoringData();
					}
					ReplicationCheckGlobals.ResetState();
				}
				TaskLogger.LogExit();
			}
		}

		// Token: 0x060035A2 RID: 13730 RVA: 0x000DC5C8 File Offset: 0x000DA7C8
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			if (base.HasErrors)
			{
				TaskLogger.LogExit();
				return;
			}
			ReplicationCheckGlobals.WriteVerboseDelegate = new Task.TaskVerboseLoggingDelegate(base.WriteVerbose);
			try
			{
				base.WriteVerbose(Strings.StartingToRunChecks(this.m_serverName));
				this.RunChecks();
				if (this.MonitoringContext && !this.m_eventManager.HasMomEvents())
				{
					base.WriteVerbose(Strings.NoMonitoringErrorsInTestReplicationHealth(this.m_serverName));
					this.m_eventManager.LogEvent(10000, Strings.NoMonitoringErrorsInTestReplicationHealth(this.m_serverName));
				}
			}
			finally
			{
				if (this.MonitoringContext)
				{
					this.WriteMonitoringData();
				}
				ReplicationCheckGlobals.ResetState();
				TaskLogger.LogExit();
			}
		}

		// Token: 0x060035A3 RID: 13731 RVA: 0x000DC684 File Offset: 0x000DA884
		private static bool AreConfigBitsSet(ServerConfig configuration, ServerConfig comparisonBits)
		{
			if (comparisonBits == ServerConfig.Unknown)
			{
				throw new ArgumentException("comparisonBits cannot be Unknown.", "comparisonBits");
			}
			return (configuration & comparisonBits) == comparisonBits;
		}

		// Token: 0x060035A4 RID: 13732 RVA: 0x000DC6A0 File Offset: 0x000DA8A0
		internal static LocalizedString GetMachineConfigurationString(ServerConfig machineConfig)
		{
			StringBuilder stringBuilder = new StringBuilder(4);
			string serverName = string.Empty;
			if (ReplicationCheckGlobals.Server != null)
			{
				serverName = ReplicationCheckGlobals.Server.Name;
			}
			if (TestReplicationHealth.AreConfigBitsSet(machineConfig, ServerConfig.DagMemberNoDatabases))
			{
				stringBuilder.AppendFormat(TestReplicationHealth.SpaceConcatFormatString, Strings.DagMemberNoDatabasesString(serverName));
			}
			else if (TestReplicationHealth.AreConfigBitsSet(machineConfig, ServerConfig.DagMember))
			{
				stringBuilder.AppendFormat(TestReplicationHealth.SpaceConcatFormatString, Strings.DagMemberString(serverName));
			}
			else
			{
				stringBuilder.AppendFormat(TestReplicationHealth.SpaceConcatFormatString, Strings.StandaloneMailboxString(serverName));
			}
			if (TestReplicationHealth.AreConfigBitsSet(machineConfig, ServerConfig.RcrSource) || TestReplicationHealth.AreConfigBitsSet(machineConfig, ServerConfig.RcrTarget))
			{
				stringBuilder.AppendFormat(TestReplicationHealth.SpaceConcatFormatString, Strings.RcrConfigString(serverName));
			}
			LocalizedString result = new LocalizedString(stringBuilder.ToString());
			return result;
		}

		// Token: 0x060035A5 RID: 13733 RVA: 0x000DC75F File Offset: 0x000DA95F
		private bool AreConfigBitsSet(ServerConfig configBits)
		{
			return TestReplicationHealth.AreConfigBitsSet(this.m_serverConfigBitfield, configBits);
		}

		// Token: 0x060035A6 RID: 13734 RVA: 0x000DC770 File Offset: 0x000DA970
		private void BuildReplayConfigurations(DatabaseAvailabilityGroup dag, IADServer server)
		{
			IADDatabaseAvailabilityGroup dag2 = null;
			if (dag != null)
			{
				dag2 = ADObjectWrapperFactory.CreateWrapper(dag);
			}
			List<ReplayConfiguration> list;
			List<ReplayConfiguration> list2;
			ReplayConfigurationHelper.TaskConstructAllDatabaseConfigurations(dag2, server, out list, out list2);
			if (list != null && list.Count > 0)
			{
				ExTraceGlobals.CmdletsTracer.TraceDebug((long)this.GetHashCode(), "BuildReplayConfigurations(): Found RCR Source Configurations.");
				this.m_serverConfigBitfield |= ServerConfig.RcrSource;
				this.m_replayConfigs.AddRange(list);
			}
			if (list2 != null && list2.Count > 0)
			{
				ExTraceGlobals.CmdletsTracer.TraceDebug((long)this.GetHashCode(), "BuildReplayConfigurations(): Found RCR Target Configurations.");
				this.m_serverConfigBitfield |= ServerConfig.RcrTarget;
				this.m_replayConfigs.AddRange(list2);
			}
			if (this.AreConfigBitsSet(ServerConfig.DagMember) && !this.AreConfigBitsSet(ServerConfig.RcrSource) && !this.AreConfigBitsSet(ServerConfig.RcrTarget))
			{
				ExTraceGlobals.CmdletsTracer.TraceDebug((long)this.GetHashCode(), "BuildReplayConfigurations(): Server is a DAG member but has no Database copies.");
				this.m_serverConfigBitfield |= ServerConfig.DagMemberNoDatabases;
			}
			ExTraceGlobals.CmdletsTracer.TraceDebug<string>((long)this.GetHashCode(), "BuildReplayConfigurations(): The following bits are set on localConfigBitfield: {0}", this.m_serverConfigBitfield.ToString());
		}

		// Token: 0x060035A7 RID: 13735 RVA: 0x000DC874 File Offset: 0x000DAA74
		private void CheckIfTaskCanRun()
		{
			base.WriteVerbose(TestReplicationHealth.GetMachineConfigurationString(this.m_serverConfigBitfield));
			if (this.AreConfigBitsSet(ServerConfig.Stopped))
			{
				this.WriteWarning(Strings.DagMemberStopped(this.m_serverName));
			}
			if (this.AreConfigBitsSet(ServerConfig.DagMemberNoDatabases))
			{
				this.WriteWarning(Strings.DagMemberNoDatabases(this.m_serverName));
			}
			if (this.IsServerStandaloneWithNoReplicas())
			{
				this.WriteWarning(Strings.StandaloneMailboxNoReplication(this.m_serverName));
			}
		}

		// Token: 0x060035A8 RID: 13736 RVA: 0x000DC8DF File Offset: 0x000DAADF
		private bool IsServerStandaloneWithNoReplicas()
		{
			return this.m_dag == null && this.m_serverConfigBitfield == ServerConfig.Unknown;
		}

		// Token: 0x060035A9 RID: 13737 RVA: 0x000DC8F4 File Offset: 0x000DAAF4
		private void RunChecks()
		{
			TaskLogger.LogEnter();
			try
			{
				DatabaseHealthValidationRunner validationRunner = new DatabaseHealthValidationRunner(this.m_serverName);
				if (this.AreConfigBitsSet(ServerConfig.DagMember))
				{
					using (DagMemberMultiChecks dagMemberMultiChecks = new DagMemberMultiChecks(this.m_serverName, this.m_eventManager, "MSExchange Monitoring ReplicationHealth", this.TransientEventSuppressionWindow, ADObjectWrapperFactory.CreateWrapper(this.m_dag)))
					{
						this.RunMultiChecks(dagMemberMultiChecks);
					}
				}
				if (this.m_useReplayRpc)
				{
					ReplicationCheckGlobals.UsingReplayRpc = true;
				}
				if (this.AreConfigBitsSet(ServerConfig.RcrSource))
				{
					using (RcrSourceMultiChecks rcrSourceMultiChecks = new RcrSourceMultiChecks(this.m_serverName, this.m_eventManager, "MSExchange Monitoring ReplicationHealth", validationRunner, this.m_replayConfigs, ReplicationCheckGlobals.CopyStatusResults, this.TransientEventSuppressionWindow))
					{
						this.RunMultiChecks(rcrSourceMultiChecks);
					}
				}
				if (this.AreConfigBitsSet(ServerConfig.RcrTarget) && this.m_dag.ThirdPartyReplication != ThirdPartyReplicationMode.Enabled)
				{
					using (TargetCopyMultiChecks targetCopyMultiChecks = new TargetCopyMultiChecks(this.m_serverName, this.m_eventManager, "MSExchange Monitoring ReplicationHealth", validationRunner, this.m_replayConfigs, ReplicationCheckGlobals.CopyStatusResults, this.TransientEventSuppressionWindow))
					{
						this.RunMultiChecks(targetCopyMultiChecks);
					}
				}
				if (this.IsServerStandaloneWithNoReplicas())
				{
					using (StandaloneMultiChecks standaloneMultiChecks = new StandaloneMultiChecks(this.m_serverName, this.m_eventManager, "MSExchange Monitoring ReplicationHealth", this.TransientEventSuppressionWindow))
					{
						this.RunMultiChecks(standaloneMultiChecks);
					}
				}
			}
			finally
			{
				TaskLogger.LogExit();
			}
		}

		// Token: 0x060035AA RID: 13738 RVA: 0x000DCAC0 File Offset: 0x000DACC0
		private void RunMultiChecks(MultiReplicationCheck multiChecks)
		{
			multiChecks.Run();
			this.WriteCheckResults(multiChecks);
			this.LogEventsInMonitoringContext(multiChecks);
		}

		// Token: 0x060035AB RID: 13739 RVA: 0x000DCAD6 File Offset: 0x000DACD6
		private void WriteErrorAndMonitoringEvent(Exception exception, ErrorCategory errorCategory, object target, int eventId, string eventSource)
		{
			this.m_monitoringData.Events.Add(new MonitoringEvent(eventSource, eventId, EventTypeEnumeration.Error, exception.Message));
			base.WriteError(exception, errorCategory, target);
		}

		// Token: 0x060035AC RID: 13740 RVA: 0x000DCB01 File Offset: 0x000DAD01
		private void WriteMonitoringData()
		{
			this.m_eventManager.WriteMonitoringEvents(this.m_monitoringData, "MSExchange Monitoring ReplicationHealth");
			base.WriteObject(this.m_monitoringData);
		}

		// Token: 0x060035AD RID: 13741 RVA: 0x000DCB28 File Offset: 0x000DAD28
		private void WriteCheckResults(MultiReplicationCheck multiChecks)
		{
			if (this.OutputObjects.ToBool())
			{
				using (List<ReplicationCheckOutputObject>.Enumerator enumerator = multiChecks.GetAllOutputObjects().GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						ReplicationCheckOutputObject sendToPipeline = enumerator.Current;
						base.WriteObject(sendToPipeline);
					}
					return;
				}
			}
			foreach (ReplicationCheckOutcome sendToPipeline2 in multiChecks.GetAllOutcomes())
			{
				base.WriteObject(sendToPipeline2);
			}
		}

		// Token: 0x060035AE RID: 13742 RVA: 0x000DCBD0 File Offset: 0x000DADD0
		private void LogEventsInMonitoringContext(MultiReplicationCheck multiChecks)
		{
			if (this.MonitoringContext)
			{
				multiChecks.LogEvents();
			}
		}

		// Token: 0x060035AF RID: 13743 RVA: 0x000DCBE0 File Offset: 0x000DADE0
		private string[] GetServerName()
		{
			string[] array = new string[]
			{
				this.m_serverName
			};
			ExTraceGlobals.HealthChecksTracer.TraceDebug<string>((long)this.GetHashCode(), "GetServerName(): Returning the following servers to use for the RPC: {0}", string.Join(",", array));
			return array;
		}

		// Token: 0x060035B0 RID: 13744 RVA: 0x000DCC24 File Offset: 0x000DAE24
		private bool CheckLocalServerRegistryRoles()
		{
			bool result;
			try
			{
				base.WriteVerbose(Strings.ReadingE14ServerRoles(this.m_serverName));
				this.m_serverRolesBitfield = MpServerRoles.GetLocalE12ServerRolesFromRegistry();
				if ((this.m_serverRolesBitfield & ServerRole.Mailbox) != ServerRole.Mailbox)
				{
					this.WriteErrorAndMonitoringEvent(new NoMailboxRoleInstalledException(this.m_serverName), ErrorCategory.NotInstalled, null, 10002, "MSExchange Monitoring ReplicationHealth");
					result = false;
				}
				else
				{
					result = true;
				}
			}
			catch (UnauthorizedAccessException ex)
			{
				CannotReadRolesFromRegistryException exception = new CannotReadRolesFromRegistryException(ex.Message);
				this.WriteErrorAndMonitoringEvent(exception, ErrorCategory.PermissionDenied, null, 10001, "MSExchange Monitoring ReplicationHealth");
				result = false;
			}
			catch (SecurityException ex2)
			{
				CannotReadRolesFromRegistryException exception2 = new CannotReadRolesFromRegistryException(ex2.Message);
				this.WriteErrorAndMonitoringEvent(exception2, ErrorCategory.PermissionDenied, null, 10001, "MSExchange Monitoring ReplicationHealth");
				result = false;
			}
			return result;
		}

		// Token: 0x060035B1 RID: 13745 RVA: 0x000DCCEC File Offset: 0x000DAEEC
		private void CheckServerObject()
		{
			if (!this.DataObject.IsMailboxServer)
			{
				this.WriteErrorAndMonitoringEvent(this.DataObject.GetServerRoleError(ServerRole.Mailbox), ErrorCategory.InvalidOperation, this.Identity, 10003, "MSExchange Monitoring ReplicationHealth");
				return;
			}
			if (!this.DataObject.IsE14OrLater)
			{
				this.WriteErrorAndMonitoringEvent(new ServerConfigurationException(this.m_serverName, Strings.ErrorServerNotE14OrLater(this.m_serverName)), ErrorCategory.InvalidOperation, this.Identity, 10003, "MSExchange Monitoring ReplicationHealth");
			}
		}

		// Token: 0x060035B2 RID: 13746 RVA: 0x000DCD6C File Offset: 0x000DAF6C
		private void ResetPrivateState()
		{
			this.m_monitoringData = new MonitoringData();
			this.m_eventManager = new ReplicationEventManager();
			this.m_replayConfigs = new List<ReplayConfiguration>();
			this.m_dag = null;
			this.m_useReplayRpc = false;
			this.m_serverName = null;
			this.m_serverRolesBitfield = ServerRole.None;
			this.m_isLocal = false;
			this.m_serverConfigBitfield = ServerConfig.Unknown;
		}

		// Token: 0x060035B3 RID: 13747 RVA: 0x000DCDC4 File Offset: 0x000DAFC4
		[Conditional("DEBUG")]
		private void AssertConfigurationIsValid()
		{
		}

		// Token: 0x060035B4 RID: 13748 RVA: 0x000DCDC6 File Offset: 0x000DAFC6
		[Conditional("DEBUG")]
		private void AssertBitsNotSetOnServerType(ServerConfig configBits, ServerConfig serverConfig)
		{
		}

		// Token: 0x060035B5 RID: 13749 RVA: 0x000DCDC8 File Offset: 0x000DAFC8
		[Conditional("DEBUG")]
		private void AssertBitsNotSetOnStandalone(ServerConfig configBits)
		{
		}

		// Token: 0x040024BF RID: 9407
		private const string CmdletNoun = "ReplicationHealth";

		// Token: 0x040024C0 RID: 9408
		private const string CmdletMonitoringEventSource = "MSExchange Monitoring ReplicationHealth";

		// Token: 0x040024C1 RID: 9409
		private const int DefaultADOperationsTimeoutInSeconds = 15;

		// Token: 0x040024C2 RID: 9410
		private MonitoringData m_monitoringData;

		// Token: 0x040024C3 RID: 9411
		private ReplicationEventManager m_eventManager;

		// Token: 0x040024C4 RID: 9412
		private string m_serverName;

		// Token: 0x040024C5 RID: 9413
		private DatabaseAvailabilityGroup m_dag;

		// Token: 0x040024C6 RID: 9414
		private ServerRole m_serverRolesBitfield;

		// Token: 0x040024C7 RID: 9415
		private List<ReplayConfiguration> m_replayConfigs;

		// Token: 0x040024C8 RID: 9416
		private bool m_useReplayRpc;

		// Token: 0x040024C9 RID: 9417
		private ServerConfig m_serverConfigBitfield;

		// Token: 0x040024CA RID: 9418
		private bool m_isLocal;

		// Token: 0x040024CB RID: 9419
		private static string SpaceConcatFormatString = "{0} ";
	}
}
