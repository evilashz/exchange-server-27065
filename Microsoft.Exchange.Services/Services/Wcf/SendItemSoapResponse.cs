using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000CC1 RID: 3265
	[MessageContract(IsWrapped = false)]
	public class SendItemSoapResponse : BaseSoapResponse
	{
		// Token: 0x04003051 RID: 12369
		[MessageBodyMember(Name = "SendItemResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public SendItemResponse Body;
	}
}
