using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Web;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200069D RID: 1693
	internal class HangingProxyServiceTask<T> : ProxyServiceTask<T>
	{
		// Token: 0x0600341A RID: 13338 RVA: 0x000BBB94 File Offset: 0x000B9D94
		public HangingProxyServiceTask(BaseRequest request, CallContext callContext, ServiceAsyncResult<T> serviceAsyncResult, WebServicesInfo[] services, int readTimeout, Func<BaseSoapResponse> getCloseConnectionResponse) : base(request, callContext, serviceAsyncResult, services)
		{
			this.readTimeout = readTimeout;
			this.getCloseConnectionResponse = getCloseConnectionResponse;
		}

		// Token: 0x0600341B RID: 13339 RVA: 0x000BBBC4 File Offset: 0x000B9DC4
		protected override void ProcessResponseMessageAndCompleteIfNecessary(TimeSpan elapsed, HttpWebResponse response, ProxyResult result)
		{
			this.proxyResponseStream = response.GetResponseStream();
			this.elapsedTime = elapsed;
			this.proxyResponseStream.ReadTimeout = this.readTimeout;
			this.proxyDoneEvent.Set();
			base.SetProxyHopHeaders(this.proxiedToService);
			this.WriteProxyHopHeadersToResponse();
			this.wireWriter = EwsResponseWireWriter.Create(CallContext.Current, true);
			this.BeginProxyResponseStreamRead();
		}

		// Token: 0x0600341C RID: 13340 RVA: 0x000BBC2C File Offset: 0x000B9E2C
		private void BeginProxyResponseStreamRead()
		{
			try
			{
				this.proxyResponseStream.BeginRead(this.responseBuffer, 0, this.responseBuffer.Length, new AsyncCallback(this.EndProxyResponseStreamRead), this.responseBuffer);
			}
			catch (IOException exception)
			{
				this.HandleProxyStreamReadException(exception);
			}
			catch (WebException exception2)
			{
				this.HandleProxyStreamReadException(exception2);
			}
		}

		// Token: 0x0600341D RID: 13341 RVA: 0x000BBC98 File Offset: 0x000B9E98
		private void EndProxyResponseStreamRead(IAsyncResult asyncResult)
		{
			try
			{
				byte[] responseBytes = asyncResult.AsyncState as byte[];
				int num = this.proxyResponseStream.EndRead(asyncResult);
				if (num != 0)
				{
					this.wireWriter.WriteResponseToWire(responseBytes, 0, num);
					this.BeginProxyResponseStreamRead();
				}
				else
				{
					this.CompleteRequest(null);
				}
			}
			catch (IOException exception)
			{
				this.HandleProxyStreamReadException(exception);
			}
			catch (WebException exception2)
			{
				this.HandleProxyStreamReadException(exception2);
			}
			catch (HttpException exception3)
			{
				this.HandleClientSendException(exception3);
			}
		}

		// Token: 0x0600341E RID: 13342 RVA: 0x000BBD28 File Offset: 0x000B9F28
		private void HandleProxyStreamReadException(Exception exception)
		{
			ExTraceGlobals.ProxyEvaluatorTracer.TraceDebug<Exception>((long)this.GetHashCode(), "[HangingProxyServiceTask::HandleProxyStreamReadException] Encountered exception reading from the proxied response stream: {0}", exception);
			RequestDetailsLoggerBase<RequestDetailsLogger>.SafeLogRequestException(base.CallContext.ProtocolLog, exception, "HangingProxy_ProxyReadError");
			try
			{
				BaseSoapResponse response = this.getCloseConnectionResponse();
				this.wireWriter.WriteResponseToWire(response, false);
				this.CompleteRequest(null);
			}
			catch (HttpException exception2)
			{
				this.HandleClientSendException(exception2);
			}
		}

		// Token: 0x0600341F RID: 13343 RVA: 0x000BBDA0 File Offset: 0x000B9FA0
		private void HandleClientSendException(Exception exception)
		{
			ExTraceGlobals.ProxyEvaluatorTracer.TraceDebug<Exception>((long)this.GetHashCode(), "[HangingProxyServiceTask::HandleClientSentException] Encountered exception sending response to client: {0}", exception);
			RequestDetailsLoggerBase<RequestDetailsLogger>.SafeLogRequestException(base.CallContext.ProtocolLog, exception, "HangingProxy_ClientSendError");
			this.CompleteRequest(null);
		}

		// Token: 0x06003420 RID: 13344 RVA: 0x000BBDD6 File Offset: 0x000B9FD6
		private void CompleteRequest(Exception exception)
		{
			this.wireWriter.WaitForSendCompletion();
			this.FinishRequest(string.Format("[C,P({0})]", this.proxiedToService.Url), this.queueAndDelayTime, this.elapsedTime, exception);
		}

		// Token: 0x06003421 RID: 13345 RVA: 0x000BBE0C File Offset: 0x000BA00C
		private void WriteProxyHopHeadersToResponse()
		{
			Dictionary<string, string> proxyHopHeaders = EWSSettings.ProxyHopHeaders;
			if (Global.WriteProxyHopHeaders && proxyHopHeaders != null && HttpContext.Current != null && HttpContext.Current.Response != null)
			{
				HttpResponse response = HttpContext.Current.Response;
				foreach (KeyValuePair<string, string> keyValuePair in proxyHopHeaders)
				{
					response.AppendHeader(keyValuePair.Key, keyValuePair.Value);
				}
			}
		}

		// Token: 0x06003422 RID: 13346 RVA: 0x000BBE98 File Offset: 0x000BA098
		protected override void InternalDispose()
		{
			base.InternalDispose();
			if (this.wireWriter != null)
			{
				this.wireWriter.Dispose();
				this.wireWriter = null;
			}
		}

		// Token: 0x04001D79 RID: 7545
		private const int BufferSize = 4096;

		// Token: 0x04001D7A RID: 7546
		private EwsResponseWireWriter wireWriter;

		// Token: 0x04001D7B RID: 7547
		private Stream proxyResponseStream;

		// Token: 0x04001D7C RID: 7548
		private TimeSpan elapsedTime;

		// Token: 0x04001D7D RID: 7549
		private int readTimeout;

		// Token: 0x04001D7E RID: 7550
		private byte[] responseBuffer = new byte[4096];

		// Token: 0x04001D7F RID: 7551
		private Func<BaseSoapResponse> getCloseConnectionResponse;
	}
}
