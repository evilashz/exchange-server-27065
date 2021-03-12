using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Entities.TypeConversion.Converters;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x0200026F RID: 623
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class EntitySyncItemId : MailboxSyncItemId
	{
		// Token: 0x06001730 RID: 5936 RVA: 0x0008A85A File Offset: 0x00088A5A
		public EntitySyncItemId()
		{
		}

		// Token: 0x06001731 RID: 5937 RVA: 0x0008A862 File Offset: 0x00088A62
		protected EntitySyncItemId(StoreObjectId id) : base(id)
		{
		}

		// Token: 0x170007E0 RID: 2016
		// (get) Token: 0x06001732 RID: 5938 RVA: 0x0008A86B File Offset: 0x00088A6B
		// (set) Token: 0x06001733 RID: 5939 RVA: 0x0008A872 File Offset: 0x00088A72
		public override ushort TypeId
		{
			get
			{
				return EntitySyncItemId.typeId;
			}
			set
			{
				EntitySyncItemId.typeId = value;
			}
		}

		// Token: 0x170007E1 RID: 2017
		// (get) Token: 0x06001734 RID: 5940 RVA: 0x0008A87A File Offset: 0x00088A7A
		// (set) Token: 0x06001735 RID: 5941 RVA: 0x0008A882 File Offset: 0x00088A82
		public string UID { get; set; }

		// Token: 0x06001736 RID: 5942 RVA: 0x0008A88B File Offset: 0x00088A8B
		public static EntitySyncItemId CreateFromId(StoreId storeId)
		{
			return new EntitySyncItemId(StoreId.GetStoreObjectId(storeId));
		}

		// Token: 0x06001737 RID: 5943 RVA: 0x0008A898 File Offset: 0x00088A98
		public static string GetEntityID(StoreId storeId)
		{
			return IdConverter.Instance.ToStringId(StoreId.GetStoreObjectId(storeId), Command.CurrentCommand.MailboxSession);
		}

		// Token: 0x06001738 RID: 5944 RVA: 0x0008A8B4 File Offset: 0x00088AB4
		public static AttachmentId GetAttachmentId(string attachmentId)
		{
			IList<AttachmentId> attachmentIds = IdConverter.GetAttachmentIds(attachmentId);
			if (attachmentIds.Count > 0)
			{
				return attachmentIds[0];
			}
			return null;
		}

		// Token: 0x06001739 RID: 5945 RVA: 0x0008A8DA File Offset: 0x00088ADA
		public override ICustomSerializable BuildObject()
		{
			return new EntitySyncItemId();
		}

		// Token: 0x04000E30 RID: 3632
		private static ushort typeId;
	}
}
