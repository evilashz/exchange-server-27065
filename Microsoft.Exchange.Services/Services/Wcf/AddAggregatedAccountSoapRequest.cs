using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D5A RID: 3418
	[MessageContract(IsWrapped = false)]
	public class AddAggregatedAccountSoapRequest : BaseSoapRequest
	{
		// Token: 0x040030EC RID: 12524
		[MessageBodyMember(Name = "AddAggregatedAccount", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public AddAggregatedAccountRequest Body;
	}
}
