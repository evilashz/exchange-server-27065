using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.Common.ExtensionMethods;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x02000120 RID: 288
	public abstract class TraceContainer<TAggregator, TKey, TParameters> where TAggregator : TraceDataAggregator<TParameters> where TParameters : ITraceParameters
	{
		// Token: 0x06000B4F RID: 2895 RVA: 0x0003926A File Offset: 0x0003746A
		public TraceContainer()
		{
			this.data = new ConcurrentDictionary<TKey, TAggregator>(20, 10000);
			this.startTimeStamp = new Stopwatch();
			this.startTimeStamp.Start();
		}

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x06000B50 RID: 2896 RVA: 0x0003929A File Offset: 0x0003749A
		internal bool HasDataToLog
		{
			get
			{
				return this.hasData && this.startTimeStamp.ToTimeSpan() > this.TraceInterval;
			}
		}

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x06000B51 RID: 2897 RVA: 0x000392BC File Offset: 0x000374BC
		internal virtual TimeSpan TraceInterval
		{
			get
			{
				return TimeSpan.FromMinutes(5.0);
			}
		}

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x06000B52 RID: 2898 RVA: 0x000392CC File Offset: 0x000374CC
		internal IEnumerable<KeyValuePair<TKey, TAggregator>> Data
		{
			get
			{
				return this.data;
			}
		}

		// Token: 0x06000B53 RID: 2899 RVA: 0x000392D4 File Offset: 0x000374D4
		internal void Set(TKey key, TParameters parameters)
		{
			if (parameters.HasDataToLog)
			{
				TAggregator orAdd;
				if (!this.data.TryGetValue(key, out orAdd))
				{
					orAdd = this.data.GetOrAdd(key, this.CreateEmptyAggregator());
				}
				this.UpdateAggregator(orAdd, parameters);
				this.hasData = true;
			}
		}

		// Token: 0x06000B54 RID: 2900 RVA: 0x00039323 File Offset: 0x00037523
		internal void Commit(StoreDatabase database, IBinaryLogger logger)
		{
			if (this.hasData && Interlocked.Exchange(ref this.governor, 1) == 0)
			{
				this.WriteTrace(database, logger);
			}
		}

		// Token: 0x06000B55 RID: 2901
		internal abstract TAggregator CreateEmptyAggregator();

		// Token: 0x06000B56 RID: 2902
		internal abstract TAggregator UpdateAggregator(TAggregator aggregator, TParameters parameters);

		// Token: 0x06000B57 RID: 2903
		internal abstract void WriteTrace(StoreDatabase database, IBinaryLogger logger);

		// Token: 0x04000641 RID: 1601
		private readonly Stopwatch startTimeStamp;

		// Token: 0x04000642 RID: 1602
		private readonly ConcurrentDictionary<TKey, TAggregator> data;

		// Token: 0x04000643 RID: 1603
		private bool hasData;

		// Token: 0x04000644 RID: 1604
		private int governor;
	}
}
