using System;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.ClientAccess.Messages;
using Microsoft.Exchange.UM.TroubleshootingTool.Shared;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020001AE RID: 430
	internal class PlayOnPhoneGreetingRequestHandler : PlayOnPhoneHandler
	{
		// Token: 0x1700033F RID: 831
		// (get) Token: 0x06000CA1 RID: 3233 RVA: 0x00036B50 File Offset: 0x00034D50
		protected override CallType OutgoingCallType
		{
			get
			{
				return 5;
			}
		}

		// Token: 0x06000CA2 RID: 3234 RVA: 0x00036B53 File Offset: 0x00034D53
		protected override ResponseBase Execute(RequestBase requestBase)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, this, "Processing a PlayOnPhone request for greetings.", new object[0]);
			return base.Execute(requestBase);
		}
	}
}
