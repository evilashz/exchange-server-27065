using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x020000C7 RID: 199
	internal sealed class RequestStatisticsForAD
	{
		// Token: 0x06000500 RID: 1280 RVA: 0x00016518 File Offset: 0x00014718
		public static RequestStatisticsForAD Begin()
		{
			return new RequestStatisticsForAD
			{
				begin = new PerformanceContext(PerformanceContext.Current)
			};
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x0001653C File Offset: 0x0001473C
		public RequestStatistics End(RequestStatisticsType tag)
		{
			return this.End(tag, null);
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x00016548 File Offset: 0x00014748
		public RequestStatistics End(RequestStatisticsType tag, string destination)
		{
			PerformanceContext performanceContext = PerformanceContext.Current;
			long timeTaken = 0L;
			int requestCount = 0;
			if (this.begin != null && performanceContext != null)
			{
				timeTaken = (long)(performanceContext.RequestLatency - this.begin.RequestLatency);
				requestCount = (int)(performanceContext.RequestCount - this.begin.RequestCount);
			}
			if (destination == null)
			{
				return RequestStatistics.Create(tag, timeTaken, requestCount);
			}
			return RequestStatistics.Create(tag, timeTaken, requestCount, destination);
		}

		// Token: 0x040002F5 RID: 757
		private PerformanceContext begin;
	}
}
