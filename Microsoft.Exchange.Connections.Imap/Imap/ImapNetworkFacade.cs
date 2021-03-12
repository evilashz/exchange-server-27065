using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net.Sockets;
using Microsoft.Exchange.Connections.Common;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ContentAggregation;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Security;

namespace Microsoft.Exchange.Connections.Imap
{
	// Token: 0x02000011 RID: 17
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class ImapNetworkFacade : DisposeTrackableBase, INetworkFacade, IDisposeTrackable, IDisposable
	{
		// Token: 0x06000127 RID: 295 RVA: 0x00006A38 File Offset: 0x00004C38
		internal ImapNetworkFacade(ConnectionParameters connectionParameters, ImapServerParameters serverParameters)
		{
			this.connectionParameters = connectionParameters;
			this.serverParameters = serverParameters;
			this.Log = connectionParameters.Log;
			this.currentResponse = new ImapResponse(this.Log);
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000128 RID: 296 RVA: 0x00006A8C File Offset: 0x00004C8C
		public long TotalBytesSent
		{
			get
			{
				base.CheckDisposed();
				return this.totalBytesSent;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000129 RID: 297 RVA: 0x00006A9A File Offset: 0x00004C9A
		public long TotalBytesReceived
		{
			get
			{
				base.CheckDisposed();
				return this.totalBytesReceived;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600012A RID: 298 RVA: 0x00006AA8 File Offset: 0x00004CA8
		public ulong TotalMessagesReceived
		{
			get
			{
				base.CheckDisposed();
				return this.totalMessagesReceived;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x0600012B RID: 299 RVA: 0x00006AB6 File Offset: 0x00004CB6
		public bool IsConnected
		{
			get
			{
				base.CheckDisposed();
				return this.networkConnection != null && !this.isNetworkConnectionShutdown;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x0600012C RID: 300 RVA: 0x00006AD1 File Offset: 0x00004CD1
		public string Server
		{
			get
			{
				base.CheckDisposed();
				return this.serverParameters.Server;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x0600012D RID: 301 RVA: 0x00006AE4 File Offset: 0x00004CE4
		// (set) Token: 0x0600012E RID: 302 RVA: 0x00006AEC File Offset: 0x00004CEC
		private ILog Log { get; set; }

		// Token: 0x0600012F RID: 303 RVA: 0x00006AF8 File Offset: 0x00004CF8
		public IAsyncResult BeginConnect(ImapConnectionContext imapConnectionContext, AsyncCallback callback, object callbackState)
		{
			base.CheckDisposed();
			this.ResetStateForConnectIfNecessary();
			this.socket = SocketFactory.CreateTcpStreamSocket();
			AsyncResult<ImapConnectionContext, ImapResultData> asyncResult = new AsyncResult<ImapConnectionContext, ImapResultData>(this, imapConnectionContext, callback, callbackState);
			try
			{
				asyncResult.PendingAsyncResult = this.socket.BeginConnect(this.serverParameters.Server, this.serverParameters.Port, new AsyncCallback(this.OnEndConnectInternalReadResponse), asyncResult);
			}
			catch (SocketException errorCode)
			{
				this.socket = null;
				ImapNetworkFacade.HandleError(errorCode, asyncResult);
			}
			return asyncResult;
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00006B80 File Offset: 0x00004D80
		public AsyncOperationResult<ImapResultData> EndConnect(IAsyncResult asyncResult)
		{
			base.CheckDisposed();
			AsyncResult<ImapConnectionContext, ImapResultData> asyncResult2 = (AsyncResult<ImapConnectionContext, ImapResultData>)asyncResult;
			return asyncResult2.WaitForCompletion();
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00006BA0 File Offset: 0x00004DA0
		public IAsyncResult BeginNegotiateTlsAsClient(ImapConnectionContext imapConnectionContext, AsyncCallback callback, object callbackState)
		{
			base.CheckDisposed();
			AsyncResult<ImapConnectionContext, ImapResultData> asyncResult = new AsyncResult<ImapConnectionContext, ImapResultData>(this, imapConnectionContext, callback, callbackState);
			asyncResult.PendingAsyncResult = this.networkConnection.BeginNegotiateTlsAsClient(null, this.networkConnection.RemoteEndPoint.Address.ToString(), new AsyncCallback(this.OnEndConnectNegotiateTlsAsClientInternalReadResponse), asyncResult);
			return asyncResult;
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00006BF4 File Offset: 0x00004DF4
		public AsyncOperationResult<ImapResultData> EndNegotiateTlsAsClient(IAsyncResult asyncResult)
		{
			base.CheckDisposed();
			AsyncResult<ImapConnectionContext, ImapResultData> asyncResult2 = (AsyncResult<ImapConnectionContext, ImapResultData>)asyncResult;
			return asyncResult2.WaitForCompletion();
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00006C14 File Offset: 0x00004E14
		public IAsyncResult BeginCommand(ImapCommand command, ImapConnectionContext imapConnectionContext, AsyncCallback callback, object callbackState)
		{
			return this.BeginCommand(command, true, imapConnectionContext, callback, callbackState);
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00006C24 File Offset: 0x00004E24
		public IAsyncResult BeginCommand(ImapCommand command, bool processResponse, ImapConnectionContext imapConnectionContext, AsyncCallback callback, object callbackState)
		{
			base.CheckDisposed();
			byte[] array = command.ToBytes();
			this.currentResponse.Reset(command);
			this.currentCommand = command;
			AsyncResult<ImapConnectionContext, ImapResultData> asyncResult = new AsyncResult<ImapConnectionContext, ImapResultData>(this, imapConnectionContext, callback, callbackState);
			asyncResult.State.TimeSent = ExDateTime.MinValue;
			if (this.cancellationRequested)
			{
				asyncResult.SetCompletedSynchronously();
				this.HandleCancellation(asyncResult);
				return asyncResult;
			}
			if (this.isNetworkConnectionShutdown)
			{
				asyncResult.SetCompletedSynchronously();
				ImapNetworkFacade.HandleError(ImapNetworkFacade.GetConnectionClosedException(), asyncResult);
				return asyncResult;
			}
			this.totalBytesSent += (long)array.Length;
			if (this.totalBytesSent > this.connectionParameters.MaxBytesToTransfer)
			{
				asyncResult.SetCompletedSynchronously();
				ImapNetworkFacade.HandleError(ImapNetworkFacade.MaxBytesSentExceeded(), asyncResult);
				return asyncResult;
			}
			if (processResponse && this.totalBytesReceived > this.connectionParameters.MaxBytesToTransfer)
			{
				asyncResult.State.Log.Debug("Not sending {0}, since we've exceeded our received-bytes threshold.", new object[]
				{
					this.currentCommand.ToPiiCleanString()
				});
				asyncResult.SetCompletedSynchronously();
				ImapNetworkFacade.HandleError(ImapNetworkFacade.MaxBytesReceivedExceeded(), asyncResult);
				return asyncResult;
			}
			asyncResult.State.Log.Info("IMAP Send command: [{0}]", new object[]
			{
				this.currentCommand.ToPiiCleanString()
			});
			DownloadCompleteEventArgs eventArgs = new DownloadCompleteEventArgs(0L, (long)array.Length);
			imapConnectionContext.ActivatePerfDownloadEvent(imapConnectionContext, eventArgs);
			if (processResponse)
			{
				asyncResult.PendingAsyncResult = this.networkConnection.BeginWrite(array, 0, array.Length, new AsyncCallback(this.OnEndWriteCommandOrLiteralBeginReadResponse), asyncResult);
			}
			else
			{
				asyncResult.PendingAsyncResult = this.networkConnection.BeginWrite(array, 0, array.Length, new AsyncCallback(this.OnEndSendCommandIgnoreResponse), asyncResult);
				asyncResult.SetCompletedSynchronously();
				this.currentResultData.Clear();
				asyncResult.ProcessCompleted(this.currentResultData);
			}
			return asyncResult;
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00006DD8 File Offset: 0x00004FD8
		public AsyncOperationResult<ImapResultData> EndCommand(IAsyncResult asyncResult)
		{
			base.CheckDisposed();
			AsyncResult<ImapConnectionContext, ImapResultData> asyncResult2 = (AsyncResult<ImapConnectionContext, ImapResultData>)asyncResult;
			return asyncResult2.WaitForCompletion();
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00006DF8 File Offset: 0x00004FF8
		public void Cancel()
		{
			lock (this.syncRoot)
			{
				base.CheckDisposed();
				if (!this.cancellationRequested)
				{
					this.cancellationRequested = true;
					if (this.networkConnection != null)
					{
						this.networkConnection.Shutdown();
					}
				}
			}
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00006E5C File Offset: 0x0000505C
		public override void Dispose()
		{
			lock (this.syncRoot)
			{
				base.Dispose();
			}
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00006E9C File Offset: 0x0000509C
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				this.CloseConnections();
			}
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00006EA7 File Offset: 0x000050A7
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<ImapNetworkFacade>(this);
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00006EAF File Offset: 0x000050AF
		private static Exception BuildTimeoutException()
		{
			Exception result;
			if ((result = ImapNetworkFacade.timeoutException) == null)
			{
				result = (ImapNetworkFacade.timeoutException = new ImapConnectionException(CXStrings.ImapServerTimeout, RetryPolicy.Backoff));
			}
			return result;
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00006ED0 File Offset: 0x000050D0
		private static Exception BuildConnectionShutdownException()
		{
			Exception result;
			if ((result = ImapNetworkFacade.connectionShutdownException) == null)
			{
				result = (ImapNetworkFacade.connectionShutdownException = new ImapConnectionException(CXStrings.ImapServerShutdown, RetryPolicy.Backoff));
			}
			return result;
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00006EF1 File Offset: 0x000050F1
		private static Exception BuildUnknownNetworkException()
		{
			Exception result;
			if ((result = ImapNetworkFacade.unknownFailureException) == null)
			{
				result = (ImapNetworkFacade.unknownFailureException = new ImapCommunicationException(CXStrings.ImapServerNetworkError, RetryPolicy.Backoff));
			}
			return result;
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00006F12 File Offset: 0x00005112
		private static Exception GetConnectionClosedException()
		{
			Exception result;
			if ((result = ImapNetworkFacade.connectionClosedException) == null)
			{
				result = (ImapNetworkFacade.connectionClosedException = new ImapConnectionClosedException(CXStrings.ImapServerConnectionClosed));
			}
			return result;
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00006F32 File Offset: 0x00005132
		private static Exception MaxBytesSentExceeded()
		{
			Exception result;
			if ((result = ImapNetworkFacade.maxBytesSentException) == null)
			{
				result = (ImapNetworkFacade.maxBytesSentException = new ItemLimitExceededException(CXStrings.ImapMaxBytesSentExceeded));
			}
			return result;
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00006F52 File Offset: 0x00005152
		private static Exception MaxBytesReceivedExceeded()
		{
			Exception result;
			if ((result = ImapNetworkFacade.maxBytesReceivedException) == null)
			{
				result = (ImapNetworkFacade.maxBytesReceivedException = new ItemLimitExceededException(CXStrings.ImapMaxBytesReceivedExceeded));
			}
			return result;
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00006F74 File Offset: 0x00005174
		private static void CountCommand(AsyncResult<ImapConnectionContext, ImapResultData> curOp, bool successful)
		{
			string server = ((ImapNetworkFacade)curOp.State.NetworkFacade).serverParameters.Server;
			if (curOp.State.TimeSent != ExDateTime.MinValue)
			{
				curOp.State.NotifyRoundtripComplete(null, new RoundtripCompleteEventArgs(ExDateTime.UtcNow - curOp.State.TimeSent, successful, server));
				curOp.State.TimeSent = ExDateTime.MinValue;
			}
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00006FEC File Offset: 0x000051EC
		private static void HandleError(object errorCode, AsyncResult<ImapConnectionContext, ImapResultData> curOp)
		{
			ImapNetworkFacade.CountCommand(curOp, false);
			if (errorCode is ConnectionsPermanentException || errorCode is ConnectionsTransientException)
			{
				curOp.State.Log.Error("Exception while communicating: {0}.", new object[]
				{
					errorCode
				});
				curOp.ProcessCompleted((Exception)errorCode);
				return;
			}
			ImapNetworkFacade imapNetworkFacade = (ImapNetworkFacade)curOp.State.NetworkFacade;
			if (imapNetworkFacade.networkConnection != null)
			{
				imapNetworkFacade.networkConnection.Shutdown();
				imapNetworkFacade.isNetworkConnectionShutdown = true;
			}
			if (errorCode is SocketError)
			{
				switch ((SocketError)errorCode)
				{
				case SocketError.Shutdown:
					curOp.State.Log.Error("Network connection shut down: {0}.", new object[]
					{
						errorCode
					});
					curOp.ProcessCompleted(ImapNetworkFacade.BuildConnectionShutdownException());
					return;
				case SocketError.TimedOut:
					curOp.State.Log.Error("Operation timed out, shutting down network connection: {0}.", new object[]
					{
						errorCode
					});
					curOp.ProcessCompleted(ImapNetworkFacade.BuildTimeoutException());
					return;
				}
				curOp.State.Log.Error("HandleError unhandled SocketError={0}, shutting down network connection.", new object[]
				{
					((SocketError)errorCode).ToString()
				});
				curOp.ProcessCompleted(ImapNetworkFacade.BuildUnknownNetworkException());
				return;
			}
			if (errorCode is SecurityStatus)
			{
				curOp.State.Log.Error("ImapNetworkFacade.HandleError (SecurityStatus={0}), shutting down network connection.", new object[]
				{
					errorCode
				});
				LocalizedString value = CXStrings.TlsFailureOccurredError(((SecurityStatus)errorCode).ToString());
				TlsFailureException innerException = new TlsFailureException(value);
				curOp.ProcessCompleted(new ImapConnectionException(CXStrings.ImapSecurityStatusError, RetryPolicy.Backoff, innerException));
				return;
			}
			curOp.State.Log.Error("HandleError unknown error type (error={0}), shutting down network connection.", new object[]
			{
				errorCode
			});
			curOp.ProcessCompleted(ImapNetworkFacade.BuildUnknownNetworkException());
		}

		// Token: 0x06000142 RID: 322 RVA: 0x000071CC File Offset: 0x000053CC
		private IAsyncResult BeginNetworkRead(AsyncResult<ImapConnectionContext, ImapResultData> curOp, AsyncCallback asyncCallback)
		{
			IAsyncResult result;
			try
			{
				result = this.networkConnection.BeginRead(asyncCallback, curOp);
			}
			catch (InvalidOperationException ex)
			{
				string text = string.Format("BUG: BeginNetworkRead : BeginRead should never throw InvalidOperationException.  Happened during {0}.", this.currentCommand.ToPiiCleanString());
				this.Log.Fatal(ex, text);
				curOp.ProcessCompleted(new ImapCommunicationException(text, RetryPolicy.Backoff, ex));
				result = null;
			}
			return result;
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00007230 File Offset: 0x00005430
		private void CloseConnections()
		{
			if (this.networkConnection != null)
			{
				this.networkConnection.Dispose();
				this.networkConnection = null;
			}
			if (this.socket != null && this.socket.Connected)
			{
				this.socket.Close();
			}
			this.socket = null;
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00007280 File Offset: 0x00005480
		private void OnEndConnectInternalReadResponse(IAsyncResult asyncResult)
		{
			AsyncResult<ImapConnectionContext, ImapResultData> asyncResult2 = null;
			ImapConnectionContext imapConnectionContext = null;
			lock (this.syncRoot)
			{
				if (!this.ShouldCancelCallback())
				{
					base.CheckDisposed();
					asyncResult2 = (AsyncResult<ImapConnectionContext, ImapResultData>)asyncResult.AsyncState;
					imapConnectionContext = asyncResult2.State;
					try
					{
						Socket socket = this.socket;
						socket.EndConnect(asyncResult);
						if (this.cancellationRequested)
						{
							this.HandleCancellation(asyncResult2);
							this.socket = null;
							return;
						}
						this.networkConnection = new NetworkConnection(this.socket, 4096);
						this.socket = null;
						this.networkConnection.Timeout = (this.connectionParameters.Timeout + 999) / 1000;
						this.Log.Debug("Connection Completed. Connection ID : {0}, Remote End Point {1}", new object[]
						{
							this.networkConnection.ConnectionId,
							this.networkConnection.RemoteEndPoint
						});
					}
					catch (SocketException ex)
					{
						ImapUtilities.LogExceptionDetails(this.Log, "Failed to connect, SocketException", ex);
						this.socket = null;
						asyncResult2.ProcessCompleted(new ImapConnectionException(CXStrings.ImapSocketException, RetryPolicy.Backoff, ex));
						return;
					}
					this.Log.Info(string.Format(CultureInfo.InvariantCulture, "Connect, from:{0} to:{1}", new object[]
					{
						this.serverParameters.Server,
						this.networkConnection.RemoteEndPoint
					}), new object[0]);
					switch (imapConnectionContext.ImapSecurityMechanism)
					{
					case ImapSecurityMechanism.None:
					case ImapSecurityMechanism.Tls:
						asyncResult2.PendingAsyncResult = this.BeginNetworkRead(asyncResult2, new AsyncCallback(this.OnReadAndDiscardLine));
						break;
					case ImapSecurityMechanism.Ssl:
						asyncResult2.PendingAsyncResult = this.networkConnection.BeginNegotiateTlsAsClient(null, this.networkConnection.RemoteEndPoint.Address.ToString(), new AsyncCallback(this.OnEndConnectNegotiateTlsAsClientInternalReadResponse), asyncResult2);
						break;
					default:
						throw new InvalidOperationException("Unexpected security mechanism " + imapConnectionContext.ImapSecurityMechanism);
					}
				}
			}
		}

		// Token: 0x06000145 RID: 325 RVA: 0x000074BC File Offset: 0x000056BC
		private void OnEndSendCommandIgnoreResponse(IAsyncResult asyncResult)
		{
			NetworkConnection networkConnection = this.ExtractNetworkConnectionFrom(asyncResult);
			object obj;
			networkConnection.EndWrite(asyncResult, out obj);
		}

		// Token: 0x06000146 RID: 326 RVA: 0x000074DC File Offset: 0x000056DC
		private void OnEndConnectNegotiateTlsAsClientInternalReadResponse(IAsyncResult asyncResult)
		{
			lock (this.syncRoot)
			{
				if (this.ShouldCancelCallback())
				{
					NetworkConnection networkConnection = this.ExtractNetworkConnectionFrom(asyncResult);
					object obj2;
					networkConnection.EndNegotiateTlsAsClient(asyncResult, out obj2);
				}
				else
				{
					base.CheckDisposed();
					AsyncResult<ImapConnectionContext, ImapResultData> asyncResult2 = (AsyncResult<ImapConnectionContext, ImapResultData>)asyncResult.AsyncState;
					ImapConnectionContext state = asyncResult2.State;
					object obj2;
					this.networkConnection.EndNegotiateTlsAsClient(asyncResult, out obj2);
					if (obj2 != null)
					{
						ImapNetworkFacade.HandleError(obj2, asyncResult2);
					}
					else if (this.cancellationRequested)
					{
						this.HandleCancellation(asyncResult2);
					}
					else
					{
						ExTraceGlobals.FaultInjectionTracer.TraceTest(3211144509U);
						switch (state.ImapSecurityMechanism)
						{
						case ImapSecurityMechanism.Ssl:
							this.currentResponse.Reset(null);
							asyncResult2.PendingAsyncResult = this.BeginNetworkRead(asyncResult2, new AsyncCallback(this.OnReadAndDiscardLine));
							break;
						case ImapSecurityMechanism.Tls:
							asyncResult2.ProcessCompleted();
							break;
						default:
							throw new InvalidOperationException("Invalid security mechanism");
						}
					}
				}
			}
		}

		// Token: 0x06000147 RID: 327 RVA: 0x000075E4 File Offset: 0x000057E4
		private void OnReadAndDiscardLine(IAsyncResult asyncResult)
		{
			lock (this.syncRoot)
			{
				if (this.ShouldCancelCallback())
				{
					NetworkConnection networkConnection = this.ExtractNetworkConnectionFrom(asyncResult);
					byte[] data;
					int offset;
					int num;
					object obj2;
					networkConnection.EndRead(asyncResult, out data, out offset, out num, out obj2);
				}
				else
				{
					base.CheckDisposed();
					AsyncResult<ImapConnectionContext, ImapResultData> asyncResult2 = (AsyncResult<ImapConnectionContext, ImapResultData>)asyncResult.AsyncState;
					ImapResponse imapResponse = this.currentResponse;
					byte[] data;
					int offset;
					int num;
					object obj2;
					this.networkConnection.EndRead(asyncResult, out data, out offset, out num, out obj2);
					if (obj2 != null)
					{
						ImapNetworkFacade.HandleError(obj2, asyncResult2);
					}
					else if (this.cancellationRequested)
					{
						this.HandleCancellation(asyncResult2);
					}
					else
					{
						ExTraceGlobals.FaultInjectionTracer.TraceTest(4284886333U);
						this.totalBytesReceived += (long)num;
						DownloadCompleteEventArgs eventArgs = new DownloadCompleteEventArgs((long)num, 0L);
						asyncResult2.State.ActivatePerfDownloadEvent(asyncResult2.State, eventArgs);
						if (this.totalBytesReceived > this.connectionParameters.MaxBytesToTransfer)
						{
							ImapNetworkFacade.HandleError(ImapNetworkFacade.MaxBytesReceivedExceeded(), asyncResult2);
						}
						else
						{
							int num2 = imapResponse.AddData(data, offset, num);
							int num3 = num - num2;
							this.Log.Assert(num3 >= 0, "The unconsumed bytes must be non-negative.", new object[0]);
							if (num3 > 0)
							{
								this.networkConnection.PutBackReceivedBytes(num3);
							}
							if (imapResponse.IsComplete)
							{
								this.currentResultData.Clear();
								this.currentResultData.Status = imapResponse.Status;
								if (asyncResult.CompletedSynchronously)
								{
									asyncResult2.SetCompletedSynchronously();
								}
								asyncResult2.State.Log.Assert(this.currentCommand == null, "this.currentCommand is expected to be null", new object[0]);
								if (imapResponse.Status == ImapStatus.No || imapResponse.Status == ImapStatus.Bad || imapResponse.Status == ImapStatus.Bye)
								{
									this.LogFailureDetails("Connecting", imapResponse);
									asyncResult2.ProcessCompleted(this.currentResultData);
								}
								else
								{
									asyncResult2.State.Log.Assert(imapResponse.ResponseLines != null && imapResponse.ResponseLines.Count > 0, "ResponseLines was null or had no content", new object[0]);
									if (imapResponse.Status != ImapStatus.Ok)
									{
										asyncResult2.ProcessCompleted(this.BuildAndLogUnknownCommandFailureException(asyncResult2.State, "Connecting"));
									}
									else
									{
										asyncResult2.ProcessCompleted(this.currentResultData);
									}
								}
							}
							else
							{
								asyncResult2.PendingAsyncResult = this.BeginNetworkRead(asyncResult2, new AsyncCallback(this.OnReadAndDiscardLine));
							}
						}
					}
				}
			}
		}

		// Token: 0x06000148 RID: 328 RVA: 0x0000786C File Offset: 0x00005A6C
		private void OnEndWriteLiteral(IAsyncResult asyncResult)
		{
			lock (this.syncRoot)
			{
				if (this.ShouldCancelCallback())
				{
					NetworkConnection networkConnection = this.ExtractNetworkConnectionFrom(asyncResult);
					object obj2;
					networkConnection.EndWrite(asyncResult, out obj2);
				}
				else
				{
					base.CheckDisposed();
					AsyncResult<ImapConnectionContext, ImapResultData> asyncResult2 = (AsyncResult<ImapConnectionContext, ImapResultData>)asyncResult.AsyncState;
					object obj2;
					this.networkConnection.EndWrite(asyncResult, out obj2);
					if (obj2 != null)
					{
						ImapNetworkFacade.HandleError(obj2, asyncResult2);
					}
					else if (this.cancellationRequested)
					{
						this.HandleCancellation(asyncResult2);
					}
					else
					{
						ExTraceGlobals.FaultInjectionTracer.TraceTest(3748015421U);
						this.totalBytesSent += (long)ImapNetworkFacade.BytesCrLf.Length;
						DownloadCompleteEventArgs eventArgs = new DownloadCompleteEventArgs(0L, (long)ImapNetworkFacade.BytesCrLf.Length);
						asyncResult2.State.ActivatePerfDownloadEvent(asyncResult2.State, eventArgs);
						asyncResult2.State.Log.Debug("Literal sent, sending CRLF to complete it.", new object[0]);
						asyncResult2.PendingAsyncResult = this.networkConnection.BeginWrite(ImapNetworkFacade.BytesCrLf, 0, ImapNetworkFacade.BytesCrLf.Length, new AsyncCallback(this.OnEndWriteCommandOrLiteralBeginReadResponse), asyncResult2);
					}
				}
			}
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00007998 File Offset: 0x00005B98
		private void OnEndWriteCommandOrLiteralBeginReadResponse(IAsyncResult asyncResult)
		{
			lock (this.syncRoot)
			{
				if (this.ShouldCancelCallback())
				{
					NetworkConnection networkConnection = this.ExtractNetworkConnectionFrom(asyncResult);
					object obj2;
					networkConnection.EndWrite(asyncResult, out obj2);
				}
				else
				{
					base.CheckDisposed();
					AsyncResult<ImapConnectionContext, ImapResultData> asyncResult2 = (AsyncResult<ImapConnectionContext, ImapResultData>)asyncResult.AsyncState;
					object obj2;
					this.networkConnection.EndWrite(asyncResult, out obj2);
					if (obj2 != null)
					{
						ImapNetworkFacade.HandleError(obj2, asyncResult2);
					}
					else if (this.cancellationRequested)
					{
						this.HandleCancellation(asyncResult2);
					}
					else
					{
						ExTraceGlobals.FaultInjectionTracer.TraceTest(2942709053U);
						asyncResult2.State.Log.Debug("Command/literal sent, begin reading response.", new object[0]);
						this.currentResponse.Reset(this.currentCommand);
						this.currentResultData.Clear();
						asyncResult2.State.TimeSent = ExDateTime.UtcNow;
						asyncResult2.PendingAsyncResult = this.BeginNetworkRead(asyncResult2, new AsyncCallback(this.OnReadMoreResponse));
					}
				}
			}
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00007AA4 File Offset: 0x00005CA4
		private void OnReadMoreResponse(IAsyncResult asyncResult)
		{
			lock (this.syncRoot)
			{
				if (this.ShouldCancelCallback())
				{
					NetworkConnection networkConnection = this.ExtractNetworkConnectionFrom(asyncResult);
					byte[] data;
					int offset;
					int num;
					object obj2;
					networkConnection.EndRead(asyncResult, out data, out offset, out num, out obj2);
				}
				else
				{
					base.CheckDisposed();
					AsyncResult<ImapConnectionContext, ImapResultData> asyncResult2 = (AsyncResult<ImapConnectionContext, ImapResultData>)asyncResult.AsyncState;
					ImapResponse imapResponse = this.currentResponse;
					byte[] data;
					int offset;
					int num;
					object obj2;
					this.networkConnection.EndRead(asyncResult, out data, out offset, out num, out obj2);
					if (obj2 != null)
					{
						ImapNetworkFacade.HandleError(obj2, asyncResult2);
					}
					else if (this.cancellationRequested)
					{
						this.HandleCancellation(asyncResult2);
					}
					else
					{
						ExTraceGlobals.FaultInjectionTracer.TraceTest(2674273597U);
						this.totalBytesReceived += (long)num;
						DownloadCompleteEventArgs eventArgs = new DownloadCompleteEventArgs((long)num, 0L);
						asyncResult2.State.ActivatePerfDownloadEvent(asyncResult2.State, eventArgs);
						if (this.totalBytesReceived > this.connectionParameters.MaxBytesToTransfer)
						{
							ImapNetworkFacade.HandleError(ImapNetworkFacade.MaxBytesReceivedExceeded(), asyncResult2);
						}
						else
						{
							int num2 = imapResponse.AddData(data, offset, num);
							int num3 = num - num2;
							this.Log.Assert(num3 >= 0, "The unconsumed bytes must be non-negative.", new object[0]);
							if (num3 > 0)
							{
								this.networkConnection.PutBackReceivedBytes(num3);
							}
							if (imapResponse.IsComplete)
							{
								ImapNetworkFacade.CountCommand(asyncResult2, true);
								asyncResult2.State.Log.Debug("Command complete: [{0}].  Status = {1}", new object[]
								{
									this.currentCommand.ToPiiCleanString(),
									imapResponse.Status
								});
								this.currentResultData.Status = imapResponse.Status;
								if (asyncResult.CompletedSynchronously)
								{
									asyncResult2.SetCompletedSynchronously();
								}
								if (imapResponse.Status == ImapStatus.No || imapResponse.Status == ImapStatus.Bad || imapResponse.Status == ImapStatus.Bye)
								{
									this.LogFailureDetails(this.currentCommand, imapResponse);
									asyncResult2.ProcessCompleted(this.currentResultData);
								}
								else if (imapResponse.Status != ImapStatus.Ok)
								{
									this.LogFailureDetails(this.currentCommand, imapResponse);
									asyncResult2.ProcessCompleted(this.BuildAndLogUnknownCommandFailureException(asyncResult2.State));
								}
								else
								{
									if (!imapResponse.TryParseIntoResult(this.currentCommand, ref this.currentResultData))
									{
										if (this.currentResultData.FailureException == null)
										{
											if (this.currentResultData.MessageStream != null)
											{
												this.totalMessagesReceived += 1UL;
											}
											asyncResult2.ProcessCompleted(this.currentResultData, this.BuildAndLogUnknownParseFailureException(asyncResult2.State));
											return;
										}
										ImapUtilities.LogExceptionDetails(asyncResult2.State.Log, this.currentCommand, this.currentResultData.FailureException);
										this.LogFailureDetails(this.currentCommand, this.currentResponse);
									}
									else
									{
										asyncResult2.State.Log.Debug("Parsed server response succesfully.", new object[0]);
									}
									asyncResult2.ProcessCompleted(this.currentResultData);
								}
							}
							else if (imapResponse.IsWaitingForLiteral)
							{
								Exception ex = null;
								Stream commandParameterStream = this.currentCommand.GetCommandParameterStream(this.serverParameters.Server, imapResponse.GetLastResponseLine(), out ex);
								if (ex != null)
								{
									if (commandParameterStream != null)
									{
										commandParameterStream.Close();
									}
									ImapNetworkFacade.CountCommand(asyncResult2, false);
									asyncResult2.ProcessCompleted(ex);
								}
								else if (commandParameterStream == null)
								{
									ImapNetworkFacade.CountCommand(asyncResult2, false);
									asyncResult2.ProcessCompleted(this.BuildAndLogUnexpectedLiteralRequestException(asyncResult2.State));
								}
								else
								{
									this.totalBytesSent += commandParameterStream.Length;
									if (this.totalBytesSent > this.connectionParameters.MaxBytesToTransfer)
									{
										ImapNetworkFacade.HandleError(ImapNetworkFacade.MaxBytesSentExceeded(), asyncResult2);
									}
									else
									{
										eventArgs = new DownloadCompleteEventArgs(0L, commandParameterStream.Length);
										asyncResult2.State.ActivatePerfDownloadEvent(asyncResult2.State, eventArgs);
										asyncResult2.State.Log.Debug("Begin writing literal stream.", new object[0]);
										asyncResult2.PendingAsyncResult = this.networkConnection.BeginWrite(commandParameterStream, new AsyncCallback(this.OnEndWriteLiteral), asyncResult2);
									}
								}
							}
							else if (imapResponse.TotalLiteralBytesExpected > 0 && this.totalBytesReceived + (long)imapResponse.LiteralBytesRemaining > this.connectionParameters.MaxBytesToTransfer)
							{
								this.totalBytesReceived += (long)imapResponse.LiteralBytesRemaining;
								ImapNetworkFacade.HandleError(ImapNetworkFacade.MaxBytesReceivedExceeded(), asyncResult2);
							}
							else
							{
								asyncResult2.PendingAsyncResult = this.BeginNetworkRead(asyncResult2, new AsyncCallback(this.OnReadMoreResponse));
							}
						}
					}
				}
			}
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00007F30 File Offset: 0x00006130
		private Exception BuildAndLogUnknownParseFailureException(ImapConnectionContext context)
		{
			string text = string.Format(CultureInfo.InvariantCulture, "Unknown parse error in response.  Command = {0}", new object[]
			{
				this.currentCommand.ToPiiCleanString()
			});
			context.Log.Error(text, new object[0]);
			this.LogFailureDetails(this.currentCommand, this.currentResponse);
			return new ImapCommunicationException(text, RetryPolicy.Backoff);
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00007F8E File Offset: 0x0000618E
		private Exception BuildAndLogUnknownCommandFailureException(ImapConnectionContext context)
		{
			return this.BuildAndLogUnknownCommandFailureException(context, this.currentCommand.ToPiiCleanString());
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00007FA4 File Offset: 0x000061A4
		private Exception BuildAndLogUnknownCommandFailureException(ImapConnectionContext context, string commandCleanString)
		{
			string text = string.Format(CultureInfo.InvariantCulture, "Unknown command failure, retry.  Command = {0}.", new object[]
			{
				commandCleanString
			});
			context.Log.Error(text, new object[0]);
			this.LogFailureDetails(commandCleanString, this.currentResponse);
			return new ImapCommunicationException(text, RetryPolicy.Immediate);
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00007FF4 File Offset: 0x000061F4
		private Exception BuildAndLogUnexpectedLiteralRequestException(ImapConnectionContext context)
		{
			string text = string.Format(CultureInfo.InvariantCulture, "Server waiting for literal, but none given with command. Command = {0}.", new object[]
			{
				this.currentCommand.ToPiiCleanString()
			});
			context.Log.Error(text, new object[0]);
			this.LogFailureDetails(this.currentCommand, this.currentResponse);
			return new ImapCommunicationException(text, RetryPolicy.Backoff);
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00008054 File Offset: 0x00006254
		private void HandleCancellation(AsyncResult<ImapConnectionContext, ImapResultData> curOp)
		{
			ImapNetworkFacade.CountCommand(curOp, false);
			this.Log.Error("networkFacade operation cancelled.  Dropped connection.", new object[0]);
			Exception canceledException = AsyncOperationResult<ImapResultData>.CanceledException;
			curOp.ProcessCompleted(new ImapConnectionException(string.Empty, RetryPolicy.Backoff, canceledException));
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00008096 File Offset: 0x00006296
		private void LogFailureDetails(ImapCommand command, ImapResponse response)
		{
			this.LogFailureDetails(command.ToPiiCleanString(), response);
		}

		// Token: 0x06000151 RID: 337 RVA: 0x000080A8 File Offset: 0x000062A8
		private void LogFailureDetails(string command, ImapResponse response)
		{
			this.Log.Error("Error while executing [{0}]", new object[]
			{
				command
			});
			IList<string> responseLines = response.ResponseLines;
			if (responseLines == null || responseLines.Count <= 0)
			{
				return;
			}
			int num = Math.Max(0, responseLines.Count - 10);
			for (int i = num; i < responseLines.Count; i++)
			{
				this.Log.Error("Response line [{0}]: {1}", new object[]
				{
					i,
					responseLines[i]
				});
			}
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00008135 File Offset: 0x00006335
		private NetworkConnection ExtractNetworkConnectionFrom(IAsyncResult asyncResult)
		{
			return ((LazyAsyncResult)asyncResult).AsyncObject as NetworkConnection;
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00008147 File Offset: 0x00006347
		private bool ShouldCancelCallback()
		{
			return base.IsDisposed && this.cancellationRequested;
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00008159 File Offset: 0x00006359
		private void ResetStateForConnectIfNecessary()
		{
			this.CloseConnections();
			this.currentCommand = null;
		}

		// Token: 0x04000094 RID: 148
		private static readonly byte[] BytesCrLf = new byte[]
		{
			Convert.ToByte('\r'),
			Convert.ToByte('\n')
		};

		// Token: 0x04000095 RID: 149
		private static Exception timeoutException;

		// Token: 0x04000096 RID: 150
		private static Exception connectionShutdownException;

		// Token: 0x04000097 RID: 151
		private static Exception unknownFailureException;

		// Token: 0x04000098 RID: 152
		private static Exception maxBytesSentException;

		// Token: 0x04000099 RID: 153
		private static Exception maxBytesReceivedException;

		// Token: 0x0400009A RID: 154
		private static Exception connectionClosedException;

		// Token: 0x0400009B RID: 155
		private readonly object syncRoot = new object();

		// Token: 0x0400009C RID: 156
		private readonly ConnectionParameters connectionParameters;

		// Token: 0x0400009D RID: 157
		private readonly ImapServerParameters serverParameters;

		// Token: 0x0400009E RID: 158
		private readonly ImapResponse currentResponse;

		// Token: 0x0400009F RID: 159
		private Socket socket;

		// Token: 0x040000A0 RID: 160
		private NetworkConnection networkConnection;

		// Token: 0x040000A1 RID: 161
		private long totalBytesSent;

		// Token: 0x040000A2 RID: 162
		private long totalBytesReceived;

		// Token: 0x040000A3 RID: 163
		private ulong totalMessagesReceived;

		// Token: 0x040000A4 RID: 164
		private ImapCommand currentCommand;

		// Token: 0x040000A5 RID: 165
		private ImapResultData currentResultData = new ImapResultData();

		// Token: 0x040000A6 RID: 166
		private bool cancellationRequested;

		// Token: 0x040000A7 RID: 167
		private bool isNetworkConnectionShutdown;
	}
}
