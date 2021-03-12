using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Diagnostics;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.ServiceCommands
{
	// Token: 0x02000302 RID: 770
	internal class DeclineBuddy : InstantMessageCommandBase<bool>
	{
		// Token: 0x060019D5 RID: 6613 RVA: 0x0005CC58 File Offset: 0x0005AE58
		static DeclineBuddy()
		{
			OwsLogRegistry.Register(OwaApplication.GetRequestDetailsLogger.Get(ExtensibleLoggerMetadata.EventId), typeof(InstantMessagingBuddyMetadata), new Type[0]);
		}

		// Token: 0x060019D6 RID: 6614 RVA: 0x0005CC7F File Offset: 0x0005AE7F
		public DeclineBuddy(CallContext callContext, InstantMessageBuddy instantMessageBuddy) : base(callContext)
		{
			WcfServiceCommandBase.ThrowIfNull(instantMessageBuddy, "instantMessageBuddy", "DeclineBuddy");
			this.instantMessageBuddy = instantMessageBuddy;
		}

		// Token: 0x060019D7 RID: 6615 RVA: 0x0005CCA0 File Offset: 0x0005AEA0
		protected override bool InternalExecute()
		{
			InstantMessageProvider provider = base.Provider;
			if (provider != null)
			{
				provider.DeclineBuddy(this.instantMessageBuddy);
				return true;
			}
			return false;
		}

		// Token: 0x04000E48 RID: 3656
		private InstantMessageBuddy instantMessageBuddy;
	}
}
