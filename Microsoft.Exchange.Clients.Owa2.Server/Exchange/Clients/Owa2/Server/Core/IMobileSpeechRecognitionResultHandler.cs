using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000375 RID: 885
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IMobileSpeechRecognitionResultHandler
	{
		// Token: 0x06001C83 RID: 7299
		void ProcessAndFormatSpeechRecognitionResults(string result, out string jsonResponse, out SpeechRecognitionProcessor.SpeechHttpStatus httpStatus);
	}
}
