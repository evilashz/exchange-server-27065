using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Reflection;
using Microsoft.Exchange.Data.Directory.ProvisioningCache;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Transport;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;
using Microsoft.Office.CompliancePolicy.PolicySync;

namespace Microsoft.Office.CompliancePolicy.Tasks
{
	// Token: 0x020000BA RID: 186
	internal abstract class CompliancePolicySyncNotificationClient
	{
		// Token: 0x1700025D RID: 605
		// (get) Token: 0x060006CA RID: 1738 RVA: 0x0001C13D File Offset: 0x0001A33D
		// (set) Token: 0x060006CB RID: 1739 RVA: 0x0001C145 File Offset: 0x0001A345
		public Workload Workload { get; private set; }

		// Token: 0x060006CD RID: 1741 RVA: 0x0001C178 File Offset: 0x0001A378
		public CompliancePolicySyncNotificationClient(Workload workload, ICredentials credentials, Uri syncSvcUrl)
		{
			ArgumentValidator.ThrowIfNull("credentials", credentials);
			ArgumentValidator.ThrowIfNull("syncSvcUrl", syncSvcUrl);
			this.Workload = workload;
			this.credentials = credentials;
			this.syncSvcUrl = syncSvcUrl;
		}

		// Token: 0x060006CE RID: 1742 RVA: 0x0001C1AB File Offset: 0x0001A3AB
		public string NotifyPolicyConfigChanges(IEnumerable<SyncChangeInfo> syncChangeInfos)
		{
			return this.NotifyPolicyConfigChanges(syncChangeInfos, false, false);
		}

		// Token: 0x060006CF RID: 1743 RVA: 0x0001C1B8 File Offset: 0x0001A3B8
		public string NotifyPolicyConfigChanges(IEnumerable<SyncChangeInfo> syncChangeInfos, bool fullSync, bool syncNow)
		{
			if (!fullSync && (syncChangeInfos == null || !syncChangeInfos.Any<SyncChangeInfo>()))
			{
				throw new ArgumentException("syncChangeInfo must be provided if it is not full sync. In full sync scenario, please provide configuration type in syncChangeInfo; otherwise, this notification will trigger full sync for all supported types.");
			}
			string result;
			try
			{
				result = this.InternalNotifyPolicyConfigChanges(syncChangeInfos, fullSync, syncNow);
			}
			catch (Exception ex)
			{
				if (this.IsKnownException(ex))
				{
					throw new CompliancePolicySyncNotificationClientException(Strings.ErrorCompliancePolicySyncNotificationClient(this.Workload.ToString(), ex.Message), ex);
				}
				throw;
			}
			return result;
		}

		// Token: 0x060006D0 RID: 1744 RVA: 0x0001C22C File Offset: 0x0001A42C
		protected static string GetClientInfoIdentifier(IEnumerable<SyncChangeInfo> syncChangeInfos)
		{
			Guid guid = Guid.NewGuid();
			if (syncChangeInfos != null && syncChangeInfos.Any<SyncChangeInfo>())
			{
				PolicyVersion version = syncChangeInfos.First<SyncChangeInfo>().Version;
				if (version != null)
				{
					guid = version.InternalStorage;
				}
			}
			return string.Format("{0}:{1}", guid.ToString(), CompliancePolicySyncNotificationClient.clientInfo);
		}

		// Token: 0x060006D1 RID: 1745 RVA: 0x0001C290 File Offset: 0x0001A490
		protected static Uri GetSyncSvrUrlFromCache(SyncSvcEndPointType endPointType = SyncSvcEndPointType.RestOAuth)
		{
			Uri uri = ProvisioningCache.Instance.TryAddAndGetGlobalDictionaryValue<Uri, string>(CannedProvisioningCacheKeys.GlobalUnifiedPolicyNotificationClientsInfo, "EopSyncSvcUrl", () => UnifiedPolicyConfiguration.GetInstance().GetSyncSvrBaseUrl());
			Uri result;
			switch (endPointType)
			{
			case SyncSvcEndPointType.RestOAuth:
				result = uri;
				break;
			case SyncSvcEndPointType.SoapOAuth:
				result = new Uri(uri.AbsoluteUri.TrimEnd(new char[]
				{
					'/'
				}) + "/soapoauth");
				break;
			case SyncSvcEndPointType.SoapCert:
				result = new Uri(uri.AbsoluteUri.TrimEnd(new char[]
				{
					'/'
				}) + "/soap");
				break;
			default:
				throw new NotSupportedException(endPointType + "is not supported by GetSyncSvrUrlFromCache");
			}
			return result;
		}

		// Token: 0x060006D2 RID: 1746 RVA: 0x0001C363 File Offset: 0x0001A563
		protected static Uri GetExoPswsHostUrlFromCache()
		{
			return ProvisioningCache.Instance.TryAddAndGetGlobalDictionaryValue<Uri, string>(CannedProvisioningCacheKeys.GlobalUnifiedPolicyNotificationClientsInfo, "ExoPswsHostUrl", () => UnifiedPolicyConfiguration.GetInstance().GetExoPswsHostUrl());
		}

		// Token: 0x060006D3 RID: 1747
		protected abstract string InternalNotifyPolicyConfigChanges(IEnumerable<SyncChangeInfo> syncChangeInfos, bool fullSync, bool syncNow);

		// Token: 0x060006D4 RID: 1748 RVA: 0x0001C396 File Offset: 0x0001A596
		protected virtual bool IsKnownException(Exception exception)
		{
			return exception is WebException;
		}

		// Token: 0x0400027C RID: 636
		private const string EopSyncSvcUrlCacheKey = "EopSyncSvcUrl";

		// Token: 0x0400027D RID: 637
		private const string ExoPswsHostUrlCacheKey = "ExoPswsHostUrl";

		// Token: 0x0400027E RID: 638
		private static readonly string clientInfo = string.Format("{0}_{1}", Environment.MachineName, FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion);

		// Token: 0x0400027F RID: 639
		protected readonly ICredentials credentials;

		// Token: 0x04000280 RID: 640
		protected readonly Uri syncSvcUrl;
	}
}
