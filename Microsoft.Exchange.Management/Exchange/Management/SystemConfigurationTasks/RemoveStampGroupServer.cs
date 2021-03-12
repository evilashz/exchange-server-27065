using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Threading;
using Microsoft.Exchange.Cluster.ClusApi;
using Microsoft.Exchange.Cluster.Replay;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020008BE RID: 2238
	[Cmdlet("Remove", "StampGroupServer", ConfirmImpact = ConfirmImpact.High, SupportsShouldProcess = true)]
	public sealed class RemoveStampGroupServer : SystemConfigurationObjectActionTask<StampGroupIdParameter, StampGroup>, IDisposable
	{
		// Token: 0x170017AF RID: 6063
		// (get) Token: 0x06004F44 RID: 20292 RVA: 0x0014A026 File Offset: 0x00148226
		// (set) Token: 0x06004F45 RID: 20293 RVA: 0x0014A03D File Offset: 0x0014823D
		[Parameter(Mandatory = true, ParameterSetName = "Identity", ValueFromPipelineByPropertyName = true, ValueFromPipeline = true, Position = 1)]
		public ServerIdParameter Server
		{
			get
			{
				return (ServerIdParameter)base.Fields["Server"];
			}
			set
			{
				base.Fields["Server"] = value;
			}
		}

		// Token: 0x170017B0 RID: 6064
		// (get) Token: 0x06004F46 RID: 20294 RVA: 0x0014A050 File Offset: 0x00148250
		// (set) Token: 0x06004F47 RID: 20295 RVA: 0x0014A076 File Offset: 0x00148276
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

		// Token: 0x06004F48 RID: 20296 RVA: 0x0014A08E File Offset: 0x0014828E
		protected override bool IsKnownException(Exception e)
		{
			return AmExceptionHelper.IsKnownClusterException(this, e) || base.IsKnownException(e);
		}

		// Token: 0x170017B1 RID: 6065
		// (get) Token: 0x06004F49 RID: 20297 RVA: 0x0014A0A2 File Offset: 0x001482A2
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageRemoveDatabaseAvailabilityGroupServer(this.m_ServerName, this.m_stampGroupName);
			}
		}

		// Token: 0x06004F4A RID: 20298 RVA: 0x0014A0B8 File Offset: 0x001482B8
		public RemoveStampGroupServer()
		{
			this.m_output = new HaTaskOutputHelper("remove-stampgroupserver", new Task.TaskErrorLoggingDelegate(base.WriteError), new Task.TaskWarningLoggingDelegate(this.WriteWarning), new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.TaskProgressLoggingDelegate(base.WriteProgress), this.GetHashCode());
			this.m_output.CreateTempLogFile();
			this.m_output.AppendLogMessage("remove-stampgroupserver started", new object[0]);
		}

		// Token: 0x06004F4B RID: 20299 RVA: 0x0014A140 File Offset: 0x00148340
		private void ResolveParameters()
		{
			this.m_output.WriteProgressSimple(Strings.DagTaskValidatingParameters);
			this.m_stampGroup = StampGroupTaskHelper.StampGroupIdParameterToStampGroup(this.Identity, this.ConfigurationSession);
			this.m_stampGroupName = this.m_stampGroup.Name;
			this.m_Server = (Server)base.GetDataObject<Server>(this.Server, base.DataSession, null, new LocalizedString?(Strings.ErrorServerNotFound(this.Server.ToString())), new LocalizedString?(Strings.ErrorServerNotUnique(this.Server.ToString())));
			this.m_ServerName = this.m_Server.Name;
			this.m_configurationOnly = this.ConfigurationOnly;
			DagTaskHelper.LogMachineIpAddresses(this.m_output, this.m_stampGroupName);
		}

		// Token: 0x06004F4C RID: 20300 RVA: 0x0014A200 File Offset: 0x00148400
		private void LogCommandLineParameters()
		{
			string[] parametersToLog = new string[]
			{
				"Identity",
				"Server",
				"ConfigurationOnly",
				"WhatIf"
			};
			DagTaskHelper.LogCommandLineParameters(this.m_output, base.MyInvocation.Line, parametersToLog, base.Fields);
		}

		// Token: 0x06004F4D RID: 20301 RVA: 0x0014A253 File Offset: 0x00148453
		protected override void InternalStateReset()
		{
			base.InternalStateReset();
			if (this.m_serversInDag != null)
			{
				this.m_serversInDag.Clear();
			}
		}

		// Token: 0x06004F4E RID: 20302 RVA: 0x0014A26E File Offset: 0x0014846E
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			this.LogCommandLineParameters();
			this.ResolveParameters();
			base.InternalValidate();
			TaskLogger.LogExit();
		}

		// Token: 0x06004F4F RID: 20303 RVA: 0x0014A28C File Offset: 0x0014848C
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			this.m_output.WriteProgressSimple(Strings.DagTaskRemovingServerFromDag(this.m_ServerName, this.m_stampGroupName));
			this.m_output.WriteProgressSimple(Strings.DagTaskUpdatingAdDagMembership(this.m_ServerName, this.m_stampGroupName));
			base.InternalProcessRecord();
			this.UpdateAdSettings();
			this.m_output.WriteProgressSimple(Strings.DagTaskUpdatedAdDagMembership(this.m_ServerName, this.m_stampGroupName));
			this.m_output.WriteProgressSimple(Strings.DagTaskRemovedServerFromDag(this.m_ServerName, this.m_stampGroupName));
			if (!this.m_configurationOnly)
			{
				this.m_output.WriteProgressSimple(Strings.DagTaskSleepAfterNodeRemoval(60, this.m_stampGroupName, this.m_ServerName));
				Thread.Sleep(60000);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06004F50 RID: 20304 RVA: 0x0014A34F File Offset: 0x0014854F
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

		// Token: 0x06004F51 RID: 20305 RVA: 0x0014A388 File Offset: 0x00148588
		private void UpdateAdSettings()
		{
			this.m_output.WriteProgressSimple(Strings.DagTaskUpdatingAdDagMembership(this.m_ServerName, this.m_stampGroupName));
			this.m_Server.DatabaseAvailabilityGroup = null;
			base.DataSession.Save(this.m_Server);
			this.m_output.WriteProgressSimple(Strings.DagTaskUpdatedAdDagMembership(this.m_ServerName, this.m_stampGroupName));
			DagTaskHelper.RevertDagServersDatabasesToStandalone(this.ConfigurationSession, this.m_output, this.m_Server);
		}

		// Token: 0x06004F52 RID: 20306 RVA: 0x0014A401 File Offset: 0x00148601
		private void DagTrace(string format, params object[] args)
		{
			this.m_output.AppendLogMessage(format, args);
		}

		// Token: 0x04002EFB RID: 12027
		private StampGroup m_stampGroup;

		// Token: 0x04002EFC RID: 12028
		private string m_stampGroupName;

		// Token: 0x04002EFD RID: 12029
		private bool m_configurationOnly;

		// Token: 0x04002EFE RID: 12030
		private Server m_Server;

		// Token: 0x04002EFF RID: 12031
		private string m_ServerName;

		// Token: 0x04002F00 RID: 12032
		private List<Server> m_serversInDag = new List<Server>(8);

		// Token: 0x04002F01 RID: 12033
		private HaTaskOutputHelper m_output;
	}
}
