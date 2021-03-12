using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x020000F9 RID: 249
	internal sealed class ConnectionDroppedPayload : IPendingRequestNotifier
	{
		// Token: 0x06000841 RID: 2113 RVA: 0x0003D014 File Offset: 0x0003B214
		public ConnectionDroppedPayload(UserContext userContext, MailboxSession mailboxSession, ConnectionDroppedNotificationHandler notificationHandler)
		{
			this.userContext = userContext;
			this.mailboxSessionDisplayName = string.Copy(mailboxSession.DisplayName);
			this.notificationHandler = notificationHandler;
			this.payloadString = string.Empty;
		}

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000842 RID: 2114 RVA: 0x0003D048 File Offset: 0x0003B248
		// (remove) Token: 0x06000843 RID: 2115 RVA: 0x0003D080 File Offset: 0x0003B280
		public event DataAvailableEventHandler DataAvailable;

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x06000844 RID: 2116 RVA: 0x0003D0B5 File Offset: 0x0003B2B5
		public bool ShouldThrottle
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000845 RID: 2117 RVA: 0x0003D0B8 File Offset: 0x0003B2B8
		public string ReadDataAndResetState()
		{
			return string.Empty;
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x0003D0C0 File Offset: 0x0003B2C0
		public void PickupData()
		{
			if (!string.IsNullOrEmpty(this.payloadString))
			{
				this.DataAvailable(this, new EventArgs());
				ExTraceGlobals.CoreCallTracer.TraceDebug<string>((long)this.GetHashCode(), "ConnectionDroppedPayload.PickupData. DataAvailable method called. Mailbox: {0}", this.mailboxSessionDisplayName);
				return;
			}
			ExTraceGlobals.CoreCallTracer.TraceDebug<string>((long)this.GetHashCode(), "ConnectionDroppedPayload.PickupData. No need to call DataAvailable method. Mailbox: {0}", this.mailboxSessionDisplayName);
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x0003D124 File Offset: 0x0003B324
		public void ConnectionAliveTimer()
		{
			this.notificationHandler.HandlePendingGetTimerCallback();
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x0003D131 File Offset: 0x0003B331
		internal void RegisterWithPendingRequestNotifier()
		{
			if (this.userContext != null && this.userContext.PendingRequestManager != null)
			{
				this.userContext.PendingRequestManager.AddPendingRequestNotifier(this);
			}
		}

		// Token: 0x040005E3 RID: 1507
		private UserContext userContext;

		// Token: 0x040005E4 RID: 1508
		private ConnectionDroppedNotificationHandler notificationHandler;

		// Token: 0x040005E5 RID: 1509
		private string mailboxSessionDisplayName;

		// Token: 0x040005E6 RID: 1510
		private string payloadString;
	}
}
