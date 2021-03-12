using System;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000139 RID: 313
	internal class FaxManager : CAMessageSubmissionManager
	{
		// Token: 0x060008CE RID: 2254 RVA: 0x000265BC File Offset: 0x000247BC
		internal FaxManager(ActivityManager manager, FaxManager.ConfigClass config) : base(manager, config)
		{
		}

		// Token: 0x060008CF RID: 2255 RVA: 0x000265C8 File Offset: 0x000247C8
		internal override string HandleFailedTransfer(BaseUMCallSession callSession)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "FaxManager handleFailedTransfer", new object[0]);
			CallTransfer callTransfer = (CallTransfer)base.CurrentActivity;
			callTransfer.HandleFailedFaxTransfer(callSession);
			return null;
		}

		// Token: 0x060008D0 RID: 2256 RVA: 0x00026600 File Offset: 0x00024800
		internal void OnFaxRequestReceive(BaseUMCallSession callSession, UMCallSessionEventArgs callSessionEventArgs)
		{
			FaxRequest faxRequest = (FaxRequest)base.CurrentActivity;
			faxRequest.OnFaxRequestReceive(callSession);
		}

		// Token: 0x060008D1 RID: 2257 RVA: 0x00026620 File Offset: 0x00024820
		internal override void Start(BaseUMCallSession callSession, string refInfo)
		{
			base.Start(callSession, refInfo);
		}

		// Token: 0x060008D2 RID: 2258 RVA: 0x0002662A File Offset: 0x0002482A
		internal override void OnUserHangup(BaseUMCallSession callSession, UMCallSessionEventArgs callSessionEventArgs)
		{
			base.OnUserHangup(callSession, callSessionEventArgs);
		}

		// Token: 0x060008D3 RID: 2259 RVA: 0x00026634 File Offset: 0x00024834
		internal override void CheckAuthorization(UMSubscriber u)
		{
		}

		// Token: 0x0200013A RID: 314
		internal class ConfigClass : ActivityManagerConfig
		{
			// Token: 0x060008D4 RID: 2260 RVA: 0x00026636 File Offset: 0x00024836
			internal ConfigClass(ActivityManagerConfig manager) : base(manager)
			{
			}

			// Token: 0x060008D5 RID: 2261 RVA: 0x0002663F File Offset: 0x0002483F
			internal override ActivityManager CreateActivityManager(ActivityManager manager)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "Constructing fax activity manager.", new object[0]);
				return new FaxManager(manager, this);
			}
		}
	}
}
