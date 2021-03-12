using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Diagnostics;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000324 RID: 804
	internal class GetPresence : InstantMessageCommandBase<int>
	{
		// Token: 0x06001ABA RID: 6842 RVA: 0x00064EE2 File Offset: 0x000630E2
		static GetPresence()
		{
			OwsLogRegistry.Register(OwaApplication.GetRequestDetailsLogger.Get(ExtensibleLoggerMetadata.EventId), typeof(InstantMessagingQueryPresenceData), new Type[0]);
		}

		// Token: 0x06001ABB RID: 6843 RVA: 0x00064F09 File Offset: 0x00063109
		public GetPresence(CallContext callContext, string[] sipUris) : base(callContext)
		{
			WcfServiceCommandBase.ThrowIfNull(sipUris, "sipUris", "GetPresence");
			this.sipUris = sipUris;
		}

		// Token: 0x06001ABC RID: 6844 RVA: 0x00064F2C File Offset: 0x0006312C
		protected override int InternalExecute()
		{
			InstantMessageProvider provider = base.Provider;
			if (provider != null)
			{
				provider.QueryPresence(this.sipUris);
				return 0;
			}
			return -11;
		}

		// Token: 0x04000ED5 RID: 3797
		private readonly string[] sipUris;
	}
}
