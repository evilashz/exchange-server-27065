using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D02 RID: 3330
	[MessageContract(IsWrapped = false)]
	public class CreateUMPromptSoapRequest : BaseSoapRequest
	{
		// Token: 0x04003094 RID: 12436
		[MessageBodyMember(Name = "CreateUMPrompt", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public CreateUMPromptRequest Body;
	}
}
