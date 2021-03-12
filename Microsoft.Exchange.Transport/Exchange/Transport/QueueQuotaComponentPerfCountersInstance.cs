using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x0200054B RID: 1355
	internal sealed class QueueQuotaComponentPerfCountersInstance : PerformanceCounterInstance
	{
		// Token: 0x06003EAB RID: 16043 RVA: 0x0010C2CC File Offset: 0x0010A4CC
		internal QueueQuotaComponentPerfCountersInstance(string instanceName, QueueQuotaComponentPerfCountersInstance autoUpdateTotalInstance) : base(instanceName, "MSExchangeTransport Queue Quota Component")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.EntitiesInThrottledState = new ExPerformanceCounter(base.CategoryName, "Entities in throttled state", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.EntitiesInThrottledState, new ExPerformanceCounter[0]);
				list.Add(this.EntitiesInThrottledState);
				this.MessagesTempRejectedTotal = new ExPerformanceCounter(base.CategoryName, "Messages temp rejected total", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MessagesTempRejectedTotal, new ExPerformanceCounter[0]);
				list.Add(this.MessagesTempRejectedTotal);
				this.MessagesTempRejectedRecently = new ExPerformanceCounter(base.CategoryName, "Messages temp rejected recently", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MessagesTempRejectedRecently, new ExPerformanceCounter[0]);
				list.Add(this.MessagesTempRejectedRecently);
				this.OldestThrottledEntityIntervalInSeconds = new ExPerformanceCounter(base.CategoryName, "Oldest Throttled Entity Interval In Seconds", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.OldestThrottledEntityIntervalInSeconds, new ExPerformanceCounter[0]);
				list.Add(this.OldestThrottledEntityIntervalInSeconds);
				long num = this.EntitiesInThrottledState.RawValue;
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

		// Token: 0x06003EAC RID: 16044 RVA: 0x0010C438 File Offset: 0x0010A638
		internal QueueQuotaComponentPerfCountersInstance(string instanceName) : base(instanceName, "MSExchangeTransport Queue Quota Component")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.EntitiesInThrottledState = new ExPerformanceCounter(base.CategoryName, "Entities in throttled state", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.EntitiesInThrottledState);
				this.MessagesTempRejectedTotal = new ExPerformanceCounter(base.CategoryName, "Messages temp rejected total", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MessagesTempRejectedTotal);
				this.MessagesTempRejectedRecently = new ExPerformanceCounter(base.CategoryName, "Messages temp rejected recently", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MessagesTempRejectedRecently);
				this.OldestThrottledEntityIntervalInSeconds = new ExPerformanceCounter(base.CategoryName, "Oldest Throttled Entity Interval In Seconds", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.OldestThrottledEntityIntervalInSeconds);
				long num = this.EntitiesInThrottledState.RawValue;
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

		// Token: 0x06003EAD RID: 16045 RVA: 0x0010C578 File Offset: 0x0010A778
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

		// Token: 0x040022D0 RID: 8912
		public readonly ExPerformanceCounter EntitiesInThrottledState;

		// Token: 0x040022D1 RID: 8913
		public readonly ExPerformanceCounter MessagesTempRejectedTotal;

		// Token: 0x040022D2 RID: 8914
		public readonly ExPerformanceCounter MessagesTempRejectedRecently;

		// Token: 0x040022D3 RID: 8915
		public readonly ExPerformanceCounter OldestThrottledEntityIntervalInSeconds;
	}
}
