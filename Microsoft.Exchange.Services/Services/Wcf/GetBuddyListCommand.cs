using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000919 RID: 2329
	internal class GetBuddyListCommand : ServiceCommand<GetBuddyListResponse>
	{
		// Token: 0x06004377 RID: 17271 RVA: 0x000E4104 File Offset: 0x000E2304
		public GetBuddyListCommand(CallContext callContext) : base(callContext)
		{
			this.session = callContext.SessionCache.GetMailboxIdentityMailboxSession();
		}

		// Token: 0x06004378 RID: 17272 RVA: 0x000E411E File Offset: 0x000E231E
		protected override GetBuddyListResponse InternalExecute()
		{
			return new GetBuddyList(this.session).Execute();
		}

		// Token: 0x04002769 RID: 10089
		private readonly MailboxSession session;
	}
}
