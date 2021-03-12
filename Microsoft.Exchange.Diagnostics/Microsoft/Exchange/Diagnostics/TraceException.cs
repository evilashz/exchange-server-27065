using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics.Components.Diagnostics;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x020000B1 RID: 177
	internal class TraceException
	{
		// Token: 0x060004AE RID: 1198 RVA: 0x00012604 File Offset: 0x00010804
		internal static void Setup()
		{
			LocalizedException.TraceExceptionCallback = new LocalizedException.TraceExceptionDelegate(TraceException.TraceExceptionFn);
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x00012617 File Offset: 0x00010817
		internal static void TraceExceptionFn(string formatString, params object[] formatObjects)
		{
			ExTraceGlobals.CommonTracer.TraceDebug(17510, 0L, formatString, formatObjects);
		}
	}
}
