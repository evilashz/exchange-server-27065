using System;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000362 RID: 866
	internal class TerminateChatSession : InstantMessageCommandBase<bool>
	{
		// Token: 0x06001BE4 RID: 7140 RVA: 0x0006C50C File Offset: 0x0006A70C
		public TerminateChatSession(CallContext callContext, int sessionId) : base(callContext)
		{
			this.sessionId = sessionId;
		}

		// Token: 0x06001BE5 RID: 7141 RVA: 0x0006C51C File Offset: 0x0006A71C
		protected override bool InternalExecute()
		{
			InstantMessageProvider provider = base.Provider;
			if (provider != null)
			{
				provider.EndChatSession(this.sessionId, true);
				return true;
			}
			return false;
		}

		// Token: 0x04000FDB RID: 4059
		private readonly int sessionId;
	}
}
