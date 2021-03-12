using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Server.Storage.Common
{
	// Token: 0x02000069 RID: 105
	public class CachePerformanceCounters<T> : ICachePerformanceCounters where T : class
	{
		// Token: 0x17000156 RID: 342
		// (get) Token: 0x060005D6 RID: 1494 RVA: 0x0001015C File Offset: 0x0000E35C
		public ExPerformanceCounter CacheSize
		{
			get
			{
				return this.sizeCounter(this.instanceAccessor());
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x060005D7 RID: 1495 RVA: 0x00010174 File Offset: 0x0000E374
		public ExPerformanceCounter CacheLookups
		{
			get
			{
				return this.lookupsCounter(this.instanceAccessor());
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x060005D8 RID: 1496 RVA: 0x0001018C File Offset: 0x0000E38C
		public ExPerformanceCounter CacheMisses
		{
			get
			{
				return this.missesCounter(this.instanceAccessor());
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x060005D9 RID: 1497 RVA: 0x000101A4 File Offset: 0x0000E3A4
		public ExPerformanceCounter CacheHits
		{
			get
			{
				return this.hitsCounter(this.instanceAccessor());
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x060005DA RID: 1498 RVA: 0x000101BC File Offset: 0x0000E3BC
		public ExPerformanceCounter CacheInserts
		{
			get
			{
				return this.insertsCounter(this.instanceAccessor());
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x060005DB RID: 1499 RVA: 0x000101D4 File Offset: 0x0000E3D4
		public ExPerformanceCounter CacheRemoves
		{
			get
			{
				return this.removesCounter(this.instanceAccessor());
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x060005DC RID: 1500 RVA: 0x000101EC File Offset: 0x0000E3EC
		public ExPerformanceCounter CacheExpirationQueueLength
		{
			get
			{
				return this.expirationQueueLengthCounter(this.instanceAccessor());
			}
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x00010204 File Offset: 0x0000E404
		public CachePerformanceCounters(Func<T> instanceAccessor, Func<T, ExPerformanceCounter> sizeCounter, Func<T, ExPerformanceCounter> lookupsCounter, Func<T, ExPerformanceCounter> missesCounter, Func<T, ExPerformanceCounter> hitsCounter, Func<T, ExPerformanceCounter> insertsCounter, Func<T, ExPerformanceCounter> removesCounter, Func<T, ExPerformanceCounter> expirationQueueLengthCounter)
		{
			this.instanceAccessor = instanceAccessor;
			this.sizeCounter = sizeCounter;
			this.lookupsCounter = lookupsCounter;
			this.missesCounter = missesCounter;
			this.hitsCounter = hitsCounter;
			this.insertsCounter = insertsCounter;
			this.removesCounter = removesCounter;
			this.expirationQueueLengthCounter = expirationQueueLengthCounter;
		}

		// Token: 0x040005F2 RID: 1522
		private Func<T> instanceAccessor;

		// Token: 0x040005F3 RID: 1523
		private Func<T, ExPerformanceCounter> sizeCounter;

		// Token: 0x040005F4 RID: 1524
		private Func<T, ExPerformanceCounter> lookupsCounter;

		// Token: 0x040005F5 RID: 1525
		private Func<T, ExPerformanceCounter> missesCounter;

		// Token: 0x040005F6 RID: 1526
		private Func<T, ExPerformanceCounter> hitsCounter;

		// Token: 0x040005F7 RID: 1527
		private Func<T, ExPerformanceCounter> insertsCounter;

		// Token: 0x040005F8 RID: 1528
		private Func<T, ExPerformanceCounter> removesCounter;

		// Token: 0x040005F9 RID: 1529
		private Func<T, ExPerformanceCounter> expirationQueueLengthCounter;
	}
}
