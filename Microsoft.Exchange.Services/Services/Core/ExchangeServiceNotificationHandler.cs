using System;
using System.Collections.Concurrent;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x020002E0 RID: 736
	internal class ExchangeServiceNotificationHandler : DisposeTrackableBase
	{
		// Token: 0x06001485 RID: 5253 RVA: 0x0006676D File Offset: 0x0006496D
		internal static ExchangeServiceNotificationHandler GetHandler(CallContext callContext)
		{
			return ExchangeServiceNotificationHandler.GetHandler(callContext, false);
		}

		// Token: 0x06001486 RID: 5254 RVA: 0x0006678C File Offset: 0x0006498C
		internal static ExchangeServiceNotificationHandler GetHandler(CallContext callContext, bool isUnifiedSessionRequired)
		{
			string key = ExchangeServiceNotificationHandler.MakeKey(callContext, isUnifiedSessionRequired);
			return ExchangeServiceNotificationHandler.allHandlers.GetOrAdd(key, (string param0) => new ExchangeServiceNotificationHandler(isUnifiedSessionRequired));
		}

		// Token: 0x06001487 RID: 5255 RVA: 0x000667CC File Offset: 0x000649CC
		internal static void RemoveHandler(CallContext callContext, bool isUnifiedSessionRequired)
		{
			string key = ExchangeServiceNotificationHandler.MakeKey(callContext, isUnifiedSessionRequired);
			ExchangeServiceNotificationHandler exchangeServiceNotificationHandler;
			if (ExchangeServiceNotificationHandler.allHandlers.TryRemove(key, out exchangeServiceNotificationHandler))
			{
				exchangeServiceNotificationHandler.Dispose();
			}
		}

		// Token: 0x06001488 RID: 5256 RVA: 0x000667F8 File Offset: 0x000649F8
		private static string MakeKey(CallContext callContext, bool isUnifiedSessionRequired)
		{
			return string.Format("{0}_{1}_{2}_{3}_{4}", new object[]
			{
				callContext.EffectiveCaller.ObjectGuid,
				callContext.OriginalCallerContext.IdentifierString,
				callContext.MailboxIdentityPrincipal.MailboxInfo.MailboxGuid,
				callContext.LogonType,
				isUnifiedSessionRequired
			});
		}

		// Token: 0x06001489 RID: 5257 RVA: 0x00066867 File Offset: 0x00064A67
		private ExchangeServiceNotificationHandler(bool isUnifiedSessionRequired)
		{
			this.isUnifiedSessionRequired = isUnifiedSessionRequired;
		}

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x0600148A RID: 5258 RVA: 0x0006688C File Offset: 0x00064A8C
		// (set) Token: 0x0600148B RID: 5259 RVA: 0x00066894 File Offset: 0x00064A94
		internal MailboxSession Session { get; private set; }

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x0600148C RID: 5260 RVA: 0x0006689D File Offset: 0x00064A9D
		// (set) Token: 0x0600148D RID: 5261 RVA: 0x000668A5 File Offset: 0x00064AA5
		internal Subscription DisconnectSubscription { get; private set; }

		// Token: 0x0600148E RID: 5262 RVA: 0x000668B0 File Offset: 0x00064AB0
		internal void AddSubscription(ExchangePrincipal exchangePrincipal, CallContext callContext, string subscriptionId, ExchangeServiceNotificationHandler.SubscriptionCreator creatorDelegate)
		{
			lock (this.mutex)
			{
				base.CheckDisposed();
				if (this.Session != null && this.Session.IsDead)
				{
					this.Session.Dispose();
					this.Session = null;
				}
				if (this.Session == null)
				{
					this.Session = MailboxSession.OpenWithBestAccess(exchangePrincipal, callContext.AccessingADUser, callContext.EffectiveCaller.ClientSecurityContext, callContext.ClientCulture, "Client=WebServices;Action=ExchangeServiceNotification", this.isUnifiedSessionRequired);
					this.DisconnectSubscription = Subscription.CreateMailboxSubscription(this.Session, new NotificationHandler(this.OnDisconnect), NotificationType.ConnectionDropped);
				}
				this.RemoveSubscriptionInternal(subscriptionId);
				ExchangeServiceSubscription value;
				try
				{
					value = creatorDelegate();
				}
				catch (ConnectionFailedTransientException)
				{
					ExchangeServiceNotificationHandler.RemoveHandler(callContext, this.isUnifiedSessionRequired);
					throw;
				}
				this.subscriptions.TryAdd(subscriptionId, value);
			}
		}

		// Token: 0x0600148F RID: 5263 RVA: 0x000669AC File Offset: 0x00064BAC
		internal void RemoveSubscription(string subscriptionId)
		{
			lock (this.mutex)
			{
				this.RemoveSubscriptionInternal(subscriptionId);
				if (this.subscriptions.Count == 0)
				{
					if (this.Session != null)
					{
						this.Session.Dispose();
						this.Session = null;
					}
					if (this.DisconnectSubscription != null)
					{
						this.DisconnectSubscription.Dispose();
						this.DisconnectSubscription = null;
					}
				}
			}
		}

		// Token: 0x06001490 RID: 5264 RVA: 0x00066A30 File Offset: 0x00064C30
		private void OnDisconnect(Notification notification)
		{
			foreach (ExchangeServiceSubscription exchangeServiceSubscription in this.subscriptions.Values)
			{
				exchangeServiceSubscription.HandleNotification(notification);
			}
		}

		// Token: 0x06001491 RID: 5265 RVA: 0x00066A84 File Offset: 0x00064C84
		private void RemoveSubscriptionInternal(string subscriptionId)
		{
			if (subscriptionId == null)
			{
				return;
			}
			ExchangeServiceSubscription exchangeServiceSubscription = null;
			if (this.subscriptions.TryRemove(subscriptionId, out exchangeServiceSubscription) && exchangeServiceSubscription != null)
			{
				exchangeServiceSubscription.Dispose();
				exchangeServiceSubscription = null;
			}
		}

		// Token: 0x06001492 RID: 5266 RVA: 0x00066AB4 File Offset: 0x00064CB4
		protected override void InternalDispose(bool isDisposing)
		{
			lock (this.mutex)
			{
				base.CheckDisposed();
				foreach (ExchangeServiceSubscription exchangeServiceSubscription in this.subscriptions.Values)
				{
					exchangeServiceSubscription.Dispose();
				}
				this.subscriptions.Clear();
				if (this.Session != null)
				{
					this.Session.Dispose();
					this.Session = null;
				}
				if (this.DisconnectSubscription != null)
				{
					this.DisconnectSubscription.Dispose();
					this.DisconnectSubscription = null;
				}
			}
		}

		// Token: 0x06001493 RID: 5267 RVA: 0x00066B74 File Offset: 0x00064D74
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<ExchangeServiceNotificationHandler>(this);
		}

		// Token: 0x04000DA2 RID: 3490
		internal const string ClientInfoString = "Client=WebServices;Action=ExchangeServiceNotification";

		// Token: 0x04000DA3 RID: 3491
		private static readonly ConcurrentDictionary<string, ExchangeServiceNotificationHandler> allHandlers = new ConcurrentDictionary<string, ExchangeServiceNotificationHandler>();

		// Token: 0x04000DA4 RID: 3492
		private readonly object mutex = new object();

		// Token: 0x04000DA5 RID: 3493
		private readonly ConcurrentDictionary<string, ExchangeServiceSubscription> subscriptions = new ConcurrentDictionary<string, ExchangeServiceSubscription>();

		// Token: 0x04000DA6 RID: 3494
		private bool isUnifiedSessionRequired;

		// Token: 0x020002E1 RID: 737
		// (Invoke) Token: 0x06001496 RID: 5270
		internal delegate ExchangeServiceSubscription SubscriptionCreator();
	}
}
