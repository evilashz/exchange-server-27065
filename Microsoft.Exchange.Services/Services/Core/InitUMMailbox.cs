using System;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.UM.UMCommon.CrossServerMailboxAccess;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x02000341 RID: 833
	internal sealed class InitUMMailbox : SingleStepServiceCommand<InitUMMailboxRequest, InitUMMailboxResponseMessage>
	{
		// Token: 0x06001777 RID: 6007 RVA: 0x0007D629 File Offset: 0x0007B829
		public InitUMMailbox(CallContext callContext, InitUMMailboxRequest request) : base(callContext, request)
		{
		}

		// Token: 0x06001778 RID: 6008 RVA: 0x0007D633 File Offset: 0x0007B833
		internal override IExchangeWebMethodResponse GetResponse()
		{
			return new InitUMMailboxResponseMessage(base.Result.Code, base.Result.Error, base.Result.Value);
		}

		// Token: 0x06001779 RID: 6009 RVA: 0x0007D65C File Offset: 0x0007B85C
		internal override ServiceResult<InitUMMailboxResponseMessage> Execute()
		{
			IRecipientSession adrecipientSession = base.CallContext.ADRecipientSessionContext.GetADRecipientSession();
			ADUser user = adrecipientSession.FindADUserByObjectId(base.CallContext.AccessingADUser.ObjectId);
			using (XSOUMUserMailboxAccessor xsoumuserMailboxAccessor = new XSOUMUserMailboxAccessor(user, base.MailboxIdentityMailboxSession))
			{
				xsoumuserMailboxAccessor.InitUMMailbox();
			}
			return new ServiceResult<InitUMMailboxResponseMessage>(new InitUMMailboxResponseMessage());
		}
	}
}
