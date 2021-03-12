using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000CB8 RID: 3256
	[MessageContract(IsWrapped = false)]
	public class CreateItemSoapRequest : BaseSoapRequest
	{
		// Token: 0x04003048 RID: 12360
		[MessageBodyMember(Name = "CreateItem", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public CreateItemRequest Body;
	}
}
