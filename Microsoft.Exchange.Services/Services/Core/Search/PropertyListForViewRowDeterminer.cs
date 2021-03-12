using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.DataConverter;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.Search
{
	// Token: 0x02000274 RID: 628
	internal class PropertyListForViewRowDeterminer
	{
		// Token: 0x06001065 RID: 4197 RVA: 0x0004EFC3 File Offset: 0x0004D1C3
		private PropertyListForViewRowDeterminer(StorePropertyDefinition idPropertyDefinition, Dictionary<ObjectInformation, ToXmlForPropertyBagPropertyList> xmlMapping, Dictionary<ObjectInformation, ToServiceObjectForPropertyBagPropertyList> serviceObjectMapping) : this(idPropertyDefinition, xmlMapping, serviceObjectMapping, xmlMapping.Values)
		{
		}

		// Token: 0x06001066 RID: 4198 RVA: 0x0004EFD4 File Offset: 0x0004D1D4
		private PropertyListForViewRowDeterminer(StorePropertyDefinition idPropertyDefinition, Dictionary<ObjectInformation, ToXmlForPropertyBagPropertyList> xmlMapping, Dictionary<ObjectInformation, ToServiceObjectForPropertyBagPropertyList> serviceObjectMapping, IEnumerable<ToXmlForPropertyBagPropertyList> propertyLists)
		{
			this.idPropertyDefinition = idPropertyDefinition;
			this.objectTypeToPropertyListMap = xmlMapping;
			this.objectTypeToServiceObjectPropertyListMap = serviceObjectMapping;
			this.propsToFetch = new List<PropertyDefinition>();
			foreach (ToXmlForPropertyBagPropertyList toXmlForPropertyBagPropertyList in propertyLists)
			{
				PropertyDefinition[] propertyDefinitions = toXmlForPropertyBagPropertyList.GetPropertyDefinitions();
				PropertyListForViewRowDeterminer.AddUniquePropertyDefinitions(propertyDefinitions, this.propsToFetch);
			}
		}

		// Token: 0x06001067 RID: 4199 RVA: 0x0004F050 File Offset: 0x0004D250
		private static void AddUniquePropertyDefinitions(IList<PropertyDefinition> source, IList<PropertyDefinition> dest)
		{
			foreach (PropertyDefinition item in source)
			{
				if (dest.IndexOf(item) == -1)
				{
					dest.Add(item);
				}
			}
		}

		// Token: 0x06001068 RID: 4200 RVA: 0x0004F0A4 File Offset: 0x0004D2A4
		internal PropertyDefinition[] GetPropertiesToFetch()
		{
			return this.propsToFetch.ToArray();
		}

		// Token: 0x06001069 RID: 4201 RVA: 0x0004F0B4 File Offset: 0x0004D2B4
		internal ToXmlForPropertyBagPropertyList GetPropertyList(IDictionary<PropertyDefinition, object> view)
		{
			StoreId storeId = null;
			PropertyCommand.TryGetValueFromPropertyBag<StoreId>(view, this.idPropertyDefinition, out storeId);
			if (storeId is ConversationId)
			{
				return this.objectTypeToPropertyListMap[Schema.Conversation];
			}
			if (storeId is PersonId)
			{
				return this.objectTypeToPropertyListMap[Schema.Persona];
			}
			StoreObjectId asStoreObjectId = IdConverter.GetAsStoreObjectId(storeId);
			StoreObjectType objectType = asStoreObjectId.ObjectType;
			ObjectInformation objectInformation = Schema.GetObjectInformation(objectType);
			ToXmlForPropertyBagPropertyList result = null;
			this.objectTypeToPropertyListMap.TryGetValue(objectInformation, out result);
			return result;
		}

		// Token: 0x0600106A RID: 4202 RVA: 0x0004F130 File Offset: 0x0004D330
		internal ToServiceObjectForPropertyBagPropertyList GetToServiceObjectPropertyList(IDictionary<PropertyDefinition, object> view, out StoreObjectType storeObjectType)
		{
			StoreId storeId = null;
			PropertyCommand.TryGetValueFromPropertyBag<StoreId>(view, this.idPropertyDefinition, out storeId);
			storeObjectType = StoreObjectType.Unknown;
			if (storeId is ConversationId)
			{
				return this.objectTypeToServiceObjectPropertyListMap[Schema.Conversation];
			}
			if (storeId is PersonId)
			{
				return this.objectTypeToServiceObjectPropertyListMap[Schema.Persona];
			}
			StoreObjectId asStoreObjectId = IdConverter.GetAsStoreObjectId(storeId);
			storeObjectType = asStoreObjectId.ObjectType;
			ObjectInformation objectInformation = Schema.GetObjectInformation(storeObjectType);
			ToServiceObjectForPropertyBagPropertyList result = null;
			this.objectTypeToServiceObjectPropertyListMap.TryGetValue(objectInformation, out result);
			return result;
		}

		// Token: 0x0600106B RID: 4203 RVA: 0x0004F1AC File Offset: 0x0004D3AC
		internal ToServiceObjectForPropertyBagPropertyList GetToServiceObjectPropertyListForConversation()
		{
			return this.objectTypeToServiceObjectPropertyListMap[Schema.Conversation];
		}

		// Token: 0x0600106C RID: 4204 RVA: 0x0004F1C0 File Offset: 0x0004D3C0
		internal static PropertyListForViewRowDeterminer BuildForFolders(ResponseShape responseShape)
		{
			Dictionary<ObjectInformation, ToXmlForPropertyBagPropertyList> xmlMapping = new Dictionary<ObjectInformation, ToXmlForPropertyBagPropertyList>();
			Dictionary<ObjectInformation, ToServiceObjectForPropertyBagPropertyList> serviceObjectMapping = new Dictionary<ObjectInformation, ToServiceObjectForPropertyBagPropertyList>();
			ObjectInformation[] folderInformation = Schema.GetFolderInformation();
			foreach (ObjectInformation objectInformation in folderInformation)
			{
				PropertyListForViewRowDeterminer.AddMapping(xmlMapping, serviceObjectMapping, responseShape, objectInformation);
			}
			return new PropertyListForViewRowDeterminer(FolderSchema.Id, xmlMapping, serviceObjectMapping);
		}

		// Token: 0x0600106D RID: 4205 RVA: 0x0004F210 File Offset: 0x0004D410
		internal static PropertyListForViewRowDeterminer BuildForItems(ResponseShape responseShape, Folder folder)
		{
			Dictionary<ObjectInformation, ToXmlForPropertyBagPropertyList> xmlMapping = new Dictionary<ObjectInformation, ToXmlForPropertyBagPropertyList>();
			Dictionary<ObjectInformation, ToServiceObjectForPropertyBagPropertyList> serviceObjectMapping = new Dictionary<ObjectInformation, ToServiceObjectForPropertyBagPropertyList>();
			ObjectInformation[] itemInformation = Schema.GetItemInformation();
			foreach (ObjectInformation objectInformation in itemInformation)
			{
				PropertyListForViewRowDeterminer.AddMapping(xmlMapping, serviceObjectMapping, responseShape, objectInformation);
			}
			List<ToXmlForPropertyBagPropertyList> list = new List<ToXmlForPropertyBagPropertyList>();
			ObjectInformation objectInformation2 = Schema.GetObjectInformation(folder);
			ObjectInformation[] itemInformationForFolder = Schema.GetItemInformationForFolder(objectInformation2);
			foreach (ObjectInformation objectInformation3 in itemInformationForFolder)
			{
				list.Add(XsoDataConverter.GetPropertyListForPropertyBag(responseShape, objectInformation3));
			}
			if (responseShape.AdditionalProperties != null && responseShape.AdditionalProperties.Length > 0)
			{
				ResponseShape responseShape2 = new ItemResponseShape
				{
					BaseShape = ShapeEnum.IdOnly,
					AdditionalProperties = responseShape.AdditionalProperties
				};
				foreach (ObjectInformation objectInformation4 in itemInformation.Except(itemInformationForFolder))
				{
					list.Add(XsoDataConverter.GetPropertyListForPropertyBag(responseShape2, objectInformation4));
				}
			}
			return new PropertyListForViewRowDeterminer(ItemSchema.Id, xmlMapping, serviceObjectMapping, list);
		}

		// Token: 0x0600106E RID: 4206 RVA: 0x0004F32C File Offset: 0x0004D52C
		internal static PropertyListForViewRowDeterminer BuildForConversation(ResponseShape responseShape)
		{
			ObjectInformation conversation = Schema.Conversation;
			Dictionary<ObjectInformation, ToXmlForPropertyBagPropertyList> dictionary = new Dictionary<ObjectInformation, ToXmlForPropertyBagPropertyList>();
			ToXmlForPropertyBagPropertyList value = PropertyList.CreateToXmlForPropertyBagPropertyList(responseShape, conversation);
			dictionary.Add(conversation, value);
			Dictionary<ObjectInformation, ToServiceObjectForPropertyBagPropertyList> dictionary2 = new Dictionary<ObjectInformation, ToServiceObjectForPropertyBagPropertyList>();
			ToServiceObjectForPropertyBagPropertyList value2 = PropertyList.CreateToServiceObjectForPropertyBagPropertyList(responseShape, conversation);
			dictionary2.Add(conversation, value2);
			return new PropertyListForViewRowDeterminer((StorePropertyDefinition)ConversationItemSchema.ConversationId, dictionary, dictionary2);
		}

		// Token: 0x0600106F RID: 4207 RVA: 0x0004F380 File Offset: 0x0004D580
		internal static PropertyListForViewRowDeterminer BuildForPersonObjects(PersonaResponseShape responseShape)
		{
			ObjectInformation persona = Schema.Persona;
			Dictionary<ObjectInformation, ToXmlForPropertyBagPropertyList> dictionary = new Dictionary<ObjectInformation, ToXmlForPropertyBagPropertyList>();
			ToXmlForPropertyBagPropertyList value = PropertyList.CreateToXmlForPropertyBagPropertyList(responseShape, persona);
			dictionary.Add(persona, value);
			Dictionary<ObjectInformation, ToServiceObjectForPropertyBagPropertyList> dictionary2 = new Dictionary<ObjectInformation, ToServiceObjectForPropertyBagPropertyList>();
			ToServiceObjectForPropertyBagPropertyList propertyListForPersonaResponseShape = Persona.GetPropertyListForPersonaResponseShape(responseShape);
			dictionary2.Add(persona, propertyListForPersonaResponseShape);
			return new PropertyListForViewRowDeterminer(PersonSchema.Id, dictionary, dictionary2);
		}

		// Token: 0x06001070 RID: 4208 RVA: 0x0004F3CC File Offset: 0x0004D5CC
		private static void AddMapping(Dictionary<ObjectInformation, ToXmlForPropertyBagPropertyList> xmlMapping, Dictionary<ObjectInformation, ToServiceObjectForPropertyBagPropertyList> serviceObjectMapping, ResponseShape responseShape, ObjectInformation objectInformation)
		{
			ToXmlForPropertyBagPropertyList propertyListForPropertyBag = XsoDataConverter.GetPropertyListForPropertyBag(responseShape, objectInformation);
			xmlMapping.Add(objectInformation, propertyListForPropertyBag);
			ToServiceObjectForPropertyBagPropertyList serviceObjectPropertyListForPropertyBag = XsoDataConverter.GetServiceObjectPropertyListForPropertyBag(responseShape, objectInformation);
			serviceObjectMapping.Add(objectInformation, serviceObjectPropertyListForPropertyBag);
		}

		// Token: 0x04000C14 RID: 3092
		private Dictionary<ObjectInformation, ToXmlForPropertyBagPropertyList> objectTypeToPropertyListMap;

		// Token: 0x04000C15 RID: 3093
		private Dictionary<ObjectInformation, ToServiceObjectForPropertyBagPropertyList> objectTypeToServiceObjectPropertyListMap;

		// Token: 0x04000C16 RID: 3094
		private List<PropertyDefinition> propsToFetch;

		// Token: 0x04000C17 RID: 3095
		private StorePropertyDefinition idPropertyDefinition;
	}
}
