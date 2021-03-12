using System;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200017F RID: 383
	internal static class NewMailNotificationHandlerFactory
	{
		// Token: 0x06000DE7 RID: 3559 RVA: 0x00034B23 File Offset: 0x00032D23
		public static NewMailNotificationHandler Create(string subscriptionId, IMailboxContext userContext, SubscriptionParameters parameters)
		{
			if (parameters.InferenceEnabled)
			{
				return new InferenceNewMailNotificationHandler(subscriptionId, userContext);
			}
			return new NewMailNotificationHandler(subscriptionId, userContext);
		}
	}
}
