using System;
using System.Configuration;
using System.Threading;
using Microsoft.Exchange.Collections.TimeoutCache;
using Microsoft.Exchange.Diagnostics.Components.FailFast;

namespace Microsoft.Exchange.Configuration.FailFast
{
	// Token: 0x0200000A RID: 10
	internal static class FailureThrottling
	{
		// Token: 0x06000032 RID: 50 RVA: 0x00002CF0 File Offset: 0x00000EF0
		static FailureThrottling()
		{
			if (!int.TryParse(ConfigurationManager.AppSettings["FailureThrottlingLimit"], out FailureThrottling.failureThrottlingLimit))
			{
				FailureThrottling.failureThrottlingLimit = 15;
			}
			Logger.TraceDebug(ExTraceGlobals.FailureThrottlingTracer, "failureThrottlingLimit = {0}.", new object[]
			{
				FailureThrottling.failureThrottlingLimit
			});
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002D68 File Offset: 0x00000F68
		internal static bool CountBasedOnStatusCode(string userToken, int statusCode)
		{
			int num;
			if (statusCode != 200)
			{
				if (statusCode != 400 && statusCode != 500)
				{
					return false;
				}
				num = 1;
			}
			else
			{
				num = -1;
			}
			FailureThrottling.FailureCounter counter = FailureThrottling.GetCounter(userToken);
			counter.AddDelta(num);
			Logger.TraceDebug(ExTraceGlobals.FailureThrottlingTracer, "User {0} counter changed to be {1}. Current status code is {2}.", new object[]
			{
				userToken,
				counter.Value,
				statusCode
			});
			if (num == 1 && counter.Value > FailureThrottling.failureThrottlingLimit)
			{
				Logger.TraceDebug(ExTraceGlobals.FailureThrottlingTracer, "User {0} is OverBudget. Counter: {1}. FailureThrottlingLimit: {2}", new object[]
				{
					userToken,
					counter.Value,
					FailureThrottling.failureThrottlingLimit
				});
				return true;
			}
			return false;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002E2C File Offset: 0x0000102C
		private static FailureThrottling.FailureCounter GetCounter(string userToken)
		{
			Logger.EnterFunction(ExTraceGlobals.FailureThrottlingTracer, "FailureThrottlingUserCache.GetCounter");
			FailureThrottling.FailureCounter failureCounter;
			FailureThrottling.failureThrottlingUserCache.TryGetValue(userToken, out failureCounter);
			if (failureCounter == null || !failureCounter.IsValid)
			{
				Logger.TraceDebug(ExTraceGlobals.FailureThrottlingTracer, "Create counter for User {0}.", new object[]
				{
					userToken
				});
				failureCounter = new FailureThrottling.FailureCounter();
				FailureThrottling.failureThrottlingUserCache.InsertAbsolute(userToken, failureCounter, FailureThrottling.FailureThrottlingTimePeriod, new RemoveItemDelegate<string, FailureThrottling.FailureCounter>(FailureThrottling.OnUserRemovedFromCache));
				FailFastModule.RemotePowershellPerfCounter.FailureThrottlingUserCacheSize.RawValue = (long)FailureThrottling.failureThrottlingUserCache.Count;
			}
			Logger.ExitFunction(ExTraceGlobals.FailureThrottlingTracer, "FailureThrottlingUserCache.GetCounter");
			return failureCounter;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002ECC File Offset: 0x000010CC
		private static void OnUserRemovedFromCache(string userToken, FailureThrottling.FailureCounter value, RemoveReason reason)
		{
			Logger.TraceDebug(ExTraceGlobals.FailureThrottlingTracer, "User {0} is removed from failure throttling user cache. Reason: {1}", new object[]
			{
				userToken,
				reason
			});
			FailFastModule.RemotePowershellPerfCounter.FailureThrottlingUserCacheSize.RawValue = (long)FailureThrottling.failureThrottlingUserCache.Count;
		}

		// Token: 0x04000020 RID: 32
		private static readonly TimeoutCache<string, FailureThrottling.FailureCounter> failureThrottlingUserCache = new TimeoutCache<string, FailureThrottling.FailureCounter>(20, 5000, false);

		// Token: 0x04000021 RID: 33
		private static readonly TimeSpan FailureThrottlingTimePeriod = TimeSpan.FromSeconds(10.0);

		// Token: 0x04000022 RID: 34
		private static readonly int failureThrottlingLimit;

		// Token: 0x0200000B RID: 11
		private class FailureCounter
		{
			// Token: 0x06000036 RID: 54 RVA: 0x00002F17 File Offset: 0x00001117
			internal FailureCounter()
			{
				this.addedTime = DateTime.UtcNow;
			}

			// Token: 0x1700000E RID: 14
			// (get) Token: 0x06000037 RID: 55 RVA: 0x00002F2A File Offset: 0x0000112A
			internal int Value
			{
				get
				{
					return this.value;
				}
			}

			// Token: 0x1700000F RID: 15
			// (get) Token: 0x06000038 RID: 56 RVA: 0x00002F32 File Offset: 0x00001132
			internal bool IsValid
			{
				get
				{
					return this.addedTime + FailureThrottling.FailureThrottlingTimePeriod > DateTime.UtcNow;
				}
			}

			// Token: 0x06000039 RID: 57 RVA: 0x00002F4E File Offset: 0x0000114E
			internal void AddDelta(int delta)
			{
				Interlocked.Add(ref this.value, delta);
			}

			// Token: 0x04000023 RID: 35
			private readonly DateTime addedTime;

			// Token: 0x04000024 RID: 36
			private int value;
		}
	}
}
