using System;

namespace System.Threading.Tasks
{
	// Token: 0x02000535 RID: 1333
	internal class ParallelForReplicaTask : Task
	{
		// Token: 0x06004033 RID: 16435 RVA: 0x000EF460 File Offset: 0x000ED660
		internal ParallelForReplicaTask(Action<object> taskReplicaDelegate, object stateObject, Task parentTask, TaskScheduler taskScheduler, TaskCreationOptions creationOptionsForReplica, InternalTaskOptions internalOptionsForReplica) : base(taskReplicaDelegate, stateObject, parentTask, default(CancellationToken), creationOptionsForReplica, internalOptionsForReplica, taskScheduler)
		{
		}

		// Token: 0x170009A7 RID: 2471
		// (get) Token: 0x06004034 RID: 16436 RVA: 0x000EF485 File Offset: 0x000ED685
		// (set) Token: 0x06004035 RID: 16437 RVA: 0x000EF48D File Offset: 0x000ED68D
		internal override object SavedStateForNextReplica
		{
			get
			{
				return this.m_stateForNextReplica;
			}
			set
			{
				this.m_stateForNextReplica = value;
			}
		}

		// Token: 0x170009A8 RID: 2472
		// (get) Token: 0x06004036 RID: 16438 RVA: 0x000EF496 File Offset: 0x000ED696
		// (set) Token: 0x06004037 RID: 16439 RVA: 0x000EF49E File Offset: 0x000ED69E
		internal override object SavedStateFromPreviousReplica
		{
			get
			{
				return this.m_stateFromPreviousReplica;
			}
			set
			{
				this.m_stateFromPreviousReplica = value;
			}
		}

		// Token: 0x170009A9 RID: 2473
		// (get) Token: 0x06004038 RID: 16440 RVA: 0x000EF4A7 File Offset: 0x000ED6A7
		// (set) Token: 0x06004039 RID: 16441 RVA: 0x000EF4AF File Offset: 0x000ED6AF
		internal override Task HandedOverChildReplica
		{
			get
			{
				return this.m_handedOverChildReplica;
			}
			set
			{
				this.m_handedOverChildReplica = value;
			}
		}

		// Token: 0x04001A68 RID: 6760
		internal object m_stateForNextReplica;

		// Token: 0x04001A69 RID: 6761
		internal object m_stateFromPreviousReplica;

		// Token: 0x04001A6A RID: 6762
		internal Task m_handedOverChildReplica;
	}
}
