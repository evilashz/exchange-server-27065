using System;
using System.Linq;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000E9C RID: 3740
	internal static class EwsServiceObjectFactory
	{
		// Token: 0x06006157 RID: 24919 RVA: 0x0012F854 File Offset: 0x0012DA54
		public static TEntity CreateEntity<TEntity>(object serviceObject) where TEntity : Entity
		{
			ArgumentValidator.ThrowIfNull("serviceObject", serviceObject);
			Type type = serviceObject.GetType();
			EwsServiceObjectFactory.ServiceObjectTypeMapEntry serviceObjectTypeMapEntry = null;
			foreach (EwsServiceObjectFactory.ServiceObjectTypeMapEntry serviceObjectTypeMapEntry2 in EwsServiceObjectFactory.map)
			{
				if (type.Equals(serviceObjectTypeMapEntry2.ServiceObjectType) || type.IsSubclassOf(serviceObjectTypeMapEntry2.ServiceObjectType))
				{
					serviceObjectTypeMapEntry = serviceObjectTypeMapEntry2;
					break;
				}
			}
			if (serviceObjectTypeMapEntry == null)
			{
				throw new NotSupportedException(string.Format("Service object type {0} not suppported", serviceObject.GetType()));
			}
			return (TEntity)((object)serviceObjectTypeMapEntry.EntityCreator());
		}

		// Token: 0x06006158 RID: 24920 RVA: 0x0012F8FC File Offset: 0x0012DAFC
		public static TServiceObject CreateServiceObject<TServiceObject>(Entity entityObject) where TServiceObject : class
		{
			ArgumentValidator.ThrowIfNull("entityObject", entityObject);
			EwsServiceObjectFactory.ServiceObjectTypeMapEntry serviceObjectTypeMapEntry = EwsServiceObjectFactory.map.FirstOrDefault((EwsServiceObjectFactory.ServiceObjectTypeMapEntry x) => x.EntityType.Equals(entityObject.GetType()));
			if (serviceObjectTypeMapEntry == null)
			{
				throw new NotSupportedException(string.Format("Entity type {0} not suppported", entityObject.GetType()));
			}
			return (TServiceObject)((object)serviceObjectTypeMapEntry.ServiceObjectCreator());
		}

		// Token: 0x06006159 RID: 24921 RVA: 0x0012F9D0 File Offset: 0x0012DBD0
		// Note: this type is marked as 'beforefieldinit'.
		static EwsServiceObjectFactory()
		{
			EwsServiceObjectFactory.ServiceObjectTypeMapEntry[] array = new EwsServiceObjectFactory.ServiceObjectTypeMapEntry[7];
			array[0] = new EwsServiceObjectFactory.ServiceObjectTypeMapEntry(typeof(MessageType), typeof(Message), () => new MessageType(), () => new Message());
			array[1] = new EwsServiceObjectFactory.ServiceObjectTypeMapEntry(typeof(ContactItemType), typeof(Contact), () => new ContactItemType(), () => new Contact());
			array[2] = new EwsServiceObjectFactory.ServiceObjectTypeMapEntry(typeof(ItemType), typeof(Message), () => new ItemType(), () => new Message());
			array[3] = new EwsServiceObjectFactory.ServiceObjectTypeMapEntry(typeof(ContactsFolderType), typeof(ContactFolder), () => new ContactsFolderType(), () => new ContactFolder());
			array[4] = new EwsServiceObjectFactory.ServiceObjectTypeMapEntry(typeof(FolderType), typeof(Folder), () => new FolderType(), () => new Folder());
			array[5] = new EwsServiceObjectFactory.ServiceObjectTypeMapEntry(typeof(FileAttachmentType), typeof(FileAttachment), () => new FileAttachmentType(), () => new FileAttachment());
			array[6] = new EwsServiceObjectFactory.ServiceObjectTypeMapEntry(typeof(ItemAttachmentType), typeof(ItemAttachment), () => new ItemAttachmentType(), () => new ItemAttachment());
			EwsServiceObjectFactory.map = array;
		}

		// Token: 0x040034AA RID: 13482
		private static readonly EwsServiceObjectFactory.ServiceObjectTypeMapEntry[] map;

		// Token: 0x02000E9D RID: 3741
		private class ServiceObjectTypeMapEntry
		{
			// Token: 0x06006168 RID: 24936 RVA: 0x0012FC44 File Offset: 0x0012DE44
			public ServiceObjectTypeMapEntry(Type serviceObjectType, Type entityType, Func<object> serviceObjectCreator, Func<Entity> entityCreator)
			{
				ArgumentValidator.ThrowIfNull("serviceObjectType", serviceObjectType);
				ArgumentValidator.ThrowIfNull("entityType", serviceObjectType);
				ArgumentValidator.ThrowIfNull("serviceObjectCreator", serviceObjectType);
				ArgumentValidator.ThrowIfNull("serviceObjectType", serviceObjectType);
				this.ServiceObjectType = serviceObjectType;
				this.EntityType = entityType;
				this.ServiceObjectCreator = serviceObjectCreator;
				this.EntityCreator = entityCreator;
			}

			// Token: 0x17001660 RID: 5728
			// (get) Token: 0x06006169 RID: 24937 RVA: 0x0012FCA0 File Offset: 0x0012DEA0
			// (set) Token: 0x0600616A RID: 24938 RVA: 0x0012FCA8 File Offset: 0x0012DEA8
			public Type ServiceObjectType { get; private set; }

			// Token: 0x17001661 RID: 5729
			// (get) Token: 0x0600616B RID: 24939 RVA: 0x0012FCB1 File Offset: 0x0012DEB1
			// (set) Token: 0x0600616C RID: 24940 RVA: 0x0012FCB9 File Offset: 0x0012DEB9
			public Type EntityType { get; private set; }

			// Token: 0x17001662 RID: 5730
			// (get) Token: 0x0600616D RID: 24941 RVA: 0x0012FCC2 File Offset: 0x0012DEC2
			// (set) Token: 0x0600616E RID: 24942 RVA: 0x0012FCCA File Offset: 0x0012DECA
			public Func<object> ServiceObjectCreator { get; private set; }

			// Token: 0x17001663 RID: 5731
			// (get) Token: 0x0600616F RID: 24943 RVA: 0x0012FCD3 File Offset: 0x0012DED3
			// (set) Token: 0x06006170 RID: 24944 RVA: 0x0012FCDB File Offset: 0x0012DEDB
			public Func<Entity> EntityCreator { get; private set; }
		}
	}
}
