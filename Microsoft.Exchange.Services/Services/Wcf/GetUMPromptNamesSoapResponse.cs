using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D01 RID: 3329
	[MessageContract(IsWrapped = false)]
	public class GetUMPromptNamesSoapResponse : BaseSoapResponse
	{
		// Token: 0x04003093 RID: 12435
		[MessageBodyMember(Name = "GetUMPromptNamesResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public GetUMPromptNamesResponseMessage Body;
	}
}
