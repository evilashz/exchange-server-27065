using System;
using System.Collections.Generic;
using System.Xml;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x0200016D RID: 365
	internal sealed class PersonaIdProperty : ComplexPropertyBase, IToXmlCommand, IToXmlForPropertyBagCommand, IToServiceObjectCommand, IToServiceObjectForPropertyBagCommand, IPropertyCommand
	{
		// Token: 0x06000A66 RID: 2662 RVA: 0x00032A3C File Offset: 0x00030C3C
		private PersonaIdProperty(CommandContext commandContext) : base(commandContext)
		{
		}

		// Token: 0x06000A67 RID: 2663 RVA: 0x00032A45 File Offset: 0x00030C45
		public static PersonaIdProperty CreateCommand(CommandContext commandContext)
		{
			return new PersonaIdProperty(commandContext);
		}

		// Token: 0x06000A68 RID: 2664 RVA: 0x00032A4D File Offset: 0x00030C4D
		private BaseItemId CreateServiceObjectId(string id, string changeKey)
		{
			return new ItemId(id, changeKey);
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x06000A69 RID: 2665 RVA: 0x00032A56 File Offset: 0x00030C56
		public override bool ToServiceObjectRequiresMailboxAccess
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000A6A RID: 2666 RVA: 0x00032A5C File Offset: 0x00030C5C
		public void ToServiceObject()
		{
			ToServiceObjectCommandSettings commandSettings = base.GetCommandSettings<ToServiceObjectCommandSettings>();
			StoreObject storeObject = commandSettings.StoreObject;
			ServiceObject serviceObject = commandSettings.ServiceObject;
			PropertyInformation propertyInformation = this.commandContext.PropertyInformation;
			PersonId personId = PropertyCommand.GetPropertyValueFromStoreObject(storeObject, PersonSchema.Id) as PersonId;
			string id = IdConverter.PersonIdToEwsId(commandSettings.IdAndSession.Session.MailboxGuid, personId);
			serviceObject.PropertyBag[propertyInformation] = this.CreateServiceObjectId(id, null);
		}

		// Token: 0x06000A6B RID: 2667 RVA: 0x00032ACC File Offset: 0x00030CCC
		public void ToServiceObjectForPropertyBag()
		{
			ToServiceObjectForPropertyBagCommandSettings commandSettings = base.GetCommandSettings<ToServiceObjectForPropertyBagCommandSettings>();
			IDictionary<PropertyDefinition, object> propertyBag = commandSettings.PropertyBag;
			ServiceObject serviceObject = commandSettings.ServiceObject;
			IdAndSession idAndSession = commandSettings.IdAndSession;
			PropertyInformation propertyInformation = this.commandContext.PropertyInformation;
			PersonId personId = null;
			if (PropertyCommand.TryGetValueFromPropertyBag<PersonId>(propertyBag, PersonSchema.Id, out personId))
			{
				string id = IdConverter.PersonIdToEwsId(idAndSession.Session.MailboxGuid, personId);
				serviceObject.PropertyBag[propertyInformation] = this.CreateServiceObjectId(id, null);
			}
		}

		// Token: 0x06000A6C RID: 2668 RVA: 0x00032B40 File Offset: 0x00030D40
		public void ToXml()
		{
			ToXmlCommandSettings commandSettings = base.GetCommandSettings<ToXmlCommandSettings>();
			StoreObject storeObject = commandSettings.StoreObject;
			XmlElement serviceItem = commandSettings.ServiceItem;
			StoreSession session = storeObject.Session;
			IdAndSession idAndSession = commandSettings.IdAndSession;
			PersonId personId = PropertyCommand.GetPropertyValueFromStoreObject(storeObject, PersonSchema.Id) as PersonId;
			IdConverter.CreatePersonIdXml(serviceItem, personId, idAndSession, this.xmlLocalName);
		}

		// Token: 0x06000A6D RID: 2669 RVA: 0x00032B94 File Offset: 0x00030D94
		public void ToXmlForPropertyBag()
		{
			ToXmlForPropertyBagCommandSettings commandSettings = base.GetCommandSettings<ToXmlForPropertyBagCommandSettings>();
			IDictionary<PropertyDefinition, object> propertyBag = commandSettings.PropertyBag;
			XmlElement serviceItem = commandSettings.ServiceItem;
			IdAndSession idAndSession = commandSettings.IdAndSession;
			PersonId personId;
			PropertyCommand.TryGetValueFromPropertyBag<PersonId>(propertyBag, PersonSchema.Id, out personId);
			IdConverter.CreatePersonIdXml(serviceItem, personId, idAndSession, this.xmlLocalName);
		}
	}
}
