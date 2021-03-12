using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage.Auditing
{
	// Token: 0x02000F54 RID: 3924
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class QueueFactory
	{
		// Token: 0x06008695 RID: 34453 RVA: 0x0024EAC8 File Offset: 0x0024CCC8
		public static IQueue<T> GetQueue<T>(Queues queue)
		{
			EnumValidator.ThrowIfInvalid<Queues>(queue);
			if (QueueFactory.queueList == null)
			{
				lock (QueueFactory.syncRoot)
				{
					if (QueueFactory.queueList == null)
					{
						QueueFactory.queueList = new IDisposable[Enum.GetValues(typeof(Queues)).Length];
					}
				}
			}
			if (QueueFactory.queueList[(int)queue] == null)
			{
				lock (QueueFactory.syncRoot)
				{
					if (QueueFactory.queueList[(int)queue] == null)
					{
						QueueFactory.queueList[(int)queue] = new MemoryQueue<T>();
					}
				}
			}
			return (IQueue<T>)QueueFactory.queueList[(int)queue];
		}

		// Token: 0x06008696 RID: 34454 RVA: 0x0024EB88 File Offset: 0x0024CD88
		internal static void Reset()
		{
			lock (QueueFactory.syncRoot)
			{
				if (QueueFactory.queueList != null)
				{
					foreach (IDisposable disposable in QueueFactory.queueList)
					{
						if (disposable != null)
						{
							disposable.Dispose();
						}
					}
					QueueFactory.queueList = null;
				}
			}
		}

		// Token: 0x04005A0A RID: 23050
		private static IDisposable[] queueList;

		// Token: 0x04005A0B RID: 23051
		private static object syncRoot = new object();
	}
}
