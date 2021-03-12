using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Autodiscover.WCF
{
	// Token: 0x020000A3 RID: 163
	[MessageContract]
	public class GetDomainSettingsResponseMessage : AutodiscoverResponseMessage
	{
		// Token: 0x060003FC RID: 1020 RVA: 0x00017B9A File Offset: 0x00015D9A
		public GetDomainSettingsResponseMessage()
		{
			this.Response = new GetDomainSettingsResponse();
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x060003FD RID: 1021 RVA: 0x00017BAD File Offset: 0x00015DAD
		// (set) Token: 0x060003FE RID: 1022 RVA: 0x00017BB5 File Offset: 0x00015DB5
		[MessageBodyMember]
		public GetDomainSettingsResponse Response { get; set; }
	}
}
