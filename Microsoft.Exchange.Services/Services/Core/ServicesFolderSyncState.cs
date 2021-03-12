using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x020003D7 RID: 983
	internal sealed class ServicesFolderSyncState : ServicesSyncStateBase, IFolderSyncState, ISyncState
	{
		// Token: 0x06001B7D RID: 7037 RVA: 0x0009C6F4 File Offset: 0x0009A8F4
		public ServicesFolderSyncState(StoreObjectId folderId, ISyncProvider syncProvider, string base64SyncData) : base(folderId, syncProvider)
		{
			ExTraceGlobals.SynchronizationTracer.TraceDebug((long)this.GetHashCode(), "ServicesFolderSyncState constructor called");
			base.Load(base64SyncData);
			if (base.Version < 3)
			{
				ExTraceGlobals.SynchronizationTracer.TraceDebug<int, int>((long)this.GetHashCode(), "The sync state version '{0}' is less than the current version '{1}', hence upgrading it.", base.Version, 3);
				RequestDetailsLogger.Current.AppendGenericInfo("SSUpgrade_Version", string.Format("V{0}-V{1}", base.Version, 3));
				this.UpgradeSyncStateTable();
			}
			if (this.Watermark == null)
			{
				ExTraceGlobals.SynchronizationTracer.TraceDebug((long)this.GetHashCode(), "Syncstate watermark was null.  Creating a new one.");
				RequestDetailsLogger.Current.AppendGenericInfo("SSUpgrade_Watermark", "Null");
				this.Watermark = syncProvider.CreateNewWatermark();
			}
		}

		// Token: 0x06001B7E RID: 7038 RVA: 0x0009C7BC File Offset: 0x0009A9BC
		protected override void InitializeSyncState(Dictionary<string, DerivedData<ICustomSerializableBuilder>> obj)
		{
			base.InitializeSyncState(obj);
			obj.Add("{49A4350A-C7A8-4AC3-BCBC-F2B8CB7F9550}WS.OldestReceivedTime", new DerivedData<ICustomSerializableBuilder>(new DateTimeData(this.OldestReceivedTime)));
			obj.Add("{49A4350A-C7A8-4AC3-BCBC-F2B8CB7F9550}WS.LastInstanceKey", new DerivedData<ICustomSerializableBuilder>(new ByteArrayData(this.LastInstanceKey)));
			obj.Add("{49A4350A-C7A8-4AC3-BCBC-F2B8CB7F9550}WS.MoreItemsOnServer", new DerivedData<ICustomSerializableBuilder>(new BooleanData(this.MoreItemsOnServer)));
		}

		// Token: 0x06001B7F RID: 7039 RVA: 0x0009C824 File Offset: 0x0009AA24
		protected override void VerifySyncState(Dictionary<string, DerivedData<ICustomSerializableBuilder>> obj)
		{
			base.VerifySyncState(obj);
			if (base.Version >= 3)
			{
				DateTimeData dateTimeData = obj["{49A4350A-C7A8-4AC3-BCBC-F2B8CB7F9550}WS.OldestReceivedTime"].Data as DateTimeData;
				ByteArrayData byteArrayData = obj["{49A4350A-C7A8-4AC3-BCBC-F2B8CB7F9550}WS.LastInstanceKey"].Data as ByteArrayData;
				BooleanData booleanData = obj["{49A4350A-C7A8-4AC3-BCBC-F2B8CB7F9550}WS.MoreItemsOnServer"].Data as BooleanData;
				if (dateTimeData == null || byteArrayData == null || booleanData == null)
				{
					RequestDetailsLogger.Current.AppendGenericError("VerifySyncState", "NullValuesInDict");
					throw new InvalidSyncStateDataException();
				}
			}
		}

		// Token: 0x06001B80 RID: 7040 RVA: 0x0009C8A6 File Offset: 0x0009AAA6
		public FolderSync GetFolderSync()
		{
			return this.GetFolderSync(ConflictResolutionPolicy.ServerWins);
		}

		// Token: 0x06001B81 RID: 7041 RVA: 0x0009C8AF File Offset: 0x0009AAAF
		public FolderSync GetFolderSync(ConflictResolutionPolicy policy)
		{
			return new FolderSync(this.syncProvider, this, policy, false);
		}

		// Token: 0x06001B82 RID: 7042 RVA: 0x0009C8BF File Offset: 0x0009AABF
		public void OnCommitStateModifications(FolderSyncStateUtil.CommitStateModificationsDelegate commitStateModificationsDelegate)
		{
		}

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x06001B83 RID: 7043 RVA: 0x0009C8C1 File Offset: 0x0009AAC1
		// (set) Token: 0x06001B84 RID: 7044 RVA: 0x0009C8D3 File Offset: 0x0009AAD3
		public ISyncWatermark Watermark
		{
			get
			{
				return (ISyncWatermark)base["{49A4350A-C7A8-4AC3-BCBC-F2B8CB7F9550}WS.SyncStateWatermark"];
			}
			set
			{
				if (this.Watermark != null)
				{
					base.Remove("{49A4350A-C7A8-4AC3-BCBC-F2B8CB7F9550}WS.SyncStateWatermark");
				}
				base["{49A4350A-C7A8-4AC3-BCBC-F2B8CB7F9550}WS.SyncStateWatermark"] = value;
			}
		}

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x06001B85 RID: 7045 RVA: 0x0009C8F4 File Offset: 0x0009AAF4
		// (set) Token: 0x06001B86 RID: 7046 RVA: 0x0009C906 File Offset: 0x0009AB06
		public ExDateTime OldestReceivedTime
		{
			get
			{
				return this.SafeGet<ExDateTime, DateTimeData>("{49A4350A-C7A8-4AC3-BCBC-F2B8CB7F9550}WS.OldestReceivedTime", ExDateTime.UtcNow);
			}
			set
			{
				this.SafeSet<ExDateTime, DateTimeData>("{49A4350A-C7A8-4AC3-BCBC-F2B8CB7F9550}WS.OldestReceivedTime", new DateTimeData(value));
			}
		}

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x06001B87 RID: 7047 RVA: 0x0009C919 File Offset: 0x0009AB19
		// (set) Token: 0x06001B88 RID: 7048 RVA: 0x0009C927 File Offset: 0x0009AB27
		public byte[] LastInstanceKey
		{
			get
			{
				return this.SafeGet<byte[], ByteArrayData>("{49A4350A-C7A8-4AC3-BCBC-F2B8CB7F9550}WS.LastInstanceKey", null);
			}
			set
			{
				this.SafeSet<byte[], ByteArrayData>("{49A4350A-C7A8-4AC3-BCBC-F2B8CB7F9550}WS.LastInstanceKey", new ByteArrayData(value));
			}
		}

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x06001B89 RID: 7049 RVA: 0x0009C93A File Offset: 0x0009AB3A
		// (set) Token: 0x06001B8A RID: 7050 RVA: 0x0009C948 File Offset: 0x0009AB48
		public bool MoreItemsOnServer
		{
			get
			{
				return this.SafeGet<bool, BooleanData>("{49A4350A-C7A8-4AC3-BCBC-F2B8CB7F9550}WS.MoreItemsOnServer", true);
			}
			set
			{
				this.SafeSet<bool, BooleanData>("{49A4350A-C7A8-4AC3-BCBC-F2B8CB7F9550}WS.MoreItemsOnServer", new BooleanData(value));
			}
		}

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x06001B8B RID: 7051 RVA: 0x0009C95B File Offset: 0x0009AB5B
		internal override StringData SyncStateTag
		{
			get
			{
				return ServicesFolderSyncState.FolderItemsSyncTagValue;
			}
		}

		// Token: 0x06001B8C RID: 7052 RVA: 0x0009C964 File Offset: 0x0009AB64
		internal ReturnType SafeGet<ReturnType, DataType>(string keyName, ReturnType defaultValue) where DataType : ComponentData<ReturnType>
		{
			if (!base.Contains(keyName))
			{
				return defaultValue;
			}
			DataType dataType = (DataType)((object)base[keyName]);
			return dataType.Data;
		}

		// Token: 0x06001B8D RID: 7053 RVA: 0x0009C996 File Offset: 0x0009AB96
		internal void SafeSet<DataType, WrapperType>(string keyName, WrapperType wrapper) where WrapperType : ComponentData<DataType>
		{
			if (base.Contains(keyName))
			{
				base.Remove(keyName);
			}
			base[keyName] = wrapper;
		}

		// Token: 0x06001B8E RID: 7054 RVA: 0x0009C9B8 File Offset: 0x0009ABB8
		private void UpgradeSyncStateTable()
		{
			ISyncWatermark syncWatermark = base.Contains(SyncStateProp.CurMaxWatermark) ? ((ISyncWatermark)base[SyncStateProp.CurMaxWatermark]) : null;
			if (syncWatermark == null)
			{
				syncWatermark = (base.Contains("{49A4350A-C7A8-4AC3-BCBC-F2B8CB7F9550}WS.SyncStateWatermark") ? ((ISyncWatermark)base["{49A4350A-C7A8-4AC3-BCBC-F2B8CB7F9550}WS.SyncStateWatermark"]) : null);
			}
			base.Version = 3;
			base.InitializeSyncStateTable();
			this.Watermark = syncWatermark;
		}

		// Token: 0x04001215 RID: 4629
		private const string SyncStateWatermarkKeyName = "{49A4350A-C7A8-4AC3-BCBC-F2B8CB7F9550}WS.SyncStateWatermark";

		// Token: 0x04001216 RID: 4630
		private const string SyncStateOldestReceivedTimeKeyName = "{49A4350A-C7A8-4AC3-BCBC-F2B8CB7F9550}WS.OldestReceivedTime";

		// Token: 0x04001217 RID: 4631
		private const string SyncStateLastInstanceKeyKeyName = "{49A4350A-C7A8-4AC3-BCBC-F2B8CB7F9550}WS.LastInstanceKey";

		// Token: 0x04001218 RID: 4632
		private const string SyncStateMoreItemsOnServerKeyName = "{49A4350A-C7A8-4AC3-BCBC-F2B8CB7F9550}WS.MoreItemsOnServer";

		// Token: 0x04001219 RID: 4633
		public const int E15RtmFolderSyncStateVersion = 3;

		// Token: 0x0400121A RID: 4634
		public const int E14RtmFolderSyncStateVersion = 2;

		// Token: 0x0400121B RID: 4635
		public const int E12RtmFolderSyncStateVersion = 1;

		// Token: 0x0400121C RID: 4636
		public const int CurrentFolderSyncStateVersion = 3;

		// Token: 0x0400121D RID: 4637
		internal static StringData FolderItemsSyncTagValue = new StringData("WS.FolderItemsSync");
	}
}
