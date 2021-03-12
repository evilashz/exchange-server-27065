using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.AirSync;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x02000281 RID: 641
	internal class SyncStatusData : DisposeTrackableBase, ISyncStatusData
	{
		// Token: 0x170007F0 RID: 2032
		// (get) Token: 0x0600178E RID: 6030 RVA: 0x0008C080 File Offset: 0x0008A280
		public List<int> ClientCategoryList
		{
			get
			{
				return this.clientCategoryList;
			}
		}

		// Token: 0x170007F1 RID: 2033
		// (get) Token: 0x0600178F RID: 6031 RVA: 0x0008C088 File Offset: 0x0008A288
		public List<string> LastClientIdsSeen
		{
			get
			{
				return this.lastClientIdsSent;
			}
		}

		// Token: 0x170007F2 RID: 2034
		// (get) Token: 0x06001790 RID: 6032 RVA: 0x0008C090 File Offset: 0x0008A290
		// (set) Token: 0x06001791 RID: 6033 RVA: 0x0008C0BE File Offset: 0x0008A2BE
		public string LastSyncRequestRandomString
		{
			get
			{
				StringData stringData = (StringData)this.syncStatusSyncState[CustomStateDatumType.LastSyncRequestRandomNumber];
				if (stringData != null)
				{
					return stringData.Data;
				}
				return null;
			}
			set
			{
				this.syncStatusSyncState[CustomStateDatumType.LastSyncRequestRandomNumber] = new StringData(value);
				this.dirty = true;
			}
		}

		// Token: 0x170007F3 RID: 2035
		// (get) Token: 0x06001792 RID: 6034 RVA: 0x0008C0E0 File Offset: 0x0008A2E0
		// (set) Token: 0x06001793 RID: 6035 RVA: 0x0008C10E File Offset: 0x0008A30E
		public byte[] LastCachableWbxmlDocument
		{
			get
			{
				ByteArrayData byteArrayData = (ByteArrayData)this.syncStatusSyncState[CustomStateDatumType.LastCachableWbxmlDocument];
				if (byteArrayData != null)
				{
					return byteArrayData.Data;
				}
				return null;
			}
			set
			{
				this.syncStatusSyncState[CustomStateDatumType.LastCachableWbxmlDocument] = new ByteArrayData(value);
				this.dirty = true;
			}
		}

		// Token: 0x170007F4 RID: 2036
		// (get) Token: 0x06001794 RID: 6036 RVA: 0x0008C130 File Offset: 0x0008A330
		// (set) Token: 0x06001795 RID: 6037 RVA: 0x0008C168 File Offset: 0x0008A368
		public ExDateTime? LastSyncAttemptTime
		{
			get
			{
				DateTimeData dateTimeData = (DateTimeData)this.syncStatusSyncState[CustomStateDatumType.LastSyncAttemptTime];
				return new ExDateTime?((dateTimeData == null) ? ExDateTime.MinValue : dateTimeData.Data);
			}
			set
			{
				if (value == null)
				{
					this.syncStatusSyncState.Remove(CustomStateDatumType.LastSyncAttemptTime);
				}
				else
				{
					this.syncStatusSyncState[CustomStateDatumType.LastSyncAttemptTime] = new DateTimeData(value.Value);
				}
				this.dirty = true;
			}
		}

		// Token: 0x170007F5 RID: 2037
		// (get) Token: 0x06001796 RID: 6038 RVA: 0x0008C1A8 File Offset: 0x0008A3A8
		// (set) Token: 0x06001797 RID: 6039 RVA: 0x0008C1E0 File Offset: 0x0008A3E0
		public ExDateTime? LastSyncSuccessTime
		{
			get
			{
				DateTimeData dateTimeData = (DateTimeData)this.syncStatusSyncState[CustomStateDatumType.LastSyncSuccessTime];
				return new ExDateTime?((dateTimeData == null) ? ExDateTime.MinValue : dateTimeData.Data);
			}
			set
			{
				if (value == null)
				{
					this.syncStatusSyncState.Remove(CustomStateDatumType.LastSyncAttemptTime);
				}
				else
				{
					this.syncStatusSyncState[CustomStateDatumType.LastSyncSuccessTime] = new DateTimeData(value.Value);
				}
				this.dirty = true;
			}
		}

		// Token: 0x170007F6 RID: 2038
		// (get) Token: 0x06001798 RID: 6040 RVA: 0x0008C220 File Offset: 0x0008A420
		// (set) Token: 0x06001799 RID: 6041 RVA: 0x0008C24E File Offset: 0x0008A44E
		public string LastSyncUserAgent
		{
			get
			{
				ConstStringData constStringData = (ConstStringData)this.syncStatusSyncState[CustomStateDatumType.UserAgent];
				if (constStringData != null)
				{
					return constStringData.Data;
				}
				return null;
			}
			set
			{
				this.syncStatusSyncState[CustomStateDatumType.UserAgent] = new ConstStringData(StaticStringPool.Instance.Intern(value));
				this.dirty = true;
			}
		}

		// Token: 0x170007F7 RID: 2039
		// (get) Token: 0x0600179A RID: 6042 RVA: 0x0008C278 File Offset: 0x0008A478
		// (set) Token: 0x0600179B RID: 6043 RVA: 0x0008C2A6 File Offset: 0x0008A4A6
		public bool ClientCanSendUpEmptyRequests
		{
			get
			{
				BooleanData booleanData = (BooleanData)this.syncStatusSyncState[CustomStateDatumType.ClientCanSendUpEmptyRequests];
				return booleanData != null && booleanData.Data;
			}
			set
			{
				this.syncStatusSyncState[CustomStateDatumType.ClientCanSendUpEmptyRequests] = new BooleanData(value);
				this.dirty = true;
			}
		}

		// Token: 0x0600179C RID: 6044 RVA: 0x0008C2C8 File Offset: 0x0008A4C8
		public void AddClientId(string clientId)
		{
			lock (this.instanceLock)
			{
				if (this.lastClientIdsSent == null)
				{
					this.lastClientIdsSent = new List<string>(1);
				}
				if (!this.lastClientIdsSent.Contains(clientId))
				{
					this.lastClientIdsSent.Add(clientId);
					while (this.lastClientIdsSent.Count > 5)
					{
						this.lastClientIdsSent.RemoveAt(0);
					}
					this.lastClientIdsSentModified = true;
					this.dirty = true;
				}
			}
		}

		// Token: 0x0600179D RID: 6045 RVA: 0x0008C35C File Offset: 0x0008A55C
		public bool ContainsClientId(string clientId)
		{
			bool result;
			lock (this.instanceLock)
			{
				if (this.lastClientIdsSent == null)
				{
					result = false;
				}
				else
				{
					result = this.lastClientIdsSent.Contains(clientId);
				}
			}
			return result;
		}

		// Token: 0x0600179E RID: 6046 RVA: 0x0008C3B0 File Offset: 0x0008A5B0
		public bool ContainsClientCategoryHash(int hashName)
		{
			bool result;
			lock (this.instanceLock)
			{
				if (this.clientCategoryList == null)
				{
					result = false;
				}
				else
				{
					result = this.clientCategoryList.Contains(hashName);
				}
			}
			return result;
		}

		// Token: 0x0600179F RID: 6047 RVA: 0x0008C404 File Offset: 0x0008A604
		public void AddClientCategoryHash(int hashName)
		{
			lock (this.instanceLock)
			{
				if (this.clientCategoryList == null)
				{
					this.clientCategoryList = new List<int>();
				}
				if (!this.clientCategoryList.Contains(hashName))
				{
					this.clientCategoryList.Add(hashName);
					this.clientCategoryListModified = true;
					this.dirty = true;
				}
			}
		}

		// Token: 0x060017A0 RID: 6048 RVA: 0x0008C47C File Offset: 0x0008A67C
		public void ClearClientCategoryHash()
		{
			this.clientCategoryList = null;
			this.clientCategoryListModified = true;
			this.dirty = true;
		}

		// Token: 0x060017A1 RID: 6049 RVA: 0x0008C494 File Offset: 0x0008A694
		private List<int> GetClientCategoryList()
		{
			GenericListData<Int32Data, int> genericListData = this.syncStatusSyncState[CustomStateDatumType.ClientCategoryList] as GenericListData<Int32Data, int>;
			if (genericListData == null || genericListData.Data == null)
			{
				return null;
			}
			return genericListData.Data;
		}

		// Token: 0x060017A2 RID: 6050 RVA: 0x0008C4CC File Offset: 0x0008A6CC
		private List<string> GetLastIdsSeen()
		{
			GenericListData<StringData, string> genericListData = this.syncStatusSyncState[CustomStateDatumType.LastClientIdsSent] as GenericListData<StringData, string>;
			if (genericListData == null || genericListData.Data == null)
			{
				return null;
			}
			return genericListData.Data;
		}

		// Token: 0x060017A3 RID: 6051 RVA: 0x0008C504 File Offset: 0x0008A704
		public static ISyncStatusData Load(SyncStateStorage syncStateStorage)
		{
			SyncStatusData syncStatusData = null;
			bool flag = false;
			ISyncStatusData result;
			try
			{
				syncStatusData = new SyncStatusData();
				syncStatusData.syncStatusSyncState = AirSyncUtility.GetOrCreateSyncStatusSyncState(syncStateStorage);
				syncStatusData.clientCategoryList = syncStatusData.GetClientCategoryList();
				syncStatusData.lastClientIdsSent = syncStatusData.GetLastIdsSeen();
				syncStatusData.dirty = false;
				syncStatusData.clientCategoryListModified = false;
				syncStatusData.lastClientIdsSentModified = false;
				flag = true;
				result = syncStatusData;
			}
			finally
			{
				if (!flag && syncStatusData != null)
				{
					syncStatusData.Dispose();
					syncStatusData = null;
				}
			}
			return result;
		}

		// Token: 0x060017A4 RID: 6052 RVA: 0x0008C57C File Offset: 0x0008A77C
		public void SaveAndDispose()
		{
			if (this.syncStatusSyncState != null && this.dirty)
			{
				try
				{
					if (this.clientCategoryListModified)
					{
						if (this.clientCategoryList == null)
						{
							this.syncStatusSyncState.Remove(CustomStateDatumType.ClientCategoryList);
						}
						else
						{
							this.syncStatusSyncState[CustomStateDatumType.ClientCategoryList] = new GenericListData<Int32Data, int>(this.clientCategoryList);
						}
					}
					if (this.lastClientIdsSentModified)
					{
						if (this.lastClientIdsSent == null)
						{
							this.syncStatusSyncState.Remove(CustomStateDatumType.LastClientIdsSent);
						}
						else
						{
							this.syncStatusSyncState[CustomStateDatumType.LastClientIdsSent] = new GenericListData<StringData, string>(this.lastClientIdsSent);
						}
					}
					this.syncStatusSyncState.Commit();
					this.dirty = false;
					this.clientCategoryListModified = false;
					this.lastClientIdsSentModified = false;
				}
				catch (LocalizedException arg)
				{
					AirSyncDiagnostics.TraceError<LocalizedException>(ExTraceGlobals.RequestsTracer, this, "[SyncData.Save] Failed to commit syncStatusSyncState. Exception: {0}", arg);
				}
				finally
				{
					this.Dispose();
				}
			}
		}

		// Token: 0x060017A5 RID: 6053 RVA: 0x0008C674 File Offset: 0x0008A874
		protected override void InternalDispose(bool disposing)
		{
			if (disposing && this.syncStatusSyncState != null)
			{
				this.syncStatusSyncState.Dispose();
				this.syncStatusSyncState = null;
			}
		}

		// Token: 0x060017A6 RID: 6054 RVA: 0x0008C693 File Offset: 0x0008A893
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<SyncStatusData>(this);
		}

		// Token: 0x04000E7C RID: 3708
		private bool dirty;

		// Token: 0x04000E7D RID: 3709
		private object instanceLock = new object();

		// Token: 0x04000E7E RID: 3710
		private List<string> lastClientIdsSent;

		// Token: 0x04000E7F RID: 3711
		private bool lastClientIdsSentModified;

		// Token: 0x04000E80 RID: 3712
		private List<int> clientCategoryList;

		// Token: 0x04000E81 RID: 3713
		private bool clientCategoryListModified;

		// Token: 0x04000E82 RID: 3714
		private CustomSyncState syncStatusSyncState;
	}
}
