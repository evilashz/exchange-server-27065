using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D04 RID: 3332
	[MessageContract(IsWrapped = false)]
	public class DeleteUMPromptsSoapRequest : BaseSoapRequest
	{
		// Token: 0x04003096 RID: 12438
		[MessageBodyMember(Name = "DeleteUMPrompts", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public DeleteUMPromptsRequest Body;
	}
}
