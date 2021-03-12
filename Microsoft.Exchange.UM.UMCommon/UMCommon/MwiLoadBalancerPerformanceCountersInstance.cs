using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x02000236 RID: 566
	internal sealed class MwiLoadBalancerPerformanceCountersInstance : PerformanceCounterInstance
	{
		// Token: 0x060011C2 RID: 4546 RVA: 0x0003C898 File Offset: 0x0003AA98
		internal MwiLoadBalancerPerformanceCountersInstance(string instanceName, MwiLoadBalancerPerformanceCountersInstance autoUpdateTotalInstance) : base(instanceName, "MSExchangeUMMessageWaitingIndicator")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.TotalMwiMessages = new ExPerformanceCounter(base.CategoryName, "Total MWI Messages", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.TotalMwiMessages);
				this.TotalFailedMwiMessages = new ExPerformanceCounter(base.CategoryName, "Total Failed MWI Messages", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.TotalFailedMwiMessages);
				this.AverageMwiProcessingTime = new ExPerformanceCounter(base.CategoryName, "Average MWI Processing Time", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageMwiProcessingTime);
				long num = this.TotalMwiMessages.RawValue;
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

		// Token: 0x060011C3 RID: 4547 RVA: 0x0003C9AC File Offset: 0x0003ABAC
		internal MwiLoadBalancerPerformanceCountersInstance(string instanceName) : base(instanceName, "MSExchangeUMMessageWaitingIndicator")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.TotalMwiMessages = new ExPerformanceCounter(base.CategoryName, "Total MWI Messages", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.TotalMwiMessages);
				this.TotalFailedMwiMessages = new ExPerformanceCounter(base.CategoryName, "Total Failed MWI Messages", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.TotalFailedMwiMessages);
				this.AverageMwiProcessingTime = new ExPerformanceCounter(base.CategoryName, "Average MWI Processing Time", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageMwiProcessingTime);
				long num = this.TotalMwiMessages.RawValue;
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

		// Token: 0x060011C4 RID: 4548 RVA: 0x0003CAC0 File Offset: 0x0003ACC0
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

		// Token: 0x04000B73 RID: 2931
		public readonly ExPerformanceCounter TotalMwiMessages;

		// Token: 0x04000B74 RID: 2932
		public readonly ExPerformanceCounter TotalFailedMwiMessages;

		// Token: 0x04000B75 RID: 2933
		public readonly ExPerformanceCounter AverageMwiProcessingTime;
	}
}
