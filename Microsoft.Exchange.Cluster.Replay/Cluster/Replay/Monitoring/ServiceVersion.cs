using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Cluster.Replay.Monitoring
{
	// Token: 0x020001DB RID: 475
	[DataContract(Name = "ServiceVersion", Namespace = "http://Microsoft.Exchange.HA.Monitoring/InternalMonitoringService/v1/")]
	public class ServiceVersion
	{
		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x060012F0 RID: 4848 RVA: 0x0004CDAF File Offset: 0x0004AFAF
		// (set) Token: 0x060012F1 RID: 4849 RVA: 0x0004CDB7 File Offset: 0x0004AFB7
		[DataMember(Name = "Version", IsRequired = true, Order = 0)]
		public long Version
		{
			get
			{
				return this.m_version;
			}
			set
			{
				this.m_version = value;
			}
		}

		// Token: 0x0400073D RID: 1853
		public const int VERSION_V1 = 1;

		// Token: 0x0400073E RID: 1854
		private long m_version;
	}
}
