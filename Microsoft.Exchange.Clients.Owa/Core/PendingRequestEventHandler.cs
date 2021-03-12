using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x0200020B RID: 523
	[OwaEventNamespace("PendingRequest")]
	internal class PendingRequestEventHandler : OwaEventHandlerBase
	{
		// Token: 0x060011A5 RID: 4517 RVA: 0x0006A4FB File Offset: 0x000686FB
		public PendingRequestEventHandler()
		{
			base.DontWriteHeaders = true;
		}

		// Token: 0x060011A6 RID: 4518 RVA: 0x0006A50C File Offset: 0x0006870C
		[OwaEventVerb(OwaEventVerb.Get)]
		[OwaEvent("PendingNotificationRequest")]
		[OwaEventParameter("UA", typeof(bool))]
		[OwaEventParameter("n", typeof(int), false, true)]
		public IAsyncResult BeginUseNotificationPipe(AsyncCallback callback, object extraData)
		{
			ExTraceGlobals.NotificationsCallTracer.TraceDebug((long)this.GetHashCode(), "BeginUseNotificationPipe called from the client");
			HttpResponse httpResponse = base.OwaContext.HttpContext.Response;
			Utilities.DisableContentEncodingForThisResponse(httpResponse);
			httpResponse.AppendHeader("X-NoCompression", "1");
			httpResponse.AppendHeader("X-NoBuffering", "1");
			base.OwaContext.DoNotTriggerLatencyDetectionReport();
			this.pendingManager = base.OwaContext.UserContext.PendingRequestManager;
			bool isFullyInitialized = base.UserContext.IsFullyInitialized;
			int num;
			base.UserContext.DangerousBeginUnlockedAction(false, out num);
			if (num != 1)
			{
				ExWatson.SendReport(new InvalidOperationException("Thread held more than 1 lock before async operation"), ReportOptions.None, null);
			}
			this.asyncResult = new OwaAsyncResult(callback, extraData);
			try
			{
				this.response = new ChunkedHttpResponse(this.HttpContext);
				this.pendingManager.BeginSendNotification(new AsyncCallback(this.UseNotificationPipeCallback), this.response, isFullyInitialized, this);
			}
			catch (Exception ex)
			{
				ExTraceGlobals.NotificationsCallTracer.TraceError<Exception>((long)this.GetHashCode(), "An exception happened while executing BeginUseNotificationPipe. Exception:{0}", ex);
				lock (this.asyncResult)
				{
					if (!this.asyncResult.IsCompleted)
					{
						this.asyncResult.CompleteRequest(true, ex);
					}
				}
			}
			return this.asyncResult;
		}

		// Token: 0x060011A7 RID: 4519 RVA: 0x0006A670 File Offset: 0x00068870
		[OwaEvent("PendingNotificationRequest")]
		public void EndUseNotificationPipe(IAsyncResult async)
		{
			if (this.isDisposing)
			{
				return;
			}
			OwaAsyncResult owaAsyncResult = (OwaAsyncResult)async;
			if (HttpContext.Current != null && OwaContext.Current != base.OwaContext)
			{
				base.OwaContext.IgnoreUnlockForcefully = true;
			}
			try
			{
				if (owaAsyncResult.Exception != null)
				{
					bool isBasicAuthentication = Utilities.IsBasicAuthentication(base.OwaContext.HttpContext.Request);
					ErrorInformation exceptionHandlingInformation = Utilities.GetExceptionHandlingInformation(owaAsyncResult.Exception, base.OwaContext.MailboxIdentity, Utilities.IsWebPartRequest(base.OwaContext), string.Empty, string.Empty, isBasicAuthentication);
					Exception ex = (owaAsyncResult.Exception.InnerException == null) ? owaAsyncResult.Exception : owaAsyncResult.Exception.InnerException;
					ExTraceGlobals.NotificationsCallTracer.TraceDebug((long)this.GetHashCode(), "An exception was thrown during the processing of the async request");
					StringBuilder stringBuilder = new StringBuilder();
					using (StringWriter stringWriter = new StringWriter(stringBuilder, CultureInfo.InvariantCulture))
					{
						OwaEventHttpHandler.RenderErrorDiv(base.OwaContext, stringWriter, exceptionHandlingInformation.Message, exceptionHandlingInformation.MessageDetails, exceptionHandlingInformation.OwaEventHandlerErrorCode, exceptionHandlingInformation.HideDebugInformation ? null : ex);
					}
					if (this.response != null)
					{
						this.response.WriteError(Utilities.JavascriptEncode(stringBuilder.ToString()));
						base.OwaContext.ErrorSent = true;
					}
				}
				if (this.response != null)
				{
					this.response.WriteLastChunk();
				}
			}
			catch (OwaNotificationPipeWriteException)
			{
			}
			finally
			{
				if (owaAsyncResult.Exception != null)
				{
					Utilities.HandleException(base.OwaContext, owaAsyncResult.Exception);
				}
			}
		}

		// Token: 0x060011A8 RID: 4520 RVA: 0x0006A830 File Offset: 0x00068A30
		[OwaEvent("FinishNotificationRequest")]
		[OwaEventParameter("Fn", typeof(bool), false, true)]
		[OwaEventVerb(OwaEventVerb.Post)]
		public void DisposePendingNotificationClientRequest()
		{
			if (base.OwaContext.UserContext == null)
			{
				return;
			}
			this.pendingManager = base.OwaContext.UserContext.PendingRequestManager;
			object parameter = base.GetParameter("Fn");
			if (parameter != null)
			{
				bool flag = (bool)parameter;
			}
			int num = 0;
			try
			{
				base.UserContext.DangerousBeginUnlockedAction(false, out num);
				if (num != 1)
				{
					ExWatson.SendReport(new InvalidOperationException("Thread held more than 1 lock in DisposePendingNotificationClientRequest method."), ReportOptions.None, null);
				}
				bool flag2 = this.pendingManager.HandleFinishRequestFromClient();
				HttpResponse httpResponse = base.OwaContext.HttpContext.Response;
				httpResponse.Write("var syncFnshRq = ");
				httpResponse.Write(flag2 ? "1" : "0");
				httpResponse.AppendHeader("X-OWA-EventResult", "0");
				Utilities.MakePageNoCacheNoStore(httpResponse);
				httpResponse.ContentType = Utilities.GetContentTypeString(base.ResponseContentType);
			}
			finally
			{
				base.UserContext.DangerousEndUnlockedAction(false, num);
			}
		}

		// Token: 0x060011A9 RID: 4521 RVA: 0x0006A924 File Offset: 0x00068B24
		private void UseNotificationPipeCallback(IAsyncResult async)
		{
			OwaAsyncResult owaAsyncResult = (OwaAsyncResult)async;
			try
			{
				this.pendingManager.EndSendNotification(owaAsyncResult);
			}
			catch (Exception exception)
			{
				lock (this.asyncResult)
				{
					if (!this.asyncResult.IsCompleted)
					{
						this.asyncResult.Exception = exception;
					}
				}
			}
			try
			{
				lock (this.asyncResult)
				{
					if (!this.asyncResult.IsCompleted)
					{
						this.asyncResult.CompleteRequest(owaAsyncResult.CompletedSynchronously);
					}
				}
			}
			finally
			{
				this.pendingManager.RecordFinishPendingRequest();
			}
		}

		// Token: 0x060011AA RID: 4522 RVA: 0x0006AA00 File Offset: 0x00068C00
		protected override void InternalDispose(bool isExplicitDispose)
		{
			if (isExplicitDispose && !this.isDisposing && this.asyncResult != null)
			{
				lock (this.asyncResult)
				{
					if (!this.asyncResult.IsCompleted)
					{
						this.isDisposing = true;
						this.asyncResult.CompleteRequest(false);
					}
				}
			}
		}

		// Token: 0x060011AB RID: 4523 RVA: 0x0006AA74 File Offset: 0x00068C74
		internal static bool IsObsoleteRequest(OwaContext owaContext, UserContext userContext)
		{
			if (owaContext.RequestType == OwaRequestType.Oeh && userContext == null)
			{
				string queryStringParameter = Utilities.GetQueryStringParameter(owaContext.HttpContext.Request, "ns", false);
				string queryStringParameter2 = Utilities.GetQueryStringParameter(owaContext.HttpContext.Request, "ev", false);
				string queryStringParameter3 = Utilities.GetQueryStringParameter(owaContext.HttpContext.Request, "Fn", false);
				return queryStringParameter == "PendingRequest" && queryStringParameter2 == "FinishNotificationRequest" && queryStringParameter3 == "1";
			}
			return false;
		}

		// Token: 0x04000BE0 RID: 3040
		private const string UserActivityParameter = "UA";

		// Token: 0x04000BE1 RID: 3041
		private const string FinalizeNotifiersParameter = "Fn";

		// Token: 0x04000BE2 RID: 3042
		internal const string EventNameSpace = "PendingRequest";

		// Token: 0x04000BE3 RID: 3043
		internal const string MethodPendingNotificationRequest = "PendingNotificationRequest";

		// Token: 0x04000BE4 RID: 3044
		internal const string MethodFinishNotificationRequest = "FinishNotificationRequest";

		// Token: 0x04000BE5 RID: 3045
		private const string UniquePendingGetIdentifierParameter = "n";

		// Token: 0x04000BE6 RID: 3046
		private OwaAsyncResult asyncResult;

		// Token: 0x04000BE7 RID: 3047
		private ChunkedHttpResponse response;

		// Token: 0x04000BE8 RID: 3048
		private PendingRequestManager pendingManager;

		// Token: 0x04000BE9 RID: 3049
		private volatile bool isDisposing;
	}
}
