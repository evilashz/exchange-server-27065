using System;
using System.Collections;

namespace Microsoft.Exchange.Transport.Agent.ProtocolAnalysis.DbAccess
{
	// Token: 0x02000013 RID: 19
	internal static class WorkQueue
	{
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00002FA6 File Offset: 0x000011A6
		public static int Count
		{
			get
			{
				return WorkQueue.workQueue.Count;
			}
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002FB2 File Offset: 0x000011B2
		public static void EnqueueWorkItemData(WorkItemData item)
		{
			WorkQueue.workQueue.Enqueue(item);
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002FBF File Offset: 0x000011BF
		public static WorkItemData DequeueWorkItemData()
		{
			if (WorkQueue.workQueue.Count == 0)
			{
				return null;
			}
			return (WorkItemData)WorkQueue.workQueue.Dequeue();
		}

		// Token: 0x04000025 RID: 37
		private static Queue workQueue = Queue.Synchronized(new Queue());
	}
}
