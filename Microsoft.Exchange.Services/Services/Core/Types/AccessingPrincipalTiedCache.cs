using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web;
using System.Web.Caching;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020006A2 RID: 1698
	internal sealed class AccessingPrincipalTiedCache : IDisposable
	{
		// Token: 0x0600345D RID: 13405 RVA: 0x000BC5F8 File Offset: 0x000BA7F8
		public AccessingPrincipalTiedCache(Guid principalObjectGuid)
		{
			this.principalObjectGuid = principalObjectGuid;
		}

		// Token: 0x17000C10 RID: 3088
		// (get) Token: 0x0600345E RID: 13406 RVA: 0x000BC624 File Offset: 0x000BA824
		public int Count
		{
			get
			{
				int count;
				lock (this.keyCache)
				{
					count = this.keyCache.Count;
				}
				return count;
			}
		}

		// Token: 0x0600345F RID: 13407 RVA: 0x000BC66C File Offset: 0x000BA86C
		public static string BuildKeyCacheKey(Guid mailboxGuid, CultureInfo culture, LogonType logonType)
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}_{1}{2}", new object[]
			{
				mailboxGuid,
				culture.Name,
				AccessingPrincipalTiedCache.GetLogonTypeSuffix(logonType)
			});
		}

		// Token: 0x06003460 RID: 13408 RVA: 0x000BC6AC File Offset: 0x000BA8AC
		private static string BuildWebCacheKey(StoreSession storeSession, CultureInfo cultureInfo)
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}_{1}_{2}{3}", new object[]
			{
				storeSession.MailboxOwner.MailboxInfo.MailboxGuid,
				storeSession.GetHashCode(),
				cultureInfo.Name,
				AccessingPrincipalTiedCache.GetLogonTypeSuffix(storeSession.LogonType)
			});
		}

		// Token: 0x06003461 RID: 13409 RVA: 0x000BC710 File Offset: 0x000BA910
		private static string GetLogonTypeSuffix(LogonType logonType)
		{
			string result = string.Empty;
			if (logonType == LogonType.Admin)
			{
				result = "_a";
			}
			else if (logonType == LogonType.SystemService)
			{
				result = "_s";
			}
			return result;
		}

		// Token: 0x06003462 RID: 13410 RVA: 0x000BC73C File Offset: 0x000BA93C
		public void AddToCheckedOutSessions(Guid mailboxGuid, LogonType logonType, SessionAndAuthZ session)
		{
			if (session == null)
			{
				return;
			}
			string key = AccessingPrincipalTiedCache.BuildKeyCacheKey(mailboxGuid, session.CultureInfo, logonType);
			lock (this.checkoutSessions)
			{
				LinkedList<WeakReference<SessionAndAuthZ>> linkedList;
				if (!this.checkoutSessions.TryGetValue(key, out linkedList))
				{
					linkedList = new LinkedList<WeakReference<SessionAndAuthZ>>();
					this.checkoutSessions.Add(key, linkedList);
				}
				linkedList.AddLast(new WeakReference<SessionAndAuthZ>(session));
			}
		}

		// Token: 0x06003463 RID: 13411 RVA: 0x000BC7B8 File Offset: 0x000BA9B8
		public void RemoveFromCheckedOutSessions(Guid mailboxGuid, LogonType logonType, SessionAndAuthZ session)
		{
			string key = AccessingPrincipalTiedCache.BuildKeyCacheKey(mailboxGuid, session.CultureInfo, logonType);
			lock (this.checkoutSessions)
			{
				LinkedList<WeakReference<SessionAndAuthZ>> linkedList;
				if (this.checkoutSessions.TryGetValue(key, out linkedList))
				{
					LinkedListNode<WeakReference<SessionAndAuthZ>> next;
					for (LinkedListNode<WeakReference<SessionAndAuthZ>> linkedListNode = linkedList.First; linkedListNode != null; linkedListNode = next)
					{
						next = linkedListNode.Next;
						SessionAndAuthZ sessionAndAuthZ;
						if (!linkedListNode.Value.TryGetTarget(out sessionAndAuthZ) || sessionAndAuthZ == session)
						{
							linkedList.Remove(linkedListNode);
						}
						if (sessionAndAuthZ == session)
						{
							break;
						}
					}
				}
			}
		}

		// Token: 0x06003464 RID: 13412 RVA: 0x000BC84C File Offset: 0x000BAA4C
		public SessionAndAuthZ GetFromCheckedOutSessionsForCloning(Guid mailboxGuid, CultureInfo cultureInfo, LogonType logonType)
		{
			string key = AccessingPrincipalTiedCache.BuildKeyCacheKey(mailboxGuid, cultureInfo, logonType);
			lock (this.checkoutSessions)
			{
				LinkedList<WeakReference<SessionAndAuthZ>> linkedList;
				if (this.checkoutSessions.TryGetValue(key, out linkedList))
				{
					LinkedListNode<WeakReference<SessionAndAuthZ>> next;
					for (LinkedListNode<WeakReference<SessionAndAuthZ>> linkedListNode = linkedList.First; linkedListNode != null; linkedListNode = next)
					{
						next = linkedListNode.Next;
						SessionAndAuthZ result;
						if (linkedListNode.Value.TryGetTarget(out result))
						{
							return result;
						}
						linkedList.Remove(linkedListNode);
					}
				}
			}
			return null;
		}

		// Token: 0x06003465 RID: 13413 RVA: 0x000BC8DC File Offset: 0x000BAADC
		public SessionAndAuthZ GetFromCache(Guid mailboxGuid, CultureInfo culture, LogonType logonType)
		{
			this.CheckDisposed();
			SessionAndAuthZ sessionAndAuthZ = null;
			string key = AccessingPrincipalTiedCache.BuildKeyCacheKey(mailboxGuid, culture, logonType);
			lock (this.keyCache)
			{
				Cache cache = HttpRuntime.Cache;
				List<string> list = null;
				if (this.keyCache.TryGetValue(key, out list))
				{
					while (list.Count > 0)
					{
						string key2 = list[0];
						list.RemoveAt(0);
						this.cachedMailboxSessionCount--;
						sessionAndAuthZ = (SessionAndAuthZ)cache.Remove(key2);
						if (sessionAndAuthZ != null)
						{
							break;
						}
					}
				}
			}
			if (sessionAndAuthZ != null)
			{
				try
				{
					bool isConnected = sessionAndAuthZ.Session.IsConnected;
				}
				catch (ObjectDisposedException)
				{
					ExTraceGlobals.SessionCacheTracer.TraceDebug<int>((long)this.GetHashCode(), "AccessingPrincipalTiedCache.GetFromCache(). Hashcode: {0}. A mailbox session from the cache was already disposed.", this.GetHashCode());
					sessionAndAuthZ.Dispose();
					sessionAndAuthZ = null;
				}
			}
			return sessionAndAuthZ;
		}

		// Token: 0x06003466 RID: 13414 RVA: 0x000BC9C4 File Offset: 0x000BABC4
		public void RemoveAllFromCache(Guid mailboxGuid, CultureInfo culture, LogonType logonType)
		{
			this.CheckDisposed();
			string key = AccessingPrincipalTiedCache.BuildKeyCacheKey(mailboxGuid, culture, logonType);
			List<string> list = null;
			lock (this.keyCache)
			{
				if (this.keyCache.TryGetValue(key, out list))
				{
					this.cachedMailboxSessionCount -= list.Count;
					this.keyCache.Remove(key);
				}
			}
			if (list != null)
			{
				Cache cache = HttpRuntime.Cache;
				foreach (string key2 in list)
				{
					SessionAndAuthZ sessionAndAuthZ = (SessionAndAuthZ)cache.Remove(key2);
					if (sessionAndAuthZ != null)
					{
						sessionAndAuthZ.Dispose();
					}
				}
			}
		}

		// Token: 0x06003467 RID: 13415 RVA: 0x000BCAA0 File Offset: 0x000BACA0
		public void AddToCache(SessionAndAuthZ sessionAndAuthZ, CultureInfo cultureInfo)
		{
			if (sessionAndAuthZ == null)
			{
				throw new ArgumentException("[AccessingPrincipalTiedCache::AddToCache] sessionAndAuthZ is null");
			}
			if (sessionAndAuthZ.ClientInfo == null)
			{
				throw new ArgumentException("[AccessingPrincipalTiedCache::AddToCache] Session being added to cache has null ClientInfo.");
			}
			this.CheckDisposed();
			Cache cache = HttpRuntime.Cache;
			string text = AccessingPrincipalTiedCache.BuildWebCacheKey(sessionAndAuthZ.Session, cultureInfo);
			lock (this.keyCache)
			{
				if (this.cachedMailboxSessionCount >= Global.AccessingPrincipalCacheSize)
				{
					ExTraceGlobals.SessionCacheTracer.TraceDebug<Guid>((long)this.GetHashCode(), "Web cache for accessing principal is full. Flushing cache. Principal Guid: {0}", this.principalObjectGuid);
					this.ClearCache();
				}
				if ((SessionAndAuthZ)cache.Get(text) == null)
				{
					cache.Add(text, sessionAndAuthZ, null, ExDateTime.Now.AddMinutes(5.0).UniversalTime, Cache.NoSlidingExpiration, CacheItemPriority.Normal, new CacheItemRemovedCallback(this.RemovedCallback));
					List<string> list = null;
					StoreSession session = sessionAndAuthZ.Session;
					string key = AccessingPrincipalTiedCache.BuildKeyCacheKey(session.MailboxOwner.MailboxInfo.MailboxGuid, cultureInfo, session.LogonType);
					if (!this.keyCache.TryGetValue(key, out list))
					{
						list = new List<string>();
						this.keyCache.Add(key, list);
					}
					this.cachedMailboxSessionCount++;
					list.Add(text);
					ExTraceGlobals.SessionCacheTracer.TraceDebug((long)this.GetHashCode(), "Added StoreSession instance to the web cache.  HashCode: {0}, Principal Guid: {1}, SmtpAddress: {2}, CacheKey: {3}, Count: {4}", new object[]
					{
						session.GetHashCode(),
						this.principalObjectGuid,
						session.MailboxOwner.MailboxInfo.PrimarySmtpAddress,
						text,
						this.keyCache.Count
					});
				}
				else
				{
					StoreSession session2 = sessionAndAuthZ.Session;
					ExTraceGlobals.SessionCacheTracer.TraceDebug<Guid, SmtpAddress>((long)this.GetHashCode(), "An attempt was made to add a duplicate mailbox instance to the cache.  Ignoring. Principal Guid: {0}, SmtpAddress: {1}", this.principalObjectGuid, session2.MailboxOwner.MailboxInfo.PrimarySmtpAddress);
				}
				if (sessionAndAuthZ.Session.IsConnected)
				{
					sessionAndAuthZ.Session.Disconnect();
				}
			}
		}

		// Token: 0x06003468 RID: 13416 RVA: 0x000BCCC8 File Offset: 0x000BAEC8
		private void ClearCache()
		{
			ExTraceGlobals.SessionCacheTracer.TraceDebug((long)this.GetHashCode(), "ClearCache method called.");
			lock (this.keyCache)
			{
				foreach (KeyValuePair<string, List<string>> keyValuePair in this.keyCache)
				{
					for (int i = 0; i < keyValuePair.Value.Count; i++)
					{
						string key = keyValuePair.Value[i];
						SessionAndAuthZ sessionAndAuthZ = (SessionAndAuthZ)HttpRuntime.Cache.Remove(key);
						if (sessionAndAuthZ != null)
						{
							sessionAndAuthZ.Dispose();
						}
					}
				}
				this.keyCache.Clear();
				this.cachedMailboxSessionCount = 0;
			}
		}

		// Token: 0x06003469 RID: 13417 RVA: 0x000BCDAC File Offset: 0x000BAFAC
		private void RemovedCallback(string key, object mailboxObject, CacheItemRemovedReason reason)
		{
			if (reason == CacheItemRemovedReason.Removed)
			{
				return;
			}
			if (this.isDisposed)
			{
				return;
			}
			SessionAndAuthZ sessionAndAuthZ = (SessionAndAuthZ)mailboxObject;
			StoreSession session = sessionAndAuthZ.Session;
			lock (this.keyCache)
			{
				List<string> list = null;
				string key2 = AccessingPrincipalTiedCache.BuildKeyCacheKey(session.MailboxOwner.MailboxInfo.MailboxGuid, sessionAndAuthZ.CultureInfo, session.LogonType);
				if (this.keyCache.TryGetValue(key2, out list))
				{
					int num = list.IndexOf(key);
					if (num <= -1)
					{
						return;
					}
					list.RemoveAt(num);
					this.cachedMailboxSessionCount--;
				}
			}
			ExTraceGlobals.SessionCacheTracer.TraceDebug<string, Guid, string>((long)this.GetHashCode(), "RemoveCallback delegate called by web cache.  Key: {0}, Principal Guid: {1}, MailboxHashCode: {2}", key, this.principalObjectGuid, (session == null) ? "<NULL>" : session.GetHashCode().ToString(CultureInfo.InvariantCulture));
			sessionAndAuthZ.Dispose();
		}

		// Token: 0x0600346A RID: 13418 RVA: 0x000BCEA4 File Offset: 0x000BB0A4
		public void Dispose()
		{
			ExTraceGlobals.SessionCacheTracer.TraceDebug<int>((long)this.GetHashCode(), "MailboxSessionTiedCache.Dispose(). Hashcode: {0}", this.GetHashCode());
			if (!this.isDisposed)
			{
				this.ClearCache();
				this.isDisposed = true;
			}
		}

		// Token: 0x0600346B RID: 13419 RVA: 0x000BCED7 File Offset: 0x000BB0D7
		private void CheckDisposed()
		{
			if (this.isDisposed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
		}

		// Token: 0x04001D96 RID: 7574
		private const int CacheHoldTimeMinutes = 5;

		// Token: 0x04001D97 RID: 7575
		private Guid principalObjectGuid;

		// Token: 0x04001D98 RID: 7576
		private Dictionary<string, List<string>> keyCache = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x04001D99 RID: 7577
		private Dictionary<string, LinkedList<WeakReference<SessionAndAuthZ>>> checkoutSessions = new Dictionary<string, LinkedList<WeakReference<SessionAndAuthZ>>>();

		// Token: 0x04001D9A RID: 7578
		private bool isDisposed;

		// Token: 0x04001D9B RID: 7579
		private int cachedMailboxSessionCount;
	}
}
