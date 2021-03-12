using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Transport.Internal.MExRuntime
{
	// Token: 0x02000071 RID: 113
	internal sealed class MExCountersInstance : PerformanceCounterInstance
	{
		// Token: 0x0600036E RID: 878 RVA: 0x0001173C File Offset: 0x0000F93C
		internal MExCountersInstance(string instanceName, MExCountersInstance autoUpdateTotalInstance) : base(instanceName, MExCounters.CategoryName)
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.AverageAgentDelay = new ExPerformanceCounter(base.CategoryName, "Average Agent Processing Time (sec)", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageAgentDelay);
				this.AverageAgentDelayBase = new ExPerformanceCounter(base.CategoryName, "Average Agent Processing Time Base (sec)", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageAgentDelayBase);
				this.AverageAgentProcessorUsageSynchronousInvocations = new ExPerformanceCounter(base.CategoryName, "Average CPU usage of synchronous invocations of agent (milliseconds)", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageAgentProcessorUsageSynchronousInvocations);
				this.SynchronousAgentInvocationSamples = new ExPerformanceCounter(base.CategoryName, "Samples used to calculate Average CPU usage of synchronous invocations", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.SynchronousAgentInvocationSamples);
				this.AverageAgentProcessorUsageAsynchronousInvocations = new ExPerformanceCounter(base.CategoryName, "Average CPU usage of asynchronous invocations of agent (milliseconds)", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageAgentProcessorUsageAsynchronousInvocations);
				this.AsynchronousAgentInvocationSamples = new ExPerformanceCounter(base.CategoryName, "Samples used to calculate Average CPU usage of asynchronous invocations", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AsynchronousAgentInvocationSamples);
				this.TotalAgentInvocations = new ExPerformanceCounter(base.CategoryName, "Total Agent Invocations", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.TotalAgentInvocations);
				this.TotalAgentErrorHandlingOverrides = new ExPerformanceCounter(base.CategoryName, "Total Agent Error Handling Overrides", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.TotalAgentErrorHandlingOverrides);
				long num = this.AverageAgentDelay.RawValue;
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

		// Token: 0x0600036F RID: 879 RVA: 0x00011938 File Offset: 0x0000FB38
		internal MExCountersInstance(string instanceName) : base(instanceName, MExCounters.CategoryName)
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.AverageAgentDelay = new ExPerformanceCounter(base.CategoryName, "Average Agent Processing Time (sec)", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageAgentDelay);
				this.AverageAgentDelayBase = new ExPerformanceCounter(base.CategoryName, "Average Agent Processing Time Base (sec)", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageAgentDelayBase);
				this.AverageAgentProcessorUsageSynchronousInvocations = new ExPerformanceCounter(base.CategoryName, "Average CPU usage of synchronous invocations of agent (milliseconds)", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageAgentProcessorUsageSynchronousInvocations);
				this.SynchronousAgentInvocationSamples = new ExPerformanceCounter(base.CategoryName, "Samples used to calculate Average CPU usage of synchronous invocations", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.SynchronousAgentInvocationSamples);
				this.AverageAgentProcessorUsageAsynchronousInvocations = new ExPerformanceCounter(base.CategoryName, "Average CPU usage of asynchronous invocations of agent (milliseconds)", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageAgentProcessorUsageAsynchronousInvocations);
				this.AsynchronousAgentInvocationSamples = new ExPerformanceCounter(base.CategoryName, "Samples used to calculate Average CPU usage of asynchronous invocations", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AsynchronousAgentInvocationSamples);
				this.TotalAgentInvocations = new ExPerformanceCounter(base.CategoryName, "Total Agent Invocations", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.TotalAgentInvocations);
				this.TotalAgentErrorHandlingOverrides = new ExPerformanceCounter(base.CategoryName, "Total Agent Error Handling Overrides", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.TotalAgentErrorHandlingOverrides);
				long num = this.AverageAgentDelay.RawValue;
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

		// Token: 0x06000370 RID: 880 RVA: 0x00011B34 File Offset: 0x0000FD34
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

		// Token: 0x04000449 RID: 1097
		public readonly ExPerformanceCounter AverageAgentDelay;

		// Token: 0x0400044A RID: 1098
		public readonly ExPerformanceCounter AverageAgentDelayBase;

		// Token: 0x0400044B RID: 1099
		public readonly ExPerformanceCounter AverageAgentProcessorUsageSynchronousInvocations;

		// Token: 0x0400044C RID: 1100
		public readonly ExPerformanceCounter SynchronousAgentInvocationSamples;

		// Token: 0x0400044D RID: 1101
		public readonly ExPerformanceCounter AverageAgentProcessorUsageAsynchronousInvocations;

		// Token: 0x0400044E RID: 1102
		public readonly ExPerformanceCounter AsynchronousAgentInvocationSamples;

		// Token: 0x0400044F RID: 1103
		public readonly ExPerformanceCounter TotalAgentInvocations;

		// Token: 0x04000450 RID: 1104
		public readonly ExPerformanceCounter TotalAgentErrorHandlingOverrides;
	}
}
