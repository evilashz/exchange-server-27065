using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000CBA RID: 3258
	[MessageContract(IsWrapped = false)]
	public class DeleteItemSoapRequest : BaseSoapRequest
	{
		// Token: 0x0400304A RID: 12362
		[MessageBodyMember(Name = "DeleteItem", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public DeleteItemRequest Body;
	}
}
