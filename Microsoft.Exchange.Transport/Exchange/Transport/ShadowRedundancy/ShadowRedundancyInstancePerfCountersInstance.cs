using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.ShadowRedundancy
{
	// Token: 0x02000564 RID: 1380
	internal sealed class ShadowRedundancyInstancePerfCountersInstance : PerformanceCounterInstance
	{
		// Token: 0x06003F4E RID: 16206 RVA: 0x00112A98 File Offset: 0x00110C98
		internal ShadowRedundancyInstancePerfCountersInstance(string instanceName, ShadowRedundancyInstancePerfCountersInstance autoUpdateTotalInstance) : base(instanceName, "MSExchangeTransport Shadow Redundancy Host Info")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.ShadowQueueLength = new ExPerformanceCounter(base.CategoryName, "Shadow Queue Length", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.ShadowQueueLength, new ExPerformanceCounter[0]);
				list.Add(this.ShadowQueueLength);
				this.ShadowHeartbeatLatencyAverageTime = new ExPerformanceCounter(base.CategoryName, "Shadow Heartbeat Latency Average Time", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.ShadowHeartbeatLatencyAverageTime, new ExPerformanceCounter[0]);
				list.Add(this.ShadowHeartbeatLatencyAverageTime);
				this.ShadowHeartbeatLatencyAverageTimeBase = new ExPerformanceCounter(base.CategoryName, "Shadow Heartbeat Latency Average Time Base", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.ShadowHeartbeatLatencyAverageTimeBase, new ExPerformanceCounter[0]);
				list.Add(this.ShadowHeartbeatLatencyAverageTimeBase);
				this.ShadowFailureCount = new ExPerformanceCounter(base.CategoryName, "Shadow Failure Count", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.ShadowFailureCount, new ExPerformanceCounter[0]);
				list.Add(this.ShadowFailureCount);
				this.ResubmittedMessageCount = new ExPerformanceCounter(base.CategoryName, "Resubmitted Message Count", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.ResubmittedMessageCount, new ExPerformanceCounter[0]);
				list.Add(this.ResubmittedMessageCount);
				this.HeartbeatFailureCount = new ExPerformanceCounter(base.CategoryName, "Shadow Heartbeat Failure Count", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.HeartbeatFailureCount, new ExPerformanceCounter[0]);
				list.Add(this.HeartbeatFailureCount);
				this.ShadowedMessageCount = new ExPerformanceCounter(base.CategoryName, "Shadowed Message Count", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.ShadowedMessageCount, new ExPerformanceCounter[0]);
				list.Add(this.ShadowedMessageCount);
				long num = this.ShadowQueueLength.RawValue;
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

		// Token: 0x06003F4F RID: 16207 RVA: 0x00112CC0 File Offset: 0x00110EC0
		internal ShadowRedundancyInstancePerfCountersInstance(string instanceName) : base(instanceName, "MSExchangeTransport Shadow Redundancy Host Info")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.ShadowQueueLength = new ExPerformanceCounter(base.CategoryName, "Shadow Queue Length", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ShadowQueueLength);
				this.ShadowHeartbeatLatencyAverageTime = new ExPerformanceCounter(base.CategoryName, "Shadow Heartbeat Latency Average Time", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ShadowHeartbeatLatencyAverageTime);
				this.ShadowHeartbeatLatencyAverageTimeBase = new ExPerformanceCounter(base.CategoryName, "Shadow Heartbeat Latency Average Time Base", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ShadowHeartbeatLatencyAverageTimeBase);
				this.ShadowFailureCount = new ExPerformanceCounter(base.CategoryName, "Shadow Failure Count", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ShadowFailureCount);
				this.ResubmittedMessageCount = new ExPerformanceCounter(base.CategoryName, "Resubmitted Message Count", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ResubmittedMessageCount);
				this.HeartbeatFailureCount = new ExPerformanceCounter(base.CategoryName, "Shadow Heartbeat Failure Count", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.HeartbeatFailureCount);
				this.ShadowedMessageCount = new ExPerformanceCounter(base.CategoryName, "Shadowed Message Count", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ShadowedMessageCount);
				long num = this.ShadowQueueLength.RawValue;
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

		// Token: 0x06003F50 RID: 16208 RVA: 0x00112E98 File Offset: 0x00111098
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

		// Token: 0x04002395 RID: 9109
		public readonly ExPerformanceCounter ShadowQueueLength;

		// Token: 0x04002396 RID: 9110
		public readonly ExPerformanceCounter ShadowHeartbeatLatencyAverageTime;

		// Token: 0x04002397 RID: 9111
		public readonly ExPerformanceCounter ShadowHeartbeatLatencyAverageTimeBase;

		// Token: 0x04002398 RID: 9112
		public readonly ExPerformanceCounter ShadowFailureCount;

		// Token: 0x04002399 RID: 9113
		public readonly ExPerformanceCounter ResubmittedMessageCount;

		// Token: 0x0400239A RID: 9114
		public readonly ExPerformanceCounter HeartbeatFailureCount;

		// Token: 0x0400239B RID: 9115
		public readonly ExPerformanceCounter ShadowedMessageCount;
	}
}
