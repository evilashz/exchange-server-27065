using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x0200007F RID: 127
	public class CompositeChunked : IChunked
	{
		// Token: 0x0600049D RID: 1181 RVA: 0x0001D0D7 File Offset: 0x0001B2D7
		public CompositeChunked(IList<Func<Context, IChunked>> workQueue)
		{
			this.workQueue = workQueue;
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x0600049E RID: 1182 RVA: 0x0001D0E6 File Offset: 0x0001B2E6
		internal IList<Func<Context, IChunked>> WorkQueueForTest
		{
			get
			{
				return this.workQueue;
			}
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x0001D0F0 File Offset: 0x0001B2F0
		public bool DoChunk(Context context)
		{
			if (this.currentWork == null)
			{
				while (this.currentIndex < this.workQueue.Count)
				{
					this.currentWork = this.workQueue[this.currentIndex](context);
					if (this.currentWork != null)
					{
						break;
					}
					this.currentIndex++;
				}
				if (this.currentWork == null)
				{
					return true;
				}
			}
			if (this.currentWork.DoChunk(context))
			{
				this.currentWork.Dispose(context);
				this.currentWork = null;
				this.currentIndex++;
			}
			return this.currentIndex >= this.workQueue.Count;
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x0001D19C File Offset: 0x0001B39C
		public void Dispose(Context context)
		{
			if (this.currentWork != null)
			{
				this.currentWork.Dispose(context);
			}
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x060004A1 RID: 1185 RVA: 0x0001D1B2 File Offset: 0x0001B3B2
		public bool MustYield
		{
			get
			{
				return this.currentWork != null && this.currentWork.MustYield;
			}
		}

		// Token: 0x040003BA RID: 954
		private readonly IList<Func<Context, IChunked>> workQueue;

		// Token: 0x040003BB RID: 955
		private IChunked currentWork;

		// Token: 0x040003BC RID: 956
		private int currentIndex;
	}
}
