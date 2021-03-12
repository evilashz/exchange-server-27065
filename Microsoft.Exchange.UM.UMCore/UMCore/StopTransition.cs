using System;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020001E9 RID: 489
	internal class StopTransition : TransitionBase
	{
		// Token: 0x06000E64 RID: 3684 RVA: 0x00040E72 File Offset: 0x0003F072
		internal StopTransition() : base(null, string.Empty, null, false, false, null)
		{
		}

		// Token: 0x06000E65 RID: 3685 RVA: 0x00040E84 File Offset: 0x0003F084
		protected override void DoTransition(ActivityManager manager, BaseUMCallSession vo)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "Executing null StopTransition.DoTransition.", new object[0]);
		}
	}
}
