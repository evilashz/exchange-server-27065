using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Diagnostics;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000363 RID: 867
	internal class UnsubscribeFromPresenceUpdates : InstantMessageCommandBase<int>
	{
		// Token: 0x06001BE6 RID: 7142 RVA: 0x0006C543 File Offset: 0x0006A743
		static UnsubscribeFromPresenceUpdates()
		{
			OwsLogRegistry.Register(OwaApplication.GetRequestDetailsLogger.Get(ExtensibleLoggerMetadata.EventId), typeof(InstantMessagingSubscriptionMetadata), new Type[0]);
		}

		// Token: 0x06001BE7 RID: 7143 RVA: 0x0006C56A File Offset: 0x0006A76A
		public UnsubscribeFromPresenceUpdates(CallContext callContext, string sipUri) : base(callContext)
		{
			WcfServiceCommandBase.ThrowIfNull(sipUri, "sipUri", "UnsubscribeFromPresenceUpdates");
			this.sipUri = sipUri;
		}

		// Token: 0x06001BE8 RID: 7144 RVA: 0x0006C58C File Offset: 0x0006A78C
		protected override int InternalExecute()
		{
			InstantMessageProvider provider = base.Provider;
			if (provider != null)
			{
				provider.RemoveSubscription(this.sipUri);
				return 0;
			}
			return -11;
		}

		// Token: 0x04000FDC RID: 4060
		private readonly string sipUri;
	}
}
