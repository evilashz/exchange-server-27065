using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D7D RID: 3453
	[MessageContract(IsWrapped = false)]
	public class CompleteFindInGALSpeechRecognitionSoapResponse : BaseSoapResponse
	{
		// Token: 0x0400310F RID: 12559
		[MessageBodyMember(Name = "CompleteFindInGALSpeechRecognitionResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public CompleteFindInGALSpeechRecognitionResponseMessage Body;
	}
}
