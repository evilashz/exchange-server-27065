using System;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020002D5 RID: 725
	internal class AcceptChatSession : InstantMessageConversationCommand
	{
		// Token: 0x0600188C RID: 6284 RVA: 0x00054575 File Offset: 0x00052775
		public AcceptChatSession(CallContext callContext, int sessionId) : base(callContext)
		{
			this.sessionId = sessionId;
		}

		// Token: 0x0600188D RID: 6285 RVA: 0x00054588 File Offset: 0x00052788
		protected override InstantMessageOperationError ExecuteInstantMessagingCommand()
		{
			InstantMessageProvider provider = base.Provider;
			if (provider != null)
			{
				return (InstantMessageOperationError)provider.ParticipateInConversation(this.sessionId);
			}
			return InstantMessageOperationError.NotSignedIn;
		}

		// Token: 0x04000D2A RID: 3370
		private readonly int sessionId;
	}
}
