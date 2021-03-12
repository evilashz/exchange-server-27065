using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x0200054D RID: 1357
	internal sealed class DsnGeneratorPerfCountersInstance : PerformanceCounterInstance
	{
		// Token: 0x06003EBA RID: 16058 RVA: 0x0010C6E4 File Offset: 0x0010A8E4
		internal DsnGeneratorPerfCountersInstance(string instanceName, DsnGeneratorPerfCountersInstance autoUpdateTotalInstance) : base(instanceName, DsnGeneratorPerfCounters.CategoryName)
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.FailedDsnTotal = new ExPerformanceCounter(base.CategoryName, "Failure DSNs Total", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.FailedDsnTotal, new ExPerformanceCounter[0]);
				list.Add(this.FailedDsnTotal);
				this.DelayedDsnTotal = new ExPerformanceCounter(base.CategoryName, "Delay DSNs", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.DelayedDsnTotal, new ExPerformanceCounter[0]);
				list.Add(this.DelayedDsnTotal);
				this.RelayedDsnTotal = new ExPerformanceCounter(base.CategoryName, "Relay DSNs", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.RelayedDsnTotal, new ExPerformanceCounter[0]);
				list.Add(this.RelayedDsnTotal);
				this.DeliveredDsnTotal = new ExPerformanceCounter(base.CategoryName, "Delivered DSNs", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.DeliveredDsnTotal, new ExPerformanceCounter[0]);
				list.Add(this.DeliveredDsnTotal);
				this.ExpandedDsnTotal = new ExPerformanceCounter(base.CategoryName, "Expanded DSNs", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.ExpandedDsnTotal, new ExPerformanceCounter[0]);
				list.Add(this.ExpandedDsnTotal);
				this.FailedDsnInLastHour = new ExPerformanceCounter(base.CategoryName, "Failure DSNs within the last hour", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.FailedDsnInLastHour, new ExPerformanceCounter[0]);
				list.Add(this.FailedDsnInLastHour);
				this.AlertableFailedDsnInLastHour = new ExPerformanceCounter(base.CategoryName, "Alertable failure DSNs within the last hour", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.AlertableFailedDsnInLastHour, new ExPerformanceCounter[0]);
				list.Add(this.AlertableFailedDsnInLastHour);
				this.DelayedDsnInLastHour = new ExPerformanceCounter(base.CategoryName, "Delayed DSNs within the last hour", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.DelayedDsnInLastHour, new ExPerformanceCounter[0]);
				list.Add(this.DelayedDsnInLastHour);
				this.CatchAllRecipientFailedDsnTotal = new ExPerformanceCounter(base.CategoryName, "NDRs generated for catch-all recipients.", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.CatchAllRecipientFailedDsnTotal, new ExPerformanceCounter[0]);
				list.Add(this.CatchAllRecipientFailedDsnTotal);
				long num = this.FailedDsnTotal.RawValue;
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

		// Token: 0x06003EBB RID: 16059 RVA: 0x0010C978 File Offset: 0x0010AB78
		internal DsnGeneratorPerfCountersInstance(string instanceName) : base(instanceName, DsnGeneratorPerfCounters.CategoryName)
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.FailedDsnTotal = new ExPerformanceCounter(base.CategoryName, "Failure DSNs Total", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.FailedDsnTotal);
				this.DelayedDsnTotal = new ExPerformanceCounter(base.CategoryName, "Delay DSNs", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.DelayedDsnTotal);
				this.RelayedDsnTotal = new ExPerformanceCounter(base.CategoryName, "Relay DSNs", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.RelayedDsnTotal);
				this.DeliveredDsnTotal = new ExPerformanceCounter(base.CategoryName, "Delivered DSNs", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.DeliveredDsnTotal);
				this.ExpandedDsnTotal = new ExPerformanceCounter(base.CategoryName, "Expanded DSNs", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ExpandedDsnTotal);
				this.FailedDsnInLastHour = new ExPerformanceCounter(base.CategoryName, "Failure DSNs within the last hour", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.FailedDsnInLastHour);
				this.AlertableFailedDsnInLastHour = new ExPerformanceCounter(base.CategoryName, "Alertable failure DSNs within the last hour", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.AlertableFailedDsnInLastHour);
				this.DelayedDsnInLastHour = new ExPerformanceCounter(base.CategoryName, "Delayed DSNs within the last hour", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.DelayedDsnInLastHour);
				this.CatchAllRecipientFailedDsnTotal = new ExPerformanceCounter(base.CategoryName, "NDRs generated for catch-all recipients.", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.CatchAllRecipientFailedDsnTotal);
				long num = this.FailedDsnTotal.RawValue;
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

		// Token: 0x06003EBC RID: 16060 RVA: 0x0010CBA8 File Offset: 0x0010ADA8
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

		// Token: 0x040022D6 RID: 8918
		public readonly ExPerformanceCounter FailedDsnTotal;

		// Token: 0x040022D7 RID: 8919
		public readonly ExPerformanceCounter DelayedDsnTotal;

		// Token: 0x040022D8 RID: 8920
		public readonly ExPerformanceCounter RelayedDsnTotal;

		// Token: 0x040022D9 RID: 8921
		public readonly ExPerformanceCounter DeliveredDsnTotal;

		// Token: 0x040022DA RID: 8922
		public readonly ExPerformanceCounter ExpandedDsnTotal;

		// Token: 0x040022DB RID: 8923
		public readonly ExPerformanceCounter FailedDsnInLastHour;

		// Token: 0x040022DC RID: 8924
		public readonly ExPerformanceCounter AlertableFailedDsnInLastHour;

		// Token: 0x040022DD RID: 8925
		public readonly ExPerformanceCounter DelayedDsnInLastHour;

		// Token: 0x040022DE RID: 8926
		public readonly ExPerformanceCounter CatchAllRecipientFailedDsnTotal;
	}
}
