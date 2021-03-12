using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000907 RID: 2311
	internal class AddBuddyCommand : ServiceCommand<bool>
	{
		// Token: 0x0600430F RID: 17167 RVA: 0x000DFD58 File Offset: 0x000DDF58
		public AddBuddyCommand(CallContext callContext, Buddy buddy) : base(callContext)
		{
			WcfServiceCommandBase.ThrowIfNull(buddy, "buddy", "AddBuddy::AddBuddy");
			this.session = callContext.SessionCache.GetMailboxIdentityMailboxSession();
			this.buddy = buddy;
		}

		// Token: 0x06004310 RID: 17168 RVA: 0x000DFD89 File Offset: 0x000DDF89
		protected override bool InternalExecute()
		{
			new AddBuddy(this.session, this.buddy).Execute();
			return true;
		}

		// Token: 0x04002714 RID: 10004
		private readonly MailboxSession session;

		// Token: 0x04002715 RID: 10005
		private readonly Buddy buddy;
	}
}
