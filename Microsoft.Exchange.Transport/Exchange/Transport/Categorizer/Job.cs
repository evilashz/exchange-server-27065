using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Internal.MExRuntime;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.Transport.MessageDepot;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x020001B9 RID: 441
	internal abstract class Job
	{
		// Token: 0x0600144B RID: 5195 RVA: 0x00051DD4 File Offset: 0x0004FFD4
		protected Job(long id, ThrottlingContext context, QueuedRecipientsByAgeToken token, IList<StageInfo> stages)
		{
			this.Id = id;
			this.context = context;
			this.token = token;
			this.stages = stages;
			ExTraceGlobals.SchedulerTracer.TraceDebug<Job>((long)this.GetHashCode(), "Job({0}) started", this);
		}

		// Token: 0x1700057E RID: 1406
		// (get) Token: 0x0600144C RID: 5196 RVA: 0x00051E10 File Offset: 0x00050010
		public IList<StageInfo> Stages
		{
			get
			{
				return this.stages;
			}
		}

		// Token: 0x1700057F RID: 1407
		// (get) Token: 0x0600144D RID: 5197 RVA: 0x00051E18 File Offset: 0x00050018
		// (set) Token: 0x0600144E RID: 5198 RVA: 0x00051E20 File Offset: 0x00050020
		public bool RootMailItemDeferred
		{
			get
			{
				return this.rootMailItemDeferred;
			}
			set
			{
				this.rootMailItemDeferred = value;
			}
		}

		// Token: 0x0600144F RID: 5199 RVA: 0x00051E29 File Offset: 0x00050029
		public static void ReleaseItem(TransportMailItem mailItem)
		{
			mailItem.ReleaseFromActiveMaterializedLazy();
		}

		// Token: 0x06001450 RID: 5200 RVA: 0x00051E34 File Offset: 0x00050034
		public void RunningTaskCompleted(TaskContext completedTask, bool async)
		{
			TaskContext taskContext = (TaskContext)Interlocked.CompareExchange(ref this.executingTask, null, completedTask);
			if (taskContext != completedTask)
			{
				throw new InvalidOperationException("The task that completed was not running previously");
			}
			if (this.pendingTasksListHead == null)
			{
				this.CompletedInternal(taskContext.SubjectMailItem);
			}
			else if (async)
			{
				this.PendingInternal();
			}
			if (async)
			{
				this.GoneAsyncInternal();
			}
		}

		// Token: 0x06001451 RID: 5201 RVA: 0x00051E8B File Offset: 0x0005008B
		public void EnqueuePendingTask(int stage, TransportMailItem mailItem, AcceptedDomainCollection acceptedDomains)
		{
			this.EnqueuePendingTask(stage, mailItem, 0, null, null, null, acceptedDomains);
		}

		// Token: 0x06001452 RID: 5202 RVA: 0x00051E9C File Offset: 0x0005009C
		public bool ExecutePendingTasks()
		{
			bool result = false;
			while (!this.IsRetired)
			{
				TaskContext nextPendingTask = this.GetNextPendingTask();
				if (nextPendingTask != null)
				{
					TaskCompletion taskCompletion = nextPendingTask.Invoke();
					result = true;
					if (taskCompletion == TaskCompletion.Completed)
					{
						continue;
					}
				}
				return result;
			}
			this.Retire();
			return result;
		}

		// Token: 0x17000580 RID: 1408
		// (get) Token: 0x06001453 RID: 5203 RVA: 0x00051ED2 File Offset: 0x000500D2
		public bool IsEmpty
		{
			get
			{
				return this.pendingTasksListHead == null;
			}
		}

		// Token: 0x06001454 RID: 5204 RVA: 0x00051EE0 File Offset: 0x000500E0
		public override string ToString()
		{
			return this.Id.ToString();
		}

		// Token: 0x06001455 RID: 5205
		public abstract bool TryGetDeferToken(TransportMailItem mailItem, out AcquireToken deferToken);

		// Token: 0x06001456 RID: 5206
		public abstract void MarkDeferred(TransportMailItem mailItem);

		// Token: 0x06001457 RID: 5207 RVA: 0x00051EFC File Offset: 0x000500FC
		public void Retire()
		{
			ExTraceGlobals.SchedulerTracer.TraceDebug<Job>((long)this.GetHashCode(), "Abandon Job ({0}) as the scheduler is retired", this);
			TransportMailItem transportMailItem = null;
			lock (this)
			{
				for (TaskContext friendNextTaskContext = this.pendingTasksListHead; friendNextTaskContext != null; friendNextTaskContext = friendNextTaskContext.FriendNextTaskContext)
				{
					friendNextTaskContext.TaskRetired();
					if (transportMailItem == null)
					{
						transportMailItem = friendNextTaskContext.SubjectMailItem;
					}
				}
			}
			this.RetireInternal(transportMailItem);
		}

		// Token: 0x17000581 RID: 1409
		// (get) Token: 0x06001458 RID: 5208
		protected abstract bool IsRetired { get; }

		// Token: 0x06001459 RID: 5209
		protected abstract void CompletedInternal(TransportMailItem mailItem);

		// Token: 0x0600145A RID: 5210
		protected abstract void PendingInternal();

		// Token: 0x0600145B RID: 5211
		protected abstract void GoneAsyncInternal();

		// Token: 0x0600145C RID: 5212
		protected abstract void RetireInternal(TransportMailItem mailItem);

		// Token: 0x0600145D RID: 5213 RVA: 0x00051F74 File Offset: 0x00050174
		internal void EnqueuePendingTask(int stage, TransportMailItem mailItem, int latestMimeVersion, WeakReference lastKnownMimeDocument, IMExSession mexSession, AgentLatencyTracker agentLatencyTracker, AcceptedDomainCollection acceptedDomains)
		{
			TaskContext taskContext = new TaskContext(stage, mailItem, latestMimeVersion, lastKnownMimeDocument, this, mexSession, agentLatencyTracker, acceptedDomains);
			lock (this)
			{
				if (this.pendingTasksListHead == null || this.pendingTasksListHead.Stage <= stage)
				{
					taskContext.FriendNextTaskContext = this.pendingTasksListHead;
					this.pendingTasksListHead = taskContext;
				}
				else
				{
					TaskContext friendNextTaskContext = this.pendingTasksListHead;
					while (friendNextTaskContext.FriendNextTaskContext != null && friendNextTaskContext.FriendNextTaskContext.Stage > stage)
					{
						friendNextTaskContext = friendNextTaskContext.FriendNextTaskContext;
					}
					taskContext.FriendNextTaskContext = friendNextTaskContext.FriendNextTaskContext;
					friendNextTaskContext.FriendNextTaskContext = taskContext;
				}
			}
		}

		// Token: 0x0600145E RID: 5214 RVA: 0x00052024 File Offset: 0x00050224
		internal CategorizerItem GetCategorizerItemById(long mailItemId)
		{
			lock (this)
			{
				TaskContext taskContext;
				for (taskContext = this.pendingTasksListHead; taskContext != null; taskContext = taskContext.FriendNextTaskContext)
				{
					if (taskContext.SubjectMailItem.RecordId == mailItemId)
					{
						return new CategorizerItem(taskContext.SubjectMailItem, taskContext.Stage);
					}
				}
				taskContext = (TaskContext)this.executingTask;
				if (taskContext != null && taskContext.SubjectMailItem.RecordId == mailItemId)
				{
					return new CategorizerItem(taskContext.SubjectMailItem, taskContext.Stage);
				}
			}
			return null;
		}

		// Token: 0x0600145F RID: 5215 RVA: 0x000520C4 File Offset: 0x000502C4
		internal void VisitCategorizerItems(Func<CategorizerItem, bool> visitor)
		{
			lock (this)
			{
				TaskContext taskContext;
				for (taskContext = this.pendingTasksListHead; taskContext != null; taskContext = taskContext.FriendNextTaskContext)
				{
					visitor(new CategorizerItem(taskContext.SubjectMailItem, taskContext.Stage));
				}
				taskContext = (TaskContext)this.executingTask;
				if (taskContext != null)
				{
					visitor(new CategorizerItem(taskContext.SubjectMailItem, taskContext.Stage));
				}
			}
		}

		// Token: 0x06001460 RID: 5216 RVA: 0x0005214C File Offset: 0x0005034C
		internal int GetMailItemCount()
		{
			int num = 0;
			lock (this)
			{
				for (TaskContext friendNextTaskContext = this.pendingTasksListHead; friendNextTaskContext != null; friendNextTaskContext = friendNextTaskContext.FriendNextTaskContext)
				{
					num++;
				}
				if (this.executingTask != null && num == 0)
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x06001461 RID: 5217 RVA: 0x000521AC File Offset: 0x000503AC
		internal ThrottlingContext GetThrottlingContext()
		{
			return this.context;
		}

		// Token: 0x06001462 RID: 5218 RVA: 0x000521B4 File Offset: 0x000503B4
		internal QueuedRecipientsByAgeToken GetQueuedRecipientsByAgeToken()
		{
			return this.token;
		}

		// Token: 0x06001463 RID: 5219 RVA: 0x000521BC File Offset: 0x000503BC
		private TaskContext GetNextPendingTask()
		{
			TaskContext taskContext = this.pendingTasksListHead;
			if (taskContext != null)
			{
				lock (this)
				{
					this.pendingTasksListHead = taskContext.FriendNextTaskContext;
					taskContext.FriendNextTaskContext = null;
					TaskContext taskContext2 = (TaskContext)Interlocked.CompareExchange(ref this.executingTask, taskContext, null);
					if (taskContext2 != null)
					{
						throw new InvalidOperationException("don't allow concurrent tasks");
					}
				}
			}
			return taskContext;
		}

		// Token: 0x04000A4A RID: 2634
		public readonly long Id;

		// Token: 0x04000A4B RID: 2635
		protected static long nextJobId = 1000L;

		// Token: 0x04000A4C RID: 2636
		private TaskContext pendingTasksListHead;

		// Token: 0x04000A4D RID: 2637
		private object executingTask;

		// Token: 0x04000A4E RID: 2638
		private ThrottlingContext context;

		// Token: 0x04000A4F RID: 2639
		private QueuedRecipientsByAgeToken token;

		// Token: 0x04000A50 RID: 2640
		private readonly IList<StageInfo> stages;

		// Token: 0x04000A51 RID: 2641
		private bool rootMailItemDeferred;
	}
}
