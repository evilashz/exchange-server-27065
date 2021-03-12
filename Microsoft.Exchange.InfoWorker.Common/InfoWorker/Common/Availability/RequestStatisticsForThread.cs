using System;
using Microsoft.Exchange.Win32;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x020000C9 RID: 201
	internal sealed class RequestStatisticsForThread
	{
		// Token: 0x06000508 RID: 1288 RVA: 0x00016650 File Offset: 0x00014850
		public static RequestStatisticsForThread Begin()
		{
			return new RequestStatisticsForThread
			{
				begin = ThreadTimes.GetFromCurrentThread()
			};
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x0001666F File Offset: 0x0001486F
		public RequestStatistics End(RequestStatisticsType tag)
		{
			return this.End(tag, null);
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x0001667C File Offset: 0x0001487C
		public RequestStatistics End(RequestStatisticsType tag, string destination)
		{
			ThreadTimes fromCurrentThread = ThreadTimes.GetFromCurrentThread();
			if (this.begin == null || fromCurrentThread == null)
			{
				return null;
			}
			long timeTaken = (long)(fromCurrentThread.Kernel.TotalMilliseconds - this.begin.Kernel.TotalMilliseconds) + (long)(fromCurrentThread.User.TotalMilliseconds - this.begin.User.TotalMilliseconds);
			if (destination == null)
			{
				return RequestStatistics.Create(tag, timeTaken);
			}
			return RequestStatistics.Create(tag, timeTaken, destination);
		}

		// Token: 0x040002F8 RID: 760
		private ThreadTimes begin;
	}
}
