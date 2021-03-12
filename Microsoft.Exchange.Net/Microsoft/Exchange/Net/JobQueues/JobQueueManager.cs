using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Net.JobQueues
{
	// Token: 0x02000740 RID: 1856
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class JobQueueManager
	{
		// Token: 0x06002408 RID: 9224 RVA: 0x0004A7ED File Offset: 0x000489ED
		private JobQueueManager()
		{
		}

		// Token: 0x06002409 RID: 9225 RVA: 0x0004A80C File Offset: 0x00048A0C
		public static void Initialize(IEnumerable<JobQueue> queues)
		{
			if (!JobQueueManager.instance.initialized)
			{
				lock (JobQueueManager.instance.initializeLockObject)
				{
					if (!JobQueueManager.instance.initialized && !JobQueueManager.instance.shuttingdown)
					{
						JobQueueManager.instance.InternalInitialize(queues);
						JobQueueManager.instance.initialized = true;
					}
				}
			}
		}

		// Token: 0x0600240A RID: 9226 RVA: 0x0004A884 File Offset: 0x00048A84
		public static void Shutdown()
		{
			lock (JobQueueManager.instance.initializeLockObject)
			{
				JobQueueManager.instance.shuttingdown = true;
				if (!JobQueueManager.instance.initialized)
				{
					return;
				}
			}
			JobQueueManager.instance.InternalShutdown();
		}

		// Token: 0x0600240B RID: 9227 RVA: 0x0004A8E8 File Offset: 0x00048AE8
		public static EnqueueResult Enqueue(QueueType type, byte[] data)
		{
			if (!JobQueueManager.instance.initialized)
			{
				return new EnqueueResult(EnqueueResultType.QueueServerNotInitialized);
			}
			if (JobQueueManager.instance.shuttingdown)
			{
				return new EnqueueResult(EnqueueResultType.QueueServerShutDown);
			}
			if (data == null || data.Length == 0)
			{
				return new EnqueueResult(EnqueueResultType.InvalidData);
			}
			JobQueue jobQueue = null;
			if (!JobQueueManager.instance.queues.TryGetValue(type, out jobQueue))
			{
				return new EnqueueResult(EnqueueResultType.QueueServerNotInitialized);
			}
			return jobQueue.Enqueue(data);
		}

		// Token: 0x0600240C RID: 9228 RVA: 0x0004A950 File Offset: 0x00048B50
		private void InternalInitialize(IEnumerable<JobQueue> queues)
		{
			this.queues.Clear();
			foreach (JobQueue jobQueue in queues)
			{
				this.queues[jobQueue.Type] = jobQueue;
			}
		}

		// Token: 0x0600240D RID: 9229 RVA: 0x0004A9B0 File Offset: 0x00048BB0
		private void InternalShutdown()
		{
			ManualResetEvent[] array = new ManualResetEvent[this.queues.Count];
			int num = 0;
			foreach (JobQueue jobQueue in this.queues.Values)
			{
				array[num] = jobQueue.ShutdownEvent;
				num++;
				jobQueue.SignalShutdown();
			}
			WaitHandle.WaitAll(array, TimeSpan.FromSeconds(15.0));
			foreach (JobQueue jobQueue2 in this.queues.Values)
			{
				jobQueue2.Cleanup();
			}
		}

		// Token: 0x040021E7 RID: 8679
		private static readonly JobQueueManager instance = new JobQueueManager();

		// Token: 0x040021E8 RID: 8680
		private readonly object initializeLockObject = new object();

		// Token: 0x040021E9 RID: 8681
		private bool initialized;

		// Token: 0x040021EA RID: 8682
		private bool shuttingdown;

		// Token: 0x040021EB RID: 8683
		private Dictionary<QueueType, JobQueue> queues = new Dictionary<QueueType, JobQueue>();
	}
}
