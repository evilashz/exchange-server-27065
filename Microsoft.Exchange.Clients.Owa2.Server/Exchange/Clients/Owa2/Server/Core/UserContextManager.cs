using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Caching;
using Microsoft.Exchange.Clients.Common;
using Microsoft.Exchange.Clients.EventLogs;
using Microsoft.Exchange.Clients.Owa2.Server.Diagnostics;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.Security.Authentication;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020000BE RID: 190
	internal sealed class UserContextManager
	{
		// Token: 0x060007A4 RID: 1956 RVA: 0x0001877E File Offset: 0x0001697E
		private UserContextManager()
		{
		}

		// Token: 0x060007A5 RID: 1957 RVA: 0x00018788 File Offset: 0x00016988
		internal static void Initialize()
		{
			UserContextManager.sharedContextsServiceCommands = new List<UserContextManager.RequestShouldUseSharedContext>
			{
				new UserContextManager.RequestShouldUseSharedContext(SubscribeToGroupUnseenNotification.RequestShouldUseSharedContext),
				new UserContextManager.RequestShouldUseSharedContext(UnsubscribeToGroupUnseenNotification.RequestShouldUseSharedContext),
				new UserContextManager.RequestShouldUseSharedContext(GetModernGroupUnseenItems.RequestShouldUseSharedContext)
			};
		}

		// Token: 0x060007A6 RID: 1958 RVA: 0x000187D8 File Offset: 0x000169D8
		public static void TerminateSession(IMailboxContext userContext, CacheItemRemovedReason abandonedReason)
		{
			ExTraceGlobals.UserContextTracer.TraceDebug(0L, "UserContextManager.TerminateSession");
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			try
			{
				userContext.LogBreadcrumb("TerminateSession");
				string text = string.Empty;
				if (userContext.LogonIdentity != null && userContext.LogonIdentity.UserSid != null)
				{
					text = userContext.LogonIdentity.UserSid.ToString();
				}
				if (!string.IsNullOrEmpty(text))
				{
					PerformanceCounterManager.UpdatePerfCounteronUserContextDeletion(text, false, false, Globals.ArePerfCountersEnabled);
				}
			}
			finally
			{
				userContext.Dispose();
				userContext = null;
			}
		}

		// Token: 0x060007A7 RID: 1959 RVA: 0x00018874 File Offset: 0x00016A74
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
			int num = UserContextManager.DefaultSessionTimeout;
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

		// Token: 0x060007A8 RID: 1960 RVA: 0x000188D2 File Offset: 0x00016AD2
		internal static UserContext GetUserContext(HttpContext httpContext)
		{
			return UserContextManager.GetUserContext(httpContext, true);
		}

		// Token: 0x060007A9 RID: 1961 RVA: 0x000188DB File Offset: 0x00016ADB
		internal static UserContext GetUserContext(HttpContext httpContext, bool create)
		{
			return UserContextManager.GetUserContext(httpContext, null, create);
		}

		// Token: 0x060007AA RID: 1962 RVA: 0x000188E8 File Offset: 0x00016AE8
		internal static UserContext GetUserContext(HttpContext httpContext, AuthZClientInfo effectiveCaller = null, bool create = true)
		{
			IMailboxContext mailboxContext = UserContextManager.GetMailboxContext(httpContext, effectiveCaller, create);
			if (mailboxContext == null)
			{
				return null;
			}
			UserContext userContext = mailboxContext as UserContext;
			if (userContext == null)
			{
				throw new OwaInvalidOperationException("Invalid user context returned. It was expected to be a full user context.");
			}
			return userContext;
		}

		// Token: 0x060007AB RID: 1963 RVA: 0x0001891C File Offset: 0x00016B1C
		internal static IMailboxContext GetMailboxContext(HttpContext httpContext, AuthZClientInfo effectiveCaller = null, bool create = true)
		{
			if (httpContext == null)
			{
				throw new ArgumentNullException("httpContext");
			}
			ClientSecurityContext overrideClientSecurityContext = (effectiveCaller == null) ? null : effectiveCaller.ClientSecurityContext;
			UserContextCookie userContextCookie;
			UserContextKey userContextKey = UserContextManager.GetUserContextKey(httpContext, overrideClientSecurityContext, out userContextCookie);
			if (!create)
			{
				return UserContextManager.GetMailboxContextFromCache(userContextKey);
			}
			return UserContextManager.AcquireUserContext(httpContext, effectiveCaller, userContextKey, userContextCookie);
		}

		// Token: 0x060007AC RID: 1964 RVA: 0x00018A68 File Offset: 0x00016C68
		internal static MailboxSession GetClonedMailboxSession(Guid mailboxGuid, CultureInfo cultureInfo, LogonType logonType, string userContextKey, ExchangePrincipal exchangePrincipal, IADOrgPerson person, ClientSecurityContext clientSecurityContext, GenericIdentity genericIdentity, bool unifiedLogon)
		{
			MailboxSession clonedMailboxSession = null;
			UserContext userContext = null;
			string expectedMailboxKey = string.Empty;
			if (!string.IsNullOrEmpty(userContextKey))
			{
				try
				{
					OwaDiagnostics.SendWatsonReportsForGrayExceptions(delegate()
					{
						UserContextManager.UserContextCacheWrapper userContextCacheWrapper = (UserContextManager.UserContextCacheWrapper)HttpRuntime.Cache.Get(userContextKey);
						if (userContextCacheWrapper != null && userContextCacheWrapper.UserContext != null && userContextCacheWrapper.UserContext.TerminationStatus == UserContextTerminationStatus.NotTerminate)
						{
							userContext = (userContextCacheWrapper.UserContext as UserContext);
							if (userContext != null && userContext.FeaturesManager != null && userContext.FeaturesManager.ServerSettings.OwaMailboxSessionCloning.Enabled && userContext.UserCulture != null && !userContext.IsExplicitLogon)
							{
								expectedMailboxKey = AccessingPrincipalTiedCache.BuildKeyCacheKey(mailboxGuid, cultureInfo, logonType);
								clonedMailboxSession = userContext.CloneMailboxSession(expectedMailboxKey, exchangePrincipal, person, clientSecurityContext, genericIdentity, unifiedLogon);
							}
						}
					});
				}
				catch (GrayException ex)
				{
					ExTraceGlobals.UserContextCallTracer.TraceError(0L, string.Format("Exception while cloning MailboxSession, MailboxKey: {0}, Exception: {1}", expectedMailboxKey, ex.Message));
				}
			}
			return clonedMailboxSession;
		}

		// Token: 0x060007AD RID: 1965 RVA: 0x00018B3C File Offset: 0x00016D3C
		internal static IMailboxContext GetMailboxContextFromCache(UserContextKey userContextKey)
		{
			return UserContextManager.GetMailboxContextFromCache(userContextKey, true);
		}

		// Token: 0x060007AE RID: 1966 RVA: 0x00018B45 File Offset: 0x00016D45
		internal static UserContextStatistics GetUserContextStatistics(HttpContext context)
		{
			return (UserContextStatistics)context.Items["UserContextStatistics"];
		}

		// Token: 0x060007AF RID: 1967 RVA: 0x00018B5C File Offset: 0x00016D5C
		private static UserContextKey GetUserContextKey(HttpContext httpContext, ClientSecurityContext overrideClientSecurityContext, out UserContextCookie userContextCookie)
		{
			UserContextKey userContextKey = null;
			string explicitLogonUser = UserContextUtilities.GetExplicitLogonUser(httpContext);
			if (string.IsNullOrEmpty(explicitLogonUser))
			{
				userContextCookie = UserContextCookie.GetUserContextCookie(httpContext);
				if (userContextCookie != null)
				{
					ExTraceGlobals.UserContextCallTracer.TraceDebug<UserContextCookie>(0L, "Found cookie in the request: {0}", userContextCookie);
					if (overrideClientSecurityContext == null)
					{
						userContextKey = UserContextKey.CreateFromCookie(userContextCookie, httpContext);
					}
					else
					{
						userContextKey = UserContextKey.CreateFromCookie(userContextCookie, overrideClientSecurityContext.UserSid);
					}
				}
			}
			else
			{
				userContextCookie = null;
				if (UserContextManager.RequestRequiresSharedContext(httpContext))
				{
					userContextKey = UserContextKey.Create("D894745CADD64DB9B00309200288E1E7", "SharedAdmin", explicitLogonUser);
				}
				else
				{
					SecurityIdentifier securityIdentifier = httpContext.User.Identity.GetSecurityIdentifier();
					if (securityIdentifier == null)
					{
						ExTraceGlobals.UserContextCallTracer.TraceDebug<IIdentity>(0L, "UserContextManager.GetUserContextKey: current user has no security identifier - '{0}'", httpContext.User.Identity);
						ExWatson.SendReport(new InvalidOperationException(string.Format("UserContextManager.GetUserContextKey: current user has no security identifier - '{0}'", httpContext.User.Identity)), ReportOptions.None, null);
						return null;
					}
					string logonUniqueKey = securityIdentifier.ToString();
					string text = httpContext.Request.Headers["X-OWA-Test-ExplicitLogonUserId"];
					if (string.IsNullOrEmpty(text) || !AppConfigLoader.GetConfigBoolValue("Test_OwaAllowHeaderOverride", false))
					{
						text = "B387FD19C8C4416694EB79909BED70B5";
					}
					userContextKey = UserContextKey.Create(text, logonUniqueKey, explicitLogonUser);
					ExTraceGlobals.UserContextCallTracer.TraceDebug<UserContextKey>(0L, "Cookie not found but this is explicit logon. Generated Key: {0}", userContextKey);
				}
			}
			return userContextKey;
		}

		// Token: 0x060007B0 RID: 1968 RVA: 0x00018C9C File Offset: 0x00016E9C
		private static bool RequestRequiresSharedContext(HttpContext httpContext)
		{
			string methodName = httpContext.Request.QueryString["action"];
			foreach (UserContextManager.RequestShouldUseSharedContext requestShouldUseSharedContext in UserContextManager.sharedContextsServiceCommands)
			{
				if (requestShouldUseSharedContext(methodName))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060007B1 RID: 1969 RVA: 0x00018D10 File Offset: 0x00016F10
		private static bool IsSharedContextKey(UserContextKey userContextKey)
		{
			return userContextKey != null && userContextKey.UserContextId == "D894745CADD64DB9B00309200288E1E7";
		}

		// Token: 0x060007B2 RID: 1970 RVA: 0x00018D28 File Offset: 0x00016F28
		private static IMailboxContext AcquireUserContext(HttpContext httpContext, AuthZClientInfo effectiveCaller, UserContextKey userContextKey, UserContextCookie userContextCookie)
		{
			IMailboxContext mailboxContext = null;
			UserContextStatistics userContextStatistics = null;
			try
			{
				if (userContextKey != null)
				{
					mailboxContext = UserContextManager.GetMailboxContextFromCache(userContextKey);
					if (mailboxContext == null || mailboxContext.State == UserContextState.Abandoned)
					{
						UserContextManager.CreateUserContext(httpContext, userContextKey, effectiveCaller, out mailboxContext, out userContextStatistics);
					}
				}
				else
				{
					UserContextManager.CreateUserContext(httpContext, null, effectiveCaller, out mailboxContext, out userContextStatistics);
					string cookieId = null;
					if (mailboxContext != null)
					{
						userContextCookie = UserContextCookie.CreateFromKey(cookieId, mailboxContext.Key, httpContext.Request.IsSecureConnection);
						httpContext.Response.Cookies.Set(userContextCookie.HttpCookie);
						userContextStatistics.CookieCreated = true;
					}
				}
			}
			finally
			{
				if (userContextStatistics != null)
				{
					SignInLogEvent logEvent = new SignInLogEvent(mailboxContext, (userContextCookie != null) ? userContextCookie.CookieValue : string.Empty, userContextStatistics, httpContext.Request.Url);
					OwaServerLogger.AppendToLog(logEvent);
				}
			}
			return mailboxContext;
		}

		// Token: 0x060007B3 RID: 1971 RVA: 0x00018DE4 File Offset: 0x00016FE4
		private static void InsertIntoCache(HttpContext httpContext, IMailboxContext userContext)
		{
			UserContextManager.UserContextCacheWrapper userContextCacheWrapper = (UserContextManager.UserContextCacheWrapper)HttpRuntime.Cache.Get(userContext.Key.ToString());
			if (userContextCacheWrapper == null)
			{
				userContextCacheWrapper = new UserContextManager.UserContextCacheWrapper
				{
					Lock = new OwaRWLockWrapper()
				};
			}
			UserContextManager.TombstoneExistingUserContext(userContextCacheWrapper);
			userContextCacheWrapper.UserContext = userContext;
			int num = UserContextManager.UserContextTimeout;
			if (UserAgentUtilities.IsMonitoringRequest(httpContext.Request.UserAgent))
			{
				num = 1;
				if (userContext.UserPrincipalName.EndsWith(".exchangemon.net", StringComparison.InvariantCultureIgnoreCase))
				{
					ExMonitoringRequestTracker.Instance.ReportMonitoringRequest(httpContext.Request);
				}
			}
			HttpRuntime.Cache.Insert(userContext.Key.ToString(), userContextCacheWrapper, null, DateTime.MaxValue, new TimeSpan((long)num * 600000000L), CacheItemPriority.NotRemovable, new CacheItemRemovedCallback(UserContextManager.UserContextRemovedCallback));
			userContext.LogBreadcrumb("Cached:Key=" + userContext.Key.ToString());
		}

		// Token: 0x060007B4 RID: 1972 RVA: 0x00018EC0 File Offset: 0x000170C0
		private static void TombstoneExistingUserContext(UserContextManager.UserContextCacheWrapper wrapper)
		{
			IMailboxContext userContext = wrapper.UserContext;
			if (userContext != null)
			{
				userContext.LogBreadcrumb("This context will be tombstoned.");
				string key = string.Format("Tombstoned-{0}", userContext.Key.ToString());
				UserContextManager.UserContextCacheWrapper value = new UserContextManager.UserContextCacheWrapper
				{
					Lock = new OwaRWLockWrapper(),
					UserContext = userContext
				};
				HttpRuntime.Cache.Insert(key, value, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(1.0), CacheItemPriority.NotRemovable, new CacheItemRemovedCallback(UserContextManager.UserContextRemovedCallback));
			}
		}

		// Token: 0x060007B5 RID: 1973 RVA: 0x00018F40 File Offset: 0x00017140
		private static void UserContextRemovedCallback(string key, object value, CacheItemRemovedReason reason)
		{
			UserContextManager.UserContextCacheWrapper userContextCacheWrapper = value as UserContextManager.UserContextCacheWrapper;
			if (userContextCacheWrapper != null && userContextCacheWrapper.UserContext != null)
			{
				userContextCacheWrapper.UserContext.LogBreadcrumb("UserContextRemovedCallback.  Key=" + userContextCacheWrapper.UserContext.Key.ToString());
			}
			ThreadPool.QueueUserWorkItem(new WaitCallback(UserContextManager.UserContextRemoved), new object[]
			{
				key,
				value,
				reason
			});
		}

		// Token: 0x060007B6 RID: 1974 RVA: 0x00018FB0 File Offset: 0x000171B0
		private static void UserContextRemoved(object parameters)
		{
			bool flag = false;
			try
			{
				if (flag)
				{
				}
			}
			finally
			{
				try
				{
					object[] array = parameters as object[];
					string text = array[0] as string;
					object obj = array[1];
					CacheItemRemovedReason cacheItemRemovedReason = (CacheItemRemovedReason)array[2];
					UserContextManager.UserContextCacheWrapper userContextCacheWrapper = obj as UserContextManager.UserContextCacheWrapper;
					if (userContextCacheWrapper != null)
					{
						if (userContextCacheWrapper.UserContext != null)
						{
							userContextCacheWrapper.UserContext.LogBreadcrumb("UserContextRemoved Invoked.  Key=" + text);
						}
						if (userContextCacheWrapper.Lock != null && !userContextCacheWrapper.Lock.IsWriterLockHeld)
						{
							try
							{
								if (userContextCacheWrapper.Lock.LockWriterElastic(3000) && userContextCacheWrapper.UserContext != null)
								{
									ExTraceGlobals.UserContextTracer.TraceDebug<string, IMailboxContext, int>(0L, "Removing user context from cache, Key={0}, User context instance={1}, Reason={2}", text, userContextCacheWrapper.UserContext, (int)cacheItemRemovedReason);
									userContextCacheWrapper.UserContext.LogBreadcrumb("UserContextRemoved Terminating.  Key=" + userContextCacheWrapper.UserContext.Key.ToString());
									UserContextManager.TerminateSession(userContextCacheWrapper.UserContext, cacheItemRemovedReason);
									userContextCacheWrapper.UserContext.LogBreadcrumb("UserContextRemoved Terminated.  Key=" + userContextCacheWrapper.UserContext.Key.ToString());
									userContextCacheWrapper.UserContext.TerminationStatus = UserContextTerminationStatus.TerminateCompleted;
								}
							}
							finally
							{
								if (userContextCacheWrapper.Lock.IsWriterLockHeld)
								{
									userContextCacheWrapper.Lock.ReleaseWriterLock();
								}
							}
						}
						if (userContextCacheWrapper.UserContext != null && userContextCacheWrapper.UserContext.TerminationStatus != UserContextTerminationStatus.TerminateCompleted)
						{
							userContextCacheWrapper.UserContext.TerminationStatus = UserContextTerminationStatus.TerminatePending;
							userContextCacheWrapper.UserContext.AbandonedReason = cacheItemRemovedReason;
						}
					}
				}
				catch (ThreadAbortException)
				{
				}
				catch (Exception ex)
				{
					OwaDiagnostics.LogEvent(ClientsEventLogConstants.Tuple_UserContextTerminationError, string.Empty, new object[]
					{
						ex.ToString()
					});
					ExTraceGlobals.UserContextTracer.TraceDebug(0L, "Sending watson report");
					ExWatson.SendReport(ex, ReportOptions.None, null);
				}
			}
		}

		// Token: 0x060007B7 RID: 1975 RVA: 0x000191DC File Offset: 0x000173DC
		private static OwaRWLockWrapper GetUserContextKeyLock(string userContextKeyString)
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

		// Token: 0x060007B8 RID: 1976 RVA: 0x00019278 File Offset: 0x00017478
		private static void CreateUserContext(HttpContext httpContext, UserContextKey userContextKey, AuthZClientInfo effectiveCaller, out IMailboxContext userContext, out UserContextStatistics userContextStats)
		{
			Stopwatch stopwatch = Stopwatch.StartNew();
			userContextStats = new UserContextStatistics();
			userContext = null;
			OwaIdentity owaIdentity = null;
			OwaIdentity owaIdentity2 = null;
			OwaIdentity owaIdentity3 = null;
			try
			{
				try
				{
					OwaIdentity owaIdentity4 = OwaIdentity.ResolveLogonIdentity(httpContext, effectiveCaller);
					owaIdentity2 = owaIdentity4;
					string explicitLogonUser = UserContextUtilities.GetExplicitLogonUser(httpContext);
					if (!string.IsNullOrEmpty(explicitLogonUser))
					{
						ExTraceGlobals.UserContextTracer.TraceDebug<string>(0L, "Created partial mailbox identity from SMTP address={0}", explicitLogonUser);
						owaIdentity = OwaIdentity.CreateOwaIdentityFromExplicitLogonAddress(explicitLogonUser);
						owaIdentity3 = owaIdentity;
					}
					if (userContextKey == null)
					{
						userContextKey = UserContextKey.CreateNew(owaIdentity4, owaIdentity, httpContext);
						ExTraceGlobals.UserContextTracer.TraceDebug<UserContextKey>(0L, "Creating new user context key: {0}", userContextKey);
					}
					else
					{
						ExTraceGlobals.UserContextTracer.TraceDebug<UserContextKey>(0L, "Reusing user context key: {0}", userContextKey);
					}
					OwaRWLockWrapper userContextKeyLock = UserContextManager.GetUserContextKeyLock(userContextKey.ToString());
					if (userContextKeyLock == null)
					{
						userContextStats.Error = UserContextCreationError.UnableToAcquireOwaRWLock;
						throw new OwaLockException("UserContextManger::CreateUserContext was not able to create a lock");
					}
					if (userContextKeyLock.LockWriterElastic(6000))
					{
						try
						{
							userContext = UserContextManager.GetMailboxContextFromCache(userContextKey, false);
							if (userContext != null && userContext.TerminationStatus == UserContextTerminationStatus.TerminatePending)
							{
								UserContextManager.TerminateSession(userContext, userContext.AbandonedReason);
								userContext = null;
							}
							if (userContext == null)
							{
								userContextStats.Created = true;
								ExTraceGlobals.UserContextTracer.TraceDebug<UserContextKey>(0L, "User context was not found in the cache, creating one. UserContextKey: {0}", userContextKey);
								bool flag = false;
								try
								{
									if (UserContextManager.IsSharedContextKey(userContextKey))
									{
										userContext = new SharedContext(userContextKey, httpContext.Request.UserAgent);
									}
									else
									{
										userContext = new UserContext(userContextKey, httpContext.Request.UserAgent);
									}
									Stopwatch stopwatch2 = Stopwatch.StartNew();
									userContext.Load(owaIdentity4, owaIdentity, userContextStats);
									userContextStats.LoadTime = (int)stopwatch2.ElapsedMilliseconds;
									UserContextManager.InsertIntoCache(httpContext, userContext);
									owaIdentity2 = null;
									owaIdentity3 = null;
									string userName = userContext.LogonIdentity.UserSid.ToString();
									PerformanceCounterManager.UpdatePerfCounteronUserContextCreation(userName, false, false, Globals.ArePerfCountersEnabled);
									flag = true;
								}
								finally
								{
									if (!flag)
									{
										ExTraceGlobals.UserContextTracer.TraceDebug<UserContextKey>(0L, "User context creation failed. UserContextKey: {0}", userContextKey);
										if (userContext != null)
										{
											ExTraceGlobals.UserContextTracer.TraceDebug<UserContextKey>(0L, "Disposing user context. UserContextKey: {0}", userContextKey);
											userContext.Dispose();
											userContext.State = UserContextState.Abandoned;
										}
									}
								}
							}
							goto IL_1EA;
						}
						finally
						{
							userContextKeyLock.ReleaseWriterLock();
						}
						goto IL_1D4;
						IL_1EA:
						goto IL_237;
					}
					IL_1D4:
					userContextStats.Error = UserContextCreationError.UnableToAcquireOwaRWLock;
					throw new OwaLockTimeoutException("UserContextManger::CreateUserContext was not able to acquire a rw lock", null, null);
				}
				catch (OwaIdentityException ex)
				{
					userContextStats.Error = UserContextCreationError.UnableToResolveLogonIdentity;
					OwaServerTraceLogger.AppendToLog(new TraceLogEvent("UserContext", userContext, "UserContextManager.CreateUserContext", ex.ToString()));
					throw;
				}
				catch (Exception ex2)
				{
					OwaServerTraceLogger.AppendToLog(new TraceLogEvent("UserContext", userContext, "UserContextManager.CreateUserContext", ex2.ToString()));
					throw;
				}
				IL_237:;
			}
			finally
			{
				if (owaIdentity2 != null)
				{
					owaIdentity2.Dispose();
				}
				if (owaIdentity3 != null)
				{
					owaIdentity3.Dispose();
				}
				stopwatch.Stop();
				userContextStats.AcquireLatency = (int)stopwatch.ElapsedMilliseconds;
				httpContext.Items["UserContextStatistics"] = userContextStats;
			}
		}

		// Token: 0x060007B9 RID: 1977 RVA: 0x00019578 File Offset: 0x00017778
		private static IMailboxContext GetMailboxContextFromCache(UserContextKey userContextKey, bool lockUserContextKey)
		{
			ExTraceGlobals.UserContextCallTracer.TraceDebug(0L, "UserContextManager.TryGetUserContextFromCache");
			if (userContextKey == null)
			{
				ExTraceGlobals.UserContextCallTracer.TraceDebug(0L, "UserContextManager.TryGetUserContextFromCache: No UserContextKey provided, returning null.");
				return null;
			}
			UserContextManager.UserContextCacheWrapper userContextCacheWrapper = null;
			IMailboxContext mailboxContext = null;
			bool flag = false;
			try
			{
				ExTraceGlobals.UserContextCallTracer.TraceDebug<UserContextKey>(0L, "Attempting to fetch user context from the cache.  Key={0}", userContextKey);
				userContextCacheWrapper = (UserContextManager.UserContextCacheWrapper)HttpRuntime.Cache.Get(userContextKey.ToString());
				if (userContextCacheWrapper != null && userContextCacheWrapper.UserContext != null)
				{
					if (lockUserContextKey)
					{
						if (userContextCacheWrapper.Lock == null)
						{
							throw new OwaLockException("UserContextManager::GetUserContextFromCache cannot get a lock from UserContextCacheWrapper");
						}
						if (!userContextCacheWrapper.Lock.IsWriterLockHeld)
						{
							if (!userContextCacheWrapper.Lock.LockWriterElastic(3000))
							{
								throw new OwaLockTimeoutException("UserContextManager::GetUserContextFromCache cannot acquire a write lock in a time fashion");
							}
							flag = true;
							mailboxContext = userContextCacheWrapper.UserContext;
							ExTraceGlobals.UserContextCallTracer.TraceDebug<UserContextKey, IMailboxContext>(0L, "User context found in cache. Key={0}, User context instance={1}", userContextKey, mailboxContext);
						}
					}
					else
					{
						mailboxContext = userContextCacheWrapper.UserContext;
						ExTraceGlobals.UserContextCallTracer.TraceDebug<UserContextKey, IMailboxContext>(0L, "User context found in cache. Key={0}, User context instance={1}", userContextKey, mailboxContext);
					}
				}
				else
				{
					ExTraceGlobals.UserContextCallTracer.TraceDebug<string>(0L, "An object for this user context ID value is not present in the cache (probably was expired), so we are going to reuse the user context ID value for the new session", userContextKey.UserContextId);
				}
			}
			finally
			{
				if (mailboxContext != null && mailboxContext.TerminationStatus == UserContextTerminationStatus.TerminatePending)
				{
					UserContextManager.TerminateSession(mailboxContext, mailboxContext.AbandonedReason);
					mailboxContext = null;
				}
				if (flag && userContextCacheWrapper != null && userContextCacheWrapper.Lock != null)
				{
					userContextCacheWrapper.Lock.ReleaseWriterLock();
				}
				if (mailboxContext != null && mailboxContext.State != UserContextState.Active)
				{
					mailboxContext = null;
				}
			}
			return mailboxContext;
		}

		// Token: 0x04000438 RID: 1080
		private const string DefaultExplicitLogonUserContextId = "B387FD19C8C4416694EB79909BED70B5";

		// Token: 0x04000439 RID: 1081
		private const string SharedContextId = "D894745CADD64DB9B00309200288E1E7";

		// Token: 0x0400043A RID: 1082
		private const string SharedContextLogonId = "SharedAdmin";

		// Token: 0x0400043B RID: 1083
		private const string UserContextStatsKey = "UserContextStatistics";

		// Token: 0x0400043C RID: 1084
		internal const int DefaultUserContextLockTimeout = 3000;

		// Token: 0x0400043D RID: 1085
		private static readonly int UserContextTouchThreshold = 5;

		// Token: 0x0400043E RID: 1086
		private static readonly int DefaultSessionTimeout = 15;

		// Token: 0x0400043F RID: 1087
		private static readonly int UserContextTimeout = 8;

		// Token: 0x04000440 RID: 1088
		private static List<UserContextManager.RequestShouldUseSharedContext> sharedContextsServiceCommands;

		// Token: 0x04000441 RID: 1089
		private static object lockObject = new object();

		// Token: 0x020000BF RID: 191
		// (Invoke) Token: 0x060007BC RID: 1980
		internal delegate bool RequestShouldUseSharedContext(string methodName);

		// Token: 0x020000C0 RID: 192
		private class UserContextCacheWrapper
		{
			// Token: 0x17000275 RID: 629
			// (get) Token: 0x060007BF RID: 1983 RVA: 0x000196EF File Offset: 0x000178EF
			// (set) Token: 0x060007C0 RID: 1984 RVA: 0x000196F7 File Offset: 0x000178F7
			public IMailboxContext UserContext { get; set; }

			// Token: 0x17000276 RID: 630
			// (get) Token: 0x060007C1 RID: 1985 RVA: 0x00019700 File Offset: 0x00017900
			// (set) Token: 0x060007C2 RID: 1986 RVA: 0x00019708 File Offset: 0x00017908
			public OwaRWLockWrapper Lock { get; set; }
		}
	}
}
