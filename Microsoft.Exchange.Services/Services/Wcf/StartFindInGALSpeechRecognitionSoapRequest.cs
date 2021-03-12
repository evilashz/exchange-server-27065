using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D7A RID: 3450
	[MessageContract(IsWrapped = false)]
	public class StartFindInGALSpeechRecognitionSoapRequest : BaseSoapRequest
	{
		// Token: 0x0400310C RID: 12556
		[MessageBodyMember(Name = "StartFindInGALSpeechRecognition", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public StartFindInGALSpeechRecognitionRequest Body;
	}
}
