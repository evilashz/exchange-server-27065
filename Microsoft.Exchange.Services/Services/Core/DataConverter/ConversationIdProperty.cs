using System;
using System.Collections.Generic;
using System.Xml;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x02000162 RID: 354
	internal sealed class ConversationIdProperty : ComplexPropertyBase, IToXmlCommand, IToXmlForPropertyBagCommand, IToServiceObjectCommand, IToServiceObjectForPropertyBagCommand, IPropertyCommand
	{
		// Token: 0x060009F1 RID: 2545 RVA: 0x00030200 File Offset: 0x0002E400
		private ConversationIdProperty(CommandContext commandContext) : base(commandContext)
		{
			this.conversationIdDefinition = this.propertyDefinitions[0];
		}

		// Token: 0x060009F2 RID: 2546 RVA: 0x00030217 File Offset: 0x0002E417
		public static ConversationIdProperty CreateCommand(CommandContext commandContext)
		{
			return new ConversationIdProperty(commandContext);
		}

		// Token: 0x060009F3 RID: 2547 RVA: 0x0003021F File Offset: 0x0002E41F
		private BaseItemId CreateServiceObjectId(string id, string changeKey)
		{
			return new ItemId(id, changeKey);
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x060009F4 RID: 2548 RVA: 0x00030228 File Offset: 0x0002E428
		public override bool ToServiceObjectRequiresMailboxAccess
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060009F5 RID: 2549 RVA: 0x0003022C File Offset: 0x0002E42C
		public void ToServiceObject()
		{
			ToServiceObjectCommandSettings commandSettings = base.GetCommandSettings<ToServiceObjectCommandSettings>();
			StoreObject storeObject = commandSettings.StoreObject;
			ServiceObject serviceObject = commandSettings.ServiceObject;
			PropertyInformation propertyInformation = this.commandContext.PropertyInformation;
			ConversationId conversationId = PropertyCommand.GetPropertyValueFromStoreObject(storeObject, this.conversationIdDefinition) as ConversationId;
			MailboxSession mailboxSession = commandSettings.IdAndSession.Session as MailboxSession;
			Guid guid;
			if (mailboxSession != null && mailboxSession.IsUnified)
			{
				guid = (Guid)PropertyCommand.GetPropertyValueFromStoreObject(storeObject, ConversationItemSchema.MailboxGuid);
				if (guid == Guid.Empty)
				{
					throw new RequiredPropertyMissingException(ResponseCodeType.ErrorRequiredPropertyMissing);
				}
			}
			else
			{
				guid = commandSettings.IdAndSession.Session.MailboxGuid;
			}
			string id = IdConverter.ConversationIdToEwsId(guid, conversationId);
			serviceObject.PropertyBag[propertyInformation] = this.CreateServiceObjectId(id, null);
		}

		// Token: 0x060009F6 RID: 2550 RVA: 0x000302EC File Offset: 0x0002E4EC
		public void ToServiceObjectForPropertyBag()
		{
			ToServiceObjectForPropertyBagCommandSettings commandSettings = base.GetCommandSettings<ToServiceObjectForPropertyBagCommandSettings>();
			IDictionary<PropertyDefinition, object> propertyBag = commandSettings.PropertyBag;
			ServiceObject serviceObject = commandSettings.ServiceObject;
			IdAndSession idAndSession = commandSettings.IdAndSession;
			PropertyInformation propertyInformation = this.commandContext.PropertyInformation;
			ConversationId conversationId = null;
			if (PropertyCommand.TryGetValueFromPropertyBag<ConversationId>(propertyBag, this.conversationIdDefinition, out conversationId))
			{
				MailboxSession mailboxSession = commandSettings.IdAndSession.Session as MailboxSession;
				Guid mailboxGuid;
				if (mailboxSession != null && mailboxSession.IsUnified)
				{
					if (!PropertyCommand.TryGetValueFromPropertyBag<Guid>(propertyBag, ConversationItemSchema.MailboxGuid, out mailboxGuid))
					{
						throw new RequiredPropertyMissingException(ResponseCodeType.ErrorRequiredPropertyMissing);
					}
				}
				else
				{
					mailboxGuid = commandSettings.IdAndSession.Session.MailboxGuid;
				}
				string id = IdConverter.ConversationIdToEwsId(mailboxGuid, conversationId);
				serviceObject.PropertyBag[propertyInformation] = this.CreateServiceObjectId(id, null);
			}
		}

		// Token: 0x060009F7 RID: 2551 RVA: 0x000303A0 File Offset: 0x0002E5A0
		public void ToXml()
		{
			ToXmlCommandSettings commandSettings = base.GetCommandSettings<ToXmlCommandSettings>();
			StoreObject storeObject = commandSettings.StoreObject;
			XmlElement serviceItem = commandSettings.ServiceItem;
			StoreSession session = storeObject.Session;
			IdAndSession idAndSession = commandSettings.IdAndSession;
			ConversationId conversationId = PropertyCommand.GetPropertyValueFromStoreObject(storeObject, this.conversationIdDefinition) as ConversationId;
			IdConverter.CreateConversationIdXml(serviceItem, conversationId, idAndSession, this.xmlLocalName);
		}

		// Token: 0x060009F8 RID: 2552 RVA: 0x000303F4 File Offset: 0x0002E5F4
		public void ToXmlForPropertyBag()
		{
			ToXmlForPropertyBagCommandSettings commandSettings = base.GetCommandSettings<ToXmlForPropertyBagCommandSettings>();
			IDictionary<PropertyDefinition, object> propertyBag = commandSettings.PropertyBag;
			XmlElement serviceItem = commandSettings.ServiceItem;
			IdAndSession idAndSession = commandSettings.IdAndSession;
			ConversationId conversationId = null;
			PropertyCommand.TryGetValueFromPropertyBag<ConversationId>(propertyBag, this.conversationIdDefinition, out conversationId);
			IdConverter.CreateConversationIdXml(serviceItem, conversationId, idAndSession, this.xmlLocalName);
		}

		// Token: 0x040007A8 RID: 1960
		private PropertyDefinition conversationIdDefinition;
	}
}
