using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Office.CompliancePolicy.PolicySync;

namespace Microsoft.Exchange.Data.Storage.UnifiedPolicy
{
	// Token: 0x02000E92 RID: 3730
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class SyncManager
	{
		// Token: 0x060081C6 RID: 33222 RVA: 0x00237563 File Offset: 0x00235763
		private SyncManager()
		{
		}

		// Token: 0x1700226E RID: 8814
		// (get) Token: 0x060081C7 RID: 33223 RVA: 0x00237581 File Offset: 0x00235781
		public static SyncManager Instance
		{
			get
			{
				if (!SyncManager.instance.initialized)
				{
					throw new InvalidOperationException("SyncManager.Initialize has not been called.");
				}
				return SyncManager.instance;
			}
		}

		// Token: 0x060081C8 RID: 33224 RVA: 0x002375A0 File Offset: 0x002357A0
		public static void Initialize(SyncAgentContext syncAgentContext)
		{
			if (!SyncManager.instance.initialized)
			{
				lock (SyncManager.instance.initializeLockObject)
				{
					if (!SyncManager.instance.initialized && !SyncManager.instance.shuttingdown)
					{
						PersistentSyncWorkItemQueueProvider persistentQueueProvider = new PersistentSyncWorkItemQueueProvider();
						ParallelJobDispatcher jobDispatcher = new ParallelJobDispatcher(syncAgentContext, syncAgentContext.SyncAgentConfig.MaxSyncWorkItemsPerJob);
						MemoryWorkItemQueueProvider value = new MemoryWorkItemQueueProvider(persistentQueueProvider, jobDispatcher, syncAgentContext);
						SyncManager.instance.queues[NotificationType.Sync] = value;
						ParallelJobDispatcher jobDispatcher2 = new ParallelJobDispatcher(syncAgentContext, syncAgentContext.SyncAgentConfig.MaxPublishWorkItemsPerJob);
						MemoryWorkItemQueueProvider value2 = new MemoryWorkItemQueueProvider(persistentQueueProvider, jobDispatcher2, syncAgentContext);
						SyncManager.instance.queues[NotificationType.ApplicationStatus] = value2;
						SyncManager.instance.initialized = true;
					}
				}
			}
		}

		// Token: 0x060081C9 RID: 33225 RVA: 0x00237674 File Offset: 0x00235874
		public static void Shutdown()
		{
			lock (SyncManager.instance.initializeLockObject)
			{
				SyncManager.instance.shuttingdown = true;
				if (!SyncManager.instance.initialized)
				{
					return;
				}
			}
			SyncManager.instance.InternalShutdown();
		}

		// Token: 0x060081CA RID: 33226 RVA: 0x002376D8 File Offset: 0x002358D8
		public static WorkItemBase EnqueueWorkItem(WorkItemBase workItem)
		{
			NotificationType index;
			if (workItem is SyncWorkItem)
			{
				index = NotificationType.Sync;
			}
			else
			{
				if (!(workItem is SyncStatusUpdateWorkitem))
				{
					throw new NotSupportedException("workitem type not supported");
				}
				index = NotificationType.ApplicationStatus;
			}
			SyncManager.Instance[index].Enqueue(workItem);
			return workItem;
		}

		// Token: 0x1700226F RID: 8815
		internal IWorkItemQueueProvider this[NotificationType index]
		{
			get
			{
				if (!SyncManager.instance.initialized)
				{
					throw new InvalidOperationException("SyncManager.Initialize has not been called.");
				}
				return this.queues[index];
			}
		}

		// Token: 0x060081CC RID: 33228 RVA: 0x00237741 File Offset: 0x00235941
		private void InternalShutdown()
		{
		}

		// Token: 0x0400571E RID: 22302
		private static readonly SyncManager instance = new SyncManager();

		// Token: 0x0400571F RID: 22303
		private readonly object initializeLockObject = new object();

		// Token: 0x04005720 RID: 22304
		private bool initialized;

		// Token: 0x04005721 RID: 22305
		private bool shuttingdown;

		// Token: 0x04005722 RID: 22306
		private Dictionary<NotificationType, IWorkItemQueueProvider> queues = new Dictionary<NotificationType, IWorkItemQueueProvider>();
	}
}
