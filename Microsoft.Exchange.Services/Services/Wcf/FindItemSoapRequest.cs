using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000CB2 RID: 3250
	[MessageContract(IsWrapped = false)]
	public class FindItemSoapRequest : BaseSoapRequest
	{
		// Token: 0x04003042 RID: 12354
		[MessageBodyMember(Name = "FindItem", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public FindItemRequest Body;
	}
}
