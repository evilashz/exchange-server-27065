using System;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.StoreDriverDelivery;
using Microsoft.Exchange.MailboxTransport.StoreDriver;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery
{
	// Token: 0x02000014 RID: 20
	internal static class StoreDriverDeliveryAgentPerfCounters
	{
		// Token: 0x06000178 RID: 376 RVA: 0x00008844 File Offset: 0x00006A44
		public static void IncrementAgentDeliveryAttempt(string agentName)
		{
			StoreDriverDeliveryAgentPerfCounters.InstanceEntry instanceEntry = StoreDriverDeliveryAgentPerfCounters.GetInstanceEntry(agentName);
			if (instanceEntry != null)
			{
				instanceEntry.DeliveryAgentFailuresCounter.AddDenominator(1L);
			}
		}

		// Token: 0x06000179 RID: 377 RVA: 0x0000886C File Offset: 0x00006A6C
		public static void IncrementAgentDeliveryFailure(string agentName)
		{
			StoreDriverDeliveryAgentPerfCounters.InstanceEntry instanceEntry = StoreDriverDeliveryAgentPerfCounters.GetInstanceEntry(agentName);
			if (instanceEntry != null)
			{
				instanceEntry.DeliveryAgentFailuresCounter.AddNumerator(1L);
			}
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00008894 File Offset: 0x00006A94
		public static void RefreshAgentDeliveryPercentCounter(string agentName)
		{
			StoreDriverDeliveryAgentPerfCounters.InstanceEntry instanceEntry = StoreDriverDeliveryAgentPerfCounters.GetInstanceEntry(agentName);
			if (instanceEntry != null)
			{
				lock (MSExchangeStoreDriverDeliveryAgent.TotalInstance.DeliveryAgentFailures)
				{
					instanceEntry.PerfCounterInstance.DeliveryAgentFailures.RawValue = (long)instanceEntry.DeliveryAgentFailuresCounter.GetSlidingPercentage();
				}
			}
		}

		// Token: 0x0600017B RID: 379 RVA: 0x000088F8 File Offset: 0x00006AF8
		private static StoreDriverDeliveryAgentPerfCounters.InstanceEntry GetInstanceEntry(string agentName)
		{
			if (string.IsNullOrEmpty(agentName))
			{
				return null;
			}
			return StoreDriverDeliveryAgentPerfCounters.PerfCountersDictionary.AddIfNotExists(agentName, new SynchronizedDictionary<string, StoreDriverDeliveryAgentPerfCounters.InstanceEntry>.CreationalMethod(StoreDriverDeliveryAgentPerfCounters.CreateInstanceEntry));
		}

		// Token: 0x0600017C RID: 380 RVA: 0x0000891C File Offset: 0x00006B1C
		private static StoreDriverDeliveryAgentPerfCounters.InstanceEntry CreateInstanceEntry(string agentName)
		{
			MSExchangeStoreDriverDeliveryAgentInstance msexchangeStoreDriverDeliveryAgentInstance = null;
			try
			{
				if (agentName != null)
				{
					msexchangeStoreDriverDeliveryAgentInstance = MSExchangeStoreDriverDeliveryAgent.GetInstance(agentName);
				}
			}
			catch (InvalidOperationException arg)
			{
				TraceHelper.StoreDriverDeliveryTracer.TraceFail<string, InvalidOperationException>(TraceHelper.MessageProbeActivityId, 0L, "Get StoreDriverDelivery agent PerfCounters Instance {0} failed due to: {1}", agentName, arg);
			}
			if (msexchangeStoreDriverDeliveryAgentInstance == null)
			{
				return null;
			}
			return new StoreDriverDeliveryAgentPerfCounters.InstanceEntry(msexchangeStoreDriverDeliveryAgentInstance);
		}

		// Token: 0x04000094 RID: 148
		private static readonly Trace diag = ExTraceGlobals.StoreDriverDeliveryTracer;

		// Token: 0x04000095 RID: 149
		private static readonly SynchronizedDictionary<string, StoreDriverDeliveryAgentPerfCounters.InstanceEntry> PerfCountersDictionary = new SynchronizedDictionary<string, StoreDriverDeliveryAgentPerfCounters.InstanceEntry>(100, StringComparer.OrdinalIgnoreCase);

		// Token: 0x04000096 RID: 150
		private static readonly TimeSpan SlidingWindowLength = TimeSpan.FromMinutes(5.0);

		// Token: 0x04000097 RID: 151
		private static readonly TimeSpan SlidingBucketLength = TimeSpan.FromMinutes(1.0);

		// Token: 0x02000015 RID: 21
		private class InstanceEntry
		{
			// Token: 0x0600017E RID: 382 RVA: 0x000089BE File Offset: 0x00006BBE
			internal InstanceEntry(MSExchangeStoreDriverDeliveryAgentInstance perfCounterInstance)
			{
				this.PerfCounterInstance = perfCounterInstance;
				this.DeliveryAgentFailuresCounter = new SlidingPercentageCounter(StoreDriverDeliveryAgentPerfCounters.SlidingWindowLength, StoreDriverDeliveryAgentPerfCounters.SlidingBucketLength, true);
			}

			// Token: 0x1700008D RID: 141
			// (get) Token: 0x0600017F RID: 383 RVA: 0x000089E3 File Offset: 0x00006BE3
			// (set) Token: 0x06000180 RID: 384 RVA: 0x000089EB File Offset: 0x00006BEB
			internal MSExchangeStoreDriverDeliveryAgentInstance PerfCounterInstance { get; private set; }

			// Token: 0x1700008E RID: 142
			// (get) Token: 0x06000181 RID: 385 RVA: 0x000089F4 File Offset: 0x00006BF4
			// (set) Token: 0x06000182 RID: 386 RVA: 0x000089FC File Offset: 0x00006BFC
			internal SlidingPercentageCounter DeliveryAgentFailuresCounter { get; private set; }
		}
	}
}
