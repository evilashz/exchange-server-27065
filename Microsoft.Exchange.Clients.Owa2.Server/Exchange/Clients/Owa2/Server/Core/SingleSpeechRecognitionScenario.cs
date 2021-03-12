using System;
using System.Collections.Generic;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000387 RID: 903
	internal sealed class SingleSpeechRecognitionScenario : SpeechRecognitionScenarioBase
	{
		// Token: 0x06001CEC RID: 7404 RVA: 0x00073EF6 File Offset: 0x000720F6
		public SingleSpeechRecognitionScenario(RequestParameters requestParameters, UserContext userContext) : base(requestParameters, userContext)
		{
			ValidateArgument.NotNull(requestParameters, "requestParameters is null");
			ValidateArgument.NotNull(userContext, "userContext is null");
		}

		// Token: 0x06001CED RID: 7405 RVA: 0x00073F18 File Offset: 0x00072118
		protected override void InitializeSpeechRecognitions(RequestParameters requestParameters)
		{
			base.RecognitionHelpers = new Dictionary<MobileSpeechRecoRequestType, SpeechRecognition>();
			SpeechRecognition speechRecognition = new LocalSpeechRecognition(requestParameters, SpeechRecognitionResultsPriority.Immediate);
			base.RecognitionHelpers.Add(speechRecognition.RequestType, speechRecognition);
		}

		// Token: 0x06001CEE RID: 7406 RVA: 0x00073F4A File Offset: 0x0007214A
		protected override SpeechRecognitionProcessor.SpeechProcessorAsyncCompletedArgs GetFormattedResultsForHighestConfidenceProcessor(SpeechRecognition recognitionWithHighestConfidence)
		{
			return recognitionWithHighestConfidence.Results;
		}
	}
}
