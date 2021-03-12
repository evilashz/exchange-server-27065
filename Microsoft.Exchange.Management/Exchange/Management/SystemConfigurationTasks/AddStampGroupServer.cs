using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Cluster.Replay;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000887 RID: 2183
	[Cmdlet("Add", "StampGroupServer", SupportsShouldProcess = true)]
	public sealed class AddStampGroupServer : SystemConfigurationObjectActionTask<StampGroupIdParameter, StampGroup>, IDisposable
	{
		// Token: 0x17001679 RID: 5753
		// (get) Token: 0x06004BC6 RID: 19398 RVA: 0x0013AF1F File Offset: 0x0013911F
		// (set) Token: 0x06004BC7 RID: 19399 RVA: 0x0013AF36 File Offset: 0x00139136
		[ValidateNotNullOrEmpty]
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

		// Token: 0x1700167A RID: 5754
		// (get) Token: 0x06004BC8 RID: 19400 RVA: 0x0013AF49 File Offset: 0x00139149
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageAddDatabaseAvailabilityGroupServer(this.m_ServerName, this.m_stampGroupName);
			}
		}

		// Token: 0x06004BC9 RID: 19401 RVA: 0x0013AF5C File Offset: 0x0013915C
		public AddStampGroupServer()
		{
			this.m_serversInStampGroup = new List<Server>(8);
			this.m_output = new HaTaskOutputHelper("add-stampgroupserver", new Task.TaskErrorLoggingDelegate(base.WriteError), new Task.TaskWarningLoggingDelegate(this.WriteWarning), new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.TaskProgressLoggingDelegate(base.WriteProgress), this.GetHashCode());
			this.m_output.CreateTempLogFile();
			this.m_output.AppendLogMessage("add-stampgroupserver started", new object[0]);
		}

		// Token: 0x06004BCA RID: 19402 RVA: 0x0013AFE3 File Offset: 0x001391E3
		protected override IConfigDataProvider CreateSession()
		{
			return DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(base.DomainController, false, ConsistencyMode.FullyConsistent, base.SessionSettings, 104, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\SystemConfigurationTasks\\Cluster\\AddStampGroupServer.cs");
		}

		// Token: 0x06004BCB RID: 19403 RVA: 0x0013B010 File Offset: 0x00139210
		private void ResolveParameters()
		{
			this.m_output.WriteProgressSimple(Strings.DagTaskValidatingParameters);
			this.m_stampGroup = StampGroupTaskHelper.StampGroupIdParameterToStampGroup(this.Identity, this.ConfigurationSession);
			this.m_stampGroupName = this.m_stampGroup.Name;
			this.m_Server = (Server)base.GetDataObject<Server>(this.Server, base.DataSession, null, new LocalizedString?(Strings.ErrorServerNotFound(this.Server.ToString())), new LocalizedString?(Strings.ErrorServerNotUnique(this.Server.ToString())));
			this.m_ServerName = this.m_Server.Name;
			this.m_output.AppendLogMessage("Server: value passed in = {0}, server.Name = {1}", new object[]
			{
				this.Server,
				this.m_ServerName
			});
			if (this.m_Server.MajorVersion != Microsoft.Exchange.Data.Directory.SystemConfiguration.Server.CurrentExchangeMajorVersion)
			{
				this.m_output.WriteErrorSimple(new DagTaskErrorServerWrongVersion(this.m_Server.Name));
			}
		}

		// Token: 0x06004BCC RID: 19404 RVA: 0x0013B108 File Offset: 0x00139308
		private void CheckServerStampGroupAdSettings()
		{
			if (this.m_stampGroup.Servers.Count >= 512)
			{
				this.m_output.WriteErrorSimple(new DagTaskErrorTooManyServers(this.m_stampGroupName, 512));
			}
			this.StampGroupTrace("Stamp group {0} has {1} servers:", new object[]
			{
				this.m_stampGroupName,
				this.m_stampGroup.Servers.Count
			});
			foreach (ADObjectId identity in this.m_stampGroup.Servers)
			{
				Server server = (Server)base.DataSession.Read<Server>(identity);
				this.StampGroupTrace("Stamp group {0} contains server {1}.", new object[]
				{
					this.m_stampGroupName,
					server.Name
				});
				this.m_serversInStampGroup.Add(server);
			}
		}

		// Token: 0x06004BCD RID: 19405 RVA: 0x0013B208 File Offset: 0x00139408
		private void LogCommandLineParameters()
		{
			string[] parametersToLog = new string[]
			{
				"Identity",
				"Server",
				"WhatIf"
			};
			DagTaskHelper.LogCommandLineParameters(this.m_output, base.MyInvocation.Line, parametersToLog, base.Fields);
		}

		// Token: 0x06004BCE RID: 19406 RVA: 0x0013B254 File Offset: 0x00139454
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			this.LogCommandLineParameters();
			this.ResolveParameters();
			base.VerifyIsWithinScopes((IConfigurationSession)base.DataSession, this.m_Server, true, new DataAccessTask<StampGroup>.ADObjectOutOfScopeString(Strings.ErrorServerOutOfScope));
			StampGroupTaskHelper.CheckServerDoesNotBelongToDifferentStampGroup(new Task.TaskErrorLoggingDelegate(this.m_output.WriteError), base.DataSession, this.m_Server, this.m_stampGroupName);
			this.CheckServerStampGroupAdSettings();
			base.InternalValidate();
			this.StampGroupTrace("InternalValidate() done.");
			TaskLogger.LogExit();
		}

		// Token: 0x06004BCF RID: 19407 RVA: 0x0013B2DC File Offset: 0x001394DC
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			this.m_output.WriteProgressSimple(ReplayStrings.DagTaskAddingServerToDag(this.m_ServerName, this.m_stampGroupName));
			this.m_output.WriteProgressSimple(Strings.DagTaskUpdatingAdDagMembership(this.m_ServerName, this.m_stampGroupName));
			base.InternalProcessRecord();
			this.UpdateAdSettings();
			this.m_output.WriteProgressSimple(Strings.DagTaskUpdatedAdDagMembership(this.m_ServerName, this.m_stampGroupName));
			this.m_output.WriteProgressSimple(Strings.DagTaskAddedServerToDag(this.m_ServerName, this.m_stampGroupName));
			TaskLogger.LogExit();
		}

		// Token: 0x06004BD0 RID: 19408 RVA: 0x0013B36F File Offset: 0x0013956F
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

		// Token: 0x06004BD1 RID: 19409 RVA: 0x0013B3A8 File Offset: 0x001395A8
		private void UpdateAdSettings()
		{
			this.m_Server.DatabaseAvailabilityGroup = (ADObjectId)this.m_stampGroup.Identity;
			base.DataSession.Save(this.m_Server);
			ExTraceGlobals.ClusterTracer.TraceDebug<string, string>((long)this.GetHashCode(), "PrepareDataObject() called on stampgroup={0} and server={1}.", this.m_stampGroupName, this.m_ServerName);
		}

		// Token: 0x06004BD2 RID: 19410 RVA: 0x0013B403 File Offset: 0x00139603
		private void StampGroupTrace(string format, params object[] args)
		{
			this.m_output.AppendLogMessage(format, args);
		}

		// Token: 0x06004BD3 RID: 19411 RVA: 0x0013B412 File Offset: 0x00139612
		private void StampGroupTrace(string message)
		{
			this.m_output.AppendLogMessage(message, new object[0]);
		}

		// Token: 0x04002D57 RID: 11607
		private const int MaxServersInStampGroup = 512;

		// Token: 0x04002D58 RID: 11608
		private StampGroup m_stampGroup;

		// Token: 0x04002D59 RID: 11609
		private string m_stampGroupName;

		// Token: 0x04002D5A RID: 11610
		private HaTaskOutputHelper m_output;

		// Token: 0x04002D5B RID: 11611
		private Server m_Server;

		// Token: 0x04002D5C RID: 11612
		private List<Server> m_serversInStampGroup;

		// Token: 0x04002D5D RID: 11613
		private string m_ServerName;
	}
}
