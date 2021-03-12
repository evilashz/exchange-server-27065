using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Directory.ExchangeTopology
{
	// Token: 0x02000A41 RID: 2625
	internal sealed class ExchangeTopologyPerformanceCountersInstance : PerformanceCounterInstance
	{
		// Token: 0x06007845 RID: 30789 RVA: 0x0018D3A0 File Offset: 0x0018B5A0
		internal ExchangeTopologyPerformanceCountersInstance(string instanceName, ExchangeTopologyPerformanceCountersInstance autoUpdateTotalInstance) : base(instanceName, "MSExchange Topology")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.LastExchangeTopologyDiscoveryTimeSeconds = new ExPerformanceCounter(base.CategoryName, "Latest Exchange Topology Discovery Time in Seconds", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.LastExchangeTopologyDiscoveryTimeSeconds);
				this.ExchangeTopologyDiscoveriesPerformed = new ExPerformanceCounter(base.CategoryName, "Number of Exchange Topology Discoveries", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ExchangeTopologyDiscoveriesPerformed);
				this.SitelessServers = new ExPerformanceCounter(base.CategoryName, "Number of Siteless Servers", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.SitelessServers);
				long num = this.LastExchangeTopologyDiscoveryTimeSeconds.RawValue;
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

		// Token: 0x06007846 RID: 30790 RVA: 0x0018D4B4 File Offset: 0x0018B6B4
		internal ExchangeTopologyPerformanceCountersInstance(string instanceName) : base(instanceName, "MSExchange Topology")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.LastExchangeTopologyDiscoveryTimeSeconds = new ExPerformanceCounter(base.CategoryName, "Latest Exchange Topology Discovery Time in Seconds", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.LastExchangeTopologyDiscoveryTimeSeconds);
				this.ExchangeTopologyDiscoveriesPerformed = new ExPerformanceCounter(base.CategoryName, "Number of Exchange Topology Discoveries", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ExchangeTopologyDiscoveriesPerformed);
				this.SitelessServers = new ExPerformanceCounter(base.CategoryName, "Number of Siteless Servers", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.SitelessServers);
				long num = this.LastExchangeTopologyDiscoveryTimeSeconds.RawValue;
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

		// Token: 0x06007847 RID: 30791 RVA: 0x0018D5C8 File Offset: 0x0018B7C8
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

		// Token: 0x04004F09 RID: 20233
		public readonly ExPerformanceCounter LastExchangeTopologyDiscoveryTimeSeconds;

		// Token: 0x04004F0A RID: 20234
		public readonly ExPerformanceCounter ExchangeTopologyDiscoveriesPerformed;

		// Token: 0x04004F0B RID: 20235
		public readonly ExPerformanceCounter SitelessServers;
	}
}
