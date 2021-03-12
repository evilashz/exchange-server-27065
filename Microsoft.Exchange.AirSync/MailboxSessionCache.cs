using System;
using System.Collections.Generic;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Diagnostics.Components.AirSync;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x020000DB RID: 219
	internal static class MailboxSessionCache
	{
		// Token: 0x06000C8C RID: 3212 RVA: 0x000438F0 File Offset: 0x00041AF0
		internal static void Start()
		{
			if (GlobalSettings.MailboxSessionCacheTimeout > TimeSpan.Zero)
			{
				lock (MailboxSessionCache.synchronizationObject)
				{
					if (MailboxSessionCache.mailboxSessionCache != null)
					{
						AirSyncDiagnostics.TraceDebug(ExTraceGlobals.RequestsTracer, MailboxSessionCache.mailboxSessionCache, "MailboxSessionCache is already started.");
					}
					else
					{
						MailboxSessionCache.mailboxSessionCache = new MruDictionaryCache<Guid, SecurityContextAndSession>(GlobalSettings.MailboxSessionCacheInitialSize, GlobalSettings.MailboxSessionCacheMaxSize, (int)GlobalSettings.MailboxSessionCacheTimeout.TotalMinutes);
					}
				}
			}
		}

		// Token: 0x06000C8D RID: 3213 RVA: 0x0004397C File Offset: 0x00041B7C
		internal static void Stop()
		{
			lock (MailboxSessionCache.synchronizationObject)
			{
				if (MailboxSessionCache.mailboxSessionCache != null)
				{
					MailboxSessionCache.mailboxSessionCache.Dispose();
					MailboxSessionCache.mailboxSessionCache = null;
				}
			}
		}

		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x06000C8E RID: 3214 RVA: 0x000439CC File Offset: 0x00041BCC
		public static int Count
		{
			get
			{
				int result;
				lock (MailboxSessionCache.synchronizationObject)
				{
					result = ((MailboxSessionCache.mailboxSessionCache == null) ? 0 : MailboxSessionCache.mailboxSessionCache.Count);
				}
				return result;
			}
		}

		// Token: 0x06000C8F RID: 3215 RVA: 0x00043A4C File Offset: 0x00041C4C
		public static List<MruCacheDiagnosticEntryInfo> GetCacheEntries()
		{
			List<MruCacheDiagnosticEntryInfo> result;
			lock (MailboxSessionCache.synchronizationObject)
			{
				if (MailboxSessionCache.mailboxSessionCache != null)
				{
					result = MailboxSessionCache.mailboxSessionCache.GetDiagnosticsInfo((SecurityContextAndSession securityContextAndSession) => securityContextAndSession.MailboxSession.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString());
				}
				else
				{
					result = null;
				}
			}
			return result;
		}

		// Token: 0x06000C90 RID: 3216 RVA: 0x00043ABC File Offset: 0x00041CBC
		public static float GetCacheEfficiency()
		{
			return MailboxSessionCache.cacheEfficiency.GetValue();
		}

		// Token: 0x06000C91 RID: 3217 RVA: 0x00043AC8 File Offset: 0x00041CC8
		public static void IncrementDiscardedSessions()
		{
			MailboxSessionCache.discardedSessions.Add(1U);
		}

		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x06000C92 RID: 3218 RVA: 0x00043AD5 File Offset: 0x00041CD5
		public static int DiscardedSessions
		{
			get
			{
				return (int)MailboxSessionCache.discardedSessions.GetValue();
			}
		}

		// Token: 0x06000C93 RID: 3219 RVA: 0x00043AE4 File Offset: 0x00041CE4
		public static bool TryGetAndRemoveValue(Guid token, out SecurityContextAndSession data)
		{
			bool result;
			lock (MailboxSessionCache.synchronizationObject)
			{
				if (MailboxSessionCache.mailboxSessionCache != null)
				{
					bool flag2 = MailboxSessionCache.mailboxSessionCache.TryGetAndRemoveValue(token, out data);
					MailboxSessionCache.cacheEfficiency.Add(flag2 ? 1U : 0U);
					result = flag2;
				}
				else
				{
					data = null;
					result = false;
				}
			}
			return result;
		}

		// Token: 0x06000C94 RID: 3220 RVA: 0x00043B4C File Offset: 0x00041D4C
		public static bool ContainsKey(Guid token)
		{
			bool result;
			lock (MailboxSessionCache.synchronizationObject)
			{
				if (MailboxSessionCache.mailboxSessionCache != null)
				{
					result = MailboxSessionCache.mailboxSessionCache.ContainsKey(token);
				}
				else
				{
					result = false;
				}
			}
			return result;
		}

		// Token: 0x06000C95 RID: 3221 RVA: 0x00043BA0 File Offset: 0x00041DA0
		public static bool AddOrReplace(Guid token, SecurityContextAndSession data)
		{
			lock (MailboxSessionCache.synchronizationObject)
			{
				if (MailboxSessionCache.mailboxSessionCache != null)
				{
					MailboxSessionCache.mailboxSessionCache[token] = data;
					return true;
				}
			}
			return false;
		}

		// Token: 0x040007D1 RID: 2001
		private const uint Hit = 1U;

		// Token: 0x040007D2 RID: 2002
		private const uint Miss = 0U;

		// Token: 0x040007D3 RID: 2003
		private static FixedTimeAverage cacheEfficiency = new FixedTimeAverage(10000, 60, Environment.TickCount, TimeSpan.FromHours(1.0));

		// Token: 0x040007D4 RID: 2004
		private static FixedTimeSum discardedSessions = new FixedTimeSum(10000, 60);

		// Token: 0x040007D5 RID: 2005
		private static object synchronizationObject = new object();

		// Token: 0x040007D6 RID: 2006
		private static MruDictionaryCache<Guid, SecurityContextAndSession> mailboxSessionCache;
	}
}
