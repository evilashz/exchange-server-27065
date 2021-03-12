using System;
using System.Web;
using Microsoft.Exchange.AirSync;
using Microsoft.Exchange.Diagnostics.Components.AirSync;

namespace Microsoft.Exchange.AirSyncHandler
{
	// Token: 0x02000004 RID: 4
	public class HandlerFactory : IHttpHandlerFactory
	{
		// Token: 0x06000025 RID: 37 RVA: 0x000053AC File Offset: 0x000035AC
		public IHttpHandler GetHandler(HttpContext context, string requestType, string url, string pathTranslated)
		{
			Handler handler = new Handler();
			AirSyncDiagnostics.TraceDebug<int>(ExTraceGlobals.RequestsTracer, this, "IHttpHandlerFactory.GetHandler called. Handler {0} created.", handler.GetHashCode());
			return handler;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000053D8 File Offset: 0x000035D8
		public void ReleaseHandler(IHttpHandler handler)
		{
			Handler handler2 = handler as Handler;
			if (handler2 == null)
			{
				throw new ArgumentNullException("handler");
			}
			AirSyncDiagnostics.TraceDebug<int>(ExTraceGlobals.RequestsTracer, this, "IHttpHandlerFactory.ReleaseHandler called on {0}.", handler2.GetHashCode());
		}
	}
}
