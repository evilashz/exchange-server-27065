using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.AirSync;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x02000256 RID: 598
	internal abstract class SyncCalendarSyncStateBase : ISyncState
	{
		// Token: 0x060015BE RID: 5566 RVA: 0x00080C79 File Offset: 0x0007EE79
		public SyncCalendarSyncStateBase(StoreObjectId folderId, ISyncProvider syncProvider)
		{
			ExTraceGlobals.MethodEnterExitTracer.TraceDebug((long)this.GetHashCode(), "SyncCalendarSyncStateBase constructor called");
			this.syncProvider = syncProvider;
			this.syncStateTable = new GenericDictionaryData<StringData, string, DerivedData<ICustomSerializableBuilder>>(new Dictionary<string, DerivedData<ICustomSerializableBuilder>>());
			this.folderId = folderId;
		}

		// Token: 0x17000782 RID: 1922
		// (get) Token: 0x060015BF RID: 5567 RVA: 0x00080CB5 File Offset: 0x0007EEB5
		// (set) Token: 0x060015C0 RID: 5568 RVA: 0x00080CBD File Offset: 0x0007EEBD
		public int Version { get; set; }

		// Token: 0x17000783 RID: 1923
		// (get) Token: 0x060015C1 RID: 5569 RVA: 0x00080CC8 File Offset: 0x0007EEC8
		public int? BackendVersion
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000784 RID: 1924
		// (get) Token: 0x060015C2 RID: 5570 RVA: 0x00080CDE File Offset: 0x0007EEDE
		internal StoreObjectIdData SyncStoreFolderId
		{
			get
			{
				return new StoreObjectIdData(this.folderId);
			}
		}

		// Token: 0x17000785 RID: 1925
		// (get) Token: 0x060015C3 RID: 5571 RVA: 0x00080CEB File Offset: 0x0007EEEB
		internal Int32Data SyncStoreVersion
		{
			get
			{
				return new Int32Data(this.Version);
			}
		}

		// Token: 0x17000786 RID: 1926
		// (get) Token: 0x060015C4 RID: 5572
		internal abstract StringData SyncStateTag { get; }

		// Token: 0x17000787 RID: 1927
		public ICustomSerializableBuilder this[string key]
		{
			get
			{
				if (!this.syncStateTable.Data.ContainsKey(key))
				{
					return null;
				}
				return this.syncStateTable.Data[key].Data;
			}
			set
			{
				this.syncStateTable.Data[key] = new DerivedData<ICustomSerializableBuilder>(value);
			}
		}

		// Token: 0x060015C7 RID: 5575 RVA: 0x00080D3E File Offset: 0x0007EF3E
		public bool Contains(string key)
		{
			return this.syncStateTable.Data.ContainsKey(key);
		}

		// Token: 0x060015C8 RID: 5576 RVA: 0x00080D51 File Offset: 0x0007EF51
		public void Remove(string key)
		{
			this.syncStateTable.Data.Remove(key);
		}

		// Token: 0x060015C9 RID: 5577 RVA: 0x00080D68 File Offset: 0x0007EF68
		public string SerializeAsBase64String()
		{
			ExTraceGlobals.MethodEnterExitTracer.TraceDebug((long)this.GetHashCode(), "SyncCalendarSyncStateBase.SerializeAsBase64String called.");
			ComponentDataPool componentDataPool = new ComponentDataPool();
			string result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream))
				{
					this.syncStateTable.SerializeData(binaryWriter, componentDataPool);
					using (MemoryStream memoryStream2 = new MemoryStream())
					{
						memoryStream.Seek(0L, SeekOrigin.Begin);
						SerializationHelper.Compress(memoryStream, memoryStream2);
						result = Convert.ToBase64String(memoryStream2.ToArray());
					}
				}
			}
			return result;
		}

		// Token: 0x060015CA RID: 5578 RVA: 0x00080E1C File Offset: 0x0007F01C
		protected void Load(string base64SyncData)
		{
			byte[] array = SyncCalendarSyncStateBase.ConvertBase64SyncStateData(base64SyncData);
			ExTraceGlobals.MethodEnterExitTracer.TraceDebug((long)this.GetHashCode(), "SyncCalendarSyncStateBase.Load called.");
			GenericDictionaryData<StringData, string, DerivedData<ICustomSerializableBuilder>> genericDictionaryData = null;
			try
			{
				if (array != null && array.Length > 0)
				{
					using (MemoryStream memoryStream = new MemoryStream(array))
					{
						using (MemoryStream memoryStream2 = new MemoryStream())
						{
							using (BinaryReader binaryReader = new BinaryReader(memoryStream2))
							{
								ComponentDataPool componentDataPool = new ComponentDataPool();
								byte[] transferBuffer = new byte[71680];
								SerializationHelper.Decompress(memoryStream, memoryStream2, transferBuffer);
								memoryStream2.Seek(0L, SeekOrigin.Begin);
								GenericDictionaryData<StringData, string, DerivedData<ICustomSerializableBuilder>> genericDictionaryData2 = new GenericDictionaryData<StringData, string, DerivedData<ICustomSerializableBuilder>>();
								genericDictionaryData2.DeserializeData(binaryReader, componentDataPool);
								genericDictionaryData = genericDictionaryData2;
							}
						}
					}
					if (genericDictionaryData == null)
					{
						throw new CorruptSyncStateException("No SyncStateDictionaryData", null);
					}
				}
				else
				{
					genericDictionaryData = new GenericDictionaryData<StringData, string, DerivedData<ICustomSerializableBuilder>>(new Dictionary<string, DerivedData<ICustomSerializableBuilder>>());
					this.InitializeSyncState(genericDictionaryData.Data);
				}
				this.VerifySyncState(genericDictionaryData.Data);
			}
			catch (CustomSerializationException innerException)
			{
				throw new CorruptSyncStateException("SyncCalendarSyncStateBase.Load caught", innerException);
			}
			catch (ArgumentException innerException2)
			{
				throw new CorruptSyncStateException("SyncCalendarSyncStateBase.Load caught", innerException2);
			}
			catch (EndOfStreamException innerException3)
			{
				throw new CorruptSyncStateException("SyncCalendarSyncStateBase.Load caught", innerException3);
			}
			this.syncStateTable = genericDictionaryData;
			ExTraceGlobals.MethodEnterExitTracer.TraceDebug((long)this.GetHashCode(), "SyncCalendarSyncStateBase.Load successful.");
		}

		// Token: 0x060015CB RID: 5579 RVA: 0x00080F98 File Offset: 0x0007F198
		private static byte[] ConvertBase64SyncStateData(string base64SyncData)
		{
			byte[] result;
			try
			{
				if (string.IsNullOrEmpty(base64SyncData))
				{
					result = null;
				}
				else
				{
					byte[] array = Convert.FromBase64String(base64SyncData);
					if (array == null || array.Length == 0)
					{
						throw new CorruptSyncStateException("result is empty", null);
					}
					result = array;
				}
			}
			catch (FormatException innerException)
			{
				throw new CorruptSyncStateException("SyncCalendarSyncStateBase.ConvertBase64SyncStateData caught", innerException);
			}
			return result;
		}

		// Token: 0x060015CC RID: 5580 RVA: 0x00080FF0 File Offset: 0x0007F1F0
		protected virtual void InitializeSyncState(Dictionary<string, DerivedData<ICustomSerializableBuilder>> obj)
		{
			obj.Add("{9150227d-9140-45d0-b4c2-e987f59cfc46}SyncCalendar.SyncStateCanary", new DerivedData<ICustomSerializableBuilder>(this.SyncStateTag));
			obj.Add("{9150227d-9140-45d0-b4c2-e987f59cfc46}SyncCalendar.SyncStateFolderId", new DerivedData<ICustomSerializableBuilder>(this.SyncStoreFolderId));
			obj.Add("{9150227d-9140-45d0-b4c2-e987f59cfc46}SyncCalendar.SyncStateVersion", new DerivedData<ICustomSerializableBuilder>(this.SyncStoreVersion));
		}

		// Token: 0x060015CD RID: 5581 RVA: 0x00081040 File Offset: 0x0007F240
		protected void InitializeSyncStateTable()
		{
			GenericDictionaryData<StringData, string, DerivedData<ICustomSerializableBuilder>> genericDictionaryData = new GenericDictionaryData<StringData, string, DerivedData<ICustomSerializableBuilder>>(new Dictionary<string, DerivedData<ICustomSerializableBuilder>>());
			this.InitializeSyncState(genericDictionaryData.Data);
			this.syncStateTable = genericDictionaryData;
		}

		// Token: 0x060015CE RID: 5582 RVA: 0x0008106C File Offset: 0x0007F26C
		protected virtual void VerifySyncState(Dictionary<string, DerivedData<ICustomSerializableBuilder>> obj)
		{
			StringData stringData = obj["{9150227d-9140-45d0-b4c2-e987f59cfc46}SyncCalendar.SyncStateCanary"].Data as StringData;
			StoreObjectIdData storeObjectIdData = obj["{9150227d-9140-45d0-b4c2-e987f59cfc46}SyncCalendar.SyncStateFolderId"].Data as StoreObjectIdData;
			Int32Data int32Data = obj["{9150227d-9140-45d0-b4c2-e987f59cfc46}SyncCalendar.SyncStateVersion"].Data as Int32Data;
			if (stringData == null || !this.SyncStateTag.Data.Equals(stringData.Data))
			{
				throw new CorruptSyncStateException("tagKey is invalid", null);
			}
			if (storeObjectIdData == null || !this.SyncStoreFolderId.Data.Equals(storeObjectIdData.Data))
			{
				throw new CorruptSyncStateException("storeId is invalid", null);
			}
			if (int32Data == null)
			{
				throw new CorruptSyncStateException("storedVersion is invalid", null);
			}
			this.Version = int32Data.Data;
		}

		// Token: 0x04000D87 RID: 3463
		protected const string SyncStateGuid = "{9150227d-9140-45d0-b4c2-e987f59cfc46}";

		// Token: 0x04000D88 RID: 3464
		private const string SyncStateTagKeyName = "{9150227d-9140-45d0-b4c2-e987f59cfc46}SyncCalendar.SyncStateCanary";

		// Token: 0x04000D89 RID: 3465
		private const string SyncStateFolderIdKeyName = "{9150227d-9140-45d0-b4c2-e987f59cfc46}SyncCalendar.SyncStateFolderId";

		// Token: 0x04000D8A RID: 3466
		protected const string SyncStateVersionKeyName = "{9150227d-9140-45d0-b4c2-e987f59cfc46}SyncCalendar.SyncStateVersion";

		// Token: 0x04000D8B RID: 3467
		private GenericDictionaryData<StringData, string, DerivedData<ICustomSerializableBuilder>> syncStateTable;

		// Token: 0x04000D8C RID: 3468
		protected ISyncProvider syncProvider;

		// Token: 0x04000D8D RID: 3469
		protected StoreObjectId folderId;
	}
}
