using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D26 RID: 3366
	[MessageContract(IsWrapped = false)]
	public class GetPersonaSoapRequest : BaseSoapRequest
	{
		// Token: 0x040030B8 RID: 12472
		[MessageBodyMember(Name = "GetPersona", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public GetPersonaRequest Body;
	}
}
