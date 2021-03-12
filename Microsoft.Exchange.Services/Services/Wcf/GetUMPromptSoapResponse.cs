using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000CFF RID: 3327
	[MessageContract(IsWrapped = false)]
	public class GetUMPromptSoapResponse : BaseSoapResponse
	{
		// Token: 0x04003091 RID: 12433
		[MessageBodyMember(Name = "GetUMPromptResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public GetUMPromptResponseMessage Body;
	}
}
