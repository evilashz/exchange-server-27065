using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Net
{
	// Token: 0x02000182 RID: 386
	internal sealed class ServiceProxyPoolCountersInstance : PerformanceCounterInstance
	{
		// Token: 0x06000940 RID: 2368 RVA: 0x0001A47C File Offset: 0x0001867C
		internal ServiceProxyPoolCountersInstance(string instanceName, ServiceProxyPoolCountersInstance autoUpdateTotalInstance) : base(instanceName, "MSExchange ServiceProxyPool")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.ProxyInstanceCount = new ExPerformanceCounter(base.CategoryName, "Proxy Instance Count", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.ProxyInstanceCount);
				this.OutstandingCalls = new ExPerformanceCounter(base.CategoryName, "Current Outstanding Calls", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.OutstandingCalls);
				ExPerformanceCounter exPerformanceCounter = new ExPerformanceCounter(base.CategoryName, "Calls/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter);
				this.NumberOfCalls = new ExPerformanceCounter(base.CategoryName, "Total Number of Calls", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter
				});
				list.Add(this.NumberOfCalls);
				this.AverageLatency = new ExPerformanceCounter(base.CategoryName, "Average Latency", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageLatency);
				this.AverageLatencyBase = new ExPerformanceCounter(base.CategoryName, "Base for Average Latency", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageLatencyBase);
				long num = this.ProxyInstanceCount.RawValue;
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

		// Token: 0x06000941 RID: 2369 RVA: 0x0001A628 File Offset: 0x00018828
		internal ServiceProxyPoolCountersInstance(string instanceName) : base(instanceName, "MSExchange ServiceProxyPool")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.ProxyInstanceCount = new ExPerformanceCounter(base.CategoryName, "Proxy Instance Count", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.ProxyInstanceCount);
				this.OutstandingCalls = new ExPerformanceCounter(base.CategoryName, "Current Outstanding Calls", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.OutstandingCalls);
				ExPerformanceCounter exPerformanceCounter = new ExPerformanceCounter(base.CategoryName, "Calls/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter);
				this.NumberOfCalls = new ExPerformanceCounter(base.CategoryName, "Total Number of Calls", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter
				});
				list.Add(this.NumberOfCalls);
				this.AverageLatency = new ExPerformanceCounter(base.CategoryName, "Average Latency", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageLatency);
				this.AverageLatencyBase = new ExPerformanceCounter(base.CategoryName, "Base for Average Latency", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageLatencyBase);
				long num = this.ProxyInstanceCount.RawValue;
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

		// Token: 0x06000942 RID: 2370 RVA: 0x0001A7D4 File Offset: 0x000189D4
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

		// Token: 0x040006B0 RID: 1712
		public readonly ExPerformanceCounter ProxyInstanceCount;

		// Token: 0x040006B1 RID: 1713
		public readonly ExPerformanceCounter OutstandingCalls;

		// Token: 0x040006B2 RID: 1714
		public readonly ExPerformanceCounter NumberOfCalls;

		// Token: 0x040006B3 RID: 1715
		public readonly ExPerformanceCounter AverageLatency;

		// Token: 0x040006B4 RID: 1716
		public readonly ExPerformanceCounter AverageLatencyBase;
	}
}
