using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000CC2 RID: 3266
	[MessageContract(IsWrapped = false)]
	public class MoveItemSoapRequest : BaseSoapRequest
	{
		// Token: 0x04003052 RID: 12370
		[MessageBodyMember(Name = "MoveItem", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public MoveItemRequest Body;
	}
}
