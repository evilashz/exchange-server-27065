using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D32 RID: 3378
	[MessageContract(IsWrapped = false)]
	public class PerformInstantSearchSoapRequest : BaseSoapRequest
	{
		// Token: 0x040030C4 RID: 12484
		[MessageBodyMember(Name = "PerformInstantSearch", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public PerformInstantSearchRequest Body;
	}
}
