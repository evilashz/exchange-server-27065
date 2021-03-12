using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x0200028A RID: 650
	internal class MobileSpeechRecoRequest : DisposableBase
	{
		// Token: 0x14000018 RID: 24
		// (add) Token: 0x06001345 RID: 4933 RVA: 0x00055D10 File Offset: 0x00053F10
		// (remove) Token: 0x06001346 RID: 4934 RVA: 0x00055D48 File Offset: 0x00053F48
		public event RequestCompletedEventHandler Completed;

		// Token: 0x06001347 RID: 4935 RVA: 0x00055D80 File Offset: 0x00053F80
		public MobileSpeechRecoRequest(Guid id, IMobileSpeechRecoRequestBehavior behavior, IPlatformBuilder platformBuilder)
		{
			ValidateArgument.NotNull(behavior, "behavior");
			ValidateArgument.NotNull(platformBuilder, "platformBuilder");
			MobileSpeechRecoTracer.TraceDebug(this, id, "Entering MobileSpeechRecoRequest constructor", new object[0]);
			this.id = id;
			this.behavior = behavior;
			this.platformBuilder = platformBuilder;
		}

		// Token: 0x06001348 RID: 4936 RVA: 0x00055DE8 File Offset: 0x00053FE8
		public void PrepareRecoRequestAsync(MobileRecoAsyncCompletedDelegate callback)
		{
			ValidateArgument.NotNull(callback, "callback");
			MobileSpeechRecoTracer.TraceDebug(this, this.id, "Entering MobileSpeechRecoRequest.PrepareRecoRequestAsync", new object[0]);
			this.timer = new Timer(new TimerCallback(this.OnTimerExpired), this, this.behavior.MaxProcessingTime, -1);
			this.InitializeAsync(MobileSpeechRecoRequest.RequestState.New, MobileSpeechRecoRequest.RequestState.Preparing, callback);
			ThreadPool.QueueUserWorkItem(new WaitCallback(this.InternalPrepareRecoRequest), null);
		}

		// Token: 0x06001349 RID: 4937 RVA: 0x00055E58 File Offset: 0x00054058
		public void RecognizeAsync(byte[] audioBytes, MobileRecoAsyncCompletedDelegate callback)
		{
			ValidateArgument.NotNull(audioBytes, "audioBytes");
			ValidateArgument.NotNull(callback, "callback");
			MobileSpeechRecoTracer.TraceDebug(this, this.id, "Entering MobileSpeechRecoRequest.RecognizeAsync", new object[0]);
			this.InitializeAsync(MobileSpeechRecoRequest.RequestState.Prepared, MobileSpeechRecoRequest.RequestState.Recognizing, callback);
			Exception ex = null;
			try
			{
				this.recognizer.RecognizeAsync(audioBytes, new RecognizeCompletedDelegate(this.RecognizeCompleted));
			}
			catch (FormatException ex2)
			{
				ex = new InvalidAudioStreamException(ex2.Message, ex2);
			}
			catch (Exception ex3)
			{
				ex = ex3;
			}
			finally
			{
				if (ex != null)
				{
					MobileRecoAsyncCompletedArgs completedArgs = new MobileRecoAsyncCompletedArgs(string.Empty, -1, ex);
					this.InvokeCallback(completedArgs, MobileSpeechRecoRequest.RequestState.Recognizing, MobileSpeechRecoRequest.RequestState.RecognizeComplete, true);
				}
			}
		}

		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x0600134A RID: 4938 RVA: 0x00055F14 File Offset: 0x00054114
		public Guid Id
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x0600134B RID: 4939 RVA: 0x00055F1C File Offset: 0x0005411C
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				MobileSpeechRecoTracer.TraceDebug(this, this.id, "Entering MobileSpeechRecoRequest.InternalDispose", new object[0]);
				if (this.recognizer != null)
				{
					this.recognizer.Dispose();
					this.recognizer = null;
				}
				if (this.timer != null)
				{
					this.timer.Dispose();
					this.timer = null;
				}
				if (this.grammars != null)
				{
					foreach (UMGrammar umgrammar in this.grammars)
					{
						if (umgrammar.DeleteFileAfterUse)
						{
							MobileSpeechRecoTracer.TraceDebug(this, this.id, "MobileSpeechRecoRequest.InternalDispose -- Deleting grammar '{0}'", new object[]
							{
								umgrammar.Path
							});
							Util.TryDeleteFile(umgrammar.Path);
						}
					}
				}
			}
		}

		// Token: 0x0600134C RID: 4940 RVA: 0x00055FF8 File Offset: 0x000541F8
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<MobileSpeechRecoRequest>(this);
		}

		// Token: 0x0600134D RID: 4941 RVA: 0x00056000 File Offset: 0x00054200
		private static IMobileRecognizer CreateRecognizer(Guid requestId, IMobileSpeechRecoRequestBehavior requestBehavior, IPlatformBuilder platformBuilder)
		{
			MobileSpeechRecoTracer.TraceDebug(null, requestId, "Entering MobileSpeechRecoRequest.CreateRecognizer", new object[0]);
			IMobileRecognizer result = null;
			SpeechRecognitionEngineType engineType = requestBehavior.EngineType;
			CultureInfo culture = requestBehavior.Culture;
			if (!platformBuilder.TryCreateMobileRecognizer(requestId, culture, engineType, requestBehavior.MaxAlternates, out result))
			{
				throw new RecognizerNotInstalledException(engineType.ToString(), culture.ToString());
			}
			return result;
		}

		// Token: 0x0600134E RID: 4942 RVA: 0x0005605C File Offset: 0x0005425C
		private void InternalPrepareRecoRequest(object state)
		{
			MobileSpeechRecoTracer.TraceDebug(this, this.id, "Entering MobileSpeechRecoRequest.InternalPrepareRecoRequest", new object[0]);
			Exception ex = null;
			try
			{
				this.recognizer = MobileSpeechRecoRequest.CreateRecognizer(this.id, this.behavior, this.platformBuilder);
				this.grammars = this.behavior.PrepareGrammars();
				this.recognizer.LoadGrammars(this.grammars);
			}
			catch (Exception ex2)
			{
				ex = ex2;
			}
			if (ex != null)
			{
				MobileRecoAsyncCompletedArgs completedArgs = new MobileRecoAsyncCompletedArgs(string.Empty, -1, ex);
				this.InvokeCallback(completedArgs, MobileSpeechRecoRequest.RequestState.Preparing, MobileSpeechRecoRequest.RequestState.Preparing, true);
				return;
			}
			MobileRecoAsyncCompletedArgs completedArgs2 = new MobileRecoAsyncCompletedArgs(string.Empty, -1, null);
			this.InvokeCallback(completedArgs2, MobileSpeechRecoRequest.RequestState.Preparing, MobileSpeechRecoRequest.RequestState.Prepared, false);
		}

		// Token: 0x0600134F RID: 4943 RVA: 0x0005610C File Offset: 0x0005430C
		private void RecognizeCompleted(List<IMobileRecognitionResult> results, Exception error, bool speechDetected)
		{
			ValidateArgument.NotNull(results, "results");
			MobileSpeechRecoTracer.TraceDebug(this, this.id, "Entering MobileSpeechRecoRequest.RecognizeCompleted results.Count='{0}', error='{1}', speechDetected='{2}'", new object[]
			{
				results.Count,
				(error == null) ? "<null>" : error.Message,
				speechDetected
			});
			MobileRecoAsyncCompletedArgs completedArgs;
			try
			{
				int resultCount = -1;
				string result;
				if (error != null)
				{
					result = string.Empty;
				}
				else if (!speechDetected)
				{
					result = string.Empty;
					error = new NoSpeechDetectedException();
				}
				else
				{
					result = this.behavior.ProcessRecoResults(results);
					resultCount = results.Count;
				}
				completedArgs = new MobileRecoAsyncCompletedArgs(result, resultCount, error);
			}
			catch (Exception error2)
			{
				completedArgs = new MobileRecoAsyncCompletedArgs(string.Empty, -1, error2);
			}
			this.InvokeCallback(completedArgs, MobileSpeechRecoRequest.RequestState.Recognizing, MobileSpeechRecoRequest.RequestState.RecognizeComplete, true);
		}

		// Token: 0x06001350 RID: 4944 RVA: 0x000561D4 File Offset: 0x000543D4
		private void OnTimerExpired(object state)
		{
			MobileSpeechRecoTracer.TraceDebug(this, this.id, "Entering MobileSpeechRecoRequest.OnTimerExpired", new object[0]);
			bool flag = false;
			lock (this.lockObj)
			{
				if (!this.syncIsRequestComplete)
				{
					this.syncIsRequestComplete = true;
					flag = true;
				}
			}
			if (flag)
			{
				UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_MobileSpeechRecoTimeout, null, new object[]
				{
					this.id
				});
				this.FireCompletedEvent();
			}
		}

		// Token: 0x06001351 RID: 4945 RVA: 0x00056268 File Offset: 0x00054468
		private void InitializeAsync(MobileSpeechRecoRequest.RequestState currentState, MobileSpeechRecoRequest.RequestState newState, MobileRecoAsyncCompletedDelegate callback)
		{
			MobileSpeechRecoTracer.TraceDebug(this, this.id, "Entering MobileSpeechRecoRequest.InitializeAsync Current state='{0}', New state ='{1}'", new object[]
			{
				currentState,
				newState
			});
			lock (this.lockObj)
			{
				this.ValidateAndChangeState(currentState, newState);
				this.syncCallback = callback;
			}
		}

		// Token: 0x06001352 RID: 4946 RVA: 0x000562DC File Offset: 0x000544DC
		private void InvokeCallback(MobileRecoAsyncCompletedArgs completedArgs, MobileSpeechRecoRequest.RequestState currentState, MobileSpeechRecoRequest.RequestState newState, bool isRequestComplete)
		{
			MobileSpeechRecoTracer.TraceDebug(this, this.id, "Entering MobileSpeechRecoRequest.InvokeCallback Current state='{0}', New state ='{1}', Request complete = '{2}'", new object[]
			{
				currentState,
				newState,
				isRequestComplete
			});
			MobileSpeechRecoTracer.TraceDebug(this, this.id, "MobileSpeechRecoRequest.InvokeCallback Completed args Result='{0}', Error='{1}'", new object[]
			{
				completedArgs.Result,
				(completedArgs.Error != null) ? completedArgs.Error.ToString() : "<null>"
			});
			bool flag = false;
			lock (this.lockObj)
			{
				if (!this.syncIsRequestComplete)
				{
					MobileSpeechRecoTracer.TraceDebug(this, this.id, "Complete async operation by invoking callback", new object[0]);
					this.ValidateAndChangeState(currentState, newState);
					if (isRequestComplete)
					{
						TimeSpan requestElapsedTime = ExDateTime.UtcNow - this.utcStartTime;
						completedArgs.RequestElapsedTime = requestElapsedTime;
					}
					this.syncCallback(completedArgs);
					this.syncCallback = null;
					if (isRequestComplete)
					{
						MobileSpeechRecoTracer.TraceDebug(this, this.id, "Set flag to fire completed event", new object[0]);
						this.syncIsRequestComplete = true;
						flag = true;
					}
				}
			}
			if (flag)
			{
				this.FireCompletedEvent();
			}
		}

		// Token: 0x06001353 RID: 4947 RVA: 0x00056418 File Offset: 0x00054618
		private void ValidateAndChangeState(MobileSpeechRecoRequest.RequestState requiredCurrentState, MobileSpeechRecoRequest.RequestState newState)
		{
			MobileSpeechRecoTracer.TraceDebug(this, this.id, "Entering MobileSpeechRecoRequest.ValidateAndChangeState Reqd current state='{0}', New state ='{1}'", new object[]
			{
				requiredCurrentState,
				newState
			});
			lock (this.lockObj)
			{
				ExAssert.RetailAssert(this.syncState == requiredCurrentState, "Current state should be '{0}' but was found to be '{1}'", new object[]
				{
					requiredCurrentState,
					this.syncState
				});
				this.syncState = newState;
			}
		}

		// Token: 0x06001354 RID: 4948 RVA: 0x000564B8 File Offset: 0x000546B8
		private void FireCompletedEvent()
		{
			MobileSpeechRecoTracer.TraceDebug(this, this.id, "Entering MobileSpeechRecoRequest.FireCompletedEvent", new object[0]);
			if (this.Completed != null)
			{
				this.Completed(this, null);
				this.Completed = null;
			}
		}

		// Token: 0x04000C5C RID: 3164
		private Guid id;

		// Token: 0x04000C5D RID: 3165
		private IMobileSpeechRecoRequestBehavior behavior;

		// Token: 0x04000C5E RID: 3166
		private IPlatformBuilder platformBuilder;

		// Token: 0x04000C5F RID: 3167
		private IMobileRecognizer recognizer;

		// Token: 0x04000C60 RID: 3168
		private Timer timer;

		// Token: 0x04000C61 RID: 3169
		private List<UMGrammar> grammars;

		// Token: 0x04000C62 RID: 3170
		private object lockObj = new object();

		// Token: 0x04000C63 RID: 3171
		private ExDateTime utcStartTime = ExDateTime.UtcNow;

		// Token: 0x04000C64 RID: 3172
		private MobileSpeechRecoRequest.RequestState syncState;

		// Token: 0x04000C65 RID: 3173
		private MobileRecoAsyncCompletedDelegate syncCallback;

		// Token: 0x04000C66 RID: 3174
		private bool syncIsRequestComplete;

		// Token: 0x0200028B RID: 651
		private enum RequestState
		{
			// Token: 0x04000C69 RID: 3177
			New,
			// Token: 0x04000C6A RID: 3178
			Preparing,
			// Token: 0x04000C6B RID: 3179
			Prepared,
			// Token: 0x04000C6C RID: 3180
			Recognizing,
			// Token: 0x04000C6D RID: 3181
			RecognizeComplete
		}
	}
}
