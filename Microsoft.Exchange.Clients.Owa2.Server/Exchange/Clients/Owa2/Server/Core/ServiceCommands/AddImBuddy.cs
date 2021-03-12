using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Diagnostics;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.ServiceCommands
{
	// Token: 0x02000245 RID: 581
	internal class AddImBuddy : InstantMessageCommandBase<bool>
	{
		// Token: 0x060015CA RID: 5578 RVA: 0x0004E300 File Offset: 0x0004C500
		static AddImBuddy()
		{
			OwsLogRegistry.Register(OwaApplication.GetRequestDetailsLogger.Get(ExtensibleLoggerMetadata.EventId), typeof(InstantMessagingBuddyMetadata), new Type[0]);
		}

		// Token: 0x060015CB RID: 5579 RVA: 0x0004E327 File Offset: 0x0004C527
		public AddImBuddy(CallContext callContext, InstantMessageBuddy instantMessageBuddy, InstantMessageGroup instantMessageGroup) : base(callContext)
		{
			WcfServiceCommandBase.ThrowIfNull(instantMessageBuddy, "instantMessageBuddy", "AddImBuddy");
			WcfServiceCommandBase.ThrowIfNull(instantMessageGroup, "instantMessageGroup", "AddImBuddy");
			this.instantMessageBuddy = instantMessageBuddy;
			this.instantMessageGroup = instantMessageGroup;
		}

		// Token: 0x060015CC RID: 5580 RVA: 0x0004E360 File Offset: 0x0004C560
		protected override bool InternalExecute()
		{
			InstantMessageProvider provider = base.Provider;
			if (provider != null)
			{
				provider.AddBuddy(base.MailboxIdentityMailboxSession, this.instantMessageBuddy, this.instantMessageGroup);
				return true;
			}
			return false;
		}

		// Token: 0x04000C16 RID: 3094
		private InstantMessageBuddy instantMessageBuddy;

		// Token: 0x04000C17 RID: 3095
		private InstantMessageGroup instantMessageGroup;
	}
}
