using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x0200010E RID: 270
	internal sealed class PublisherCountersInstance : PerformanceCounterInstance
	{
		// Token: 0x060008BB RID: 2235 RVA: 0x0001AAFC File Offset: 0x00018CFC
		internal PublisherCountersInstance(string instanceName, PublisherCountersInstance autoUpdateTotalInstance) : base(instanceName, "MSExchange Push Notifications Publishers")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.QueueSize = new ExPerformanceCounter(base.CategoryName, "Publisher Queue Size - Count", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.QueueSize);
				this.QueueSizePeak = new ExPerformanceCounter(base.CategoryName, "Publisher Queue Size - Peak", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.QueueSizePeak);
				this.QueueSizeTotal = new ExPerformanceCounter(base.CategoryName, "Publisher Queue Size - Total", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.QueueSizeTotal);
				this.EnqueueError = new ExPerformanceCounter(base.CategoryName, "Publisher Enqueue Error - Counter", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.EnqueueError);
				this.PreprocessError = new ExPerformanceCounter(base.CategoryName, "Preprocess Error - Counter", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.PreprocessError);
				ExPerformanceCounter exPerformanceCounter = new ExPerformanceCounter(base.CategoryName, "Total Notifications Sent/Sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter);
				this.TotalNotificationsSent = new ExPerformanceCounter(base.CategoryName, "Total Notifications Sent - Counter", instanceName, true, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter
				});
				list.Add(this.TotalNotificationsSent);
				ExPerformanceCounter exPerformanceCounter2 = new ExPerformanceCounter(base.CategoryName, "Total Notifications Discarded/Sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter2);
				this.TotalNotificationsDiscarded = new ExPerformanceCounter(base.CategoryName, "Total Notifications Discarded - Counter", instanceName, true, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter2
				});
				list.Add(this.TotalNotificationsDiscarded);
				this.AveragePublisherSendTime = new ExPerformanceCounter(base.CategoryName, "Publisher Send - Average Time", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.AveragePublisherSendTime);
				this.AveragePublisherSendTimeBase = new ExPerformanceCounter(base.CategoryName, "Publisher Send - Average Time Base", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.AveragePublisherSendTimeBase);
				this.AveragePreprocessTime = new ExPerformanceCounter(base.CategoryName, "Preprocess - Average Time", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.AveragePreprocessTime);
				this.AveragePreprocessTimeBase = new ExPerformanceCounter(base.CategoryName, "Preprocess - Average Time Base", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.AveragePreprocessTimeBase);
				this.AverageQueueItemTime = new ExPerformanceCounter(base.CategoryName, "Queue Item - Average Time", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageQueueItemTime);
				this.AverageQueueItemTimeBase = new ExPerformanceCounter(base.CategoryName, "Queue Item - Average Time Base", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageQueueItemTimeBase);
				long num = this.QueueSize.RawValue;
				num += 1L;
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					foreach (ExPerformanceCounter exPerformanceCounter3 in list)
					{
						exPerformanceCounter3.Close();
					}
				}
			}
			this.counters = list.ToArray();
		}

		// Token: 0x060008BC RID: 2236 RVA: 0x0001AE34 File Offset: 0x00019034
		internal PublisherCountersInstance(string instanceName) : base(instanceName, "MSExchange Push Notifications Publishers")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.QueueSize = new ExPerformanceCounter(base.CategoryName, "Publisher Queue Size - Count", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.QueueSize);
				this.QueueSizePeak = new ExPerformanceCounter(base.CategoryName, "Publisher Queue Size - Peak", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.QueueSizePeak);
				this.QueueSizeTotal = new ExPerformanceCounter(base.CategoryName, "Publisher Queue Size - Total", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.QueueSizeTotal);
				this.EnqueueError = new ExPerformanceCounter(base.CategoryName, "Publisher Enqueue Error - Counter", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.EnqueueError);
				this.PreprocessError = new ExPerformanceCounter(base.CategoryName, "Preprocess Error - Counter", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.PreprocessError);
				ExPerformanceCounter exPerformanceCounter = new ExPerformanceCounter(base.CategoryName, "Total Notifications Sent/Sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter);
				this.TotalNotificationsSent = new ExPerformanceCounter(base.CategoryName, "Total Notifications Sent - Counter", instanceName, true, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter
				});
				list.Add(this.TotalNotificationsSent);
				ExPerformanceCounter exPerformanceCounter2 = new ExPerformanceCounter(base.CategoryName, "Total Notifications Discarded/Sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter2);
				this.TotalNotificationsDiscarded = new ExPerformanceCounter(base.CategoryName, "Total Notifications Discarded - Counter", instanceName, true, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter2
				});
				list.Add(this.TotalNotificationsDiscarded);
				this.AveragePublisherSendTime = new ExPerformanceCounter(base.CategoryName, "Publisher Send - Average Time", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.AveragePublisherSendTime);
				this.AveragePublisherSendTimeBase = new ExPerformanceCounter(base.CategoryName, "Publisher Send - Average Time Base", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.AveragePublisherSendTimeBase);
				this.AveragePreprocessTime = new ExPerformanceCounter(base.CategoryName, "Preprocess - Average Time", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.AveragePreprocessTime);
				this.AveragePreprocessTimeBase = new ExPerformanceCounter(base.CategoryName, "Preprocess - Average Time Base", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.AveragePreprocessTimeBase);
				this.AverageQueueItemTime = new ExPerformanceCounter(base.CategoryName, "Queue Item - Average Time", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageQueueItemTime);
				this.AverageQueueItemTimeBase = new ExPerformanceCounter(base.CategoryName, "Queue Item - Average Time Base", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageQueueItemTimeBase);
				long num = this.QueueSize.RawValue;
				num += 1L;
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					foreach (ExPerformanceCounter exPerformanceCounter3 in list)
					{
						exPerformanceCounter3.Close();
					}
				}
			}
			this.counters = list.ToArray();
		}

		// Token: 0x060008BD RID: 2237 RVA: 0x0001B16C File Offset: 0x0001936C
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

		// Token: 0x040004EF RID: 1263
		public readonly ExPerformanceCounter QueueSize;

		// Token: 0x040004F0 RID: 1264
		public readonly ExPerformanceCounter QueueSizePeak;

		// Token: 0x040004F1 RID: 1265
		public readonly ExPerformanceCounter QueueSizeTotal;

		// Token: 0x040004F2 RID: 1266
		public readonly ExPerformanceCounter EnqueueError;

		// Token: 0x040004F3 RID: 1267
		public readonly ExPerformanceCounter PreprocessError;

		// Token: 0x040004F4 RID: 1268
		public readonly ExPerformanceCounter TotalNotificationsSent;

		// Token: 0x040004F5 RID: 1269
		public readonly ExPerformanceCounter TotalNotificationsDiscarded;

		// Token: 0x040004F6 RID: 1270
		public readonly ExPerformanceCounter AveragePublisherSendTime;

		// Token: 0x040004F7 RID: 1271
		public readonly ExPerformanceCounter AveragePublisherSendTimeBase;

		// Token: 0x040004F8 RID: 1272
		public readonly ExPerformanceCounter AveragePreprocessTime;

		// Token: 0x040004F9 RID: 1273
		public readonly ExPerformanceCounter AveragePreprocessTimeBase;

		// Token: 0x040004FA RID: 1274
		public readonly ExPerformanceCounter AverageQueueItemTime;

		// Token: 0x040004FB RID: 1275
		public readonly ExPerformanceCounter AverageQueueItemTimeBase;
	}
}
