using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x0200026D RID: 621
	internal sealed class RecipientInfoCacheSyncWatermark : ISyncWatermark, ICustomSerializableBuilder, ICustomSerializable, IComparable, ICloneable
	{
		// Token: 0x06001710 RID: 5904 RVA: 0x0008A392 File Offset: 0x00088592
		public RecipientInfoCacheSyncWatermark()
		{
			this.cacheEntryIdToLastUpdateTime = new Dictionary<RecipientInfoCacheSyncItemId, long>(100);
			this.lastModifiedTime = ExDateTime.MinValue;
		}

		// Token: 0x06001711 RID: 5905 RVA: 0x0008A3B2 File Offset: 0x000885B2
		private RecipientInfoCacheSyncWatermark(Dictionary<RecipientInfoCacheSyncItemId, long> entries, ExDateTime lastModifiedTime)
		{
			this.cacheEntryIdToLastUpdateTime = entries;
			this.lastModifiedTime = lastModifiedTime;
		}

		// Token: 0x170007DB RID: 2011
		// (get) Token: 0x06001712 RID: 5906 RVA: 0x0008A3C8 File Offset: 0x000885C8
		public bool IsNew
		{
			get
			{
				return this.lastModifiedTime == ExDateTime.MinValue;
			}
		}

		// Token: 0x170007DC RID: 2012
		// (get) Token: 0x06001713 RID: 5907 RVA: 0x0008A3DA File Offset: 0x000885DA
		// (set) Token: 0x06001714 RID: 5908 RVA: 0x0008A3E2 File Offset: 0x000885E2
		public ExDateTime LastModifiedTime
		{
			get
			{
				return this.lastModifiedTime;
			}
			set
			{
				this.lastModifiedTime = value;
			}
		}

		// Token: 0x170007DD RID: 2013
		// (get) Token: 0x06001715 RID: 5909 RVA: 0x0008A3EB File Offset: 0x000885EB
		// (set) Token: 0x06001716 RID: 5910 RVA: 0x0008A3F2 File Offset: 0x000885F2
		public ushort TypeId
		{
			get
			{
				return RecipientInfoCacheSyncWatermark.typeId;
			}
			set
			{
				RecipientInfoCacheSyncWatermark.typeId = value;
			}
		}

		// Token: 0x170007DE RID: 2014
		// (get) Token: 0x06001717 RID: 5911 RVA: 0x0008A3FA File Offset: 0x000885FA
		internal Dictionary<RecipientInfoCacheSyncItemId, long> Entries
		{
			get
			{
				return this.cacheEntryIdToLastUpdateTime;
			}
		}

		// Token: 0x06001718 RID: 5912 RVA: 0x0008A402 File Offset: 0x00088602
		public static RecipientInfoCacheSyncWatermark Create()
		{
			return new RecipientInfoCacheSyncWatermark();
		}

		// Token: 0x06001719 RID: 5913 RVA: 0x0008A409 File Offset: 0x00088609
		public static RecipientInfoCacheSyncWatermark Create(Dictionary<RecipientInfoCacheSyncItemId, long> dictionary, ExDateTime lastModifiedTime)
		{
			if (dictionary == null)
			{
				throw new ArgumentNullException("dictionary");
			}
			return new RecipientInfoCacheSyncWatermark(dictionary, lastModifiedTime);
		}

		// Token: 0x0600171A RID: 5914 RVA: 0x0008A420 File Offset: 0x00088620
		public static RecipientInfoCacheSyncWatermark Create(List<RecipientInfoCacheEntry> cache, ExDateTime lastModifiedTime)
		{
			Dictionary<RecipientInfoCacheSyncItemId, long> dictionary = new Dictionary<RecipientInfoCacheSyncItemId, long>(cache.Count);
			foreach (RecipientInfoCacheEntry recipientInfoCacheEntry in cache)
			{
				using (RecipientInfoCacheSyncItem recipientInfoCacheSyncItem = RecipientInfoCacheSyncItem.Bind(recipientInfoCacheEntry))
				{
					dictionary[(RecipientInfoCacheSyncItemId)recipientInfoCacheSyncItem.Id] = recipientInfoCacheEntry.DateTimeTicks;
				}
			}
			return new RecipientInfoCacheSyncWatermark(dictionary, lastModifiedTime);
		}

		// Token: 0x0600171B RID: 5915 RVA: 0x0008A4B0 File Offset: 0x000886B0
		public ICustomSerializable BuildObject()
		{
			return new RecipientInfoCacheSyncWatermark();
		}

		// Token: 0x0600171C RID: 5916 RVA: 0x0008A4B8 File Offset: 0x000886B8
		public void DeserializeData(BinaryReader reader, ComponentDataPool componentDataPool)
		{
			GenericDictionaryData<DerivedData<RecipientInfoCacheSyncItemId>, RecipientInfoCacheSyncItemId, Int64Data, long> genericDictionaryData = new GenericDictionaryData<DerivedData<RecipientInfoCacheSyncItemId>, RecipientInfoCacheSyncItemId, Int64Data, long>();
			genericDictionaryData.DeserializeData(reader, componentDataPool);
			this.cacheEntryIdToLastUpdateTime = genericDictionaryData.Data;
			DateTimeData dateTimeData = new DateTimeData();
			dateTimeData.DeserializeData(reader, componentDataPool);
			this.lastModifiedTime = dateTimeData.Data;
		}

		// Token: 0x0600171D RID: 5917 RVA: 0x0008A4FC File Offset: 0x000886FC
		public void SerializeData(BinaryWriter writer, ComponentDataPool componentDataPool)
		{
			GenericDictionaryData<DerivedData<RecipientInfoCacheSyncItemId>, RecipientInfoCacheSyncItemId, Int64Data, long> genericDictionaryData = new GenericDictionaryData<DerivedData<RecipientInfoCacheSyncItemId>, RecipientInfoCacheSyncItemId, Int64Data, long>();
			genericDictionaryData.Bind(this.cacheEntryIdToLastUpdateTime).SerializeData(writer, componentDataPool);
			DateTimeData dateTimeData = new DateTimeData(this.LastModifiedTime);
			dateTimeData.SerializeData(writer, componentDataPool);
		}

		// Token: 0x0600171E RID: 5918 RVA: 0x0008A538 File Offset: 0x00088738
		public int CompareTo(object thatObject)
		{
			RecipientInfoCacheSyncWatermark recipientInfoCacheSyncWatermark = (RecipientInfoCacheSyncWatermark)thatObject;
			return this.lastModifiedTime.CompareTo(recipientInfoCacheSyncWatermark.lastModifiedTime);
		}

		// Token: 0x0600171F RID: 5919 RVA: 0x0008A560 File Offset: 0x00088760
		public override bool Equals(object thatObject)
		{
			RecipientInfoCacheSyncWatermark recipientInfoCacheSyncWatermark = thatObject as RecipientInfoCacheSyncWatermark;
			return thatObject != null && this.lastModifiedTime == recipientInfoCacheSyncWatermark.lastModifiedTime;
		}

		// Token: 0x06001720 RID: 5920 RVA: 0x0008A58A File Offset: 0x0008878A
		public override int GetHashCode()
		{
			throw new NotImplementedException("RecipientInfoCacheSyncWatermark.GetHashCode()");
		}

		// Token: 0x06001721 RID: 5921 RVA: 0x0008A598 File Offset: 0x00088798
		public object Clone()
		{
			Dictionary<RecipientInfoCacheSyncItemId, long> dictionary = new Dictionary<RecipientInfoCacheSyncItemId, long>(this.cacheEntryIdToLastUpdateTime.Count);
			foreach (KeyValuePair<RecipientInfoCacheSyncItemId, long> keyValuePair in this.cacheEntryIdToLastUpdateTime)
			{
				dictionary[keyValuePair.Key] = keyValuePair.Value;
			}
			return new RecipientInfoCacheSyncWatermark(dictionary, this.lastModifiedTime);
		}

		// Token: 0x04000E2B RID: 3627
		private static ushort typeId;

		// Token: 0x04000E2C RID: 3628
		private Dictionary<RecipientInfoCacheSyncItemId, long> cacheEntryIdToLastUpdateTime;

		// Token: 0x04000E2D RID: 3629
		private ExDateTime lastModifiedTime;
	}
}
