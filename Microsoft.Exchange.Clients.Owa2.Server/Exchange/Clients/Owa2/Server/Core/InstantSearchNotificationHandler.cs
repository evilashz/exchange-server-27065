using System;
using Microsoft.Exchange.Services.Wcf;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000185 RID: 389
	internal class InstantSearchNotificationHandler : IInstantSearchNotificationHandler, IDisposable
	{
		// Token: 0x06000E05 RID: 3589 RVA: 0x00034DF4 File Offset: 0x00032FF4
		public InstantSearchNotificationHandler(UserContext userContext)
		{
			this.notifier = new InstantSearchNotifier("InstantSearchNotification", userContext);
			this.notifier.RegisterWithPendingRequestNotifier();
		}

		// Token: 0x06000E06 RID: 3590 RVA: 0x00034E18 File Offset: 0x00033018
		public void DeliverInstantSearchPayload(InstantSearchPayloadType instantSearchPayloadType)
		{
			this.notifier.AddPayload(new InstantSearchNotificationPayload("InstantSearchNotification", instantSearchPayloadType));
		}

		// Token: 0x06000E07 RID: 3591 RVA: 0x00034E30 File Offset: 0x00033030
		public void Dispose()
		{
			this.notifier.UnregisterWithPendingRequestNotifier();
		}

		// Token: 0x04000876 RID: 2166
		public const string InstantSearchSubscriptionId = "InstantSearchNotification";

		// Token: 0x04000877 RID: 2167
		private readonly InstantSearchNotifier notifier;
	}
}
