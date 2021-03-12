using System;
using Microsoft.Exchange.UM.TroubleshootingTool.Shared;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMCore.Exceptions;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000260 RID: 608
	internal static class RouterCallHandler
	{
		// Token: 0x060011E8 RID: 4584 RVA: 0x0004FB70 File Offset: 0x0004DD70
		public static void Handle(CafeRoutingContext context)
		{
			RouterCallHandler.InternalHandle(RouterCallHandler.callHandlers, context);
		}

		// Token: 0x060011E9 RID: 4585 RVA: 0x0004FB7D File Offset: 0x0004DD7D
		public static void HandleServiceRequest(CafeRoutingContext context)
		{
			RouterCallHandler.InternalHandle(RouterCallHandler.serviceHandlers, context);
		}

		// Token: 0x060011EA RID: 4586 RVA: 0x0004FB8C File Offset: 0x0004DD8C
		private static void InternalHandle(ICallHandler[] handlers, CafeRoutingContext context)
		{
			ValidateArgument.NotNull(handlers, "handlers");
			ValidateArgument.NotNull(context, "context");
			for (int i = 0; i < handlers.Length; i++)
			{
				handlers[i].HandleCall(context);
				if (context.RedirectUri != null)
				{
					return;
				}
			}
			throw CallRejectedException.Create(Strings.CallCouldNotBeHandled(context.CallInfo.CallId, context.CallInfo.RemotePeer.ToString()), CallEndingReason.TransientError, null, new object[0]);
		}

		// Token: 0x04000BFB RID: 3067
		private static ICallHandler[] callHandlers = new ICallHandler[]
		{
			new ForestResolver(),
			new GatewayResolver(),
			new AccessProxyCallHandler(),
			new DialplanResolver(),
			new ReferredByCallHandler(),
			new DiversionResolver(),
			new AutoAttendantCallHandler(),
			new CallAnsweringCallHandler(),
			new SubscriberAccessCallHandler()
		};

		// Token: 0x04000BFC RID: 3068
		private static ICallHandler[] serviceHandlers = new ICallHandler[]
		{
			new ForestResolver(),
			new LyncServiceRequestHandler()
		};
	}
}
