using System;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Security.Authentication.FederatedAuthService;

namespace Microsoft.Exchange.Security.Authentication
{
	// Token: 0x02000054 RID: 84
	internal static class AuthModulePerformanceCounterHelper
	{
		// Token: 0x06000244 RID: 580 RVA: 0x0000DF60 File Offset: 0x0000C160
		public static void Initialize(string protocol)
		{
			AuthModulePerformanceCounterHelper.counters = LiveIdBasicAuthenticationCounters.GetInstance(protocol);
			AuthModulePerformanceCounterHelper.percentageLogonCacheHitLastMinute = new SlidingPercentageCounter(TimeSpan.FromMinutes(1.0), TimeSpan.FromSeconds(1.0));
			AuthModulePerformanceCounterHelper.percentageCookieHitLastMinute = new SlidingPercentageCounter(TimeSpan.FromMinutes(1.0), TimeSpan.FromSeconds(1.0));
			AuthModulePerformanceCounterHelper.percentileCountersTimer = new Timer(new TimerCallback(AuthModulePerformanceCounterHelper.UpdatePercentileCounters));
			AuthModulePerformanceCounterHelper.percentileCountersTimer.Change(60000, -1);
		}

		// Token: 0x06000245 RID: 581 RVA: 0x0000DFEC File Offset: 0x0000C1EC
		private static void UpdatePercentileCounters(object state)
		{
			AuthModulePerformanceCounterHelper.percentageCookieHitLastMinute.GetSlidingPercentage();
			AuthModulePerformanceCounterHelper.counters.NumberOfRequestsLastMinute.RawValue = AuthModulePerformanceCounterHelper.percentageCookieHitLastMinute.Denominator;
			if (AuthModulePerformanceCounterHelper.counters.NumberOfRequestsLastMinute.RawValue < 2L)
			{
				AuthModulePerformanceCounterHelper.counters.LogonCacheHit.RawValue = 0L;
				AuthModulePerformanceCounterHelper.counters.PercentageOfCookieBasedAuth.RawValue = 0L;
			}
			else
			{
				AuthModulePerformanceCounterHelper.counters.LogonCacheHit.RawValue = (long)((int)AuthModulePerformanceCounterHelper.percentageLogonCacheHitLastMinute.GetSlidingPercentage());
				AuthModulePerformanceCounterHelper.counters.PercentageOfCookieBasedAuth.RawValue = (long)((int)AuthModulePerformanceCounterHelper.percentageCookieHitLastMinute.GetSlidingPercentage());
			}
			AuthModulePerformanceCounterHelper.percentileCountersTimer.Dispose();
			AuthModulePerformanceCounterHelper.percentileCountersTimer = new Timer(new TimerCallback(AuthModulePerformanceCounterHelper.UpdatePercentileCounters));
			AuthModulePerformanceCounterHelper.percentileCountersTimer.Change(60000, -1);
		}

		// Token: 0x04000200 RID: 512
		private const int numberOfTotalRequestsToIgnoreInPercentileCounter = 2;

		// Token: 0x04000201 RID: 513
		private const int percentileCountersUpdateIntervalInSeconds = 60;

		// Token: 0x04000202 RID: 514
		private static Timer percentileCountersTimer = null;

		// Token: 0x04000203 RID: 515
		internal static SlidingPercentageCounter percentageLogonCacheHitLastMinute = null;

		// Token: 0x04000204 RID: 516
		internal static SlidingPercentageCounter percentageCookieHitLastMinute = null;

		// Token: 0x04000205 RID: 517
		internal static LiveIdBasicAuthenticationCountersInstance counters;
	}
}
