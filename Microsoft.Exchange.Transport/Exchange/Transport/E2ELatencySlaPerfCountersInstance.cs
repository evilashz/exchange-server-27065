using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000568 RID: 1384
	internal sealed class E2ELatencySlaPerfCountersInstance : PerformanceCounterInstance
	{
		// Token: 0x06003F6A RID: 16234 RVA: 0x001134A4 File Offset: 0x001116A4
		internal E2ELatencySlaPerfCountersInstance(string instanceName, E2ELatencySlaPerfCountersInstance autoUpdateTotalInstance) : base(instanceName, "MSExchangeTransport E2E Latency SLA")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.DeliverPercentMeetingSla = new ExPerformanceCounter(base.CategoryName, "Deliver Percent Meeting SLA", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.DeliverPercentMeetingSla);
				this.SendToExternalPercentMeetingSla = new ExPerformanceCounter(base.CategoryName, "Send To External Percent Meeting SLA", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.SendToExternalPercentMeetingSla);
				long num = this.DeliverPercentMeetingSla.RawValue;
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

		// Token: 0x06003F6B RID: 16235 RVA: 0x0011358C File Offset: 0x0011178C
		internal E2ELatencySlaPerfCountersInstance(string instanceName) : base(instanceName, "MSExchangeTransport E2E Latency SLA")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.DeliverPercentMeetingSla = new ExPerformanceCounter(base.CategoryName, "Deliver Percent Meeting SLA", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.DeliverPercentMeetingSla);
				this.SendToExternalPercentMeetingSla = new ExPerformanceCounter(base.CategoryName, "Send To External Percent Meeting SLA", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.SendToExternalPercentMeetingSla);
				long num = this.DeliverPercentMeetingSla.RawValue;
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

		// Token: 0x06003F6C RID: 16236 RVA: 0x00113674 File Offset: 0x00111874
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

		// Token: 0x040023A5 RID: 9125
		public readonly ExPerformanceCounter DeliverPercentMeetingSla;

		// Token: 0x040023A6 RID: 9126
		public readonly ExPerformanceCounter SendToExternalPercentMeetingSla;
	}
}
