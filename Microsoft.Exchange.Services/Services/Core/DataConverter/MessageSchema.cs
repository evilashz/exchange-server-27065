using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x020001B0 RID: 432
	internal sealed class MessageSchema : Schema
	{
		// Token: 0x06000BBD RID: 3005 RVA: 0x0003B46C File Offset: 0x0003966C
		static MessageSchema()
		{
			XmlElementInformation[] xmlElements = new XmlElementInformation[]
			{
				MessageSchema.Sender,
				MessageSchema.ToRecipients,
				MessageSchema.CcRecipients,
				MessageSchema.BccRecipients,
				MessageSchema.IsReadReceiptRequested,
				MessageSchema.IsDeliveryReceiptRequested,
				MessageSchema.RelyOnConversationIndex,
				MessageSchema.IsSpecificMessageReplyStamped,
				MessageSchema.IsSpecificMessageReply,
				MessageSchema.ConversationIndex,
				MessageSchema.ConversationTopic,
				MessageSchema.From,
				MessageSchema.InternetMessageId,
				MessageSchema.IsRead,
				MessageSchema.IsResponseRequested,
				MessageSchema.References,
				MessageSchema.ReplyTo,
				MessageSchema.ReceivedBy,
				MessageSchema.ReceivedRepresenting,
				MessageSchema.ApprovalRequestData,
				MessageSchema.VotingInformation,
				MessageSchema.ReminderMessageData,
				MessageSchema.ModernReminders,
				MessageSchema.LikeCount,
				MessageSchema.RecipientCounts,
				MessageSchema.Likers,
				MessageSchema.IsGroupEscalationMessage
			};
			MessageSchema.schema = new MessageSchema(xmlElements);
		}

		// Token: 0x06000BBE RID: 3006 RVA: 0x0003BBEC File Offset: 0x00039DEC
		private MessageSchema(XmlElementInformation[] xmlElements) : base(xmlElements)
		{
			IList<PropertyInformation> propertyInformationListByShapeEnum = base.GetPropertyInformationListByShapeEnum(ShapeEnum.AllProperties);
			propertyInformationListByShapeEnum.Remove(MessageSchema.ApprovalRequestData);
			propertyInformationListByShapeEnum.Remove(MessageSchema.VotingInformation);
			propertyInformationListByShapeEnum.Remove(MessageSchema.ReminderMessageData);
			propertyInformationListByShapeEnum.Remove(MessageSchema.ModernReminders);
			propertyInformationListByShapeEnum.Remove(MessageSchema.RecipientCounts);
		}

		// Token: 0x06000BBF RID: 3007 RVA: 0x0003BC44 File Offset: 0x00039E44
		public static Schema GetSchema()
		{
			return MessageSchema.schema;
		}

		// Token: 0x04000940 RID: 2368
		private static Schema schema;

		// Token: 0x04000941 RID: 2369
		public static readonly PropertyInformation BccRecipients = new PropertyInformation("BccRecipients", ExchangeVersion.Exchange2007, null, new PropertyUri(PropertyUriEnum.BccRecipients), new PropertyCommand.CreatePropertyCommand(BccRecipientsProperty.CreateCommand));

		// Token: 0x04000942 RID: 2370
		public static readonly PropertyInformation CcRecipients = new PropertyInformation("CcRecipients", ExchangeVersion.Exchange2007, null, new PropertyUri(PropertyUriEnum.CcRecipients), new PropertyCommand.CreatePropertyCommand(CcRecipientsProperty.CreateCommand));

		// Token: 0x04000943 RID: 2371
		public static readonly PropertyInformation ConversationIndex = new PropertyInformation("ConversationIndex", ExchangeVersion.Exchange2007, ItemSchema.ConversationIndex, new PropertyUri(PropertyUriEnum.ConversationIndex), new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x04000944 RID: 2372
		public static readonly PropertyInformation ConversationTopic = new PropertyInformation("ConversationTopic", ExchangeVersion.Exchange2007, ItemSchema.ConversationTopic, new PropertyUri(PropertyUriEnum.ConversationTopic), new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x04000945 RID: 2373
		public static readonly PropertyInformation From = new PropertyInformation("From", ServiceXml.GetFullyQualifiedName("From"), ServiceXml.DefaultNamespaceUri, ExchangeVersion.Exchange2007, new PropertyDefinition[]
		{
			ItemSchema.SentRepresentingDisplayName,
			ItemSchema.SentRepresentingType,
			ItemSchema.SentRepresentingEmailAddress,
			ItemSchema.From,
			MessageItemSchema.IsDraft,
			MessageItemSchema.SharingInstanceGuid
		}, new PropertyUri(PropertyUriEnum.From), new PropertyCommand.CreatePropertyCommand(FromProperty.CreateCommand));

		// Token: 0x04000946 RID: 2374
		public static readonly PropertyInformation InternetMessageId = new PropertyInformation("InternetMessageId", ExchangeVersion.Exchange2007, ItemSchema.InternetMessageId, new PropertyUri(PropertyUriEnum.InternetMessageId), new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsSetCommand | PropertyInformationAttributes.ImplementsToXmlCommand | PropertyInformationAttributes.ImplementsToXmlForPropertyBagCommand | PropertyInformationAttributes.ImplementsToServiceObjectCommand | PropertyInformationAttributes.ImplementsToServiceObjectForPropertyBagCommand);

		// Token: 0x04000947 RID: 2375
		public static readonly PropertyInformation IsDeliveryReceiptRequested = new PropertyInformation("IsDeliveryReceiptRequested", ExchangeVersion.Exchange2007, MessageItemSchema.IsDeliveryReceiptRequested, new PropertyUri(PropertyUriEnum.IsDeliveryReceiptRequested), new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateIsDeliveryReceiptRequestedCommand));

		// Token: 0x04000948 RID: 2376
		public static readonly PropertyInformation IsRead = new PropertyInformation("IsRead", ExchangeVersion.Exchange2007, MessageItemSchema.IsRead, new PropertyUri(PropertyUriEnum.IsRead), new PropertyCommand.CreatePropertyCommand(IsReadProperty.CreateCommand), PropertyInformationAttributes.ImplementsSetCommand | PropertyInformationAttributes.ImplementsToXmlCommand | PropertyInformationAttributes.ImplementsSetUpdateCommand | PropertyInformationAttributes.ImplementsToXmlForPropertyBagCommand | PropertyInformationAttributes.ImplementsToServiceObjectCommand | PropertyInformationAttributes.ImplementsToServiceObjectForPropertyBagCommand);

		// Token: 0x04000949 RID: 2377
		public static readonly PropertyInformation IsReadReceiptRequested = new PropertyInformation("IsReadReceiptRequested", ExchangeVersion.Exchange2007, MessageItemSchema.IsReadReceiptRequested, new PropertyUri(PropertyUriEnum.IsReadReceiptRequested), new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateIsReadReceiptRequestedCommand));

		// Token: 0x0400094A RID: 2378
		public static readonly PropertyInformation IsResponseRequested = new PropertyInformation("IsResponseRequested", ExchangeVersion.Exchange2007, ItemSchema.IsResponseRequested, new PropertyUri(PropertyUriEnum.IsResponseRequested), new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand));

		// Token: 0x0400094B RID: 2379
		public static readonly PropertyInformation ReceivedBy = new PropertyInformation("ReceivedBy", ServiceXml.GetFullyQualifiedName("ReceivedBy"), ServiceXml.DefaultNamespaceUri, ExchangeVersion.Exchange2007SP1, new PropertyDefinition[]
		{
			MessageItemSchema.ReceivedByName,
			MessageItemSchema.ReceivedByAddrType,
			MessageItemSchema.ReceivedByEmailAddress,
			MessageItemSchema.ReceivedBy
		}, new PropertyUri(PropertyUriEnum.ReceivedBy), new PropertyCommand.CreatePropertyCommand(ParticipantProperty.CreateCommandForReceivedBy), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x0400094C RID: 2380
		public static readonly PropertyInformation ReceivedRepresenting = new PropertyInformation("ReceivedRepresenting", ServiceXml.GetFullyQualifiedName("ReceivedRepresenting"), ServiceXml.DefaultNamespaceUri, ExchangeVersion.Exchange2007SP1, new PropertyDefinition[]
		{
			MessageItemSchema.ReceivedRepresentingDisplayName,
			MessageItemSchema.ReceivedRepresentingAddressType,
			MessageItemSchema.ReceivedRepresentingEmailAddress,
			MessageItemSchema.ReceivedRepresenting
		}, new PropertyUri(PropertyUriEnum.ReceivedRepresenting), new PropertyCommand.CreatePropertyCommand(ParticipantProperty.CreateCommandForReceivedRepresenting), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x0400094D RID: 2381
		public static readonly PropertyInformation References = new PropertyInformation("References", ExchangeVersion.Exchange2007, ItemSchema.InternetReferences, new PropertyUri(PropertyUriEnum.References), new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand));

		// Token: 0x0400094E RID: 2382
		public static readonly PropertyInformation ReplyTo = new PropertyInformation("ReplyTo", ExchangeVersion.Exchange2007, MessageItemSchema.ReplyToNames, new PropertyUri(PropertyUriEnum.ReplyTo), new PropertyCommand.CreatePropertyCommand(ReplyToProperty.CreateCommand));

		// Token: 0x0400094F RID: 2383
		public static readonly PropertyInformation Sender = new PropertyInformation("Sender", ServiceXml.GetFullyQualifiedName("Sender"), ServiceXml.DefaultNamespaceUri, ExchangeVersion.Exchange2007, new PropertyDefinition[]
		{
			MessageItemSchema.SenderDisplayName,
			MessageItemSchema.SenderAddressType,
			MessageItemSchema.SenderEmailAddress,
			ItemSchema.Sender
		}, new PropertyUri(PropertyUriEnum.Sender), new PropertyCommand.CreatePropertyCommand(SenderProperty.CreateCommand));

		// Token: 0x04000950 RID: 2384
		public static readonly PropertyInformation ToRecipients = new PropertyInformation("ToRecipients", ExchangeVersion.Exchange2007, null, new PropertyUri(PropertyUriEnum.ToRecipients), new PropertyCommand.CreatePropertyCommand(ToRecipientsProperty.CreateCommand));

		// Token: 0x04000951 RID: 2385
		public static readonly PropertyInformation ApprovalRequestData = new PropertyInformation(PropertyUriEnum.ApprovalRequestData.ToString(), ServiceXml.GetFullyQualifiedName(PropertyUriEnum.ApprovalRequestData.ToString()), ServiceXml.DefaultNamespaceUri, ExchangeVersion.Exchange2012, new PropertyDefinition[]
		{
			MessageItemSchema.ApprovalDecision,
			MessageItemSchema.ApprovalDecisionMaker,
			MessageItemSchema.ApprovalDecisionTime
		}, new PropertyUri(PropertyUriEnum.ApprovalRequestData), new PropertyCommand.CreatePropertyCommand(ApprovalRequestDataProperty.CreateCommand), PropertyInformationAttributes.ImplementsToServiceObjectCommand);

		// Token: 0x04000952 RID: 2386
		public static readonly PropertyInformation ReminderMessageData = new PropertyInformation(PropertyUriEnum.ReminderMessageData.ToString(), ServiceXml.GetFullyQualifiedName(PropertyUriEnum.ReminderMessageData.ToString()), ServiceXml.DefaultNamespaceUri, ExchangeVersion.Exchange2013, new PropertyDefinition[]
		{
			ReminderMessageSchema.ReminderText,
			CalendarItemBaseSchema.Location,
			ReminderMessageSchema.ReminderStartTime,
			ReminderMessageSchema.ReminderEndTime,
			ReminderMessageSchema.ReminderItemGlobalObjectId,
			ReminderMessageSchema.ReminderOccurrenceGlobalObjectId
		}, new PropertyUri(PropertyUriEnum.ReminderMessageData), new PropertyCommand.CreatePropertyCommand(ReminderMessageDataProperty.CreateCommand), PropertyInformationAttributes.ImplementsToServiceObjectCommand);

		// Token: 0x04000953 RID: 2387
		public static readonly PropertyInformation VotingInformation = new PropertyInformation(PropertyUriEnum.VotingInformation.ToString(), ExchangeVersion.Exchange2012, MessageItemSchema.VotingBlob, new PropertyUri(PropertyUriEnum.VotingInformation), new PropertyCommand.CreatePropertyCommand(VotingInformationProperty.CreateCommand), PropertyInformationAttributes.ImplementsToServiceObjectCommand);

		// Token: 0x04000954 RID: 2388
		public static readonly PropertyInformation RelyOnConversationIndex = new PropertyInformation("RelyOnConversationIndex", ExchangeVersion.Exchange2013, MessageItemSchema.RelyOnConversationIndex, new PropertyUri(PropertyUriEnum.RelyOnConversationIndex), new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand));

		// Token: 0x04000955 RID: 2389
		public static readonly PropertyInformation IsSpecificMessageReplyStamped = new PropertyInformation("IsSpecificMessageReplyStamped", ExchangeVersion.Exchange2013, MessageItemSchema.IsSpecificMessageReplyStamped, new PropertyUri(PropertyUriEnum.IsSpecificMessageReplyStamped), new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand));

		// Token: 0x04000956 RID: 2390
		public static readonly PropertyInformation IsSpecificMessageReply = new PropertyInformation("IsSpecificMessageReply", ExchangeVersion.Exchange2013, MessageItemSchema.IsSpecificMessageReply, new PropertyUri(PropertyUriEnum.IsSpecificMessageReply), new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand));

		// Token: 0x04000957 RID: 2391
		public static readonly PropertyInformation ModernReminders = new PropertyInformation("ModernReminders", ExchangeVersion.Exchange2013, MessageItemSchema.QuickCaptureReminders, new PropertyUri(PropertyUriEnum.ModernReminders), new PropertyCommand.CreatePropertyCommand(ModernRemindersMessageProperty.CreateCommand), PropertyInformationAttributes.ImplementsSetUpdateCommand);

		// Token: 0x04000958 RID: 2392
		public static readonly PropertyInformation LikeCount = new PropertyInformation("LikeCount", ExchangeVersion.Exchange2013, MessageItemSchema.LikeCount, new PropertyUri(PropertyUriEnum.LikeCount), new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand));

		// Token: 0x04000959 RID: 2393
		public static readonly PropertyInformation Likers = new PropertyInformation("Likers", ExchangeVersion.Exchange2013, MessageItemSchema.LikersBlob, new PropertyUri(PropertyUriEnum.Likers), new PropertyCommand.CreatePropertyCommand(LikersProperty.CreateCommand));

		// Token: 0x0400095A RID: 2394
		public static readonly PropertyInformation RecipientCounts = new PropertyInformation("RecipientCounts", ExchangeVersion.Exchange2013, null, new PropertyUri(PropertyUriEnum.RecipientCounts), new PropertyCommand.CreatePropertyCommand(RecipientCountsProperty.CreateCommand), PropertyInformationAttributes.ImplementsToServiceObjectCommand);

		// Token: 0x0400095B RID: 2395
		public static readonly PropertyInformation IsGroupEscalationMessage = new PropertyInformation("IsGroupEscalationMessage", ExchangeVersion.Exchange2013, MessageItemSchema.IsGroupEscalationMessage, new PropertyUri(PropertyUriEnum.IsGroupEscalationMessage), new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);
	}
}
