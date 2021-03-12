using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D9A RID: 3482
	[MessageContract(IsWrapped = false)]
	public class GetUserUnifiedGroupsSoapRequest : BaseSoapRequest
	{
		// Token: 0x0400312C RID: 12588
		[MessageBodyMember(Name = "GetUserUnifiedGroups", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public GetUserUnifiedGroupsRequest Body;
	}
}
