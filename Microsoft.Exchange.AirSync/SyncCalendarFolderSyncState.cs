using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.AirSync;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x02000257 RID: 599
	internal sealed class SyncCalendarFolderSyncState : SyncCalendarSyncStateBase, IFolderSyncState, ISyncState
	{
		// Token: 0x060015CF RID: 5583 RVA: 0x00081124 File Offset: 0x0007F324
		public SyncCalendarFolderSyncState(StoreObjectId folderId, ISyncProvider syncProvider, string base64SyncData) : base(folderId, syncProvider)
		{
			ExTraceGlobals.MethodEnterExitTracer.TraceDebug((long)this.GetHashCode(), "SyncCalendarFolderSyncState constructor called");
			base.Load(base64SyncData);
			if (base.Version < 1)
			{
				this.UpgradeSyncStateTable();
			}
			if (this.Watermark == null)
			{
				this.Watermark = syncProvider.CreateNewWatermark();
			}
		}

		// Token: 0x17000788 RID: 1928
		// (get) Token: 0x060015D0 RID: 5584 RVA: 0x00081179 File Offset: 0x0007F379
		// (set) Token: 0x060015D1 RID: 5585 RVA: 0x0008118B File Offset: 0x0007F38B
		public ISyncWatermark Watermark
		{
			get
			{
				return (ISyncWatermark)base["{9150227d-9140-45d0-b4c2-e987f59cfc46}SyncCalendar.SyncStateWatermark"];
			}
			set
			{
				if (this.Watermark != null)
				{
					base.Remove("{9150227d-9140-45d0-b4c2-e987f59cfc46}SyncCalendar.SyncStateWatermark");
				}
				base["{9150227d-9140-45d0-b4c2-e987f59cfc46}SyncCalendar.SyncStateWatermark"] = value;
			}
		}

		// Token: 0x17000789 RID: 1929
		// (get) Token: 0x060015D2 RID: 5586 RVA: 0x000811AC File Offset: 0x0007F3AC
		// (set) Token: 0x060015D3 RID: 5587 RVA: 0x000811D6 File Offset: 0x0007F3D6
		public ExDateTime OldestReceivedTime
		{
			get
			{
				if (base.Contains("{9150227d-9140-45d0-b4c2-e987f59cfc46}SyncCalendar.OldestReceivedTime"))
				{
					return ((DateTimeData)base["{9150227d-9140-45d0-b4c2-e987f59cfc46}SyncCalendar.OldestReceivedTime"]).Data;
				}
				return ExDateTime.UtcNow;
			}
			set
			{
				if (base.Contains("{9150227d-9140-45d0-b4c2-e987f59cfc46}SyncCalendar.OldestReceivedTime"))
				{
					base.Remove("{9150227d-9140-45d0-b4c2-e987f59cfc46}SyncCalendar.OldestReceivedTime");
				}
				base["{9150227d-9140-45d0-b4c2-e987f59cfc46}SyncCalendar.OldestReceivedTime"] = new DateTimeData(value);
			}
		}

		// Token: 0x1700078A RID: 1930
		// (get) Token: 0x060015D4 RID: 5588 RVA: 0x00081201 File Offset: 0x0007F401
		// (set) Token: 0x060015D5 RID: 5589 RVA: 0x00081227 File Offset: 0x0007F427
		public byte[] LastInstanceKey
		{
			get
			{
				if (base.Contains("{9150227d-9140-45d0-b4c2-e987f59cfc46}SyncCalendar.LastInstanceKey"))
				{
					return ((ByteArrayData)base["{9150227d-9140-45d0-b4c2-e987f59cfc46}SyncCalendar.LastInstanceKey"]).Data;
				}
				return null;
			}
			set
			{
				if (base.Contains("{9150227d-9140-45d0-b4c2-e987f59cfc46}SyncCalendar.LastInstanceKey"))
				{
					base.Remove("{9150227d-9140-45d0-b4c2-e987f59cfc46}SyncCalendar.LastInstanceKey");
				}
				base["{9150227d-9140-45d0-b4c2-e987f59cfc46}SyncCalendar.LastInstanceKey"] = new ByteArrayData(value);
			}
		}

		// Token: 0x1700078B RID: 1931
		// (get) Token: 0x060015D6 RID: 5590 RVA: 0x00081252 File Offset: 0x0007F452
		// (set) Token: 0x060015D7 RID: 5591 RVA: 0x00081278 File Offset: 0x0007F478
		public bool MoreItemsOnServer
		{
			get
			{
				return !base.Contains("{9150227d-9140-45d0-b4c2-e987f59cfc46}SyncCalendar.MoreItemsOnServer") || ((BooleanData)base["{9150227d-9140-45d0-b4c2-e987f59cfc46}SyncCalendar.MoreItemsOnServer"]).Data;
			}
			set
			{
				if (base.Contains("{9150227d-9140-45d0-b4c2-e987f59cfc46}SyncCalendar.MoreItemsOnServer"))
				{
					base.Remove("{9150227d-9140-45d0-b4c2-e987f59cfc46}SyncCalendar.MoreItemsOnServer");
				}
				base["{9150227d-9140-45d0-b4c2-e987f59cfc46}SyncCalendar.MoreItemsOnServer"] = new BooleanData(value);
			}
		}

		// Token: 0x1700078C RID: 1932
		// (get) Token: 0x060015D8 RID: 5592 RVA: 0x000812A3 File Offset: 0x0007F4A3
		internal override StringData SyncStateTag
		{
			get
			{
				return SyncCalendarFolderSyncState.FolderItemsSyncTagValue;
			}
		}

		// Token: 0x060015D9 RID: 5593 RVA: 0x000812AC File Offset: 0x0007F4AC
		protected override void InitializeSyncState(Dictionary<string, DerivedData<ICustomSerializableBuilder>> obj)
		{
			base.InitializeSyncState(obj);
			obj.Add("{9150227d-9140-45d0-b4c2-e987f59cfc46}SyncCalendar.OldestReceivedTime", new DerivedData<ICustomSerializableBuilder>(new DateTimeData(this.OldestReceivedTime)));
			obj.Add("{9150227d-9140-45d0-b4c2-e987f59cfc46}SyncCalendar.LastInstanceKey", new DerivedData<ICustomSerializableBuilder>(new ByteArrayData(this.LastInstanceKey)));
			obj.Add("{9150227d-9140-45d0-b4c2-e987f59cfc46}SyncCalendar.MoreItemsOnServer", new DerivedData<ICustomSerializableBuilder>(new BooleanData(this.MoreItemsOnServer)));
		}

		// Token: 0x060015DA RID: 5594 RVA: 0x00081314 File Offset: 0x0007F514
		protected override void VerifySyncState(Dictionary<string, DerivedData<ICustomSerializableBuilder>> obj)
		{
			base.VerifySyncState(obj);
			if (base.Version >= 1)
			{
				DateTimeData dateTimeData = obj["{9150227d-9140-45d0-b4c2-e987f59cfc46}SyncCalendar.OldestReceivedTime"].Data as DateTimeData;
				ByteArrayData byteArrayData = obj["{9150227d-9140-45d0-b4c2-e987f59cfc46}SyncCalendar.LastInstanceKey"].Data as ByteArrayData;
				BooleanData booleanData = obj["{9150227d-9140-45d0-b4c2-e987f59cfc46}SyncCalendar.MoreItemsOnServer"].Data as BooleanData;
				if (dateTimeData == null || byteArrayData == null || booleanData == null)
				{
					throw new CorruptSyncStateException("Empty mandatory key", null);
				}
			}
		}

		// Token: 0x060015DB RID: 5595 RVA: 0x00081388 File Offset: 0x0007F588
		public FolderSync GetFolderSync()
		{
			return this.GetFolderSync(ConflictResolutionPolicy.ServerWins);
		}

		// Token: 0x060015DC RID: 5596 RVA: 0x00081391 File Offset: 0x0007F591
		public FolderSync GetFolderSync(ConflictResolutionPolicy policy)
		{
			return new FolderSync(this.syncProvider, this, policy, false);
		}

		// Token: 0x060015DD RID: 5597 RVA: 0x000813A1 File Offset: 0x0007F5A1
		public void OnCommitStateModifications(FolderSyncStateUtil.CommitStateModificationsDelegate commitStateModificationsDelegate)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060015DE RID: 5598 RVA: 0x000813A8 File Offset: 0x0007F5A8
		private void UpgradeSyncStateTable()
		{
			ExTraceGlobals.MethodEnterExitTracer.TraceDebug((long)this.GetHashCode(), "SyncCalendarFolderSyncState.UpgradeSyncStateTable called");
			ISyncWatermark watermark = base.Contains(SyncStateProp.CurMaxWatermark) ? ((ISyncWatermark)base[SyncStateProp.CurMaxWatermark]) : null;
			base.Version = 1;
			base.InitializeSyncStateTable();
			this.Watermark = watermark;
		}

		// Token: 0x04000D8F RID: 3471
		private const string SyncStateWatermarkKeyName = "{9150227d-9140-45d0-b4c2-e987f59cfc46}SyncCalendar.SyncStateWatermark";

		// Token: 0x04000D90 RID: 3472
		private const string SyncStateOldestReceivedTimeKeyName = "{9150227d-9140-45d0-b4c2-e987f59cfc46}SyncCalendar.OldestReceivedTime";

		// Token: 0x04000D91 RID: 3473
		private const string SyncStateLastInstanceKeyKeyName = "{9150227d-9140-45d0-b4c2-e987f59cfc46}SyncCalendar.LastInstanceKey";

		// Token: 0x04000D92 RID: 3474
		private const string SyncStateMoreItemsOnServerKeyName = "{9150227d-9140-45d0-b4c2-e987f59cfc46}SyncCalendar.MoreItemsOnServer";

		// Token: 0x04000D93 RID: 3475
		public const int E15RtmFolderSyncStateVersion = 1;

		// Token: 0x04000D94 RID: 3476
		public const int CurrentFolderSyncStateVersion = 1;

		// Token: 0x04000D95 RID: 3477
		internal static StringData FolderItemsSyncTagValue = new StringData("SyncCalendar.FolderItemsSync");
	}
}
