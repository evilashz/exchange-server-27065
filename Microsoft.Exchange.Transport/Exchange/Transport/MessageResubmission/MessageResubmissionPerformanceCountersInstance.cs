using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.MessageResubmission
{
	// Token: 0x02000562 RID: 1378
	internal sealed class MessageResubmissionPerformanceCountersInstance : PerformanceCounterInstance
	{
		// Token: 0x06003F3F RID: 16191 RVA: 0x00112524 File Offset: 0x00110724
		internal MessageResubmissionPerformanceCountersInstance(string instanceName, MessageResubmissionPerformanceCountersInstance autoUpdateTotalInstance) : base(instanceName, "MSExchangeTransport Safety Net")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.SafetyNetResubmissionCount = new ExPerformanceCounter(base.CategoryName, "Safety Net Resubmission Count", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.SafetyNetResubmissionCount);
				this.ShadowSafetyNetResubmissionCount = new ExPerformanceCounter(base.CategoryName, "Shadow Safety Net Resubmission Count", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ShadowSafetyNetResubmissionCount);
				this.ResubmitLatencyAverageTime = new ExPerformanceCounter(base.CategoryName, "Resubmit Latency Average Time", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ResubmitLatencyAverageTime);
				this.ResubmitLatencyAverageTimeBase = new ExPerformanceCounter(base.CategoryName, "Resubmit Latency Average Time Base", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ResubmitLatencyAverageTimeBase);
				this.ResubmitRequestCount = new ExPerformanceCounter(base.CategoryName, "Resubmit Request Count", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ResubmitRequestCount);
				this.RecentResubmitRequestCount = new ExPerformanceCounter(base.CategoryName, "Safety Net Resubmit Request Count", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.RecentResubmitRequestCount);
				this.RecentShadowResubmitRequestCount = new ExPerformanceCounter(base.CategoryName, "Shadow Safety Net Resubmit Request Count", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.RecentShadowResubmitRequestCount);
				this.AverageResubmitRequestTimeSpan = new ExPerformanceCounter(base.CategoryName, "Average Safety Net Resubmit Request Time Span", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageResubmitRequestTimeSpan);
				long num = this.SafetyNetResubmissionCount.RawValue;
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

		// Token: 0x06003F40 RID: 16192 RVA: 0x00112728 File Offset: 0x00110928
		internal MessageResubmissionPerformanceCountersInstance(string instanceName) : base(instanceName, "MSExchangeTransport Safety Net")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.SafetyNetResubmissionCount = new ExPerformanceCounter(base.CategoryName, "Safety Net Resubmission Count", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.SafetyNetResubmissionCount);
				this.ShadowSafetyNetResubmissionCount = new ExPerformanceCounter(base.CategoryName, "Shadow Safety Net Resubmission Count", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ShadowSafetyNetResubmissionCount);
				this.ResubmitLatencyAverageTime = new ExPerformanceCounter(base.CategoryName, "Resubmit Latency Average Time", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ResubmitLatencyAverageTime);
				this.ResubmitLatencyAverageTimeBase = new ExPerformanceCounter(base.CategoryName, "Resubmit Latency Average Time Base", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ResubmitLatencyAverageTimeBase);
				this.ResubmitRequestCount = new ExPerformanceCounter(base.CategoryName, "Resubmit Request Count", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ResubmitRequestCount);
				this.RecentResubmitRequestCount = new ExPerformanceCounter(base.CategoryName, "Safety Net Resubmit Request Count", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.RecentResubmitRequestCount);
				this.RecentShadowResubmitRequestCount = new ExPerformanceCounter(base.CategoryName, "Shadow Safety Net Resubmit Request Count", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.RecentShadowResubmitRequestCount);
				this.AverageResubmitRequestTimeSpan = new ExPerformanceCounter(base.CategoryName, "Average Safety Net Resubmit Request Time Span", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageResubmitRequestTimeSpan);
				long num = this.SafetyNetResubmissionCount.RawValue;
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

		// Token: 0x06003F41 RID: 16193 RVA: 0x0011292C File Offset: 0x00110B2C
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

		// Token: 0x0400238B RID: 9099
		public readonly ExPerformanceCounter SafetyNetResubmissionCount;

		// Token: 0x0400238C RID: 9100
		public readonly ExPerformanceCounter ShadowSafetyNetResubmissionCount;

		// Token: 0x0400238D RID: 9101
		public readonly ExPerformanceCounter ResubmitLatencyAverageTime;

		// Token: 0x0400238E RID: 9102
		public readonly ExPerformanceCounter ResubmitLatencyAverageTimeBase;

		// Token: 0x0400238F RID: 9103
		public readonly ExPerformanceCounter ResubmitRequestCount;

		// Token: 0x04002390 RID: 9104
		public readonly ExPerformanceCounter RecentResubmitRequestCount;

		// Token: 0x04002391 RID: 9105
		public readonly ExPerformanceCounter RecentShadowResubmitRequestCount;

		// Token: 0x04002392 RID: 9106
		public readonly ExPerformanceCounter AverageResubmitRequestTimeSpan;
	}
}
