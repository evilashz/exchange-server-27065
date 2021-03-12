using System;
using System.IO;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x02000269 RID: 617
	internal sealed class RecipientInfoCacheSyncItemId : ISyncItemId, ICustomSerializableBuilder, ICustomSerializable
	{
		// Token: 0x060016E7 RID: 5863 RVA: 0x00089ADE File Offset: 0x00087CDE
		public RecipientInfoCacheSyncItemId()
		{
		}

		// Token: 0x060016E8 RID: 5864 RVA: 0x00089AED File Offset: 0x00087CED
		public RecipientInfoCacheSyncItemId(int cacheEntryId)
		{
			if (cacheEntryId <= 0)
			{
				throw new ArgumentException("cacheEntryId is less than or equal to 0");
			}
			this.cacheEntryId = cacheEntryId;
		}

		// Token: 0x170007D2 RID: 2002
		// (get) Token: 0x060016E9 RID: 5865 RVA: 0x00089B12 File Offset: 0x00087D12
		public object NativeId
		{
			get
			{
				return this.cacheEntryId;
			}
		}

		// Token: 0x170007D3 RID: 2003
		// (get) Token: 0x060016EA RID: 5866 RVA: 0x00089B1F File Offset: 0x00087D1F
		public int CacheEntryId
		{
			get
			{
				return this.cacheEntryId;
			}
		}

		// Token: 0x170007D4 RID: 2004
		// (get) Token: 0x060016EB RID: 5867 RVA: 0x00089B27 File Offset: 0x00087D27
		// (set) Token: 0x060016EC RID: 5868 RVA: 0x00089B2E File Offset: 0x00087D2E
		public ushort TypeId
		{
			get
			{
				return RecipientInfoCacheSyncItemId.typeId;
			}
			set
			{
				RecipientInfoCacheSyncItemId.typeId = value;
			}
		}

		// Token: 0x060016ED RID: 5869 RVA: 0x00089B36 File Offset: 0x00087D36
		public ICustomSerializable BuildObject()
		{
			return new RecipientInfoCacheSyncItemId();
		}

		// Token: 0x060016EE RID: 5870 RVA: 0x00089B40 File Offset: 0x00087D40
		public void DeserializeData(BinaryReader reader, ComponentDataPool componentDataPool)
		{
			Int32Data int32DataInstance = componentDataPool.GetInt32DataInstance();
			int32DataInstance.DeserializeData(reader, componentDataPool);
			this.cacheEntryId = int32DataInstance.Data;
		}

		// Token: 0x060016EF RID: 5871 RVA: 0x00089B68 File Offset: 0x00087D68
		public void SerializeData(BinaryWriter writer, ComponentDataPool componentDataPool)
		{
			componentDataPool.GetInt32DataInstance().Bind(this.cacheEntryId).SerializeData(writer, componentDataPool);
		}

		// Token: 0x060016F0 RID: 5872 RVA: 0x00089B84 File Offset: 0x00087D84
		public override bool Equals(object syncItemId)
		{
			RecipientInfoCacheSyncItemId recipientInfoCacheSyncItemId = syncItemId as RecipientInfoCacheSyncItemId;
			return recipientInfoCacheSyncItemId != null && this.cacheEntryId == recipientInfoCacheSyncItemId.cacheEntryId;
		}

		// Token: 0x060016F1 RID: 5873 RVA: 0x00089BAB File Offset: 0x00087DAB
		public override int GetHashCode()
		{
			return this.cacheEntryId.GetHashCode();
		}

		// Token: 0x04000E1A RID: 3610
		private static ushort typeId;

		// Token: 0x04000E1B RID: 3611
		private int cacheEntryId = -1;
	}
}
