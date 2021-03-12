using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000A50 RID: 2640
	internal sealed class MSExchangeResourceLoadInstance : PerformanceCounterInstance
	{
		// Token: 0x060078A0 RID: 30880 RVA: 0x0018FC60 File Offset: 0x0018DE60
		internal MSExchangeResourceLoadInstance(string instanceName, MSExchangeResourceLoadInstance autoUpdateTotalInstance) : base(instanceName, "MSExchange Resource Load")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.ResourceMetric = new ExPerformanceCounter(base.CategoryName, "Resource Metric", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.ResourceMetric);
				this.ResourceLoad = new ExPerformanceCounter(base.CategoryName, "Resource Load", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.ResourceLoad);
				long num = this.ResourceMetric.RawValue;
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

		// Token: 0x060078A1 RID: 30881 RVA: 0x0018FD48 File Offset: 0x0018DF48
		internal MSExchangeResourceLoadInstance(string instanceName) : base(instanceName, "MSExchange Resource Load")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.ResourceMetric = new ExPerformanceCounter(base.CategoryName, "Resource Metric", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.ResourceMetric);
				this.ResourceLoad = new ExPerformanceCounter(base.CategoryName, "Resource Load", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.ResourceLoad);
				long num = this.ResourceMetric.RawValue;
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

		// Token: 0x060078A2 RID: 30882 RVA: 0x0018FE30 File Offset: 0x0018E030
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

		// Token: 0x04004F70 RID: 20336
		public readonly ExPerformanceCounter ResourceMetric;

		// Token: 0x04004F71 RID: 20337
		public readonly ExPerformanceCounter ResourceLoad;
	}
}
