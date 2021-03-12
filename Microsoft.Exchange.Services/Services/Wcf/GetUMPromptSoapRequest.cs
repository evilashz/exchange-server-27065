using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000CFE RID: 3326
	[MessageContract(IsWrapped = false)]
	public class GetUMPromptSoapRequest : BaseSoapRequest
	{
		// Token: 0x04003090 RID: 12432
		[MessageBodyMember(Name = "GetUMPrompt", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public GetUMPromptRequest Body;
	}
}
