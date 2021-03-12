using System;
using System.Collections.Generic;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000385 RID: 901
	internal class FindPeopleSpeechRecognitionScenario : SpeechRecognitionScenarioBase
	{
		// Token: 0x06001CE4 RID: 7396 RVA: 0x00073DB1 File Offset: 0x00071FB1
		public FindPeopleSpeechRecognitionScenario(RequestParameters requestParameters, UserContext userContext) : base(requestParameters, userContext)
		{
			ValidateArgument.NotNull(requestParameters, "requestParameters is null");
			ValidateArgument.NotNull(userContext, "userContext is null");
		}

		// Token: 0x06001CE5 RID: 7397 RVA: 0x00073DD4 File Offset: 0x00071FD4
		protected override void InitializeSpeechRecognitions(RequestParameters requestParameters)
		{
			base.RecognitionHelpers = new Dictionary<MobileSpeechRecoRequestType, SpeechRecognition>();
			RequestParameters requestParameters2 = SpeechRecognitionScenarioBase.CreateRequestParameters(MobileSpeechRecoRequestType.FindInPersonalContacts, requestParameters);
			SpeechRecognition speechRecognition = new LocalSpeechRecognition(requestParameters2, SpeechRecognitionResultsPriority.Wait);
			base.RecognitionHelpers.Add(speechRecognition.RequestType, speechRecognition);
			RequestParameters requestParameters3 = SpeechRecognitionScenarioBase.CreateRequestParameters(MobileSpeechRecoRequestType.FindInGAL, requestParameters);
			speechRecognition = new FindInGALSpeechRecognition(requestParameters3, SpeechRecognitionResultsPriority.Wait);
			base.RecognitionHelpers.Add(speechRecognition.RequestType, speechRecognition);
		}

		// Token: 0x06001CE6 RID: 7398 RVA: 0x00073E30 File Offset: 0x00072030
		protected override SpeechRecognitionProcessor.SpeechProcessorAsyncCompletedArgs GetFormattedResultsForHighestConfidenceProcessor(SpeechRecognition recognitionWithHighestConfidence)
		{
			switch (recognitionWithHighestConfidence.RequestType)
			{
			case MobileSpeechRecoRequestType.FindInGAL:
			case MobileSpeechRecoRequestType.FindInPersonalContacts:
			{
				SpeechRecognition galRecoHelper = base.RecognitionHelpers[MobileSpeechRecoRequestType.FindInGAL];
				SpeechRecognition personalContactsRecoHelper = base.RecognitionHelpers[MobileSpeechRecoRequestType.FindInPersonalContacts];
				return SpeechRecognitionUtils.GetCombinedPeopleSearchResult(galRecoHelper, personalContactsRecoHelper, recognitionWithHighestConfidence.ResultType);
			}
			default:
				throw new ArgumentOutOfRangeException("RequestType", recognitionWithHighestConfidence.RequestType, "Invalid parameter");
			}
		}
	}
}
