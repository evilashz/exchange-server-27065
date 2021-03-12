using System;
using System.IO;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000E0F RID: 3599
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class FolderStateEntry : ICustomSerializableBuilder, ICustomSerializable
	{
		// Token: 0x06007C23 RID: 31779 RVA: 0x00223B8E File Offset: 0x00221D8E
		public FolderStateEntry()
		{
		}

		// Token: 0x06007C24 RID: 31780 RVA: 0x00223B96 File Offset: 0x00221D96
		public FolderStateEntry(StoreObjectId parentId, byte[] changeKey, int changeTrackingHash)
		{
			this.parentId = parentId;
			this.changeKey = changeKey;
			this.changeTrackingHash = changeTrackingHash;
		}

		// Token: 0x17002139 RID: 8505
		// (get) Token: 0x06007C25 RID: 31781 RVA: 0x00223BB3 File Offset: 0x00221DB3
		// (set) Token: 0x06007C26 RID: 31782 RVA: 0x00223BBA File Offset: 0x00221DBA
		public ushort TypeId
		{
			get
			{
				return FolderStateEntry.typeId;
			}
			set
			{
				FolderStateEntry.typeId = value;
			}
		}

		// Token: 0x1700213A RID: 8506
		// (get) Token: 0x06007C27 RID: 31783 RVA: 0x00223BC2 File Offset: 0x00221DC2
		// (set) Token: 0x06007C28 RID: 31784 RVA: 0x00223BCA File Offset: 0x00221DCA
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

		// Token: 0x1700213B RID: 8507
		// (get) Token: 0x06007C29 RID: 31785 RVA: 0x00223BD3 File Offset: 0x00221DD3
		// (set) Token: 0x06007C2A RID: 31786 RVA: 0x00223BDB File Offset: 0x00221DDB
		public int ChangeTrackingHash
		{
			get
			{
				return this.changeTrackingHash;
			}
			set
			{
				this.changeTrackingHash = value;
			}
		}

		// Token: 0x1700213C RID: 8508
		// (get) Token: 0x06007C2B RID: 31787 RVA: 0x00223BE4 File Offset: 0x00221DE4
		// (set) Token: 0x06007C2C RID: 31788 RVA: 0x00223BEC File Offset: 0x00221DEC
		public StoreObjectId ParentId
		{
			get
			{
				return this.parentId;
			}
			set
			{
				this.parentId = value;
			}
		}

		// Token: 0x06007C2D RID: 31789 RVA: 0x00223BF8 File Offset: 0x00221DF8
		public static explicit operator FolderStateEntry(FolderManifestEntry manifestEntry)
		{
			return new FolderStateEntry(manifestEntry.ParentId, manifestEntry.ChangeKey, manifestEntry.ChangeTrackingHash);
		}

		// Token: 0x06007C2E RID: 31790 RVA: 0x00223C1E File Offset: 0x00221E1E
		public ICustomSerializable BuildObject()
		{
			return new FolderStateEntry();
		}

		// Token: 0x06007C2F RID: 31791 RVA: 0x00223C28 File Offset: 0x00221E28
		public void DeserializeData(BinaryReader reader, ComponentDataPool componentDataPool)
		{
			StoreObjectIdData storeObjectIdDataInstance = componentDataPool.GetStoreObjectIdDataInstance();
			storeObjectIdDataInstance.DeserializeData(reader, componentDataPool);
			this.parentId = storeObjectIdDataInstance.Data;
			ByteArrayData byteArrayInstance = componentDataPool.GetByteArrayInstance();
			byteArrayInstance.DeserializeData(reader, componentDataPool);
			this.changeKey = byteArrayInstance.Data;
			this.changeTrackingHash = reader.ReadInt32();
		}

		// Token: 0x06007C30 RID: 31792 RVA: 0x00223C77 File Offset: 0x00221E77
		public void SerializeData(BinaryWriter writer, ComponentDataPool componentDataPool)
		{
			componentDataPool.GetStoreObjectIdDataInstance().Bind(this.parentId).SerializeData(writer, componentDataPool);
			componentDataPool.GetByteArrayInstance().Bind(this.changeKey).SerializeData(writer, componentDataPool);
			writer.Write(this.changeTrackingHash);
		}

		// Token: 0x0400550B RID: 21771
		private static ushort typeId;

		// Token: 0x0400550C RID: 21772
		private byte[] changeKey;

		// Token: 0x0400550D RID: 21773
		private int changeTrackingHash;

		// Token: 0x0400550E RID: 21774
		private StoreObjectId parentId;
	}
}
