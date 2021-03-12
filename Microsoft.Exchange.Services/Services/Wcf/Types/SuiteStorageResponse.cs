using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000B10 RID: 2832
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class SuiteStorageResponse
	{
		// Token: 0x17001339 RID: 4921
		// (get) Token: 0x0600506D RID: 20589 RVA: 0x00109ABB File Offset: 0x00107CBB
		// (set) Token: 0x0600506E RID: 20590 RVA: 0x00109AC3 File Offset: 0x00107CC3
		[DataMember]
		public SuiteStorageType[] Settings { get; set; }
	}
}
