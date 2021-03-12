using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x0200056A RID: 1386
	internal sealed class LatencyTrackerPerfCountersInstance : PerformanceCounterInstance
	{
		// Token: 0x06003F78 RID: 16248 RVA: 0x001137D0 File Offset: 0x001119D0
		internal LatencyTrackerPerfCountersInstance(string instanceName, LatencyTrackerPerfCountersInstance autoUpdateTotalInstance) : base(instanceName, LatencyTrackerPerfCounters.CategoryName)
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

		// Token: 0x06003F79 RID: 16249 RVA: 0x00113A20 File Offset: 0x00111C20
		internal LatencyTrackerPerfCountersInstance(string instanceName) : base(instanceName, LatencyTrackerPerfCounters.CategoryName)
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

		// Token: 0x06003F7A RID: 16250 RVA: 0x00113C70 File Offset: 0x00111E70
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

		// Token: 0x040023A9 RID: 9129
		public readonly ExPerformanceCounter Percentile50;

		// Token: 0x040023AA RID: 9130
		public readonly ExPerformanceCounter Percentile80;

		// Token: 0x040023AB RID: 9131
		public readonly ExPerformanceCounter Percentile90;

		// Token: 0x040023AC RID: 9132
		public readonly ExPerformanceCounter Percentile95;

		// Token: 0x040023AD RID: 9133
		public readonly ExPerformanceCounter Percentile99;

		// Token: 0x040023AE RID: 9134
		public readonly ExPerformanceCounter Percentile50Samples;

		// Token: 0x040023AF RID: 9135
		public readonly ExPerformanceCounter Percentile80Samples;

		// Token: 0x040023B0 RID: 9136
		public readonly ExPerformanceCounter Percentile90Samples;

		// Token: 0x040023B1 RID: 9137
		public readonly ExPerformanceCounter Percentile95Samples;

		// Token: 0x040023B2 RID: 9138
		public readonly ExPerformanceCounter Percentile99Samples;
	}
}
