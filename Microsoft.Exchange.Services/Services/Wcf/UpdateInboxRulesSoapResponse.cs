using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D2B RID: 3371
	[MessageContract(IsWrapped = false)]
	public class UpdateInboxRulesSoapResponse : BaseSoapResponse
	{
		// Token: 0x040030BD RID: 12477
		[MessageBodyMember(Name = "UpdateInboxRulesResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public UpdateInboxRulesResponse Body;
	}
}
