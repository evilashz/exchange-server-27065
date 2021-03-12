using System;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x02000022 RID: 34
	internal interface IApnsFeedbackProvider
	{
		// Token: 0x0600015B RID: 347
		ApnsFeedbackValidationResult ValidateNotification(ApnsNotification notification);
	}
}
