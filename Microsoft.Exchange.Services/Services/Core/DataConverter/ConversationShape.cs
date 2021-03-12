using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x020001C1 RID: 449
	internal sealed class ConversationShape : Shape
	{
		// Token: 0x06000C51 RID: 3153 RVA: 0x0003E784 File Offset: 0x0003C984
		static ConversationShape()
		{
			ConversationShape.defaultProperties.Add(ConversationSchema.ConversationId);
			ConversationShape.defaultProperties.Add(ConversationSchema.ConversationTopic);
			ConversationShape.defaultProperties.Add(ConversationSchema.UniqueRecipients);
			ConversationShape.defaultProperties.Add(ConversationSchema.GlobalUniqueRecipients);
			ConversationShape.defaultProperties.Add(ConversationSchema.UniqueUnreadSenders);
			ConversationShape.defaultProperties.Add(ConversationSchema.GlobalUniqueUnreadSenders);
			ConversationShape.defaultProperties.Add(ConversationSchema.UniqueSenders);
			ConversationShape.defaultProperties.Add(ConversationSchema.GlobalUniqueSenders);
			ConversationShape.defaultProperties.Add(ConversationSchema.LastDeliveryTime);
			ConversationShape.defaultProperties.Add(ConversationSchema.GlobalLastDeliveryTime);
			ConversationShape.defaultProperties.Add(ConversationSchema.Categories);
			ConversationShape.defaultProperties.Add(ConversationSchema.GlobalCategories);
			ConversationShape.defaultProperties.Add(ConversationSchema.FlagStatus);
			ConversationShape.defaultProperties.Add(ConversationSchema.GlobalFlagStatus);
			ConversationShape.defaultProperties.Add(ConversationSchema.HasAttachments);
			ConversationShape.defaultProperties.Add(ConversationSchema.GlobalHasAttachments);
			ConversationShape.defaultProperties.Add(ConversationSchema.HasIrm);
			ConversationShape.defaultProperties.Add(ConversationSchema.GlobalHasIrm);
			ConversationShape.defaultProperties.Add(ConversationSchema.MessageCount);
			ConversationShape.defaultProperties.Add(ConversationSchema.GlobalMessageCount);
			ConversationShape.defaultProperties.Add(ConversationSchema.UnreadCount);
			ConversationShape.defaultProperties.Add(ConversationSchema.GlobalUnreadCount);
			ConversationShape.defaultProperties.Add(ConversationSchema.Size);
			ConversationShape.defaultProperties.Add(ConversationSchema.GlobalSize);
			ConversationShape.defaultProperties.Add(ConversationSchema.ItemClasses);
			ConversationShape.defaultProperties.Add(ConversationSchema.GlobalItemClasses);
			ConversationShape.defaultProperties.Add(ConversationSchema.Importance);
			ConversationShape.defaultProperties.Add(ConversationSchema.GlobalImportance);
			ConversationShape.defaultProperties.Add(ConversationSchema.ItemIds);
			ConversationShape.defaultProperties.Add(ConversationSchema.GlobalItemIds);
			ConversationShape.defaultProperties.Add(ConversationSchema.LastModifiedTime);
			ConversationShape.defaultProperties.Add(ConversationSchema.InstanceKey);
			ConversationShape.defaultProperties.Add(ConversationSchema.MailboxGuid);
		}

		// Token: 0x06000C52 RID: 3154 RVA: 0x0003E98A File Offset: 0x0003CB8A
		private ConversationShape(List<PropertyInformation> defaultProperties) : base(Schema.Conversation, ConversationSchema.GetSchema(), null, defaultProperties)
		{
		}

		// Token: 0x06000C53 RID: 3155 RVA: 0x0003E99E File Offset: 0x0003CB9E
		internal static ConversationShape CreateShape()
		{
			return new ConversationShape(ConversationShape.defaultProperties);
		}

		// Token: 0x040009AD RID: 2477
		private static List<PropertyInformation> defaultProperties = new List<PropertyInformation>();
	}
}
