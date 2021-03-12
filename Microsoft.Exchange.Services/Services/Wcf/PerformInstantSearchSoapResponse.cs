using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D33 RID: 3379
	[MessageContract(IsWrapped = false)]
	public class PerformInstantSearchSoapResponse : BaseSoapResponse
	{
		// Token: 0x040030C5 RID: 12485
		[MessageBodyMember(Name = "PerformInstantSearchResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public PerformInstantSearchResponse Body;
	}
}
