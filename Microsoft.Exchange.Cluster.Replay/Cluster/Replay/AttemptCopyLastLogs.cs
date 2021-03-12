using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Security;
using Microsoft.Exchange.Cluster.Replay.Dumpster;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Common.HA;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.HA.DirectoryServices;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;
using Microsoft.Exchange.EseRepl;
using Microsoft.Exchange.Rpc.ActiveManager;
using Microsoft.Isam.Esent.Interop;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020002C5 RID: 709
	internal sealed class AttemptCopyLastLogs
	{
		// Token: 0x1700077B RID: 1915
		// (get) Token: 0x06001B9A RID: 7066 RVA: 0x00075F3F File Offset: 0x0007413F
		public static Trace Tracer
		{
			get
			{
				return ExTraceGlobals.ReplicaInstanceTracer;
			}
		}

		// Token: 0x1700077C RID: 1916
		// (get) Token: 0x06001B9B RID: 7067 RVA: 0x00075F46 File Offset: 0x00074146
		// (set) Token: 0x06001B9C RID: 7068 RVA: 0x00075F4E File Offset: 0x0007414E
		public long LastLogNotifiedAtStart { get; private set; }

		// Token: 0x1700077D RID: 1917
		// (get) Token: 0x06001B9D RID: 7069 RVA: 0x00075F57 File Offset: 0x00074157
		// (set) Token: 0x06001B9E RID: 7070 RVA: 0x00075F5F File Offset: 0x0007415F
		public long LastLogShippedAtStart { get; private set; }

		// Token: 0x1700077E RID: 1918
		// (get) Token: 0x06001B9F RID: 7071 RVA: 0x00075F68 File Offset: 0x00074168
		// (set) Token: 0x06001BA0 RID: 7072 RVA: 0x00075F70 File Offset: 0x00074170
		public long LastLogReplayedAtStart { get; private set; }

		// Token: 0x06001BA1 RID: 7073 RVA: 0x00075F7C File Offset: 0x0007417C
		public AttemptCopyLastLogs(IPerfmonCounters perfmonCounters, ReplayConfiguration configuration, FileChecker fileChecker, bool performDivergenceCheck, ISetGeneration setGeneration, ISetViable setViable, bool fSkipHealthChecks, DatabaseMountDialOverride mountDialOverride, AcllPerformanceTracker acllPerf, string uniqueOperationId, int subactionAttemptNumber)
		{
			this.m_uniqueOperationId = uniqueOperationId;
			this.m_subactionAttemptNumber = subactionAttemptNumber;
			this.m_acllPerf = acllPerf;
			this.m_fPerformDivergenceCheck = performDivergenceCheck;
			this.m_fSkipHealthChecks = fSkipHealthChecks;
			this.m_mountDialOverride = mountDialOverride;
			this.m_perfmonCounters = perfmonCounters;
			this.m_configuration = configuration;
			string displayName = this.m_configuration.DisplayName;
			this.m_setBrokenForCopier = new SimpleSetBroken(displayName);
			this.m_setBrokenForInspector = new SimpleSetBroken(displayName);
			this.m_setBrokenForReplayer = new SimpleSetBroken(displayName);
			this.m_setBrokenForOther = new SimpleSetBroken(displayName);
			this.m_setGeneration = setGeneration;
			this.m_setViable = setViable;
			this.m_state = this.m_configuration.ReplayState;
			this.m_fileChecker = fileChecker;
			this.m_fileState = fileChecker.FileState;
			this.m_logCopier = null;
			this.m_logInspector = null;
			this.m_remoteConfig = (this.m_configuration as RemoteReplayConfiguration);
			this.NumberOfLogsLost = -1L;
			DiagCore.RetailAssert(this.m_remoteConfig != null, "ACLL is only supported for HA replicated databases", new object[0]);
		}

		// Token: 0x1700077F RID: 1919
		// (get) Token: 0x06001BA2 RID: 7074 RVA: 0x00076098 File Offset: 0x00074298
		// (set) Token: 0x06001BA3 RID: 7075 RVA: 0x000760A0 File Offset: 0x000742A0
		public bool NoLoss
		{
			get
			{
				return this.m_fNoLoss;
			}
			set
			{
				this.m_fNoLoss = value;
			}
		}

		// Token: 0x17000780 RID: 1920
		// (get) Token: 0x06001BA4 RID: 7076 RVA: 0x000760A9 File Offset: 0x000742A9
		// (set) Token: 0x06001BA5 RID: 7077 RVA: 0x000760B1 File Offset: 0x000742B1
		public bool MountAllowed
		{
			get
			{
				return this.m_fMountAllowed;
			}
			set
			{
				this.m_fMountAllowed = value;
			}
		}

		// Token: 0x17000781 RID: 1921
		// (get) Token: 0x06001BA6 RID: 7078 RVA: 0x000760BA File Offset: 0x000742BA
		// (set) Token: 0x06001BA7 RID: 7079 RVA: 0x000760C2 File Offset: 0x000742C2
		public bool MountDialOverrideUsed { get; private set; }

		// Token: 0x17000782 RID: 1922
		// (get) Token: 0x06001BA8 RID: 7080 RVA: 0x000760CB File Offset: 0x000742CB
		// (set) Token: 0x06001BA9 RID: 7081 RVA: 0x000760D3 File Offset: 0x000742D3
		public long LastLogShipped { get; private set; }

		// Token: 0x17000783 RID: 1923
		// (get) Token: 0x06001BAA RID: 7082 RVA: 0x000760DC File Offset: 0x000742DC
		// (set) Token: 0x06001BAB RID: 7083 RVA: 0x000760E4 File Offset: 0x000742E4
		public long LastLogNotified { get; private set; }

		// Token: 0x17000784 RID: 1924
		// (get) Token: 0x06001BAC RID: 7084 RVA: 0x000760ED File Offset: 0x000742ED
		// (set) Token: 0x06001BAD RID: 7085 RVA: 0x000760F5 File Offset: 0x000742F5
		public DateTime LastLogInspectedTime { get; private set; }

		// Token: 0x17000785 RID: 1925
		// (get) Token: 0x06001BAE RID: 7086 RVA: 0x000760FE File Offset: 0x000742FE
		// (set) Token: 0x06001BAF RID: 7087 RVA: 0x00076106 File Offset: 0x00074306
		public long NumberOfLogsLost { get; private set; }

		// Token: 0x17000786 RID: 1926
		// (get) Token: 0x06001BB0 RID: 7088 RVA: 0x0007610F File Offset: 0x0007430F
		public LocalizedString ErrorMessage
		{
			get
			{
				return this.m_errorMessage;
			}
		}

		// Token: 0x06001BB1 RID: 7089 RVA: 0x00076118 File Offset: 0x00074318
		public static AmAcllReturnStatus AttemptCopyLastLogsOnceRcr(IPerfmonCounters perfmonCounters, ReplayConfiguration configuration, FileChecker fileChecker, ISetGeneration setGeneration, ISetViable setViable, LogCopier logCopier, LogInspector logInspector, LogReplayer logReplayer, bool performDivergenceCheck, bool fSkipHealthChecks, DatabaseMountDialOverride mountDialOverride, AcllPerformanceTracker acllPerf, string uniqueOperationId, int subactionAttemptNumber, bool mountPending)
		{
			AttemptCopyLastLogs attemptCopyLastLogs = new AttemptCopyLastLogs(perfmonCounters, configuration, fileChecker, performDivergenceCheck, setGeneration, setViable, fSkipHealthChecks, mountDialOverride, acllPerf, uniqueOperationId, subactionAttemptNumber);
			attemptCopyLastLogs.MakeAttempt(logCopier, logInspector, logReplayer);
			return attemptCopyLastLogs.GetReturnStatus();
		}

		// Token: 0x06001BB2 RID: 7090 RVA: 0x0007614F File Offset: 0x0007434F
		public void ResetMountAllowed()
		{
			ExTraceGlobals.ReplicaInstanceTracer.TraceDebug<bool, bool>((long)this.GetHashCode(), "ACLL.ResetMountAllowed(): Values are being reset to false. Old values are: NoLoss={0}, MountAllowed={1}", this.NoLoss, this.MountAllowed);
			this.NoLoss = false;
			this.MountAllowed = false;
		}

		// Token: 0x06001BB3 RID: 7091 RVA: 0x00076184 File Offset: 0x00074384
		private void MakeAttempt(LogCopier logCopier, LogInspector logInspector, LogReplayer logReplayer)
		{
			bool flag = false;
			Exception ex = null;
			try
			{
				this.m_logCopier = logCopier;
				this.m_logInspector = logInspector;
				this.m_logReplayer = logReplayer;
				this.ResetMountAllowed();
				this.RecordStartingFileState();
				ExTraceGlobals.FaultInjectionTracer.TraceTest(3227921725U);
				this.AttemptCopyLastLogsInternal();
				flag = true;
			}
			catch (ClusterException ex2)
			{
				ex = ex2;
			}
			catch (TransientException ex3)
			{
				ex = ex3;
			}
			catch (AmServerException ex4)
			{
				ex = ex4;
			}
			catch (EsentErrorException ex5)
			{
				ex = ex5;
			}
			catch (DumpsterRedeliveryException ex6)
			{
				ex = ex6;
			}
			catch (SerializationException ex7)
			{
				ex = ex7;
			}
			finally
			{
				if (ex != null)
				{
					this.m_setBrokenForOther.SetBroken(FailureTag.NoOp, ReplayEventLogConstants.Tuple_AttemptCopyLastLogsFailed, ex, new string[]
					{
						ex.ToString()
					});
					ReplayCrimsonEvents.AttemptCopyLastLogsFailed.Log<string, string>(this.m_configuration.DisplayName, ex.ToString());
					this.m_errorMessage = this.m_setBrokenForOther.ErrorMessage;
				}
				this.RecordEndingFileState();
				if (!flag)
				{
					this.ResetMountAllowed();
				}
				this.StopCopierAndInspector();
			}
			if (ex == null && !this.ErrorMessage.IsEmpty)
			{
				this.m_setBrokenForOther.SetBroken(FailureTag.NoOp, ReplayEventLogConstants.Tuple_AttemptCopyLastLogsFailed, new string[]
				{
					this.ErrorMessage
				});
				ReplayCrimsonEvents.AttemptCopyLastLogsFailed.Log<string, LocalizedString>(this.m_configuration.DisplayName, this.ErrorMessage);
				this.m_errorMessage = this.m_setBrokenForOther.ErrorMessage;
			}
		}

		// Token: 0x06001BB4 RID: 7092 RVA: 0x00076364 File Offset: 0x00074564
		private void AttemptCopyLastLogsInternal()
		{
			ExTraceGlobals.ReplicaInstanceTracer.TraceFunction((long)this.GetHashCode(), "AttemptCopyLastLogsInternal invoked");
			this.CreateCopierAndInspector();
			LocalizedString errorString = LocalizedString.Empty;
			this.m_acllPerf.RunTimedOperation(AcllTimedOperation.CopyLogsOverall, delegate
			{
				errorString = this.CopyLogs();
			});
			this.m_acllPerf.RunTimedOperation(AcllTimedOperation.ComputeLossAndMountAllowedOverall, delegate
			{
				this.ComputeLossAndMountAllowed(errorString);
				this.m_errorMessage = errorString;
			});
		}

		// Token: 0x06001BB5 RID: 7093 RVA: 0x000763D8 File Offset: 0x000745D8
		private void CreateCopierAndInspector()
		{
			string text = "." + this.m_configuration.LogExtension;
			if (this.m_logCopier == null)
			{
				long fromNumber = ReplicaInstance.CopyGeneration(this.m_configuration, this.m_fileState, this.m_setGeneration, null);
				this.m_initialNetworkPath = NetworkManager.ChooseNetworkPath(this.m_configuration.SourceMachine, null, NetworkPath.ConnectionPurpose.LogCopy);
				this.m_logCopier = new LogCopier(this.m_perfmonCounters, this.m_configuration.LogFilePrefix, fromNumber, text, this.m_configuration.LogInspectorPath, this.m_configuration.DestinationLogPath, this.m_configuration, this.m_fileChecker.FileState, this.m_setBrokenForCopier, this.m_setBrokenForCopier, this.m_setGeneration, this.m_initialNetworkPath, true);
				this.m_fCreatedCopier = true;
			}
			this.m_logCopier.SetReportingCallbacks(this.m_setBrokenForCopier, this.m_setBrokenForCopier);
			if (this.m_logInspector == null)
			{
				this.m_logInspector = new LogInspector(this.m_perfmonCounters, this.m_configuration, this.m_configuration.LogFilePrefix, text, this.m_configuration.DestinationLogPath, this.m_fileState, null, this.m_setBrokenForInspector, this.m_setGeneration, this.m_setBrokenForInspector, this.m_initialNetworkPath, true);
				this.m_fCreatedInspector = true;
			}
			this.m_logInspector.UseSuspendLock = false;
			this.m_logInspector.SetReportingCallback(this.m_setBrokenForInspector);
			this.m_logInspector.SetLogShipACLLTimeoutMs();
			if (this.m_logReplayer != null)
			{
				this.m_logReplayer.SetReportingCallback(this.m_setBrokenForReplayer);
			}
		}

		// Token: 0x06001BB6 RID: 7094 RVA: 0x00076554 File Offset: 0x00074754
		private void StopCopierAndInspector()
		{
			if (this.m_fCreatedCopier)
			{
				this.m_logCopier.PrepareToStop();
			}
			if (this.m_fCreatedInspector)
			{
				this.m_logInspector.PrepareToStop();
			}
			if (this.m_fCreatedCopier)
			{
				this.m_logCopier.Stop();
				this.m_logCopier = null;
				this.m_fCreatedCopier = false;
			}
			if (this.m_fCreatedInspector)
			{
				this.m_logInspector.Stop();
				this.m_logInspector = null;
				this.m_fCreatedInspector = false;
			}
		}

		// Token: 0x06001BB7 RID: 7095 RVA: 0x00076698 File Offset: 0x00074898
		private LocalizedString CopyLogs()
		{
			ExTraceGlobals.ReplicaInstanceTracer.TraceFunction((long)this.GetHashCode(), "ACLL.CopyLogs invoked");
			LocalizedString errorString = LocalizedString.Empty;
			bool e00Exists = false;
			bool fSkipLogCopy = false;
			if (this.m_logCopier == null)
			{
				return errorString;
			}
			this.m_acllPerf.RunTimedOperation(AcllTimedOperation.IsIncrementalReseedNecessary, delegate
			{
				fSkipLogCopy = this.IsIncrementalReseedNecessary(out e00Exists, out errorString);
			});
			if (!fSkipLogCopy)
			{
				if (!e00Exists)
				{
					if (this.m_fCreatedInspector)
					{
						ExTraceGlobals.ReplicaInstanceTracer.TraceDebug<string>((long)this.GetHashCode(), "ACLL: {0}: Starting the log inspector to run in parallel with AttemptFinalCopy().", this.m_configuration.Name);
						this.m_logInspector.Start();
					}
					Result attemptFinalCopyResult = Result.GiveUp;
					this.m_acllPerf.RunTimedOperation(AcllTimedOperation.AttemptFinalCopy, delegate
					{
						attemptFinalCopyResult = this.m_logCopier.AttemptFinalCopy(this.m_acllPerf, out errorString);
					});
					if (this.m_logReplayer != null)
					{
						this.m_logReplayer.UseSuspendLock = false;
						this.m_logReplayer.WakeupReplayer();
					}
					LocalizedString inspectionError = LocalizedString.Empty;
					bool fLogInspectorPassed = false;
					this.m_acllPerf.RunTimedOperation(AcllTimedOperation.InspectFinalLogs, delegate
					{
						fLogInspectorPassed = this.m_logInspector.InspectFinalLogsDuringAcll(out inspectionError);
					});
					if (attemptFinalCopyResult == Result.Success && !fLogInspectorPassed)
					{
						errorString = inspectionError;
					}
					if (fLogInspectorPassed && (attemptFinalCopyResult == Result.Success || this.m_logCopier.GranuleUsedAsE00))
					{
						bool e00InspectionPassed = false;
						this.m_acllPerf.RunTimedOperation(AcllTimedOperation.InspectE00Log, delegate
						{
							e00InspectionPassed = this.m_logInspector.InspectLog(0L, false, out inspectionError);
						});
						if (!e00InspectionPassed)
						{
							errorString = inspectionError;
							string text = Path.Combine(this.m_configuration.LogInspectorPath, this.m_configuration.LogFilePrefix + "." + this.m_configuration.LogExtension);
							ExTraceGlobals.ReplicaInstanceTracer.TraceError((long)this.GetHashCode(), "InspectLog failed, removing new e00");
							string destinationDirectory = Path.Combine(this.m_logInspector.Config.E00LogBackupPath, "InspectionFailed");
							if (File.Exists(text))
							{
								ReplicaInstance.MoveE00Log(text, ReplayStrings.RemoveLogReasonFailedInspection, destinationDirectory, this.m_configuration);
							}
						}
						else
						{
							e00Exists = true;
							if (attemptFinalCopyResult == Result.Success)
							{
								this.NoLoss = true;
							}
							this.m_acllPerf.IsGranuleUsedAsE00 = this.m_logCopier.GranuleUsedAsE00;
						}
					}
				}
				else
				{
					this.NoLoss = true;
					this.m_acllPerf.IsE00EndOfLogStreamAfterIncReseed = true;
				}
				if (this.m_logReplayer != null)
				{
					this.m_logReplayer.SendFinalLogReplayRequest();
				}
			}
			if (this.m_fSkipHealthChecks || !this.m_setViable.Viable)
			{
				if (e00Exists)
				{
					this.m_acllPerf.RunTimedOperation(AcllTimedOperation.SetE00LogGeneration, delegate
					{
						this.SetE00LogGeneration();
					});
				}
				this.m_acllPerf.RunTimedOperation(AcllTimedOperation.CheckRequiredLogFilesAtEnd, delegate
				{
					this.CheckRequiredLogFiles();
				});
			}
			if (!this.NoLoss && !e00Exists)
			{
				ExTraceGlobals.ReplicaInstanceTracer.TraceDebug<string>((long)this.GetHashCode(), "ACLL.CopyLogs(): {0}: E00.log does not exist. If active manager allows mount, recovery callback will take care of it", this.m_configuration.Name);
			}
			return errorString;
		}

		// Token: 0x06001BB8 RID: 7096 RVA: 0x000769D4 File Offset: 0x00074BD4
		private void SetE00LogGeneration()
		{
			Exception ex = null;
			string text = EseHelper.MakeLogFilePath(this.m_configuration, 0L, this.m_configuration.DestinationLogPath);
			try
			{
				long logfileGeneration = EseHelper.GetLogfileGeneration(text);
				ExTraceGlobals.ReplicaInstanceTracer.TraceDebug<string, long>((long)this.GetHashCode(), "ACLL.CopyLogs(): {0}: Setting E00.log FileState generation to: {1}", this.m_configuration.Name, logfileGeneration);
				this.m_fileState.SetE00LogGeneration(logfileGeneration);
			}
			catch (FileCheckException ex2)
			{
				ex = ex2;
			}
			catch (EsentErrorException ex3)
			{
				ex = ex3;
			}
			catch (IOException ex4)
			{
				ex = ex4;
			}
			catch (SecurityException ex5)
			{
				ex = ex5;
			}
			catch (UnauthorizedAccessException ex6)
			{
				ex = ex6;
			}
			if (ex != null)
			{
				ExTraceGlobals.ReplicaInstanceTracer.TraceError<string, Exception>((long)this.GetHashCode(), "ACLL.CopyLogs(): {0}: SetE00LogGeneration() got exception: {1}", this.m_configuration.Name, ex);
				throw new AcllSetCurrentLogGenerationException(this.m_configuration.DisplayName, text, ex.Message, ex);
			}
		}

		// Token: 0x06001BB9 RID: 7097 RVA: 0x00076AD4 File Offset: 0x00074CD4
		private void CheckRequiredLogFiles()
		{
			bool flag = false;
			try
			{
				flag = this.m_fileChecker.CheckRequiredLogFilesForDatabaseMountable();
				ExTraceGlobals.ReplicaInstanceTracer.TraceDebug<string, bool>((long)this.GetHashCode(), "ACLL.CopyLogs(): {0}: CheckRequiredLogFilesForDatabaseMountable() returned '{1}'", this.m_configuration.Name, flag);
			}
			catch (FileCheckException ex)
			{
				flag = false;
				ExTraceGlobals.ReplicaInstanceTracer.TraceError<string, FileCheckException>((long)this.GetHashCode(), "ACLL.CopyLogs(): {0}: CheckRequiredLogFilesForDatabaseMountable() threw exception: {1}", this.m_configuration.Name, ex);
				throw new AcllCopyIsNotViableErrorException(this.m_configuration.DisplayName, ex.Message, ex);
			}
			if (!flag)
			{
				throw new AcllCopyIsNotViableException(this.m_configuration.DisplayName);
			}
			this.m_setViable.SetViable();
		}

		// Token: 0x06001BBA RID: 7098 RVA: 0x00076B88 File Offset: 0x00074D88
		private void ComputeLossAndMountAllowed(LocalizedString errorString)
		{
			AutoDatabaseMountDial autoDatabaseMountDial = AutoDatabaseMountDial.Lossless;
			bool flag = false;
			this.m_acllPerf.RunTimedOperation(AcllTimedOperation.SetLossVariables, delegate
			{
				this.SetLossVariables();
			});
			this.UpdateMountAllowedNow(out autoDatabaseMountDial, out flag);
			if (this.NumberOfLogsLost > 0L)
			{
				ReplayCrimsonEvents.AcllReportedLoss.Log<string, Guid, long, long, long, AutoDatabaseMountDial, bool, string, int, DateTime, DateTime>(this.m_configuration.DatabaseName, this.m_configuration.IdentityGuid, this.NumberOfLogsLost, this.LastLogNotified, this.LastLogShipped, autoDatabaseMountDial, flag, this.m_uniqueOperationId, this.m_subactionAttemptNumber, this.LastLogInspectedTime, this.m_state.LatestInspectorTime);
				this.LogLossyAcll();
			}
			if (this.NoLoss)
			{
				ReplayEventLogConstants.Tuple_AutoMountReportNoLoss.LogEvent(this.m_configuration.Identity, new object[]
				{
					this.m_configuration.DisplayName,
					this.m_configuration.TargetMachine
				});
				return;
			}
			if (flag && this.MountAllowed)
			{
				ReplayEventLogConstants.Tuple_MountAllowedWithMountDialOverride.LogEvent(this.m_configuration.Identity, new object[]
				{
					this.m_configuration.DisplayName,
					this.m_configuration.TargetMachine,
					this.m_mountDialOverride,
					this.LastLogNotified,
					this.LastLogShipped,
					errorString
				});
				return;
			}
			if (flag && !this.MountAllowed)
			{
				ReplayEventLogConstants.Tuple_MountNotAllowedWithMountDialOverride.LogEvent(this.m_configuration.Identity, new object[]
				{
					this.m_configuration.DisplayName,
					this.m_configuration.TargetMachine,
					this.m_mountDialOverride,
					this.LastLogNotified,
					this.LastLogShipped,
					errorString
				});
				return;
			}
			if (this.MountAllowed)
			{
				ReplayEventLogConstants.Tuple_AutoMountReportMountWithDataLoss.LogEvent(this.m_configuration.Identity, new object[]
				{
					this.m_configuration.DisplayName,
					this.m_configuration.TargetMachine,
					this.LastLogNotified,
					this.LastLogShipped,
					autoDatabaseMountDial,
					errorString
				});
				return;
			}
			ReplayEventLogConstants.Tuple_AutoMountReportMountNotAllowed.LogEvent(this.m_configuration.Identity, new object[]
			{
				this.m_configuration.DisplayName,
				this.m_configuration.TargetMachine,
				this.LastLogNotified,
				this.LastLogShipped,
				autoDatabaseMountDial,
				errorString
			});
		}

		// Token: 0x06001BBB RID: 7099 RVA: 0x00076E40 File Offset: 0x00075040
		private void LogLossyAcll()
		{
			List<string> list = new List<string>(4);
			string simpleName = AmServerName.GetSimpleName(this.m_configuration.SourceMachine);
			string machineName = Environment.MachineName;
			foreach (IADDatabaseCopy iaddatabaseCopy in this.m_configuration.Database.DatabaseCopies)
			{
				if (!MachineName.Comparer.Equals(iaddatabaseCopy.HostServerName, simpleName) && !MachineName.Comparer.Equals(iaddatabaseCopy.HostServerName, machineName))
				{
					list.Add(iaddatabaseCopy.HostServerName);
				}
			}
			string text = string.Join(",", list.ToArray());
			long generation = this.LastLogShipped;
			if (this.m_logCopier.GranuleUsedAsE00)
			{
				generation = 0L;
			}
			string text2 = this.m_configuration.BuildFullLogfileName(generation);
			int num = 0;
			Exception ex;
			EseLogRecordPosition lastLogRecordPosition = EseHelper.GetLastLogRecordPosition(text2, this.m_configuration.E00LogBackupPath, out ex);
			if (lastLogRecordPosition != null)
			{
				num = lastLogRecordPosition.ByteOffsetToStartOfRec + lastLogRecordPosition.LogRecordLength;
			}
			else
			{
				AttemptCopyLastLogs.Tracer.TraceError<string, string>((long)this.GetHashCode(), "Failed to GetLastLogRecordPosition for '{0}': {1}", text2, (ex == null) ? "log appears empty" : ex.ToString());
			}
			ReplayCrimsonEvents.AcllReportedLoss2.Log<string, Guid, long, string, string, string, string, bool, string, string, string>(this.m_configuration.DatabaseName, this.m_configuration.IdentityGuid, this.NumberOfLogsLost, string.Format("0x{0:X}", this.LastLogNotified), string.Format("0x{0:X}", this.LastLogShipped), string.Format("0x{0:X}", num), string.Format("{0} UTC", this.LastLogInspectedTime.ToString("yyyy-MM-dd HH:mm:ss.fff")), this.m_logCopier.GranuleUsedAsE00, this.m_configuration.SourceMachine, text, this.m_uniqueOperationId);
		}

		// Token: 0x06001BBC RID: 7100 RVA: 0x00076FFC File Offset: 0x000751FC
		private DateTime GetWriteTimeFromLastCopiedGeneration()
		{
			long arg = -1L;
			string text = null;
			DateTime result = ReplayState.ZeroFileTime;
			Exception ex = null;
			bool flag = false;
			try
			{
				long[] array = new long[]
				{
					0L,
					this.LastLogShipped
				};
				foreach (long num in array)
				{
					string text2 = EseHelper.MakeLogFilePath(this.m_configuration, num, this.m_configuration.DestinationLogPath);
					arg = num;
					text = text2;
					FileInfo fileInfo = new FileInfo(text2);
					if (fileInfo.Exists)
					{
						result = fileInfo.LastWriteTimeUtc;
						flag = true;
						break;
					}
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
				AttemptCopyLastLogs.Tracer.TraceError<long, string, Exception>((long)this.GetHashCode(), "GetWriteTimeFromGeneration() for gen {0}, log file '{1}' threw exception: {2}", arg, text, ex);
				throw new AcllLastLogTimeErrorException(this.m_configuration.DisplayName, text, ex.Message, ex);
			}
			if (!flag)
			{
				throw new AcllLastLogNotFoundException(this.m_configuration.DisplayName, this.LastLogShipped);
			}
			return result;
		}

		// Token: 0x06001BBD RID: 7101 RVA: 0x0007711C File Offset: 0x0007531C
		private void SetLossVariables()
		{
			ExTraceGlobals.FaultInjectionTracer.TraceTest(2993040701U);
			this.LastLogShipped = this.m_fileState.HighestGenerationPresentWithE00;
			this.LastLogInspectedTime = this.GetWriteTimeFromLastCopiedGeneration();
			if (this.NoLoss)
			{
				this.LastLogNotified = this.LastLogShipped;
				this.NumberOfLogsLost = 0L;
			}
			else
			{
				this.m_lastLogInfo = this.m_state.GetLastLogInfo();
				if (this.m_lastLogInfo.IsStale)
				{
					this.LastLogNotified = this.m_lastLogInfo.LastLogGenToReport;
				}
				else
				{
					this.LastLogNotified = this.m_lastLogInfo.LastLogGenToReport + 1L;
				}
				long num = this.LastLogNotified - this.LastLogShipped;
				num = Math.Abs(num);
				this.NumberOfLogsLost = Math.Max(1L, num);
			}
			ExTraceGlobals.ReplicaInstanceTracer.TraceDebug<long, long, DateTime>((long)this.GetHashCode(), "LastLogNotified=0x{0:X}, LastLogShipped=0x{1:X}, LastInspected={2}", this.LastLogNotified, this.LastLogShipped, this.LastLogInspectedTime);
			ExTraceGlobals.ReplicaInstanceTracer.TraceDebug<long>((long)this.GetHashCode(), "Loss is {0} logs", this.NumberOfLogsLost);
		}

		// Token: 0x06001BBE RID: 7102 RVA: 0x00077220 File Offset: 0x00075420
		private void ProtectUnboundedDataloss()
		{
			TimeSpan t = TimeSpan.FromSeconds((double)RegistryParameters.UnboundedDatalossSafeGuardDurationInSec);
			TimeSpan t2 = this.m_lastLogInfo.CollectionTime - this.m_lastLogInfo.StaleCheckTime;
			if (t2 > t)
			{
				string text = this.m_lastLogInfo.StaleCheckTime.ToString("s");
				ExTraceGlobals.ReplicaInstanceTracer.TraceDebug((long)this.GetHashCode(), "Detected a possible unbounded dataloss scenario. Last updated time for a database {0}in the persistent storage is greater than allowed. (LastUpdatedTime(UTC)={1}, Allowed duration={2}, Actual duration={3})", new object[]
				{
					this.m_configuration.Name,
					text,
					t.ToString(),
					t2.ToString()
				});
				ReplayCrimsonEvents.DetectedPotentialUnboundedDataloss.Log<string, Guid, string, string, string, string>(this.m_configuration.Name, this.m_configuration.IdentityGuid, this.m_uniqueOperationId, text, t.ToString(), t2.ToString());
				if (this.m_lastLogInfo.IsStale && this.m_mountDialOverride != DatabaseMountDialOverride.BestEffort)
				{
					throw new AcllUnboundedDatalossDetectedException(this.m_configuration.Name, text, t.ToString(), t2.ToString());
				}
			}
		}

		// Token: 0x06001BBF RID: 7103 RVA: 0x0007737C File Offset: 0x0007557C
		private void UpdateMountAllowedNow(out AutoDatabaseMountDial mountDial, out bool mountDialOverrideUsed)
		{
			mountDial = AutoDatabaseMountDial.Lossless;
			mountDialOverrideUsed = false;
			if (this.NoLoss)
			{
				ExTraceGlobals.ReplicaInstanceTracer.TraceDebug((long)this.GetHashCode(), "MountAllowedNow returns true as NoLoss.");
				this.MountAllowed = true;
				return;
			}
			bool flag = false;
			long num = 0L;
			mountDialOverrideUsed = this.GetLossyMountSettings(ref num, ref flag);
			this.m_acllPerf.RunTimedOperation(AcllTimedOperation.ProtectUnboundedDataloss, delegate
			{
				this.ProtectUnboundedDataloss();
			});
			if (mountDialOverrideUsed && flag)
			{
				ExTraceGlobals.ReplicaInstanceTracer.TraceDebug((long)this.GetHashCode(), "MountDialOverride of BestEffort, allowing Mount now.");
			}
			else
			{
				mountDial = (AutoDatabaseMountDial)num;
				flag = (num != 0L && this.NumberOfLogsLost <= num);
			}
			if (flag)
			{
				this.m_acllPerf.RunTimedOperation(AcllTimedOperation.MarkRedeliveryRequired, delegate
				{
					DumpsterRedeliveryWrapper.MarkRedeliveryRequired(this.m_configuration, this.LastLogInspectedTime, this.LastLogShipped, this.NumberOfLogsLost);
				});
			}
			ExTraceGlobals.ReplicaInstanceTracer.TraceDebug<bool>((long)this.GetHashCode(), "fMountAllowed = {0}", flag);
			this.MountAllowed = flag;
			this.MountDialOverrideUsed = mountDialOverrideUsed;
		}

		// Token: 0x06001BC0 RID: 7104 RVA: 0x00077464 File Offset: 0x00075664
		private bool GetLossyMountSettings(ref long allowedLoss, ref bool fMountAllowed)
		{
			bool flag = false;
			if (this.m_mountDialOverride == DatabaseMountDialOverride.None)
			{
				allowedLoss = (long)this.m_remoteConfig.AutoDatabaseMountDial;
			}
			else if (this.m_mountDialOverride == DatabaseMountDialOverride.BestEffort)
			{
				fMountAllowed = true;
				flag = true;
			}
			else
			{
				allowedLoss = (long)this.m_mountDialOverride;
				flag = true;
			}
			if (flag)
			{
				ExTraceGlobals.ReplicaInstanceTracer.TraceDebug<string>((long)this.GetHashCode(), "MountDialOverride of '{0}' will be used. Ignoring server settings.", this.m_mountDialOverride.ToString());
			}
			else
			{
				ExTraceGlobals.ReplicaInstanceTracer.TraceDebug<long>((long)this.GetHashCode(), "Server settings will be used: allowedLoss={0}", allowedLoss);
			}
			return flag;
		}

		// Token: 0x06001BC1 RID: 7105 RVA: 0x000774EC File Offset: 0x000756EC
		private void RecordStartingFileState()
		{
			LastLogInfo lastLogInfo = this.m_state.GetLastLogInfo();
			long num = Math.Max(lastLogInfo.ClusterLastLogGen, lastLogInfo.ReplLastLogGen);
			this.LastLogNotifiedAtStart = num;
			this.LastLogShippedAtStart = this.m_fileState.HighestGenerationPresentWithE00;
			this.LastLogReplayedAtStart = this.m_state.ReplayGenerationNumber;
			this.m_acllPerf.CopyQueueLengthAcllStart = Math.Max(this.LastLogNotifiedAtStart - this.LastLogShippedAtStart, 0L);
			this.m_acllPerf.ReplayQueueLengthAcllStart = Math.Max(this.LastLogShippedAtStart - this.LastLogReplayedAtStart, 0L);
			ReplayCrimsonEvents.AcllReportLogInfo.Log<string, string, Guid, long, long, long, string, bool, Exception, long, string, bool, string>(this.m_uniqueOperationId, this.m_configuration.DatabaseName, this.m_configuration.IdentityGuid, num, this.m_acllPerf.CopyQueueLengthAcllStart, lastLogInfo.ClusterLastLogGen, lastLogInfo.ClusterLastLogTime.ToString("u"), lastLogInfo.ClusterTimeIsMissing, lastLogInfo.ClusterLastLogException, lastLogInfo.ReplLastLogGen, lastLogInfo.ReplLastLogTime.ToString("u"), lastLogInfo.IsStale, lastLogInfo.CollectionTime.ToString("u"));
		}

		// Token: 0x06001BC2 RID: 7106 RVA: 0x00077608 File Offset: 0x00075808
		private void RecordEndingFileState()
		{
			this.m_acllPerf.NumberOfLogsLost = this.NumberOfLogsLost;
			this.m_acllPerf.NumberOfLogsCopied = Math.Max(this.LastLogShipped - this.LastLogShippedAtStart, 0L);
			this.m_acllPerf.ReplayQueueLengthAcllEnd = Math.Max(this.LastLogShipped - this.m_state.ReplayGenerationNumber, 0L);
		}

		// Token: 0x06001BC3 RID: 7107 RVA: 0x0007766C File Offset: 0x0007586C
		private bool IsIncrementalReseedNecessary(out bool e00Exists, out LocalizedString errorString)
		{
			e00Exists = false;
			errorString = LocalizedString.Empty;
			bool flag = false;
			bool flag2 = false;
			bool flag3 = this.m_fPerformDivergenceCheck;
			string path = EseHelper.MakeLogfileName(this.m_configuration.LogFilePrefix, "." + this.m_configuration.LogExtension);
			string text = Path.Combine(this.m_configuration.DestinationLogPath, path);
			FileInfo fileInfo = new FileInfo(text);
			if (fileInfo.Exists)
			{
				e00Exists = true;
				flag3 = true;
				ExTraceGlobals.ReplicaInstanceTracer.TraceDebug<string, string>((long)this.GetHashCode(), "AttemptCopyLastLogs.IsIncrementalReseedNecessary(): {0}: Found existing current log at '{1}'.", this.m_configuration.DisplayName, text);
				string identity = this.m_configuration.Identity;
				ExTraceGlobals.FaultInjectionTracer.TraceTest<string>(3880135997U, identity);
			}
			if (flag3)
			{
				ExTraceGlobals.ReplicaInstanceTracer.TraceDebug<string>((long)this.GetHashCode(), "AttemptCopyLastLogs.IsIncrementalReseedNecessary(): {0}: Now performing a divergence check...", this.m_configuration.DisplayName);
				flag2 = this.AreLogFilesDivergentFromSource(out flag, out errorString);
			}
			fileInfo.Refresh();
			e00Exists = fileInfo.Exists;
			if (!flag2 && e00Exists && !flag)
			{
				errorString = ReplayStrings.AcllFailedCurrentLogPresent(this.m_configuration.DisplayName, text, this.m_configuration.SourceMachine);
				ExTraceGlobals.ReplicaInstanceTracer.TraceDebug<string, string>((long)this.GetHashCode(), "AttemptCopyLastLogs.IsIncrementalReseedNecessary(): {0}: returning 'true' since current log '{1}' already exists.", this.m_configuration.DisplayName, text);
				flag2 = true;
			}
			return flag2;
		}

		// Token: 0x06001BC4 RID: 7108 RVA: 0x00077820 File Offset: 0x00075A20
		private bool AreLogFilesDivergentFromSource(out bool e00IsEndOfLogStream, out LocalizedString errorString)
		{
			bool isDiverged = true;
			bool tempE00IsEndOfLogStream = false;
			IncSeedDivergenceCheckFailedException ex = null;
			IncReseedPerformanceTracker incReseedPerformanceTracker = new IncReseedPerformanceTracker(this.m_configuration);
			errorString = LocalizedString.Empty;
			e00IsEndOfLogStream = false;
			try
			{
				IncrementalReseeder incReseeder = new IncrementalReseeder(this.m_perfmonCounters, incReseedPerformanceTracker, this.m_configuration, this.m_fileState, this.m_setBrokenForOther, this.m_setBrokenForOther, null, this.m_initialNetworkPath, true, null);
				long highestLogGenCompared;
				incReseedPerformanceTracker.RunTimedOperation(IncReseedOperation.IsIncrementalReseedRequiredOverall, delegate
				{
					isDiverged = incReseeder.IsIncrementalReseedRequired(delegate
					{
					}, out highestLogGenCompared, out tempE00IsEndOfLogStream);
				});
				e00IsEndOfLogStream = tempE00IsEndOfLogStream;
			}
			catch (EsentErrorException ex2)
			{
				ex = new IncSeedDivergenceCheckFailedException(this.m_configuration.DatabaseName, this.m_configuration.SourceMachine, ex2.Message, ex2);
			}
			catch (IncrementalReseedFailedException ex3)
			{
				ex = new IncSeedDivergenceCheckFailedException(this.m_configuration.DatabaseName, this.m_configuration.SourceMachine, ex3.Message, ex3);
			}
			catch (IncSeedDivergenceCheckFailedException ex4)
			{
				ex = ex4;
			}
			catch (ReseedCheckMissingLogfileException ex5)
			{
				ex = new IncSeedDivergenceCheckFailedException(this.m_configuration.DatabaseName, this.m_configuration.SourceMachine, ex5.Message, ex5);
			}
			catch (IncrementalReseedRetryableException ex6)
			{
				ex = new IncSeedDivergenceCheckFailedException(this.m_configuration.DatabaseName, this.m_configuration.SourceMachine, ex6.Message, ex6);
			}
			catch (IOException ex7)
			{
				ex = new IncSeedDivergenceCheckFailedException(this.m_configuration.DatabaseName, this.m_configuration.SourceMachine, ex7.Message, ex7);
			}
			catch (UnauthorizedAccessException ex8)
			{
				ex = new IncSeedDivergenceCheckFailedException(this.m_configuration.DatabaseName, this.m_configuration.SourceMachine, ex8.Message, ex8);
			}
			catch (SecurityException ex9)
			{
				ex = new IncSeedDivergenceCheckFailedException(this.m_configuration.DatabaseName, this.m_configuration.SourceMachine, ex9.Message, ex9);
			}
			catch (NetworkRemoteException ex10)
			{
				ex = new IncSeedDivergenceCheckFailedException(this.m_configuration.DatabaseName, this.m_configuration.SourceMachine, ex10.Message, ex10);
			}
			catch (NetworkTransportException ex11)
			{
				ex = new IncSeedDivergenceCheckFailedException(this.m_configuration.DatabaseName, this.m_configuration.SourceMachine, ex11.Message, ex11);
			}
			catch (TransientException ex12)
			{
				ex = new IncSeedDivergenceCheckFailedException(this.m_configuration.DatabaseName, this.m_configuration.SourceMachine, ex12.Message, ex12);
			}
			finally
			{
				incReseedPerformanceTracker.IsIncReseedNeeded = isDiverged;
				incReseedPerformanceTracker.LogEvent();
			}
			if (ex != null)
			{
				ExTraceGlobals.ReplicaInstanceTracer.TraceError<string, IncSeedDivergenceCheckFailedException>((long)this.GetHashCode(), "AttemptCopyLastLogs.AreLogFilesDivergentFromSource(): {0}: IsIncrementalReseedRequired() threw exception: {1}", this.m_configuration.DisplayName, ex);
				errorString = new LocalizedString(ex.Message);
			}
			else if (isDiverged)
			{
				errorString = ReplayStrings.AcllFailedLogDivergenceDetected(this.m_configuration.DisplayName, this.m_configuration.SourceMachine);
			}
			ExTraceGlobals.ReplicaInstanceTracer.TraceDebug<string, bool>((long)this.GetHashCode(), "AttemptCopyLastLogs.AreLogFilesDivergentFromSource(): {0}: returning '{1}'.", this.m_configuration.DisplayName, isDiverged);
			return isDiverged;
		}

		// Token: 0x06001BC5 RID: 7109 RVA: 0x00077C40 File Offset: 0x00075E40
		private AmAcllReturnStatus GetReturnStatus()
		{
			AmAcllReturnStatus amAcllReturnStatus = new AmAcllReturnStatus();
			amAcllReturnStatus.NoLoss = this.NoLoss;
			amAcllReturnStatus.MountAllowed = this.MountAllowed;
			amAcllReturnStatus.MountDialOverrideUsed = this.MountDialOverrideUsed;
			amAcllReturnStatus.LastLogGenerationNotified = this.LastLogNotified;
			amAcllReturnStatus.LastLogGenerationShipped = this.LastLogShipped;
			amAcllReturnStatus.LastInspectedLogTime = this.ToNonNullableDateTime(new DateTime?(this.LastLogInspectedTime));
			amAcllReturnStatus.NumberOfLogsLost = this.NumberOfLogsLost;
			if (!this.ErrorMessage.IsEmpty)
			{
				amAcllReturnStatus.LastError = this.ErrorMessage;
			}
			return amAcllReturnStatus;
		}

		// Token: 0x06001BC6 RID: 7110 RVA: 0x00077CD4 File Offset: 0x00075ED4
		private DateTime ToNonNullableDateTime(DateTime? dateTime)
		{
			if (dateTime != null)
			{
				return dateTime.Value;
			}
			return this.m_defaultTime;
		}

		// Token: 0x04000B61 RID: 2913
		private readonly DateTime m_defaultTime = DateTime.FromFileTimeUtc(0L);

		// Token: 0x04000B62 RID: 2914
		private bool m_fNoLoss;

		// Token: 0x04000B63 RID: 2915
		private bool m_fMountAllowed;

		// Token: 0x04000B64 RID: 2916
		private bool m_fPerformDivergenceCheck;

		// Token: 0x04000B65 RID: 2917
		private bool m_fSkipHealthChecks;

		// Token: 0x04000B66 RID: 2918
		private LocalizedString m_errorMessage = LocalizedString.Empty;

		// Token: 0x04000B67 RID: 2919
		private DatabaseMountDialOverride m_mountDialOverride;

		// Token: 0x04000B68 RID: 2920
		private IPerfmonCounters m_perfmonCounters;

		// Token: 0x04000B69 RID: 2921
		private ReplayConfiguration m_configuration;

		// Token: 0x04000B6A RID: 2922
		private RemoteReplayConfiguration m_remoteConfig;

		// Token: 0x04000B6B RID: 2923
		private SimpleSetBroken m_setBrokenForCopier;

		// Token: 0x04000B6C RID: 2924
		private SimpleSetBroken m_setBrokenForInspector;

		// Token: 0x04000B6D RID: 2925
		private SimpleSetBroken m_setBrokenForReplayer;

		// Token: 0x04000B6E RID: 2926
		private SimpleSetBroken m_setBrokenForOther;

		// Token: 0x04000B6F RID: 2927
		private ISetGeneration m_setGeneration;

		// Token: 0x04000B70 RID: 2928
		private ISetViable m_setViable;

		// Token: 0x04000B71 RID: 2929
		private ReplayState m_state;

		// Token: 0x04000B72 RID: 2930
		private FileChecker m_fileChecker;

		// Token: 0x04000B73 RID: 2931
		private FileState m_fileState;

		// Token: 0x04000B74 RID: 2932
		private string m_uniqueOperationId;

		// Token: 0x04000B75 RID: 2933
		private int m_subactionAttemptNumber;

		// Token: 0x04000B76 RID: 2934
		private bool m_fCreatedCopier;

		// Token: 0x04000B77 RID: 2935
		private bool m_fCreatedInspector;

		// Token: 0x04000B78 RID: 2936
		private LogCopier m_logCopier;

		// Token: 0x04000B79 RID: 2937
		private LogInspector m_logInspector;

		// Token: 0x04000B7A RID: 2938
		private LogReplayer m_logReplayer;

		// Token: 0x04000B7B RID: 2939
		private NetworkPath m_initialNetworkPath;

		// Token: 0x04000B7C RID: 2940
		private AcllPerformanceTracker m_acllPerf;

		// Token: 0x04000B7D RID: 2941
		private LastLogInfo m_lastLogInfo;
	}
}
