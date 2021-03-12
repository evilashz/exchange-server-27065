using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.UM.ClientAccess
{
	// Token: 0x02000234 RID: 564
	internal sealed class UMClientAccessCountersInstance : PerformanceCounterInstance
	{
		// Token: 0x060011B4 RID: 4532 RVA: 0x0003C478 File Offset: 0x0003A678
		internal UMClientAccessCountersInstance(string instanceName, UMClientAccessCountersInstance autoUpdateTotalInstance) : base(instanceName, "MSExchangeUMClientAccess")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.TotalPlayOnPhoneRequests = new ExPerformanceCounter(base.CategoryName, "Total Number of Play on Phone Requests", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.TotalPlayOnPhoneRequests);
				this.TotalPlayOnPhoneErrors = new ExPerformanceCounter(base.CategoryName, "Total Number of Failed Play on Phone Requests", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.TotalPlayOnPhoneErrors);
				this.TotalPINResetRequests = new ExPerformanceCounter(base.CategoryName, "Total Number of PIN Reset Requests", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.TotalPINResetRequests);
				this.TotalPINResetErrors = new ExPerformanceCounter(base.CategoryName, "Total Number of Failed PIN Reset Requests", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.TotalPINResetErrors);
				this.PID = new ExPerformanceCounter(base.CategoryName, "PID", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.PID);
				long num = this.TotalPlayOnPhoneRequests.RawValue;
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

		// Token: 0x060011B5 RID: 4533 RVA: 0x0003C5E0 File Offset: 0x0003A7E0
		internal UMClientAccessCountersInstance(string instanceName) : base(instanceName, "MSExchangeUMClientAccess")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.TotalPlayOnPhoneRequests = new ExPerformanceCounter(base.CategoryName, "Total Number of Play on Phone Requests", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.TotalPlayOnPhoneRequests);
				this.TotalPlayOnPhoneErrors = new ExPerformanceCounter(base.CategoryName, "Total Number of Failed Play on Phone Requests", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.TotalPlayOnPhoneErrors);
				this.TotalPINResetRequests = new ExPerformanceCounter(base.CategoryName, "Total Number of PIN Reset Requests", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.TotalPINResetRequests);
				this.TotalPINResetErrors = new ExPerformanceCounter(base.CategoryName, "Total Number of Failed PIN Reset Requests", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.TotalPINResetErrors);
				this.PID = new ExPerformanceCounter(base.CategoryName, "PID", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.PID);
				long num = this.TotalPlayOnPhoneRequests.RawValue;
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

		// Token: 0x060011B6 RID: 4534 RVA: 0x0003C748 File Offset: 0x0003A948
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

		// Token: 0x04000B6C RID: 2924
		public readonly ExPerformanceCounter TotalPlayOnPhoneRequests;

		// Token: 0x04000B6D RID: 2925
		public readonly ExPerformanceCounter TotalPlayOnPhoneErrors;

		// Token: 0x04000B6E RID: 2926
		public readonly ExPerformanceCounter TotalPINResetRequests;

		// Token: 0x04000B6F RID: 2927
		public readonly ExPerformanceCounter TotalPINResetErrors;

		// Token: 0x04000B70 RID: 2928
		public readonly ExPerformanceCounter PID;
	}
}
