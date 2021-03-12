using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Autodiscover.WCF
{
	// Token: 0x020000A4 RID: 164
	[MessageContract]
	public class GetFederationInformationResponseMessage : AutodiscoverResponseMessage
	{
		// Token: 0x060003FF RID: 1023 RVA: 0x00017BBE File Offset: 0x00015DBE
		public GetFederationInformationResponseMessage()
		{
			this.Response = new GetFederationInformationResponse();
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000400 RID: 1024 RVA: 0x00017BD1 File Offset: 0x00015DD1
		// (set) Token: 0x06000401 RID: 1025 RVA: 0x00017BD9 File Offset: 0x00015DD9
		[MessageBodyMember]
		public GetFederationInformationResponse Response { get; set; }
	}
}
