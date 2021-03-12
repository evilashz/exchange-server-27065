using System;
using System.Threading;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x02000122 RID: 290
	public abstract class TraceCollector<TAggregator, TContainer, TKey, TParameters> where TAggregator : TraceDataAggregator<TParameters> where TContainer : TraceContainer<TAggregator, TKey, TParameters>, new() where TParameters : ITraceParameters
	{
		// Token: 0x06000B5F RID: 2911 RVA: 0x00039784 File Offset: 0x00037984
		protected TraceCollector(StoreDatabase database, LoggerType loggerType)
		{
			this.database = database;
			this.logger = LoggerManager.GetLogger(loggerType);
			this.container = Activator.CreateInstance<TContainer>();
		}

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x06000B60 RID: 2912 RVA: 0x000397AA File Offset: 0x000379AA
		internal TContainer Data
		{
			get
			{
				return this.container;
			}
		}

		// Token: 0x06000B61 RID: 2913 RVA: 0x000397B4 File Offset: 0x000379B4
		public void Add(TKey key, TParameters parameters)
		{
			if (this.logger != null && this.logger.IsLoggingEnabled)
			{
				this.container.Set(key, parameters);
				if (this.container.HasDataToLog && Interlocked.Exchange(ref this.governor, 1) == 0)
				{
					TContainer tcontainer = Interlocked.Exchange<TContainer>(ref this.container, Activator.CreateInstance<TContainer>());
					if (this.logger.IsLoggingEnabled)
					{
						tcontainer.Commit(this.database, this.logger);
					}
					Interlocked.Exchange(ref this.governor, 0);
				}
			}
		}

		// Token: 0x04000646 RID: 1606
		private readonly StoreDatabase database;

		// Token: 0x04000647 RID: 1607
		private readonly IBinaryLogger logger;

		// Token: 0x04000648 RID: 1608
		private TContainer container;

		// Token: 0x04000649 RID: 1609
		private int governor;
	}
}
