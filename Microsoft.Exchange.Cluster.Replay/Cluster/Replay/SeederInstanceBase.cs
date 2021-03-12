using System;
using System.Threading;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Common.Cluster;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Cluster;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;
using Microsoft.Exchange.Rpc.Cluster;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200028F RID: 655
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class SeederInstanceBase : IIdentityGuid
	{
		// Token: 0x17000702 RID: 1794
		// (get) Token: 0x06001984 RID: 6532 RVA: 0x0006AA39 File Offset: 0x00068C39
		public Guid DatabaseGuid
		{
			get
			{
				return this.SeederArgs.InstanceGuid;
			}
		}

		// Token: 0x17000703 RID: 1795
		// (get) Token: 0x06001985 RID: 6533 RVA: 0x0006AA46 File Offset: 0x00068C46
		public string DatabaseName
		{
			get
			{
				return this.ConfigArgs.Name;
			}
		}

		// Token: 0x06001986 RID: 6534 RVA: 0x0006AA54 File Offset: 0x00068C54
		protected SeederInstanceBase(RpcSeederArgs rpcArgs, ConfigurationArgs configArgs)
		{
			this.SeederArgs = rpcArgs;
			this.ConfigArgs = configArgs;
			this.m_seederStatus = new RpcSeederStatus();
			ExTraceGlobals.SeederServerTracer.TraceDebug<string, string>((long)this.GetHashCode(), "SeederInstanceBase constructed with the following arguments: {0}; {1}", this.SeederArgs.ToString(), this.ConfigArgs.ToString());
			this.InitializePerfCounters();
			this.m_completedTimeUtc = DateTime.MaxValue;
			if (!string.IsNullOrEmpty(rpcArgs.SourceMachineName) && !SharedHelper.StringIEquals(rpcArgs.SourceMachineName, configArgs.SourceMachine))
			{
				this.m_fPassiveSeeding = true;
			}
		}

		// Token: 0x17000704 RID: 1796
		// (get) Token: 0x06001987 RID: 6535 RVA: 0x0006AAE4 File Offset: 0x00068CE4
		// (set) Token: 0x06001988 RID: 6536 RVA: 0x0006AAEC File Offset: 0x00068CEC
		public DateTime CompletedTimeUtc
		{
			get
			{
				return this.m_completedTimeUtc;
			}
			internal set
			{
				this.m_completedTimeUtc = value;
			}
		}

		// Token: 0x17000705 RID: 1797
		// (get) Token: 0x06001989 RID: 6537 RVA: 0x0006AAF8 File Offset: 0x00068CF8
		// (set) Token: 0x0600198A RID: 6538 RVA: 0x0006AB48 File Offset: 0x00068D48
		public SeederState SeedState
		{
			get
			{
				SeederState lastStateRead;
				lock (this)
				{
					this.m_lastStateRead = this.m_seederStatus.State;
					lastStateRead = this.m_lastStateRead;
				}
				return lastStateRead;
			}
			internal set
			{
				this.m_seederStatus.State = value;
				this.m_lastStateRead = value;
			}
		}

		// Token: 0x17000706 RID: 1798
		// (get) Token: 0x0600198B RID: 6539 RVA: 0x0006AB5D File Offset: 0x00068D5D
		public string Name
		{
			get
			{
				return this.ConfigArgs.Name;
			}
		}

		// Token: 0x17000707 RID: 1799
		// (get) Token: 0x0600198C RID: 6540
		public abstract string Identity { get; }

		// Token: 0x17000708 RID: 1800
		// (get) Token: 0x0600198D RID: 6541 RVA: 0x0006AB6A File Offset: 0x00068D6A
		protected ISetSeeding SetSeedingCallback
		{
			get
			{
				if (this.m_setSeedingCallback == null)
				{
					this.m_setSeedingCallback = this.GetSeederStatusCallback();
				}
				return this.m_setSeedingCallback;
			}
		}

		// Token: 0x17000709 RID: 1801
		// (get) Token: 0x0600198E RID: 6542 RVA: 0x0006AB86 File Offset: 0x00068D86
		protected ISetGeneration SetGenerationCallback
		{
			get
			{
				if (this.m_setGenerationCallback == null)
				{
					this.m_setGenerationCallback = this.GetSetGenerationCallback();
				}
				return this.m_setGenerationCallback;
			}
		}

		// Token: 0x1700070A RID: 1802
		// (get) Token: 0x0600198F RID: 6543 RVA: 0x0006ABA2 File Offset: 0x00068DA2
		protected ReplicaSeederPerfmonInstance SeederPerfmonInstance
		{
			get
			{
				return this.m_perfmon;
			}
		}

		// Token: 0x06001990 RID: 6544
		protected abstract void ResetPerfmonSeedingProgress();

		// Token: 0x06001991 RID: 6545 RVA: 0x0006ABAC File Offset: 0x00068DAC
		public RpcSeederStatus GetSeedStatus()
		{
			RpcSeederStatus result;
			lock (this)
			{
				RpcSeederStatus seederStatus = this.m_seederStatus;
				result = new RpcSeederStatus(seederStatus);
			}
			return result;
		}

		// Token: 0x06001992 RID: 6546 RVA: 0x0006ABF0 File Offset: 0x00068DF0
		public void BeginDbSeed()
		{
			SeederState seederState;
			SeederState seederState2;
			if (!this.UpdateState(SeederState.SeedInProgress, out seederState, out seederState2))
			{
				throw new SeederOperationAbortedException();
			}
			this.m_seederThread = new Thread(new ThreadStart(this.SeedThreadProc));
			this.m_seederThread.Start();
			ExTraceGlobals.SeederServerTracer.TraceDebug((long)this.GetHashCode(), "SeederInstanceBase.BeginDbSeed: Started background worker thread for seeding.");
		}

		// Token: 0x06001993 RID: 6547 RVA: 0x0006AC48 File Offset: 0x00068E48
		public void WaitUntilStopped()
		{
			try
			{
				Thread seederThread = this.m_seederThread;
				if (seederThread != null)
				{
					seederThread.Join();
					this.m_seederThread = null;
				}
			}
			finally
			{
				this.Cleanup();
			}
		}

		// Token: 0x06001994 RID: 6548 RVA: 0x0006AC90 File Offset: 0x00068E90
		protected void SeedThreadProc()
		{
			try
			{
				RpcErrorExceptionInfo rpcErrorExceptionInfo = SeederRpcExceptionWrapper.Instance.RunRpcServerOperation(this.ConfigArgs.Name, delegate()
				{
					this.SeedThreadProcInternal();
				});
				if (rpcErrorExceptionInfo != null && rpcErrorExceptionInfo.IsFailed())
				{
					this.LogErrorExceptionInfo(rpcErrorExceptionInfo, false);
				}
				ExTraceGlobals.SeederServerTracer.TraceDebug((long)this.GetHashCode(), "DatabaseSeederInstance: StoppedEvent set.");
			}
			finally
			{
				this.m_completedTimeUtc = DateTime.UtcNow;
			}
		}

		// Token: 0x06001995 RID: 6549
		protected abstract void SeedThreadProcInternal();

		// Token: 0x06001996 RID: 6550 RVA: 0x0006AD10 File Offset: 0x00068F10
		protected void CreateDirectoryIfNecessary(string directoryPath)
		{
			Exception ex = DirectoryOperations.TryCreateDirectory(directoryPath);
			if (ex != null)
			{
				this.LogError(ReplayStrings.SeederFailedToCreateDirectory(directoryPath, ex.ToString()));
			}
		}

		// Token: 0x06001997 RID: 6551 RVA: 0x0006AD40 File Offset: 0x00068F40
		protected void LogError(string error)
		{
			ReplayCrimsonEvents.SeedingErrorOnTarget.Log<Guid, string, string>(this.DatabaseGuid, this.DatabaseName, error);
			ExTraceGlobals.SeederServerTracer.TraceError<string, string>((long)this.GetHashCode(), "LogError: Database ({0}) failed to seed. Reason: {1}", this.ConfigArgs.Name, error);
			bool flag = false;
			try
			{
				Monitor.Enter(this, ref flag);
				SeederState seedState = this.SeedState;
				Exception exception = this.GetException(error, seedState);
				throw exception;
			}
			finally
			{
				if (flag)
				{
					Monitor.Exit(this);
					goto IL_60;
				}
				goto IL_60;
				IL_60:;
			}
		}

		// Token: 0x06001998 RID: 6552 RVA: 0x0006ADC0 File Offset: 0x00068FC0
		protected void LogError(Exception ex)
		{
			ReplayCrimsonEvents.SeedingErrorOnTarget.Log<Guid, string, string>(this.DatabaseGuid, this.DatabaseName, ex.ToString());
			ExTraceGlobals.SeederServerTracer.TraceError<string, Exception>((long)this.GetHashCode(), "LogError: Database ({0}) failed to seed. Reason: {1}", this.ConfigArgs.Name, ex);
			bool flag = false;
			try
			{
				Monitor.Enter(this, ref flag);
				SeederState seedState = this.SeedState;
				Exception exception = this.GetException(this.GetAppropriateErrorMessage(ex), seedState, ex);
				throw exception;
			}
			finally
			{
				if (flag)
				{
					Monitor.Exit(this);
					goto IL_6C;
				}
				goto IL_6C;
				IL_6C:;
			}
		}

		// Token: 0x06001999 RID: 6553 RVA: 0x0006AE4C File Offset: 0x0006904C
		protected void LogErrorExceptionInfo(RpcErrorExceptionInfo errorExceptionInfo, bool fThrow)
		{
			Exception ex = null;
			lock (this)
			{
				ExTraceGlobals.SeederServerTracer.TraceError<string, RpcErrorExceptionInfo>((long)this.GetHashCode(), "LogErrorExceptionInfo: Database ({0}) failed to seed. errorExceptionInfo = ({1})", this.ConfigArgs.Name, errorExceptionInfo);
				SeederState seederState;
				SeederState origState;
				bool flag2 = this.UpdateState(SeederState.SeedFailed, out seederState, out origState);
				string errorMessageAndExceptionFromErrorExceptionInfo = this.GetErrorMessageAndExceptionFromErrorExceptionInfo(errorExceptionInfo, origState, out ex);
				string text = string.Format("Msg={0} Ex={1}", errorMessageAndExceptionFromErrorExceptionInfo, ex);
				ReplayCrimsonEvents.SeedingErrorOnTarget.Log<Guid, string, string>(this.DatabaseGuid, this.DatabaseName, text);
				if (flag2 || seederState == SeederState.SeedCancelled || !Cluster.StringIEquals(this.m_lastErrorMessage, errorMessageAndExceptionFromErrorExceptionInfo))
				{
					ExEventLog.EventTuple eventTupleFromState = this.GetEventTupleFromState(origState);
					eventTupleFromState.LogEvent(null, new object[]
					{
						this.ConfigArgs.Name,
						errorMessageAndExceptionFromErrorExceptionInfo
					});
					ExTraceGlobals.SeederServerTracer.TraceError<string, Exception>((long)this.GetHashCode(), "Database ({0}) failed to seed with the exception: {1}", this.ConfigArgs.Name, ex);
					this.m_seederStatus.ErrorInfo = errorExceptionInfo;
					this.m_lastErrorMessage = errorMessageAndExceptionFromErrorExceptionInfo;
					if (this.m_setSeedingCallback != null)
					{
						this.CallFailedDbSeed(eventTupleFromState, ex);
					}
				}
				else
				{
					this.m_lastErrorMessage = ex.Message;
				}
			}
			this.CloseSeeding(false);
			if (ex != null && fThrow)
			{
				throw ex;
			}
		}

		// Token: 0x0600199A RID: 6554 RVA: 0x0006AFA0 File Offset: 0x000691A0
		protected virtual void CallFailedDbSeed(ExEventLog.EventTuple tuple, Exception ex)
		{
			this.m_setSeedingCallback.FailedDbSeed(tuple, new LocalizedString(ex.Message), new ExtendedErrorInfo(ex));
		}

		// Token: 0x0600199B RID: 6555
		protected abstract void CloseSeeding(bool wasSeedSuccessful);

		// Token: 0x0600199C RID: 6556
		protected abstract void Cleanup();

		// Token: 0x0600199D RID: 6557 RVA: 0x0006AFC0 File Offset: 0x000691C0
		protected virtual bool UpdateState(SeederState intendedState, out SeederState actualState, out SeederState origState)
		{
			bool result = false;
			lock (this)
			{
				actualState = (origState = this.SeedState);
				if (SeedStateGraph.IsTransitionPossible(actualState, intendedState))
				{
					actualState = intendedState;
					this.m_seederStatus.State = intendedState;
					result = true;
					ExTraceGlobals.SeederServerTracer.TraceDebug<SeederState, SeederState>((long)this.GetHashCode(), "DatabaseSeederInstance.UpdateState: Updated seeder state from '{0}' to '{1}'", origState, intendedState);
				}
				else
				{
					ExTraceGlobals.SeederServerTracer.TraceDebug<SeederState, SeederState>((long)this.GetHashCode(), "DatabaseSeederInstance.UpdateState: Could not update seeder state from '{0}' to '{1}'", origState, intendedState);
				}
			}
			return result;
		}

		// Token: 0x0600199E RID: 6558 RVA: 0x0006B054 File Offset: 0x00069254
		protected void CheckOperationCancelled()
		{
			if (this.m_fcancelled)
			{
				throw new SeederOperationAbortedException();
			}
		}

		// Token: 0x0600199F RID: 6559 RVA: 0x0006B064 File Offset: 0x00069264
		private void InitializePerfCounters()
		{
			try
			{
				this.m_perfmon = ReplicaSeederPerfmon.GetInstance(this.ConfigArgs.Name);
				this.ResetPerfmonSeedingProgress();
				ExTraceGlobals.SeederServerTracer.TraceDebug<string, string>((long)this.GetHashCode(), "DatabaseSeederInstance.InitializePerfCounters initialized for {0} ({1})", this.ConfigArgs.Name, this.Identity);
			}
			catch (InvalidOperationException ex)
			{
				ExTraceGlobals.SeederServerTracer.TraceError<string, string, InvalidOperationException>((long)this.GetHashCode(), "Failed to initialize seeder performance counters for {0} ({1}): {2}", this.ConfigArgs.Name, this.Identity, ex);
				ReplayEventLogConstants.Tuple_SeederPerfCountersLoadFailure.LogEvent(null, new object[]
				{
					this.ConfigArgs.Name,
					ex.ToString()
				});
			}
		}

		// Token: 0x060019A0 RID: 6560 RVA: 0x0006B11C File Offset: 0x0006931C
		private ISetSeeding GetSeederStatusCallback()
		{
			IReplicaInstanceManager replicaInstanceManager = this.ConfigArgs.ReplicaInstanceManager;
			if (replicaInstanceManager == null)
			{
				return null;
			}
			ISetSeeding setSeeding = null;
			try
			{
				setSeeding = replicaInstanceManager.GetSeederStatusCallback(this.SeederArgs.InstanceGuid);
			}
			catch (TaskServerTransientException ex)
			{
				ExTraceGlobals.SeederServerTracer.TraceError<Guid, TaskServerTransientException>((long)this.GetHashCode(), "GetSeederStatusCallback ({0}): ReplicaInstanceManager threw exception: {1}", this.SeederArgs.InstanceGuid, ex);
				this.LogError(ex);
			}
			catch (TaskServerException ex2)
			{
				ExTraceGlobals.SeederServerTracer.TraceError<Guid, TaskServerException>((long)this.GetHashCode(), "GetSeederStatusCallback ({0}): ReplicaInstanceManager threw exception: {1}", this.SeederArgs.InstanceGuid, ex2);
				this.LogError(ex2);
			}
			if (setSeeding == null)
			{
				ExTraceGlobals.SeederServerTracer.TraceDebug<Guid>((long)this.GetHashCode(), "GetSeederStatusCallback ({0}): The RI is not running even after re-running ConfigUpdater. The configuration must have gone away.", this.SeederArgs.InstanceGuid);
				throw new SeederOperationAbortedException();
			}
			return setSeeding;
		}

		// Token: 0x060019A1 RID: 6561 RVA: 0x0006B1F0 File Offset: 0x000693F0
		private ISetGeneration GetSetGenerationCallback()
		{
			IReplicaInstanceManager replicaInstanceManager = this.ConfigArgs.ReplicaInstanceManager;
			if (replicaInstanceManager == null)
			{
				return null;
			}
			ISetGeneration setGeneration = null;
			try
			{
				setGeneration = replicaInstanceManager.GetSetGenerationCallback(this.SeederArgs.InstanceGuid);
			}
			catch (TaskServerTransientException ex)
			{
				ExTraceGlobals.SeederServerTracer.TraceError<Guid, TaskServerTransientException>((long)this.GetHashCode(), "GetSetGenerationCallback ({0}): ReplicaInstanceManager threw exception: {1}", this.SeederArgs.InstanceGuid, ex);
				this.LogError(ex);
			}
			catch (TaskServerException ex2)
			{
				ExTraceGlobals.SeederServerTracer.TraceError<Guid, TaskServerException>((long)this.GetHashCode(), "GetSetGenerationCallback ({0}): ReplicaInstanceManager threw exception: {1}", this.SeederArgs.InstanceGuid, ex2);
				this.LogError(ex2);
			}
			if (setGeneration == null)
			{
				ExTraceGlobals.SeederServerTracer.TraceDebug<Guid>((long)this.GetHashCode(), "GetSetGenerationCallback ({0}): The RI is not running even after re-running ConfigUpdater. The configuration must have gone away.", this.SeederArgs.InstanceGuid);
				throw new SeederOperationAbortedException();
			}
			return setGeneration;
		}

		// Token: 0x060019A2 RID: 6562 RVA: 0x0006B2C4 File Offset: 0x000694C4
		private string GetErrorMessageAndExceptionFromErrorExceptionInfo(RpcErrorExceptionInfo errorExceptionInfo, SeederState origState, out Exception newException)
		{
			string text;
			if (errorExceptionInfo.ReconstitutedException != null)
			{
				text = this.GetMessageAndException(errorExceptionInfo.ReconstitutedException, origState, out newException);
			}
			else
			{
				text = errorExceptionInfo.ErrorMessage;
				newException = this.GetException(text, origState);
			}
			return text;
		}

		// Token: 0x060019A3 RID: 6563 RVA: 0x0006B300 File Offset: 0x00069500
		private string GetAppropriateErrorMessage(Exception ex)
		{
			string result = (ex != null) ? ex.Message : null;
			if (ex is SeedPrepareException)
			{
				result = ((SeedPrepareException)ex).ErrMessage;
			}
			else if (ex is SeedInProgressException)
			{
				result = ((SeedInProgressException)ex).ErrMessage;
			}
			else if (ex is IHaRpcServerBaseException)
			{
				result = ((IHaRpcServerBaseException)ex).ErrorMessage;
			}
			return result;
		}

		// Token: 0x060019A4 RID: 6564 RVA: 0x0006B35C File Offset: 0x0006955C
		private string GetMessageAndException(Exception ex, SeederState origState, out Exception newException)
		{
			newException = null;
			string text;
			if (ex is SeedPrepareException)
			{
				newException = ex;
				text = ((SeedPrepareException)ex).ErrMessage;
			}
			else if (ex is SeedInProgressException)
			{
				newException = ex;
				text = ((SeedInProgressException)ex).ErrMessage;
			}
			else if (ex is SeederServerException)
			{
				newException = ex;
				text = ((SeederServerException)ex).ErrorMessage;
			}
			else if (ex is SeederServerTransientException)
			{
				newException = ex;
				text = ((SeederServerTransientException)ex).ErrorMessage;
			}
			else if (ex is IHaRpcServerBaseException)
			{
				text = ((IHaRpcServerBaseException)ex).ErrorMessage;
			}
			else
			{
				text = ex.Message;
			}
			if (newException == null)
			{
				newException = this.GetException(text, origState);
			}
			return text;
		}

		// Token: 0x060019A5 RID: 6565 RVA: 0x0006B3FE File Offset: 0x000695FE
		private Exception GetException(string error, SeederState origState)
		{
			return this.GetException(error, origState, null);
		}

		// Token: 0x060019A6 RID: 6566 RVA: 0x0006B40C File Offset: 0x0006960C
		private Exception GetException(string error, SeederState origState, Exception innerException)
		{
			ExTraceGlobals.SeederServerTracer.TraceDebug<SeederState>((long)this.GetHashCode(), "DatabaseSeederInstance: SeederState={0}", origState);
			switch (origState)
			{
			case SeederState.Unknown:
			case SeederState.SeedPrepared:
				return new SeedPrepareException(error, innerException);
			case SeederState.SeedInProgress:
				return new SeedInProgressException(error, innerException);
			case SeederState.SeedCancelled:
				return new SeederOperationAbortedException(innerException);
			}
			return new SeederOperationFailedException(error, innerException);
		}

		// Token: 0x060019A7 RID: 6567 RVA: 0x0006B478 File Offset: 0x00069678
		private ExEventLog.EventTuple GetEventTupleFromState(SeederState origState)
		{
			switch (origState)
			{
			case SeederState.Unknown:
			case SeederState.SeedPrepared:
				return ReplayEventLogConstants.Tuple_SeedInstancePrepareFailed;
			case SeederState.SeedInProgress:
				return ReplayEventLogConstants.Tuple_SeedInstanceInProgressFailed;
			case SeederState.SeedCancelled:
				return ReplayEventLogConstants.Tuple_SeedInstanceCancelled;
			}
			return ReplayEventLogConstants.Tuple_SeedInstanceAnotherError;
		}

		// Token: 0x060019A8 RID: 6568 RVA: 0x0006B4C2 File Offset: 0x000696C2
		protected void ReadSeedTestHook()
		{
			this.m_testHookSeedDelayPerCallback = RegistryTestHook.SeedDelayPerCallbackInMilliSeconds;
			this.m_testHookSeedFailAfterProgressPercent = RegistryTestHook.SeedFailAfterProgressPercent;
			this.m_testHookSeedDisableTruncationCoordination = RegistryTestHook.SeedDisableTruncationCoordination;
		}

		// Token: 0x04000A40 RID: 2624
		protected readonly RpcSeederArgs SeederArgs;

		// Token: 0x04000A41 RID: 2625
		protected readonly ConfigurationArgs ConfigArgs;

		// Token: 0x04000A42 RID: 2626
		protected RpcSeederStatus m_seederStatus;

		// Token: 0x04000A43 RID: 2627
		protected ISetSeeding m_setSeedingCallback;

		// Token: 0x04000A44 RID: 2628
		protected ISetGeneration m_setGenerationCallback;

		// Token: 0x04000A45 RID: 2629
		protected string m_lastErrorMessage;

		// Token: 0x04000A46 RID: 2630
		protected bool m_fcancelled;

		// Token: 0x04000A47 RID: 2631
		protected bool m_fPassiveSeeding;

		// Token: 0x04000A48 RID: 2632
		private Thread m_seederThread;

		// Token: 0x04000A49 RID: 2633
		private ReplicaSeederPerfmonInstance m_perfmon;

		// Token: 0x04000A4A RID: 2634
		private DateTime m_completedTimeUtc;

		// Token: 0x04000A4B RID: 2635
		private SeederState m_lastStateRead;

		// Token: 0x04000A4C RID: 2636
		protected int m_testHookSeedDelayPerCallback;

		// Token: 0x04000A4D RID: 2637
		protected int m_testHookSeedFailAfterProgressPercent;

		// Token: 0x04000A4E RID: 2638
		protected bool m_testHookSeedDisableTruncationCoordination;
	}
}
