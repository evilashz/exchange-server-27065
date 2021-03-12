using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000CC0 RID: 3264
	[MessageContract(IsWrapped = false)]
	public class SendItemSoapRequest : BaseSoapRequest
	{
		// Token: 0x04003050 RID: 12368
		[MessageBodyMember(Name = "SendItem", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public SendItemRequest Body;
	}
}
