using System;
using System.Management.Automation;
using Microsoft.Exchange.Cluster.ClusApi;
using Microsoft.Exchange.Cluster.Replay;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Monitoring;
using Microsoft.Exchange.Rpc.Cluster;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020008CC RID: 2252
	[Cmdlet("Update", "MailboxDatabaseCopy", SupportsShouldProcess = true, DefaultParameterSetName = "Identity", ConfirmImpact = ConfirmImpact.High)]
	public sealed class UpdateDatabaseCopy : DatabaseCopyActionTask
	{
		// Token: 0x170017DB RID: 6107
		// (get) Token: 0x06004FE1 RID: 20449 RVA: 0x0014E846 File Offset: 0x0014CA46
		// (set) Token: 0x06004FE2 RID: 20450 RVA: 0x0014E84E File Offset: 0x0014CA4E
		private bool IsServerLevel { get; set; }

		// Token: 0x170017DC RID: 6108
		// (get) Token: 0x06004FE3 RID: 20451 RVA: 0x0014E857 File Offset: 0x0014CA57
		// (set) Token: 0x06004FE4 RID: 20452 RVA: 0x0014E86E File Offset: 0x0014CA6E
		[Parameter(Mandatory = true, ParameterSetName = "Identity", ValueFromPipeline = true, ValueFromPipelineByPropertyName = true, Position = 0)]
		[Parameter(Mandatory = true, ParameterSetName = "CancelSeed", ValueFromPipeline = true, ValueFromPipelineByPropertyName = true, Position = 0)]
		public override DatabaseCopyIdParameter Identity
		{
			get
			{
				return (DatabaseCopyIdParameter)base.Fields["Identity"];
			}
			set
			{
				base.Fields["Identity"] = value;
			}
		}

		// Token: 0x170017DD RID: 6109
		// (get) Token: 0x06004FE5 RID: 20453 RVA: 0x0014E881 File Offset: 0x0014CA81
		// (set) Token: 0x06004FE6 RID: 20454 RVA: 0x0014E898 File Offset: 0x0014CA98
		[Parameter(Mandatory = true, ParameterSetName = "ExplicitServer", ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
		public MailboxServerIdParameter Server
		{
			get
			{
				return (MailboxServerIdParameter)base.Fields["Server"];
			}
			set
			{
				base.Fields["Server"] = value;
			}
		}

		// Token: 0x170017DE RID: 6110
		// (get) Token: 0x06004FE7 RID: 20455 RVA: 0x0014E8AB File Offset: 0x0014CAAB
		// (set) Token: 0x06004FE8 RID: 20456 RVA: 0x0014E8CD File Offset: 0x0014CACD
		[Parameter(Mandatory = false, ParameterSetName = "ExplicitServer")]
		public int MaximumSeedsInParallel
		{
			get
			{
				return (int)(base.Fields["MaximumSeedsInParallel"] ?? 10);
			}
			set
			{
				base.Fields["MaximumSeedsInParallel"] = value;
			}
		}

		// Token: 0x170017DF RID: 6111
		// (get) Token: 0x06004FE9 RID: 20457 RVA: 0x0014E8E5 File Offset: 0x0014CAE5
		// (set) Token: 0x06004FEA RID: 20458 RVA: 0x0014E90B File Offset: 0x0014CB0B
		[Parameter(Mandatory = true, ParameterSetName = "CancelSeed")]
		public SwitchParameter CancelSeed
		{
			get
			{
				return (SwitchParameter)(base.Fields["CancelSeed"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["CancelSeed"] = value;
			}
		}

		// Token: 0x170017E0 RID: 6112
		// (get) Token: 0x06004FEB RID: 20459 RVA: 0x0014E923 File Offset: 0x0014CB23
		// (set) Token: 0x06004FEC RID: 20460 RVA: 0x0014E949 File Offset: 0x0014CB49
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public SwitchParameter BeginSeed
		{
			get
			{
				return (SwitchParameter)(base.Fields["BeginSeed"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["BeginSeed"] = value;
			}
		}

		// Token: 0x170017E1 RID: 6113
		// (get) Token: 0x06004FED RID: 20461 RVA: 0x0014E961 File Offset: 0x0014CB61
		// (set) Token: 0x06004FEE RID: 20462 RVA: 0x0014E987 File Offset: 0x0014CB87
		[Parameter(Mandatory = false, ParameterSetName = "ExplicitServer")]
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public SwitchParameter DatabaseOnly
		{
			get
			{
				return (SwitchParameter)(base.Fields["DatabaseOnly"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["DatabaseOnly"] = value;
			}
		}

		// Token: 0x170017E2 RID: 6114
		// (get) Token: 0x06004FEF RID: 20463 RVA: 0x0014E99F File Offset: 0x0014CB9F
		// (set) Token: 0x06004FF0 RID: 20464 RVA: 0x0014E9C5 File Offset: 0x0014CBC5
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		[Parameter(Mandatory = false, ParameterSetName = "ExplicitServer")]
		public SwitchParameter CatalogOnly
		{
			get
			{
				return (SwitchParameter)(base.Fields["CatalogOnly"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["CatalogOnly"] = value;
			}
		}

		// Token: 0x170017E3 RID: 6115
		// (get) Token: 0x06004FF1 RID: 20465 RVA: 0x0014E9DD File Offset: 0x0014CBDD
		// (set) Token: 0x06004FF2 RID: 20466 RVA: 0x0014EA03 File Offset: 0x0014CC03
		[Parameter(Mandatory = false, ParameterSetName = "ExplicitServer")]
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public SwitchParameter ManualResume
		{
			get
			{
				return (SwitchParameter)(base.Fields["ManualResume"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["ManualResume"] = value;
			}
		}

		// Token: 0x170017E4 RID: 6116
		// (get) Token: 0x06004FF3 RID: 20467 RVA: 0x0014EA1B File Offset: 0x0014CC1B
		// (set) Token: 0x06004FF4 RID: 20468 RVA: 0x0014EA41 File Offset: 0x0014CC41
		[Parameter(Mandatory = false, ParameterSetName = "ExplicitServer")]
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public SwitchParameter DeleteExistingFiles
		{
			get
			{
				return (SwitchParameter)(base.Fields["DeleteExistingFiles"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["DeleteExistingFiles"] = value;
			}
		}

		// Token: 0x170017E5 RID: 6117
		// (get) Token: 0x06004FF5 RID: 20469 RVA: 0x0014EA59 File Offset: 0x0014CC59
		// (set) Token: 0x06004FF6 RID: 20470 RVA: 0x0014EA7F File Offset: 0x0014CC7F
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		[Parameter(Mandatory = false, ParameterSetName = "ExplicitServer")]
		public SwitchParameter SafeDeleteExistingFiles
		{
			get
			{
				return (SwitchParameter)(base.Fields["SafeDeleteExistingFiles"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["SafeDeleteExistingFiles"] = value;
			}
		}

		// Token: 0x170017E6 RID: 6118
		// (get) Token: 0x06004FF7 RID: 20471 RVA: 0x0014EA97 File Offset: 0x0014CC97
		// (set) Token: 0x06004FF8 RID: 20472 RVA: 0x0014EABD File Offset: 0x0014CCBD
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public SwitchParameter Force
		{
			get
			{
				return (SwitchParameter)(base.Fields["Force"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["Force"] = value;
			}
		}

		// Token: 0x170017E7 RID: 6119
		// (get) Token: 0x06004FF9 RID: 20473 RVA: 0x0014EAD5 File Offset: 0x0014CCD5
		// (set) Token: 0x06004FFA RID: 20474 RVA: 0x0014EAEC File Offset: 0x0014CCEC
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public DatabaseAvailabilityGroupNetworkIdParameter Network
		{
			get
			{
				return (DatabaseAvailabilityGroupNetworkIdParameter)base.Fields["Network"];
			}
			set
			{
				base.Fields["Network"] = value;
			}
		}

		// Token: 0x170017E8 RID: 6120
		// (get) Token: 0x06004FFB RID: 20475 RVA: 0x0014EAFF File Offset: 0x0014CCFF
		// (set) Token: 0x06004FFC RID: 20476 RVA: 0x0014EB20 File Offset: 0x0014CD20
		[Parameter(Mandatory = false, ParameterSetName = "ExplicitServer")]
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public UseDagDefaultOnOff NetworkCompressionOverride
		{
			get
			{
				return (UseDagDefaultOnOff)(base.Fields["NetworkCompressionOverride"] ?? UseDagDefaultOnOff.UseDagDefault);
			}
			set
			{
				base.Fields["NetworkCompressionOverride"] = value;
			}
		}

		// Token: 0x170017E9 RID: 6121
		// (get) Token: 0x06004FFD RID: 20477 RVA: 0x0014EB38 File Offset: 0x0014CD38
		// (set) Token: 0x06004FFE RID: 20478 RVA: 0x0014EB59 File Offset: 0x0014CD59
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		[Parameter(Mandatory = false, ParameterSetName = "ExplicitServer")]
		public UseDagDefaultOnOff NetworkEncryptionOverride
		{
			get
			{
				return (UseDagDefaultOnOff)(base.Fields["NetworkEncryptionOverride"] ?? UseDagDefaultOnOff.UseDagDefault);
			}
			set
			{
				base.Fields["NetworkEncryptionOverride"] = value;
			}
		}

		// Token: 0x170017EA RID: 6122
		// (get) Token: 0x06004FFF RID: 20479 RVA: 0x0014EB71 File Offset: 0x0014CD71
		// (set) Token: 0x06005000 RID: 20480 RVA: 0x0014EB88 File Offset: 0x0014CD88
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public ServerIdParameter SourceServer
		{
			get
			{
				return (ServerIdParameter)base.Fields["SourceServer"];
			}
			set
			{
				base.Fields["SourceServer"] = value;
			}
		}

		// Token: 0x170017EB RID: 6123
		// (get) Token: 0x06005001 RID: 20481 RVA: 0x0014EB9C File Offset: 0x0014CD9C
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				if (this.IsServerLevel)
				{
					return Strings.ConfirmationMessageUpdateDatabaseCopyServerBegin(this.m_server.Fqdn);
				}
				if (this.CancelSeed)
				{
					return Strings.ConfirmationMessageUpdateDatabaseCopyCancel(this.DataObject.Identity.ToString());
				}
				if (this.BeginSeed)
				{
					return Strings.ConfirmationMessageUpdateDatabaseCopyBegin(this.DataObject.Identity.ToString());
				}
				return Strings.ConfirmationMessageUpdateDatabaseCopy(this.DataObject.Identity.ToString());
			}
		}

		// Token: 0x06005002 RID: 20482 RVA: 0x0014EC1D File Offset: 0x0014CE1D
		protected override bool IsKnownException(Exception ex)
		{
			return AmExceptionHelper.IsKnownClusterException(this, ex) || ex is SeederServerException || ex is TaskServerException || base.IsKnownException(ex);
		}

		// Token: 0x06005003 RID: 20483 RVA: 0x0014EC44 File Offset: 0x0014CE44
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			this.CheckParameterCombination();
			if (this.IsServerLevel)
			{
				this.InternalValidateServerMode();
			}
			else
			{
				this.InternalValidateDatabaseMode();
			}
			base.VerifyIsWithinScopes((IConfigurationSession)base.DataSession, this.m_server, true, new DataAccessTask<DatabaseCopy>.ADObjectOutOfScopeString(Strings.ErrorServerOutOfScope));
			TaskLogger.LogExit();
		}

		// Token: 0x06005004 RID: 20484 RVA: 0x0014EC9C File Offset: 0x0014CE9C
		protected sealed override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			try
			{
				if (this.IsServerLevel)
				{
					this.InternalProcessServerMode();
				}
				else
				{
					this.InternalProcessDatabaseMode();
				}
			}
			finally
			{
				TaskLogger.LogExit();
			}
		}

		// Token: 0x06005005 RID: 20485 RVA: 0x0014ECDC File Offset: 0x0014CEDC
		private void PerformDatabaseSeed()
		{
			ExTraceGlobals.CmdletsTracer.TraceDebug<ObjectId>((long)this.GetHashCode(), "UpdateDatabaseCopy: PerformDatabaseSeed: {0}", this.DataObject.Identity);
			using (TaskSeeder taskSeeder = this.ConstructSeeder())
			{
				taskSeeder.SeedDatabase();
			}
		}

		// Token: 0x06005006 RID: 20486 RVA: 0x0014ED34 File Offset: 0x0014CF34
		private void CheckParameterCombination()
		{
			if (this.Server != null)
			{
				this.IsServerLevel = true;
			}
			this.m_seedDatabase = !this.CatalogOnly;
			this.m_seedCiFiles = !this.DatabaseOnly;
			if (this.CatalogOnly && this.DatabaseOnly)
			{
				base.WriteError(new UpdateDbcCatalogOnlyAndDatabaseOnlyAreMutuallyExclusiveException(), ErrorCategory.InvalidOperation, this.Identity);
			}
			if (this.DeleteExistingFiles && this.SafeDeleteExistingFiles)
			{
				base.WriteError(new UpdateDbcDeleteFilesInvalidParametersException(), ErrorCategory.InvalidArgument, this.Identity);
			}
		}

		// Token: 0x06005007 RID: 20487 RVA: 0x0014EDD0 File Offset: 0x0014CFD0
		private void InternalValidateDatabaseMode()
		{
			base.InternalValidate();
			if (base.HasErrors)
			{
				return;
			}
			this.m_database = this.DataObject.GetDatabase<Database>();
			this.m_dbCopyName = string.Format("{0}\\{1}", this.m_database.Name, this.m_server.Name);
			if (this.m_database.ReplicationType != ReplicationType.Remote)
			{
				base.WriteError(new InvalidRCROperationOnNonRcrDB(this.m_database.Name), ErrorCategory.InvalidOperation, this.Identity);
			}
			if (this.CancelSeed)
			{
				ExTraceGlobals.CmdletsTracer.TraceDebug<string>((long)this.GetHashCode(), "UpdateDatabaseCopy: CancelSeed called for database copy '{0}'", this.m_dbCopyName);
				base.WriteVerbose(Strings.SeederCancelCalled(this.m_dbCopyName));
			}
			if (this.SourceServer != null)
			{
				this.m_sourceServer = (Server)base.GetDataObject<Server>(this.SourceServer, base.DataSession, null, new LocalizedString?(Strings.ErrorServerNotFound(this.SourceServer.ToString())), new LocalizedString?(Strings.ErrorServerNotUnique(this.SourceServer.ToString())));
				if (!this.m_sourceServer.IsMailboxServer)
				{
					base.WriteError(new OperationOnlyOnMailboxServerException(this.m_sourceServer.Name), ErrorCategory.InvalidOperation, this.Identity);
				}
				if (this.m_sourceServer.MajorVersion != Microsoft.Exchange.Data.Directory.SystemConfiguration.Server.CurrentExchangeMajorVersion)
				{
					base.WriteError(new DagTaskErrorServerWrongVersion(this.m_sourceServer.Name), ErrorCategory.InvalidOperation, this.Identity);
				}
			}
			DatabaseAvailabilityGroup dagForDatabase = DagTaskHelper.GetDagForDatabase(this.m_database, base.DataSession, new Task.TaskErrorLoggingDelegate(base.WriteError));
			DagTaskHelper.PreventTaskWhenTPREnabled(dagForDatabase, this);
		}

		// Token: 0x06005008 RID: 20488 RVA: 0x0014EF5C File Offset: 0x0014D15C
		private void InternalValidateServerMode()
		{
			this.ResolveServerParameters();
			DatabaseTasksHelper.CheckServerObjectForCopyTask(this.Server, new Task.TaskErrorLoggingDelegate(base.WriteError), this.m_server);
			if (this.m_server.DatabaseAvailabilityGroup == null)
			{
				base.WriteError(new InvalidOperationException(Strings.ErrorSourceServerNotInDag(this.m_server.Name)), ErrorCategory.InvalidOperation, this.m_server);
			}
			DatabaseAvailabilityGroup databaseAvailabilityGroup = ((IConfigurationSession)base.DataSession).Read<DatabaseAvailabilityGroup>(this.m_server.DatabaseAvailabilityGroup);
			if (databaseAvailabilityGroup == null)
			{
				base.WriteError(new InconsistentADException(Strings.InconsistentServerNotInDag(this.m_server.Name, this.m_server.DatabaseAvailabilityGroup.ToString())), ErrorCategory.InvalidOperation, this.m_server);
			}
			DagTaskHelper.PreventTaskWhenTPREnabled(databaseAvailabilityGroup, this);
		}

		// Token: 0x06005009 RID: 20489 RVA: 0x0014F020 File Offset: 0x0014D220
		private void ResolveServerParameters()
		{
			this.m_server = (Server)base.GetDataObject<Server>(this.Server, base.DataSession, null, new LocalizedString?(Strings.ErrorMailboxServerNotFound(this.Server.ToString())), new LocalizedString?(Strings.ErrorMailboxServerNotUnique(this.Server.ToString())));
			if (this.m_server == null)
			{
				base.WriteError(new InvalidOperationException(Strings.ServerNotFound(this.Server.ToString())), ErrorCategory.InvalidData, this.Server.ToString());
			}
		}

		// Token: 0x0600500A RID: 20490 RVA: 0x0014F0AC File Offset: 0x0014D2AC
		private void InternalProcessServerMode()
		{
			ExTraceGlobals.CmdletsTracer.TraceFunction<string>((long)this.GetHashCode(), "UpdateDatabaseCopy: enter InternalProcessServerMode: {0}", this.m_server.Name);
			base.WriteVerbose(Strings.SeederServerLevelBeginCalled(this.m_server.Name));
			try
			{
				using (SeederClient seederClient = SeederClient.Create(this.m_server, null, null))
				{
					seederClient.BeginServerLevelSeed(this.DeleteExistingFiles, this.SafeDeleteExistingFiles, this.MaximumSeedsInParallel, false, this.ManualResume, this.m_seedDatabase, this.m_seedCiFiles, UpdateDatabaseCopy.UseDagDefaultOnOffToNullableBool(this.NetworkCompressionOverride), UpdateDatabaseCopy.UseDagDefaultOnOffToNullableBool(this.NetworkEncryptionOverride), SeederRpcFlags.None);
				}
			}
			catch (FullServerSeedInProgressException ex)
			{
				base.WriteWarning(ex.Message);
			}
			catch (SeederServerException exception)
			{
				base.WriteError(exception, ErrorCategory.InvalidOperation, this.m_server);
			}
			catch (SeederServerTransientException exception2)
			{
				base.WriteError(exception2, ErrorCategory.InvalidOperation, this.m_server);
			}
			ExTraceGlobals.CmdletsTracer.TraceFunction<string>((long)this.GetHashCode(), "UpdateDatabaseCopy: leave InternalProcessServerMode: {0}", this.m_server.Name);
		}

		// Token: 0x0600500B RID: 20491 RVA: 0x0014F1E4 File Offset: 0x0014D3E4
		private void InternalProcessDatabaseMode()
		{
			ExTraceGlobals.CmdletsTracer.TraceFunction<string>((long)this.GetHashCode(), "UpdateDatabaseCopy: enter InternalProcessDatabaseMode: {0}", this.m_dbCopyName);
			if (this.CancelSeed)
			{
				ExTraceGlobals.CmdletsTracer.TraceDebug<string>((long)this.GetHashCode(), "UpdateDatabaseCopy: Attempting to cancel the seed for '{0}'", this.m_dbCopyName);
				using (TaskSeeder taskSeeder = this.ConstructSeeder())
				{
					taskSeeder.CancelSeed();
					goto IL_9E;
				}
			}
			if (this.BeginSeed)
			{
				ExTraceGlobals.CmdletsTracer.TraceDebug<string>((long)this.GetHashCode(), "UpdateDatabaseCopy: BeginSeed called for database copy '{0}' to asynchronously start the seed.", this.m_dbCopyName);
				base.WriteVerbose(Strings.SeederAsyncBeginCalled(this.m_dbCopyName));
			}
			this.PerformDatabaseSeed();
			IL_9E:
			ExTraceGlobals.CmdletsTracer.TraceFunction<string>((long)this.GetHashCode(), "UpdateDatabaseCopy: leave InternalProcessDatabaseMode: {0}", this.m_dbCopyName);
		}

		// Token: 0x0600500C RID: 20492 RVA: 0x0014F2C4 File Offset: 0x0014D4C4
		private TaskSeeder ConstructSeeder()
		{
			return new TaskSeeder(SeedingTask.UpdateDatabaseCopy, this.m_server, this.m_database, this.m_sourceServer, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.TaskWarningLoggingDelegate(this.WriteWarning), new Task.TaskErrorLoggingDelegate(base.WriteError), new Task.TaskProgressLoggingDelegate(base.WriteProgress), new Task.TaskShouldContinueDelegate(base.ShouldContinue), () => base.Stopping)
			{
				BeginSeed = this.BeginSeed,
				AutoSuspend = false,
				NetworkId = this.Network,
				DeleteExistingFiles = this.DeleteExistingFiles,
				SafeDeleteExistingFiles = this.SafeDeleteExistingFiles,
				Force = this.Force,
				ManualResume = this.ManualResume,
				SeedCiFiles = this.m_seedCiFiles,
				SeedDatabaseFiles = this.m_seedDatabase,
				CompressOverride = UpdateDatabaseCopy.UseDagDefaultOnOffToNullableBool(this.NetworkCompressionOverride),
				EncryptOverride = UpdateDatabaseCopy.UseDagDefaultOnOffToNullableBool(this.NetworkEncryptionOverride)
			};
		}

		// Token: 0x0600500D RID: 20493 RVA: 0x0014F3D8 File Offset: 0x0014D5D8
		private static bool? UseDagDefaultOnOffToNullableBool(UseDagDefaultOnOff behaviour)
		{
			bool? result = null;
			switch (behaviour)
			{
			case UseDagDefaultOnOff.Off:
				result = new bool?(false);
				break;
			case UseDagDefaultOnOff.On:
				result = new bool?(true);
				break;
			}
			return result;
		}

		// Token: 0x04002F37 RID: 12087
		private const string CancelSeedParamSetName = "CancelSeed";

		// Token: 0x04002F38 RID: 12088
		private const string ServerParamSetName = "ExplicitServer";

		// Token: 0x04002F39 RID: 12089
		private const int DefaultMaximumSeedsInParallel = 10;

		// Token: 0x04002F3A RID: 12090
		private bool m_seedDatabase;

		// Token: 0x04002F3B RID: 12091
		private bool m_seedCiFiles;

		// Token: 0x04002F3C RID: 12092
		private Database m_database;

		// Token: 0x04002F3D RID: 12093
		private Server m_sourceServer;

		// Token: 0x04002F3E RID: 12094
		private string m_dbCopyName;
	}
}
