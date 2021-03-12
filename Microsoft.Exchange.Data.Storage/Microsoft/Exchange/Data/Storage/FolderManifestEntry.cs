using System;
using System.IO;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000E0C RID: 3596
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class FolderManifestEntry : ICustomSerializable
	{
		// Token: 0x06007C00 RID: 31744 RVA: 0x002238BD File Offset: 0x00221ABD
		public FolderManifestEntry()
		{
		}

		// Token: 0x06007C01 RID: 31745 RVA: 0x002238C5 File Offset: 0x00221AC5
		public FolderManifestEntry(StoreObjectId itemId)
		{
			this.internalItemId = itemId;
			this.changeTrackingHash = -1;
		}

		// Token: 0x1700212A RID: 8490
		// (get) Token: 0x06007C02 RID: 31746 RVA: 0x002238DB File Offset: 0x00221ADB
		public StoreObjectId ItemId
		{
			get
			{
				return this.internalItemId;
			}
		}

		// Token: 0x1700212B RID: 8491
		// (get) Token: 0x06007C03 RID: 31747 RVA: 0x002238E3 File Offset: 0x00221AE3
		// (set) Token: 0x06007C04 RID: 31748 RVA: 0x002238EB File Offset: 0x00221AEB
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

		// Token: 0x1700212C RID: 8492
		// (get) Token: 0x06007C05 RID: 31749 RVA: 0x002238F4 File Offset: 0x00221AF4
		// (set) Token: 0x06007C06 RID: 31750 RVA: 0x002238FC File Offset: 0x00221AFC
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

		// Token: 0x1700212D RID: 8493
		// (get) Token: 0x06007C07 RID: 31751 RVA: 0x00223905 File Offset: 0x00221B05
		// (set) Token: 0x06007C08 RID: 31752 RVA: 0x0022390D File Offset: 0x00221B0D
		public ChangeType ChangeType
		{
			get
			{
				return this.changeType;
			}
			set
			{
				EnumValidator.ThrowIfInvalid<ChangeType>(value, "value");
				this.changeType = value;
			}
		}

		// Token: 0x1700212E RID: 8494
		// (get) Token: 0x06007C09 RID: 31753 RVA: 0x00223921 File Offset: 0x00221B21
		// (set) Token: 0x06007C0A RID: 31754 RVA: 0x00223929 File Offset: 0x00221B29
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

		// Token: 0x1700212F RID: 8495
		// (get) Token: 0x06007C0B RID: 31755 RVA: 0x00223932 File Offset: 0x00221B32
		// (set) Token: 0x06007C0C RID: 31756 RVA: 0x0022393A File Offset: 0x00221B3A
		public SyncPermissions Permissions
		{
			get
			{
				return this.permissions;
			}
			set
			{
				EnumValidator.ThrowIfInvalid<SyncPermissions>(value, "value");
				this.permissions = value;
			}
		}

		// Token: 0x17002130 RID: 8496
		// (get) Token: 0x06007C0D RID: 31757 RVA: 0x0022394E File Offset: 0x00221B4E
		// (set) Token: 0x06007C0E RID: 31758 RVA: 0x00223956 File Offset: 0x00221B56
		public string Owner { get; set; }

		// Token: 0x17002131 RID: 8497
		// (get) Token: 0x06007C0F RID: 31759 RVA: 0x0022395F File Offset: 0x00221B5F
		// (set) Token: 0x06007C10 RID: 31760 RVA: 0x00223967 File Offset: 0x00221B67
		public bool Hidden { get; set; }

		// Token: 0x17002132 RID: 8498
		// (get) Token: 0x06007C11 RID: 31761 RVA: 0x00223970 File Offset: 0x00221B70
		// (set) Token: 0x06007C12 RID: 31762 RVA: 0x00223978 File Offset: 0x00221B78
		public string DisplayName { get; set; }

		// Token: 0x17002133 RID: 8499
		// (get) Token: 0x06007C13 RID: 31763 RVA: 0x00223981 File Offset: 0x00221B81
		// (set) Token: 0x06007C14 RID: 31764 RVA: 0x00223989 File Offset: 0x00221B89
		public string ClassName { get; set; }

		// Token: 0x06007C15 RID: 31765 RVA: 0x00223994 File Offset: 0x00221B94
		public void DeserializeData(BinaryReader reader, ComponentDataPool componentDataPool)
		{
			StoreObjectIdData storeObjectIdDataInstance = componentDataPool.GetStoreObjectIdDataInstance();
			storeObjectIdDataInstance.DeserializeData(reader, componentDataPool);
			this.internalItemId = storeObjectIdDataInstance.Data;
			storeObjectIdDataInstance.DeserializeData(reader, componentDataPool);
			this.parentId = storeObjectIdDataInstance.Data;
			this.changeType = (ChangeType)reader.ReadInt32();
			ByteArrayData byteArrayInstance = componentDataPool.GetByteArrayInstance();
			byteArrayInstance.DeserializeData(reader, componentDataPool);
			this.changeKey = byteArrayInstance.Data;
			this.changeTrackingHash = reader.ReadInt32();
			if (componentDataPool.InternalVersion > 1)
			{
				Int32Data int32DataInstance = componentDataPool.GetInt32DataInstance();
				int32DataInstance.DeserializeData(reader, componentDataPool);
				this.Permissions = (SyncPermissions)int32DataInstance.Data;
				StringData stringDataInstance = componentDataPool.GetStringDataInstance();
				stringDataInstance.DeserializeData(reader, componentDataPool);
				this.Owner = stringDataInstance.Data;
				BooleanData booleanDataInstance = componentDataPool.GetBooleanDataInstance();
				booleanDataInstance.DeserializeData(reader, componentDataPool);
				this.Hidden = booleanDataInstance.Data;
			}
		}

		// Token: 0x06007C16 RID: 31766 RVA: 0x00223A60 File Offset: 0x00221C60
		public void SerializeData(BinaryWriter writer, ComponentDataPool componentDataPool)
		{
			componentDataPool.GetStoreObjectIdDataInstance().Bind(this.internalItemId).SerializeData(writer, componentDataPool);
			componentDataPool.GetStoreObjectIdDataInstance().Bind(this.parentId).SerializeData(writer, componentDataPool);
			writer.Write((int)this.changeType);
			componentDataPool.GetByteArrayInstance().Bind(this.changeKey).SerializeData(writer, componentDataPool);
			writer.Write(this.changeTrackingHash);
			componentDataPool.GetInt32DataInstance().Bind((int)this.Permissions).SerializeData(writer, componentDataPool);
			componentDataPool.GetStringDataInstance().Bind(this.Owner).SerializeData(writer, componentDataPool);
			componentDataPool.GetBooleanDataInstance().Bind(this.Hidden).SerializeData(writer, componentDataPool);
		}

		// Token: 0x040054FD RID: 21757
		private byte[] changeKey;

		// Token: 0x040054FE RID: 21758
		private int changeTrackingHash;

		// Token: 0x040054FF RID: 21759
		private ChangeType changeType;

		// Token: 0x04005500 RID: 21760
		private StoreObjectId parentId;

		// Token: 0x04005501 RID: 21761
		private StoreObjectId internalItemId;

		// Token: 0x04005502 RID: 21762
		private SyncPermissions permissions;
	}
}
