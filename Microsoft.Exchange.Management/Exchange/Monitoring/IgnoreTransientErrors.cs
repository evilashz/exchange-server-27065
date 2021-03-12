using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000544 RID: 1348
	internal static class IgnoreTransientErrors
	{
		// Token: 0x06003031 RID: 12337 RVA: 0x000C36E4 File Offset: 0x000C18E4
		public static bool IgnoreTransientError(string errorKey, uint transientErrorWindowMinutes, ErrorType errorType, out bool isTransitioningState)
		{
			bool flag = false;
			long num = (long)((ulong)(transientErrorWindowMinutes * 60U));
			IgnoreTransientErrors.CheckFailureInfo checkFailureInfo;
			if (IgnoreTransientErrors.Failures.TryGetValue(errorKey, out checkFailureInfo))
			{
				checkFailureInfo.NumSuccessiveFailures += 1U;
				if (checkFailureInfo.LatestPass >= checkFailureInfo.StartOfFailures)
				{
					checkFailureInfo.StartOfFailures = DateTime.UtcNow;
				}
				if (num == 0L)
				{
					ExTraceGlobals.HealthChecksTracer.TraceDebug((long)IgnoreTransientErrors.Failures.GetHashCode(), "IgnoreTransientError(): Error with key '{0}' is _NOT_ being ignored. It has now occurred {1} successive times, which exceeds the maximum {2} secs. Last pass time is: {3}. Start of Failures is: {4}. FailedDuration is: {5} secs. ", new object[]
					{
						errorKey,
						checkFailureInfo.NumSuccessiveFailures,
						num,
						checkFailureInfo.LatestPass,
						checkFailureInfo.StartOfFailures,
						checkFailureInfo.FailedDurationSeconds
					});
				}
				else if (checkFailureInfo.FailedDurationSeconds <= num)
				{
					ExTraceGlobals.HealthChecksTracer.TraceDebug((long)IgnoreTransientErrors.Failures.GetHashCode(), "IgnoreTransientError(): Error with key '{0}' is being ignored. It has now occurred {1} successive times. Last pass time is: {2}. Start of Failures is: {3}. FailedDuration is: {4} secs. Threshold is: {5} secs.", new object[]
					{
						errorKey,
						checkFailureInfo.NumSuccessiveFailures,
						checkFailureInfo.LatestPass,
						checkFailureInfo.StartOfFailures,
						checkFailureInfo.FailedDurationSeconds,
						num
					});
					flag = true;
				}
				else
				{
					ExTraceGlobals.HealthChecksTracer.TraceDebug((long)IgnoreTransientErrors.Failures.GetHashCode(), "IgnoreTransientError(): Error with key '{0}' is _NOT_ being ignored. It has now occurred {1} successive times, which exceeds the maximum {2} secs. Last pass time is: {3}. Start of Failures is: {4}. FailedDuration is: {5} secs.", new object[]
					{
						errorKey,
						checkFailureInfo.NumSuccessiveFailures,
						num,
						checkFailureInfo.LatestPass,
						checkFailureInfo.StartOfFailures,
						checkFailureInfo.FailedDurationSeconds
					});
				}
				if (checkFailureInfo.ErrorType != errorType || checkFailureInfo.LastIgnoreTransientErrorValue != flag)
				{
					isTransitioningState = true;
					ExTraceGlobals.HealthChecksTracer.TraceDebug((long)IgnoreTransientErrors.Failures.GetHashCode(), "IgnoreTransientError(): Setting isTransitioningState={0} for errorKey '{1}'. LastIgnoreTransientErrorValue is: {2}. Current IgnoreTransientErrorValue is: {3}. Last ErrorType is: {4}. Current ErrorType is: {5}.", new object[]
					{
						isTransitioningState,
						errorKey,
						checkFailureInfo.LastIgnoreTransientErrorValue,
						flag,
						checkFailureInfo.ErrorType,
						errorType
					});
				}
				else
				{
					isTransitioningState = false;
				}
				checkFailureInfo.ErrorType = errorType;
			}
			else
			{
				isTransitioningState = true;
				ExTraceGlobals.HealthChecksTracer.TraceDebug<string, bool>((long)IgnoreTransientErrors.Failures.GetHashCode(), "IgnoreTransientError(): First time recording error with key '{0}'. Returning isTransitioningState={1}.", errorKey, isTransitioningState);
				checkFailureInfo = new IgnoreTransientErrors.CheckFailureInfo(DateTime.UtcNow, errorType);
				IgnoreTransientErrors.Failures[errorKey] = checkFailureInfo;
				if (num == 0L)
				{
					ExTraceGlobals.HealthChecksTracer.TraceDebug((long)IgnoreTransientErrors.Failures.GetHashCode(), "IgnoreTransientError(): Error with key '{0}' is _NOT_ being ignored. It has now occurred {1} successive times, which exceeds the maximum {2} secs. Last pass time is: {3}. Start of Failures is: {4}. FailedDuration is: {5} secs. ", new object[]
					{
						errorKey,
						checkFailureInfo.NumSuccessiveFailures,
						num,
						checkFailureInfo.LatestPass,
						checkFailureInfo.StartOfFailures,
						checkFailureInfo.FailedDurationSeconds
					});
				}
				else if (checkFailureInfo.FailedDurationSeconds <= num)
				{
					ExTraceGlobals.HealthChecksTracer.TraceDebug((long)IgnoreTransientErrors.Failures.GetHashCode(), "IgnoreTransientError(): Error with key '{0}' is being ignored. It has now occurred {1} successive times. Last pass time is: {2}. Start of Failures is: {3}. FailedDuration is: {4} secs. Threshold is: {5} secs.", new object[]
					{
						errorKey,
						checkFailureInfo.NumSuccessiveFailures,
						checkFailureInfo.LatestPass,
						checkFailureInfo.StartOfFailures,
						checkFailureInfo.FailedDurationSeconds,
						num
					});
					flag = true;
				}
				else
				{
					ExTraceGlobals.HealthChecksTracer.TraceDebug((long)IgnoreTransientErrors.Failures.GetHashCode(), "IgnoreTransientError(): Error with key '{0}' is _NOT_ being ignored. It has now occurred {1} successive times, which exceeds the maximum {2} secs. Last pass time is: {3}. Start of Failures is: {4}. FailedDuration is: {5} secs. ", new object[]
					{
						errorKey,
						checkFailureInfo.NumSuccessiveFailures,
						num,
						checkFailureInfo.LatestPass,
						checkFailureInfo.StartOfFailures,
						checkFailureInfo.FailedDurationSeconds
					});
				}
			}
			checkFailureInfo.LastIgnoreTransientErrorValue = flag;
			return flag;
		}

		// Token: 0x06003032 RID: 12338 RVA: 0x000C3AC4 File Offset: 0x000C1CC4
		public static bool ResetError(string errorKey)
		{
			bool flag = false;
			IgnoreTransientErrors.CheckFailureInfo checkFailureInfo;
			if (IgnoreTransientErrors.Failures.TryGetValue(errorKey, out checkFailureInfo))
			{
				ExTraceGlobals.HealthChecksTracer.TraceDebug<string>((long)IgnoreTransientErrors.Failures.GetHashCode(), "ResetError(): Resetting error with key '{0}'.", errorKey);
				if (checkFailureInfo.ErrorType != ErrorType.Success)
				{
					flag = true;
				}
				checkFailureInfo.NumSuccessiveFailures = 0U;
				checkFailureInfo.LatestPass = DateTime.UtcNow;
				checkFailureInfo.ErrorType = ErrorType.Success;
			}
			else
			{
				flag = true;
				ExTraceGlobals.HealthChecksTracer.TraceDebug<string, bool>((long)IgnoreTransientErrors.Failures.GetHashCode(), "ResetError(): First time recording error with key '{0}'. Returning isTransitioningState={1}.", errorKey, flag);
				ExTraceGlobals.HealthChecksTracer.TraceDebug<string>((long)IgnoreTransientErrors.Failures.GetHashCode(), "ResetError(): Error with key '{0}' has not been logged before. This means it hasn't failed or issued a warning before.", errorKey);
				checkFailureInfo = new IgnoreTransientErrors.CheckFailureInfo(DateTime.UtcNow);
				IgnoreTransientErrors.Failures[errorKey] = checkFailureInfo;
			}
			return flag;
		}

		// Token: 0x06003033 RID: 12339 RVA: 0x000C3B74 File Offset: 0x000C1D74
		public static bool HasPassed(string errorKey)
		{
			IgnoreTransientErrors.CheckFailureInfo checkFailureInfo;
			if (IgnoreTransientErrors.Failures.TryGetValue(errorKey, out checkFailureInfo))
			{
				ExTraceGlobals.HealthChecksTracer.TraceDebug<string, uint>((long)IgnoreTransientErrors.Failures.GetHashCode(), "HasPassed(): Error with key '{0}' has been logged before. NumSuccessiveFailures = {1}.", errorKey, checkFailureInfo.NumSuccessiveFailures);
				return checkFailureInfo.NumSuccessiveFailures == 0U;
			}
			ExTraceGlobals.HealthChecksTracer.TraceDebug<string>((long)IgnoreTransientErrors.Failures.GetHashCode(), "HasPassed(): Error with key '{0}' has not been logged before. This means it hasn't failed or issued a warning before.", errorKey);
			return true;
		}

		// Token: 0x06003034 RID: 12340 RVA: 0x000C3BD8 File Offset: 0x000C1DD8
		internal static bool ContainsKey(string errorKey)
		{
			IgnoreTransientErrors.CheckFailureInfo checkFailureInfo;
			return IgnoreTransientErrors.Failures.TryGetValue(errorKey, out checkFailureInfo);
		}

		// Token: 0x04002244 RID: 8772
		public const uint DefaultSuccessiveErrorsThreshold = 3U;

		// Token: 0x04002245 RID: 8773
		private static Dictionary<string, IgnoreTransientErrors.CheckFailureInfo> Failures = new Dictionary<string, IgnoreTransientErrors.CheckFailureInfo>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x02000545 RID: 1349
		internal class CheckFailureInfo
		{
			// Token: 0x06003035 RID: 12341 RVA: 0x000C3BF2 File Offset: 0x000C1DF2
			public CheckFailureInfo(DateTime latestPass)
			{
				this.m_numSuccessiveFailures = 0U;
				this.m_latestPass = latestPass;
				this.m_lastIgnoreTransientErrorValue = false;
				this.m_errorType = ErrorType.Success;
			}

			// Token: 0x06003036 RID: 12342 RVA: 0x000C3C16 File Offset: 0x000C1E16
			public CheckFailureInfo(DateTime startOfFailures, ErrorType errorType)
			{
				this.m_numSuccessiveFailures = 1U;
				this.m_startOfFailures = startOfFailures;
				this.m_lastIgnoreTransientErrorValue = false;
				this.m_errorType = errorType;
			}

			// Token: 0x17000E43 RID: 3651
			// (get) Token: 0x06003037 RID: 12343 RVA: 0x000C3C3A File Offset: 0x000C1E3A
			// (set) Token: 0x06003038 RID: 12344 RVA: 0x000C3C42 File Offset: 0x000C1E42
			public uint NumSuccessiveFailures
			{
				get
				{
					return this.m_numSuccessiveFailures;
				}
				set
				{
					this.m_numSuccessiveFailures = value;
				}
			}

			// Token: 0x17000E44 RID: 3652
			// (get) Token: 0x06003039 RID: 12345 RVA: 0x000C3C4B File Offset: 0x000C1E4B
			// (set) Token: 0x0600303A RID: 12346 RVA: 0x000C3C53 File Offset: 0x000C1E53
			public DateTime LatestPass
			{
				get
				{
					return this.m_latestPass;
				}
				set
				{
					this.m_latestPass = value;
				}
			}

			// Token: 0x17000E45 RID: 3653
			// (get) Token: 0x0600303B RID: 12347 RVA: 0x000C3C5C File Offset: 0x000C1E5C
			// (set) Token: 0x0600303C RID: 12348 RVA: 0x000C3C64 File Offset: 0x000C1E64
			public DateTime StartOfFailures
			{
				get
				{
					return this.m_startOfFailures;
				}
				set
				{
					this.m_startOfFailures = value;
				}
			}

			// Token: 0x17000E46 RID: 3654
			// (get) Token: 0x0600303D RID: 12349 RVA: 0x000C3C6D File Offset: 0x000C1E6D
			// (set) Token: 0x0600303E RID: 12350 RVA: 0x000C3C75 File Offset: 0x000C1E75
			public bool LastIgnoreTransientErrorValue
			{
				get
				{
					return this.m_lastIgnoreTransientErrorValue;
				}
				set
				{
					this.m_lastIgnoreTransientErrorValue = value;
				}
			}

			// Token: 0x17000E47 RID: 3655
			// (get) Token: 0x0600303F RID: 12351 RVA: 0x000C3C7E File Offset: 0x000C1E7E
			// (set) Token: 0x06003040 RID: 12352 RVA: 0x000C3C86 File Offset: 0x000C1E86
			public ErrorType ErrorType
			{
				get
				{
					return this.m_errorType;
				}
				set
				{
					this.m_errorType = value;
				}
			}

			// Token: 0x17000E48 RID: 3656
			// (get) Token: 0x06003041 RID: 12353 RVA: 0x000C3C90 File Offset: 0x000C1E90
			public long FailedDurationSeconds
			{
				get
				{
					if (!(this.m_startOfFailures > DateTime.MinValue))
					{
						return 0L;
					}
					if (this.m_latestPass > this.m_startOfFailures)
					{
						return 0L;
					}
					return Convert.ToInt64(Math.Ceiling(DateTime.UtcNow.Subtract(this.m_startOfFailures).TotalSeconds));
				}
			}

			// Token: 0x04002246 RID: 8774
			private uint m_numSuccessiveFailures;

			// Token: 0x04002247 RID: 8775
			private DateTime m_latestPass;

			// Token: 0x04002248 RID: 8776
			private DateTime m_startOfFailures;

			// Token: 0x04002249 RID: 8777
			private bool m_lastIgnoreTransientErrorValue;

			// Token: 0x0400224A RID: 8778
			private ErrorType m_errorType;
		}
	}
}
