using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x020000EC RID: 236
	internal class NewAutdStatusData : IAutdStatusData
	{
		// Token: 0x17000518 RID: 1304
		// (get) Token: 0x06000D17 RID: 3351 RVA: 0x00047898 File Offset: 0x00045A98
		// (set) Token: 0x06000D18 RID: 3352 RVA: 0x000478A0 File Offset: 0x00045AA0
		public int? LastPingHeartbeat
		{
			get
			{
				return this.lastPingHeartbeat;
			}
			set
			{
				this.lastPingHeartbeat = value;
				this.globalStateModified = true;
			}
		}

		// Token: 0x17000519 RID: 1305
		// (get) Token: 0x06000D19 RID: 3353 RVA: 0x000478B0 File Offset: 0x00045AB0
		// (set) Token: 0x06000D1A RID: 3354 RVA: 0x000478B8 File Offset: 0x00045AB8
		public Dictionary<string, PingCommand.DPFolderInfo> DPFolderList
		{
			get
			{
				return this.dpFolderList;
			}
			set
			{
				this.dpFolderList = value;
				this.deviceMetadataStateChanged = true;
			}
		}

		// Token: 0x06000D1B RID: 3355 RVA: 0x000478C8 File Offset: 0x00045AC8
		public static IAutdStatusData Load(GlobalInfo globalInfo, SyncStateStorage syncStateStorage)
		{
			NewAutdStatusData newAutdStatusData = new NewAutdStatusData();
			newAutdStatusData.globalInfo = globalInfo;
			newAutdStatusData.deviceMetadata = syncStateStorage.DeviceMetadata;
			if (!globalInfo.HasNewAutdData)
			{
				using (AutdStatusData autdStatusData = AutdStatusData.Load(syncStateStorage, false, false))
				{
					if (autdStatusData != null)
					{
						newAutdStatusData.lastPingHeartbeat = autdStatusData.LastPingHeartbeat;
						newAutdStatusData.DPFolderList = ((autdStatusData.DPFolderList == null) ? null : new Dictionary<string, PingCommand.DPFolderInfo>(autdStatusData.DPFolderList));
					}
				}
				newAutdStatusData.deviceMetadataStateChanged = true;
				newAutdStatusData.globalStateModified = true;
			}
			else
			{
				newAutdStatusData.lastPingHeartbeat = globalInfo.LastPingHeartbeat;
				if (newAutdStatusData.deviceMetadata.PingFolderList != null)
				{
					newAutdStatusData.DPFolderList = ((newAutdStatusData.deviceMetadata.PingFolderList == null) ? null : new Dictionary<string, PingCommand.DPFolderInfo>((Dictionary<string, PingCommand.DPFolderInfo>)newAutdStatusData.deviceMetadata.PingFolderList));
				}
				newAutdStatusData.deviceMetadataStateChanged = false;
				newAutdStatusData.globalStateModified = false;
			}
			return newAutdStatusData;
		}

		// Token: 0x06000D1C RID: 3356 RVA: 0x000479AC File Offset: 0x00045BAC
		public void SaveAndDispose()
		{
			if (this.globalStateModified)
			{
				this.globalInfo.LastPingHeartbeat = this.lastPingHeartbeat;
			}
			if (this.deviceMetadataStateChanged)
			{
				this.deviceMetadata.PingFolderList = ((this.DPFolderList == null) ? null : new Dictionary<string, PingCommand.DPFolderInfo>(this.DPFolderList));
			}
			this.deviceMetadataStateChanged = false;
			this.globalStateModified = false;
		}

		// Token: 0x04000814 RID: 2068
		private bool globalStateModified;

		// Token: 0x04000815 RID: 2069
		private bool deviceMetadataStateChanged;

		// Token: 0x04000816 RID: 2070
		private int? lastPingHeartbeat;

		// Token: 0x04000817 RID: 2071
		private Dictionary<string, PingCommand.DPFolderInfo> dpFolderList;

		// Token: 0x04000818 RID: 2072
		private GlobalInfo globalInfo;

		// Token: 0x04000819 RID: 2073
		private DeviceSyncStateMetadata deviceMetadata;
	}
}
