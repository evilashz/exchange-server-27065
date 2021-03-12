using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.DxStore.Common
{
	// Token: 0x02000046 RID: 70
	[DataContract(Namespace = "http://www.outlook.com/highavailability/dxstore/v1/")]
	[Serializable]
	public class ReadResult
	{
		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x0600024B RID: 587 RVA: 0x000044D6 File Offset: 0x000026D6
		// (set) Token: 0x0600024C RID: 588 RVA: 0x000044DE File Offset: 0x000026DE
		[DataMember]
		public bool IsStale { get; set; }
	}
}
