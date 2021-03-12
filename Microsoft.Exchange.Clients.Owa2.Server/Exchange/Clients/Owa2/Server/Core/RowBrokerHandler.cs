using System;
using System.Threading;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Notifications.Broker;
using Microsoft.Exchange.Services;
using Microsoft.Exchange.Services.Core.DataConverter;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200015C RID: 348
	internal sealed class RowBrokerHandler : BrokerHandler
	{
		// Token: 0x06000CC5 RID: 3269 RVA: 0x0002F53C File Offset: 0x0002D73C
		public RowBrokerHandler(string subscriptionId, SubscriptionParameters parameters, ExTimeZone timeZone, IMailboxContext userContext) : base(subscriptionId, parameters, userContext)
		{
			using (DisposeGuard disposeGuard = this.Guard())
			{
				this.rowNotifier = new RowNotifier(subscriptionId, userContext, userContext.ExchangePrincipal.MailboxInfo.MailboxGuid);
				this.rowNotifier.RegisterWithPendingRequestNotifier();
				this.folderId = StoreId.EwsIdToFolderStoreObjectId(parameters.FolderId);
				this.timeZone = timeZone;
				disposeGuard.Success();
			}
		}

		// Token: 0x06000CC6 RID: 3270 RVA: 0x0002F5C4 File Offset: 0x0002D7C4
		protected override BaseSubscription GetSubscriptionParmeters()
		{
			return new ConversationSubscription
			{
				ConsumerSubscriptionId = base.SubscriptionId,
				ClutterFilter = base.Parameters.ClutterFilter,
				ConversationShape = this.GetNotificationPayloadShape(base.Parameters.ConversationShapeName),
				CultureInfo = Thread.CurrentThread.CurrentCulture,
				Filter = base.Parameters.Filter,
				FromFilter = base.Parameters.FromFilter,
				FolderId = base.Parameters.FolderId,
				SortBy = base.Parameters.SortBy
			};
		}

		// Token: 0x06000CC7 RID: 3271 RVA: 0x0002F660 File Offset: 0x0002D860
		private ConversationResponseShape GetNotificationPayloadShape(string requestedConversationShapeName)
		{
			if (string.IsNullOrEmpty(requestedConversationShapeName))
			{
				requestedConversationShapeName = RowBrokerHandler.DefaultConversationViewShapeName;
			}
			ConversationResponseShape clientResponseShape = new ConversationResponseShape(ShapeEnum.IdOnly, Array<PropertyPath>.Empty);
			return Global.ResponseShapeResolver.GetResponseShape<ConversationResponseShape>(requestedConversationShapeName, clientResponseShape, this.GetFeaturesManager());
		}

		// Token: 0x06000CC8 RID: 3272 RVA: 0x0002F69C File Offset: 0x0002D89C
		protected override void HandleNotificatonInternal(BrokerNotification notification)
		{
			Microsoft.Exchange.Notifications.Broker.ConversationNotification conversationNotification = notification.Payload as Microsoft.Exchange.Notifications.Broker.ConversationNotification;
			if (conversationNotification == null)
			{
				ExTraceGlobals.NotificationsCallTracer.TraceError((long)this.GetHashCode(), "[RowBrokerHandler.HandleNotificatonInternal]. Ignoring null/wrong type of payload.");
				return;
			}
			conversationNotification.Conversation.LastDeliveryTime = this.ConvertToRequestedTimeZone(conversationNotification.Conversation.LastDeliveryTime);
			conversationNotification.Conversation.LastDeliveryOrRenewTime = this.ConvertToRequestedTimeZone(conversationNotification.Conversation.LastDeliveryOrRenewTime);
			conversationNotification.Conversation.LastDeliveryOrRenewTime = this.ConvertToRequestedTimeZone(conversationNotification.Conversation.LastModifiedTime);
			this.rowNotifier.AddFolderContentChangePayload(this.folderId, new RowNotificationPayload
			{
				Conversation = conversationNotification.Conversation,
				EventType = conversationNotification.EventType,
				FolderId = conversationNotification.FolderId,
				Item = conversationNotification.Item,
				Prior = conversationNotification.Prior,
				SubscriptionId = conversationNotification.ConsumerSubscriptionId
			});
			this.rowNotifier.PickupData();
		}

		// Token: 0x06000CC9 RID: 3273 RVA: 0x0002F790 File Offset: 0x0002D990
		private string ConvertToRequestedTimeZone(string dateTimeStringInUtc)
		{
			string text = dateTimeStringInUtc;
			if (!string.IsNullOrEmpty(text) && this.timeZone != ExTimeZone.UtcTimeZone)
			{
				text = ExDateTimeConverter.ToOffsetXsdDateTime(ExDateTimeConverter.Parse(dateTimeStringInUtc), this.timeZone);
			}
			return text;
		}

		// Token: 0x06000CCA RID: 3274 RVA: 0x0002F7C8 File Offset: 0x0002D9C8
		private IFeaturesManager GetFeaturesManager()
		{
			UserContext userContext = base.UserContext as UserContext;
			return (userContext == null) ? null : userContext.FeaturesManager;
		}

		// Token: 0x040007DA RID: 2010
		private static readonly string DefaultConversationViewShapeName = WellKnownShapeName.ConversationUberListView.ToString();

		// Token: 0x040007DB RID: 2011
		private readonly RowNotifier rowNotifier;

		// Token: 0x040007DC RID: 2012
		private readonly StoreObjectId folderId;

		// Token: 0x040007DD RID: 2013
		private readonly ExTimeZone timeZone;
	}
}
