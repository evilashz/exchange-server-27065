using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x020000ED RID: 237
	internal class NewSyncStatusData : ISyncStatusData
	{
		// Token: 0x1700051A RID: 1306
		// (get) Token: 0x06000D1E RID: 3358 RVA: 0x00047A11 File Offset: 0x00045C11
		// (set) Token: 0x06000D1F RID: 3359 RVA: 0x00047A19 File Offset: 0x00045C19
		public bool ClientCanSendUpEmptyRequests
		{
			get
			{
				return this.clientCanSendUpEmptyRequests;
			}
			set
			{
				this.clientCanSendUpEmptyRequests = value;
				this.deviceMetadataStateChanged = true;
			}
		}

		// Token: 0x1700051B RID: 1307
		// (get) Token: 0x06000D20 RID: 3360 RVA: 0x00047A29 File Offset: 0x00045C29
		// (set) Token: 0x06000D21 RID: 3361 RVA: 0x00047A31 File Offset: 0x00045C31
		public string LastSyncRequestRandomString
		{
			get
			{
				return this.lastSyncRequestRandomString;
			}
			set
			{
				this.lastSyncRequestRandomString = value;
				this.deviceMetadataStateChanged = true;
			}
		}

		// Token: 0x1700051C RID: 1308
		// (get) Token: 0x06000D22 RID: 3362 RVA: 0x00047A41 File Offset: 0x00045C41
		// (set) Token: 0x06000D23 RID: 3363 RVA: 0x00047A49 File Offset: 0x00045C49
		public byte[] LastCachableWbxmlDocument
		{
			get
			{
				return this.lastCachableWbxmlDocument;
			}
			set
			{
				this.lastCachableWbxmlDocument = value;
				this.deviceMetadataStateChanged = true;
			}
		}

		// Token: 0x1700051D RID: 1309
		// (get) Token: 0x06000D24 RID: 3364 RVA: 0x00047A59 File Offset: 0x00045C59
		// (set) Token: 0x06000D25 RID: 3365 RVA: 0x00047A61 File Offset: 0x00045C61
		public ExDateTime? LastSyncAttemptTime
		{
			get
			{
				return this.lastSyncAttemptTime;
			}
			set
			{
				this.lastSyncAttemptTime = value;
				this.globalStateModified |= NewSyncStatusData.GlobalInfoStateModified.LastSyncAttemptTime;
			}
		}

		// Token: 0x1700051E RID: 1310
		// (get) Token: 0x06000D26 RID: 3366 RVA: 0x00047A78 File Offset: 0x00045C78
		// (set) Token: 0x06000D27 RID: 3367 RVA: 0x00047A80 File Offset: 0x00045C80
		public ExDateTime? LastSyncSuccessTime
		{
			get
			{
				return this.lastSyncSuccessTime;
			}
			set
			{
				this.lastSyncSuccessTime = value;
				this.globalStateModified |= NewSyncStatusData.GlobalInfoStateModified.LastSyncSuccessTime;
			}
		}

		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x06000D28 RID: 3368 RVA: 0x00047A97 File Offset: 0x00045C97
		// (set) Token: 0x06000D29 RID: 3369 RVA: 0x00047A9F File Offset: 0x00045C9F
		public string LastSyncUserAgent
		{
			get
			{
				return this.lastSyncUserAgent;
			}
			set
			{
				this.lastSyncUserAgent = value;
				this.globalStateModified |= NewSyncStatusData.GlobalInfoStateModified.LastSyncUserAgent;
			}
		}

		// Token: 0x06000D2A RID: 3370 RVA: 0x00047AB8 File Offset: 0x00045CB8
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
					this.globalStateModified |= NewSyncStatusData.GlobalInfoStateModified.LastClientIdsSeen;
				}
			}
		}

		// Token: 0x06000D2B RID: 3371 RVA: 0x00047B4C File Offset: 0x00045D4C
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

		// Token: 0x06000D2C RID: 3372 RVA: 0x00047BA0 File Offset: 0x00045DA0
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

		// Token: 0x06000D2D RID: 3373 RVA: 0x00047BF4 File Offset: 0x00045DF4
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
					this.globalStateModified |= NewSyncStatusData.GlobalInfoStateModified.ClientCategoryList;
				}
			}
		}

		// Token: 0x06000D2E RID: 3374 RVA: 0x00047C6C File Offset: 0x00045E6C
		public void ClearClientCategoryHash()
		{
			this.clientCategoryList = null;
			this.globalStateModified |= NewSyncStatusData.GlobalInfoStateModified.ClientCategoryList;
		}

		// Token: 0x06000D2F RID: 3375 RVA: 0x00047C84 File Offset: 0x00045E84
		public static ISyncStatusData Load(GlobalInfo globalInfo, SyncStateStorage syncStateStorage)
		{
			NewSyncStatusData newSyncStatusData = new NewSyncStatusData();
			newSyncStatusData.globalInfo = globalInfo;
			newSyncStatusData.deviceMetadata = syncStateStorage.DeviceMetadata;
			if (!globalInfo.HasNewSyncData)
			{
				using (SyncStatusData syncStatusData = SyncStatusData.Load(syncStateStorage) as SyncStatusData)
				{
					newSyncStatusData.clientCategoryList = ((syncStatusData.ClientCategoryList == null) ? null : new List<int>(syncStatusData.ClientCategoryList));
					newSyncStatusData.lastClientIdsSent = ((syncStatusData.LastClientIdsSeen == null) ? null : new List<string>(syncStatusData.LastClientIdsSeen));
					newSyncStatusData.lastSyncRequestRandomString = syncStatusData.LastSyncRequestRandomString;
					newSyncStatusData.lastCachableWbxmlDocument = syncStatusData.LastCachableWbxmlDocument;
					newSyncStatusData.ClientCanSendUpEmptyRequests = syncStatusData.ClientCanSendUpEmptyRequests;
					newSyncStatusData.lastSyncAttemptTime = syncStatusData.LastSyncAttemptTime;
					newSyncStatusData.lastSyncSuccessTime = syncStatusData.LastSyncSuccessTime;
					newSyncStatusData.lastSyncUserAgent = syncStatusData.LastSyncUserAgent;
				}
				newSyncStatusData.deviceMetadataStateChanged = true;
				newSyncStatusData.globalStateModified = NewSyncStatusData.GlobalInfoStateModified.All;
			}
			else
			{
				newSyncStatusData.clientCategoryList = ((globalInfo.ClientCategoryHashList == null) ? null : new List<int>(globalInfo.ClientCategoryHashList));
				newSyncStatusData.lastClientIdsSent = ((globalInfo.LastClientIdsSeen == null) ? null : new List<string>(globalInfo.LastClientIdsSeen));
				newSyncStatusData.lastSyncRequestRandomString = newSyncStatusData.deviceMetadata.LastSyncRequestRandomString;
				newSyncStatusData.lastCachableWbxmlDocument = newSyncStatusData.deviceMetadata.LastCachedSyncRequest;
				newSyncStatusData.ClientCanSendUpEmptyRequests = newSyncStatusData.deviceMetadata.ClientCanSendUpEmptyRequests;
				newSyncStatusData.lastSyncAttemptTime = globalInfo.LastSyncAttemptTime;
				newSyncStatusData.lastSyncSuccessTime = globalInfo.LastSyncSuccessTime;
				newSyncStatusData.lastSyncUserAgent = globalInfo.LastSyncUserAgent;
				newSyncStatusData.deviceMetadataStateChanged = false;
				newSyncStatusData.globalStateModified = NewSyncStatusData.GlobalInfoStateModified.None;
			}
			return newSyncStatusData;
		}

		// Token: 0x06000D30 RID: 3376 RVA: 0x00047E14 File Offset: 0x00046014
		public void SaveAndDispose()
		{
			if (this.globalInfo == null || this.globalInfo.IsDisposed)
			{
				return;
			}
			if (this.globalStateModified != NewSyncStatusData.GlobalInfoStateModified.None)
			{
				if ((this.globalStateModified & NewSyncStatusData.GlobalInfoStateModified.ClientCategoryList) == NewSyncStatusData.GlobalInfoStateModified.ClientCategoryList)
				{
					this.globalInfo.ClientCategoryHashList = ((this.clientCategoryList == null) ? null : this.clientCategoryList.ToArray());
				}
				if ((this.globalStateModified & NewSyncStatusData.GlobalInfoStateModified.LastClientIdsSeen) == NewSyncStatusData.GlobalInfoStateModified.LastClientIdsSeen)
				{
					this.globalInfo.LastClientIdsSeen = ((this.lastClientIdsSent == null) ? null : this.lastClientIdsSent.ToArray());
				}
				if ((this.globalStateModified & NewSyncStatusData.GlobalInfoStateModified.LastSyncAttemptTime) == NewSyncStatusData.GlobalInfoStateModified.LastSyncAttemptTime)
				{
					this.globalInfo.LastSyncAttemptTime = this.lastSyncAttemptTime;
				}
				if ((this.globalStateModified & NewSyncStatusData.GlobalInfoStateModified.LastSyncSuccessTime) == NewSyncStatusData.GlobalInfoStateModified.LastSyncSuccessTime)
				{
					this.globalInfo.LastSyncSuccessTime = this.lastSyncSuccessTime;
				}
				if ((this.globalStateModified & NewSyncStatusData.GlobalInfoStateModified.LastSyncUserAgent) == NewSyncStatusData.GlobalInfoStateModified.LastSyncUserAgent)
				{
					this.globalInfo.LastSyncUserAgent = this.lastSyncUserAgent;
				}
			}
			if (this.deviceMetadataStateChanged)
			{
				this.deviceMetadata.SaveSyncStatusData(this.lastSyncRequestRandomString, this.lastCachableWbxmlDocument, this.ClientCanSendUpEmptyRequests);
			}
			this.deviceMetadataStateChanged = false;
			this.globalStateModified = NewSyncStatusData.GlobalInfoStateModified.None;
			this.globalInfo = null;
		}

		// Token: 0x0400081A RID: 2074
		private NewSyncStatusData.GlobalInfoStateModified globalStateModified;

		// Token: 0x0400081B RID: 2075
		private bool deviceMetadataStateChanged;

		// Token: 0x0400081C RID: 2076
		private object instanceLock = new object();

		// Token: 0x0400081D RID: 2077
		private List<string> lastClientIdsSent;

		// Token: 0x0400081E RID: 2078
		private List<int> clientCategoryList;

		// Token: 0x0400081F RID: 2079
		private string lastSyncRequestRandomString;

		// Token: 0x04000820 RID: 2080
		private byte[] lastCachableWbxmlDocument;

		// Token: 0x04000821 RID: 2081
		private bool clientCanSendUpEmptyRequests;

		// Token: 0x04000822 RID: 2082
		private ExDateTime? lastSyncAttemptTime;

		// Token: 0x04000823 RID: 2083
		private ExDateTime? lastSyncSuccessTime;

		// Token: 0x04000824 RID: 2084
		private string lastSyncUserAgent;

		// Token: 0x04000825 RID: 2085
		private GlobalInfo globalInfo;

		// Token: 0x04000826 RID: 2086
		private DeviceSyncStateMetadata deviceMetadata;

		// Token: 0x020000EE RID: 238
		[Flags]
		private enum GlobalInfoStateModified
		{
			// Token: 0x04000828 RID: 2088
			None = 0,
			// Token: 0x04000829 RID: 2089
			LastSyncAttemptTime = 1,
			// Token: 0x0400082A RID: 2090
			LastSyncSuccessTime = 2,
			// Token: 0x0400082B RID: 2091
			LastSyncUserAgent = 4,
			// Token: 0x0400082C RID: 2092
			ClientCategoryList = 8,
			// Token: 0x0400082D RID: 2093
			LastClientIdsSeen = 16,
			// Token: 0x0400082E RID: 2094
			All = 2147483647
		}
	}
}
