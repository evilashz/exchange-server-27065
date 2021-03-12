using System;
using System.Web;
using Microsoft.Exchange.Data.ApplicationLogic.Cafe;
using Microsoft.Exchange.Diagnostics.Components.HttpProxy;
using Microsoft.Exchange.HttpProxy.Common;
using Microsoft.Exchange.HttpProxy.Routing;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x02000004 RID: 4
	internal abstract class AnchorMailbox
	{
		// Token: 0x06000009 RID: 9 RVA: 0x000021B0 File Offset: 0x000003B0
		protected AnchorMailbox(AnchorSource anchorSource, object sourceObject, IRequestContext requestContext)
		{
			if (sourceObject == null)
			{
				throw new ArgumentNullException("sourceObject");
			}
			if (requestContext == null)
			{
				throw new ArgumentNullException("requestContext");
			}
			this.AnchorSource = anchorSource;
			this.SourceObject = sourceObject;
			this.RequestContext = requestContext;
			ExTraceGlobals.VerboseTracer.TraceDebug<int, AnchorSource, object>((long)this.GetHashCode(), "[AnchorMailbox::ctor]: context {0}; AnchorSource {1}; SourceObject {2}", this.RequestContext.TraceContext, anchorSource, sourceObject);
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000A RID: 10 RVA: 0x00002217 File Offset: 0x00000417
		// (set) Token: 0x0600000B RID: 11 RVA: 0x0000221F File Offset: 0x0000041F
		public AnchorSource AnchorSource { get; private set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000C RID: 12 RVA: 0x00002228 File Offset: 0x00000428
		// (set) Token: 0x0600000D RID: 13 RVA: 0x00002230 File Offset: 0x00000430
		public object SourceObject { get; private set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000E RID: 14 RVA: 0x00002239 File Offset: 0x00000439
		// (set) Token: 0x0600000F RID: 15 RVA: 0x00002241 File Offset: 0x00000441
		public IRequestContext RequestContext { get; private set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000010 RID: 16 RVA: 0x0000224A File Offset: 0x0000044A
		// (set) Token: 0x06000011 RID: 17 RVA: 0x00002252 File Offset: 0x00000452
		public Func<Exception> NotFoundExceptionCreator { get; set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000012 RID: 18 RVA: 0x0000225B File Offset: 0x0000045B
		// (set) Token: 0x06000013 RID: 19 RVA: 0x00002263 File Offset: 0x00000463
		public AnchorMailbox OriginalAnchorMailbox { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000014 RID: 20 RVA: 0x0000226C File Offset: 0x0000046C
		// (set) Token: 0x06000015 RID: 21 RVA: 0x00002274 File Offset: 0x00000474
		public bool CacheEntryCacheHit { get; private set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000016 RID: 22 RVA: 0x0000227D File Offset: 0x0000047D
		// (set) Token: 0x06000017 RID: 23 RVA: 0x00002285 File Offset: 0x00000485
		private protected BackEndCookieEntryBase IncomingCookieEntry { protected get; private set; }

		// Token: 0x06000018 RID: 24 RVA: 0x0000228E File Offset: 0x0000048E
		public virtual string GetOrganizationNameForLogging()
		{
			if (this.loadedCachedEntry != null)
			{
				return this.loadedCachedEntry.DomainName;
			}
			return null;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000022A5 File Offset: 0x000004A5
		public virtual BackEndServer TryDirectBackEndCalculation()
		{
			return null;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000022A8 File Offset: 0x000004A8
		public virtual BackEndServer AcceptBackEndCookie(HttpCookie backEndCookie)
		{
			if (backEndCookie == null)
			{
				throw new ArgumentNullException("backEndCookie");
			}
			string name = this.ToCookieKey();
			string text = backEndCookie.Values[name];
			BackEndServer backEndServer = null;
			if (!string.IsNullOrEmpty(text))
			{
				try
				{
					BackEndCookieEntryBase backEndCookieEntryBase;
					string text2;
					if (!BackEndCookieEntryParser.TryParse(text, out backEndCookieEntryBase, out text2))
					{
						throw new InvalidBackEndCookieException();
					}
					if (backEndCookieEntryBase.Expired)
					{
						ExTraceGlobals.VerboseTracer.TraceDebug<BackEndCookieEntryBase>((long)this.GetHashCode(), "[AnchorMailbox::ProcessBackEndCookie]: Back end cookie entry {0} has expired.", backEndCookieEntryBase);
						this.RequestContext.Logger.SafeSet(HttpProxyMetadata.BackEndCookie, string.Format("Expired~{0}", text2));
						throw new InvalidBackEndCookieException();
					}
					this.RequestContext.Logger.SafeSet(HttpProxyMetadata.BackEndCookie, text2);
					this.IncomingCookieEntry = backEndCookieEntryBase;
					this.CacheEntryCacheHit = true;
					PerfCounters.HttpProxyCacheCountersInstance.CookieUseRate.Increment();
					PerfCounters.UpdateMovingPercentagePerformanceCounter(PerfCounters.HttpProxyCacheCountersInstance.MovingPercentageCookieUseRate);
					backEndServer = this.TryGetBackEndFromCookie(this.IncomingCookieEntry);
					ExTraceGlobals.VerboseTracer.TraceDebug<BackEndServer, BackEndCookieEntryBase>((long)this.GetHashCode(), "[AnchorMailbox::ProcessBackEndCookie]: Back end server {0} resolved from cookie {1}.", backEndServer, this.IncomingCookieEntry);
				}
				catch (InvalidBackEndCookieException)
				{
					ExTraceGlobals.VerboseTracer.TraceDebug((long)this.GetHashCode(), "[AnchorMailbox::ProcessBackEndCookie]: Invalid back end cookie entry.");
					backEndCookie.Values.Remove(name);
				}
			}
			return backEndServer;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000023E4 File Offset: 0x000005E4
		public virtual BackEndCookieEntryBase BuildCookieEntryForTarget(BackEndServer routingTarget, bool proxyToDownLevel, bool useResourceForest)
		{
			if (routingTarget == null)
			{
				throw new ArgumentNullException("routingTarget");
			}
			return new BackEndServerCookieEntry(routingTarget.Fqdn, routingTarget.Version);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002405 File Offset: 0x00000605
		public virtual string ToCookieKey()
		{
			if (this.OriginalAnchorMailbox != null)
			{
				return this.OriginalAnchorMailbox.ToCookieKey();
			}
			return this.SourceObject.ToString().Replace(" ", "_").Replace("=", "+");
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002444 File Offset: 0x00000644
		public virtual IRoutingEntry GetRoutingEntry()
		{
			return null;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002447 File Offset: 0x00000647
		public override string ToString()
		{
			return string.Format("{0}~{1}", this.AnchorSource, this.SourceObject);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002464 File Offset: 0x00000664
		public void UpdateCache(AnchorMailboxCacheEntry cacheEntry)
		{
			string key = this.ToCacheKey();
			AnchorMailboxCache.Instance.Add(key, cacheEntry, this.RequestContext);
			if (HttpProxySettings.NegativeAnchorMailboxCacheEnabled.Value)
			{
				NegativeAnchorMailboxCache.Instance.Remove(key);
			}
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000024A4 File Offset: 0x000006A4
		public void InvalidateCache()
		{
			string key = this.ToCacheKey();
			AnchorMailboxCache.Instance.Remove(key, this.RequestContext);
			if (HttpProxySettings.NegativeAnchorMailboxCacheEnabled.Value)
			{
				NegativeAnchorMailboxCache.Instance.Remove(key);
			}
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000024E0 File Offset: 0x000006E0
		public void UpdateNegativeCache(NegativeAnchorMailboxCacheEntry cacheEntry)
		{
			if (!HttpProxySettings.NegativeAnchorMailboxCacheEnabled.Value)
			{
				return;
			}
			string key = this.ToCacheKey();
			NegativeAnchorMailboxCache.Instance.Add(key, cacheEntry);
			AnchorMailboxCache.Instance.Remove(key, this.RequestContext);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002520 File Offset: 0x00000720
		public NegativeAnchorMailboxCacheEntry GetNegativeCacheEntry()
		{
			if (!HttpProxySettings.NegativeAnchorMailboxCacheEnabled.Value)
			{
				return null;
			}
			string key = this.ToCacheKey();
			NegativeAnchorMailboxCacheEntry result;
			if (NegativeAnchorMailboxCache.Instance.TryGet(key, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002554 File Offset: 0x00000754
		protected virtual BackEndServer TryGetBackEndFromCookie(BackEndCookieEntryBase cookieEntry)
		{
			BackEndServerCookieEntry backEndServerCookieEntry = cookieEntry as BackEndServerCookieEntry;
			if (backEndServerCookieEntry != null)
			{
				ExTraceGlobals.VerboseTracer.TraceDebug<string>((long)this.GetHashCode(), "[AnchorMailbox::TryGetBackEndFromCookie]: BackEndServerCookier {0}", backEndServerCookieEntry.ToString());
				return new BackEndServer(backEndServerCookieEntry.Fqdn, backEndServerCookieEntry.Version);
			}
			ExTraceGlobals.VerboseTracer.TraceDebug((long)this.GetHashCode(), "[AnchorMailbox::TryGetBackEndFromCookie]: No BackEndServerCookie");
			return null;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000025B0 File Offset: 0x000007B0
		protected AnchorMailboxCacheEntry GetCacheEntry()
		{
			if (this.loadedCachedEntry == null)
			{
				this.loadedCachedEntry = this.LoadCacheEntryFromIncomingCookie();
				string key = this.ToCacheKey();
				if (this.loadedCachedEntry == null)
				{
					if (AnchorMailboxCache.Instance.TryGet(key, this.RequestContext, out this.loadedCachedEntry))
					{
						ExTraceGlobals.VerboseTracer.TraceDebug<AnchorMailboxCacheEntry, AnchorMailbox>((long)this.GetHashCode(), "[AnchorMailbox::GetCacheEntry]: Using cached entry {0} for anchor mailbox {1}.", this.loadedCachedEntry, this);
						this.CacheEntryCacheHit = true;
					}
					else
					{
						this.loadedCachedEntry = this.RefreshCacheEntry();
						ExTraceGlobals.VerboseTracer.TraceDebug<AnchorMailboxCacheEntry, AnchorMailbox>((long)this.GetHashCode(), "[AnchorMailbox::GetCacheEntry]: RefreshCacheEntry() returns {0} for anchor mailbox {1}.", this.loadedCachedEntry, this);
						if (this.ShouldAddEntryToAnchorMailboxCache(this.loadedCachedEntry))
						{
							this.UpdateCache(this.loadedCachedEntry);
						}
						else
						{
							ExTraceGlobals.VerboseTracer.TraceDebug<AnchorMailboxCacheEntry, AnchorMailbox>((long)this.GetHashCode(), "[AnchorMailbox::GetCacheEntry]: Will not add cache entry {0} for anchor mailbox {1}.", this.loadedCachedEntry, this);
						}
					}
				}
				else
				{
					this.CacheEntryCacheHit = true;
				}
				this.OnPopulateCacheEntry(this.loadedCachedEntry);
			}
			return this.loadedCachedEntry;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000026A2 File Offset: 0x000008A2
		protected virtual bool ShouldAddEntryToAnchorMailboxCache(AnchorMailboxCacheEntry cacheEntry)
		{
			return true;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000026A5 File Offset: 0x000008A5
		protected virtual void OnPopulateCacheEntry(AnchorMailboxCacheEntry cacheEntry)
		{
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000026A7 File Offset: 0x000008A7
		protected virtual AnchorMailboxCacheEntry LoadCacheEntryFromIncomingCookie()
		{
			return null;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000026AA File Offset: 0x000008AA
		protected virtual AnchorMailboxCacheEntry RefreshCacheEntry()
		{
			return null;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000026AD File Offset: 0x000008AD
		protected virtual string ToCacheKey()
		{
			return this.ToString().Replace(" ", "_");
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000026C4 File Offset: 0x000008C4
		protected T CheckForNullAndThrowIfApplicable<T>(T ret)
		{
			if (ret == null && this.NotFoundExceptionCreator != null)
			{
				throw this.NotFoundExceptionCreator();
			}
			return ret;
		}

		// Token: 0x04000016 RID: 22
		public static readonly BoolAppSettingsEntry AllowMissingTenant = new BoolAppSettingsEntry("AnchorMailbox.AllowMissingTenant", false, ExTraceGlobals.VerboseTracer);

		// Token: 0x04000017 RID: 23
		private AnchorMailboxCacheEntry loadedCachedEntry;
	}
}
