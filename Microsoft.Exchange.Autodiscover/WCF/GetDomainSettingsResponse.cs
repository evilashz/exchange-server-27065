using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Autodiscover.WCF
{
	// Token: 0x020000A2 RID: 162
	[DataContract(Name = "GetDomainSettingsResponse", Namespace = "http://schemas.microsoft.com/exchange/2010/Autodiscover")]
	public class GetDomainSettingsResponse : AutodiscoverResponse
	{
		// Token: 0x060003F9 RID: 1017 RVA: 0x00017B76 File Offset: 0x00015D76
		public GetDomainSettingsResponse()
		{
			this.DomainResponses = new List<DomainResponse>();
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060003FA RID: 1018 RVA: 0x00017B89 File Offset: 0x00015D89
		// (set) Token: 0x060003FB RID: 1019 RVA: 0x00017B91 File Offset: 0x00015D91
		[DataMember(Name = "DomainResponses")]
		public List<DomainResponse> DomainResponses { get; set; }
	}
}
