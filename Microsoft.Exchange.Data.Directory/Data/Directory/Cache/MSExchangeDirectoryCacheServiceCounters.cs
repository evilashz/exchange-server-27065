using System;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Directory.Cache
{
	// Token: 0x02000A43 RID: 2627
	internal static class MSExchangeDirectoryCacheServiceCounters
	{
		// Token: 0x0600784A RID: 30794 RVA: 0x0018D808 File Offset: 0x0018BA08
		public static void GetPerfCounterInfo(XElement element)
		{
			if (MSExchangeDirectoryCacheServiceCounters.AllCounters == null)
			{
				return;
			}
			foreach (ExPerformanceCounter exPerformanceCounter in MSExchangeDirectoryCacheServiceCounters.AllCounters)
			{
				try
				{
					element.Add(new XElement(ExPerformanceCounter.GetEncodedName(exPerformanceCounter.CounterName), exPerformanceCounter.NextValue()));
				}
				catch (XmlException ex)
				{
					XElement content = new XElement("Error", ex.Message);
					element.Add(content);
				}
			}
		}

		// Token: 0x04004F14 RID: 20244
		public const string CategoryName = "MSExchange Directory Cache Service";

		// Token: 0x04004F15 RID: 20245
		public static readonly ExPerformanceCounter CacheHitRatio = new ExPerformanceCounter("MSExchange Directory Cache Service", "Cache Hit Ratio", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04004F16 RID: 20246
		public static readonly ExPerformanceCounter CacheHitRatioBase = new ExPerformanceCounter("MSExchange Directory Cache Service", "Cache Hit Ratio Base", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04004F17 RID: 20247
		public static readonly ExPerformanceCounter CacheHit = new ExPerformanceCounter("MSExchange Directory Cache Service", "Percentage of Directory cache hits for the last minute", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04004F18 RID: 20248
		private static readonly ExPerformanceCounter RateOfCacheReadRequest = new ExPerformanceCounter("MSExchange Directory Cache Service", "Cache Read Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04004F19 RID: 20249
		public static readonly ExPerformanceCounter NumberOfCacheReadRequests = new ExPerformanceCounter("MSExchange Directory Cache Service", "Cache Read Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			MSExchangeDirectoryCacheServiceCounters.RateOfCacheReadRequest
		});

		// Token: 0x04004F1A RID: 20250
		private static readonly ExPerformanceCounter RateOfCacheInsertionRequest = new ExPerformanceCounter("MSExchange Directory Cache Service", "Cache Insertion Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04004F1B RID: 20251
		public static readonly ExPerformanceCounter NumberOfCacheInsertionRequests = new ExPerformanceCounter("MSExchange Directory Cache Service", "Cache Insertion Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			MSExchangeDirectoryCacheServiceCounters.RateOfCacheInsertionRequest
		});

		// Token: 0x04004F1C RID: 20252
		private static readonly ExPerformanceCounter RateOfCacheRemovalRequest = new ExPerformanceCounter("MSExchange Directory Cache Service", "Cache Removal Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04004F1D RID: 20253
		public static readonly ExPerformanceCounter NumberOfCacheRemovalRequests = new ExPerformanceCounter("MSExchange Directory Cache Service", "Cache Removal Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			MSExchangeDirectoryCacheServiceCounters.RateOfCacheRemovalRequest
		});

		// Token: 0x04004F1E RID: 20254
		public static readonly ExPerformanceCounter AcceptedDomainHit = new ExPerformanceCounter("MSExchange Directory Cache Service", "Percentage of AcceptedDomain Cache Hits", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04004F1F RID: 20255
		public static readonly ExPerformanceCounter ConfigurationUnitHit = new ExPerformanceCounter("MSExchange Directory Cache Service", "Percentage of ConfigurationUnit Cache Hit", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04004F20 RID: 20256
		public static readonly ExPerformanceCounter RecipientHit = new ExPerformanceCounter("MSExchange Directory Cache Service", "Percentage of Recipient Cache Hit", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04004F21 RID: 20257
		public static readonly ExPerformanceCounter ADRawEntryCacheHit = new ExPerformanceCounter("MSExchange Directory Cache Service", "Percentage of ADRawEntry Cache Hit", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04004F22 RID: 20258
		public static readonly ExPerformanceCounter ADRawEntryPropertiesMismatchLastMinute = new ExPerformanceCounter("MSExchange Directory Cache Service", "Percentage of ADRawEntry Cache Properties Mismatch", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04004F23 RID: 20259
		public static readonly ExPerformanceCounter[] AllCounters = new ExPerformanceCounter[]
		{
			MSExchangeDirectoryCacheServiceCounters.CacheHitRatio,
			MSExchangeDirectoryCacheServiceCounters.CacheHitRatioBase,
			MSExchangeDirectoryCacheServiceCounters.CacheHit,
			MSExchangeDirectoryCacheServiceCounters.NumberOfCacheReadRequests,
			MSExchangeDirectoryCacheServiceCounters.NumberOfCacheInsertionRequests,
			MSExchangeDirectoryCacheServiceCounters.NumberOfCacheRemovalRequests,
			MSExchangeDirectoryCacheServiceCounters.AcceptedDomainHit,
			MSExchangeDirectoryCacheServiceCounters.ConfigurationUnitHit,
			MSExchangeDirectoryCacheServiceCounters.RecipientHit,
			MSExchangeDirectoryCacheServiceCounters.ADRawEntryCacheHit,
			MSExchangeDirectoryCacheServiceCounters.ADRawEntryPropertiesMismatchLastMinute
		};
	}
}
