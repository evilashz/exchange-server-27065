using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.DxStore.Common
{
	// Token: 0x02000045 RID: 69
	[DataContract(Namespace = "http://www.outlook.com/highavailability/dxstore/v1/")]
	[Serializable]
	public class ReadOptions
	{
		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000244 RID: 580 RVA: 0x0000449B File Offset: 0x0000269B
		// (set) Token: 0x06000245 RID: 581 RVA: 0x000044A3 File Offset: 0x000026A3
		[DataMember]
		public bool IsAllowStale { get; set; }

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000246 RID: 582 RVA: 0x000044AC File Offset: 0x000026AC
		// (set) Token: 0x06000247 RID: 583 RVA: 0x000044B4 File Offset: 0x000026B4
		[DataMember]
		public bool ReadMajority { get; set; }

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000248 RID: 584 RVA: 0x000044BD File Offset: 0x000026BD
		// (set) Token: 0x06000249 RID: 585 RVA: 0x000044C5 File Offset: 0x000026C5
		[DataMember]
		public bool ReadAfterTestWrite { get; set; }
	}
}
