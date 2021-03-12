using System;
using System.Collections.Concurrent;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Services.OnlineMeetings.Autodiscover
{
	// Token: 0x0200002B RID: 43
	internal class AutodiscoverCache
	{
		// Token: 0x1700007B RID: 123
		// (get) Token: 0x0600018D RID: 397 RVA: 0x00005DD7 File Offset: 0x00003FD7
		internal static ConcurrentDictionary<string, string> DomainCache
		{
			get
			{
				return AutodiscoverCache.perDomainEndpointCache;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600018E RID: 398 RVA: 0x00005DDE File Offset: 0x00003FDE
		internal static ConcurrentDictionary<string, AutodiscoverCacheEntry> UserCache
		{
			get
			{
				return AutodiscoverCache.perUserUcwaUrlCache;
			}
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00005DE8 File Offset: 0x00003FE8
		public static bool ContainsDomain(string domain)
		{
			string text;
			return AutodiscoverCache.DomainCache.TryGetValue(domain, out text);
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00005E04 File Offset: 0x00004004
		public static string GetValueForDomain(string domain)
		{
			string result;
			if (AutodiscoverCache.DomainCache.TryGetValue(domain, out result))
			{
				return result;
			}
			return string.Empty;
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00005E38 File Offset: 0x00004038
		public static void UpdateDomain(string domain, string endpoint)
		{
			AutodiscoverCache.DomainCache.AddOrUpdate(domain, endpoint, (string key, string value) => endpoint);
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00005E70 File Offset: 0x00004070
		public static bool InvalidateDomain(string domain)
		{
			bool result;
			lock (AutodiscoverCache.lockObject)
			{
				string arg;
				if (AutodiscoverCache.DomainCache.TryRemove(domain, out arg))
				{
					ExTraceGlobals.OnlineMeetingTracer.TraceInformation<string, string>(0, 0L, "[AutodiscoverCache][InvalidateDomain] Removed entry from per domain cache: key '{0}' with value '{1}'", domain, arg);
					string[] array = new string[AutodiscoverCache.UserCache.Keys.Count];
					AutodiscoverCache.UserCache.Keys.CopyTo(array, 0);
					foreach (string sipAddress in array)
					{
						if (string.Compare(domain, AutodiscoverCache.GetDomain(sipAddress), StringComparison.CurrentCultureIgnoreCase) == 0)
						{
							AutodiscoverCache.InvalidateUserImpl(sipAddress);
						}
					}
					result = true;
				}
				else
				{
					ExTraceGlobals.OnlineMeetingTracer.TraceDebug<string>(0, 0L, "[AutodiscoverCache][InvalidateDomain] Unable to find entry for '{0}' in per domain cache", domain);
					result = false;
				}
			}
			return result;
		}

		// Token: 0x06000193 RID: 403 RVA: 0x00005F44 File Offset: 0x00004144
		public static bool ContainsUser(string user)
		{
			string text;
			return AutodiscoverCache.TryGetUserUcwaUserUrl(user, out text);
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00005F5C File Offset: 0x0000415C
		public static string GetValueForUser(string user)
		{
			string result;
			AutodiscoverCache.TryGetUserUcwaUserUrl(user, out result);
			return result;
		}

		// Token: 0x06000195 RID: 405 RVA: 0x00005F74 File Offset: 0x00004174
		private static bool TryGetUserUcwaUserUrl(string user, out string ucwaUrl)
		{
			ucwaUrl = string.Empty;
			AutodiscoverCacheEntry autodiscoverCacheEntry;
			if (!AutodiscoverCache.UserCache.TryGetValue(user, out autodiscoverCacheEntry))
			{
				ExTraceGlobals.OnlineMeetingTracer.TraceDebug<string>(0, 0L, "[AutodiscoverCache][TryGetUserUcwaUserUrl] Unable to find entry for '{0}' in per user cache", user);
				return false;
			}
			if (autodiscoverCacheEntry.IsValid)
			{
				ExTraceGlobals.OnlineMeetingTracer.TraceInformation<string>(0, 0L, "[AutodiscoverCache][TryGetUserUcwaUserUrl] Cache entry for user '{0}' is valid", user);
				ucwaUrl = autodiscoverCacheEntry.UcwaUrl;
				return true;
			}
			ExTraceGlobals.OnlineMeetingTracer.TraceInformation<string, int, string>(0, 0L, "[AutodiscoverCache][TryGetUserUcwaUserUrl] Cache entry for user '{0}' not valid. FailureCount: {1}, ExpirationDate: {2}", user, autodiscoverCacheEntry.FailureCount, (autodiscoverCacheEntry.Expiration != null) ? autodiscoverCacheEntry.Expiration.Value.ToString() : "none");
			return false;
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00006030 File Offset: 0x00004230
		public static void UpdateUser(string sipAddress, string ucwaUrl)
		{
			bool flag = false;
			try
			{
				object obj;
				Monitor.Enter(obj = AutodiscoverCache.lockObject, ref flag);
				AutodiscoverCacheEntry entry;
				if (string.IsNullOrEmpty(ucwaUrl))
				{
					ExDateTime value2 = ExDateTime.Now.AddDays(1.0);
					ExTraceGlobals.OnlineMeetingTracer.TraceInformation<string, string>(0, 0L, "[AutodiscoverCache][UpdateUser] Updating entry in per user cache for user '{0}' with no ucwaUrl and expiration date of '{1}'", sipAddress, value2.ToString());
					entry = new AutodiscoverCacheEntry(sipAddress, ucwaUrl, new ExDateTime?(value2));
				}
				else
				{
					ExTraceGlobals.OnlineMeetingTracer.TraceInformation<string, string>(0, 0L, "[AutodiscoverCache][UpdateUser] Updating entry in per user cache for user '{0}' with ucwaUrl '{1}' with no expiration", sipAddress, ucwaUrl);
					entry = new AutodiscoverCacheEntry(sipAddress, ucwaUrl, null);
				}
				AutodiscoverCache.UserCache.AddOrUpdate(sipAddress, entry, (string key, AutodiscoverCacheEntry value) => entry);
			}
			finally
			{
				if (flag)
				{
					object obj;
					Monitor.Exit(obj);
				}
			}
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00006110 File Offset: 0x00004310
		public static bool InvalidateUser(string sipAddress)
		{
			bool result;
			lock (AutodiscoverCache.lockObject)
			{
				result = AutodiscoverCache.InvalidateUserImpl(sipAddress);
			}
			return result;
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00006154 File Offset: 0x00004354
		public static void IncrementFailureCount(string sipAddress)
		{
			lock (AutodiscoverCache.lockObject)
			{
				AutodiscoverCacheEntry autodiscoverCacheEntry;
				if (AutodiscoverCache.UserCache.TryGetValue(sipAddress, out autodiscoverCacheEntry))
				{
					autodiscoverCacheEntry.IncrementFailureCount();
				}
			}
		}

		// Token: 0x06000199 RID: 409 RVA: 0x000061A4 File Offset: 0x000043A4
		private static bool InvalidateUserImpl(string sipAddress)
		{
			AutodiscoverCacheEntry autodiscoverCacheEntry;
			if (AutodiscoverCache.UserCache.TryRemove(sipAddress, out autodiscoverCacheEntry))
			{
				ExTraceGlobals.OnlineMeetingTracer.TraceInformation<string>(0, 0L, "[AutodiscoverCache][InvalidateUserImpl] Removing entry from per user cache: key '{0}'", sipAddress);
				return true;
			}
			ExTraceGlobals.OnlineMeetingTracer.TraceDebug<string>(0, 0L, "[AutodiscoverCache][InvalidateUserImpl] Unable to find entry for '{0}' in per user cache", sipAddress);
			return false;
		}

		// Token: 0x0600019A RID: 410 RVA: 0x000061EC File Offset: 0x000043EC
		private static string GetDomain(string sipAddress)
		{
			if (string.IsNullOrWhiteSpace(sipAddress))
			{
				return string.Empty;
			}
			int num = sipAddress.LastIndexOf("@");
			if (num <= 0)
			{
				return string.Empty;
			}
			return sipAddress.Substring(num + 1);
		}

		// Token: 0x04000123 RID: 291
		private const string AutodiscoverCacheName = "[AutodiscoverCache]";

		// Token: 0x04000124 RID: 292
		private static readonly ConcurrentDictionary<string, string> perDomainEndpointCache = new ConcurrentDictionary<string, string>();

		// Token: 0x04000125 RID: 293
		private static readonly ConcurrentDictionary<string, AutodiscoverCacheEntry> perUserUcwaUrlCache = new ConcurrentDictionary<string, AutodiscoverCacheEntry>();

		// Token: 0x04000126 RID: 294
		private static readonly object lockObject = new object();
	}
}
