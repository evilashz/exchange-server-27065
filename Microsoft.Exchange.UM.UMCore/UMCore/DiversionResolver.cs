using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000257 RID: 599
	internal class DiversionResolver : ICallHandler
	{
		// Token: 0x060011B8 RID: 4536 RVA: 0x0004E698 File Offset: 0x0004C898
		public void HandleCall(CafeRoutingContext context)
		{
			ValidateArgument.NotNull(context, "RoutingContext");
			context.Tracer.Trace("DiversionResolver : TryHandleCall  : AA found:{0}, IsDivertedCall:{1}", new object[]
			{
				context.AutoAttendant != null,
				context.IsDivertedCall
			});
			if (context.AutoAttendant == null && context.IsDivertedCall)
			{
				UMAutoAttendant umautoAttendant = null;
				UMRecipient umrecipient = null;
				if (DiversionUtils.TryAnalyzeDiversionForRoutingPurposes(context, out umautoAttendant, out umrecipient))
				{
					context.Tracer.Trace("DiversionResolver : TryHandleCall  : found Diversion based AA:{0} or user:{1}", new object[]
					{
						umautoAttendant != null,
						umrecipient != null
					});
					context.AutoAttendant = umautoAttendant;
					context.DivertedUser = umrecipient;
				}
			}
		}
	}
}
