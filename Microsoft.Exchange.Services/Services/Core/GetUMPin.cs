using System;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.UM.Rpc;
using Microsoft.Exchange.UM.UMCommon.CrossServerMailboxAccess;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x02000336 RID: 822
	internal sealed class GetUMPin : SingleStepServiceCommand<GetUMPinRequest, GetUMPinResponseMessage>
	{
		// Token: 0x0600170D RID: 5901 RVA: 0x0007A994 File Offset: 0x00078B94
		public GetUMPin(CallContext callContext, GetUMPinRequest request) : base(callContext, request)
		{
		}

		// Token: 0x0600170E RID: 5902 RVA: 0x0007A99E File Offset: 0x00078B9E
		internal override IExchangeWebMethodResponse GetResponse()
		{
			return new GetUMPinResponseMessage(base.Result.Code, base.Result.Error, base.Result.Value);
		}

		// Token: 0x0600170F RID: 5903 RVA: 0x0007A9C8 File Offset: 0x00078BC8
		internal override ServiceResult<GetUMPinResponseMessage> Execute()
		{
			IRecipientSession adrecipientSession = base.CallContext.ADRecipientSessionContext.GetADRecipientSession();
			ADUser user = adrecipientSession.FindADUserByObjectId(base.CallContext.AccessingADUser.ObjectId);
			PINInfo umpin;
			using (XSOUMUserMailboxAccessor xsoumuserMailboxAccessor = new XSOUMUserMailboxAccessor(user, base.MailboxIdentityMailboxSession))
			{
				umpin = xsoumuserMailboxAccessor.GetUMPin();
			}
			GetUMPinResponseMessage value = new GetUMPinResponseMessage
			{
				PinInfo = umpin
			};
			return new ServiceResult<GetUMPinResponseMessage>(value);
		}
	}
}
