using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.Exchange.Cluster.Common;

namespace Microsoft.Exchange.Cluster.Replay.Monitoring
{
	// Token: 0x020001CD RID: 461
	[DataContract(Name = "HealthInfo", Namespace = "http://Microsoft.Exchange.HA.Monitoring/InternalMonitoringService/v1/")]
	public class HealthInfoPersisted
	{
		// Token: 0x170004FD RID: 1277
		// (get) Token: 0x0600124A RID: 4682 RVA: 0x0004B78E File Offset: 0x0004998E
		// (set) Token: 0x0600124B RID: 4683 RVA: 0x0004B796 File Offset: 0x00049996
		[DataMember(Name = "CreateTime", Order = 1)]
		public string CreateTimeUtcStr { get; set; }

		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x0600124C RID: 4684 RVA: 0x0004B79F File Offset: 0x0004999F
		// (set) Token: 0x0600124D RID: 4685 RVA: 0x0004B7A7 File Offset: 0x000499A7
		[DataMember(Name = "LastUpdateTime", Order = 2)]
		public string LastUpdateTimeUtcStr { get; set; }

		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x0600124E RID: 4686 RVA: 0x0004B7B0 File Offset: 0x000499B0
		// (set) Token: 0x0600124F RID: 4687 RVA: 0x0004B7B8 File Offset: 0x000499B8
		[DataMember(Order = 3)]
		public List<ServerHealthInfoPersisted> Servers { get; set; }

		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x06001250 RID: 4688 RVA: 0x0004B7C1 File Offset: 0x000499C1
		// (set) Token: 0x06001251 RID: 4689 RVA: 0x0004B7C9 File Offset: 0x000499C9
		[DataMember(Order = 4)]
		public List<DbHealthInfoPersisted> Databases { get; set; }

		// Token: 0x06001252 RID: 4690 RVA: 0x0004B7D2 File Offset: 0x000499D2
		public HealthInfoPersisted()
		{
			this.Databases = new List<DbHealthInfoPersisted>(160);
			this.Servers = new List<ServerHealthInfoPersisted>(16);
		}

		// Token: 0x06001253 RID: 4691 RVA: 0x0004B7F8 File Offset: 0x000499F8
		public DateTime GetLastUpdateTimeUtc()
		{
			DateTime result;
			DateTimeHelper.TryParseIntoDateTime(this.LastUpdateTimeUtcStr, ref result);
			return result;
		}

		// Token: 0x06001254 RID: 4692 RVA: 0x0004B814 File Offset: 0x00049A14
		public DateTime GetCreateTimeUtc()
		{
			DateTime result;
			DateTimeHelper.TryParseIntoDateTime(this.CreateTimeUtcStr, ref result);
			return result;
		}
	}
}
