using System;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x02000391 RID: 913
	internal sealed class Unsubscribe : NotificationCommandBase<UnsubscribeRequest, ServiceResultNone>
	{
		// Token: 0x06001989 RID: 6537 RVA: 0x000916FC File Offset: 0x0008F8FC
		public Unsubscribe(CallContext callContext, UnsubscribeRequest request) : base(callContext, request)
		{
			this.subscriptionId = base.Request.SubscriptionId;
			ServiceCommandBase.ThrowIfNullOrEmpty(this.subscriptionId, "subscriptionId", "Unsubscribe:PreExecuteCommand");
		}

		// Token: 0x0600198A RID: 6538 RVA: 0x0009172C File Offset: 0x0008F92C
		internal override IExchangeWebMethodResponse GetResponse()
		{
			UnsubscribeResponse unsubscribeResponse = new UnsubscribeResponse();
			unsubscribeResponse.ProcessServiceResult(base.Result);
			return unsubscribeResponse;
		}

		// Token: 0x0600198B RID: 6539 RVA: 0x0009174C File Offset: 0x0008F94C
		internal override ServiceResult<ServiceResultNone> Execute()
		{
			SubscriptionBase subscription = Subscriptions.Singleton.Get(this.subscriptionId);
			base.ValidateSubscriptionUpdate(subscription, new Type[]
			{
				typeof(PullSubscription),
				typeof(StreamingSubscription)
			});
			Subscriptions.Singleton.Delete(this.subscriptionId);
			return new ServiceResult<ServiceResultNone>(new ServiceResultNone());
		}

		// Token: 0x04001123 RID: 4387
		private string subscriptionId;
	}
}
