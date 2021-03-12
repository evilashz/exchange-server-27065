using System;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.ClientAccess.Messages;
using Microsoft.Exchange.UM.TroubleshootingTool.Shared;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020001B1 RID: 433
	internal class PlayOnPhoneMessageRequestHandler : PlayOnPhoneHandler
	{
		// Token: 0x17000341 RID: 833
		// (get) Token: 0x06000CB3 RID: 3251 RVA: 0x000372B4 File Offset: 0x000354B4
		protected override CallType OutgoingCallType
		{
			get
			{
				return 5;
			}
		}

		// Token: 0x06000CB4 RID: 3252 RVA: 0x000372B7 File Offset: 0x000354B7
		protected override ResponseBase Execute(RequestBase requestBase)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, this, "Processing a PlayOnPhone request for voice messages.", new object[0]);
			return base.Execute(requestBase);
		}
	}
}
