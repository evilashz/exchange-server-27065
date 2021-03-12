using System;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x0200028E RID: 654
	internal class MobileSpeechRecoTracer
	{
		// Token: 0x0600135C RID: 4956 RVA: 0x000566E8 File Offset: 0x000548E8
		public static void TraceDebug(object context, Guid recoRequestId, string message, params object[] args)
		{
			using (new CallId(recoRequestId.ToString()))
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.MobileSpeechRecoTracer, context, message, args);
			}
		}

		// Token: 0x0600135D RID: 4957 RVA: 0x00056734 File Offset: 0x00054934
		public static void TraceDebug(object context, Guid recoRequestId, PIIMessage pii, string message, params object[] args)
		{
			using (new CallId(recoRequestId.ToString()))
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.MobileSpeechRecoTracer, context, pii, message, args);
			}
		}

		// Token: 0x0600135E RID: 4958 RVA: 0x00056780 File Offset: 0x00054980
		public static void TraceError(object context, Guid recoRequestId, string message, params object[] args)
		{
			using (new CallId(recoRequestId.ToString()))
			{
				CallIdTracer.TraceError(ExTraceGlobals.MobileSpeechRecoTracer, context, message, args);
			}
		}

		// Token: 0x0600135F RID: 4959 RVA: 0x000567CC File Offset: 0x000549CC
		public static void TraceWarning(object context, Guid recoRequestId, string message, params object[] args)
		{
			using (new CallId(recoRequestId.ToString()))
			{
				CallIdTracer.TraceWarning(ExTraceGlobals.MobileSpeechRecoTracer, context, message, args);
			}
		}

		// Token: 0x06001360 RID: 4960 RVA: 0x00056818 File Offset: 0x00054A18
		public static void TracePerformance(object context, Guid recoRequestId, string message, params object[] args)
		{
			using (new CallId(recoRequestId.ToString()))
			{
				CallIdTracer.TracePerformance(ExTraceGlobals.MobileSpeechRecoTracer, context, message, args);
			}
		}
	}
}
