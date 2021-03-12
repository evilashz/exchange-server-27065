using System;
using Microsoft.Exchange.UM.TroubleshootingTool.Shared;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMCore.Exceptions;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000242 RID: 578
	internal class AccessProxyCallHandler : ICallHandler
	{
		// Token: 0x060010FD RID: 4349 RVA: 0x0004BE54 File Offset: 0x0004A054
		public void HandleCall(CafeRoutingContext context)
		{
			ValidateArgument.NotNull(context, "RoutingContext");
			if (!context.IsAccessProxyCall)
			{
				return;
			}
			context.Tracer.Trace("AccessProxyCallHandler : TryHandleCall: RemoteFqdn = {0}", new object[]
			{
				context.RemoteMatchedFqdn
			});
			SipRoutingHelper.Context routingContext = context.RoutingHelper.GetRoutingContext(context.ToUri.SimplifiedUri, context.FromUri.SimplifiedUri, (context.CallInfo.DiversionInfo.Count > 0) ? context.CallInfo.DiversionInfo[0].UserAtHost : null, context.RequestUriOfCall);
			if (routingContext.AutoAttendant != null)
			{
				context.Tracer.Trace("AccessProxyCallHandler : TryHandleCall: AutoAttendant = {0}", new object[]
				{
					routingContext.AutoAttendant.Name
				});
				context.AutoAttendant = routingContext.AutoAttendant;
				context.DialPlan = context.ScopedADConfigurationSession.GetDialPlanFromId(context.AutoAttendant.UMDialPlan);
				return;
			}
			if (routingContext.DialPlanId != null)
			{
				context.DialPlan = context.ScopedADConfigurationSession.GetDialPlanFromId(routingContext.DialPlanId);
				return;
			}
			throw CallRejectedException.Create(Strings.InvalidRequest, CallEndingReason.InvalidRequest, null, new object[0]);
		}
	}
}
