using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000CB9 RID: 3257
	[MessageContract(IsWrapped = false)]
	public class CreateItemSoapResponse : BaseSoapResponse
	{
		// Token: 0x04003049 RID: 12361
		[MessageBodyMember(Name = "CreateItemResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public CreateItemResponse Body;
	}
}
