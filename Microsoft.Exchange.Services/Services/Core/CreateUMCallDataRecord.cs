using System;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMCommon.CrossServerMailboxAccess;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x020002C5 RID: 709
	internal sealed class CreateUMCallDataRecord : SingleStepServiceCommand<CreateUMCallDataRecordRequest, CreateUMCallDataRecordResponseMessage>
	{
		// Token: 0x060013A4 RID: 5028 RVA: 0x00062512 File Offset: 0x00060712
		public CreateUMCallDataRecord(CallContext callContext, CreateUMCallDataRecordRequest request) : base(callContext, request)
		{
			this.cdrData = request.CDRData;
		}

		// Token: 0x060013A5 RID: 5029 RVA: 0x00062528 File Offset: 0x00060728
		internal override IExchangeWebMethodResponse GetResponse()
		{
			return new CreateUMCallDataRecordResponseMessage(base.Result.Code, base.Result.Error);
		}

		// Token: 0x060013A6 RID: 5030 RVA: 0x00062548 File Offset: 0x00060748
		internal override ServiceResult<CreateUMCallDataRecordResponseMessage> Execute()
		{
			using (XSOUMCallDataRecordAccessor xsoumcallDataRecordAccessor = new XSOUMCallDataRecordAccessor(base.MailboxIdentityMailboxSession))
			{
				xsoumcallDataRecordAccessor.CreateUMCallDataRecord(this.cdrData);
			}
			return new ServiceResult<CreateUMCallDataRecordResponseMessage>(new CreateUMCallDataRecordResponseMessage());
		}

		// Token: 0x04000D60 RID: 3424
		private readonly CDRData cdrData;
	}
}
