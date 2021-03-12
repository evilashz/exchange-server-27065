using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000CB6 RID: 3254
	[MessageContract(IsWrapped = false)]
	public class GetItemSoapRequest : BaseSoapRequest
	{
		// Token: 0x04003046 RID: 12358
		[MessageBodyMember(Name = "GetItem", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public GetItemRequest Body;
	}
}
