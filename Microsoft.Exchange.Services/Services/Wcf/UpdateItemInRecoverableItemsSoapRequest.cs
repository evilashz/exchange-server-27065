using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000CBE RID: 3262
	[MessageContract(IsWrapped = false)]
	public class UpdateItemInRecoverableItemsSoapRequest : BaseSoapRequest
	{
		// Token: 0x0400304E RID: 12366
		[MessageBodyMember(Name = "UpdateItemInRecoverableItems", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public UpdateItemInRecoverableItemsRequest Body;
	}
}
