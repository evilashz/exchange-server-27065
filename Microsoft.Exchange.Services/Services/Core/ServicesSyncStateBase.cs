using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x020003D6 RID: 982
	internal abstract class ServicesSyncStateBase : ISyncState
	{
		// Token: 0x06001B6C RID: 7020 RVA: 0x0009C26F File Offset: 0x0009A46F
		public ServicesSyncStateBase(StoreObjectId folderId, ISyncProvider syncProvider)
		{
			ExTraceGlobals.SynchronizationTracer.TraceDebug((long)this.GetHashCode(), "ServicesSyncStateBase constructor called");
			this.syncProvider = syncProvider;
			this.syncStateTable = new GenericDictionaryData<StringData, string, DerivedData<ICustomSerializableBuilder>>(new Dictionary<string, DerivedData<ICustomSerializableBuilder>>());
			this.folderId = folderId;
		}

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x06001B6D RID: 7021 RVA: 0x0009C2AB File Offset: 0x0009A4AB
		// (set) Token: 0x06001B6E RID: 7022 RVA: 0x0009C2B3 File Offset: 0x0009A4B3
		public int Version
		{
			get
			{
				return this.version;
			}
			set
			{
				this.version = value;
			}
		}

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x06001B6F RID: 7023 RVA: 0x0009C2BC File Offset: 0x0009A4BC
		public int? BackendVersion
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700033A RID: 826
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

		// Token: 0x06001B72 RID: 7026 RVA: 0x0009C318 File Offset: 0x0009A518
		public bool Contains(string key)
		{
			return this.syncStateTable.Data.ContainsKey(key);
		}

		// Token: 0x06001B73 RID: 7027 RVA: 0x0009C32B File Offset: 0x0009A52B
		public void Remove(string key)
		{
			this.syncStateTable.Data.Remove(key);
		}

		// Token: 0x06001B74 RID: 7028 RVA: 0x0009C340 File Offset: 0x0009A540
		public string SerializeAsBase64String()
		{
			ExTraceGlobals.SynchronizationTracer.TraceDebug((long)this.GetHashCode(), "ServicesFolderSyncState.SerializeAsBase64String called.");
			SyncStateTypeFactory.GetInstance().RegisterInternalBuilders();
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

		// Token: 0x06001B75 RID: 7029 RVA: 0x0009C3FC File Offset: 0x0009A5FC
		protected void Load(string base64SyncData)
		{
			byte[] array = ServicesSyncStateBase.ConvertBase64SyncStateData(base64SyncData);
			ExTraceGlobals.SynchronizationTracer.TraceDebug((long)this.GetHashCode(), "ServicesFolderSyncState.Load called.");
			GenericDictionaryData<StringData, string, DerivedData<ICustomSerializableBuilder>> genericDictionaryData = null;
			try
			{
				SyncStateTypeFactory.GetInstance().RegisterInternalBuilders();
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
						throw new InvalidSyncStateDataException();
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
				throw new InvalidSyncStateDataException(innerException);
			}
			catch (ArgumentException innerException2)
			{
				throw new InvalidSyncStateDataException(innerException2);
			}
			catch (EndOfStreamException innerException3)
			{
				throw new InvalidSyncStateDataException(innerException3);
			}
			this.syncStateTable = genericDictionaryData;
			ExTraceGlobals.SynchronizationTracer.TraceDebug((long)this.GetHashCode(), "ServicesFolderSyncState.Load successful.");
		}

		// Token: 0x06001B76 RID: 7030 RVA: 0x0009C56C File Offset: 0x0009A76C
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
						throw new InvalidSyncStateDataException();
					}
					result = array;
				}
			}
			catch (FormatException innerException)
			{
				throw new InvalidSyncStateDataException(innerException);
			}
			return result;
		}

		// Token: 0x06001B77 RID: 7031 RVA: 0x0009C5B8 File Offset: 0x0009A7B8
		protected virtual void InitializeSyncState(Dictionary<string, DerivedData<ICustomSerializableBuilder>> obj)
		{
			obj.Add("{49A4350A-C7A8-4AC3-BCBC-F2B8CB7F9550}WS.SyncStateCanary", new DerivedData<ICustomSerializableBuilder>(this.SyncStateTag));
			obj.Add("{49A4350A-C7A8-4AC3-BCBC-F2B8CB7F9550}WS.SyncStateFolderId", new DerivedData<ICustomSerializableBuilder>(this.SyncStoreFolderId));
			obj.Add("{49A4350A-C7A8-4AC3-BCBC-F2B8CB7F9550}WS.SyncStateVersion", new DerivedData<ICustomSerializableBuilder>(this.SyncStoreVersion));
		}

		// Token: 0x06001B78 RID: 7032 RVA: 0x0009C608 File Offset: 0x0009A808
		protected void InitializeSyncStateTable()
		{
			GenericDictionaryData<StringData, string, DerivedData<ICustomSerializableBuilder>> genericDictionaryData = new GenericDictionaryData<StringData, string, DerivedData<ICustomSerializableBuilder>>(new Dictionary<string, DerivedData<ICustomSerializableBuilder>>());
			this.InitializeSyncState(genericDictionaryData.Data);
			this.syncStateTable = genericDictionaryData;
		}

		// Token: 0x06001B79 RID: 7033 RVA: 0x0009C634 File Offset: 0x0009A834
		protected virtual void VerifySyncState(Dictionary<string, DerivedData<ICustomSerializableBuilder>> obj)
		{
			StringData stringData = obj["{49A4350A-C7A8-4AC3-BCBC-F2B8CB7F9550}WS.SyncStateCanary"].Data as StringData;
			StoreObjectIdData storeObjectIdData = obj["{49A4350A-C7A8-4AC3-BCBC-F2B8CB7F9550}WS.SyncStateFolderId"].Data as StoreObjectIdData;
			Int32Data int32Data = obj["{49A4350A-C7A8-4AC3-BCBC-F2B8CB7F9550}WS.SyncStateVersion"].Data as Int32Data;
			if (stringData == null || !this.SyncStateTag.Data.Equals(stringData.Data))
			{
				throw new InvalidSyncStateDataException();
			}
			if (storeObjectIdData == null || !this.SyncStoreFolderId.Data.Equals(storeObjectIdData.Data))
			{
				throw new InvalidSyncStateDataException();
			}
			if (int32Data == null)
			{
				throw new InvalidSyncStateDataException();
			}
			this.version = int32Data.Data;
		}

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x06001B7A RID: 7034 RVA: 0x0009C6DA File Offset: 0x0009A8DA
		internal StoreObjectIdData SyncStoreFolderId
		{
			get
			{
				return new StoreObjectIdData(this.folderId);
			}
		}

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x06001B7B RID: 7035 RVA: 0x0009C6E7 File Offset: 0x0009A8E7
		internal Int32Data SyncStoreVersion
		{
			get
			{
				return new Int32Data(this.version);
			}
		}

		// Token: 0x1700033D RID: 829
		// (get) Token: 0x06001B7C RID: 7036
		internal abstract StringData SyncStateTag { get; }

		// Token: 0x0400120D RID: 4621
		protected const string SyncStateGuid = "{49A4350A-C7A8-4AC3-BCBC-F2B8CB7F9550}";

		// Token: 0x0400120E RID: 4622
		private const string SyncStateTagKeyName = "{49A4350A-C7A8-4AC3-BCBC-F2B8CB7F9550}WS.SyncStateCanary";

		// Token: 0x0400120F RID: 4623
		private const string SyncStateFolderIdKeyName = "{49A4350A-C7A8-4AC3-BCBC-F2B8CB7F9550}WS.SyncStateFolderId";

		// Token: 0x04001210 RID: 4624
		protected const string SyncStateVersionKeyName = "{49A4350A-C7A8-4AC3-BCBC-F2B8CB7F9550}WS.SyncStateVersion";

		// Token: 0x04001211 RID: 4625
		private GenericDictionaryData<StringData, string, DerivedData<ICustomSerializableBuilder>> syncStateTable;

		// Token: 0x04001212 RID: 4626
		private int version;

		// Token: 0x04001213 RID: 4627
		protected ISyncProvider syncProvider;

		// Token: 0x04001214 RID: 4628
		protected StoreObjectId folderId;
	}
}
