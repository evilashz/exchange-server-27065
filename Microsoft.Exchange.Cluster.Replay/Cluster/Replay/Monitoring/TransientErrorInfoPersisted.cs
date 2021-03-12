using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Cluster.Replay.Monitoring
{
	// Token: 0x020001D1 RID: 465
	[DataContract(Name = "TransientErrorInfo", Namespace = "http://Microsoft.Exchange.HA.Monitoring/InternalMonitoringService/v1/")]
	public class TransientErrorInfoPersisted
	{
		// Token: 0x17000521 RID: 1313
		// (get) Token: 0x06001298 RID: 4760 RVA: 0x0004BA97 File Offset: 0x00049C97
		// (set) Token: 0x06001299 RID: 4761 RVA: 0x0004BA9F File Offset: 0x00049C9F
		[DataMember(Name = "CES", Order = 1)]
		public ErrorTypePersisted CurrentErrorState { get; set; }

		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x0600129A RID: 4762 RVA: 0x0004BAA8 File Offset: 0x00049CA8
		// (set) Token: 0x0600129B RID: 4763 RVA: 0x0004BAB0 File Offset: 0x00049CB0
		[DataMember(Name = "LST", Order = 2)]
		public string LastSuccessTransitionUtc { get; set; }

		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x0600129C RID: 4764 RVA: 0x0004BAB9 File Offset: 0x00049CB9
		// (set) Token: 0x0600129D RID: 4765 RVA: 0x0004BAC1 File Offset: 0x00049CC1
		[DataMember(Name = "LFT", Order = 3)]
		public string LastFailureTransitionUtc { get; set; }
	}
}
