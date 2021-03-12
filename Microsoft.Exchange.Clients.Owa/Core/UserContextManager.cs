using System;
using System.Diagnostics;
using System.Threading;
using System.Web;
using System.Web.Caching;
using Microsoft.Exchange.Clients.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x0200027E RID: 638
	public sealed class UserContextManager
	{
		// Token: 0x06001665 RID: 5733 RVA: 0x0008397A File Offset: 0x00081B7A
		private UserContextManager()
		{
		}

		// Token: 0x06001666 RID: 5734 RVA: 0x00083984 File Offset: 0x00081B84
		public static UserContext GetUserContext()
		{
			OwaContext owaContext = OwaContext.Current;
			if (owaContext == null)
			{
				return null;
			}
			return owaContext.UserContext;
		}

		// Token: 0x06001667 RID: 5735 RVA: 0x000839A4 File Offset: 0x00081BA4
		internal static void InsertIntoCache(UserContext userContext)
		{
			UserContextManager.UserContextCacheWrapper userContextCacheWrapper = (UserContextManager.UserContextCacheWrapper)HttpRuntime.Cache.Get(userContext.Key.ToString());
			if (userContextCacheWrapper == null)
			{
				userContextCacheWrapper = new UserContextManager.UserContextCacheWrapper();
			}
			userContextCacheWrapper.UserContext = userContext;
			HttpRuntime.Cache.Insert(userContext.Key.ToString(), userContextCacheWrapper, null, DateTime.MaxValue, new TimeSpan(userContext.CalculateTimeout() * 10000L), CacheItemPriority.NotRemovable, new CacheItemRemovedCallback(UserContextManager.UserContextRemovedCallback));
		}

		// Token: 0x06001668 RID: 5736 RVA: 0x00083A18 File Offset: 0x00081C18
		internal static UserContext TryGetUserContextFromCache(UserContextKey userContextKey)
		{
			ExTraceGlobals.UserContextCallTracer.TraceDebug(0L, "UserContextManager.TryGetUserContextFromCache");
			UserContext userContext = null;
			try
			{
				ExTraceGlobals.UserContextTracer.TraceDebug<UserContextKey>(0L, "Attempting to fetch user context from the cache.  Key={0}", userContextKey);
				UserContextManager.UserContextCacheWrapper userContextCacheWrapper = (UserContextManager.UserContextCacheWrapper)HttpRuntime.Cache.Get(userContextKey.ToString());
				if (userContextCacheWrapper != null && userContextCacheWrapper.UserContext != null)
				{
					userContext = userContextCacheWrapper.UserContext;
					ExTraceGlobals.UserContextTracer.TraceDebug<UserContextKey, UserContext>(0L, "User context found in cache. Key={0}, User context instance={1}", userContextKey, userContext);
					userContext.UpdateLastAccessedTime();
					userContext.Lock(true);
				}
				else
				{
					ExTraceGlobals.UserContextTracer.TraceDebug<string>(0L, "An object for this user context ID value is not present in the cache (probably was expired), so we are going to reuse the user context ID value for the new session", userContextKey.UserContextId);
				}
			}
			finally
			{
				if (userContext != null && userContext.State != UserContextState.Active && userContext.LockedByCurrentThread())
				{
					userContext.Unlock();
					userContext = null;
				}
			}
			return userContext;
		}

		// Token: 0x06001669 RID: 5737 RVA: 0x00083ADC File Offset: 0x00081CDC
		internal static void UserContextRemovedCallback(string key, object value, CacheItemRemovedReason reason)
		{
			try
			{
			}
			finally
			{
				try
				{
					UserContextManager.UserContextCacheWrapper userContextCacheWrapper = value as UserContextManager.UserContextCacheWrapper;
					if (userContextCacheWrapper.UserContext != null)
					{
						ExTraceGlobals.UserContextTracer.TraceDebug<string, UserContext, int>(0L, "Removing user context from cache, Key={0}, User context instance={1}, Reason={2}", key, userContextCacheWrapper.UserContext, (int)reason);
						UserContextManager.TerminateSession(userContextCacheWrapper.UserContext, reason);
					}
				}
				catch (ThreadAbortException)
				{
				}
				catch (Exception exception)
				{
					if (Globals.SendWatsonReports)
					{
						ExTraceGlobals.CoreTracer.TraceDebug(0L, "Sending watson report");
						ExWatson.SendReport(exception, ReportOptions.None, null);
					}
				}
			}
		}

		// Token: 0x0600166A RID: 5738 RVA: 0x00083B70 File Offset: 0x00081D70
		public static void TerminateSession(UserContext userContext, CacheItemRemovedReason abandonedReason)
		{
			ExTraceGlobals.UserContextCallTracer.TraceDebug(0L, "UserContextManager.TerminateSession");
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			userContext.TerminationStatus = UserContextTerminationStatus.TerminatePending;
			bool flag = false;
			try
			{
				try
				{
					userContext.Lock();
					flag = true;
				}
				catch (OwaLockTimeoutException)
				{
					ExTraceGlobals.UserContextTracer.TraceDebug<UserContext>(0L, "TerminateSession tried to grab a lock for an user context and it timed out. Mark it to be abandoned when unlocked. User context instance={0}", userContext);
					userContext.AbandonedReason = abandonedReason;
					return;
				}
				userContext.TerminationStatus = UserContextTerminationStatus.TerminateStarted;
				if (userContext.State != UserContextState.Abandoned)
				{
					try
					{
						UserContextManager.RecordUserContextDeletion(userContext.LogonIdentity, userContext);
						if (Globals.ArePerfCountersEnabled)
						{
							if (abandonedReason == CacheItemRemovedReason.Expired)
							{
								OwaSingleCounters.TotalSessionsEndedByTimeout.Increment();
							}
							else if (abandonedReason == CacheItemRemovedReason.Removed && userContext.State == UserContextState.MarkedForLogoff)
							{
								OwaSingleCounters.TotalSessionsEndedByLogoff.Increment();
							}
						}
					}
					finally
					{
						userContext.Dispose();
					}
				}
			}
			finally
			{
				if (userContext.LockedByCurrentThread() && flag)
				{
					userContext.State = UserContextState.Abandoned;
					userContext.AbandonedReason = abandonedReason;
					userContext.TerminationStatus = UserContextTerminationStatus.TerminateCompleted;
					userContext.Unlock();
				}
			}
		}

		// Token: 0x1700062D RID: 1581
		// (get) Token: 0x0600166B RID: 5739 RVA: 0x00083C78 File Offset: 0x00081E78
		public static long CachedUserContextsCount
		{
			get
			{
				return OwaSingleCounters.CurrentUsers.RawValue;
			}
		}

		// Token: 0x0600166C RID: 5740 RVA: 0x00083C84 File Offset: 0x00081E84
		internal static void RecordUserContextCreation(OwaIdentity logonIdentity, UserContext userContext)
		{
			if (logonIdentity == null)
			{
				throw new ArgumentNullException("logonIdentity");
			}
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			string userName = logonIdentity.UserSid.ToString();
			PerformanceCounterManager.UpdatePerfCounteronUserContextCreation(userName, userContext.IsProxy, userContext.IsBasicExperience, Globals.ArePerfCountersEnabled);
		}

		// Token: 0x0600166D RID: 5741 RVA: 0x00083CD0 File Offset: 0x00081ED0
		internal static void RecordUserContextDeletion(OwaIdentity logonIdentity, UserContext userContext)
		{
			string userName = logonIdentity.UserSid.ToString();
			PerformanceCounterManager.UpdatePerfCounteronUserContextDeletion(userName, userContext.IsProxy, userContext.IsBasicExperience, Globals.ArePerfCountersEnabled);
		}

		// Token: 0x0600166E RID: 5742 RVA: 0x00083D00 File Offset: 0x00081F00
		public static void TouchUserContext(UserContext userContext, Stopwatch stopWatch)
		{
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			if (stopWatch == null)
			{
				throw new ArgumentNullException("stopWatch");
			}
			int num = userContext.Configuration.SessionTimeout;
			if (num > UserContextManager.UserContextTouchThreshold)
			{
				num -= UserContextManager.UserContextTouchThreshold;
			}
			if (stopWatch.ElapsedMilliseconds > (long)(num * 60 * 1000))
			{
				userContext.Touch();
				stopWatch.Reset();
			}
		}

		// Token: 0x0600166F RID: 5743 RVA: 0x00083D64 File Offset: 0x00081F64
		internal static OwaRWLockWrapper GetUserContextKeyLock(string userContextKeyString)
		{
			UserContextManager.UserContextCacheWrapper userContextCacheWrapper = (UserContextManager.UserContextCacheWrapper)HttpRuntime.Cache.Get(userContextKeyString);
			if (userContextCacheWrapper == null)
			{
				lock (UserContextManager.lockObject)
				{
					userContextCacheWrapper = (UserContextManager.UserContextCacheWrapper)HttpRuntime.Cache.Get(userContextKeyString);
					if (userContextCacheWrapper == null)
					{
						userContextCacheWrapper = new UserContextManager.UserContextCacheWrapper
						{
							Lock = new OwaRWLockWrapper()
						};
						HttpRuntime.Cache.Insert(userContextKeyString, userContextCacheWrapper, null, DateTime.MaxValue, TimeSpan.FromMinutes(2.5), CacheItemPriority.NotRemovable, null);
					}
				}
			}
			return userContextCacheWrapper.Lock;
		}

		// Token: 0x04001163 RID: 4451
		private static readonly int UserContextTouchThreshold = 5;

		// Token: 0x04001164 RID: 4452
		private static object lockObject = new object();

		// Token: 0x0200027F RID: 639
		private class UserContextCacheWrapper
		{
			// Token: 0x1700062E RID: 1582
			// (get) Token: 0x06001671 RID: 5745 RVA: 0x00083E12 File Offset: 0x00082012
			// (set) Token: 0x06001672 RID: 5746 RVA: 0x00083E1A File Offset: 0x0008201A
			public UserContext UserContext { get; set; }

			// Token: 0x1700062F RID: 1583
			// (get) Token: 0x06001673 RID: 5747 RVA: 0x00083E23 File Offset: 0x00082023
			// (set) Token: 0x06001674 RID: 5748 RVA: 0x00083E2B File Offset: 0x0008202B
			public OwaRWLockWrapper Lock { get; set; }
		}
	}
}
