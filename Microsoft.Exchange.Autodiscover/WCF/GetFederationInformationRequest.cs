using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Autodiscover.WCF
{
	// Token: 0x0200008A RID: 138
	[DataContract(Namespace = "http://schemas.microsoft.com/exchange/2010/Autodiscover")]
	public class GetFederationInformationRequest : AutodiscoverRequest
	{
		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x0600039C RID: 924 RVA: 0x000168DE File Offset: 0x00014ADE
		// (set) Token: 0x0600039D RID: 925 RVA: 0x000168E6 File Offset: 0x00014AE6
		[DataMember(Name = "Domain", IsRequired = true)]
		public string Domain { get; set; }
	}
}
