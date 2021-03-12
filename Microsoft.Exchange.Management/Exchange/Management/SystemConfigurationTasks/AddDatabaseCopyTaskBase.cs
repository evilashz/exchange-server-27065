using System;
using System.IO;
using System.Management.Automation;
using Microsoft.Exchange.Cluster.ClusApi;
using Microsoft.Exchange.Cluster.Replay;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.HA.DirectoryServices;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000978 RID: 2424
	public abstract class AddDatabaseCopyTaskBase<TDataObject> : DatabaseActionTaskBase<TDataObject> where TDataObject : Database, new()
	{
		// Token: 0x170019D3 RID: 6611
		// (get) Token: 0x06005690 RID: 22160 RVA: 0x00164170 File Offset: 0x00162370
		// (set) Token: 0x06005691 RID: 22161 RVA: 0x00164196 File Offset: 0x00162396
		[Parameter(Mandatory = false)]
		public SwitchParameter SeedingPostponed
		{
			get
			{
				return (SwitchParameter)(base.Fields["SeedingPostponed"] ?? false);
			}
			set
			{
				base.Fields["SeedingPostponed"] = value;
			}
		}

		// Token: 0x170019D4 RID: 6612
		// (get) Token: 0x06005692 RID: 22162 RVA: 0x001641AE File Offset: 0x001623AE
		// (set) Token: 0x06005693 RID: 22163 RVA: 0x001641D4 File Offset: 0x001623D4
		[Parameter(Mandatory = false)]
		public SwitchParameter ConfigurationOnly
		{
			get
			{
				return (SwitchParameter)(base.Fields["ConfigurationOnly"] ?? false);
			}
			set
			{
				base.Fields["ConfigurationOnly"] = value;
			}
		}

		// Token: 0x170019D5 RID: 6613
		// (get) Token: 0x06005694 RID: 22164 RVA: 0x001641EC File Offset: 0x001623EC
		// (set) Token: 0x06005695 RID: 22165 RVA: 0x00164203 File Offset: 0x00162403
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan ReplayLagTime
		{
			get
			{
				return (EnhancedTimeSpan)base.Fields["ReplayLagTime"];
			}
			set
			{
				base.Fields["ReplayLagTime"] = value;
			}
		}

		// Token: 0x170019D6 RID: 6614
		// (get) Token: 0x06005696 RID: 22166 RVA: 0x0016421B File Offset: 0x0016241B
		// (set) Token: 0x06005697 RID: 22167 RVA: 0x00164232 File Offset: 0x00162432
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan TruncationLagTime
		{
			get
			{
				return (EnhancedTimeSpan)base.Fields["TruncationLagTime"];
			}
			set
			{
				base.Fields["TruncationLagTime"] = value;
			}
		}

		// Token: 0x06005698 RID: 22168 RVA: 0x0016424A File Offset: 0x0016244A
		protected override bool IsKnownException(Exception ex)
		{
			return ex is WmiException || ex is ArgumentException || AmExceptionHelper.IsKnownClusterException(this, ex) || ex is SeederServerException || ex is TaskServerException || base.IsKnownException(ex);
		}

		// Token: 0x06005699 RID: 22169 RVA: 0x00164280 File Offset: 0x00162480
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			if (base.HasErrors)
			{
				TaskLogger.LogExit();
				return;
			}
			TDataObject dataObject = this.DataObject;
			if (!dataObject.IsExchange2009OrLater)
			{
				TDataObject dataObject2 = this.DataObject;
				base.WriteError(new ErrorDatabaseWrongVersion(dataObject2.Name), ErrorCategory.InvalidOperation, this.Identity);
			}
			TDataObject dataObject3 = this.DataObject;
			if (dataObject3.EdbFilePath == null)
			{
				base.WriteError(new InvalidOperationException(Strings.ErrorEdbFilePathMissed(this.Identity.ToString())), ErrorCategory.InvalidOperation, this.Identity);
			}
			TDataObject dataObject4 = this.DataObject;
			if (dataObject4.LogFolderPath == null)
			{
				base.WriteError(new InvalidOperationException(Strings.ErrorLogFolderPathMissed(this.Identity.ToString())), ErrorCategory.InvalidOperation, this.Identity);
			}
			TDataObject dataObject5 = this.DataObject;
			if (dataObject5.Recovery)
			{
				TDataObject dataObject6 = this.DataObject;
				Exception exception = new InvalidOperationException(Strings.ErrorInvalidOperationOnRecoveryMailboxDatabase(dataObject6.Name));
				ErrorCategory category = ErrorCategory.InvalidOperation;
				TDataObject dataObject7 = this.DataObject;
				base.WriteError(exception, category, dataObject7.Identity);
			}
			this.m_SeedingPostponedSpecified = (base.Fields["SeedingPostponed"] != null);
			if (!this.m_SeedingPostponedSpecified)
			{
				this.m_fSeeding = true;
			}
			else
			{
				this.m_fSeeding = !this.SeedingPostponed;
			}
			this.m_ConfigurationOnlySpecified = (base.Fields["ConfigurationOnly"] != null);
			if (!this.m_ConfigurationOnlySpecified)
			{
				this.m_fConfigOnly = false;
			}
			else
			{
				this.m_fConfigOnly = this.ConfigurationOnly;
				if (this.m_fConfigOnly)
				{
					this.m_fSeeding = false;
				}
			}
			if (base.Fields["ReplayLagTime"] != null)
			{
				this.WriteWarning(Strings.WarningReplayLagTimeMustBeLessThanSafetyNetHoldTime);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x0600569A RID: 22170 RVA: 0x00164470 File Offset: 0x00162670
		protected void InitializeLagTimes(DatabaseCopy preExistingCopy)
		{
			if (base.Fields["ReplayLagTime"] == null)
			{
				if (preExistingCopy == null)
				{
					this.m_replayLagTime = EnhancedTimeSpan.Parse("00:00:00");
				}
				else
				{
					this.m_replayLagTime = preExistingCopy.ReplayLagTime;
				}
			}
			else
			{
				this.m_replayLagTime = this.ReplayLagTime;
			}
			if (base.Fields["TruncationLagTime"] != null)
			{
				this.m_truncationLagTime = this.TruncationLagTime;
				return;
			}
			if (preExistingCopy == null)
			{
				this.m_truncationLagTime = EnhancedTimeSpan.Parse("00:00:00");
				return;
			}
			this.m_truncationLagTime = preExistingCopy.TruncationLagTime;
		}

		// Token: 0x0600569B RID: 22171 RVA: 0x00164500 File Offset: 0x00162700
		protected void CreateTargetEdbDirectory()
		{
			TDataObject dataObject = this.DataObject;
			string pathName = dataObject.EdbFilePath.PathName;
			try
			{
				base.WriteVerbose(Strings.VerboseCheckFileExistenceCondition(this.m_server.Fqdn, pathName));
				bool flag = false;
				string directoryName = Path.GetDirectoryName(pathName);
				TDataObject dataObject2 = this.DataObject;
				if (string.Compare(dataObject2.LogFolderPath.PathName, directoryName, StringComparison.InvariantCultureIgnoreCase) != 0)
				{
					TDataObject dataObject3 = this.DataObject;
					if (string.Compare(dataObject3.SystemFolderPath.PathName, directoryName, StringComparison.InvariantCultureIgnoreCase) != 0)
					{
						goto IL_82;
					}
				}
				flag = true;
				IL_82:
				if (!flag)
				{
					SystemConfigurationTasksHelper.TryCreateDirectory(this.m_server.Fqdn, directoryName, Database_Directory.GetDomainWidePermissions(), new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.TaskWarningLoggingDelegate(this.WriteWarning));
				}
			}
			catch (WmiException)
			{
				this.WriteWarning(Strings.FailedToGetCopyEdbFileStatus(this.m_server.Fqdn, pathName));
			}
		}

		// Token: 0x0600569C RID: 22172 RVA: 0x001645F0 File Offset: 0x001627F0
		protected void PerformSeedIfNecessary()
		{
			TDataObject dataObject = this.DataObject;
			IIdentityParameter id = new DatabaseIdParameter(dataObject.Id);
			IConfigDataProvider dataSession = base.DataSession;
			ObjectId rootId = this.RootId;
			TDataObject dataObject2 = this.DataObject;
			LocalizedString? notFoundError = new LocalizedString?(Strings.ErrorDatabaseNotFound(dataObject2.Name));
			TDataObject dataObject3 = this.DataObject;
			Database database = (Database)base.GetDataObject<Database>(id, dataSession, rootId, notFoundError, new LocalizedString?(Strings.ErrorDatabaseNotUnique(dataObject3.Name)));
			IADDatabaseAvailabilityGroup dag = null;
			if (this.m_server.DatabaseAvailabilityGroup != null)
			{
				DatabaseAvailabilityGroup dag2 = this.ConfigurationSession.Read<DatabaseAvailabilityGroup>(this.m_server.DatabaseAvailabilityGroup);
				dag = ADObjectWrapperFactory.CreateWrapper(dag2);
			}
			ReplayConfiguration config = RemoteReplayConfiguration.TaskGetReplayConfig(dag, ADObjectWrapperFactory.CreateWrapper(database), ADObjectWrapperFactory.CreateWrapper(this.m_server));
			if (this.m_fSeeding)
			{
				this.SeedDatabase(config);
			}
			this.SuspendDatabaseCopyIfNecessary(config);
		}

		// Token: 0x0600569D RID: 22173 RVA: 0x001646C8 File Offset: 0x001628C8
		internal void SuspendDatabaseCopyIfNecessary(ReplayConfiguration config)
		{
			string text = string.Empty;
			text = config.TargetMachine;
			if (!this.m_fConfigOnly && !WmiWrapper.IsFileExisting(text, config.DestinationEdbPath))
			{
				string fileName = string.Empty;
				fileName = Path.Combine(config.DestinationLogPath, EseHelper.MakeLogfileName(config.LogFilePrefix, "." + config.LogExtension, 1L));
				if (!WmiWrapper.IsFileExisting(SharedHelper.GetFqdnNameFromNode(config.SourceMachine), fileName))
				{
					try
					{
						this.WriteWarning(Strings.EnableDBCSuspendReplayNoDbComment(config.Name));
						ReplayRpcClientWrapper.RequestSuspend(text, config.IdentityGuid, Strings.EnableDBCSuspendReplayNoDbComment(config.Name));
						ReplayEventLogConstants.Tuple_DbSeedingRequired.LogEvent(null, new object[]
						{
							config.Name,
							text
						});
					}
					catch (TaskServerTransientException ex)
					{
						ExTraceGlobals.CmdletsTracer.TraceDebug<TaskServerTransientException>((long)this.GetHashCode(), "SeedDatabase: Caught exception in RPC: {0}", ex);
						base.WriteError(new InvalidOperationException(Strings.SgcFailedToSuspendRpc(config.Name, ex.Message)), ErrorCategory.InvalidOperation, this.Identity);
					}
					catch (TaskServerException ex2)
					{
						ExTraceGlobals.CmdletsTracer.TraceDebug<TaskServerException>((long)this.GetHashCode(), "SeedDatabase: Caught exception in RPC: {0}", ex2);
						if (!(ex2 is ReplayServiceSuspendWantedSetException))
						{
							if (ex2 is ReplayServiceSuspendRpcPartialSuccessCatalogFailedException)
							{
								base.WriteWarning(ex2.Message);
							}
							else
							{
								base.WriteError(new InvalidOperationException(Strings.SgcFailedToSuspendRpc(config.Name, ex2.Message)), ErrorCategory.InvalidOperation, this.Identity);
							}
						}
					}
				}
			}
		}

		// Token: 0x0600569E RID: 22174 RVA: 0x00164858 File Offset: 0x00162A58
		internal void SeedDatabase(ReplayConfiguration config)
		{
			ReplayState replayState = config.ReplayState;
			if (config is RemoteReplayConfiguration)
			{
				string targetMachine = config.TargetMachine;
				try
				{
					string machineFqdn = targetMachine;
					string destinationLogPath = config.DestinationLogPath;
					string destinationEdbPath = config.DestinationEdbPath;
					TDataObject dataObject = this.DataObject;
					AddDatabaseCopyTaskBase<TDataObject>.CheckSeedingPath(machineFqdn, destinationLogPath, destinationEdbPath, dataObject.LogFilePrefix);
				}
				catch (SeedingPathWarningException ex)
				{
					if (this.m_SeedingPostponedSpecified)
					{
						base.WriteWarning(ex.Message);
					}
					return;
				}
				catch (SeedingPathErrorException exception)
				{
					base.WriteError(exception, ErrorCategory.InvalidOperation, this.Identity);
				}
				SystemConfigurationTasksHelper.TryCreateDirectory(this.m_server.Fqdn, config.DestinationLogPath, Database_Directory.GetDomainWidePermissions(), new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.TaskWarningLoggingDelegate(this.WriteWarning));
				using (TaskSeeder taskSeeder = this.ConstructSeeder())
				{
					taskSeeder.SeedDatabase();
				}
				return;
			}
			throw new NotSupportedException(config.GetType() + " is not supported");
		}

		// Token: 0x0600569F RID: 22175 RVA: 0x00164968 File Offset: 0x00162B68
		private TaskSeeder ConstructSeeder()
		{
			return new TaskSeeder(SeedingTask.AddMailboxDatabaseCopy, this.m_server, this.DataObject, null, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.TaskWarningLoggingDelegate(this.WriteWarning), new Task.TaskErrorLoggingDelegate(base.WriteError), new Task.TaskProgressLoggingDelegate(base.WriteProgress), new Task.TaskShouldContinueDelegate(base.ShouldContinue), () => base.Stopping);
		}

		// Token: 0x060056A0 RID: 22176 RVA: 0x001649D8 File Offset: 0x00162BD8
		public static bool CheckSeedingPath(string machineFqdn, string logFolderPath, string edbFilePath, string logPrefix)
		{
			LocalLongFullPath.Parse(logFolderPath);
			LocalLongFullPath.Parse(edbFilePath);
			string directoryName = Path.GetDirectoryName(edbFilePath);
			string[] array = new string[]
			{
				logFolderPath,
				directoryName
			};
			string[] array2 = new string[]
			{
				"LogFolderPath",
				"EdbFolderPath"
			};
			for (int i = 0; i < array.Length; i++)
			{
				if (WmiWrapper.IsFileExisting(machineFqdn, array[i]))
				{
					throw new SeedingPathErrorException(Strings.SeedingErrorDirectoryIsFile(array2[i], array[i]));
				}
			}
			string text = Path.Combine(logFolderPath, logPrefix + ".log");
			if (WmiWrapper.IsDirectoryExisting(machineFqdn, text))
			{
				throw new SeedingPathErrorException(Strings.SeedingErrorFileIsDirectory("CopyLogFile", text));
			}
			if (WmiWrapper.IsDirectoryExisting(machineFqdn, edbFilePath))
			{
				throw new SeedingPathErrorException(Strings.SeedingErrorFileIsDirectory("CopyEdbFilePath", edbFilePath));
			}
			if (WmiWrapper.IsFileExisting(machineFqdn, edbFilePath))
			{
				throw new SeedingPathWarningException(Strings.SeedingEdbFileExists(edbFilePath));
			}
			if (WmiWrapper.IsFileExisting(machineFqdn, text))
			{
				throw new SeedingPathWarningException(Strings.SeedingLogFileExists(text));
			}
			return true;
		}

		// Token: 0x0400320D RID: 12813
		internal const string ReplayLagTimeName = "ReplayLagTime";

		// Token: 0x0400320E RID: 12814
		internal const string TruncationLagTimeName = "TruncationLagTime";

		// Token: 0x0400320F RID: 12815
		protected const string ParamNameSeedingPostponed = "SeedingPostponed";

		// Token: 0x04003210 RID: 12816
		protected const string ParamNameConfigurationOnly = "ConfigurationOnly";

		// Token: 0x04003211 RID: 12817
		protected const string DefaultReplayLagTimeStr = "00:00:00";

		// Token: 0x04003212 RID: 12818
		protected const string DefaultTruncationLagTimeStr = "00:00:00";

		// Token: 0x04003213 RID: 12819
		protected Server m_server;

		// Token: 0x04003214 RID: 12820
		protected Database[] m_ownerServerDatabases;

		// Token: 0x04003215 RID: 12821
		protected string m_progressMessage;

		// Token: 0x04003216 RID: 12822
		protected string m_taskName;

		// Token: 0x04003217 RID: 12823
		protected bool m_fConfigOnly;

		// Token: 0x04003218 RID: 12824
		protected bool m_fSeeding;

		// Token: 0x04003219 RID: 12825
		protected bool m_ConfigurationOnlySpecified;

		// Token: 0x0400321A RID: 12826
		protected bool m_SeedingPostponedSpecified;

		// Token: 0x0400321B RID: 12827
		protected EnhancedTimeSpan m_replayLagTime;

		// Token: 0x0400321C RID: 12828
		protected EnhancedTimeSpan m_truncationLagTime;
	}
}
