using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security;
using System.Threading;
using Microsoft.Exchange.Cluster.ActiveManagerServer;
using Microsoft.Exchange.Cluster.ClusApi;
using Microsoft.Exchange.Cluster.Replay.IO;
using Microsoft.Exchange.Cluster.Replay.MountPoint;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Cluster.ReplicaSeeder;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;
using Microsoft.Exchange.EseRepl;
using Microsoft.Exchange.Rpc.Cluster;
using Microsoft.Isam.Esent.Interop;
using Microsoft.Isam.Esent.Interop.Unpublished;
using Microsoft.Mapi;
using Microsoft.Win32;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020002A7 RID: 679
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class DatabaseSeederInstance : SeederInstanceBase, IReplicaSeederCallback, IIdentityGuid
	{
		// Token: 0x17000732 RID: 1842
		// (get) Token: 0x06001A68 RID: 6760 RVA: 0x0006F461 File Offset: 0x0006D661
		public static Trace Tracer
		{
			get
			{
				return ExTraceGlobals.SeederServerTracer;
			}
		}

		// Token: 0x06001A69 RID: 6761 RVA: 0x0006F468 File Offset: 0x0006D668
		internal static string BuildDivergenceCheckFileNamePattern(string logFilePrefix)
		{
			return string.Format("{0}*.{1}", logFilePrefix, "SeedDivergenceCheck");
		}

		// Token: 0x06001A6A RID: 6762 RVA: 0x0006F47C File Offset: 0x0006D67C
		internal static string BuildDivergenceCheckFileName(string edbFolder, string logFilePrefix, long maxReqGen)
		{
			string path = string.Format("{0}{1:X8}.{2}", logFilePrefix, maxReqGen, "SeedDivergenceCheck");
			return Path.Combine(edbFolder, path);
		}

		// Token: 0x06001A6B RID: 6763 RVA: 0x0006F4A8 File Offset: 0x0006D6A8
		internal static void DeleteDivergenceCheckFiles(string dbName, string edbFolder, string logFilePrefix)
		{
			string searchPattern = DatabaseSeederInstance.BuildDivergenceCheckFileNamePattern(logFilePrefix);
			foreach (string text in Directory.GetFiles(edbFolder, searchPattern))
			{
				DatabaseSeederInstance.Tracer.TraceDebug<string>(0L, "DeleteSeedDivergenceCheckingFiles({0})", text);
				File.Delete(text);
				ReplayCrimsonEvents.FileDeleted.Log<string, string, string>(dbName, Environment.MachineName, text);
			}
		}

		// Token: 0x06001A6C RID: 6764 RVA: 0x0006F500 File Offset: 0x0006D700
		internal static long GetDivergenceCheckGeneration(string edbFolder, string logFilePrefix)
		{
			long num = 0L;
			string searchPattern = DatabaseSeederInstance.BuildDivergenceCheckFileNamePattern(logFilePrefix);
			foreach (string text in Directory.GetFiles(edbFolder, searchPattern))
			{
				string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(text);
				string s = fileNameWithoutExtension.Substring(logFilePrefix.Length);
				long num2;
				if (long.TryParse(s, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out num2))
				{
					if (num2 > num)
					{
						num = num2;
					}
				}
				else
				{
					DatabaseSeederInstance.Tracer.TraceError<string>(0L, "GetDivergenceCheckGeneration failed on {0}", text);
				}
			}
			return num;
		}

		// Token: 0x06001A6D RID: 6765 RVA: 0x0006F580 File Offset: 0x0006D780
		public DatabaseSeederInstance(RpcSeederArgs rpcArgs, ConfigurationArgs configArgs, SeedCompletionCallback completionCallback, string targetNode = null) : base(rpcArgs, configArgs)
		{
			if (targetNode != null)
			{
				this.m_nodeBeingSeeded = AmServerName.GetSimpleName(targetNode);
			}
			this.m_completionCallback = (completionCallback ?? new SeedCompletionCallback(this.DefaultSeedCompletionCallback));
			this.InitializeDatabaseSeeder();
		}

		// Token: 0x17000733 RID: 1843
		// (get) Token: 0x06001A6E RID: 6766 RVA: 0x0006F5CE File Offset: 0x0006D7CE
		public override string Identity
		{
			get
			{
				return SafeInstanceTable<DatabaseSeederInstance>.GetIdentityFromGuid(this.SeederArgs.InstanceGuid);
			}
		}

		// Token: 0x17000734 RID: 1844
		// (get) Token: 0x06001A6F RID: 6767 RVA: 0x0006F5E0 File Offset: 0x0006D7E0
		private string TargetDatabaseFullFilePath
		{
			get
			{
				return Path.Combine(this.m_seedingPath, this.m_databaseFileName);
			}
		}

		// Token: 0x17000735 RID: 1845
		// (get) Token: 0x06001A70 RID: 6768 RVA: 0x0006F5F3 File Offset: 0x0006D7F3
		private string StagingDatabaseFullFilePath
		{
			get
			{
				return Path.Combine(this.m_tempSeedingPath, this.m_databaseFileName);
			}
		}

		// Token: 0x17000736 RID: 1846
		// (set) Token: 0x06001A71 RID: 6769 RVA: 0x0006F606 File Offset: 0x0006D806
		private int PerfmonSeedingProgress
		{
			set
			{
				if (base.SeederPerfmonInstance != null)
				{
					base.SeederPerfmonInstance.DbSeedingProgress.RawValue = (long)value;
				}
			}
		}

		// Token: 0x17000737 RID: 1847
		// (set) Token: 0x06001A72 RID: 6770 RVA: 0x0006F622 File Offset: 0x0006D822
		private long PerfmonKbytesRead
		{
			set
			{
				if (base.SeederPerfmonInstance != null)
				{
					base.SeederPerfmonInstance.DbSeedingBytesRead.RawValue = value;
					base.SeederPerfmonInstance.DbSeedingBytesReadPerSecond.RawValue = value;
				}
			}
		}

		// Token: 0x17000738 RID: 1848
		// (set) Token: 0x06001A73 RID: 6771 RVA: 0x0006F64E File Offset: 0x0006D84E
		private long PerfmonKbytesWritten
		{
			set
			{
				if (base.SeederPerfmonInstance != null)
				{
					base.SeederPerfmonInstance.DbSeedingBytesWritten.RawValue = value;
					base.SeederPerfmonInstance.DbSeedingBytesWrittenPerSecond.RawValue = value;
				}
			}
		}

		// Token: 0x06001A74 RID: 6772 RVA: 0x0006F67A File Offset: 0x0006D87A
		protected override void ResetPerfmonSeedingProgress()
		{
			this.PerfmonSeedingProgress = 0;
		}

		// Token: 0x06001A75 RID: 6773 RVA: 0x0006F684 File Offset: 0x0006D884
		public void PrepareDbSeed()
		{
			ExTraceGlobals.SeederServerTracer.TraceDebug<string, string, RpcSeederArgs>((long)this.GetHashCode(), "DatabaseSeederInstance: PrepareDbSeed() started for DB '{0}' ({1}) with arguments: {2}", this.ConfigArgs.Name, this.Identity, this.SeederArgs);
			ExTraceGlobals.SeederServerTracer.TraceDebug<string>((long)this.GetHashCode(), "DatabaseSeederInstance: PrepareDbSeed() Replica '{0}' called", this.ConfigArgs.Name);
			if (base.SetSeedingCallback != null)
			{
				base.SetSeedingCallback.CheckReseedBlocked();
			}
			base.CheckOperationCancelled();
			if (!string.IsNullOrEmpty(this.SeederArgs.NetworkId))
			{
				this.m_requestedNetwork = this.SeederArgs.NetworkId;
				lock (this)
				{
					this.m_seederStatus.AddressForData = this.m_requestedNetwork;
				}
			}
			base.CheckOperationCancelled();
			if (this.DoesTempDatabaseExist())
			{
				ExTraceGlobals.SeederServerTracer.TraceDebug<string, string>((long)this.GetHashCode(), "DatabaseSeederInstance: The temp database file exists, and will be deleted for db '{0}' ({1})", this.ConfigArgs.Name, this.Identity);
				this.DeleteTempTargetDatabase();
			}
			else
			{
				ExTraceGlobals.SeederServerTracer.TraceDebug<string, string>((long)this.GetHashCode(), "DatabaseSeederInstance: The temp database file does not exist for db '{0}' ({1})", this.ConfigArgs.Name, this.Identity);
			}
			base.CheckOperationCancelled();
			if (!this.m_fSkipSuspendCheck && base.SetSeedingCallback != null)
			{
				CopyStatusEnum status = base.SetSeedingCallback.GetStatus();
				if (status != CopyStatusEnum.Suspended && status != CopyStatusEnum.FailedAndSuspended)
				{
					base.LogError(ReplayStrings.DBCNotSuspendedYet(this.ConfigArgs.Name));
				}
			}
			ExTraceGlobals.FaultInjectionTracer.TraceTest(2540055869U);
			base.CheckOperationCancelled();
			this.CheckPreExistingFilesIfNecessary();
			base.CheckOperationCancelled();
			if (!this.m_fPassiveSeeding)
			{
				AmServerName serverName = new AmServerName(this.ConfigArgs.SourceMachine);
				if (!AmStoreHelper.IsMounted(serverName, this.ConfigArgs.IdentityGuid))
				{
					base.LogError(ReplayStrings.DatabaseNotMounted(this.ConfigArgs.Name));
				}
			}
			else
			{
				try
				{
					RpcDatabaseCopyStatus2[] copyStatus = Dependencies.ReplayRpcClientWrapper.GetCopyStatus(this.SeederArgs.SourceMachineName, RpcGetDatabaseCopyStatusFlags2.ReadThrough, new Guid[]
					{
						this.ConfigArgs.IdentityGuid
					});
					if (copyStatus.Length == 1)
					{
						if (copyStatus[0].CopyStatus != CopyStatusEnum.Healthy)
						{
							base.LogError(ReplayStrings.CopyStatusIsNotHealthy(this.SeederArgs.SourceMachineName, this.ConfigArgs.Name, copyStatus[0].CopyStatus.ToString()));
						}
					}
					else
					{
						base.LogError(new FailedToGetCopyStatusException(this.SeederArgs.SourceMachineName, this.ConfigArgs.Name));
					}
				}
				catch (TaskServerTransientException ex)
				{
					base.LogError(ex);
				}
				catch (TaskServerException ex2)
				{
					base.LogError(ex2);
				}
			}
			if (RegistryParameters.DisableEdbLogDirectoryCreation && !Directory.Exists(this.m_seedingPath))
			{
				base.LogError(ReplayStrings.SeederFailedToFindDirectory(this.m_seedingPath));
			}
			if (RegistryParameters.EnforceDbFolderUnderMountPoint)
			{
				DatabaseVolumeInfo instance = DatabaseVolumeInfo.GetInstance(this.ConfigArgs.DestinationEdbPath, this.ConfigArgs.DestinationLogPath, this.ConfigArgs.Name, this.ConfigArgs.AutoDagVolumesRootFolderPath, this.ConfigArgs.AutoDagDatabasesRootFolderPath, this.ConfigArgs.AutoDagDatabaseCopiesPerVolume);
				if (!instance.IsValid || instance.LastException != null)
				{
					ExTraceGlobals.SeederServerTracer.TraceError<string, string>((long)this.GetHashCode(), "DatabaseSeederInstance: Unable to get valid volumeinfo for DB '{0}'. Error '{1}'.", this.ConfigArgs.Name, AmExceptionHelper.GetExceptionMessageOrNoneString(instance.LastException));
					base.LogError(ReplayStrings.SeederFailedToFindValidVolumeInfo(this.ConfigArgs.Name, AmExceptionHelper.GetExceptionMessageOrNoneString(instance.LastException)));
				}
				if (!instance.IsDatabasePathOnMountedFolder || !instance.IsLogPathOnMountedFolder)
				{
					ExTraceGlobals.SeederServerTracer.TraceError<string>((long)this.GetHashCode(), "DatabaseSeederInstance: DB '{0}' folder are not under a mountpoint.", this.ConfigArgs.Name);
					base.LogError(ReplayStrings.SeederFailedToFindDirectoriesUnderMountPoint(this.m_seedingPath));
				}
				ExTraceGlobals.SeederServerTracer.TraceDebug<string>((long)this.GetHashCode(), "DatabaseSeederInstance: DB '{0}' has folders under a mountpoint", this.ConfigArgs.Name);
			}
			base.CreateDirectoryIfNecessary(this.m_seedingPath);
			base.CreateDirectoryIfNecessary(this.m_tempSeedingPath);
			base.CreateDirectoryIfNecessary(this.m_inspectorLogPath);
			this.UpdateReadHintSize();
			base.CheckOperationCancelled();
			SeederState seederState;
			SeederState seederState2;
			if (!this.UpdateState(SeederState.SeedPrepared, out seederState, out seederState2))
			{
				throw new SeederOperationAbortedException();
			}
		}

		// Token: 0x06001A76 RID: 6774 RVA: 0x0006FAD8 File Offset: 0x0006DCD8
		public void CancelDbSeed()
		{
			lock (this)
			{
				if (!this.m_fcancelled)
				{
					ReplayCrimsonEvents.SeedingErrorOnTarget.Log<Guid, string, string>(base.DatabaseGuid, base.DatabaseName, "CancelDbSeed invoked");
					SeederState seederState;
					SeederState seederState2;
					this.UpdateState(SeederState.SeedCancelled, out seederState, out seederState2);
					NetworkChannel channel = this.m_channel;
					if (channel != null)
					{
						channel.Abort();
					}
					this.m_fcancelled = true;
				}
			}
		}

		// Token: 0x06001A77 RID: 6775 RVA: 0x0006FB58 File Offset: 0x0006DD58
		public void ReportProgress(string edbName, long edbSize, long bytesRead, long bytesWritten)
		{
			RpcSeederStatus status = null;
			lock (this)
			{
				this.m_seederStatus.FileFullPath = edbName;
				this.m_seederStatus.BytesTotal = edbSize;
				this.m_seederStatus.BytesRead = bytesRead;
				this.m_seederStatus.BytesWritten = bytesWritten;
				ExTraceGlobals.SeederServerTracer.TraceDebug<int>((long)this.GetHashCode(), "DatabaseSeederInstance.UpdateProgress: Progress percentage = {0}%", this.m_seederStatus.PercentComplete);
				this.PerfmonSeedingProgress = this.m_seederStatus.PercentComplete;
				this.PerfmonKbytesRead = bytesRead / 1024L;
				this.PerfmonKbytesWritten = bytesWritten / 1024L;
				status = this.m_seederStatus;
			}
			this.TestHook(status);
		}

		// Token: 0x06001A78 RID: 6776 RVA: 0x0006FC20 File Offset: 0x0006DE20
		public bool IsBackupCancelled()
		{
			return this.m_fcancelled;
		}

		// Token: 0x06001A79 RID: 6777 RVA: 0x0006FC28 File Offset: 0x0006DE28
		internal static void WriteDatabaseStreamToFile(Guid databaseGuid, string databaseName, string sourceMachine, string stagingFileName, string targetFileName, string seedingPath, uint writeSize, NetworkChannel channel, IReplicaSeederCallback callback, Action deleteAction)
		{
			new FileInfo(stagingFileName);
			NetworkChannelMessage message = channel.GetMessage();
			SeedDatabaseFileReply seedDatabaseFileReply = message as SeedDatabaseFileReply;
			if (seedDatabaseFileReply == null)
			{
				channel.ThrowUnexpectedMessage(message);
			}
			long fileSize = seedDatabaseFileReply.FileSize;
			FileInfo fileInfo = new FileInfo(targetFileName);
			if (fileInfo.Exists)
			{
				long length = fileInfo.Length;
				ExTraceGlobals.SeederServerTracer.TraceDebug<string, long>(0L, "Final destination '{0}' exists. and is {1} bytes.", stagingFileName, length);
				if (!CReplicaSeederInterop.IsSpaceSufficient((ulong)fileSize, (ulong)length, seedingPath))
				{
					throw new SeederEcNotEnoughDiskException();
				}
				fileInfo.Delete();
			}
			if (deleteAction != null)
			{
				deleteAction();
			}
			seedDatabaseFileReply.DestinationFileName = stagingFileName;
			DateTime utcNow = DateTime.UtcNow;
			channel.ReceiveSeedingData(seedDatabaseFileReply, callback);
			TimeSpan timeSpan = DateTime.UtcNow - utcNow;
			long totalDecompressedBytesReceived = channel.PackagingLayer.TotalDecompressedBytesReceived;
			long totalCompressedBytesReceived = channel.PackagingLayer.TotalCompressedBytesReceived;
			double num = 1.0 - (double)totalCompressedBytesReceived / (double)totalDecompressedBytesReceived;
			double num2 = 0.0;
			if (timeSpan.TotalSeconds > 0.0)
			{
				num2 = (double)totalDecompressedBytesReceived / 1048576.0 / timeSpan.TotalSeconds;
			}
			CompressionConfig compressionConfig = channel.PackagingLayer.CompressionConfig;
			ReplayCrimsonEvents.DBSeedPerf.Log<string, Guid, TimeSpan, string, string, long, long, string, string>(databaseName, databaseGuid, timeSpan, string.Format("{0:F2} MB/sec", num2), string.Format("{0:F2} %", num * 100.0), totalDecompressedBytesReceived, totalCompressedBytesReceived, sourceMachine, (compressionConfig == null) ? "None" : compressionConfig.ToString());
		}

		// Token: 0x06001A7A RID: 6778 RVA: 0x0006FD98 File Offset: 0x0006DF98
		internal static void WriteAndInspectLogFiles(string inspectorDir, string destinationDir, string logfilePrefix, string logfileSuffix, NetworkChannel channel, ISetGeneration setGeneration)
		{
			NetworkChannelMessage message = channel.GetMessage();
			ExTraceGlobals.SeederServerTracer.TraceDebug<string>(0L, "WriteAndInspectLogFiles get message {0}", message.ToString());
			FileState fileState = new FileState();
			LogVerifier logVerifier = new LogVerifier(logfilePrefix);
			try
			{
				LogContinuityChecker continuityChecker = new LogContinuityChecker();
				while (!(message is NotifyEndOfLogReply))
				{
					CopyLogReply copyLogReply = message as CopyLogReply;
					if (message == null)
					{
						channel.ThrowUnexpectedMessage(message);
					}
					string text = Path.Combine(inspectorDir, EseHelper.MakeLogfileName(logfilePrefix, logfileSuffix, copyLogReply.ThisLogGeneration));
					string text2 = Path.Combine(destinationDir, EseHelper.MakeLogfileName(logfilePrefix, logfileSuffix, copyLogReply.ThisLogGeneration));
					CheckSummer summer = null;
					if (channel.ChecksumDataTransfer)
					{
						summer = new CheckSummer();
					}
					copyLogReply.ReceiveFile(text, null, summer);
					setGeneration.SetCopyGeneration(copyLogReply.ThisLogGeneration, copyLogReply.LastWriteUtc);
					LocalizedString value;
					if (!LogInspector.VerifyLogTask(copyLogReply.ThisLogGeneration, text, fileState, logVerifier, continuityChecker, out value))
					{
						DatabaseSeederInstance.DeleteLogFiles(inspectorDir, logfilePrefix, logfileSuffix);
						throw new SeederFailedToInspectLogException(text, value);
					}
					File.Move(text, text2);
					setGeneration.SetInspectGeneration(copyLogReply.ThisLogGeneration, copyLogReply.LastWriteUtc);
					ExTraceGlobals.SeederServerTracer.TraceError<string>(0L, "WriteAndInspectLogFiles : successfully get {0}", text2);
					message = channel.GetMessage();
				}
			}
			finally
			{
				logVerifier.Term();
			}
		}

		// Token: 0x06001A7B RID: 6779 RVA: 0x0006FEDC File Offset: 0x0006E0DC
		internal void CloseChannel()
		{
			if (this.m_channel != null)
			{
				this.m_channel.Close();
				this.m_channel = null;
			}
		}

		// Token: 0x06001A7C RID: 6780 RVA: 0x0006FEF8 File Offset: 0x0006E0F8
		internal void DefaultSeedCompletionCallback(bool successfulSeed)
		{
			ExTraceGlobals.SeederServerTracer.TraceDebug<string>((long)this.GetHashCode(), "DefaultSeedCompletionCallback: successfulSeed: {0}.", successfulSeed.ToString());
		}

		// Token: 0x06001A7D RID: 6781 RVA: 0x0006FF18 File Offset: 0x0006E118
		private void DeleteFilesAfterEDBWasDeleted()
		{
			DatabaseSeederInstance.DeleteLogFiles(this.ConfigArgs.InspectorLogPath, this.ConfigArgs.LogFilePrefix, this.ConfigArgs.LogFileSuffix);
			DatabaseSeederInstance.DeleteDivergenceCheckFiles(base.DatabaseName, this.m_seedingPath, this.ConfigArgs.LogFilePrefix);
			EseDatabasePatchFileIO.DeleteAll(this.ConfigArgs.DestinationEdbPath);
			if (this.DoesCheckpointFileExist())
			{
				this.DeleteCheckpointFile();
			}
			if (this.SeederArgs.DeleteExistingFiles || this.SeederArgs.SafeDeleteExistingFiles)
			{
				this.DeleteExistingLogs();
			}
		}

		// Token: 0x06001A7E RID: 6782 RVA: 0x0006FFA8 File Offset: 0x0006E1A8
		protected override void SeedThreadProcInternal()
		{
			bool flag = false;
			string stagingDatabaseFullFilePath = this.StagingDatabaseFullFilePath;
			string targetDatabaseFullFilePath = this.TargetDatabaseFullFilePath;
			string inspectorLogPath = this.m_inspectorLogPath;
			string destinationLogPath = this.ConfigArgs.DestinationLogPath;
			ExTraceGlobals.SeederServerTracer.TraceDebug<string>((long)this.GetHashCode(), "DatabaseSeederInstance.SeedThreadProcInternal(): Seeding started for DB '{0}'.", this.ConfigArgs.Name);
			ReplayCrimsonEvents.DBSeedingBegins.Log<Guid, string, string>(base.DatabaseGuid, base.DatabaseName, string.Empty);
			base.CheckOperationCancelled();
			if (this.SeederArgs.AutoSuspend && this.ConfigArgs.ReplicaInstanceManager != null)
			{
				ExTraceGlobals.SeederServerTracer.TraceDebug<string>((long)this.GetHashCode(), "DatabaseSeederInstance: Attempting to suspend database copy '{0}' because AutoSuspend=true", this.ConfigArgs.Name);
				SeedHelper.SuspendDatabaseCopy(this.SeederArgs.InstanceGuid, Environment.MachineName, ReplayStrings.InstanceSuspendedAutoInitialSeed(this.ConfigArgs.Name));
			}
			ExTraceGlobals.SeederServerTracer.TraceDebug<string>((long)this.GetHashCode(), "DatabaseSeederInstance: Changing replica state to 'Seeding' for DB '{0}'.", this.ConfigArgs.Name);
			if (base.SetSeedingCallback != null && !base.SetSeedingCallback.TryBeginDbSeed(this.SeederArgs))
			{
				this.m_setSeedingCallback = null;
				base.LogError(ReplayStrings.SeederCopyNotSuspended(this.ConfigArgs.Name));
			}
			ExTraceGlobals.FaultInjectionTracer.TraceTest(2405838141U);
			SeederEC seederEC = SeederEC.EcError;
			try
			{
				base.CheckOperationCancelled();
				if ((this.SeederArgs.Flags & SeederRpcFlags.SkipSettingReseedAutoReseedState) != SeederRpcFlags.SkipSettingReseedAutoReseedState)
				{
					Exception ex = AutoReseedWorkflowState.WriteManualWorkflowExecutionState(this.ConfigArgs.IdentityGuid, AutoReseedWorkflowType.ManualReseed);
					if (ex != null)
					{
						ExTraceGlobals.SeederServerTracer.TraceDebug<Exception>((long)this.GetHashCode(), "RegistryParameterException while writing AutoReseed registry state to mark that the reseed is starting: {0}", ex);
						base.LogError(ex);
					}
				}
				if (!this.m_testHookSeedDisableTruncationCoordination)
				{
					this.ManageLogTruncation();
				}
				else
				{
					ExTraceGlobals.SeederServerTracer.TraceDebug<string>((long)this.GetHashCode(), "DatabaseSeederInstance: Skipping global truncation coordination for DB '{0}' due to test hook.", this.ConfigArgs.Name);
				}
				try
				{
					this.GetChannel();
					if (!this.m_fPassiveSeeding)
					{
						SeedDatabaseFileRequest seedDatabaseFileRequest = new SeedDatabaseFileRequest(this.m_channel, this.SeederArgs.InstanceGuid, this.m_readHintSizeKB * 1024U);
						seedDatabaseFileRequest.Send();
						ExTraceGlobals.SeederServerTracer.TraceDebug<string>((long)this.GetHashCode(), "SeedDatabaseFileRequest sent for db {0}", this.ConfigArgs.Name);
					}
					else
					{
						ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.FullyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 1957, "SeedThreadProcInternal", "f:\\15.00.1497\\sources\\dev\\cluster\\src\\Replay\\seeder\\SeederInstance.cs");
						Server server = topologyConfigurationSession.FindServerByName(Environment.MachineName);
						PassiveSeedDatabaseFileRequest passiveSeedDatabaseFileRequest = new PassiveSeedDatabaseFileRequest(this.m_channel, this.SeederArgs.InstanceGuid, server.Guid);
						passiveSeedDatabaseFileRequest.Send();
						ExTraceGlobals.SeederServerTracer.TraceDebug<string>((long)this.GetHashCode(), "PassiveSeedDatabaseFileRequest sent", this.ConfigArgs.Name);
					}
					DatabaseSeederInstance.WriteDatabaseStreamToFile(base.DatabaseGuid, base.DatabaseName, this.ConfigArgs.SourceMachine, stagingDatabaseFullFilePath, targetDatabaseFullFilePath, this.m_seedingPath, this.m_readHintSizeKB * 1024U, this.m_channel, this, new Action(this.DeleteFilesAfterEDBWasDeleted));
					seederEC = SeederEC.EcSuccess;
				}
				catch (NetworkRemoteException ex2)
				{
					ExTraceGlobals.SeederServerTracer.TraceDebug<string, NetworkRemoteException>((long)this.GetHashCode(), "NetworkRemoteException while accessing stagingDatabaseFile {0}: {1}", stagingDatabaseFullFilePath, ex2);
					base.LogError(ex2);
				}
				catch (NetworkTransportException ex3)
				{
					ExTraceGlobals.SeederServerTracer.TraceDebug<string, NetworkTransportException>((long)this.GetHashCode(), "NetworkTransportException while accessing stagingDatabaseFile {0}: {1}", stagingDatabaseFullFilePath, ex3);
					base.LogError(ex3);
				}
				catch (IOException ex4)
				{
					ExTraceGlobals.SeederServerTracer.TraceDebug<string, IOException>((long)this.GetHashCode(), "IO Error while accessing stagingDatabaseFile {0}: {1}", stagingDatabaseFullFilePath, ex4);
					base.LogError(ex4);
				}
				catch (SecurityException ex5)
				{
					ExTraceGlobals.SeederServerTracer.TraceDebug<string, SecurityException>((long)this.GetHashCode(), "SecurityException while accessing stagingDatabaseFile {0}: {1}", stagingDatabaseFullFilePath, ex5);
					base.LogError(ex5);
				}
				catch (UnauthorizedAccessException ex6)
				{
					ExTraceGlobals.SeederServerTracer.TraceDebug<string, UnauthorizedAccessException>((long)this.GetHashCode(), "UnauthorizedAccessException while accessing stagingDatabaseFile {0}: {1}", stagingDatabaseFullFilePath, ex6);
					base.LogError(ex6);
				}
				catch (ADTransientException ex7)
				{
					ExTraceGlobals.SeederServerTracer.TraceDebug<ADTransientException>((long)this.GetHashCode(), "ADTransientException while trying to resolve local server", ex7);
					base.LogError(ex7);
				}
				catch (ADOperationException ex8)
				{
					ExTraceGlobals.SeederServerTracer.TraceDebug<ADOperationException>((long)this.GetHashCode(), "ADOperationException while trying to resolve local server", ex8);
					base.LogError(ex8);
				}
				if (seederEC == SeederEC.EcSuccess)
				{
					ExTraceGlobals.SeederServerTracer.TraceDebug((long)this.GetHashCode(), "DatabaseSeederInstance: SeedReplica() for DB '{0}' ({1}) at {2} succeeded. EC={3}", new object[]
					{
						this.ConfigArgs.Name,
						this.Identity,
						this.m_tempSeedingPath,
						seederEC
					});
				}
				else
				{
					ExTraceGlobals.SeederServerTracer.TraceError((long)this.GetHashCode(), "SeederInstance: SeedReplica() for DB '{0}' ({1}) at {2} failed. EC={3}", new object[]
					{
						this.ConfigArgs.Name,
						this.Identity,
						this.m_tempSeedingPath,
						seederEC
					});
					this.LogError((long)seederEC);
				}
				base.CheckOperationCancelled();
				this.DeployDatabase();
				ExTraceGlobals.FaultInjectionTracer.TraceTest(3085315389U);
				try
				{
					if (this.m_fPassiveSeeding)
					{
						SeedLogCopyRequest seedLogCopyRequest = new SeedLogCopyRequest(this.m_channel, this.SeederArgs.InstanceGuid);
						seedLogCopyRequest.Send();
						ExTraceGlobals.SeederServerTracer.TraceDebug<string>((long)this.GetHashCode(), "SeedLogCopyRequest sent", this.ConfigArgs.Name);
						DatabaseSeederInstance.WriteAndInspectLogFiles(inspectorLogPath, destinationLogPath, this.ConfigArgs.LogFilePrefix, this.ConfigArgs.LogFileSuffix, this.m_channel, base.SetGenerationCallback);
					}
					flag = true;
				}
				catch (NetworkRemoteException ex9)
				{
					ExTraceGlobals.SeederServerTracer.TraceDebug<NetworkRemoteException>((long)this.GetHashCode(), "NetworkRemoteException while receiving log files: {0}", ex9);
					base.LogError(ex9);
				}
				catch (NetworkTransportException ex10)
				{
					ExTraceGlobals.SeederServerTracer.TraceDebug<NetworkTransportException>((long)this.GetHashCode(), "NetworkTransportException while receiving log files: {0}", ex10);
					base.LogError(ex10);
				}
				if (this.m_setSeedingCallback != null)
				{
					this.m_setSeedingCallback.EndDbSeed();
				}
				this.CloseSeeding(flag);
				if (!this.m_fmanualResume)
				{
					ExTraceGlobals.SeederServerTracer.TraceDebug<string, string>((long)this.GetHashCode(), "DatabaseSeederInstance: Attempting to auto-resume replica for DB '{0}' {1}. (AutoSuspend=false)", this.ConfigArgs.Name, this.Identity);
					SeedHelper.TryResumeDatabaseCopy(this.SeederArgs.InstanceGuid, Environment.MachineName, true);
				}
			}
			catch (PagePatchApiFailedException ex11)
			{
				base.LogError(ex11);
			}
			catch (EsentErrorException ex12)
			{
				base.LogError(ex12);
			}
			catch (LogTruncationException ex13)
			{
				base.LogError(ex13);
			}
			catch (IOException ex14)
			{
				base.LogError(ex14);
			}
			catch (UnauthorizedAccessException ex15)
			{
				base.LogError(ex15);
			}
			finally
			{
				ReplayCrimsonEvents.DBSeedingEnds.Log<Guid, string, string>(base.DatabaseGuid, base.DatabaseName, string.Empty);
				this.CloseChannel();
			}
			this.m_completionCallback(flag);
		}

		// Token: 0x06001A7F RID: 6783 RVA: 0x00070758 File Offset: 0x0006E958
		protected override void CloseSeeding(bool wasSeedSuccessful)
		{
			ExTraceGlobals.SeederServerTracer.TraceDebug<string, string, bool>((long)this.GetHashCode(), "DatabaseSeederInstance.CloseSeeding() called for {0} ({1}), wasSeedSuccessful='{2}'", this.ConfigArgs.Name, this.Identity, wasSeedSuccessful);
			if (wasSeedSuccessful)
			{
				lock (this)
				{
					SeederState seederState;
					SeederState seederState2;
					this.UpdateState(SeederState.SeedSuccessful, out seederState, out seederState2);
					this.m_seederStatus.ErrorInfo = new RpcErrorExceptionInfo();
					this.m_lastErrorMessage = null;
					ReplayEventLogConstants.Tuple_SeedInstanceSuccess.LogEvent(null, new object[]
					{
						this.ConfigArgs.Name
					});
				}
			}
			if (this.m_tempSeedingPath != null)
			{
				try
				{
					ExTraceGlobals.SeederServerTracer.TraceDebug<string, string, string>((long)this.GetHashCode(), "DatabaseSeederInstance.CloseSeeding(): Attempting to delete seeding directory '{0}' for {1} ({2})", this.m_tempSeedingPath, this.ConfigArgs.Name, this.Identity);
					Directory.Delete(this.m_tempSeedingPath);
				}
				catch (ArgumentException ex)
				{
					ExTraceGlobals.SeederServerTracer.TraceError((long)this.GetHashCode(), "DatabaseSeederInstance.CloseSeeding() failed to cleanup {0} for {1} ({2}), reason: {3}", new object[]
					{
						this.m_tempSeedingPath,
						this.ConfigArgs.Name,
						this.Identity,
						ex
					});
				}
				catch (IOException ex2)
				{
					ExTraceGlobals.SeederServerTracer.TraceError((long)this.GetHashCode(), "DatabaseSeederInstance.CloseSeeding() failed to cleanup {0} for {1} ({2}), reason: {3}", new object[]
					{
						this.m_tempSeedingPath,
						this.ConfigArgs.Name,
						this.Identity,
						ex2
					});
				}
				catch (UnauthorizedAccessException ex3)
				{
					ExTraceGlobals.SeederServerTracer.TraceError((long)this.GetHashCode(), "DatabaseSeederInstance.CloseSeeding() failed to cleanup {0} for {1} ({2}), reason: {3}", new object[]
					{
						this.m_tempSeedingPath,
						this.ConfigArgs.Name,
						this.Identity,
						ex3
					});
				}
			}
			if (Directory.Exists(this.m_inspectorLogPath))
			{
				try
				{
					ExTraceGlobals.SeederServerTracer.TraceDebug<string, string, string>((long)this.GetHashCode(), "DatabaseSeederInstance.CloseSeeding(): Attempting to delete seeding directory '{0}' for {1} ({2})", this.m_inspectorLogPath, this.ConfigArgs.Name, this.Identity);
					Directory.Delete(this.m_inspectorLogPath);
				}
				catch (ArgumentException ex4)
				{
					ExTraceGlobals.SeederServerTracer.TraceError((long)this.GetHashCode(), "DatabaseSeederInstance.CloseSeeding() failed to cleanup {0} for {1} ({2}), reason: {3}", new object[]
					{
						this.m_inspectorLogPath,
						this.ConfigArgs.Name,
						this.Identity,
						ex4
					});
				}
				catch (IOException ex5)
				{
					ExTraceGlobals.SeederServerTracer.TraceError((long)this.GetHashCode(), "DatabaseSeederInstance.CloseSeeding() failed to cleanup {0} for {1} ({2}), reason: {3}", new object[]
					{
						this.m_tempSeedingPath,
						this.ConfigArgs.Name,
						this.Identity,
						ex5
					});
				}
				catch (UnauthorizedAccessException ex6)
				{
					ExTraceGlobals.SeederServerTracer.TraceError((long)this.GetHashCode(), "DatabaseSeederInstance.CloseSeeding() failed to cleanup {0} for {1} ({2}), reason: {3}", new object[]
					{
						this.m_tempSeedingPath,
						this.ConfigArgs.Name,
						this.Identity,
						ex6
					});
				}
			}
		}

		// Token: 0x06001A80 RID: 6784 RVA: 0x00070AB0 File Offset: 0x0006ECB0
		protected override void Cleanup()
		{
		}

		// Token: 0x06001A81 RID: 6785 RVA: 0x00070AB4 File Offset: 0x0006ECB4
		private static void DeleteLogFiles(string logFolderPath, string logfilePrefix, string logfileSuffix)
		{
			ExTraceGlobals.SeederServerTracer.TraceDebug<string>(0L, "DatabaseSeederInstance: DeleteLogFiles() attempting to delete the existing logs at '{0}'", logFolderPath);
			int num = 0;
			DatabaseSeederInstance.DeleteLogFiles(new DirectoryInfo(logFolderPath), logfilePrefix, logfileSuffix, out num);
		}

		// Token: 0x06001A82 RID: 6786 RVA: 0x00070AE8 File Offset: 0x0006ECE8
		private static bool DeleteLogFiles(DirectoryInfo di, string logfilePrefix, string logfileSuffix, out int logNum)
		{
			logNum = 0;
			if (di.Exists)
			{
				using (DirectoryEnumerator directoryEnumerator = new DirectoryEnumerator(di, false, false))
				{
					string filter = logfilePrefix + "*" + logfileSuffix;
					foreach (string path in directoryEnumerator.EnumerateFiles(filter, null))
					{
						File.Delete(path);
						logNum++;
					}
				}
			}
			if (logNum > 0)
			{
				ExTraceGlobals.SeederServerTracer.TraceDebug<string>(0L, "DatabaseSeederInstance: DeleteLogFiles() has deleted the existing logs at '{0}'", di.ToString());
				return true;
			}
			ExTraceGlobals.SeederServerTracer.TraceDebug<string>(0L, "DatabaseSeederInstance: DeleteLogFiles() did not delete any logs at '{0}' because no logs exist.", di.ToString());
			return false;
		}

		// Token: 0x06001A83 RID: 6787 RVA: 0x00070BAC File Offset: 0x0006EDAC
		private void LogError(long seederEC)
		{
			base.LogError(this.InterpretSeederEC(seederEC));
		}

		// Token: 0x06001A84 RID: 6788 RVA: 0x00070BBC File Offset: 0x0006EDBC
		private void GetChannel()
		{
			if (this.m_channel == null)
			{
				NetworkPath networkPath = NetworkManager.ChooseNetworkPath(this.SeederArgs.SourceMachineName ?? this.ConfigArgs.SourceMachine, this.SeederArgs.NetworkId, NetworkPath.ConnectionPurpose.Seeding);
				if (this.SeederArgs.CompressOverride != null || this.SeederArgs.EncryptOverride != null || !string.IsNullOrEmpty(this.SeederArgs.NetworkId))
				{
					networkPath.Compress = (this.SeederArgs.CompressOverride ?? networkPath.Compress);
					networkPath.Encrypt = (this.SeederArgs.EncryptOverride ?? networkPath.Encrypt);
					networkPath.NetworkChoiceIsMandatory = true;
				}
				this.m_channel = NetworkChannel.Connect(networkPath, TcpChannel.GetDefaultTimeoutInMs(), false);
			}
		}

		// Token: 0x06001A85 RID: 6789 RVA: 0x00070CAC File Offset: 0x0006EEAC
		private void InitializeDatabaseSeeder()
		{
			this.m_targetCheckpointFilePath = Path.Combine(this.ConfigArgs.DestinationSystemPath, EseHelper.MakeCheckpointFileName(this.ConfigArgs.LogFilePrefix));
			string destinationEdbPath = this.ConfigArgs.DestinationEdbPath;
			if (string.IsNullOrEmpty(destinationEdbPath))
			{
				base.LogError(ReplayStrings.DBCHasNoValidTargetEdbPath);
			}
			this.m_databaseFileName = Path.GetFileName(destinationEdbPath);
			this.m_seedingPath = (string.IsNullOrEmpty(this.SeederArgs.SeedingPath) ? Path.GetDirectoryName(destinationEdbPath) : this.SeederArgs.SeedingPath);
			this.m_fSkipSuspendCheck = this.SeederArgs.AutoSuspend;
			this.m_fmanualResume = (!this.SeederArgs.AutoSuspend && this.SeederArgs.ManualResume);
			this.m_tempSeedingPath = Path.Combine(this.m_seedingPath, "temp-seeding");
			this.m_inspectorLogPath = this.ConfigArgs.InspectorLogPath + "Seed";
			base.ReadSeedTestHook();
			ExTraceGlobals.SeederServerTracer.TraceDebug((long)this.GetHashCode(), "DatabaseSeederInstance.Initialize(): m_targetCheckpointFilePath=({0}), m_databaseFileName=({1}), m_seedingPath=({2}), m_tempSeedingPath=({3})m_inspectorLogPath=({4})", new object[]
			{
				this.m_targetCheckpointFilePath,
				this.m_databaseFileName,
				this.m_seedingPath,
				this.m_tempSeedingPath,
				this.m_inspectorLogPath
			});
		}

		// Token: 0x06001A86 RID: 6790 RVA: 0x00070DF0 File Offset: 0x0006EFF0
		private void ManageLogTruncation()
		{
			long num = -1L;
			long num2 = -1L;
			try
			{
				if (this.m_fPassiveSeeding)
				{
					num = 1L;
				}
				else
				{
					num = this.GetMinRequiredGenFromActive();
				}
				ADReplicationRetryTimer adreplicationRetryTimer = new ADReplicationRetryTimer();
				for (;;)
				{
					base.CheckOperationCancelled();
					try
					{
						num2 = LogTruncater.RequestGlobalTruncationCoordination(num, this.ConfigArgs.SourceMachine, this.m_nodeBeingSeeded, base.DatabaseGuid, this.ConfigArgs.LogFilePrefix, this.ConfigArgs.DestinationLogPath, this.ConfigArgs.CircularLoggingEnabled, null);
						DatabaseSeederInstance.Tracer.TraceDebug<long>((long)this.GetHashCode(), "ManageLogTruncation finds globalMin= {0}", num2);
					}
					catch (CopyUnknownToActiveLogTruncationException ex)
					{
						if (!adreplicationRetryTimer.IsExpired)
						{
							adreplicationRetryTimer.Sleep();
							continue;
						}
						throw ex;
					}
					break;
				}
			}
			finally
			{
				ReplayCrimsonEvents.SeedStartTruncation.Log<Guid, string, string, string>(base.DatabaseGuid, base.DatabaseName, LogCopier.FormatLogGeneration(num), LogCopier.FormatLogGeneration(num2));
			}
		}

		// Token: 0x06001A87 RID: 6791 RVA: 0x00070ED4 File Offset: 0x0006F0D4
		private long GetMinRequiredGenFromActive()
		{
			Exception ex = null;
			string sourceMachine = this.ConfigArgs.SourceMachine;
			JET_DBINFOMISC jet_DBINFOMISC = new JET_DBINFOMISC();
			try
			{
				using (IStoreRpc newStoreControllerInstance = Dependencies.GetNewStoreControllerInstance(sourceMachine))
				{
					newStoreControllerInstance.GetDatabaseInformation(base.DatabaseGuid, out jet_DBINFOMISC);
				}
			}
			catch (MapiPermanentException ex2)
			{
				ex = ex2;
			}
			catch (MapiRetryableException ex3)
			{
				ex = ex3;
			}
			if (ex != null)
			{
				DatabaseSeederInstance.Tracer.TraceError<Exception>((long)this.GetHashCode(), "GetMinRequiredGenFromActive failed: {0}", ex);
				base.LogError(ex);
			}
			else
			{
				DatabaseSeederInstance.Tracer.TraceDebug<int>((long)this.GetHashCode(), "GetMinRequiredGenFromActive returns loggen {0}", jet_DBINFOMISC.genMinRequired);
			}
			return (long)jet_DBINFOMISC.genMinRequired;
		}

		// Token: 0x06001A88 RID: 6792 RVA: 0x00070F94 File Offset: 0x0006F194
		private bool DoesTempDatabaseExist()
		{
			return this.DoesFileExist(Path.Combine(this.m_tempSeedingPath, this.m_databaseFileName));
		}

		// Token: 0x06001A89 RID: 6793 RVA: 0x00070FAD File Offset: 0x0006F1AD
		private bool DoesTargetDatabaseExist()
		{
			return this.DoesFileExist(this.TargetDatabaseFullFilePath);
		}

		// Token: 0x06001A8A RID: 6794 RVA: 0x00070FBC File Offset: 0x0006F1BC
		private void DeleteTempTargetDatabase()
		{
			string text = Path.Combine(this.m_tempSeedingPath, this.m_databaseFileName);
			string error;
			if (!this.DeleteFile(text, out error))
			{
				base.LogError(ReplayStrings.FailedToDeleteTempDatabase(text, error));
			}
		}

		// Token: 0x06001A8B RID: 6795 RVA: 0x00070FF8 File Offset: 0x0006F1F8
		private void DeleteExistingLogs()
		{
			try
			{
				ExTraceGlobals.SeederServerTracer.TraceDebug<string, string, string>((long)this.GetHashCode(), "DatabaseSeederInstance: DeleteExistingLogs attempting to delete the existing logs at '{0}' for DB '{1}' ({2})", this.ConfigArgs.DestinationLogPath, this.ConfigArgs.Name, this.Identity);
				int num = 0;
				if (DatabaseSeederInstance.DeleteLogFiles(new DirectoryInfo(this.ConfigArgs.DestinationLogPath), this.ConfigArgs.LogFilePrefix, this.ConfigArgs.LogFileSuffix, out num))
				{
					ReplayEventLogConstants.Tuple_SeedInstanceDeletedExistingLogs.LogEvent(null, new object[]
					{
						this.ConfigArgs.Name,
						num,
						this.ConfigArgs.DestinationLogPath
					});
				}
				AgedOutDirectoryHelper.DeleteSkippedLogs(this.ConfigArgs.IgnoredLogsPath, this.ConfigArgs.Name, true);
				Exception ex;
				LastLogReplacer.CleanupTemporaryFiles(this.ConfigArgs.LogFilePrefix, this.ConfigArgs.DestinationLogPath, out ex);
				if (ex != null)
				{
					base.LogError(ReplayStrings.SeederFailedToDeleteLogs(this.ConfigArgs.DestinationLogPath, ex.ToString()));
				}
			}
			catch (IOException ex2)
			{
				base.LogError(ReplayStrings.SeederFailedToDeleteLogs(this.ConfigArgs.DestinationLogPath, ex2.ToString()));
			}
			catch (UnauthorizedAccessException ex3)
			{
				base.LogError(ReplayStrings.SeederFailedToDeleteLogs(this.ConfigArgs.DestinationLogPath, ex3.ToString()));
			}
			catch (SecurityException ex4)
			{
				base.LogError(ReplayStrings.SeederFailedToDeleteLogs(this.ConfigArgs.DestinationLogPath, ex4.ToString()));
			}
			catch (ArgumentException ex5)
			{
				base.LogError(ReplayStrings.SeederFailedToDeleteLogs(this.ConfigArgs.DestinationLogPath, ex5.ToString()));
			}
		}

		// Token: 0x06001A8C RID: 6796 RVA: 0x00071200 File Offset: 0x0006F400
		private bool DoesFileExist(string fileFullPath)
		{
			bool result;
			try
			{
				result = File.Exists(fileFullPath);
			}
			catch (IOException ex)
			{
				ExTraceGlobals.SeederServerTracer.TraceError((long)this.GetHashCode(), "DatabaseSeederInstance: DoesFileExist() got exception when looking for '{0}' for DB {1} ({2}). Exception: {3}", new object[]
				{
					fileFullPath,
					this.ConfigArgs.Name,
					this.Identity,
					ex
				});
				result = false;
			}
			return result;
		}

		// Token: 0x06001A8D RID: 6797 RVA: 0x0007126C File Offset: 0x0006F46C
		private bool DoesCheckpointFileExist()
		{
			return this.DoesFileExist(this.m_targetCheckpointFilePath);
		}

		// Token: 0x06001A8E RID: 6798 RVA: 0x0007127C File Offset: 0x0006F47C
		private bool DeleteFile(string fileFullPath, out string errorMessage)
		{
			ExTraceGlobals.SeederServerTracer.TraceDebug<string, string, string>((long)this.GetHashCode(), "DatabaseSeederInstance: DeleteFile attempting to delete '{0}' for DB '{1}' ({2})", fileFullPath, this.ConfigArgs.Name, this.Identity);
			try
			{
				errorMessage = null;
				File.Delete(fileFullPath);
				ExTraceGlobals.SeederServerTracer.TraceDebug<string, string, string>((long)this.GetHashCode(), "DatabaseSeederInstance: DeleteFile deleted '{0}' for DB '{1}' ({2})", fileFullPath, this.ConfigArgs.Name, this.Identity);
				return true;
			}
			catch (IOException ex)
			{
				errorMessage = ex.Message;
			}
			catch (UnauthorizedAccessException ex2)
			{
				errorMessage = ex2.Message;
			}
			return false;
		}

		// Token: 0x06001A8F RID: 6799 RVA: 0x0007131C File Offset: 0x0006F51C
		private void DeleteCheckpointFile()
		{
			string error;
			if (!this.DeleteFile(this.m_targetCheckpointFilePath, out error))
			{
				base.LogError(ReplayStrings.SeederFailedToDeleteCheckpoint(this.m_targetCheckpointFilePath, error));
				return;
			}
			ReplayEventLogConstants.Tuple_SeedInstanceDeletedCheckpointFile.LogEvent(null, new object[]
			{
				this.ConfigArgs.Name,
				this.m_targetCheckpointFilePath
			});
		}

		// Token: 0x06001A90 RID: 6800 RVA: 0x0007137C File Offset: 0x0006F57C
		private bool LogsExist()
		{
			Exception ex = null;
			try
			{
				using (DirectoryEnumerator directoryEnumerator = new DirectoryEnumerator(new DirectoryInfo(this.ConfigArgs.DestinationLogPath), false, false))
				{
					string filter = this.ConfigArgs.LogFilePrefix + "*" + this.ConfigArgs.LogFileSuffix;
					using (IEnumerator<string> enumerator = directoryEnumerator.EnumerateFiles(filter, null).GetEnumerator())
					{
						if (enumerator.MoveNext())
						{
							string text = enumerator.Current;
							return true;
						}
					}
				}
				if (LastLogReplacer.AreTemporaryFilesPresent(this.ConfigArgs.LogFilePrefix, this.ConfigArgs.DestinationLogPath, out ex))
				{
					return true;
				}
			}
			catch (IOException arg)
			{
				ExTraceGlobals.SeederServerTracer.TraceError<IOException, string, string>((long)this.GetHashCode(), "DatabaseSeederInstance.LogsExist got IOException {0} for DB '{1}' ({2})", arg, this.ConfigArgs.Name, this.Identity);
			}
			catch (UnauthorizedAccessException ex2)
			{
				ex = ex2;
			}
			catch (SecurityException ex3)
			{
				ex = ex3;
			}
			catch (ArgumentException ex4)
			{
				ex = ex4;
			}
			if (ex != null)
			{
				ExTraceGlobals.SeederServerTracer.TraceError<Exception, string, string>((long)this.GetHashCode(), "DatabaseSeederInstance.LogsExist got fatal exception {0} for DB '{1}' ({2})", ex, this.ConfigArgs.Name, this.Identity);
				base.LogError(ex);
			}
			return false;
		}

		// Token: 0x06001A91 RID: 6801 RVA: 0x000714F0 File Offset: 0x0006F6F0
		private void DeployDatabase()
		{
			ExTraceGlobals.SeederServerTracer.TraceDebug<string, string>((long)this.GetHashCode(), "DatabaseSeederInstance: DeployDatabase() for DB '{0}' ({1})", this.ConfigArgs.Name, this.Identity);
			string stagingDatabaseFullFilePath = this.StagingDatabaseFullFilePath;
			string targetDatabaseFullFilePath = this.TargetDatabaseFullFilePath;
			ExTraceGlobals.SeederServerTracer.TraceDebug((long)this.GetHashCode(), "DatabaseSeederInstance: DeployDatabase() starting to move database from '{0}' to '{1}' for '{2}' ({3})", new object[]
			{
				stagingDatabaseFullFilePath,
				targetDatabaseFullFilePath,
				this.ConfigArgs.Name,
				this.Identity
			});
			try
			{
				JET_DBUTIL dbutil = new JET_DBUTIL
				{
					op = DBUTIL_OP.UpdateDBHeaderFromTrailer,
					szDatabase = stagingDatabaseFullFilePath
				};
				UnpublishedApi.JetDBUtilities(dbutil);
				this.FetchSeededDbInfo();
				if (!this.m_fPassiveSeeding)
				{
					this.PreventCopyingFromDivergentPath();
				}
				File.Move(stagingDatabaseFullFilePath, targetDatabaseFullFilePath);
				ExTraceGlobals.SeederServerTracer.TraceDebug((long)this.GetHashCode(), "DatabaseSeederInstance: DeployDatabase() moved database from '{0}' to '{1}' for '{2}' ({3})", new object[]
				{
					stagingDatabaseFullFilePath,
					targetDatabaseFullFilePath,
					this.ConfigArgs.Name,
					this.Identity
				});
			}
			catch (IOException ex)
			{
				base.LogError(ReplayStrings.SeederFailedToDeployDatabase(stagingDatabaseFullFilePath, targetDatabaseFullFilePath, ex.ToString()));
			}
			catch (UnauthorizedAccessException ex2)
			{
				base.LogError(ReplayStrings.SeederFailedToDeployDatabase(stagingDatabaseFullFilePath, targetDatabaseFullFilePath, ex2.ToString()));
			}
		}

		// Token: 0x06001A92 RID: 6802 RVA: 0x00071648 File Offset: 0x0006F848
		private void FetchSeededDbInfo()
		{
			string stagingDatabaseFullFilePath = this.StagingDatabaseFullFilePath;
			try
			{
				Api.JetGetDatabaseFileInfo(stagingDatabaseFullFilePath, out this.seededDbInfo, JET_DbInfo.Misc);
			}
			catch (EsentErrorException ex)
			{
				DatabaseSeederInstance.Tracer.TraceError<EsentErrorException>((long)this.GetHashCode(), "FetchSeededDbInfo failed: {0}", ex);
				base.LogError(ex);
			}
		}

		// Token: 0x06001A93 RID: 6803 RVA: 0x000716A0 File Offset: 0x0006F8A0
		private void PreventCopyingFromDivergentPath()
		{
			Exception ex = null;
			DatabaseSeederInstance.DeleteDivergenceCheckFiles(base.DatabaseName, this.m_seedingPath, this.ConfigArgs.LogFilePrefix);
			long num = (long)this.seededDbInfo.genMaxRequired;
			DatabaseSeederInstance.Tracer.TraceDebug<string>((long)this.GetHashCode(), "PreventCopyingFromDivergentPath: need gen {0}", LogCopier.FormatLogGeneration(num));
			string text = DatabaseSeederInstance.BuildDivergenceCheckFileName(this.m_seedingPath, this.ConfigArgs.LogFilePrefix, num);
			try
			{
				LogCopyClient.CopyLog(base.DatabaseGuid, this.m_channel, num, text);
			}
			catch (NetworkRemoteException ex2)
			{
				ex = ex2;
			}
			catch (NetworkTransportException ex3)
			{
				ex = ex3;
			}
			if (ex == null)
			{
				ex = LogVerifier.Verify(text, this.ConfigArgs.LogFilePrefix, num, new JET_SIGNATURE?(this.seededDbInfo.signLog));
			}
			if (ex != null)
			{
				DatabaseSeederInstance.Tracer.TraceDebug<Exception>((long)this.GetHashCode(), "PreventCopyingFromDivergentPath failed: {0}", ex);
				ReplayCrimsonEvents.SeedDivergenceFailed.Log<Guid, string, string, Exception>(base.DatabaseGuid, base.DatabaseName, text, ex);
				string targetCopyName = string.Format("{0}\\{1}", base.DatabaseName, this.m_nodeBeingSeeded);
				base.LogError(new SeedDivergenceFailedException(targetCopyName, text, ex.Message, ex));
			}
		}

		// Token: 0x06001A94 RID: 6804 RVA: 0x000717D0 File Offset: 0x0006F9D0
		private void CheckPreExistingFilesIfNecessary()
		{
			LocalizedString empty = LocalizedString.Empty;
			if (this.SeederArgs.DeleteExistingFiles || this.SeederArgs.SafeDeleteExistingFiles)
			{
				if (this.SeederArgs.SafeDeleteExistingFiles)
				{
					DatabaseSeederInstance.Tracer.TraceDebug<string, string>((long)this.GetHashCode(), "DatabaseSeederInstance: SafeDeleteExistingFiles was specified for DB '{0}' ({1}). Checking the existing database files.", this.ConfigArgs.Name, this.Identity);
					if (!this.DoesTargetDatabaseExist())
					{
						DatabaseSeederInstance.Tracer.TraceDebug<string, string>((long)this.GetHashCode(), "DatabaseSeederInstance: No EDB file exists for DB '{0}' ({1}). Skipping one-copy check.", this.ConfigArgs.Name, this.Identity);
						return;
					}
					DatabaseSeederInstance.Tracer.TraceDebug<string, string, LocalizedString>((long)this.GetHashCode(), "DatabaseSeederInstance: Need to run one-copy check because pre-existing files (logs, edb or chk) were found for DB '{0}' ({1}). Error: {2}", this.ConfigArgs.Name, this.Identity, empty);
					Exception ex = null;
					try
					{
						DatabaseHealthValidationRunner databaseHealthValidationRunner = new DatabaseHealthValidationRunner(AmServerName.LocalComputerName.NetbiosName);
						databaseHealthValidationRunner.Initialize();
						IHealthValidationResultMinimal healthValidationResultMinimal = databaseHealthValidationRunner.RunDatabaseRedundancyChecksForCopyRemoval(true, new Guid?(base.DatabaseGuid), true, true, -1).FirstOrDefault<IHealthValidationResultMinimal>();
						if (healthValidationResultMinimal == null)
						{
							DatabaseSeederInstance.Tracer.TraceError<string, string>((long)this.GetHashCode(), "DatabaseSeederInstance: One-copy redundancy check for DB '{0}' ({1}) failed because no validation result was returned. The database may have been removed from AD or AD lookup failed.", this.ConfigArgs.Name, this.Identity);
							base.LogError(new SafeDeleteExistingFilesDataRedundancyNoResultException(base.DatabaseName));
						}
						if (healthValidationResultMinimal.IsValidationSuccessful)
						{
							DatabaseSeederInstance.Tracer.TraceDebug<string, string, int>((long)this.GetHashCode(), "DatabaseSeederInstance: One-copy redundancy check for DB '{0}' ({1}) passed. Redundancy count: {2}. Database files will be deleted as part of reseed.", this.ConfigArgs.Name, this.Identity, healthValidationResultMinimal.HealthyCopiesCount);
							return;
						}
						DatabaseSeederInstance.Tracer.TraceError<string, string, string>((long)this.GetHashCode(), "DatabaseSeederInstance: One-copy redundancy check for DB '{0}' ({1}) failed. Validation error: {2}", this.ConfigArgs.Name, this.Identity, healthValidationResultMinimal.ErrorMessageWithoutFullStatus);
						base.LogError(new SafeDeleteExistingFilesDataRedundancyException(base.DatabaseName, healthValidationResultMinimal.ErrorMessageWithoutFullStatus));
					}
					catch (MonitoringADConfigException ex2)
					{
						ex = ex2;
					}
					catch (AmServerException ex3)
					{
						ex = ex3;
					}
					catch (AmServerTransientException ex4)
					{
						ex = ex4;
					}
					if (ex != null)
					{
						base.LogError(new SafeDeleteExistingFilesDataRedundancyException(base.DatabaseName, ex.Message, ex));
					}
				}
				return;
			}
			DatabaseSeederInstance.Tracer.TraceDebug<string, string>((long)this.GetHashCode(), "DatabaseSeederInstance: Checking the existing database files for DB '{0}' ({1})", this.ConfigArgs.Name, this.Identity);
			if (this.DoDatabaseFilesAlreadyExist(out empty))
			{
				base.LogError(empty);
				return;
			}
			DatabaseSeederInstance.Tracer.TraceDebug<string, string>((long)this.GetHashCode(), "DatabaseSeederInstance: No database files exist for DB '{0}' ({1})", this.ConfigArgs.Name, this.Identity);
		}

		// Token: 0x06001A95 RID: 6805 RVA: 0x00071A38 File Offset: 0x0006FC38
		private bool DoDatabaseFilesAlreadyExist(out LocalizedString error)
		{
			bool result = true;
			error = LocalizedString.Empty;
			if (this.LogsExist())
			{
				error = ReplayStrings.SeederEcLogAlreadyExist(this.ConfigArgs.DestinationLogPath);
			}
			else if (this.DoesTargetDatabaseExist())
			{
				error = ReplayStrings.TargetDBAlreadyExists(this.TargetDatabaseFullFilePath);
			}
			else if (this.DoesCheckpointFileExist())
			{
				error = ReplayStrings.TargetChkptAlreadyExists(this.m_targetCheckpointFilePath);
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06001A96 RID: 6806 RVA: 0x00071AAC File Offset: 0x0006FCAC
		private string InterpretSeederEC(long ec)
		{
			SeederEC seederEC = (SeederEC)ec;
			string result;
			switch (seederEC)
			{
			case SeederEC.EcDirDoesNotExist:
				result = ReplayStrings.SeederEcDirDoesNotExist(this.m_tempSeedingPath);
				break;
			case SeederEC.EcLogAlreadyExist:
				result = ReplayStrings.SeederEcLogAlreadyExist(this.ConfigArgs.DestinationLogPath);
				break;
			case SeederEC.EcJtxAlreadyExist:
				result = ReplayStrings.SeederEcJtxAlreadyExist(this.ConfigArgs.DestinationLogPath);
				break;
			default:
				switch (seederEC)
				{
				case SeederEC.EcTestAborted:
					result = "Seeding aborted by test hook.";
					break;
				case SeederEC.EcTargetDbFileInUse:
					result = ReplayStrings.SeederEcTargetDbFileInUse(this.TargetDatabaseFullFilePath);
					break;
				default:
					result = SeedHelper.TranslateSeederErrorCode(ec, AmServerName.GetSimpleName(this.ConfigArgs.SourceMachine));
					break;
				}
				break;
			}
			return result;
		}

		// Token: 0x06001A97 RID: 6807 RVA: 0x00071B5C File Offset: 0x0006FD5C
		private void UpdateReadHintSize()
		{
			RegistryKey registryKey = null;
			uint num = 1024U;
			bool flag = false;
			try
			{
				registryKey = Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Replay\\Parameters");
				ExTraceGlobals.SeederServerTracer.TraceDebug<string>((long)this.GetHashCode(), "DatabaseSeederInstance: Created local RegistryKey, key = {0}", "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Replay\\Parameters");
			}
			catch (SecurityException)
			{
				registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Replay\\Parameters", RegistryKeyPermissionCheck.ReadSubTree);
				flag = true;
				ExTraceGlobals.SeederServerTracer.TraceError<string>((long)this.GetHashCode(), "DatabaseSeederInstance: Opened read-only local RegistryKey, key = {0}", "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Replay\\Parameters");
			}
			catch (UnauthorizedAccessException)
			{
				registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Replay\\Parameters", RegistryKeyPermissionCheck.ReadSubTree);
				flag = true;
				ExTraceGlobals.SeederServerTracer.TraceError<string>((long)this.GetHashCode(), "DatabaseSeederInstance: Opened read-only local RegistryKey, key = {0}", "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Replay\\Parameters");
			}
			if (registryKey != null)
			{
				ExTraceGlobals.SeederServerTracer.TraceDebug<string>((long)this.GetHashCode(), "DatabaseSeederInstance: Registry key '{0}' exists", "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Replay\\Parameters");
				bool flag2 = false;
				object value = registryKey.GetValue("ESE Read Hint Size (KB)");
				if (value == null)
				{
					num = 1024U;
					flag2 = true;
					ExTraceGlobals.SeederServerTracer.TraceDebug<string, string, uint>((long)this.GetHashCode(), "DatabaseSeederInstance: Registry key '{0}' does not exist in registry path '{1}'. The default ReadHintSizeKB is set to {2}", "ESE Read Hint Size (KB)", "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Replay\\Parameters", num);
				}
				else
				{
					num = (uint)((int)value);
					ExTraceGlobals.SeederServerTracer.TraceDebug<uint, string, string>((long)this.GetHashCode(), "DatabaseSeederInstance: Read registry value={0}, registry key: {1}, key name: {2}", num, "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Replay\\Parameters", "ESE Read Hint Size (KB)");
				}
				if (num < 96U)
				{
					num = 96U;
					flag2 = true;
				}
				if (num > 204800U)
				{
					num = 204800U;
					flag2 = true;
				}
				if (flag2 && !flag)
				{
					registryKey.SetValue("ESE Read Hint Size (KB)", num, RegistryValueKind.DWord);
					ExTraceGlobals.SeederServerTracer.TraceDebug<uint, string, string>((long)this.GetHashCode(), "DatabaseSeederInstance: Updated registry value={0}, register key: {1}, key name: {2}", num, "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Replay\\Parameters", "ESE Read Hint Size (KB)");
				}
				registryKey.Close();
			}
			else
			{
				ExTraceGlobals.SeederServerTracer.TraceError<string>((long)this.GetHashCode(), "DatabaseSeederInstance: Failed to create or open registry key: {0}", "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Replay\\Parameters");
			}
			this.m_readHintSizeKB = num;
			ExTraceGlobals.SeederServerTracer.TraceDebug<uint, string, string>((long)this.GetHashCode(), "DatabaseSeederInstance: Updated ESE cbReadHintSize = {0} for seeding {1} ({2})", num, this.ConfigArgs.Name, this.Identity);
		}

		// Token: 0x06001A98 RID: 6808 RVA: 0x00071D4C File Offset: 0x0006FF4C
		private void TestHook(RpcSeederStatus status)
		{
			if (this.m_testHookSeedFailAfterProgressPercent > 0 && status.PercentComplete >= this.m_testHookSeedFailAfterProgressPercent)
			{
				this.LogError(15L);
			}
			if (this.m_testHookSeedDelayPerCallback > 0)
			{
				Thread.Sleep(this.m_testHookSeedDelayPerCallback);
			}
		}

		// Token: 0x04000A8F RID: 2703
		internal const string SeedDivergenceCheckFileSuffix = "SeedDivergenceCheck";

		// Token: 0x04000A90 RID: 2704
		internal const string SeedingTestAbortedString = "Seeding aborted by test hook.";

		// Token: 0x04000A91 RID: 2705
		private const string TempSeedingFolderName = "temp-seeding";

		// Token: 0x04000A92 RID: 2706
		private const string ReadHintSizeRegKey = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Replay\\Parameters";

		// Token: 0x04000A93 RID: 2707
		private const string ReadHintSizeKeyName = "ESE Read Hint Size (KB)";

		// Token: 0x04000A94 RID: 2708
		private const uint MinReadHintSizeKB = 96U;

		// Token: 0x04000A95 RID: 2709
		private const uint MaxReadHintSizeKB = 204800U;

		// Token: 0x04000A96 RID: 2710
		private const uint OptimizedReadHintSizeKB = 1024U;

		// Token: 0x04000A97 RID: 2711
		private readonly SeedCompletionCallback m_completionCallback;

		// Token: 0x04000A98 RID: 2712
		private string m_requestedNetwork;

		// Token: 0x04000A99 RID: 2713
		private string m_targetCheckpointFilePath;

		// Token: 0x04000A9A RID: 2714
		private string m_seedingPath;

		// Token: 0x04000A9B RID: 2715
		private string m_databaseFileName;

		// Token: 0x04000A9C RID: 2716
		private string m_tempSeedingPath;

		// Token: 0x04000A9D RID: 2717
		private string m_inspectorLogPath;

		// Token: 0x04000A9E RID: 2718
		private bool m_fSkipSuspendCheck;

		// Token: 0x04000A9F RID: 2719
		private bool m_fmanualResume;

		// Token: 0x04000AA0 RID: 2720
		private uint m_readHintSizeKB;

		// Token: 0x04000AA1 RID: 2721
		private NetworkChannel m_channel;

		// Token: 0x04000AA2 RID: 2722
		private readonly string m_nodeBeingSeeded = Environment.MachineName;

		// Token: 0x04000AA3 RID: 2723
		private JET_DBINFOMISC seededDbInfo;
	}
}
