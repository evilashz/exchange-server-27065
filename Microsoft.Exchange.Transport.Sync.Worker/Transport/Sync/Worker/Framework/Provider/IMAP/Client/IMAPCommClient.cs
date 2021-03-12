using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net.Sockets;
using System.Security;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ContentAggregation;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.MailboxTransport.ContentAggregation;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Security;
using Microsoft.Exchange.Transport.Logging.ConnectionLog;
using Microsoft.Exchange.Transport.Sync.Common;
using Microsoft.Exchange.Transport.Sync.Common.Exceptions;
using Microsoft.Exchange.Transport.Sync.Common.Logging;
using Microsoft.Exchange.Transport.Sync.Common.Subscription;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Pim;
using Microsoft.Win32;

namespace Microsoft.Exchange.Transport.Sync.Worker.Framework.Provider.IMAP.Client
{
	// Token: 0x020001D3 RID: 467
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class IMAPCommClient : DisposeTrackableBase, ICommClient, IDisposeTrackable, IDisposable
	{
		// Token: 0x06000E11 RID: 3601 RVA: 0x00023FD0 File Offset: 0x000221D0
		internal IMAPCommClient(Fqdn host, int port, SyncLogSession syncLogSession, string sessionId, Guid subscriptionGuid, long maxDownloadBytesAllowed, int connectionTimeout)
		{
			this.host = host;
			this.port = port;
			this.logSession = syncLogSession;
			this.sessionId = sessionId;
			this.subscriptionGuid = subscriptionGuid;
			this.maxDownloadBytesAllowed = maxDownloadBytesAllowed;
			this.connectionTimeout = connectionTimeout;
			this.currentResponse = new IMAPResponse(syncLogSession);
		}

		// Token: 0x17000517 RID: 1303
		// (get) Token: 0x06000E12 RID: 3602 RVA: 0x0002403A File Offset: 0x0002223A
		public long TotalBytesSent
		{
			get
			{
				base.CheckDisposed();
				return this.totalBytesSent;
			}
		}

		// Token: 0x17000518 RID: 1304
		// (get) Token: 0x06000E13 RID: 3603 RVA: 0x00024048 File Offset: 0x00022248
		public long TotalBytesReceived
		{
			get
			{
				base.CheckDisposed();
				return this.totalBytesReceived;
			}
		}

		// Token: 0x17000519 RID: 1305
		// (get) Token: 0x06000E14 RID: 3604 RVA: 0x00024056 File Offset: 0x00022256
		public bool IsConnected
		{
			get
			{
				base.CheckDisposed();
				return this.conn != null && !this.isNetworkConnectionShutdown;
			}
		}

		// Token: 0x06000E15 RID: 3605 RVA: 0x00024074 File Offset: 0x00022274
		public IAsyncResult BeginConnect(IMAPClientState imapClientState, AsyncCallback callback, object asyncState, object syncPoisonContext)
		{
			base.CheckDisposed();
			this.ResetStateForConnectIfNecessary();
			this.socket = SocketFactory.CreateTcpStreamSocket();
			AsyncResult<IMAPClientState, IMAPResultData> asyncResult = new AsyncResult<IMAPClientState, IMAPResultData>(this, imapClientState, callback, asyncState, syncPoisonContext);
			try
			{
				asyncResult.PendingAsyncResult = this.socket.BeginConnect(this.host, this.port, asyncResult.GetAsyncCallbackWithPoisonContextAndUnhandledExceptionRedirect(new AsyncCallback(this.OnEndConnectInternalReadResponse)), asyncResult);
			}
			catch (SocketException errorCode)
			{
				this.socket = null;
				IMAPCommClient.HandleError(errorCode, asyncResult);
			}
			return asyncResult;
		}

		// Token: 0x06000E16 RID: 3606 RVA: 0x00024100 File Offset: 0x00022300
		public AsyncOperationResult<IMAPResultData> EndConnect(IAsyncResult asyncResult)
		{
			base.CheckDisposed();
			AsyncResult<IMAPClientState, IMAPResultData> asyncResult2 = (AsyncResult<IMAPClientState, IMAPResultData>)asyncResult;
			return asyncResult2.WaitForCompletion();
		}

		// Token: 0x06000E17 RID: 3607 RVA: 0x00024120 File Offset: 0x00022320
		public IAsyncResult BeginNegotiateTlsAsClient(IMAPClientState imapClientState, AsyncCallback callback, object asyncState, object syncPoisonContext)
		{
			base.CheckDisposed();
			AsyncResult<IMAPClientState, IMAPResultData> asyncResult = new AsyncResult<IMAPClientState, IMAPResultData>(this, imapClientState, callback, asyncState, syncPoisonContext);
			asyncResult.PendingAsyncResult = this.conn.BeginNegotiateTlsAsClient(null, this.conn.RemoteEndPoint.Address.ToString(), asyncResult.GetAsyncCallbackWithPoisonContext(new AsyncCallback(this.OnEndConnectNegotiateTlsAsClientInternalReadResponse)), asyncResult);
			return asyncResult;
		}

		// Token: 0x06000E18 RID: 3608 RVA: 0x0002417C File Offset: 0x0002237C
		public AsyncOperationResult<IMAPResultData> EndNegotiateTlsAsClient(IAsyncResult asyncResult)
		{
			base.CheckDisposed();
			AsyncResult<IMAPClientState, IMAPResultData> asyncResult2 = (AsyncResult<IMAPClientState, IMAPResultData>)asyncResult;
			return asyncResult2.WaitForCompletion();
		}

		// Token: 0x06000E19 RID: 3609 RVA: 0x0002419C File Offset: 0x0002239C
		public IAsyncResult BeginCommand(IMAPCommand command, IMAPClientState imapClientState, AsyncCallback callback, object asyncState, object syncPoisonContext)
		{
			return this.BeginCommand(command, true, imapClientState, callback, asyncState, syncPoisonContext);
		}

		// Token: 0x06000E1A RID: 3610 RVA: 0x000241AC File Offset: 0x000223AC
		public IAsyncResult BeginCommand(IMAPCommand command, bool processResponse, IMAPClientState imapClientState, AsyncCallback callback, object asyncState, object syncPoisonContext)
		{
			base.CheckDisposed();
			byte[] array = command.ToBytes();
			this.currentResponse.Reset(command);
			this.currentCommand = command;
			AsyncResult<IMAPClientState, IMAPResultData> asyncResult = new AsyncResult<IMAPClientState, IMAPResultData>(this, imapClientState, callback, asyncState, syncPoisonContext);
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
				IMAPCommClient.HandleError(IMAPCommClient.GetConnectionClosedException(), asyncResult);
				return asyncResult;
			}
			this.totalBytesSent += (long)array.Length;
			if (this.totalBytesSent > this.maxDownloadBytesAllowed)
			{
				asyncResult.SetCompletedSynchronously();
				IMAPCommClient.HandleError(IMAPCommClient.MaxBytesSentExceeded(), asyncResult);
				return asyncResult;
			}
			if (processResponse && this.totalBytesReceived > this.maxDownloadBytesAllowed)
			{
				asyncResult.State.Log.LogVerbose((TSLID)845UL, IMAPCommClient.Tracer, "Not sending {0}, since we've exceeded our received-bytes threshold.", new object[]
				{
					this.currentCommand.ToPiiCleanString()
				});
				asyncResult.SetCompletedSynchronously();
				IMAPCommClient.HandleError(IMAPCommClient.MaxBytesReceivedExceeded(), asyncResult);
				return asyncResult;
			}
			asyncResult.State.Log.LogSend((TSLID)846UL, "IMAP Send command: [{0}]", new object[]
			{
				this.currentCommand.ToPiiCleanString()
			});
			DownloadCompleteEventArgs eventArgs = new DownloadCompleteEventArgs(0L, (long)array.Length);
			imapClientState.ActivatePerfDownloadEvent(imapClientState, eventArgs);
			if (processResponse)
			{
				asyncResult.PendingAsyncResult = this.conn.BeginWrite(array, 0, array.Length, asyncResult.GetAsyncCallbackWithPoisonContext(new AsyncCallback(this.OnEndWriteCommandOrLiteralBeginReadResponse)), asyncResult);
			}
			else
			{
				asyncResult.PendingAsyncResult = this.conn.BeginWrite(array, 0, array.Length, asyncResult.GetAsyncCallbackWithPoisonContext(new AsyncCallback(this.OnEndSendCommandIgnoreResponse)), asyncResult);
				asyncResult.SetCompletedSynchronously();
				this.currentResultData.Clear();
				asyncResult.ProcessCompleted(this.currentResultData);
			}
			return asyncResult;
		}

		// Token: 0x06000E1B RID: 3611 RVA: 0x0002437C File Offset: 0x0002257C
		public AsyncOperationResult<IMAPResultData> EndCommand(IAsyncResult asyncResult)
		{
			base.CheckDisposed();
			AsyncResult<IMAPClientState, IMAPResultData> asyncResult2 = (AsyncResult<IMAPClientState, IMAPResultData>)asyncResult;
			return asyncResult2.WaitForCompletion();
		}

		// Token: 0x06000E1C RID: 3612 RVA: 0x0002439C File Offset: 0x0002259C
		public void Cancel()
		{
			lock (this.syncRoot)
			{
				base.CheckDisposed();
				if (!this.cancellationRequested)
				{
					this.cancellationRequested = true;
					if (this.conn != null)
					{
						this.conn.Shutdown();
					}
				}
			}
		}

		// Token: 0x06000E1D RID: 3613 RVA: 0x00024400 File Offset: 0x00022600
		public override void Dispose()
		{
			lock (this.syncRoot)
			{
				base.Dispose();
			}
		}

		// Token: 0x06000E1E RID: 3614 RVA: 0x00024440 File Offset: 0x00022640
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				this.CloseConnections();
			}
		}

		// Token: 0x06000E1F RID: 3615 RVA: 0x0002444B File Offset: 0x0002264B
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<IMAPCommClient>(this);
		}

		// Token: 0x06000E20 RID: 3616 RVA: 0x00024453 File Offset: 0x00022653
		private static Exception BuildTimeoutException()
		{
			if (IMAPCommClient.timeoutException == null)
			{
				IMAPCommClient.timeoutException = SyncTransientException.CreateOperationLevelException(DetailedAggregationStatus.ConnectionError, new IMAPException("Timeout while talking with IMAP server"), true);
			}
			return IMAPCommClient.timeoutException;
		}

		// Token: 0x06000E21 RID: 3617 RVA: 0x00024477 File Offset: 0x00022677
		private static Exception BuildConnectionShutdownException()
		{
			if (IMAPCommClient.connectionShutdownException == null)
			{
				IMAPCommClient.connectionShutdownException = SyncTransientException.CreateOperationLevelException(DetailedAggregationStatus.ConnectionError, new IMAPException("Connection to IMAP server has been shut down"), true);
			}
			return IMAPCommClient.connectionShutdownException;
		}

		// Token: 0x06000E22 RID: 3618 RVA: 0x0002449B File Offset: 0x0002269B
		private static Exception BuildUnknownNetworkException()
		{
			if (IMAPCommClient.unknownFailureException == null)
			{
				IMAPCommClient.unknownFailureException = SyncTransientException.CreateOperationLevelException(DetailedAggregationStatus.CommunicationError, new IMAPException("Unknown failure while talking with IMAP server."), true);
			}
			return IMAPCommClient.unknownFailureException;
		}

		// Token: 0x06000E23 RID: 3619 RVA: 0x000244BF File Offset: 0x000226BF
		private static Exception MaxBytesSentExceeded()
		{
			if (IMAPCommClient.maxBytesSentException == null)
			{
				IMAPCommClient.maxBytesSentException = SyncTransientException.CreateItemLevelException(new IMAPException("Exceeded maximum number of sent bytes for this session."));
			}
			return IMAPCommClient.maxBytesSentException;
		}

		// Token: 0x06000E24 RID: 3620 RVA: 0x000244E1 File Offset: 0x000226E1
		private static Exception MaxBytesReceivedExceeded()
		{
			if (IMAPCommClient.maxBytesReceivedException == null)
			{
				IMAPCommClient.maxBytesReceivedException = SyncTransientException.CreateItemLevelException(new ConnectionDownloadedLimitExceededException(new IMAPException("Exceeded maximum number of received bytes for this session.")));
			}
			return IMAPCommClient.maxBytesReceivedException;
		}

		// Token: 0x06000E25 RID: 3621 RVA: 0x00024508 File Offset: 0x00022708
		private static void CountCommand(AsyncResult<IMAPClientState, IMAPResultData> curOp, bool successful)
		{
			string serverName = ((IMAPCommClient)curOp.State.CommClient).host;
			if (curOp.State.TimeSent != ExDateTime.MinValue)
			{
				curOp.State.NotifyRoundtripComplete(null, new RemoteServerRoundtripCompleteEventArgs(serverName, ExDateTime.UtcNow - curOp.State.TimeSent, successful));
				curOp.State.TimeSent = ExDateTime.MinValue;
			}
		}

		// Token: 0x06000E26 RID: 3622 RVA: 0x0002457F File Offset: 0x0002277F
		private static Exception GetConnectionClosedException()
		{
			if (IMAPCommClient.connectionClosedException == null)
			{
				IMAPCommClient.connectionClosedException = SyncTransientException.CreateOperationLevelException(DetailedAggregationStatus.CommunicationError, new ConnectionClosedException());
			}
			return IMAPCommClient.connectionClosedException;
		}

		// Token: 0x06000E27 RID: 3623 RVA: 0x000245A0 File Offset: 0x000227A0
		private static void HandleError(object errorCode, AsyncResult<IMAPClientState, IMAPResultData> curOp)
		{
			IMAPCommClient.CountCommand(curOp, false);
			if (errorCode is SyncPermanentException || errorCode is SyncTransientException)
			{
				curOp.State.Log.LogError((TSLID)847UL, IMAPCommClient.Tracer, "Exception while communicating: {0}.", new object[]
				{
					errorCode
				});
				curOp.ProcessCompleted((Exception)errorCode);
				return;
			}
			IMAPCommClient imapcommClient = (IMAPCommClient)curOp.State.CommClient;
			if (imapcommClient.conn != null)
			{
				imapcommClient.conn.Shutdown();
				imapcommClient.isNetworkConnectionShutdown = true;
			}
			if (errorCode is SocketError)
			{
				switch ((SocketError)errorCode)
				{
				case SocketError.Shutdown:
					curOp.State.Log.LogError((TSLID)849UL, IMAPCommClient.Tracer, "Network connection shut down: {0}.", new object[]
					{
						errorCode
					});
					curOp.ProcessCompleted(IMAPCommClient.BuildConnectionShutdownException());
					return;
				case SocketError.TimedOut:
					curOp.State.Log.LogError((TSLID)848UL, IMAPCommClient.Tracer, "Operation timed out, shutting down network connection: {0}.", new object[]
					{
						errorCode
					});
					curOp.ProcessCompleted(IMAPCommClient.BuildTimeoutException());
					return;
				}
				curOp.State.Log.LogError((TSLID)850UL, IMAPCommClient.Tracer, "HandleError unhandled SocketError={0}, shutting down network connection.", new object[]
				{
					((SocketError)errorCode).ToString()
				});
				curOp.ProcessCompleted(IMAPCommClient.BuildUnknownNetworkException());
				return;
			}
			if (errorCode is SecurityStatus)
			{
				curOp.State.Log.LogError((TSLID)851UL, IMAPCommClient.Tracer, "IMAPCommClient.HandleError (SecurityStatus={0}), shutting down network connection.", new object[]
				{
					errorCode
				});
				curOp.ProcessCompleted(SyncTransientException.CreateOperationLevelException(DetailedAggregationStatus.ConnectionError, new TlsFailureException(Strings.TlsFailureErrorOccurred((SecurityStatus)errorCode)), true));
				return;
			}
			curOp.State.Log.LogError((TSLID)852UL, IMAPCommClient.Tracer, "HandleError unknown error type (error={0}), shutting down network connection.", new object[]
			{
				errorCode
			});
			curOp.ProcessCompleted(IMAPCommClient.BuildUnknownNetworkException());
		}

		// Token: 0x06000E28 RID: 3624 RVA: 0x000247C4 File Offset: 0x000229C4
		private IAsyncResult BeginNetworkRead(AsyncResult<IMAPClientState, IMAPResultData> curOp, AsyncCallback asyncCallback)
		{
			IAsyncResult result;
			try
			{
				result = this.conn.BeginRead(asyncCallback, curOp);
			}
			catch (InvalidOperationException ex)
			{
				string text = string.Format("BUG: BeginNetworkRead : BeginRead should never throw InvalidOperationException.  Happened during {0}.", this.currentCommand.ToPiiCleanString());
				this.logSession.LogError((TSLID)853UL, IMAPCommClient.Tracer, text, new object[0]);
				curOp.State.Log.ReportWatson(text, ex);
				curOp.ProcessCompleted(SyncTransientException.CreateOperationLevelException(DetailedAggregationStatus.CommunicationError, ex, true));
				result = null;
			}
			return result;
		}

		// Token: 0x06000E29 RID: 3625 RVA: 0x00024850 File Offset: 0x00022A50
		private void CloseConnections()
		{
			if (this.conn != null)
			{
				ConnectionLog.AggregationConnectionStop(IMAPClient.ImapComponentId, this.sessionId, this.conn.RemoteEndPoint.ToString(), this.totalMessagesReceived);
				this.conn.Dispose();
				this.conn = null;
			}
			if (this.socket != null && this.socket.Connected)
			{
				this.socket.Close();
			}
			this.socket = null;
		}

		// Token: 0x06000E2A RID: 3626 RVA: 0x000248C4 File Offset: 0x00022AC4
		private void OnEndConnectInternalReadResponse(IAsyncResult asyncResult)
		{
			AsyncResult<IMAPClientState, IMAPResultData> asyncResult2 = null;
			IMAPClientState imapclientState = null;
			Socket socket = null;
			try
			{
				lock (this.syncRoot)
				{
					if (!this.ShouldCancelCallback())
					{
						base.CheckDisposed();
						asyncResult2 = (AsyncResult<IMAPClientState, IMAPResultData>)asyncResult.AsyncState;
						imapclientState = asyncResult2.State;
						try
						{
							socket = this.socket;
							socket.EndConnect(asyncResult);
							if (this.cancellationRequested)
							{
								this.HandleCancellation(asyncResult2);
								this.socket = null;
								return;
							}
							this.conn = new NetworkConnection(this.socket, 4096);
							this.socket = null;
							this.conn.Timeout = (this.connectionTimeout + 999) / 1000;
							this.logSession.LogVerbose((TSLID)854UL, IMAPCommClient.Tracer, "Connection Completed. Connection ID : {0}, Remote End Point {1}", new object[]
							{
								this.conn.ConnectionId,
								this.conn.RemoteEndPoint
							});
						}
						catch (SocketException ex)
						{
							IMAPUtils.LogExceptionDetails(this.logSession, IMAPCommClient.Tracer, "Failed to connect, SocketException", ex);
							this.socket = null;
							asyncResult2.ProcessCompleted(SyncTransientException.CreateOperationLevelException(DetailedAggregationStatus.ConnectionError, new IMAPException(ex.ToString()), true));
							return;
						}
						ExTraceGlobals.FaultInjectionTracer.TraceTest(3345362237U);
						ConnectionLog.AggregationConnectionStart(IMAPClient.ImapComponentId, this.sessionId, this.conn.RemoteEndPoint.ToString(), this.subscriptionGuid);
						this.logSession.LogConnectionInformation((TSLID)855UL, string.Format(CultureInfo.InvariantCulture, "{0} - {1}", new object[]
						{
							this.host,
							this.conn.RemoteEndPoint
						}));
						switch (imapclientState.IMAPSecurityMechanism)
						{
						case IMAPSecurityMechanism.None:
						case IMAPSecurityMechanism.Tls:
							asyncResult2.PendingAsyncResult = this.BeginNetworkRead(asyncResult2, asyncResult2.GetAsyncCallbackWithPoisonContext(new AsyncCallback(this.OnReadAndDiscardLine)));
							break;
						case IMAPSecurityMechanism.Ssl:
							asyncResult2.PendingAsyncResult = this.conn.BeginNegotiateTlsAsClient(null, this.conn.RemoteEndPoint.Address.ToString(), asyncResult2.GetAsyncCallbackWithPoisonContext(new AsyncCallback(this.OnEndConnectNegotiateTlsAsClientInternalReadResponse)), asyncResult2);
							break;
						default:
							throw new InvalidOperationException("Unexpected security mechanism " + imapclientState.IMAPSecurityMechanism);
						}
					}
				}
			}
			catch (NullReferenceException)
			{
				this.logSession.LogVerbose((TSLID)523UL, IMAPCommClient.Tracer, "NullReferenceException details: asyncResult: %s, curOp: %s, state: %s, socket: %s", new object[]
				{
					(asyncResult == null) ? "Null" : asyncResult.ToString(),
					(asyncResult2 == null) ? "Null" : asyncResult2.ToString(),
					(imapclientState == null) ? "Null" : imapclientState.ToString(),
					(socket == null) ? "Null" : imapclientState.ToString()
				});
				throw;
			}
		}

		// Token: 0x06000E2B RID: 3627 RVA: 0x00024BE8 File Offset: 0x00022DE8
		private void OnEndSendCommandIgnoreResponse(IAsyncResult asyncResult)
		{
			NetworkConnection networkConnection = this.ExtractNetworkConnectionFrom(asyncResult);
			object obj;
			networkConnection.EndWrite(asyncResult, out obj);
		}

		// Token: 0x06000E2C RID: 3628 RVA: 0x00024C08 File Offset: 0x00022E08
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
					AsyncResult<IMAPClientState, IMAPResultData> asyncResult2 = (AsyncResult<IMAPClientState, IMAPResultData>)asyncResult.AsyncState;
					IMAPClientState state = asyncResult2.State;
					object obj2;
					this.conn.EndNegotiateTlsAsClient(asyncResult, out obj2);
					if (obj2 != null)
					{
						IMAPCommClient.HandleError(obj2, asyncResult2);
					}
					else if (this.cancellationRequested)
					{
						this.HandleCancellation(asyncResult2);
					}
					else
					{
						ExTraceGlobals.FaultInjectionTracer.TraceTest(3211144509U);
						switch (state.IMAPSecurityMechanism)
						{
						case IMAPSecurityMechanism.Ssl:
							this.currentResponse.Reset(null);
							asyncResult2.PendingAsyncResult = this.BeginNetworkRead(asyncResult2, asyncResult2.GetAsyncCallbackWithPoisonContext(new AsyncCallback(this.OnReadAndDiscardLine)));
							break;
						case IMAPSecurityMechanism.Tls:
							asyncResult2.ProcessCompleted();
							break;
						default:
							throw new InvalidOperationException("Invalid security mechanism");
						}
					}
				}
			}
		}

		// Token: 0x06000E2D RID: 3629 RVA: 0x00024D18 File Offset: 0x00022F18
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
					AsyncResult<IMAPClientState, IMAPResultData> asyncResult2 = (AsyncResult<IMAPClientState, IMAPResultData>)asyncResult.AsyncState;
					IMAPResponse imapresponse = this.currentResponse;
					byte[] data;
					int offset;
					int num;
					object obj2;
					this.conn.EndRead(asyncResult, out data, out offset, out num, out obj2);
					if (obj2 != null)
					{
						IMAPCommClient.HandleError(obj2, asyncResult2);
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
						if (this.totalBytesReceived > this.maxDownloadBytesAllowed)
						{
							IMAPCommClient.HandleError(IMAPCommClient.MaxBytesReceivedExceeded(), asyncResult2);
						}
						else
						{
							int num2 = imapresponse.AddData(data, offset, num);
							int num3 = num - num2;
							if (num3 > 0)
							{
								this.conn.PutBackReceivedBytes(num3);
							}
							if (imapresponse.IsComplete)
							{
								this.currentResultData.Clear();
								this.currentResultData.Status = imapresponse.Status;
								if (asyncResult.CompletedSynchronously)
								{
									asyncResult2.SetCompletedSynchronously();
								}
								if (imapresponse.Status == IMAPStatus.No || imapresponse.Status == IMAPStatus.Bad || imapresponse.Status == IMAPStatus.Bye)
								{
									this.LogFailureDetails("Connecting", imapresponse);
									asyncResult2.ProcessCompleted(this.currentResultData);
								}
								else if (asyncResult2.State.AggregationType == AggregationType.Aggregation && imapresponse.Status == IMAPStatus.Ok && imapresponse.ResponseLines[0].IndexOf(this.GetGmailBanner(asyncResult2.State.Log), StringComparison.InvariantCulture) != -1)
								{
									asyncResult2.State.Log.LogInformation((TSLID)856UL, IMAPCommClient.Tracer, "Detected Gmail based on the server banner which is not supported at this time. Returning an error", new object[0]);
									asyncResult2.ProcessCompleted(SyncPermanentException.CreateOperationLevelException(DetailedAggregationStatus.CommunicationError, new IMAPGmailNotSupportedException()));
								}
								else if (imapresponse.Status != IMAPStatus.Ok)
								{
									asyncResult2.ProcessCompleted(this.BuildAndLogUnknownCommandFailureException(asyncResult2.State, "Connecting"));
								}
								else
								{
									asyncResult2.ProcessCompleted(this.currentResultData);
								}
							}
							else
							{
								asyncResult2.PendingAsyncResult = this.BeginNetworkRead(asyncResult2, asyncResult2.GetAsyncCallbackWithPoisonContext(new AsyncCallback(this.OnReadAndDiscardLine)));
							}
						}
					}
				}
			}
		}

		// Token: 0x06000E2E RID: 3630 RVA: 0x00024FA8 File Offset: 0x000231A8
		private string GetGmailBanner(SyncLogSession syncLogSession)
		{
			if (IMAPCommClient.gmailBannerText == null)
			{
				lock (IMAPCommClient.staticSyncRoot)
				{
					if (IMAPCommClient.gmailBannerText == null)
					{
						Exception ex = null;
						IMAPCommClient.gmailBannerText = " Gimap ";
						try
						{
							using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Transport", false))
							{
								if (registryKey != null)
								{
									string value = (string)registryKey.GetValue("TransportSyncIMAPGmailBannerText", string.Empty);
									if (!string.IsNullOrEmpty(value))
									{
										IMAPCommClient.gmailBannerText = value;
									}
								}
							}
						}
						catch (SecurityException ex2)
						{
							ex = ex2;
						}
						catch (UnauthorizedAccessException ex3)
						{
							ex = ex3;
						}
						if (ex != null)
						{
							syncLogSession.LogError((TSLID)857UL, IMAPCommClient.Tracer, "IMAPCommClient:Exception encountered while attempting to read banner override: {0}", new object[]
							{
								ex
							});
						}
						syncLogSession.LogInformation((TSLID)858UL, IMAPCommClient.Tracer, "IMAPCommClient: Using the banner text: {0}", new object[]
						{
							IMAPCommClient.gmailBannerText
						});
					}
				}
			}
			return IMAPCommClient.gmailBannerText;
		}

		// Token: 0x06000E2F RID: 3631 RVA: 0x000250E8 File Offset: 0x000232E8
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
					AsyncResult<IMAPClientState, IMAPResultData> asyncResult2 = (AsyncResult<IMAPClientState, IMAPResultData>)asyncResult.AsyncState;
					object obj2;
					this.conn.EndWrite(asyncResult, out obj2);
					if (obj2 != null)
					{
						IMAPCommClient.HandleError(obj2, asyncResult2);
					}
					else if (this.cancellationRequested)
					{
						this.HandleCancellation(asyncResult2);
					}
					else
					{
						ExTraceGlobals.FaultInjectionTracer.TraceTest(3748015421U);
						this.totalBytesSent += (long)IMAPCommClient.BytesCrLf.Length;
						DownloadCompleteEventArgs eventArgs = new DownloadCompleteEventArgs(0L, (long)IMAPCommClient.BytesCrLf.Length);
						asyncResult2.State.ActivatePerfDownloadEvent(asyncResult2.State, eventArgs);
						asyncResult2.State.Log.LogRawData((TSLID)859UL, "Literal sent, sending CRLF to complete it.", new object[0]);
						asyncResult2.PendingAsyncResult = this.conn.BeginWrite(IMAPCommClient.BytesCrLf, 0, IMAPCommClient.BytesCrLf.Length, asyncResult2.GetAsyncCallbackWithPoisonContext(new AsyncCallback(this.OnEndWriteCommandOrLiteralBeginReadResponse)), asyncResult2);
					}
				}
			}
		}

		// Token: 0x06000E30 RID: 3632 RVA: 0x00025230 File Offset: 0x00023430
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
					AsyncResult<IMAPClientState, IMAPResultData> asyncResult2 = (AsyncResult<IMAPClientState, IMAPResultData>)asyncResult.AsyncState;
					object obj2;
					this.conn.EndWrite(asyncResult, out obj2);
					if (obj2 != null)
					{
						IMAPCommClient.HandleError(obj2, asyncResult2);
					}
					else if (this.cancellationRequested)
					{
						this.HandleCancellation(asyncResult2);
					}
					else
					{
						ExTraceGlobals.FaultInjectionTracer.TraceTest(2942709053U);
						asyncResult2.State.Log.LogRawData((TSLID)860UL, "Command/literal sent, begin reading response.", new object[0]);
						this.currentResponse.Reset(this.currentCommand);
						this.currentResultData.Clear();
						asyncResult2.State.TimeSent = ExDateTime.UtcNow;
						asyncResult2.PendingAsyncResult = this.BeginNetworkRead(asyncResult2, asyncResult2.GetAsyncCallbackWithPoisonContext(new AsyncCallback(this.OnReadMoreResponse)));
					}
				}
			}
		}

		// Token: 0x06000E31 RID: 3633 RVA: 0x00025350 File Offset: 0x00023550
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
					AsyncResult<IMAPClientState, IMAPResultData> asyncResult2 = (AsyncResult<IMAPClientState, IMAPResultData>)asyncResult.AsyncState;
					IMAPResponse imapresponse = this.currentResponse;
					byte[] data;
					int offset;
					int num;
					object obj2;
					this.conn.EndRead(asyncResult, out data, out offset, out num, out obj2);
					if (obj2 != null)
					{
						IMAPCommClient.HandleError(obj2, asyncResult2);
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
						if (this.totalBytesReceived > this.maxDownloadBytesAllowed)
						{
							IMAPCommClient.HandleError(IMAPCommClient.MaxBytesReceivedExceeded(), asyncResult2);
						}
						else
						{
							int num2 = imapresponse.AddData(data, offset, num);
							int num3 = num - num2;
							if (num3 > 0)
							{
								this.conn.PutBackReceivedBytes(num3);
							}
							if (imapresponse.IsComplete)
							{
								IMAPCommClient.CountCommand(asyncResult2, true);
								asyncResult2.State.Log.LogRawData((TSLID)861UL, "Command complete: [{0}].  Status = {1}", new object[]
								{
									this.currentCommand.ToPiiCleanString(),
									imapresponse.Status
								});
								this.currentResultData.Status = imapresponse.Status;
								if (asyncResult.CompletedSynchronously)
								{
									asyncResult2.SetCompletedSynchronously();
								}
								if (imapresponse.Status == IMAPStatus.No || imapresponse.Status == IMAPStatus.Bad || imapresponse.Status == IMAPStatus.Bye)
								{
									this.LogFailureDetails(this.currentCommand, imapresponse);
									asyncResult2.ProcessCompleted(this.currentResultData);
								}
								else if (imapresponse.Status != IMAPStatus.Ok)
								{
									this.LogFailureDetails(this.currentCommand, imapresponse);
									asyncResult2.ProcessCompleted(this.BuildAndLogUnknownCommandFailureException(asyncResult2.State));
								}
								else
								{
									if (!imapresponse.TryParseIntoResult(this.currentCommand, ref this.currentResultData))
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
										IMAPUtils.LogExceptionDetails(asyncResult2.State.Log, IMAPCommClient.Tracer, this.currentCommand, this.currentResultData.FailureException);
										this.LogFailureDetails(this.currentCommand, this.currentResponse);
									}
									else
									{
										asyncResult2.State.Log.LogRawData((TSLID)862UL, "Parsed server response succesfully.", new object[0]);
									}
									asyncResult2.ProcessCompleted(this.currentResultData);
								}
							}
							else if (imapresponse.IsWaitingForLiteral)
							{
								Exception ex = null;
								Stream commandParameterStream = this.currentCommand.GetCommandParameterStream(this.host, imapresponse.GetLastResponseLine(), out ex);
								if (ex != null)
								{
									if (commandParameterStream != null)
									{
										commandParameterStream.Close();
									}
									IMAPCommClient.CountCommand(asyncResult2, false);
									asyncResult2.ProcessCompleted(ex);
								}
								else if (commandParameterStream == null)
								{
									IMAPCommClient.CountCommand(asyncResult2, false);
									asyncResult2.ProcessCompleted(this.BuildAndLogUnexpectedLiteralRequestException(asyncResult2.State));
								}
								else
								{
									this.totalBytesSent += commandParameterStream.Length;
									if (this.totalBytesSent > this.maxDownloadBytesAllowed)
									{
										IMAPCommClient.HandleError(IMAPCommClient.MaxBytesSentExceeded(), asyncResult2);
									}
									else
									{
										eventArgs = new DownloadCompleteEventArgs(0L, commandParameterStream.Length);
										asyncResult2.State.ActivatePerfDownloadEvent(asyncResult2.State, eventArgs);
										asyncResult2.State.Log.LogRawData((TSLID)863UL, "Begin writing literal stream.", new object[0]);
										asyncResult2.PendingAsyncResult = this.conn.BeginWrite(commandParameterStream, asyncResult2.GetAsyncCallbackWithPoisonContext(new AsyncCallback(this.OnEndWriteLiteral)), asyncResult2);
									}
								}
							}
							else if (imapresponse.TotalLiteralBytesExpected > 0 && this.totalBytesReceived + (long)imapresponse.LiteralBytesRemaining > this.maxDownloadBytesAllowed)
							{
								this.totalBytesReceived += (long)imapresponse.LiteralBytesRemaining;
								IMAPCommClient.HandleError(IMAPCommClient.MaxBytesReceivedExceeded(), asyncResult2);
							}
							else
							{
								asyncResult2.PendingAsyncResult = this.BeginNetworkRead(asyncResult2, asyncResult2.GetAsyncCallbackWithPoisonContext(new AsyncCallback(this.OnReadMoreResponse)));
							}
						}
					}
				}
			}
		}

		// Token: 0x06000E32 RID: 3634 RVA: 0x000257E0 File Offset: 0x000239E0
		private Exception BuildAndLogUnknownParseFailureException(IMAPClientState state)
		{
			string text = string.Format(CultureInfo.InvariantCulture, "Unknown parse error in response.  Command = {0}", new object[]
			{
				this.currentCommand.ToPiiCleanString()
			});
			state.Log.LogError((TSLID)864UL, IMAPCommClient.Tracer, text, new object[0]);
			this.LogFailureDetails(this.currentCommand, this.currentResponse);
			return SyncTransientException.CreateOperationLevelException(DetailedAggregationStatus.CommunicationError, new IMAPException(text), true);
		}

		// Token: 0x06000E33 RID: 3635 RVA: 0x00025854 File Offset: 0x00023A54
		private Exception BuildAndLogUnknownCommandFailureException(IMAPClientState state)
		{
			return this.BuildAndLogUnknownCommandFailureException(state, this.currentCommand.ToPiiCleanString());
		}

		// Token: 0x06000E34 RID: 3636 RVA: 0x00025868 File Offset: 0x00023A68
		private Exception BuildAndLogUnknownCommandFailureException(IMAPClientState state, string commandCleanString)
		{
			string text = string.Format(CultureInfo.InvariantCulture, "Unknown command failure, retry.  Command = {0}.", new object[]
			{
				commandCleanString
			});
			state.Log.LogError((TSLID)865UL, IMAPCommClient.Tracer, text, new object[0]);
			this.LogFailureDetails(commandCleanString, this.currentResponse);
			return SyncTransientException.CreateOperationLevelException(DetailedAggregationStatus.CommunicationError, new IMAPException(text), false);
		}

		// Token: 0x06000E35 RID: 3637 RVA: 0x000258D0 File Offset: 0x00023AD0
		private Exception BuildAndLogUnexpectedLiteralRequestException(IMAPClientState state)
		{
			string text = string.Format(CultureInfo.InvariantCulture, "Server waiting for literal, but none given with command. Command = {0}.", new object[]
			{
				this.currentCommand.ToPiiCleanString()
			});
			state.Log.LogError((TSLID)866UL, IMAPCommClient.Tracer, text, new object[0]);
			this.LogFailureDetails(this.currentCommand, this.currentResponse);
			return SyncTransientException.CreateOperationLevelException(DetailedAggregationStatus.CommunicationError, new IMAPException(text), true);
		}

		// Token: 0x06000E36 RID: 3638 RVA: 0x00025944 File Offset: 0x00023B44
		private void HandleCancellation(AsyncResult<IMAPClientState, IMAPResultData> curOp)
		{
			IMAPCommClient.CountCommand(curOp, false);
			this.logSession.LogError((TSLID)867UL, IMAPCommClient.Tracer, "CommClient operation cancelled.  Dropped connection.", new object[0]);
			curOp.ProcessCompleted(SyncTransientException.CreateOperationLevelException(DetailedAggregationStatus.ConnectionError, AsyncOperationResult<IMAPResultData>.CanceledException, true));
		}

		// Token: 0x06000E37 RID: 3639 RVA: 0x00025990 File Offset: 0x00023B90
		private void LogFailureDetails(IMAPCommand command, IMAPResponse response)
		{
			this.LogFailureDetails(command.ToPiiCleanString(), response);
		}

		// Token: 0x06000E38 RID: 3640 RVA: 0x000259A0 File Offset: 0x00023BA0
		private void LogFailureDetails(string command, IMAPResponse response)
		{
			this.logSession.LogError((TSLID)868UL, IMAPCommClient.Tracer, "Error while executing [{0}]", new object[]
			{
				command
			});
			IList<string> responseLines = response.ResponseLines;
			if (responseLines != null && responseLines.Count > 0)
			{
				for (int i = Math.Max(0, responseLines.Count - 10); i < responseLines.Count; i++)
				{
					this.logSession.LogError((TSLID)869UL, IMAPCommClient.Tracer, "Response line [{0}]: {1}", new object[]
					{
						i,
						responseLines[i]
					});
				}
			}
		}

		// Token: 0x06000E39 RID: 3641 RVA: 0x00025A46 File Offset: 0x00023C46
		private NetworkConnection ExtractNetworkConnectionFrom(IAsyncResult asyncResult)
		{
			return ((LazyAsyncResult)asyncResult).AsyncObject as NetworkConnection;
		}

		// Token: 0x06000E3A RID: 3642 RVA: 0x00025A58 File Offset: 0x00023C58
		private bool ShouldCancelCallback()
		{
			return base.IsDisposed && this.cancellationRequested;
		}

		// Token: 0x06000E3B RID: 3643 RVA: 0x00025A6A File Offset: 0x00023C6A
		private void ResetStateForConnectIfNecessary()
		{
			this.CloseConnections();
			this.currentCommand = null;
		}

		// Token: 0x040007AC RID: 1964
		private const int NumberOfLinesToLog = 10;

		// Token: 0x040007AD RID: 1965
		private const string NullString = "Null";

		// Token: 0x040007AE RID: 1966
		internal static readonly Trace Tracer = ExTraceGlobals.IMAPClientTracer;

		// Token: 0x040007AF RID: 1967
		private static readonly byte[] BytesCrLf = new byte[]
		{
			Convert.ToByte('\r'),
			Convert.ToByte('\n')
		};

		// Token: 0x040007B0 RID: 1968
		private static volatile string gmailBannerText;

		// Token: 0x040007B1 RID: 1969
		private static object staticSyncRoot = new object();

		// Token: 0x040007B2 RID: 1970
		private static Exception timeoutException;

		// Token: 0x040007B3 RID: 1971
		private static Exception connectionShutdownException;

		// Token: 0x040007B4 RID: 1972
		private static Exception unknownFailureException;

		// Token: 0x040007B5 RID: 1973
		private static Exception maxBytesSentException;

		// Token: 0x040007B6 RID: 1974
		private static Exception maxBytesReceivedException;

		// Token: 0x040007B7 RID: 1975
		private static Exception connectionClosedException;

		// Token: 0x040007B8 RID: 1976
		private Fqdn host;

		// Token: 0x040007B9 RID: 1977
		private int port;

		// Token: 0x040007BA RID: 1978
		private Socket socket;

		// Token: 0x040007BB RID: 1979
		private NetworkConnection conn;

		// Token: 0x040007BC RID: 1980
		private SyncLogSession logSession;

		// Token: 0x040007BD RID: 1981
		private string sessionId;

		// Token: 0x040007BE RID: 1982
		private Guid subscriptionGuid;

		// Token: 0x040007BF RID: 1983
		private int connectionTimeout;

		// Token: 0x040007C0 RID: 1984
		private long totalBytesSent;

		// Token: 0x040007C1 RID: 1985
		private long totalBytesReceived;

		// Token: 0x040007C2 RID: 1986
		private ulong totalMessagesReceived;

		// Token: 0x040007C3 RID: 1987
		private long maxDownloadBytesAllowed;

		// Token: 0x040007C4 RID: 1988
		private IMAPResultData currentResultData = new IMAPResultData();

		// Token: 0x040007C5 RID: 1989
		private IMAPResponse currentResponse;

		// Token: 0x040007C6 RID: 1990
		private IMAPCommand currentCommand;

		// Token: 0x040007C7 RID: 1991
		private bool cancellationRequested;

		// Token: 0x040007C8 RID: 1992
		private bool isNetworkConnectionShutdown;

		// Token: 0x040007C9 RID: 1993
		private object syncRoot = new object();
	}
}
