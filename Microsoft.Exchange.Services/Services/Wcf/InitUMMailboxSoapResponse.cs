using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D87 RID: 3463
	[MessageContract(IsWrapped = false)]
	public class InitUMMailboxSoapResponse : BaseSoapResponse
	{
		// Token: 0x04003119 RID: 12569
		[MessageBodyMember(Name = "InitUMMailboxResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public InitUMMailboxResponseMessage Body;
	}
}
