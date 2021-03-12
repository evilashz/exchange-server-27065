using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.HygieneData;

namespace Microsoft.Exchange.Hygiene.Data.Domain
{
	// Token: 0x02000131 RID: 305
	internal static class TraceHelper
	{
		// Token: 0x06000BDE RID: 3038 RVA: 0x00025B94 File Offset: 0x00023D94
		public static void TraceInformation(this DomainSession session, string formatString, params object[] args)
		{
			string formatString2 = session.ContextPrefix + formatString;
			ExTraceGlobals.DomainSessionTracer.Information((long)session.GetHashCode(), formatString2, args);
		}

		// Token: 0x06000BDF RID: 3039 RVA: 0x00025BC4 File Offset: 0x00023DC4
		public static void TraceError(this DomainSession session, string formatString, params object[] args)
		{
			string formatString2 = session.ContextPrefix + formatString;
			ExTraceGlobals.DomainSessionTracer.TraceError((long)session.GetHashCode(), formatString2, args);
		}

		// Token: 0x06000BE0 RID: 3040 RVA: 0x00025BF4 File Offset: 0x00023DF4
		public static void TraceWarning(this DomainSession session, string formatString, params object[] args)
		{
			string formatString2 = session.ContextPrefix + formatString;
			ExTraceGlobals.DomainSessionTracer.TraceWarning((long)session.GetHashCode(), formatString2, args);
		}

		// Token: 0x06000BE1 RID: 3041 RVA: 0x00025C24 File Offset: 0x00023E24
		public static void TraceDebug(this DomainSession session, string formatString, params object[] args)
		{
			string formatString2 = session.ContextPrefix + formatString;
			ExTraceGlobals.DomainSessionTracer.TraceDebug((long)session.GetHashCode(), formatString2, args);
		}

		// Token: 0x06000BE2 RID: 3042 RVA: 0x00025C54 File Offset: 0x00023E54
		public static void TraceDebug(this DomainSession session, string message)
		{
			string message2 = session.ContextPrefix + message;
			ExTraceGlobals.DomainSessionTracer.TraceDebug((long)session.GetHashCode(), message2);
		}

		// Token: 0x06000BE3 RID: 3043 RVA: 0x00025C80 File Offset: 0x00023E80
		public static bool IsDebugTraceEnabled()
		{
			return ExTraceGlobals.DomainSessionTracer.IsTraceEnabled(TraceType.DebugTrace);
		}
	}
}
