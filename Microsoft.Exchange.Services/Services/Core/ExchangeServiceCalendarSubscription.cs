using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.Services.ExchangeService;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x020002DC RID: 732
	internal class ExchangeServiceCalendarSubscription : ExchangeServiceSubscription
	{
		// Token: 0x06001446 RID: 5190 RVA: 0x00065523 File Offset: 0x00063723
		internal ExchangeServiceCalendarSubscription(string subscriptionId) : base(subscriptionId)
		{
		}

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06001447 RID: 5191 RVA: 0x0006552C File Offset: 0x0006372C
		// (set) Token: 0x06001448 RID: 5192 RVA: 0x00065534 File Offset: 0x00063734
		internal Subscription Subscription { get; set; }

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06001449 RID: 5193 RVA: 0x0006553D File Offset: 0x0006373D
		// (set) Token: 0x0600144A RID: 5194 RVA: 0x00065545 File Offset: 0x00063745
		internal Action<CalendarChangeNotificationType> Callback { get; set; }

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x0600144B RID: 5195 RVA: 0x0006554E File Offset: 0x0006374E
		// (set) Token: 0x0600144C RID: 5196 RVA: 0x00065556 File Offset: 0x00063756
		internal QueryResult QueryResult { get; set; }

		// Token: 0x0600144D RID: 5197 RVA: 0x00065560 File Offset: 0x00063760
		internal override void HandleNotification(Notification notification)
		{
			if (notification == null)
			{
				ExTraceGlobals.NotificationsCallTracer.TraceWarning<string>((long)this.GetHashCode(), "ExchangeServiceCalendarSubscription.HandleNotification: Received a null notification for subscriptionId: {0}", base.SubscriptionId);
				return;
			}
			if (notification is ConnectionDroppedNotification)
			{
				this.Callback(CalendarChangeNotificationType.ConnectionLost);
				ExTraceGlobals.NotificationsCallTracer.TraceDebug((long)this.GetHashCode(), "ExchangeServiceCalendarSubscription.HandleNotification: Connection dropped, returning notification for reload");
				return;
			}
			QueryNotification queryNotification = notification as QueryNotification;
			if (queryNotification == null)
			{
				ExTraceGlobals.NotificationsCallTracer.TraceWarning<string>((long)this.GetHashCode(), "ExchangeServiceCalendarSubscription.HandleNotification: Received a notification of an unknown type for subscriptionId: {0}", base.SubscriptionId);
				return;
			}
			ExTraceGlobals.NotificationsCallTracer.TraceDebug<string, string>((long)this.GetHashCode(), "ExchangeServiceCalendarSubscription.HandleNotification: Received a {0} notification for subscriptionId: {1}", queryNotification.EventType.ToString(), base.SubscriptionId);
			switch (queryNotification.EventType)
			{
			case QueryNotificationType.RowAdded:
			case QueryNotificationType.RowDeleted:
			case QueryNotificationType.RowModified:
				ExTraceGlobals.NotificationsCallTracer.TraceDebug((long)this.GetHashCode(), "ExchangeServiceCalendarSubscription.HandleNotification: Calling notification callback for calendar");
				this.Callback(CalendarChangeNotificationType.CalendarChanged);
				ExTraceGlobals.NotificationsCallTracer.TraceDebug((long)this.GetHashCode(), "ExchangeServiceCalendarSubscription.HandleNotification: Returned from callback");
				return;
			default:
				ExTraceGlobals.NotificationsCallTracer.TraceDebug((long)this.GetHashCode(), "ExchangeServiceCalendarSubscription.HandleNotification: Unknown notification event type");
				return;
			}
		}

		// Token: 0x0600144E RID: 5198 RVA: 0x00065677 File Offset: 0x00063877
		protected override void InternalDispose(bool isDisposing)
		{
			if (this.Subscription != null)
			{
				this.Subscription.Dispose();
				this.Subscription = null;
			}
			if (this.QueryResult != null)
			{
				this.QueryResult.Dispose();
				this.QueryResult = null;
			}
		}

		// Token: 0x0600144F RID: 5199 RVA: 0x000656AD File Offset: 0x000638AD
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<ExchangeServiceCalendarSubscription>(this);
		}
	}
}
