using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Autodiscover.WCF
{
	// Token: 0x020000A7 RID: 167
	[MessageContract]
	public class GetOrganizationRelationshipSettingsResponseMessage : AutodiscoverResponseMessage
	{
		// Token: 0x0600040C RID: 1036 RVA: 0x00017C36 File Offset: 0x00015E36
		public GetOrganizationRelationshipSettingsResponseMessage()
		{
			this.Response = new GetOrganizationRelationshipSettingsResponse();
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x0600040D RID: 1037 RVA: 0x00017C49 File Offset: 0x00015E49
		// (set) Token: 0x0600040E RID: 1038 RVA: 0x00017C51 File Offset: 0x00015E51
		[MessageBodyMember]
		public GetOrganizationRelationshipSettingsResponse Response { get; set; }
	}
}
