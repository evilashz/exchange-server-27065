using System;
using Microsoft.Exchange.Collections.TimeoutCache;
using Microsoft.Exchange.Diagnostics.Components.FailFast;

namespace Microsoft.Exchange.Configuration.FailFast
{
	// Token: 0x02000004 RID: 4
	internal static class ConnectedUserManager
	{
		// Token: 0x06000006 RID: 6 RVA: 0x000021D0 File Offset: 0x000003D0
		internal static bool ShouldFailFastUserInCache(string userToken, string cacheKey, FailFastUserCacheValue cacheValue, BlockedReason blockedReason)
		{
			Logger.EnterFunction(ExTraceGlobals.FailFastModuleTracer, "ConnectedUserManager.ShouldFailFastUserInCache");
			if (blockedReason == BlockedReason.BySelf)
			{
				if (cacheValue.HitCount >= 3)
				{
					Logger.TraceDebug(ExTraceGlobals.FailFastModuleTracer, "User {0} is considered fail-fast because blocked reason is Self. cacheValue: {1}", new object[]
					{
						userToken,
						cacheValue
					});
					return true;
				}
				return false;
			}
			else
			{
				bool flag;
				if (!ConnectedUserManager.connectedUserCache.TryGetValue(userToken, out flag))
				{
					Logger.TraceDebug(ExTraceGlobals.FailFastModuleTracer, "User {0} is considered fail-fast because he is not in connected user cache.", new object[]
					{
						userToken
					});
					return true;
				}
				Logger.ExitFunction(ExTraceGlobals.FailFastModuleTracer, "ConnectedUserManager.ShouldFailFastUserInCache");
				return false;
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002258 File Offset: 0x00000458
		internal static void RemoveUser(string userToken)
		{
			if (ConnectedUserManager.connectedUserCache.Remove(userToken))
			{
				Logger.TraceDebug(ExTraceGlobals.FailFastModuleTracer, "User {0} is removed from connected user cache.", new object[]
				{
					userToken
				});
			}
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002290 File Offset: 0x00000490
		internal static void AddUser(string userToken)
		{
			ConnectedUserManager.connectedUserCache.InsertSliding(userToken, true, ConnectedUserManager.DefaultTimeOutForConnectedUser, new RemoveItemDelegate<string, bool>(ConnectedUserManager.OnUserRemovedFromCache));
			Logger.TraceDebug(ExTraceGlobals.FailFastModuleTracer, "User {0} is iserted into connected user cache.", new object[]
			{
				userToken
			});
			FailFastModule.RemotePowershellPerfCounter.ConnectedUserCacheSize.RawValue = (long)ConnectedUserManager.connectedUserCache.Count;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000022F0 File Offset: 0x000004F0
		internal static void RefreshUser(string userToken)
		{
			bool flag;
			if (ConnectedUserManager.connectedUserCache.TryGetValue(userToken, out flag))
			{
				Logger.TraceDebug(ExTraceGlobals.FailFastModuleTracer, "User {0} is refreshed in connected user cache.", new object[]
				{
					userToken
				});
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002328 File Offset: 0x00000528
		private static void OnUserRemovedFromCache(string userToken, bool value, RemoveReason reason)
		{
			FailFastModule.RemotePowershellPerfCounter.ConnectedUserCacheSize.RawValue = (long)ConnectedUserManager.connectedUserCache.Count;
			Logger.TraceDebug(ExTraceGlobals.FailFastModuleTracer, "User {0} is removed from connected user cache. Reason: {1}", new object[]
			{
				userToken,
				reason
			});
		}

		// Token: 0x04000002 RID: 2
		private const int ThresholdToConvertToBadUser = 3;

		// Token: 0x04000003 RID: 3
		private static readonly TimeoutCache<string, bool> connectedUserCache = new TimeoutCache<string, bool>(20, 5000, false);

		// Token: 0x04000004 RID: 4
		private static readonly TimeSpan DefaultTimeOutForConnectedUser = TimeSpan.FromMinutes(5.0);
	}
}
