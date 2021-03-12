using System;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200034D RID: 845
	internal class SendChatMessage : InstantMessageConversationCommand
	{
		// Token: 0x06001B96 RID: 7062 RVA: 0x00069D2D File Offset: 0x00067F2D
		public SendChatMessage(CallContext callContext, ChatMessage chatMessage) : base(callContext)
		{
			if (chatMessage == null)
			{
				throw new ArgumentNullException("chatMessage");
			}
			this.chatMessage = chatMessage;
		}

		// Token: 0x06001B97 RID: 7063 RVA: 0x00069D4C File Offset: 0x00067F4C
		protected override InstantMessageOperationError ExecuteInstantMessagingCommand()
		{
			InstantMessageProvider provider = base.Provider;
			if (provider == null)
			{
				return InstantMessageOperationError.NotSignedIn;
			}
			if (this.chatMessage.ChatSessionId > 0)
			{
				return (InstantMessageOperationError)provider.SendChatMessage(this.chatMessage);
			}
			return (InstantMessageOperationError)provider.SendNewChatMessage(this.chatMessage);
		}

		// Token: 0x04000F94 RID: 3988
		private ChatMessage chatMessage;
	}
}
