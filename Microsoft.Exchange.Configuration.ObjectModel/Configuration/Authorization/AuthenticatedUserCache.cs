using System;
using System.Security.Principal;
using Microsoft.Exchange.Collections.TimeoutCache;
using Microsoft.Exchange.Configuration.Core;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Authorization;

namespace Microsoft.Exchange.Configuration.Authorization
{
	// Token: 0x0200021D RID: 541
	internal class AuthenticatedUserCache : TimeoutCache<string, AuthZPluginUserToken>
	{
		// Token: 0x060012D2 RID: 4818 RVA: 0x0003D279 File Offset: 0x0003B479
		private AuthenticatedUserCache() : base(20, 5000, false)
		{
		}

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x060012D3 RID: 4819 RVA: 0x0003D289 File Offset: 0x0003B489
		internal static AuthenticatedUserCache Instance
		{
			get
			{
				return AuthenticatedUserCache.instance;
			}
		}

		// Token: 0x060012D4 RID: 4820 RVA: 0x0003D290 File Offset: 0x0003B490
		internal static string CreateKeyForPsws(SecurityIdentifier userSid, Microsoft.Exchange.Configuration.Core.AuthenticationType authenticationType, PartitionId partitionId)
		{
			ExAssert.RetailAssert(userSid != null, "The user sid is invalid (null).");
			return string.Format("{0}:{1}:{2}", authenticationType, partitionId, userSid.Value);
		}

		// Token: 0x060012D5 RID: 4821 RVA: 0x0003D2BC File Offset: 0x0003B4BC
		internal void AddUserToCache(string key, AuthZPluginUserToken userToken)
		{
			ExTraceGlobals.PublicPluginAPITracer.TraceDebug<string, string>(0L, "Add user token {0} with key {1} to cache.", userToken.UserName, key.ToString());
			base.InsertAbsolute(key, userToken, AuthenticatedUserCache.DefaultTimeOut, new RemoveItemDelegate<string, AuthZPluginUserToken>(this.OnUserRemovedFromCache));
			RemotePowershellPerformanceCountersInstance remotePowershellPerfCounter = ExchangeAuthorizationPlugin.RemotePowershellPerfCounter;
			if (remotePowershellPerfCounter != null)
			{
				remotePowershellPerfCounter.AuthenticatedUserCacheSize.RawValue = (long)base.Count;
			}
		}

		// Token: 0x060012D6 RID: 4822 RVA: 0x0003D31C File Offset: 0x0003B51C
		private void OnUserRemovedFromCache(string key, AuthZPluginUserToken userToken, RemoveReason reason)
		{
			ExTraceGlobals.PublicPluginAPITracer.TraceDebug<string, string, RemoveReason>(0L, "User token {0} with key {1} removed from cache. Reason: {2}.", userToken.UserName, key.ToString(), reason);
			RemotePowershellPerformanceCountersInstance remotePowershellPerfCounter = ExchangeAuthorizationPlugin.RemotePowershellPerfCounter;
			if (remotePowershellPerfCounter != null)
			{
				remotePowershellPerfCounter.AuthenticatedUserCacheSize.RawValue = (long)base.Count;
			}
		}

		// Token: 0x040004AF RID: 1199
		private const string CacheKeyStringFormat = "{0}:{1}";

		// Token: 0x040004B0 RID: 1200
		private const string UserNameFormatForCertDetails = "ISS:{0};SUB:{1}";

		// Token: 0x040004B1 RID: 1201
		private const string PswsCacheKeyStringFormat = "{0}:{1}:{2}";

		// Token: 0x040004B2 RID: 1202
		private static readonly AuthenticatedUserCache instance = new AuthenticatedUserCache();

		// Token: 0x040004B3 RID: 1203
		private static readonly TimeSpan DefaultTimeOut = new TimeSpan(0, 5, 0);
	}
}
