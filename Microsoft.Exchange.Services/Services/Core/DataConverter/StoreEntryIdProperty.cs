using System;
using System.Collections.Generic;
using System.Xml;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x02000174 RID: 372
	internal sealed class StoreEntryIdProperty : PropertyCommand, IToXmlCommand, IToXmlForPropertyBagCommand, IToServiceObjectCommand, IToServiceObjectForPropertyBagCommand, IPropertyCommand
	{
		// Token: 0x06000AB4 RID: 2740 RVA: 0x00033E5A File Offset: 0x0003205A
		private StoreEntryIdProperty(CommandContext commandContext) : base(commandContext)
		{
			this.propertyConverter = BaseConverter.GetConverterForPropertyDefinition(commandContext.GetPropertyDefinitions()[0]);
		}

		// Token: 0x06000AB5 RID: 2741 RVA: 0x00033E76 File Offset: 0x00032076
		public static StoreEntryIdProperty CreateCommand(CommandContext commandContext)
		{
			return new StoreEntryIdProperty(commandContext);
		}

		// Token: 0x06000AB6 RID: 2742 RVA: 0x00033E80 File Offset: 0x00032080
		public void ToServiceObject()
		{
			ToServiceObjectCommandSettings commandSettings = base.GetCommandSettings<ToServiceObjectCommandSettings>();
			StoreObject storeObject = commandSettings.StoreObject;
			ServiceObject serviceObject = commandSettings.ServiceObject;
			PropertyInformation propertyInformation = this.commandContext.PropertyInformation;
			byte[] idFromExrpc = PropertyCommand.GetPropertyValueFromStoreObject(storeObject, StoreObjectSchema.MapiStoreEntryId) as byte[];
			serviceObject.PropertyBag[propertyInformation] = StoreEntryId.WrapStoreId(idFromExrpc);
		}

		// Token: 0x06000AB7 RID: 2743 RVA: 0x00033ED4 File Offset: 0x000320D4
		public void ToServiceObjectForPropertyBag()
		{
			ToServiceObjectForPropertyBagCommandSettings commandSettings = base.GetCommandSettings<ToServiceObjectForPropertyBagCommandSettings>();
			IDictionary<PropertyDefinition, object> propertyBag = commandSettings.PropertyBag;
			ServiceObject serviceObject = commandSettings.ServiceObject;
			PropertyInformation propertyInformation = this.commandContext.PropertyInformation;
			byte[] idFromExrpc;
			if (PropertyCommand.TryGetValueFromPropertyBag<byte[]>(propertyBag, StoreObjectSchema.MapiStoreEntryId, out idFromExrpc))
			{
				serviceObject.PropertyBag[propertyInformation] = StoreEntryId.WrapStoreId(idFromExrpc);
			}
		}

		// Token: 0x06000AB8 RID: 2744 RVA: 0x00033F24 File Offset: 0x00032124
		private void WriteXml(byte[] storeEntryIdValue, XmlElement serviceItem)
		{
			string text = this.propertyConverter.ConvertToString(storeEntryIdValue);
			if (text != null)
			{
				base.CreateXmlTextElement(serviceItem, this.xmlLocalName, text);
			}
		}

		// Token: 0x06000AB9 RID: 2745 RVA: 0x00033F50 File Offset: 0x00032150
		public void ToXml()
		{
			ToXmlCommandSettings commandSettings = base.GetCommandSettings<ToXmlCommandSettings>();
			StoreObject storeObject = commandSettings.StoreObject;
			XmlElement serviceItem = commandSettings.ServiceItem;
			byte[] idFromExrpc = PropertyCommand.GetPropertyValueFromStoreObject(storeObject, StoreObjectSchema.MapiStoreEntryId) as byte[];
			byte[] storeEntryIdValue = StoreEntryId.WrapStoreId(idFromExrpc);
			this.WriteXml(storeEntryIdValue, serviceItem);
		}

		// Token: 0x06000ABA RID: 2746 RVA: 0x00033F94 File Offset: 0x00032194
		public void ToXmlForPropertyBag()
		{
			ToXmlForPropertyBagCommandSettings commandSettings = base.GetCommandSettings<ToXmlForPropertyBagCommandSettings>();
			IDictionary<PropertyDefinition, object> propertyBag = commandSettings.PropertyBag;
			XmlElement serviceItem = commandSettings.ServiceItem;
			byte[] idFromExrpc = null;
			if (PropertyCommand.TryGetValueFromPropertyBag<byte[]>(propertyBag, StoreObjectSchema.MapiStoreEntryId, out idFromExrpc))
			{
				byte[] storeEntryIdValue = StoreEntryId.WrapStoreId(idFromExrpc);
				this.WriteXml(storeEntryIdValue, serviceItem);
			}
		}

		// Token: 0x040007D0 RID: 2000
		private BaseConverter propertyConverter;
	}
}
