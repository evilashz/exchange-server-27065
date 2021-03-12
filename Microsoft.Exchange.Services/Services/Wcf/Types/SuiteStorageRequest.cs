using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000B0F RID: 2831
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class SuiteStorageRequest
	{
		// Token: 0x17001337 RID: 4919
		// (get) Token: 0x06005068 RID: 20584 RVA: 0x00109A91 File Offset: 0x00107C91
		// (set) Token: 0x06005069 RID: 20585 RVA: 0x00109A99 File Offset: 0x00107C99
		[DataMember]
		public SuiteStorageKeyType[] ReadSettings { get; set; }

		// Token: 0x17001338 RID: 4920
		// (get) Token: 0x0600506A RID: 20586 RVA: 0x00109AA2 File Offset: 0x00107CA2
		// (set) Token: 0x0600506B RID: 20587 RVA: 0x00109AAA File Offset: 0x00107CAA
		[DataMember]
		public SuiteStorageType[] WriteSettings { get; set; }
	}
}
