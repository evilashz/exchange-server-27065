using System;
using System.Web;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000394 RID: 916
	internal class SpeechRecognitionHandler : IHttpAsyncHandler, IHttpHandler
	{
		// Token: 0x17000693 RID: 1683
		// (get) Token: 0x06001D49 RID: 7497 RVA: 0x000748D6 File Offset: 0x00072AD6
		public bool IsReusable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001D4A RID: 7498 RVA: 0x000748D9 File Offset: 0x00072AD9
		public void ProcessRequest(HttpContext context)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06001D4B RID: 7499 RVA: 0x000748E0 File Offset: 0x00072AE0
		public IAsyncResult BeginProcessRequest(HttpContext httpContext, AsyncCallback callback, object context)
		{
			ExTraceGlobals.SpeechRecognitionTracer.TraceDebug((long)this.GetHashCode(), "Entering SpeechRecognitionHandler.BeginProcessRequest");
			SpeechRecognitionHandler.InitializeStatisticsLoggers();
			this.speechRecoProcessor = new SpeechRecognitionProcessor(httpContext);
			return this.speechRecoProcessor.BeginRecognition(callback, context);
		}

		// Token: 0x06001D4C RID: 7500 RVA: 0x00074918 File Offset: 0x00072B18
		public void EndProcessRequest(IAsyncResult result)
		{
			ExTraceGlobals.SpeechRecognitionTracer.TraceDebug((long)this.GetHashCode(), "Entering SpeechRecognitionHandler.EndProcessRequest");
			SpeechRecognitionAsyncResult speechRecognitionAsyncResult = result as SpeechRecognitionAsyncResult;
			HttpResponse response = this.speechRecoProcessor.HttpContext.Response;
			response.TrySkipIisCustomErrors = true;
			response.BufferOutput = false;
			response.StatusCode = speechRecognitionAsyncResult.StatusCode;
			if (!string.IsNullOrEmpty(speechRecognitionAsyncResult.StatusDescription))
			{
				response.StatusDescription = speechRecognitionAsyncResult.StatusDescription;
			}
			response.ContentType = "application/json; charset=utf-8";
			response.Write(speechRecognitionAsyncResult.ResponseText);
		}

		// Token: 0x06001D4D RID: 7501 RVA: 0x000749A0 File Offset: 0x00072BA0
		private static void InitializeStatisticsLoggers()
		{
			if (SpeechRecognitionHandler.MobileSpeechRequestStatisticsLogger == null)
			{
				lock (SpeechRecognitionHandler.loggerLock)
				{
					if (SpeechRecognitionHandler.MobileSpeechRequestStatisticsLogger == null)
					{
						SpeechRecognitionHandler.MobileSpeechRequestStatisticsLogger = MobileSpeechRequestStatisticsLogger.Instance;
						SpeechRecognitionHandler.MobileSpeechRequestStatisticsLogger.Init();
					}
				}
			}
			if (SpeechRecognitionHandler.SpeechProcessorStatisticsLogger == null)
			{
				lock (SpeechRecognitionHandler.loggerLock)
				{
					if (SpeechRecognitionHandler.SpeechProcessorStatisticsLogger == null)
					{
						SpeechRecognitionHandler.SpeechProcessorStatisticsLogger = SpeechProcessorStatisticsLogger.Instance;
						SpeechRecognitionHandler.SpeechProcessorStatisticsLogger.Init();
					}
				}
			}
			if (SpeechRecognitionHandler.MethodCallLatencyStatisticsLogger == null)
			{
				lock (SpeechRecognitionHandler.loggerLock)
				{
					if (SpeechRecognitionHandler.MethodCallLatencyStatisticsLogger == null)
					{
						SpeechRecognitionHandler.MethodCallLatencyStatisticsLogger = MethodCallLatencyStatisticsLogger.Instance;
						SpeechRecognitionHandler.MethodCallLatencyStatisticsLogger.Init();
					}
				}
			}
		}

		// Token: 0x04001084 RID: 4228
		public static MobileSpeechRequestStatisticsLogger MobileSpeechRequestStatisticsLogger;

		// Token: 0x04001085 RID: 4229
		public static SpeechProcessorStatisticsLogger SpeechProcessorStatisticsLogger;

		// Token: 0x04001086 RID: 4230
		public static MethodCallLatencyStatisticsLogger MethodCallLatencyStatisticsLogger;

		// Token: 0x04001087 RID: 4231
		private static object loggerLock = new object();

		// Token: 0x04001088 RID: 4232
		private SpeechRecognitionProcessor speechRecoProcessor;
	}
}
