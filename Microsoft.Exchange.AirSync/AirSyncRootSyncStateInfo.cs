using System;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x02000023 RID: 35
	internal class AirSyncRootSyncStateInfo : CustomSyncStateInfo
	{
		// Token: 0x17000117 RID: 279
		// (get) Token: 0x0600029B RID: 667 RVA: 0x0000EFA5 File Offset: 0x0000D1A5
		// (set) Token: 0x0600029C RID: 668 RVA: 0x0000EFAC File Offset: 0x0000D1AC
		public override string UniqueName
		{
			get
			{
				return "AirSyncRoot";
			}
			set
			{
				throw new InvalidOperationException("AirSyncRootSyncStateInfo.UniqueName is not settable.");
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x0600029D RID: 669 RVA: 0x0000EFB8 File Offset: 0x0000D1B8
		public override int Version
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x0600029E RID: 670 RVA: 0x0000EFBB File Offset: 0x0000D1BB
		public override void HandleSyncStateVersioning(SyncState syncState)
		{
			base.HandleSyncStateVersioning(syncState);
		}

		// Token: 0x04000242 RID: 578
		internal const string UniqueNameString = "AirSyncRoot";

		// Token: 0x04000243 RID: 579
		internal const int CurrentVersion = 0;

		// Token: 0x04000244 RID: 580
		internal const int E14BaseVersion = 0;

		// Token: 0x02000024 RID: 36
		internal struct PropertyNames
		{
			// Token: 0x04000245 RID: 581
			internal const string LastMaxDevicesExceededMailSentTime = "LastMaxDevicesExceededMailSentTime";
		}
	}
}
