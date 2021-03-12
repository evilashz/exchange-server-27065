using System;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x020002DD RID: 733
	internal class ExchangeServiceConversationSubscription : ExchangeServiceSubscription
	{
		// Token: 0x06001450 RID: 5200 RVA: 0x000656B5 File Offset: 0x000638B5
		internal ExchangeServiceConversationSubscription(string subscriptionId) : base(subscriptionId)
		{
		}

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06001451 RID: 5201 RVA: 0x000656BE File Offset: 0x000638BE
		// (set) Token: 0x06001452 RID: 5202 RVA: 0x000656C6 File Offset: 0x000638C6
		internal Guid MailboxGuid { get; set; }

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06001453 RID: 5203 RVA: 0x000656CF File Offset: 0x000638CF
		// (set) Token: 0x06001454 RID: 5204 RVA: 0x000656D7 File Offset: 0x000638D7
		internal Subscription Subscription { get; set; }

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06001455 RID: 5205 RVA: 0x000656E0 File Offset: 0x000638E0
		// (set) Token: 0x06001456 RID: 5206 RVA: 0x000656E8 File Offset: 0x000638E8
		internal Action<ConversationNotification> Callback { get; set; }

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x06001457 RID: 5207 RVA: 0x000656F1 File Offset: 0x000638F1
		// (set) Token: 0x06001458 RID: 5208 RVA: 0x000656F9 File Offset: 0x000638F9
		internal QueryResult QueryResult { get; set; }

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x06001459 RID: 5209 RVA: 0x00065702 File Offset: 0x00063902
		// (set) Token: 0x0600145A RID: 5210 RVA: 0x0006570A File Offset: 0x0006390A
		internal PropertyDefinition[] PropertyList { get; set; }

		// Token: 0x0600145B RID: 5211 RVA: 0x00065714 File Offset: 0x00063914
		internal override void HandleNotification(Notification notification)
		{
			ConversationNotification conversationNotification = null;
			if (notification == null)
			{
				ExTraceGlobals.NotificationsCallTracer.TraceWarning<string>((long)this.GetHashCode(), "ExchangeServiceConversationSubscription.HandleNotification: Received a null notification for subscriptionId: {0}", base.SubscriptionId);
				return;
			}
			if (notification is ConnectionDroppedNotification)
			{
				conversationNotification = new ConversationNotification();
				conversationNotification.NotificationType = NotificationTypeType.Reload;
				ExTraceGlobals.NotificationsCallTracer.TraceDebug((long)this.GetHashCode(), "ExchangeServiceConversationSubscription.HandleNotification: Connection dropped, returning notification for reload");
			}
			else
			{
				QueryNotification queryNotification = notification as QueryNotification;
				if (queryNotification == null)
				{
					ExTraceGlobals.NotificationsCallTracer.TraceWarning<string>((long)this.GetHashCode(), "ExchangeServiceConversationSubscription.HandleNotification: Received a notification of an unknown type for subscriptionId: {0}", base.SubscriptionId);
					return;
				}
				ExTraceGlobals.NotificationsCallTracer.TraceDebug<string, string>((long)this.GetHashCode(), "ExchangeServiceConversationSubscription.HandleNotification: Received a {0} notification for subscriptionId: {1}", queryNotification.EventType.ToString(), base.SubscriptionId);
				switch (queryNotification.EventType)
				{
				case QueryNotificationType.RowAdded:
				case QueryNotificationType.RowModified:
					conversationNotification = new ConversationNotification();
					conversationNotification.NotificationType = ((queryNotification.EventType == QueryNotificationType.RowAdded) ? NotificationTypeType.Create : NotificationTypeType.Update);
					conversationNotification.Conversation = this.GetConversationFromNotification(queryNotification);
					ExTraceGlobals.NotificationsCallTracer.TraceDebug<string>((long)this.GetHashCode(), "ExchangeServiceConversationSubscription.HandleNotification: Calling notification callback for conversation: {0}", conversationNotification.Conversation.ConversationId.Id);
					goto IL_19B;
				case QueryNotificationType.RowDeleted:
					conversationNotification = new ConversationNotification();
					conversationNotification.NotificationType = NotificationTypeType.Delete;
					conversationNotification.Conversation = this.GetConversationFromNotification(queryNotification, queryNotification.PropertyDefinitions.ToArray<PropertyDefinition>());
					ExTraceGlobals.NotificationsCallTracer.TraceDebug((long)this.GetHashCode(), "ExchangeServiceConversationSubscription.HandleNotification: Notification for deletion");
					goto IL_19B;
				case QueryNotificationType.Reload:
					conversationNotification = new ConversationNotification();
					conversationNotification.NotificationType = NotificationTypeType.Reload;
					ExTraceGlobals.NotificationsCallTracer.TraceDebug((long)this.GetHashCode(), "ExchangeServiceConversationSubscription.HandleNotification: Notification for reload");
					goto IL_19B;
				}
				ExTraceGlobals.NotificationsCallTracer.TraceDebug((long)this.GetHashCode(), "ExchangeServiceConversationSubscription.HandleNotification: Unknown notification event type");
			}
			IL_19B:
			if (conversationNotification != null)
			{
				this.Callback(conversationNotification);
				ExTraceGlobals.NotificationsCallTracer.TraceDebug((long)this.GetHashCode(), "ExchangeServiceConversationSubscription.HandleNotification: Returned from callback");
			}
		}

		// Token: 0x0600145C RID: 5212 RVA: 0x000658E1 File Offset: 0x00063AE1
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

		// Token: 0x0600145D RID: 5213 RVA: 0x00065917 File Offset: 0x00063B17
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<ExchangeServiceConversationSubscription>(this);
		}

		// Token: 0x0600145E RID: 5214 RVA: 0x0006591F File Offset: 0x00063B1F
		private ConversationType GetConversationFromNotification(QueryNotification notification)
		{
			return this.GetConversationFromNotification(notification, this.PropertyList);
		}

		// Token: 0x0600145F RID: 5215 RVA: 0x00065930 File Offset: 0x00063B30
		private ConversationType GetConversationFromNotification(QueryNotification notification, PropertyDefinition[] propertyList)
		{
			ConversationType conversationType = new ConversationType();
			conversationType.InstanceKey = notification.Index;
			conversationType.BulkAssignProperties(propertyList, notification.Row, this.MailboxGuid, null);
			return conversationType;
		}
	}
}
