using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x02000298 RID: 664
	internal class AddImContactToGroupCommand : SingleStepServiceCommand<AddImContactToGroupRequest, ServiceResultNone>
	{
		// Token: 0x060011AF RID: 4527 RVA: 0x00055BD7 File Offset: 0x00053DD7
		public AddImContactToGroupCommand(CallContext callContext, AddImContactToGroupRequest request) : base(callContext, request)
		{
		}

		// Token: 0x060011B0 RID: 4528 RVA: 0x00055BE1 File Offset: 0x00053DE1
		internal override IExchangeWebMethodResponse GetResponse()
		{
			return new AddImContactToGroupResponseMessage(base.Result.Code, base.Result.Error);
		}

		// Token: 0x060011B1 RID: 4529 RVA: 0x00055C00 File Offset: 0x00053E00
		internal override ServiceResult<ServiceResultNone> Execute()
		{
			IdAndSession idAndSession = base.IdConverter.ConvertItemIdToIdAndSessionReadOnly(base.Request.ContactId);
			StoreId id = idAndSession.Id;
			StoreId groupId = null;
			if (base.Request.GroupId != null)
			{
				IdAndSession idAndSession2 = base.IdConverter.ConvertItemIdToIdAndSessionReadOnly(base.Request.GroupId);
				groupId = idAndSession2.Id;
			}
			MailboxSession mailboxSession = (idAndSession.Session as MailboxSession) ?? base.CallContext.SessionCache.GetMailboxIdentityMailboxSession();
			new AddImContactToGroup(mailboxSession, id, groupId, new XSOFactory(), Global.UnifiedContactStoreConfiguration).Execute();
			return new ServiceResult<ServiceResultNone>(new ServiceResultNone());
		}
	}
}
