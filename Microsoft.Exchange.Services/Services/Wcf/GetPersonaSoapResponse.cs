using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D27 RID: 3367
	[MessageContract(IsWrapped = false)]
	public class GetPersonaSoapResponse : BaseSoapResponse
	{
		// Token: 0x040030B9 RID: 12473
		[MessageBodyMember(Name = "GetPersonaResponseMessage", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public GetPersonaResponseMessage Body;
	}
}
