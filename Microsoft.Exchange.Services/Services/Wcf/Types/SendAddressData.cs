using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000AD5 RID: 2773
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class SendAddressData : OptionsPropertyChangeTracker
	{
		// Token: 0x170012AD RID: 4781
		// (get) Token: 0x06004EF2 RID: 20210 RVA: 0x001083E5 File Offset: 0x001065E5
		// (set) Token: 0x06004EF3 RID: 20211 RVA: 0x001083ED File Offset: 0x001065ED
		[DataMember(IsRequired = true)]
		public string DisplayName { get; set; }

		// Token: 0x170012AE RID: 4782
		// (get) Token: 0x06004EF4 RID: 20212 RVA: 0x001083F6 File Offset: 0x001065F6
		// (set) Token: 0x06004EF5 RID: 20213 RVA: 0x001083FE File Offset: 0x001065FE
		[DataMember]
		public string AddressId { get; set; }
	}
}
