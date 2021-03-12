using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;

namespace Microsoft.Exchange.Net.MonitoringWebClient
{
	// Token: 0x020007C8 RID: 1992
	internal class NameResolver
	{
		// Token: 0x06002936 RID: 10550 RVA: 0x000582D0 File Offset: 0x000564D0
		public NameResolver()
		{
		}

		// Token: 0x06002937 RID: 10551 RVA: 0x000582E3 File Offset: 0x000564E3
		public NameResolver(Dictionary<string, List<NamedVip>> staticMapping)
		{
			this.staticMapping = staticMapping;
		}

		// Token: 0x06002938 RID: 10552 RVA: 0x00058300 File Offset: 0x00056500
		public Uri ResolveUri(HttpWebRequestWrapper requestWrapper)
		{
			Uri requestUri = requestWrapper.RequestUri;
			IPAddress ipaddress;
			if (IPAddress.TryParse(requestUri.Host, out ipaddress))
			{
				return requestUri;
			}
			NameResolver.DnsCacheEntry orAdd;
			if (!this.dnsCache.TryGetValue(requestUri.Host, out orAdd))
			{
				Stopwatch stopwatch = new Stopwatch();
				List<NamedVip> list;
				try
				{
					stopwatch.Start();
					IPHostEntry hostEntry = Dns.GetHostEntry(requestUri.Host);
					list = new List<NamedVip>();
					foreach (IPAddress ipaddress2 in hostEntry.AddressList)
					{
						list.Add(new NamedVip
						{
							IPAddress = ipaddress2,
							Name = requestUri.Host
						});
					}
				}
				catch (Exception innerException)
				{
					throw new NameResolutionException(MonitoringWebClientStrings.NameResolutionFailure(requestUri.Host), requestWrapper, innerException, requestUri.Host);
				}
				finally
				{
					stopwatch.Stop();
					requestWrapper.DnsLatency = stopwatch.Elapsed;
				}
				List<NamedVip> list2 = this.ResolveStaticEntries(requestUri.Host);
				if (list2 != null)
				{
					list = list2;
				}
				list.Shuffle<NamedVip>();
				orAdd = this.dnsCache.GetOrAdd(requestUri.Host, new NameResolver.DnsCacheEntry
				{
					HostEntry = list,
					CurrentEntry = 0
				});
			}
			NamedVip namedVip = orAdd.HostEntry[orAdd.CurrentEntry];
			UriBuilder uriBuilder = new UriBuilder(requestUri.Scheme, namedVip.IPAddress.ToString());
			requestWrapper.TargetVipName = namedVip.Name;
			requestWrapper.TargetVipForestName = namedVip.ForestName;
			if (!requestUri.IsDefaultPort)
			{
				uriBuilder.Port = requestUri.Port;
			}
			uriBuilder.Path = requestUri.AbsolutePath;
			if (!string.IsNullOrEmpty(requestUri.Query))
			{
				uriBuilder.Query = requestUri.Query.TrimStart(new char[]
				{
					'?'
				});
			}
			return uriBuilder.Uri;
		}

		// Token: 0x06002939 RID: 10553 RVA: 0x000584DC File Offset: 0x000566DC
		public bool ShouldRetryWithDnsRoundRobin(string hostName)
		{
			NameResolver.DnsCacheEntry dnsCacheEntry = this.dnsCache[hostName];
			dnsCacheEntry.CurrentEntry++;
			if (dnsCacheEntry.CurrentEntry >= dnsCacheEntry.HostEntry.Count)
			{
				dnsCacheEntry.CurrentEntry = 0;
				return false;
			}
			return true;
		}

		// Token: 0x0600293A RID: 10554 RVA: 0x00058521 File Offset: 0x00056721
		private List<NamedVip> ResolveStaticEntries(string hostname)
		{
			if (this.staticMapping == null)
			{
				return null;
			}
			if (!this.staticMapping.ContainsKey(hostname))
			{
				return null;
			}
			return this.staticMapping[hostname];
		}

		// Token: 0x0400247B RID: 9339
		private ConcurrentDictionary<string, NameResolver.DnsCacheEntry> dnsCache = new ConcurrentDictionary<string, NameResolver.DnsCacheEntry>();

		// Token: 0x0400247C RID: 9340
		private Dictionary<string, List<NamedVip>> staticMapping;

		// Token: 0x020007C9 RID: 1993
		private class DnsCacheEntry
		{
			// Token: 0x17000AEA RID: 2794
			// (get) Token: 0x0600293B RID: 10555 RVA: 0x00058549 File Offset: 0x00056749
			// (set) Token: 0x0600293C RID: 10556 RVA: 0x00058551 File Offset: 0x00056751
			public List<NamedVip> HostEntry { get; set; }

			// Token: 0x17000AEB RID: 2795
			// (get) Token: 0x0600293D RID: 10557 RVA: 0x0005855A File Offset: 0x0005675A
			// (set) Token: 0x0600293E RID: 10558 RVA: 0x00058562 File Offset: 0x00056762
			public int CurrentEntry { get; set; }
		}
	}
}
