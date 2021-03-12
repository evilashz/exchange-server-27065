using System;
using Microsoft.Exchange.Clients.Owa2.Server.Diagnostics;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Notifications.Broker;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000157 RID: 343
	internal abstract class BrokerHandler : DisposeTrackableBase
	{
		// Token: 0x06000C75 RID: 3189 RVA: 0x0002E56F File Offset: 0x0002C76F
		protected BrokerHandler(string subscriptionId, SubscriptionParameters parameters, IMailboxContext userContext)
		{
			this.SubscriptionId = subscriptionId;
			this.Parameters = parameters;
			this.UserContext = userContext;
			this.BrokerSubscriptionId = Guid.NewGuid();
			this.nextResubscribeTime = ExDateTime.MinValue;
		}

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x06000C76 RID: 3190 RVA: 0x0002E5A2 File Offset: 0x0002C7A2
		// (set) Token: 0x06000C77 RID: 3191 RVA: 0x0002E5AA File Offset: 0x0002C7AA
		public string SubscriptionId { get; private set; }

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x06000C78 RID: 3192 RVA: 0x0002E5B3 File Offset: 0x0002C7B3
		// (set) Token: 0x06000C79 RID: 3193 RVA: 0x0002E5BB File Offset: 0x0002C7BB
		public Guid BrokerSubscriptionId { get; private set; }

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x06000C7A RID: 3194 RVA: 0x0002E5C4 File Offset: 0x0002C7C4
		// (set) Token: 0x06000C7B RID: 3195 RVA: 0x0002E5CC File Offset: 0x0002C7CC
		private protected SubscriptionParameters Parameters { protected get; private set; }

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x06000C7C RID: 3196 RVA: 0x0002E5D5 File Offset: 0x0002C7D5
		// (set) Token: 0x06000C7D RID: 3197 RVA: 0x0002E5DD File Offset: 0x0002C7DD
		private protected IMailboxContext UserContext { protected get; private set; }

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x06000C7E RID: 3198 RVA: 0x0002E5E6 File Offset: 0x0002C7E6
		protected virtual int ExpirationDurationInMins
		{
			get
			{
				return BrokerHandler.DefaultExpirationDurationInMins;
			}
		}

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x06000C7F RID: 3199 RVA: 0x0002E5ED File Offset: 0x0002C7ED
		protected virtual int ResubscribeTimeInMins
		{
			get
			{
				return BrokerHandler.DefaultResubscribeTimeInMins;
			}
		}

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x06000C80 RID: 3200 RVA: 0x0002E5F4 File Offset: 0x0002C7F4
		public virtual IBrokerGateway Gateway
		{
			get
			{
				return BrokerGateway.Instance;
			}
		}

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x06000C81 RID: 3201 RVA: 0x0002E5FB File Offset: 0x0002C7FB
		protected virtual ExchangePrincipal ReceiverPrincipal
		{
			get
			{
				return this.UserContext.ExchangePrincipal;
			}
		}

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x06000C82 RID: 3202 RVA: 0x0002E608 File Offset: 0x0002C808
		protected virtual ExchangePrincipal SenderPrincipal
		{
			get
			{
				return this.UserContext.ExchangePrincipal;
			}
		}

		// Token: 0x06000C83 RID: 3203 RVA: 0x0002E618 File Offset: 0x0002C818
		public void Subscribe()
		{
			try
			{
				this.SubscribeInternal();
			}
			catch (NotificationsBrokerException handledException)
			{
				OwaServerTraceLogger.AppendToLog(new BrokerLogEvent
				{
					Principal = this.UserContext.ExchangePrincipal,
					UserContextKey = this.UserContext.Key.ToString(),
					SubscriptionId = this.SubscriptionId,
					BrokerSubscriptionId = this.BrokerSubscriptionId,
					EventName = "Subscribe",
					HandledException = handledException
				});
				throw;
			}
		}

		// Token: 0x06000C84 RID: 3204 RVA: 0x0002E6A0 File Offset: 0x0002C8A0
		public void KeepAlive(ExDateTime eventTime)
		{
			if (base.IsDisposed || eventTime <= this.nextResubscribeTime)
			{
				return;
			}
			try
			{
				this.SubscribeInternal();
			}
			catch (NotificationsBrokerException handledException)
			{
				OwaServerTraceLogger.AppendToLog(new BrokerLogEvent
				{
					Principal = this.UserContext.ExchangePrincipal,
					UserContextKey = this.UserContext.Key.ToString(),
					SubscriptionId = this.SubscriptionId,
					BrokerSubscriptionId = this.BrokerSubscriptionId,
					EventName = "KeepAlive",
					HandledException = handledException
				});
			}
		}

		// Token: 0x06000C85 RID: 3205 RVA: 0x0002E758 File Offset: 0x0002C958
		public void HandleNotification(BrokerNotification notification)
		{
			try
			{
				if (!base.IsDisposed)
				{
					OwaDiagnostics.SendWatsonReportsForGrayExceptions(delegate()
					{
						this.HandleNotificatonInternal(notification);
					});
				}
			}
			catch (GrayException handledException)
			{
				OwaServerTraceLogger.AppendToLog(new BrokerLogEvent
				{
					Principal = this.UserContext.ExchangePrincipal,
					UserContextKey = this.UserContext.Key.ToString(),
					SubscriptionId = this.SubscriptionId,
					BrokerSubscriptionId = this.BrokerSubscriptionId,
					EventName = "HandleNotification",
					HandledException = handledException
				});
			}
		}

		// Token: 0x06000C86 RID: 3206
		protected abstract BaseSubscription GetSubscriptionParmeters();

		// Token: 0x06000C87 RID: 3207
		protected abstract void HandleNotificatonInternal(BrokerNotification notification);

		// Token: 0x06000C88 RID: 3208 RVA: 0x0002E810 File Offset: 0x0002CA10
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<BrokerHandler>(this);
		}

		// Token: 0x06000C89 RID: 3209 RVA: 0x0002E818 File Offset: 0x0002CA18
		protected override void InternalDispose(bool isDisposing)
		{
			if (isDisposing)
			{
				this.Unsubscribe();
			}
		}

		// Token: 0x06000C8A RID: 3210 RVA: 0x0002E824 File Offset: 0x0002CA24
		private void SubscribeInternal()
		{
			this.Gateway.Subscribe(this.GetBrokerSubscription(), this);
			this.nextResubscribeTime = ExDateTime.Now.AddMinutes((double)this.ResubscribeTimeInMins);
		}

		// Token: 0x06000C8B RID: 3211 RVA: 0x0002E860 File Offset: 0x0002CA60
		private void Unsubscribe()
		{
			try
			{
				this.Gateway.Unsubscribe(this.GetBrokerSubscription());
			}
			catch (NotificationsBrokerException handledException)
			{
				OwaServerTraceLogger.AppendToLog(new BrokerLogEvent
				{
					Principal = this.UserContext.ExchangePrincipal,
					UserContextKey = this.UserContext.Key.ToString(),
					SubscriptionId = this.SubscriptionId,
					BrokerSubscriptionId = this.BrokerSubscriptionId,
					EventName = "Unsubscribe",
					HandledException = handledException
				});
			}
		}

		// Token: 0x06000C8C RID: 3212 RVA: 0x0002E8F0 File Offset: 0x0002CAF0
		private BrokerSubscription GetBrokerSubscription()
		{
			return BrokerSubscriptionFactory.Create(this.BrokerSubscriptionId, this.Parameters.ChannelId, DateTime.UtcNow.AddMinutes((double)this.ExpirationDurationInMins), this.SenderPrincipal, this.ReceiverPrincipal, this.GetSubscriptionParmeters());
		}

		// Token: 0x040007C9 RID: 1993
		private const string DefaultExpirationDurationKey = "BrokerHandlerDefaultExpirationDurationInMins";

		// Token: 0x040007CA RID: 1994
		private const string DefaultResubscribeTimeKey = "BrokerHandlerDefaultResubscribeTimeInMins";

		// Token: 0x040007CB RID: 1995
		private static readonly int DefaultExpirationDurationInMins = BaseApplication.GetAppSetting<int>("BrokerHandlerDefaultExpirationDurationInMins", 60);

		// Token: 0x040007CC RID: 1996
		private static readonly int DefaultResubscribeTimeInMins = BaseApplication.GetAppSetting<int>("BrokerHandlerDefaultResubscribeTimeInMins", 45);

		// Token: 0x040007CD RID: 1997
		private ExDateTime nextResubscribeTime;
	}
}
