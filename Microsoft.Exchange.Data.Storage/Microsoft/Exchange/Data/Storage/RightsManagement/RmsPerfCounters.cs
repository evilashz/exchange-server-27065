using System;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.RightsManagement
{
	// Token: 0x020001B2 RID: 434
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal static class RmsPerfCounters
	{
		// Token: 0x060017C5 RID: 6085 RVA: 0x000739A4 File Offset: 0x00071BA4
		public static void GetPerfCounterInfo(XElement element)
		{
			if (RmsPerfCounters.AllCounters == null)
			{
				return;
			}
			foreach (ExPerformanceCounter exPerformanceCounter in RmsPerfCounters.AllCounters)
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

		// Token: 0x04000C3B RID: 3131
		public const string CategoryName = "MSExchange Rights Management";

		// Token: 0x04000C3C RID: 3132
		private static readonly ExPerformanceCounter RateOfSuccessfulCertify = new ExPerformanceCounter("MSExchange Rights Management", "Successful Certify()/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000C3D RID: 3133
		private static readonly ExPerformanceCounter AverageSuccessfulCertifyTime = new ExPerformanceCounter("MSExchange Rights Management", "Average time for successful Certify()", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000C3E RID: 3134
		public static readonly ExPerformanceCounter TotalSuccessfulCertifyTime = new ExPerformanceCounter("MSExchange Rights Management", "Total time for successful Certify()", string.Empty, null, new ExPerformanceCounter[]
		{
			RmsPerfCounters.AverageSuccessfulCertifyTime
		});

		// Token: 0x04000C3F RID: 3135
		private static readonly ExPerformanceCounter AverageSuccessfulCertifyTimeBase = new ExPerformanceCounter("MSExchange Rights Management", "Base of Average time for successful Certify()", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000C40 RID: 3136
		public static readonly ExPerformanceCounter TotalSuccessfulCertify = new ExPerformanceCounter("MSExchange Rights Management", "Total successful Certify()", string.Empty, null, new ExPerformanceCounter[]
		{
			RmsPerfCounters.RateOfSuccessfulCertify,
			RmsPerfCounters.AverageSuccessfulCertifyTimeBase
		});

		// Token: 0x04000C41 RID: 3137
		private static readonly ExPerformanceCounter RateOfFailedCertify = new ExPerformanceCounter("MSExchange Rights Management", "Failed Certify()/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000C42 RID: 3138
		public static readonly ExPerformanceCounter TotalFailedCertify = new ExPerformanceCounter("MSExchange Rights Management", "Total failed Certify()", string.Empty, null, new ExPerformanceCounter[]
		{
			RmsPerfCounters.RateOfFailedCertify
		});

		// Token: 0x04000C43 RID: 3139
		private static readonly ExPerformanceCounter RateOfSuccessfulGetClientLicensorCert = new ExPerformanceCounter("MSExchange Rights Management", "Successful GetClientLicensorCert()/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000C44 RID: 3140
		private static readonly ExPerformanceCounter AverageSuccessfulGetClientLicensorCertTime = new ExPerformanceCounter("MSExchange Rights Management", "Average time for successful GetClientLicensorCert()", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000C45 RID: 3141
		public static readonly ExPerformanceCounter TotalSuccessfulGetClientLicensorCertTime = new ExPerformanceCounter("MSExchange Rights Management", "Total time for successful GetClientLicensorCert()", string.Empty, null, new ExPerformanceCounter[]
		{
			RmsPerfCounters.AverageSuccessfulGetClientLicensorCertTime
		});

		// Token: 0x04000C46 RID: 3142
		private static readonly ExPerformanceCounter AverageSuccessfulGetClientLicensorCertTimeBase = new ExPerformanceCounter("MSExchange Rights Management", "Base of Average time for successful GetClientLicensorCert()", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000C47 RID: 3143
		public static readonly ExPerformanceCounter TotalSuccessfulGetClientLicensorCert = new ExPerformanceCounter("MSExchange Rights Management", "Total successful GetClientLicensorCert()", string.Empty, null, new ExPerformanceCounter[]
		{
			RmsPerfCounters.RateOfSuccessfulGetClientLicensorCert,
			RmsPerfCounters.AverageSuccessfulGetClientLicensorCertTimeBase
		});

		// Token: 0x04000C48 RID: 3144
		private static readonly ExPerformanceCounter RateOfFailedGetClientLicensorCert = new ExPerformanceCounter("MSExchange Rights Management", "Failed GetClientLicensorCert()/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000C49 RID: 3145
		public static readonly ExPerformanceCounter TotalFailedGetClientLicensorCert = new ExPerformanceCounter("MSExchange Rights Management", "Total failed GetClientLicensorCert()", string.Empty, null, new ExPerformanceCounter[]
		{
			RmsPerfCounters.RateOfFailedGetClientLicensorCert
		});

		// Token: 0x04000C4A RID: 3146
		private static readonly ExPerformanceCounter RateOfSuccessfulAcquireLicense = new ExPerformanceCounter("MSExchange Rights Management", "Successful AcquireLicense()/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000C4B RID: 3147
		private static readonly ExPerformanceCounter AverageSuccessfulAcquireLicenseTime = new ExPerformanceCounter("MSExchange Rights Management", "Average time for successful AcquireLicense()", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000C4C RID: 3148
		public static readonly ExPerformanceCounter TotalSuccessfulAcquireLicenseTime = new ExPerformanceCounter("MSExchange Rights Management", "Total time for successful AcquireLicense()", string.Empty, null, new ExPerformanceCounter[]
		{
			RmsPerfCounters.AverageSuccessfulAcquireLicenseTime
		});

		// Token: 0x04000C4D RID: 3149
		private static readonly ExPerformanceCounter AverageSuccessfulAcquireLicenseTimeBase = new ExPerformanceCounter("MSExchange Rights Management", "Base of Average time for successful AcquireLicense()", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000C4E RID: 3150
		public static readonly ExPerformanceCounter TotalSuccessfulAcquireLicense = new ExPerformanceCounter("MSExchange Rights Management", "Total successful AcquireLicense()", string.Empty, null, new ExPerformanceCounter[]
		{
			RmsPerfCounters.RateOfSuccessfulAcquireLicense,
			RmsPerfCounters.AverageSuccessfulAcquireLicenseTimeBase
		});

		// Token: 0x04000C4F RID: 3151
		private static readonly ExPerformanceCounter RateOfFailedAcquireLicense = new ExPerformanceCounter("MSExchange Rights Management", "Failed AcquireLicense()/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000C50 RID: 3152
		public static readonly ExPerformanceCounter TotalFailedAcquireLicense = new ExPerformanceCounter("MSExchange Rights Management", "Total failed AcquireLicense()", string.Empty, null, new ExPerformanceCounter[]
		{
			RmsPerfCounters.RateOfFailedAcquireLicense
		});

		// Token: 0x04000C51 RID: 3153
		private static readonly ExPerformanceCounter RateOfSuccessfulAcquirePreLicense = new ExPerformanceCounter("MSExchange Rights Management", "Successful AcquirePreLicense()/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000C52 RID: 3154
		private static readonly ExPerformanceCounter AverageSuccessfulAcquirePreLicenseTime = new ExPerformanceCounter("MSExchange Rights Management", "Average time for successful AcquirePreLicense()", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000C53 RID: 3155
		public static readonly ExPerformanceCounter TotalSuccessfulAcquirePreLicenseTime = new ExPerformanceCounter("MSExchange Rights Management", "Total time for successful AcquirePreLicense()", string.Empty, null, new ExPerformanceCounter[]
		{
			RmsPerfCounters.AverageSuccessfulAcquirePreLicenseTime
		});

		// Token: 0x04000C54 RID: 3156
		private static readonly ExPerformanceCounter AverageSuccessfulAcquirePreLicenseTimeBase = new ExPerformanceCounter("MSExchange Rights Management", "Base of Average time for successful AcquirePreLicense()", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000C55 RID: 3157
		public static readonly ExPerformanceCounter TotalSuccessfulAcquirePreLicense = new ExPerformanceCounter("MSExchange Rights Management", "Total successful AcquirePreLicense()", string.Empty, null, new ExPerformanceCounter[]
		{
			RmsPerfCounters.RateOfSuccessfulAcquirePreLicense,
			RmsPerfCounters.AverageSuccessfulAcquirePreLicenseTimeBase
		});

		// Token: 0x04000C56 RID: 3158
		private static readonly ExPerformanceCounter RateOfFailedAcquirePreLicense = new ExPerformanceCounter("MSExchange Rights Management", "Failed AcquirePreLicense()/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000C57 RID: 3159
		public static readonly ExPerformanceCounter TotalFailedAcquirePreLicense = new ExPerformanceCounter("MSExchange Rights Management", "Total failed AcquirePreLicense()", string.Empty, null, new ExPerformanceCounter[]
		{
			RmsPerfCounters.RateOfFailedAcquirePreLicense
		});

		// Token: 0x04000C58 RID: 3160
		private static readonly ExPerformanceCounter RateOfSuccessfulFindServiceLocations = new ExPerformanceCounter("MSExchange Rights Management", "Successful FindServiceLocations()/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000C59 RID: 3161
		private static readonly ExPerformanceCounter AverageSuccessfulFindServiceLocationsTime = new ExPerformanceCounter("MSExchange Rights Management", "Average time for successful FindServiceLocations()", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000C5A RID: 3162
		public static readonly ExPerformanceCounter TotalSuccessfulFindServiceLocationsTime = new ExPerformanceCounter("MSExchange Rights Management", "Total time for successful FindServiceLocations()", string.Empty, null, new ExPerformanceCounter[]
		{
			RmsPerfCounters.AverageSuccessfulFindServiceLocationsTime
		});

		// Token: 0x04000C5B RID: 3163
		private static readonly ExPerformanceCounter AverageSuccessfulFindServiceLocationsTimeBase = new ExPerformanceCounter("MSExchange Rights Management", "Base of Average time for successful FindServiceLocations()", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000C5C RID: 3164
		public static readonly ExPerformanceCounter TotalSuccessfulFindServiceLocations = new ExPerformanceCounter("MSExchange Rights Management", "Total successful FindServiceLocations()", string.Empty, null, new ExPerformanceCounter[]
		{
			RmsPerfCounters.RateOfSuccessfulFindServiceLocations,
			RmsPerfCounters.AverageSuccessfulFindServiceLocationsTimeBase
		});

		// Token: 0x04000C5D RID: 3165
		private static readonly ExPerformanceCounter RateOfFailedFindServiceLocations = new ExPerformanceCounter("MSExchange Rights Management", "Failed FindServiceLocations()/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000C5E RID: 3166
		public static readonly ExPerformanceCounter TotalFailedFindServiceLocations = new ExPerformanceCounter("MSExchange Rights Management", "Total failed FindServiceLocations()", string.Empty, null, new ExPerformanceCounter[]
		{
			RmsPerfCounters.RateOfFailedFindServiceLocations
		});

		// Token: 0x04000C5F RID: 3167
		private static readonly ExPerformanceCounter RateOfSuccessfulAcquireTemplates = new ExPerformanceCounter("MSExchange Rights Management", "Successful AcquireTemplates()/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000C60 RID: 3168
		private static readonly ExPerformanceCounter AverageSuccessfulAcquireTemplatesTime = new ExPerformanceCounter("MSExchange Rights Management", "Average time for successful AcquireTemplates()", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000C61 RID: 3169
		public static readonly ExPerformanceCounter TotalSuccessfulAcquireTemplatesTime = new ExPerformanceCounter("MSExchange Rights Management", "Total time for successful AcquireTemplates()", string.Empty, null, new ExPerformanceCounter[]
		{
			RmsPerfCounters.AverageSuccessfulAcquireTemplatesTime
		});

		// Token: 0x04000C62 RID: 3170
		private static readonly ExPerformanceCounter AverageSuccessfulAcquireTemplatesTimeBase = new ExPerformanceCounter("MSExchange Rights Management", "Base of Average time for successful AcquireTemplates()", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000C63 RID: 3171
		public static readonly ExPerformanceCounter TotalSuccessfulAcquireTemplates = new ExPerformanceCounter("MSExchange Rights Management", "Total successful AcquireTemplates()", string.Empty, null, new ExPerformanceCounter[]
		{
			RmsPerfCounters.RateOfSuccessfulAcquireTemplates,
			RmsPerfCounters.AverageSuccessfulAcquireTemplatesTimeBase
		});

		// Token: 0x04000C64 RID: 3172
		private static readonly ExPerformanceCounter RateOfFailedAcquireTemplates = new ExPerformanceCounter("MSExchange Rights Management", "Failed AcquireTemplates()/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000C65 RID: 3173
		public static readonly ExPerformanceCounter TotalFailedAcquireTemplates = new ExPerformanceCounter("MSExchange Rights Management", "Total failed AcquireTemplates()", string.Empty, null, new ExPerformanceCounter[]
		{
			RmsPerfCounters.RateOfFailedAcquireTemplates
		});

		// Token: 0x04000C66 RID: 3174
		private static readonly ExPerformanceCounter RateOfRmsServerInfoCacheHit = new ExPerformanceCounter("MSExchange Rights Management", "Cache-hit of RMS Server Info/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000C67 RID: 3175
		public static readonly ExPerformanceCounter TotalRmsServerInfoCacheHit = new ExPerformanceCounter("MSExchange Rights Management", "Total cache-hit of RMS Server Info", string.Empty, null, new ExPerformanceCounter[]
		{
			RmsPerfCounters.RateOfRmsServerInfoCacheHit
		});

		// Token: 0x04000C68 RID: 3176
		private static readonly ExPerformanceCounter RateOfRmsServerInfoCacheMiss = new ExPerformanceCounter("MSExchange Rights Management", "Cache-miss of RMS Server Info/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000C69 RID: 3177
		public static readonly ExPerformanceCounter TotalRmsServerInfoCacheMiss = new ExPerformanceCounter("MSExchange Rights Management", "Total cache-miss of RMS Server Info", string.Empty, null, new ExPerformanceCounter[]
		{
			RmsPerfCounters.RateOfRmsServerInfoCacheMiss
		});

		// Token: 0x04000C6A RID: 3178
		private static readonly ExPerformanceCounter RateOfRmsServerInfoCacheAdd = new ExPerformanceCounter("MSExchange Rights Management", "Entries added into RMS Server Info cache/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000C6B RID: 3179
		public static readonly ExPerformanceCounter TotalRmsServerInfoCacheAdd = new ExPerformanceCounter("MSExchange Rights Management", "Total entries added into RMS Server Info cache.", string.Empty, null, new ExPerformanceCounter[]
		{
			RmsPerfCounters.RateOfRmsServerInfoCacheAdd
		});

		// Token: 0x04000C6C RID: 3180
		private static readonly ExPerformanceCounter RateOfRmsServerInfoCacheRemove = new ExPerformanceCounter("MSExchange Rights Management", "Entries removed from RMS Server Info cache/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000C6D RID: 3181
		public static readonly ExPerformanceCounter TotalRmsServerInfoCacheRemove = new ExPerformanceCounter("MSExchange Rights Management", "Total entries removed from RMS Server Info cache", string.Empty, null, new ExPerformanceCounter[]
		{
			RmsPerfCounters.RateOfRmsServerInfoCacheRemove
		});

		// Token: 0x04000C6E RID: 3182
		private static readonly ExPerformanceCounter RateOfLicenseStoreL1CacheHit = new ExPerformanceCounter("MSExchange Rights Management", "L1 Cache-Hit of RMS License Store/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000C6F RID: 3183
		public static readonly ExPerformanceCounter TotalLicenseStoreL1CacheHit = new ExPerformanceCounter("MSExchange Rights Management", "Total L1 Cache-Hit of Rms License Store", string.Empty, null, new ExPerformanceCounter[]
		{
			RmsPerfCounters.RateOfLicenseStoreL1CacheHit
		});

		// Token: 0x04000C70 RID: 3184
		private static readonly ExPerformanceCounter RateOfLicenseStoreL1CacheMiss = new ExPerformanceCounter("MSExchange Rights Management", "L1 Cache-Miss of RMS License Store/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000C71 RID: 3185
		public static readonly ExPerformanceCounter TotalLicenseStoreL1CacheMiss = new ExPerformanceCounter("MSExchange Rights Management", "Total L1 Cache-Miss of RMS License Store", string.Empty, null, new ExPerformanceCounter[]
		{
			RmsPerfCounters.RateOfLicenseStoreL1CacheMiss
		});

		// Token: 0x04000C72 RID: 3186
		private static readonly ExPerformanceCounter RateOfLicenseStoreL2CacheHit = new ExPerformanceCounter("MSExchange Rights Management", "L2 Cache-Hit of RMS License Store/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000C73 RID: 3187
		public static readonly ExPerformanceCounter TotalLicenseStoreL2CacheHit = new ExPerformanceCounter("MSExchange Rights Management", "Total L2 Cache-Hit of RMS License Store", string.Empty, null, new ExPerformanceCounter[]
		{
			RmsPerfCounters.RateOfLicenseStoreL2CacheHit
		});

		// Token: 0x04000C74 RID: 3188
		private static readonly ExPerformanceCounter RateOfLicenseStoreL2CacheMiss = new ExPerformanceCounter("MSExchange Rights Management", "L2 Cache-Miss of RMS License Store/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000C75 RID: 3189
		public static readonly ExPerformanceCounter TotalLicenseStoreL2CacheMiss = new ExPerformanceCounter("MSExchange Rights Management", "Total L2 Cache-Miss of RMS License Store", string.Empty, null, new ExPerformanceCounter[]
		{
			RmsPerfCounters.RateOfLicenseStoreL2CacheMiss
		});

		// Token: 0x04000C76 RID: 3190
		private static readonly ExPerformanceCounter RateOfLicenseStoreCacheAdd = new ExPerformanceCounter("MSExchange Rights Management", "Entries added into RMS License Store/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000C77 RID: 3191
		public static readonly ExPerformanceCounter TotalLicenseStoreCacheAdd = new ExPerformanceCounter("MSExchange Rights Management", "Total entries added into RMS License Store", string.Empty, null, new ExPerformanceCounter[]
		{
			RmsPerfCounters.RateOfLicenseStoreCacheAdd
		});

		// Token: 0x04000C78 RID: 3192
		private static readonly ExPerformanceCounter RateOfLicenseStoreCacheRemove = new ExPerformanceCounter("MSExchange Rights Management", "Entries removed from RMS License Store/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000C79 RID: 3193
		public static readonly ExPerformanceCounter TotalLicenseStoreCacheRemove = new ExPerformanceCounter("MSExchange Rights Management", "Total entries removed from RMS License Store", string.Empty, null, new ExPerformanceCounter[]
		{
			RmsPerfCounters.RateOfLicenseStoreCacheRemove
		});

		// Token: 0x04000C7A RID: 3194
		private static readonly ExPerformanceCounter RateOfLicenseStoreFileRead = new ExPerformanceCounter("MSExchange Rights Management", "Files read by RMS License Store/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000C7B RID: 3195
		public static readonly ExPerformanceCounter TotalLicenseStoreFileRead = new ExPerformanceCounter("MSExchange Rights Management", "Total files read by RMS License Store from Disk", string.Empty, null, new ExPerformanceCounter[]
		{
			RmsPerfCounters.RateOfLicenseStoreFileRead
		});

		// Token: 0x04000C7C RID: 3196
		private static readonly ExPerformanceCounter RateOfLicenseStoreFileWrite = new ExPerformanceCounter("MSExchange Rights Management", "Files written by RMS License Store/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000C7D RID: 3197
		public static readonly ExPerformanceCounter TotalLicenseStoreFileWrite = new ExPerformanceCounter("MSExchange Rights Management", "Total files written by RMS License Store to Disk", string.Empty, null, new ExPerformanceCounter[]
		{
			RmsPerfCounters.RateOfLicenseStoreFileWrite
		});

		// Token: 0x04000C7E RID: 3198
		private static readonly ExPerformanceCounter RateOfExternalSuccessfulCertify = new ExPerformanceCounter("MSExchange Rights Management", "Successful External Certification Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000C7F RID: 3199
		public static readonly ExPerformanceCounter TotalExternalSuccessfulCertify = new ExPerformanceCounter("MSExchange Rights Management", "Total external successful certification requests", string.Empty, null, new ExPerformanceCounter[]
		{
			RmsPerfCounters.RateOfExternalSuccessfulCertify
		});

		// Token: 0x04000C80 RID: 3200
		private static readonly ExPerformanceCounter RateOfExternalFailedCertify = new ExPerformanceCounter("MSExchange Rights Management", "Failed External Certification Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000C81 RID: 3201
		public static readonly ExPerformanceCounter TotalExternalFailedCertify = new ExPerformanceCounter("MSExchange Rights Management", "Total external failed certification requests", string.Empty, null, new ExPerformanceCounter[]
		{
			RmsPerfCounters.RateOfExternalFailedCertify
		});

		// Token: 0x04000C82 RID: 3202
		private static readonly ExPerformanceCounter RateOfExternalSuccessfulAcquireLicense = new ExPerformanceCounter("MSExchange Rights Management", "Successful External AcquireLicense Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000C83 RID: 3203
		public static readonly ExPerformanceCounter TotalExternalSuccessfulAcquireLicense = new ExPerformanceCounter("MSExchange Rights Management", "Total external successful AcquireLicense requests", string.Empty, null, new ExPerformanceCounter[]
		{
			RmsPerfCounters.RateOfExternalSuccessfulAcquireLicense
		});

		// Token: 0x04000C84 RID: 3204
		private static readonly ExPerformanceCounter RateOfExternalFailedAcquireLicense = new ExPerformanceCounter("MSExchange Rights Management", "Failed External AcquireLicense Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000C85 RID: 3205
		public static readonly ExPerformanceCounter TotalExternalFailedAcquireLicense = new ExPerformanceCounter("MSExchange Rights Management", "Total external failed AcquireLicense requests", string.Empty, null, new ExPerformanceCounter[]
		{
			RmsPerfCounters.RateOfExternalFailedAcquireLicense
		});

		// Token: 0x04000C86 RID: 3206
		public static readonly ExPerformanceCounter[] AllCounters = new ExPerformanceCounter[]
		{
			RmsPerfCounters.TotalSuccessfulCertify,
			RmsPerfCounters.TotalSuccessfulCertifyTime,
			RmsPerfCounters.TotalFailedCertify,
			RmsPerfCounters.TotalSuccessfulGetClientLicensorCert,
			RmsPerfCounters.TotalSuccessfulGetClientLicensorCertTime,
			RmsPerfCounters.TotalFailedGetClientLicensorCert,
			RmsPerfCounters.TotalSuccessfulAcquireLicense,
			RmsPerfCounters.TotalSuccessfulAcquireLicenseTime,
			RmsPerfCounters.TotalFailedAcquireLicense,
			RmsPerfCounters.TotalSuccessfulAcquirePreLicense,
			RmsPerfCounters.TotalSuccessfulAcquirePreLicenseTime,
			RmsPerfCounters.TotalFailedAcquirePreLicense,
			RmsPerfCounters.TotalSuccessfulFindServiceLocations,
			RmsPerfCounters.TotalSuccessfulFindServiceLocationsTime,
			RmsPerfCounters.TotalFailedFindServiceLocations,
			RmsPerfCounters.TotalSuccessfulAcquireTemplates,
			RmsPerfCounters.TotalSuccessfulAcquireTemplatesTime,
			RmsPerfCounters.TotalFailedAcquireTemplates,
			RmsPerfCounters.TotalRmsServerInfoCacheHit,
			RmsPerfCounters.TotalRmsServerInfoCacheMiss,
			RmsPerfCounters.TotalRmsServerInfoCacheAdd,
			RmsPerfCounters.TotalRmsServerInfoCacheRemove,
			RmsPerfCounters.TotalLicenseStoreL1CacheHit,
			RmsPerfCounters.TotalLicenseStoreL1CacheMiss,
			RmsPerfCounters.TotalLicenseStoreL2CacheHit,
			RmsPerfCounters.TotalLicenseStoreL2CacheMiss,
			RmsPerfCounters.TotalLicenseStoreCacheAdd,
			RmsPerfCounters.TotalLicenseStoreCacheRemove,
			RmsPerfCounters.TotalLicenseStoreFileRead,
			RmsPerfCounters.TotalLicenseStoreFileWrite,
			RmsPerfCounters.TotalExternalSuccessfulCertify,
			RmsPerfCounters.TotalExternalFailedCertify,
			RmsPerfCounters.TotalExternalSuccessfulAcquireLicense,
			RmsPerfCounters.TotalExternalFailedAcquireLicense
		};
	}
}
