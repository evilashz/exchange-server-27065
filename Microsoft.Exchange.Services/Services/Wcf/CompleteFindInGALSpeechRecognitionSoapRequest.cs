using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D7C RID: 3452
	[MessageContract(IsWrapped = false)]
	public class CompleteFindInGALSpeechRecognitionSoapRequest : BaseSoapRequest
	{
		// Token: 0x0400310E RID: 12558
		[MessageBodyMember(Name = "CompleteFindInGALSpeechRecognition", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public CompleteFindInGALSpeechRecognitionRequest Body;
	}
}
