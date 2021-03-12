using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x02000005 RID: 5
	internal interface IThrottlingManager
	{
		// Token: 0x0600000D RID: 13
		bool TryApproveNotification(PushNotification notification, out OverBudgetException overBudgetException);

		// Token: 0x0600000E RID: 14
		void ReportInvalidNotifications(PushNotification notification);
	}
}
