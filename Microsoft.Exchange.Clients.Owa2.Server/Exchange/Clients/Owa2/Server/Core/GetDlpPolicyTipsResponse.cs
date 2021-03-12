using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020003CC RID: 972
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public sealed class GetDlpPolicyTipsResponse
	{
		// Token: 0x06001F1B RID: 7963 RVA: 0x0007701C File Offset: 0x0007521C
		internal GetDlpPolicyTipsResponse(EvaluationResult result)
		{
			this.EvaluationResult = result;
			this.Matches = new DlpPolicyMatchDetail[0];
		}

		// Token: 0x06001F1C RID: 7964 RVA: 0x00077037 File Offset: 0x00075237
		internal GetDlpPolicyTipsResponse(EvaluationResult evalResult, OptimizationResult optimizationResult) : this(evalResult)
		{
			this.OptimizationResult = optimizationResult;
		}

		// Token: 0x06001F1D RID: 7965 RVA: 0x00077048 File Offset: 0x00075248
		public static GetDlpPolicyTipsResponse GetResponseToPingRequest()
		{
			if (GetDlpPolicyTipsResponse.responseToPingRequest == null)
			{
				GetDlpPolicyTipsResponse getDlpPolicyTipsResponse = new GetDlpPolicyTipsResponse(EvaluationResult.Success);
				getDlpPolicyTipsResponse.Matches = new DlpPolicyMatchDetail[2];
				getDlpPolicyTipsResponse.Matches[0] = new DlpPolicyMatchDetail();
				getDlpPolicyTipsResponse.Matches[0].Action = DlpPolicyTipAction.NotifyOnly;
				getDlpPolicyTipsResponse.Matches[0].AttachmentIds = new AttachmentIdType[]
				{
					new AttachmentIdType(Guid.NewGuid().ToString()),
					new AttachmentIdType(Guid.NewGuid().ToString())
				};
				getDlpPolicyTipsResponse.Matches[1] = new DlpPolicyMatchDetail();
				getDlpPolicyTipsResponse.Matches[1].Action = DlpPolicyTipAction.RejectUnlessSilentOverride;
				getDlpPolicyTipsResponse.Matches[1].AttachmentIds = new AttachmentIdType[]
				{
					new AttachmentIdType(Guid.NewGuid().ToString()),
					new AttachmentIdType(Guid.NewGuid().ToString())
				};
				GetDlpPolicyTipsResponse.responseToPingRequest = getDlpPolicyTipsResponse;
			}
			return GetDlpPolicyTipsResponse.responseToPingRequest;
		}

		// Token: 0x1700074A RID: 1866
		// (get) Token: 0x06001F1E RID: 7966 RVA: 0x00077155 File Offset: 0x00075355
		// (set) Token: 0x06001F1F RID: 7967 RVA: 0x0007715D File Offset: 0x0007535D
		[DataMember]
		public EvaluationResult EvaluationResult { get; set; }

		// Token: 0x1700074B RID: 1867
		// (get) Token: 0x06001F20 RID: 7968 RVA: 0x00077166 File Offset: 0x00075366
		// (set) Token: 0x06001F21 RID: 7969 RVA: 0x0007716E File Offset: 0x0007536E
		[DataMember]
		public OptimizationResult OptimizationResult { get; set; }

		// Token: 0x1700074C RID: 1868
		// (get) Token: 0x06001F22 RID: 7970 RVA: 0x00077177 File Offset: 0x00075377
		// (set) Token: 0x06001F23 RID: 7971 RVA: 0x0007717F File Offset: 0x0007537F
		[DataMember]
		public DlpPolicyMatchDetail[] Matches { get; set; }

		// Token: 0x1700074D RID: 1869
		// (get) Token: 0x06001F24 RID: 7972 RVA: 0x00077188 File Offset: 0x00075388
		// (set) Token: 0x06001F25 RID: 7973 RVA: 0x00077190 File Offset: 0x00075390
		[DataMember]
		public string DiagnosticData { get; set; }

		// Token: 0x1700074E RID: 1870
		// (get) Token: 0x06001F26 RID: 7974 RVA: 0x00077199 File Offset: 0x00075399
		// (set) Token: 0x06001F27 RID: 7975 RVA: 0x000771A1 File Offset: 0x000753A1
		[DataMember]
		public PolicyTipCustomizedStrings CustomizedStrings { get; set; }

		// Token: 0x1700074F RID: 1871
		// (get) Token: 0x06001F28 RID: 7976 RVA: 0x000771AA File Offset: 0x000753AA
		// (set) Token: 0x06001F29 RID: 7977 RVA: 0x000771B2 File Offset: 0x000753B2
		[DataMember]
		public string DetectedClassificationIds { get; set; }

		// Token: 0x17000750 RID: 1872
		// (get) Token: 0x06001F2A RID: 7978 RVA: 0x000771BB File Offset: 0x000753BB
		// (set) Token: 0x06001F2B RID: 7979 RVA: 0x000771C3 File Offset: 0x000753C3
		[DataMember]
		public string ScanResultData { get; set; }

		// Token: 0x040011AA RID: 4522
		private static volatile GetDlpPolicyTipsResponse responseToPingRequest = null;

		// Token: 0x040011AB RID: 4523
		public static readonly GetDlpPolicyTipsResponse NoRulesResponse = new GetDlpPolicyTipsResponse(EvaluationResult.Success, OptimizationResult.NoRules);

		// Token: 0x040011AC RID: 4524
		public static readonly GetDlpPolicyTipsResponse NullOrganizationResponse = new GetDlpPolicyTipsResponse(EvaluationResult.NullOrganization);

		// Token: 0x040011AD RID: 4525
		public static readonly GetDlpPolicyTipsResponse InvalidStoreItemIdResponse = new GetDlpPolicyTipsResponse(EvaluationResult.ClientErrorInvalidStoreItemId);

		// Token: 0x040011AE RID: 4526
		public static readonly GetDlpPolicyTipsResponse AccessDeniedStoreItemIdResponse = new GetDlpPolicyTipsResponse(EvaluationResult.ClientErrorAccessDeniedStoreItemId);

		// Token: 0x040011AF RID: 4527
		public static readonly GetDlpPolicyTipsResponse InvalidClientScanResultResponse = new GetDlpPolicyTipsResponse(EvaluationResult.ClientErrorInvalidClientScanResult);

		// Token: 0x040011B0 RID: 4528
		public static readonly GetDlpPolicyTipsResponse NoContentResponse = new GetDlpPolicyTipsResponse(EvaluationResult.ClientErrorNoContent);

		// Token: 0x040011B1 RID: 4529
		public static readonly GetDlpPolicyTipsResponse ItemAlreadyBeingProcessedResponse = new GetDlpPolicyTipsResponse(EvaluationResult.ClientErrorItemAlreadyBeingProcessed);

		// Token: 0x040011B2 RID: 4530
		public static readonly GetDlpPolicyTipsResponse TooManyPendingRequestResponse = new GetDlpPolicyTipsResponse(EvaluationResult.TooManyPendingRequests);
	}
}
