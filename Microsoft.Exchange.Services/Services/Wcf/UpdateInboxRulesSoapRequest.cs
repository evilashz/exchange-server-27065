using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D2A RID: 3370
	[MessageContract(IsWrapped = false)]
	public class UpdateInboxRulesSoapRequest : BaseSoapRequest
	{
		// Token: 0x040030BC RID: 12476
		[MessageBodyMember(Name = "UpdateInboxRules", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public UpdateInboxRulesRequest Body;
	}
}
