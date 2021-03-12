using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Internal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.PopImap.Core
{
	// Token: 0x02000003 RID: 3
	internal abstract class BaseSession : IDisposeTrackable, IDisposable
	{
		// Token: 0x0600007E RID: 126 RVA: 0x000036D8 File Offset: 0x000018D8
		public BaseSession(NetworkConnection connection, int connectionTimeout, int maxCommandLength)
		{
			if (connection == null)
			{
				throw new ArgumentNullException("connection");
			}
			this.Started = ExDateTime.UtcNow;
			this.connection = connection;
			this.responseItems = new ResponseQueue(100);
			this.sessionId = connection.ConnectionId;
			connection.Timeout = connectionTimeout;
			this.SetMaxCommandLength(maxCommandLength);
			this.disposeTracker = this.GetDisposeTracker();
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600007F RID: 127 RVA: 0x00003749 File Offset: 0x00001949
		public static byte[] LowerC
		{
			get
			{
				return BufferParser.LowerC;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000080 RID: 128 RVA: 0x00003750 File Offset: 0x00001950
		public StringResponseItemProcessor StringResponseItemProcessor
		{
			get
			{
				return this.stringResponseItemProcessor;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000081 RID: 129 RVA: 0x00003758 File Offset: 0x00001958
		// (set) Token: 0x06000082 RID: 130 RVA: 0x00003760 File Offset: 0x00001960
		public NetworkConnection Connection
		{
			get
			{
				return this.connection;
			}
			protected set
			{
				this.connection = value;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000083 RID: 131 RVA: 0x00003769 File Offset: 0x00001969
		public IPEndPoint LocalEndPoint
		{
			get
			{
				if (this.connection != null)
				{
					return this.connection.LocalEndPoint;
				}
				return null;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000084 RID: 132 RVA: 0x00003780 File Offset: 0x00001980
		public IPEndPoint RemoteEndPoint
		{
			get
			{
				if (this.connection != null)
				{
					return this.connection.RemoteEndPoint;
				}
				return null;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000085 RID: 133 RVA: 0x00003797 File Offset: 0x00001997
		public long SessionId
		{
			get
			{
				return this.sessionId;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000086 RID: 134 RVA: 0x0000379F File Offset: 0x0000199F
		// (set) Token: 0x06000087 RID: 135 RVA: 0x000037A7 File Offset: 0x000019A7
		public bool Disconnected
		{
			get
			{
				return this.disconnected;
			}
			set
			{
				this.disconnected = value;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000088 RID: 136 RVA: 0x000037B0 File Offset: 0x000019B0
		// (set) Token: 0x06000089 RID: 137 RVA: 0x000037B8 File Offset: 0x000019B8
		public bool NegotiatingTls
		{
			get
			{
				return this.negotiatingTls;
			}
			set
			{
				this.negotiatingTls = value;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600008A RID: 138 RVA: 0x000037C1 File Offset: 0x000019C1
		// (set) Token: 0x0600008B RID: 139 RVA: 0x000037C9 File Offset: 0x000019C9
		public ExDateTime Started { get; set; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600008C RID: 140 RVA: 0x000037D2 File Offset: 0x000019D2
		// (set) Token: 0x0600008D RID: 141 RVA: 0x000037DA File Offset: 0x000019DA
		public ExDateTime LastActivityTime { get; set; }

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600008E RID: 142 RVA: 0x000037E3 File Offset: 0x000019E3
		public bool Disposed
		{
			get
			{
				return this.disposed;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600008F RID: 143 RVA: 0x000037EB File Offset: 0x000019EB
		// (set) Token: 0x06000090 RID: 144 RVA: 0x000037F3 File Offset: 0x000019F3
		protected BaseSession.ConnectionShutdownDelegate ConnectionShutdown
		{
			get
			{
				return this.connectionShutdown;
			}
			set
			{
				this.connectionShutdown = value;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000091 RID: 145 RVA: 0x000037FC File Offset: 0x000019FC
		protected int ProcessingCommandRefCounter
		{
			get
			{
				return this.processingCommandRefCounter;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000092 RID: 146 RVA: 0x00003804 File Offset: 0x00001A04
		protected EndResponseItem BeginReadResponseItem
		{
			get
			{
				if (this.beginReadResponseItem == null)
				{
					this.beginReadResponseItem = new EndResponseItem(new BaseSession.SendCompleteDelegate(this.BeginRead));
				}
				return this.beginReadResponseItem;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000093 RID: 147 RVA: 0x0000382C File Offset: 0x00001A2C
		protected byte[] ProxyBuffer
		{
			get
			{
				if (BaseSession.proxyBufferPool == null)
				{
					lock (BaseSession.proxyBufferPoolLock)
					{
						if (BaseSession.proxyBufferPool == null)
						{
							BaseSession.proxyBufferPool = new BufferPool(4096);
						}
					}
				}
				if (this.proxyBuffer == null)
				{
					lock (this.LockObject)
					{
						if (this.proxyBuffer == null)
						{
							this.proxyBuffer = BaseSession.proxyBufferPool.Acquire();
						}
					}
				}
				return this.proxyBuffer;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000094 RID: 148 RVA: 0x000038E4 File Offset: 0x00001AE4
		protected virtual object LockObject
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06000095 RID: 149 RVA: 0x000038E7 File Offset: 0x00001AE7
		public void SetMaxCommandLength(int maxCommandLength)
		{
			this.maxCommandLength = maxCommandLength;
			this.connection.MaxLineLength = Math.Min(maxCommandLength, 4096);
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00003908 File Offset: 0x00001B08
		public bool SendToClient(IResponseItem responseitem)
		{
			if (this.disposed)
			{
				using (responseitem as IDisposable)
				{
				}
				return !this.disconnected;
			}
			lock (this.LockObject)
			{
				if (this.disposed)
				{
					using (responseitem as IDisposable)
					{
					}
					return !this.disconnected;
				}
				this.responseItems.Enqueue(responseitem);
				if (this.responseItems.IsSending || this.negotiatingTls)
				{
					return !this.disconnected;
				}
				this.responseItems.DequeueForSend();
			}
			if (this.connection != null)
			{
				this.SendNextChunk(this.connection);
			}
			return !this.disconnected;
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00003A04 File Offset: 0x00001C04
		public override string ToString()
		{
			string result;
			lock (this.LockObject)
			{
				if (this.connection != null)
				{
					string text = this.SessionId.ToString();
					string text2 = this.RemoteEndPoint.ToString();
					string text3 = this.LocalEndPoint.ToString();
					StringBuilder stringBuilder = new StringBuilder(text.Length + text2.Length + text3.Length + 21);
					stringBuilder.Append("Connection ");
					stringBuilder.Append(text);
					stringBuilder.Append(" from ");
					stringBuilder.Append(text2);
					stringBuilder.Append(" to ");
					stringBuilder.Append(text3);
					result = stringBuilder.ToString();
				}
				else
				{
					result = "Disconnected connection " + this.SessionId;
				}
			}
			return result;
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00003AF4 File Offset: 0x00001CF4
		public virtual void EnterReadLoop(NetworkConnection networkConnection)
		{
			networkConnection.BeginReadLine(new AsyncCallback(this.ReadLineCompleteCallback), networkConnection);
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00003B0C File Offset: 0x00001D0C
		public void SendBufferAsCommand(byte[] buf, int offset, int size)
		{
			byte[] array;
			if (size > this.ProxyBuffer.Length - Strings.CRLFByteArray.Length)
			{
				array = new byte[size + Strings.CRLFByteArray.Length];
			}
			else
			{
				array = this.ProxyBuffer;
			}
			Array.Copy(buf, offset, array, 0, size);
			Array.Copy(Strings.CRLFByteArray, 0, array, size, Strings.CRLFByteArray.Length);
			this.SendToClient(new BufferResponseItem(array, 0, size + Strings.CRLFByteArray.Length));
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00003B79 File Offset: 0x00001D79
		public void Dispose()
		{
			if (!this.disposed)
			{
				this.InternalDispose();
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00003B8F File Offset: 0x00001D8F
		public void EnterCommandProcessing()
		{
			Interlocked.Increment(ref this.processingCommandRefCounter);
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00003BA0 File Offset: 0x00001DA0
		public void LeaveCommandProcessing()
		{
			lock (this.LockObject)
			{
				if (Interlocked.Decrement(ref this.processingCommandRefCounter) == 0 && this.disconnected)
				{
					this.Dispose();
				}
			}
		}

		// Token: 0x0600009D RID: 157
		public abstract bool IsUserTraceEnabled();

		// Token: 0x0600009E RID: 158
		public abstract string GetUserNameForLogging();

		// Token: 0x0600009F RID: 159
		public abstract string GetUserConfigurationName();

		// Token: 0x060000A0 RID: 160 RVA: 0x00003BF8 File Offset: 0x00001DF8
		public virtual bool CheckNonCriticalException(Exception exception)
		{
			if (!ProtocolBaseServices.IsCriticalException(exception))
			{
				if (!this.disposed && !(exception is LocalizedException))
				{
					ProtocolBaseServices.SendWatson(exception, false);
				}
				else
				{
					ProtocolBaseServices.ServerTracer.TraceError<Exception>(0L, "Non-critical exception is caught and handled: {0}", exception);
				}
				return true;
			}
			ProtocolBaseServices.ServerTracer.TraceError<Exception>(0L, "Critical exception is not handled: {0}", exception);
			return false;
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00003C4F File Offset: 0x00001E4F
		public void EnableUserTracing()
		{
			if (this.IsUserTraceEnabled())
			{
				BaseTrace.CurrentThreadSettings.EnableTracing();
			}
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00003C63 File Offset: 0x00001E63
		public void DisableUserTracing()
		{
			if (BaseTrace.CurrentThreadSettings.IsEnabled)
			{
				BaseTrace.CurrentThreadSettings.DisableTracing();
			}
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00003C7B File Offset: 0x00001E7B
		public DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<BaseSession>(this);
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00003C83 File Offset: 0x00001E83
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00003C98 File Offset: 0x00001E98
		internal static int GetNextToken(byte[] buffer, int beginIndex, int size, out int tokenEnd)
		{
			return BufferParser.GetNextToken(buffer, beginIndex, size, out tokenEnd);
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00003CA3 File Offset: 0x00001EA3
		internal static bool CompareArg(byte[] byteEncodedStrBuf, byte[] buffer, int beginOffset, int count)
		{
			return BufferParser.CompareArg(byteEncodedStrBuf, buffer, beginOffset, count);
		}

		// Token: 0x060000A7 RID: 167
		internal abstract void HandleCommandTooLongError(NetworkConnection nc, byte[] buf, int offset, int amount);

		// Token: 0x060000A8 RID: 168
		internal abstract void HandleIdleTimeout();

		// Token: 0x060000A9 RID: 169 RVA: 0x00003CB0 File Offset: 0x00001EB0
		protected virtual void InternalDispose()
		{
			this.LastActivityTime = ExDateTime.UtcNow;
			lock (this.LockObject)
			{
				ProtocolBaseServices.SessionTracer.TraceDebug(this.SessionId, "InternalDispose has been called!");
				try
				{
					if (this.connectionShutdown != null)
					{
						this.connectionShutdown();
						this.connectionShutdown = null;
					}
				}
				finally
				{
					if (this.disposeTracker != null)
					{
						this.disposeTracker.Dispose();
						this.disposeTracker = null;
					}
					if (this.connection != null)
					{
						this.connection.Dispose();
						this.connection = null;
					}
					if (this.responseItems != null)
					{
						this.responseItems.Dispose();
						this.responseItems = null;
					}
					if (this.stringResponseItemProcessor != null)
					{
						this.stringResponseItemProcessor.Dispose();
						this.stringResponseItemProcessor = null;
					}
					if (this.proxyBuffer != null)
					{
						BaseSession.proxyBufferPool.Release(this.proxyBuffer);
						this.proxyBuffer = null;
					}
					this.disposed = true;
				}
			}
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00003DE8 File Offset: 0x00001FE8
		protected virtual void ReadLineCompleteCallback(IAsyncResult iar)
		{
			if (iar.CompletedSynchronously)
			{
				ThreadPool.QueueUserWorkItem(delegate(object _)
				{
					this.ReadLineCallBackFunction(iar);
				});
				return;
			}
			this.ReadLineCallBackFunction(iar);
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00003E3C File Offset: 0x0000203C
		private void ReadLineCallBackFunction(IAsyncResult iar)
		{
			this.LastActivityTime = ExDateTime.UtcNow;
			this.EnableUserTracing();
			try
			{
				ProtocolBaseServices.SessionTracer.TraceDebug<string>(this.SessionId, "User {0} entering BaseSession.ReadLineCompleteCallback.", this.GetUserNameForLogging());
				NetworkConnection networkConnection = (NetworkConnection)iar.AsyncState;
				byte[] array;
				int num;
				int num2;
				object obj;
				networkConnection.EndReadLine(iar, out array, out num, out num2, out obj);
				if (obj != null && (!(obj is SocketError) || (SocketError)obj != SocketError.MessageSize))
				{
					this.HandleError(obj, networkConnection);
				}
				else if (this.disconnected || this.disposed)
				{
					ProtocolBaseServices.SessionTracer.TraceDebug(this.SessionId, "Session unexpectedly disposed");
				}
				else
				{
					this.EnterCommandProcessing();
					try
					{
						if (obj != null || this.largeReceiveBuffer != null)
						{
							if (num2 + this.largeReceiveBufferLength >= this.maxCommandLength)
							{
								if (this.largeReceiveBuffer != null)
								{
									this.HandleCommandTooLongError(networkConnection, this.largeReceiveBuffer, 0, this.largeReceiveBufferLength);
								}
								else
								{
									this.HandleCommandTooLongError(networkConnection, array, num, num2);
								}
								return;
							}
							if (this.largeReceiveBuffer == null)
							{
								this.largeReceiveBuffer = new byte[this.maxCommandLength];
							}
							Buffer.BlockCopy(array, num, this.largeReceiveBuffer, this.largeReceiveBufferLength, num2);
							this.largeReceiveBufferLength += num2;
							if (obj != null)
							{
								this.BeginRead(networkConnection);
								return;
							}
						}
						if (this.largeReceiveBuffer == null)
						{
							this.HandleCommand(networkConnection, array, num, num2);
						}
						else
						{
							this.HandleCommand(networkConnection, this.largeReceiveBuffer, 0, this.largeReceiveBufferLength);
							this.largeReceiveBuffer = null;
							this.largeReceiveBufferLength = 0;
						}
					}
					finally
					{
						this.LeaveCommandProcessing();
					}
				}
			}
			catch (Exception exception)
			{
				if (!this.CheckNonCriticalException(exception))
				{
					throw;
				}
				this.HandleError(null, iar.AsyncState as NetworkConnection);
			}
			finally
			{
				try
				{
					this.ReadLineCompletePostProcessing();
				}
				finally
				{
					this.DisableUserTracing();
					ProtocolBaseServices.InMemoryTraceOperationCompleted(this.SessionId);
				}
			}
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00004068 File Offset: 0x00002268
		protected virtual void ReadLineCompletePostProcessing()
		{
		}

		// Token: 0x060000AD RID: 173
		protected abstract void HandleCommand(NetworkConnection nc, byte[] buf, int offset, int size);

		// Token: 0x060000AE RID: 174 RVA: 0x0000406A File Offset: 0x0000226A
		protected virtual void BeginRead(NetworkConnection nc)
		{
			this.LastActivityTime = ExDateTime.UtcNow;
			if (nc != null)
			{
				nc.BeginReadLine(new AsyncCallback(this.ReadLineCompleteCallback), nc);
			}
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00004090 File Offset: 0x00002290
		protected virtual int SendNextChunk(NetworkConnection nc)
		{
			this.LastActivityTime = ExDateTime.UtcNow;
			for (;;)
			{
				byte[] buffer;
				int offset;
				int nextChunk = this.GetNextChunk(out buffer, out offset);
				if (nextChunk == 0 || this.disposed)
				{
					break;
				}
				IAsyncResult asyncResult = nc.BeginWrite(buffer, offset, nextChunk, new AsyncCallback(this.WriteCompleteCallback), nc);
				if (!asyncResult.IsCompleted || !asyncResult.CompletedSynchronously)
				{
					return nextChunk;
				}
			}
			return 0;
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x000040E8 File Offset: 0x000022E8
		protected void BeginRead()
		{
			if (this.Connection != null)
			{
				this.BeginRead(this.Connection);
			}
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00004100 File Offset: 0x00002300
		protected virtual void HandleError(object error, NetworkConnection connection)
		{
			ProtocolBaseServices.SessionTracer.TraceDebug<object, NetworkConnection>(this.SessionId, "Handling error \"{0}\" on connection {1}.", error, connection);
			if (error is SocketError && (SocketError)error == SocketError.TimedOut)
			{
				this.HandleIdleTimeout();
				return;
			}
			lock (this.LockObject)
			{
				if (this.processingCommandRefCounter > 0)
				{
					this.disconnected = true;
				}
				else
				{
					this.Dispose();
				}
			}
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00004188 File Offset: 0x00002388
		private int GetNextChunk(out byte[] buffer, out int offset)
		{
			buffer = null;
			offset = 0;
			if (this.disposed)
			{
				return 0;
			}
			int result;
			lock (this.LockObject)
			{
				if (this.disposed)
				{
					result = 0;
				}
				else
				{
					result = this.responseItems.GetNextChunk(this, out buffer, out offset);
				}
			}
			return result;
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x000041F0 File Offset: 0x000023F0
		private void WriteCompleteCallback(IAsyncResult iar)
		{
			this.LastActivityTime = ExDateTime.UtcNow;
			this.EnableUserTracing();
			try
			{
				ProtocolBaseServices.SessionTracer.TraceDebug<string, bool>(this.SessionId, "User {0} entering BaseSession.WriteCompleteCallback. CompletedSynchronously {1}", this.GetUserNameForLogging(), iar.CompletedSynchronously);
				NetworkConnection networkConnection = (NetworkConnection)iar.AsyncState;
				object obj;
				networkConnection.EndWrite(iar, out obj);
				if (obj != null)
				{
					this.HandleError(obj, networkConnection);
				}
				else if (!iar.CompletedSynchronously)
				{
					this.SendNextChunk(networkConnection);
				}
			}
			catch (Exception exception)
			{
				if (!this.CheckNonCriticalException(exception))
				{
					throw;
				}
				this.HandleError(null, iar.AsyncState as NetworkConnection);
			}
			finally
			{
				this.DisableUserTracing();
				ProtocolBaseServices.InMemoryTraceOperationCompleted(this.SessionId);
			}
		}

		// Token: 0x0400003C RID: 60
		private static volatile BufferPool proxyBufferPool;

		// Token: 0x0400003D RID: 61
		private static object proxyBufferPoolLock = new object();

		// Token: 0x0400003E RID: 62
		private readonly long sessionId;

		// Token: 0x0400003F RID: 63
		private NetworkConnection connection;

		// Token: 0x04000040 RID: 64
		private bool negotiatingTls;

		// Token: 0x04000041 RID: 65
		private int processingCommandRefCounter;

		// Token: 0x04000042 RID: 66
		private bool disconnected;

		// Token: 0x04000043 RID: 67
		private BaseSession.ConnectionShutdownDelegate connectionShutdown;

		// Token: 0x04000044 RID: 68
		private ResponseQueue responseItems;

		// Token: 0x04000045 RID: 69
		private bool disposed;

		// Token: 0x04000046 RID: 70
		private int maxCommandLength;

		// Token: 0x04000047 RID: 71
		private byte[] largeReceiveBuffer;

		// Token: 0x04000048 RID: 72
		private int largeReceiveBufferLength;

		// Token: 0x04000049 RID: 73
		private DisposeTracker disposeTracker;

		// Token: 0x0400004A RID: 74
		private StringResponseItemProcessor stringResponseItemProcessor = new StringResponseItemProcessor();

		// Token: 0x0400004B RID: 75
		private EndResponseItem beginReadResponseItem;

		// Token: 0x0400004C RID: 76
		private volatile byte[] proxyBuffer;

		// Token: 0x02000004 RID: 4
		// (Invoke) Token: 0x060000B6 RID: 182
		public delegate void ConnectionShutdownDelegate();

		// Token: 0x02000005 RID: 5
		// (Invoke) Token: 0x060000BA RID: 186
		public delegate void SendCompleteDelegate();
	}
}
