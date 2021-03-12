using System;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x0200025F RID: 607
	internal class ReferredByCallHandler : ICallHandler
	{
		// Token: 0x060011E5 RID: 4581 RVA: 0x0004FA6D File Offset: 0x0004DC6D
		public void HandleCall(CafeRoutingContext context)
		{
			ValidateArgument.NotNull(context, "RoutingContext");
			if (!string.IsNullOrEmpty(context.ReferredByHeader))
			{
				this.HandleTransferredCall(context);
			}
		}

		// Token: 0x060011E6 RID: 4582 RVA: 0x0004FA90 File Offset: 0x0004DC90
		private void HandleTransferredCall(CafeRoutingContext context)
		{
			context.Tracer.Trace("ReferredByCallHandler : HandleTransferredCall, referredbyheader = {0}", new object[]
			{
				context.ReferredByHeader
			});
			UMRecipient umrecipient = null;
			UserTransferWithContext.DeserializedReferredByHeader deserializedReferredByHeader = null;
			try
			{
				if (UserTransferWithContext.TryParseReferredByHeader(context.ReferredByHeader, context.DialPlan, out umrecipient, out deserializedReferredByHeader))
				{
					context.Tracer.Trace("ReferredByCallHandler : HandleTransferredCall, TypeOfTransferredCall = {0}", new object[]
					{
						deserializedReferredByHeader.TypeOfTransferredCall
					});
					switch (deserializedReferredByHeader.TypeOfTransferredCall)
					{
					case 3:
						context.RedirectUri = RedirectionTarget.Instance.GetForSubscriberAccessCall(umrecipient, context).Uri;
						break;
					case 4:
						context.RedirectUri = RedirectionTarget.Instance.GetForCallAnsweringCall(umrecipient, context).Uri;
						break;
					}
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
