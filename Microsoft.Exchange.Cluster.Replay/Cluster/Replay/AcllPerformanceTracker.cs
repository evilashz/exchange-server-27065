using System;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Rpc.Cluster;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020002C3 RID: 707
	internal class AcllPerformanceTracker : FailoverPerformanceTrackerBase<AcllTimedOperation>
	{
		// Token: 0x06001B56 RID: 6998 RVA: 0x00075A91 File Offset: 0x00073C91
		public AcllPerformanceTracker(ReplayConfiguration config, string uniqueOperationId, int subactionAttemptNumber) : base("AcllPerf")
		{
			this.m_uniqueOperationId = uniqueOperationId;
			this.m_subactionAttemptNumber = subactionAttemptNumber;
			this.m_config = config;
		}

		// Token: 0x1700075B RID: 1883
		// (get) Token: 0x06001B57 RID: 6999 RVA: 0x00075AB3 File Offset: 0x00073CB3
		// (set) Token: 0x06001B58 RID: 7000 RVA: 0x00075ABB File Offset: 0x00073CBB
		public bool IsSkipHealthChecks { private get; set; }

		// Token: 0x1700075C RID: 1884
		// (get) Token: 0x06001B59 RID: 7001 RVA: 0x00075AC4 File Offset: 0x00073CC4
		// (set) Token: 0x06001B5A RID: 7002 RVA: 0x00075ACC File Offset: 0x00073CCC
		public bool IsPrepareToStopCalled { private get; set; }

		// Token: 0x1700075D RID: 1885
		// (get) Token: 0x06001B5B RID: 7003 RVA: 0x00075AD5 File Offset: 0x00073CD5
		// (set) Token: 0x06001B5C RID: 7004 RVA: 0x00075ADD File Offset: 0x00073CDD
		public bool IsNewCopierInspectorCreated { private get; set; }

		// Token: 0x1700075E RID: 1886
		// (get) Token: 0x06001B5D RID: 7005 RVA: 0x00075AE6 File Offset: 0x00073CE6
		// (set) Token: 0x06001B5E RID: 7006 RVA: 0x00075AEE File Offset: 0x00073CEE
		public bool IsGranuleUsedAsE00 { private get; set; }

		// Token: 0x1700075F RID: 1887
		// (get) Token: 0x06001B5F RID: 7007 RVA: 0x00075AF7 File Offset: 0x00073CF7
		// (set) Token: 0x06001B60 RID: 7008 RVA: 0x00075AFF File Offset: 0x00073CFF
		public bool IsE00EndOfLogStreamAfterIncReseed { private get; set; }

		// Token: 0x17000760 RID: 1888
		// (get) Token: 0x06001B61 RID: 7009 RVA: 0x00075B08 File Offset: 0x00073D08
		// (set) Token: 0x06001B62 RID: 7010 RVA: 0x00075B10 File Offset: 0x00073D10
		public bool IsAcllFoundDeadConnection { private get; set; }

		// Token: 0x17000761 RID: 1889
		// (get) Token: 0x06001B63 RID: 7011 RVA: 0x00075B19 File Offset: 0x00073D19
		// (set) Token: 0x06001B64 RID: 7012 RVA: 0x00075B21 File Offset: 0x00073D21
		public bool IsAcllCouldNotControlLogCopier { private get; set; }

		// Token: 0x17000762 RID: 1890
		// (get) Token: 0x06001B65 RID: 7013 RVA: 0x00075B2A File Offset: 0x00073D2A
		// (set) Token: 0x06001B66 RID: 7014 RVA: 0x00075B32 File Offset: 0x00073D32
		public bool IsLogCopierInitializedForAcll { private get; set; }

		// Token: 0x17000763 RID: 1891
		// (get) Token: 0x06001B67 RID: 7015 RVA: 0x00075B3B File Offset: 0x00073D3B
		// (set) Token: 0x06001B68 RID: 7016 RVA: 0x00075B43 File Offset: 0x00073D43
		public ReplicaInstanceStage ReplicaInstanceStage { private get; set; }

		// Token: 0x17000764 RID: 1892
		// (get) Token: 0x06001B69 RID: 7017 RVA: 0x00075B4C File Offset: 0x00073D4C
		// (set) Token: 0x06001B6A RID: 7018 RVA: 0x00075B54 File Offset: 0x00073D54
		public CopyStatusEnum CopyStatus { private get; set; }

		// Token: 0x17000765 RID: 1893
		// (get) Token: 0x06001B6B RID: 7019 RVA: 0x00075B5D File Offset: 0x00073D5D
		// (set) Token: 0x06001B6C RID: 7020 RVA: 0x00075B65 File Offset: 0x00073D65
		public long CopyQueueLengthAcllStart { get; set; }

		// Token: 0x17000766 RID: 1894
		// (get) Token: 0x06001B6D RID: 7021 RVA: 0x00075B6E File Offset: 0x00073D6E
		// (set) Token: 0x06001B6E RID: 7022 RVA: 0x00075B76 File Offset: 0x00073D76
		public long ReplayQueueLengthAcllStart { private get; set; }

		// Token: 0x17000767 RID: 1895
		// (get) Token: 0x06001B6F RID: 7023 RVA: 0x00075B7F File Offset: 0x00073D7F
		// (set) Token: 0x06001B70 RID: 7024 RVA: 0x00075B87 File Offset: 0x00073D87
		public long ReplayQueueLengthAcllEnd { private get; set; }

		// Token: 0x17000768 RID: 1896
		// (get) Token: 0x06001B71 RID: 7025 RVA: 0x00075B90 File Offset: 0x00073D90
		// (set) Token: 0x06001B72 RID: 7026 RVA: 0x00075B98 File Offset: 0x00073D98
		public long NumberOfLogsLost { private get; set; }

		// Token: 0x17000769 RID: 1897
		// (get) Token: 0x06001B73 RID: 7027 RVA: 0x00075BA1 File Offset: 0x00073DA1
		// (set) Token: 0x06001B74 RID: 7028 RVA: 0x00075BA9 File Offset: 0x00073DA9
		public long NumberOfLogsCopied { private get; set; }

		// Token: 0x06001B75 RID: 7029 RVA: 0x00075BB4 File Offset: 0x00073DB4
		public override void LogEvent()
		{
			ReplayCrimsonEvents.AcllPerformance.Log<string, string, Guid, int, bool, bool, bool, bool, bool, bool, ReplicaInstanceStage, CopyStatusEnum, long, long, long, long, long, TimeSpan, TimeSpan, TimeSpan, TimeSpan, TimeSpan, TimeSpan, TimeSpan, TimeSpan, TimeSpan, TimeSpan, TimeSpan, TimeSpan, TimeSpan, TimeSpan, bool, TimeSpan, TimeSpan, TimeSpan, TimeSpan, TimeSpan, TimeSpan, TimeSpan, bool, TimeSpan>(this.m_uniqueOperationId, this.m_config.DatabaseName, this.m_config.IdentityGuid, this.m_subactionAttemptNumber, this.IsSkipHealthChecks, this.IsPrepareToStopCalled, this.IsNewCopierInspectorCreated, this.IsGranuleUsedAsE00, this.IsAcllFoundDeadConnection, this.IsAcllCouldNotControlLogCopier, this.ReplicaInstanceStage, this.CopyStatus, this.CopyQueueLengthAcllStart, this.ReplayQueueLengthAcllStart, this.ReplayQueueLengthAcllEnd, this.NumberOfLogsLost, this.NumberOfLogsCopied, base.GetDuration(AcllTimedOperation.AcllQueuedOpStart), base.GetDuration(AcllTimedOperation.AcllQueuedOpExecution), base.GetDuration(AcllTimedOperation.AcquireSuspendLock), base.GetDuration(AcllTimedOperation.EnsureTargetDismounted), base.GetDuration(AcllTimedOperation.FileCheckerAtStart), base.GetDuration(AcllTimedOperation.CleanUpTempIncReseedFiles), base.GetDuration(AcllTimedOperation.IsIncrementalReseedNecessary), base.GetDuration(AcllTimedOperation.AttemptFinalCopy), base.GetDuration(AcllTimedOperation.InspectFinalLogs), base.GetDuration(AcllTimedOperation.InspectE00Log), base.GetDuration(AcllTimedOperation.CheckRequiredLogFilesAtEnd), base.GetDuration(AcllTimedOperation.CopyLogsOverall), base.GetDuration(AcllTimedOperation.AcllEnterLogCopierWorkerLock), base.GetDuration(AcllTimedOperation.RunAcllInit), this.IsLogCopierInitializedForAcll, base.GetDuration(AcllTimedOperation.AcllLogCopierFirstInit), base.GetDuration(AcllTimedOperation.AcllInitWaitForReadCallback), base.GetDuration(AcllTimedOperation.ComputeLossAndMountAllowedOverall), base.GetDuration(AcllTimedOperation.SetLossVariables), base.GetDuration(AcllTimedOperation.ProtectUnboundedDataloss), base.GetDuration(AcllTimedOperation.MarkRedeliveryRequired), base.GetDuration(AcllTimedOperation.RollbackLastLogIfNecessary), this.IsE00EndOfLogStreamAfterIncReseed, base.GetDuration(AcllTimedOperation.SetE00LogGeneration));
		}

		// Token: 0x04000B3D RID: 2877
		private ReplayConfiguration m_config;

		// Token: 0x04000B3E RID: 2878
		private string m_uniqueOperationId;

		// Token: 0x04000B3F RID: 2879
		private int m_subactionAttemptNumber;
	}
}
