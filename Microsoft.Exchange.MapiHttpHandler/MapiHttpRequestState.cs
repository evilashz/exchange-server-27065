using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics.Components.MapiHttpHandler;
using Microsoft.Exchange.RpcClientAccess;

namespace Microsoft.Exchange.MapiHttp
{
	// Token: 0x02000045 RID: 69
	internal class MapiHttpRequestState : IAsyncResult
	{
		// Token: 0x0600027D RID: 637 RVA: 0x0000E649 File Offset: 0x0000C849
		public MapiHttpRequestState(MapiHttpHandler mapiHttpHandler, HttpContextBase context, AsyncCallback asyncCallback, object asyncState)
		{
			this.mapiHttpHandler = mapiHttpHandler;
			this.context = context;
			this.asyncCallback = asyncCallback;
			this.asyncState = asyncState;
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x0600027E RID: 638 RVA: 0x0000E66E File Offset: 0x0000C86E
		public object AsyncState
		{
			get
			{
				return this.asyncState;
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x0600027F RID: 639 RVA: 0x0000E676 File Offset: 0x0000C876
		public WaitHandle AsyncWaitHandle
		{
			get
			{
				return ((IAsyncResult)this.asyncTask).AsyncWaitHandle;
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000280 RID: 640 RVA: 0x0000E683 File Offset: 0x0000C883
		public bool CompletedSynchronously
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000281 RID: 641 RVA: 0x0000E686 File Offset: 0x0000C886
		public bool IsCompleted
		{
			get
			{
				return ((IAsyncResult)this.asyncTask).IsCompleted;
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000282 RID: 642 RVA: 0x0000E694 File Offset: 0x0000C894
		private static string FailureResponseHeadersFormatString
		{
			get
			{
				if (MapiHttpRequestState.failureResponseHeadersFormatString == null)
				{
					MapiHttpRequestState.failureResponseHeadersFormatString = string.Format("{0}\r\n{1}: {2}\r\n{3}: {4}\r\n{5}: {6}\r\n{7}: {8}\r\n{9}: {10}\r\n{11}: {12}\r\n\r\n", new object[]
					{
						"DONE",
						"Content-Type",
						"text/html",
						"X-ResponseCode",
						"{0}",
						"X-FailureLID",
						"{1}",
						"X-FailureDescription",
						"{2}",
						"X-StartTime",
						"{3}",
						"X-ElapsedTime",
						"{4}"
					});
				}
				return MapiHttpRequestState.failureResponseHeadersFormatString;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000283 RID: 643 RVA: 0x0000E734 File Offset: 0x0000C934
		private static string SuccessfulResponseHeadersFormatString
		{
			get
			{
				if (MapiHttpRequestState.successfulResponseHeadersFormatString == null)
				{
					MapiHttpRequestState.successfulResponseHeadersFormatString = string.Format("{0}\r\n{1}: {2}\r\n{3}: {4}\r\n\r\n", new object[]
					{
						"DONE",
						"X-StartTime",
						"{0}",
						"X-ElapsedTime",
						"{1}"
					});
				}
				return MapiHttpRequestState.successfulResponseHeadersFormatString;
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000284 RID: 644 RVA: 0x0000E78C File Offset: 0x0000C98C
		private static string SuccessfulResponseHeadersFormatStringWithProcessing
		{
			get
			{
				if (MapiHttpRequestState.successfulResponseHeadersFormatStringWithProcessing == null)
				{
					MapiHttpRequestState.successfulResponseHeadersFormatStringWithProcessing = string.Format("{0}\r\n{1}\r\n{2}: {3}\r\n{4}: {5}\r\n\r\n", new object[]
					{
						"PROCESSING",
						"DONE",
						"X-StartTime",
						"{0}",
						"X-ElapsedTime",
						"{1}"
					});
				}
				return MapiHttpRequestState.successfulResponseHeadersFormatStringWithProcessing;
			}
		}

		// Token: 0x06000285 RID: 645 RVA: 0x0000E7EC File Offset: 0x0000C9EC
		public void Begin()
		{
			if (this.asyncTask != null)
			{
				throw new InvalidOperationException("Begin can only be called once.");
			}
			this.beginThreadId = new int?(Thread.CurrentThread.ManagedThreadId);
			try
			{
				this.asyncTask = this.ProcessRequestAsync();
				if (this.asyncCallback != null)
				{
					this.asyncTask.ContinueWith(new Action<Task>(this.Complete));
				}
			}
			finally
			{
				this.beginThreadId = null;
			}
		}

		// Token: 0x06000286 RID: 646 RVA: 0x0000E86C File Offset: 0x0000CA6C
		public void End()
		{
			if (this.asyncTask == null)
			{
				throw new InvalidOperationException("Begin must be called before End");
			}
			try
			{
				if (!this.asyncTask.IsCompleted || this.asyncTask.IsFaulted)
				{
					this.asyncTask.GetAwaiter().GetResult();
				}
			}
			finally
			{
				Util.DisposeIfPresent(this.asyncTask);
			}
		}

		// Token: 0x06000287 RID: 647 RVA: 0x0000E8D8 File Offset: 0x0000CAD8
		private void GetRequestTimes(out DateTime startTime, out TimeSpan elapsedTime)
		{
			startTime = this.context.Timestamp.ToUniversalTime();
			DateTime utcNow = DateTime.UtcNow;
			if (startTime < utcNow)
			{
				elapsedTime = utcNow - startTime;
				return;
			}
			elapsedTime = TimeSpan.Zero;
		}

		// Token: 0x06000288 RID: 648 RVA: 0x0000E930 File Offset: 0x0000CB30
		private ArraySegment<byte> BuildFailureResponseHeaders(ResponseCode? responseCode, LID? failureLID, string failureDescription)
		{
			DateTime dateTime;
			TimeSpan timeSpan;
			this.GetRequestTimes(out dateTime, out timeSpan);
			return new ArraySegment<byte>(Encoding.UTF8.GetBytes(string.Format(MapiHttpRequestState.FailureResponseHeadersFormatString, new object[]
			{
				(uint)responseCode.Value,
				(uint)failureLID.Value,
				failureDescription,
				dateTime.ToString("R"),
				(uint)timeSpan.TotalMilliseconds
			})));
		}

		// Token: 0x06000289 RID: 649 RVA: 0x0000E9AC File Offset: 0x0000CBAC
		private ArraySegment<byte> BuildDoneMetaData(bool needProcessingMetaTag)
		{
			DateTime dateTime;
			TimeSpan timeSpan;
			this.GetRequestTimes(out dateTime, out timeSpan);
			return new ArraySegment<byte>(Encoding.UTF8.GetBytes(string.Format(needProcessingMetaTag ? MapiHttpRequestState.SuccessfulResponseHeadersFormatStringWithProcessing : MapiHttpRequestState.SuccessfulResponseHeadersFormatString, dateTime.ToString("R"), (uint)timeSpan.TotalMilliseconds)));
		}

		// Token: 0x0600028A RID: 650 RVA: 0x0000ECE4 File Offset: 0x0000CEE4
		private async Task SendFailureInformationAsync(MapiHttpRequestState.ResponseStreamState state, ResponseCode? responseCode, LID? failureLID, HttpStatusCode? httpStatusCode, string httpStatusDescription, string failureDescription, Exception failureException)
		{
			if (!this.context.Response.IsClientConnected)
			{
				this.context.Response.Close();
			}
			else
			{
				ArraySegment<byte> failureExceptionData = new ArraySegment<byte>(Encoding.UTF8.GetBytes(this.context.HtmlEncode(failureException.ToString())));
				switch (state)
				{
				case MapiHttpRequestState.ResponseStreamState.None:
				case MapiHttpRequestState.ResponseStreamState.Initial:
					this.context.SetFailureResponse(responseCode, failureLID, httpStatusCode, httpStatusDescription, failureDescription);
					await this.context.WriteResponseBuffersAsync(failureExceptionData, null, true);
					goto IL_27F;
				case MapiHttpRequestState.ResponseStreamState.ProcessingStarted:
				case MapiHttpRequestState.ResponseStreamState.ProcessingDone:
				{
					ArraySegment<byte> failureHeaderData = this.BuildFailureResponseHeaders(responseCode, failureLID, failureDescription);
					await this.context.WriteResponseDataAsync(failureHeaderData);
					await this.context.WriteResponseDataAsync(failureExceptionData);
					goto IL_27F;
				}
				case MapiHttpRequestState.ResponseStreamState.ResponseDone:
					goto IL_27F;
				}
				this.context.Response.Close();
				IL_27F:;
			}
		}

		// Token: 0x0600028B RID: 651 RVA: 0x0000EFE4 File Offset: 0x0000D1E4
		private async Task ProcessRequestAsync()
		{
			if (string.Compare(this.context.Request.RequestType, "POST", true) == 0)
			{
				await this.ExecuteRequestAsync();
			}
			else
			{
				if (string.Compare(this.context.Request.RequestType, "GET", true) == 0)
				{
					foreach (string acceptType in this.context.Request.AcceptTypes)
					{
						if (string.Compare(acceptType, "text/html", true) == 0 || string.Compare(acceptType, "text/*", true) == 0 || string.Compare(acceptType, "*/*", true) == 0)
						{
							await MapiHttpStatusPage.ExecuteStatusAsync(this.context, this.mapiHttpHandler.EndpointVdirPath);
							return;
						}
					}
				}
				this.context.Response.StatusCode = 400;
			}
		}

		// Token: 0x0600028C RID: 652 RVA: 0x0000F02C File Offset: 0x0000D22C
		private void LogAndTraceFailure(SessionContext sessionContext, Exception exception)
		{
			string empty;
			if (!this.context.TryGetContextCookie(out empty))
			{
				empty = string.Empty;
			}
			string empty2;
			if (!this.context.TryGetSequenceCookie(out empty2))
			{
				empty2 = string.Empty;
			}
			string empty3;
			if (!this.context.TryGetRequestId(out empty3))
			{
				empty3 = string.Empty;
			}
			string empty4;
			if (!this.context.TryGetCafeActivityId(out empty4))
			{
				empty4 = string.Empty;
			}
			string text;
			if (!this.context.TryGetRequestType(out text))
			{
				text = "Unknown";
			}
			string empty5;
			string empty6;
			if (!this.context.TryGetClientIPEndpoints(out empty5, out empty6))
			{
				empty5 = string.Empty;
				empty6 = string.Empty;
			}
			string text2 = text;
			string text3 = null;
			string text4 = null;
			string text5 = null;
			if (MapiHttpHandler.NeedTokenRehydration(text))
			{
				string text6;
				string text7;
				this.context.TryGetUserIdentityInfo(out text3, out text4, out text6, out text7, out text5);
			}
			else if (sessionContext != null)
			{
				text3 = sessionContext.UserContext.UserName;
				text4 = sessionContext.UserContext.UserPrincipalName;
				text5 = sessionContext.UserContext.Organization;
			}
			text3 = (text3 ?? string.Empty);
			text4 = (text4 ?? string.Empty);
			text5 = (text5 ?? string.Empty);
			if (string.IsNullOrEmpty(text3))
			{
				text3 = text4;
			}
			string text8 = string.Empty;
			if (!string.IsNullOrEmpty(text3) && SmtpAddress.IsValidSmtpAddress(text3))
			{
				SmtpAddress smtpAddress = new SmtpAddress(text3);
				text8 = smtpAddress.Domain;
			}
			else if (!string.IsNullOrEmpty(text4) && SmtpAddress.IsValidSmtpAddress(text4))
			{
				SmtpAddress smtpAddress2 = new SmtpAddress(text4);
				text8 = smtpAddress2.Domain;
			}
			string protocolSequence;
			if (string.IsNullOrEmpty(text4) && string.IsNullOrEmpty(text3))
			{
				protocolSequence = "MapiHttp:" + (text8 ?? string.Empty);
			}
			else
			{
				protocolSequence = string.Format("{0}{1}[{0}{2}]", "MapiHttp:", text8 ?? string.Empty, string.IsNullOrEmpty(text4) ? text3 : text4);
			}
			List<string> list = new List<string>(2);
			if (!string.IsNullOrEmpty(empty3))
			{
				list.Add("R:" + empty3);
			}
			if (!string.IsNullOrEmpty(empty4))
			{
				list.Add("A:" + empty4);
			}
			List<string> list2 = new List<string>(2);
			if (!string.IsNullOrEmpty(empty))
			{
				list2.Add("C:" + empty);
			}
			if (!string.IsNullOrEmpty(empty2))
			{
				list2.Add("S:" + empty2);
			}
			MapiHttpHandler mapiHttpHandler = this.mapiHttpHandler;
			IList<string> requestIds = list;
			IList<string> cookies = list2;
			string message = text2;
			string userName;
			if ((userName = text4) == null)
			{
				userName = (text3 ?? string.Empty);
			}
			mapiHttpHandler.LogFailure(requestIds, cookies, message, userName, protocolSequence, empty5, text5 ?? string.Empty, exception, ExTraceGlobals.AsyncOperationTracer);
		}

		// Token: 0x0600028D RID: 653 RVA: 0x0000F2A4 File Offset: 0x0000D4A4
		private AsyncOperation ValidateOperation()
		{
			if (!this.context.User.Identity.IsAuthenticated)
			{
				throw ProtocolException.FromResponseCode((LID)45824, "Anonymous not allowed", ResponseCode.AnonymousNotAllowed, null);
			}
			if (string.Compare(this.context.Request.RequestType, "POST", true) != 0)
			{
				throw ProtocolException.FromResponseCode((LID)45312, string.Format("The verb {0} is not valid.", this.context.Request.RequestType), ResponseCode.InvalidVerb, null);
			}
			string requestType = this.context.GetRequestType();
			if (string.Compare(requestType, "PING", true) == 0)
			{
				return new PingAsyncOperation(this.context);
			}
			return this.mapiHttpHandler.BuildAsyncOperation(requestType, this.context);
		}

		// Token: 0x0600028E RID: 654 RVA: 0x0000FF60 File Offset: 0x0000E160
		private async Task ExecuteRequestAsync()
		{
			MapiHttpRequestState.ResponseStreamState state = MapiHttpRequestState.ResponseStreamState.None;
			Exception failureException = null;
			AsyncOperation asyncOperation = null;
			WorkBuffer requestBuffer = null;
			WorkBuffer[] responseBuffers = null;
			ResponseCode? responseCode = null;
			LID? failureLID = null;
			try
			{
				try
				{
					bool isOperationCleanedUp = false;
					try
					{
						state = MapiHttpRequestState.ResponseStreamState.Initial;
						if (!this.mapiHttpHandler.TryEnsureHandlerIsInitialized())
						{
							throw ProtocolException.FromResponseCode((LID)60956, "Endpoint is in the process of shutting down.", ResponseCode.EndpointShuttingDown, null);
						}
						asyncOperation = this.ValidateOperation();
						requestBuffer = await this.context.ReadRequestBufferAsync(268288);
						TimeSpan pendingPeriod = this.context.GetPendingPeriod();
						TimeSpan clientResponsePendingPeriod = pendingPeriod + pendingPeriod;
						this.context.SetPendingPeriod(clientResponsePendingPeriod);
						asyncOperation.Prepare();
						try
						{
							asyncOperation.ParseRequest(requestBuffer);
						}
						catch (BufferParseException innerException)
						{
							throw ProtocolException.FromResponseCode((LID)52316, string.Format("{0} request body is not formatted correctly.", asyncOperation.RequestType), ResponseCode.InvalidPayload, innerException);
						}
						this.context.SetContentType(asyncOperation.ContentType);
						this.context.SetResponseCode(ResponseCode.Success);
						this.context.Response.StatusCode = 200;
						responseCode = new ResponseCode?(ResponseCode.Success);
						AsyncOperationInfo asyncOperationInfo = asyncOperation.AsyncOperationInfo;
						TimeSpan timerInterval = TimeSpan.FromTicks(Math.Min(asyncOperation.InitialFlushPeriod.Ticks, pendingPeriod.Ticks));
						Task asyncOperationTask = asyncOperation.ExecuteAsync();
						try
						{
							ArraySegment<byte> marker = MapiHttpRequestState.ProcessingMarker;
							while (!asyncOperationTask.IsCompleted && this.context.Response.IsClientConnected)
							{
								if (timerInterval != TimeSpan.Zero)
								{
									Task delayTask = DelayTimer.Instance.GetTimerTask(timerInterval);
									try
									{
										await Task.WhenAny(new Task[]
										{
											asyncOperationTask,
											delayTask
										});
									}
									catch (OperationCanceledException)
									{
									}
								}
								timerInterval = pendingPeriod;
								if (asyncOperationTask.IsCompleted || !this.context.Response.IsClientConnected)
								{
									break;
								}
								if (state == MapiHttpRequestState.ResponseStreamState.ProcessingStarted)
								{
									marker = MapiHttpRequestState.PendingMarker;
									if (asyncOperationInfo != null)
									{
										asyncOperationInfo.OnPendingSent();
									}
								}
								else
								{
									this.context.Response.BufferOutput = false;
									state = MapiHttpRequestState.ResponseStreamState.ProcessingStarted;
								}
								await this.context.WriteResponseDataAsync(marker);
								await Task.Factory.FromAsync(new Func<AsyncCallback, object, IAsyncResult>(this.context.Response.BeginFlush), new Action<IAsyncResult>(this.context.Response.EndFlush), null);
							}
						}
						catch (Exception ex)
						{
							failureException = ex;
						}
						switch (asyncOperationTask.Status)
						{
						case TaskStatus.RanToCompletion:
						case TaskStatus.Canceled:
							break;
						case TaskStatus.Faulted:
							if (failureException == null)
							{
								failureException = asyncOperationTask.Exception;
							}
							break;
						default:
							if (!this.context.Response.IsClientConnected)
							{
								MapiHttpHandler.QueueDroppedConnection(asyncOperation.DroppedConnectionContextHandle);
							}
							try
							{
								await asyncOperationTask;
							}
							catch (Exception ex2)
							{
								if (failureException == null)
								{
									failureException = ex2;
								}
							}
							break;
						}
						Util.DisposeIfPresent(asyncOperationTask);
						if (failureException == null && !this.context.Response.IsClientConnected)
						{
							failureException = ProtocolException.FromResponseCode((LID)63520, "Client has dropped connection", ResponseCode.NoClient, null);
						}
						if (failureException == null)
						{
							try
							{
								asyncOperation.SerializeResponse(out responseBuffers);
							}
							catch (BufferParseException innerException2)
							{
								throw ProtocolException.FromResponseCode((LID)46172, string.Format("{0} response body is not able to be serialized.", asyncOperation.RequestType), ResponseCode.InvalidResponse, innerException2);
							}
							asyncOperation.Cleanup();
							isOperationCleanedUp = true;
							bool responseNotStarted = state == MapiHttpRequestState.ResponseStreamState.Initial;
							ArraySegment<byte> doneMetaData = this.BuildDoneMetaData(responseNotStarted);
							state = MapiHttpRequestState.ResponseStreamState.ResponseStarted;
							await this.context.WriteResponseBuffersAsync(doneMetaData, responseBuffers, responseNotStarted);
							state = MapiHttpRequestState.ResponseStreamState.ResponseDone;
						}
					}
					catch (Exception ex3)
					{
						failureException = ex3;
					}
					finally
					{
						if (!isOperationCleanedUp && asyncOperation != null)
						{
							asyncOperation.Cleanup();
						}
						responseBuffers.DisposeIfPresent();
						responseBuffers = null;
						Util.DisposeIfPresent(requestBuffer);
						requestBuffer = null;
					}
					if (failureException != null)
					{
						this.LogAndTraceFailure((asyncOperation != null) ? asyncOperation.SessionContext : null, failureException);
						ResponseCode? localResponseCode = null;
						LID? localFailureLID = null;
						HttpStatusCode? localHttpStatusCode = null;
						string localHttpStatusDescription = null;
						string localFailureDescription = null;
						if (!failureException.TryGetFailureInformation(out localResponseCode, out localFailureLID, out localHttpStatusCode, out localHttpStatusDescription, out localFailureDescription))
						{
							localResponseCode = null;
							localFailureLID = null;
							localHttpStatusCode = null;
							localHttpStatusDescription = null;
							localFailureDescription = null;
						}
						if (localResponseCode != null)
						{
							responseCode = new ResponseCode?(localResponseCode.Value);
						}
						if (localFailureLID != null)
						{
							failureLID = new LID?(localFailureLID.Value);
						}
						if (this.context.Response.IsClientConnected)
						{
							try
							{
								await this.SendFailureInformationAsync(state, localResponseCode, localFailureLID, localHttpStatusCode, localHttpStatusDescription, localFailureDescription, failureException);
							}
							catch (Exception exception)
							{
								this.LogAndTraceFailure((asyncOperation != null) ? asyncOperation.SessionContext : null, exception);
								throw;
							}
						}
					}
				}
				finally
				{
					this.context.AppendLogResponseInfo(responseCode, failureLID, asyncOperation);
					this.context.AppendLogExceptionInfo(failureException);
					if (asyncOperation != null)
					{
						asyncOperation.OnComplete(failureException);
					}
				}
			}
			finally
			{
				Util.DisposeIfPresent(asyncOperation);
			}
		}

		// Token: 0x0600028F RID: 655 RVA: 0x0000FFB4 File Offset: 0x0000E1B4
		private void Complete(Task unusedTask)
		{
			if (this.asyncCallback != null)
			{
				int? num = this.beginThreadId;
				if (num == null || num.Value != Thread.CurrentThread.ManagedThreadId)
				{
					this.asyncCallback(this);
					return;
				}
				if (!ThreadPool.QueueUserWorkItem(delegate(object state)
				{
					this.asyncCallback(this);
				}))
				{
					this.asyncCallback(this);
				}
			}
		}

		// Token: 0x0400010F RID: 271
		private static readonly ArraySegment<byte> ProcessingMarker = new ArraySegment<byte>(Constants.ProcessingMarker);

		// Token: 0x04000110 RID: 272
		private static readonly ArraySegment<byte> PendingMarker = new ArraySegment<byte>(Constants.PendingMarker);

		// Token: 0x04000111 RID: 273
		private static readonly ArraySegment<byte> DoneMarker = new ArraySegment<byte>(Constants.DoneMarker);

		// Token: 0x04000112 RID: 274
		private static readonly ArraySegment<byte> DoneMarkerNoEmptyLine = new ArraySegment<byte>(Constants.DoneMarkerNoEmptyLine);

		// Token: 0x04000113 RID: 275
		private static string failureResponseHeadersFormatString = null;

		// Token: 0x04000114 RID: 276
		private static string successfulResponseHeadersFormatString = null;

		// Token: 0x04000115 RID: 277
		private static string successfulResponseHeadersFormatStringWithProcessing = null;

		// Token: 0x04000116 RID: 278
		private readonly MapiHttpHandler mapiHttpHandler;

		// Token: 0x04000117 RID: 279
		private readonly HttpContextBase context;

		// Token: 0x04000118 RID: 280
		private readonly AsyncCallback asyncCallback;

		// Token: 0x04000119 RID: 281
		private readonly object asyncState;

		// Token: 0x0400011A RID: 282
		private Task asyncTask;

		// Token: 0x0400011B RID: 283
		private int? beginThreadId;

		// Token: 0x02000046 RID: 70
		private enum ResponseStreamState
		{
			// Token: 0x0400011D RID: 285
			None,
			// Token: 0x0400011E RID: 286
			Initial,
			// Token: 0x0400011F RID: 287
			ProcessingStarted,
			// Token: 0x04000120 RID: 288
			ProcessingDone,
			// Token: 0x04000121 RID: 289
			ResponseStarted,
			// Token: 0x04000122 RID: 290
			ResponseDone
		}
	}
}
