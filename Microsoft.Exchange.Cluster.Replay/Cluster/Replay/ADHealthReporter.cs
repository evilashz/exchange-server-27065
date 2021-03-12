using System;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020001DD RID: 477
	internal class ADHealthReporter
	{
		// Token: 0x06001303 RID: 4867 RVA: 0x0004CE9B File Offset: 0x0004B09B
		public ADHealthReporter(TimeSpan redSuppression, TimeSpan greenSuppression)
		{
			this.maxGreenFreq = greenSuppression;
			this.maxRedFreq = redSuppression;
		}

		// Token: 0x06001304 RID: 4868 RVA: 0x0004CEC7 File Offset: 0x0004B0C7
		public ADHealthReporter() : this(TimeSpan.FromMinutes(2.0), TimeSpan.FromMinutes(5.0))
		{
		}

		// Token: 0x06001305 RID: 4869 RVA: 0x0004CEEC File Offset: 0x0004B0EC
		public void RaiseRedEvent(string failureMsg)
		{
			if (ExDateTime.Now - this.lastRedEventRaisedTime > this.maxRedFreq || this.lastGreenEventRaisedTime > this.lastRedEventRaisedTime)
			{
				string text = string.Format("ADHealthReporter: AD access is failing: {0}", failureMsg);
				EventNotificationItem eventNotificationItem = new EventNotificationItem("MSExchangeRepl", "MonitoringADConfigManager", "ADConfigQueryStatus", text, text, ResultSeverityLevel.Critical);
				eventNotificationItem.Publish(false);
				this.lastRedEventRaisedTime = ExDateTime.Now;
			}
		}

		// Token: 0x06001306 RID: 4870 RVA: 0x0004CF60 File Offset: 0x0004B160
		public void RaiseGreenEvent()
		{
			if (ExDateTime.Now - this.lastGreenEventRaisedTime > this.maxGreenFreq)
			{
				string text = "ADHealthReporter: AD access is healthy.";
				EventNotificationItem eventNotificationItem = new EventNotificationItem("MSExchangeRepl", "MonitoringADConfigManager", "ADConfigQueryStatus", text, text, ResultSeverityLevel.Informational);
				eventNotificationItem.Publish(false);
				this.lastGreenEventRaisedTime = ExDateTime.Now;
			}
		}

		// Token: 0x04000742 RID: 1858
		public const string NotificationItemServiceName = "MSExchangeRepl";

		// Token: 0x04000743 RID: 1859
		public const string NotificationItemComponentName = "MonitoringADConfigManager";

		// Token: 0x04000744 RID: 1860
		public const string NotificationItemTag = "ADConfigQueryStatus";

		// Token: 0x04000745 RID: 1861
		private ExDateTime lastGreenEventRaisedTime = ExDateTime.MinValue;

		// Token: 0x04000746 RID: 1862
		private ExDateTime lastRedEventRaisedTime = ExDateTime.MinValue;

		// Token: 0x04000747 RID: 1863
		private readonly TimeSpan maxGreenFreq;

		// Token: 0x04000748 RID: 1864
		private readonly TimeSpan maxRedFreq;
	}
}
