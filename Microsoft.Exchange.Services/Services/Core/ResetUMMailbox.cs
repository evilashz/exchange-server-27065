using System;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.UM.UMCommon.CrossServerMailboxAccess;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x0200035F RID: 863
	internal sealed class ResetUMMailbox : SingleStepServiceCommand<ResetUMMailboxRequest, ResetUMMailboxResponseMessage>
	{
		// Token: 0x0600182B RID: 6187 RVA: 0x00081ED0 File Offset: 0x000800D0
		public ResetUMMailbox(CallContext callContext, ResetUMMailboxRequest request) : base(callContext, request)
		{
			this.keepProperties = request.KeepProperties;
		}

		// Token: 0x0600182C RID: 6188 RVA: 0x00081EE6 File Offset: 0x000800E6
		internal override IExchangeWebMethodResponse GetResponse()
		{
			return new ResetUMMailboxResponseMessage(base.Result.Code, base.Result.Error, base.Result.Value);
		}

		// Token: 0x0600182D RID: 6189 RVA: 0x00081F10 File Offset: 0x00080110
		internal override ServiceResult<ResetUMMailboxResponseMessage> Execute()
		{
			IRecipientSession adrecipientSession = base.CallContext.ADRecipientSessionContext.GetADRecipientSession();
			ADUser user = adrecipientSession.FindADUserByObjectId(base.CallContext.AccessingADUser.ObjectId);
			using (XSOUMUserMailboxAccessor xsoumuserMailboxAccessor = new XSOUMUserMailboxAccessor(user, base.MailboxIdentityMailboxSession))
			{
				xsoumuserMailboxAccessor.ResetUMMailbox(this.keepProperties);
			}
			return new ServiceResult<ResetUMMailboxResponseMessage>(new ResetUMMailboxResponseMessage());
		}

		// Token: 0x0400102C RID: 4140
		private readonly bool keepProperties;
	}
}
