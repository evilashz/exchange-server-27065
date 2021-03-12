using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Compliance.TaskDistributionFabric
{
	// Token: 0x0200002B RID: 43
	internal sealed class TaskDistributionFabricPerfCountersInstance : PerformanceCounterInstance
	{
		// Token: 0x060000EA RID: 234 RVA: 0x00005E58 File Offset: 0x00004058
		internal TaskDistributionFabricPerfCountersInstance(string instanceName, TaskDistributionFabricPerfCountersInstance autoUpdateTotalInstance) : base(instanceName, "MSExchange Task Distribution Fabric")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.QueueSize75Percentile = new ExPerformanceCounter(base.CategoryName, "Queue size - 75 percentile", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.QueueSize75Percentile);
				this.QueueSize90Percentile = new ExPerformanceCounter(base.CategoryName, "Queue size - 90 percentile", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.QueueSize90Percentile);
				this.QueueSize99Percentile = new ExPerformanceCounter(base.CategoryName, "Queue size - 99 percentile", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.QueueSize99Percentile);
				this.QueueLatency75Percentile = new ExPerformanceCounter(base.CategoryName, "Queue latency - 75 percentile", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.QueueLatency75Percentile);
				this.QueueLatency90Percentile = new ExPerformanceCounter(base.CategoryName, "Queue latency - 90 percentile", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.QueueLatency90Percentile);
				this.QueueLatency99Percentile = new ExPerformanceCounter(base.CategoryName, "Queue latency - 99 percentile", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.QueueLatency99Percentile);
				this.ProcessorLatency75Percentile = new ExPerformanceCounter(base.CategoryName, "Processor latency - 75 percentile", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.ProcessorLatency75Percentile);
				this.ProcessorLatency90Percentile = new ExPerformanceCounter(base.CategoryName, "Processor latency - 90 percentile", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.ProcessorLatency90Percentile);
				this.ProcessorLatency99Percentile = new ExPerformanceCounter(base.CategoryName, "Processor latency - 99 percentile", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.ProcessorLatency99Percentile);
				this.CurrentRequests = new ExPerformanceCounter(base.CategoryName, "Current requests", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.CurrentRequests);
				this.CurrentQueueLength = new ExPerformanceCounter(base.CategoryName, "Current queue length", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.CurrentQueueLength);
				this.RecentFailedRequestsCount = new ExPerformanceCounter(base.CategoryName, "Failed requests in last 15 minutes", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.RecentFailedRequestsCount);
				this.RecentSuccessfulRequestsCount = new ExPerformanceCounter(base.CategoryName, "Successful requests in last 15 minutes", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.RecentSuccessfulRequestsCount);
				long num = this.QueueSize75Percentile.RawValue;
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

		// Token: 0x060000EB RID: 235 RVA: 0x00006128 File Offset: 0x00004328
		internal TaskDistributionFabricPerfCountersInstance(string instanceName) : base(instanceName, "MSExchange Task Distribution Fabric")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.QueueSize75Percentile = new ExPerformanceCounter(base.CategoryName, "Queue size - 75 percentile", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.QueueSize75Percentile);
				this.QueueSize90Percentile = new ExPerformanceCounter(base.CategoryName, "Queue size - 90 percentile", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.QueueSize90Percentile);
				this.QueueSize99Percentile = new ExPerformanceCounter(base.CategoryName, "Queue size - 99 percentile", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.QueueSize99Percentile);
				this.QueueLatency75Percentile = new ExPerformanceCounter(base.CategoryName, "Queue latency - 75 percentile", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.QueueLatency75Percentile);
				this.QueueLatency90Percentile = new ExPerformanceCounter(base.CategoryName, "Queue latency - 90 percentile", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.QueueLatency90Percentile);
				this.QueueLatency99Percentile = new ExPerformanceCounter(base.CategoryName, "Queue latency - 99 percentile", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.QueueLatency99Percentile);
				this.ProcessorLatency75Percentile = new ExPerformanceCounter(base.CategoryName, "Processor latency - 75 percentile", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.ProcessorLatency75Percentile);
				this.ProcessorLatency90Percentile = new ExPerformanceCounter(base.CategoryName, "Processor latency - 90 percentile", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.ProcessorLatency90Percentile);
				this.ProcessorLatency99Percentile = new ExPerformanceCounter(base.CategoryName, "Processor latency - 99 percentile", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.ProcessorLatency99Percentile);
				this.CurrentRequests = new ExPerformanceCounter(base.CategoryName, "Current requests", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.CurrentRequests);
				this.CurrentQueueLength = new ExPerformanceCounter(base.CategoryName, "Current queue length", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.CurrentQueueLength);
				this.RecentFailedRequestsCount = new ExPerformanceCounter(base.CategoryName, "Failed requests in last 15 minutes", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.RecentFailedRequestsCount);
				this.RecentSuccessfulRequestsCount = new ExPerformanceCounter(base.CategoryName, "Successful requests in last 15 minutes", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.RecentSuccessfulRequestsCount);
				long num = this.QueueSize75Percentile.RawValue;
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

		// Token: 0x060000EC RID: 236 RVA: 0x000063F8 File Offset: 0x000045F8
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

		// Token: 0x04000065 RID: 101
		public readonly ExPerformanceCounter QueueSize75Percentile;

		// Token: 0x04000066 RID: 102
		public readonly ExPerformanceCounter QueueSize90Percentile;

		// Token: 0x04000067 RID: 103
		public readonly ExPerformanceCounter QueueSize99Percentile;

		// Token: 0x04000068 RID: 104
		public readonly ExPerformanceCounter QueueLatency75Percentile;

		// Token: 0x04000069 RID: 105
		public readonly ExPerformanceCounter QueueLatency90Percentile;

		// Token: 0x0400006A RID: 106
		public readonly ExPerformanceCounter QueueLatency99Percentile;

		// Token: 0x0400006B RID: 107
		public readonly ExPerformanceCounter ProcessorLatency75Percentile;

		// Token: 0x0400006C RID: 108
		public readonly ExPerformanceCounter ProcessorLatency90Percentile;

		// Token: 0x0400006D RID: 109
		public readonly ExPerformanceCounter ProcessorLatency99Percentile;

		// Token: 0x0400006E RID: 110
		public readonly ExPerformanceCounter CurrentRequests;

		// Token: 0x0400006F RID: 111
		public readonly ExPerformanceCounter CurrentQueueLength;

		// Token: 0x04000070 RID: 112
		public readonly ExPerformanceCounter RecentFailedRequestsCount;

		// Token: 0x04000071 RID: 113
		public readonly ExPerformanceCounter RecentSuccessfulRequestsCount;
	}
}
