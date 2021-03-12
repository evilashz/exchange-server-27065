using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000B41 RID: 2881
	[DataContract(Name = "GetModernGroupDomainResponse", Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetModernGroupDomainResponse : BaseJsonResponse
	{
		// Token: 0x170013A6 RID: 5030
		// (get) Token: 0x0600519B RID: 20891 RVA: 0x0010A9AC File Offset: 0x00108BAC
		// (set) Token: 0x0600519C RID: 20892 RVA: 0x0010A9B4 File Offset: 0x00108BB4
		[DataMember(Name = "Domain", IsRequired = false)]
		public string Domain { get; set; }

		// Token: 0x0600519D RID: 20893 RVA: 0x0010A9BD File Offset: 0x00108BBD
		internal GetModernGroupDomainResponse(string domain)
		{
			this.Domain = domain;
		}
	}
}
