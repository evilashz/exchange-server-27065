using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PushNotifications.Server.Services
{
	// Token: 0x0200001A RID: 26
	internal interface IDeviceBudget : IBudget, IDisposable
	{
		// Token: 0x0600009A RID: 154
		bool TryApproveSendNotification(out OverBudgetException obe);

		// Token: 0x0600009B RID: 155
		bool TryApproveInvalidNotification(out OverBudgetException obe);
	}
}
