using System;
using System.Collections.Generic;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Management.Transport;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;

namespace Microsoft.Office.CompliancePolicy.Tasks
{
	// Token: 0x020000E7 RID: 231
	[Serializable]
	public class PolicyDistributionErrorDetails
	{
		// Token: 0x06000933 RID: 2355 RVA: 0x000263C8 File Offset: 0x000245C8
		public PolicyDistributionErrorDetails(string endpoint, Guid objectId, ConfigurationObjectType objectType, Workload workload, UnifiedPolicyErrorCode errorCode, string errorMessage, DateTime lastErrorTime, string additionalDiagnostics)
		{
			this.Endpoint = endpoint;
			this.ObjectId = objectId;
			this.ObjectType = objectType;
			this.Workload = workload;
			this.ResultCode = errorCode;
			this.ResultMessage = errorMessage;
			this.LastResultTime = new DateTime?(lastErrorTime);
			this.AdditionalDiagnostics = additionalDiagnostics;
			this.Severity = PolicyDistributionResultSeverity.Error;
			if (string.IsNullOrEmpty(this.ResultMessage))
			{
				string resultMessage;
				if (!PolicyDistributionErrorDetails.errorCodeToStringMap.TryGetValue(this.ResultCode, out resultMessage))
				{
					resultMessage = Strings.UnknownErrorMsg;
				}
				this.ResultMessage = resultMessage;
			}
		}

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x06000934 RID: 2356 RVA: 0x00026457 File Offset: 0x00024657
		// (set) Token: 0x06000935 RID: 2357 RVA: 0x0002645F File Offset: 0x0002465F
		public string Endpoint { get; private set; }

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x06000936 RID: 2358 RVA: 0x00026468 File Offset: 0x00024668
		// (set) Token: 0x06000937 RID: 2359 RVA: 0x00026470 File Offset: 0x00024670
		public Guid ObjectId { get; private set; }

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x06000938 RID: 2360 RVA: 0x00026479 File Offset: 0x00024679
		// (set) Token: 0x06000939 RID: 2361 RVA: 0x00026481 File Offset: 0x00024681
		public ConfigurationObjectType ObjectType { get; private set; }

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x0600093A RID: 2362 RVA: 0x0002648A File Offset: 0x0002468A
		// (set) Token: 0x0600093B RID: 2363 RVA: 0x00026492 File Offset: 0x00024692
		public Workload Workload { get; private set; }

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x0600093C RID: 2364 RVA: 0x0002649B File Offset: 0x0002469B
		// (set) Token: 0x0600093D RID: 2365 RVA: 0x000264A3 File Offset: 0x000246A3
		public UnifiedPolicyErrorCode ResultCode { get; private set; }

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x0600093E RID: 2366 RVA: 0x000264AC File Offset: 0x000246AC
		// (set) Token: 0x0600093F RID: 2367 RVA: 0x000264B4 File Offset: 0x000246B4
		public string ResultMessage { get; internal set; }

		// Token: 0x1700033D RID: 829
		// (get) Token: 0x06000940 RID: 2368 RVA: 0x000264BD File Offset: 0x000246BD
		// (set) Token: 0x06000941 RID: 2369 RVA: 0x000264C5 File Offset: 0x000246C5
		public DateTime? LastResultTime { get; private set; }

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x06000942 RID: 2370 RVA: 0x000264CE File Offset: 0x000246CE
		// (set) Token: 0x06000943 RID: 2371 RVA: 0x000264D6 File Offset: 0x000246D6
		public string AdditionalDiagnostics { get; private set; }

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x06000944 RID: 2372 RVA: 0x000264DF File Offset: 0x000246DF
		// (set) Token: 0x06000945 RID: 2373 RVA: 0x000264E7 File Offset: 0x000246E7
		public PolicyDistributionResultSeverity Severity { get; set; }

		// Token: 0x06000946 RID: 2374 RVA: 0x000264F0 File Offset: 0x000246F0
		public override string ToString()
		{
			return string.Format("[{0}]{1}:{2}", this.Workload, this.Endpoint, this.ResultMessage);
		}

		// Token: 0x06000947 RID: 2375 RVA: 0x00026513 File Offset: 0x00024713
		public void Redact()
		{
			this.Endpoint = SuppressingPiiData.Redact(this.Endpoint);
		}

		// Token: 0x06000948 RID: 2376 RVA: 0x00026526 File Offset: 0x00024726
		internal void AppendAdditionalDiagnosticsInfo(string diagnosticsInfo)
		{
			if (!string.IsNullOrWhiteSpace(diagnosticsInfo))
			{
				this.AdditionalDiagnostics = (string.IsNullOrWhiteSpace(this.AdditionalDiagnostics) ? diagnosticsInfo : string.Format("{0}, {1}", this.AdditionalDiagnostics, diagnosticsInfo));
			}
		}

		// Token: 0x040003EA RID: 1002
		private static readonly Dictionary<UnifiedPolicyErrorCode, string> errorCodeToStringMap = new Dictionary<UnifiedPolicyErrorCode, string>
		{
			{
				UnifiedPolicyErrorCode.PolicyNotifyError,
				Strings.PolicyNotifyErrorErrorMsg
			},
			{
				UnifiedPolicyErrorCode.PolicySyncTimeout,
				Strings.PolicySyncTimeoutErrorMsg
			},
			{
				UnifiedPolicyErrorCode.FailedToOpenContainer,
				Strings.FailedToOpenContainerErrorMsg
			},
			{
				UnifiedPolicyErrorCode.SiteInReadonlyOrNotAccessible,
				Strings.SiteInReadonlyOrNotAccessibleErrorMsg
			},
			{
				UnifiedPolicyErrorCode.SiteOutOfQuota,
				Strings.SiteOutOfQuotaErrorMsg
			}
		};
	}
}
