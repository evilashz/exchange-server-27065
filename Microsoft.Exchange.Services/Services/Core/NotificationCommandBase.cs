using System;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x02000308 RID: 776
	internal abstract class NotificationCommandBase<RequestType, SingleItemType> : SingleStepServiceCommand<RequestType, SingleItemType> where RequestType : BaseRequest
	{
		// Token: 0x060015FE RID: 5630 RVA: 0x00072094 File Offset: 0x00070294
		public NotificationCommandBase(CallContext callContext, RequestType request) : base(callContext, request)
		{
		}

		// Token: 0x060015FF RID: 5631 RVA: 0x000720A0 File Offset: 0x000702A0
		protected internal void ValidateSubscriptionUpdate(SubscriptionBase subscription, params Type[] subscriptionTypes)
		{
			bool flag = false;
			foreach (Type o in subscriptionTypes)
			{
				if (subscription.GetType().Equals(o))
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				if (subscriptionTypes.Length == 1 && subscriptionTypes[0] == typeof(PullSubscription))
				{
					throw new InvalidPullSubscriptionIdException();
				}
				if (ExchangeVersion.Current.Supports(ExchangeVersion.Exchange2010SP1))
				{
					throw new InvalidSubscriptionException();
				}
				throw new InvalidPullSubscriptionIdException();
			}
			else
			{
				if (!subscription.CheckCallerHasRights(base.CallContext))
				{
					throw new SubscriptionAccessDeniedException();
				}
				return;
			}
		}
	}
}
