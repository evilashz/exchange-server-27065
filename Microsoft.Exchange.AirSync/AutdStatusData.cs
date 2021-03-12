using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.AirSync;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x02000036 RID: 54
	internal class AutdStatusData : DisposeTrackableBase, IAutdStatusData
	{
		// Token: 0x17000162 RID: 354
		// (get) Token: 0x06000389 RID: 905 RVA: 0x00015418 File Offset: 0x00013618
		// (set) Token: 0x0600038A RID: 906 RVA: 0x00015453 File Offset: 0x00013653
		public int? LastPingHeartbeat
		{
			get
			{
				UInt32Data uint32Data = (UInt32Data)this.autdStatusSyncState[CustomStateDatumType.LastPingHeartbeat];
				if (uint32Data != null)
				{
					return new int?((int)uint32Data.Data);
				}
				return null;
			}
			set
			{
				if (value == null)
				{
					this.autdStatusSyncState.Remove(CustomStateDatumType.LastPingHeartbeat);
				}
				else
				{
					this.autdStatusSyncState[CustomStateDatumType.LastPingHeartbeat] = new UInt32Data((uint)value.Value);
				}
				this.dirty = true;
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x0600038B RID: 907 RVA: 0x00015494 File Offset: 0x00013694
		// (set) Token: 0x0600038C RID: 908 RVA: 0x000154C2 File Offset: 0x000136C2
		public Dictionary<string, PingCommand.DPFolderInfo> DPFolderList
		{
			get
			{
				GenericDictionaryData<StringData, string, PingCommand.DPFolderInfo> genericDictionaryData = (GenericDictionaryData<StringData, string, PingCommand.DPFolderInfo>)this.autdStatusSyncState[CustomStateDatumType.DPFolderList];
				if (genericDictionaryData != null)
				{
					return genericDictionaryData.Data;
				}
				return null;
			}
			set
			{
				if (value == null)
				{
					this.autdStatusSyncState.Remove(CustomStateDatumType.DPFolderList);
				}
				else
				{
					this.autdStatusSyncState[CustomStateDatumType.DPFolderList] = new GenericDictionaryData<StringData, string, PingCommand.DPFolderInfo>(value);
				}
				this.dirty = true;
			}
		}

		// Token: 0x0600038D RID: 909 RVA: 0x000154F8 File Offset: 0x000136F8
		public static AutdStatusData Load(SyncStateStorage syncStateStorage, bool readOnly, bool createIfMissing)
		{
			AutdStatusData autdStatusData = null;
			bool flag = false;
			try
			{
				autdStatusData = new AutdStatusData();
				AutdSyncStateInfo autdSyncStateInfo = new AutdSyncStateInfo();
				autdSyncStateInfo.ReadOnly = readOnly;
				autdStatusData.autdStatusSyncState = syncStateStorage.GetCustomSyncState(autdSyncStateInfo, new PropertyDefinition[0]);
				if (autdStatusData.autdStatusSyncState == null)
				{
					if (!createIfMissing)
					{
						return null;
					}
					autdStatusData.autdStatusSyncState = syncStateStorage.CreateCustomSyncState(autdSyncStateInfo);
				}
				autdStatusData.dirty = false;
				flag = true;
			}
			finally
			{
				if (!flag && autdStatusData != null)
				{
					autdStatusData.Dispose();
					autdStatusData = null;
				}
			}
			return autdStatusData;
		}

		// Token: 0x0600038E RID: 910 RVA: 0x00015578 File Offset: 0x00013778
		public void SaveAndDispose()
		{
			if (this.autdStatusSyncState != null && this.dirty)
			{
				try
				{
					this.autdStatusSyncState.Commit();
					this.dirty = false;
				}
				catch (LocalizedException arg)
				{
					AirSyncDiagnostics.TraceError<LocalizedException>(ExTraceGlobals.RequestsTracer, this, "[SyncData.Save] Failed to commit autdStatusSyncState. Exception: {0}", arg);
				}
				finally
				{
					this.Dispose();
				}
			}
		}

		// Token: 0x0600038F RID: 911 RVA: 0x000155E4 File Offset: 0x000137E4
		protected override void InternalDispose(bool disposing)
		{
			if (disposing && this.autdStatusSyncState != null)
			{
				this.autdStatusSyncState.Dispose();
				this.autdStatusSyncState = null;
			}
		}

		// Token: 0x06000390 RID: 912 RVA: 0x00015603 File Offset: 0x00013803
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<AutdStatusData>(this);
		}

		// Token: 0x040002AC RID: 684
		private bool dirty;

		// Token: 0x040002AD RID: 685
		private CustomSyncState autdStatusSyncState;
	}
}
