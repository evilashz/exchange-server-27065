﻿using System;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.TroubleshootingTool.Shared;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMCore.Exceptions;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000254 RID: 596
	internal class DataCenterLegacySupportRedirectionTarget : DataCenterRedirectionTarget
	{
		// Token: 0x060011AA RID: 4522 RVA: 0x0004DD34 File Offset: 0x0004BF34
		protected override RedirectionTarget.ResultSet GetLocalForestLegacyRedirectionTarget(UMRecipient recipient, IRoutingContext context)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, null, "GetLocalForestLegacyRedirectionTarget: legacy user", new object[0]);
			IRedirectTargetChooser redirectTargetChooser = new DataCenterServerChooser(context.DialPlan, context.IsSecuredCall, recipient);
			string text;
			int port;
			if (!redirectTargetChooser.GetTargetServer(out text, out port))
			{
				redirectTargetChooser.HandleServerNotFound();
				throw CallRejectedException.Create(Strings.NoValidLegacyServer(context.CallId, redirectTargetChooser.SubscriberLogId), CallEndingReason.UserRoutingIssue, null, new object[0]);
			}
			RedirectionTarget.ResultSet resultSet = new RedirectionTarget.ResultSet(RouterUtils.GetRedirectContactUri(context.RequestUriOfCall, context.RoutingHelper, text, port, TransportParameter.Tls, string.Empty), text, port);
			CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, null, "GetLocalForestLegacyRedirectionTarget: legacy user - redirecting to {0}", new object[]
			{
				resultSet.Uri
			});
			return resultSet;
		}

		// Token: 0x060011AB RID: 4523 RVA: 0x0004DDE8 File Offset: 0x0004BFE8
		protected override RedirectionTarget.ResultSet GetRemoteForestLegacyRedirectionTarget(IRoutingContext context)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, null, "GetRemoteForestLegacyRedirectionTarget: target in remote forest - use template {0}", new object[]
			{
				context.UMPodRedirectTemplate
			});
			string text;
			if (MserveHelper.TryMapTenantGuidToForest(context.TenantGuid, context.UMPodRedirectTemplate, out text))
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, null, "GetRemoteForestLegacyRedirectionTarget: MServe found {0}", new object[]
				{
					text
				});
				return new RedirectionTarget.ResultSet(RouterUtils.GetRedirectContactUri(context.RequestUriOfCall, context.RoutingHelper, text, 5061, TransportParameter.Tls, string.Empty), text, 5061);
			}
			CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, null, "GetRemoteForestLegacyRedirectionTarget: Could not map Tenant '{0}' to a forest", new object[]
			{
				context.TenantGuid
			});
			UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_CallRouterUnableToMapGatewayToForest, null, new object[]
			{
				context.CallId,
				context.TenantGuid.ToString("D")
			});
			throw CallRejectedException.Create(Strings.PartnerGatewayNotFoundError, CallEndingReason.GatewaylookupIssue, null, new object[0]);
		}

		// Token: 0x060011AC RID: 4524 RVA: 0x0004DEE9 File Offset: 0x0004C0E9
		protected override RedirectionTarget.ResultSet HandleNoOrgMailboxForRouting(IRoutingContext context)
		{
			return this.GetRemoteForestLegacyRedirectionTarget(context);
		}
	}
}
