using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000CB7 RID: 3255
	[MessageContract(IsWrapped = false)]
	public class GetItemSoapResponse : BaseSoapResponse
	{
		// Token: 0x04003047 RID: 12359
		[MessageBodyMember(Name = "GetItemResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public GetItemResponse Body;
	}
}
