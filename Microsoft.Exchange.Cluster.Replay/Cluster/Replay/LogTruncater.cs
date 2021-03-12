using System;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Common.HA;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.HA;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.HA.FailureItem;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020002E8 RID: 744
	internal class LogTruncater : TimerComponent, ILogTruncater, IStartStop
	{
		// Token: 0x06001DE5 RID: 7653 RVA: 0x00088A4C File Offset: 0x00086C4C
		public LogTruncater(IPerfmonCounters perfmonCounters, IFileChecker fileChecker, ISetBroken setBroken, IReplayConfiguration configuration, ITruncationConfiguration truncationConfig, IReplicaInstanceContext replicaInstanceContext, ManualOneShotEvent shuttingDownEvent) : base(TimeSpan.Zero, TimeSpan.FromMilliseconds((double)RegistryParameters.LogTruncationTimerDuration), "LogTruncater")
		{
			this.m_perfmonCounters = perfmonCounters;
			this.m_fileChecker = fileChecker;
			this.m_configuration = configuration;
			this.m_truncationConfig = truncationConfig;
			this.m_replicaInstanceContext = replicaInstanceContext;
			this.m_shuttingDownEvent = shuttingDownEvent;
			this.m_localInspectorGen = this.m_configuration.ReplayState.InspectorGenerationNumber;
			this.m_localReplayerGen = this.m_configuration.ReplayState.ReplayGenerationNumber;
			this.m_perfmonCounters.TruncatedGenerationNumber = this.m_genTruncatedLocally;
			LogTruncater.Tracer.TraceDebug<IReplayConfiguration>((long)this.GetHashCode(), "LogTruncater initialized - configuration = {0}", configuration);
		}

		// Token: 0x170007F2 RID: 2034
		// (get) Token: 0x06001DE6 RID: 7654 RVA: 0x00088AF7 File Offset: 0x00086CF7
		public static Trace Tracer
		{
			get
			{
				return ExTraceGlobals.LogTruncaterTracer;
			}
		}

		// Token: 0x170007F3 RID: 2035
		// (get) Token: 0x06001DE7 RID: 7655 RVA: 0x00088AFE File Offset: 0x00086CFE
		private ITruncationConfiguration TruncationConfig
		{
			get
			{
				return this.m_truncationConfig;
			}
		}

		// Token: 0x170007F4 RID: 2036
		// (get) Token: 0x06001DE8 RID: 7656 RVA: 0x00088B06 File Offset: 0x00086D06
		private IReplayConfiguration Configuration
		{
			get
			{
				return this.m_configuration;
			}
		}

		// Token: 0x170007F5 RID: 2037
		// (get) Token: 0x06001DE9 RID: 7657 RVA: 0x00088B0E File Offset: 0x00086D0E
		private ReplayState State
		{
			get
			{
				return this.m_configuration.ReplayState;
			}
		}

		// Token: 0x170007F6 RID: 2038
		// (get) Token: 0x06001DEA RID: 7658 RVA: 0x00088B1B File Offset: 0x00086D1B
		private IFileChecker FileChecker
		{
			get
			{
				return this.m_fileChecker;
			}
		}

		// Token: 0x170007F7 RID: 2039
		// (get) Token: 0x06001DEB RID: 7659 RVA: 0x00088B24 File Offset: 0x00086D24
		private LogShipContextWrapper LogShipContext
		{
			get
			{
				if (this.m_logShipContext == null)
				{
					LogTruncater.Tracer.TraceDebug((long)this.GetHashCode(), "LogShipContext: Creating a new LogShipContextWrapper instance.");
					this.m_logShipContext = new LogShipContextWrapper(this.TruncationConfig, TimeSpan.FromSeconds((double)RegistryParameters.LogTruncationOpenContextTimeoutInSec), this.m_shuttingDownEvent);
				}
				return this.m_logShipContext;
			}
		}

		// Token: 0x06001DEC RID: 7660 RVA: 0x00088B78 File Offset: 0x00086D78
		public static long CalculateLowestGenerationRequired(IReplayConfiguration config, FileState fileState)
		{
			long num = fileState.LowestGenerationRequired;
			if (!config.CircularLoggingEnabled && fileState.LastGenerationBackedUp != 0L)
			{
				num = Math.Min(num, fileState.LastGenerationBackedUp);
			}
			return num;
		}

		// Token: 0x06001DED RID: 7661 RVA: 0x00088BAC File Offset: 0x00086DAC
		protected override void StopInternal()
		{
			LogTruncater.Tracer.TraceDebug((long)this.GetHashCode(), "LogTruncater stopping...");
			base.StopInternal();
			if (this.m_logShipContext != null)
			{
				LogTruncater.Tracer.TraceDebug<int>((long)this.GetHashCode(), "TruncateLogs: Calling LogShipContextWrapper.Dispose ({0}).", this.m_logShipContext.GetHashCode());
				this.m_logShipContext.Dispose();
				this.m_logShipContext = null;
			}
			base.LogStopEventAndSetFinalStopTime(this.Configuration.DatabaseName);
			LogTruncater.Tracer.TraceDebug((long)this.GetHashCode(), "LogTruncater Stopped.");
		}

		// Token: 0x06001DEE RID: 7662 RVA: 0x00088C37 File Offset: 0x00086E37
		public void RecordReplayGeneration(long genRequired)
		{
			LogTruncater.Tracer.TraceDebug<string, long>((long)this.GetHashCode(), "RecordReplayGeneration called on {0}. lowest genRequired = 0x{1:X}", this.Configuration.Name, genRequired);
			this.m_localReplayerGen = genRequired;
		}

		// Token: 0x06001DEF RID: 7663 RVA: 0x00088C62 File Offset: 0x00086E62
		public void RecordInspectorGeneration(long genInspected)
		{
			LogTruncater.Tracer.TraceDebug<string, long>((long)this.GetHashCode(), "RecordInspectorGeneration called on {0}. logFileNumber = 0x{1:X}", this.Configuration.Name, genInspected);
			this.m_localInspectorGen = genInspected;
		}

		// Token: 0x06001DF0 RID: 7664 RVA: 0x00088C90 File Offset: 0x00086E90
		public void StopTruncation()
		{
			LogTruncater.Tracer.TraceDebug((long)this.GetHashCode(), "LogTruncater is being stopped by component request.");
			base.Stop();
			LogTruncater.RequestGlobalTruncationCoordination(1L, this.m_configuration.SourceMachine, this.m_configuration.TargetMachine, this.m_configuration.IdentityGuid, this.m_configuration.LogFilePrefix, this.m_configuration.DestinationLogPath, this.m_configuration.CircularLoggingEnabled, this.m_shuttingDownEvent);
		}

		// Token: 0x06001DF1 RID: 7665 RVA: 0x00088D0C File Offset: 0x00086F0C
		internal static long RequestGlobalTruncationCoordination(long localTruncationPoint, string sourceMachineFqdn, string localNodeName, Guid identityGuid, string logPrefix, string destLogPath, bool circularLogging, ManualOneShotEvent cancelEvent)
		{
			long num = -1L;
			TimeSpan timeout = TimeSpan.FromSeconds((double)RegistryParameters.LogTruncationOpenContextTimeoutInSec);
			localNodeName = AmServerName.GetSimpleName(localNodeName);
			using (LogShipContextWrapper logShipContextWrapper = new LogShipContextWrapper(TestSupport.UseLocalMachineNameOnZerobox(sourceMachineFqdn), localNodeName, identityGuid, logPrefix, destLogPath, circularLogging, timeout, cancelEvent))
			{
				LogTruncater.Tracer.TraceDebug<Guid, string, long>(0L, "RequestGlobalTruncationCoordination for db {0}: Calling Notify() to source server {1}, with local truncation point of {2}.", identityGuid, sourceMachineFqdn, localTruncationPoint);
				logShipContextWrapper.Notify(localTruncationPoint, ref num);
				LogTruncater.Tracer.TraceDebug<Guid, long, long>(0L, "RequestGlobalTruncationCoordination for db {0} notified our lowest is 0x{1:X}, learned the global truncation is 0x{2:X}", identityGuid, localTruncationPoint, num);
			}
			return num;
		}

		// Token: 0x06001DF2 RID: 7666 RVA: 0x00088D98 File Offset: 0x00086F98
		private long CalculatePassiveTruncationPoint()
		{
			long num = Math.Min(this.m_lowestGenRequiredGlobally, LogTruncater.CalculateLowestGenerationRequired(this.Configuration, this.FileChecker.FileState));
			LogTruncater.Tracer.TraceDebug<long>((long)this.GetHashCode(), "PassiveTruncationPoint is now 0x{0:X}", num);
			return num;
		}

		// Token: 0x06001DF3 RID: 7667 RVA: 0x00088DE0 File Offset: 0x00086FE0
		private long GetExtendedGeneration(long inputGeneration)
		{
			int num = RegistryParameters.LogTruncationExtendedPreservation;
			if (num < 2)
			{
				num = 2;
			}
			if (num > 0 && inputGeneration > (long)num)
			{
				inputGeneration -= (long)num;
			}
			return inputGeneration;
		}

		// Token: 0x06001DF4 RID: 7668 RVA: 0x00088E0C File Offset: 0x0008700C
		private DateTime GetFileTime(long generationNumber)
		{
			string fileName = Path.Combine(this.Configuration.DestinationLogPath, EseHelper.MakeLogfileName(this.Configuration.LogFilePrefix, "." + this.Configuration.LogExtension, generationNumber));
			FileInfo fileInfo = new FileInfo(fileName);
			return fileInfo.LastWriteTimeUtc;
		}

		// Token: 0x06001DF5 RID: 7669 RVA: 0x00088E60 File Offset: 0x00087060
		private void ProbeForLowestLogPresent()
		{
			DirectoryInfo di = new DirectoryInfo(this.Configuration.DestinationLogPath);
			long lowestGenerationPresent = ShipControl.LowestGenerationInDirectory(di, this.Configuration.LogFilePrefix, "." + this.Configuration.LogExtension, false);
			this.FileChecker.FileState.SetLowestGenerationPresent(lowestGenerationPresent);
		}

		// Token: 0x06001DF6 RID: 7670 RVA: 0x00088EB8 File Offset: 0x000870B8
		protected override void TimerCallbackInternal()
		{
			LogTruncater.Tracer.TraceDebug<string, DateTime>((long)this.GetHashCode(), "TimerCallback: {0}: Entered at {1}", this.Configuration.Name, DateTime.UtcNow);
			if (base.PrepareToStopCalled || this.m_replicaInstanceContext.Initializing || this.m_replicaInstanceContext.Resynchronizing || this.m_replicaInstanceContext.Seeding)
			{
				LogTruncater.Tracer.TraceDebug<string>((long)this.GetHashCode(), "TimerCallback: {0}: The instance is stopping or checking stage, abort this timercallback", this.Configuration.Name);
				return;
			}
			if (this.Configuration.DestinationEdbPath == null)
			{
				LogTruncater.Tracer.TraceDebug<string>((long)this.GetHashCode(), "TimerCallback: {0}: Returns since we have no database.", this.Configuration.Name);
				return;
			}
			LogTruncater.Tracer.TraceDebug((long)this.GetHashCode(), "TimerCallback: {0} : m_localReplayerGen = 0x{1:X}, m_localInspectorGen = 0x{2:X}, m_lowestGenRequiredGlobally = 0x{3:X}, m_genTruncatedLocally = 0x{4:X}.", new object[]
			{
				this.Configuration.Name,
				this.m_localReplayerGen,
				this.m_localInspectorGen,
				this.m_lowestGenRequiredGlobally,
				this.m_genTruncatedLocally
			});
			long num = this.CalculateGlobalTruncateInput();
			if (num <= 0L)
			{
				LogTruncater.Tracer.TraceDebug<string>((long)this.GetHashCode(), "TimerCallback: {0}: Exiting because inspector/replayer haven't established position yet.", this.Configuration.Name);
				return;
			}
			num = this.GetExtendedGeneration(num);
			Exception arg = null;
			bool flag = false;
			try
			{
				this.LogShipCall(num, false);
				long num2 = this.CalculatePassiveTruncationPoint();
				LooseTruncation looseTruncation = new LooseTruncation();
				if (looseTruncation.Enabled && looseTruncation.SpaceIsLow(this.m_configuration))
				{
					long num3 = looseTruncation.MinRequiredGen(this.FileChecker, this.Configuration);
					LogTruncater.Tracer.TraceDebug<long, long>((long)this.GetHashCode(), "LooseTruncation MinRequiredGen={0} NormalLocalGen={1}", num3, num2);
					if (num3 > num2)
					{
						ReplayCrimsonEvents.LooseTruncationOnPassiveBeingUsed.LogPeriodic<string, string, long, long>(this.Configuration.Identity, DiagCore.DefaultEventSuppressionInterval, this.Configuration.Identity, this.Configuration.DatabaseName, num2, num3);
						num2 = num3;
					}
				}
				if (num2 > this.m_genTruncatedLocally)
				{
					long num4 = num2;
					if (this.m_configuration.TruncationLagTime != EnhancedTimeSpan.Zero)
					{
						if (this.m_genTruncatedLocally == 0L)
						{
							num4 = this.FileChecker.FileState.LowestGenerationPresent;
						}
						else
						{
							num4 = this.m_genTruncatedLocally;
						}
						while (num4 < num2)
						{
							if (base.PrepareToStopCalled)
							{
								throw new OperationAbortedException();
							}
							DateTime fileTime = this.GetFileTime(num4);
							if ((DateTime)ExDateTime.UtcNow - fileTime < this.m_configuration.TruncationLagTime)
							{
								LogTruncater.Tracer.TraceDebug<long, DateTime, string>((long)this.GetHashCode(), "TimerCallback: {2}: Log 0x{0:X} with time '{1}' protected by TruncationLag", num4, fileTime, this.Configuration.Name);
								break;
							}
							num4 += 1L;
						}
					}
					if (num4 != this.m_genTruncatedLocally)
					{
						for (long num5 = this.FileChecker.FileState.LowestGenerationPresent; num5 < num4; num5 += 1L)
						{
							string text = this.Configuration.BuildFullLogfileName(num5);
							Exception ex = FileCleanup.Delete(text);
							if (ex != null)
							{
								LogTruncater.Tracer.TraceError<string, Exception>((long)this.GetHashCode(), "Truncation failed to delete {0}: {1}", text, ex);
								uint hrforException = (uint)Marshal.GetHRForException(ex);
								throw new FailedToTruncateLocallyException(hrforException, ex.Message, ex);
							}
							if (base.PrepareToStopCalled)
							{
								throw new OperationAbortedException();
							}
						}
						this.m_genTruncatedLocally = num4;
						this.ProbeForLowestLogPresent();
					}
				}
				flag = true;
			}
			catch (OperationAbortedException arg2)
			{
				flag = true;
				LogTruncater.Tracer.TraceDebug<OperationAbortedException>((long)this.GetHashCode(), "Stop detected: {0}", arg2);
			}
			catch (IOException ex2)
			{
				flag = true;
				LogTruncater.Tracer.TraceError<string, IOException>((long)this.GetHashCode(), "TimerCallback: {0}: Got an IO exception and probably the file we are going to delete doesn't exist : {1}", this.Configuration.Name, ex2);
				ReplayEventLogConstants.Tuple_LogTruncationLocalFailure.LogEvent(null, new object[]
				{
					this.Configuration.DisplayName,
					ex2.Message
				});
			}
			catch (FailedToOpenLogTruncContextException ex3)
			{
				arg = ex3;
				ReplayEventLogConstants.Tuple_LogTruncationOpenFailed.LogEvent(this.Configuration.Identity, new object[]
				{
					this.Configuration.DisplayName,
					ex3.Hresult,
					ex3.Message
				});
			}
			catch (CopyUnknownToActiveLogTruncationException ex4)
			{
				arg = ex4;
				ReplayEventLogConstants.Tuple_LogTruncationOpenFailed.LogEvent(this.Configuration.Identity, new object[]
				{
					this.Configuration.DisplayName,
					ex4.Hresult,
					ex4.Message
				});
			}
			catch (FailedToNotifySourceLogTruncException ex5)
			{
				arg = ex5;
				ReplayEventLogConstants.Tuple_LogTruncationSourceFailure.LogEvent(null, new object[]
				{
					this.Configuration.DisplayName,
					ex5.Message
				});
			}
			catch (FailedToTruncateLocallyException ex6)
			{
				arg = ex6;
				ReplayEventLogConstants.Tuple_LogTruncationLocalFailure.LogEvent(this.Configuration.DisplayName, new object[]
				{
					this.Configuration.DisplayName,
					ex6.Message
				});
				FailureTag failureTag = FailureTag.NoOp;
				uint hresult = ex6.Hresult;
				if (hresult == 3355444222U)
				{
					failureTag = FailureTag.IoHard;
				}
				if (failureTag != FailureTag.NoOp)
				{
					FailureItemPublisherHelper.PublishAction(failureTag, this.m_configuration.IdentityGuid, this.m_configuration.DatabaseName);
				}
			}
			finally
			{
				if (!flag)
				{
					LogTruncater.Tracer.TraceError<string, Exception>((long)this.GetHashCode(), "TimerCallback: {0}: Esebcli2 call failed, or unhandled exception occurred. Disposing m_logShipContext... Exception: {1}", this.Configuration.Name, arg);
					if (this.m_logShipContext != null)
					{
						this.m_logShipContext.Dispose();
						this.m_logShipContext = null;
					}
					LogTruncater.Tracer.TraceDebug<string>((long)this.GetHashCode(), "TimerCallback: {0}: m_logShipContext disposed.", this.Configuration.Name);
				}
			}
			this.m_perfmonCounters.TruncatedGenerationNumber = this.m_genTruncatedLocally;
		}

		// Token: 0x06001DF7 RID: 7671 RVA: 0x000894FC File Offset: 0x000876FC
		private long CalculateGlobalTruncateInput()
		{
			long num = this.m_localInspectorGen;
			bool flag = true;
			if (this.Configuration.ReplayLagTime != EnhancedTimeSpan.Zero && !RegistryParameters.LogTruncationKeepAllLogsForLagCopy)
			{
				flag = false;
			}
			if (flag)
			{
				num = Math.Min(num, this.m_localReplayerGen);
			}
			return num;
		}

		// Token: 0x06001DF8 RID: 7672 RVA: 0x00089544 File Offset: 0x00087744
		private void LogShipCall(long lgenDone, bool fLocal)
		{
			long num = 0L;
			if (fLocal)
			{
				LogTruncater.Tracer.TraceDebug<string, long>((long)this.GetHashCode(), "LogShipCall: {0}: Calling local truncate with lgenDone = 0x{1:X}", this.Configuration.Name, lgenDone);
				this.LogShipContext.Truncate(lgenDone, ref num);
				this.m_genTruncatedLocally = num;
				LogTruncater.Tracer.TraceDebug<string, long>((long)this.GetHashCode(), "LogShipCall: {0}: Local truncate call returned m_genTruncatedLocally = 0x{1:X}.", this.Configuration.Name, this.m_genTruncatedLocally);
				return;
			}
			long lowestGenRequiredGlobally = this.m_lowestGenRequiredGlobally;
			LogTruncater.Tracer.TraceDebug((long)this.GetHashCode(), "LogShipCall: {0}: Notifying source server '{1}' about our position. lgenDone = 0x{2:X}, m_lowestGenRequiredGlobally = 0x{3:X}.", new object[]
			{
				this.Configuration.Name,
				this.Configuration.SourceMachine,
				lgenDone,
				this.m_lowestGenRequiredGlobally
			});
			this.LogShipContext.Notify(lgenDone, ref num);
			this.m_lowestGenRequiredGlobally = num;
			LogTruncater.Tracer.TraceDebug<string, long>((long)this.GetHashCode(), "LogShipCall: {0}: Source server Notify() returned m_lowestGenRequiredGlobally = 0x{1:X}.", this.Configuration.Name, this.m_lowestGenRequiredGlobally);
			if (lowestGenRequiredGlobally != this.m_lowestGenRequiredGlobally)
			{
				ReplayCrimsonEvents.LogTruncationStateChange.Log<string, string, string, string, long, long>(this.m_configuration.Name, this.m_configuration.Identity, this.m_configuration.ServerName, "GlobalTruncationPoint", lowestGenRequiredGlobally, this.m_lowestGenRequiredGlobally);
			}
		}

		// Token: 0x04000C91 RID: 3217
		private readonly IPerfmonCounters m_perfmonCounters;

		// Token: 0x04000C92 RID: 3218
		private readonly IFileChecker m_fileChecker;

		// Token: 0x04000C93 RID: 3219
		private readonly IReplayConfiguration m_configuration;

		// Token: 0x04000C94 RID: 3220
		private readonly ITruncationConfiguration m_truncationConfig;

		// Token: 0x04000C95 RID: 3221
		private IReplicaInstanceContext m_replicaInstanceContext;

		// Token: 0x04000C96 RID: 3222
		private LogShipContextWrapper m_logShipContext;

		// Token: 0x04000C97 RID: 3223
		private ManualOneShotEvent m_shuttingDownEvent;

		// Token: 0x04000C98 RID: 3224
		private long m_localReplayerGen;

		// Token: 0x04000C99 RID: 3225
		private long m_localInspectorGen;

		// Token: 0x04000C9A RID: 3226
		private long m_lowestGenRequiredGlobally;

		// Token: 0x04000C9B RID: 3227
		private long m_genTruncatedLocally;
	}
}
