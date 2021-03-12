using System;
using System.Runtime.CompilerServices;

namespace System.Threading.Tasks
{
	// Token: 0x02000534 RID: 1332
	internal class ParallelForReplicatingTask : Task
	{
		// Token: 0x06004030 RID: 16432 RVA: 0x000EF3E4 File Offset: 0x000ED5E4
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal ParallelForReplicatingTask(ParallelOptions parallelOptions, Action action, TaskCreationOptions creationOptions, InternalTaskOptions internalOptions) : base(action, null, Task.InternalCurrent, default(CancellationToken), creationOptions, internalOptions | InternalTaskOptions.SelfReplicating, null)
		{
			this.m_replicationDownCount = parallelOptions.EffectiveMaxConcurrencyLevel;
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			base.PossiblyCaptureContext(ref stackCrawlMark);
		}

		// Token: 0x06004031 RID: 16433 RVA: 0x000EF427 File Offset: 0x000ED627
		internal override bool ShouldReplicate()
		{
			if (this.m_replicationDownCount == -1)
			{
				return true;
			}
			if (this.m_replicationDownCount > 0)
			{
				this.m_replicationDownCount--;
				return true;
			}
			return false;
		}

		// Token: 0x06004032 RID: 16434 RVA: 0x000EF44E File Offset: 0x000ED64E
		internal override Task CreateReplicaTask(Action<object> taskReplicaDelegate, object stateObject, Task parentTask, TaskScheduler taskScheduler, TaskCreationOptions creationOptionsForReplica, InternalTaskOptions internalOptionsForReplica)
		{
			return new ParallelForReplicaTask(taskReplicaDelegate, stateObject, parentTask, taskScheduler, creationOptionsForReplica, internalOptionsForReplica);
		}

		// Token: 0x04001A67 RID: 6759
		private int m_replicationDownCount;
	}
}
