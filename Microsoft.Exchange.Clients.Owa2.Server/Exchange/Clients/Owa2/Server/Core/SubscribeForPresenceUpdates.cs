using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Diagnostics;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200035B RID: 859
	internal class SubscribeForPresenceUpdates : InstantMessageCommandBase<int>
	{
		// Token: 0x06001BD0 RID: 7120 RVA: 0x0006B760 File Offset: 0x00069960
		static SubscribeForPresenceUpdates()
		{
			OwsLogRegistry.Register(OwaApplication.GetRequestDetailsLogger.Get(ExtensibleLoggerMetadata.EventId), typeof(InstantMessagingSubscriptionMetadata), new Type[0]);
		}

		// Token: 0x06001BD1 RID: 7121 RVA: 0x0006B787 File Offset: 0x00069987
		public SubscribeForPresenceUpdates(CallContext callContext, string[] sipUris) : base(callContext)
		{
			WcfServiceCommandBase.ThrowIfNull(sipUris, "sipUris", "SubscribeForPresenceUpdates");
			this.sipUris = sipUris;
		}

		// Token: 0x06001BD2 RID: 7122 RVA: 0x0006B7A8 File Offset: 0x000699A8
		protected override int InternalExecute()
		{
			InstantMessageProvider provider = base.Provider;
			if (provider != null)
			{
				provider.AddSubscription(this.sipUris);
				return 0;
			}
			return -11;
		}

		// Token: 0x04000FC9 RID: 4041
		private readonly string[] sipUris;
	}
}
