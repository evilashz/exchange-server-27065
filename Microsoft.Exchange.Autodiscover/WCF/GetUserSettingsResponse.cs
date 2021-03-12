using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Autodiscover.WCF
{
	// Token: 0x020000A8 RID: 168
	[DataContract(Name = "GetUserSettingsResponse", Namespace = "http://schemas.microsoft.com/exchange/2010/Autodiscover")]
	public class GetUserSettingsResponse : AutodiscoverResponse
	{
		// Token: 0x0600040F RID: 1039 RVA: 0x00017C5A File Offset: 0x00015E5A
		public GetUserSettingsResponse()
		{
			this.UserResponses = new List<UserResponse>();
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000410 RID: 1040 RVA: 0x00017C6D File Offset: 0x00015E6D
		// (set) Token: 0x06000411 RID: 1041 RVA: 0x00017C75 File Offset: 0x00015E75
		[DataMember(Name = "UserResponses")]
		public List<UserResponse> UserResponses { get; set; }
	}
}
