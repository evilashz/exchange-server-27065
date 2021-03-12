using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using Microsoft.Exchange.Collections.TimeoutCache;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Search;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.ProcessManager;
using Microsoft.Exchange.Search.Core.Abstraction;
using Microsoft.Exchange.Search.Core.Diagnostics;

namespace Microsoft.Exchange.Search.Core.Common
{
	// Token: 0x02000087 RID: 135
	internal class StreamManager : IStreamManager, IDisposable
	{
		// Token: 0x06000384 RID: 900 RVA: 0x0000B7D0 File Offset: 0x000099D0
		internal StreamManager(string eventSource, TcpListener.HandleFailure failureHandler)
		{
			this.diagnosticsSession = DiagnosticsSession.CreateComponentDiagnosticsSession("StreamManager", ComponentInstance.Globals.Search.ServiceName, ExTraceGlobals.StreamManagerTracer, (long)this.GetHashCode());
			this.failureHandler = failureHandler;
			if (eventSource != null)
			{
				this.eventLogger = new ExEventLog(Guid.Empty, eventSource);
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000385 RID: 901 RVA: 0x0000B857 File Offset: 0x00009A57
		public static TimeSpan DefaultTimeout
		{
			[DebuggerStepThrough]
			get
			{
				return TimeSpan.FromSeconds(30.0);
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000386 RID: 902 RVA: 0x0000B867 File Offset: 0x00009A67
		// (set) Token: 0x06000387 RID: 903 RVA: 0x0000B86F File Offset: 0x00009A6F
		public int ListenPort
		{
			[DebuggerStepThrough]
			get
			{
				return this.listenPort;
			}
			[DebuggerStepThrough]
			set
			{
				this.listenPort = value;
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000388 RID: 904 RVA: 0x0000B878 File Offset: 0x00009A78
		// (set) Token: 0x06000389 RID: 905 RVA: 0x0000B880 File Offset: 0x00009A80
		public TimeSpan CacheTimeout
		{
			[DebuggerStepThrough]
			get
			{
				return this.cacheTimeout;
			}
			[DebuggerStepThrough]
			set
			{
				this.cacheTimeout = value;
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x0600038A RID: 906 RVA: 0x0000B889 File Offset: 0x00009A89
		// (set) Token: 0x0600038B RID: 907 RVA: 0x0000B891 File Offset: 0x00009A91
		public TimeSpan ConnectionTimeout
		{
			[DebuggerStepThrough]
			get
			{
				return this.connectionTimeout;
			}
			[DebuggerStepThrough]
			set
			{
				this.connectionTimeout = value;
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x0600038C RID: 908 RVA: 0x0000B89A File Offset: 0x00009A9A
		public bool Listening
		{
			[DebuggerStepThrough]
			get
			{
				return this.tcpListener != null;
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x0600038D RID: 909 RVA: 0x0000B8A8 File Offset: 0x00009AA8
		internal int CacheCount
		{
			[DebuggerStepThrough]
			get
			{
				return this.timeoutCache.Count;
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x0600038E RID: 910 RVA: 0x0000B8B5 File Offset: 0x00009AB5
		private bool CanListen
		{
			[DebuggerStepThrough]
			get
			{
				return this.eventLogger != null;
			}
		}

		// Token: 0x0600038F RID: 911 RVA: 0x0000B8C3 File Offset: 0x00009AC3
		public static StreamManager Create()
		{
			return new StreamManager(null, null);
		}

		// Token: 0x06000390 RID: 912 RVA: 0x0000B8CC File Offset: 0x00009ACC
		public static StreamManager CreateForListen(string eventSource, TcpListener.HandleFailure failureHandler)
		{
			Util.ThrowOnNullArgument(eventSource, "eventSource");
			return new StreamManager(eventSource, failureHandler);
		}

		// Token: 0x06000391 RID: 913 RVA: 0x0000B8E0 File Offset: 0x00009AE0
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06000392 RID: 914 RVA: 0x0000B8EC File Offset: 0x00009AEC
		public void StartListening()
		{
			this.CheckDisposed();
			if (!this.CanListen)
			{
				throw new InvalidOperationException("Object is not configured to listen");
			}
			lock (this.eventLogger)
			{
				if (this.tcpListener != null)
				{
					throw new InvalidOperationException("Already listening");
				}
				this.AssignListenPortIfNecessary();
				IPEndPoint ipendPoint = StreamManager.GetIPEndPoint(this.listenPort);
				this.diagnosticsSession.TraceDebug<IPEndPoint>("StartListening on {0}", ipendPoint);
				this.tcpListener = new TcpListener(new TcpListener.HandleFailure(this.HandleTcpListenerFailure), new TcpListener.HandleConnection(this.AcceptCompleteCallback), new IPEndPoint[]
				{
					ipendPoint
				}, this.diagnosticsSession.Tracer, this.eventLogger, int.MaxValue, false, false);
				this.tcpListener.StartListening(true);
			}
		}

		// Token: 0x06000393 RID: 915 RVA: 0x0000B9C8 File Offset: 0x00009BC8
		public void StopListening()
		{
			this.diagnosticsSession.TraceDebug("StopListening", new object[0]);
			if (!this.CanListen)
			{
				return;
			}
			lock (this.eventLogger)
			{
				TcpListener tcpListener = this.tcpListener;
				this.tcpListener = null;
				if (tcpListener != null)
				{
					tcpListener.ProcessStopping = true;
					tcpListener.StopListening();
					tcpListener.Shutdown();
				}
			}
		}

		// Token: 0x06000394 RID: 916 RVA: 0x0000BA48 File Offset: 0x00009C48
		public Stream WaitForConnection(Guid contextId)
		{
			this.diagnosticsSession.TraceDebug("WaitForConnection", new object[0]);
			StreamManager.AsyncResult asyncResult = (StreamManager.AsyncResult)this.BeginWaitForConnection(contextId, null, null);
			Stream result;
			try
			{
				result = this.EndWaitForConnection(asyncResult);
			}
			finally
			{
				asyncResult.InternalCleanup();
			}
			return result;
		}

		// Token: 0x06000395 RID: 917 RVA: 0x0000BA9C File Offset: 0x00009C9C
		public ICancelableAsyncResult BeginWaitForConnection(Guid contextId, AsyncCallback callback, object state)
		{
			this.diagnosticsSession.TraceDebug<Guid>("BeginWaitForConnection: {0}", contextId);
			if (!this.Listening)
			{
				throw new TimeoutException();
			}
			ICancelableAsyncResult result;
			lock (this.asyncResultDictionary)
			{
				this.CheckDisposed();
				if (this.asyncResultDictionary.ContainsKey(contextId))
				{
					throw new ArgumentException("contextId");
				}
				StreamManager.AsyncResult asyncResult = new StreamManager.AsyncResult(contextId, callback, state);
				asyncResult.StartTimer(this.connectionTimeout);
				this.asyncResultDictionary.Add(contextId, asyncResult);
				result = asyncResult;
			}
			return result;
		}

		// Token: 0x06000396 RID: 918 RVA: 0x0000BB3C File Offset: 0x00009D3C
		public Stream EndWaitForConnection(IAsyncResult asyncResult)
		{
			this.diagnosticsSession.TraceDebug("EndWaitForConnection", new object[0]);
			StreamManager.AsyncResult asyncResult2 = StreamManager.AsyncResult.EndAsyncOperation(asyncResult);
			this.diagnosticsSession.TraceDebug<Guid>("EndWaitForConnection complete: {0}", asyncResult2.ContextId);
			lock (this.asyncResultDictionary)
			{
				this.asyncResultDictionary.Remove(asyncResult2.ContextId);
			}
			Exception ex = asyncResult2.Result as Exception;
			if (ex != null)
			{
				this.diagnosticsSession.TraceError<Exception>("EndWaitForConnection exception: {0}", ex);
				asyncResult2.ReleaseResources();
				throw ex;
			}
			return asyncResult2.Channel;
		}

		// Token: 0x06000397 RID: 919 RVA: 0x0000BBEC File Offset: 0x00009DEC
		public Stream Connect(int port, Guid contextId)
		{
			this.diagnosticsSession.TraceDebug("Connect", new object[0]);
			StreamManager.AsyncResult asyncResult = (StreamManager.AsyncResult)this.BeginConnect(port, contextId, null, null);
			Stream result;
			try
			{
				result = this.EndConnect(asyncResult);
			}
			finally
			{
				asyncResult.InternalCleanup();
			}
			return result;
		}

		// Token: 0x06000398 RID: 920 RVA: 0x0000BC44 File Offset: 0x00009E44
		public ICancelableAsyncResult BeginConnect(int port, Guid contextId, AsyncCallback callback, object state)
		{
			this.diagnosticsSession.TraceDebug<int, Guid>("BeginConnect, port: {0}, context: {1}", port, contextId);
			IPEndPoint ipendPoint = StreamManager.GetIPEndPoint(port);
			StreamManager.AsyncResult asyncResult;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				Socket socket = new Socket(ipendPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
				disposeGuard.Add<Socket>(socket);
				lock (this.asyncResultDictionary)
				{
					this.CheckDisposed();
					if (this.asyncResultDictionary.ContainsKey(contextId))
					{
						throw new ArgumentException("contextId");
					}
					asyncResult = new StreamManager.AsyncResult(contextId, callback, state);
					asyncResult.Socket = socket;
					asyncResult.StartTimer(this.connectionTimeout);
					this.asyncResultDictionary.Add(contextId, asyncResult);
				}
				disposeGuard.Success();
			}
			try
			{
				asyncResult.Socket.BeginConnect(ipendPoint, new AsyncCallback(this.ConnectComplete), asyncResult);
			}
			catch (Exception ex)
			{
				this.diagnosticsSession.TraceError<Exception>("Socket.BeginConnect exception: {0}", ex);
				asyncResult.InvokeCallback(ex);
			}
			return asyncResult;
		}

		// Token: 0x06000399 RID: 921 RVA: 0x0000BD70 File Offset: 0x00009F70
		public Stream EndConnect(IAsyncResult asyncResult)
		{
			StreamManager.AsyncResult asyncResult2 = StreamManager.AsyncResult.EndAsyncOperation(asyncResult);
			lock (this.asyncResultDictionary)
			{
				this.asyncResultDictionary.Remove(asyncResult2.ContextId);
			}
			Exception ex = asyncResult2.Result as Exception;
			if (ex != null)
			{
				asyncResult2.ReleaseResources();
				throw ex;
			}
			return asyncResult2.Channel;
		}

		// Token: 0x0600039A RID: 922 RVA: 0x0000BDE0 File Offset: 0x00009FE0
		public void CancelPendingOperation(Guid contextId)
		{
			StreamManager.AsyncResult asyncResult;
			lock (this.asyncResultDictionary)
			{
				asyncResult = this.asyncResultDictionary[contextId];
			}
			this.diagnosticsSession.TraceDebug<Guid, string>("CancelPendingOperation, context: {0} - {1}", contextId, (asyncResult != null) ? "Attempting to cancel" : "Nothing to cancel");
			if (asyncResult != null)
			{
				asyncResult.InvokeCallback(new OperationCanceledException());
			}
		}

		// Token: 0x0600039B RID: 923 RVA: 0x0000BE58 File Offset: 0x0000A058
		public void CheckIn(Stream channel)
		{
			StreamChannel streamChannel = (StreamChannel)channel;
			this.timeoutCache.AddSliding(streamChannel.ContextId, streamChannel, this.cacheTimeout, new RemoveItemDelegate<Guid, StreamChannel>(this.RemoveFromCacheCallback));
		}

		// Token: 0x0600039C RID: 924 RVA: 0x0000BE90 File Offset: 0x0000A090
		public Stream CheckOut(Guid contextId)
		{
			return this.timeoutCache.Remove(contextId);
		}

		// Token: 0x0600039D RID: 925 RVA: 0x0000BE9E File Offset: 0x0000A09E
		internal static IPEndPoint GetIPEndPoint(int port)
		{
			if (Socket.OSSupportsIPv4)
			{
				return new IPEndPoint(IPAddress.Loopback, port);
			}
			return new IPEndPoint(IPAddress.IPv6Loopback, port);
		}

		// Token: 0x0600039E RID: 926 RVA: 0x0000BEC0 File Offset: 0x0000A0C0
		protected virtual void Dispose(bool calledFromDispose)
		{
			if (calledFromDispose)
			{
				this.StopListening();
				List<StreamManager.AsyncResult> list;
				lock (this.asyncResultDictionary)
				{
					this.disposed = true;
					list = new List<StreamManager.AsyncResult>(this.asyncResultDictionary.Values);
				}
				foreach (StreamManager.AsyncResult asyncResult in list)
				{
					asyncResult.TimeoutOperation(null);
				}
				this.timeoutCache.Dispose();
			}
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600039F RID: 927 RVA: 0x0000BF6C File Offset: 0x0000A16C
		private void AssignListenPortIfNecessary()
		{
			if (this.listenPort != 0)
			{
				return;
			}
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				IPEndPoint ipendPoint = StreamManager.GetIPEndPoint(0);
				Socket socket = new Socket(ipendPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
				disposeGuard.Add<Socket>(socket);
				socket.Bind(ipendPoint);
				ipendPoint = (IPEndPoint)socket.LocalEndPoint;
				this.listenPort = ipendPoint.Port;
			}
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x0000BFE8 File Offset: 0x0000A1E8
		private void RemoveFromCacheCallback(Guid contextId, StreamChannel channel, RemoveReason reason)
		{
			if (reason != RemoveReason.Removed)
			{
				try
				{
					channel.Dispose();
				}
				catch (Exception arg)
				{
					this.diagnosticsSession.TraceError<Guid, Exception>("Exception disposing context {0}: {1}", contextId, arg);
				}
			}
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x0000C028 File Offset: 0x0000A228
		private void HandleTcpListenerFailure(bool addressAlreadyInUseFailure)
		{
			this.diagnosticsSession.TraceError("TcpListener told us to stop listening", new object[0]);
			this.StopListening();
			if (this.failureHandler != null)
			{
				this.failureHandler(false);
			}
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x0000C05C File Offset: 0x0000A25C
		private bool AcceptCompleteCallback(Socket connection)
		{
			bool result;
			try
			{
				using (DisposeGuard disposeGuard = default(DisposeGuard))
				{
					StreamChannel streamChannel = new StreamChannel(connection);
					disposeGuard.Add<StreamChannel>(streamChannel);
					this.diagnosticsSession.TraceDebug<StreamChannel>("Accept complete, {0}", streamChannel);
					streamChannel.ReadTimeout = (int)this.connectionTimeout.TotalMilliseconds;
					streamChannel.WriteTimeout = (int)this.connectionTimeout.TotalMilliseconds;
					this.CheckDisposed();
					StreamManager.ChannelAndBuffer channelAndBuffer = new StreamManager.ChannelAndBuffer(streamChannel, new byte[16]);
					streamChannel.BeginRead(channelAndBuffer.Buffer, 0, channelAndBuffer.Buffer.Length, new AsyncCallback(this.ContextIdReceivedCallback), channelAndBuffer);
					disposeGuard.Success();
					result = true;
				}
			}
			catch (Exception arg)
			{
				this.diagnosticsSession.TraceError<Exception>("AcceptCompleteCallback exception: {0}", arg);
				result = false;
			}
			return result;
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x0000C13C File Offset: 0x0000A33C
		private void ContextIdReceivedCallback(IAsyncResult asyncResult)
		{
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				StreamManager.AsyncResult asyncResult2 = null;
				StreamManager.ChannelAndBuffer channelAndBuffer = (StreamManager.ChannelAndBuffer)asyncResult.AsyncState;
				StreamChannel channel = channelAndBuffer.Channel;
				disposeGuard.Add<StreamChannel>(channel);
				int num;
				try
				{
					num = channel.EndRead(asyncResult);
				}
				catch (IOException arg)
				{
					this.diagnosticsSession.TraceError<StreamChannel, IOException>("ContextIdReceivedCallback failed for {0}, Exception: {1}", channel, arg);
					return;
				}
				catch (InvalidDataException arg2)
				{
					this.diagnosticsSession.TraceError<StreamChannel, InvalidDataException>("ContextIdReceivedCallback failed for {0}, Exception: {1}", channel, arg2);
					return;
				}
				if (num != 16)
				{
					this.diagnosticsSession.TraceError<StreamChannel, int, int>("ContextId length mismatch for channel {0}, expected {1}, received {2}", channel, 16, num);
				}
				else
				{
					channel.ContextId = new Guid(channelAndBuffer.Buffer);
					this.diagnosticsSession.TraceDebug<Guid>("ContextId: {0}", channel.ContextId);
					lock (this.asyncResultDictionary)
					{
						this.asyncResultDictionary.TryGetValue(channel.ContextId, out asyncResult2);
					}
					if (asyncResult2 != null)
					{
						disposeGuard.Success();
						asyncResult2.Channel = channel;
						asyncResult2.InvokeCallback();
					}
				}
			}
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x0000C288 File Offset: 0x0000A488
		private void ConnectComplete(IAsyncResult asyncResult)
		{
			StreamManager.AsyncResult asyncResult2 = (StreamManager.AsyncResult)asyncResult.AsyncState;
			try
			{
				asyncResult2.Socket.EndConnect(asyncResult);
				this.CheckDisposed();
				asyncResult2.ConvertSocketToStreamChannel();
				StreamChannel channel = asyncResult2.Channel;
				channel.ReadTimeout = (int)this.connectionTimeout.TotalMilliseconds;
				channel.WriteTimeout = (int)this.connectionTimeout.TotalMilliseconds;
				byte[] array = asyncResult2.ContextId.ToByteArray();
				channel.BeginWrite(array, 0, array.Length, new AsyncCallback(this.SendContextIdComplete), asyncResult2);
			}
			catch (Exception ex)
			{
				this.diagnosticsSession.TraceError<Exception>("ConnectComplete failed, exception: {0}", ex);
				asyncResult2.InvokeCallback(ex);
			}
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x0000C33C File Offset: 0x0000A53C
		private void SendContextIdComplete(IAsyncResult asyncResult)
		{
			StreamManager.AsyncResult asyncResult2 = (StreamManager.AsyncResult)asyncResult.AsyncState;
			try
			{
				asyncResult2.Channel.EndWrite(asyncResult);
				asyncResult2.InvokeCallback();
			}
			catch (Exception ex)
			{
				this.diagnosticsSession.TraceError<Exception>("SendContextIdComplete failed, exception: {0}", ex);
				asyncResult2.InvokeCallback(ex);
			}
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x0000C398 File Offset: 0x0000A598
		private void CheckDisposed()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException("StreamManager");
			}
		}

		// Token: 0x0400018B RID: 395
		private const int MaxListenBacklog = 2147483647;

		// Token: 0x0400018C RID: 396
		private const int ContextIdByteLength = 16;

		// Token: 0x0400018D RID: 397
		private readonly IDiagnosticsSession diagnosticsSession;

		// Token: 0x0400018E RID: 398
		private readonly ExEventLog eventLogger;

		// Token: 0x0400018F RID: 399
		private TimeSpan cacheTimeout = StreamManager.DefaultTimeout;

		// Token: 0x04000190 RID: 400
		private TimeSpan connectionTimeout = StreamManager.DefaultTimeout;

		// Token: 0x04000191 RID: 401
		private TcpListener tcpListener;

		// Token: 0x04000192 RID: 402
		private int listenPort;

		// Token: 0x04000193 RID: 403
		private Dictionary<Guid, StreamManager.AsyncResult> asyncResultDictionary = new Dictionary<Guid, StreamManager.AsyncResult>();

		// Token: 0x04000194 RID: 404
		private TimeoutCache<Guid, StreamChannel> timeoutCache = new TimeoutCache<Guid, StreamChannel>(1, 250, true);

		// Token: 0x04000195 RID: 405
		private bool disposed;

		// Token: 0x04000196 RID: 406
		private TcpListener.HandleFailure failureHandler;

		// Token: 0x02000088 RID: 136
		private class ChannelAndBuffer
		{
			// Token: 0x060003A7 RID: 935 RVA: 0x0000C3AD File Offset: 0x0000A5AD
			internal ChannelAndBuffer(StreamChannel channel, byte[] buffer)
			{
				this.Channel = channel;
				this.Buffer = buffer;
			}

			// Token: 0x170000C7 RID: 199
			// (get) Token: 0x060003A8 RID: 936 RVA: 0x0000C3C3 File Offset: 0x0000A5C3
			// (set) Token: 0x060003A9 RID: 937 RVA: 0x0000C3CB File Offset: 0x0000A5CB
			internal StreamChannel Channel { get; set; }

			// Token: 0x170000C8 RID: 200
			// (get) Token: 0x060003AA RID: 938 RVA: 0x0000C3D4 File Offset: 0x0000A5D4
			// (set) Token: 0x060003AB RID: 939 RVA: 0x0000C3DC File Offset: 0x0000A5DC
			internal byte[] Buffer { get; set; }
		}

		// Token: 0x02000089 RID: 137
		private class AsyncResult : LazyAsyncResultWithTimeout
		{
			// Token: 0x060003AC RID: 940 RVA: 0x0000C3E5 File Offset: 0x0000A5E5
			internal AsyncResult(Guid contextId, AsyncCallback callback, object callerState) : base(null, callerState, callback)
			{
				this.contextId = contextId;
			}

			// Token: 0x170000C9 RID: 201
			// (get) Token: 0x060003AD RID: 941 RVA: 0x0000C3F7 File Offset: 0x0000A5F7
			// (set) Token: 0x060003AE RID: 942 RVA: 0x0000C3FF File Offset: 0x0000A5FF
			internal Socket Socket
			{
				[DebuggerStepThrough]
				get
				{
					return this.socket;
				}
				[DebuggerStepThrough]
				set
				{
					this.socket = value;
				}
			}

			// Token: 0x170000CA RID: 202
			// (get) Token: 0x060003AF RID: 943 RVA: 0x0000C408 File Offset: 0x0000A608
			// (set) Token: 0x060003B0 RID: 944 RVA: 0x0000C410 File Offset: 0x0000A610
			internal StreamChannel Channel
			{
				[DebuggerStepThrough]
				get
				{
					return this.channel;
				}
				[DebuggerStepThrough]
				set
				{
					this.channel = value;
				}
			}

			// Token: 0x170000CB RID: 203
			// (get) Token: 0x060003B1 RID: 945 RVA: 0x0000C419 File Offset: 0x0000A619
			internal Guid ContextId
			{
				[DebuggerStepThrough]
				get
				{
					return this.contextId;
				}
			}

			// Token: 0x060003B2 RID: 946 RVA: 0x0000C424 File Offset: 0x0000A624
			internal static StreamManager.AsyncResult EndAsyncOperation(IAsyncResult asyncResult)
			{
				StreamManager.AsyncResult asyncResult2 = LazyAsyncResult.EndAsyncOperation<StreamManager.AsyncResult>(asyncResult);
				asyncResult2.DisposeTimer();
				return asyncResult2;
			}

			// Token: 0x060003B3 RID: 947 RVA: 0x0000C43F File Offset: 0x0000A63F
			internal void ConvertSocketToStreamChannel()
			{
				this.channel = new StreamChannel(this.socket);
				this.channel.ContextId = this.contextId;
				this.socket = null;
			}

			// Token: 0x060003B4 RID: 948 RVA: 0x0000C46A File Offset: 0x0000A66A
			internal void ReleaseResources()
			{
				if (this.socket != null)
				{
					this.socket.Dispose();
					this.socket = null;
				}
				if (this.channel != null)
				{
					this.channel.Dispose();
					this.channel = null;
				}
			}

			// Token: 0x04000199 RID: 409
			private readonly Guid contextId;

			// Token: 0x0400019A RID: 410
			private StreamChannel channel;

			// Token: 0x0400019B RID: 411
			private Socket socket;
		}
	}
}
