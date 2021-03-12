using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Cluster.Replay.Monitoring
{
	// Token: 0x020001D0 RID: 464
	[DataContract(Name = "DbHealthInfo", Namespace = "http://Microsoft.Exchange.HA.Monitoring/InternalMonitoringService/v1/")]
	public class DbHealthInfoPersisted
	{
		// Token: 0x17000514 RID: 1300
		// (get) Token: 0x0600127D RID: 4733 RVA: 0x0004B998 File Offset: 0x00049B98
		// (set) Token: 0x0600127E RID: 4734 RVA: 0x0004B9A0 File Offset: 0x00049BA0
		[DataMember(Order = 1)]
		public Guid DbGuid { get; private set; }

		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x0600127F RID: 4735 RVA: 0x0004B9A9 File Offset: 0x00049BA9
		// (set) Token: 0x06001280 RID: 4736 RVA: 0x0004B9B1 File Offset: 0x00049BB1
		[DataMember(Order = 2)]
		public string DbName { get; private set; }

		// Token: 0x17000516 RID: 1302
		// (get) Token: 0x06001281 RID: 4737 RVA: 0x0004B9BA File Offset: 0x00049BBA
		// (set) Token: 0x06001282 RID: 4738 RVA: 0x0004B9C2 File Offset: 0x00049BC2
		[DataMember(Order = 3)]
		public TransientErrorInfoPersisted DbFoundInAD { get; set; }

		// Token: 0x17000517 RID: 1303
		// (get) Token: 0x06001283 RID: 4739 RVA: 0x0004B9CB File Offset: 0x00049BCB
		// (set) Token: 0x06001284 RID: 4740 RVA: 0x0004B9D3 File Offset: 0x00049BD3
		[DataMember(Order = 4)]
		public TransientErrorInfoPersisted IsAtLeast2RedundantCopy { get; set; }

		// Token: 0x17000518 RID: 1304
		// (get) Token: 0x06001285 RID: 4741 RVA: 0x0004B9DC File Offset: 0x00049BDC
		// (set) Token: 0x06001286 RID: 4742 RVA: 0x0004B9E4 File Offset: 0x00049BE4
		[DataMember(Order = 5)]
		public TransientErrorInfoPersisted IsAtLeast3RedundantCopy { get; set; }

		// Token: 0x17000519 RID: 1305
		// (get) Token: 0x06001287 RID: 4743 RVA: 0x0004B9ED File Offset: 0x00049BED
		// (set) Token: 0x06001288 RID: 4744 RVA: 0x0004B9F5 File Offset: 0x00049BF5
		[DataMember(Order = 6)]
		public TransientErrorInfoPersisted IsAtLeast4RedundantCopy { get; set; }

		// Token: 0x1700051A RID: 1306
		// (get) Token: 0x06001289 RID: 4745 RVA: 0x0004B9FE File Offset: 0x00049BFE
		// (set) Token: 0x0600128A RID: 4746 RVA: 0x0004BA06 File Offset: 0x00049C06
		[DataMember(Order = 7)]
		public TransientErrorInfoPersisted IsAtLeast2AvailableCopy { get; set; }

		// Token: 0x1700051B RID: 1307
		// (get) Token: 0x0600128B RID: 4747 RVA: 0x0004BA0F File Offset: 0x00049C0F
		// (set) Token: 0x0600128C RID: 4748 RVA: 0x0004BA17 File Offset: 0x00049C17
		[DataMember(Order = 8)]
		public TransientErrorInfoPersisted IsAtLeast3AvailableCopy { get; set; }

		// Token: 0x1700051C RID: 1308
		// (get) Token: 0x0600128D RID: 4749 RVA: 0x0004BA20 File Offset: 0x00049C20
		// (set) Token: 0x0600128E RID: 4750 RVA: 0x0004BA28 File Offset: 0x00049C28
		[DataMember(Order = 9)]
		public TransientErrorInfoPersisted IsAtLeast4AvailableCopy { get; set; }

		// Token: 0x1700051D RID: 1309
		// (get) Token: 0x0600128F RID: 4751 RVA: 0x0004BA31 File Offset: 0x00049C31
		// (set) Token: 0x06001290 RID: 4752 RVA: 0x0004BA39 File Offset: 0x00049C39
		[DataMember(Order = 10)]
		public List<DbCopyHealthInfoPersisted> DbCopies { get; set; }

		// Token: 0x1700051E RID: 1310
		// (get) Token: 0x06001291 RID: 4753 RVA: 0x0004BA42 File Offset: 0x00049C42
		// (set) Token: 0x06001292 RID: 4754 RVA: 0x0004BA4A File Offset: 0x00049C4A
		[DataMember]
		public TransientErrorInfoPersisted SkippedFromMonitoring { get; set; }

		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x06001293 RID: 4755 RVA: 0x0004BA53 File Offset: 0x00049C53
		// (set) Token: 0x06001294 RID: 4756 RVA: 0x0004BA5B File Offset: 0x00049C5B
		[DataMember]
		public TransientErrorInfoPersisted IsAtLeast1RedundantCopy { get; set; }

		// Token: 0x17000520 RID: 1312
		// (get) Token: 0x06001295 RID: 4757 RVA: 0x0004BA64 File Offset: 0x00049C64
		// (set) Token: 0x06001296 RID: 4758 RVA: 0x0004BA6C File Offset: 0x00049C6C
		[DataMember]
		public TransientErrorInfoPersisted IsAtLeast1AvailableCopy { get; set; }

		// Token: 0x06001297 RID: 4759 RVA: 0x0004BA75 File Offset: 0x00049C75
		public DbHealthInfoPersisted(Guid dbGuid, string dbName)
		{
			this.DbGuid = dbGuid;
			this.DbName = dbName;
			this.DbCopies = new List<DbCopyHealthInfoPersisted>(5);
		}
	}
}
