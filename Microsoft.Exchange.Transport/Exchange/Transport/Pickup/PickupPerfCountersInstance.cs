using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Pickup
{
	// Token: 0x02000552 RID: 1362
	internal sealed class PickupPerfCountersInstance : PerformanceCounterInstance
	{
		// Token: 0x06003ED8 RID: 16088 RVA: 0x0010D3A4 File Offset: 0x0010B5A4
		internal PickupPerfCountersInstance(string instanceName, PickupPerfCountersInstance autoUpdateTotalInstance) : base(instanceName, "MSExchangeTransport Pickup")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.Submitted = new ExPerformanceCounter(base.CategoryName, "Messages Submitted", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.Submitted);
				this.NDRed = new ExPerformanceCounter(base.CategoryName, "Messages NDRed", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.NDRed);
				this.Badmailed = new ExPerformanceCounter(base.CategoryName, "Messages Badmailed", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.Badmailed);
				long num = this.Submitted.RawValue;
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

		// Token: 0x06003ED9 RID: 16089 RVA: 0x0010D4B8 File Offset: 0x0010B6B8
		internal PickupPerfCountersInstance(string instanceName) : base(instanceName, "MSExchangeTransport Pickup")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.Submitted = new ExPerformanceCounter(base.CategoryName, "Messages Submitted", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.Submitted);
				this.NDRed = new ExPerformanceCounter(base.CategoryName, "Messages NDRed", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.NDRed);
				this.Badmailed = new ExPerformanceCounter(base.CategoryName, "Messages Badmailed", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.Badmailed);
				long num = this.Submitted.RawValue;
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

		// Token: 0x06003EDA RID: 16090 RVA: 0x0010D5CC File Offset: 0x0010B7CC
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

		// Token: 0x040022F7 RID: 8951
		public readonly ExPerformanceCounter Submitted;

		// Token: 0x040022F8 RID: 8952
		public readonly ExPerformanceCounter NDRed;

		// Token: 0x040022F9 RID: 8953
		public readonly ExPerformanceCounter Badmailed;
	}
}
