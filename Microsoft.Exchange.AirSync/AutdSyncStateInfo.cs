using System;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x02000037 RID: 55
	internal class AutdSyncStateInfo : CustomSyncStateInfo
	{
		// Token: 0x17000164 RID: 356
		// (get) Token: 0x06000392 RID: 914 RVA: 0x00015613 File Offset: 0x00013813
		// (set) Token: 0x06000393 RID: 915 RVA: 0x0001561A File Offset: 0x0001381A
		public override string UniqueName
		{
			get
			{
				return "Autd";
			}
			set
			{
				throw new InvalidOperationException("AutdSyncStateInfo.UniqueName is not settable.");
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x06000394 RID: 916 RVA: 0x00015626 File Offset: 0x00013826
		public override int Version
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x06000395 RID: 917 RVA: 0x00015629 File Offset: 0x00013829
		public override void HandleSyncStateVersioning(SyncState syncState)
		{
			base.HandleSyncStateVersioning(syncState);
		}

		// Token: 0x040002AE RID: 686
		internal const string UniqueNameString = "Autd";

		// Token: 0x02000038 RID: 56
		internal struct PropertyNames
		{
			// Token: 0x040002AF RID: 687
			internal const string DPFolderList = "DPFolderList";

			// Token: 0x040002B0 RID: 688
			internal const string LastPingHeartbeat = "LastPingHeartbeat";
		}
	}
}
