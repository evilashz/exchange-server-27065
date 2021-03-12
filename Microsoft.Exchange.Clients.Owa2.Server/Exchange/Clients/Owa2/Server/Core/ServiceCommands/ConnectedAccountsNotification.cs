using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.ServiceCommands
{
	// Token: 0x020002F5 RID: 757
	internal class ConnectedAccountsNotification : ServiceCommand<bool>
	{
		// Token: 0x0600198A RID: 6538 RVA: 0x000593B9 File Offset: 0x000575B9
		public ConnectedAccountsNotification(CallContext callContext, bool isOWALogon) : base(callContext)
		{
			this.isOWALogon = isOWALogon;
		}

		// Token: 0x0600198B RID: 6539 RVA: 0x000593CC File Offset: 0x000575CC
		protected override bool InternalExecute()
		{
			UserContext userContext = UserContextManager.GetUserContext(CallContext.Current.HttpContext, CallContext.Current.EffectiveCaller, true);
			List<IConnectedAccountsNotificationManager> connectedAccountNotificationManagers = userContext.GetConnectedAccountNotificationManagers(base.MailboxIdentityMailboxSession);
			foreach (IConnectedAccountsNotificationManager notificationManager in connectedAccountNotificationManagers)
			{
				this.SendSyncNowNotification(notificationManager);
			}
			return true;
		}

		// Token: 0x0600198C RID: 6540 RVA: 0x00059444 File Offset: 0x00057644
		private void SendSyncNowNotification(IConnectedAccountsNotificationManager notificationManager)
		{
			if (this.isOWALogon)
			{
				notificationManager.SendLogonTriggeredSyncNowRequest();
				ExTraceGlobals.ConnectedAccountsTracer.TraceDebug((long)this.GetHashCode(), "ConnectedAccountsNotification.SendSyncNowNotification - ConnectedAccountsNotificationManager was setup and SendLogonTriggeredSyncNowRequest invoked.");
				return;
			}
			notificationManager.SendRefreshButtonTriggeredSyncNowRequest();
			ExTraceGlobals.ConnectedAccountsTracer.TraceDebug((long)this.GetHashCode(), "ConnectedAccountsNotification.SendSyncNowNotification - ConnectedAccountsNotificationManager is setup and SendRefreshButtonTriggeredSyncNowRequest invoked.");
		}

		// Token: 0x04000E16 RID: 3606
		private readonly bool isOWALogon;
	}
}
