using System;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Notifications.Broker;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Services.Core;
using Microsoft.Exchange.Services.Core.DataConverter;
using Microsoft.Exchange.Services.Core.Search;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Notifications.Broker
{
	// Token: 0x02000011 RID: 17
	internal class ConversationNotificationHandler : RowNotificationHandler
	{
		// Token: 0x060000BC RID: 188 RVA: 0x0000582A File Offset: 0x00003A2A
		public ConversationNotificationHandler(string name, MailboxSessionContext sessionContext, BaseSubscription parameters) : base(name, sessionContext, parameters)
		{
			ArgumentValidator.ThrowIfTypeInvalid<ConversationSubscription>("parameters", parameters);
			this.conversationViewQuerySubscriptionProperties = ConversationNotificationHandler.GetSubscriptionProperties(((ConversationSubscription)parameters).ConversationShape);
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000BD RID: 189 RVA: 0x00005856 File Offset: 0x00003A56
		protected override PropertyDefinition[] SubscriptionProperties
		{
			get
			{
				return this.conversationViewQuerySubscriptionProperties;
			}
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00005860 File Offset: 0x00003A60
		protected override RowNotification GetPayloadFromNotification(StoreObjectId folderId, QueryNotification notification)
		{
			ExTraceGlobals.ServiceTracer.TraceDebug<string>((long)this.GetHashCode(), "Handler : {0}. GetPayloadFromNotification.", base.Name);
			ConversationNotification conversationNotification = new ConversationNotification();
			base.GetPartialPayloadFromNotification(conversationNotification, notification);
			conversationNotification.Conversation = this.GetConversationFromNotification(notification);
			return conversationNotification;
		}

		// Token: 0x060000BF RID: 191 RVA: 0x000058A8 File Offset: 0x00003AA8
		protected override QueryResult GetQueryResult(Folder folder)
		{
			ExTraceGlobals.ServiceTracer.TraceDebug<string>((long)this.GetHashCode(), "Handler : {0}. GetQueryResult.", base.Name);
			QueryFilter queryFilter = null;
			SortBy[] sortColumns = base.SortBy;
			if (SearchUtil.IsComplexClutterFilteredView(base.ViewFilter, base.ClutterFilter))
			{
				queryFilter = SearchUtil.GetViewQueryForComplexClutterFilteredView(base.ClutterFilter, true);
			}
			else if (!string.IsNullOrEmpty(base.FromFilter))
			{
				queryFilter = PeopleIKnowQuery.GetConversationQueryFilter(base.FromFilter);
				sortColumns = PeopleIKnowQuery.GetConversationQuerySortBy(base.SortBy);
			}
			return folder.ConversationItemQuery(queryFilter, sortColumns, this.SubscriptionProperties);
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x0000592F File Offset: 0x00003B2F
		protected override RowNotification CreateBlankPayload()
		{
			return new ConversationNotification();
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00005936 File Offset: 0x00003B36
		protected override RowNotification ProcessQueryResultChangedNotification()
		{
			return base.GetRefreshPayload();
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x0000593E File Offset: 0x00003B3E
		protected override RowNotification ProcessReloadNotification()
		{
			return base.GetRefreshPayload();
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00005948 File Offset: 0x00003B48
		private static PropertyDefinition[] GetSubscriptionProperties(ConversationResponseShape requestedResponseShape)
		{
			Shape[] shapes = new Shape[]
			{
				ConversationShape.CreateShape()
			};
			PropertyDefinition[] specialConversationProperties = ConversationNotificationHandler.GetSpecialConversationProperties(requestedResponseShape);
			return RowNotificationHandler.GetPropertyDefinitionsForResponseShape(shapes, requestedResponseShape, specialConversationProperties);
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00005994 File Offset: 0x00003B94
		private static PropertyDefinition[] GetSpecialConversationProperties(ConversationResponseShape itemShape)
		{
			PropertyDefinition[] result = null;
			if (itemShape.AdditionalProperties.Any((PropertyPath propertyPath) => propertyPath is PropertyUri && ((PropertyUri)propertyPath).Uri == PropertyUriEnum.ConversationPreview))
			{
				result = new PropertyDefinition[]
				{
					ConversationItemSchema.ConversationPreview
				};
			}
			return result;
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00005A1C File Offset: 0x00003C1C
		private ConversationType GetConversationFromNotification(QueryNotification notification)
		{
			ExTraceGlobals.ServiceTracer.TraceDebug<string>((long)this.GetHashCode(), "Handler : {0}. GetConversationFromNotification.", base.Name);
			ConversationType conv = new ConversationType
			{
				InstanceKey = notification.Index
			};
			if (notification.EventType != QueryNotificationType.RowDeleted)
			{
				conv.BulkAssignProperties(this.conversationViewQuerySubscriptionProperties, notification.Row, base.MailboxGuid, ExTimeZone.UtcTimeZone);
				StoreId[] itemProperty = RowNotificationHandler.GetItemProperty<StoreId[]>(notification, Array.IndexOf<PropertyDefinition>(this.conversationViewQuerySubscriptionProperties, ConversationItemSchema.ConversationGlobalItemIds), new StoreId[0]);
				conv.DraftStoreIds = (from storeId in itemProperty
				where DraftItemIdsProperty.IsItemInDraftsFolder(storeId, this.GetDraftFolderId())
				select storeId).Select(new Func<StoreId, StoreObjectId>(StoreId.GetStoreObjectId));
				if (conv.DraftStoreIds.FirstOrDefault<StoreId>() != null)
				{
					base.SessionContext.DoOperationUnderSessionLock(delegate(MailboxSession session)
					{
						NormalQueryView.PrepareDraftItemIds(session, new ConversationType[]
						{
							conv
						});
					});
				}
				this.LoadConversationFeedPropertiesIfRequested(notification, conv);
			}
			return conv;
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00005B46 File Offset: 0x00003D46
		private StoreObjectId GetDraftFolderId()
		{
			if (this.draftFolderId != null)
			{
				return this.draftFolderId;
			}
			base.SessionContext.DoOperationUnderSessionLock(delegate(MailboxSession session)
			{
				this.draftFolderId = session.GetDefaultFolderId(DefaultFolderType.Drafts);
			});
			return this.draftFolderId;
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00005BB0 File Offset: 0x00003DB0
		private void LoadConversationFeedPropertiesIfRequested(QueryNotification notification, ConversationType conv)
		{
			bool flag = false;
			int index = Array.IndexOf<PropertyDefinition>(this.SubscriptionProperties, ConversationItemSchema.ConversationInitialMemberDocumentId);
			int index2 = Array.IndexOf<PropertyDefinition>(this.SubscriptionProperties, ConversationItemSchema.ConversationMemberDocumentIds);
			int? initialMemberDocumentId = null;
			int[] memberDocumentIds = null;
			if (RowNotificationHandler.IsPropertyDefined(notification, index))
			{
				initialMemberDocumentId = RowNotificationHandler.GetItemProperty<int?>(notification, index);
				flag = true;
			}
			if (RowNotificationHandler.IsPropertyDefined(notification, index2))
			{
				memberDocumentIds = RowNotificationHandler.GetItemProperty<int[]>(notification, index2);
				flag = true;
			}
			if (flag)
			{
				base.SessionContext.DoOperationUnderSessionLock(delegate(MailboxSession session)
				{
					ConversationFeedLoader conversationFeedLoader = new ConversationFeedLoader(session, ExTimeZone.UtcTimeZone);
					conversationFeedLoader.LoadConversationFeedItems(conv, initialMemberDocumentId, memberDocumentIds);
				});
			}
		}

		// Token: 0x0400005C RID: 92
		private readonly PropertyDefinition[] conversationViewQuerySubscriptionProperties;

		// Token: 0x0400005D RID: 93
		private StoreObjectId draftFolderId;
	}
}
