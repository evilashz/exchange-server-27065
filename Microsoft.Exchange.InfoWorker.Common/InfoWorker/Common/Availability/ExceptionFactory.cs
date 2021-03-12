using System;
using System.ComponentModel;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.InfoWorker.Availability;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x0200007E RID: 126
	internal static class ExceptionFactory
	{
		// Token: 0x0600033B RID: 827 RVA: 0x0000E6A8 File Offset: 0x0000C8A8
		internal static LocalizedException LocalizedExceptionFromWin32Error(int error, string message)
		{
			LocalizedException ex = new Win32InteropException(new Win32Exception(error));
			ExceptionFactory.SecurityTracer.TraceDebug<int>((long)ex.GetHashCode(), message, error);
			return ex;
		}

		// Token: 0x0400021A RID: 538
		private static readonly Trace SecurityTracer = ExTraceGlobals.SecurityTracer;
	}
}
