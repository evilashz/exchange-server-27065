using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x0200029A RID: 666
	internal class AddImGroupCommand : SingleStepServiceCommand<AddImGroupRequest, ImGroup>
	{
		// Token: 0x060011B4 RID: 4532 RVA: 0x00055D29 File Offset: 0x00053F29
		public AddImGroupCommand(CallContext callContext, AddImGroupRequest request) : base(callContext, request)
		{
		}

		// Token: 0x060011B5 RID: 4533 RVA: 0x00055D33 File Offset: 0x00053F33
		internal override IExchangeWebMethodResponse GetResponse()
		{
			return new AddImGroupResponseMessage(base.Result.Code, base.Result.Error, base.Result.Value);
		}

		// Token: 0x060011B6 RID: 4534 RVA: 0x00055D5C File Offset: 0x00053F5C
		internal override ServiceResult<ImGroup> Execute()
		{
			string displayName = base.Request.DisplayName;
			MailboxSession mailboxIdentityMailboxSession = base.CallContext.SessionCache.GetMailboxIdentityMailboxSession();
			RawImGroup rawImGroup = new AddImGroup(mailboxIdentityMailboxSession, displayName, new XSOFactory(), Global.UnifiedContactStoreConfiguration).Execute();
			return new ServiceResult<ImGroup>(ImGroup.LoadFromRawImGroup(rawImGroup, mailboxIdentityMailboxSession));
		}
	}
}
