using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Sync.Common;

namespace Microsoft.Exchange.MailboxTransport.ContentAggregation
{
	// Token: 0x02000223 RID: 547
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class UnifiedCustomSyncStateItem : ICustomSerializableBuilder, ICustomSerializable
	{
		// Token: 0x060013A0 RID: 5024 RVA: 0x00042D2C File Offset: 0x00040F2C
		internal UnifiedCustomSyncStateItem() : this(short.MinValue)
		{
		}

		// Token: 0x060013A1 RID: 5025 RVA: 0x00042D39 File Offset: 0x00040F39
		internal UnifiedCustomSyncStateItem(short version)
		{
			this.version = version;
		}

		// Token: 0x060013A2 RID: 5026 RVA: 0x00042D48 File Offset: 0x00040F48
		internal UnifiedCustomSyncStateItem(StoreObjectId nativeId, byte[] changeKey, StoreObjectId nativeFolderId, string cloudId, string cloudFolderId, string cloudVersion, Dictionary<string, string> properties, short version) : this(version)
		{
			SyncUtilities.ThrowIfArgumentNull("cloudId", cloudId);
			this.nativeId = nativeId;
			this.changeKey = changeKey;
			this.nativeFolderId = nativeFolderId;
			this.cloudId = cloudId;
			this.cloudFolderId = cloudFolderId;
			this.cloudVersion = cloudVersion;
			this.properties = properties;
		}

		// Token: 0x170006AC RID: 1708
		// (get) Token: 0x060013A3 RID: 5027 RVA: 0x00042D9E File Offset: 0x00040F9E
		// (set) Token: 0x060013A4 RID: 5028 RVA: 0x00042DA6 File Offset: 0x00040FA6
		public StoreObjectId NativeFolderId
		{
			get
			{
				return this.nativeFolderId;
			}
			set
			{
				this.nativeFolderId = value;
			}
		}

		// Token: 0x170006AD RID: 1709
		// (get) Token: 0x060013A5 RID: 5029 RVA: 0x00042DAF File Offset: 0x00040FAF
		// (set) Token: 0x060013A6 RID: 5030 RVA: 0x00042DB7 File Offset: 0x00040FB7
		public StoreObjectId NativeId
		{
			get
			{
				return this.nativeId;
			}
			set
			{
				this.nativeId = value;
			}
		}

		// Token: 0x170006AE RID: 1710
		// (get) Token: 0x060013A7 RID: 5031 RVA: 0x00042DC0 File Offset: 0x00040FC0
		// (set) Token: 0x060013A8 RID: 5032 RVA: 0x00042DC8 File Offset: 0x00040FC8
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

		// Token: 0x170006AF RID: 1711
		// (get) Token: 0x060013A9 RID: 5033 RVA: 0x00042DD1 File Offset: 0x00040FD1
		// (set) Token: 0x060013AA RID: 5034 RVA: 0x00042DD9 File Offset: 0x00040FD9
		public string CloudId
		{
			get
			{
				return this.cloudId;
			}
			set
			{
				this.cloudId = value;
			}
		}

		// Token: 0x170006B0 RID: 1712
		// (get) Token: 0x060013AB RID: 5035 RVA: 0x00042DE2 File Offset: 0x00040FE2
		// (set) Token: 0x060013AC RID: 5036 RVA: 0x00042DEA File Offset: 0x00040FEA
		public string CloudFolderId
		{
			get
			{
				return this.cloudFolderId;
			}
			set
			{
				this.cloudFolderId = value;
			}
		}

		// Token: 0x170006B1 RID: 1713
		// (get) Token: 0x060013AD RID: 5037 RVA: 0x00042DF3 File Offset: 0x00040FF3
		// (set) Token: 0x060013AE RID: 5038 RVA: 0x00042DFB File Offset: 0x00040FFB
		public string CloudVersion
		{
			get
			{
				return this.cloudVersion;
			}
			set
			{
				this.cloudVersion = value;
			}
		}

		// Token: 0x170006B2 RID: 1714
		// (get) Token: 0x060013AF RID: 5039 RVA: 0x00042E04 File Offset: 0x00041004
		// (set) Token: 0x060013B0 RID: 5040 RVA: 0x00042E0C File Offset: 0x0004100C
		public Dictionary<string, string> Properties
		{
			get
			{
				return this.properties;
			}
			set
			{
				this.properties = value;
			}
		}

		// Token: 0x170006B3 RID: 1715
		// (get) Token: 0x060013B1 RID: 5041 RVA: 0x00042E15 File Offset: 0x00041015
		// (set) Token: 0x060013B2 RID: 5042 RVA: 0x00042E1C File Offset: 0x0004101C
		public ushort TypeId
		{
			get
			{
				return UnifiedCustomSyncStateItem.typeId;
			}
			set
			{
				UnifiedCustomSyncStateItem.typeId = value;
			}
		}

		// Token: 0x060013B3 RID: 5043 RVA: 0x00042E24 File Offset: 0x00041024
		public ICustomSerializable BuildObject()
		{
			return new UnifiedCustomSyncStateItem();
		}

		// Token: 0x060013B4 RID: 5044 RVA: 0x00042E2C File Offset: 0x0004102C
		public void SerializeData(BinaryWriter writer, ComponentDataPool componentDataPool)
		{
			componentDataPool.GetStoreObjectIdDataInstance().Bind(this.nativeId).SerializeData(writer, componentDataPool);
			componentDataPool.GetByteArrayInstance().Bind(this.changeKey).SerializeData(writer, componentDataPool);
			componentDataPool.GetStoreObjectIdDataInstance().Bind(this.nativeFolderId).SerializeData(writer, componentDataPool);
			componentDataPool.GetStringDataInstance().Bind(this.cloudId).SerializeData(writer, componentDataPool);
			componentDataPool.GetStringDataInstance().Bind(this.cloudFolderId).SerializeData(writer, componentDataPool);
			componentDataPool.GetStringDataInstance().Bind(this.cloudVersion).SerializeData(writer, componentDataPool);
			new GenericDictionaryData<StringData, string, StringData, string>(this.properties).SerializeData(writer, componentDataPool);
		}

		// Token: 0x060013B5 RID: 5045 RVA: 0x00042EDC File Offset: 0x000410DC
		public void DeserializeData(BinaryReader reader, ComponentDataPool componentDataPool)
		{
			StoreObjectIdData storeObjectIdDataInstance = componentDataPool.GetStoreObjectIdDataInstance();
			storeObjectIdDataInstance.DeserializeData(reader, componentDataPool);
			this.nativeId = storeObjectIdDataInstance.Data;
			if (this.version > 0)
			{
				ByteArrayData byteArrayInstance = componentDataPool.GetByteArrayInstance();
				byteArrayInstance.DeserializeData(reader, componentDataPool);
				this.changeKey = byteArrayInstance.Data;
			}
			storeObjectIdDataInstance.DeserializeData(reader, componentDataPool);
			this.nativeFolderId = storeObjectIdDataInstance.Data;
			StringData stringDataInstance = componentDataPool.GetStringDataInstance();
			stringDataInstance.DeserializeData(reader, componentDataPool);
			this.cloudId = stringDataInstance.Data;
			stringDataInstance.DeserializeData(reader, componentDataPool);
			this.cloudFolderId = stringDataInstance.Data;
			stringDataInstance.DeserializeData(reader, componentDataPool);
			this.cloudVersion = stringDataInstance.Data;
			if (this.version <= 2)
			{
				DateTimeData dateTimeDataInstance = componentDataPool.GetDateTimeDataInstance();
				dateTimeDataInstance.DeserializeData(reader, componentDataPool);
			}
			if (this.version >= 3)
			{
				GenericDictionaryData<StringData, string, StringData, string> genericDictionaryData = new GenericDictionaryData<StringData, string, StringData, string>();
				genericDictionaryData.DeserializeData(reader, componentDataPool);
				this.properties = genericDictionaryData.Data;
			}
			if (this.properties == null)
			{
				this.properties = new Dictionary<string, string>(3);
			}
		}

		// Token: 0x04000A62 RID: 2658
		private const int DefaultEstimatePropertyCapacity = 3;

		// Token: 0x04000A63 RID: 2659
		private static ushort typeId;

		// Token: 0x04000A64 RID: 2660
		private StoreObjectId nativeFolderId;

		// Token: 0x04000A65 RID: 2661
		private StoreObjectId nativeId;

		// Token: 0x04000A66 RID: 2662
		private byte[] changeKey;

		// Token: 0x04000A67 RID: 2663
		private string cloudId;

		// Token: 0x04000A68 RID: 2664
		private string cloudFolderId;

		// Token: 0x04000A69 RID: 2665
		private string cloudVersion;

		// Token: 0x04000A6A RID: 2666
		private Dictionary<string, string> properties;

		// Token: 0x04000A6B RID: 2667
		private short version;
	}
}
