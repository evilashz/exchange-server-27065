using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMCommon.CrossServerMailboxAccess;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x02000335 RID: 821
	internal sealed class GetUMCallSummary : SingleStepServiceCommand<GetUMCallSummaryRequest, GetUMCallSummaryResponseMessage>
	{
		// Token: 0x0600170A RID: 5898 RVA: 0x0007A8B0 File Offset: 0x00078AB0
		public GetUMCallSummary(CallContext callContext, GetUMCallSummaryRequest request) : base(callContext, request)
		{
			this.dialPlanGuid = request.DailPlanGuid;
			this.gatewayGuid = request.GatewayGuid;
			this.groupRecordsBy = (GroupBy)Enum.Parse(typeof(GroupBy), request.GroupRecordsBy.ToString());
		}

		// Token: 0x0600170B RID: 5899 RVA: 0x0007A907 File Offset: 0x00078B07
		internal override IExchangeWebMethodResponse GetResponse()
		{
			return new GetUMCallSummaryResponseMessage(base.Result.Code, base.Result.Error, base.Result.Value);
		}

		// Token: 0x0600170C RID: 5900 RVA: 0x0007A930 File Offset: 0x00078B30
		internal override ServiceResult<GetUMCallSummaryResponseMessage> Execute()
		{
			UMReportRawCounters[] umcallSummary;
			using (XSOUMCallDataRecordAccessor xsoumcallDataRecordAccessor = new XSOUMCallDataRecordAccessor(base.MailboxIdentityMailboxSession))
			{
				umcallSummary = xsoumcallDataRecordAccessor.GetUMCallSummary(this.dialPlanGuid, this.gatewayGuid, this.groupRecordsBy);
			}
			return new ServiceResult<GetUMCallSummaryResponseMessage>(new GetUMCallSummaryResponseMessage
			{
				UMReportRawCountersCollection = umcallSummary
			});
		}

		// Token: 0x04000FA6 RID: 4006
		private readonly Guid dialPlanGuid;

		// Token: 0x04000FA7 RID: 4007
		private readonly Guid gatewayGuid;

		// Token: 0x04000FA8 RID: 4008
		private readonly GroupBy groupRecordsBy;
	}
}
