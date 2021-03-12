using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000CBF RID: 3263
	[MessageContract(IsWrapped = false)]
	public class UpdateItemInRecoverableItemsSoapResponse : BaseSoapResponse
	{
		// Token: 0x0400304F RID: 12367
		[MessageBodyMember(Name = "UpdateItemInRecoverableItemsResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public UpdateItemInRecoverableItemsResponse Body;
	}
}
