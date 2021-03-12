using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x0200035D RID: 861
	internal class RemoveImGroupCommand : SingleStepServiceCommand<RemoveImGroupRequest, ServiceResultNone>
	{
		// Token: 0x0600181F RID: 6175 RVA: 0x00081ABC File Offset: 0x0007FCBC
		public RemoveImGroupCommand(CallContext callContext, RemoveImGroupRequest request) : base(callContext, request)
		{
		}

		// Token: 0x06001820 RID: 6176 RVA: 0x00081AC6 File Offset: 0x0007FCC6
		internal override IExchangeWebMethodResponse GetResponse()
		{
			return new RemoveImGroupResponseMessage(base.Result.Code, base.Result.Error);
		}

		// Token: 0x06001821 RID: 6177 RVA: 0x00081AE4 File Offset: 0x0007FCE4
		internal override ServiceResult<ServiceResultNone> Execute()
		{
			IdAndSession idAndSession = base.IdConverter.ConvertItemIdToIdAndSessionReadOnly(base.Request.GroupId);
			StoreId id = idAndSession.Id;
			MailboxSession session = (idAndSession.Session as MailboxSession) ?? base.CallContext.SessionCache.GetMailboxIdentityMailboxSession();
			new RemoveImGroup(session, id, new XSOFactory()).Execute();
			return new ServiceResult<ServiceResultNone>(new ServiceResultNone());
		}
	}
}
