using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MapiHttpClient;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.RpcClientAccess;
using Microsoft.Exchange.RpcClientAccess.Parser;

namespace Microsoft.Exchange.MapiHttp
{
	// Token: 0x02000002 RID: 2
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class ClientAsyncOperation : EasyCancelableAsyncResult
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		protected ClientAsyncOperation(ClientSessionContext context, CancelableAsyncCallback asyncCallback, object asyncState) : base(asyncCallback, asyncState)
		{
			this.context = context;
			this.perfDateTime = new PerfDateTime();
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x0000210B File Offset: 0x0000030B
		public ClientSessionContext Context
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000003 RID: 3 RVA: 0x00002113 File Offset: 0x00000313
		// (set) Token: 0x06000004 RID: 4 RVA: 0x0000211B File Offset: 0x0000031B
		public MapiHttpVersion ClientVersion { get; set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000005 RID: 5 RVA: 0x00002124 File Offset: 0x00000324
		public WebHeaderCollection HttpWebResponseHeaders
		{
			get
			{
				return this.context.ResponseHeaders;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000006 RID: 6 RVA: 0x00002131 File Offset: 0x00000331
		public WebHeaderCollection HttpWebRequestHeaders
		{
			get
			{
				if (this.httpWebRequest != null)
				{
					return ClientAsyncOperation.GetDisplayableHeaders(this.httpWebRequest.Headers);
				}
				return new WebHeaderCollection();
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000007 RID: 7 RVA: 0x00002154 File Offset: 0x00000354
		public HttpStatusCode LastResponseStatusCode
		{
			get
			{
				return this.context.LastResponseStatusCode.GetValueOrDefault();
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000008 RID: 8 RVA: 0x00002174 File Offset: 0x00000374
		public string LastResponseStatusDescription
		{
			get
			{
				return this.context.LastResponseStatusDescription;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000009 RID: 9
		protected abstract string RequestType { get; }

		// Token: 0x0600000A RID: 10 RVA: 0x00002184 File Offset: 0x00000384
		public bool TryGetServerVersion(out MapiHttpVersion version)
		{
			version = null;
			if (this.httpWebResponse == null)
			{
				return false;
			}
			string text = this.httpWebResponse.Headers.Get("X-ServerApplication");
			if (!string.IsNullOrWhiteSpace(text))
			{
				int num = text.IndexOf('/');
				if (num >= 0)
				{
					return MapiHttpVersion.TryParse(text.Substring(num + 1), out version);
				}
			}
			return false;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002220 File Offset: 0x00000420
		protected void InternalBegin(Action<Writer> serializer)
		{
			bool flag = false;
			try
			{
				int num = 0;
				if (serializer != null)
				{
					using (CountWriter countWriter = new CountWriter())
					{
						serializer(countWriter);
						num = (int)countWriter.Position;
					}
				}
				if (num > 268288)
				{
					throw new InvalidOperationException(string.Format("Request is larger than the maximum request size allowed; size={0}, maxSize={1}", num, 268288));
				}
				if (num > 0)
				{
					this.requestBuffer = new WorkBuffer(num);
					using (BufferWriter bufferWriter = new BufferWriter(this.requestBuffer.ArraySegment))
					{
						serializer(bufferWriter);
						this.requestData = new ArraySegment<byte>(this.requestBuffer.Array, this.requestBuffer.Offset, (int)bufferWriter.Position);
					}
				}
				this.readResponseBuffer = new WorkBuffer(32768);
				this.httpWebRequest = this.context.CreateRequest(out this.requestId);
				this.httpWebRequest.ContentLength = (long)this.requestData.Count;
				this.httpWebRequest.Headers.Add("X-RequestType", this.RequestType);
				if (this.ClientVersion != null)
				{
					this.httpWebRequest.Headers.Add("X-ClientApplication", string.Format("{0}/{1}", "MapiHttpClient", this.ClientVersion));
				}
				else
				{
					this.httpWebRequest.Headers.Add("X-ClientApplication", ClientAsyncOperation.ClientApplication);
				}
				if (this.context.DesiredExpiration != null)
				{
					int num2 = (int)this.context.DesiredExpiration.Value.TotalMilliseconds;
					this.httpWebRequest.Headers.Add("X-ExpirationInfo", num2.ToString());
				}
				this.context.UpdateElapsedTime(null);
				ExTraceGlobals.ClientAsyncOperationTracer.TraceInformation(51616, 0L, "{0}.Begin; ContextHandle={1}, RequestId={2}, URI={3}", new object[]
				{
					this.RequestType,
					this.context.ContextHandle,
					this.requestId,
					this.context.RequestPath
				});
				this.context.SetAdditionalRequestHeaders(this.httpWebRequest);
				this.Execute(delegate
				{
					this.StartTimerWrapper(delegate
					{
						this.requestStartTime = new DateTime?(this.perfDateTime.UtcNow);
						this.httpWebRequest.BeginGetRequestStream(new AsyncCallback(ClientAsyncOperation.BeginGetRequestStreamCallbackEntry), this);
					});
				});
				ExTraceGlobals.ClientAsyncOperationTracer.TraceInformation<string, IntPtr, string>(45472, 0L, "{0}.Begin; Success; ContextHandle={1}, RequestId={2}", this.RequestType, this.context.ContextHandle, this.requestId);
				flag = true;
			}
			catch (Exception ex)
			{
				ExTraceGlobals.ClientAsyncOperationTracer.TraceInformation(61856, 0L, "{0}.Begin; Failed; ContextHandle={1}, RequestId={2}, Exception={3}", new object[]
				{
					this.RequestType,
					this.context.ContextHandle,
					this.requestId,
					ex
				});
				throw;
			}
			finally
			{
				if (!flag)
				{
					this.Cleanup();
				}
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002568 File Offset: 0x00000768
		protected ErrorCode InternalEnd(Func<Reader, int> parser)
		{
			ErrorCode errorCode = ErrorCode.None;
			ExTraceGlobals.ClientAsyncOperationTracer.TraceInformation<string, IntPtr, string>(37280, 0L, "{0}.End; ContextHandle={1}, RequestId={2}", this.RequestType, this.context.ContextHandle, this.requestId);
			try
			{
				base.WaitForCompletion();
				if (this.exception != null)
				{
					throw new AggregateException("Operation failed", this.exception);
				}
				if (this.httpWebResponse == null)
				{
					throw new InvalidOperationException("Should have a HttpWebResponse if no exception.");
				}
				if (this.httpWebResponse.StatusCode != HttpStatusCode.OK)
				{
					this.exceptionTime = new DateTime?(this.perfDateTime.UtcNow);
					throw ProtocolException.FromHttpStatusCode((LID)47372, string.Format("Server returned HttpStatusCode.{0} failure.", this.httpWebResponse.StatusCode), this.GetFailureContext(null), this.httpWebResponse.StatusCode, this.httpWebResponse.StatusDescription, this.httpWebRequest.Headers, this.httpWebResponse.Headers, null);
				}
				if (this.responseParser == null)
				{
					throw new InvalidOperationException("Should have a ResponseParser on HttpStatusCode.OK");
				}
				if (this.responseParser.ResponseCode != ResponseCode.Success)
				{
					MapiHttpVersion mapiHttpVersion;
					if (!this.TryGetServerVersion(out mapiHttpVersion))
					{
						mapiHttpVersion = null;
					}
					this.exceptionTime = new DateTime?(this.perfDateTime.UtcNow);
					throw ProtocolException.FromResponseCode((LID)49184, string.Format("Server returned ResponseCode.{0} failure.", this.responseParser.ResponseCode), this.GetFailureContext(null), this.httpWebResponse.StatusCode, this.httpWebResponse.StatusDescription, this.responseParser.ResponseCode, null, this.httpWebRequest.Headers, this.httpWebResponse.Headers, mapiHttpVersion);
				}
				if (parser != null)
				{
					using (BufferReader bufferReader = new BufferReader(this.responseParser.ResponseData.DeepClone<byte>()))
					{
						this.CheckStatusCodeAndThrowOnFailedResponse(bufferReader);
						errorCode = (ErrorCode)parser(bufferReader);
						goto IL_206;
					}
				}
				if (this.responseParser.ResponseData.Count > 0)
				{
					throw new InvalidOperationException(string.Format("Operation {0} didn't supply a response parser and response was returned from server; size={1}", this.RequestType, this.responseParser.ResponseData.Count));
				}
				IL_206:
				if (this.context != null)
				{
					this.context.UpdateElapsedTime(this.responseParser.ElapsedTime);
					TimeSpan expiration;
					if (this.TryGetExpirationInfo(out expiration))
					{
						this.context.UpdateRefresh(expiration);
					}
				}
				ExTraceGlobals.ClientAsyncOperationTracer.TraceInformation(53664, 0L, "{0}.End; Success; ContextHandle={1}, RequestId={2}, ErrorCode={3}", new object[]
				{
					this.RequestType,
					this.context.ContextHandle,
					this.requestId,
					errorCode
				});
			}
			catch (Exception ex)
			{
				ExTraceGlobals.ClientAsyncOperationTracer.TraceInformation(41376, 0L, "{0}.End; Failed; ContextHandle={1}, RequestId={2}, Exception={3}", new object[]
				{
					this.RequestType,
					this.context.ContextHandle,
					this.requestId,
					ex
				});
				throw;
			}
			finally
			{
				this.Cleanup();
			}
			return errorCode;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000028AC File Offset: 0x00000AAC
		protected override void InternalCancel()
		{
			try
			{
				this.StopTimeoutTimer();
				if (this.httpWebRequest != null)
				{
					this.httpWebRequest.Abort();
					ExTraceGlobals.ClientAsyncOperationTracer.TraceInformation<string, IntPtr, string>(43132, 0L, "{0}.Cancel; Success; ContextHandle={1}, RequestId={2}", this.RequestType, this.context.ContextHandle, this.requestId);
				}
			}
			catch (Exception ex)
			{
				ExTraceGlobals.ClientAsyncOperationTracer.TraceInformation(59516, 0L, "{0}.Cancel; Failed; ContextHandle={1}, RequestId={2}, Exception={3}", new object[]
				{
					this.RequestType,
					this.context.ContextHandle,
					this.requestId,
					ex
				});
			}
		}

		// Token: 0x0600000E RID: 14 RVA: 0x0000295C File Offset: 0x00000B5C
		private static void AppendHeaders(StringBuilder stringBuilder, WebHeaderCollection headers)
		{
			WebHeaderCollection displayableHeaders = ClientAsyncOperation.GetDisplayableHeaders(headers);
			if (displayableHeaders.Count > 0)
			{
				for (int i = 0; i < displayableHeaders.Count; i++)
				{
					stringBuilder.AppendFormat("{0}: {1} \r\n", displayableHeaders.Keys[i], displayableHeaders[i]);
				}
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000029AC File Offset: 0x00000BAC
		private static WebHeaderCollection GetDisplayableHeaders(WebHeaderCollection headers)
		{
			WebHeaderCollection webHeaderCollection = new WebHeaderCollection();
			if (headers != null && headers.Count > 0)
			{
				for (int i = 0; i < headers.Count; i++)
				{
					string value;
					if (ClientAsyncOperation.TryGetDisplayableHeader(headers.Keys[i], headers[i], out value))
					{
						webHeaderCollection.Add(headers.Keys[i], value);
					}
				}
			}
			return webHeaderCollection;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002A0C File Offset: 0x00000C0C
		private static bool TryGetDisplayableHeader(string key, string header, out string displayableHeader)
		{
			displayableHeader = header;
			if (string.Compare(key, "authorization", true) == 0 || string.Compare(key, "WWW-Authenticate", true) == 0)
			{
				int num = header.IndexOf(" ");
				if (num >= 0)
				{
					displayableHeader = string.Format("{0} [truncated]", header.Substring(0, num));
				}
			}
			return true;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002A60 File Offset: 0x00000C60
		private static string GetMinimalElapsedTime(TimeSpan elapsedTime)
		{
			if (elapsedTime < TimeSpan.FromSeconds(1.0))
			{
				return elapsedTime.ToString("s\\.fff");
			}
			if (elapsedTime < TimeSpan.FromMinutes(1.0))
			{
				return elapsedTime.ToString("m\\:ss\\.fff");
			}
			if (elapsedTime < TimeSpan.FromHours(1.0))
			{
				return elapsedTime.ToString("h\\:mm\\:ss\\.fff");
			}
			return elapsedTime.ToString("c");
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002AE4 File Offset: 0x00000CE4
		private static void StopTimerWrapper(IAsyncResult asyncResult, Action<ClientAsyncOperation> action)
		{
			ClientAsyncOperation clientAsyncOperation = (ClientAsyncOperation)asyncResult.AsyncState;
			if (clientAsyncOperation != null)
			{
				clientAsyncOperation.StopTimeoutTimer();
				action(clientAsyncOperation);
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002B24 File Offset: 0x00000D24
		private static void BeginGetRequestStreamCallbackEntry(IAsyncResult asyncResult)
		{
			ClientAsyncOperation.StopTimerWrapper(asyncResult, delegate(ClientAsyncOperation operation)
			{
				operation.BeginGetRequestStreamCallback(asyncResult);
			});
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002B6C File Offset: 0x00000D6C
		private static void BeginWriteCallbackEntry(IAsyncResult asyncResult)
		{
			ClientAsyncOperation.StopTimerWrapper(asyncResult, delegate(ClientAsyncOperation operation)
			{
				operation.BeginWriteCallback(asyncResult);
			});
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002BB4 File Offset: 0x00000DB4
		private static void BeginGetResponseCallbackEntry(IAsyncResult asyncResult)
		{
			ClientAsyncOperation.StopTimerWrapper(asyncResult, delegate(ClientAsyncOperation operation)
			{
				operation.BeginGetResponseCallback(asyncResult);
			});
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002BFC File Offset: 0x00000DFC
		private static void BeginReadCallbackEntry(IAsyncResult asyncResult)
		{
			ClientAsyncOperation.StopTimerWrapper(asyncResult, delegate(ClientAsyncOperation operation)
			{
				operation.BeginReadCallback(asyncResult);
			});
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002C30 File Offset: 0x00000E30
		private void ResponseTimeoutCallback(object stateInfo)
		{
			try
			{
				if (this.httpWebRequest != null)
				{
					this.httpWebRequest.Abort();
					ExTraceGlobals.ClientAsyncOperationTracer.TraceInformation<string, IntPtr, string>(34940, 0L, "{0}.AbortOnTimeout; Success; ContextHandle={1}, RequestId={2}", this.RequestType, this.context.ContextHandle, this.requestId);
				}
			}
			catch (Exception ex)
			{
				ExTraceGlobals.ClientAsyncOperationTracer.TraceInformation(51324, 0L, "{0}.AbortOnTimeout; Failed; ContextHandle={1}, RequestId={2}, Exception={3}", new object[]
				{
					this.RequestType,
					this.context.ContextHandle,
					this.requestId,
					ex
				});
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002E5C File Offset: 0x0000105C
		private void BeginGetRequestStreamCallback(IAsyncResult asyncResult)
		{
			this.Execute(delegate
			{
				this.requestStream = this.httpWebRequest.EndGetRequestStream(asyncResult);
				if (this.requestBuffer != null && this.requestData.Count > 0)
				{
					this.StartTimerWrapper(delegate
					{
						this.requestWriteTime = new DateTime?(this.perfDateTime.UtcNow);
						this.requestStream.BeginWrite(this.requestData.Array, this.requestData.Offset, this.requestData.Count, new AsyncCallback(ClientAsyncOperation.BeginWriteCallbackEntry), this);
					});
					return;
				}
				this.requestStream.Close();
				this.requestStream = null;
				this.StartTimerWrapper(delegate
				{
					this.requestSentTime = new DateTime?(this.perfDateTime.UtcNow);
					this.httpWebRequest.BeginGetResponse(new AsyncCallback(ClientAsyncOperation.BeginGetResponseCallbackEntry), this);
				});
			});
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002F40 File Offset: 0x00001140
		private void BeginWriteCallback(IAsyncResult asyncResult)
		{
			this.Execute(delegate
			{
				this.requestStream.EndWrite(asyncResult);
				this.requestStream.Close();
				this.requestStream = null;
				this.StartTimerWrapper(delegate
				{
					this.requestSentTime = new DateTime?(this.perfDateTime.UtcNow);
					this.httpWebRequest.BeginGetResponse(new AsyncCallback(ClientAsyncOperation.BeginGetResponseCallbackEntry), this);
				});
			});
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00003108 File Offset: 0x00001308
		private void BeginGetResponseCallback(IAsyncResult asyncResult)
		{
			this.Execute(delegate
			{
				try
				{
					this.responseStartTime = new DateTime?(this.perfDateTime.UtcNow);
					this.httpWebResponse = (HttpWebResponse)this.httpWebRequest.EndGetResponse(asyncResult);
				}
				catch (WebException ex)
				{
					HttpWebResponse httpWebResponse = ex.Response as HttpWebResponse;
					if (httpWebResponse == null)
					{
						throw;
					}
					this.httpWebResponse = httpWebResponse;
				}
				this.context.Update(this.httpWebResponse);
				HttpStatusCode statusCode = this.GetStatusCode();
				ResponseCode responseCode = ResponseCode.Success;
				if (statusCode == HttpStatusCode.OK)
				{
					responseCode = this.GetResponseCode();
				}
				this.responseParser = new PendingResponseParser(statusCode, responseCode, 1054720, this.perfDateTime);
				this.responseStream = this.httpWebResponse.GetResponseStream();
				this.UpdateTimeoutTimer(this.httpWebResponse);
				this.StartTimerWrapper(delegate
				{
					this.responseStream.BeginRead(this.readResponseBuffer.Array, this.readResponseBuffer.Offset, this.readResponseBuffer.Count, new AsyncCallback(ClientAsyncOperation.BeginReadCallbackEntry), this);
				});
			});
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00003290 File Offset: 0x00001490
		private void BeginReadCallback(IAsyncResult asyncResult)
		{
			this.Execute(delegate
			{
				if (this.responseReadTime == null)
				{
					this.responseReadTime = new DateTime?(this.perfDateTime.UtcNow);
				}
				int num = this.responseStream.EndRead(asyncResult);
				if (num == 0)
				{
					this.responseReceivedTime = new DateTime?(this.perfDateTime.UtcNow);
					this.responseParser.Done();
					this.InvokeCallback();
					return;
				}
				this.responseParser.PutData(new ArraySegment<byte>(this.readResponseBuffer.Array, this.readResponseBuffer.Offset, num));
				this.StartTimerWrapper(delegate
				{
					this.responseStream.BeginRead(this.readResponseBuffer.Array, this.readResponseBuffer.Offset, this.readResponseBuffer.Count, new AsyncCallback(ClientAsyncOperation.BeginReadCallbackEntry), this);
				});
			});
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000032C4 File Offset: 0x000014C4
		private void UpdateTimeoutTimer(HttpWebResponse response)
		{
			string text = response.Headers.Get("X-PendingPeriod");
			int num;
			if (!string.IsNullOrEmpty(text) && int.TryParse(text, out num))
			{
				this.httpRequestTimeout = num * 2;
			}
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000032FD File Offset: 0x000014FD
		private void StartTimeoutTimer()
		{
			if (this.httpRequestTimeoutTimer == null)
			{
				this.httpRequestTimeoutTimer = new Timer(new TimerCallback(this.ResponseTimeoutCallback), null, this.httpRequestTimeout, 0);
				return;
			}
			this.httpRequestTimeoutTimer.Change(this.httpRequestTimeout, 0);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x0000333A File Offset: 0x0000153A
		private void StopTimeoutTimer()
		{
			if (this.httpRequestTimeoutTimer != null)
			{
				this.httpRequestTimeoutTimer.Change(-1, -1);
			}
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00003352 File Offset: 0x00001552
		private HttpStatusCode GetStatusCode()
		{
			if (this.httpWebResponse == null)
			{
				throw new InvalidOperationException("Attempting to retrieve response status with no valid HttpWebResponse object");
			}
			return this.httpWebResponse.StatusCode;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00003374 File Offset: 0x00001574
		private ResponseCode GetResponseCode()
		{
			if (this.httpWebResponse == null)
			{
				throw new InvalidOperationException("Attempting to retrieve response status with no valid HttpWebResponse object");
			}
			string text = this.httpWebResponse.Headers.Get("X-ResponseCode");
			if (string.IsNullOrWhiteSpace(text))
			{
				throw ProtocolException.FromResponseCode((LID)56864, string.Format("Server didn't return {0} header", "X-ResponseCode"), this.GetFailureContext(null), this.httpWebResponse.StatusCode, this.httpWebResponse.StatusDescription, ResponseCode.MissingHeader, null, this.httpWebRequest.Headers, this.httpWebResponse.Headers, null);
			}
			int result;
			if (!int.TryParse(text, out result))
			{
				throw ProtocolException.FromResponseCode((LID)44576, string.Format("Unable to parse an int value from {0} header; found={1}", "X-ResponseCode", text), this.GetFailureContext(null), this.httpWebResponse.StatusCode, this.httpWebResponse.StatusDescription, ResponseCode.InvalidHeader, null, this.httpWebRequest.Headers, this.httpWebResponse.Headers, null);
			}
			return (ResponseCode)result;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00003460 File Offset: 0x00001660
		private bool TryGetExpirationInfo(out TimeSpan expiration)
		{
			expiration = TimeSpan.Zero;
			if (this.httpWebResponse == null)
			{
				throw new InvalidOperationException("Attempting to retrieve response session expiration information with no valid HttpWebResponse object");
			}
			string text = this.httpWebResponse.Headers.Get("X-ExpirationInfo");
			if (string.IsNullOrWhiteSpace(text))
			{
				return false;
			}
			int num;
			if (!int.TryParse(text, out num))
			{
				return false;
			}
			expiration = TimeSpan.FromMilliseconds((double)num);
			return true;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000034C8 File Offset: 0x000016C8
		private string GetFailureContext(string remoteExceptionTrace = null)
		{
			StringBuilder stringBuilder = new StringBuilder();
			this.GetHttpRequest(stringBuilder);
			this.GetHttpResponse(stringBuilder);
			if (remoteExceptionTrace != null)
			{
				stringBuilder.Append("\r\n###### REMOTE-EXCEPTION-INFO ######\r\n\r\n");
				stringBuilder.Append(remoteExceptionTrace);
			}
			if (this.exceptionTime != null)
			{
				string arg = string.Empty;
				if (this.requestStartTime != null)
				{
					TimeSpan elapsedTime = this.exceptionTime.Value - this.requestStartTime.Value;
					arg = string.Format(" [+{0}]", ClientAsyncOperation.GetMinimalElapsedTime(elapsedTime));
				}
				stringBuilder.AppendFormat("\r\n###### EXCEPTION THROWN{0} ######\r\n\r\n", arg);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00003560 File Offset: 0x00001760
		private void GetHttpRequest(StringBuilder stringBuilder)
		{
			if (this.httpWebRequest != null && this.requestStartTime != null)
			{
				DateTime value = this.requestStartTime.Value;
				stringBuilder.AppendFormat("\r\n\r\n###### REQUEST [{0}] ######\r\n\r\n", this.requestStartTime.Value.ToString("o"));
				stringBuilder.AppendFormat("{0} {1} HTTP/1.1\r\n", this.httpWebRequest.Method, this.httpWebRequest.RequestUri.PathAndQuery);
				ClientAsyncOperation.AppendHeaders(stringBuilder, this.httpWebRequest.Headers);
				if (this.requestWriteTime != null)
				{
					TimeSpan elapsedTime = this.requestWriteTime.Value - value;
					stringBuilder.AppendFormat("\r\n--- REQUEST BODY [+{0}] ---\r\n", ClientAsyncOperation.GetMinimalElapsedTime(elapsedTime));
					stringBuilder.AppendFormat("..[BODY SIZE: {0}]\r\n", this.httpWebRequest.ContentLength);
				}
				if (this.requestSentTime != null)
				{
					TimeSpan elapsedTime = this.requestSentTime.Value - value;
					stringBuilder.AppendFormat("\r\n--- REQUEST SENT [+{0}] ---\r\n", ClientAsyncOperation.GetMinimalElapsedTime(elapsedTime));
				}
			}
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00003670 File Offset: 0x00001870
		private void GetHttpResponse(StringBuilder stringBuilder)
		{
			if (this.httpWebResponse != null && this.requestStartTime != null && this.responseStartTime != null)
			{
				DateTime value = this.requestStartTime.Value;
				TimeSpan elapsedTime = this.responseStartTime.Value - value;
				stringBuilder.AppendFormat("\r\n###### RESPONSE [+{0}] ######\r\n\r\n", ClientAsyncOperation.GetMinimalElapsedTime(elapsedTime));
				stringBuilder.AppendFormat("HTTP/1.1 {0} {1}\r\n", (int)this.httpWebResponse.StatusCode, this.httpWebResponse.StatusDescription);
				ClientAsyncOperation.AppendHeaders(stringBuilder, this.httpWebResponse.Headers);
				if (this.responseReadTime != null)
				{
					elapsedTime = this.responseReadTime.Value - value;
					stringBuilder.AppendFormat("\r\n--- RESPONSE BODY [+{0}] ---\r\n", ClientAsyncOperation.GetMinimalElapsedTime(elapsedTime));
				}
				if (this.responseParser != null)
				{
					this.responseParser.AppendParserDiagnosticInformation(stringBuilder);
				}
				if (this.responseReceivedTime != null)
				{
					elapsedTime = this.responseReceivedTime.Value - value;
					stringBuilder.AppendFormat("\r\n--- RESPONSE DONE [+{0}] ---\r\n", ClientAsyncOperation.GetMinimalElapsedTime(elapsedTime));
				}
			}
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00003788 File Offset: 0x00001988
		private void CheckStatusCodeAndThrowOnFailedResponse(BufferReader reader)
		{
			ServiceCode serviceCode = (ServiceCode)reader.PeekUInt32(0L);
			if (serviceCode != ServiceCode.Success)
			{
				MapiHttpFailureResponse mapiHttpFailureResponse = new MapiHttpFailureResponse(reader);
				IEnumerable<AuxiliaryBlock> source = AuxiliaryData.ParseAuxiliaryBuffer(mapiHttpFailureResponse.AuxiliaryBuffer);
				ExceptionTraceAuxiliaryBlock exceptionTraceAuxiliaryBlock = source.OfType<ExceptionTraceAuxiliaryBlock>().FirstOrDefault<ExceptionTraceAuxiliaryBlock>();
				string remoteExceptionTrace = string.Empty;
				if (exceptionTraceAuxiliaryBlock != null)
				{
					remoteExceptionTrace = exceptionTraceAuxiliaryBlock.ExceptionTrace;
				}
				throw ProtocolException.FromServiceCode((LID)56412, string.Format("Server returned ServiceCode.{0} failure.", serviceCode), this.GetFailureContext(remoteExceptionTrace), this.httpWebResponse.StatusCode, this.httpWebResponse.StatusDescription, serviceCode, null, this.httpWebRequest.Headers, this.httpWebResponse.Headers);
			}
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00003828 File Offset: 0x00001A28
		private void Execute(Action action)
		{
			try
			{
				action();
			}
			catch (Exception ex)
			{
				try
				{
					this.exceptionTime = new DateTime?(this.perfDateTime.UtcNow);
					WebException ex2 = ex as WebException;
					if (ex2 != null)
					{
						this.exception = ProtocolException.FromTransportException((LID)60960, "WebException thrown.", this.GetFailureContext(null), (this.httpWebResponse != null) ? this.httpWebResponse.StatusCode : HttpStatusCode.OK, (this.httpWebResponse != null) ? this.httpWebResponse.StatusDescription : string.Empty, ex, (this.httpWebRequest != null) ? this.httpWebRequest.Headers : null, (this.httpWebResponse != null) ? this.httpWebResponse.Headers : null);
					}
					else
					{
						IOException ex3 = ex as IOException;
						if (ex3 != null)
						{
							this.exception = ProtocolException.FromTransportException((LID)50444, "IOException thrown.", this.GetFailureContext(null), (this.httpWebResponse != null) ? this.httpWebResponse.StatusCode : HttpStatusCode.OK, (this.httpWebResponse != null) ? this.httpWebResponse.StatusDescription : string.Empty, ex, (this.httpWebRequest != null) ? this.httpWebRequest.Headers : null, (this.httpWebResponse != null) ? this.httpWebResponse.Headers : null);
						}
						else
						{
							this.exception = ex;
						}
					}
				}
				catch (Exception ex4)
				{
					this.exception = ex4;
				}
				base.InvokeCallback();
			}
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000039C0 File Offset: 0x00001BC0
		private void StartTimerWrapper(Action action)
		{
			this.StartTimeoutTimer();
			try
			{
				action();
			}
			catch
			{
				this.StopTimeoutTimer();
				throw;
			}
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000039F4 File Offset: 0x00001BF4
		private void Cleanup()
		{
			if (this.responseStream != null)
			{
				this.DisposeWithExceptionHandling(this.responseStream);
				this.responseStream = null;
			}
			if (this.httpWebResponse != null)
			{
				this.DisposeWithExceptionHandling(this.httpWebResponse);
				this.httpWebResponse = null;
			}
			if (this.requestStream != null)
			{
				this.DisposeWithExceptionHandling(this.requestStream);
				this.requestStream = null;
			}
			Util.DisposeIfPresent(this.requestBuffer);
			Util.DisposeIfPresent(this.readResponseBuffer);
			Util.DisposeIfPresent(this.responseParser);
			Util.DisposeIfPresent(this.httpRequestTimeoutTimer);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00003A80 File Offset: 0x00001C80
		private void DisposeWithExceptionHandling(IDisposable objToDispose)
		{
			try
			{
				objToDispose.Dispose();
			}
			catch (IOException ex)
			{
				ExTraceGlobals.ClientAsyncOperationTracer.TraceInformation<string>(0, 0L, "IOException occurred: {0}", ex.Message);
			}
			catch (WebException ex2)
			{
				ExTraceGlobals.ClientAsyncOperationTracer.TraceInformation<string>(0, 0L, "WebException occurred: {0}", ex2.Message);
			}
		}

		// Token: 0x04000001 RID: 1
		private static readonly string ClientApplication = string.Format("{0}/{1}", "MapiHttpClient", "15.00.1497.010");

		// Token: 0x04000002 RID: 2
		private readonly ClientSessionContext context;

		// Token: 0x04000003 RID: 3
		private readonly PerfDateTime perfDateTime;

		// Token: 0x04000004 RID: 4
		private HttpWebRequest httpWebRequest;

		// Token: 0x04000005 RID: 5
		private string requestId;

		// Token: 0x04000006 RID: 6
		private WorkBuffer requestBuffer;

		// Token: 0x04000007 RID: 7
		private ArraySegment<byte> requestData;

		// Token: 0x04000008 RID: 8
		private Stream requestStream;

		// Token: 0x04000009 RID: 9
		private HttpWebResponse httpWebResponse;

		// Token: 0x0400000A RID: 10
		private WorkBuffer readResponseBuffer;

		// Token: 0x0400000B RID: 11
		private Stream responseStream;

		// Token: 0x0400000C RID: 12
		private ResponseParser responseParser;

		// Token: 0x0400000D RID: 13
		private int httpRequestTimeout = (int)Constants.HttpRequestTimeout.TotalMilliseconds;

		// Token: 0x0400000E RID: 14
		private Timer httpRequestTimeoutTimer;

		// Token: 0x0400000F RID: 15
		private Exception exception;

		// Token: 0x04000010 RID: 16
		private DateTime? requestStartTime;

		// Token: 0x04000011 RID: 17
		private DateTime? requestWriteTime;

		// Token: 0x04000012 RID: 18
		private DateTime? requestSentTime;

		// Token: 0x04000013 RID: 19
		private DateTime? responseStartTime;

		// Token: 0x04000014 RID: 20
		private DateTime? responseReadTime;

		// Token: 0x04000015 RID: 21
		private DateTime? responseReceivedTime;

		// Token: 0x04000016 RID: 22
		private DateTime? exceptionTime;
	}
}
