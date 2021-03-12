using System;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x02000039 RID: 57
	internal class AutdTriggerSyncStateInfo : CustomSyncStateInfo
	{
		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06000397 RID: 919 RVA: 0x0001563A File Offset: 0x0001383A
		// (set) Token: 0x06000398 RID: 920 RVA: 0x00015641 File Offset: 0x00013841
		public override string UniqueName
		{
			get
			{
				return "AutdTrigger";
			}
			set
			{
				throw new InvalidOperationException("AutdTriggerSyncStateInfo.UniqueName is not settable.");
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x06000399 RID: 921 RVA: 0x0001564D File Offset: 0x0001384D
		public override int Version
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x0600039A RID: 922 RVA: 0x00015650 File Offset: 0x00013850
		public override void HandleSyncStateVersioning(SyncState syncState)
		{
			base.HandleSyncStateVersioning(syncState);
		}

		// Token: 0x040002B1 RID: 689
		internal const string UniqueNameString = "AutdTrigger";
	}
}
