using System;
using System.Collections.Generic;
using System.Xml;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x02000211 RID: 529
	internal static class XsoDataConverter
	{
		// Token: 0x06000DBC RID: 3516 RVA: 0x00044404 File Offset: 0x00042604
		public static ToXmlPropertyList GetPropertyList(StoreObject storeObject, ResponseShape responseShape)
		{
			return PropertyList.CreateToXmlPropertyList(responseShape, storeObject);
		}

		// Token: 0x06000DBD RID: 3517 RVA: 0x0004440D File Offset: 0x0004260D
		public static ToServiceObjectPropertyList GetToServiceObjectPropertyList(StoreObject storeObject, ResponseShape responseShape)
		{
			return PropertyList.CreateToServiceObjectPropertyList(responseShape, storeObject);
		}

		// Token: 0x06000DBE RID: 3518 RVA: 0x00044416 File Offset: 0x00042616
		public static ToServiceObjectPropertyListInMemory GetToServiceObjectPropertyListInMemory(StoreObject storeObject, ResponseShape responseShape)
		{
			return PropertyList.CreateToServiceObjectPropertyListInMemory(responseShape, storeObject);
		}

		// Token: 0x06000DBF RID: 3519 RVA: 0x00044420 File Offset: 0x00042620
		public static ToServiceObjectPropertyList GetToServiceObjectPropertyListForPropertyBagUsingStoreObject(StoreObjectId storeObjectId, ResponseShape responseShape, IParticipantResolver participantResolver)
		{
			ObjectInformation objectInformation = Schema.GetObjectInformation(storeObjectId.ObjectType);
			return PropertyList.CreateToServiceObjectForPropertyBagUsingStoreObject(responseShape, objectInformation, participantResolver);
		}

		// Token: 0x06000DC0 RID: 3520 RVA: 0x00044441 File Offset: 0x00042641
		public static ToXmlPropertyList GetPropertyList(ObjectInformation objectInformation, ResponseShape responseShape)
		{
			return PropertyList.CreateToXmlPropertyList(responseShape, objectInformation);
		}

		// Token: 0x06000DC1 RID: 3521 RVA: 0x0004444A File Offset: 0x0004264A
		public static ToServiceObjectPropertyList GetToServiceObjectPropertyList(ObjectInformation objectInformation, ResponseShape responseShape, IParticipantResolver participantResolver)
		{
			return PropertyList.CreateToServiceObjectPropertyList(responseShape, objectInformation, participantResolver);
		}

		// Token: 0x06000DC2 RID: 3522 RVA: 0x00044454 File Offset: 0x00042654
		public static ToXmlPropertyList GetPropertyList(StoreId storeId, StoreSession session, ResponseShape responseShape)
		{
			StoreObjectId asStoreObjectId = IdConverter.GetAsStoreObjectId(storeId);
			ObjectInformation objectInformation = Schema.GetObjectInformation(asStoreObjectId.ObjectType);
			ToXmlPropertyList propertyList;
			try
			{
				propertyList = XsoDataConverter.GetPropertyList(objectInformation, responseShape);
			}
			catch (InvalidPropertyRequestException)
			{
				propertyList = XsoDataConverter.GetPropertyList(objectInformation, new ItemResponseShape(ShapeEnum.IdOnly, BodyResponseType.Best, false, responseShape.AdditionalProperties));
			}
			return propertyList;
		}

		// Token: 0x06000DC3 RID: 3523 RVA: 0x000444A8 File Offset: 0x000426A8
		public static ToServiceObjectPropertyList GetToServiceObjectPropertyList(StoreId storeId, StoreSession session, ResponseShape responseShape, IParticipantResolver participantResolver)
		{
			StoreObjectId asStoreObjectId = IdConverter.GetAsStoreObjectId(storeId);
			ObjectInformation objectInformation = Schema.GetObjectInformation(asStoreObjectId.ObjectType);
			ToServiceObjectPropertyList toServiceObjectPropertyList;
			try
			{
				toServiceObjectPropertyList = XsoDataConverter.GetToServiceObjectPropertyList(objectInformation, responseShape, participantResolver);
			}
			catch (InvalidPropertyRequestException)
			{
				StoreObjectType objectType = IdConverter.GetAsStoreObjectId(storeId).ObjectType;
				using (StoreObject storeObject = (responseShape is FolderResponseShape) ? Folder.Bind(session, storeId, null) : ServiceCommandBase.GetXsoItem(session, storeId, new PropertyDefinition[0]))
				{
					XsoDataConverter.VerifyObjectTypeAssumptions(objectType, storeObject);
				}
				throw;
			}
			return toServiceObjectPropertyList;
		}

		// Token: 0x06000DC4 RID: 3524 RVA: 0x00044534 File Offset: 0x00042734
		public static void VerifyObjectTypeAssumptions(StoreObjectType expectedType, StoreObject actualObject)
		{
			StoreObjectType objectType = actualObject.Id.ObjectId.ObjectType;
			if (expectedType == StoreObjectType.Folder && IdConverter.IsFolderObjectType(objectType))
			{
				return;
			}
			if (expectedType == StoreObjectType.Unknown)
			{
				return;
			}
			if (ExchangeVersion.Current.Supports(ExchangeVersion.Exchange2007SP1) && objectType == StoreObjectType.Unknown && expectedType == StoreObjectType.Message)
			{
				return;
			}
			if (objectType != expectedType)
			{
				throw new ObjectNotFoundException(ServerStrings.ConflictingObjectType((int)expectedType, (int)objectType));
			}
		}

		// Token: 0x06000DC5 RID: 3525 RVA: 0x0004458E File Offset: 0x0004278E
		public static ToXmlForPropertyBagPropertyList GetPropertyListForPropertyBag(ResponseShape responseShape, ObjectInformation objectInformation)
		{
			return PropertyList.CreateToXmlForPropertyBagPropertyList(responseShape, objectInformation);
		}

		// Token: 0x06000DC6 RID: 3526 RVA: 0x00044597 File Offset: 0x00042797
		public static ToServiceObjectForPropertyBagPropertyList GetServiceObjectPropertyListForPropertyBag(ResponseShape responseShape, ObjectInformation objectInformation)
		{
			return PropertyList.CreateToServiceObjectForPropertyBagPropertyList(responseShape, objectInformation);
		}

		// Token: 0x06000DC7 RID: 3527 RVA: 0x000445A0 File Offset: 0x000427A0
		public static void SetProperties(StoreObject storeObject, ServiceObject serviceObject, IdConverter idConverter)
		{
			IList<ISetCommand> list = PropertyList.CreateSetPropertyCommands(serviceObject, storeObject, idConverter);
			foreach (ISetCommand setCommand in list)
			{
				setCommand.Set();
			}
			foreach (ISetCommand setCommand2 in list)
			{
				setCommand2.SetPhase2();
			}
			foreach (ISetCommand setCommand3 in list)
			{
				setCommand3.SetPhase3();
			}
		}

		// Token: 0x06000DC8 RID: 3528 RVA: 0x0004466C File Offset: 0x0004286C
		public static void UpdateProperties(StoreObject storeObject, PropertyUpdate[] propertyUpdates, IdConverter idConverter, bool suppressReadReceipts, IFeaturesManager featuresManager)
		{
			IList<IUpdateCommand> list = PropertyList.CreateUpdatePropertyCommands(propertyUpdates, storeObject, idConverter, suppressReadReceipts, featuresManager);
			foreach (IUpdateCommand updateCommand in list)
			{
				updateCommand.Update();
			}
			foreach (IUpdateCommand updateCommand2 in list)
			{
				updateCommand2.PostUpdate();
			}
		}

		// Token: 0x06000DC9 RID: 3529 RVA: 0x000446FC File Offset: 0x000428FC
		public static bool TryGetStoreObject<T>(StoreObject storeObject, out T typedStoreObject) where T : StoreObject
		{
			typedStoreObject = (storeObject as T);
			return typedStoreObject != null;
		}

		// Token: 0x06000DCA RID: 3530 RVA: 0x00044720 File Offset: 0x00042920
		public static void SetProperties(StoreObject storeObject, XmlElement serviceItem, IdConverter idConverter)
		{
			IList<ISetCommand> list = PropertyList.CreateSetPropertyCommands(serviceItem, storeObject, idConverter);
			foreach (ISetCommand setCommand in list)
			{
				setCommand.Set();
			}
			foreach (ISetCommand setCommand2 in list)
			{
				setCommand2.SetPhase2();
			}
			foreach (ISetCommand setCommand3 in list)
			{
				setCommand3.SetPhase3();
			}
		}
	}
}
