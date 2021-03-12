using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D03 RID: 3331
	[MessageContract(IsWrapped = false)]
	public class CreateUMPromptSoapResponse : BaseSoapResponse
	{
		// Token: 0x04003095 RID: 12437
		[MessageBodyMember(Name = "CreateUMPromptResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public CreateUMPromptResponseMessage Body;
	}
}
