using System;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.ClientAccess.Messages;
using Microsoft.Exchange.UM.TroubleshootingTool.Shared;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020002A5 RID: 677
	internal class PlayOnPhonePAAGreetingRequestHandler : PlayOnPhoneHandler
	{
		// Token: 0x17000518 RID: 1304
		// (get) Token: 0x0600148C RID: 5260 RVA: 0x00059244 File Offset: 0x00057444
		protected override CallType OutgoingCallType
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x0600148D RID: 5261 RVA: 0x00059248 File Offset: 0x00057448
		protected override ResponseBase Execute(RequestBase requestBase)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "PlayOnPhonePAAGreetingRequestHandler::Execute()", new object[0]);
			return base.Execute(requestBase);
		}
	}
}
