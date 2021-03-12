using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x0200056C RID: 1388
	internal sealed class LatencyTrackerEndToEndPerfCountersInstance : PerformanceCounterInstance
	{
		// Token: 0x06003F86 RID: 16262 RVA: 0x00113DCC File Offset: 0x00111FCC
		internal LatencyTrackerEndToEndPerfCountersInstance(string instanceName, LatencyTrackerEndToEndPerfCountersInstance autoUpdateTotalInstance) : base(instanceName, LatencyTrackerEndToEndPerfCounters.CategoryName)
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.Percentile50 = new ExPerformanceCounter(base.CategoryName, "Percentile50", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.Percentile50);
				this.Percentile80 = new ExPerformanceCounter(base.CategoryName, "Percentile80", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.Percentile80);
				this.Percentile90 = new ExPerformanceCounter(base.CategoryName, "Percentile90", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.Percentile90);
				this.Percentile95 = new ExPerformanceCounter(base.CategoryName, "Percentile95", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.Percentile95);
				this.Percentile99 = new ExPerformanceCounter(base.CategoryName, "Percentile99", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.Percentile99);
				this.Percentile50Samples = new ExPerformanceCounter(base.CategoryName, "Percentile50Samples", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.Percentile50Samples);
				this.Percentile80Samples = new ExPerformanceCounter(base.CategoryName, "Percentile80Samples", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.Percentile80Samples);
				this.Percentile90Samples = new ExPerformanceCounter(base.CategoryName, "Percentile90Samples", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.Percentile90Samples);
				this.Percentile95Samples = new ExPerformanceCounter(base.CategoryName, "Percentile95Samples", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.Percentile95Samples);
				this.Percentile99Samples = new ExPerformanceCounter(base.CategoryName, "Percentile99Samples", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.Percentile99Samples);
				long num = this.Percentile50.RawValue;
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

		// Token: 0x06003F87 RID: 16263 RVA: 0x0011401C File Offset: 0x0011221C
		internal LatencyTrackerEndToEndPerfCountersInstance(string instanceName) : base(instanceName, LatencyTrackerEndToEndPerfCounters.CategoryName)
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.Percentile50 = new ExPerformanceCounter(base.CategoryName, "Percentile50", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.Percentile50);
				this.Percentile80 = new ExPerformanceCounter(base.CategoryName, "Percentile80", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.Percentile80);
				this.Percentile90 = new ExPerformanceCounter(base.CategoryName, "Percentile90", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.Percentile90);
				this.Percentile95 = new ExPerformanceCounter(base.CategoryName, "Percentile95", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.Percentile95);
				this.Percentile99 = new ExPerformanceCounter(base.CategoryName, "Percentile99", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.Percentile99);
				this.Percentile50Samples = new ExPerformanceCounter(base.CategoryName, "Percentile50Samples", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.Percentile50Samples);
				this.Percentile80Samples = new ExPerformanceCounter(base.CategoryName, "Percentile80Samples", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.Percentile80Samples);
				this.Percentile90Samples = new ExPerformanceCounter(base.CategoryName, "Percentile90Samples", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.Percentile90Samples);
				this.Percentile95Samples = new ExPerformanceCounter(base.CategoryName, "Percentile95Samples", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.Percentile95Samples);
				this.Percentile99Samples = new ExPerformanceCounter(base.CategoryName, "Percentile99Samples", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.Percentile99Samples);
				long num = this.Percentile50.RawValue;
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

		// Token: 0x06003F88 RID: 16264 RVA: 0x0011426C File Offset: 0x0011246C
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

		// Token: 0x040023B5 RID: 9141
		public readonly ExPerformanceCounter Percentile50;

		// Token: 0x040023B6 RID: 9142
		public readonly ExPerformanceCounter Percentile80;

		// Token: 0x040023B7 RID: 9143
		public readonly ExPerformanceCounter Percentile90;

		// Token: 0x040023B8 RID: 9144
		public readonly ExPerformanceCounter Percentile95;

		// Token: 0x040023B9 RID: 9145
		public readonly ExPerformanceCounter Percentile99;

		// Token: 0x040023BA RID: 9146
		public readonly ExPerformanceCounter Percentile50Samples;

		// Token: 0x040023BB RID: 9147
		public readonly ExPerformanceCounter Percentile80Samples;

		// Token: 0x040023BC RID: 9148
		public readonly ExPerformanceCounter Percentile90Samples;

		// Token: 0x040023BD RID: 9149
		public readonly ExPerformanceCounter Percentile95Samples;

		// Token: 0x040023BE RID: 9150
		public readonly ExPerformanceCounter Percentile99Samples;
	}
}
