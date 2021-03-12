using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x020001C0 RID: 448
	internal sealed class ConversationSchema : Schema
	{
		// Token: 0x06000C4E RID: 3150 RVA: 0x0003DC68 File Offset: 0x0003BE68
		static ConversationSchema()
		{
			XmlElementInformation[] xmlElements = new XmlElementInformation[]
			{
				ConversationSchema.ConversationId,
				ConversationSchema.ConversationTopic,
				ConversationSchema.UniqueRecipients,
				ConversationSchema.GlobalUniqueRecipients,
				ConversationSchema.UniqueUnreadSenders,
				ConversationSchema.GlobalUniqueUnreadSenders,
				ConversationSchema.UniqueSenders,
				ConversationSchema.GlobalUniqueSenders,
				ConversationSchema.LastDeliveryTime,
				ConversationSchema.GlobalLastDeliveryTime,
				ConversationSchema.Categories,
				ConversationSchema.GlobalCategories,
				ConversationSchema.FlagStatus,
				ConversationSchema.GlobalFlagStatus,
				ConversationSchema.HasAttachments,
				ConversationSchema.GlobalHasAttachments,
				ConversationSchema.HasIrm,
				ConversationSchema.GlobalHasIrm,
				ConversationSchema.MessageCount,
				ConversationSchema.GlobalMessageCount,
				ConversationSchema.UnreadCount,
				ConversationSchema.GlobalUnreadCount,
				ConversationSchema.Size,
				ConversationSchema.GlobalSize,
				ConversationSchema.ItemClasses,
				ConversationSchema.GlobalItemClasses,
				ConversationSchema.Importance,
				ConversationSchema.GlobalImportance,
				ConversationSchema.ItemIds,
				ConversationSchema.GlobalItemIds,
				ConversationSchema.LastModifiedTime,
				ConversationSchema.InstanceKey,
				ConversationSchema.Preview,
				ConversationSchema.IconIndex,
				ConversationSchema.GlobalIconIndex,
				ConversationSchema.DraftItemIds,
				ConversationSchema.HasClutter,
				ConversationSchema.InitialPost,
				ConversationSchema.RecentReplys,
				ConversationSchema.FamilyId,
				ConversationSchema.GlobalLastDeliveryOrRenewTime,
				ConversationSchema.GlobalRichContent,
				ConversationSchema.MailboxGuid,
				ConversationSchema.LastDeliveryOrRenewTime,
				ConversationSchema.WorkingSetSourcePartition
			};
			ConversationSchema.schema = new ConversationSchema(xmlElements, ConversationSchema.ConversationId);
		}

		// Token: 0x06000C4F RID: 3151 RVA: 0x0003E773 File Offset: 0x0003C973
		private ConversationSchema(XmlElementInformation[] xmlElements, PropertyInformation conversationIdPropertyInformation) : base(xmlElements, conversationIdPropertyInformation)
		{
		}

		// Token: 0x06000C50 RID: 3152 RVA: 0x0003E77D File Offset: 0x0003C97D
		public static Schema GetSchema()
		{
			return ConversationSchema.schema;
		}

		// Token: 0x0400097E RID: 2430
		private static Schema schema;

		// Token: 0x0400097F RID: 2431
		public static readonly PropertyInformation ConversationId = new PropertyInformation("ConversationId", ExchangeVersion.Exchange2010SP1, ConversationItemSchema.ConversationId, new PropertyUri(PropertyUriEnum.ConversationGuidId), new PropertyCommand.CreatePropertyCommand(ConversationIdProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x04000980 RID: 2432
		public static readonly PropertyInformation ConversationTopic = new PropertyInformation("ConversationTopic", ExchangeVersion.Exchange2010SP1, ConversationItemSchema.ConversationTopic, new PropertyUri(PropertyUriEnum.Topic), new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x04000981 RID: 2433
		public static readonly PropertyInformation UniqueRecipients = new ArrayPropertyInformation("UniqueRecipients", ExchangeVersion.Exchange2010SP1, "String", ConversationItemSchema.ConversationMVTo, new PropertyUri(PropertyUriEnum.ConversationUniqueRecipients), new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand));

		// Token: 0x04000982 RID: 2434
		public static readonly PropertyInformation GlobalUniqueRecipients = new ArrayPropertyInformation("GlobalUniqueRecipients", ExchangeVersion.Exchange2010SP1, "String", ConversationItemSchema.ConversationGlobalMVTo, new PropertyUri(PropertyUriEnum.ConversationGlobalUniqueRecipients), new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand));

		// Token: 0x04000983 RID: 2435
		public static readonly PropertyInformation UniqueUnreadSenders = new ArrayPropertyInformation("UniqueUnreadSenders", ExchangeVersion.Exchange2010SP1, "String", ConversationItemSchema.ConversationMVUnreadFrom, new PropertyUri(PropertyUriEnum.ConversationUniqueUnreadSenders), new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand));

		// Token: 0x04000984 RID: 2436
		public static readonly PropertyInformation GlobalUniqueUnreadSenders = new ArrayPropertyInformation("GlobalUniqueUnreadSenders", ExchangeVersion.Exchange2010SP1, "String", ConversationItemSchema.ConversationGlobalMVUnreadFrom, new PropertyUri(PropertyUriEnum.ConversationGlobalUniqueUnreadSenders), new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand));

		// Token: 0x04000985 RID: 2437
		public static readonly PropertyInformation UniqueSenders = new ArrayPropertyInformation("UniqueSenders", ExchangeVersion.Exchange2010SP1, "String", ConversationItemSchema.ConversationMVFrom, new PropertyUri(PropertyUriEnum.ConversationUniqueSenders), new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand));

		// Token: 0x04000986 RID: 2438
		public static readonly PropertyInformation GlobalUniqueSenders = new ArrayPropertyInformation("GlobalUniqueSenders", ExchangeVersion.Exchange2010SP1, "String", ConversationItemSchema.ConversationGlobalMVFrom, new PropertyUri(PropertyUriEnum.ConversationGlobalUniqueSenders), new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand));

		// Token: 0x04000987 RID: 2439
		public static readonly PropertyInformation LastDeliveryTime = new PropertyInformation("LastDeliveryTime", ExchangeVersion.Exchange2010SP1, ConversationItemSchema.ConversationLastDeliveryTime, new PropertyUri(PropertyUriEnum.ConversationLastDeliveryTime), new PropertyCommand.CreatePropertyCommand(DateTimeProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x04000988 RID: 2440
		public static readonly PropertyInformation GlobalLastDeliveryTime = new PropertyInformation("GlobalLastDeliveryTime", ExchangeVersion.Exchange2010SP1, ConversationItemSchema.ConversationGlobalLastDeliveryTime, new PropertyUri(PropertyUriEnum.ConversationGlobalLastDeliveryTime), new PropertyCommand.CreatePropertyCommand(DateTimeProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x04000989 RID: 2441
		public static readonly PropertyInformation GlobalLastDeliveryOrRenewTime = new PropertyInformation("GlobalLastDeliveryOrRenewTime", ExchangeVersion.Exchange2012, ConversationItemSchema.ConversationGlobalLastDeliveryOrRenewTime, new PropertyUri(PropertyUriEnum.ConversationGlobalLastDeliveryOrRenewTime), new PropertyCommand.CreatePropertyCommand(DateTimeProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x0400098A RID: 2442
		public static readonly PropertyInformation GlobalRichContent = new ArrayPropertyInformation("GlobalRichContent", ExchangeVersion.Exchange2012, "RichContent", ConversationItemSchema.ConversationGlobalRichContent, new PropertyUri(PropertyUriEnum.ConversationGlobalRichContent), new PropertyCommand.CreatePropertyCommand(ShortArrayValueProperty.CreateCommand));

		// Token: 0x0400098B RID: 2443
		public static readonly PropertyInformation MailboxGuid = new PropertyInformation("MailboxGuid", ExchangeVersion.Exchange2012, ConversationItemSchema.MailboxGuid, new PropertyUri(PropertyUriEnum.ConversationMailboxGuid), new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x0400098C RID: 2444
		public static readonly PropertyInformation Categories = new ArrayPropertyInformation("Categories", ExchangeVersion.Exchange2010SP1, "String", ConversationItemSchema.ConversationCategories, new PropertyUri(PropertyUriEnum.ConversationCategories), new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand));

		// Token: 0x0400098D RID: 2445
		public static readonly PropertyInformation GlobalCategories = new ArrayPropertyInformation("GlobalCategories", ExchangeVersion.Exchange2010SP1, "String", ConversationItemSchema.ConversationGlobalCategories, new PropertyUri(PropertyUriEnum.ConversationGlobalCategories), new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand));

		// Token: 0x0400098E RID: 2446
		public static readonly PropertyInformation FlagStatus = new PropertyInformation("FlagStatus", ExchangeVersion.Exchange2010SP1, ConversationItemSchema.ConversationFlagStatus, new PropertyUri(PropertyUriEnum.ConversationFlagStatus), new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x0400098F RID: 2447
		public static readonly PropertyInformation GlobalFlagStatus = new PropertyInformation("GlobalFlagStatus", ExchangeVersion.Exchange2010SP1, ConversationItemSchema.ConversationGlobalFlagStatus, new PropertyUri(PropertyUriEnum.ConversationGlobalFlagStatus), new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x04000990 RID: 2448
		public static readonly PropertyInformation HasAttachments = new PropertyInformation("HasAttachments", ExchangeVersion.Exchange2010SP1, ConversationItemSchema.ConversationHasAttach, new PropertyUri(PropertyUriEnum.ConversationHasAttachments), new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x04000991 RID: 2449
		public static readonly PropertyInformation GlobalHasAttachments = new PropertyInformation("GlobalHasAttachments", ExchangeVersion.Exchange2010SP1, ConversationItemSchema.ConversationGlobalHasAttach, new PropertyUri(PropertyUriEnum.ConversationGlobalHasAttachments), new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x04000992 RID: 2450
		public static readonly PropertyInformation HasIrm = new PropertyInformation("HasIrm", ExchangeVersion.Exchange2012, ConversationItemSchema.ConversationHasIrm, new PropertyUri(PropertyUriEnum.ConversationHasIrm), new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x04000993 RID: 2451
		public static readonly PropertyInformation GlobalHasIrm = new PropertyInformation("GlobalHasIrm", ExchangeVersion.Exchange2012, ConversationItemSchema.ConversationGlobalHasIrm, new PropertyUri(PropertyUriEnum.ConversationGlobalHasIrm), new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x04000994 RID: 2452
		public static readonly PropertyInformation MessageCount = new PropertyInformation("MessageCount", ExchangeVersion.Exchange2010SP1, ConversationItemSchema.ConversationMessageCount, new PropertyUri(PropertyUriEnum.ConversationMessageCount), new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x04000995 RID: 2453
		public static readonly PropertyInformation GlobalMessageCount = new PropertyInformation("GlobalMessageCount", ExchangeVersion.Exchange2010SP1, ConversationItemSchema.ConversationGlobalMessageCount, new PropertyUri(PropertyUriEnum.ConversationGlobalMessageCount), new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x04000996 RID: 2454
		public static readonly PropertyInformation UnreadCount = new PropertyInformation("UnreadCount", ExchangeVersion.Exchange2010SP1, ConversationItemSchema.ConversationUnreadMessageCount, new PropertyUri(PropertyUriEnum.ConversationUnreadCount), new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommandForPropertyWithDefaultValue), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x04000997 RID: 2455
		public static readonly PropertyInformation GlobalUnreadCount = new PropertyInformation("GlobalUnreadCount", ExchangeVersion.Exchange2010SP1, ConversationItemSchema.ConversationGlobalUnreadMessageCount, new PropertyUri(PropertyUriEnum.ConversationGlobalUnreadCount), new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommandForPropertyWithDefaultValue), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x04000998 RID: 2456
		public static readonly PropertyInformation Size = new PropertyInformation("Size", ExchangeVersion.Exchange2010SP1, ConversationItemSchema.ConversationMessageSize, new PropertyUri(PropertyUriEnum.ConversationSize), new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x04000999 RID: 2457
		public static readonly PropertyInformation GlobalSize = new PropertyInformation("GlobalSize", ExchangeVersion.Exchange2010SP1, ConversationItemSchema.ConversationGlobalMessageSize, new PropertyUri(PropertyUriEnum.ConversationGlobalSize), new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x0400099A RID: 2458
		public static readonly PropertyInformation ItemClasses = new ArrayPropertyInformation("ItemClasses", ExchangeVersion.Exchange2010SP1, "ItemClass", ConversationItemSchema.ConversationMessageClasses, new PropertyUri(PropertyUriEnum.ConversationItemClasses), new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand));

		// Token: 0x0400099B RID: 2459
		public static readonly PropertyInformation GlobalItemClasses = new ArrayPropertyInformation("GlobalItemClasses", ExchangeVersion.Exchange2010SP1, "ItemClass", ConversationItemSchema.ConversationGlobalMessageClasses, new PropertyUri(PropertyUriEnum.ConversationGlobalItemClasses), new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand));

		// Token: 0x0400099C RID: 2460
		public static readonly PropertyInformation Importance = new PropertyInformation("Importance", ExchangeVersion.Exchange2010SP1, ConversationItemSchema.ConversationImportance, new PropertyUri(PropertyUriEnum.ConversationImportance), new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x0400099D RID: 2461
		public static readonly PropertyInformation GlobalImportance = new PropertyInformation("GlobalImportance", ExchangeVersion.Exchange2010SP1, ConversationItemSchema.ConversationGlobalImportance, new PropertyUri(PropertyUriEnum.ConversationGlobalImportance), new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x0400099E RID: 2462
		public static readonly PropertyInformation ItemIds = new ArrayPropertyInformation("ItemIds", ExchangeVersion.Exchange2010SP1, "ItemId", ConversationItemSchema.ConversationItemIds, new PropertyUri(PropertyUriEnum.ConversationItemIds), new PropertyCommand.CreatePropertyCommand(ItemIdProperty.CreateCommand));

		// Token: 0x0400099F RID: 2463
		public static readonly PropertyInformation GlobalItemIds = new ArrayPropertyInformation("GlobalItemIds", ExchangeVersion.Exchange2010SP1, "ItemId", ConversationItemSchema.ConversationGlobalItemIds, new PropertyUri(PropertyUriEnum.ConversationGlobalItemIds), new PropertyCommand.CreatePropertyCommand(ItemIdProperty.CreateCommand));

		// Token: 0x040009A0 RID: 2464
		public static readonly PropertyInformation LastModifiedTime = new PropertyInformation("LastModifiedTime", ExchangeVersion.Exchange2012, StoreObjectSchema.LastModifiedTime, new PropertyUri(PropertyUriEnum.ConversationLastModifiedTime), new PropertyCommand.CreatePropertyCommand(DateTimeProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x040009A1 RID: 2465
		public static readonly PropertyInformation InstanceKey = new PropertyInformation("InstanceKey", ExchangeVersion.Exchange2012, ItemSchema.InstanceKey, new PropertyUri(PropertyUriEnum.ConversationInstanceKey), new PropertyCommand.CreatePropertyCommand(InstanceKeyProperty.CreateCommand), PropertyInformationAttributes.ImplementsToXmlForPropertyBagCommand | PropertyInformationAttributes.ImplementsToServiceObjectForPropertyBagCommand);

		// Token: 0x040009A2 RID: 2466
		public static readonly PropertyInformation Preview = new PropertyInformation("Preview", ExchangeVersion.Exchange2012, ConversationItemSchema.ConversationPreview, new PropertyUri(PropertyUriEnum.ConversationPreview), new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x040009A3 RID: 2467
		public static readonly PropertyInformation GlobalPreview = new PropertyInformation("GlobalPreview", ExchangeVersion.Exchange2013, ConversationItemSchema.ConversationGlobalPreview, new PropertyUri(PropertyUriEnum.ConversationGlobalPreview), new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x040009A4 RID: 2468
		public static readonly PropertyInformation InitialPost = new PropertyInformation("InitialPost", ExchangeVersion.Exchange2012, ConversationItemSchema.ConversationInitialMemberDocumentId, new PropertyUri(PropertyUriEnum.ConversationInitialPost), new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x040009A5 RID: 2469
		public static readonly PropertyInformation RecentReplys = new PropertyInformation("RecentReplys", ExchangeVersion.Exchange2012, ConversationItemSchema.ConversationMemberDocumentIds, new PropertyUri(PropertyUriEnum.ConversationRecentReplys), new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x040009A6 RID: 2470
		public static readonly PropertyInformation WorkingSetSourcePartition = new PropertyInformation("WorkingSetSourcePartition", ExchangeVersion.Exchange2012, ConversationItemSchema.ConversationWorkingSetSourcePartition, new PropertyUri(PropertyUriEnum.ConversationWorkingSetSourcePartition), new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x040009A7 RID: 2471
		public static readonly PropertyInformation IconIndex = new PropertyInformation("IconIndex", ExchangeVersion.Exchange2012, ConversationItemSchema.ConversationReplyForwardState, new PropertyUri(PropertyUriEnum.ConversationIconIndex), new PropertyCommand.CreatePropertyCommand(IconIndexProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x040009A8 RID: 2472
		public static readonly PropertyInformation GlobalIconIndex = new PropertyInformation("GlobalIconIndex", ExchangeVersion.Exchange2012, ConversationItemSchema.ConversationGlobalReplyForwardState, new PropertyUri(PropertyUriEnum.ConversationGlobalIconIndex), new PropertyCommand.CreatePropertyCommand(IconIndexProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x040009A9 RID: 2473
		public static readonly PropertyInformation DraftItemIds = new ArrayPropertyInformation("DraftItemIds", ExchangeVersion.Exchange2012, "ItemId", ConversationItemSchema.ConversationGlobalItemIds, new PropertyUri(PropertyUriEnum.ConversationDraftItemIds), new PropertyCommand.CreatePropertyCommand(DraftItemIdsProperty.CreateCommand));

		// Token: 0x040009AA RID: 2474
		public static readonly PropertyInformation HasClutter = new PropertyInformation("HasClutter", ExchangeVersion.Exchange2012, ConversationItemSchema.ConversationHasClutter, new PropertyUri(PropertyUriEnum.ConversationHasClutter), new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x040009AB RID: 2475
		public static readonly PropertyInformation FamilyId = new PropertyInformation("FamilyId", ExchangeVersion.Exchange2012, ConversationItemSchema.FamilyId, new PropertyUri(PropertyUriEnum.FamilyId), new PropertyCommand.CreatePropertyCommand(ConversationIdProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x040009AC RID: 2476
		public static readonly PropertyInformation LastDeliveryOrRenewTime = new PropertyInformation("LastDeliveryOrRenewTime", ExchangeVersion.Exchange2012, ConversationItemSchema.ConversationLastDeliveryOrRenewTime, new PropertyUri(PropertyUriEnum.ConversationLastDeliveryOrRenewTime), new PropertyCommand.CreatePropertyCommand(DateTimeProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);
	}
}
