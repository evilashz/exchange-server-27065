using System;
using System.Text;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x0200025F RID: 607
	internal sealed class SubscriptionPayload : IPendingRequestNotifier
	{
		// Token: 0x06001476 RID: 5238 RVA: 0x0007D041 File Offset: 0x0007B241
		public SubscriptionPayload(UserContext userContext, MailboxSession mailboxSession, SubscriptionNotificationHandler notificationHandler)
		{
			this.payloadString = new StringBuilder(256);
			this.userContext = userContext;
			this.mailboxSessionDisplayName = string.Copy(mailboxSession.DisplayName);
			this.notificationHandler = notificationHandler;
		}

		// Token: 0x1400000D RID: 13
		// (add) Token: 0x06001477 RID: 5239 RVA: 0x0007D078 File Offset: 0x0007B278
		// (remove) Token: 0x06001478 RID: 5240 RVA: 0x0007D0B0 File Offset: 0x0007B2B0
		public event DataAvailableEventHandler DataAvailable;

		// Token: 0x1700056A RID: 1386
		// (get) Token: 0x06001479 RID: 5241 RVA: 0x0007D0E5 File Offset: 0x0007B2E5
		public bool ShouldThrottle
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600147A RID: 5242 RVA: 0x0007D0E8 File Offset: 0x0007B2E8
		public string ReadDataAndResetState()
		{
			string result = string.Empty;
			ExTraceGlobals.CoreCallTracer.TraceDebug<string>((long)this.GetHashCode(), "SubscriptionPayload.ReadDataAndResetState. Mailbox: {0}", this.mailboxSessionDisplayName);
			lock (this)
			{
				this.containsDataToPickup = false;
				if (0 < this.payloadString.Length)
				{
					result = this.payloadString.ToString();
					this.Clear();
				}
			}
			return result;
		}

		// Token: 0x0600147B RID: 5243 RVA: 0x0007D168 File Offset: 0x0007B368
		public void PickupData()
		{
			bool flag = false;
			lock (this)
			{
				flag = (!this.containsDataToPickup && 0 < this.payloadString.Length);
				if (flag)
				{
					this.containsDataToPickup = true;
				}
			}
			if (flag)
			{
				this.DataAvailable(this, new EventArgs());
				ExTraceGlobals.CoreCallTracer.TraceDebug<string>((long)this.GetHashCode(), "SubscriptionPayload.PickupData. DataAvailable method called. Mailbox: {0}", this.mailboxSessionDisplayName);
				return;
			}
			ExTraceGlobals.CoreCallTracer.TraceDebug<string>((long)this.GetHashCode(), "SubscriptionPayload.PickupData. No need to call DataAvailable method. Mailbox: {0}", this.mailboxSessionDisplayName);
		}

		// Token: 0x0600147C RID: 5244 RVA: 0x0007D210 File Offset: 0x0007B410
		public void Clear()
		{
			lock (this)
			{
				this.payloadString.Remove(0, this.payloadString.Length);
			}
		}

		// Token: 0x0600147D RID: 5245 RVA: 0x0007D260 File Offset: 0x0007B460
		public void ConnectionAliveTimer()
		{
			this.notificationHandler.HandlePendingGetTimerCallback();
		}

		// Token: 0x0600147E RID: 5246 RVA: 0x0007D270 File Offset: 0x0007B470
		public void AddPayload(StringBuilder payloadBuilder)
		{
			lock (this)
			{
				this.payloadString.Append(payloadBuilder.ToString());
			}
		}

		// Token: 0x0600147F RID: 5247 RVA: 0x0007D2B8 File Offset: 0x0007B4B8
		internal void RegisterWithPendingRequestNotifier()
		{
			if (this.userContext != null && this.userContext.PendingRequestManager != null)
			{
				this.userContext.PendingRequestManager.AddPendingRequestNotifier(this);
			}
		}

		// Token: 0x04000DF4 RID: 3572
		private const int DefaultPayloadStringSize = 256;

		// Token: 0x04000DF5 RID: 3573
		private bool containsDataToPickup;

		// Token: 0x04000DF6 RID: 3574
		private StringBuilder payloadString;

		// Token: 0x04000DF7 RID: 3575
		private UserContext userContext;

		// Token: 0x04000DF8 RID: 3576
		private SubscriptionNotificationHandler notificationHandler;

		// Token: 0x04000DF9 RID: 3577
		private string mailboxSessionDisplayName;
	}
}
