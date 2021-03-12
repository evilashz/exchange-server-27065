using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.HttpProxy.Common
{
	// Token: 0x02000025 RID: 37
	internal sealed class HttpProxyPerArrayCountersInstance : PerformanceCounterInstance
	{
		// Token: 0x060000D1 RID: 209 RVA: 0x00007400 File Offset: 0x00005600
		internal HttpProxyPerArrayCountersInstance(string instanceName, HttpProxyPerArrayCountersInstance autoUpdateTotalInstance) : base(instanceName, "MSExchange HttpProxy Per Array")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.TotalServersInArray = new ExPerformanceCounter(base.CategoryName, "Total Servers In Array", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.TotalServersInArray);
				long num = this.TotalServersInArray.RawValue;
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

		// Token: 0x060000D2 RID: 210 RVA: 0x000074C0 File Offset: 0x000056C0
		internal HttpProxyPerArrayCountersInstance(string instanceName) : base(instanceName, "MSExchange HttpProxy Per Array")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.TotalServersInArray = new ExPerformanceCounter(base.CategoryName, "Total Servers In Array", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.TotalServersInArray);
				long num = this.TotalServersInArray.RawValue;
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

		// Token: 0x060000D3 RID: 211 RVA: 0x00007580 File Offset: 0x00005780
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

		// Token: 0x04000139 RID: 313
		public readonly ExPerformanceCounter TotalServersInArray;
	}
}
