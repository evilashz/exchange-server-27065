using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Microsoft.Exchange.Clients.Owa2.Server.Diagnostics;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020001B6 RID: 438
	internal class SubscriberConcurrencyTracker
	{
		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x06000F87 RID: 3975 RVA: 0x0003C77C File Offset: 0x0003A97C
		public static SubscriberConcurrencyTracker Instance
		{
			get
			{
				return SubscriberConcurrencyTracker.instance;
			}
		}

		// Token: 0x06000F88 RID: 3976 RVA: 0x0003C783 File Offset: 0x0003A983
		private SubscriberConcurrencyTracker()
		{
			this.groupToConcurrentStatsMap = new Dictionary<Tuple<string, NotificationType>, Tuple<long, int>>();
		}

		// Token: 0x06000F89 RID: 3977 RVA: 0x0003C7B8 File Offset: 0x0003A9B8
		public void OnSubscribe(string mailbox, NotificationType notificationType)
		{
			Tuple<string, NotificationType> key = new Tuple<string, NotificationType>(mailbox, notificationType);
			long elapsedMilliseconds = this.groupConcurrencyStopWatch.ElapsedMilliseconds;
			long elapsedTime;
			int num;
			int num2;
			lock (this.groupConcurrencyStatsSyncObject)
			{
				Tuple<long, int> tuple;
				if (!this.groupToConcurrentStatsMap.TryGetValue(key, out tuple))
				{
					elapsedTime = 0L;
					num = 0;
				}
				else
				{
					elapsedTime = elapsedMilliseconds - tuple.Item1;
					num = tuple.Item2;
				}
				num2 = num + 1;
				this.groupToConcurrentStatsMap[key] = new Tuple<long, int>(elapsedMilliseconds, num2);
			}
			this.logEventQueue.Enqueue(new GroupConcurrencyLogEvent(mailbox, notificationType, elapsedTime, num, num2));
			this.StartGroupConcurrencyLogThread();
		}

		// Token: 0x06000F8A RID: 3978 RVA: 0x0003C868 File Offset: 0x0003AA68
		public void OnUnsubscribe(string mailbox, NotificationType notificationType)
		{
			this.OnUnsubscribe(mailbox, notificationType, 1);
		}

		// Token: 0x06000F8B RID: 3979 RVA: 0x0003C874 File Offset: 0x0003AA74
		public void OnUnsubscribe(string mailbox, NotificationType notificationType, int count)
		{
			Tuple<string, NotificationType> key = new Tuple<string, NotificationType>(mailbox, notificationType);
			long elapsedMilliseconds = this.groupConcurrencyStopWatch.ElapsedMilliseconds;
			long elapsedTime;
			int item;
			int num;
			lock (this.groupConcurrencyStatsSyncObject)
			{
				Tuple<long, int> tuple;
				if (!this.groupToConcurrentStatsMap.TryGetValue(key, out tuple))
				{
					return;
				}
				elapsedTime = elapsedMilliseconds - tuple.Item1;
				item = tuple.Item2;
				num = item - count;
				if (num == 0)
				{
					this.groupToConcurrentStatsMap.Remove(key);
				}
				else
				{
					this.groupToConcurrentStatsMap[key] = new Tuple<long, int>(elapsedMilliseconds, num);
				}
			}
			this.logEventQueue.Enqueue(new GroupConcurrencyLogEvent(mailbox, notificationType, elapsedTime, item, num));
			this.StartGroupConcurrencyLogThread();
		}

		// Token: 0x06000F8C RID: 3980 RVA: 0x0003C934 File Offset: 0x0003AB34
		internal void OnResubscribe(string mailbox, NotificationType notificationType)
		{
			Tuple<string, NotificationType> key = new Tuple<string, NotificationType>(mailbox, notificationType);
			long elapsedMilliseconds = this.groupConcurrencyStopWatch.ElapsedMilliseconds;
			Tuple<long, int> tuple;
			lock (this.groupConcurrencyStatsSyncObject)
			{
				if (!this.groupToConcurrentStatsMap.TryGetValue(key, out tuple))
				{
					return;
				}
			}
			this.logEventQueue.Enqueue(new GroupConcurrencyLogEvent(mailbox, notificationType, elapsedMilliseconds - tuple.Item1, tuple.Item2, tuple.Item2));
			this.StartGroupConcurrencyLogThread();
		}

		// Token: 0x06000F8D RID: 3981 RVA: 0x0003CA10 File Offset: 0x0003AC10
		private void StartGroupConcurrencyLogThread()
		{
			if (this.loggerThreadAlive != 0)
			{
				return;
			}
			ThreadPool.QueueUserWorkItem(delegate(object param0)
			{
				while (Interlocked.CompareExchange(ref this.loggerThreadAlive, 1, 0) == 0)
				{
					ILogEvent logEvent;
					while (this.logEventQueue.TryDequeue(out logEvent))
					{
						OwaServerLogger.AppendToLog(logEvent);
					}
					Interlocked.Exchange(ref this.loggerThreadAlive, 0);
					if (this.logEventQueue.Count == 0)
					{
						return;
					}
				}
			});
		}

		// Token: 0x04000953 RID: 2387
		private static readonly SubscriberConcurrencyTracker instance = new SubscriberConcurrencyTracker();

		// Token: 0x04000954 RID: 2388
		private readonly object groupConcurrencyStatsSyncObject = new object();

		// Token: 0x04000955 RID: 2389
		private readonly Dictionary<Tuple<string, NotificationType>, Tuple<long, int>> groupToConcurrentStatsMap;

		// Token: 0x04000956 RID: 2390
		private readonly Stopwatch groupConcurrencyStopWatch = Stopwatch.StartNew();

		// Token: 0x04000957 RID: 2391
		private readonly ConcurrentQueue<ILogEvent> logEventQueue = new ConcurrentQueue<ILogEvent>();

		// Token: 0x04000958 RID: 2392
		private int loggerThreadAlive;
	}
}
