using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000578 RID: 1400
	internal sealed class ResourceThrottlingPerfCountersInstance : PerformanceCounterInstance
	{
		// Token: 0x06003FDC RID: 16348 RVA: 0x00116170 File Offset: 0x00114370
		internal ResourceThrottlingPerfCountersInstance(string instanceName, ResourceThrottlingPerfCountersInstance autoUpdateTotalInstance) : base(instanceName, "MSExchangeTransport ResourceThrottling")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.BackPressureTime = new ExPerformanceCounter(base.CategoryName, "Backpressure: Current Sustained Time in seconds", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.BackPressureTime);
				this.ResourceMeterLongestCallDuration = new ExPerformanceCounter(base.CategoryName, "Backpressure: Longest Resource Meter call in milliseconds", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ResourceMeterLongestCallDuration);
				this.ResourceObserverLongestCallDuration = new ExPerformanceCounter(base.CategoryName, "Backpressure: Longest Resource Observer call in milliseconds", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ResourceObserverLongestCallDuration);
				long num = this.BackPressureTime.RawValue;
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

		// Token: 0x06003FDD RID: 16349 RVA: 0x00116284 File Offset: 0x00114484
		internal ResourceThrottlingPerfCountersInstance(string instanceName) : base(instanceName, "MSExchangeTransport ResourceThrottling")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.BackPressureTime = new ExPerformanceCounter(base.CategoryName, "Backpressure: Current Sustained Time in seconds", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.BackPressureTime);
				this.ResourceMeterLongestCallDuration = new ExPerformanceCounter(base.CategoryName, "Backpressure: Longest Resource Meter call in milliseconds", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ResourceMeterLongestCallDuration);
				this.ResourceObserverLongestCallDuration = new ExPerformanceCounter(base.CategoryName, "Backpressure: Longest Resource Observer call in milliseconds", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ResourceObserverLongestCallDuration);
				long num = this.BackPressureTime.RawValue;
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

		// Token: 0x06003FDE RID: 16350 RVA: 0x00116398 File Offset: 0x00114598
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

		// Token: 0x040023E9 RID: 9193
		public readonly ExPerformanceCounter BackPressureTime;

		// Token: 0x040023EA RID: 9194
		public readonly ExPerformanceCounter ResourceMeterLongestCallDuration;

		// Token: 0x040023EB RID: 9195
		public readonly ExPerformanceCounter ResourceObserverLongestCallDuration;
	}
}
