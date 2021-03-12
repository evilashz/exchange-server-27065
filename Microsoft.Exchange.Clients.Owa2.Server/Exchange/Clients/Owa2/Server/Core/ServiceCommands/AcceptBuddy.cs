using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Diagnostics;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.ServiceCommands
{
	// Token: 0x020002D6 RID: 726
	internal class AcceptBuddy : InstantMessageCommandBase<bool>
	{
		// Token: 0x0600188E RID: 6286 RVA: 0x000545AE File Offset: 0x000527AE
		static AcceptBuddy()
		{
			OwsLogRegistry.Register(OwaApplication.GetRequestDetailsLogger.Get(ExtensibleLoggerMetadata.EventId), typeof(InstantMessagingBuddyMetadata), new Type[0]);
		}

		// Token: 0x0600188F RID: 6287 RVA: 0x000545D5 File Offset: 0x000527D5
		public AcceptBuddy(CallContext callContext, InstantMessageBuddy instantMessageBuddy, InstantMessageGroup instantMessageGroup) : base(callContext)
		{
			WcfServiceCommandBase.ThrowIfNull(instantMessageBuddy, "instantMessageBuddy", "AcceptBuddy");
			WcfServiceCommandBase.ThrowIfNull(instantMessageBuddy, "instantMessageGroup", "AcceptBuddy");
			this.instantMessageBuddy = instantMessageBuddy;
			this.instantMessageGroup = instantMessageGroup;
		}

		// Token: 0x06001890 RID: 6288 RVA: 0x0005460C File Offset: 0x0005280C
		protected override bool InternalExecute()
		{
			InstantMessageProvider provider = base.Provider;
			if (provider != null)
			{
				provider.AcceptBuddy(base.MailboxIdentityMailboxSession, this.instantMessageBuddy, this.instantMessageGroup);
				return true;
			}
			return false;
		}

		// Token: 0x04000D2B RID: 3371
		private InstantMessageBuddy instantMessageBuddy;

		// Token: 0x04000D2C RID: 3372
		private InstantMessageGroup instantMessageGroup;
	}
}
