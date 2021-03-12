using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A73 RID: 2675
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class CarrierData : OptionsPropertyChangeTracker
	{
		// Token: 0x17001173 RID: 4467
		// (get) Token: 0x06004BE0 RID: 19424 RVA: 0x00105E4F File Offset: 0x0010404F
		// (set) Token: 0x06004BE1 RID: 19425 RVA: 0x00105E57 File Offset: 0x00104057
		[DataMember]
		public string CarrierId { get; set; }

		// Token: 0x17001174 RID: 4468
		// (get) Token: 0x06004BE2 RID: 19426 RVA: 0x00105E60 File Offset: 0x00104060
		// (set) Token: 0x06004BE3 RID: 19427 RVA: 0x00105E68 File Offset: 0x00104068
		[DataMember]
		public string CarrierName { get; set; }

		// Token: 0x17001175 RID: 4469
		// (get) Token: 0x06004BE4 RID: 19428 RVA: 0x00105E71 File Offset: 0x00104071
		// (set) Token: 0x06004BE5 RID: 19429 RVA: 0x00105E79 File Offset: 0x00104079
		[DataMember]
		public bool HasSmtpGateway { get; set; }

		// Token: 0x04002B10 RID: 11024
		[DataMember]
		public UnifiedMessagingInfo UnifiedMessagingInfo;
	}
}
