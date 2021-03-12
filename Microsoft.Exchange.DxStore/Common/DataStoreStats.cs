using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.DxStore.Common
{
	// Token: 0x02000002 RID: 2
	[DataContract(Namespace = "http://www.outlook.com/highavailability/dxstore/v1/")]
	[Serializable]
	public class DataStoreStats
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		// (set) Token: 0x06000002 RID: 2 RVA: 0x000020D8 File Offset: 0x000002D8
		[DataMember]
		public int LastUpdateNumber { get; set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000020E1 File Offset: 0x000002E1
		// (set) Token: 0x06000004 RID: 4 RVA: 0x000020E9 File Offset: 0x000002E9
		[DataMember]
		public DateTimeOffset LastUpdateTime { get; set; }

		// Token: 0x06000005 RID: 5 RVA: 0x000020F2 File Offset: 0x000002F2
		public DataStoreStats Clone()
		{
			return (DataStoreStats)base.MemberwiseClone();
		}
	}
}
