using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x02000377 RID: 887
	internal class SetImGroupCommand : SingleStepServiceCommand<SetImGroupRequest, ServiceResultNone>
	{
		// Token: 0x060018D6 RID: 6358 RVA: 0x000891C1 File Offset: 0x000873C1
		public SetImGroupCommand(CallContext callContext, SetImGroupRequest request) : base(callContext, request)
		{
		}

		// Token: 0x060018D7 RID: 6359 RVA: 0x000891CB File Offset: 0x000873CB
		internal override IExchangeWebMethodResponse GetResponse()
		{
			return new SetImGroupResponseMessage(base.Result.Code, base.Result.Error);
		}

		// Token: 0x060018D8 RID: 6360 RVA: 0x000891E8 File Offset: 0x000873E8
		internal override ServiceResult<ServiceResultNone> Execute()
		{
			IdAndSession idAndSession = base.IdConverter.ConvertItemIdToIdAndSessionReadOnly(base.Request.GroupId);
			StoreId id = idAndSession.Id;
			string newDisplayName = base.Request.NewDisplayName;
			MailboxSession session = (idAndSession.Session as MailboxSession) ?? base.CallContext.SessionCache.GetMailboxIdentityMailboxSession();
			new SetImGroup(session, id, newDisplayName, new XSOFactory()).Execute();
			return new ServiceResult<ServiceResultNone>(new ServiceResultNone());
		}
	}
}
