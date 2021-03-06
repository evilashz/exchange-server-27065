using System;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000262 RID: 610
	internal class SubscriberAccessCallHandler : ICallHandler
	{
		// Token: 0x060011F7 RID: 4599 RVA: 0x000500EC File Offset: 0x0004E2EC
		public void HandleCall(CafeRoutingContext context)
		{
			ValidateArgument.NotNull(context, "RoutingContext");
			UMRecipient umrecipient = null;
			try
			{
				umrecipient = SubscriberAccessUtils.ResolveCaller(context.CallingParty, context.DivertedUser, context.DialPlan);
				if (umrecipient != null && umrecipient.IsUMEnabledMailbox)
				{
					context.Tracer.Trace("SubscriberAccessCallHandler : TryHandleCall, resolved caller to = {0}", new object[]
					{
						umrecipient.DisplayName
					});
					context.RedirectUri = RedirectionTarget.Instance.GetForSubscriberAccessCall(umrecipient, context).Uri;
				}
				else
				{
					context.Tracer.Trace("SubscriberAccessCallHandler : TryHandleCall, couldnt resolve caller", new object[0]);
					context.RedirectUri = RedirectionTarget.Instance.GetForNonUserSpecificCall(context.Gateway.OrganizationId, context).Uri;
				}
			}
			finally
			{
				if (umrecipient != null)
				{
					umrecipient.Dispose();
				}
			}
		}
	}
}
