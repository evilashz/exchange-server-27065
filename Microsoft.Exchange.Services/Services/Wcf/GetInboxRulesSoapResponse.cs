using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D29 RID: 3369
	[MessageContract(IsWrapped = false)]
	public class GetInboxRulesSoapResponse : BaseSoapResponse
	{
		// Token: 0x040030BB RID: 12475
		[MessageBodyMember(Name = "GetInboxRulesResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public GetInboxRulesResponse Body;
	}
}
