using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.ClientAccess.Messages;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000122 RID: 290
	internal abstract class RequestHandler : DisposableBase
	{
		// Token: 0x06000826 RID: 2086 RVA: 0x000226D4 File Offset: 0x000208D4
		internal static ResponseBase ProcessRequest(RequestBase request)
		{
			string @namespace = typeof(RequestHandler).Namespace;
			Type type = Type.GetType(@namespace + "." + request.GetType().Name + "Handler", false);
			ResponseBase result;
			using (RequestHandler requestHandler = (RequestHandler)Activator.CreateInstance(type))
			{
				result = requestHandler.Execute(request);
			}
			return result;
		}

		// Token: 0x06000827 RID: 2087
		protected abstract ResponseBase Execute(RequestBase request);

		// Token: 0x06000828 RID: 2088 RVA: 0x00022744 File Offset: 0x00020944
		protected override void InternalDispose(bool disposing)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.PromptProvisioningTracer, null, "RequestHandler.InternalDispose called", new object[0]);
		}

		// Token: 0x06000829 RID: 2089 RVA: 0x0002275C File Offset: 0x0002095C
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<RequestHandler>(this);
		}

		// Token: 0x0400088A RID: 2186
		private const string HandlerSuffix = "Handler";
	}
}
