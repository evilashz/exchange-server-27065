using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000CBC RID: 3260
	[MessageContract(IsWrapped = false)]
	public class UpdateItemSoapRequest : BaseSoapRequest
	{
		// Token: 0x0400304C RID: 12364
		[MessageBodyMember(Name = "UpdateItem", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public UpdateItemRequest Body;
	}
}
