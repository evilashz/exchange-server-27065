using System;
using System.Web;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x02000027 RID: 39
	internal class ThrottlingLogInfo
	{
		// Token: 0x060001D9 RID: 473 RVA: 0x00009F20 File Offset: 0x00008120
		private void InternalAddDataPoint(int throttleTime, float cpuPercent)
		{
			lock (this.lockObject)
			{
				double num = 1.0 - (double)this.sampleCount / ((double)this.sampleCount + 1.0);
				double num2 = 1.0 - num;
				this.averageCPUPercent = (float)((double)this.averageCPUPercent * num2 + (double)cpuPercent * num);
				this.averageThrottleTime = (int)((double)this.averageThrottleTime * num2 + (double)throttleTime * num);
				this.sampleCount++;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060001DA RID: 474 RVA: 0x00009FC4 File Offset: 0x000081C4
		public int AverageThrottleTime
		{
			get
			{
				return this.averageThrottleTime;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060001DB RID: 475 RVA: 0x00009FCC File Offset: 0x000081CC
		public int AverageCPUPercent
		{
			get
			{
				return (int)this.averageCPUPercent;
			}
		}

		// Token: 0x060001DC RID: 476 RVA: 0x00009FD8 File Offset: 0x000081D8
		public static void AddDataPoint(int throttleTime, float cpuPercent)
		{
			HttpContext httpContext = EWSSettings.GetHttpContext();
			if (httpContext == null)
			{
				return;
			}
			ThrottlingLogInfo throttlingLogInfo;
			if (!ThrottlingLogInfo.TryGet(out throttlingLogInfo))
			{
				throttlingLogInfo = new ThrottlingLogInfo();
				httpContext.Items[ThrottlingLogInfo.Key] = throttlingLogInfo;
			}
			throttlingLogInfo.InternalAddDataPoint(throttleTime, cpuPercent);
		}

		// Token: 0x060001DD RID: 477 RVA: 0x0000A017 File Offset: 0x00008217
		public static bool TryGet(out ThrottlingLogInfo info)
		{
			if (HttpContext.Current == null)
			{
				info = null;
				return false;
			}
			info = (HttpContext.Current.Items[ThrottlingLogInfo.Key] as ThrottlingLogInfo);
			return info != null;
		}

		// Token: 0x040001BD RID: 445
		private static readonly string Key = "ThrottlingLogInfo";

		// Token: 0x040001BE RID: 446
		private int averageThrottleTime;

		// Token: 0x040001BF RID: 447
		private float averageCPUPercent;

		// Token: 0x040001C0 RID: 448
		private int sampleCount;

		// Token: 0x040001C1 RID: 449
		private object lockObject = new object();
	}
}
