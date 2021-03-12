using System;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.StoreDriverSubmission;
using Microsoft.Exchange.MailboxTransport.StoreDriver;

namespace Microsoft.Exchange.MailboxTransport.Submission.StoreDriverSubmission
{
	// Token: 0x02000031 RID: 49
	internal static class StoreDriverSubmissionAgentPerfCounters
	{
		// Token: 0x060001FD RID: 509 RVA: 0x0000C268 File Offset: 0x0000A468
		public static void IncrementAgentSubmissionAttempt(string agentName)
		{
			StoreDriverSubmissionAgentPerfCounters.InstanceEntry instanceEntry = StoreDriverSubmissionAgentPerfCounters.GetInstanceEntry(agentName);
			if (instanceEntry != null)
			{
				instanceEntry.SubmissionAgentFailuresCounter.AddDenominator(1L);
			}
		}

		// Token: 0x060001FE RID: 510 RVA: 0x0000C290 File Offset: 0x0000A490
		public static void IncrementAgentSubmissionFailure(string agentName)
		{
			StoreDriverSubmissionAgentPerfCounters.InstanceEntry instanceEntry = StoreDriverSubmissionAgentPerfCounters.GetInstanceEntry(agentName);
			if (instanceEntry != null)
			{
				instanceEntry.SubmissionAgentFailuresCounter.AddNumerator(1L);
			}
		}

		// Token: 0x060001FF RID: 511 RVA: 0x0000C2B8 File Offset: 0x0000A4B8
		public static void RefreshAgentSubmissionPercentCounter(string agentName)
		{
			StoreDriverSubmissionAgentPerfCounters.InstanceEntry instanceEntry = StoreDriverSubmissionAgentPerfCounters.GetInstanceEntry(agentName);
			if (instanceEntry != null)
			{
				lock (MSExchangeStoreDriverSubmissionAgent.TotalInstance.SubmissionAgentFailures)
				{
					instanceEntry.PerfCounterInstance.SubmissionAgentFailures.RawValue = (long)instanceEntry.SubmissionAgentFailuresCounter.GetSlidingPercentage();
				}
			}
		}

		// Token: 0x06000200 RID: 512 RVA: 0x0000C31C File Offset: 0x0000A51C
		private static StoreDriverSubmissionAgentPerfCounters.InstanceEntry GetInstanceEntry(string agentName)
		{
			if (string.IsNullOrEmpty(agentName))
			{
				return null;
			}
			return StoreDriverSubmissionAgentPerfCounters.PerfCountersDictionary.AddIfNotExists(agentName, new SynchronizedDictionary<string, StoreDriverSubmissionAgentPerfCounters.InstanceEntry>.CreationalMethod(StoreDriverSubmissionAgentPerfCounters.CreateInstanceEntry));
		}

		// Token: 0x06000201 RID: 513 RVA: 0x0000C340 File Offset: 0x0000A540
		private static StoreDriverSubmissionAgentPerfCounters.InstanceEntry CreateInstanceEntry(string agentName)
		{
			MSExchangeStoreDriverSubmissionAgentInstance msexchangeStoreDriverSubmissionAgentInstance = null;
			try
			{
				if (agentName != null)
				{
					msexchangeStoreDriverSubmissionAgentInstance = MSExchangeStoreDriverSubmissionAgent.GetInstance(agentName);
				}
			}
			catch (InvalidOperationException arg)
			{
				TraceHelper.StoreDriverSubmissionTracer.TraceFail<string, InvalidOperationException>(TraceHelper.MessageProbeActivityId, 0L, "Get StoreDriverSubmission agent PerfCounters Instance {0} failed due to: {1}", agentName, arg);
			}
			if (msexchangeStoreDriverSubmissionAgentInstance == null)
			{
				return null;
			}
			return new StoreDriverSubmissionAgentPerfCounters.InstanceEntry(msexchangeStoreDriverSubmissionAgentInstance);
		}

		// Token: 0x040000E5 RID: 229
		private static readonly Trace diag = ExTraceGlobals.MapiStoreDriverSubmissionTracer;

		// Token: 0x040000E6 RID: 230
		private static readonly SynchronizedDictionary<string, StoreDriverSubmissionAgentPerfCounters.InstanceEntry> PerfCountersDictionary = new SynchronizedDictionary<string, StoreDriverSubmissionAgentPerfCounters.InstanceEntry>(100, StringComparer.OrdinalIgnoreCase);

		// Token: 0x040000E7 RID: 231
		private static readonly TimeSpan SlidingWindowLength = TimeSpan.FromMinutes(5.0);

		// Token: 0x040000E8 RID: 232
		private static readonly TimeSpan SlidingBucketLength = TimeSpan.FromMinutes(1.0);

		// Token: 0x02000032 RID: 50
		private class InstanceEntry
		{
			// Token: 0x06000203 RID: 515 RVA: 0x0000C3E2 File Offset: 0x0000A5E2
			internal InstanceEntry(MSExchangeStoreDriverSubmissionAgentInstance perfCounterInstance)
			{
				this.PerfCounterInstance = perfCounterInstance;
				this.SubmissionAgentFailuresCounter = new SlidingPercentageCounter(StoreDriverSubmissionAgentPerfCounters.SlidingWindowLength, StoreDriverSubmissionAgentPerfCounters.SlidingBucketLength, true);
			}

			// Token: 0x170000A8 RID: 168
			// (get) Token: 0x06000204 RID: 516 RVA: 0x0000C407 File Offset: 0x0000A607
			// (set) Token: 0x06000205 RID: 517 RVA: 0x0000C40F File Offset: 0x0000A60F
			internal MSExchangeStoreDriverSubmissionAgentInstance PerfCounterInstance { get; private set; }

			// Token: 0x170000A9 RID: 169
			// (get) Token: 0x06000206 RID: 518 RVA: 0x0000C418 File Offset: 0x0000A618
			// (set) Token: 0x06000207 RID: 519 RVA: 0x0000C420 File Offset: 0x0000A620
			internal SlidingPercentageCounter SubmissionAgentFailuresCounter { get; private set; }
		}
	}
}
