using System;
using System.Collections.Generic;
using System.Web;

namespace Microsoft.Exchange.Diagnostics.LatencyDetection
{
	// Token: 0x02000164 RID: 356
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class AspPerformanceData
	{
		// Token: 0x06000A2D RID: 2605 RVA: 0x00026238 File Offset: 0x00024438
		public static PerformanceDataProvider GetStepData(RequestNotification notification)
		{
			if (AspPerformanceData.stepData == null)
			{
				AspPerformanceData.stepData = new Dictionary<RequestNotification, PerformanceDataProvider>(13);
			}
			PerformanceDataProvider performanceDataProvider;
			if (!AspPerformanceData.stepData.TryGetValue(notification, out performanceDataProvider))
			{
				performanceDataProvider = new PerformanceDataProvider(notification.ToString());
				AspPerformanceData.stepData[notification] = performanceDataProvider;
			}
			return performanceDataProvider;
		}

		// Token: 0x06000A2E RID: 2606 RVA: 0x00026285 File Offset: 0x00024485
		public void StepStarted(RequestNotification notification)
		{
			this.StepCompleted();
			this.activeNotificationTimer = AspPerformanceData.GetStepData(notification).StartRequestTimer();
		}

		// Token: 0x06000A2F RID: 2607 RVA: 0x000262A0 File Offset: 0x000244A0
		public void StepCompleted()
		{
			try
			{
				if (this.activeNotificationTimer != null)
				{
					this.activeNotificationTimer.Dispose();
				}
			}
			finally
			{
				this.activeNotificationTimer = null;
			}
		}

		// Token: 0x040006E9 RID: 1769
		[ThreadStatic]
		private static Dictionary<RequestNotification, PerformanceDataProvider> stepData;

		// Token: 0x040006EA RID: 1770
		private IDisposable activeNotificationTimer;
	}
}
