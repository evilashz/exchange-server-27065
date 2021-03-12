using System;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x020000A0 RID: 160
	internal class FolderIdMappingSyncStateInfo : CustomSyncStateInfo
	{
		// Token: 0x17000365 RID: 869
		// (get) Token: 0x060008F8 RID: 2296 RVA: 0x00035892 File Offset: 0x00033A92
		// (set) Token: 0x060008F9 RID: 2297 RVA: 0x00035899 File Offset: 0x00033A99
		public override string UniqueName
		{
			get
			{
				return "FolderIdMapping";
			}
			set
			{
				throw new InvalidOperationException("FolderIdMappingSyncStateInfo.UniqueName is not settable.");
			}
		}

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x060008FA RID: 2298 RVA: 0x000358A5 File Offset: 0x00033AA5
		public override int Version
		{
			get
			{
				return 4;
			}
		}

		// Token: 0x060008FB RID: 2299 RVA: 0x000358A8 File Offset: 0x00033AA8
		public override void HandleSyncStateVersioning(SyncState syncState)
		{
			if (syncState == null)
			{
				throw new ArgumentNullException("syncState");
			}
			if (syncState.BackendVersion == null)
			{
				return;
			}
			bool flag = true;
			if (syncState.BackendVersion < 2 || syncState.BackendVersion > this.Version)
			{
				flag = false;
			}
			else if (syncState.BackendVersion.Value != this.Version)
			{
				switch (syncState.BackendVersion.Value)
				{
				case 2:
					FolderTree.BuildFolderTree(syncState.StoreObject.Session as MailboxSession, syncState);
					break;
				case 3:
					break;
				default:
					flag = false;
					break;
				}
			}
			if (!flag)
			{
				syncState.HandleCorruptSyncState();
			}
		}

		// Token: 0x040005AB RID: 1451
		internal const string UniqueNameString = "FolderIdMapping";

		// Token: 0x040005AC RID: 1452
		internal const int E12BaseVersion = 2;

		// Token: 0x020000A1 RID: 161
		internal struct PropertyNames
		{
			// Token: 0x040005AD RID: 1453
			internal const string IdMapping = "IdMapping";

			// Token: 0x040005AE RID: 1454
			internal const string FullFolderTree = "FullFolderTree";

			// Token: 0x040005AF RID: 1455
			internal const string RecoveryFullFolderTree = "RecoveryFullFolderTree";
		}
	}
}
