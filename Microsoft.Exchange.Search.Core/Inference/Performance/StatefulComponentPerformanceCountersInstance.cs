using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Inference.Performance
{
	// Token: 0x020000CE RID: 206
	internal sealed class StatefulComponentPerformanceCountersInstance : PerformanceCounterInstance
	{
		// Token: 0x06000631 RID: 1585 RVA: 0x00013BB4 File Offset: 0x00011DB4
		internal StatefulComponentPerformanceCountersInstance(string instanceName, StatefulComponentPerformanceCountersInstance autoUpdateTotalInstance) : base(instanceName, "MSExchangeInference StatefulComponent")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.AverageSignalDispatchingLatency = new ExPerformanceCounter(base.CategoryName, "Average Signal Dispatching Latency (msec)", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageSignalDispatchingLatency);
				this.AverageSignalDispatchingLatencyBase = new ExPerformanceCounter(base.CategoryName, "Average Signal Dispatching Latency Base", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageSignalDispatchingLatencyBase);
				long num = this.AverageSignalDispatchingLatency.RawValue;
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

		// Token: 0x06000632 RID: 1586 RVA: 0x00013C9C File Offset: 0x00011E9C
		internal StatefulComponentPerformanceCountersInstance(string instanceName) : base(instanceName, "MSExchangeInference StatefulComponent")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.AverageSignalDispatchingLatency = new ExPerformanceCounter(base.CategoryName, "Average Signal Dispatching Latency (msec)", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageSignalDispatchingLatency);
				this.AverageSignalDispatchingLatencyBase = new ExPerformanceCounter(base.CategoryName, "Average Signal Dispatching Latency Base", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageSignalDispatchingLatencyBase);
				long num = this.AverageSignalDispatchingLatency.RawValue;
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

		// Token: 0x06000633 RID: 1587 RVA: 0x00013D84 File Offset: 0x00011F84
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

		// Token: 0x04000312 RID: 786
		public readonly ExPerformanceCounter AverageSignalDispatchingLatency;

		// Token: 0x04000313 RID: 787
		public readonly ExPerformanceCounter AverageSignalDispatchingLatencyBase;
	}
}
