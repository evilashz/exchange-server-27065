using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000CC3 RID: 3267
	[MessageContract(IsWrapped = false)]
	public class MoveItemSoapResponse : BaseSoapResponse
	{
		// Token: 0x04003053 RID: 12371
		[MessageBodyMember(Name = "MoveItemResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public MoveItemResponse Body;
	}
}
