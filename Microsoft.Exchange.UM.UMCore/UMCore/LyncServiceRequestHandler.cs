using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.UM.TroubleshootingTool.Shared;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMCore.Exceptions;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x0200025D RID: 605
	internal class LyncServiceRequestHandler : ICallHandler
	{
		// Token: 0x060011D0 RID: 4560 RVA: 0x0004F7F8 File Offset: 0x0004D9F8
		public void HandleCall(CafeRoutingContext cafeContext)
		{
			ValidateArgument.NotNull(cafeContext, "cafeContext");
			string simplifiedUri = cafeContext.FromUri.SimplifiedUri;
			PlatformSipUri requestUriOfCall = cafeContext.RequestUriOfCall;
			SipRoutingHelper.Context routingContext = cafeContext.RoutingHelper.GetRoutingContext(simplifiedUri, requestUriOfCall);
			ExAssert.RetailAssert(routingContext != null, "SipRoutingHelper.Context was not set.");
			if (routingContext.Recipient == null)
			{
				throw CallRejectedException.Create(Strings.InvalidRequest, CallEndingReason.CouldNotFindUserBySipUri, "User: {0}.", new object[]
				{
					simplifiedUri
				});
			}
			using (UMRecipient umrecipient = UMRecipient.Factory.FromADRecipient<UMRecipient>(routingContext.Recipient))
			{
				if (umrecipient == null)
				{
					throw CallRejectedException.Create(Strings.InvalidRequest, CallEndingReason.CouldNotFindUserBySipUri, "User: {0}.", new object[]
					{
						simplifiedUri
					});
				}
				cafeContext.RedirectUri = RedirectionTarget.Instance.GetForCallAnsweringCall(umrecipient, cafeContext).Uri;
			}
		}
	}
}
