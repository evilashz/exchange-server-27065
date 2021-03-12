using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.DxStore.Common
{
	// Token: 0x02000043 RID: 67
	[DataContract(Namespace = "http://www.outlook.com/highavailability/dxstore/v1/")]
	[Serializable]
	public class PropertyNameInfo
	{
		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000233 RID: 563 RVA: 0x00004108 File Offset: 0x00002308
		// (set) Token: 0x06000234 RID: 564 RVA: 0x00004110 File Offset: 0x00002310
		[DataMember]
		public string Name { get; set; }

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000235 RID: 565 RVA: 0x00004119 File Offset: 0x00002319
		// (set) Token: 0x06000236 RID: 566 RVA: 0x00004121 File Offset: 0x00002321
		[DataMember]
		public int Kind { get; set; }
	}
}
