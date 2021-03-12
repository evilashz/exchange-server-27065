using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;

namespace Microsoft.Exchange.Diagnostics.WorkloadManagement
{
	// Token: 0x02000207 RID: 519
	internal sealed class MSExchangeActivityContextInstance : PerformanceCounterInstance
	{
		// Token: 0x06000F3B RID: 3899 RVA: 0x0003DD1C File Offset: 0x0003BF1C
		internal MSExchangeActivityContextInstance(string instanceName, MSExchangeActivityContextInstance autoUpdateTotalInstance) : base(instanceName, "MSExchange Activity Context Resources")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.TimeInResourcePerSecond = new ExPerformanceCounter(base.CategoryName, "Time in Resource per second", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.TimeInResourcePerSecond);
				long num = this.TimeInResourcePerSecond.RawValue;
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

		// Token: 0x06000F3C RID: 3900 RVA: 0x0003DDDC File Offset: 0x0003BFDC
		internal MSExchangeActivityContextInstance(string instanceName) : base(instanceName, "MSExchange Activity Context Resources")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.TimeInResourcePerSecond = new ExPerformanceCounter(base.CategoryName, "Time in Resource per second", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.TimeInResourcePerSecond);
				long num = this.TimeInResourcePerSecond.RawValue;
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

		// Token: 0x06000F3D RID: 3901 RVA: 0x0003DE9C File Offset: 0x0003C09C
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

		// Token: 0x04000AC7 RID: 2759
		public readonly ExPerformanceCounter TimeInResourcePerSecond;
	}
}
