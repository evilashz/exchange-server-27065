using System;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000243 RID: 579
	internal class AutoAttendantCallHandler : ICallHandler
	{
		// Token: 0x060010FF RID: 4351 RVA: 0x0004BF84 File Offset: 0x0004A184
		public void HandleCall(CafeRoutingContext context)
		{
			ValidateArgument.NotNull(context, "RoutingContext");
			if (context.AutoAttendant != null)
			{
				context.Tracer.Trace("AutoAttendantCallHandler : TryHandleCall AA = {0}", new object[]
				{
					context.AutoAttendant.Name
				});
				context.RedirectUri = RedirectionTarget.Instance.GetForNonUserSpecificCall(context.AutoAttendant.OrganizationId, context).Uri;
			}
		}
	}
}
