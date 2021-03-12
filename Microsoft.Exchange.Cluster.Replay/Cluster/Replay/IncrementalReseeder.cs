using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Security;
using System.Threading;
using System.Transactions;
using Microsoft.Exchange.Cluster.ActiveManagerServer;
using Microsoft.Exchange.Cluster.ClusApi;
using Microsoft.Exchange.Cluster.Replay.MountPoint;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Common.HA;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.HA;
using Microsoft.Exchange.Data.HA.DirectoryServices;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;
using Microsoft.Exchange.EseRepl;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.HA.FailureItem;
using Microsoft.Isam.Esent.Interop;
using Microsoft.Isam.Esent.Interop.Unpublished;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020002D6 RID: 726
	internal class IncrementalReseeder
	{
		// Token: 0x170007B8 RID: 1976
		// (get) Token: 0x06001C9E RID: 7326 RVA: 0x0007DD60 File Offset: 0x0007BF60
		public static Trace Tracer
		{
			get
			{
				return ExTraceGlobals.IncrementalReseederTracer;
			}
		}

		// Token: 0x06001C9F RID: 7327 RVA: 0x0007DD68 File Offset: 0x0007BF68
		public IncrementalReseeder(IPerfmonCounters perfmonCounters, IncReseedPerformanceTracker perfTracker, IReplayConfiguration config, FileState fileState, ISetBroken setBroken, ISetDisconnected setDisconnected, ISetViable setViable, NetworkPath netPath, bool runningAcll, ManualOneShotEvent shuttingDownEvent)
		{
			if (config == null)
			{
				throw new ArgumentNullException("config");
			}
			if (fileState == null)
			{
				throw new ArgumentNullException("fileState");
			}
			if (setBroken == null)
			{
				throw new ArgumentNullException("setBroken");
			}
			this.m_config = config;
			this.m_fileState = fileState;
			this.m_setBroken = setBroken;
			this.m_setDisconnected = setDisconnected;
			this.m_perfmonCounters = perfmonCounters;
			this.m_perfTracker = perfTracker;
			this.m_setViable = setViable;
			this.m_runningAcll = runningAcll;
			this.m_shuttingDownEvent = shuttingDownEvent;
			if (this.m_config.DatabaseName == null)
			{
				throw new IncSeedConfigNotSupportedException("DatabaseName");
			}
			if (this.m_config.DestinationEdbPath == null)
			{
				throw new IncSeedConfigNotSupportedException("DestinationEdbPath");
			}
			this.m_destEdbFile = this.m_config.DestinationEdbPath;
			this.m_srcEdbFile = this.m_config.SourceEdbPath;
			this.m_srcLogPath = this.m_config.SourceLogPath;
			this.m_destLogPath = this.m_config.DestinationLogPath;
			this.m_destDbPath = this.m_config.DestinationEdbPath;
			this.m_tempLogPath = IncrementalReseeder.GetTempLogPath(this.m_config);
			this.m_destSystemPath = this.m_config.DestinationSystemPath;
			this.m_tempBackupPath = IncrementalReseeder.GetTempBackupPath(this.m_config);
			this.m_tempInspectPath = IncrementalReseeder.GetTempInspectPath(this.m_config);
			this.m_e00LogBackupPath = this.m_config.E00LogBackupPath;
			this.m_dbName = this.m_config.DatabaseName;
			this.m_srcNode = AmServerName.GetSimpleName(this.m_config.SourceMachine ?? Environment.MachineName);
			this.m_logSuffix = "." + this.m_config.LogExtension;
			this.m_databaseVolumeInfo = DatabaseVolumeInfo.GetInstance(this.m_config);
			this.m_logSource = LogSource.Construct(config, perfmonCounters, netPath, RegistryParameters.LogShipACLLTimeoutInMsec);
			this.m_perfTracker.IsRunningACLL = this.m_runningAcll;
			this.m_perfTracker.SourceServer = this.m_srcNode;
		}

		// Token: 0x06001CA0 RID: 7328 RVA: 0x0007DF8E File Offset: 0x0007C18E
		public static string GetTempLogPath(IReplayConfiguration config)
		{
			if (config == null)
			{
				throw new ArgumentNullException("config");
			}
			return Path.Combine(config.DestinationLogPath, "incseed");
		}

		// Token: 0x06001CA1 RID: 7329 RVA: 0x0007DFAE File Offset: 0x0007C1AE
		public static string GetTempBackupPath(IReplayConfiguration config)
		{
			if (config == null)
			{
				throw new ArgumentNullException("config");
			}
			return Path.Combine(config.DestinationLogPath, "incseedBackup");
		}

		// Token: 0x06001CA2 RID: 7330 RVA: 0x0007DFCE File Offset: 0x0007C1CE
		public static string GetTempInspectPath(IReplayConfiguration config)
		{
			if (config == null)
			{
				throw new ArgumentNullException("config");
			}
			return Path.Combine(config.DestinationLogPath, "incseedInspect");
		}

		// Token: 0x06001CA3 RID: 7331 RVA: 0x0007DFEE File Offset: 0x0007C1EE
		public static string GetTempReseedPath(IReplayConfiguration config)
		{
			if (config == null)
			{
				throw new ArgumentNullException("config");
			}
			return Path.Combine(config.DestinationEdbPath, "incseed");
		}

		// Token: 0x06001CA4 RID: 7332 RVA: 0x0007E010 File Offset: 0x0007C210
		public static bool IsFileBinaryEqual(string filename1, string filename2)
		{
			FileInfo fileInfo = new FileInfo(filename1);
			FileInfo fileInfo2 = new FileInfo(filename2);
			if (fileInfo.Length != fileInfo2.Length)
			{
				return false;
			}
			bool result;
			using (BinaryReader binaryReader = new BinaryReader(File.OpenRead(filename1)))
			{
				using (BinaryReader binaryReader2 = new BinaryReader(File.OpenRead(filename2)))
				{
					for (;;)
					{
						byte[] array = binaryReader.ReadBytes(65536);
						byte[] array2 = binaryReader2.ReadBytes(65536);
						if (array.Length != array2.Length)
						{
							break;
						}
						if (array.Length == 0)
						{
							goto IL_8B;
						}
						for (int i = 0; i < array.Length; i++)
						{
							if (array[i] != array2[i])
							{
								goto Block_7;
							}
						}
					}
					return false;
					Block_7:
					return false;
					IL_8B:
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06001CA5 RID: 7333 RVA: 0x0007E0E0 File Offset: 0x0007C2E0
		public static Exception CleanupDirectory(string directory)
		{
			return IncrementalReseeder.CleanupDirectory(directory, true);
		}

		// Token: 0x06001CA6 RID: 7334 RVA: 0x0007E0EC File Offset: 0x0007C2EC
		public static Exception CleanupDirectory(string directory, bool deleteDirectory)
		{
			Exception ex = null;
			if (!Directory.Exists(directory))
			{
				return null;
			}
			foreach (string fileFullPath in Directory.GetFiles(directory))
			{
				ex = (FileCleanup.Delete(fileFullPath) ?? ex);
			}
			if (deleteDirectory)
			{
				try
				{
					Directory.Delete(directory);
				}
				catch (IOException ex2)
				{
					ex = ex2;
				}
				catch (SecurityException ex3)
				{
					ex = ex3;
				}
				catch (UnauthorizedAccessException ex4)
				{
					ex = ex4;
				}
			}
			return ex;
		}

		// Token: 0x06001CA7 RID: 7335 RVA: 0x0007E178 File Offset: 0x0007C378
		public static bool CleanupFiles(IReplayConfiguration config, bool fThrow, bool fPublishFailureItem)
		{
			ExTraceGlobals.IncrementalReseederTracer.TraceDebug(0L, "Enter CleanupFiles");
			Exception ex = null;
			string destinationEdbPath = config.DestinationEdbPath;
			try
			{
				EseDatabasePatchFileIO.DeleteAll(destinationEdbPath);
			}
			catch (PagePatchApiFailedException ex2)
			{
				throw new IncrementalReseedRetryableException(ex2.Message, ex2);
			}
			ex = (IncrementalReseeder.TryCleanUpDir(config, IncrementalReseeder.GetTempBackupPath(config), fPublishFailureItem) ?? ex);
			ex = (IncrementalReseeder.TryCleanUpDir(config, IncrementalReseeder.GetTempInspectPath(config), fPublishFailureItem, false) ?? ex);
			ex = (IncrementalReseeder.TryCleanUpDir(config, IncrementalReseeder.GetTempReseedPath(config), fPublishFailureItem) ?? ex);
			ex = (IncrementalReseeder.TryCleanUpDir(config, IncrementalReseeder.GetTempLogPath(config), fPublishFailureItem) ?? ex);
			if (fThrow && ex != null)
			{
				throw new IncrementalReseedRetryableException(ReplayStrings.FailToCleanUpFiles, ex);
			}
			if (ex != null)
			{
				return false;
			}
			ExTraceGlobals.IncrementalReseederTracer.TraceDebug(0L, "Leave CleanupFiles");
			return true;
		}

		// Token: 0x06001CA8 RID: 7336 RVA: 0x0007E280 File Offset: 0x0007C480
		public bool IsIncrementalReseedRequired(Action checkAbortRequested, out long highestLogGenCompared, out bool e00IsEndOfLogStream)
		{
			bool flag = false;
			highestLogGenCompared = -1L;
			e00IsEndOfLogStream = false;
			if (IncrementalReseeder.CheckForInterruptedPatch(this.m_config, this.m_perfTracker))
			{
				this.m_fReusePatchFile = true;
				this.m_perfTracker.IsRestartedIncReseed = true;
				IncrementalReseeder.Tracer.TraceDebug<string, string>((long)this.GetHashCode(), "TargetReplicaInstance {0} : IsIncrementalReseedRequired() found that database '{1}' is already in IncReseed page-patching (V2) stage.", this.m_config.Name, this.m_destEdbFile);
				ExTraceGlobals.FaultInjectionTracer.TraceTest(2286300477U);
				return true;
			}
			ExTraceGlobals.FaultInjectionTracer.TraceTest(3360042301U);
			this.m_perfTracker.RunTimedOperation(IncReseedOperation.CheckForDivergenceAfterSeeding, delegate
			{
				this.CheckForDivergenceAfterSeeding();
			});
			string e00LogFilePath = this.GetE00LogFilePath();
			try
			{
				if (!Directory.Exists(this.m_config.DestinationLogPath))
				{
					if (RegistryParameters.DisableEdbLogDirectoryCreation)
					{
						throw new IncSeedDivergenceCheckFailedException(this.m_dbName, this.m_config.SourceMachine, ReplayStrings.LogDirectoryCreationDisabled(this.m_config.DestinationLogPath));
					}
					Exception ex = DirectoryOperations.TryCreateDirectory(this.m_config.DestinationLogPath);
					if (ex != null)
					{
						throw ex;
					}
					if (RegistryParameters.EnforceDbFolderUnderMountPoint)
					{
						if (!this.m_databaseVolumeInfo.IsValid || this.m_databaseVolumeInfo.LastException != null)
						{
							IncrementalReseeder.Tracer.TraceError<string, string>((long)this.GetHashCode(), "TargetReplicaInstance: Couldn't get valid volume info for DB '{0}'. Error: '{1}'.", this.m_dbName, AmExceptionHelper.GetExceptionMessageOrNoneString(this.m_databaseVolumeInfo.LastException));
							throw new IncSeedDivergenceCheckFailedException(this.m_dbName, this.m_config.SourceMachine, ReplayStrings.DatabaseFailedToGetVolumeInfo(this.m_dbName));
						}
						if (!this.m_databaseVolumeInfo.IsDatabasePathOnMountedFolder || !this.m_databaseVolumeInfo.IsLogPathOnMountedFolder)
						{
							IncrementalReseeder.Tracer.TraceError<string>((long)this.GetHashCode(), "TargetReplicaInstance: DB '{0}' folders are not under a mountpoint. Either disable regkey EnforceDbFolderUnderMountPoint or move database folders under a mountpoint.", this.m_dbName);
							throw new IncSeedDivergenceCheckFailedException(this.m_dbName, this.m_config.SourceMachine, ReplayStrings.DatabaseLogFoldersNotUnderMountpoint(this.m_dbName));
						}
						IncrementalReseeder.Tracer.TraceDebug<string>((long)this.GetHashCode(), "TargetReplicaInstance: DB '{0}' folders are under a mountpoint", this.m_dbName);
					}
				}
				if (!Directory.Exists(this.m_tempInspectPath))
				{
					Directory.CreateDirectory(this.m_tempInspectPath);
				}
				long num = this.m_fileState.HighestGenerationPresent;
				bool flag2 = false;
				int num2 = 0;
				EseLogRecordPosition eseLogRecordPosition = null;
				if (File.Exists(e00LogFilePath))
				{
					num += 1L;
					flag2 = true;
					this.m_perfTracker.RunTimedOperation(IncReseedOperation.EnsureTargetDismounted, delegate
					{
						TargetReplicaInstance.EnsureTargetDismounted(this.m_config);
					});
				}
				string text = this.m_config.BuildFullLogfileName(flag2 ? 0L : num);
				this.m_perfTracker.IsE00LogExists = flag2;
				this.m_perfTracker.PassiveEOLGen = num;
				DateTime utcNow = DateTime.UtcNow;
				this.m_perfTracker.RunTimedOperation(IncReseedOperation.QueryLogRangeFirst, delegate
				{
					this.m_logSource.QueryEndOfLog();
				});
				this.m_setDisconnected.ClearDisconnected();
				checkAbortRequested();
				this.m_logSource.SetTimeoutInMsec(LogSource.GetLogShipTimeoutInMsec(this.m_runningAcll));
				if (!this.m_runningAcll)
				{
					this.InitializeCopyNotificationGeneration(utcNow);
					checkAbortRequested();
				}
				if (0L == this.m_fileState.HighestGenerationPresent)
				{
					IncrementalReseeder.Tracer.TraceError((long)this.GetHashCode(), "No logfiles. IsIncrementalReseedRequired() returning 'false'.");
					return false;
				}
				long num3 = this.m_logSource.CachedEndOfLog;
				this.m_perfTracker.ActiveHighestLogGen = num3;
				if (num3 < num)
				{
					IncrementalReseeder.Tracer.TraceError<long, long, bool>((long)this.GetHashCode(), "Active may have lost log. Active.MaxGen=0x{0:X} Passive.MaxGen=0x{1:X} Passive.HasE00={2}", num3, this.m_fileState.HighestGenerationPresent, flag2);
					if (this.m_runningAcll)
					{
						if (!flag2 || num3 != num - 1L)
						{
							highestLogGenCompared = num3;
							flag = true;
							return true;
						}
						if (!this.m_logSource.LogExists(0L))
						{
							highestLogGenCompared = num3;
							flag = true;
							return true;
						}
						highestLogGenCompared = 0L;
						flag = this.FCompareLogIncseedNeeded(checkAbortRequested, 0L, num, flag2, text, e00LogFilePath, ref eseLogRecordPosition, out num2);
						e00IsEndOfLogStream = !flag;
						return flag;
					}
					else
					{
						this.m_perfTracker.RunTimedOperation(IncReseedOperation.CheckSourceDatabaseMountedFirst, delegate
						{
							this.CheckSourceDatabaseMounted(checkAbortRequested);
						});
						checkAbortRequested();
						num3 = this.m_logSource.QueryEndOfLog();
						this.m_perfTracker.ActiveHighestLogGen = num3;
					}
				}
				if (num3 < num)
				{
					flag = true;
					IncrementalReseeder.Tracer.TraceError<long, long, bool>((long)this.GetHashCode(), "Passive starting with loss. Active.MaxGen=0x{0:X} Passive.MaxGen=0x{1:X} Passive.HasE00={2}", num3, this.m_fileState.HighestGenerationPresent, flag2);
					highestLogGenCompared = num3 + 1L;
					Exception ex2;
					eseLogRecordPosition = EseHelper.GetLastLogRecordPosition(text, this.m_config.E00LogBackupPath, out ex2);
					if (eseLogRecordPosition != null)
					{
						num2 = eseLogRecordPosition.ByteOffsetToStartOfRec + eseLogRecordPosition.LogRecordLength;
						IncrementalReseeder.Tracer.TraceDebug<string, EseLogRecordPosition>((long)this.GetHashCode(), "LastLogRec in '{0}' is {1}", text, eseLogRecordPosition);
					}
					else
					{
						IncrementalReseeder.Tracer.TraceError<string, string>((long)this.GetHashCode(), "Failed to GetLastLogRecordPosition for '{0}': {1}", text, (ex2 == null) ? "log appears empty" : ex2.ToString());
					}
				}
				else
				{
					highestLogGenCompared = num;
					flag = this.FCompareLogIncseedNeeded(checkAbortRequested, highestLogGenCompared, num, flag2, text, e00LogFilePath, ref eseLogRecordPosition, out num2);
				}
				FileInfo fileInfo = new FileInfo(text);
				this.m_endOfLogInformation = new EndOfLogInformation();
				this.m_endOfLogInformation.Generation = num;
				this.m_endOfLogInformation.ByteOffset = num2;
				this.m_endOfLogInformation.LastLogRecPos = eseLogRecordPosition;
				this.m_endOfLogInformation.E00Exists = flag2;
				this.m_endOfLogInformation.LastWriteUtc = fileInfo.LastWriteTimeUtc;
				ReplayCrimsonEvents.PassiveStartupEndOfLogReport.Log<string, Guid, string, string, string, string, bool, bool, string>(this.m_config.DatabaseName, this.m_config.IdentityGuid, text, string.Format("0x{0:X}", num), string.Format("0x{0:X}", num2), string.Format("{0}", this.m_endOfLogInformation.LastWriteUtc.ToString("yyyy-MM-ddTHH:mm:ss.fffZ")), flag2, flag, this.m_config.SourceMachine);
				checkAbortRequested();
				if (!this.m_runningAcll && flag)
				{
					this.CheckSourceDatabaseMounted(checkAbortRequested);
					checkAbortRequested();
				}
			}
			catch (IOException ex3)
			{
				ExTraceGlobals.IncrementalReseederTracer.TraceError<string, IOException>((long)this.GetHashCode(), "IsIncrementalReseedRequired(): Got Exception for {0}: {1}", this.m_config.Name, ex3);
				throw new IncSeedDivergenceCheckFailedException(this.m_dbName, this.m_config.SourceMachine, ex3.Message, ex3);
			}
			catch (EsentErrorException ex4)
			{
				ExTraceGlobals.IncrementalReseederTracer.TraceError<string, EsentErrorException>((long)this.GetHashCode(), "IsIncrementalReseedRequired(): Got Exception for {0}: {1}", this.m_config.Name, ex4);
				throw new IncSeedDivergenceCheckFailedException(this.m_dbName, this.m_config.SourceMachine, ex4.Message, ex4);
			}
			catch (UnauthorizedAccessException ex5)
			{
				ExTraceGlobals.IncrementalReseederTracer.TraceError<string, UnauthorizedAccessException>((long)this.GetHashCode(), "IsIncrementalReseedRequired(): Got Exception for {0}: {1}", this.m_config.Name, ex5);
				throw new IncSeedDivergenceCheckFailedException(this.m_dbName, this.m_config.SourceMachine, ex5.Message, ex5);
			}
			catch (SecurityException ex6)
			{
				ExTraceGlobals.IncrementalReseederTracer.TraceError<string, SecurityException>((long)this.GetHashCode(), "IsIncrementalReseedRequired(): Got Exception for {0}: {1}", this.m_config.Name, ex6);
				throw new IncSeedDivergenceCheckFailedException(this.m_dbName, this.m_config.SourceMachine, ex6.Message, ex6);
			}
			catch (LastLogReplacementException ex7)
			{
				ExTraceGlobals.IncrementalReseederTracer.TraceError<string, LastLogReplacementException>((long)this.GetHashCode(), "IsIncrementalReseedRequired(): Got Exception for {0}: {1}", this.m_config.Name, ex7);
				throw new IncSeedDivergenceCheckFailedException(this.m_dbName, this.m_config.SourceMachine, ex7.Message, ex7);
			}
			catch (Win32Exception ex8)
			{
				ExTraceGlobals.IncrementalReseederTracer.TraceError<string, Win32Exception>((long)this.GetHashCode(), "IsIncrementalReseedRequired(): Got Exception for {0}: {1}", this.m_config.Name, ex8);
				throw new IncSeedDivergenceCheckFailedException(this.m_dbName, this.m_config.SourceMachine, ex8.Message, ex8);
			}
			catch (NetworkRemoteException ex9)
			{
				ExTraceGlobals.IncrementalReseederTracer.TraceError<string, NetworkRemoteException>((long)this.GetHashCode(), "IsIncrementalReseedRequired(): Got Exception for {0}: {1}", this.m_config.Name, ex9);
				if (this.m_runningAcll)
				{
					throw new IncSeedDivergenceCheckFailedException(this.m_dbName, this.m_config.SourceMachine, ex9.Message, ex9);
				}
				this.m_setDisconnected.SetDisconnected(FailureTag.NoOp, ReplayEventLogConstants.Tuple_IncrementalReseedError, new string[]
				{
					ex9.Message
				});
				this.m_setBroken.RestartInstanceSoon(true);
			}
			catch (NetworkTransportException ex10)
			{
				ExTraceGlobals.IncrementalReseederTracer.TraceError<string, NetworkTransportException>((long)this.GetHashCode(), "IsIncrementalReseedRequired(): Network exception occurred for {0}: {1}", this.m_config.Name, ex10);
				if (this.m_runningAcll)
				{
					throw new IncSeedDivergenceCheckFailedException(this.m_dbName, this.m_config.SourceMachine, ex10.Message, ex10);
				}
				this.m_setDisconnected.SetDisconnected(FailureTag.NoOp, ReplayEventLogConstants.Tuple_IncrementalReseedError, new string[]
				{
					ex10.Message
				});
				this.m_setBroken.RestartInstanceSoon(true);
			}
			finally
			{
				if (!flag || this.m_runningAcll)
				{
					this.CloseLogSource();
				}
			}
			IncrementalReseeder.Tracer.TraceDebug<string, bool>((long)this.GetHashCode(), "IsIncrementalReseedRequired: fIncseedNeeded for {0}: {1}", this.m_config.DisplayName, flag);
			return flag;
		}

		// Token: 0x06001CA9 RID: 7337 RVA: 0x0007EC38 File Offset: 0x0007CE38
		public void PerformIncrementalReseed(long startGen)
		{
			Exception ex = null;
			bool flag = false;
			try
			{
				this.ReadSleepTestHook();
				EseHelper.GlobalInit();
				this.IncrementalReseedPrereqChecks();
				flag = true;
				if (!this.m_fReusePatchFile)
				{
					this.m_startGen = startGen - 1L;
					if (this.m_startGen < 0L)
					{
						string msg = string.Format("AssertFailed: Invalid startGen {0}", startGen);
						throw new IncrementalReseedFailedException(msg, 0U);
					}
				}
				this.PrepareIncrementalReseedInternal();
				if (!this.PerformIncreseedV1IfNecessary())
				{
					this.PerformIncReseedV2();
				}
				else
				{
					this.m_perfTracker.IsIncReseedV1Performed = true;
				}
				ReplayEventLogConstants.Tuple_IncSeedingComplete.LogEvent(string.Empty, new object[]
				{
					this.m_dbName
				});
				ExTraceGlobals.IncrementalReseederTracer.TraceDebug((long)this.GetHashCode(), "Increseed restarting instance");
				this.m_setBroken.RestartInstanceNow(ReplayConfigChangeHints.IncReseedCompleted);
			}
			catch (PagePatchApiFailedException ex2)
			{
				this.HandleException(ex2);
			}
			catch (LogInspectorFailedException ex3)
			{
				this.HandleException(ex3);
			}
			catch (IOException ex4)
			{
				this.HandleException(ex4);
			}
			catch (UnauthorizedAccessException ex5)
			{
				this.HandleException(ex5);
			}
			catch (EsentErrorException ex6)
			{
				this.HandleException(ex6);
			}
			catch (Win32Exception ex7)
			{
				this.HandleException(ex7);
			}
			catch (TransactionAbortedException ex8)
			{
				this.HandleException(ex8);
			}
			catch (NetworkRemoteException ex9)
			{
				this.HandleException(ex9);
			}
			catch (NetworkTransportException ex10)
			{
				if (this.m_stopCalled)
				{
					ex = ex10;
				}
				else
				{
					this.HandleException(ex10);
				}
			}
			catch (OperationAbortedException ex11)
			{
				ex = ex11;
			}
			finally
			{
				this.CloseLogSource();
			}
			if (ex != null)
			{
				if (flag)
				{
					ReplayEventLogConstants.Tuple_IncSeedingTerminated.LogEvent(string.Empty, new object[]
					{
						this.m_dbName,
						ex.Message
					});
				}
				ExTraceGlobals.IncrementalReseederTracer.TraceError((long)this.GetHashCode(), "Incremental Reseed is being aborted");
			}
		}

		// Token: 0x06001CAA RID: 7338 RVA: 0x0007EEDC File Offset: 0x0007D0DC
		public void PrepareToStop()
		{
			ExTraceGlobals.IncrementalReseederTracer.TraceError((long)this.GetHashCode(), "Incremental Reseed PrepareStop called");
			this.m_stopCalled = true;
			this.m_logSource.Cancel();
			ExTraceGlobals.FaultInjectionTracer.TraceTest<string>(3896913213U, this.m_config.DatabaseName);
		}

		// Token: 0x06001CAB RID: 7339 RVA: 0x0007EF2D File Offset: 0x0007D12D
		public void CloseLogSource()
		{
			if (this.m_logSource != null)
			{
				this.m_logSource.Close();
			}
		}

		// Token: 0x06001CAC RID: 7340 RVA: 0x0007EF44 File Offset: 0x0007D144
		private static Exception TryCleanupIncReseedFile(IReplayConfiguration config, string fullFilePath, bool fPublishFailureItem)
		{
			if (string.IsNullOrEmpty(fullFilePath))
			{
				throw new ArgumentNullException("fullFilePath");
			}
			Exception ex = FileCleanup.Delete(fullFilePath);
			if (ex != null)
			{
				ExTraceGlobals.IncrementalReseederTracer.TraceError<string, string, string>(0L, "Failed to clean up one of the temp files used for IncReseed '{0}' for database '{1}'; Error: {2}", fullFilePath, config.DisplayName, ex.ToString());
				if (fPublishFailureItem)
				{
					FailureItemPublisherHelper.PublishActionAndLogEvent(FailureTag.AlertOnly, config.IdentityGuid, config.DatabaseName, ReplayEventLogConstants.Tuple_FailedToCleanUpSingleIncReseedFile, new string[]
					{
						config.DatabaseName,
						fullFilePath,
						ex.Message
					});
				}
			}
			return ex;
		}

		// Token: 0x06001CAD RID: 7341 RVA: 0x0007EFC6 File Offset: 0x0007D1C6
		private static Exception TryCleanUpDir(IReplayConfiguration config, string path, bool fPublishFailureItem)
		{
			return IncrementalReseeder.TryCleanUpDir(config, path, fPublishFailureItem, true);
		}

		// Token: 0x06001CAE RID: 7342 RVA: 0x0007EFD4 File Offset: 0x0007D1D4
		private static Exception TryCleanUpDir(IReplayConfiguration config, string path, bool fPublishFailureItem, bool deleteDirectory)
		{
			if (string.IsNullOrEmpty(path))
			{
				throw new ArgumentNullException("path");
			}
			Exception ex = IncrementalReseeder.CleanupDirectory(path, deleteDirectory);
			if (ex != null)
			{
				ExTraceGlobals.IncrementalReseederTracer.TraceDebug<string, string, string>(0L, "Failed to clean up one of the temp dir {0} for database {1}; Exception: {2}", path, config.DisplayName, ex.ToString());
				if (fPublishFailureItem)
				{
					FailureItemPublisherHelper.PublishActionAndLogEvent(FailureTag.AlertOnly, config.IdentityGuid, config.DatabaseName, ReplayEventLogConstants.Tuple_FailedToCleanUpFile, new string[]
					{
						path,
						config.DatabaseName,
						ex.Message
					});
				}
			}
			return ex;
		}

		// Token: 0x06001CAF RID: 7343 RVA: 0x0007F057 File Offset: 0x0007D257
		private static void UpdateKnownEndOfLogFromActive(long generation, DateTime writeTime, ReplayState state, DateTime lastContactTime)
		{
			state.CopyNotificationGenerationNumber = generation;
			state.LatestCopyNotificationTime = writeTime;
			state.LatestCopierContactTime = lastContactTime;
		}

		// Token: 0x06001CB0 RID: 7344 RVA: 0x0007F06E File Offset: 0x0007D26E
		private void InitializeCopyNotificationGeneration(DateTime lastContactTime)
		{
			ExTraceGlobals.FaultInjectionTracer.TraceTest(3185978685U);
			IncrementalReseeder.UpdateKnownEndOfLogFromActive(this.m_logSource.CachedEndOfLog, this.m_logSource.CachedEndOfLogWriteTimeUtc, this.m_config.ReplayState, lastContactTime);
		}

		// Token: 0x06001CB1 RID: 7345 RVA: 0x0007F190 File Offset: 0x0007D390
		private bool FCompareLogIncseedNeeded(Action checkAbortRequested, long lgenToCompare, long passiveEndOfLogGen, bool e00Exist, string endOfLogFilename, string e00Log, ref EseLogRecordPosition lastLogRecInfo, out int endOfLogByteOffset)
		{
			IncrementalReseeder.<>c__DisplayClassc CS$<>8__locals1 = new IncrementalReseeder.<>c__DisplayClassc();
			CS$<>8__locals1.lgenToCompare = lgenToCompare;
			CS$<>8__locals1.endOfLogFilename = endOfLogFilename;
			CS$<>8__locals1.e00Log = e00Log;
			CS$<>8__locals1.<>4__this = this;
			bool flag = false;
			string text = string.Format("{0}{1}{2}", this.m_logSource.SourcePath, Path.DirectorySeparatorChar, this.m_config.BuildShortLogfileName(CS$<>8__locals1.lgenToCompare));
			if (!this.m_logSource.LogExists(CS$<>8__locals1.lgenToCompare))
			{
				IncrementalReseeder.Tracer.TraceError<string, string>((long)this.GetHashCode(), "TargetReplicaInstance {0} : IsIncrementalReseedRequired could not find source logfile {1}. Incremental or full reseed required.", this.m_config.Name, text);
				throw new ReseedCheckMissingLogfileException(text);
			}
			if (!e00Exist && !File.Exists(CS$<>8__locals1.endOfLogFilename))
			{
				IncrementalReseeder.Tracer.TraceError<string, string>((long)this.GetHashCode(), "TargetReplicaInstance {0} : IsIncrementalReseedRequired could not find target logfile {1}. Return false and let copier try copy the file again.", this.m_config.Name, CS$<>8__locals1.endOfLogFilename);
				throw new ReseedCheckMissingLogfileException(CS$<>8__locals1.endOfLogFilename);
			}
			checkAbortRequested();
			lastLogRecInfo = new EseLogRecordPosition();
			CS$<>8__locals1.tempLastLogRecInfo = lastLogRecInfo;
			using (TemporaryFile tempSrcFile = this.GenerateTempSourceLogfileName(CS$<>8__locals1.lgenToCompare, "TEMP.INSPCTR."))
			{
				IncrementalReseeder.Tracer.TraceError<string, TemporaryFile, string>((long)this.GetHashCode(), "TargetReplicaInstance {0} : IsIncrementalReseedRequired will compare '{1}' and '{2}'", this.m_config.Name, tempSrcFile, CS$<>8__locals1.endOfLogFilename);
				checkAbortRequested();
				this.m_logSource.CopyLog(CS$<>8__locals1.lgenToCompare, tempSrcFile);
				checkAbortRequested();
				if (!e00Exist)
				{
					bool isLogFileEqual = false;
					this.m_perfTracker.RunTimedOperation(IncReseedOperation.IsLogfileEqual, delegate
					{
						isLogFileEqual = EseHelper.IsLogfileEqual(tempSrcFile, CS$<>8__locals1.endOfLogFilename, CS$<>8__locals1.<>4__this.m_config.E00LogBackupPath, null, CS$<>8__locals1.tempLastLogRecInfo);
					});
					if (!isLogFileEqual)
					{
						IncrementalReseeder.Tracer.TraceError<string, TemporaryFile, string>((long)this.GetHashCode(), "TargetReplicaInstance {0} : IsIncrementalReseedRequired found that '{1}' and '{2}' are different. Incremental or full reseed required.", this.m_config.Name, tempSrcFile, CS$<>8__locals1.endOfLogFilename);
						flag = true;
					}
				}
				else
				{
					checkAbortRequested();
					bool isLogFileSubset = false;
					this.m_perfTracker.RunTimedOperation(IncReseedOperation.IsLogFileSubset, delegate
					{
						isLogFileSubset = CS$<>8__locals1.<>4__this.IsLogFileSubset(CS$<>8__locals1.e00Log, CS$<>8__locals1.tempLastLogRecInfo, tempSrcFile);
					});
					if (!isLogFileSubset)
					{
						IncrementalReseeder.Tracer.TraceDebug((long)this.GetHashCode(), "TargetReplicaInstance {0} : Logfile '{1}' is not a subset of logfile '{2}'({3}). Incremental or full reseed is required", new object[]
						{
							this.m_config.Name,
							CS$<>8__locals1.e00Log,
							text,
							tempSrcFile
						});
						flag = true;
					}
					else
					{
						checkAbortRequested();
						long num = passiveEndOfLogGen - 1L;
						IncrementalReseeder.Tracer.TraceDebug((long)this.GetHashCode(), "TargetReplicaInstance {0} : Logfile '{1}' is a subset of logfile '{2}'({3}). Now checking if previous log generation {4} is binary equal to the log on the source server.", new object[]
						{
							this.m_config.Name,
							CS$<>8__locals1.e00Log,
							text,
							tempSrcFile,
							num
						});
						if (num <= 0L)
						{
							throw new IncrementalReseedFailedException(ReplayStrings.NoDivergedPointFound(this.m_config.DisplayName, this.m_config.SourceMachine), 0U);
						}
						if (!this.IsSourceLogFileBinaryEqual(num, "TEMP.INSPCTR."))
						{
							IncrementalReseeder.Tracer.TraceDebug((long)this.GetHashCode(), "TargetReplicaInstance {0} : Logfile '{1}' is a subset of logfile '{2}'({3}). However, the previous log generation {4} is not binary equal to the log on the source server. Incremental or full reseed is required", new object[]
							{
								this.m_config.Name,
								CS$<>8__locals1.e00Log,
								text,
								tempSrcFile,
								num
							});
							flag = true;
							this.m_perfTracker.IsPreviousLogNotBinaryEqual = true;
						}
						else
						{
							checkAbortRequested();
							this.m_perfTracker.RunTimedOperation(IncReseedOperation.ReplaceE00LogTransacted, delegate
							{
								LastLogReplacer.ReplaceLastLog(CS$<>8__locals1.<>4__this.m_config, CS$<>8__locals1.lgenToCompare, CS$<>8__locals1.e00Log, tempSrcFile);
							});
							if (CS$<>8__locals1.lgenToCompare != 0L)
							{
								this.m_fileState.ClearE00LogGeneration();
								this.m_fileState.SetLowestAndHighestGenerationsPresent(this.m_fileState.LowestGenerationPresent, CS$<>8__locals1.lgenToCompare);
							}
						}
						ExTraceGlobals.FaultInjectionTracer.TraceTest(2806394173U);
					}
				}
			}
			endOfLogByteOffset = lastLogRecInfo.ByteOffsetToStartOfRec + lastLogRecInfo.LogRecordLength;
			IncrementalReseeder.Tracer.TraceDebug<string, EseLogRecordPosition>((long)this.GetHashCode(), "LastLogRec in '{0}' is {1}", CS$<>8__locals1.endOfLogFilename, lastLogRecInfo);
			IncrementalReseeder.Tracer.TraceDebug<string, bool>((long)this.GetHashCode(), "FCompareLogIncseedNeeded for {0}: {1}", this.m_config.DisplayName, flag);
			return flag;
		}

		// Token: 0x06001CB2 RID: 7346 RVA: 0x0007F628 File Offset: 0x0007D828
		private bool IsLogFileSubset(string e00Log, EseLogRecordPosition tempLastLogRecInfo, TemporaryFile tempSrcFile)
		{
			bool result = EseHelper.IsLogfileSubset(tempSrcFile, e00Log, this.m_config.E00LogBackupPath, null, tempLastLogRecInfo);
			ExTraceGlobals.FaultInjectionTracer.TraceTest<bool>(3733335357U, ref result);
			return result;
		}

		// Token: 0x06001CB3 RID: 7347 RVA: 0x0007F664 File Offset: 0x0007D864
		private void CheckForDivergenceAfterSeeding()
		{
			FileState fileState = this.m_fileState;
			if (fileState.HighestGenerationPresent >= fileState.HighestGenerationRequired)
			{
				DatabaseSeederInstance.DeleteDivergenceCheckFiles(this.m_config.DatabaseName, this.m_config.DestinationSystemPath, this.m_config.LogFilePrefix);
				return;
			}
			long divergenceCheckGeneration = DatabaseSeederInstance.GetDivergenceCheckGeneration(this.m_config.DestinationSystemPath, this.m_config.LogFilePrefix);
			if (divergenceCheckGeneration > fileState.HighestGenerationPresent)
			{
				string text = DatabaseSeederInstance.BuildDivergenceCheckFileName(this.m_config.DestinationSystemPath, this.m_config.LogFilePrefix, divergenceCheckGeneration);
				LogSource logSource = LogSource.Construct(this.m_config, null, null, LogSource.GetLogShipTimeoutInMsec(false));
				Exception ex = null;
				try
				{
					string text2 = text + ".Remote";
					using (new TemporaryFile(text2))
					{
						logSource.CopyLog(fileState.HighestGenerationRequired, text2);
						if (!IncrementalReseeder.IsFileBinaryEqual(text, text2))
						{
							this.m_perfTracker.IsDivergentAfterSeed = true;
							IncrementalReseeder.Tracer.TraceDebug<string, string>((long)this.GetHashCode(), "Post seed check failed on '{0}' and '{1}'", text, text2);
							ReplayCrimsonEvents.DivergenceAfterSeedError.Log<string, string, string, long>(this.m_config.DatabaseName, Environment.MachineName, this.m_config.SourceMachine, divergenceCheckGeneration);
							throw new IncrementalReseedFailedException(ReplayStrings.LogRepairNotPossibleActiveIsDivergent(this.m_config.SourceMachine), 0U);
						}
					}
				}
				catch (NetworkRemoteException ex2)
				{
					ex = ex2;
				}
				catch (NetworkTransportException ex3)
				{
					ex = ex3;
				}
				catch (IOException ex4)
				{
					ex = ex4;
				}
				finally
				{
					logSource.Close();
				}
				if (ex != null)
				{
					IncrementalReseeder.Tracer.TraceDebug<Exception>((long)this.GetHashCode(), "CheckForDivergenceAfterSeeding failed: {0}", ex);
					throw new IncSeedDivergenceCheckFailedException(this.m_config.DatabaseName, this.m_config.SourceMachine, ex.Message, ex);
				}
			}
		}

		// Token: 0x06001CB4 RID: 7348 RVA: 0x0007F850 File Offset: 0x0007DA50
		private void CheckSourceDatabaseMounted(Action checkAbortRequested)
		{
			Exception ex = null;
			bool flag = false;
			try
			{
				using (IStoreRpc newStoreControllerInstance = Dependencies.GetNewStoreControllerInstance(this.m_config.SourceMachine))
				{
					checkAbortRequested();
					Guid[] dbGuids = new Guid[]
					{
						this.m_config.IdentityGuid
					};
					MdbStatus[] array = newStoreControllerInstance.ListMdbStatus(dbGuids);
					checkAbortRequested();
					if (array == null || array.Length == 0)
					{
						IncrementalReseeder.Tracer.TraceError<string>((long)this.GetHashCode(), "TargetReplicaInstance {0}: CheckSourceDatabaseMounted: DB is not yet online. Got an empty result from ListMdbStatus query.", this.m_config.Name);
					}
					else if ((array[0].Status & MdbStatusFlags.Online) == MdbStatusFlags.Online)
					{
						flag = true;
					}
					else
					{
						IncrementalReseeder.Tracer.TraceError<string, MdbStatusFlags>((long)this.GetHashCode(), "TargetReplicaInstance {0}: DB is not yet online. Status is {1}", this.m_config.Name, array[0].Status);
					}
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
				IncrementalReseeder.Tracer.TraceError<string, Exception>((long)this.GetHashCode(), "TargetReplicaInstance {0}: CheckSourceDatabaseMounted failed: {1}", this.m_config.Name, ex);
				this.m_setDisconnected.SetDisconnected(FailureTag.NoOp, ReplayEventLogConstants.Tuple_IncrementalReseedSourceDatabaseMountRpcError, new string[]
				{
					this.m_config.SourceMachine,
					ex.Message
				});
			}
			else if (!flag)
			{
				this.m_setDisconnected.SetDisconnected(FailureTag.NoOp, ReplayEventLogConstants.Tuple_IncrementalReseedSourceDatabaseDismounted, new string[]
				{
					this.m_config.SourceMachine
				});
			}
			if (!flag)
			{
				this.m_setBroken.RestartInstanceSoon(true);
			}
		}

		// Token: 0x06001CB5 RID: 7349 RVA: 0x0007F9F0 File Offset: 0x0007DBF0
		private void HandleException(Exception ex)
		{
			ExTraceGlobals.IncrementalReseederTracer.TraceError<Exception>((long)this.GetHashCode(), "Fail to incremental reseed: {0}", ex);
			if (ex is EsentErrorException)
			{
				throw new IncrementalReseedFailedException(ex.Message, (uint)((EsentErrorException)ex).Error, ex);
			}
			if (ex is LogInspectorFailedException)
			{
				throw new IncrementalReseedFailedException(ex.Message, 0U, ex);
			}
			if (ex is PagePatchApiFailedException)
			{
				throw new IncrementalReseedFailedException(ex.Message, 0U, ex);
			}
			if (ex is NetworkTransportException || ex is NetworkRemoteException)
			{
				this.HandleSourceSideException(ex);
				return;
			}
			if (!(ex is IOException))
			{
				throw new IncrementalReseedRetryableException(ex.Message, ex);
			}
			int error = 0;
			if (FileOperations.IsFatalIOException((IOException)ex, out error))
			{
				throw new IncrementalReseedFailedException(ex.Message, (uint)error, ex);
			}
			throw new IncrementalReseedRetryableException(ex.Message, ex);
		}

		// Token: 0x06001CB6 RID: 7350 RVA: 0x0007FAB8 File Offset: 0x0007DCB8
		private void HandleSourceSideException(Exception ex)
		{
			if (this.m_runningAcll)
			{
				throw new IncrementalReseedRetryableException(ex.Message, ex);
			}
			this.m_setDisconnected.SetDisconnected(FailureTag.NoOp, ReplayEventLogConstants.Tuple_IncrementalReseedError, new string[]
			{
				ex.Message
			});
			this.m_setBroken.RestartInstanceSoon(true);
			throw new SetBrokenControlTransferException();
		}

		// Token: 0x06001CB7 RID: 7351 RVA: 0x0007FB10 File Offset: 0x0007DD10
		internal static bool CheckForInterruptedPatch(IReplayConfiguration config, IncReseedPerformanceTracker perfTracker)
		{
			string destinationEdbPath = config.DestinationEdbPath;
			if (!File.Exists(destinationEdbPath))
			{
				return false;
			}
			if (EseDatabasePatchFileIO.IsLegacyPatchFilePresent(destinationEdbPath))
			{
				IncrementalReseeder.Tracer.TraceError(0L, "A legacy patch file was found from an older build. The copy must be reseeded.");
				PagePatchLegacyFileExistsException ex = new PagePatchLegacyFileExistsException();
				throw new IncrementalReseedFailedException(ex.Message, 0U, ex);
			}
			string text;
			string text2;
			EseDatabasePatchFileIO.GetNames(destinationEdbPath, out text, out text2);
			if (File.Exists(text))
			{
				return true;
			}
			JET_DBINFOMISC passiveDatabaseFileInfo = FileChecker.GetPassiveDatabaseFileInfo(destinationEdbPath, config.DatabaseName, config.IdentityGuid, config.ServerName);
			if (EseHelper.IsIncrementalReseedInProgress(passiveDatabaseFileInfo))
			{
				if (perfTracker != null)
				{
					perfTracker.IsFailedPassivePagePatch = true;
				}
				ReseedCheckMissingLogfileException ex2 = new ReseedCheckMissingLogfileException(text);
				throw new IncrementalReseedFailedException(ex2.Message, 0U, ex2);
			}
			return false;
		}

		// Token: 0x06001CB8 RID: 7352 RVA: 0x0007FBB8 File Offset: 0x0007DDB8
		private void IncrementalReseedPrereqChecks()
		{
			if (!File.Exists(this.m_destEdbFile))
			{
				throw new IncrementalReseedPrereqException(ReplayStrings.TargetDbNotThere(this.m_destEdbFile));
			}
			if (this.m_fReusePatchFile)
			{
				using (EseDatabasePatchFileIO eseDatabasePatchFileIO = EseDatabasePatchFileIO.OpenRead(this.m_destEdbFile))
				{
					EseDatabasePatchState eseDatabasePatchState = eseDatabasePatchFileIO.ReadHeader();
					eseDatabasePatchState.Validate(this.m_config.IdentityGuid, 32768L, eseDatabasePatchFileIO.DoneName);
				}
			}
		}

		// Token: 0x06001CB9 RID: 7353 RVA: 0x0007FC3C File Offset: 0x0007DE3C
		private string GetDebuggerOutputFileName()
		{
			string text = Path.Combine(this.m_destLogPath, "incseedDebug");
			if (!Directory.Exists(text))
			{
				Directory.CreateDirectory(text);
			}
			DirectoryInfo directoryInfo = new DirectoryInfo(text);
			FileInfo[] files = directoryInfo.GetFiles(".pageNumber");
			if (files.Length > 3)
			{
				DateTime t = DateTime.UtcNow;
				FileInfo fileInfo = files[0];
				foreach (FileInfo fileInfo2 in files)
				{
					if (t > fileInfo2.CreationTimeUtc)
					{
						fileInfo = fileInfo2;
						t = fileInfo2.CreationTimeUtc;
					}
				}
				try
				{
					fileInfo.Delete();
				}
				catch (IOException)
				{
				}
				catch (UnauthorizedAccessException)
				{
				}
			}
			return Path.Combine(text, "IncReseedDebug_" + DateTime.UtcNow.Ticks.ToString() + ".pageNumber");
		}

		// Token: 0x06001CBA RID: 7354 RVA: 0x0007FD50 File Offset: 0x0007DF50
		private void PrepareIncrementalReseedInternal()
		{
			ExTraceGlobals.FaultInjectionTracer.TraceTest<string>(2552638781U, this.m_config.DatabaseName);
			TargetReplicaInstance.EnsureTargetDismounted(this.m_config);
			if (this.m_fReusePatchFile)
			{
				ExTraceGlobals.IncrementalReseederTracer.TraceError((long)this.GetHashCode(), "If we crashed during database patching, we will directly go continuing patching instead of restart");
				ReplayCrimsonEvents.IncrementalReseedReusepatchfile.Log<string, string>(this.m_config.DatabaseName, this.m_config.ServerName);
				return;
			}
			IncrementalReseeder.CleanupFiles(this.m_config, true, false);
			this.GetSuspendLockAndCheckStop(delegate
			{
				this.m_perfTracker.RunTimedOperation(IncReseedOperation.FindDivergencePoint, delegate
				{
					this.FindDivergencePoint();
					this.m_perfTracker.FirstDivergedLogGen = this.m_divergedPoint;
				});
			});
		}

		// Token: 0x06001CBB RID: 7355 RVA: 0x0007FDF0 File Offset: 0x0007DFF0
		private bool PerformIncreseedV1IfNecessary()
		{
			if ((this.m_config.ReplayLagTime != EnhancedTimeSpan.Zero || RegistryParameters.EnableV1IncReseed) && !this.m_fReusePatchFile && this.m_fileState.HighestGenerationRequired < this.m_divergedPoint && EseHelper.IsV1IncrementalReseedSupported(this.m_destEdbFile))
			{
				ExTraceGlobals.IncrementalReseederTracer.TraceDebug<long, long>((long)this.GetHashCode(), "HighestGenerationRequired {0} <= diverged point {1}, we start LLR routine", this.m_fileState.HighestGenerationRequired, this.m_divergedPoint);
				ReplayCrimsonEvents.IncrementalReseedV1Performed.Log<string, string, long, long, long>(this.m_config.DatabaseName, this.m_config.ServerName, this.m_fileState.LowestGenerationRequired, this.m_fileState.HighestGenerationRequired, this.m_divergedPoint);
				this.GetSuspendLockAndCheckStop(delegate
				{
					this.RemoveDivergedDestLogfiles(this.m_divergedPoint, true);
				});
				return true;
			}
			return false;
		}

		// Token: 0x06001CBC RID: 7356 RVA: 0x0007FF78 File Offset: 0x0007E178
		private void PrepareIncReseedV2()
		{
			if (this.m_fReusePatchFile)
			{
				return;
			}
			this.CheckIncrementalReseedPossible();
			try
			{
				this.m_perfTracker.RunTimedOperation(IncReseedOperation.PauseTruncation, delegate
				{
					LogTruncater.RequestGlobalTruncationCoordination(1L, this.m_config.SourceMachine, this.m_config.TargetMachine, this.m_config.IdentityGuid, this.m_config.LogFilePrefix, this.m_config.DestinationLogPath, this.m_config.CircularLoggingEnabled, this.m_shuttingDownEvent);
				});
			}
			catch (FailedToOpenLogTruncContextException ex)
			{
				throw new IncrementalReseedRetryableException(ex.Message);
			}
			catch (LogTruncationException ex2)
			{
				throw new IncrementalReseedRetryableException(ex2.Message);
			}
			this.GetSuspendLockAndCheckStop(delegate
			{
				this.m_perfTracker.RunTimedOperation(IncReseedOperation.GeneratePageListSinceDivergence, delegate
				{
					this.GeneratePageListSinceDivergence();
				});
			});
			this.m_logSource.QueryEndOfLog();
			this.CheckDiskSpaceAndThreshold(this.m_logSource.CachedEndOfLog);
			this.GetSuspendLockAndCheckStop(delegate
			{
				this.m_perfTracker.RunTimedOperation(IncReseedOperation.ReadDatabasePagesFromActive, delegate
				{
					this.ReadDatabasePages();
				});
			});
			this.UpdatePerfTrackerRequiredLogRange();
			ReplayCrimsonEvents.IncrementalReseedGenCalculation.Log<string, string, long, long>(this.m_config.DatabaseName, this.m_config.ServerName, this.m_lowestGenRequired, this.m_highestGenRequired);
			this.m_perfTracker.RunTimedOperation(IncReseedOperation.CopyAndInspectRequiredLogFiles, delegate
			{
				this.CopyAndInspectRequiredLogFiles();
			});
			this.FinishWritingPatchFile();
		}

		// Token: 0x06001CBD RID: 7357 RVA: 0x00080080 File Offset: 0x0007E280
		private void FinishWritingPatchFile()
		{
			this.m_patchHeader.LowestRequiredLogGen = this.m_lowestGenRequired;
			this.m_patchHeader.HighestRequiredLogGen = this.m_highestGenRequired;
			this.m_patchHeader.CurrentPatchPhase = EseDatabasePatchState.PatchPhase.GatheringComplete;
			using (EseDatabasePatchFileIO eseDatabasePatchFileIO = EseDatabasePatchFileIO.OpenWrite(this.m_destEdbFile, this.m_patchHeader))
			{
				eseDatabasePatchFileIO.WriteHeader();
				eseDatabasePatchFileIO.FinishWriting();
			}
		}

		// Token: 0x06001CBE RID: 7358 RVA: 0x000800F8 File Offset: 0x0007E2F8
		private void UpdatePerfTrackerRequiredLogRange()
		{
			this.m_perfTracker.LowestLogGenRequired = this.m_lowestGenRequired;
			this.m_perfTracker.HighestLogGenRequired = this.m_highestGenRequired;
		}

		// Token: 0x06001CBF RID: 7359 RVA: 0x0008011C File Offset: 0x0007E31C
		private void CalculateNewRequiredRangeForNoPageReferences()
		{
			JET_DBINFOMISC jet_DBINFOMISC;
			Api.JetGetDatabaseFileInfo(this.m_destEdbFile, out jet_DBINFOMISC, JET_DbInfo.Misc);
			long val = (long)jet_DBINFOMISC.genMinRequired;
			this.m_lowestGenRequired = Math.Min(this.m_divergedPoint, val);
			long highestGenerationPresentWithE = this.m_fileState.HighestGenerationPresentWithE00;
			this.m_highestGenRequired = Math.Min(highestGenerationPresentWithE, this.m_logSource.CachedEndOfLog);
			this.m_highestGenRequired = Math.Max(this.m_highestGenRequired, Math.Max(this.m_lowestGenRequired, this.m_divergedPoint));
			ExTraceGlobals.IncrementalReseederTracer.TraceDebug<long, long>((long)this.GetHashCode(), "CalculateNewRequiredRange(): m_lowestGenRequired={0}, m_highestGenRequired={1}", this.m_lowestGenRequired, this.m_highestGenRequired);
		}

		// Token: 0x06001CC0 RID: 7360 RVA: 0x000801F4 File Offset: 0x0007E3F4
		private void PerformIncReseedV2()
		{
			this.m_perfTracker.RunTimedOperation(IncReseedOperation.PrepareIncReseedV2Overall, delegate
			{
				this.PrepareIncReseedV2();
			});
			ReplayEventLogConstants.Tuple_IncSeedingSourceReleased.LogEvent(string.Empty, new object[]
			{
				this.m_dbName
			});
			ExTraceGlobals.FaultInjectionTracer.TraceTest<string>(3626380605U, this.m_config.DatabaseName);
			this.SleepTestHook();
			this.GetSuspendLockAndCheckStop(delegate
			{
				this.m_perfTracker.RunTimedOperation(IncReseedOperation.PatchDatabaseOverall, delegate
				{
					this.PatchDatabase();
				});
				IncrementalReseeder.CleanupFiles(this.m_config, false, false);
			});
		}

		// Token: 0x06001CC1 RID: 7361 RVA: 0x0008026C File Offset: 0x0007E46C
		public void GetSuspendLockAndCheckStop(IncrementalReseeder.ReseedOperation operation)
		{
			StateLock stateLock = null;
			bool flag = false;
			try
			{
				this.CheckStopPending();
				stateLock = this.m_config.ReplayState.SuspendLock;
				flag = stateLock.TryEnter(LockOwner.Component, false);
				if (!flag)
				{
					ExTraceGlobals.IncrementalReseederTracer.TraceError((long)this.GetHashCode(), "Incremental Reseed was suspended in the middle");
					throw new OperationAbortedException();
				}
				operation();
			}
			finally
			{
				if (stateLock != null && flag)
				{
					stateLock.Leave(LockOwner.Component);
				}
			}
		}

		// Token: 0x06001CC2 RID: 7362 RVA: 0x000802E8 File Offset: 0x0007E4E8
		private string GenerateTempSourceLogfileName(long generationNum, string filePrefix)
		{
			string path = EseHelper.MakeLogfileName(filePrefix + this.m_config.LogFilePrefix, this.m_logSuffix, generationNum);
			return Path.Combine(this.m_tempInspectPath, path);
		}

		// Token: 0x06001CC3 RID: 7363 RVA: 0x00080324 File Offset: 0x0007E524
		public void FindDivergencePoint()
		{
			ExTraceGlobals.IncrementalReseederTracer.TraceDebug((long)this.GetHashCode(), "Enter FindDivergencePoint");
			long startGen = this.m_startGen;
			long num = Math.Max(this.m_fileState.LowestGenerationPresent, this.m_logSource.QueryLogRange());
			if (num != 0L)
			{
				for (long num2 = startGen; num2 >= num; num2 -= 1L)
				{
					if (this.IsSourceLogFileBinaryEqual(num2, "TEMP.RESEEDER."))
					{
						this.m_divergedPoint = num2 + 1L;
						break;
					}
				}
			}
			ExTraceGlobals.IncrementalReseederTracer.TraceDebug<string, string, long>(0L, "Diverged Point between {0} and {1} is {2}", this.m_srcLogPath, this.m_destLogPath, this.m_divergedPoint);
			if (this.m_divergedPoint <= 0L)
			{
				throw new IncrementalReseedFailedException(ReplayStrings.NoDivergedPointFound(this.m_config.DisplayName, this.m_config.SourceMachine), 0U);
			}
			ReplayCrimsonEvents.IncrementalReseedDivergencePoint.Log<string, string, long>(this.m_config.DatabaseName, this.m_config.ServerName, this.m_divergedPoint);
			this.LogLossInformation();
			ExTraceGlobals.IncrementalReseederTracer.TraceDebug((long)this.GetHashCode(), "Leave FindDivergencePoint");
		}

		// Token: 0x06001CC4 RID: 7364 RVA: 0x0008042C File Offset: 0x0007E62C
		private bool IsSourceLogFileBinaryEqual(long lGenToCompare, string filePrefix)
		{
			string text = this.m_config.BuildFullLogfileName(lGenToCompare);
			if (!File.Exists(text))
			{
				throw new IncrementalReseedFailedException(ReplayStrings.MissingPassiveLogRequiredForDivergenceDetection(text), 0U);
			}
			bool result;
			using (TemporaryFile temporaryFile = this.GenerateTempSourceLogfileName(lGenToCompare, filePrefix))
			{
				if (!this.m_logSource.LogExists(lGenToCompare))
				{
					throw new IncrementalReseedFailedException(ReplayStrings.MissingActiveLogRequiredForDivergenceDetection(text, this.m_config.SourceMachine), 0U);
				}
				this.m_logSource.CopyLog(lGenToCompare, temporaryFile);
				result = IncrementalReseeder.IsFileBinaryEqual(temporaryFile, text);
			}
			return result;
		}

		// Token: 0x06001CC5 RID: 7365 RVA: 0x000804D8 File Offset: 0x0007E6D8
		private void LogLossInformation()
		{
			long num = 0L;
			long num2 = (this.m_endOfLogInformation.Generation - this.m_divergedPoint) * 1048576L + (long)this.m_endOfLogInformation.ByteOffset - num;
			ReplayCrimsonEvents.RecordLossDuringPassiveStartup.Log<string, Guid, long, string, string, string, string, string, bool, string>(this.m_config.DatabaseName, this.m_config.IdentityGuid, num2, string.Format("0x{0:X}", this.m_endOfLogInformation.Generation), string.Format("0x{0:X}", this.m_endOfLogInformation.ByteOffset), string.Format("0x{0:X}", this.m_divergedPoint), string.Format("0x{0:X}", num), string.Format("{0}", this.m_endOfLogInformation.LastWriteUtc.ToString("yyyy-MM-ddTHH:mm:ss.fffZ")), this.m_endOfLogInformation.E00Exists, this.m_config.SourceMachine);
		}

		// Token: 0x06001CC6 RID: 7366 RVA: 0x000805C4 File Offset: 0x0007E7C4
		public long[] GeneratePageListSinceDivergence()
		{
			ExTraceGlobals.IncrementalReseederTracer.TraceDebug((long)this.GetHashCode(), "Enter GeneratePageListSinceDivergence");
			this.m_sizeOfLocalDbBeforePatchInPagesExcludingHeader = new FileInfo(this.m_destDbPath).Length / 32768L - 2L;
			List<long> list = new List<long>();
			this.GeneratePageListForMultiFiles(this.m_config.DestinationLogPath, this.m_divergedPoint, this.m_fileState.HighestGenerationPresent, list);
			this.CheckStopPending();
			this.m_pageList = new long[list.Count];
			list.CopyTo(this.m_pageList, 0);
			this.m_perfTracker.NumPagesToBePatched = this.m_pageList.LongLength;
			ExTraceGlobals.IncrementalReseederTracer.TraceDebug<int>((long)this.GetHashCode(), "Leave GeneratePageListSinceDivergence, pages need to be copied {0}", this.m_pageList.Length);
			ReplayCrimsonEvents.IncrementalReseedGeneratedPageList.Log<string, string, int, long>(this.m_config.DatabaseName, this.m_config.ServerName, this.m_pageList.Length, this.m_sizeOfLocalDbBeforePatchInPagesExcludingHeader + 2L);
			return this.m_pageList;
		}

		// Token: 0x06001CC7 RID: 7367 RVA: 0x000806BF File Offset: 0x0007E8BF
		public void TestOnlySetLowestHighestGenRequired(long lowReq, long highReq)
		{
			this.m_lowestGenRequired = lowReq;
			this.m_highestGenRequired = highReq;
		}

		// Token: 0x06001CC8 RID: 7368 RVA: 0x000806D0 File Offset: 0x0007E8D0
		public void CopyAndInspectRequiredLogFiles()
		{
			ExTraceGlobals.IncrementalReseederTracer.TraceDebug((long)this.GetHashCode(), "Enter CopyAndInspectRequiredLogFiles");
			if (!Directory.Exists(this.m_tempInspectPath))
			{
				Directory.CreateDirectory(this.m_tempInspectPath);
			}
			this.CheckStopPending();
			if (!Directory.Exists(this.m_tempLogPath))
			{
				Directory.CreateDirectory(this.m_tempLogPath);
			}
			this.CheckStopPending();
			foreach (string path in Directory.GetFiles(this.m_tempInspectPath))
			{
				this.CheckStopPending();
				File.Delete(path);
			}
			foreach (string path2 in Directory.GetFiles(this.m_tempLogPath))
			{
				this.CheckStopPending();
				File.Delete(path2);
			}
			LogVerifier logVerifier = new LogVerifier(this.m_config.LogFilePrefix);
			try
			{
				LogContinuityChecker logContinuityChecker = new LogContinuityChecker();
				if (this.m_lowestGenRequired > this.m_fileState.LowestGenerationPresent)
				{
					logContinuityChecker.Initialize(this.m_lowestGenRequired - 1L, this.m_config.DestinationLogPath, this.m_config.LogFilePrefix, this.m_logSuffix);
				}
				ReplayCrimsonEvents.IncrementalReseedStartedCopyingLogs.Log<string, string, long, long>(this.m_config.DatabaseName, this.m_config.ServerName, this.m_lowestGenRequired, this.m_highestGenRequired);
				for (long num = this.m_lowestGenRequired; num <= this.m_highestGenRequired; num += 1L)
				{
					this.CheckStopPending();
					this.CopyAndInspectOneFile(num, logVerifier, logContinuityChecker);
				}
				ReplayCrimsonEvents.IncrementalReseedFinishedCopyingLogs.Log<string, string, long, long>(this.m_config.DatabaseName, this.m_config.ServerName, this.m_lowestGenRequired, this.m_highestGenRequired);
				string divergedLogFilePath = this.GetDivergedLogFilePath();
				ExTraceGlobals.IncrementalReseederTracer.TraceDebug<string>((long)this.GetHashCode(), "divergence file {0}", divergedLogFilePath);
				JET_LOGINFOMISC jet_LOGINFOMISC;
				UnpublishedApi.JetGetLogFileInfo(divergedLogFilePath, out jet_LOGINFOMISC, JET_LogInfo.Misc2);
				long num2 = (long)jet_LOGINFOMISC.lgposCheckpoint.lGeneration;
				DiagCore.RetailAssert(num2 >= 1L, "the checkpoint gen {0} in the divergence point should be greater than 1", new object[]
				{
					num2
				});
				this.CheckStopPending();
				if (num2 < this.m_lowestGenRequired)
				{
					ExTraceGlobals.IncrementalReseederTracer.TraceDebug<long>((long)this.GetHashCode(), "actual Min gen required after adjustment {0} ", num2);
					logContinuityChecker = new LogContinuityChecker();
					if (num2 > this.m_fileState.LowestGenerationPresent)
					{
						logContinuityChecker.Initialize(num2 - 1L, this.m_config.DestinationLogPath, this.m_config.LogFilePrefix, this.m_logSuffix);
					}
					ReplayCrimsonEvents.IncrementalReseedStartedCopyingLogs.Log<string, string, long, long>(this.m_config.DatabaseName, this.m_config.ServerName, num2, this.m_lowestGenRequired - 1L);
					for (long num3 = num2; num3 < this.m_lowestGenRequired; num3 += 1L)
					{
						this.CheckStopPending();
						this.CopyAndInspectOneFile(num3, logVerifier, logContinuityChecker);
					}
					ReplayCrimsonEvents.IncrementalReseedFinishedCopyingLogs.Log<string, string, long, long>(this.m_config.DatabaseName, this.m_config.ServerName, num2, this.m_lowestGenRequired - 1L);
					this.m_lowestGenRequired = num2;
				}
			}
			finally
			{
				logVerifier.Term();
			}
			ExTraceGlobals.IncrementalReseederTracer.TraceDebug((long)this.GetHashCode(), "Leave CopyAndInspectRequiredLogFiles");
		}

		// Token: 0x06001CC9 RID: 7369 RVA: 0x00080A64 File Offset: 0x0007EC64
		private void CopyAndInspectOneFile(long gen, LogVerifier logVerifier, LogContinuityChecker continuityChecker)
		{
			int cretries;
			for (cretries = 1; cretries <= 3; cretries++)
			{
				bool result = false;
				IncrementalReseeder.Tracer.TraceDebug<string, int, long>((long)this.GetHashCode(), "IR.CopyAndInspectOneFile( {0} ): Attempt {1} to copy log generation {2}", this.m_config.Name, cretries, gen);
				this.GetSuspendLockAndCheckStop(delegate
				{
					this.CopyAndInspectOneFileInternal(gen, logVerifier, continuityChecker, ref cretries, ref result);
				});
				if (result)
				{
					break;
				}
			}
			this.m_perfmonCounters.IncReseedLogCopiedNumber = (gen - this.m_lowestGenRequired + 1L) * 1048576L / 1024L;
		}

		// Token: 0x06001CCA RID: 7370 RVA: 0x00080B44 File Offset: 0x0007ED44
		private void CopyAndInspectOneFileInternal(long gen, LogVerifier logVerifier, LogContinuityChecker continuityChecker, ref int cretries, ref bool result)
		{
			string path = EseHelper.MakeLogfileName(this.m_config.LogFilePrefix, this.m_logSuffix, gen);
			string text = Path.Combine(this.m_tempInspectPath, path);
			string destFileName = Path.Combine(this.m_tempLogPath, path);
			Exception ex = null;
			try
			{
				if (!this.m_logSource.LogExists(gen))
				{
					long num = this.m_logSource.QueryEndOfLog();
					if (gen <= num)
					{
						throw new IncrementalReseedFailedException(ReplayStrings.MissingLogRequired(text), 0U);
					}
					long num2 = Math.Min(gen - num, 5L);
					this.AttemptLogRoll(num2);
					if (cretries != 3)
					{
						Thread.Sleep(3000 * cretries);
						result = false;
						return;
					}
					IncrementalReseeder.Tracer.TraceError<string, int, long>((long)this.GetHashCode(), "IR.CopyAndInspectOneFile( {0} ): Maximum attempts ({1}) to copy log generation {2} have been reached. Restarting the ReplicaInstance via RestartInstanceSoon(true)...", this.m_config.Name, 3, gen);
					ReplayCrimsonEvents.IncrementalReseedWaitingForLogGeneration.Log<string, string, Guid, int, long, long, long>(this.m_config.Name, this.m_config.ServerName, this.m_config.IdentityGuid, 3, gen, num, num2);
					this.m_setBroken.RestartInstanceSoon(true);
					this.CheckStopPending();
				}
				DateTime dateTime;
				this.m_logSource.CopyLog(gen, text, out dateTime);
				this.CheckStopPending();
				LocalizedString value;
				result = LogInspector.VerifyLogStatic(gen, this.m_logSource, text, true, this.m_fileState, logVerifier, continuityChecker, new LogInspector.CheckStopDelegate(this.CheckStopPending), out value);
				if (!result)
				{
					throw new LogInspectorFailedException(value);
				}
				File.Move(text, destFileName);
				this.CheckStopPending();
			}
			catch (NetworkTransportException ex2)
			{
				cretries = 3;
				ex = ex2;
			}
			catch (NetworkRemoteException ex3)
			{
				ex = ex3;
			}
			catch (IOException ex4)
			{
				ex = ex4;
			}
			catch (UnauthorizedAccessException ex5)
			{
				ex = ex5;
			}
			if (ex != null)
			{
				result = false;
				ExTraceGlobals.IncrementalReseederTracer.TraceDebug<string, Exception>((long)this.GetHashCode(), "Copy {0} got {1}", text, ex);
				if (cretries == 3)
				{
					this.HandleException(ex);
					return;
				}
				Thread.Sleep(3000 * cretries);
			}
		}

		// Token: 0x06001CCB RID: 7371 RVA: 0x00080D78 File Offset: 0x0007EF78
		private void AttemptLogRoll(long numLogsToRoll)
		{
			Exception ex = null;
			try
			{
				IncrementalReseeder.Tracer.TraceDebug<string, long, string>((long)this.GetHashCode(), "IncReseed.AttemptLogRoll( {0} ): Attempting to roll {1} logs on the active copy '{2}'", this.m_config.Name, numLogsToRoll, this.m_config.SourceMachine);
				using (IStoreRpc newStoreControllerInstance = Dependencies.GetNewStoreControllerInstance(this.m_config.SourceMachine))
				{
					newStoreControllerInstance.ForceNewLog(this.m_config.IdentityGuid, numLogsToRoll);
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
				IncrementalReseeder.Tracer.TraceError<string, Exception>((long)this.GetHashCode(), "IncReseed.AttemptLogRoll( {0} ): Exception occurred: {1}", this.m_config.Name, ex);
			}
		}

		// Token: 0x06001CCC RID: 7372 RVA: 0x00080E3C File Offset: 0x0007F03C
		private string GetDivergedLogFilePath()
		{
			long divergedPoint = this.m_divergedPoint;
			return Path.Combine(this.m_tempLogPath, EseHelper.MakeLogfileName(this.m_config.LogFilePrefix, this.m_logSuffix, divergedPoint));
		}

		// Token: 0x06001CCD RID: 7373 RVA: 0x00080E74 File Offset: 0x0007F074
		private void GeneratePageListForMultiFiles(string dir, long lowGen, long highGen, List<long> result)
		{
			if (result == null)
			{
				throw new ArgumentNullException("result");
			}
			string text = EseHelper.MakeLogFilePath(this.m_config, 0L, dir);
			bool flag = File.Exists(text);
			for (long num = lowGen; num <= highGen; num += 1L)
			{
				this.CheckStopPending();
				this.GeneratePageListForOneFile(Path.Combine(dir, EseHelper.MakeLogfileName(this.m_config.LogFilePrefix, this.m_logSuffix, num)), result);
			}
			if (flag)
			{
				this.CheckStopPending();
				this.GeneratePageListForOneFile(text, result);
			}
			this.CheckStopPending();
			result.Sort();
		}

		// Token: 0x06001CCE RID: 7374 RVA: 0x00080F00 File Offset: 0x0007F100
		private void GeneratePageListForOneFile(string fileName, List<long> result)
		{
			if (result == null)
			{
				throw new ArgumentNullException("result");
			}
			this.KeepLogChannelAlive();
			EseLogRecord[] logRecords = EseHelper.GetLogRecords(fileName, ".");
			foreach (EseLogRecord eseLogRecord in logRecords)
			{
				if (eseLogRecord is EsePageRecord)
				{
					long pageNumber = ((EsePageRecord)eseLogRecord).PageNumber;
					if (pageNumber >= this.m_sizeOfLocalDbBeforePatchInPagesExcludingHeader)
					{
						ReplayCrimsonEvents.IncrementalReseedPageExceedCurrentSize.Log<string, string, string, long>(this.m_config.DatabaseName, this.m_config.ServerName, pageNumber.ToString(), this.m_sizeOfLocalDbBeforePatchInPagesExcludingHeader);
					}
					if (!result.Contains(pageNumber))
					{
						result.Add(pageNumber);
					}
				}
				else if (eseLogRecord is EseDatabaseResizeRecord)
				{
					if (((EseDatabaseResizeRecord)eseLogRecord).Operation == DatabaseResizeOperation.Shrink)
					{
						throw new IncrementalReseedFailedException(ReplayStrings.IncSeedNotSupportedWithShrinkDatabaseError, 0U);
					}
				}
				else if (eseLogRecord is EseAttachInfoRecord)
				{
					DiagCore.RetailAssert(((EseAttachInfoRecord)eseLogRecord).DatabaseId == 1, "more than one db attach to the log stream?", new object[0]);
				}
				else if (eseLogRecord is EseDatabaseFileRecord)
				{
					DiagCore.RetailAssert(((EseDatabaseFileRecord)eseLogRecord).DatabaseId == 1, "more than one db attach to the log stream?", new object[0]);
				}
			}
		}

		// Token: 0x06001CCF RID: 7375 RVA: 0x00081020 File Offset: 0x0007F220
		private string GetIncReseedLogFileBackupDirectory()
		{
			string currentDateString = FileOperations.GetCurrentDateString();
			return Path.Combine(this.m_e00LogBackupPath, "IncReseed" + currentDateString);
		}

		// Token: 0x06001CD0 RID: 7376 RVA: 0x0008104C File Offset: 0x0007F24C
		private void RemoveDivergedDestLogfiles(long fromGeneration, bool fUpdateDbHeader)
		{
			ExTraceGlobals.IncrementalReseederTracer.TraceDebug<string, string>((long)this.GetHashCode(), "Removing diverged log files from {0} to {1}", this.m_destLogPath, this.m_e00LogBackupPath);
			string text = this.GetE00TmpLogFilePath();
			File.Delete(text);
			this.CheckStopPending();
			string incReseedLogFileBackupDirectory = this.GetIncReseedLogFileBackupDirectory();
			text = this.GetE00LogFilePath();
			if (File.Exists(text))
			{
				this.RemoveLogAndUpdateDatabaseHeader(text, incReseedLogFileBackupDirectory, true);
			}
			this.CheckStopPending();
			for (long num = this.m_fileState.HighestGenerationPresent; num >= fromGeneration; num -= 1L)
			{
				this.CheckStopPending();
				text = Path.Combine(this.m_config.DestinationLogPath, EseHelper.MakeLogfileName(this.m_config.LogFilePrefix, this.m_logSuffix, num));
				this.RemoveLogAndUpdateDatabaseHeader(text, incReseedLogFileBackupDirectory, fUpdateDbHeader);
			}
		}

		// Token: 0x06001CD1 RID: 7377 RVA: 0x00081101 File Offset: 0x0007F301
		private string GetE00LogFilePath()
		{
			return EseHelper.MakeLogFilePath(this.m_config, 0L, this.m_config.DestinationLogPath);
		}

		// Token: 0x06001CD2 RID: 7378 RVA: 0x0008111C File Offset: 0x0007F31C
		private string GetE00TmpLogFilePath()
		{
			return Path.Combine(this.m_destLogPath, this.m_config.LogFilePrefix + "tmp" + this.m_logSuffix);
		}

		// Token: 0x06001CD3 RID: 7379 RVA: 0x00081154 File Offset: 0x0007F354
		private void RemoveLogAndUpdateDatabaseHeader(string logFile, string destinationDir, bool fUpdateDbHeader)
		{
			ExTraceGlobals.IncrementalReseederTracer.TraceDebug<string, string>((long)this.GetHashCode(), "enter RemoveLogAndUpdateDatabaseHeader {0} {1}", logFile, destinationDir);
			if (!File.Exists(logFile))
			{
				throw new ArgumentException("logFile", string.Format("{0} doesn't exist", logFile));
			}
			if (!Directory.Exists(destinationDir))
			{
				Directory.CreateDirectory(destinationDir);
			}
			ExTraceGlobals.IncrementalReseederTracer.TraceDebug<string, string>((long)this.GetHashCode(), "Moving logfile '{0}' to directory '{1}' for incremental reseed", logFile, destinationDir);
			string destFileName = Path.Combine(destinationDir, Path.GetFileName(logFile));
			if (fUpdateDbHeader && !EseHelper.IsDatabaseConsistent(this.m_destDbPath))
			{
				UnpublishedApi.JetRemoveLogfile(this.m_destDbPath, logFile, RemoveLogfileGrbit.None);
			}
			File.Move(logFile, destFileName);
		}

		// Token: 0x06001CD4 RID: 7380 RVA: 0x000811F0 File Offset: 0x0007F3F0
		private bool DoesSourceSupportMultiplePageRead()
		{
			if (RegistryParameters.DisableISeedStreamingPageReader)
			{
				return false;
			}
			AmServerName serverName = new AmServerName(this.m_srcNode);
			Exception ex;
			IADServer miniServer = AmBestCopySelectionHelper.GetMiniServer(serverName, out ex);
			return miniServer != null && ServerVersion.Compare(miniServer.AdminDisplayVersion, IncrementalReseeder.FirstVersionSupportingMultiplePageRead) >= 0;
		}

		// Token: 0x06001CD5 RID: 7381 RVA: 0x00081234 File Offset: 0x0007F434
		private void ReadDatabasePages()
		{
			ExTraceGlobals.IncrementalReseederTracer.TraceDebug<ExDateTime>((long)this.GetHashCode(), "Enter ReadDatabasePages {0}", ExDateTime.Now);
			try
			{
				long num = -1L;
				long num2 = -1L;
				long num3 = num;
				long num4 = num2;
				string debuggerOutputFileName = this.GetDebuggerOutputFileName();
				this.m_patchHeader = new EseDatabasePatchState(this.m_config.IdentityGuid, EseDatabasePatchComponent.IncrementalReseed, 32768L);
				this.m_patchHeader.FirstDivergedLogGen = this.m_divergedPoint;
				this.m_patchHeader.NumPagesToPatch = this.m_pageList.Length;
				using (StreamWriter streamWriter = new StreamWriter(File.OpenWrite(debuggerOutputFileName)))
				{
					using (EseDatabasePatchFileIO eseDatabasePatchFileIO = EseDatabasePatchFileIO.OpenWrite(this.m_destEdbFile, this.m_patchHeader))
					{
						ExTraceGlobals.FaultInjectionTracer.TraceTest(2821074237U);
						this.CheckStopPending();
						eseDatabasePatchFileIO.WriteHeader();
						eseDatabasePatchFileIO.WritePageNumbers(this.m_pageList);
						streamWriter.Write("SourceMachine:" + this.m_srcNode + ";");
						streamWriter.Write("divergedPoint:" + this.m_divergedPoint.ToString() + ";");
						streamWriter.Write("PageListLength:" + this.m_pageList.Length + ";");
						if (this.IsPagesReferencedInDivergentLogs())
						{
							this.m_fPagesReferencedInDivergentRange = true;
							ExTraceGlobals.IncrementalReseederTracer.TraceDebug<bool>((long)this.GetHashCode(), "ReadDatabasePages(): m_fPagesReferencedInDivergentRange = {0}", this.m_fPagesReferencedInDivergentRange);
							using (IEseDatabaseReader remoteEseDatabaseReader = EseDatabaseReader.GetRemoteEseDatabaseReader(this.m_srcNode, this.m_config.IdentityGuid, false))
							{
								int num5 = (int)remoteEseDatabaseReader.ReadPageSize();
								if (num5 != 32768)
								{
									throw new IncrementalReseedFailedException(ReplayStrings.DatabasePageSizeUnexpected((long)num5, 32768L), 0U);
								}
								RemoteEseDatabaseReader remoteEseDatabaseReader2 = remoteEseDatabaseReader as RemoteEseDatabaseReader;
								bool flag = remoteEseDatabaseReader2 != null && this.DoesSourceSupportMultiplePageRead();
								streamWriter.Write("Pages:");
								ExTraceGlobals.IncrementalReseederTracer.TraceDebug((long)this.GetHashCode(), "Finish Writing the pagelist to reseed file");
								this.CheckStopPending();
								if (flag)
								{
									remoteEseDatabaseReader2.StartSendingPages(this.m_pageList);
								}
								for (int i = 0; i < this.m_pageList.Length; i++)
								{
									this.CheckStopPending();
									this.KeepLogChannelAlive();
									Exception ex = null;
									byte[] array = null;
									try
									{
										if (flag)
										{
											array = remoteEseDatabaseReader2.ReadNextPage(this.m_pageList[i], out num3, out num4);
										}
										else
										{
											array = remoteEseDatabaseReader.ReadOnePage(this.m_pageList[i], out num3, out num4);
										}
										this.CheckStopPending();
										if (num == -1L || num > num3)
										{
											num = num3;
										}
										if (num2 == -1L || num2 < num4)
										{
											num2 = num4;
										}
										JET_PAGEINFO jet_PAGEINFO = new JET_PAGEINFO
										{
											pgno = (int)this.m_pageList[i]
										};
										JET_PAGEINFO[] pageInfos = new JET_PAGEINFO[]
										{
											jet_PAGEINFO
										};
										UnpublishedApi.JetGetPageInfo2(array, array.Length, pageInfos, PageInfoGrbit.None, JET_PageInfo.Level1);
										streamWriter.Write(this.m_pageList[i] + "|");
										this.CheckStopPending();
									}
									catch (NetworkRemoteException ex2)
									{
										ExTraceGlobals.IncrementalReseederTracer.TraceDebug<long, Exception>((long)this.GetHashCode(), "read no. {0} page got {1}", this.m_pageList[i], ex2.InnerException);
										if (ex2.InnerException is JetErrorFileIOBeyondEOFException)
										{
											this.HandleEOFDuringReadPages(i, array, eseDatabasePatchFileIO, streamWriter);
											break;
										}
										ex = ex2;
									}
									catch (EsentErrorException ex3)
									{
										ExTraceGlobals.IncrementalReseederTracer.TraceDebug<long, EsentErrorException>((long)this.GetHashCode(), "read no. {0} page got {1}", this.m_pageList[i], ex3);
										ex = ex3;
									}
									if (ex != null)
									{
										throw new IncrementalReseedRetryableException(ReplayStrings.CopyPageFailed((int)this.m_pageList[i], this.m_srcEdbFile), ex);
									}
									eseDatabasePatchFileIO.WritePageData(array);
									this.m_perfmonCounters.IncReseedDBPagesReadNumber = (long)((i + 1) * 32768 / 1024);
								}
								remoteEseDatabaseReader.ForceNewLog();
								this.CheckStopPending();
							}
							JET_DBINFOMISC jet_DBINFOMISC;
							Api.JetGetDatabaseFileInfo(this.m_destEdbFile, out jet_DBINFOMISC, JET_DbInfo.Misc);
							long num6 = (long)jet_DBINFOMISC.genMinRequired;
							this.CheckStopPending();
							if (num6 != 0L)
							{
								this.m_lowestGenRequired = Math.Min(Math.Min(num, this.m_divergedPoint), num6);
							}
							else
							{
								this.m_lowestGenRequired = Math.Min(num, this.m_divergedPoint);
							}
							this.m_highestGenRequired = Math.Max(num2, Math.Max(this.m_lowestGenRequired, this.m_divergedPoint));
							ExTraceGlobals.IncrementalReseederTracer.TraceDebug((long)this.GetHashCode(), "Min Required:{0}, Max Required:{1}, Destination Min Required:{2}, Divergence point:{3}", new object[]
							{
								this.m_lowestGenRequired,
								this.m_highestGenRequired,
								num6,
								this.m_divergedPoint
							});
						}
						else
						{
							this.CalculateNewRequiredRangeForNoPageReferences();
							ExTraceGlobals.IncrementalReseederTracer.TraceDebug<long, long, long>((long)this.GetHashCode(), "Min Required:{0}, Max Required:{1}, Divergence point:{2}", this.m_lowestGenRequired, this.m_highestGenRequired, this.m_divergedPoint);
							ExTraceGlobals.FaultInjectionTracer.TraceTest<string>(3628477757U, this.m_config.DatabaseName);
						}
						streamWriter.Write("HighestGenerationRequired:" + this.m_highestGenRequired + ";");
						streamWriter.Write("LowestGenerationRequired:" + this.m_lowestGenRequired + ";");
						streamWriter.Flush();
						this.m_patchHeader.LowestRequiredLogGen = this.m_lowestGenRequired;
						this.m_patchHeader.HighestRequiredLogGen = this.m_highestGenRequired;
						eseDatabasePatchFileIO.WriteHeader();
					}
				}
			}
			catch (UnExpectedPageSizeException ex4)
			{
				throw new IncrementalReseedFailedException(ex4.Message, 0U);
			}
			catch (NetworkRemoteException ex5)
			{
				throw new IncrementalReseedRetryableException(ex5.Message, ex5);
			}
			catch (NetworkTransportException ex6)
			{
				throw new IncrementalReseedRetryableException(ex6.Message, ex6);
			}
			catch (FailedToOpenBackupFileHandleException ex7)
			{
				throw new IncrementalReseedRetryableException(ex7.Message, ex7);
			}
			finally
			{
				ExTraceGlobals.IncrementalReseederTracer.TraceDebug<ExDateTime>((long)this.GetHashCode(), "Leave ReadDatabasePages {0}", ExDateTime.Now);
			}
		}

		// Token: 0x06001CD6 RID: 7382 RVA: 0x000818D4 File Offset: 0x0007FAD4
		private void HandleEOFDuringReadPages(int pageIndex, byte[] pageBuf, EseDatabasePatchFileIO patchFile, StreamWriter debuggerFile)
		{
			long num = this.m_pageList[pageIndex];
			if (num > 0L)
			{
				for (int i = 0; i < pageBuf.Length; i++)
				{
					pageBuf[i] = 0;
				}
				while (pageIndex < this.m_pageList.Length)
				{
					if (this.m_pageList[pageIndex] < num)
					{
						throw new IncrementalReseedFailedException("PageList was not sorted.", 0U);
					}
					debuggerFile.Write(this.m_pageList[pageIndex] + "|");
					patchFile.WritePageData(pageBuf);
					this.CheckStopPending();
					pageIndex++;
				}
				IncrementalReseeder.Tracer.TraceDebug((long)this.GetHashCode(), "Pages were sucessfully zeroed");
				return;
			}
			throw new IncrementalReseedFailedException("EOF returned for Page 0.", 0U);
		}

		// Token: 0x06001CD7 RID: 7383 RVA: 0x00081980 File Offset: 0x0007FB80
		private void CheckIncrementalReseedPossible()
		{
			JET_DBINFOMISC jet_DBINFOMISC;
			if (!EseHelper.IsIncrementalReseedPossible(this.m_destEdbFile, out jet_DBINFOMISC))
			{
				ExTraceGlobals.IncrementalReseederTracer.TraceError<string>((long)this.GetHashCode(), "IsIncrementalReseedPossible fails state={0}", jet_DBINFOMISC.dbstate.ToString());
				throw new IncrementalReseedFailedException(ReplayStrings.InvalidDbStateForIncReseed(jet_DBINFOMISC.dbstate.ToString()), 0U);
			}
			if (EseHelper.IsDatabaseConsistent(this.m_destEdbFile))
			{
				string text = null;
				bool flag = false;
				this.m_perfTracker.IsDatabaseConsistent = true;
				try
				{
					this.m_perfTracker.RunTimedOperation(IncReseedOperation.RedirtyDatabase, delegate
					{
						this.RedirtyDatabase();
					});
					flag = true;
					throw new IncrementalReseedFileStateChangedException();
				}
				catch (EsentErrorException ex)
				{
					IncrementalReseeder.Tracer.TraceError<EsentErrorException>((long)this.GetHashCode(), "RedirtyDatabase failed: {0}", ex);
					text = string.Format("{0}: {1}", ex.GetType(), ex.Message);
					throw new IncrementalReseedFailedException(text, 0U, ex);
				}
				finally
				{
					if (flag)
					{
						ReplayCrimsonEvents.ISeedRedirtySuccess.Log<string, string>(this.m_config.DatabaseName, this.m_config.ServerName);
					}
					else
					{
						ReplayCrimsonEvents.ISeedRedirtyFailed.Log<string, string, string>(this.m_config.DatabaseName, this.m_config.ServerName, text ?? ReplayStrings.UnknownError);
					}
				}
			}
		}

		// Token: 0x06001CD8 RID: 7384 RVA: 0x00081AD8 File Offset: 0x0007FCD8
		private void RedirtyDatabase()
		{
			IncrementalReseeder.Tracer.TraceDebug<string>((long)this.GetHashCode(), "Redirtying {0}", this.m_config.DatabaseName);
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			JET_INSTANCE instance;
			Api.JetCreateInstance(out instance, "IncSeedReDirty" + this.m_dbName);
			try
			{
				InstanceParameters instanceParameters = new InstanceParameters(instance);
				instanceParameters.BaseName = this.m_config.LogFilePrefix;
				instanceParameters.LogFileDirectory = this.m_config.DestinationLogPath;
				instanceParameters.SystemDirectory = this.m_destSystemPath;
				instanceParameters.NoInformationEvent = true;
				instanceParameters.AggressiveLogRollover = true;
				instanceParameters.MaxVerPages = 8000;
			}
			catch (EsentErrorException arg)
			{
				IncrementalReseeder.Tracer.TraceError<EsentErrorException>((long)this.GetHashCode(), "Redirty failed to init: {0}", arg);
				Api.JetTerm2(instance, TermGrbit.None);
				throw;
			}
			string e00LogFilePath = this.GetE00LogFilePath();
			Exception ex;
			if (!File.Exists(e00LogFilePath) && !EseHelper.CreateTempLog(this.m_config.LogFilePrefix, this.m_config.DestinationLogPath, out ex))
			{
				ReplayEventLogConstants.Tuple_RedirtyDatabaseCreateTempLog.LogEvent(this.m_dbName, new object[]
				{
					this.m_dbName,
					ex
				});
			}
			Api.JetInit(ref instance);
			try
			{
				JET_SESID sesid;
				Api.JetBeginSession(instance, out sesid, null, null);
				try
				{
					Api.JetAttachDatabase(sesid, this.m_destEdbFile, AttachDatabaseGrbit.None);
					flag2 = true;
				}
				finally
				{
					Api.JetEndSession(sesid, EndSessionGrbit.None);
					flag3 = true;
				}
			}
			finally
			{
				try
				{
					Api.JetTerm2(instance, (TermGrbit)8);
					flag = true;
				}
				catch (EsentDirtyShutdownException)
				{
					flag = true;
				}
			}
			if (!flag || !flag2 || !flag3)
			{
				throw new IncrementalReseedFailedException("Redirty failed to report an exception.", 0U);
			}
			if (!EseHelper.IsDatabaseDirty(this.m_destEdbFile))
			{
				throw new IncrementalReseedFailedException("Redirty failed to set the database state to DirtyShutdown.", 0U);
			}
			IncrementalReseeder.Tracer.TraceDebug<string>((long)this.GetHashCode(), "Successfully redirtied {0}", this.m_config.DatabaseName);
		}

		// Token: 0x06001CD9 RID: 7385 RVA: 0x00081CD8 File Offset: 0x0007FED8
		private void PatchDatabase()
		{
			ExTraceGlobals.IncrementalReseederTracer.TraceDebug<ExDateTime>((long)this.GetHashCode(), "Enter PatchDatabase at {0}", ExDateTime.Now);
			using (EseDatabasePatchFileIO eseDatabasePatchFileIO = EseDatabasePatchFileIO.OpenRead(this.m_destEdbFile))
			{
				this.m_patchHeader = eseDatabasePatchFileIO.ReadHeader();
			}
			using (EseDatabasePatchFileIO patchFile = EseDatabasePatchFileIO.OpenWrite(this.m_destEdbFile, this.m_patchHeader))
			{
				this.m_highestGenRequired = this.m_patchHeader.HighestRequiredLogGen;
				this.m_lowestGenRequired = this.m_patchHeader.LowestRequiredLogGen;
				ExTraceGlobals.IncrementalReseederTracer.TraceDebug<long, long>((long)this.GetHashCode(), "m_highestGenRequired = {0}, m_lowestGenRequired = {1}", this.m_highestGenRequired, this.m_lowestGenRequired);
				this.UpdatePerfTrackerRequiredLogRange();
				JET_INSTANCE instance;
				Api.JetCreateInstance(out instance, "IncSeed" + this.m_dbName);
				try
				{
					this.InitializeJetInstance(instance);
					ReplayCrimsonEvents.IncrementalReseedPatchedDatabase.Log<string, string>(this.m_config.DatabaseName, this.m_config.ServerName);
					this.m_setViable.ClearViable();
					UnpublishedApi.JetBeginDatabaseIncrementalReseed(instance, this.m_destEdbFile, BeginDatabaseIncrementalReseedGrbit.None);
					ExTraceGlobals.FaultInjectionTracer.TraceTest<string>(2823171389U, this.m_config.DatabaseName);
					ExTraceGlobals.FaultInjectionTracer.TraceTest(3991285053U);
					if (!this.m_patchHeader.IsPatchCompleted)
					{
						if (this.m_patchHeader.NumPagesToPatch > 0)
						{
							int num = 0;
							foreach (long num2 in patchFile.ReadPageNumbers())
							{
								int num3 = (int)num2;
								this.m_perfTracker.IsPagesReferencedInDivergentLogs = true;
								ExTraceGlobals.IncrementalReseederTracer.TraceDebug<int>((long)this.GetHashCode(), "Database patching is required because there are {0} pages referenced in the divergent log range.", this.m_patchHeader.NumPagesToPatch);
								byte[] array = patchFile.ReadPageData(num++);
								try
								{
									UnpublishedApi.JetPatchDatabasePages(instance, this.m_destEdbFile, num3, 1, array, array.Length, PatchDatabasePagesGrbit.None);
								}
								catch (EsentFileIOBeyondEOFException)
								{
									ReplayCrimsonEvents.IncrementalReseedIgnoreExceptionWhilePatching.Log<string, string, int, long>(this.m_config.DatabaseName, this.m_config.ServerName, num3, new FileInfo(this.m_destDbPath).Length / 32768L);
								}
							}
							ReplayCrimsonEvents.IncrementalReseedWritePages.Log<string, string, int, long>(this.m_config.DatabaseName, this.m_config.ServerName, this.m_patchHeader.NumPagesToPatch, new FileInfo(this.m_destDbPath).Length / 32768L);
						}
						this.m_patchHeader.IsPatchCompleted = true;
						this.m_patchHeader.CurrentPatchPhase = EseDatabasePatchState.PatchPhase.PatchingPagesComplete;
						patchFile.WriteHeader();
					}
					this.m_perfTracker.RunTimedOperation(IncReseedOperation.ReplaceLogFiles, delegate
					{
						this.ReplaceLogFiles(patchFile);
					});
					UnpublishedApi.JetEndDatabaseIncrementalReseed(instance, this.m_destEdbFile, (int)this.m_patchHeader.LowestRequiredLogGen, (int)this.m_patchHeader.FirstDivergedLogGen, (int)this.m_patchHeader.HighestRequiredLogGen, EndDatabaseIncrementalReseedGrbit.None);
				}
				catch (EsentDatabaseInvalidIncrementalReseedException ex)
				{
					JET_DBINFOMISC jet_DBINFOMISC;
					Api.JetGetDatabaseFileInfo(this.m_destEdbFile, out jet_DBINFOMISC, JET_DbInfo.Misc);
					ExTraceGlobals.IncrementalReseederTracer.TraceError<string, EsentDatabaseInvalidIncrementalReseedException>((long)this.GetHashCode(), "PatchDatabase fails with IsamDatabaseInvalidIncrementalReseedException. state={0} ex = {1}", jet_DBINFOMISC.dbstate.ToString(), ex);
					throw new IncrementalReseedFailedException(ReplayStrings.InvalidDbStateForIncReseed(jet_DBINFOMISC.dbstate.ToString()), (uint)ex.Error, ex);
				}
				finally
				{
					Api.JetTerm2(instance, TermGrbit.None);
				}
			}
			EseDatabasePatchFileIO.DeleteAll(this.m_destEdbFile);
			ExTraceGlobals.IncrementalReseederTracer.TraceDebug<ExDateTime>((long)this.GetHashCode(), "Leave PatchDatabase at {0}", ExDateTime.Now);
		}

		// Token: 0x06001CDA RID: 7386 RVA: 0x00082104 File Offset: 0x00080304
		private void InitializeJetInstance(JET_INSTANCE instance)
		{
			InstanceParameters instanceParameters = new InstanceParameters(instance);
			instanceParameters.BaseName = this.m_config.LogFilePrefix;
			instanceParameters.LogFileDirectory = this.m_destLogPath;
			instanceParameters.SystemDirectory = this.m_destSystemPath;
			instanceParameters.NoInformationEvent = true;
			instanceParameters.EventLoggingLevel = EventLoggingLevels.Min;
			if (!RegistryParameters.DisableJetFailureItemPublish)
			{
				Api.JetSetSystemParameter(instance, JET_SESID.Nil, (JET_param)168, 1, string.Empty);
			}
			ExTraceGlobals.IncrementalReseederTracer.TraceDebug((long)this.GetHashCode(), "Finished setting Jet Params.");
		}

		// Token: 0x06001CDB RID: 7387 RVA: 0x00082184 File Offset: 0x00080384
		private void EmptyTheBackupFolder()
		{
			if (!Directory.Exists(this.m_tempBackupPath))
			{
				Directory.CreateDirectory(this.m_tempBackupPath);
			}
			DirectoryInfo directoryInfo = new DirectoryInfo(this.m_tempBackupPath);
			foreach (FileInfo fileInfo in directoryInfo.GetFiles())
			{
				fileInfo.Delete();
			}
		}

		// Token: 0x06001CDC RID: 7388 RVA: 0x000821D8 File Offset: 0x000803D8
		private void ReplaceLogFiles(EseDatabasePatchFileIO patchFile)
		{
			Exception ex = null;
			try
			{
				if (this.m_patchHeader.CurrentPatchPhase < EseDatabasePatchState.PatchPhase.MovingOldLogs)
				{
					this.EmptyTheBackupFolder();
					this.m_patchHeader.CurrentPatchPhase = EseDatabasePatchState.PatchPhase.MovingOldLogs;
					patchFile.WriteHeader();
				}
				if (this.m_patchHeader.CurrentPatchPhase == EseDatabasePatchState.PatchPhase.MovingOldLogs)
				{
					this.SaveOldLogs();
					this.m_patchHeader.CurrentPatchPhase = EseDatabasePatchState.PatchPhase.MovingNewLogs;
					patchFile.WriteHeader();
				}
				if (this.m_patchHeader.CurrentPatchPhase == EseDatabasePatchState.PatchPhase.MovingNewLogs)
				{
					this.PlaceNewLogs();
					this.m_patchHeader.CurrentPatchPhase = EseDatabasePatchState.PatchPhase.LogReplacementComplete;
					patchFile.WriteHeader();
				}
			}
			catch (IOException ex2)
			{
				ex = ex2;
			}
			catch (SecurityException ex3)
			{
				ex = ex3;
			}
			catch (UnauthorizedAccessException ex4)
			{
				ex = ex4;
			}
			if (ex != null)
			{
				throw new IncrementalReseedFailedException("Failed to replace log files. Error:" + ex.Message, 0U, ex);
			}
			ReplayCrimsonEvents.IncrementalReseedReplacedLogfiles.Log<string, string, long, long, string>(this.m_config.DatabaseName, this.m_config.ServerName, this.m_lowestGenRequired, this.m_highestGenRequired, this.m_destLogPath);
		}

		// Token: 0x06001CDD RID: 7389 RVA: 0x000822E0 File Offset: 0x000804E0
		private void SaveOldLogs()
		{
			long num = Math.Max(this.m_lowestGenRequired, this.m_fileState.LowestGenerationPresent);
			long highestGenerationPresent = this.m_fileState.HighestGenerationPresent;
			ReplayCrimsonEvents.IncrementalReseedIsMovingOldLogfiles.Log<string, string, string, string, string>(this.m_config.DatabaseName, this.m_config.ServerName, LogCopier.FormatLogGeneration(num), LogCopier.FormatLogGeneration(highestGenerationPresent), this.m_tempBackupPath);
			ExTraceGlobals.IncrementalReseederTracer.TraceDebug<long, long>((long)this.GetHashCode(), "SaveOldLogs: Moving from {0} to {1}", num, highestGenerationPresent);
			string text = this.GetE00TmpLogFilePath();
			if (File.Exists(text))
			{
				File.Move(text, Path.Combine(this.m_tempBackupPath, Path.GetFileName(text)));
			}
			text = this.GetE00LogFilePath();
			if (File.Exists(text))
			{
				File.Move(text, Path.Combine(this.m_tempBackupPath, Path.GetFileName(text)));
			}
			for (long num2 = highestGenerationPresent; num2 >= num; num2 -= 1L)
			{
				text = this.GetLogFilePath(this.m_destLogPath, num2);
				File.Move(text, Path.Combine(this.m_tempBackupPath, Path.GetFileName(text)));
			}
		}

		// Token: 0x06001CDE RID: 7390 RVA: 0x000823D8 File Offset: 0x000805D8
		private void PlaceNewLogs()
		{
			long lowestGenRequired = this.m_lowestGenRequired;
			long highestGenRequired = this.m_highestGenRequired;
			ReplayCrimsonEvents.IncrementalReseedIsMovingNewLogfiles.Log<string, string, string, string, string>(this.m_config.DatabaseName, this.m_config.ServerName, LogCopier.FormatLogGeneration(lowestGenRequired), LogCopier.FormatLogGeneration(highestGenRequired), this.m_tempLogPath);
			ExTraceGlobals.IncrementalReseederTracer.TraceDebug<long, long>((long)this.GetHashCode(), "PlaceNewLogs: Moving from {0} to {1}", lowestGenRequired, highestGenRequired);
			for (long num = lowestGenRequired; num <= highestGenRequired; num += 1L)
			{
				string logFilePath = this.GetLogFilePath(this.m_tempLogPath, num);
				if (File.Exists(logFilePath))
				{
					File.Move(logFilePath, Path.Combine(this.m_destLogPath, Path.GetFileName(logFilePath)));
				}
				else if (!this.m_fReusePatchFile)
				{
					string msg = string.Format("Assert: File '{0}' does not exist, but was expected.", logFilePath);
					throw new IncrementalReseedFailedException(msg, 0U);
				}
			}
		}

		// Token: 0x06001CDF RID: 7391 RVA: 0x00082498 File Offset: 0x00080698
		private string GetLogFilePath(string destinationDirectory, long generationNum)
		{
			return Path.Combine(destinationDirectory, EseHelper.MakeLogfileName(this.m_config.LogFilePrefix, this.m_logSuffix, generationNum));
		}

		// Token: 0x06001CE0 RID: 7392 RVA: 0x000824C4 File Offset: 0x000806C4
		public void CheckDiskSpaceAndThreshold(long srcHighestGen)
		{
			ExTraceGlobals.IncrementalReseederTracer.TraceDebug((long)this.GetHashCode(), "Enter CheckDiskSpaceAndThreshold");
			ulong freeSpace = this.GetFreeSpace(this.m_destLogPath);
			ulong freeSpace2 = this.GetFreeSpace(Path.GetDirectoryName(this.m_destDbPath));
			long num = (srcHighestGen - this.m_divergedPoint + 3L) * 1048576L;
			long num2 = this.m_pageList.LongLength * 32768L;
			if (num > (long)freeSpace)
			{
				throw new IncrementalReseedPrereqException(ReplayStrings.LogDriveNotBigEnough(this.m_destLogPath, num, freeSpace));
			}
			if (num2 > (long)freeSpace2)
			{
				throw new IncrementalReseedPrereqException(ReplayStrings.DbDriveNotBigEnough(this.m_destDbPath, num2, freeSpace2));
			}
			int num3 = RegistryParameters.IncSeedThreshold;
			if (num3 < 0 || num3 > 100)
			{
				ExTraceGlobals.IncrementalReseederTracer.TraceDebug<int>((long)this.GetHashCode(), "threshold value {0}, ignore it", num3);
				num3 = 100;
			}
			FileInfo fileInfo = new FileInfo(this.m_destDbPath);
			if ((num + num2) / fileInfo.Length * 100L > (long)num3)
			{
				throw new IncrementalReseedPrereqException(ReplayStrings.PreferFullReseed(num3));
			}
			ExTraceGlobals.IncrementalReseederTracer.TraceDebug((long)this.GetHashCode(), "Leave CheckDiskSpaceAndThreshold");
		}

		// Token: 0x06001CE1 RID: 7393 RVA: 0x000825DC File Offset: 0x000807DC
		private ulong GetFreeSpace(string path)
		{
			ulong result;
			ulong num;
			ulong num2;
			if (!NativeMethods.GetDiskFreeSpaceEx(path, out result, out num, out num2))
			{
				Win32Exception ex = new Win32Exception();
				this.m_setBroken.SetBroken(FailureTag.NoOp, ReplayEventLogConstants.Tuple_IncrementalReseedInitException, ex, new string[]
				{
					ReplayStrings.FailedToGetDiskSpace(path, ex.Message)
				});
				throw new SetBrokenControlTransferException();
			}
			return result;
		}

		// Token: 0x06001CE2 RID: 7394 RVA: 0x00082638 File Offset: 0x00080838
		private bool IsPagesReferencedInDivergentLogs()
		{
			bool flag = !this.m_fReusePatchFile && this.m_pageList != null && this.m_pageList.Length > 0;
			this.m_perfTracker.IsPagesReferencedInDivergentLogs = flag;
			return flag;
		}

		// Token: 0x06001CE3 RID: 7395 RVA: 0x00082671 File Offset: 0x00080871
		private void CheckStopPending()
		{
			if (this.m_stopCalled)
			{
				throw new OperationAbortedException();
			}
		}

		// Token: 0x06001CE4 RID: 7396 RVA: 0x00082684 File Offset: 0x00080884
		private void SleepTestHook()
		{
			if (this.m_delaySecs > 0)
			{
				try
				{
					ExTraceGlobals.IncrementalReseederTracer.TraceDebug<int>((long)this.GetHashCode(), "SleepTestHook: Sleeping {0} seconds started.", this.m_delaySecs);
					int num = Math.Min(this.m_delaySecs, 30);
					Thread.Sleep(num * 1000);
					ExTraceGlobals.IncrementalReseederTracer.TraceDebug<int>((long)this.GetHashCode(), "SleepTestHook: Actual sleep {0} seconds is complete.If we slept less time it is because there is a max cap for the sleep", num);
				}
				catch (ObjectDisposedException arg)
				{
					ExTraceGlobals.IncrementalReseederTracer.TraceDebug<ObjectDisposedException>((long)this.GetHashCode(), "SleepTestHook: {0}", arg);
				}
			}
		}

		// Token: 0x06001CE5 RID: 7397 RVA: 0x00082714 File Offset: 0x00080914
		private void ReadSleepTestHook()
		{
			this.m_delaySecs = RegistryTestHook.IncReseedDelayInSecs;
		}

		// Token: 0x06001CE6 RID: 7398 RVA: 0x00082724 File Offset: 0x00080924
		private void KeepLogChannelAlive()
		{
			DateTime utcNow = DateTime.UtcNow;
			if (utcNow > this.m_lastLogChannelSendTime + this.m_keepAliveInterval)
			{
				this.m_lastLogChannelSendTime = utcNow;
				this.m_logSource.QueryEndOfLog();
				IncrementalReseeder.UpdateKnownEndOfLogFromActive(this.m_logSource.CachedEndOfLog, this.m_logSource.CachedEndOfLogWriteTimeUtc, this.m_config.ReplayState, utcNow);
			}
		}

		// Token: 0x04000BDE RID: 3038
		private const int MaxSleep = 30;

		// Token: 0x04000BDF RID: 3039
		private const int PageSize = 32768;

		// Token: 0x04000BE0 RID: 3040
		private const int LogFileSize = 1048576;

		// Token: 0x04000BE1 RID: 3041
		private const int RetryTime = 3000;

		// Token: 0x04000BE2 RID: 3042
		private const string IncSeedStr = "incseed";

		// Token: 0x04000BE3 RID: 3043
		private const string DebuggerFileDir = "incseedDebug";

		// Token: 0x04000BE4 RID: 3044
		private const string PageNumberSuffix = ".pageNumber";

		// Token: 0x04000BE5 RID: 3045
		private const string ReseederLogFilePrefix = "TEMP.RESEEDER.";

		// Token: 0x04000BE6 RID: 3046
		private const string InspectorLogFilePrefix = "TEMP.INSPCTR.";

		// Token: 0x04000BE7 RID: 3047
		private const int CopyRetries = 3;

		// Token: 0x04000BE8 RID: 3048
		private static readonly ServerVersion FirstVersionSupportingMultiplePageRead = new ServerVersion(15, 0, 867, 0);

		// Token: 0x04000BE9 RID: 3049
		private int m_delaySecs;

		// Token: 0x04000BEA RID: 3050
		private bool m_runningAcll;

		// Token: 0x04000BEB RID: 3051
		private IReplayConfiguration m_config;

		// Token: 0x04000BEC RID: 3052
		private FileState m_fileState;

		// Token: 0x04000BED RID: 3053
		private ISetBroken m_setBroken;

		// Token: 0x04000BEE RID: 3054
		private ISetDisconnected m_setDisconnected;

		// Token: 0x04000BEF RID: 3055
		private ISetViable m_setViable;

		// Token: 0x04000BF0 RID: 3056
		private ManualOneShotEvent m_shuttingDownEvent;

		// Token: 0x04000BF1 RID: 3057
		private volatile bool m_stopCalled;

		// Token: 0x04000BF2 RID: 3058
		private long[] m_pageList;

		// Token: 0x04000BF3 RID: 3059
		private string m_destEdbFile;

		// Token: 0x04000BF4 RID: 3060
		private string m_srcEdbFile;

		// Token: 0x04000BF5 RID: 3061
		private long m_lowestGenRequired = -1L;

		// Token: 0x04000BF6 RID: 3062
		private long m_highestGenRequired = -1L;

		// Token: 0x04000BF7 RID: 3063
		private long m_startGen = -1L;

		// Token: 0x04000BF8 RID: 3064
		private EseDatabasePatchState m_patchHeader;

		// Token: 0x04000BF9 RID: 3065
		private long m_divergedPoint;

		// Token: 0x04000BFA RID: 3066
		private IPerfmonCounters m_perfmonCounters;

		// Token: 0x04000BFB RID: 3067
		private IncReseedPerformanceTracker m_perfTracker;

		// Token: 0x04000BFC RID: 3068
		private string m_tempLogPath;

		// Token: 0x04000BFD RID: 3069
		private string m_tempBackupPath;

		// Token: 0x04000BFE RID: 3070
		private string m_tempInspectPath;

		// Token: 0x04000BFF RID: 3071
		private string m_srcLogPath;

		// Token: 0x04000C00 RID: 3072
		private string m_destLogPath;

		// Token: 0x04000C01 RID: 3073
		private string m_destDbPath;

		// Token: 0x04000C02 RID: 3074
		private string m_e00LogBackupPath;

		// Token: 0x04000C03 RID: 3075
		private string m_dbName;

		// Token: 0x04000C04 RID: 3076
		private string m_srcNode;

		// Token: 0x04000C05 RID: 3077
		private string m_destSystemPath;

		// Token: 0x04000C06 RID: 3078
		private string m_logSuffix;

		// Token: 0x04000C07 RID: 3079
		private bool m_fReusePatchFile;

		// Token: 0x04000C08 RID: 3080
		private bool m_fPagesReferencedInDivergentRange;

		// Token: 0x04000C09 RID: 3081
		private LogSource m_logSource;

		// Token: 0x04000C0A RID: 3082
		private DateTime m_lastLogChannelSendTime = DateTime.UtcNow;

		// Token: 0x04000C0B RID: 3083
		private readonly TimeSpan m_keepAliveInterval = TimeSpan.FromSeconds((double)(RegistryParameters.TcpChannelIdleLimitInSec / 2));

		// Token: 0x04000C0C RID: 3084
		private EndOfLogInformation m_endOfLogInformation;

		// Token: 0x04000C0D RID: 3085
		private long m_sizeOfLocalDbBeforePatchInPagesExcludingHeader;

		// Token: 0x04000C0E RID: 3086
		private DatabaseVolumeInfo m_databaseVolumeInfo;

		// Token: 0x020002D7 RID: 727
		// (Invoke) Token: 0x06001CF9 RID: 7417
		internal delegate void ReseedOperation();
	}
}
