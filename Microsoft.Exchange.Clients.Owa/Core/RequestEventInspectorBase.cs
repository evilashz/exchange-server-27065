using System;
using System.Net;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x020001F5 RID: 501
	internal abstract class RequestEventInspectorBase
	{
		// Token: 0x06001078 RID: 4216
		internal abstract void Init();

		// Token: 0x06001079 RID: 4217
		internal abstract void OnBeginRequest(object sender, EventArgs e, out bool stopExecution);

		// Token: 0x0600107A RID: 4218
		internal abstract void OnPostAuthorizeRequest(object sender, EventArgs e);

		// Token: 0x0600107B RID: 4219 RVA: 0x00065578 File Offset: 0x00063778
		internal void OnPreRequestHandlerExecute(OwaContext owaContext)
		{
			if (owaContext.HttpContext.Handler is IRegistryOnlyForm && !owaContext.LoadedByFormsRegistry)
			{
				ExTraceGlobals.CoreTracer.TraceDebug<string>(0L, "Form can not be accessed directly. url = {0}", owaContext.HttpContext.Request.Path);
				Utilities.EndResponse(owaContext.HttpContext, HttpStatusCode.BadRequest);
			}
		}

		// Token: 0x0600107C RID: 4220
		internal abstract void OnEndRequest(OwaContext owaContext);
	}
}
