using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D05 RID: 3333
	[MessageContract(IsWrapped = false)]
	public class DeleteUMPromptsSoapResponse : BaseSoapResponse
	{
		// Token: 0x04003097 RID: 12439
		[MessageBodyMember(Name = "DeleteUMPromptsResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public DeleteUMPromptsResponseMessage Body;
	}
}
