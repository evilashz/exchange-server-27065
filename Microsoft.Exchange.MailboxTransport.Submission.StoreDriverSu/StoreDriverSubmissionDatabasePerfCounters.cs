using System;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.StoreDriverSubmission;
using Microsoft.Exchange.MailboxTransport.StoreDriver;

namespace Microsoft.Exchange.MailboxTransport.Submission.StoreDriverSubmission
{
	// Token: 0x0200002F RID: 47
	internal static class StoreDriverSubmissionDatabasePerfCounters
	{
		// Token: 0x060001ED RID: 493 RVA: 0x0000BF44 File Offset: 0x0000A144
		public static void IncrementSubmissionAttempt(string mdbName, bool calculateOnly = false)
		{
			StoreDriverSubmissionDatabasePerfCounters.InstanceEntry instanceEntry = StoreDriverSubmissionDatabasePerfCounters.GetInstanceEntry(mdbName);
			if (instanceEntry != null)
			{
				if (!calculateOnly)
				{
					instanceEntry.SubmissionAttemptsCounter.AddValue(1L);
				}
				lock (MSExchangeStoreDriverSubmissionDatabase.TotalInstance.SubmissionAttempts)
				{
					instanceEntry.PerfCounterInstance.SubmissionAttempts.RawValue = instanceEntry.SubmissionAttemptsCounter.CalculateAverage();
				}
			}
		}

		// Token: 0x060001EE RID: 494 RVA: 0x0000BFB8 File Offset: 0x0000A1B8
		public static void IncrementSubmissionFailure(string mdbName, bool calculateOnly = false)
		{
			StoreDriverSubmissionDatabasePerfCounters.InstanceEntry instanceEntry = StoreDriverSubmissionDatabasePerfCounters.GetInstanceEntry(mdbName);
			if (instanceEntry != null)
			{
				if (!calculateOnly)
				{
					instanceEntry.SubmissionFailuresCounter.AddValue(1L);
				}
				lock (MSExchangeStoreDriverSubmissionDatabase.TotalInstance.SubmissionFailures)
				{
					instanceEntry.PerfCounterInstance.SubmissionFailures.RawValue = instanceEntry.SubmissionFailuresCounter.CalculateAverage();
				}
			}
		}

		// Token: 0x060001EF RID: 495 RVA: 0x0000C02C File Offset: 0x0000A22C
		public static void IncrementSkippedSubmission(string mdbName, bool calculateOnly = false)
		{
			StoreDriverSubmissionDatabasePerfCounters.InstanceEntry instanceEntry = StoreDriverSubmissionDatabasePerfCounters.GetInstanceEntry(mdbName);
			if (instanceEntry != null)
			{
				if (!calculateOnly)
				{
					instanceEntry.SkippedSubmissionsCounter.AddValue(1L);
				}
				lock (MSExchangeStoreDriverSubmissionDatabase.TotalInstance.SkippedSubmissions)
				{
					instanceEntry.PerfCounterInstance.SkippedSubmissions.RawValue = instanceEntry.SkippedSubmissionsCounter.CalculateAverage();
				}
			}
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x0000C0A0 File Offset: 0x0000A2A0
		public static void RefreshPerformanceCounters()
		{
			foreach (string mdbName in StoreDriverSubmissionDatabasePerfCounters.PerfCountersDictionary.Keys)
			{
				StoreDriverSubmissionDatabasePerfCounters.IncrementSubmissionAttempt(mdbName, true);
				StoreDriverSubmissionDatabasePerfCounters.IncrementSubmissionFailure(mdbName, true);
				StoreDriverSubmissionDatabasePerfCounters.IncrementSkippedSubmission(mdbName, true);
			}
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x0000C100 File Offset: 0x0000A300
		private static StoreDriverSubmissionDatabasePerfCounters.InstanceEntry GetInstanceEntry(string mdbName)
		{
			if (string.IsNullOrEmpty(mdbName))
			{
				return null;
			}
			return StoreDriverSubmissionDatabasePerfCounters.PerfCountersDictionary.AddIfNotExists(mdbName, new SynchronizedDictionary<string, StoreDriverSubmissionDatabasePerfCounters.InstanceEntry>.CreationalMethod(StoreDriverSubmissionDatabasePerfCounters.CreateInstanceEntry));
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x0000C124 File Offset: 0x0000A324
		private static StoreDriverSubmissionDatabasePerfCounters.InstanceEntry CreateInstanceEntry(string mdbName)
		{
			MSExchangeStoreDriverSubmissionDatabaseInstance msexchangeStoreDriverSubmissionDatabaseInstance = null;
			try
			{
				if (mdbName != null)
				{
					msexchangeStoreDriverSubmissionDatabaseInstance = MSExchangeStoreDriverSubmissionDatabase.GetInstance(mdbName);
				}
			}
			catch (InvalidOperationException arg)
			{
				TraceHelper.StoreDriverSubmissionTracer.TraceFail<string, InvalidOperationException>(TraceHelper.MessageProbeActivityId, 0L, "Get StoreDriverSubmission PerfCounters Instance {0} failed due to: {1}", mdbName, arg);
			}
			if (msexchangeStoreDriverSubmissionDatabaseInstance == null)
			{
				return null;
			}
			return new StoreDriverSubmissionDatabasePerfCounters.InstanceEntry(msexchangeStoreDriverSubmissionDatabaseInstance);
		}

		// Token: 0x040000DD RID: 221
		private static readonly Trace diag = ExTraceGlobals.MapiStoreDriverSubmissionTracer;

		// Token: 0x040000DE RID: 222
		private static readonly SynchronizedDictionary<string, StoreDriverSubmissionDatabasePerfCounters.InstanceEntry> PerfCountersDictionary = new SynchronizedDictionary<string, StoreDriverSubmissionDatabasePerfCounters.InstanceEntry>(100, StringComparer.OrdinalIgnoreCase);

		// Token: 0x040000DF RID: 223
		private static readonly TimeSpan SlidingWindowLength = TimeSpan.FromMinutes(5.0);

		// Token: 0x040000E0 RID: 224
		private static readonly TimeSpan SlidingBucketLength = TimeSpan.FromMinutes(1.0);

		// Token: 0x02000030 RID: 48
		private class InstanceEntry
		{
			// Token: 0x060001F4 RID: 500 RVA: 0x0000C1C8 File Offset: 0x0000A3C8
			internal InstanceEntry(MSExchangeStoreDriverSubmissionDatabaseInstance perfCounterInstance)
			{
				this.PerfCounterInstance = perfCounterInstance;
				this.SubmissionAttemptsCounter = new SlidingAverageCounter(StoreDriverSubmissionDatabasePerfCounters.SlidingWindowLength, StoreDriverSubmissionDatabasePerfCounters.SlidingBucketLength);
				this.SubmissionFailuresCounter = new SlidingAverageCounter(StoreDriverSubmissionDatabasePerfCounters.SlidingWindowLength, StoreDriverSubmissionDatabasePerfCounters.SlidingBucketLength);
				this.SkippedSubmissionsCounter = new SlidingAverageCounter(StoreDriverSubmissionDatabasePerfCounters.SlidingWindowLength, StoreDriverSubmissionDatabasePerfCounters.SlidingBucketLength);
			}

			// Token: 0x170000A4 RID: 164
			// (get) Token: 0x060001F5 RID: 501 RVA: 0x0000C221 File Offset: 0x0000A421
			// (set) Token: 0x060001F6 RID: 502 RVA: 0x0000C229 File Offset: 0x0000A429
			internal MSExchangeStoreDriverSubmissionDatabaseInstance PerfCounterInstance { get; private set; }

			// Token: 0x170000A5 RID: 165
			// (get) Token: 0x060001F7 RID: 503 RVA: 0x0000C232 File Offset: 0x0000A432
			// (set) Token: 0x060001F8 RID: 504 RVA: 0x0000C23A File Offset: 0x0000A43A
			internal SlidingAverageCounter SubmissionAttemptsCounter { get; private set; }

			// Token: 0x170000A6 RID: 166
			// (get) Token: 0x060001F9 RID: 505 RVA: 0x0000C243 File Offset: 0x0000A443
			// (set) Token: 0x060001FA RID: 506 RVA: 0x0000C24B File Offset: 0x0000A44B
			internal SlidingAverageCounter SubmissionFailuresCounter { get; private set; }

			// Token: 0x170000A7 RID: 167
			// (get) Token: 0x060001FB RID: 507 RVA: 0x0000C254 File Offset: 0x0000A454
			// (set) Token: 0x060001FC RID: 508 RVA: 0x0000C25C File Offset: 0x0000A45C
			internal SlidingAverageCounter SkippedSubmissionsCounter { get; private set; }
		}
	}
}
