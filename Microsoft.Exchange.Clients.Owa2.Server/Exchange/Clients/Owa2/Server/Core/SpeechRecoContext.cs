using System;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000373 RID: 883
	internal class SpeechRecoContext
	{
		// Token: 0x06001C76 RID: 7286 RVA: 0x00071D1D File Offset: 0x0006FF1D
		internal SpeechRecoContext(SpeechRecognition recognitionHelper)
		{
			this.recognitionHelper = recognitionHelper;
			this.Event = new ManualResetEvent(false);
		}

		// Token: 0x1700065D RID: 1629
		// (get) Token: 0x06001C77 RID: 7287 RVA: 0x00071D43 File Offset: 0x0006FF43
		// (set) Token: 0x06001C78 RID: 7288 RVA: 0x00071D4B File Offset: 0x0006FF4B
		internal SpeechRecognitionProcessor.SpeechHttpStatus Status { get; private set; }

		// Token: 0x1700065E RID: 1630
		// (get) Token: 0x06001C79 RID: 7289 RVA: 0x00071D54 File Offset: 0x0006FF54
		// (set) Token: 0x06001C7A RID: 7290 RVA: 0x00071D5C File Offset: 0x0006FF5C
		internal string Results { get; private set; }

		// Token: 0x1700065F RID: 1631
		// (get) Token: 0x06001C7B RID: 7291 RVA: 0x00071D65 File Offset: 0x0006FF65
		// (set) Token: 0x06001C7C RID: 7292 RVA: 0x00071D6D File Offset: 0x0006FF6D
		internal ManualResetEvent Event { get; private set; }

		// Token: 0x06001C7D RID: 7293 RVA: 0x00071D7F File Offset: 0x0006FF7F
		public void AddRecoRequestAsync()
		{
			ExTraceGlobals.SpeechRecognitionTracer.TraceDebug((long)this.GetHashCode(), "Entering SpeechRecoContext.AddRecoRequestAsync");
			this.recognitionHelper.AddRecoRequestAsync(delegate(SpeechRecognitionProcessor.SpeechProcessorAsyncCompletedArgs args)
			{
				this.SetAsyncArgsAndSignalEvent(args);
			});
		}

		// Token: 0x06001C7E RID: 7294 RVA: 0x00071DB8 File Offset: 0x0006FFB8
		public void RecognizeAsync(byte[] audioBytes)
		{
			lock (this.thisLock)
			{
				ExTraceGlobals.SpeechRecognitionTracer.TraceDebug((long)this.GetHashCode(), "Entering SpeechRecoContext.RecognizeAsync");
				if (this.Event != null)
				{
					this.Event.Reset();
					this.recognitionHelper.RecognizeAsync(audioBytes, delegate(SpeechRecognitionProcessor.SpeechProcessorAsyncCompletedArgs args)
					{
						this.SetAsyncArgsAndSignalEvent(args);
					});
				}
			}
		}

		// Token: 0x06001C7F RID: 7295 RVA: 0x00071E3C File Offset: 0x0007003C
		public void Dispose()
		{
			lock (this.thisLock)
			{
				if (this.Event != null)
				{
					this.Event.Dispose();
					this.Event = null;
				}
			}
		}

		// Token: 0x06001C80 RID: 7296 RVA: 0x00071E90 File Offset: 0x00070090
		private void SetAsyncArgsAndSignalEvent(SpeechRecognitionProcessor.SpeechProcessorAsyncCompletedArgs args)
		{
			ExTraceGlobals.SpeechRecognitionTracer.TraceDebug<int, string>((long)this.GetHashCode(), "SpeechRecoContext HttpStatus code:{0} ,  ResponseText:{1}", args.HttpStatus.StatusCode, args.ResponseText);
			lock (this.thisLock)
			{
				if (this.Event != null)
				{
					this.Status = args.HttpStatus;
					this.Results = args.ResponseText;
					this.Event.Set();
				}
			}
		}

		// Token: 0x0400100F RID: 4111
		private object thisLock = new object();

		// Token: 0x04001010 RID: 4112
		private SpeechRecognition recognitionHelper;
	}
}
