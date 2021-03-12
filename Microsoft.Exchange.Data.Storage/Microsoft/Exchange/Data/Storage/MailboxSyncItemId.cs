using System;
using System.IO;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000E60 RID: 3680
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MailboxSyncItemId : ISyncItemId, ICustomSerializableBuilder, ICustomSerializable
	{
		// Token: 0x06007F6F RID: 32623 RVA: 0x0022E61C File Offset: 0x0022C81C
		public MailboxSyncItemId()
		{
		}

		// Token: 0x06007F70 RID: 32624 RVA: 0x0022E624 File Offset: 0x0022C824
		protected MailboxSyncItemId(StoreObjectId id)
		{
			this.nativeId = id;
		}

		// Token: 0x170021FC RID: 8700
		// (get) Token: 0x06007F71 RID: 32625 RVA: 0x0022E633 File Offset: 0x0022C833
		public object NativeId
		{
			get
			{
				return this.nativeId;
			}
		}

		// Token: 0x170021FD RID: 8701
		// (get) Token: 0x06007F72 RID: 32626 RVA: 0x0022E63B File Offset: 0x0022C83B
		// (set) Token: 0x06007F73 RID: 32627 RVA: 0x0022E643 File Offset: 0x0022C843
		public byte[] ChangeKey
		{
			get
			{
				return this.changeKey;
			}
			set
			{
				this.changeKey = value;
			}
		}

		// Token: 0x170021FE RID: 8702
		// (get) Token: 0x06007F74 RID: 32628 RVA: 0x0022E64C File Offset: 0x0022C84C
		// (set) Token: 0x06007F75 RID: 32629 RVA: 0x0022E653 File Offset: 0x0022C853
		public virtual ushort TypeId
		{
			get
			{
				return MailboxSyncItemId.typeId;
			}
			set
			{
				MailboxSyncItemId.typeId = value;
			}
		}

		// Token: 0x06007F76 RID: 32630 RVA: 0x0022E65C File Offset: 0x0022C85C
		public static MailboxSyncItemId CreateForExistingItem(FolderSync folderSync, StoreObjectId id)
		{
			MailboxSyncItemId mailboxSyncItemId = new MailboxSyncItemId(id);
			if (folderSync == null || folderSync.ClientState.ContainsKey(mailboxSyncItemId))
			{
				return mailboxSyncItemId;
			}
			return null;
		}

		// Token: 0x06007F77 RID: 32631 RVA: 0x0022E684 File Offset: 0x0022C884
		public static MailboxSyncItemId CreateForNewItem(StoreObjectId id)
		{
			return new MailboxSyncItemId(id);
		}

		// Token: 0x06007F78 RID: 32632 RVA: 0x0022E68C File Offset: 0x0022C88C
		public virtual ICustomSerializable BuildObject()
		{
			return new MailboxSyncItemId();
		}

		// Token: 0x06007F79 RID: 32633 RVA: 0x0022E694 File Offset: 0x0022C894
		public void DeserializeData(BinaryReader reader, ComponentDataPool componentDataPool)
		{
			StoreObjectIdData storeObjectIdDataInstance = componentDataPool.GetStoreObjectIdDataInstance();
			storeObjectIdDataInstance.DeserializeData(reader, componentDataPool);
			this.nativeId = storeObjectIdDataInstance.Data;
		}

		// Token: 0x06007F7A RID: 32634 RVA: 0x0022E6BC File Offset: 0x0022C8BC
		public void SerializeData(BinaryWriter writer, ComponentDataPool componentDataPool)
		{
			componentDataPool.GetStoreObjectIdDataInstance().Bind(this.nativeId).SerializeData(writer, componentDataPool);
		}

		// Token: 0x06007F7B RID: 32635 RVA: 0x0022E6D8 File Offset: 0x0022C8D8
		public override bool Equals(object syncItemId)
		{
			MailboxSyncItemId mailboxSyncItemId = syncItemId as MailboxSyncItemId;
			return mailboxSyncItemId != null && this.nativeId.Equals(mailboxSyncItemId.nativeId);
		}

		// Token: 0x06007F7C RID: 32636 RVA: 0x0022E702 File Offset: 0x0022C902
		public override int GetHashCode()
		{
			return this.nativeId.GetHashCode();
		}

		// Token: 0x06007F7D RID: 32637 RVA: 0x0022E70F File Offset: 0x0022C90F
		public override string ToString()
		{
			if (this.nativeId != null)
			{
				return this.nativeId.ToString();
			}
			return "MailboxSyncItemId with null native id";
		}

		// Token: 0x04005642 RID: 22082
		private static ushort typeId;

		// Token: 0x04005643 RID: 22083
		private StoreObjectId nativeId;

		// Token: 0x04005644 RID: 22084
		private byte[] changeKey;
	}
}
