using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D5B RID: 3419
	[MessageContract(IsWrapped = false)]
	public class AddAggregatedAccountSoapResponse : BaseSoapResponse
	{
		// Token: 0x040030ED RID: 12525
		[MessageBodyMember(Name = "AddAggregatedAccountResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public AddAggregatedAccountResponse Body;
	}
}
