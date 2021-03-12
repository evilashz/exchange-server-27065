using System;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Services.Core.DataConverter;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x020002DF RID: 735
	internal class ExchangeServiceMessageSubscription : ExchangeServiceSubscription
	{
		// Token: 0x0600146D RID: 5229 RVA: 0x00065CE4 File Offset: 0x00063EE4
		internal ExchangeServiceMessageSubscription(string subscriptionId) : base(subscriptionId)
		{
			this.mailboxId = new LazyMember<MailboxId>(() => new MailboxId(this.MailboxGuid));
		}

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x0600146E RID: 5230 RVA: 0x00065D16 File Offset: 0x00063F16
		// (set) Token: 0x0600146F RID: 5231 RVA: 0x00065D1E File Offset: 0x00063F1E
		internal Guid MailboxGuid { get; set; }

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x06001470 RID: 5232 RVA: 0x00065D27 File Offset: 0x00063F27
		// (set) Token: 0x06001471 RID: 5233 RVA: 0x00065D2F File Offset: 0x00063F2F
		internal Subscription Subscription { get; set; }

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06001472 RID: 5234 RVA: 0x00065D38 File Offset: 0x00063F38
		// (set) Token: 0x06001473 RID: 5235 RVA: 0x00065D40 File Offset: 0x00063F40
		internal Action<MessageNotification> Callback { get; set; }

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x06001474 RID: 5236 RVA: 0x00065D49 File Offset: 0x00063F49
		// (set) Token: 0x06001475 RID: 5237 RVA: 0x00065D51 File Offset: 0x00063F51
		internal QueryResult QueryResult { get; set; }

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x06001476 RID: 5238 RVA: 0x00065D5A File Offset: 0x00063F5A
		// (set) Token: 0x06001477 RID: 5239 RVA: 0x00065D62 File Offset: 0x00063F62
		internal PropertyDefinition[] PropertyList { get; set; }

		// Token: 0x06001478 RID: 5240 RVA: 0x00065D6C File Offset: 0x00063F6C
		internal override void HandleNotification(Notification notification)
		{
			MessageNotification messageNotification = null;
			if (notification == null)
			{
				ExTraceGlobals.NotificationsCallTracer.TraceWarning<string>((long)this.GetHashCode(), "ExchangeServiceMessageSubscription.HandleNotification: Received a null notification for subscriptionId: {0}", base.SubscriptionId);
				return;
			}
			if (notification is ConnectionDroppedNotification)
			{
				messageNotification = new MessageNotification();
				messageNotification.NotificationType = NotificationTypeType.Reload;
				ExTraceGlobals.NotificationsCallTracer.TraceDebug((long)this.GetHashCode(), "ExchangeServiceMessageSubscription.HandleNotification: Connection dropped, returning notification for reload");
			}
			else
			{
				QueryNotification queryNotification = notification as QueryNotification;
				if (queryNotification == null)
				{
					ExTraceGlobals.NotificationsCallTracer.TraceWarning<string>((long)this.GetHashCode(), "ExchangeServiceMessageSubscription.HandleNotification: Received a notification of an unknown type for subscriptionId: {0}", base.SubscriptionId);
					return;
				}
				ExTraceGlobals.NotificationsCallTracer.TraceDebug<string, string>((long)this.GetHashCode(), "ExchangeServiceMessageSubscription.HandleNotification: Received a {0} notification for subscriptionId: {1}", queryNotification.EventType.ToString(), base.SubscriptionId);
				switch (queryNotification.EventType)
				{
				case QueryNotificationType.RowAdded:
				case QueryNotificationType.RowModified:
					messageNotification = new MessageNotification();
					messageNotification.NotificationType = ((queryNotification.EventType == QueryNotificationType.RowAdded) ? NotificationTypeType.Create : NotificationTypeType.Update);
					messageNotification.Message = this.GetMessageFromNotification(queryNotification, this.PropertyList);
					ExTraceGlobals.NotificationsCallTracer.TraceDebug<string>((long)this.GetHashCode(), "ExchangeServiceMessageSubscription.HandleNotification: Calling notification callback for message: {0}", messageNotification.Message.ItemId.Id);
					goto IL_1A1;
				case QueryNotificationType.RowDeleted:
					messageNotification = new MessageNotification();
					messageNotification.NotificationType = NotificationTypeType.Delete;
					messageNotification.Message = this.GetMessageFromNotification(queryNotification, queryNotification.PropertyDefinitions.ToArray<PropertyDefinition>());
					ExTraceGlobals.NotificationsCallTracer.TraceDebug((long)this.GetHashCode(), "ExchangeServiceMessageSubscription.HandleNotification: Notification for deletion");
					goto IL_1A1;
				case QueryNotificationType.Reload:
					messageNotification = new MessageNotification();
					messageNotification.NotificationType = NotificationTypeType.Reload;
					ExTraceGlobals.NotificationsCallTracer.TraceDebug((long)this.GetHashCode(), "ExchangeServiceMessageSubscription.HandleNotification: Notification for reload");
					goto IL_1A1;
				}
				ExTraceGlobals.NotificationsCallTracer.TraceDebug((long)this.GetHashCode(), "ExchangeServiceMessageSubscription.HandleNotification: Unknown notification event type");
			}
			IL_1A1:
			if (messageNotification != null)
			{
				this.Callback(messageNotification);
				ExTraceGlobals.NotificationsCallTracer.TraceDebug((long)this.GetHashCode(), "ExchangeServiceMessageSubscription.HandleNotification: Returned from callback");
			}
		}

		// Token: 0x06001479 RID: 5241 RVA: 0x00065F3F File Offset: 0x0006413F
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

		// Token: 0x0600147A RID: 5242 RVA: 0x00065F75 File Offset: 0x00064175
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<ExchangeServiceMessageSubscription>(this);
		}

		// Token: 0x0600147B RID: 5243 RVA: 0x00065F7D File Offset: 0x0006417D
		protected static bool IsPropertyDefined(QueryNotification notification, int index)
		{
			return index >= 0 && index < notification.Row.Length && notification.Row[index] != null && !(notification.Row[index] is PropertyError);
		}

		// Token: 0x0600147C RID: 5244 RVA: 0x00065FB0 File Offset: 0x000641B0
		internal static SingleRecipientType CreateRecipientFromParticipant(Participant participant)
		{
			return new SingleRecipientType
			{
				Mailbox = new EmailAddressWrapper(),
				Mailbox = 
				{
					Name = participant.DisplayName,
					EmailAddress = participant.EmailAddress,
					RoutingType = participant.RoutingType,
					MailboxType = MailboxHelper.GetMailboxType(participant.Origin, participant.RoutingType).ToString()
				}
			};
		}

		// Token: 0x0600147D RID: 5245 RVA: 0x00066028 File Offset: 0x00064228
		protected static T GetItemProperty<T>(QueryNotification notification, int index)
		{
			return ExchangeServiceMessageSubscription.GetItemProperty<T>(notification, index, default(T));
		}

		// Token: 0x0600147E RID: 5246 RVA: 0x00066048 File Offset: 0x00064248
		protected static T GetItemProperty<T>(QueryNotification notification, int index, T defaultValue)
		{
			if (!ExchangeServiceMessageSubscription.IsPropertyDefined(notification, index))
			{
				return defaultValue;
			}
			object obj = notification.Row[index];
			if (!(obj is T))
			{
				return defaultValue;
			}
			return (T)((object)obj);
		}

		// Token: 0x0600147F RID: 5247 RVA: 0x0006607C File Offset: 0x0006427C
		protected string GetDateTimeProperty(QueryNotification notification, int index, ExTimeZone timeZone = null)
		{
			ExDateTime itemProperty = ExchangeServiceMessageSubscription.GetItemProperty<ExDateTime>(notification, index, ExDateTime.MinValue);
			if (ExDateTime.MinValue.Equals(itemProperty))
			{
				return null;
			}
			ExTimeZone exTimeZone = (timeZone == null || timeZone == ExTimeZone.UnspecifiedTimeZone) ? itemProperty.TimeZone : timeZone;
			if (exTimeZone == ExTimeZone.UtcTimeZone)
			{
				return ExDateTimeConverter.ToUtcXsdDateTime(itemProperty);
			}
			return ExDateTimeConverter.ToOffsetXsdDateTime(itemProperty, exTimeZone);
		}

		// Token: 0x06001480 RID: 5248 RVA: 0x000660D4 File Offset: 0x000642D4
		private ItemId StoreIdToEwsItemId(StoreId storeId)
		{
			ConcatenatedIdAndChangeKey concatenatedId = IdConverter.GetConcatenatedId(storeId, this.mailboxId.Member, null);
			return new ItemId(concatenatedId.Id, concatenatedId.ChangeKey);
		}

		// Token: 0x06001481 RID: 5249 RVA: 0x00066107 File Offset: 0x00064307
		protected string GetEwsId(StoreId storeId)
		{
			if (storeId == null)
			{
				return null;
			}
			return StoreId.StoreIdToEwsId(this.MailboxGuid, storeId);
		}

		// Token: 0x06001482 RID: 5250 RVA: 0x0006611C File Offset: 0x0006431C
		private MessageType GetMessageFromNotification(QueryNotification notification, PropertyDefinition[] propertyList)
		{
			StoreId storeId = null;
			StoreObjectId storeObjectId = null;
			if (ExchangeServiceMessageSubscription.IsPropertyDefined(notification, Array.IndexOf<PropertyDefinition>(propertyList, ItemSchema.Id)))
			{
				storeId = ExchangeServiceMessageSubscription.GetItemProperty<StoreId>(notification, Array.IndexOf<PropertyDefinition>(propertyList, ItemSchema.Id));
				if (storeId != null)
				{
					storeObjectId = StoreId.GetStoreObjectId(storeId);
				}
			}
			MessageType messageType = (storeObjectId != null) ? MessageType.CreateFromStoreObjectType(storeObjectId.ObjectType) : new MessageType();
			if (storeId != null)
			{
				messageType.ItemId = this.StoreIdToEwsItemId(storeId);
			}
			messageType.InstanceKey = notification.Index;
			if (ExchangeServiceMessageSubscription.IsPropertyDefined(notification, Array.IndexOf<PropertyDefinition>(propertyList, StoreObjectSchema.ParentItemId)))
			{
				messageType.ParentFolderId = new FolderId(this.GetEwsId(ExchangeServiceMessageSubscription.GetItemProperty<StoreId>(notification, Array.IndexOf<PropertyDefinition>(propertyList, StoreObjectSchema.ParentItemId))), null);
			}
			if (ExchangeServiceMessageSubscription.IsPropertyDefined(notification, Array.IndexOf<PropertyDefinition>(propertyList, ItemSchema.ConversationId)))
			{
				messageType.ConversationId = new ItemId(IdConverter.ConversationIdToEwsId(this.MailboxGuid, ExchangeServiceMessageSubscription.GetItemProperty<ConversationId>(notification, Array.IndexOf<PropertyDefinition>(propertyList, ItemSchema.ConversationId))), null);
			}
			if (ExchangeServiceMessageSubscription.IsPropertyDefined(notification, Array.IndexOf<PropertyDefinition>(propertyList, ItemSchema.Subject)))
			{
				messageType.Subject = ExchangeServiceMessageSubscription.GetItemProperty<string>(notification, Array.IndexOf<PropertyDefinition>(propertyList, ItemSchema.Subject));
			}
			if (ExchangeServiceMessageSubscription.IsPropertyDefined(notification, Array.IndexOf<PropertyDefinition>(propertyList, ItemSchema.Importance)))
			{
				messageType.ImportanceString = ExchangeServiceMessageSubscription.GetItemProperty<Importance>(notification, Array.IndexOf<PropertyDefinition>(propertyList, ItemSchema.Importance), Importance.Normal).ToString();
			}
			if (ExchangeServiceMessageSubscription.IsPropertyDefined(notification, Array.IndexOf<PropertyDefinition>(propertyList, ItemSchema.Sensitivity)))
			{
				messageType.SensitivityString = ExchangeServiceMessageSubscription.GetItemProperty<Sensitivity>(notification, Array.IndexOf<PropertyDefinition>(propertyList, ItemSchema.Sensitivity), Sensitivity.Normal).ToString();
			}
			if (ExchangeServiceMessageSubscription.IsPropertyDefined(notification, Array.IndexOf<PropertyDefinition>(propertyList, ItemSchema.ReceivedTime)))
			{
				messageType.DateTimeReceived = this.GetDateTimeProperty(notification, Array.IndexOf<PropertyDefinition>(propertyList, ItemSchema.ReceivedTime), null);
			}
			if (ExchangeServiceMessageSubscription.IsPropertyDefined(notification, Array.IndexOf<PropertyDefinition>(propertyList, ItemSchema.HasAttachment)))
			{
				messageType.HasAttachments = new bool?(ExchangeServiceMessageSubscription.GetItemProperty<bool>(notification, Array.IndexOf<PropertyDefinition>(propertyList, ItemSchema.HasAttachment)));
			}
			if (ExchangeServiceMessageSubscription.IsPropertyDefined(notification, Array.IndexOf<PropertyDefinition>(propertyList, MessageItemSchema.IsDraft)))
			{
				messageType.IsDraft = new bool?(ExchangeServiceMessageSubscription.GetItemProperty<bool>(notification, Array.IndexOf<PropertyDefinition>(propertyList, MessageItemSchema.IsDraft)));
			}
			if (ExchangeServiceMessageSubscription.IsPropertyDefined(notification, Array.IndexOf<PropertyDefinition>(propertyList, StoreObjectSchema.ItemClass)))
			{
				messageType.ItemClass = ExchangeServiceMessageSubscription.GetItemProperty<string>(notification, Array.IndexOf<PropertyDefinition>(propertyList, StoreObjectSchema.ItemClass));
			}
			if (ExchangeServiceMessageSubscription.IsPropertyDefined(notification, Array.IndexOf<PropertyDefinition>(propertyList, ItemSchema.From)))
			{
				messageType.From = ExchangeServiceMessageSubscription.CreateRecipientFromParticipant((Participant)notification.Row[Array.IndexOf<PropertyDefinition>(propertyList, ItemSchema.From)]);
			}
			if (ExchangeServiceMessageSubscription.IsPropertyDefined(notification, Array.IndexOf<PropertyDefinition>(propertyList, ItemSchema.Sender)))
			{
				messageType.Sender = ExchangeServiceMessageSubscription.CreateRecipientFromParticipant((Participant)notification.Row[Array.IndexOf<PropertyDefinition>(propertyList, ItemSchema.Sender)]);
			}
			if (ExchangeServiceMessageSubscription.IsPropertyDefined(notification, Array.IndexOf<PropertyDefinition>(propertyList, MessageItemSchema.IsRead)))
			{
				messageType.IsRead = new bool?(ExchangeServiceMessageSubscription.GetItemProperty<bool>(notification, Array.IndexOf<PropertyDefinition>(propertyList, MessageItemSchema.IsRead)));
			}
			FlagType flagType = new FlagType();
			if (ExchangeServiceMessageSubscription.IsPropertyDefined(notification, Array.IndexOf<PropertyDefinition>(propertyList, ItemSchema.CompleteDate)))
			{
				flagType.CompleteDate = this.GetDateTimeProperty(notification, Array.IndexOf<PropertyDefinition>(propertyList, ItemSchema.CompleteDate), null);
			}
			if (ExchangeServiceMessageSubscription.IsPropertyDefined(notification, Array.IndexOf<PropertyDefinition>(propertyList, ItemSchema.FlagStatus)))
			{
				flagType.FlagStatus = ExchangeServiceMessageSubscription.GetItemProperty<FlagStatus>(notification, Array.IndexOf<PropertyDefinition>(propertyList, ItemSchema.FlagStatus), FlagStatus.NotFlagged);
			}
			if (ExchangeServiceMessageSubscription.IsPropertyDefined(notification, Array.IndexOf<PropertyDefinition>(propertyList, TaskSchema.StartDate)))
			{
				flagType.StartDate = this.GetDateTimeProperty(notification, Array.IndexOf<PropertyDefinition>(propertyList, TaskSchema.StartDate), null);
			}
			if (ExchangeServiceMessageSubscription.IsPropertyDefined(notification, Array.IndexOf<PropertyDefinition>(propertyList, TaskSchema.DueDate)))
			{
				flagType.DueDate = this.GetDateTimeProperty(notification, Array.IndexOf<PropertyDefinition>(propertyList, TaskSchema.DueDate), null);
			}
			messageType.Flag = flagType;
			if (ExchangeServiceMessageSubscription.IsPropertyDefined(notification, Array.IndexOf<PropertyDefinition>(propertyList, StoreObjectSchema.CreationTime)))
			{
				messageType.DateTimeCreated = this.GetDateTimeProperty(notification, Array.IndexOf<PropertyDefinition>(propertyList, StoreObjectSchema.CreationTime), null);
			}
			if (ExchangeServiceMessageSubscription.IsPropertyDefined(notification, Array.IndexOf<PropertyDefinition>(propertyList, StoreObjectSchema.LastModifiedTime)))
			{
				messageType.LastModifiedTime = this.GetDateTimeProperty(notification, Array.IndexOf<PropertyDefinition>(propertyList, StoreObjectSchema.LastModifiedTime), null);
			}
			if (ExchangeServiceMessageSubscription.IsPropertyDefined(notification, Array.IndexOf<PropertyDefinition>(propertyList, ItemSchema.ReceivedOrRenewTime)))
			{
				messageType.ReceivedOrRenewTime = this.GetDateTimeProperty(notification, Array.IndexOf<PropertyDefinition>(propertyList, ItemSchema.ReceivedOrRenewTime), null);
			}
			if (ExchangeServiceMessageSubscription.IsPropertyDefined(notification, Array.IndexOf<PropertyDefinition>(propertyList, ItemSchema.Categories)))
			{
				messageType.Categories = ExchangeServiceMessageSubscription.GetItemProperty<string[]>(notification, Array.IndexOf<PropertyDefinition>(propertyList, ItemSchema.Categories));
			}
			if (ExchangeServiceMessageSubscription.IsPropertyDefined(notification, Array.IndexOf<PropertyDefinition>(propertyList, ItemSchema.Preview)))
			{
				messageType.Preview = ExchangeServiceMessageSubscription.GetItemProperty<string>(notification, Array.IndexOf<PropertyDefinition>(propertyList, ItemSchema.Preview), null);
			}
			if (ExchangeServiceMessageSubscription.IsPropertyDefined(notification, Array.IndexOf<PropertyDefinition>(propertyList, ItemSchema.Size)))
			{
				messageType.Size = ExchangeServiceMessageSubscription.GetItemProperty<int?>(notification, Array.IndexOf<PropertyDefinition>(propertyList, ItemSchema.Size), null);
			}
			if (ExchangeServiceMessageSubscription.IsPropertyDefined(notification, Array.IndexOf<PropertyDefinition>(propertyList, ItemSchema.NormalizedSubject)))
			{
				messageType.AddExtendedPropertyValue(new ExtendedPropertyType(ExchangeServiceMessageSubscription.normalizedSubject, ExchangeServiceMessageSubscription.GetItemProperty<string>(notification, Array.IndexOf<PropertyDefinition>(propertyList, ItemSchema.NormalizedSubject), null)));
			}
			if (ExchangeServiceMessageSubscription.IsPropertyDefined(notification, Array.IndexOf<PropertyDefinition>(propertyList, ItemSchema.DisplayTo)))
			{
				messageType.DisplayTo = ExchangeServiceMessageSubscription.GetItemProperty<string>(notification, Array.IndexOf<PropertyDefinition>(propertyList, ItemSchema.DisplayTo), null);
			}
			if (ExchangeServiceMessageSubscription.IsPropertyDefined(notification, Array.IndexOf<PropertyDefinition>(propertyList, ItemSchema.IconIndex)))
			{
				IconIndex itemProperty = (IconIndex)ExchangeServiceMessageSubscription.GetItemProperty<int>(notification, Array.IndexOf<PropertyDefinition>(propertyList, ItemSchema.IconIndex));
				if (itemProperty > (IconIndex)0)
				{
					messageType.IconIndexString = itemProperty.ToString();
				}
			}
			if (ExchangeServiceMessageSubscription.IsPropertyDefined(notification, Array.IndexOf<PropertyDefinition>(propertyList, ItemSchema.SentTime)))
			{
				messageType.DateTimeSent = this.GetDateTimeProperty(notification, Array.IndexOf<PropertyDefinition>(propertyList, ItemSchema.SentTime), null);
			}
			if (ExchangeServiceMessageSubscription.IsPropertyDefined(notification, Array.IndexOf<PropertyDefinition>(propertyList, MessageItemSchema.LastVerbExecuted)))
			{
				messageType.AddExtendedPropertyValue(new ExtendedPropertyType(ExchangeServiceMessageSubscription.lastVerbExecuted, ExchangeServiceMessageSubscription.GetItemProperty<int>(notification, Array.IndexOf<PropertyDefinition>(propertyList, MessageItemSchema.LastVerbExecuted)).ToString()));
			}
			if (ExchangeServiceMessageSubscription.IsPropertyDefined(notification, Array.IndexOf<PropertyDefinition>(propertyList, MessageItemSchema.LastVerbExecutionTime)))
			{
				messageType.AddExtendedPropertyValue(new ExtendedPropertyType(ExchangeServiceMessageSubscription.lastVerbExecutionTime, this.GetDateTimeProperty(notification, Array.IndexOf<PropertyDefinition>(propertyList, MessageItemSchema.LastVerbExecutionTime), null)));
			}
			ExTraceGlobals.NotificationsCallTracer.TraceDebug<string>((long)this.GetHashCode(), "[MessageItemExchangeServiceMessageSubscription.GetMessageItemFromNotification] End. SubscriptionId: {0}", base.SubscriptionId);
			return messageType;
		}

		// Token: 0x04000D99 RID: 3481
		private readonly LazyMember<MailboxId> mailboxId;

		// Token: 0x04000D9A RID: 3482
		private static ExtendedPropertyUri lastVerbExecuted = new ExtendedPropertyUri
		{
			PropertyTag = "0x1081",
			PropertyType = MapiPropertyType.Integer
		};

		// Token: 0x04000D9B RID: 3483
		private static ExtendedPropertyUri lastVerbExecutionTime = new ExtendedPropertyUri
		{
			PropertyTag = "0x1082",
			PropertyType = MapiPropertyType.SystemTime
		};

		// Token: 0x04000D9C RID: 3484
		private static ExtendedPropertyUri normalizedSubject = new ExtendedPropertyUri
		{
			PropertyTag = "0xe1d",
			PropertyType = MapiPropertyType.String
		};
	}
}
