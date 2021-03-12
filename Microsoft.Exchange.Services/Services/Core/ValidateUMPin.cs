using System;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.UM.Rpc;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMCommon.CrossServerMailboxAccess;
using Microsoft.Exchange.UM.UMCommon.Exceptions;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x0200039D RID: 925
	internal sealed class ValidateUMPin : SingleStepServiceCommand<ValidateUMPinRequest, ValidateUMPinResponseMessage>
	{
		// Token: 0x06001A07 RID: 6663 RVA: 0x00096240 File Offset: 0x00094440
		public ValidateUMPin(CallContext callContext, ValidateUMPinRequest request) : base(callContext, request)
		{
			this.pinInfo = request.PinInfo;
			this.userUMMailboxPolicyGuid = request.UserUMMailboxPolicyGuid;
		}

		// Token: 0x06001A08 RID: 6664 RVA: 0x00096262 File Offset: 0x00094462
		internal override IExchangeWebMethodResponse GetResponse()
		{
			return new ValidateUMPinResponseMessage(base.Result.Code, base.Result.Error, base.Result.Value);
		}

		// Token: 0x06001A09 RID: 6665 RVA: 0x0009628C File Offset: 0x0009448C
		internal override ServiceResult<ValidateUMPinResponseMessage> Execute()
		{
			IRecipientSession adrecipientSession = base.CallContext.ADRecipientSessionContext.GetADRecipientSession();
			ADUser aduser = adrecipientSession.FindADUserByObjectId(base.CallContext.AccessingADUser.ObjectId);
			if (aduser == null)
			{
				ServiceError error = new ServiceError(Strings.UMMailboxNotFound(base.CallContext.AccessingADUser.PrimarySmtpAddress.ToString()), ResponseCodeType.ErrorRecipientNotFound, 0, ExchangeVersion.Exchange2012);
				return new ServiceResult<ValidateUMPinResponseMessage>(error);
			}
			PINInfo pininfo;
			try
			{
				using (XSOUMUserMailboxAccessor xsoumuserMailboxAccessor = new XSOUMUserMailboxAccessor(aduser, base.MailboxIdentityMailboxSession))
				{
					string pin = (this.pinInfo == null) ? null : this.pinInfo.PIN;
					pininfo = xsoumuserMailboxAccessor.ValidateUMPin(pin, this.userUMMailboxPolicyGuid);
				}
			}
			catch (UMMbxPolicyNotFoundException ex)
			{
				ServiceError error2 = new ServiceError(ex.Message, ResponseCodeType.ErrorMailboxConfiguration, 0, ExchangeVersion.Exchange2012);
				return new ServiceResult<ValidateUMPinResponseMessage>(error2);
			}
			ValidateUMPinResponseMessage value = new ValidateUMPinResponseMessage
			{
				PinInfo = pininfo
			};
			return new ServiceResult<ValidateUMPinResponseMessage>(value);
		}

		// Token: 0x0400114A RID: 4426
		private readonly PINInfo pinInfo;

		// Token: 0x0400114B RID: 4427
		private readonly Guid userUMMailboxPolicyGuid;
	}
}
