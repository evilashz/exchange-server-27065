using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x02000356 RID: 854
	internal class RemoveContactFromImListCommand : SingleStepServiceCommand<RemoveContactFromImListRequest, ServiceResultNone>
	{
		// Token: 0x0600180A RID: 6154 RVA: 0x00081660 File Offset: 0x0007F860
		public RemoveContactFromImListCommand(CallContext callContext, RemoveContactFromImListRequest request) : base(callContext, request)
		{
		}

		// Token: 0x0600180B RID: 6155 RVA: 0x0008166A File Offset: 0x0007F86A
		internal override IExchangeWebMethodResponse GetResponse()
		{
			return new RemoveContactFromImListResponseMessage(base.Result.Code, base.Result.Error);
		}

		// Token: 0x0600180C RID: 6156 RVA: 0x00081688 File Offset: 0x0007F888
		internal override ServiceResult<ServiceResultNone> Execute()
		{
			IdAndSession idAndSession = base.IdConverter.ConvertItemIdToIdAndSessionReadOnly(base.Request.ContactId);
			StoreId id = idAndSession.Id;
			MailboxSession session = (idAndSession.Session as MailboxSession) ?? base.CallContext.SessionCache.GetMailboxIdentityMailboxSession();
			new RemoveContactFromImList(session, id, new XSOFactory()).Execute();
			return new ServiceResult<ServiceResultNone>(new ServiceResultNone());
		}
	}
}
