using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D00 RID: 3328
	[MessageContract(IsWrapped = false)]
	public class GetUMPromptNamesSoapRequest : BaseSoapRequest
	{
		// Token: 0x04003092 RID: 12434
		[MessageBodyMember(Name = "GetUMPromptNames", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public GetUMPromptNamesRequest Body;
	}
}
