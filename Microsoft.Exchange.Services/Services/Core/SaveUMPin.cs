using System;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.UM.Rpc;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMCommon.CrossServerMailboxAccess;
using Microsoft.Exchange.UM.UMCommon.Exceptions;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x02000363 RID: 867
	internal sealed class SaveUMPin : SingleStepServiceCommand<SaveUMPinRequest, SaveUMPinResponseMessage>
	{
		// Token: 0x0600183A RID: 6202 RVA: 0x00082C52 File Offset: 0x00080E52
		public SaveUMPin(CallContext callContext, SaveUMPinRequest request) : base(callContext, request)
		{
			this.pinInfo = request.PinInfo;
			this.userUMMailboxPolicyGuid = request.UserUMMailboxPolicyGuid;
		}

		// Token: 0x0600183B RID: 6203 RVA: 0x00082C74 File Offset: 0x00080E74
		internal override IExchangeWebMethodResponse GetResponse()
		{
			return new SaveUMPinResponseMessage(base.Result.Code, base.Result.Error, base.Result.Value);
		}

		// Token: 0x0600183C RID: 6204 RVA: 0x00082C9C File Offset: 0x00080E9C
		internal override ServiceResult<SaveUMPinResponseMessage> Execute()
		{
			IRecipientSession adrecipientSession = base.CallContext.ADRecipientSessionContext.GetADRecipientSession();
			ADUser aduser = adrecipientSession.FindADUserByObjectId(base.CallContext.AccessingADUser.ObjectId);
			if (aduser == null)
			{
				ServiceError error = new ServiceError(Strings.UMMailboxNotFound(base.CallContext.AccessingADUser.PrimarySmtpAddress.ToString()), ResponseCodeType.ErrorRecipientNotFound, 0, ExchangeVersion.Exchange2012);
				return new ServiceResult<SaveUMPinResponseMessage>(error);
			}
			try
			{
				using (XSOUMUserMailboxAccessor xsoumuserMailboxAccessor = new XSOUMUserMailboxAccessor(aduser, base.MailboxIdentityMailboxSession))
				{
					xsoumuserMailboxAccessor.SaveUMPin(this.pinInfo, this.userUMMailboxPolicyGuid);
				}
			}
			catch (UMMbxPolicyNotFoundException ex)
			{
				ServiceError error2 = new ServiceError(ex.Message, ResponseCodeType.ErrorMailboxConfiguration, 0, ExchangeVersion.Exchange2012);
				return new ServiceResult<SaveUMPinResponseMessage>(error2);
			}
			return new ServiceResult<SaveUMPinResponseMessage>(new SaveUMPinResponseMessage());
		}

		// Token: 0x0400103B RID: 4155
		private readonly PINInfo pinInfo;

		// Token: 0x0400103C RID: 4156
		private readonly Guid userUMMailboxPolicyGuid;
	}
}
