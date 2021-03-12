using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Diagnostics;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.ServiceCommands
{
	// Token: 0x02000244 RID: 580
	internal class AddFavoriteCommand : InstantMessageCommandBase<bool>
	{
		// Token: 0x060015C7 RID: 5575 RVA: 0x0004E29C File Offset: 0x0004C49C
		static AddFavoriteCommand()
		{
			OwsLogRegistry.Register(OwaApplication.GetRequestDetailsLogger.Get(ExtensibleLoggerMetadata.EventId), typeof(InstantMessagingBuddyMetadata), new Type[0]);
		}

		// Token: 0x060015C8 RID: 5576 RVA: 0x0004E2C3 File Offset: 0x0004C4C3
		public AddFavoriteCommand(CallContext callContext, InstantMessageBuddy instantMessageBuddy) : base(callContext)
		{
			WcfServiceCommandBase.ThrowIfNull(instantMessageBuddy, "instantMessageBuddy", "AddFavoriteCommand");
			this.instantMessageBuddy = instantMessageBuddy;
		}

		// Token: 0x060015C9 RID: 5577 RVA: 0x0004E2E3 File Offset: 0x0004C4E3
		protected override bool InternalExecute()
		{
			return new AddFavorite(new XSOFactory(), base.MailboxIdentityMailboxSession, this.instantMessageBuddy).Execute();
		}

		// Token: 0x04000C15 RID: 3093
		private readonly InstantMessageBuddy instantMessageBuddy;
	}
}
