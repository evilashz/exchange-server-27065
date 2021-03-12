using System;
using Microsoft.Exchange.Cluster.ActiveManagerServer;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Rpc.ActiveManager;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000311 RID: 785
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ReplicaInstanceAttemptCopyLastLogs : ReplicaInstanceAmQueuedItem
	{
		// Token: 0x0600205B RID: 8283 RVA: 0x00096A3D File Offset: 0x00094C3D
		public ReplicaInstanceAttemptCopyLastLogs(AmAcllArgs acllArgs, AcllPerformanceTracker acllPerf, ReplicaInstance instance) : base(instance)
		{
			this.AcllArgs = acllArgs;
			this.AcllPerfTracker = acllPerf;
		}

		// Token: 0x1700087F RID: 2175
		// (get) Token: 0x0600205C RID: 8284 RVA: 0x00096A54 File Offset: 0x00094C54
		// (set) Token: 0x0600205D RID: 8285 RVA: 0x00096A5C File Offset: 0x00094C5C
		internal AmAcllArgs AcllArgs { get; private set; }

		// Token: 0x17000880 RID: 2176
		// (get) Token: 0x0600205E RID: 8286 RVA: 0x00096A65 File Offset: 0x00094C65
		// (set) Token: 0x0600205F RID: 8287 RVA: 0x00096A6D File Offset: 0x00094C6D
		internal AcllPerformanceTracker AcllPerfTracker { get; private set; }

		// Token: 0x17000881 RID: 2177
		// (get) Token: 0x06002060 RID: 8288 RVA: 0x00096A76 File Offset: 0x00094C76
		// (set) Token: 0x06002061 RID: 8289 RVA: 0x00096A7E File Offset: 0x00094C7E
		internal AmAcllReturnStatus AcllStatus { get; private set; }

		// Token: 0x06002062 RID: 8290 RVA: 0x00096A87 File Offset: 0x00094C87
		protected override Exception GetReplicaInstanceNotFoundException()
		{
			return new AmDbAcllErrorNoReplicaInstance(base.DbName, Environment.MachineName);
		}

		// Token: 0x06002063 RID: 8291 RVA: 0x00096A99 File Offset: 0x00094C99
		protected override void CheckOperationApplicable()
		{
			if (base.ReplicaInstance.CurrentContext.IsFailoverPending())
			{
				throw new AcllAlreadyRunningException(base.DbCopyName);
			}
		}

		// Token: 0x06002064 RID: 8292 RVA: 0x00096ABC File Offset: 0x00094CBC
		protected override void ExecuteInternal()
		{
			base.ExecuteInternal();
			AmAcllReturnStatus acllStatus = base.ReplicaInstance.AttemptCopyLastLogsRcr(this.AcllArgs, this.AcllPerfTracker);
			this.AcllStatus = acllStatus;
		}
	}
}
