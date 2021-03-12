using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000137 RID: 311
	internal abstract class PendingRequestNotifierBase : IPendingRequestNotifier
	{
		// Token: 0x06000A74 RID: 2676 RVA: 0x00023F10 File Offset: 0x00022110
		public PendingRequestNotifierBase(IMailboxContext userContext) : this(null, userContext)
		{
		}

		// Token: 0x06000A75 RID: 2677 RVA: 0x00023F1A File Offset: 0x0002211A
		public PendingRequestNotifierBase(string subscriptionId, IMailboxContext userContext)
		{
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			this.SubscriptionId = subscriptionId;
			this.UserContext = userContext;
		}

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x06000A76 RID: 2678 RVA: 0x00023F40 File Offset: 0x00022140
		// (remove) Token: 0x06000A77 RID: 2679 RVA: 0x00023F78 File Offset: 0x00022178
		public event DataAvailableEventHandler DataAvailable;

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x06000A78 RID: 2680 RVA: 0x00023FAD File Offset: 0x000221AD
		public virtual bool ShouldThrottle
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x06000A79 RID: 2681 RVA: 0x00023FB0 File Offset: 0x000221B0
		// (set) Token: 0x06000A7A RID: 2682 RVA: 0x00023FB8 File Offset: 0x000221B8
		public string SubscriptionId { get; protected set; }

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x06000A7B RID: 2683 RVA: 0x00023FC4 File Offset: 0x000221C4
		public string ContextKey
		{
			get
			{
				string result = null;
				if (this.UserContext != null)
				{
					result = this.UserContext.Key.ToString();
				}
				return result;
			}
		}

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x06000A7C RID: 2684 RVA: 0x00023FED File Offset: 0x000221ED
		// (set) Token: 0x06000A7D RID: 2685 RVA: 0x00023FF5 File Offset: 0x000221F5
		protected IMailboxContext UserContext { get; set; }

		// Token: 0x06000A7E RID: 2686 RVA: 0x00024000 File Offset: 0x00022200
		public virtual IList<NotificationPayloadBase> ReadDataAndResetState()
		{
			List<NotificationPayloadBase> result = null;
			lock (this)
			{
				this.containsDataToPickup = false;
				result = (List<NotificationPayloadBase>)this.ReadDataAndResetStateInternal();
			}
			return result;
		}

		// Token: 0x06000A7F RID: 2687 RVA: 0x0002404C File Offset: 0x0002224C
		internal void RegisterWithPendingRequestNotifier()
		{
			if (this.UserContext != null && this.UserContext.PendingRequestManager != null)
			{
				lock (this)
				{
					if (this.hasNotifierBeenUnregistered)
					{
						throw new InvalidOperationException("A notifier should not be reused after being unregistered");
					}
				}
				this.UserContext.PendingRequestManager.AddPendingRequestNotifier(this);
			}
		}

		// Token: 0x06000A80 RID: 2688 RVA: 0x000240BC File Offset: 0x000222BC
		internal void UnregisterWithPendingRequestNotifier()
		{
			if (this.UserContext != null && this.UserContext.PendingRequestManager != null)
			{
				lock (this)
				{
					if (this.hasNotifierBeenUnregistered)
					{
						throw new InvalidOperationException("A notifier should not be unregistered twice.");
					}
					this.hasNotifierBeenUnregistered = true;
				}
				this.UserContext.PendingRequestManager.RemovePendingRequestNotifier(this);
			}
		}

		// Token: 0x06000A81 RID: 2689 RVA: 0x00024138 File Offset: 0x00022338
		internal virtual void PickupData()
		{
			ExTraceGlobals.NotificationsCallTracer.TraceDebug<Type>((long)this.GetHashCode(), "PendingRequestNotifierBase.PickupData Begin. type: {0}", base.GetType());
			bool flag = false;
			lock (this)
			{
				flag = (this.IsDataAvailableForPickup() && !this.containsDataToPickup);
				if (flag)
				{
					this.containsDataToPickup = true;
				}
			}
			if (flag)
			{
				this.FireDataAvailableEvent();
				ExTraceGlobals.CoreCallTracer.TraceDebug<SmtpAddress>((long)this.GetHashCode(), "PickupData. FireDataAvailableEvent method. User {0}", this.UserContext.PrimarySmtpAddress);
				return;
			}
			ExTraceGlobals.CoreCallTracer.TraceDebug<SmtpAddress>((long)this.GetHashCode(), "PickupData. No need to call FireDataAvailableEvent method. User {0}", this.UserContext.PrimarySmtpAddress);
		}

		// Token: 0x06000A82 RID: 2690
		protected abstract IList<NotificationPayloadBase> ReadDataAndResetStateInternal();

		// Token: 0x06000A83 RID: 2691 RVA: 0x000241F8 File Offset: 0x000223F8
		protected virtual bool IsDataAvailableForPickup()
		{
			return true;
		}

		// Token: 0x06000A84 RID: 2692 RVA: 0x000241FC File Offset: 0x000223FC
		protected void FireDataAvailableEvent()
		{
			DataAvailableEventHandler dataAvailable = this.DataAvailable;
			if (dataAvailable != null)
			{
				dataAvailable(this, new EventArgs());
				return;
			}
			ExTraceGlobals.CoreCallTracer.TraceError((long)this.GetHashCode(), "FireDataAvailableEvent() cannot fire because DataAvailable is null.");
			if (!this.hasNotifierBeenUnregistered)
			{
				throw new InvalidOperationException("DataAvailable has not been set");
			}
		}

		// Token: 0x040006D9 RID: 1753
		private bool containsDataToPickup;

		// Token: 0x040006DA RID: 1754
		private volatile bool hasNotifierBeenUnregistered;
	}
}
