using System;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x0200024E RID: 590
	internal class CallAnsweringCallHandler : ICallHandler
	{
		// Token: 0x06001182 RID: 4482 RVA: 0x0004D3D4 File Offset: 0x0004B5D4
		public void HandleCall(CafeRoutingContext context)
		{
			ValidateArgument.NotNull(context, "RoutingContext");
			if (context.DivertedUser != null)
			{
				context.Tracer.Trace("CallAnsweringCallHandler : TryHandleCall : Diverted User = {0}", new object[]
				{
					context.DivertedUser.DisplayName
				});
				context.RedirectUri = RedirectionTarget.Instance.GetForCallAnsweringCall(context.DivertedUser, context).Uri;
			}
		}
	}
}
