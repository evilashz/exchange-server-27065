using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D9B RID: 3483
	[MessageContract(IsWrapped = false)]
	public class GetUserUnifiedGroupsSoapResponse : BaseSoapResponse
	{
		// Token: 0x0400312D RID: 12589
		[MessageBodyMember(Name = "GetUserUnifiedGroupsResponseMessage", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public GetUserUnifiedGroupsResponseMessage Body;
	}
}
