using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Entities.DataModel;

namespace Microsoft.Exchange.Entities.TypeConversion.Converters
{
	// Token: 0x0200005E RID: 94
	internal class IdConverter
	{
		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000211 RID: 529 RVA: 0x00007768 File Offset: 0x00005968
		public static IdConverter Instance
		{
			get
			{
				return IdConverter.SingleTonInstance;
			}
		}

		// Token: 0x06000212 RID: 530 RVA: 0x00007770 File Offset: 0x00005970
		public static IList<AttachmentId> GetAttachmentIds(string id)
		{
			byte[] buffer = Convert.FromBase64String(id);
			IList<AttachmentId> result;
			using (MemoryStream memoryStream = new MemoryStream(buffer))
			{
				using (BinaryReader binaryReader = new BinaryReader(memoryStream))
				{
					List<AttachmentId> list = new List<AttachmentId>();
					ServiceIdConverter.ReadAttachmentIds(binaryReader, list);
					result = list;
				}
			}
			return result;
		}

		// Token: 0x06000213 RID: 531 RVA: 0x000077D8 File Offset: 0x000059D8
		public static string GetHierarchicalAttachmentStringId(IList<AttachmentId> attachmentIds)
		{
			int requiredByteCountForAttachmentIds = ServiceIdConverter.GetRequiredByteCountForAttachmentIds(attachmentIds);
			byte[] array = new byte[requiredByteCountForAttachmentIds];
			ServiceIdConverter.WriteAttachmentIds(attachmentIds, array, 0);
			return Convert.ToBase64String(array);
		}

		// Token: 0x06000214 RID: 532 RVA: 0x00007804 File Offset: 0x00005A04
		public virtual TEntity CreateBasicEntity<TEntity>(VersionedId objectId, IStoreSession session) where TEntity : IStorageEntity, new()
		{
			string changeKey;
			string id = this.ToStringId(objectId, session, out changeKey);
			TEntity result = (default(TEntity) == null) ? Activator.CreateInstance<TEntity>() : default(TEntity);
			result.Id = id;
			result.ChangeKey = changeKey;
			return result;
		}

		// Token: 0x06000215 RID: 533 RVA: 0x0000785B File Offset: 0x00005A5B
		public virtual StoreId GetStoreId(IStorageEntity entity)
		{
			return this.ToStoreId(entity.Id, entity.ChangeKey);
		}

		// Token: 0x06000216 RID: 534 RVA: 0x00007870 File Offset: 0x00005A70
		public virtual StoreId ToStoreId(string entityId, string changeKey)
		{
			StoreObjectId storeObjectId = this.ToStoreObjectId(entityId);
			if (string.IsNullOrEmpty(changeKey))
			{
				return storeObjectId;
			}
			byte[] changeKey2 = Convert.FromBase64String(changeKey);
			return new VersionedId(storeObjectId, changeKey2);
		}

		// Token: 0x06000217 RID: 535 RVA: 0x0000789D File Offset: 0x00005A9D
		public virtual StoreObjectId ToStoreObjectId(string id)
		{
			return StoreId.EwsIdToStoreObjectId(id);
		}

		// Token: 0x06000218 RID: 536 RVA: 0x000078A8 File Offset: 0x00005AA8
		public virtual string ToStringId(StoreId storeId, IStoreSession session, out string changeKey)
		{
			VersionedId versionedId = storeId as VersionedId;
			changeKey = ((versionedId == null) ? null : versionedId.ChangeKeyAsBase64String());
			return this.ToStringId(storeId, session);
		}

		// Token: 0x06000219 RID: 537 RVA: 0x000078D4 File Offset: 0x00005AD4
		public virtual string ToStringId(StoreId storeId, IStoreSession session)
		{
			if (session is IPublicFolderSession)
			{
				StoreObjectId parentFolderId = session.GetParentFolderId(StoreId.GetStoreObjectId(storeId));
				return StoreId.PublicFolderStoreIdToEwsId(storeId, parentFolderId);
			}
			return StoreId.StoreIdToEwsId(session.MailboxGuid, storeId);
		}

		// Token: 0x0600021A RID: 538 RVA: 0x0000790C File Offset: 0x00005B0C
		public virtual string ToStringId(AttachmentId attachementId)
		{
			return IdConverter.GetHierarchicalAttachmentStringId(new List<AttachmentId>
			{
				attachementId
			});
		}

		// Token: 0x040000A6 RID: 166
		private static readonly IdConverter SingleTonInstance = new IdConverter();
	}
}
