using System;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200033D RID: 829
	internal class NotifyTyping : InstantMessageCommandBase<bool>
	{
		// Token: 0x06001B68 RID: 7016 RVA: 0x0006830C File Offset: 0x0006650C
		public NotifyTyping(CallContext callContext, int sessionId, bool typingCancelled) : base(callContext)
		{
			this.sessionId = sessionId;
			this.typingCancelled = typingCancelled;
		}

		// Token: 0x06001B69 RID: 7017 RVA: 0x00068324 File Offset: 0x00066524
		protected override bool InternalExecute()
		{
			InstantMessageProvider provider = base.Provider;
			if (provider != null)
			{
				provider.NotifyTyping(this.sessionId, this.typingCancelled);
				return true;
			}
			return false;
		}

		// Token: 0x04000F5F RID: 3935
		private readonly int sessionId;

		// Token: 0x04000F60 RID: 3936
		private readonly bool typingCancelled;
	}
}
