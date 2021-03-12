using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.UM;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMCore;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000383 RID: 899
	internal abstract class SpeechRecognitionScenarioBase
	{
		// Token: 0x06001CCE RID: 7374 RVA: 0x000735F0 File Offset: 0x000717F0
		public SpeechRecognitionScenarioBase(RequestParameters requestParameters, UserContext userContext)
		{
			this.Parameters = requestParameters;
			this.UserContext = userContext;
			this.resultsProcessed = false;
			CultureInfo c = (requestParameters != null) ? requestParameters.Culture : CommonConstants.DefaultCulture;
			this.immediateThreshold = LocConfig.Instance[c].MowaSpeech.MowaVoiceImmediateThreshold;
			this.InitializeSpeechRecognitions(requestParameters);
		}

		// Token: 0x1700066D RID: 1645
		// (get) Token: 0x06001CCF RID: 7375 RVA: 0x00073656 File Offset: 0x00071856
		// (set) Token: 0x06001CD0 RID: 7376 RVA: 0x0007365E File Offset: 0x0007185E
		public UserContext UserContext { get; private set; }

		// Token: 0x1700066E RID: 1646
		// (get) Token: 0x06001CD1 RID: 7377 RVA: 0x00073667 File Offset: 0x00071867
		// (set) Token: 0x06001CD2 RID: 7378 RVA: 0x0007366F File Offset: 0x0007186F
		public RequestParameters Parameters { get; set; }

		// Token: 0x1700066F RID: 1647
		// (get) Token: 0x06001CD3 RID: 7379 RVA: 0x00073678 File Offset: 0x00071878
		// (set) Token: 0x06001CD4 RID: 7380 RVA: 0x00073680 File Offset: 0x00071880
		protected Dictionary<MobileSpeechRecoRequestType, SpeechRecognition> RecognitionHelpers { get; set; }

		// Token: 0x06001CD5 RID: 7381 RVA: 0x0007368C File Offset: 0x0007188C
		internal virtual void StartRecoRequestAsync(SpeechRecognitionProcessor.SpeechProcessorAsyncCompletedDelegate callback)
		{
			ExTraceGlobals.SpeechRecognitionTracer.TraceDebug((long)this.GetHashCode(), "Entering SpeechRecognitionScenariosBase.StartRecoRequestAsync");
			this.resultHandlerCallback = callback;
			foreach (SpeechRecognition speechRecognition in this.RecognitionHelpers.Values)
			{
				speechRecognition.StartRecoRequestAsync(new SpeechRecognitionProcessor.SpeechProcessorResultsCompletedDelegate(this.HandleResults));
			}
		}

		// Token: 0x06001CD6 RID: 7382 RVA: 0x0007370C File Offset: 0x0007190C
		internal virtual void SetAudio(byte[] audioBytes)
		{
			ExTraceGlobals.SpeechRecognitionTracer.TraceDebug((long)this.GetHashCode(), "Entering SpeechRecognitionScenariosBase.AudioReadyForRecognition");
			lock (this.thisLock)
			{
				foreach (SpeechRecognition speechRecognition in this.RecognitionHelpers.Values)
				{
					speechRecognition.SignalAudioReady(audioBytes);
				}
			}
		}

		// Token: 0x06001CD7 RID: 7383 RVA: 0x000737A4 File Offset: 0x000719A4
		protected static RequestParameters CreateRequestParameters(MobileSpeechRecoRequestType requestType, RequestParameters requestParameters)
		{
			Guid requestId = Guid.NewGuid();
			ExTraceGlobals.SpeechRecognitionTracer.TraceDebug<string, string, string>(0L, "Requested Id Guid:'{0}' created for Request Type:'{1}', Parameter Tag:'{2}'", requestId.ToString(), requestType.ToString(), requestParameters.Tag);
			return new RequestParameters(requestId, requestParameters.Tag, requestType, requestParameters.Culture, requestParameters.TimeZone, requestParameters.UserObjectGuid, requestParameters.TenantGuid, requestParameters.OrgId);
		}

		// Token: 0x06001CD8 RID: 7384
		protected abstract void InitializeSpeechRecognitions(RequestParameters requestParameters);

		// Token: 0x06001CD9 RID: 7385
		protected abstract SpeechRecognitionProcessor.SpeechProcessorAsyncCompletedArgs GetFormattedResultsForHighestConfidenceProcessor(SpeechRecognition recognitionWithHighestConfidence);

		// Token: 0x06001CDA RID: 7386 RVA: 0x00073814 File Offset: 0x00071A14
		private void HandleResults(SpeechRecognition helper)
		{
			lock (this.thisLock)
			{
				try
				{
					ExTraceGlobals.SpeechRecognitionTracer.TraceDebug((long)this.GetHashCode(), "Entering SpeechRecognitionScenariosBase.HandleResults");
					if (!this.resultsProcessed)
					{
						if (helper.ResultsPriority == SpeechRecognitionResultsPriority.Immediate)
						{
							this.ProcessImmediatePriorityProcessor(helper);
						}
						else
						{
							this.ProcessWaitingPriorityProcessor(helper);
						}
					}
				}
				catch (Exception e)
				{
					this.HandleUnexpectedException(e);
				}
			}
		}

		// Token: 0x06001CDB RID: 7387 RVA: 0x0007389C File Offset: 0x00071A9C
		private void ProcessImmediatePriorityProcessor(SpeechRecognition helper)
		{
			lock (this.thisLock)
			{
				if (helper.Results.HttpStatus == SpeechRecognitionProcessor.SpeechHttpStatus.Success && helper.HighestConfidenceResult > this.immediateThreshold)
				{
					ExTraceGlobals.SpeechRecognitionTracer.TraceDebug<string>((long)this.GetHashCode(), "ProcessImediatePriorityProcessor Recognition: {0} was successful and will be processed immediately", helper.RequestType.ToString());
					this.resultsProcessed = true;
					this.InvokeHandlerCallbackAndDisposeHelpers(helper.Results);
				}
				else
				{
					ExTraceGlobals.SpeechRecognitionTracer.TraceDebug<string, string, double>((long)this.GetHashCode(), "ProcessImediatePriorityProcessor Recognition: {0} was Unsuccessful with error description:{1}, confidence value:{2}", helper.RequestType.ToString(), helper.Results.HttpStatus.StatusDescription, helper.HighestConfidenceResult);
					this.ProcessWaitingPriorityProcessor(helper);
				}
			}
		}

		// Token: 0x06001CDC RID: 7388 RVA: 0x00073974 File Offset: 0x00071B74
		private void ProcessWaitingPriorityProcessor(SpeechRecognition helper)
		{
			lock (this.thisLock)
			{
				if (!this.resultsProcessed)
				{
					this.resultsProcessed = true;
					if (helper.Results.HttpStatus == SpeechRecognitionProcessor.SpeechHttpStatus.NoSpeechDetected)
					{
						ExTraceGlobals.SpeechRecognitionTracer.TraceDebug<string>((long)this.GetHashCode(), "ProcessWaitingPriorityProcessor: No Speech Detected from Speech Recognition:'{0}'", helper.RequestType.ToString());
						this.InvokeHandlerCallbackAndDisposeHelpers(helper.Results);
					}
					else
					{
						ExTraceGlobals.SpeechRecognitionTracer.TraceDebug((long)this.GetHashCode(), "ProcessWaitingPriorityProcessor: Final Results hasnt been processed yet. Initiate processing...");
						SpeechRecognition speechRecognition = helper;
						foreach (KeyValuePair<MobileSpeechRecoRequestType, SpeechRecognition> keyValuePair in this.RecognitionHelpers)
						{
							if (!keyValuePair.Value.ResultsReceived)
							{
								ExTraceGlobals.SpeechRecognitionTracer.TraceDebug<MobileSpeechRecoRequestType>((long)this.GetHashCode(), "ProcessWaitingPriorityProcessor Recognition: {0} result is not available yet and will be waited on", keyValuePair.Key);
								this.resultsProcessed = false;
								break;
							}
							if (speechRecognition.HighestConfidenceResult < keyValuePair.Value.HighestConfidenceResult)
							{
								speechRecognition = keyValuePair.Value;
							}
						}
						if (this.resultsProcessed)
						{
							SpeechRecognitionProcessor.SpeechProcessorAsyncCompletedArgs formattedResultsForHighestConfidenceProcessor = this.GetFormattedResultsForHighestConfidenceProcessor(speechRecognition);
							this.InvokeHandlerCallbackAndDisposeHelpers(formattedResultsForHighestConfidenceProcessor);
						}
					}
				}
				else
				{
					ExTraceGlobals.SpeechRecognitionTracer.TraceDebug((long)this.GetHashCode(), "ProcessWaitingPriorityProcessor: Final Results already processed. Skip processing stage.");
				}
			}
		}

		// Token: 0x06001CDD RID: 7389 RVA: 0x00073AF8 File Offset: 0x00071CF8
		private void InvokeHandlerCallbackAndDisposeHelpers(SpeechRecognitionProcessor.SpeechProcessorAsyncCompletedArgs argResults)
		{
			this.resultHandlerCallback(argResults);
			this.DisposeRecognitionHelpers();
		}

		// Token: 0x06001CDE RID: 7390 RVA: 0x00073B0C File Offset: 0x00071D0C
		private void DisposeRecognitionHelpers()
		{
			lock (this.thisLock)
			{
				foreach (KeyValuePair<MobileSpeechRecoRequestType, SpeechRecognition> keyValuePair in this.RecognitionHelpers)
				{
					ExTraceGlobals.SpeechRecognitionTracer.TraceDebug<MobileSpeechRecoRequestType>((long)this.GetHashCode(), "DisposeRecognitionHelpers disposing Recognition: {0}", keyValuePair.Key);
					keyValuePair.Value.Dispose();
				}
				this.RecognitionHelpers.Clear();
			}
		}

		// Token: 0x06001CDF RID: 7391 RVA: 0x00073BB8 File Offset: 0x00071DB8
		private void HandleUnexpectedException(Exception e)
		{
			ExTraceGlobals.SpeechRecognitionTracer.TraceError<Exception, Guid>((long)this.GetHashCode(), "SpeechRecognitionScenarioBase - HandleUnexpectedException='{0}' RequestId='{1}'", e, this.Parameters.RequestId);
			ExWatson.SendReport(e, ReportOptions.None, null);
			this.HandleException(e, SpeechRecognitionProcessor.SpeechHttpStatus.InternalServerError);
		}

		// Token: 0x06001CE0 RID: 7392 RVA: 0x00073BF0 File Offset: 0x00071DF0
		private void HandleException(Exception e, SpeechRecognitionProcessor.SpeechHttpStatus status)
		{
			ExTraceGlobals.SpeechRecognitionTracer.TraceError<Exception, int, string>((long)this.GetHashCode(), "SpeechRecognitionScenarioBase - Exception='{0}', Status Code='{1}', Status Description='{2}'", e, status.StatusCode, status.StatusDescription);
			UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_SpeechRecoRequestFailed, null, new object[]
			{
				this.Parameters.RequestId,
				this.Parameters.UserObjectGuid,
				this.Parameters.TenantGuid,
				CommonUtil.ToEventLogString(e)
			});
			SpeechRecognitionProcessor.SpeechProcessorAsyncCompletedArgs argResults = new SpeechRecognitionProcessor.SpeechProcessorAsyncCompletedArgs(string.Empty, status);
			this.InvokeHandlerCallbackAndDisposeHelpers(argResults);
		}

		// Token: 0x04001039 RID: 4153
		private readonly double immediateThreshold;

		// Token: 0x0400103A RID: 4154
		private object thisLock = new object();

		// Token: 0x0400103B RID: 4155
		private bool resultsProcessed;

		// Token: 0x0400103C RID: 4156
		private SpeechRecognitionProcessor.SpeechProcessorAsyncCompletedDelegate resultHandlerCallback;
	}
}
