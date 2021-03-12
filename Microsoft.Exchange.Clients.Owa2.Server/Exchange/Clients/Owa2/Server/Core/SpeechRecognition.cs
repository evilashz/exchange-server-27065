using System;
using System.IO;
using System.Threading;
using System.Xml;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.UM;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200036F RID: 879
	internal abstract class SpeechRecognition
	{
		// Token: 0x06001C34 RID: 7220 RVA: 0x00070044 File Offset: 0x0006E244
		public SpeechRecognition(RequestParameters requestParameters, SpeechRecognitionResultsPriority resultsPriority)
		{
			ValidateArgument.NotNull(requestParameters, "requestParameters is null");
			this.Parameters = requestParameters;
			this.audioReady = false;
			this.addRecoRequestReady = false;
			this.ResultsPriority = resultsPriority;
			this.Results = null;
			this.ResultsReceived = false;
			this.HighestConfidenceResult = 0.0;
			this.ResultType = MobileSpeechRecoResultType.None;
			this.audioReadyEvent = new ManualResetEvent(false);
			this.audioBuffer = null;
			this.recoContext = new SpeechRecoContext(this);
		}

		// Token: 0x17000654 RID: 1620
		// (get) Token: 0x06001C35 RID: 7221 RVA: 0x000700D7 File Offset: 0x0006E2D7
		// (set) Token: 0x06001C36 RID: 7222 RVA: 0x000700DF File Offset: 0x0006E2DF
		public SpeechRecognitionResultsPriority ResultsPriority { get; private set; }

		// Token: 0x17000655 RID: 1621
		// (get) Token: 0x06001C37 RID: 7223 RVA: 0x000700E8 File Offset: 0x0006E2E8
		public string RequestId
		{
			get
			{
				return this.Parameters.RequestId.ToString();
			}
		}

		// Token: 0x17000656 RID: 1622
		// (get) Token: 0x06001C38 RID: 7224 RVA: 0x0007010E File Offset: 0x0006E30E
		public MobileSpeechRecoRequestType RequestType
		{
			get
			{
				return this.Parameters.RequestType;
			}
		}

		// Token: 0x17000657 RID: 1623
		// (get) Token: 0x06001C39 RID: 7225 RVA: 0x0007011B File Offset: 0x0006E31B
		// (set) Token: 0x06001C3A RID: 7226 RVA: 0x00070123 File Offset: 0x0006E323
		public SpeechRecognitionProcessor.SpeechProcessorAsyncCompletedArgs Results { get; private set; }

		// Token: 0x17000658 RID: 1624
		// (get) Token: 0x06001C3B RID: 7227 RVA: 0x0007012C File Offset: 0x0006E32C
		// (set) Token: 0x06001C3C RID: 7228 RVA: 0x00070170 File Offset: 0x0006E370
		public bool ResultsReceived
		{
			get
			{
				bool result;
				lock (this.receivedLock)
				{
					result = this.resultReceived;
				}
				return result;
			}
			private set
			{
				lock (this.receivedLock)
				{
					this.resultReceived = value;
				}
			}
		}

		// Token: 0x17000659 RID: 1625
		// (get) Token: 0x06001C3D RID: 7229 RVA: 0x000701B4 File Offset: 0x0006E3B4
		// (set) Token: 0x06001C3E RID: 7230 RVA: 0x000701BC File Offset: 0x0006E3BC
		public double HighestConfidenceResult { get; set; }

		// Token: 0x1700065A RID: 1626
		// (get) Token: 0x06001C3F RID: 7231 RVA: 0x000701C5 File Offset: 0x0006E3C5
		// (set) Token: 0x06001C40 RID: 7232 RVA: 0x000701CD File Offset: 0x0006E3CD
		public MobileSpeechRecoResultType ResultType { get; set; }

		// Token: 0x1700065B RID: 1627
		// (get) Token: 0x06001C41 RID: 7233 RVA: 0x000701D6 File Offset: 0x0006E3D6
		// (set) Token: 0x06001C42 RID: 7234 RVA: 0x000701DE File Offset: 0x0006E3DE
		protected RequestParameters Parameters { get; set; }

		// Token: 0x06001C43 RID: 7235 RVA: 0x000701E7 File Offset: 0x0006E3E7
		public void StartRecoRequestAsync(SpeechRecognitionProcessor.SpeechProcessorResultsCompletedDelegate resultsCompletedCallback)
		{
			ValidateArgument.NotNull(resultsCompletedCallback, " callback is null");
			this.scenarioCallback = resultsCompletedCallback;
			ThreadPool.QueueUserWorkItem(new WaitCallback(this.StartRecoRequest));
		}

		// Token: 0x06001C44 RID: 7236 RVA: 0x00070210 File Offset: 0x0006E410
		public void SignalAudioReady(byte[] audioBuffer)
		{
			ExTraceGlobals.SpeechRecognitionTracer.TraceDebug<MobileSpeechRecoRequestType>((long)this.GetHashCode(), "SpeechRecognition.SignalAudioReady for Request Type:'{0}'", this.RequestType);
			lock (this.thisLock)
			{
				this.audioBuffer = audioBuffer;
				this.audioReadyEvent.Set();
			}
		}

		// Token: 0x06001C45 RID: 7237 RVA: 0x0007027C File Offset: 0x0006E47C
		public void Dispose()
		{
			lock (this.thisLock)
			{
				if (this.audioReadyEvent != null)
				{
					this.audioReadyEvent.Dispose();
					this.audioReadyEvent = null;
				}
				this.recoContext.Dispose();
			}
		}

		// Token: 0x06001C46 RID: 7238
		public abstract void AddRecoRequestAsync(SpeechRecognitionProcessor.SpeechProcessorAsyncCompletedDelegate callback);

		// Token: 0x06001C47 RID: 7239
		public abstract void RecognizeAsync(byte[] audioBytes, SpeechRecognitionProcessor.SpeechProcessorAsyncCompletedDelegate callback);

		// Token: 0x06001C48 RID: 7240 RVA: 0x000702DC File Offset: 0x0006E4DC
		protected void CollectAndLogStatisticsInformation(MobileSpeechRecoRequestStepLogId requestStepId, int audioLength)
		{
			ExTraceGlobals.SpeechRecognitionTracer.TraceDebug<MobileSpeechRecoRequestStepLogId, int>((long)this.GetHashCode(), "CollectAndLogStatisticsInformation with RequestStepId='{0}', audioLength:'{1}'", requestStepId, audioLength);
			MobileSpeechRequestStatisticsLogger.MobileSpeechRequestStatisticsLogRow row = this.CollectStatisticsLog(requestStepId, audioLength);
			SpeechRecognitionHandler.MobileSpeechRequestStatisticsLogger.Append(row);
		}

		// Token: 0x06001C49 RID: 7241 RVA: 0x00070318 File Offset: 0x0006E518
		protected void CollectAndLogMethodCallStatisticInformation(string methodName, ExDateTime startTime, TimeSpan latency)
		{
			ExTraceGlobals.SpeechRecognitionTracer.TraceDebug<string, string, string>((long)this.GetHashCode(), "CollectAndLogMethodCallStatisticInformation with methodName='{0}', startTime:'{1}', latency:'{2}'", methodName, startTime.ToString(), latency.TotalSeconds.ToString());
			MethodCallLatencyStatisticsLogger.MethodCallLatencyStatisticsLogRow row = this.CollectMethodCallStatisticsLog(methodName, startTime, latency);
			SpeechRecognitionHandler.MethodCallLatencyStatisticsLogger.Append(row);
		}

		// Token: 0x06001C4A RID: 7242 RVA: 0x00070370 File Offset: 0x0006E570
		protected void GetHighestConfidenceValueAndResultTypeFromResult(string results)
		{
			double highestConfidenceResult = 0.0;
			this.ResultType = SpeechRecognitionUtils.ParseMobileScenarioXML(results);
			if (this.ResultType != MobileSpeechRecoResultType.None)
			{
				highestConfidenceResult = SpeechRecognition.GetHighestConfidence(results);
			}
			this.HighestConfidenceResult = highestConfidenceResult;
			ExTraceGlobals.SpeechRecognitionTracer.TraceDebug<MobileSpeechRecoRequestType, double, MobileSpeechRecoResultType>((long)this.GetHashCode(), "HighestConfidence value for Request Type:'{0}', HighestConfidence:'{1}', Result Type: '{2}'", this.RequestType, this.HighestConfidenceResult, this.ResultType);
		}

		// Token: 0x06001C4B RID: 7243 RVA: 0x000703D4 File Offset: 0x0006E5D4
		private static double GetHighestConfidence(string results)
		{
			double num = 0.0;
			try
			{
				if (!string.IsNullOrEmpty(results))
				{
					using (XmlReader xmlReader = XmlReader.Create(new StringReader(results)))
					{
						if (xmlReader.ReadToFollowing("MobileReco"))
						{
							using (XmlReader xmlReader2 = xmlReader.ReadSubtree())
							{
								while (xmlReader2.ReadToFollowing("Alternate"))
								{
									if (xmlReader2.MoveToAttribute("confidence"))
									{
										double num2 = (double)xmlReader2.ReadContentAsFloat();
										if (num2 > num)
										{
											num = num2;
										}
									}
								}
							}
						}
					}
				}
			}
			catch (FormatException obj)
			{
				ExTraceGlobals.SpeechRecognitionTracer.TraceError<string>(0L, "Format exception while getting confidence value Exception: {0}", CommonUtil.ToEventLogString(obj));
			}
			return num;
		}

		// Token: 0x06001C4C RID: 7244 RVA: 0x0007051C File Offset: 0x0006E71C
		private void StartRecoRequest(object state)
		{
			this.recoContext.AddRecoRequestAsync();
			this.HandleTimeoutAndAsyncRecoComplete(new SpeechRecognitionProcessor.SpeechProcessorAsyncCompletedDelegate(this.OnAddRecoRequestCompleted), "AddRecoRequestAsync");
			lock (this.thisLock)
			{
				if (this.audioReadyEvent != null)
				{
					ThreadPool.RegisterWaitForSingleObject(this.audioReadyEvent, delegate(object objectstate, bool eventTimeOut)
					{
						if (eventTimeOut)
						{
							ExTraceGlobals.SpeechRecognitionTracer.TraceDebug<MobileSpeechRecoRequestType>((long)this.GetHashCode(), "Timeout occurs while waiting for the audio, sending an empty buffer to complete the request, RequestType:'{0}'", this.RequestType);
							MemoryStream memoryStream = new MemoryStream();
							this.audioBuffer = memoryStream.ToArray();
							memoryStream.Close();
						}
						lock (this.thisLock)
						{
							this.audioReady = true;
							this.ConsumeAudioIfReady();
						}
					}, null, TimeSpan.FromMilliseconds(30000.0), true);
				}
			}
		}

		// Token: 0x06001C4D RID: 7245 RVA: 0x000705B0 File Offset: 0x0006E7B0
		private MobileSpeechRequestStatisticsLogger.MobileSpeechRequestStatisticsLogRow CollectStatisticsLog(MobileSpeechRecoRequestStepLogId requestStepId, int audioLength)
		{
			return new MobileSpeechRequestStatisticsLogger.MobileSpeechRequestStatisticsLogRow
			{
				RequestId = this.Parameters.RequestId,
				StartTime = ExDateTime.UtcNow,
				RequestType = new MobileSpeechRecoRequestType?(this.Parameters.RequestType),
				Tag = this.Parameters.Tag,
				TenantGuid = new Guid?(this.Parameters.TenantGuid),
				UserObjectGuid = new Guid?(this.Parameters.UserObjectGuid),
				TimeZone = this.Parameters.TimeZone.Id,
				RequestStepId = new MobileSpeechRecoRequestStepLogId?(requestStepId),
				AudioLength = audioLength,
				LogOrigin = new MobileSpeechRecoLogStatisticOrigin?(MobileSpeechRecoLogStatisticOrigin.CAS)
			};
		}

		// Token: 0x06001C4E RID: 7246 RVA: 0x00070668 File Offset: 0x0006E868
		private MethodCallLatencyStatisticsLogger.MethodCallLatencyStatisticsLogRow CollectMethodCallStatisticsLog(string methodName, ExDateTime startTime, TimeSpan latency)
		{
			return new MethodCallLatencyStatisticsLogger.MethodCallLatencyStatisticsLogRow
			{
				RequestId = this.Parameters.RequestId,
				StartTime = startTime,
				Tag = this.Parameters.Tag,
				TenantGuid = new Guid?(this.Parameters.TenantGuid),
				UserObjectGuid = new Guid?(this.Parameters.UserObjectGuid),
				MethodName = methodName,
				Latency = latency
			};
		}

		// Token: 0x06001C4F RID: 7247 RVA: 0x000706E0 File Offset: 0x0006E8E0
		private void ConsumeAudioIfReady()
		{
			lock (this.thisLock)
			{
				if (this.audioReady && this.addRecoRequestReady)
				{
					this.recoContext.RecognizeAsync(this.audioBuffer);
					this.HandleTimeoutAndAsyncRecoComplete(new SpeechRecognitionProcessor.SpeechProcessorAsyncCompletedDelegate(this.OnRecognizeCompleted), "RecognizeAsync");
				}
			}
		}

		// Token: 0x06001C50 RID: 7248 RVA: 0x00070854 File Offset: 0x0006EA54
		private void HandleTimeoutAndAsyncRecoComplete(SpeechRecognitionProcessor.SpeechProcessorAsyncCompletedDelegate callback, string asyncRecoType)
		{
			lock (this.thisLock)
			{
				if (this.recoContext.Event == null)
				{
					ExTraceGlobals.SpeechRecognitionTracer.TraceWarning<string>(0L, "The RecoContext for RecoType:'{0}' has already been disposed of, sending internal error to complete the reco request loop", asyncRecoType);
					callback(new SpeechRecognitionProcessor.SpeechProcessorAsyncCompletedArgs(string.Empty, SpeechRecognitionProcessor.SpeechHttpStatus.InternalServerError));
				}
				else
				{
					ThreadPool.RegisterWaitForSingleObject(this.recoContext.Event, delegate(object state, bool timedOut)
					{
						SpeechRecognitionProcessor.SpeechProcessorAsyncCompletedArgs args;
						if (timedOut)
						{
							ExTraceGlobals.SpeechRecognitionTracer.TraceError<string, string>(0L, "The Async call:'{0}' for Recognition:'{1}' Timed out", asyncRecoType, this.RequestType.ToString());
							UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_MobileSpeechRecoClientAsyncCallTimedOut, null, new object[]
							{
								this.RequestId,
								asyncRecoType,
								this.RequestType.ToString()
							});
							args = new SpeechRecognitionProcessor.SpeechProcessorAsyncCompletedArgs(string.Empty, SpeechRecognitionProcessor.SpeechHttpStatus.InternalServerError);
						}
						else
						{
							ExTraceGlobals.SpeechRecognitionTracer.TraceDebug<string, string>(0L, "The {0} for Recognition:{1} did not time out", asyncRecoType, this.RequestType.ToString());
							args = new SpeechRecognitionProcessor.SpeechProcessorAsyncCompletedArgs(this.recoContext.Results, this.recoContext.Status);
						}
						callback(args);
					}, null, TimeSpan.FromMilliseconds(30000.0), true);
				}
			}
		}

		// Token: 0x06001C51 RID: 7249 RVA: 0x00070920 File Offset: 0x0006EB20
		private void OnAddRecoRequestCompleted(SpeechRecognitionProcessor.SpeechProcessorAsyncCompletedArgs args)
		{
			ExTraceGlobals.SpeechRecognitionTracer.TraceDebug<MobileSpeechRecoRequestType>((long)this.GetHashCode(), "Entering SpeechRecognition.OnAddRecoRequestCompleted request type:{0}", this.RequestType);
			try
			{
				if (args.HttpStatus == SpeechRecognitionProcessor.SpeechHttpStatus.Success)
				{
					lock (this.thisLock)
					{
						this.addRecoRequestReady = true;
						this.ConsumeAudioIfReady();
						goto IL_A3;
					}
				}
				ExTraceGlobals.SpeechRecognitionTracer.TraceDebug<int, string>((long)this.GetHashCode(), "SpeechRecognition.OnAddRecoRequestCompleted not successful,  HttpStatus Code:{0} HttpStatus Description:{1}", args.HttpStatus.StatusCode, args.HttpStatus.StatusDescription);
				SpeechRecognitionProcessor.SpeechProcessorAsyncCompletedArgs state = new SpeechRecognitionProcessor.SpeechProcessorAsyncCompletedArgs(string.Empty, args.HttpStatus);
				ThreadPool.QueueUserWorkItem(new WaitCallback(this.HandleResults), state);
				IL_A3:;
			}
			catch (Exception e)
			{
				this.HandleUnexpectedException(e);
			}
		}

		// Token: 0x06001C52 RID: 7250 RVA: 0x000709F8 File Offset: 0x0006EBF8
		private void OnRecognizeCompleted(SpeechRecognitionProcessor.SpeechProcessorAsyncCompletedArgs args)
		{
			ExTraceGlobals.SpeechRecognitionTracer.TraceDebug<MobileSpeechRecoRequestType>((long)this.GetHashCode(), "Entering SpeechRecognition.OnRecognizeCompleted request type:{0}", this.RequestType);
			try
			{
				if (args.HttpStatus == SpeechRecognitionProcessor.SpeechHttpStatus.Success)
				{
					this.GetHighestConfidenceValueAndResultTypeFromResult(args.ResponseText);
					ThreadPool.QueueUserWorkItem(new WaitCallback(this.HandleResults), args);
				}
				else
				{
					ExTraceGlobals.SpeechRecognitionTracer.TraceDebug<int, string>((long)this.GetHashCode(), "SpeechRecognition.OnRecognizeCompleted not successful,  HttpStatus Code:{0} HttpStatus Description:{1}", args.HttpStatus.StatusCode, args.HttpStatus.StatusDescription);
					SpeechRecognitionProcessor.SpeechProcessorAsyncCompletedArgs state = new SpeechRecognitionProcessor.SpeechProcessorAsyncCompletedArgs(string.Empty, args.HttpStatus);
					ThreadPool.QueueUserWorkItem(new WaitCallback(this.HandleResults), state);
				}
			}
			catch (Exception e)
			{
				this.HandleUnexpectedException(e);
			}
		}

		// Token: 0x06001C53 RID: 7251 RVA: 0x00070ABC File Offset: 0x0006ECBC
		private void HandleResults(object state)
		{
			ExTraceGlobals.SpeechRecognitionTracer.TraceDebug<MobileSpeechRecoRequestType>((long)this.GetHashCode(), "Entering SpeechRecognition.HandleResults request type:{0}", this.RequestType);
			try
			{
				this.ResultsReceived = true;
				this.Results = (SpeechRecognitionProcessor.SpeechProcessorAsyncCompletedArgs)state;
				this.scenarioCallback(this);
			}
			catch (Exception e)
			{
				this.HandleUnexpectedException(e);
			}
		}

		// Token: 0x06001C54 RID: 7252 RVA: 0x00070B20 File Offset: 0x0006ED20
		private void HandleUnexpectedException(Exception e)
		{
			ExTraceGlobals.SpeechRecognitionTracer.TraceError<Exception, MobileSpeechRecoRequestType>((long)this.GetHashCode(), "SpeechRecognition - HandleUnexpectedException='{0}' RequestType='{1}'", e, this.RequestType);
			ExWatson.SendReport(e, ReportOptions.None, null);
			this.HandleException(e, SpeechRecognitionProcessor.SpeechHttpStatus.InternalServerError);
		}

		// Token: 0x06001C55 RID: 7253 RVA: 0x00070B54 File Offset: 0x0006ED54
		private void HandleException(Exception e, SpeechRecognitionProcessor.SpeechHttpStatus status)
		{
			ExTraceGlobals.SpeechRecognitionTracer.TraceError<Exception, int, string>((long)this.GetHashCode(), "SpeechRecognition - Exception='{0}', Status Code='{1}', Status Description='{2}'", e, status.StatusCode, status.StatusDescription);
			UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_SpeechRecoRequestFailed, null, new object[]
			{
				this.RequestId,
				this.Parameters.UserObjectGuid,
				this.Parameters.TenantGuid,
				CommonUtil.ToEventLogString(e)
			});
			this.Results = new SpeechRecognitionProcessor.SpeechProcessorAsyncCompletedArgs(string.Empty, status);
			this.ResultsReceived = true;
			this.scenarioCallback(this);
		}

		// Token: 0x04000FF7 RID: 4087
		private const int MaxTimeoutForAsyncCallback = 30000;

		// Token: 0x04000FF8 RID: 4088
		private object thisLock = new object();

		// Token: 0x04000FF9 RID: 4089
		private bool audioReady;

		// Token: 0x04000FFA RID: 4090
		private bool addRecoRequestReady;

		// Token: 0x04000FFB RID: 4091
		private ManualResetEvent audioReadyEvent;

		// Token: 0x04000FFC RID: 4092
		private byte[] audioBuffer;

		// Token: 0x04000FFD RID: 4093
		private SpeechRecoContext recoContext;

		// Token: 0x04000FFE RID: 4094
		private bool resultReceived;

		// Token: 0x04000FFF RID: 4095
		private object receivedLock = new object();

		// Token: 0x04001000 RID: 4096
		private SpeechRecognitionProcessor.SpeechProcessorResultsCompletedDelegate scenarioCallback;
	}
}
