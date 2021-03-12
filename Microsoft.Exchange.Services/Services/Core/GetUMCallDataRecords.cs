using System;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMCommon.CrossServerMailboxAccess;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x02000334 RID: 820
	internal sealed class GetUMCallDataRecords : SingleStepServiceCommand<GetUMCallDataRecordsRequest, GetUMCallDataRecordsResponseMessage>
	{
		// Token: 0x06001706 RID: 5894 RVA: 0x0007A72C File Offset: 0x0007892C
		public GetUMCallDataRecords(CallContext callContext, GetUMCallDataRecordsRequest request) : base(callContext, request)
		{
			this.startTime = request.StartDateTime;
			this.endTime = request.EndDateTime;
			this.offset = request.Offset;
			this.numberOfRecords = request.NumberOfRecords;
			this.userLegacyExchangeDN = request.UserLegacyExchangeDN;
			this.filterBy = request.FilterBy;
			this.ValidateArguments();
		}

		// Token: 0x06001707 RID: 5895 RVA: 0x0007A78F File Offset: 0x0007898F
		internal override IExchangeWebMethodResponse GetResponse()
		{
			return new GetUMCallDataRecordsResponseMessage(base.Result.Code, base.Result.Error, base.Result.Value);
		}

		// Token: 0x06001708 RID: 5896 RVA: 0x0007A7B8 File Offset: 0x000789B8
		internal override ServiceResult<GetUMCallDataRecordsResponseMessage> Execute()
		{
			CDRData[] callDataRecords;
			using (XSOUMCallDataRecordAccessor xsoumcallDataRecordAccessor = new XSOUMCallDataRecordAccessor(base.MailboxIdentityMailboxSession))
			{
				if (this.filterBy == UMCDRFilterByType.FilterByUser)
				{
					callDataRecords = xsoumcallDataRecordAccessor.GetUMCallDataRecordsForUser(this.userLegacyExchangeDN);
				}
				else
				{
					ExDateTime startDateTime = new ExDateTime(ExTimeZone.UtcTimeZone, this.startTime.Year, this.startTime.Month, this.startTime.Day);
					ExDateTime endDateTime = new ExDateTime(ExTimeZone.UtcTimeZone, this.endTime.Year, this.endTime.Month, this.endTime.Day);
					callDataRecords = xsoumcallDataRecordAccessor.GetUMCallDataRecords(startDateTime, endDateTime, this.offset, this.numberOfRecords);
				}
			}
			return new ServiceResult<GetUMCallDataRecordsResponseMessage>(new GetUMCallDataRecordsResponseMessage
			{
				CallDataRecords = callDataRecords
			});
		}

		// Token: 0x06001709 RID: 5897 RVA: 0x0007A8A4 File Offset: 0x00078AA4
		private void ValidateArguments()
		{
			UMCDRFilterByType umcdrfilterByType = this.filterBy;
		}

		// Token: 0x04000FA0 RID: 4000
		private readonly DateTime startTime;

		// Token: 0x04000FA1 RID: 4001
		private readonly DateTime endTime;

		// Token: 0x04000FA2 RID: 4002
		private readonly int offset;

		// Token: 0x04000FA3 RID: 4003
		private readonly int numberOfRecords;

		// Token: 0x04000FA4 RID: 4004
		private readonly string userLegacyExchangeDN;

		// Token: 0x04000FA5 RID: 4005
		private UMCDRFilterByType filterBy;
	}
}
