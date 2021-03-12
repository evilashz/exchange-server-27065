using System;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Common.Cache;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Security.RightsManagement;

namespace Microsoft.Exchange.Data.Storage.RightsManagement
{
	// Token: 0x02000B5D RID: 2909
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class RmsPerformanceCounters : IWSManagerPerfCounters
	{
		// Token: 0x06006963 RID: 26979 RVA: 0x001C48C8 File Offset: 0x001C2AC8
		public void Initialize()
		{
			foreach (ExPerformanceCounter exPerformanceCounter in RmsPerfCounters.AllCounters)
			{
				exPerformanceCounter.RawValue = 0L;
			}
		}

		// Token: 0x06006964 RID: 26980 RVA: 0x001C48F5 File Offset: 0x001C2AF5
		public void CertifySuccessful(long elapsedMilliseconds)
		{
			RmsPerfCounters.TotalSuccessfulCertify.Increment();
			RmsPerfCounters.TotalSuccessfulCertifyTime.IncrementBy(elapsedMilliseconds);
		}

		// Token: 0x06006965 RID: 26981 RVA: 0x001C490E File Offset: 0x001C2B0E
		public void CertifyFailed()
		{
			RmsPerfCounters.TotalFailedCertify.Increment();
		}

		// Token: 0x06006966 RID: 26982 RVA: 0x001C491B File Offset: 0x001C2B1B
		public void GetClientLicensorCertSuccessful(long elapsedMilliseconds)
		{
			RmsPerfCounters.TotalSuccessfulGetClientLicensorCert.Increment();
			RmsPerfCounters.TotalSuccessfulGetClientLicensorCertTime.IncrementBy(elapsedMilliseconds);
		}

		// Token: 0x06006967 RID: 26983 RVA: 0x001C4934 File Offset: 0x001C2B34
		public void GetClientLicensorCertFailed()
		{
			RmsPerfCounters.TotalFailedGetClientLicensorCert.Increment();
		}

		// Token: 0x06006968 RID: 26984 RVA: 0x001C4941 File Offset: 0x001C2B41
		public void AcquireLicenseSuccessful(long elapsedMilliseconds)
		{
			RmsPerfCounters.TotalSuccessfulAcquireLicense.Increment();
			RmsPerfCounters.TotalSuccessfulAcquireLicenseTime.IncrementBy(elapsedMilliseconds);
		}

		// Token: 0x06006969 RID: 26985 RVA: 0x001C495A File Offset: 0x001C2B5A
		public void AcquireLicenseFailed()
		{
			RmsPerfCounters.TotalFailedAcquireLicense.Increment();
		}

		// Token: 0x0600696A RID: 26986 RVA: 0x001C4967 File Offset: 0x001C2B67
		public void AcquirePreLicenseSuccessful(long elapsedMilliseconds)
		{
			RmsPerfCounters.TotalSuccessfulAcquirePreLicense.Increment();
			RmsPerfCounters.TotalSuccessfulAcquirePreLicenseTime.IncrementBy(elapsedMilliseconds);
		}

		// Token: 0x0600696B RID: 26987 RVA: 0x001C4980 File Offset: 0x001C2B80
		public void AcquirePreLicenseFailed()
		{
			RmsPerfCounters.TotalFailedAcquirePreLicense.Increment();
		}

		// Token: 0x0600696C RID: 26988 RVA: 0x001C498D File Offset: 0x001C2B8D
		public void AcquireTemplatesSuccessful(long elapsedMilliseconds)
		{
			RmsPerfCounters.TotalSuccessfulAcquireTemplates.Increment();
			RmsPerfCounters.TotalSuccessfulAcquireTemplatesTime.IncrementBy(elapsedMilliseconds);
		}

		// Token: 0x0600696D RID: 26989 RVA: 0x001C49A6 File Offset: 0x001C2BA6
		public void AcquireTemplatesFailed()
		{
			RmsPerfCounters.TotalFailedAcquireTemplates.Increment();
		}

		// Token: 0x0600696E RID: 26990 RVA: 0x001C49B3 File Offset: 0x001C2BB3
		public void FindServiceLocationsSuccessful(long elapsedMilliseconds)
		{
			RmsPerfCounters.TotalSuccessfulFindServiceLocations.Increment();
			RmsPerfCounters.TotalSuccessfulFindServiceLocationsTime.IncrementBy(elapsedMilliseconds);
		}

		// Token: 0x0600696F RID: 26991 RVA: 0x001C49CC File Offset: 0x001C2BCC
		public void FindServiceLocationsFailed()
		{
			RmsPerfCounters.TotalFailedFindServiceLocations.Increment();
		}

		// Token: 0x06006970 RID: 26992 RVA: 0x001C49D9 File Offset: 0x001C2BD9
		public void WCFCertifySuccessful()
		{
			RmsPerfCounters.TotalExternalSuccessfulCertify.Increment();
		}

		// Token: 0x06006971 RID: 26993 RVA: 0x001C49E6 File Offset: 0x001C2BE6
		public void WCFCertifyFailed()
		{
			RmsPerfCounters.TotalExternalFailedCertify.Increment();
		}

		// Token: 0x06006972 RID: 26994 RVA: 0x001C49F3 File Offset: 0x001C2BF3
		public void WCFAcquireServerLicenseSuccessful()
		{
			RmsPerfCounters.TotalExternalSuccessfulAcquireLicense.Increment();
		}

		// Token: 0x06006973 RID: 26995 RVA: 0x001C4A00 File Offset: 0x001C2C00
		public void WCFAcquireServerLicenseFailed()
		{
			RmsPerfCounters.TotalExternalFailedAcquireLicense.Increment();
		}

		// Token: 0x04003C03 RID: 15363
		public readonly RmsPerformanceCounters.ServerInfoMapPerformanceCounters ServerInfoMapPerfCounters = new RmsPerformanceCounters.ServerInfoMapPerformanceCounters();

		// Token: 0x04003C04 RID: 15364
		public readonly RmsPerformanceCounters.LicenseStorePerformanceCounters LicenseStorePerfCounters = new RmsPerformanceCounters.LicenseStorePerformanceCounters();

		// Token: 0x02000B5E RID: 2910
		public sealed class ServerInfoMapPerformanceCounters : IMruDictionaryPerfCounters
		{
			// Token: 0x06006975 RID: 26997 RVA: 0x001C4A2B File Offset: 0x001C2C2B
			public void CacheHit()
			{
				RmsPerfCounters.TotalRmsServerInfoCacheHit.Increment();
			}

			// Token: 0x06006976 RID: 26998 RVA: 0x001C4A38 File Offset: 0x001C2C38
			public void CacheMiss()
			{
				RmsPerfCounters.TotalRmsServerInfoCacheMiss.Increment();
			}

			// Token: 0x06006977 RID: 26999 RVA: 0x001C4A45 File Offset: 0x001C2C45
			public void CacheAdd(bool overwrite, bool remove)
			{
				if (overwrite)
				{
					return;
				}
				if (remove)
				{
					RmsPerfCounters.TotalRmsServerInfoCacheRemove.Increment();
					RmsPerfCounters.TotalRmsServerInfoCacheAdd.Increment();
					return;
				}
				RmsPerfCounters.TotalRmsServerInfoCacheAdd.Increment();
			}

			// Token: 0x06006978 RID: 27000 RVA: 0x001C4A70 File Offset: 0x001C2C70
			public void CacheRemove()
			{
				RmsPerfCounters.TotalRmsServerInfoCacheRemove.Increment();
			}

			// Token: 0x06006979 RID: 27001 RVA: 0x001C4A7D File Offset: 0x001C2C7D
			public void FileRead(int count)
			{
			}

			// Token: 0x0600697A RID: 27002 RVA: 0x001C4A7F File Offset: 0x001C2C7F
			public void FileWrite(int count)
			{
			}
		}

		// Token: 0x02000B5F RID: 2911
		public sealed class LicenseStorePerformanceCounters : IMruDictionaryPerfCounters, ICachePerformanceCounters
		{
			// Token: 0x0600697C RID: 27004 RVA: 0x001C4A89 File Offset: 0x001C2C89
			public void CacheHit()
			{
				RmsPerfCounters.TotalLicenseStoreL2CacheHit.Increment();
			}

			// Token: 0x0600697D RID: 27005 RVA: 0x001C4A96 File Offset: 0x001C2C96
			public void CacheMiss()
			{
				RmsPerfCounters.TotalLicenseStoreL2CacheMiss.Increment();
			}

			// Token: 0x0600697E RID: 27006 RVA: 0x001C4AA3 File Offset: 0x001C2CA3
			public void CacheAdd(bool overwrite, bool remove)
			{
				if (overwrite)
				{
					return;
				}
				if (remove)
				{
					RmsPerfCounters.TotalLicenseStoreCacheRemove.Increment();
					RmsPerfCounters.TotalLicenseStoreCacheAdd.Increment();
					return;
				}
				RmsPerfCounters.TotalLicenseStoreCacheAdd.Increment();
			}

			// Token: 0x0600697F RID: 27007 RVA: 0x001C4ACE File Offset: 0x001C2CCE
			public void CacheRemove()
			{
				RmsPerfCounters.TotalLicenseStoreCacheRemove.Increment();
			}

			// Token: 0x06006980 RID: 27008 RVA: 0x001C4ADB File Offset: 0x001C2CDB
			public void FileRead(int count)
			{
				RmsPerfCounters.TotalLicenseStoreFileRead.IncrementBy((long)count);
			}

			// Token: 0x06006981 RID: 27009 RVA: 0x001C4AEA File Offset: 0x001C2CEA
			public void FileWrite(int count)
			{
				RmsPerfCounters.TotalLicenseStoreFileWrite.IncrementBy((long)count);
			}

			// Token: 0x06006982 RID: 27010 RVA: 0x001C4AF9 File Offset: 0x001C2CF9
			public void Accessed(AccessStatus accessStatus)
			{
				if (accessStatus == AccessStatus.Hit)
				{
					RmsPerfCounters.TotalLicenseStoreL1CacheHit.Increment();
					return;
				}
				RmsPerfCounters.TotalLicenseStoreL1CacheMiss.Increment();
			}

			// Token: 0x06006983 RID: 27011 RVA: 0x001C4B15 File Offset: 0x001C2D15
			public void SizeUpdated(long cacheSize)
			{
			}
		}
	}
}
