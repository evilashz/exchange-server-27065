using System;
using Microsoft.Exchange.Configuration.Core;
using Microsoft.Exchange.Diagnostics.Components.FailFast;

namespace Microsoft.Exchange.Configuration.FailFast
{
	// Token: 0x0200000D RID: 13
	internal class PassiveFailFastUserCache : FailFastUserCache
	{
		// Token: 0x06000042 RID: 66 RVA: 0x000030E9 File Offset: 0x000012E9
		private PassiveFailFastUserCache() : base(1, 1)
		{
			this.passiveObjectBehavior = new CrossAppDomainPassiveObjectBehavior(FailFastUserCache.PipeNameOfThisProcess, BehaviorDirection.Out);
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000043 RID: 67 RVA: 0x00003104 File Offset: 0x00001304
		internal static PassiveFailFastUserCache Singleton
		{
			get
			{
				return PassiveFailFastUserCache.singleton;
			}
		}

		// Token: 0x06000044 RID: 68 RVA: 0x0000310B File Offset: 0x0000130B
		internal override bool IsUserInCache(string userToken, string userTenant, out string cacheKey, out FailFastUserCacheValue cacheValue, out BlockedReason blockedReason)
		{
			throw new NotSupportedException("The IsUserBlocked should not be invoked from PassiveFailFastUserCache.");
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00003118 File Offset: 0x00001318
		protected override void InsertValueToCache(string key, BlockedType blockedType, TimeSpan blockedTime)
		{
			if (!FailFastUserCache.FailFastEnabled)
			{
				return;
			}
			Logger.EnterFunction(ExTraceGlobals.FailFastCacheTracer, "PassiveFailFastUserCache.InsertValueToCache");
			long num = blockedTime.Ticks;
			if (num < 0L)
			{
				Logger.TraceDebug(ExTraceGlobals.FailFastCacheTracer, "Blocked time ticks {0} < 0", new object[]
				{
					num
				});
				num = 0L;
			}
			string text = string.Concat(new object[]
			{
				key,
				';',
				blockedType,
				';',
				num
			});
			byte[] bytes = FailFastUserCache.Encoding.GetBytes(text);
			Logger.TraceInformation(ExTraceGlobals.FailFastCacheTracer, "Send the blocked user info {0} to server stream.", new object[]
			{
				text
			});
			this.passiveObjectBehavior.SendMessage(bytes);
			Logger.ExitFunction(ExTraceGlobals.FailFastCacheTracer, "PassiveFailFastUserCache.InsertValueToCache");
		}

		// Token: 0x0400002C RID: 44
		private static PassiveFailFastUserCache singleton = new PassiveFailFastUserCache();

		// Token: 0x0400002D RID: 45
		private readonly CrossAppDomainPassiveObjectBehavior passiveObjectBehavior;
	}
}
