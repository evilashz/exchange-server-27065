using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x0200035B RID: 859
	internal class RemoveImContactFromGroupCommand : SingleStepServiceCommand<RemoveImContactFromGroupRequest, ServiceResultNone>
	{
		// Token: 0x0600181A RID: 6170 RVA: 0x000819A9 File Offset: 0x0007FBA9
		public RemoveImContactFromGroupCommand(CallContext callContext, RemoveImContactFromGroupRequest request) : base(callContext, request)
		{
		}

		// Token: 0x0600181B RID: 6171 RVA: 0x000819B3 File Offset: 0x0007FBB3
		internal override IExchangeWebMethodResponse GetResponse()
		{
			return new RemoveImContactFromGroupResponseMessage(base.Result.Code, base.Result.Error);
		}

		// Token: 0x0600181C RID: 6172 RVA: 0x000819D0 File Offset: 0x0007FBD0
		internal override ServiceResult<ServiceResultNone> Execute()
		{
			IdAndSession idAndSession = base.IdConverter.ConvertItemIdToIdAndSessionReadOnly(base.Request.ContactId);
			IdAndSession idAndSession2 = base.IdConverter.ConvertItemIdToIdAndSessionReadOnly(base.Request.GroupId);
			StoreId id = idAndSession.Id;
			StoreId id2 = idAndSession2.Id;
			MailboxSession mailboxSession = (idAndSession.Session as MailboxSession) ?? base.CallContext.SessionCache.GetMailboxIdentityMailboxSession();
			new RemoveImContactFromGroup(mailboxSession, id, id2, new XSOFactory()).Execute();
			return new ServiceResult<ServiceResultNone>(new ServiceResultNone());
		}
	}
}
