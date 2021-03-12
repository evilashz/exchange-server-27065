using System;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Extensibility.Internal;

namespace Microsoft.Exchange.Data.Transport.Internal.MExRuntime
{
	// Token: 0x0200008F RID: 143
	internal sealed class MExPerfCounters
	{
		// Token: 0x0600049A RID: 1178 RVA: 0x00015EF0 File Offset: 0x000140F0
		public MExPerfCounters(ProcessTransportRole processRole, AgentRecord[] agentRecords)
		{
			this.agentRecords = agentRecords;
			MExCounters.SetCategoryName(MExPerfCounters.GetPerformanceCounterCategory(processRole));
			this.synchronousAgentProcessorUsageSlidingCounters = new SlidingAverageCounter[this.agentRecords.Length];
			this.asynchronousAgentProcessorUsageSlidingCounters = new SlidingAverageCounter[this.agentRecords.Length];
			for (int i = 0; i < this.agentRecords.Length; i++)
			{
				MExCounters.GetInstance(this.agentRecords[i].Name);
				this.synchronousAgentProcessorUsageSlidingCounters[this.agentRecords[i].SequenceNumber] = new SlidingAverageCounter(MExPerfCounters.slidingWindowLengthForAgentProcessorUsageCounters, MExPerfCounters.bucketLengthForAgentProcessorUsageCounters);
				this.asynchronousAgentProcessorUsageSlidingCounters[this.agentRecords[i].SequenceNumber] = new SlidingAverageCounter(MExPerfCounters.slidingWindowLengthForAgentProcessorUsageCounters, MExPerfCounters.bucketLengthForAgentProcessorUsageCounters);
			}
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x00015FA8 File Offset: 0x000141A8
		public void Shutdown()
		{
			for (int i = 0; i < this.agentRecords.Length; i++)
			{
				MExCounters.RemoveInstance(this.agentRecords[i].Name);
			}
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x00015FDA File Offset: 0x000141DA
		internal void Subscribe(IDispatcher dispatcher)
		{
			dispatcher.OnAgentInvokeStart += this.AgentInvokeStartHandler;
			dispatcher.OnAgentInvokeEnd += this.AgentInvokeEndHandler;
			dispatcher.OnAgentInvokeReturns += this.AgentInvokeReturnsHandler;
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x00016012 File Offset: 0x00014212
		internal void AgentInvokeStartHandler(object source, MExSession context)
		{
			context.BeginInvokeTicks = Stopwatch.GetTimestamp();
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x00016020 File Offset: 0x00014220
		internal void AgentInvokeEndHandler(object dispatcher, MExSession context)
		{
			MExCountersInstance instance = MExCounters.GetInstance(context.CurrentAgent.Name);
			long timestamp = Stopwatch.GetTimestamp();
			instance.AverageAgentDelay.IncrementBy(timestamp - context.BeginInvokeTicks);
			instance.AverageAgentDelayBase.Increment();
			instance.TotalAgentInvocations.Increment();
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x00016070 File Offset: 0x00014270
		internal void AgentInvokeReturnsHandler(object dispatcher, MExSession context)
		{
			MExCountersInstance instance = MExCounters.GetInstance(context.CurrentAgent.Name);
			if (context.IsAsyncAgent)
			{
				SlidingAverageCounter slidingAverageCounter = this.asynchronousAgentProcessorUsageSlidingCounters[context.CurrentAgent.SequenceNumber];
				slidingAverageCounter.AddValue(context.TotalProcessorTime);
				long rawValue2;
				long rawValue = slidingAverageCounter.CalculateAverageAcrossAllSamples(out rawValue2);
				instance.AverageAgentProcessorUsageAsynchronousInvocations.RawValue = rawValue;
				instance.AsynchronousAgentInvocationSamples.RawValue = rawValue2;
			}
			else
			{
				SlidingAverageCounter slidingAverageCounter2 = this.synchronousAgentProcessorUsageSlidingCounters[context.CurrentAgent.SequenceNumber];
				slidingAverageCounter2.AddValue(context.TotalProcessorTime);
				long rawValue4;
				long rawValue3 = slidingAverageCounter2.CalculateAverageAcrossAllSamples(out rawValue4);
				instance.AverageAgentProcessorUsageSynchronousInvocations.RawValue = rawValue3;
				instance.SynchronousAgentInvocationSamples.RawValue = rawValue4;
			}
			context.CleanupCpuTracker();
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x00016124 File Offset: 0x00014324
		private static string GetPerformanceCounterCategory(ProcessTransportRole processRole)
		{
			switch (processRole)
			{
			case ProcessTransportRole.Hub:
			case ProcessTransportRole.Edge:
				return "MSExchangeTransport Extensibility Agents";
			case ProcessTransportRole.FrontEnd:
				return "MSExchangeFrontEndTransport Extensibility Agents";
			case ProcessTransportRole.MailboxSubmission:
				return "MSExchange Submission Extensibility Agents";
			case ProcessTransportRole.MailboxDelivery:
				return "MSExchange Delivery Extensibility Agents";
			default:
				throw new InvalidOperationException(string.Format("Performance counter category not defined for process role {0}", processRole));
			}
		}

		// Token: 0x040004E0 RID: 1248
		private static readonly TimeSpan slidingWindowLengthForAgentProcessorUsageCounters = TimeSpan.FromMinutes(10.0);

		// Token: 0x040004E1 RID: 1249
		private static readonly TimeSpan bucketLengthForAgentProcessorUsageCounters = TimeSpan.FromSeconds(10.0);

		// Token: 0x040004E2 RID: 1250
		private readonly AgentRecord[] agentRecords;

		// Token: 0x040004E3 RID: 1251
		private SlidingAverageCounter[] synchronousAgentProcessorUsageSlidingCounters;

		// Token: 0x040004E4 RID: 1252
		private SlidingAverageCounter[] asynchronousAgentProcessorUsageSlidingCounters;
	}
}
