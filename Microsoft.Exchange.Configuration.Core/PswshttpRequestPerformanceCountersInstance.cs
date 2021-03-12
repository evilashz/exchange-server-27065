using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Configuration.Core
{
	// Token: 0x02000039 RID: 57
	internal sealed class PswshttpRequestPerformanceCountersInstance : PerformanceCounterInstance
	{
		// Token: 0x06000147 RID: 327 RVA: 0x00007F68 File Offset: 0x00006168
		internal PswshttpRequestPerformanceCountersInstance(string instanceName, PswshttpRequestPerformanceCountersInstance autoUpdateTotalInstance) : base(instanceName, "MSExchangePowershellWebServiceHttpRequest")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.AverageResponseTime = new ExPerformanceCounter(base.CategoryName, "PowerShell Web Service Average Response Time", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.AverageResponseTime, new ExPerformanceCounter[0]);
				list.Add(this.AverageResponseTime);
				long num = this.AverageResponseTime.RawValue;
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

		// Token: 0x06000148 RID: 328 RVA: 0x00008030 File Offset: 0x00006230
		internal PswshttpRequestPerformanceCountersInstance(string instanceName) : base(instanceName, "MSExchangePowershellWebServiceHttpRequest")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.AverageResponseTime = new ExPerformanceCounter(base.CategoryName, "PowerShell Web Service Average Response Time", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageResponseTime);
				long num = this.AverageResponseTime.RawValue;
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

		// Token: 0x06000149 RID: 329 RVA: 0x000080F0 File Offset: 0x000062F0
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

		// Token: 0x040000DD RID: 221
		public readonly ExPerformanceCounter AverageResponseTime;
	}
}
