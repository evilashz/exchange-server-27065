using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000CBD RID: 3261
	[MessageContract(IsWrapped = false)]
	public class UpdateItemSoapResponse : BaseSoapResponse
	{
		// Token: 0x0400304D RID: 12365
		[MessageBodyMember(Name = "UpdateItemResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public UpdateItemResponse Body;
	}
}
