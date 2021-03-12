using System;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x0200037F RID: 895
	internal sealed class SubscribeForStreaming : SubscribeCommandBase
	{
		// Token: 0x060018FB RID: 6395 RVA: 0x0008A4A3 File Offset: 0x000886A3
		public SubscribeForStreaming(CallContext callContext, SubscribeRequest request) : base(callContext, request)
		{
			this.streamingSubscriptionRequest = (base.Request.SubscriptionRequest as StreamingSubscriptionRequest);
		}

		// Token: 0x060018FC RID: 6396 RVA: 0x0008A4C3 File Offset: 0x000886C3
		protected override SubscriptionBase CreateSubscriptionInstance(IdAndSession[] folderIds)
		{
			return new StreamingSubscription(this.streamingSubscriptionRequest, folderIds, base.CallContext.OriginalCallerContext.IdentifierString);
		}

		// Token: 0x040010B3 RID: 4275
		private StreamingSubscriptionRequest streamingSubscriptionRequest;
	}
}
