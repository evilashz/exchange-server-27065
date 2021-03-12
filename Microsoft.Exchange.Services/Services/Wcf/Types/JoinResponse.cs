using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000B28 RID: 2856
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class JoinResponse
	{
		// Token: 0x1700135F RID: 4959
		// (get) Token: 0x060050E6 RID: 20710 RVA: 0x0010A042 File Offset: 0x00108242
		// (set) Token: 0x060050E7 RID: 20711 RVA: 0x0010A04A File Offset: 0x0010824A
		[DataMember]
		public bool IsSubscribed { get; set; }
	}
}
