using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.HA.Services
{
	// Token: 0x02000329 RID: 809
	[DataContract(Name = "GetActiveCopiesForDatabaseAvailabilityGroupParameters", Namespace = "http://www.outlook.com/highavailability/ServerLocator/v1/")]
	public class GetActiveCopiesForDatabaseAvailabilityGroupParameters
	{
		// Token: 0x170008B1 RID: 2225
		// (get) Token: 0x06002133 RID: 8499 RVA: 0x00099D18 File Offset: 0x00097F18
		// (set) Token: 0x06002134 RID: 8500 RVA: 0x00099D20 File Offset: 0x00097F20
		[DataMember(Name = "CachedData", IsRequired = false, Order = 0)]
		public bool CachedData
		{
			get
			{
				return this.m_cachedData;
			}
			set
			{
				this.m_cachedData = value;
			}
		}

		// Token: 0x04000D6E RID: 3438
		private bool m_cachedData;
	}
}
