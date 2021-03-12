using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.Wrappers
{
	// Token: 0x020002D3 RID: 723
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class VerifyCertRequestWrapper
	{
		// Token: 0x170005F9 RID: 1529
		// (get) Token: 0x06001886 RID: 6278 RVA: 0x00054507 File Offset: 0x00052707
		// (set) Token: 0x06001887 RID: 6279 RVA: 0x0005450F File Offset: 0x0005270F
		[DataMember(Name = "certRawData")]
		public string CertRawData { get; set; }
	}
}
