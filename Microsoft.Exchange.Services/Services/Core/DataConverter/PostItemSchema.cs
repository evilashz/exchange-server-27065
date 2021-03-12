using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x020001B2 RID: 434
	internal sealed class PostItemSchema : Schema
	{
		// Token: 0x06000BC4 RID: 3012 RVA: 0x0003BDFC File Offset: 0x00039FFC
		static PostItemSchema()
		{
			XmlElementInformation[] xmlElements = new XmlElementInformation[]
			{
				PostItemSchema.ConversationIndex,
				PostItemSchema.ConversationTopic,
				PostItemSchema.From,
				PostItemSchema.InternetMessageId,
				PostItemSchema.IsRead,
				PostItemSchema.PostedTime,
				PostItemSchema.References,
				PostItemSchema.Sender
			};
			PostItemSchema.schema = new PostItemSchema(xmlElements);
		}

		// Token: 0x06000BC5 RID: 3013 RVA: 0x0003C02D File Offset: 0x0003A22D
		private PostItemSchema(XmlElementInformation[] xmlElements) : base(xmlElements)
		{
		}

		// Token: 0x06000BC6 RID: 3014 RVA: 0x0003C036 File Offset: 0x0003A236
		public static Schema GetSchema()
		{
			return PostItemSchema.schema;
		}

		// Token: 0x0400095E RID: 2398
		private static Schema schema;

		// Token: 0x0400095F RID: 2399
		public static readonly PropertyInformation ConversationIndex = new PropertyInformation("ConversationIndex", ExchangeVersion.Exchange2007SP1, ItemSchema.ConversationIndex, new PropertyUri(PropertyUriEnum.ConversationIndex), new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x04000960 RID: 2400
		public static readonly PropertyInformation ConversationTopic = new PropertyInformation("ConversationTopic", ExchangeVersion.Exchange2007SP1, ItemSchema.ConversationTopic, new PropertyUri(PropertyUriEnum.ConversationTopic), new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x04000961 RID: 2401
		public static readonly PropertyInformation From = new PropertyInformation("From", ServiceXml.GetFullyQualifiedName("From"), ServiceXml.DefaultNamespaceUri, ExchangeVersion.Exchange2007SP1, new PropertyDefinition[]
		{
			ItemSchema.SentRepresentingDisplayName,
			ItemSchema.SentRepresentingEmailAddress,
			ItemSchema.SentRepresentingType
		}, new PropertyUri(PropertyUriEnum.From), new PropertyCommand.CreatePropertyCommand(PostItemFromProperty.CreateCommand));

		// Token: 0x04000962 RID: 2402
		public static readonly PropertyInformation InternetMessageId = new PropertyInformation("InternetMessageId", ExchangeVersion.Exchange2007SP1, ItemSchema.InternetMessageId, new PropertyUri(PropertyUriEnum.InternetMessageId), new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x04000963 RID: 2403
		public static readonly PropertyInformation IsRead = new PropertyInformation("IsRead", ExchangeVersion.Exchange2007SP1, MessageItemSchema.IsRead, new PropertyUri(PropertyUriEnum.IsRead), new PropertyCommand.CreatePropertyCommand(IsReadProperty.CreateCommand), PropertyInformationAttributes.ImplementsSetCommand | PropertyInformationAttributes.ImplementsToXmlCommand | PropertyInformationAttributes.ImplementsSetUpdateCommand | PropertyInformationAttributes.ImplementsToXmlForPropertyBagCommand | PropertyInformationAttributes.ImplementsToServiceObjectCommand | PropertyInformationAttributes.ImplementsToServiceObjectForPropertyBagCommand);

		// Token: 0x04000964 RID: 2404
		public static readonly PropertyInformation PostedTime = new PropertyInformation("PostedTime", ExchangeVersion.Exchange2007SP1, ItemSchema.SentTime, new PropertyUri(PropertyUriEnum.PostedTime), new PropertyCommand.CreatePropertyCommand(DateTimeProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x04000965 RID: 2405
		public static readonly PropertyInformation References = new PropertyInformation("References", ExchangeVersion.Exchange2007SP1, ItemSchema.InternetReferences, new PropertyUri(PropertyUriEnum.References), new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand));

		// Token: 0x04000966 RID: 2406
		public static readonly PropertyInformation Sender = new PropertyInformation("Sender", ServiceXml.GetFullyQualifiedName("Sender"), ServiceXml.DefaultNamespaceUri, ExchangeVersion.Exchange2007SP1, new PropertyDefinition[]
		{
			PostItemSchema.SenderDisplayName,
			PostItemSchema.SenderEmailAddress,
			PostItemSchema.SenderAddressType
		}, new PropertyUri(PropertyUriEnum.Sender), new PropertyCommand.CreatePropertyCommand(PostItemSenderProperty.CreateCommand));
	}
}
