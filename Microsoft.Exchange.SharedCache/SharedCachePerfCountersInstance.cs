using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.SharedCache
{
	// Token: 0x02000003 RID: 3
	internal sealed class SharedCachePerfCountersInstance : PerformanceCounterInstance
	{
		// Token: 0x0600000C RID: 12 RVA: 0x00002178 File Offset: 0x00000378
		internal SharedCachePerfCountersInstance(string instanceName, SharedCachePerfCountersInstance autoUpdateTotalInstance) : base(instanceName, "MSExchange Shared Cache")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				ExPerformanceCounter exPerformanceCounter = new ExPerformanceCounter(base.CategoryName, "Requests/Sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter);
				this.TotalRequests = new ExPerformanceCounter(base.CategoryName, "Total Requests", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter
				});
				list.Add(this.TotalRequests);
				this.CacheSize = new ExPerformanceCounter(base.CategoryName, "Cache Size", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.CacheSize);
				this.CacheHitRate = new ExPerformanceCounter(base.CategoryName, "Cache Hit Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.CacheHitRate);
				this.AverageLatency = new ExPerformanceCounter(base.CategoryName, "Average Latency", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageLatency);
				this.FailedRequests = new ExPerformanceCounter(base.CategoryName, "Failed Requests", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.FailedRequests);
				this.CorruptEntries = new ExPerformanceCounter(base.CategoryName, "Corrupt Entries", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.CorruptEntries);
				this.DiskSpace = new ExPerformanceCounter(base.CategoryName, "Disk Space (bytes)", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.DiskSpace);
				long num = this.TotalRequests.RawValue;
				num += 1L;
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					foreach (ExPerformanceCounter exPerformanceCounter2 in list)
					{
						exPerformanceCounter2.Close();
					}
				}
			}
			this.counters = list.ToArray();
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002378 File Offset: 0x00000578
		internal SharedCachePerfCountersInstance(string instanceName) : base(instanceName, "MSExchange Shared Cache")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				ExPerformanceCounter exPerformanceCounter = new ExPerformanceCounter(base.CategoryName, "Requests/Sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter);
				this.TotalRequests = new ExPerformanceCounter(base.CategoryName, "Total Requests", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter
				});
				list.Add(this.TotalRequests);
				this.CacheSize = new ExPerformanceCounter(base.CategoryName, "Cache Size", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.CacheSize);
				this.CacheHitRate = new ExPerformanceCounter(base.CategoryName, "Cache Hit Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.CacheHitRate);
				this.AverageLatency = new ExPerformanceCounter(base.CategoryName, "Average Latency", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageLatency);
				this.FailedRequests = new ExPerformanceCounter(base.CategoryName, "Failed Requests", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.FailedRequests);
				this.CorruptEntries = new ExPerformanceCounter(base.CategoryName, "Corrupt Entries", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.CorruptEntries);
				this.DiskSpace = new ExPerformanceCounter(base.CategoryName, "Disk Space (bytes)", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.DiskSpace);
				long num = this.TotalRequests.RawValue;
				num += 1L;
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					foreach (ExPerformanceCounter exPerformanceCounter2 in list)
					{
						exPerformanceCounter2.Close();
					}
				}
			}
			this.counters = list.ToArray();
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002578 File Offset: 0x00000778
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

		// Token: 0x04000003 RID: 3
		public readonly ExPerformanceCounter TotalRequests;

		// Token: 0x04000004 RID: 4
		public readonly ExPerformanceCounter CacheSize;

		// Token: 0x04000005 RID: 5
		public readonly ExPerformanceCounter CacheHitRate;

		// Token: 0x04000006 RID: 6
		public readonly ExPerformanceCounter AverageLatency;

		// Token: 0x04000007 RID: 7
		public readonly ExPerformanceCounter FailedRequests;

		// Token: 0x04000008 RID: 8
		public readonly ExPerformanceCounter CorruptEntries;

		// Token: 0x04000009 RID: 9
		public readonly ExPerformanceCounter DiskSpace;
	}
}
