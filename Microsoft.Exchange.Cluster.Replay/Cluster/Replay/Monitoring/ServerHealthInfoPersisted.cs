using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Cluster.Replay.Monitoring
{
	// Token: 0x020001CE RID: 462
	[DataContract(Name = "ServerHealthInfo", Namespace = "http://Microsoft.Exchange.HA.Monitoring/InternalMonitoringService/v1/")]
	public class ServerHealthInfoPersisted
	{
		// Token: 0x17000501 RID: 1281
		// (get) Token: 0x06001255 RID: 4693 RVA: 0x0004B830 File Offset: 0x00049A30
		// (set) Token: 0x06001256 RID: 4694 RVA: 0x0004B838 File Offset: 0x00049A38
		[DataMember(Name = "FQDN", Order = 1)]
		public string ServerFqdn { get; private set; }

		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x06001257 RID: 4695 RVA: 0x0004B841 File Offset: 0x00049A41
		// (set) Token: 0x06001258 RID: 4696 RVA: 0x0004B849 File Offset: 0x00049A49
		[DataMember(Order = 2)]
		public TransientErrorInfoPersisted ServerFoundInAD { get; set; }

		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x06001259 RID: 4697 RVA: 0x0004B852 File Offset: 0x00049A52
		// (set) Token: 0x0600125A RID: 4698 RVA: 0x0004B85A File Offset: 0x00049A5A
		[DataMember(Order = 3)]
		public TransientErrorInfoPersisted CriticalForMaintainingAvailability { get; set; }

		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x0600125B RID: 4699 RVA: 0x0004B863 File Offset: 0x00049A63
		// (set) Token: 0x0600125C RID: 4700 RVA: 0x0004B86B File Offset: 0x00049A6B
		[DataMember(Order = 4)]
		public TransientErrorInfoPersisted CriticalForMaintainingRedundancy { get; set; }

		// Token: 0x17000505 RID: 1285
		// (get) Token: 0x0600125D RID: 4701 RVA: 0x0004B874 File Offset: 0x00049A74
		// (set) Token: 0x0600125E RID: 4702 RVA: 0x0004B87C File Offset: 0x00049A7C
		[DataMember(Order = 5)]
		public TransientErrorInfoPersisted CriticalForRestoringAvailability { get; set; }

		// Token: 0x17000506 RID: 1286
		// (get) Token: 0x0600125F RID: 4703 RVA: 0x0004B885 File Offset: 0x00049A85
		// (set) Token: 0x06001260 RID: 4704 RVA: 0x0004B88D File Offset: 0x00049A8D
		[DataMember(Order = 6)]
		public TransientErrorInfoPersisted CriticalForRestoringRedundancy { get; set; }

		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x06001261 RID: 4705 RVA: 0x0004B896 File Offset: 0x00049A96
		// (set) Token: 0x06001262 RID: 4706 RVA: 0x0004B89E File Offset: 0x00049A9E
		[DataMember(Order = 7)]
		public TransientErrorInfoPersisted HighForRestoringAvailability { get; set; }

		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x06001263 RID: 4707 RVA: 0x0004B8A7 File Offset: 0x00049AA7
		// (set) Token: 0x06001264 RID: 4708 RVA: 0x0004B8AF File Offset: 0x00049AAF
		[DataMember(Order = 8)]
		public TransientErrorInfoPersisted HighForRestoringRedundancy { get; set; }

		// Token: 0x06001265 RID: 4709 RVA: 0x0004B8B8 File Offset: 0x00049AB8
		public ServerHealthInfoPersisted(string serverFqdn)
		{
			this.ServerFqdn = serverFqdn;
		}
	}
}
