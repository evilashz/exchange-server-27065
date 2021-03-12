using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.HA.Services
{
	// Token: 0x02000336 RID: 822
	[DataContract(Name = "ServiceVersion", Namespace = "http://www.outlook.com/highavailability/ServerLocator/v1/")]
	public class ServiceVersion
	{
		// Token: 0x170008C0 RID: 2240
		// (get) Token: 0x06002178 RID: 8568 RVA: 0x0009B7BE File Offset: 0x000999BE
		// (set) Token: 0x06002179 RID: 8569 RVA: 0x0009B7C6 File Offset: 0x000999C6
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

		// Token: 0x04000DAF RID: 3503
		public const int VERSION_V1 = 1;

		// Token: 0x04000DB0 RID: 3504
		public const int VERSION_V2 = 2;

		// Token: 0x04000DB1 RID: 3505
		private long m_version;
	}
}
