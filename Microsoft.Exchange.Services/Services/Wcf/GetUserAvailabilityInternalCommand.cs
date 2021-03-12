using System;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.InfoWorker.Availability;
using Microsoft.Exchange.Services.Core;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x0200095D RID: 2397
	internal sealed class GetUserAvailabilityInternalCommand : ServiceCommand<GetUserAvailabilityInternalJsonResponse>
	{
		// Token: 0x06004508 RID: 17672 RVA: 0x000F03F1 File Offset: 0x000EE5F1
		public GetUserAvailabilityInternalCommand(CallContext callContext, GetUserAvailabilityRequest request) : base(callContext)
		{
			this.request = request;
		}

		// Token: 0x06004509 RID: 17673 RVA: 0x000F0404 File Offset: 0x000EE604
		protected override GetUserAvailabilityInternalJsonResponse InternalExecute()
		{
			ExTraceGlobals.GetUserAvailabilityInternalCallTracer.TraceDebug((long)this.GetHashCode(), "Executing call to GetUserAvailability.");
			GetUserAvailability getUserAvailability = new GetUserAvailability(base.CallContext, this.request);
			GetUserAvailabilityResponse value = getUserAvailability.Execute().Value;
			ExTraceGlobals.GetUserAvailabilityInternalCallTracer.TraceDebug<int>((long)this.GetHashCode(), "Receiving response - FreeBusyResponseArrayLength: {0}", value.FreeBusyResponseArray.Length);
			return new GetUserAvailabilityInternal(base.MailboxIdentityMailboxSession, this.request, value).Execute();
		}

		// Token: 0x04002828 RID: 10280
		private readonly GetUserAvailabilityRequest request;
	}
}
