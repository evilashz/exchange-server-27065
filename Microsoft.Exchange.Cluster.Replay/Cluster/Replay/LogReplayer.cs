using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Common.HA;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Cluster;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;
using Microsoft.Exchange.Isam.Esebcli;
using Microsoft.Isam.Esent.Interop;
using Microsoft.Isam.Esent.Interop.Unpublished;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020002E3 RID: 739
	internal class LogReplayer : ShipControl
	{
		// Token: 0x06001D70 RID: 7536 RVA: 0x00085664 File Offset: 0x00083864
		public LogReplayer(IPerfmonCounters perfmonCounters, string fromPrefix, long fromNumber, string fromSuffix, IFileChecker fileChecker, IInspectLog inspectLogFile, ILogTruncater logTruncater, ISetBroken setBroken, ISetViable setViable, IReplayConfiguration configuration, ISetPassiveSeeding setPassiveSeeding, IReplicaProgress replicaProgress) : base(configuration.DestinationLogPath, fromPrefix, fromNumber, fromSuffix, setBroken, replicaProgress)
		{
			this.m_perfmonCounters = perfmonCounters;
			this.m_configuration = configuration;
			this.m_initialFromNumber = fromNumber;
			this.m_fileChecker = fileChecker;
			this.m_checkFilesForReplayHasRun = false;
			this.m_logTruncater = logTruncater;
			this.m_replayThreadWakeupEvent = new AutoResetEvent(false);
			this.m_replayLag = this.Configuration.ReplayLagTime;
			this.m_replayDir = this.Configuration.DestinationLogPath;
			this.m_waitForNextReplayLog = TimeSpan.FromMilliseconds((double)RegistryParameters.LogReplayerPauseDurationInMSecs);
			this.m_waitForNextStoreRpc = TimeSpan.FromMilliseconds((double)RegistryParameters.LogReplayerIdleStoreRpcIntervalInMSecs);
			this.m_setViable = setViable;
			this.m_stopCalled = false;
			this.m_setPassiveSeeding = setPassiveSeeding;
			this.m_queueHighPlayDownSuppression = new TransientErrorInfo();
			this.SuspendThreshold = RegistryParameters.LogReplayerSuspendThreshold;
			this.ResumeThreshold = RegistryParameters.LogReplayerResumeThreshold;
			this.UseSuspendLock = true;
			this.IsDismountControlledExternally = false;
			this.IsReplayLagDisabled = this.m_configuration.ReplayState.ReplayLagDisabled;
			this.m_logRepairWasPending = (this.State.LogRepairMode != LogRepairMode.Off);
			if (this.SuspendThreshold > this.ResumeThreshold)
			{
				this.IsCopyQueueBasedSuspendEnabled = true;
			}
			else
			{
				this.IsCopyQueueBasedSuspendEnabled = false;
			}
			if (RegistryParameters.LogReplayerDelayInMsec > 0)
			{
				this.m_testDelayEvent = new ManualResetEvent(false);
			}
			this.m_perfmonCounters.ReplayLagPercentage = (long)this.GetActualReplayLagPercentage();
			IMonitoringADConfigProvider monitoringADConfigProvider = Dependencies.MonitoringADConfigProvider;
			ICopyStatusClientLookup monitoringCopyStatusClientLookup = Dependencies.MonitoringCopyStatusClientLookup;
			this.m_dbScanControl = new LogReplayScanControl(configuration.Database, this.m_replayLag > TimeSpan.FromSeconds(0.0), monitoringADConfigProvider, monitoringCopyStatusClientLookup, this.m_perfmonCounters);
			LogReplayer.Tracer.TraceDebug<bool, int, int>((long)this.GetHashCode(), "Replayer suspendEnabled is {0} because SuspendThreshold={1}, ResumeThreshold={2}", this.IsCopyQueueBasedSuspendEnabled, this.SuspendThreshold, this.ResumeThreshold);
			ExTraceGlobals.LogReplayerTracer.TraceDebug((long)this.GetHashCode(), "LogReplayer initialized - fromDir = {0}, fromNumber = {1}, fromPrefix = {2}, replayLag = {3}", new object[]
			{
				configuration.DestinationLogPath,
				fromNumber,
				fromPrefix,
				configuration.ReplayLagTime.ToString()
			});
			ExTraceGlobals.PFDTracer.TracePfd((long)this.GetHashCode(), "PFD CRS {0} LogReplayer initialized - fromDir = {1}, fromNumber = {2}, fromPrefix = {3}, replayLag = {4}", new object[]
			{
				32413,
				configuration.DestinationLogPath,
				fromNumber,
				fromPrefix,
				configuration.ReplayLagTime.ToString()
			});
			this.Resume();
		}

		// Token: 0x170007D5 RID: 2005
		// (get) Token: 0x06001D71 RID: 7537 RVA: 0x00085914 File Offset: 0x00083B14
		public static Trace Tracer
		{
			get
			{
				return ExTraceGlobals.LogReplayerTracer;
			}
		}

		// Token: 0x06001D72 RID: 7538 RVA: 0x0008591C File Offset: 0x00083B1C
		internal static int GetActualReplayLagPercentage(IReplayConfiguration config)
		{
			DateTime utcNow = DateTime.UtcNow;
			int result = 0;
			TimeSpan timeSpan = TimeSpan.Zero;
			TimeSpan t = config.ReplayLagTime;
			if (t != EnhancedTimeSpan.Zero)
			{
				DateTime latestReplayTime = config.ReplayState.LatestReplayTime;
				if (latestReplayTime > ReplayState.ZeroFileTime && utcNow > latestReplayTime)
				{
					timeSpan = utcNow.Subtract(latestReplayTime);
				}
				result = Math.Min(100, (int)Math.Round(timeSpan.TotalMilliseconds / t.TotalMilliseconds * 100.0));
			}
			return result;
		}

		// Token: 0x06001D73 RID: 7539 RVA: 0x000859A8 File Offset: 0x00083BA8
		internal static bool DismountReplayDatabase(Guid dbGuid, string identity, string dbName, string localNodeName)
		{
			bool result = false;
			UnmountFlags unmountFlags = UnmountFlags.SkipCacheFlush;
			using (IStoreMountDismount storeMountDismountInstance = Dependencies.GetStoreMountDismountInstance(localNodeName))
			{
				try
				{
					ExTraceGlobals.LogReplayerTracer.TraceDebug<Guid, int>(0L, "UnmountDatabase called for {0} (flags={1}).", dbGuid, (int)unmountFlags);
					storeMountDismountInstance.UnmountDatabase(Guid.Empty, dbGuid, (int)unmountFlags);
					result = true;
				}
				catch (MapiExceptionNotFound arg)
				{
					ExTraceGlobals.LogReplayerTracer.TraceError<MapiExceptionNotFound>(0L, "UnmountDatabase: Ignoring MapiExceptionNotFound: {0}", arg);
					result = true;
				}
				catch (MapiRetryableException ex)
				{
					ExTraceGlobals.LogReplayerTracer.TraceError<MapiRetryableException>(0L, "UnmountDatabase Exception: {0}", ex);
					ReplayEventLogConstants.Tuple_LogReplayMapiException.LogEvent(identity, new object[]
					{
						dbName,
						ex.Message
					});
				}
				catch (MapiPermanentException ex2)
				{
					ExTraceGlobals.LogReplayerTracer.TraceError<MapiPermanentException>(0L, "LogReplay MapiPermanentException:{0}", ex2);
					ReplayEventLogConstants.Tuple_LogReplayMapiException.LogEvent(identity, new object[]
					{
						dbName,
						ex2.Message
					});
				}
			}
			return result;
		}

		// Token: 0x170007D6 RID: 2006
		// (get) Token: 0x06001D74 RID: 7540 RVA: 0x00085AB8 File Offset: 0x00083CB8
		public IReplayConfiguration Configuration
		{
			get
			{
				return this.m_configuration;
			}
		}

		// Token: 0x170007D7 RID: 2007
		// (get) Token: 0x06001D75 RID: 7541 RVA: 0x00085AC0 File Offset: 0x00083CC0
		public ILogTruncater LogTruncater
		{
			get
			{
				return this.m_logTruncater;
			}
		}

		// Token: 0x170007D8 RID: 2008
		// (get) Token: 0x06001D76 RID: 7542 RVA: 0x00085AC8 File Offset: 0x00083CC8
		private ReplayState State
		{
			get
			{
				return this.m_configuration.ReplayState;
			}
		}

		// Token: 0x170007D9 RID: 2009
		// (get) Token: 0x06001D77 RID: 7543 RVA: 0x00085AD5 File Offset: 0x00083CD5
		// (set) Token: 0x06001D78 RID: 7544 RVA: 0x00085ADD File Offset: 0x00083CDD
		internal string LocalNodeName { get; set; }

		// Token: 0x170007DA RID: 2010
		// (get) Token: 0x06001D79 RID: 7545 RVA: 0x00085AE8 File Offset: 0x00083CE8
		private IStoreRpc StoreReplayController
		{
			get
			{
				lock (this.m_storeLocker)
				{
					if (this.m_storeReplayController != null)
					{
						return this.m_storeReplayController;
					}
					this.m_storeReplayController = Dependencies.GetNewStoreControllerInstance(this.LocalNodeName);
					if (this.m_setPassiveSeeding.PassiveSeedingSourceContext != PassiveSeedingSourceContextEnum.Database)
					{
						LogReplayer.Tracer.TraceDebug<string>((long)this.GetHashCode(), "{0}: Mount starts", this.DatabaseName);
						this.m_storeReplayController.MountDatabase(Guid.Empty, this.Configuration.IdentityGuid, 8);
						LogReplayer.Tracer.TraceDebug<string>((long)this.GetHashCode(), "{0}: Mount complete", this.DatabaseName);
						this.IsDismountPending = true;
						int workerProcessId;
						int num;
						int num2;
						int num3;
						this.m_storeReplayController.GetDatabaseProcessInfo(this.Configuration.IdentityGuid, out workerProcessId, out num, out num2, out num3);
						this.State.WorkerProcessId = workerProcessId;
					}
					else
					{
						ExTraceGlobals.LogReplayerTracer.TraceDebug<Guid>((long)this.GetHashCode(), "Didn't MountDatabase for {0} because it is being a seeding source.", this.Configuration.IdentityGuid);
					}
				}
				return this.m_storeReplayController;
			}
		}

		// Token: 0x170007DB RID: 2011
		// (get) Token: 0x06001D7A RID: 7546 RVA: 0x00085C10 File Offset: 0x00083E10
		// (set) Token: 0x06001D7B RID: 7547 RVA: 0x00085C18 File Offset: 0x00083E18
		internal bool IsDismountPending { get; set; }

		// Token: 0x170007DC RID: 2012
		// (get) Token: 0x06001D7C RID: 7548 RVA: 0x00085C21 File Offset: 0x00083E21
		internal long MaxLogToReplay
		{
			get
			{
				return this.m_highestGenerationToReplay;
			}
		}

		// Token: 0x170007DD RID: 2013
		// (get) Token: 0x06001D7D RID: 7549 RVA: 0x00085C29 File Offset: 0x00083E29
		// (set) Token: 0x06001D7E RID: 7550 RVA: 0x00085C31 File Offset: 0x00083E31
		private int SuspendThreshold { get; set; }

		// Token: 0x170007DE RID: 2014
		// (get) Token: 0x06001D7F RID: 7551 RVA: 0x00085C3A File Offset: 0x00083E3A
		// (set) Token: 0x06001D80 RID: 7552 RVA: 0x00085C42 File Offset: 0x00083E42
		private int ResumeThreshold { get; set; }

		// Token: 0x170007DF RID: 2015
		// (get) Token: 0x06001D81 RID: 7553 RVA: 0x00085C4B File Offset: 0x00083E4B
		// (set) Token: 0x06001D82 RID: 7554 RVA: 0x00085C53 File Offset: 0x00083E53
		private bool IsCopyQueueBasedSuspendEnabled { get; set; }

		// Token: 0x170007E0 RID: 2016
		// (get) Token: 0x06001D83 RID: 7555 RVA: 0x00085C5C File Offset: 0x00083E5C
		// (set) Token: 0x06001D84 RID: 7556 RVA: 0x00085C64 File Offset: 0x00083E64
		internal bool UseSuspendLock { get; set; }

		// Token: 0x170007E1 RID: 2017
		// (get) Token: 0x06001D85 RID: 7557 RVA: 0x00085C6D File Offset: 0x00083E6D
		// (set) Token: 0x06001D86 RID: 7558 RVA: 0x00085C75 File Offset: 0x00083E75
		internal bool IsDismountControlledExternally { get; set; }

		// Token: 0x170007E2 RID: 2018
		// (get) Token: 0x06001D87 RID: 7559 RVA: 0x00085C7E File Offset: 0x00083E7E
		// (set) Token: 0x06001D88 RID: 7560 RVA: 0x00085C86 File Offset: 0x00083E86
		private bool IsReplayLagDisabled { get; set; }

		// Token: 0x170007E3 RID: 2019
		// (get) Token: 0x06001D89 RID: 7561 RVA: 0x00085C8F File Offset: 0x00083E8F
		private string DatabaseName
		{
			get
			{
				return this.m_configuration.DatabaseName;
			}
		}

		// Token: 0x06001D8A RID: 7562 RVA: 0x00085C9C File Offset: 0x00083E9C
		public override Result ShipAction(long logFileNumber)
		{
			ExTraceGlobals.LogReplayerTracer.TraceDebug<long>((long)this.GetHashCode(), "ShipAction called, logFileNumber = {0}", logFileNumber);
			ExTraceGlobals.PFDTracer.TracePfd<int, long>((long)this.GetHashCode(), "PFD CRS {0} ShipAction called, logFileNumber = {1}", 28317, logFileNumber);
			lock (this)
			{
				this.SetHighestGenerationAvailable(logFileNumber);
			}
			return Result.Success;
		}

		// Token: 0x06001D8B RID: 7563 RVA: 0x00085D0C File Offset: 0x00083F0C
		public override void LogError(string inputFile, Exception ex)
		{
			ExTraceGlobals.LogReplayerTracer.TraceError<string, Exception>((long)this.GetHashCode(), "Log Replay error Input file is: {0}" + Environment.NewLine + "Exception received was: {1}", inputFile, ex);
		}

		// Token: 0x06001D8C RID: 7564 RVA: 0x00085D38 File Offset: 0x00083F38
		internal void WaitForIdle()
		{
			ExTraceGlobals.LogReplayerTracer.TraceDebug((long)this.GetHashCode(), "WaitForIdle()");
			Thread thread = this.m_thread;
			if (thread != null)
			{
				thread.Join();
			}
		}

		// Token: 0x06001D8D RID: 7565 RVA: 0x00085D6B File Offset: 0x00083F6B
		internal void Suspend()
		{
			this.State.ReplaySuspended = true;
		}

		// Token: 0x06001D8E RID: 7566 RVA: 0x00085D79 File Offset: 0x00083F79
		internal void Resume()
		{
			this.State.ReplaySuspended = false;
		}

		// Token: 0x06001D8F RID: 7567 RVA: 0x00085D87 File Offset: 0x00083F87
		protected override void PrepareToStopInternal()
		{
			ExTraceGlobals.LogReplayerTracer.TraceDebug((long)this.GetHashCode(), "PrepareToStop()");
			this.m_stopCalled = true;
			this.WakeupReplayer();
			if (this.m_testDelayEvent != null)
			{
				this.m_testDelayEvent.Set();
			}
		}

		// Token: 0x06001D90 RID: 7568 RVA: 0x00085DC0 File Offset: 0x00083FC0
		protected override void StopInternal()
		{
			if (!this.m_fStopped)
			{
				Thread thread = this.m_thread;
				if (thread != null)
				{
					thread.Join();
					this.m_thread = null;
				}
				this.m_fStopped = true;
				lock (this.m_replayThreadWakeupEventLocker)
				{
					if (this.m_replayThreadWakeupEvent != null)
					{
						this.m_replayThreadWakeupEvent.Close();
						this.m_replayThreadWakeupEvent = null;
					}
				}
				this.CloseReplayController();
				base.EndOfStopTime = DateTime.UtcNow;
				TimerComponent.LogStopEvent("LogReplayer", this.Configuration.DatabaseName, base.PrepareToStopTime, base.StartOfStopTime, base.EndOfStopTime);
			}
			ExTraceGlobals.LogReplayerTracer.TraceDebug((long)this.GetHashCode(), "LogReplayer Stop ()");
			ExTraceGlobals.PFDTracer.TracePfd<int>((long)this.GetHashCode(), "PFD CRS {0} LogReplayer Stopped ()", 24221);
		}

		// Token: 0x06001D91 RID: 7569 RVA: 0x00085EA8 File Offset: 0x000840A8
		protected override void TestDelaySleep()
		{
			int logReplayerDelayInMsec = RegistryParameters.LogReplayerDelayInMsec;
			if (logReplayerDelayInMsec > 0 && this.m_testDelayEvent != null)
			{
				this.m_testDelayEvent.WaitOne(logReplayerDelayInMsec);
			}
		}

		// Token: 0x06001D92 RID: 7570 RVA: 0x00085ED4 File Offset: 0x000840D4
		protected override Result InitializeStartContext()
		{
			ExTraceGlobals.LogReplayerTracer.TraceDebug((long)this.GetHashCode(), "InitializeStartContext()");
			ExTraceGlobals.PFDTracer.TracePfd<int, LogReplayer>((long)this.GetHashCode(), "PFD CRS {0} InitializeStartContext() for {1} ", 22685, this);
			return Result.Success;
		}

		// Token: 0x06001D93 RID: 7571 RVA: 0x00085F0C File Offset: 0x0008410C
		private static FailureTag ChooseFailureTag(MapiPermanentException ex)
		{
			if (ex is MapiExceptionNotFound || ex is MapiExceptionMdbOffline)
			{
				return FailureTag.NoOp;
			}
			if (!(ex is MapiExceptionCallFailed))
			{
				return FailureTag.NoOp;
			}
			int lowLevelError = ex.LowLevelError;
			EsentErrorException ex2 = EsentExceptionHelper.JetErrToException((JET_err)lowLevelError);
			if (ex2 is EsentDiskReadVerificationFailureException)
			{
				return FailureTag.RecoveryRedoLogCorruption;
			}
			return ReplicaInstance.TargetIsamErrorExceptionToFailureTag(ex2);
		}

		// Token: 0x06001D94 RID: 7572 RVA: 0x00085F54 File Offset: 0x00084154
		private bool TryGetSuspendLock()
		{
			LockOwner lockOwner;
			if (!this.State.SuspendLock.TryEnter(LockOwner.Component, false, out lockOwner))
			{
				ExTraceGlobals.LogReplayerTracer.TraceDebug((long)this.GetHashCode(), "unable to get SuspendLock, restarting the instance");
				if (lockOwner != LockOwner.AttemptCopyLastLogs)
				{
					ExTraceGlobals.LogReplayerTracer.TraceDebug<LockOwner>((long)this.GetHashCode(), "unable to get SuspendLock, current owner: {0}, stopping the instance", lockOwner);
					this.m_setBroken.RestartInstanceSoon(true);
				}
				else
				{
					this.m_setBroken.RestartInstanceSoon(false);
				}
			}
			else
			{
				this.m_haveSuspendLock = true;
			}
			return this.m_haveSuspendLock;
		}

		// Token: 0x06001D95 RID: 7573 RVA: 0x00085FD3 File Offset: 0x000841D3
		private void ReleaseSuspendLock()
		{
			if (this.m_haveSuspendLock)
			{
				this.State.SuspendLock.Leave(LockOwner.Component);
				this.m_haveSuspendLock = false;
			}
		}

		// Token: 0x06001D96 RID: 7574 RVA: 0x00085FF8 File Offset: 0x000841F8
		private bool CheckFilesForReplay()
		{
			if (this.m_checkFilesForReplayHasRun)
			{
				return true;
			}
			try
			{
				if (!this.m_fileChecker.RecalculateRequiredGenerations())
				{
					return false;
				}
				if (0L == this.m_fileChecker.FileState.HighestGenerationPresent)
				{
					ExTraceGlobals.LogReplayerTracer.TraceError((long)this.GetHashCode(), "No logfiles. Replay cannot run");
					return false;
				}
				if (!this.m_fileChecker.FileState.RequiredLogfilesArePresent())
				{
					ExTraceGlobals.LogReplayerTracer.TraceError((long)this.GetHashCode(), "Required logfiles are not present. Replay requires generations {0} through {1}. Found generations {2} through {3}. Replay cannot be run until all required logfiles are present.", new object[]
					{
						this.m_fileChecker.FileState.LowestGenerationRequired,
						this.m_fileChecker.FileState.HighestGenerationRequired,
						this.m_fileChecker.FileState.LowestGenerationPresent,
						this.m_fileChecker.FileState.HighestGenerationPresent
					});
					return false;
				}
				if (!this.m_fileChecker.CheckRequiredLogfilesForPassiveOrInconsistentDatabase(true))
				{
					return false;
				}
				this.m_setViable.SetViable();
				this.m_fileChecker.CheckCheckpoint();
			}
			catch (FileCheckLogfileMissingException arg)
			{
				ExTraceGlobals.LogReplayerTracer.TraceError<FileCheckLogfileMissingException>((long)this.GetHashCode(), "FileCheckException thrown in CheckFilesForReplay. Exception is: {0}. We will wait for the log file to arrive", arg);
				return false;
			}
			catch (FileCheckException ex)
			{
				ExTraceGlobals.LogReplayerTracer.TraceError<FileCheckException>((long)this.GetHashCode(), "FileCheckException thrown in CheckFilesForReplay. Exception is: {0}", ex);
				this.m_setBroken.SetBroken(ReplicaInstance.TargetFileCheckerExceptionToFailureTag(ex), ReplayEventLogConstants.Tuple_FileCheckError, ex, new string[]
				{
					ex.ToString()
				});
				return false;
			}
			this.m_highestGenerationToReplay = this.m_fileChecker.FileState.LowestGenerationPresent;
			this.m_checkFilesForReplayHasRun = true;
			return true;
		}

		// Token: 0x06001D97 RID: 7575 RVA: 0x000861D4 File Offset: 0x000843D4
		private bool PostReplayDatabaseCheck()
		{
			string destinationEdbPath = this.m_configuration.DestinationEdbPath;
			if (!File.Exists(destinationEdbPath))
			{
				ExTraceGlobals.LogReplayerTracer.TraceError<string>((long)this.GetHashCode(), "PostReplayChecks failed. Database {0} doesn't exist.", destinationEdbPath);
				this.m_setBroken.SetBroken(FailureTag.Reseed, ReplayEventLogConstants.Tuple_DatabaseNotPresentAfterReplay, new string[]
				{
					destinationEdbPath
				});
				return false;
			}
			return true;
		}

		// Token: 0x06001D98 RID: 7576 RVA: 0x0008622D File Offset: 0x0008442D
		private int GetActualReplayLagPercentage()
		{
			return LogReplayer.GetActualReplayLagPercentage(this.Configuration);
		}

		// Token: 0x06001D99 RID: 7577 RVA: 0x0008623C File Offset: 0x0008443C
		private void ReplayCompleted(long lastLogReplayed, ref JET_DBINFOMISC dbinfo)
		{
			ExTraceGlobals.LogReplayerTracer.TraceDebug<long, int, int>((long)this.GetHashCode(), "ReplayCompleted. LastLogReplayed={0}, dbinfo.Version={1}, dbinfo.genMaxRequired={2}", lastLogReplayed, dbinfo.ulVersion, dbinfo.genMaxRequired);
			long replayGenerationNumber = this.State.ReplayGenerationNumber;
			if (lastLogReplayed > replayGenerationNumber)
			{
				if (this.m_logRepairWasPending)
				{
					if (!LogRepair.ExitLogRepair(this.Configuration.IdentityGuid))
					{
						this.m_setBroken.RestartInstanceNow(ReplayConfigChangeHints.LogReplayerHitLogCorruption);
					}
					this.m_logRepairWasPending = false;
				}
				this.m_replicaProgress.ReportLogsReplayed(lastLogReplayed - replayGenerationNumber);
			}
			this.State.ReplayGenerationNumber = lastLogReplayed;
			string filenameFromGenerationNumber = base.GetFilenameFromGenerationNumber(lastLogReplayed);
			string logfile = Path.Combine(this.m_replayDir, filenameFromGenerationNumber);
			DateTime filetimeOfLog = base.GetFiletimeOfLog(logfile);
			this.m_configuration.ReplayState.LatestReplayTime = filetimeOfLog;
			if (this.m_configuration.ReplayState.ReplayGenerationNumber < this.m_highestGenerationToReplay)
			{
				this.m_configuration.ReplayState.CurrentReplayTime = filetimeOfLog;
			}
			if (this.PostReplayDatabaseCheck() && dbinfo.ulVersion != 0)
			{
				this.m_fileChecker.RecalculateRequiredGenerations(ref dbinfo);
				if (this.m_fileChecker.FileState.LatestFullBackupTime != null)
				{
					this.State.LatestFullBackupTime = this.m_fileChecker.FileState.LatestFullBackupTime.Value.ToUniversalTime();
				}
				if (this.m_fileChecker.FileState.LatestIncrementalBackupTime != null)
				{
					this.State.LatestIncrementalBackupTime = this.m_fileChecker.FileState.LatestIncrementalBackupTime.Value.ToUniversalTime();
				}
				if (this.m_fileChecker.FileState.LatestDifferentialBackupTime != null)
				{
					this.State.LatestDifferentialBackupTime = this.m_fileChecker.FileState.LatestDifferentialBackupTime.Value.ToUniversalTime();
				}
				if (this.m_fileChecker.FileState.LatestCopyBackupTime != null)
				{
					this.State.LatestCopyBackupTime = this.m_fileChecker.FileState.LatestCopyBackupTime.Value.ToUniversalTime();
				}
				if (this.m_fileChecker.FileState.SnapshotBackup != null)
				{
					this.State.SnapshotBackup = this.m_fileChecker.FileState.SnapshotBackup.Value;
				}
				if (this.m_fileChecker.FileState.SnapshotLatestFullBackup != null)
				{
					this.State.SnapshotLatestFullBackup = this.m_fileChecker.FileState.SnapshotLatestFullBackup.Value;
				}
				if (this.m_fileChecker.FileState.SnapshotLatestIncrementalBackup != null)
				{
					this.State.SnapshotLatestIncrementalBackup = this.m_fileChecker.FileState.SnapshotLatestIncrementalBackup.Value;
				}
				if (this.m_fileChecker.FileState.SnapshotLatestDifferentialBackup != null)
				{
					this.State.SnapshotLatestDifferentialBackup = this.m_fileChecker.FileState.SnapshotLatestDifferentialBackup.Value;
				}
				if (this.m_fileChecker.FileState.SnapshotLatestCopyBackup != null)
				{
					this.State.SnapshotLatestCopyBackup = this.m_fileChecker.FileState.SnapshotLatestCopyBackup.Value;
				}
				if (this.LogTruncater != null)
				{
					long genRequired = Math.Min((long)dbinfo.genMinRequired, lastLogReplayed);
					this.LogTruncater.RecordReplayGeneration(genRequired);
				}
			}
		}

		// Token: 0x06001D9A RID: 7578 RVA: 0x000865C4 File Offset: 0x000847C4
		private LogReplayRpcFlags ChooseReplayFlags()
		{
			LogReplayRpcFlags result = LogReplayRpcFlags.SetDbScan | LogReplayRpcFlags.EnableDbScan;
			bool isFastLagPlaydownDesired = false;
			long replayQ;
			if (this.m_replayLag > TimeSpan.FromSeconds(0.0))
			{
				if (this.m_fPlayingDownLag || this.IsReplayLagDisabled)
				{
					isFastLagPlaydownDesired = true;
					replayQ = this.CurrentReplayQueue();
				}
				else
				{
					replayQ = Math.Max(0L, this.m_highestGenerationToReplay - this.State.ReplayGenerationNumber);
				}
			}
			else
			{
				replayQ = this.CurrentReplayQueue();
			}
			if (!this.m_dbScanControl.ShouldBeEnabled(isFastLagPlaydownDesired, replayQ))
			{
				result = LogReplayRpcFlags.SetDbScan;
			}
			return result;
		}

		// Token: 0x06001D9B RID: 7579 RVA: 0x00086654 File Offset: 0x00084854
		private void LogReplayRpc(Guid mdbGuid, long replayGen)
		{
			ExTraceGlobals.LogReplayerTracer.TraceDebug<Guid, long>((long)this.GetHashCode(), "LogReplayRpc entered for mdbGuid={0}, generation={1}", mdbGuid, replayGen);
			ExTraceGlobals.FaultInjectionTracer.TraceTest(3905301821U);
			try
			{
				LogReplayRpcFlags logReplayRpcFlags = this.ChooseReplayFlags();
				ExTraceGlobals.LogReplayerTracer.TraceDebug<string, LogReplayRpcFlags>((long)this.GetHashCode(), "LogReplayRpc({0}) flags=0x{1:X}", this.DatabaseName, logReplayRpcFlags);
				uint num;
				JET_DBINFOMISC jet_DBINFOMISC;
				IPagePatchReply pagePatchReply;
				uint[] array;
				lock (this.m_storeLocker)
				{
					this.StoreReplayController.LogReplayRequest(mdbGuid, (uint)replayGen, (uint)logReplayRpcFlags, out num, out jet_DBINFOMISC, out pagePatchReply, out array);
				}
				if (pagePatchReply != null)
				{
					ThreadPool.QueueUserWorkItem(delegate(object obj)
					{
						this.SendPageToActive(obj as IPagePatchReply);
					}, pagePatchReply);
				}
				if (num > 1U)
				{
					this.ReplayCompleted((long)((ulong)(num - 1U)), ref jet_DBINFOMISC);
				}
				else
				{
					ExTraceGlobals.LogReplayerTracer.TraceDebug((long)this.GetHashCode(), "LogReplayRpc skipped ReplayCompleted as nextLogToReplay is zero or 1.");
				}
				if (array != null)
				{
					this.PatchPassiveDatabase(array);
				}
			}
			catch (MapiExceptionNotFound mapiExceptionNotFound)
			{
				ExTraceGlobals.LogReplayerTracer.TraceError<MapiExceptionNotFound>((long)this.GetHashCode(), "LogReplay MapiExceptionNotFound:{0}", mapiExceptionNotFound);
				if (this.m_setPassiveSeeding.PassiveSeedingSourceContext != PassiveSeedingSourceContextEnum.Database)
				{
					this.m_setBroken.SetBroken(LogReplayer.ChooseFailureTag(mapiExceptionNotFound), ReplayEventLogConstants.Tuple_LogReplayMapiException, mapiExceptionNotFound, new string[]
					{
						mapiExceptionNotFound.Message
					});
				}
				else
				{
					ExTraceGlobals.LogReplayerTracer.TraceDebug((long)this.GetHashCode(), "LogReplay MapiExceptionNotFound is ignored and SetBroken() skipped because instance is a passive seeding source.");
				}
			}
			catch (MapiExceptionMdbOffline mapiExceptionMdbOffline)
			{
				ExTraceGlobals.LogReplayerTracer.TraceError<MapiExceptionMdbOffline>((long)this.GetHashCode(), "LogReplay MapiExceptionMdbOffline:{0}", mapiExceptionMdbOffline);
				if (this.m_setPassiveSeeding.PassiveSeedingSourceContext != PassiveSeedingSourceContextEnum.Database)
				{
					this.m_setBroken.SetBroken(LogReplayer.ChooseFailureTag(mapiExceptionMdbOffline), ReplayEventLogConstants.Tuple_LogReplayMapiException, mapiExceptionMdbOffline, new string[]
					{
						mapiExceptionMdbOffline.Message
					});
				}
				else
				{
					ExTraceGlobals.LogReplayerTracer.TraceDebug((long)this.GetHashCode(), "LogReplay MapiExceptionMdbOffline is ignored and SetBroken() skipped because instance is a passive seeding source.");
				}
			}
			catch (MapiRetryableException ex)
			{
				ExTraceGlobals.LogReplayerTracer.TraceError<MapiRetryableException>((long)this.GetHashCode(), "LogReplay MapiRetryableException:{0}", ex);
				if (this.m_setPassiveSeeding.PassiveSeedingSourceContext != PassiveSeedingSourceContextEnum.Database)
				{
					this.m_setBroken.SetBroken(FailureTag.NoOp, ReplayEventLogConstants.Tuple_LogReplayMapiException, ex, new string[]
					{
						ex.Message
					});
				}
				else
				{
					ExTraceGlobals.LogReplayerTracer.TraceDebug((long)this.GetHashCode(), "LogReplay MapiRetryableException is ignored and SetBroken() skipped because instance is a passive seeding source.");
				}
			}
			catch (MapiPermanentException ex2)
			{
				ExTraceGlobals.LogReplayerTracer.TraceError<MapiPermanentException>((long)this.GetHashCode(), "LogReplay MapiPermanentException:{0}", ex2);
				this.m_setBroken.SetBroken(LogReplayer.ChooseFailureTag(ex2), ReplayEventLogConstants.Tuple_LogReplayMapiException, ex2, new string[]
				{
					ex2.Message
				});
			}
			catch (UnauthorizedAccessException ex3)
			{
				ExTraceGlobals.LogReplayerTracer.TraceError<UnauthorizedAccessException>((long)this.GetHashCode(), "LogReplay UnauthorizedAccessException:{0}", ex3);
				this.m_setBroken.SetBroken(FailureTag.Configuration, ReplayEventLogConstants.Tuple_LogReplayMapiException, ex3, new string[]
				{
					ex3.Message
				});
			}
			finally
			{
				this.m_lastStoreRpcUTC = DateTime.UtcNow;
			}
			ExTraceGlobals.LogReplayerTracer.TraceDebug<Guid, long>((long)this.GetHashCode(), "LogReplayRpc completed for mdbGuid={0}, generation={1}", mdbGuid, replayGen);
		}

		// Token: 0x06001D9C RID: 7580 RVA: 0x000869F4 File Offset: 0x00084BF4
		internal void SetReportingCallback(ISetBroken setBroken)
		{
			this.m_shipLogsSetBroken.SetReportingCallbacksForAcll(setBroken, null);
		}

		// Token: 0x06001D9D RID: 7581 RVA: 0x00086A04 File Offset: 0x00084C04
		internal void SendFinalLogReplayRequest()
		{
			ExTraceGlobals.LogReplayerTracer.TraceDebug((long)this.GetHashCode(), "ReplayFinalLogsDuringAcll() called");
			this.m_fAttemptFinalCopyCalled = true;
			lock (this)
			{
				if (base.ShiplogsActive)
				{
					LogReplayer.Tracer.TraceDebug((long)this.GetHashCode(), "Another ShipLogs() is running, creating GoingIdleEvent");
					base.GoingIdleEvent = new ManualResetEvent(false);
				}
			}
			if (base.GoingIdleEvent != null)
			{
				ExTraceGlobals.LogReplayerTracer.TraceDebug((long)this.GetHashCode(), "ReplayFinalLogsDuringAcll is waiting on GoingIdleEvent");
				base.GoingIdleEvent.WaitOne();
				ExTraceGlobals.LogReplayerTracer.TraceDebug((long)this.GetHashCode(), "ReplayFinalLogsDuringAcll: GoingIdleEvent was set");
			}
			ExTraceGlobals.LogReplayerTracer.TraceDebug((long)this.GetHashCode(), "ReplayFinalLogsDuringAcll: now running ShipLogs()");
			base.ShipLogs(true);
		}

		// Token: 0x06001D9E RID: 7582 RVA: 0x00086AE0 File Offset: 0x00084CE0
		private void PatchPassiveDatabase(IEnumerable<uint> corruptedPages)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("PatchPassiveDatabase(");
			foreach (uint num in corruptedPages)
			{
				stringBuilder.AppendFormat(" {0} ", num);
			}
			stringBuilder.Append(")");
			ExTraceGlobals.LogReplayerTracer.TraceDebug((long)this.GetHashCode(), stringBuilder.ToString());
			this.PublishPageFailureItems(corruptedPages, IoErrorCategory.Read, FailureTag.PagePatchRequested);
			lock (this.m_storeLocker)
			{
				this.StoreReplayController.UnmountDatabase(Guid.Empty, this.Configuration.IdentityGuid, 16);
				this.CloseReplayController();
			}
			Exception ex = null;
			FailureTag failureTag = FailureTag.ReplayFailedToPagePatch;
			ExEventLog.EventTuple setBrokenEventTuple = ReplayEventLogConstants.Tuple_LogReplayPatchFailedPrepareException;
			bool flag2 = false;
			try
			{
				this.StopTruncationWithRetry();
				this.GetPagesAndUpdateDatabase(corruptedPages);
				flag2 = true;
				this.PublishPageFailureItems(corruptedPages, IoErrorCategory.None, FailureTag.PagePatchCompleted);
			}
			catch (LogTruncationException ex2)
			{
				ex = ex2;
				string message = string.Format("Error occurred while stopping global truncation: {0}", ex2);
				ExTraceGlobals.LogReplayerTracer.TraceError((long)this.GetHashCode(), message);
				failureTag = FailureTag.NoOp;
			}
			catch (EsebcliException ex3)
			{
				ex = ex3;
				string message2 = string.Format("Esebcli error when retrieving pages for patching passive: {0}", ex3);
				ExTraceGlobals.LogReplayerTracer.TraceError((long)this.GetHashCode(), message2);
				failureTag = FailureTag.NoOp;
			}
			catch (EsentErrorException ex4)
			{
				ex = ex4;
				failureTag = ReplicaInstance.TargetIsamErrorExceptionToFailureTag(ex4);
				setBrokenEventTuple = ReplayEventLogConstants.Tuple_LogReplayPatchFailedIsamException;
				LogReplayer.Tracer.TraceError<FailureTag, EsentErrorException>((long)this.GetHashCode(), "PatchPassiveDatabase failed: FailTag: {0} Ex: {1}", failureTag, ex4);
			}
			finally
			{
				if (!flag2)
				{
					this.m_setBroken.SetBroken(failureTag, setBrokenEventTuple, ex, new string[]
					{
						ex.ToString()
					});
				}
				else
				{
					this.m_setBroken.RestartInstanceSoon(false);
				}
			}
		}

		// Token: 0x06001D9F RID: 7583 RVA: 0x00086CE0 File Offset: 0x00084EE0
		private void PublishPageFailureItems(IEnumerable<uint> pages, IoErrorCategory category, FailureTag tag)
		{
			foreach (uint num in pages)
			{
				long offset = (long)((ulong)(num + 1U) * 32768UL);
				IoErrorInfo ioErrorInfo = new IoErrorInfo(category, this.Configuration.DestinationEdbPath, offset, 32768L);
				Dependencies.FailureItemPublisher.PublishAction(tag, this.Configuration.IdentityGuid, this.Configuration.DatabaseName, ioErrorInfo);
			}
		}

		// Token: 0x06001DA0 RID: 7584 RVA: 0x00086D68 File Offset: 0x00084F68
		private void StopTruncationWithRetry()
		{
			int num = 0;
			try
			{
				IL_04:
				this.LogTruncater.StopTruncation();
			}
			catch (FailedToOpenLogTruncContextException ex)
			{
				Exception ex2;
				string message;
				if (ex.TryGetExceptionOrInnerOfType(out ex2))
				{
					message = string.Format("Shutting down exception on attempt {1} to stop global truncation for page patching. Exception: {0}", ex, num);
					ExTraceGlobals.LogReplayerTracer.TraceError((long)this.GetHashCode(), message);
					throw;
				}
				message = string.Format("Error {0} on attempt {1} to stop global truncation for page patching", ex, num);
				ExTraceGlobals.LogReplayerTracer.TraceError((long)this.GetHashCode(), message);
				if (++num == 10)
				{
					throw;
				}
				Thread.Sleep(3000);
				goto IL_04;
			}
			catch (LogTruncationException arg)
			{
				string message = string.Format("Error {0} on attempt {1} to stop global truncation for page patching", arg, num);
				ExTraceGlobals.LogReplayerTracer.TraceError((long)this.GetHashCode(), message);
				if (++num == 10)
				{
					throw;
				}
				Thread.Sleep(3000);
				goto IL_04;
			}
		}

		// Token: 0x06001DA1 RID: 7585 RVA: 0x00086E54 File Offset: 0x00085054
		private void GetPagesAndUpdateDatabase(IEnumerable<uint> corruptedPages)
		{
			int num;
			int num2;
			Dictionary<uint, byte[]> pagesWithRetry = this.GetPagesWithRetry(corruptedPages, out num, out num2);
			string message = string.Format("Waiting for generation {0}", num2);
			ExTraceGlobals.LogReplayerTracer.TraceDebug((long)this.GetHashCode(), message);
			while (!this.ShouldCancelReplay() && this.m_highestGenerationAvailable < (long)num2)
			{
				this.ReplayPause(new long?((long)num2));
			}
			ExTraceGlobals.FaultInjectionTracer.TraceTest(2414226749U);
			if (!this.ShouldCancelReplay())
			{
				this.UpdateDatabaseWithPages(pagesWithRetry, (long)num, (long)num2);
			}
		}

		// Token: 0x06001DA2 RID: 7586 RVA: 0x00086ED4 File Offset: 0x000850D4
		private Dictionary<uint, byte[]> GetPagesWithRetry(IEnumerable<uint> corruptedPages, out int lowestGenRequired, out int highestGenRequired)
		{
			int num = 0;
			Dictionary<uint, byte[]> pages;
			try
			{
				IL_02:
				pages = this.GetPages(corruptedPages, out highestGenRequired, out lowestGenRequired);
			}
			catch (EsebcliException arg)
			{
				string message = string.Format("Error {0} on attempt {1} to get pages for page patching", arg, num);
				ExTraceGlobals.LogReplayerTracer.TraceError((long)this.GetHashCode(), message);
				if (++num == 10)
				{
					throw;
				}
				Thread.Sleep(3000);
				goto IL_02;
			}
			return pages;
		}

		// Token: 0x06001DA3 RID: 7587 RVA: 0x00086F3C File Offset: 0x0008513C
		private Dictionary<uint, byte[]> GetPages(IEnumerable<uint> corruptedPages, out int highestGenRequired, out int lowestGenRequired)
		{
			lowestGenRequired = int.MaxValue;
			highestGenRequired = 0;
			AmServerName amServerName = new AmServerName(this.Configuration.SourceMachine);
			string text;
			IEsebcli esebcli = this.GetEsebcli(amServerName, out text);
			Dictionary<uint, byte[]> dictionary = new Dictionary<uint, byte[]>();
			foreach (uint num in corruptedPages)
			{
				if (this.ShouldCancelReplay())
				{
					break;
				}
				string message = string.Format("Getting page {0} from server {1}", num, amServerName);
				ExTraceGlobals.LogReplayerTracer.TraceDebug((long)this.GetHashCode(), message);
				int val = 0;
				int val2 = 0;
				dictionary[num] = esebcli.GetDatabasePage(text, this.Configuration.DatabaseName, this.Configuration.DatabaseName, (int)num, ref val, ref val2);
				highestGenRequired = Math.Max(highestGenRequired, val2);
				lowestGenRequired = Math.Min(lowestGenRequired, val);
				message = string.Format("Got page {0} from server {1}", num, amServerName);
				ExTraceGlobals.LogReplayerTracer.TraceDebug((long)this.GetHashCode(), message);
			}
			if (!this.ShouldCancelReplay())
			{
				foreach (uint num2 in corruptedPages)
				{
				}
			}
			return dictionary;
		}

		// Token: 0x06001DA4 RID: 7588 RVA: 0x00087094 File Offset: 0x00085294
		private void UpdateDatabaseWithPages(Dictionary<uint, byte[]> pages, long lowestGenRequired, long highestGenRequired)
		{
			string destinationEdbPath = this.Configuration.DestinationEdbPath;
			string message = string.Format("Starting to patch {0}", destinationEdbPath);
			ExTraceGlobals.LogReplayerTracer.TraceDebug((long)this.GetHashCode(), message);
			JET_INSTANCE instance;
			Api.JetCreateInstance2(out instance, this.Configuration.Identity, this.Configuration.DatabaseName, CreateInstanceGrbit.None);
			try
			{
				InstanceParameters instanceParameters = new InstanceParameters(instance);
				instanceParameters.EventLoggingLevel = EventLoggingLevels.Low;
				instanceParameters.BaseName = this.Configuration.LogFilePrefix;
				instanceParameters.LogFileDirectory = this.Configuration.DestinationLogPath;
				instanceParameters.SystemDirectory = this.Configuration.DestinationSystemPath;
				instanceParameters.NoInformationEvent = true;
				SystemParameters.EventLoggingLevel = 25;
				this.m_setViable.ClearViable();
				UnpublishedApi.JetBeginDatabaseIncrementalReseed(instance, destinationEdbPath, BeginDatabaseIncrementalReseedGrbit.None);
				foreach (KeyValuePair<uint, byte[]> keyValuePair in pages)
				{
					message = string.Format("Patching page {0}", keyValuePair.Key);
					ExTraceGlobals.LogReplayerTracer.TraceDebug((long)this.GetHashCode(), message);
					UnpublishedApi.JetPatchDatabasePages(instance, destinationEdbPath, (int)keyValuePair.Key, 1, keyValuePair.Value, keyValuePair.Value.Length, PatchDatabasePagesGrbit.None);
				}
				UnpublishedApi.JetEndDatabaseIncrementalReseed(instance, destinationEdbPath, (int)lowestGenRequired, 0, (int)highestGenRequired, EndDatabaseIncrementalReseedGrbit.None);
				this.m_setViable.SetViable();
				message = string.Format("Finished patching {0}", destinationEdbPath);
				ExTraceGlobals.LogReplayerTracer.TraceDebug((long)this.GetHashCode(), message);
			}
			finally
			{
				Api.JetTerm(instance);
			}
		}

		// Token: 0x06001DA5 RID: 7589 RVA: 0x00087234 File Offset: 0x00085434
		private void SendPageToActive(IPagePatchReply reply)
		{
			string databaseName = this.Configuration.DatabaseName;
			AmServerName amServerName = new AmServerName(this.Configuration.SourceMachine);
			try
			{
				string message = string.Format("Sending page {0} for database '{1}' to {2}", reply.PageNumber, databaseName, amServerName.NetbiosName);
				ExTraceGlobals.LogReplayerTracer.TraceDebug((long)this.GetHashCode(), message);
				string text;
				IEsebcli esebcli = this.GetEsebcli(amServerName, out text);
				esebcli.OnlinePatchDatabasePage(text, databaseName, databaseName, reply.PageNumber, reply.Token, reply.Data);
			}
			catch (EsebcliException ex)
			{
				string message2 = string.Format("Error sending page {0} for database '{1}' to {2}: {3}", new object[]
				{
					reply.PageNumber,
					databaseName,
					amServerName,
					ex
				});
				ExTraceGlobals.LogReplayerTracer.TraceError((long)this.GetHashCode(), message2);
			}
		}

		// Token: 0x06001DA6 RID: 7590 RVA: 0x00087314 File Offset: 0x00085514
		private IEsebcli GetEsebcli(AmServerName serverName, out string endPoint)
		{
			endPoint = null;
			IEsebcli esebcli = Dependencies.Container.Resolve<IEsebcli>();
			esebcli.SetServer(serverName.Fqdn);
			esebcli.SetTimeout(30000);
			string identity = this.m_configuration.Identity;
			EndPointInfo[] endPointInfo = esebcli.GetEndPointInfo();
			if (endPointInfo.Length == 0)
			{
				string text = string.Format("Esebcli failed to connect to {0}: no ESEBACK servers were found", serverName.Fqdn);
				ExTraceGlobals.LogReplayerTracer.TraceError((long)this.GetHashCode(), text);
				throw new EsebcliException(text);
			}
			foreach (EndPointInfo endPointInfo2 in endPointInfo)
			{
				if (string.Equals(identity, endPointInfo2.Annotation, StringComparison.OrdinalIgnoreCase))
				{
					endPoint = endPointInfo2.Annotation;
				}
			}
			if (endPoint == null)
			{
				endPoint = "Microsoft Information Store";
				LogReplayer.Tracer.TraceError<string, string>((long)this.GetHashCode(), "Failed to locate ESEBAK for dbguid {0}. Trying as if single instance store with {1}", identity, endPoint);
			}
			return esebcli;
		}

		// Token: 0x06001DA7 RID: 7591 RVA: 0x000873E3 File Offset: 0x000855E3
		internal void DisableReplayLag()
		{
			ExTraceGlobals.LogReplayerTracer.TraceDebug<string>((long)this.GetHashCode(), "DisableReplayLag( {0} ): Replay lag is being disabled...", this.m_configuration.DisplayName);
			this.IsReplayLagDisabled = true;
			this.WakeupReplayer();
		}

		// Token: 0x06001DA8 RID: 7592 RVA: 0x00087413 File Offset: 0x00085613
		internal void EnableReplayLag()
		{
			ExTraceGlobals.LogReplayerTracer.TraceDebug<string>((long)this.GetHashCode(), "EnableReplayLag( {0} ): Replay lag is being enabled...", this.m_configuration.DisplayName);
			this.IsReplayLagDisabled = false;
		}

		// Token: 0x06001DA9 RID: 7593 RVA: 0x00087440 File Offset: 0x00085640
		internal void WakeupReplayer()
		{
			lock (this.m_replayThreadWakeupEventLocker)
			{
				if (this.m_replayThreadWakeupEvent != null)
				{
					this.m_replayThreadWakeupEvent.Set();
				}
			}
		}

		// Token: 0x06001DAA RID: 7594 RVA: 0x00087490 File Offset: 0x00085690
		private bool WaitUntilRequiredLogFilesShowup()
		{
			while (!this.ShouldCancelReplay() && !this.ShouldReacquireLock())
			{
				if (this.CheckFilesForReplay())
				{
					return true;
				}
				this.ReplayPause(null);
				this.m_perfmonCounters.ReplayLagPercentage = (long)this.GetActualReplayLagPercentage();
			}
			return false;
		}

		// Token: 0x06001DAB RID: 7595 RVA: 0x000874E1 File Offset: 0x000856E1
		private bool ShouldReacquireLock()
		{
			return !this.m_haveSuspendLock && this.UseSuspendLock;
		}

		// Token: 0x06001DAC RID: 7596 RVA: 0x000874F8 File Offset: 0x000856F8
		private void RecoveryThreadProc(object unused)
		{
			bool flag = false;
			while (!this.m_stopCalled)
			{
				bool flag2 = false;
				if (this.UseSuspendLock)
				{
					flag2 = this.TryGetSuspendLock();
				}
				if (flag2 || !this.UseSuspendLock)
				{
					bool flag3 = false;
					try
					{
						try
						{
							if (this.WaitUntilRequiredLogFilesShowup())
							{
								while (!this.ShouldCancelReplay() && !this.ShouldReacquireLock())
								{
									bool flag4 = true;
									bool flag5 = false;
									if (this.m_highestGenerationToReplay <= this.m_highestGenerationAvailable)
									{
										this.FindNextHighestGenerationToReplay(ref flag4, out flag5);
									}
									if (flag4)
									{
										if (flag5 || this.State.ReplayGenerationNumber != this.m_highestGenerationToReplay || this.m_lastStoreRpcUTC.Add(this.m_waitForNextStoreRpc) < DateTime.UtcNow)
										{
											this.LogReplayRpc(this.Configuration.IdentityGuid, this.m_highestGenerationToReplay);
										}
										this.ReplayPause(new long?(this.m_highestGenerationToReplay));
									}
									this.m_perfmonCounters.ReplayLagPercentage = (long)this.GetActualReplayLagPercentage();
								}
							}
							flag3 = true;
						}
						catch (Win32Exception ex)
						{
							ExTraceGlobals.LogReplayerTracer.TraceError<Win32Exception>((long)this.GetHashCode(), "LogReplayer got Win32Exception: {0}", ex);
							FailureTag failureTag;
							FileOperations.Win32ErrorCodeToIOFailureTag(ex.NativeErrorCode, FailureTag.NoOp, out failureTag);
							this.m_setBroken.SetBroken(failureTag, ReplayEventLogConstants.Tuple_LogReplayGenericError, ex, new string[]
							{
								ex.Message
							});
						}
						catch (IOException ex2)
						{
							string text = ex2.ToString();
							ExTraceGlobals.LogReplayerTracer.TraceError<string>((long)this.GetHashCode(), "LogReplayer got: {0}", text);
							this.m_setBroken.SetBroken(ReplicaInstance.IOExceptionToFailureTag(ex2), ReplayEventLogConstants.Tuple_LogReplayGenericError, ex2, new string[]
							{
								text
							});
						}
						continue;
					}
					finally
					{
						if (!flag3 || !this.IsDismountControlledExternally)
						{
							this.DismountAndCloseReplayController();
						}
						else
						{
							this.CloseReplayController();
						}
						flag = true;
						this.ReleaseSuspendLock();
					}
				}
				this.ReplayPause(null);
			}
			if (!flag)
			{
				if (!this.IsDismountControlledExternally)
				{
					this.DismountAndCloseReplayController();
					return;
				}
				this.CloseReplayController();
			}
		}

		// Token: 0x06001DAD RID: 7597 RVA: 0x0008772C File Offset: 0x0008592C
		private void FindNextHighestGenerationToReplay(ref bool allowRpc, out bool maxReplayGenerationReset)
		{
			LogReplayPlayDownReason logReplayPlayDownReason = LogReplayPlayDownReason.None;
			maxReplayGenerationReset = false;
			this.m_highestGenerationToReplay = Math.Max(this.m_highestGenerationToReplay, this.GetHighestGenerationToReplay());
			string text;
			if (this.m_fLogsScannedAtLeastOnce && this.ShouldPlayDownReplayLag(this.m_highestGenerationToReplay + 1L, out logReplayPlayDownReason, out text))
			{
				long num = Math.Max(0L, this.m_highestGenerationToReplay - (long)RegistryParameters.LogReplayerScanMoreLogsWhenReplayWithinThreshold);
				if (this.State.ReplayGenerationNumber < num)
				{
					ExTraceGlobals.LogReplayerTracer.TraceDebug((long)this.GetHashCode(), "FindNextHighestGenerationToReplay( {0} ): Skipped this call since it was throttled. m_fLogsScannedAtLeastOnce={1}, ReplayGen={2}, replayGenThresholdToContinue={3}, m_highestGenerationToReplay={4}", new object[]
					{
						this.m_configuration.DisplayName,
						this.m_fLogsScannedAtLeastOnce,
						this.State.ReplayGenerationNumber,
						num,
						this.m_highestGenerationToReplay
					});
					return;
				}
			}
			int num2 = 0;
			int logReplayerMaxLogsToScanInOneIteration = RegistryParameters.LogReplayerMaxLogsToScanInOneIteration;
			bool flag;
			while (!this.ShouldPauseReplay(this.m_highestGenerationToReplay + 1L, out flag, out logReplayPlayDownReason))
			{
				if (this.ShouldCancelReplay() || this.ShouldReacquireLock())
				{
					allowRpc = false;
					break;
				}
				this.m_fLogsScannedAtLeastOnce = true;
				if (!flag)
				{
					this.m_highestGenerationToReplay += 1L;
					if ((logReplayPlayDownReason == LogReplayPlayDownReason.LagDisabled || logReplayPlayDownReason == LogReplayPlayDownReason.NotEnoughFreeSpace || logReplayPlayDownReason == LogReplayPlayDownReason.NormalLogReplay) && ++num2 > logReplayerMaxLogsToScanInOneIteration)
					{
						ExTraceGlobals.LogReplayerTracer.TraceDebug<string, int, LogReplayPlayDownReason>((long)this.GetHashCode(), "FindNextHighestGenerationToReplay( {0} ): Throttling loop after scanning {1} logs. playDownReason={2}", this.m_configuration.DisplayName, logReplayerMaxLogsToScanInOneIteration, logReplayPlayDownReason);
						break;
					}
				}
				else
				{
					maxReplayGenerationReset = true;
					num2 = 0;
					this.m_fLogsScannedAtLeastOnce = false;
				}
			}
			ExTraceGlobals.LogReplayerTracer.TraceDebug((long)this.GetHashCode(), "FindNextHighestGenerationToReplay( {0} ): Calculated m_highestGenerationToReplay={1}, allowRpc={2}, maxReplayGenerationReset={3}", new object[]
			{
				this.m_configuration.DisplayName,
				this.m_highestGenerationToReplay,
				allowRpc,
				maxReplayGenerationReset
			});
		}

		// Token: 0x06001DAE RID: 7598 RVA: 0x000878F8 File Offset: 0x00085AF8
		private void DismountAndCloseReplayController()
		{
			lock (this.m_storeLocker)
			{
				try
				{
					if (this.IsDismountPending)
					{
						LogReplayer.DismountReplayDatabase(this.m_configuration.IdentityGuid, this.m_configuration.Identity, this.m_configuration.Name, this.LocalNodeName);
					}
				}
				finally
				{
					this.IsDismountPending = false;
					this.CloseReplayController();
				}
			}
		}

		// Token: 0x06001DAF RID: 7599 RVA: 0x00087984 File Offset: 0x00085B84
		private void CloseReplayController()
		{
			lock (this.m_storeLocker)
			{
				if (this.m_storeReplayController != null)
				{
					LogReplayer.Tracer.TraceDebug<string>((long)this.GetHashCode(), "{0}: ReplayController closed", this.DatabaseName);
					this.m_storeReplayController.Close();
					this.m_storeReplayController = null;
				}
			}
		}

		// Token: 0x06001DB0 RID: 7600 RVA: 0x000879F4 File Offset: 0x00085BF4
		private long GetHighestGenerationToReplay()
		{
			return Math.Min(this.m_highestGenerationAvailable, Math.Max(this.State.ReplayGenerationNumber, this.m_fileChecker.FileState.HighestGenerationRequired));
		}

		// Token: 0x06001DB1 RID: 7601 RVA: 0x00087A30 File Offset: 0x00085C30
		private void SetHighestGenerationAvailable(long generation)
		{
			if (!this.ShouldCancelReplay())
			{
				this.m_highestGenerationAvailable = Math.Max(generation, this.m_highestGenerationAvailable);
				this.m_perfmonCounters.ReplayNotificationGenerationNumber = this.m_highestGenerationAvailable;
				ExTraceGlobals.LogReplayerTracer.TraceDebug<long>((long)this.GetHashCode(), "HighestGenerationAvailable = {0}=0x{0:x}", this.m_highestGenerationAvailable);
				if (this.m_thread == null)
				{
					this.StartReplayThread();
				}
				if (generation != 0L && !this.State.ReplaySuspended)
				{
					this.WakeupReplayer();
				}
			}
		}

		// Token: 0x06001DB2 RID: 7602 RVA: 0x00087AAF File Offset: 0x00085CAF
		private void StartReplayThread()
		{
			this.m_thread = new Thread(new ParameterizedThreadStart(this.RecoveryThreadProc));
			this.m_thread.Start();
		}

		// Token: 0x06001DB3 RID: 7603 RVA: 0x00087AD4 File Offset: 0x00085CD4
		private void ReplayPause(long? generation)
		{
			TimeSpan waitForNextReplayLog = this.m_waitForNextReplayLog;
			ExTraceGlobals.LogReplayerTracer.TraceDebug<TimeSpan, long?>((long)this.GetHashCode(), "Pausing replay for {0} at generation {1}", waitForNextReplayLog, generation);
			this.m_replayThreadWakeupEvent.WaitOne(waitForNextReplayLog, false);
		}

		// Token: 0x06001DB4 RID: 7604 RVA: 0x00087B10 File Offset: 0x00085D10
		private bool ShouldPauseReplay(long generation, out bool maxReplayGenerationReset, out LogReplayPlayDownReason playDownReasonEnum)
		{
			maxReplayGenerationReset = false;
			playDownReasonEnum = LogReplayPlayDownReason.None;
			TimeSpan timeSpan = this.m_waitForNextReplayLog;
			if (generation == 0L)
			{
				return true;
			}
			string filenameFromGenerationNumber = base.GetFilenameFromGenerationNumber(generation);
			string text = Path.Combine(this.m_replayDir, filenameFromGenerationNumber);
			FileInfo fileInfo = new FileInfo(text);
			DateTime dateTime = fileInfo.LastWriteTimeUtc;
			if (dateTime < CultureInfo.InstalledUICulture.Calendar.MinSupportedDateTime)
			{
				dateTime = CultureInfo.InstalledUICulture.Calendar.MinSupportedDateTime;
			}
			DateTime utcNow = DateTime.UtcNow;
			string text2 = null;
			long highestGenerationToReplay = this.m_highestGenerationToReplay;
			string text3 = null;
			if (dateTime > utcNow)
			{
				dateTime = utcNow;
			}
			TimeSpan timeSpan2 = utcNow - dateTime;
			if (fileInfo.Exists)
			{
				if (this.m_configuration.ReplayState.ReplayGenerationNumber == this.m_highestGenerationToReplay)
				{
					this.m_configuration.ReplayState.CurrentReplayTime = fileInfo.LastWriteTimeUtc;
				}
			}
			else
			{
				timeSpan2 = TimeSpan.Zero;
			}
			bool flag;
			if (this.ReplayerShouldBeSuspended(out text2))
			{
				flag = true;
			}
			else if (generation > this.m_highestGenerationAvailable)
			{
				flag = true;
				text2 = "generation > m_highestGenerationAvailable";
			}
			else if (!fileInfo.Exists)
			{
				flag = false;
			}
			else if (generation <= this.m_fileChecker.FileState.HighestGenerationRequired)
			{
				flag = false;
				playDownReasonEnum = LogReplayPlayDownReason.InRequiredRange;
				this.m_lastPlayDownReason = playDownReasonEnum;
				ExTraceGlobals.LogReplayerTracer.TraceDebug((long)this.GetHashCode(), "ShouldPauseReplay( {0} ) returns false. Log file is in required range. (generation = {1}, logfileAge = {2}, lowestGenReq = {3}, highestGenReq = {4})", new object[]
				{
					this.m_configuration.DisplayName,
					generation,
					timeSpan2,
					this.m_fileChecker.FileState.LowestGenerationRequired,
					this.m_fileChecker.FileState.HighestGenerationRequired
				});
				ReplayCrimsonEvents.LogReplayPlayingDownLogsInRequiredRange.LogPeriodic<string, Guid, long, TimeSpan, long, long>(this.m_configuration.Identity + this.m_fileChecker.FileState.HighestGenerationRequired, DiagCore.DefaultEventSuppressionInterval, this.m_configuration.Name, this.m_configuration.IdentityGuid, generation, timeSpan2, this.m_fileChecker.FileState.LowestGenerationRequired, this.m_fileChecker.FileState.HighestGenerationRequired);
			}
			else if (timeSpan2 < this.m_replayLag)
			{
				if (this.ShouldPlayDownReplayLag(generation, out playDownReasonEnum, out text3))
				{
					if (!this.m_fPlayingDownLag || this.m_lastPlayDownReason != playDownReasonEnum)
					{
						ReplayCrimsonEvents.LogReplayPlayingDownLogsInLagRange.Log<string, string, long, TimeSpan, string, LogReplayPlayDownReason>(this.m_configuration.Name, this.m_configuration.Identity, generation, timeSpan2, text3, playDownReasonEnum);
						this.m_fPlayingDownLag = true;
						this.m_lastPlayDownReason = playDownReasonEnum;
					}
					flag = false;
				}
				else
				{
					flag = true;
					text2 = "logfileAge < m_replayLag";
					timeSpan = this.m_replayLag - timeSpan2;
				}
			}
			else
			{
				flag = false;
				playDownReasonEnum = LogReplayPlayDownReason.NormalLogReplay;
				this.m_lastPlayDownReason = playDownReasonEnum;
			}
			if (flag && this.m_fPlayingDownLag)
			{
				if (!this.ShouldPlayDownReplayLag(generation, out playDownReasonEnum, out text3))
				{
					this.m_highestGenerationToReplay = this.GetHighestGenerationToReplay();
					this.m_fPlayingDownLag = false;
					maxReplayGenerationReset = true;
					flag = false;
					text2 = null;
					timeSpan = this.m_waitForNextReplayLog;
					ExTraceGlobals.LogReplayerTracer.TraceDebug<string, long, long>((long)this.GetHashCode(), "ShouldPauseReplay( {0} ) returns false. ReplayLag is not being played down anymore, so setting back m_highestGenerationToReplay to {1}, from {2}", this.m_configuration.DisplayName, this.m_highestGenerationToReplay, highestGenerationToReplay);
					ReplayCrimsonEvents.LogReplayReinstatingReplayLag.Log<string, Guid, long, TimeSpan, long, long>(this.m_configuration.Name, this.m_configuration.IdentityGuid, generation, timeSpan2, highestGenerationToReplay, this.m_highestGenerationToReplay);
				}
				else if (this.m_lastPlayDownReason != playDownReasonEnum)
				{
					ReplayCrimsonEvents.LogReplayPlayingDownLogsInLagRange.Log<string, string, long, TimeSpan, string, LogReplayPlayDownReason>(this.m_configuration.Name, this.m_configuration.Identity, generation, timeSpan2, text3, playDownReasonEnum);
					this.m_lastPlayDownReason = playDownReasonEnum;
				}
			}
			LogReplayPlayDownReason logReplayPlayDownReason = playDownReasonEnum;
			if (logReplayPlayDownReason == LogReplayPlayDownReason.NormalLogReplay)
			{
				logReplayPlayDownReason = LogReplayPlayDownReason.None;
			}
			this.Configuration.ReplayState.ReplayLagPlayDownReason = logReplayPlayDownReason;
			if (flag)
			{
				ExTraceGlobals.LogReplayerTracer.TraceDebug((long)this.GetHashCode(), "ShouldPauseReplay returns true ( generation = {0}=0x{0:x}, m_highestGenerationAvailable = {1}=0x{1:x}, logfileCreationTime = {2}, currentTime = {3}, logfileAge = {4}, m_replayLag = {5}, timeToPause = {6}, reasonForPause = '{7}', logfile = '{8}' )", new object[]
				{
					generation,
					this.m_highestGenerationAvailable,
					dateTime.ToString(),
					utcNow.ToString(),
					timeSpan2.ToString(),
					this.m_replayLag.ToString(),
					timeSpan.ToString(),
					text2,
					text
				});
			}
			return flag;
		}

		// Token: 0x06001DB5 RID: 7605 RVA: 0x00087F58 File Offset: 0x00086158
		private bool ShouldPlayDownReplayLag(long generation, out LogReplayPlayDownReason reasonEnum, out string reason)
		{
			bool result = false;
			long num = 0L;
			reason = null;
			reasonEnum = LogReplayPlayDownReason.None;
			string arg;
			if (this.IsReplayLagDisabled)
			{
				result = true;
				reasonEnum = LogReplayPlayDownReason.LagDisabled;
				reason = "Replay lag has been disabled.";
				ExTraceGlobals.LogReplayerTracer.TraceDebug<string, long, string>((long)this.GetHashCode(), "ShouldPlayDownReplayLag( {0} ) returns true for generation {1}. {2}", this.m_configuration.DisplayName, generation, reason);
			}
			else if (this.IsReplayQueueTooHighWithSuppression(out num, out arg))
			{
				result = true;
				reasonEnum = LogReplayPlayDownReason.NotEnoughFreeSpace;
				reason = string.Format("There are too many logs ({0}) in the lag range, or suppression of {1} was applied. {2}", num, this.m_queueHighPlayDownDisabledSuppressionDuration, arg);
				ExTraceGlobals.LogReplayerTracer.TraceDebug<string, long, string>((long)this.GetHashCode(), "ShouldPlayDownReplayLag( {0} ) returns true for generation {1}. {2}", this.m_configuration.DisplayName, generation, reason);
			}
			return result;
		}

		// Token: 0x06001DB6 RID: 7606 RVA: 0x00088000 File Offset: 0x00086200
		private bool IsReplayQueueTooHighWithSuppression(out long replayQ, out string error)
		{
			bool result = this.m_fPlayingDownLag;
			if (this.IsReplayQueueTooHigh(out replayQ, out error))
			{
				if (this.m_queueHighPlayDownSuppression.ReportFailure(this.m_queueHighPlayDownEnabledSuppressionDuration))
				{
					result = true;
				}
			}
			else if (this.m_queueHighPlayDownSuppression.ReportSuccess(this.m_queueHighPlayDownDisabledSuppressionDuration))
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06001DB7 RID: 7607 RVA: 0x0008804B File Offset: 0x0008624B
		private long CurrentReplayQueue()
		{
			return Math.Max(0L, this.State.InspectorGenerationNumber - this.State.ReplayGenerationNumber);
		}

		// Token: 0x06001DB8 RID: 7608 RVA: 0x0008806C File Offset: 0x0008626C
		private bool IsReplayQueueTooHigh(out long replayQ, out string error)
		{
			replayQ = this.CurrentReplayQueue();
			LogReplayer.LogSpaceInfo logSpaceInfo = new LogReplayer.LogSpaceInfo(this.Configuration);
			return logSpaceInfo.CheckLogsTakingUpTooMuchSpace(replayQ, out error);
		}

		// Token: 0x06001DB9 RID: 7609 RVA: 0x00088098 File Offset: 0x00086298
		private bool ShouldCancelReplay()
		{
			bool result = false;
			LockOwner lockOwner;
			if (this.State.SuspendLock.ShouldGiveUpLock(out lockOwner))
			{
				result = true;
				if (lockOwner != LockOwner.AttemptCopyLastLogs)
				{
					ExTraceGlobals.LogReplayerTracer.TraceDebug<LockOwner>((long)this.GetHashCode(), "should give up SuspendLock, highest priority pending owner: {0}, stopping the instance", lockOwner);
					this.m_setBroken.RestartInstanceSoon(true);
				}
				else
				{
					this.m_setBroken.RestartInstanceSoon(false);
				}
			}
			if (this.m_stopCalled)
			{
				result = true;
			}
			ExTraceGlobals.FaultInjectionTracer.TraceTest<bool>(2149985597U, ref result);
			return result;
		}

		// Token: 0x06001DBA RID: 7610 RVA: 0x00088110 File Offset: 0x00086310
		private bool ReplayerShouldBeSuspended(out string reasonForPause)
		{
			reasonForPause = null;
			if (this.m_setPassiveSeeding.PassiveSeedingSourceContext == PassiveSeedingSourceContextEnum.Database)
			{
				LogReplayer.Tracer.TraceDebug((long)this.GetHashCode(), "LogReplay suspending because copy is a passive seeding source.");
				reasonForPause = "Copy is a passive seeding source";
				this.Suspend();
				this.CloseReplayController();
			}
			else if (!this.IsCopyQueueBasedSuspendEnabled)
			{
				this.Resume();
			}
			else
			{
				long inspectorGenerationNumber = this.State.InspectorGenerationNumber;
				long copyGenerationNumber = this.State.CopyGenerationNumber;
				long num = Math.Max(0L, copyGenerationNumber - inspectorGenerationNumber);
				if (!this.State.ReplaySuspended && num > (long)this.SuspendThreshold)
				{
					reasonForPause = "InspectorQueue is too long";
					long arg;
					string text;
					if (this.IsReplayQueueTooHigh(out arg, out text))
					{
						LogReplayer.Tracer.TraceError((long)this.GetHashCode(), "Skipping suspending log replay because the replay queue is too high");
					}
					else if (!this.State.ReplaySuspended)
					{
						LogReplayer.Tracer.TraceError<long, long, long>((long)this.GetHashCode(), "Suspending log replay because lastCopied({0}) - lastInspected({1}) = {2}", copyGenerationNumber, inspectorGenerationNumber, num);
						this.Suspend();
						ReplayEventLogConstants.Tuple_LogReplaySuspendedDueToCopyQ.LogEvent(this.m_configuration.Identity, new object[]
						{
							this.m_configuration.DatabaseName,
							num,
							this.SuspendThreshold,
							this.ResumeThreshold
						});
					}
				}
				else if (this.State.ReplaySuspended)
				{
					long arg;
					string text;
					bool flag = this.IsReplayQueueTooHigh(out arg, out text);
					if (!flag && num >= (long)this.ResumeThreshold)
					{
						reasonForPause = "InspectorQueue is too long";
					}
					else
					{
						if (flag)
						{
							LogReplayer.Tracer.TraceDebug<long>((long)this.GetHashCode(), "Resuming log replay because the replay queue {0} is too high.", arg);
						}
						else
						{
							LogReplayer.Tracer.TraceDebug<long, long, long>((long)this.GetHashCode(), "Resuming log replay because lastCopied({0}) - lastInspected({1}) = {2}", copyGenerationNumber, inspectorGenerationNumber, num);
						}
						this.Resume();
						ReplayEventLogConstants.Tuple_LogReplayResumedDueToCopyQ.LogEvent(this.m_configuration.Identity, new object[]
						{
							this.m_configuration.DatabaseName,
							num,
							this.ResumeThreshold
						});
					}
				}
			}
			return this.State.ReplaySuspended;
		}

		// Token: 0x04000C54 RID: 3156
		private const int MaxPatchRetryAttempts = 10;

		// Token: 0x04000C55 RID: 3157
		private const int PatchRetrySleepIntervalMs = 3000;

		// Token: 0x04000C56 RID: 3158
		private const int EseBackTimeoutInMs = 30000;

		// Token: 0x04000C57 RID: 3159
		private readonly TimeSpan m_queueHighPlayDownEnabledSuppressionDuration = TimeSpan.FromSeconds((double)RegistryParameters.LogReplayQueueHighPlayDownEnableSuppressionWindowInSecs);

		// Token: 0x04000C58 RID: 3160
		private readonly TimeSpan m_queueHighPlayDownDisabledSuppressionDuration = TimeSpan.FromSeconds((double)RegistryParameters.LogReplayQueueHighPlayDownDisableSuppressionWindowInSecs);

		// Token: 0x04000C59 RID: 3161
		private readonly IPerfmonCounters m_perfmonCounters;

		// Token: 0x04000C5A RID: 3162
		private readonly IReplayConfiguration m_configuration;

		// Token: 0x04000C5B RID: 3163
		private readonly long m_initialFromNumber;

		// Token: 0x04000C5C RID: 3164
		private readonly IFileChecker m_fileChecker;

		// Token: 0x04000C5D RID: 3165
		private readonly ILogTruncater m_logTruncater;

		// Token: 0x04000C5E RID: 3166
		private readonly ISetViable m_setViable;

		// Token: 0x04000C5F RID: 3167
		private readonly TimeSpan m_replayLag;

		// Token: 0x04000C60 RID: 3168
		private readonly string m_replayDir;

		// Token: 0x04000C61 RID: 3169
		private readonly TimeSpan m_waitForNextReplayLog;

		// Token: 0x04000C62 RID: 3170
		private readonly TimeSpan m_waitForNextStoreRpc;

		// Token: 0x04000C63 RID: 3171
		private DateTime m_lastStoreRpcUTC;

		// Token: 0x04000C64 RID: 3172
		private EventWaitHandle m_replayThreadWakeupEvent;

		// Token: 0x04000C65 RID: 3173
		private ManualResetEvent m_testDelayEvent;

		// Token: 0x04000C66 RID: 3174
		private ISetPassiveSeeding m_setPassiveSeeding;

		// Token: 0x04000C67 RID: 3175
		private bool m_haveSuspendLock;

		// Token: 0x04000C68 RID: 3176
		private bool m_checkFilesForReplayHasRun;

		// Token: 0x04000C69 RID: 3177
		private bool m_stopCalled;

		// Token: 0x04000C6A RID: 3178
		private bool m_fStopped;

		// Token: 0x04000C6B RID: 3179
		private IStoreRpc m_storeReplayController;

		// Token: 0x04000C6C RID: 3180
		private Thread m_thread;

		// Token: 0x04000C6D RID: 3181
		private long m_highestGenerationAvailable;

		// Token: 0x04000C6E RID: 3182
		private long m_highestGenerationToReplay;

		// Token: 0x04000C6F RID: 3183
		private bool m_fPlayingDownLag;

		// Token: 0x04000C70 RID: 3184
		private LogReplayPlayDownReason m_lastPlayDownReason;

		// Token: 0x04000C71 RID: 3185
		private bool m_fLogsScannedAtLeastOnce;

		// Token: 0x04000C72 RID: 3186
		private TransientErrorInfo m_queueHighPlayDownSuppression;

		// Token: 0x04000C73 RID: 3187
		private object m_storeLocker = new object();

		// Token: 0x04000C74 RID: 3188
		private object m_replayThreadWakeupEventLocker = new object();

		// Token: 0x04000C75 RID: 3189
		private bool m_logRepairWasPending;

		// Token: 0x04000C76 RID: 3190
		private LogReplayScanControl m_dbScanControl;

		// Token: 0x020002E4 RID: 740
		private struct LogSpaceInfo
		{
			// Token: 0x170007E4 RID: 2020
			// (get) Token: 0x06001DBC RID: 7612 RVA: 0x0008832B File Offset: 0x0008652B
			// (set) Token: 0x06001DBD RID: 7613 RVA: 0x00088333 File Offset: 0x00086533
			public long MaxLogsForReplayLag { get; private set; }

			// Token: 0x170007E5 RID: 2021
			// (get) Token: 0x06001DBE RID: 7614 RVA: 0x0008833C File Offset: 0x0008653C
			// (set) Token: 0x06001DBF RID: 7615 RVA: 0x00088344 File Offset: 0x00086544
			public ByteQuantifiedSize TotalDiskFreeSpace { get; private set; }

			// Token: 0x170007E6 RID: 2022
			// (get) Token: 0x06001DC0 RID: 7616 RVA: 0x0008834D File Offset: 0x0008654D
			// (set) Token: 0x06001DC1 RID: 7617 RVA: 0x00088355 File Offset: 0x00086555
			public ByteQuantifiedSize TotalDiskSpace { get; private set; }

			// Token: 0x170007E7 RID: 2023
			// (get) Token: 0x06001DC2 RID: 7618 RVA: 0x0008835E File Offset: 0x0008655E
			// (set) Token: 0x06001DC3 RID: 7619 RVA: 0x00088366 File Offset: 0x00086566
			public ulong LowSpacePlaydownThresholdInMB { get; private set; }

			// Token: 0x170007E8 RID: 2024
			// (get) Token: 0x06001DC4 RID: 7620 RVA: 0x0008836F File Offset: 0x0008656F
			// (set) Token: 0x06001DC5 RID: 7621 RVA: 0x00088377 File Offset: 0x00086577
			public int CurrentFreeSpacePercentage { get; private set; }

			// Token: 0x06001DC6 RID: 7622 RVA: 0x00088380 File Offset: 0x00086580
			public LogSpaceInfo(IReplayConfiguration config)
			{
				this = default(LogReplayer.LogSpaceInfo);
				if (RegistryParameters.LogReplayerMaximumLogsForReplayLag > 0)
				{
					LogReplayer.Tracer.TraceDebug<string, int>((long)this.GetHashCode(), "LogSpaceInfo..ctor(): ({0}): LogReplayerMaximumLogsForReplayLag regkey is set to max of {1} logs", config.DisplayName, RegistryParameters.LogReplayerMaximumLogsForReplayLag);
					this.MaxLogsForReplayLag = (long)RegistryParameters.LogReplayerMaximumLogsForReplayLag;
				}
				if (RegistryParameters.ReplayLagLowSpacePlaydownThresholdInMB > 0)
				{
					this.LowSpacePlaydownThresholdInMB = (ulong)((long)RegistryParameters.ReplayLagLowSpacePlaydownThresholdInMB);
				}
				string destinationLogPath = config.DestinationLogPath;
				ulong bytesValue;
				ulong bytesValue2;
				Exception freeSpace = DiskHelper.GetFreeSpace(destinationLogPath, out bytesValue, out bytesValue2);
				if (freeSpace != null)
				{
					LogReplayer.Tracer.TraceError<string, Exception>((long)this.GetHashCode(), "LogSpaceInfo..ctor(): GetFreeSpace() failed against directory '{0}'. Exception: {1}", destinationLogPath, freeSpace);
					throw freeSpace;
				}
				this.TotalDiskFreeSpace = ByteQuantifiedSize.FromBytes(bytesValue2);
				this.TotalDiskSpace = ByteQuantifiedSize.FromBytes(bytesValue);
				this.CurrentFreeSpacePercentage = DiskHelper.GetFreeSpacePercentage(this.TotalDiskFreeSpace.ToBytes(), this.TotalDiskSpace.ToBytes());
				LogReplayer.Tracer.TraceDebug((long)this.GetHashCode(), "LogSpaceInfo..ctor(): ({4}): The free space limit is {2}MB and it is currently {3}% of total disk space. [Free Space: {0}, Total Space: {1}]", new object[]
				{
					this.TotalDiskFreeSpace,
					this.TotalDiskSpace,
					this.LowSpacePlaydownThresholdInMB,
					this.CurrentFreeSpacePercentage,
					config.DisplayName
				});
			}

			// Token: 0x06001DC7 RID: 7623 RVA: 0x000884C8 File Offset: 0x000866C8
			public bool CheckLogsTakingUpTooMuchSpace(long replayQueue, out string error)
			{
				error = null;
				if (this.TotalDiskFreeSpace.ToBytes() / 1048576UL < this.LowSpacePlaydownThresholdInMB)
				{
					error = string.Format("There is not enough free space on the disk. The allowed free space is {0}MB but it is currently only {1}MB of total disk space. [Free Space: {2}, Total Space: {3}]", new object[]
					{
						this.LowSpacePlaydownThresholdInMB,
						this.TotalDiskFreeSpace.ToBytes() / 1048576UL,
						this.TotalDiskFreeSpace,
						this.TotalDiskSpace
					});
					return true;
				}
				if (this.MaxLogsForReplayLag != 0L && replayQueue > this.MaxLogsForReplayLag)
				{
					error = string.Format("Maximum number of logs allowed based on registry override (LogReplayerMaximumLogsForReplayLag): {0} logs.", this.MaxLogsForReplayLag);
					return true;
				}
				return false;
			}
		}
	}
}
