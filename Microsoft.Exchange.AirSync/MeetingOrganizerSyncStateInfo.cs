using System;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x020000E1 RID: 225
	internal class MeetingOrganizerSyncStateInfo : CustomSyncStateInfo
	{
		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x06000CCF RID: 3279 RVA: 0x000448F4 File Offset: 0x00042AF4
		// (set) Token: 0x06000CD0 RID: 3280 RVA: 0x000448FB File Offset: 0x00042AFB
		public override string UniqueName
		{
			get
			{
				return "MeetingOrganizerInfo";
			}
			set
			{
				throw new InvalidOperationException("MeetingOrganizerSyncStateInfo.UniqueName is not settable.");
			}
		}

		// Token: 0x17000505 RID: 1285
		// (get) Token: 0x06000CD1 RID: 3281 RVA: 0x00044907 File Offset: 0x00042B07
		public override int Version
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06000CD2 RID: 3282 RVA: 0x0004490C File Offset: 0x00042B0C
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
			if (!(syncState.BackendVersion == this.Version))
			{
				syncState.HandleCorruptSyncState();
			}
		}

		// Token: 0x040007E9 RID: 2025
		internal const string UniqueNameString = "MeetingOrganizerInfo";

		// Token: 0x040007EA RID: 2026
		internal const int CurrentVersion = 0;

		// Token: 0x040007EB RID: 2027
		internal const int E14BaseVersion = 20;

		// Token: 0x020000E2 RID: 226
		internal struct PropertyNames
		{
			// Token: 0x040007EC RID: 2028
			internal const string MeetingOrganizerInfo = "MeetingOrganizerInfo";
		}
	}
}
