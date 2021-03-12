using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Cluster.Replay.Monitoring
{
	// Token: 0x020001CF RID: 463
	[DataContract(Name = "DbCopyHealthInfo", Namespace = "http://Microsoft.Exchange.HA.Monitoring/InternalMonitoringService/v1/")]
	public class DbCopyHealthInfoPersisted
	{
		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x06001266 RID: 4710 RVA: 0x0004B8C7 File Offset: 0x00049AC7
		// (set) Token: 0x06001267 RID: 4711 RVA: 0x0004B8CF File Offset: 0x00049ACF
		public Guid DbGuid { get; private set; }

		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x06001268 RID: 4712 RVA: 0x0004B8D8 File Offset: 0x00049AD8
		// (set) Token: 0x06001269 RID: 4713 RVA: 0x0004B8E0 File Offset: 0x00049AE0
		[DataMember(Name = "FQDN", Order = 1)]
		public string ServerFqdn { get; private set; }

		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x0600126A RID: 4714 RVA: 0x0004B8E9 File Offset: 0x00049AE9
		// (set) Token: 0x0600126B RID: 4715 RVA: 0x0004B8F1 File Offset: 0x00049AF1
		[DataMember(Order = 2)]
		public TransientErrorInfoPersisted CopyFoundInAD { get; set; }

		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x0600126C RID: 4716 RVA: 0x0004B8FA File Offset: 0x00049AFA
		// (set) Token: 0x0600126D RID: 4717 RVA: 0x0004B902 File Offset: 0x00049B02
		[DataMember(Order = 3)]
		public TransientErrorInfoPersisted CopyStatusRetrieved { get; set; }

		// Token: 0x1700050D RID: 1293
		// (get) Token: 0x0600126E RID: 4718 RVA: 0x0004B90B File Offset: 0x00049B0B
		// (set) Token: 0x0600126F RID: 4719 RVA: 0x0004B913 File Offset: 0x00049B13
		[DataMember(Order = 4)]
		public TransientErrorInfoPersisted CopyIsAvailable { get; set; }

		// Token: 0x1700050E RID: 1294
		// (get) Token: 0x06001270 RID: 4720 RVA: 0x0004B91C File Offset: 0x00049B1C
		// (set) Token: 0x06001271 RID: 4721 RVA: 0x0004B924 File Offset: 0x00049B24
		[DataMember(Order = 5)]
		public TransientErrorInfoPersisted CopyIsRedundant { get; set; }

		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x06001272 RID: 4722 RVA: 0x0004B92D File Offset: 0x00049B2D
		// (set) Token: 0x06001273 RID: 4723 RVA: 0x0004B935 File Offset: 0x00049B35
		[DataMember(Order = 6)]
		public TransientErrorInfoPersisted CopyStatusHealthy { get; set; }

		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x06001274 RID: 4724 RVA: 0x0004B93E File Offset: 0x00049B3E
		// (set) Token: 0x06001275 RID: 4725 RVA: 0x0004B946 File Offset: 0x00049B46
		[DataMember(Order = 7)]
		public DateTime LastCopyStatusTransitionTime { get; set; }

		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x06001276 RID: 4726 RVA: 0x0004B94F File Offset: 0x00049B4F
		// (set) Token: 0x06001277 RID: 4727 RVA: 0x0004B957 File Offset: 0x00049B57
		[DataMember(Order = 8)]
		public TransientErrorInfoPersisted CopyStatusActive { get; set; }

		// Token: 0x17000512 RID: 1298
		// (get) Token: 0x06001278 RID: 4728 RVA: 0x0004B960 File Offset: 0x00049B60
		// (set) Token: 0x06001279 RID: 4729 RVA: 0x0004B968 File Offset: 0x00049B68
		[DataMember(Order = 9)]
		public TransientErrorInfoPersisted CopyStatusMounted { get; set; }

		// Token: 0x17000513 RID: 1299
		// (get) Token: 0x0600127A RID: 4730 RVA: 0x0004B971 File Offset: 0x00049B71
		// (set) Token: 0x0600127B RID: 4731 RVA: 0x0004B979 File Offset: 0x00049B79
		[DataMember(Order = 10)]
		public DateTime LastTouchedTime { get; set; }

		// Token: 0x0600127C RID: 4732 RVA: 0x0004B982 File Offset: 0x00049B82
		public DbCopyHealthInfoPersisted(Guid dbGuid, string serverFqdn)
		{
			this.DbGuid = dbGuid;
			this.ServerFqdn = serverFqdn;
		}
	}
}
