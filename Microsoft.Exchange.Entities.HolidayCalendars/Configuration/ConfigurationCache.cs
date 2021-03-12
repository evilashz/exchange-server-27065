using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics.Components.Entities.HolidayCalendars;
using Microsoft.Exchange.Entities.HolidayCalendars.Configuration.Exceptions;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Entities.HolidayCalendars.Configuration
{
	// Token: 0x02000002 RID: 2
	internal class ConfigurationCache
	{
		// Token: 0x06000002 RID: 2 RVA: 0x000020DE File Offset: 0x000002DE
		public ConfigurationCache(IEndpointInformationRetrieverFactory endpointRetrieverFactory = null, IDateTimeFactory dateTimeFactory = null)
		{
			this.dateTimeFactory = (dateTimeFactory ?? new DateTimeFactory());
			this.endpointRetrieverFactory = (endpointRetrieverFactory ?? new EndpointInformationRetrieverFactory());
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x00002114 File Offset: 0x00000314
		public int CacheSize
		{
			get
			{
				int count;
				lock (this.cache)
				{
					count = this.cache.Count;
				}
				return count;
			}
		}

		// Token: 0x06000004 RID: 4 RVA: 0x0000215C File Offset: 0x0000035C
		public UrlResolver GetUrlResolver(VariantConfigurationSnapshot configSnapshot)
		{
			return this.GetUrlResolver(new HolidayConfigurationSnapshot(configSnapshot));
		}

		// Token: 0x06000005 RID: 5 RVA: 0x0000216C File Offset: 0x0000036C
		public UrlResolver GetUrlResolver(HolidayConfigurationSnapshot configSnapshot)
		{
			ConfigurationCache.CacheEntry endpointCacheEntry = this.GetEndpointCacheEntry(configSnapshot);
			if (endpointCacheEntry.UrlResolver == null)
			{
				throw endpointCacheEntry.ErrorInformation;
			}
			return endpointCacheEntry.UrlResolver;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002198 File Offset: 0x00000398
		private ConfigurationCache.CacheEntry GetEndpointCacheEntry(HolidayConfigurationSnapshot configSnapshot)
		{
			Uri calendarEndpoint = configSnapshot.CalendarEndpoint;
			int configurationFetchTimeout = configSnapshot.ConfigurationFetchTimeout;
			ConfigurationCache.CacheEntry cacheEntry;
			lock (this.cache)
			{
				if (!this.cache.TryGetValue(calendarEndpoint.AbsoluteUri, out cacheEntry) || (cacheEntry.EndpointInformation == null && cacheEntry.UtcTimeStamp.AddMinutes(5.0) < this.dateTimeFactory.GetUtcNow()))
				{
					cacheEntry = this.RequestEndpointInfo(calendarEndpoint, configurationFetchTimeout);
				}
			}
			return cacheEntry;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002234 File Offset: 0x00000434
		private ConfigurationCache.CacheEntry RequestEndpointInfo(Uri endpoint, int timeout)
		{
			IEndpointInformationRetriever endpointInformationRetriever = this.endpointRetrieverFactory.Create(endpoint, timeout);
			ConfigurationCache.CacheEntry cacheEntry;
			try
			{
				EndpointInformation endpointInformation = endpointInformationRetriever.FetchEndpointInformation();
				cacheEntry = new ConfigurationCache.CacheEntry(this.dateTimeFactory.GetUtcNow(), endpointInformation);
			}
			catch (EndpointConfigurationException ex)
			{
				ExTraceGlobals.ConfigurationCacheTracer.TraceError<string>((long)this.GetHashCode(), "Unable to fetch endpoint info. {0}", ex.Message);
				cacheEntry = new ConfigurationCache.CacheEntry(this.dateTimeFactory.GetUtcNow(), ex);
			}
			if (this.cache.ContainsKey(endpoint.AbsoluteUri))
			{
				this.cache[endpoint.AbsoluteUri] = cacheEntry;
			}
			else
			{
				this.cache.Add(endpoint.AbsoluteUri, cacheEntry);
			}
			return cacheEntry;
		}

		// Token: 0x04000001 RID: 1
		private const int FailedConfigurationDownloadQuarantineInMinutes = 5;

		// Token: 0x04000002 RID: 2
		public static readonly ConfigurationCache Instance = new ConfigurationCache(null, null);

		// Token: 0x04000003 RID: 3
		private readonly Dictionary<string, ConfigurationCache.CacheEntry> cache = new Dictionary<string, ConfigurationCache.CacheEntry>();

		// Token: 0x04000004 RID: 4
		private readonly IDateTimeFactory dateTimeFactory;

		// Token: 0x04000005 RID: 5
		private readonly IEndpointInformationRetrieverFactory endpointRetrieverFactory;

		// Token: 0x02000003 RID: 3
		private class CacheEntry
		{
			// Token: 0x06000008 RID: 8 RVA: 0x000022E8 File Offset: 0x000004E8
			public CacheEntry(ExDateTime utcTimeStamp, EndpointInformation endpointInformation)
			{
				this.UtcTimeStamp = utcTimeStamp;
				this.EndpointInformation = endpointInformation;
			}

			// Token: 0x06000009 RID: 9 RVA: 0x000022FE File Offset: 0x000004FE
			public CacheEntry(ExDateTime utcTimeStamp, Exception error)
			{
				this.UtcTimeStamp = utcTimeStamp;
				this.ErrorInformation = error;
			}

			// Token: 0x17000002 RID: 2
			// (get) Token: 0x0600000A RID: 10 RVA: 0x00002314 File Offset: 0x00000514
			// (set) Token: 0x0600000B RID: 11 RVA: 0x0000231C File Offset: 0x0000051C
			public ExDateTime UtcTimeStamp { get; private set; }

			// Token: 0x17000003 RID: 3
			// (get) Token: 0x0600000C RID: 12 RVA: 0x00002325 File Offset: 0x00000525
			// (set) Token: 0x0600000D RID: 13 RVA: 0x0000232D File Offset: 0x0000052D
			public EndpointInformation EndpointInformation { get; private set; }

			// Token: 0x17000004 RID: 4
			// (get) Token: 0x0600000E RID: 14 RVA: 0x00002336 File Offset: 0x00000536
			// (set) Token: 0x0600000F RID: 15 RVA: 0x0000233E File Offset: 0x0000053E
			public Exception ErrorInformation { get; private set; }

			// Token: 0x17000005 RID: 5
			// (get) Token: 0x06000010 RID: 16 RVA: 0x00002347 File Offset: 0x00000547
			public UrlResolver UrlResolver
			{
				get
				{
					if (this.urlResolver == null && this.EndpointInformation != null)
					{
						this.urlResolver = new UrlResolver(this.EndpointInformation);
					}
					return this.urlResolver;
				}
			}

			// Token: 0x04000006 RID: 6
			private UrlResolver urlResolver;
		}
	}
}
