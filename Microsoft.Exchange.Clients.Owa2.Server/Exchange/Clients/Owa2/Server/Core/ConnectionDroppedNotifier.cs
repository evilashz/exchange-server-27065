using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000167 RID: 359
	internal class ConnectionDroppedNotifier : PendingRequestNotifierBase
	{
		// Token: 0x06000D4F RID: 3407 RVA: 0x00032105 File Offset: 0x00030305
		public ConnectionDroppedNotifier(IMailboxContext userContext) : base(userContext)
		{
			this.mailboxSessionDisplayName = userContext.ExchangePrincipal.MailboxInfo.DisplayName;
			this.payloadString = string.Empty;
		}

		// Token: 0x06000D50 RID: 3408 RVA: 0x00032130 File Offset: 0x00030330
		internal override void PickupData()
		{
			if (!string.IsNullOrEmpty(this.payloadString))
			{
				base.FireDataAvailableEvent();
				ExTraceGlobals.CoreCallTracer.TraceDebug<string>((long)this.GetHashCode(), "ConnectionDroppedPayload.PickupData. DataAvailable method called. Mailbox: {0}", this.mailboxSessionDisplayName);
				return;
			}
			ExTraceGlobals.CoreCallTracer.TraceDebug<string>((long)this.GetHashCode(), "ConnectionDroppedPayload.PickupData. No need to call DataAvailable method. Mailbox: {0}", this.mailboxSessionDisplayName);
		}

		// Token: 0x06000D51 RID: 3409 RVA: 0x00032189 File Offset: 0x00030389
		protected override IList<NotificationPayloadBase> ReadDataAndResetStateInternal()
		{
			return null;
		}

		// Token: 0x0400080A RID: 2058
		private readonly string mailboxSessionDisplayName;

		// Token: 0x0400080B RID: 2059
		private readonly string payloadString;
	}
}
