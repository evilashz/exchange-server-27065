using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x02000359 RID: 857
	internal class RemoveDistributionGroupFromImListCommand : SingleStepServiceCommand<RemoveDistributionGroupFromImListRequest, ServiceResultNone>
	{
		// Token: 0x06001815 RID: 6165 RVA: 0x00081898 File Offset: 0x0007FA98
		public RemoveDistributionGroupFromImListCommand(CallContext callContext, RemoveDistributionGroupFromImListRequest request) : base(callContext, request)
		{
		}

		// Token: 0x06001816 RID: 6166 RVA: 0x000818A2 File Offset: 0x0007FAA2
		internal override IExchangeWebMethodResponse GetResponse()
		{
			return new RemoveDistributionGroupFromImListResponseMessage(base.Result.Code, base.Result.Error);
		}

		// Token: 0x06001817 RID: 6167 RVA: 0x000818C0 File Offset: 0x0007FAC0
		internal override ServiceResult<ServiceResultNone> Execute()
		{
			IdAndSession idAndSession = base.IdConverter.ConvertItemIdToIdAndSessionReadOnly(base.Request.GroupId);
			StoreId id = idAndSession.Id;
			MailboxSession session = (idAndSession.Session as MailboxSession) ?? base.CallContext.SessionCache.GetMailboxIdentityMailboxSession();
			new RemoveDistributionGroupFromImList(session, id, new XSOFactory()).Execute();
			return new ServiceResult<ServiceResultNone>(new ServiceResultNone());
		}
	}
}
