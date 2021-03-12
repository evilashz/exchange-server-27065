using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Autodiscover.WCF
{
	// Token: 0x020000A9 RID: 169
	[MessageContract]
	public class GetUserSettingsResponseMessage : AutodiscoverResponseMessage
	{
		// Token: 0x06000412 RID: 1042 RVA: 0x00017C7E File Offset: 0x00015E7E
		public GetUserSettingsResponseMessage()
		{
			this.Response = new GetUserSettingsResponse();
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000413 RID: 1043 RVA: 0x00017C91 File Offset: 0x00015E91
		// (set) Token: 0x06000414 RID: 1044 RVA: 0x00017C99 File Offset: 0x00015E99
		[MessageBodyMember]
		public GetUserSettingsResponse Response { get; set; }
	}
}
