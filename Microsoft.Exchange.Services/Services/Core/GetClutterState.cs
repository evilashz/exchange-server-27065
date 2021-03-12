using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x020002FF RID: 767
	internal sealed class GetClutterState : SingleStepServiceCommand<GetClutterStateRequest, GetClutterStateResponse>
	{
		// Token: 0x060015A7 RID: 5543 RVA: 0x000707C0 File Offset: 0x0006E9C0
		public GetClutterState(CallContext callContext, GetClutterStateRequest request) : base(callContext, request)
		{
		}

		// Token: 0x060015A8 RID: 5544 RVA: 0x000707CA File Offset: 0x0006E9CA
		internal override IExchangeWebMethodResponse GetResponse()
		{
			return base.Result.Value;
		}

		// Token: 0x060015A9 RID: 5545 RVA: 0x000707D8 File Offset: 0x0006E9D8
		internal override ServiceResult<GetClutterStateResponse> Execute()
		{
			MailboxSession mailboxSession = base.GetMailboxSession(base.CallContext.AccessingPrincipal.MailboxInfo.PrimarySmtpAddress.ToString());
			GetClutterStateResponse value = new GetClutterStateResponse(ServiceResultCode.Success, null)
			{
				ClutterState = Util.GetMailboxClutterState(mailboxSession)
			};
			return new ServiceResult<GetClutterStateResponse>(value);
		}
	}
}
