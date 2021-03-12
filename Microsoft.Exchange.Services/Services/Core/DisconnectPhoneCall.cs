using System;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.UM.ClientAccess;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x020002D4 RID: 724
	internal sealed class DisconnectPhoneCall : SingleStepServiceCommand<DisconnectPhoneCallRequest, DisconnectPhoneCallResponseMessage>
	{
		// Token: 0x06001413 RID: 5139 RVA: 0x0006475E File Offset: 0x0006295E
		public DisconnectPhoneCall(CallContext callContext, DisconnectPhoneCallRequest request) : base(callContext, request)
		{
			this.callId = request.CallId;
		}

		// Token: 0x06001414 RID: 5140 RVA: 0x00064774 File Offset: 0x00062974
		internal override IExchangeWebMethodResponse GetResponse()
		{
			return new DisconnectPhoneCallResponseMessage(base.Result.Code, base.Result.Error);
		}

		// Token: 0x06001415 RID: 5141 RVA: 0x00064794 File Offset: 0x00062994
		internal override ServiceResult<DisconnectPhoneCallResponseMessage> Execute()
		{
			using (UMClientCommon umclientCommon = new UMClientCommon(base.CallContext.AccessingPrincipal))
			{
				umclientCommon.Disconnect(this.callId.Id);
			}
			return new ServiceResult<DisconnectPhoneCallResponseMessage>(new DisconnectPhoneCallResponseMessage());
		}

		// Token: 0x04000D83 RID: 3459
		private PhoneCallId callId;
	}
}
