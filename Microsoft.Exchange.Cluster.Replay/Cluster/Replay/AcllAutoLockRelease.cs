using System;
using System.Threading;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020002BB RID: 699
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class AcllAutoLockRelease : TimerComponent
	{
		// Token: 0x06001B3A RID: 6970 RVA: 0x00075602 File Offset: 0x00073802
		public AcllAutoLockRelease(ReplicaInstance instance) : base(TimeSpan.FromMilliseconds((double)RegistryParameters.AcllLockAutoReleaseAfterDurationMs), TimeSpan.FromMilliseconds((double)RegistryParameters.AcllLockAutoReleaseAfterDurationMs), "AcllAutoLockRelease")
		{
			this.ReplicaInstance = instance;
		}

		// Token: 0x17000754 RID: 1876
		// (get) Token: 0x06001B3B RID: 6971 RVA: 0x0007562C File Offset: 0x0007382C
		// (set) Token: 0x06001B3C RID: 6972 RVA: 0x00075634 File Offset: 0x00073834
		private ReplicaInstance ReplicaInstance { get; set; }

		// Token: 0x06001B3D RID: 6973 RVA: 0x0007563D File Offset: 0x0007383D
		protected override void StopInternal()
		{
			base.StopInternal();
			this.ReplicaInstance = null;
		}

		// Token: 0x06001B3E RID: 6974 RVA: 0x00075654 File Offset: 0x00073854
		protected override void TimerCallbackInternal()
		{
			bool flag = true;
			if (this.ReplicaInstance.CurrentContext.InAttemptCopyLastLogs)
			{
				ExTraceGlobals.ReplicaInstanceTracer.TraceDebug<string, string>((long)this.ReplicaInstance.GetHashCode(), "AcllAutoLockRelease: Skipping ACLL lock auto-release for {0}({1}) since ACLL is still in progress.", this.ReplicaInstance.Configuration.DisplayName, this.ReplicaInstance.Configuration.Identity);
				flag = false;
			}
			else if (this.ReplicaInstance.CurrentContext.IsFailoverPending())
			{
				ExTraceGlobals.ReplicaInstanceTracer.TraceDebug<string, string, TimeSpan>((long)this.ReplicaInstance.GetHashCode(), "AcllAutoLockRelease: Auto-releasing ACLL lock for {0}({1}) after {2}ms", this.ReplicaInstance.Configuration.DisplayName, this.ReplicaInstance.Configuration.Identity, base.Period);
				this.ReplicaInstance.CurrentContext.BestEffortDismountReplayDatabase();
				this.ReplicaInstance.Configuration.ReplayState.SuspendLock.LeaveAttemptCopyLastLogs();
				this.ReplicaInstance.CurrentContext.ClearAttemptCopyLastLogsEndTime();
			}
			else
			{
				ExTraceGlobals.ReplicaInstanceTracer.TraceDebug<string, string>((long)this.ReplicaInstance.GetHashCode(), "AcllAutoLockRelease: Skipping ACLL lock auto-release for {0}({1}) since a move/failover is no longer pending.", this.ReplicaInstance.Configuration.DisplayName, this.ReplicaInstance.Configuration.Identity);
			}
			if (flag)
			{
				ThreadPool.QueueUserWorkItem(delegate(object param0)
				{
					base.Stop();
				});
			}
		}
	}
}
