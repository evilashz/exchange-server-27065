using System;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x0200037D RID: 893
	internal sealed class SubscribeForPull : SubscribeCommandBase
	{
		// Token: 0x060018F5 RID: 6389 RVA: 0x0008A394 File Offset: 0x00088594
		public SubscribeForPull(CallContext callContext, SubscribeRequest request) : base(callContext, request)
		{
			this.pullSubscriptionRequest = (base.Request.SubscriptionRequest as PullSubscriptionRequest);
		}

		// Token: 0x060018F6 RID: 6390 RVA: 0x0008A3B4 File Offset: 0x000885B4
		protected override SubscriptionBase CreateSubscriptionInstance(IdAndSession[] folderIds)
		{
			return new PullSubscription(this.pullSubscriptionRequest, folderIds, base.CallContext.EffectiveCaller.ObjectGuid);
		}

		// Token: 0x040010B0 RID: 4272
		private PullSubscriptionRequest pullSubscriptionRequest;
	}
}
