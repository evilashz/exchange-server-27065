using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D7B RID: 3451
	[MessageContract(IsWrapped = false)]
	public class StartFindInGALSpeechRecognitionSoapResponse : BaseSoapResponse
	{
		// Token: 0x0400310D RID: 12557
		[MessageBodyMember(Name = "StartFindInGALSpeechRecognitionResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public StartFindInGALSpeechRecognitionResponseMessage Body;
	}
}
