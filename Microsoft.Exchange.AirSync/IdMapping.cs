using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x0200009E RID: 158
	internal abstract class IdMapping : IIdMapping, ICustomSerializableBuilder, ICustomSerializable
	{
		// Token: 0x060008D3 RID: 2259 RVA: 0x00034F18 File Offset: 0x00033118
		public IdMapping()
		{
			this.syncIdToMailboxIdTable = new Dictionary<string, ISyncItemId>();
			this.mailboxIdToSyncIdTable = new Dictionary<ISyncItemId, string>();
			this.deletedItems = new List<string>();
			this.addedItems = new List<string>();
			this.uniqueCounter = 1L;
			this.oldIds = new Dictionary<ISyncItemId, string>();
		}

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x060008D4 RID: 2260 RVA: 0x00034F6A File Offset: 0x0003316A
		public bool IsDirty
		{
			get
			{
				return this.dirty;
			}
		}

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x060008D5 RID: 2261 RVA: 0x00034F72 File Offset: 0x00033172
		public IDictionaryEnumerator MailboxIdIdEnumerator
		{
			get
			{
				return this.mailboxIdToSyncIdTable.GetEnumerator();
			}
		}

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x060008D6 RID: 2262 RVA: 0x00034F84 File Offset: 0x00033184
		public IDictionaryEnumerator SyncIdIdEnumerator
		{
			get
			{
				return this.syncIdToMailboxIdTable.GetEnumerator();
			}
		}

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x060008D7 RID: 2263
		// (set) Token: 0x060008D8 RID: 2264
		public abstract ushort TypeId { get; set; }

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x060008D9 RID: 2265 RVA: 0x00034F96 File Offset: 0x00033196
		public bool UsingWriteBuffer
		{
			get
			{
				return this.usingWriteBuffer;
			}
		}

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x060008DA RID: 2266 RVA: 0x00034F9E File Offset: 0x0003319E
		// (set) Token: 0x060008DB RID: 2267 RVA: 0x00034FA6 File Offset: 0x000331A6
		protected Dictionary<ISyncItemId, string> OldIds
		{
			get
			{
				return this.oldIds;
			}
			set
			{
				this.oldIds = value;
			}
		}

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x060008DC RID: 2268 RVA: 0x00034FAF File Offset: 0x000331AF
		// (set) Token: 0x060008DD RID: 2269 RVA: 0x00034FB7 File Offset: 0x000331B7
		protected long UniqueCounter
		{
			get
			{
				return this.uniqueCounter;
			}
			set
			{
				this.uniqueCounter = value;
			}
		}

		// Token: 0x17000360 RID: 864
		public virtual ISyncItemId this[string syncId]
		{
			get
			{
				if (this.syncIdToMailboxIdTable.ContainsKey(syncId))
				{
					return this.syncIdToMailboxIdTable[syncId];
				}
				return null;
			}
		}

		// Token: 0x17000361 RID: 865
		public virtual string this[ISyncItemId mailboxId]
		{
			get
			{
				if (this.mailboxIdToSyncIdTable.ContainsKey(mailboxId))
				{
					return this.mailboxIdToSyncIdTable[mailboxId];
				}
				return null;
			}
		}

		// Token: 0x060008E0 RID: 2272
		public abstract ICustomSerializable BuildObject();

		// Token: 0x060008E1 RID: 2273 RVA: 0x00034FFC File Offset: 0x000331FC
		public void ClearChanges()
		{
			for (int i = 0; i < this.addedItems.Count; i++)
			{
				string text = this.addedItems[i];
				ISyncItemId key = this.syncIdToMailboxIdTable[text];
				this.syncIdToMailboxIdTable.Remove(text);
				this.mailboxIdToSyncIdTable.Remove(key);
				this.oldIds[key] = text;
				this.dirty = true;
			}
			this.addedItems.Clear();
			if (this.deletedItems.Count > 0)
			{
				this.deletedItems.Clear();
				this.dirty = true;
			}
		}

		// Token: 0x060008E2 RID: 2274 RVA: 0x00035094 File Offset: 0x00033294
		public void CommitChanges()
		{
			if (this.addedItems.Count > 0)
			{
				this.addedItems.Clear();
				this.dirty = true;
			}
			for (int i = 0; i < this.deletedItems.Count; i++)
			{
				string key = this.deletedItems[i];
				ISyncItemId key2 = this.syncIdToMailboxIdTable[key];
				this.mailboxIdToSyncIdTable.Remove(key2);
				this.syncIdToMailboxIdTable.Remove(key);
				this.dirty = true;
			}
			this.deletedItems.Clear();
		}

		// Token: 0x060008E3 RID: 2275 RVA: 0x0003511D File Offset: 0x0003331D
		public virtual bool Contains(ISyncItemId mailboxId)
		{
			return this.mailboxIdToSyncIdTable.ContainsKey(mailboxId) || (this.usingWriteBuffer && this.addedItemsWriteBufferReversed.ContainsKey(mailboxId));
		}

		// Token: 0x060008E4 RID: 2276 RVA: 0x00035145 File Offset: 0x00033345
		public virtual bool Contains(string syncId)
		{
			return this.syncIdToMailboxIdTable.ContainsKey(syncId) || (this.usingWriteBuffer && this.addedItemsWriteBuffer.ContainsKey(syncId));
		}

		// Token: 0x060008E5 RID: 2277 RVA: 0x00035170 File Offset: 0x00033370
		public void Delete(params ISyncItemId[] mailboxIds)
		{
			AirSyncDiagnostics.Assert(mailboxIds != null);
			for (int i = 0; i < mailboxIds.Length; i++)
			{
				if (this.mailboxIdToSyncIdTable.ContainsKey(mailboxIds[i]))
				{
					string item = this.mailboxIdToSyncIdTable[mailboxIds[i]];
					if (this.usingWriteBuffer)
					{
						if (!this.deletedItems.Contains(item) && !this.deletedItemsWriteBuffer.Contains(item))
						{
							this.deletedItemsWriteBuffer.Add(item);
						}
					}
					else if (!this.deletedItems.Contains(item))
					{
						this.deletedItems.Add(item);
						this.dirty = true;
					}
				}
				else if (this.usingWriteBuffer && this.addedItemsWriteBufferReversed.ContainsKey(mailboxIds[i]))
				{
					this.addedItemsWriteBuffer.Remove(this.addedItemsWriteBufferReversed[mailboxIds[i]]);
					this.addedItemsWriteBufferReversed.Remove(mailboxIds[i]);
				}
				else
				{
					AirSyncDiagnostics.Assert(false, "Id '{0}' is not found in the mapping", new object[]
					{
						mailboxIds[i]
					});
				}
			}
		}

		// Token: 0x060008E6 RID: 2278 RVA: 0x00035274 File Offset: 0x00033474
		public void Delete(params string[] syncIds)
		{
			AirSyncDiagnostics.Assert(syncIds != null);
			for (int i = 0; i < syncIds.Length; i++)
			{
				if (this.syncIdToMailboxIdTable.ContainsKey(syncIds[i]))
				{
					if (this.usingWriteBuffer)
					{
						if (!this.deletedItems.Contains(syncIds[i]) && !this.deletedItemsWriteBuffer.Contains(syncIds[i]))
						{
							this.deletedItemsWriteBuffer.Add(syncIds[i]);
						}
					}
					else if (!this.deletedItems.Contains(syncIds[i]))
					{
						this.deletedItems.Add(syncIds[i]);
						this.dirty = true;
					}
				}
				else if (this.usingWriteBuffer && this.addedItemsWriteBuffer.ContainsKey(syncIds[i]))
				{
					this.addedItemsWriteBufferReversed.Remove(this.addedItemsWriteBuffer[syncIds[i]]);
					this.addedItemsWriteBuffer.Remove(syncIds[i]);
				}
				else
				{
					AirSyncDiagnostics.Assert(false, "Id '{0}' is not found in the mapping", new object[]
					{
						syncIds[i]
					});
				}
			}
		}

		// Token: 0x060008E7 RID: 2279 RVA: 0x00035378 File Offset: 0x00033578
		public virtual void DeserializeData(BinaryReader reader, ComponentDataPool componentDataPool)
		{
			if (componentDataPool.ExternalVersion < 1)
			{
				GenericDictionaryData<StringData, string, StoreObjectIdData, StoreObjectId> genericDictionaryData = new GenericDictionaryData<StringData, string, StoreObjectIdData, StoreObjectId>();
				genericDictionaryData.DeserializeData(reader, componentDataPool);
				Dictionary<string, StoreObjectId> data = genericDictionaryData.Data;
				this.syncIdToMailboxIdTable = new Dictionary<string, ISyncItemId>(data.Count);
				using (Dictionary<string, StoreObjectId>.Enumerator enumerator = data.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						KeyValuePair<string, StoreObjectId> keyValuePair = enumerator.Current;
						this.syncIdToMailboxIdTable[keyValuePair.Key] = MailboxSyncItemId.CreateForNewItem(keyValuePair.Value);
					}
					goto IL_92;
				}
			}
			GenericDictionaryData<StringData, string, DerivedData<ISyncItemId>, ISyncItemId> genericDictionaryData2 = new GenericDictionaryData<StringData, string, DerivedData<ISyncItemId>, ISyncItemId>();
			genericDictionaryData2.DeserializeData(reader, componentDataPool);
			this.syncIdToMailboxIdTable = genericDictionaryData2.Data;
			IL_92:
			this.mailboxIdToSyncIdTable = new Dictionary<ISyncItemId, string>(this.syncIdToMailboxIdTable.Count);
			foreach (KeyValuePair<string, ISyncItemId> keyValuePair2 in this.syncIdToMailboxIdTable)
			{
				this.mailboxIdToSyncIdTable.Add(keyValuePair2.Value, keyValuePair2.Key);
			}
			GenericListData<StringData, string> genericListData = new GenericListData<StringData, string>();
			genericListData.DeserializeData(reader, componentDataPool);
			this.deletedItems = genericListData.Data;
			genericListData.DeserializeData(reader, componentDataPool);
			this.addedItems = genericListData.Data;
			this.uniqueCounter = reader.ReadInt64();
		}

		// Token: 0x060008E8 RID: 2280 RVA: 0x000354D4 File Offset: 0x000336D4
		public void Flush()
		{
			if (!this.usingWriteBuffer)
			{
				return;
			}
			this.usingWriteBuffer = false;
			foreach (string text in this.deletedItemsWriteBuffer)
			{
				this.Delete(new string[]
				{
					text
				});
			}
			this.deletedItemsWriteBuffer = null;
			foreach (string text2 in this.addedItemsWriteBuffer.Keys)
			{
				this.Add(this.addedItemsWriteBuffer[text2], text2);
			}
			this.addedItemsWriteBuffer = null;
		}

		// Token: 0x060008E9 RID: 2281 RVA: 0x000355A4 File Offset: 0x000337A4
		public void IncreaseCounterTo(long newCount)
		{
			this.uniqueCounter = ((this.uniqueCounter > newCount) ? this.uniqueCounter : newCount);
		}

		// Token: 0x060008EA RID: 2282 RVA: 0x000355C0 File Offset: 0x000337C0
		public virtual void SerializeData(BinaryWriter writer, ComponentDataPool componentDataPool)
		{
			new GenericDictionaryData<StringData, string, DerivedData<ISyncItemId>, ISyncItemId>(this.syncIdToMailboxIdTable).SerializeData(writer, componentDataPool);
			new GenericListData<StringData, string>(this.deletedItems).SerializeData(writer, componentDataPool);
			new GenericListData<StringData, string>(this.addedItems).SerializeData(writer, componentDataPool);
			writer.Write(this.uniqueCounter);
		}

		// Token: 0x060008EB RID: 2283 RVA: 0x0003560F File Offset: 0x0003380F
		public void UseWriteBuffer()
		{
			this.deletedItemsWriteBuffer = new List<string>(16);
			this.addedItemsWriteBuffer = new Dictionary<string, ISyncItemId>(16);
			this.addedItemsWriteBufferReversed = new Dictionary<ISyncItemId, string>(16);
			this.usingWriteBuffer = true;
		}

		// Token: 0x060008EC RID: 2284 RVA: 0x00035640 File Offset: 0x00033840
		protected void Add(ISyncItemId mailboxId, string syncId)
		{
			AirSyncDiagnostics.Assert(mailboxId != null);
			AirSyncDiagnostics.Assert(syncId != null);
			if (this.syncIdToMailboxIdTable.ContainsKey(syncId))
			{
				ISyncItemId syncItemId = this.syncIdToMailboxIdTable[syncId];
				if (syncItemId.Equals(mailboxId))
				{
					return;
				}
				throw new InvalidOperationException("SyncId has already been mapped to a different MailboxId");
			}
			else if (this.usingWriteBuffer && this.addedItemsWriteBuffer.ContainsKey(syncId))
			{
				ISyncItemId syncItemId2 = this.addedItemsWriteBuffer[syncId];
				if (syncItemId2.Equals(mailboxId))
				{
					return;
				}
				throw new InvalidOperationException("SyncId has already been mapped to a different MailboxId");
			}
			else
			{
				if (this.mailboxIdToSyncIdTable.ContainsKey(mailboxId) || (this.usingWriteBuffer && this.addedItemsWriteBufferReversed.ContainsKey(mailboxId)))
				{
					throw new InvalidOperationException("MailboxId has already been mapped to a different SyncId");
				}
				if (this.usingWriteBuffer)
				{
					this.addedItemsWriteBuffer[syncId] = mailboxId;
					this.addedItemsWriteBufferReversed[mailboxId] = syncId;
					this.uniqueCounter += 1L;
					return;
				}
				this.syncIdToMailboxIdTable.Add(syncId, mailboxId);
				this.mailboxIdToSyncIdTable.Add(mailboxId, syncId);
				this.addedItems.Add(syncId);
				this.uniqueCounter += 1L;
				this.dirty = true;
				return;
			}
		}

		// Token: 0x060008ED RID: 2285 RVA: 0x0003576A File Offset: 0x0003396A
		protected bool IsInDeletedItemsBuffer(string syncId)
		{
			return this.deletedItems.Contains(syncId) || (this.usingWriteBuffer && this.deletedItemsWriteBuffer.Contains(syncId));
		}

		// Token: 0x0400059E RID: 1438
		protected const int MaxSyncIdLength = 64;

		// Token: 0x0400059F RID: 1439
		private List<string> addedItems;

		// Token: 0x040005A0 RID: 1440
		[NonSerialized]
		private Dictionary<string, ISyncItemId> addedItemsWriteBuffer;

		// Token: 0x040005A1 RID: 1441
		[NonSerialized]
		private Dictionary<ISyncItemId, string> addedItemsWriteBufferReversed;

		// Token: 0x040005A2 RID: 1442
		private List<string> deletedItems;

		// Token: 0x040005A3 RID: 1443
		[NonSerialized]
		private List<string> deletedItemsWriteBuffer;

		// Token: 0x040005A4 RID: 1444
		[NonSerialized]
		private bool dirty;

		// Token: 0x040005A5 RID: 1445
		private Dictionary<ISyncItemId, string> mailboxIdToSyncIdTable;

		// Token: 0x040005A6 RID: 1446
		private Dictionary<ISyncItemId, string> oldIds;

		// Token: 0x040005A7 RID: 1447
		private Dictionary<string, ISyncItemId> syncIdToMailboxIdTable;

		// Token: 0x040005A8 RID: 1448
		private long uniqueCounter;

		// Token: 0x040005A9 RID: 1449
		private bool usingWriteBuffer;
	}
}
