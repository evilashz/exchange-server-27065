using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x020000A6 RID: 166
	internal sealed class LimitedThreadPool
	{
		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060003A4 RID: 932 RVA: 0x0000EF08 File Offset: 0x0000D108
		public static int MaximumThreads
		{
			get
			{
				int result;
				int num;
				ThreadPool.GetAvailableThreads(out result, out num);
				return result;
			}
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x0000EF1F File Offset: 0x0000D11F
		public LimitedThreadPool(int maximumThreads, WaitCallback callback)
		{
			this.maximumThreads = maximumThreads;
			this.callback = callback;
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x0000EF4B File Offset: 0x0000D14B
		public void Add(object state)
		{
			this.queue.Enqueue(state);
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x0000EF5C File Offset: 0x0000D15C
		public void Start()
		{
			lock (this.locker)
			{
				this.Dispatch();
			}
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x0000EF9C File Offset: 0x0000D19C
		public void Cancel()
		{
			lock (this.locker)
			{
				this.cancelled = true;
				this.queue.Clear();
			}
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x0000EFE8 File Offset: 0x0000D1E8
		private void Dispatch()
		{
			while (this.threadsInUse < this.maximumThreads)
			{
				if (this.queue.Count == 0)
				{
					return;
				}
				object state = this.queue.Dequeue();
				using (ActivityContext.SuppressThreadScope())
				{
					ThreadPool.QueueUserWorkItem(new WaitCallback(this.Worker), state);
				}
				this.threadsInUse++;
			}
		}

		// Token: 0x060003AA RID: 938 RVA: 0x0000F064 File Offset: 0x0000D264
		private void Worker(object state)
		{
			if (!this.cancelled)
			{
				try
				{
					this.callback(state);
				}
				finally
				{
					if (!this.cancelled)
					{
						lock (this.locker)
						{
							this.threadsInUse--;
							this.Dispatch();
						}
					}
				}
			}
		}

		// Token: 0x04000226 RID: 550
		private WaitCallback callback;

		// Token: 0x04000227 RID: 551
		private Queue<object> queue = new Queue<object>();

		// Token: 0x04000228 RID: 552
		private int threadsInUse;

		// Token: 0x04000229 RID: 553
		private int maximumThreads;

		// Token: 0x0400022A RID: 554
		private bool cancelled;

		// Token: 0x0400022B RID: 555
		private object locker = new object();
	}
}
