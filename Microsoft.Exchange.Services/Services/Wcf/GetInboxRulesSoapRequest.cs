using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D28 RID: 3368
	[MessageContract(IsWrapped = false)]
	public class GetInboxRulesSoapRequest : BaseSoapRequest
	{
		// Token: 0x040030BA RID: 12474
		[MessageBodyMember(Name = "GetInboxRules", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public GetInboxRulesRequest Body;
	}
}
