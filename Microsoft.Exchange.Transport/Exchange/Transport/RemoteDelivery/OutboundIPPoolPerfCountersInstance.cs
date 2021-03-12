using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.RemoteDelivery
{
	// Token: 0x02000550 RID: 1360
	internal sealed class OutboundIPPoolPerfCountersInstance : PerformanceCounterInstance
	{
		// Token: 0x06003ECA RID: 16074 RVA: 0x0010CFD4 File Offset: 0x0010B1D4
		internal OutboundIPPoolPerfCountersInstance(string instanceName, OutboundIPPoolPerfCountersInstance autoUpdateTotalInstance) : base(instanceName, "MSExchangeTransport Outbound IP Pools")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.NormalRisk = new ExPerformanceCounter(base.CategoryName, "Percentage of Normal Risk queues with failures", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.NormalRisk);
				this.BulkRisk = new ExPerformanceCounter(base.CategoryName, "Percentage of Bulk Risk queues with failures", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.BulkRisk);
				this.HighRisk = new ExPerformanceCounter(base.CategoryName, "Percentage of High Risk queues with failures", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.HighRisk);
				this.LowRisk = new ExPerformanceCounter(base.CategoryName, "Percentage of Low Risk queues with failures", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.LowRisk);
				long num = this.NormalRisk.RawValue;
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

		// Token: 0x06003ECB RID: 16075 RVA: 0x0010D114 File Offset: 0x0010B314
		internal OutboundIPPoolPerfCountersInstance(string instanceName) : base(instanceName, "MSExchangeTransport Outbound IP Pools")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.NormalRisk = new ExPerformanceCounter(base.CategoryName, "Percentage of Normal Risk queues with failures", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.NormalRisk);
				this.BulkRisk = new ExPerformanceCounter(base.CategoryName, "Percentage of Bulk Risk queues with failures", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.BulkRisk);
				this.HighRisk = new ExPerformanceCounter(base.CategoryName, "Percentage of High Risk queues with failures", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.HighRisk);
				this.LowRisk = new ExPerformanceCounter(base.CategoryName, "Percentage of Low Risk queues with failures", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.LowRisk);
				long num = this.NormalRisk.RawValue;
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

		// Token: 0x06003ECC RID: 16076 RVA: 0x0010D254 File Offset: 0x0010B454
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

		// Token: 0x040022F1 RID: 8945
		public readonly ExPerformanceCounter NormalRisk;

		// Token: 0x040022F2 RID: 8946
		public readonly ExPerformanceCounter BulkRisk;

		// Token: 0x040022F3 RID: 8947
		public readonly ExPerformanceCounter HighRisk;

		// Token: 0x040022F4 RID: 8948
		public readonly ExPerformanceCounter LowRisk;
	}
}
