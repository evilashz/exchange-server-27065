using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ThrottlingService;

namespace Microsoft.Exchange.Data.ThrottlingService.Client
{
	// Token: 0x02000004 RID: 4
	internal sealed class ThrottlingClientPerformanceCountersImpl : IThrottlingClientPerformanceCounters
	{
		// Token: 0x06000016 RID: 22 RVA: 0x000025FC File Offset: 0x000007FC
		public ThrottlingClientPerformanceCountersImpl(string instanceName)
		{
			if (string.IsNullOrEmpty(instanceName))
			{
				throw new ArgumentException("Throttling Service Client PerfCounters InstanceName name cannot be null or empty", "throttlingServiceClientPerfCounterInstanceName");
			}
			this.percentageCounterRequestsSucceeded = new SlidingPercentageCounter(ThrottlingClientPerformanceCountersImpl.SlidingCounterInterval, ThrottlingClientPerformanceCountersImpl.SlidingCounterPrecision);
			this.percentageCounterRequestsDenied = new SlidingPercentageCounter(ThrottlingClientPerformanceCountersImpl.SlidingCounterInterval, ThrottlingClientPerformanceCountersImpl.SlidingCounterPrecision);
			this.percentageCounterRequestsBypassed = new SlidingPercentageCounter(ThrottlingClientPerformanceCountersImpl.SlidingCounterInterval, ThrottlingClientPerformanceCountersImpl.SlidingCounterPrecision);
			this.averageCountersForRequestInterval = new SlidingPercentageCounter(ThrottlingClientPerformanceCountersImpl.SlidingCounterInterval, ThrottlingClientPerformanceCountersImpl.SlidingCounterPrecision);
			try
			{
				this.perfCountersInstance = ThrottlingServiceClientPerformanceCounters.GetInstance(instanceName);
			}
			catch (InvalidOperationException arg)
			{
				ThrottlingClientPerformanceCountersImpl.tracer.TraceError<string, InvalidOperationException>(0L, "Failed to initialize performance counters instance '{0}': {1}", instanceName, arg);
				this.perfCountersInstance = null;
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000026B8 File Offset: 0x000008B8
		public void AddRequestStatus(ThrottlingRpcResult result)
		{
			this.AddRequestStatusToCounters(result, -1L);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000026C3 File Offset: 0x000008C3
		public void AddRequestStatus(ThrottlingRpcResult result, long requestTimeMsec)
		{
			if (requestTimeMsec < 0L)
			{
				throw new ArgumentException("Request time must be greater than or equal to zero.", "requestTimeMsec");
			}
			this.AddRequestStatusToCounters(result, requestTimeMsec);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000026E4 File Offset: 0x000008E4
		private void AddRequestStatusToCounters(ThrottlingRpcResult result, long requestTimeMsec)
		{
			if (this.perfCountersInstance != null)
			{
				switch (result)
				{
				case ThrottlingRpcResult.Allowed:
					this.percentageCounterRequestsSucceeded.AddNumerator(1L);
					break;
				case ThrottlingRpcResult.Bypassed:
				case ThrottlingRpcResult.Failed:
					this.percentageCounterRequestsBypassed.AddNumerator(1L);
					break;
				case ThrottlingRpcResult.Denied:
					this.percentageCounterRequestsDenied.AddNumerator(1L);
					break;
				}
				this.percentageCounterRequestsSucceeded.AddDenominator(1L);
				this.percentageCounterRequestsDenied.AddDenominator(1L);
				this.percentageCounterRequestsBypassed.AddDenominator(1L);
				this.perfCountersInstance.SuccessfulSubmissionRequests.RawValue = (long)this.percentageCounterRequestsSucceeded.GetSlidingPercentage();
				this.perfCountersInstance.BypassedSubmissionRequests.RawValue = (long)this.percentageCounterRequestsBypassed.GetSlidingPercentage();
				this.perfCountersInstance.DeniedSubmissionRequest.RawValue = (long)this.percentageCounterRequestsDenied.GetSlidingPercentage();
				if (requestTimeMsec >= 0L)
				{
					this.averageCountersForRequestInterval.AddNumerator(requestTimeMsec);
					this.averageCountersForRequestInterval.AddDenominator(1L);
					this.perfCountersInstance.AverageSubmissionRequestTime.RawValue = (long)this.averageCountersForRequestInterval.GetSlidingPercentage() / 100L;
				}
			}
		}

		// Token: 0x04000009 RID: 9
		private static readonly TimeSpan SlidingCounterInterval = TimeSpan.FromMinutes(5.0);

		// Token: 0x0400000A RID: 10
		private static readonly TimeSpan SlidingCounterPrecision = TimeSpan.FromSeconds(1.0);

		// Token: 0x0400000B RID: 11
		private static Trace tracer = ExTraceGlobals.ThrottlingClientTracer;

		// Token: 0x0400000C RID: 12
		private SlidingPercentageCounter percentageCounterRequestsSucceeded;

		// Token: 0x0400000D RID: 13
		private SlidingPercentageCounter percentageCounterRequestsDenied;

		// Token: 0x0400000E RID: 14
		private SlidingPercentageCounter percentageCounterRequestsBypassed;

		// Token: 0x0400000F RID: 15
		private SlidingPercentageCounter averageCountersForRequestInterval;

		// Token: 0x04000010 RID: 16
		private ThrottlingServiceClientPerformanceCountersInstance perfCountersInstance;
	}
}
