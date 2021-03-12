using System;
using System.Net;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x02000099 RID: 153
	internal static class BailOut
	{
		// Token: 0x060003A3 RID: 931 RVA: 0x000128CB File Offset: 0x00010ACB
		internal static void SetHTTPStatusAndClose(HttpStatusCode statusCode)
		{
			ExTraceGlobals.ExceptionTracer.TraceDebug<HttpStatusCode>(0L, "[BailOut::SetHTTPStatusAndClose] Creating renderer for HTTP status code {0}.", statusCode);
			EWSSettings.ResponseRenderer = HttpResponseRenderer.Create(statusCode);
			throw new BailOutException();
		}
	}
}
