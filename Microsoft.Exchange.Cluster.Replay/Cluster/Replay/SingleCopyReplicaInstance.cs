using System;
using Microsoft.Exchange.Cluster.ActiveManagerServer;
using Microsoft.Exchange.Rpc.ActiveManager;
using Microsoft.Exchange.Rpc.Cluster;
using Microsoft.Isam.Esent.Interop;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200031A RID: 794
	internal sealed class SingleCopyReplicaInstance : ReplicaInstance
	{
		// Token: 0x060020AA RID: 8362 RVA: 0x00096E7E File Offset: 0x0009507E
		public SingleCopyReplicaInstance(ReplayConfiguration replayConfiguration, IPerfmonCounters perfCounters) : base(replayConfiguration, false, null, perfCounters)
		{
			ReplicaInstance.DisposeIfActionUnsuccessful(delegate
			{
			}, this);
		}

		// Token: 0x1700089B RID: 2203
		// (get) Token: 0x060020AB RID: 8363 RVA: 0x00096EAD File Offset: 0x000950AD
		protected override bool ShouldAcquireSuspendLockInConfigChecker
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060020AC RID: 8364 RVA: 0x00096EB0 File Offset: 0x000950B0
		internal override bool GetSignatureAndCheckpoint(out JET_SIGNATURE? logfileSignature, out long lowestGenerationRequired, out long highestGenerationRequired, out long lastGenerationBackedUp)
		{
			lowestGenerationRequired = 0L;
			highestGenerationRequired = 0L;
			lastGenerationBackedUp = 0L;
			logfileSignature = base.FileChecker.FileState.LogfileSignature;
			if (logfileSignature == null)
			{
				base.FileChecker.TryUpdateActiveDatabaseLogfileSignature();
				logfileSignature = base.FileChecker.FileState.LogfileSignature;
			}
			return logfileSignature != null;
		}

		// Token: 0x060020AD RID: 8365 RVA: 0x00096F0F File Offset: 0x0009510F
		internal override AmAcllReturnStatus AttemptCopyLastLogsRcr(AmAcllArgs acllArgs, AcllPerformanceTracker acllPerf)
		{
			throw new AcllInvalidForSingleCopyException(base.Configuration.DisplayName);
		}

		// Token: 0x060020AE RID: 8366 RVA: 0x00096F21 File Offset: 0x00095121
		internal override void RequestSuspend(string suspendComment, DatabaseCopyActionFlags flags, ActionInitiatorType initiator)
		{
			throw new ReplayServiceSuspendRpcInvalidForSingleCopyException(base.Configuration.DisplayName);
		}

		// Token: 0x060020AF RID: 8367 RVA: 0x00096F33 File Offset: 0x00095133
		internal override void RequestResume(DatabaseCopyActionFlags flags)
		{
			throw new ReplayServiceResumeRpcInvalidForSingleCopyException(base.Configuration.DisplayName);
		}

		// Token: 0x060020B0 RID: 8368 RVA: 0x00096F45 File Offset: 0x00095145
		protected override bool ConfigurationCheckerInternal()
		{
			base.FileChecker.TryUpdateActiveDatabaseLogfileSignature();
			return true;
		}

		// Token: 0x060020B1 RID: 8369 RVA: 0x00096F53 File Offset: 0x00095153
		protected override void StartComponents()
		{
		}

		// Token: 0x060020B2 RID: 8370 RVA: 0x00096F55 File Offset: 0x00095155
		protected override void PrepareToStopInternal()
		{
		}

		// Token: 0x060020B3 RID: 8371 RVA: 0x00096F57 File Offset: 0x00095157
		protected override void StopInternal()
		{
		}
	}
}
