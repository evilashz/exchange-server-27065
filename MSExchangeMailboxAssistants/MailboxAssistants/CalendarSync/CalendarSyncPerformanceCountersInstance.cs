using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxAssistants.CalendarSync
{
	// Token: 0x020000CC RID: 204
	internal sealed class CalendarSyncPerformanceCountersInstance : PerformanceCounterInstance
	{
		// Token: 0x060008B8 RID: 2232 RVA: 0x0003B804 File Offset: 0x00039A04
		internal CalendarSyncPerformanceCountersInstance(string instanceName, CalendarSyncPerformanceCountersInstance autoUpdateTotalInstance) : base(instanceName, "MSExchange Calendar Sync Assistant")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.OnDemandRequests = new ExPerformanceCounter(base.CategoryName, "On demand requests", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.OnDemandRequests, new ExPerformanceCounter[0]);
				list.Add(this.OnDemandRequests);
				this.AverageSubscriptionsPerMailbox = new ExPerformanceCounter(base.CategoryName, "Average Number of Subscriptions per Mailbox", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.AverageSubscriptionsPerMailbox, new ExPerformanceCounter[0]);
				list.Add(this.AverageSubscriptionsPerMailbox);
				this.AverageSubscriptionsPerMailboxBase = new ExPerformanceCounter(base.CategoryName, "Average Number of Subscriptions per Mailbox Base", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.AverageSubscriptionsPerMailboxBase, new ExPerformanceCounter[0]);
				list.Add(this.AverageSubscriptionsPerMailboxBase);
				this.AverageTimeBetweenSyncs = new ExPerformanceCounter(base.CategoryName, "Average Time Between Syncs", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.AverageTimeBetweenSyncs, new ExPerformanceCounter[0]);
				list.Add(this.AverageTimeBetweenSyncs);
				this.AverageTimeBetweenSyncsBase = new ExPerformanceCounter(base.CategoryName, "Average Time Between Syncs Base", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.AverageTimeBetweenSyncsBase, new ExPerformanceCounter[0]);
				list.Add(this.AverageTimeBetweenSyncsBase);
				this.AverageDownloadTime = new ExPerformanceCounter(base.CategoryName, "Average Download Time", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.AverageDownloadTime, new ExPerformanceCounter[0]);
				list.Add(this.AverageDownloadTime);
				this.AverageDownloadTimeBase = new ExPerformanceCounter(base.CategoryName, "Average Download Time Base", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.AverageDownloadTimeBase, new ExPerformanceCounter[0]);
				list.Add(this.AverageDownloadTimeBase);
				this.AverageSubscriptionProcessingTime = new ExPerformanceCounter(base.CategoryName, "Average Subscription Processing Time", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.AverageSubscriptionProcessingTime, new ExPerformanceCounter[0]);
				list.Add(this.AverageSubscriptionProcessingTime);
				this.AverageSubscriptionProcessingTimeBase = new ExPerformanceCounter(base.CategoryName, "Average Subscription Processing Time Base", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.AverageSubscriptionProcessingTimeBase, new ExPerformanceCounter[0]);
				list.Add(this.AverageSubscriptionProcessingTimeBase);
				this.AverageSubscriptionImportTime = new ExPerformanceCounter(base.CategoryName, "Average Subscription Import Time", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.AverageSubscriptionImportTime, new ExPerformanceCounter[0]);
				list.Add(this.AverageSubscriptionImportTime);
				this.AverageSubscriptionImportTimeBase = new ExPerformanceCounter(base.CategoryName, "Average Subscription Import Time Base", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.AverageSubscriptionImportTimeBase, new ExPerformanceCounter[0]);
				list.Add(this.AverageSubscriptionImportTimeBase);
				long num = this.OnDemandRequests.RawValue;
				num += 1L;
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					foreach (ExPerformanceCounter exPerformanceCounter in list)
					{
						exPerformanceCounter.Close();
					}
				}
			}
			this.counters = list.ToArray();
		}

		// Token: 0x060008B9 RID: 2233 RVA: 0x0003BB04 File Offset: 0x00039D04
		internal CalendarSyncPerformanceCountersInstance(string instanceName) : base(instanceName, "MSExchange Calendar Sync Assistant")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.OnDemandRequests = new ExPerformanceCounter(base.CategoryName, "On demand requests", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.OnDemandRequests);
				this.AverageSubscriptionsPerMailbox = new ExPerformanceCounter(base.CategoryName, "Average Number of Subscriptions per Mailbox", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageSubscriptionsPerMailbox);
				this.AverageSubscriptionsPerMailboxBase = new ExPerformanceCounter(base.CategoryName, "Average Number of Subscriptions per Mailbox Base", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageSubscriptionsPerMailboxBase);
				this.AverageTimeBetweenSyncs = new ExPerformanceCounter(base.CategoryName, "Average Time Between Syncs", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageTimeBetweenSyncs);
				this.AverageTimeBetweenSyncsBase = new ExPerformanceCounter(base.CategoryName, "Average Time Between Syncs Base", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageTimeBetweenSyncsBase);
				this.AverageDownloadTime = new ExPerformanceCounter(base.CategoryName, "Average Download Time", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageDownloadTime);
				this.AverageDownloadTimeBase = new ExPerformanceCounter(base.CategoryName, "Average Download Time Base", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageDownloadTimeBase);
				this.AverageSubscriptionProcessingTime = new ExPerformanceCounter(base.CategoryName, "Average Subscription Processing Time", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageSubscriptionProcessingTime);
				this.AverageSubscriptionProcessingTimeBase = new ExPerformanceCounter(base.CategoryName, "Average Subscription Processing Time Base", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageSubscriptionProcessingTimeBase);
				this.AverageSubscriptionImportTime = new ExPerformanceCounter(base.CategoryName, "Average Subscription Import Time", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageSubscriptionImportTime);
				this.AverageSubscriptionImportTimeBase = new ExPerformanceCounter(base.CategoryName, "Average Subscription Import Time Base", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageSubscriptionImportTimeBase);
				long num = this.OnDemandRequests.RawValue;
				num += 1L;
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					foreach (ExPerformanceCounter exPerformanceCounter in list)
					{
						exPerformanceCounter.Close();
					}
				}
			}
			this.counters = list.ToArray();
		}

		// Token: 0x060008BA RID: 2234 RVA: 0x0003BD88 File Offset: 0x00039F88
		public override void GetPerfCounterDiagnosticsInfo(XElement topElement)
		{
			XElement xelement = null;
			foreach (ExPerformanceCounter exPerformanceCounter in this.counters)
			{
				try
				{
					if (xelement == null)
					{
						xelement = new XElement(ExPerformanceCounter.GetEncodedName(exPerformanceCounter.InstanceName));
						topElement.Add(xelement);
					}
					xelement.Add(new XElement(ExPerformanceCounter.GetEncodedName(exPerformanceCounter.CounterName), exPerformanceCounter.NextValue()));
				}
				catch (XmlException ex)
				{
					XElement content = new XElement("Error", ex.Message);
					topElement.Add(content);
				}
			}
		}

		// Token: 0x04000605 RID: 1541
		public readonly ExPerformanceCounter OnDemandRequests;

		// Token: 0x04000606 RID: 1542
		public readonly ExPerformanceCounter AverageSubscriptionsPerMailbox;

		// Token: 0x04000607 RID: 1543
		public readonly ExPerformanceCounter AverageSubscriptionsPerMailboxBase;

		// Token: 0x04000608 RID: 1544
		public readonly ExPerformanceCounter AverageTimeBetweenSyncs;

		// Token: 0x04000609 RID: 1545
		public readonly ExPerformanceCounter AverageTimeBetweenSyncsBase;

		// Token: 0x0400060A RID: 1546
		public readonly ExPerformanceCounter AverageDownloadTime;

		// Token: 0x0400060B RID: 1547
		public readonly ExPerformanceCounter AverageDownloadTimeBase;

		// Token: 0x0400060C RID: 1548
		public readonly ExPerformanceCounter AverageSubscriptionProcessingTime;

		// Token: 0x0400060D RID: 1549
		public readonly ExPerformanceCounter AverageSubscriptionProcessingTimeBase;

		// Token: 0x0400060E RID: 1550
		public readonly ExPerformanceCounter AverageSubscriptionImportTime;

		// Token: 0x0400060F RID: 1551
		public readonly ExPerformanceCounter AverageSubscriptionImportTimeBase;
	}
}
