using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Logging.Search;

namespace Microsoft.Exchange.InfoWorker.Common.MessageTracking
{
	// Token: 0x020002F6 RID: 758
	internal class TrackingEventBudget : IDisposeTrackable, IDisposable
	{
		// Token: 0x170005D7 RID: 1495
		// (get) Token: 0x0600165F RID: 5727 RVA: 0x00068813 File Offset: 0x00066A13
		public TimeSpan Elapsed
		{
			get
			{
				return this.timer.Elapsed;
			}
		}

		// Token: 0x06001660 RID: 5728 RVA: 0x00068820 File Offset: 0x00066A20
		public TrackingEventBudget(TrackingErrorCollection errors, TimeSpan timeBudgetAllowed)
		{
			TraceWrapper.SearchLibraryTracer.TraceDebug<double>(this.GetHashCode(), "Time budget: {0} msec", timeBudgetAllowed.TotalMilliseconds);
			this.budgetUsed = 0U;
			this.errors = errors;
			this.timeBudgetAllowed = timeBudgetAllowed;
			this.timer = Stopwatch.StartNew();
		}

		// Token: 0x06001661 RID: 5729 RVA: 0x00068870 File Offset: 0x00066A70
		internal static void AcquireThread()
		{
			int num = Interlocked.Increment(ref TrackingEventBudget.currentSearchingThreadCount);
			if (num <= ServerCache.Instance.NumberOfThreadsAllowed)
			{
				return;
			}
			TraceWrapper.SearchLibraryTracer.TraceError<int, int>(0, "Server too busy, current-threads={0} max-threads={1}", num, ServerCache.Instance.NumberOfThreadsAllowed);
			TrackingError trackingError = new TrackingError(ErrorCode.TotalBudgetExceeded, string.Empty, string.Empty, string.Empty);
			throw new TrackingTransientException(trackingError, null, false);
		}

		// Token: 0x06001662 RID: 5730 RVA: 0x000688CF File Offset: 0x00066ACF
		internal static void ReleaseThread()
		{
			Interlocked.Decrement(ref TrackingEventBudget.currentSearchingThreadCount);
		}

		// Token: 0x06001663 RID: 5731 RVA: 0x000688DC File Offset: 0x00066ADC
		internal bool IsUnderBudget()
		{
			TrackingError trackingError;
			return this.IsUnderResourceBudget() && this.IsUnderTimeBudget(out trackingError);
		}

		// Token: 0x06001664 RID: 5732 RVA: 0x000688FC File Offset: 0x00066AFC
		internal void CheckTimeBudget()
		{
			TrackingError trackingError;
			if (!this.IsUnderTimeBudget(out trackingError))
			{
				throw new TrackingTransientException(trackingError, null, true);
			}
		}

		// Token: 0x06001665 RID: 5733 RVA: 0x0006891C File Offset: 0x00066B1C
		public void GetTimeBudgetRemainingForWSCall(TrackingAuthorityKind authorityKindToConnect, out TimeSpan clientTimeout, out TimeSpan serverTimeout)
		{
			TrackingAuthorityKindInformation trackingAuthorityKindInformation;
			if (EnumAttributeInfo<TrackingAuthorityKind, TrackingAuthorityKindInformation>.TryGetValue((int)authorityKindToConnect, out trackingAuthorityKindInformation))
			{
				int expectedConnectionLatencyMSec = trackingAuthorityKindInformation.ExpectedConnectionLatencyMSec;
			}
			int num = (int)Math.Min(this.timer.Elapsed.TotalMilliseconds, 2147483647.0);
			if (this.timeBudgetAllowed.TotalMilliseconds <= (double)num)
			{
				TraceWrapper.SearchLibraryTracer.TraceError<double, int>(this.GetHashCode(), "No time budget remaining. Total budget: {0}, Already used up: {1}", this.timeBudgetAllowed.TotalMilliseconds, num);
				clientTimeout = TimeSpan.Zero;
				serverTimeout = TimeSpan.Zero;
				return;
			}
			double val = this.timeBudgetAllowed.TotalMilliseconds - (double)num;
			int num2 = (int)Math.Min(val, 2147483647.0);
			if (num2 <= trackingAuthorityKindInformation.ExpectedConnectionLatencyMSec)
			{
				TraceWrapper.SearchLibraryTracer.TraceError<int, string, int>(this.GetHashCode(), "Remaining time in budget = {0} is less than connection overhead for Connection-Type: {1}. Overhead = {2}", num2, Names<TrackingAuthorityKind>.Map[(int)authorityKindToConnect], trackingAuthorityKindInformation.ExpectedConnectionLatencyMSec);
				clientTimeout = TimeSpan.Zero;
				serverTimeout = TimeSpan.Zero;
				return;
			}
			int num3 = num2 - trackingAuthorityKindInformation.ExpectedConnectionLatencyMSec;
			clientTimeout = TimeSpan.FromMilliseconds((double)num2);
			serverTimeout = TimeSpan.FromMilliseconds((double)num3);
			TraceWrapper.SearchLibraryTracer.TraceError(this.GetHashCode(), "Timeouts calculated based on budget: clientTimeout={0}, serverTimeout={1}, elapsed={2}, total={3}", new object[]
			{
				num2,
				num3,
				num,
				this.timeBudgetAllowed.TotalMilliseconds
			});
		}

		// Token: 0x06001666 RID: 5734 RVA: 0x00068A84 File Offset: 0x00066C84
		public void IncrementBy(uint incrementValue)
		{
			if ((ulong)(this.budgetUsed + incrementValue) >= (ulong)((long)ServerCache.Instance.MaxTrackingEventBudgetForSingleQuery))
			{
				TrackingError trackingError = this.errors.Add(ErrorCode.BudgetExceeded, string.Empty, string.Empty, string.Empty);
				throw new TrackingFatalException(trackingError, null, true);
			}
			this.budgetUsed += incrementValue;
			Interlocked.Add(ref TrackingEventBudget.totalBudgetUsed, (int)incrementValue);
			if (TrackingEventBudget.totalBudgetUsed > ServerCache.Instance.MaxTrackingEventBudgetForAllQueries)
			{
				TrackingError trackingError2 = this.errors.Add(ErrorCode.TotalBudgetExceeded, string.Empty, string.Empty, string.Empty);
				throw new TrackingTransientException(trackingError2, null, true);
			}
		}

		// Token: 0x06001667 RID: 5735 RVA: 0x00068B1C File Offset: 0x00066D1C
		public void RestoreBudgetTo(uint budgetToRestoreTo)
		{
			if (budgetToRestoreTo > this.budgetUsed)
			{
				throw new InvalidOperationException("Restoring budget to a higher level than what is in use");
			}
			uint num = this.budgetUsed - budgetToRestoreTo;
			this.budgetUsed = budgetToRestoreTo;
			int num2 = 0;
			if (num > 0U)
			{
				num2 = Interlocked.Add(ref TrackingEventBudget.totalBudgetUsed, (int)(-(int)((ulong)num)));
			}
			if (num2 < 0)
			{
				throw new InvalidOperationException(string.Format("Total budget is decremented to {0}. budgetToRestoreTo is {1}, totalBudgetToReduce is {2}", num2, budgetToRestoreTo, num));
			}
		}

		// Token: 0x170005D8 RID: 1496
		// (get) Token: 0x06001668 RID: 5736 RVA: 0x00068B88 File Offset: 0x00066D88
		public uint BudgetUsed
		{
			get
			{
				return this.budgetUsed;
			}
		}

		// Token: 0x06001669 RID: 5737 RVA: 0x00068B90 File Offset: 0x00066D90
		public void TestDelayOperation(string operation)
		{
			if (ServerCache.Instance.IsTimeoutOverrideConfigured)
			{
				lock (TrackingEventBudget.TestOperationCount)
				{
					string text = ServerCache.TryReadRegistryKey<string>(operation, string.Empty);
					if (!string.IsNullOrEmpty(text))
					{
						string[] array = text.Split(new char[]
						{
							':'
						});
						int num = -1;
						HostId hostId = ServerCache.Instance.HostId;
						string y = Names<HostId>.Map[(int)hostId];
						if (array.Length != 2 || !StringComparer.OrdinalIgnoreCase.Equals(array[0], y) || !int.TryParse(array[1], out num))
						{
							TraceWrapper.SearchLibraryTracer.TraceError<string>(this.GetHashCode(), "TEST CODE: Invalid registry key: {0}", text);
						}
						else
						{
							int num2 = 0;
							if (!TrackingEventBudget.TestOperationCount.TryGetValue(operation, out num2))
							{
								num2 = 0;
							}
							num2++;
							TrackingEventBudget.TestOperationCount[operation] = num2;
							if (num2 > num)
							{
								TraceWrapper.SearchLibraryTracer.TraceDebug<string, int>(this.GetHashCode(), "TEST CODE: Delaying {0} operation, count={1}", operation, num2);
								TimeSpan elapsed = this.timer.Elapsed;
								if (elapsed < this.timeBudgetAllowed)
								{
									int num3 = (int)(this.timeBudgetAllowed - elapsed).TotalMilliseconds;
									TraceWrapper.SearchLibraryTracer.TraceDebug<int>(this.GetHashCode(), "TEST CODE: Pause injected, sleeping away remaining budget: {0} milliseconds", num3);
									Thread.Sleep(num3);
									TrackingEventBudget.TestOperationCount.Clear();
								}
								else
								{
									TraceWrapper.SearchLibraryTracer.TraceDebug<double>(this.GetHashCode(), "TEST CODE: Already over budget, sleep skipped. Elapsed time for this request: {0}", elapsed.TotalMilliseconds);
								}
							}
							else
							{
								TraceWrapper.SearchLibraryTracer.TraceDebug<int, string, int>(this.GetHashCode(), "TEST CODE: {0} {1} operations completed (will delay after {2})", num2, operation, num);
							}
						}
					}
				}
			}
		}

		// Token: 0x0600166A RID: 5738 RVA: 0x00068D4C File Offset: 0x00066F4C
		private bool IsUnderTimeBudget(out TrackingError trackingError)
		{
			trackingError = null;
			if (this.timer.Elapsed > this.timeBudgetAllowed)
			{
				TraceWrapper.SearchLibraryTracer.TraceError<long, double>(this.GetHashCode(), "Over time budget, used={0}, allowed={1}", this.timer.ElapsedMilliseconds, this.timeBudgetAllowed.TotalMilliseconds);
				trackingError = this.errors.Add(ErrorCode.TimeBudgetExceeded, string.Empty, string.Empty, string.Empty);
				return false;
			}
			return true;
		}

		// Token: 0x0600166B RID: 5739 RVA: 0x00068DC0 File Offset: 0x00066FC0
		private bool IsUnderResourceBudget()
		{
			if ((ulong)this.budgetUsed >= (ulong)((long)ServerCache.Instance.MaxTrackingEventBudgetForSingleQuery))
			{
				TraceWrapper.SearchLibraryTracer.TraceError<uint>(this.GetHashCode(), "Over single request budget, current resource consumption: {0}", this.budgetUsed);
				this.errors.Add(ErrorCode.BudgetExceeded, string.Empty, string.Empty, string.Empty);
				return false;
			}
			if (TrackingEventBudget.totalBudgetUsed >= ServerCache.Instance.MaxTrackingEventBudgetForAllQueries)
			{
				TraceWrapper.SearchLibraryTracer.TraceError<int>(this.GetHashCode(), "Over total budget, current total resource consumption: {0}", TrackingEventBudget.totalBudgetUsed);
				this.errors.Add(ErrorCode.TotalBudgetExceeded, string.Empty, string.Empty, string.Empty);
				return false;
			}
			return true;
		}

		// Token: 0x0600166C RID: 5740 RVA: 0x00068E64 File Offset: 0x00067064
		public DisposeTracker GetDisposeTracker()
		{
			this.disposeTracker = DisposeTracker.Get<TrackingEventBudget>(this);
			return this.disposeTracker;
		}

		// Token: 0x0600166D RID: 5741 RVA: 0x00068E78 File Offset: 0x00067078
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x0600166E RID: 5742 RVA: 0x00068E8D File Offset: 0x0006708D
		public void Dispose()
		{
			if (this.timer.IsRunning)
			{
				this.timer.Stop();
			}
			GC.SuppressFinalize(this);
		}

		// Token: 0x04000E78 RID: 3704
		public const string SearchRpcsBeforeDelay = "SearchRpcsBeforeDelay";

		// Token: 0x04000E79 RID: 3705
		public const string GetRpcsBeforeDelay = "GetRpcsBeforeDelay";

		// Token: 0x04000E7A RID: 3706
		public const string EwsCallsBeforeDelay = "EwsCallsBeforeDelay";

		// Token: 0x04000E7B RID: 3707
		public const uint DefaultWebServiceFindMessageTrackingReportCost = 10U;

		// Token: 0x04000E7C RID: 3708
		private static Dictionary<string, int> TestOperationCount = new Dictionary<string, int>(3);

		// Token: 0x04000E7D RID: 3709
		private static int currentSearchingThreadCount = 0;

		// Token: 0x04000E7E RID: 3710
		private TimeSpan timeBudgetAllowed;

		// Token: 0x04000E7F RID: 3711
		private static int totalBudgetUsed;

		// Token: 0x04000E80 RID: 3712
		private uint budgetUsed;

		// Token: 0x04000E81 RID: 3713
		private TrackingErrorCollection errors;

		// Token: 0x04000E82 RID: 3714
		private Stopwatch timer;

		// Token: 0x04000E83 RID: 3715
		private DisposeTracker disposeTracker;
	}
}
