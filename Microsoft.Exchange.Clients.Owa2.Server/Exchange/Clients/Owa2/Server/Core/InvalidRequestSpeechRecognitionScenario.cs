using System;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000386 RID: 902
	internal sealed class InvalidRequestSpeechRecognitionScenario : SpeechRecognitionScenarioBase
	{
		// Token: 0x06001CE7 RID: 7399 RVA: 0x00073E96 File Offset: 0x00072096
		public InvalidRequestSpeechRecognitionScenario(SpeechRecognitionProcessor.SpeechHttpStatus status) : base(null, null)
		{
			ValidateArgument.NotNull(status, "status");
			this.status = status;
		}

		// Token: 0x06001CE8 RID: 7400 RVA: 0x00073EB2 File Offset: 0x000720B2
		internal override void StartRecoRequestAsync(SpeechRecognitionProcessor.SpeechProcessorAsyncCompletedDelegate callback)
		{
			ValidateArgument.NotNull(callback, "callback is null");
			ExTraceGlobals.SpeechRecognitionTracer.TraceDebug((long)this.GetHashCode(), "InvalidRequestSpeechRecognitionScenarios.StartRecoRequestAsync");
			callback(new SpeechRecognitionProcessor.SpeechProcessorAsyncCompletedArgs(string.Empty, this.status));
		}

		// Token: 0x06001CE9 RID: 7401 RVA: 0x00073EEB File Offset: 0x000720EB
		internal override void SetAudio(byte[] audioBytes)
		{
		}

		// Token: 0x06001CEA RID: 7402 RVA: 0x00073EED File Offset: 0x000720ED
		protected override void InitializeSpeechRecognitions(RequestParameters requestParameters)
		{
		}

		// Token: 0x06001CEB RID: 7403 RVA: 0x00073EEF File Offset: 0x000720EF
		protected override SpeechRecognitionProcessor.SpeechProcessorAsyncCompletedArgs GetFormattedResultsForHighestConfidenceProcessor(SpeechRecognition recognitionWithHighestConfidence)
		{
			throw new NotSupportedException();
		}

		// Token: 0x04001040 RID: 4160
		private SpeechRecognitionProcessor.SpeechHttpStatus status;
	}
}
