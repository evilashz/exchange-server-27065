using System;
using System.Collections.Generic;
using System.Xml;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x020001B4 RID: 436
	internal abstract class PropertyList
	{
		// Token: 0x06000BCA RID: 3018 RVA: 0x0003C11F File Offset: 0x0003A31F
		protected PropertyList()
		{
		}

		// Token: 0x06000BCB RID: 3019 RVA: 0x0003C127 File Offset: 0x0003A327
		public PropertyList(Shape shape)
		{
			this.shape = shape;
		}

		// Token: 0x06000BCC RID: 3020 RVA: 0x0003C138 File Offset: 0x0003A338
		public static ToXmlPropertyList CreateToXmlPropertyList(ResponseShape responseShape, StoreObject storeObject)
		{
			Shape shape = ObjectInformation.CreateShape(storeObject);
			return new ToXmlPropertyList(shape, responseShape);
		}

		// Token: 0x06000BCD RID: 3021 RVA: 0x0003C154 File Offset: 0x0003A354
		public static ToServiceObjectPropertyList CreateToServiceObjectPropertyList(ResponseShape responseShape, StoreObject storeObject)
		{
			Shape shape = ObjectInformation.CreateShape(storeObject);
			return new ToServiceObjectPropertyList(shape, responseShape, StaticParticipantResolver.DefaultInstance);
		}

		// Token: 0x06000BCE RID: 3022 RVA: 0x0003C174 File Offset: 0x0003A374
		public static ToServiceObjectPropertyListInMemory CreateToServiceObjectPropertyListInMemory(ResponseShape responseShape, StoreObject storeObject)
		{
			Shape shape = ObjectInformation.CreateShape(storeObject);
			return new ToServiceObjectPropertyListInMemory(shape, responseShape);
		}

		// Token: 0x06000BCF RID: 3023 RVA: 0x0003C190 File Offset: 0x0003A390
		public static ToXmlPropertyList CreateToXmlPropertyList(ResponseShape responseShape, ObjectInformation objectInformation)
		{
			Shape shape = objectInformation.CreateShape(false);
			return new ToXmlPropertyList(shape, responseShape);
		}

		// Token: 0x06000BD0 RID: 3024 RVA: 0x0003C1AC File Offset: 0x0003A3AC
		public static ToServiceObjectPropertyList CreateToServiceObjectPropertyList(ResponseShape responseShape, ObjectInformation objectInformation, IParticipantResolver participantResolver)
		{
			Shape shape = objectInformation.CreateShape(false);
			return new ToServiceObjectPropertyList(shape, responseShape, participantResolver);
		}

		// Token: 0x06000BD1 RID: 3025 RVA: 0x0003C1CC File Offset: 0x0003A3CC
		public static ToXmlForPropertyBagPropertyList CreateToXmlForPropertyBagPropertyList(ResponseShape responseShape, ObjectInformation objectInformation)
		{
			Shape shape = objectInformation.CreateShape(true);
			return new ToXmlForPropertyBagPropertyList(shape, responseShape);
		}

		// Token: 0x06000BD2 RID: 3026 RVA: 0x0003C1E8 File Offset: 0x0003A3E8
		public static ToServiceObjectForPropertyBagPropertyList CreateToServiceObjectForPropertyBagPropertyList(ResponseShape responseShape, ObjectInformation objectInformation)
		{
			Shape shape = objectInformation.CreateShape(true);
			return new ToServiceObjectForPropertyBagPropertyList(shape, responseShape);
		}

		// Token: 0x06000BD3 RID: 3027 RVA: 0x0003C204 File Offset: 0x0003A404
		public static ToXmlForPropertyBagUsingStoreObject CreateToXmlForPropertyBagUsingStoreObject(ResponseShape responseShape, ObjectInformation objectInformation)
		{
			Shape shape = objectInformation.CreateShape(true);
			return new ToXmlForPropertyBagUsingStoreObject(shape, responseShape);
		}

		// Token: 0x06000BD4 RID: 3028 RVA: 0x0003C220 File Offset: 0x0003A420
		public static ToServiceObjectForPropertyBagUsingStoreObject CreateToServiceObjectForPropertyBagUsingStoreObject(ResponseShape responseShape, ObjectInformation objectInformation, IParticipantResolver participantResolver)
		{
			Shape shape = objectInformation.CreateShape(true);
			return new ToServiceObjectForPropertyBagUsingStoreObject(shape, responseShape, participantResolver);
		}

		// Token: 0x06000BD5 RID: 3029 RVA: 0x0003C240 File Offset: 0x0003A440
		public static IList<ISetCommand> CreateSetPropertyCommands(ServiceObject serviceObject, StoreObject storeObject, IdConverter idConverter)
		{
			Shape shape = ObjectInformation.CreateShape(storeObject);
			SetPropertyList setPropertyList = new SetPropertyList(shape, serviceObject, storeObject, idConverter);
			return setPropertyList.CreatePropertyCommands();
		}

		// Token: 0x06000BD6 RID: 3030 RVA: 0x0003C264 File Offset: 0x0003A464
		public static IList<IUpdateCommand> CreateUpdatePropertyCommands(PropertyUpdate[] propertyUpdates, StoreObject storeObject, IdConverter idConverter, bool suppressReadReceipts, IFeaturesManager featuresManager)
		{
			Shape shape = ObjectInformation.CreateShape(storeObject);
			UpdatePropertyList updatePropertyList = new UpdatePropertyList(shape, propertyUpdates, storeObject, idConverter, suppressReadReceipts, featuresManager);
			return updatePropertyList.CreatePropertyCommands();
		}

		// Token: 0x06000BD7 RID: 3031 RVA: 0x0003C28C File Offset: 0x0003A48C
		public static IList<ISetCommand> CreateSetPropertyCommands(XmlElement serviceItem, StoreObject storeObject, IdConverter idConverter)
		{
			Shape shape = ObjectInformation.CreateShape(storeObject);
			SetPropertyList setPropertyList = new SetPropertyList(shape, serviceItem, storeObject, idConverter);
			return setPropertyList.CreatePropertyCommands();
		}

		// Token: 0x04000968 RID: 2408
		protected Shape shape;
	}
}
