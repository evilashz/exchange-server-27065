using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000CB3 RID: 3251
	[MessageContract(IsWrapped = false)]
	public class FindItemSoapResponse : BaseSoapResponse
	{
		// Token: 0x04003043 RID: 12355
		[MessageBodyMember(Name = "FindItemResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public FindItemResponse Body;
	}
}
