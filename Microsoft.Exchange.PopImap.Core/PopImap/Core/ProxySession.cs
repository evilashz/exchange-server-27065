using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.PopImap.Core
{
	// Token: 0x02000030 RID: 48
	internal abstract class ProxySession : BaseSession
	{
		// Token: 0x060002ED RID: 749 RVA: 0x0000AAC4 File Offset: 0x00008CC4
		public ProxySession(ResponseFactory responseFactory, NetworkConnection networkConnection) : base(networkConnection, ProtocolBaseServices.Service.ConnectionTimeout, 4096)
		{
			this.responseFactory = responseFactory;
			this.incomingSession = responseFactory.Session;
			if (this.responseFactory == null || this.IncomingSession == null || this.IncomingSession.VirtualServer == null)
			{
				return;
			}
			this.IncomingSession.ProxySession = this;
			this.IncomingSession.VirtualServer.Proxy_Connections_Total.Increment();
			this.IncomingSession.VirtualServer.Proxy_Connections_Current.Increment();
			if (responseFactory.ProxyEncryptionType == EncryptionType.TLS)
			{
				this.proxyState = ProxySession.ProxyState.initTls;
				this.EnterReadLoop(base.Connection);
				return;
			}
			if (responseFactory.ProxyEncryptionType == EncryptionType.SSL)
			{
				this.proxyState = ProxySession.ProxyState.startTls;
				this.TransitProxyState(null, 0, 0);
				return;
			}
			if (responseFactory.ProxyEncryptionType == null)
			{
				this.proxyState = ProxySession.ProxyState.initialization;
				this.EnterReadLoop(base.Connection);
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x060002EE RID: 750 RVA: 0x0000ABD1 File Offset: 0x00008DD1
		// (set) Token: 0x060002EF RID: 751 RVA: 0x0000ABDC File Offset: 0x00008DDC
		public ProxySession.ProxyState State
		{
			get
			{
				return this.proxyState;
			}
			set
			{
				lock (this.LockObject)
				{
					if (this.proxyState != ProxySession.ProxyState.failed && value == ProxySession.ProxyState.failed && this.IncomingSession.VirtualServer != null && this.IncomingSession.VirtualServer.Proxy_Connections_Failed != null)
					{
						this.IncomingSession.VirtualServer.Proxy_Connections_Failed.Increment();
					}
					this.proxyState = value;
					if (this.responseFactory != null && this.responseFactory.ConnectionCreated != null && (this.proxyState == ProxySession.ProxyState.completed || this.proxyState == ProxySession.ProxyState.failed))
					{
						lock (this.responseFactory)
						{
							if (this.responseFactory.ConnectionCreated != null && !this.responseFactory.ConnectionCreated.SafeWaitHandle.IsClosed)
							{
								this.responseFactory.ConnectionCreated.Set();
							}
						}
					}
				}
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x060002F0 RID: 752 RVA: 0x0000ACE4 File Offset: 0x00008EE4
		public bool IsConnected
		{
			get
			{
				return this.proxyState == ProxySession.ProxyState.completed;
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x060002F1 RID: 753 RVA: 0x0000ACF0 File Offset: 0x00008EF0
		public ProtocolSession IncomingSession
		{
			get
			{
				return this.incomingSession;
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x060002F2 RID: 754 RVA: 0x0000ACF8 File Offset: 0x00008EF8
		protected override object LockObject
		{
			get
			{
				return this.incomingSession;
			}
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x0000AD00 File Offset: 0x00008F00
		public override string ToString()
		{
			if (base.Connection != null)
			{
				return string.Format("Proxy connection {0} from {1} to {2}", this.GetHashCode(), base.LocalEndPoint, base.RemoteEndPoint);
			}
			return string.Format("Disconnected proxy connection {0}", base.SessionId);
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x0000AD4C File Offset: 0x00008F4C
		public override bool IsUserTraceEnabled()
		{
			return this.incomingSession.IsUserTraceEnabled();
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x0000AD59 File Offset: 0x00008F59
		public override string GetUserNameForLogging()
		{
			return this.IncomingSession.GetUserNameForLogging();
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x0000AD66 File Offset: 0x00008F66
		public override bool CheckNonCriticalException(Exception exception)
		{
			if (this.incomingSession.LightLogSession != null)
			{
				this.incomingSession.LightLogSession.ExceptionCaught = exception;
			}
			return base.CheckNonCriticalException(exception);
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x0000AD90 File Offset: 0x00008F90
		public void TransitProxyState(byte[] buf, int offset, int size)
		{
			lock (this.LockObject)
			{
				this.responseFactory.DoProxyConnect(buf, offset, size, this);
				if (this.State != ProxySession.ProxyState.initialization)
				{
					base.SendToClient(base.BeginReadResponseItem);
				}
			}
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x0000ADF0 File Offset: 0x00008FF0
		internal void Shutdown()
		{
			lock (this.LockObject)
			{
				if (this.IsConnected)
				{
					this.incomingSession.BeginShutdown();
				}
				else
				{
					this.State = ProxySession.ProxyState.failed;
					if (base.Connection != null)
					{
						try
						{
							base.Connection.Shutdown();
							base.Connection.Dispose();
						}
						catch (Exception arg)
						{
							ProtocolBaseServices.SessionTracer.TraceDebug<Exception>(base.SessionId, "Exception caught while closing proxy connection:\r\n{0}.", arg);
						}
						base.Connection = null;
					}
				}
			}
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x0000AE94 File Offset: 0x00009094
		internal override void HandleCommandTooLongError(NetworkConnection nc, byte[] buf, int offset, int amount)
		{
			if (base.Disposed)
			{
				ProtocolBaseServices.SessionTracer.TraceDebug(base.SessionId, "ProxySession.HandleCommandTooLongError. Proxy session disposed");
				return;
			}
			if (!this.IsConnected)
			{
				this.incomingSession.HandleCommandTooLongError(nc, buf, offset, amount);
				return;
			}
			if (!this.incomingSession.SendToClient(new BufferResponseItem(buf, offset, amount)))
			{
				return;
			}
			this.IncomingSession.SendToClient(base.BeginReadResponseItem);
		}

		// Token: 0x060002FA RID: 762 RVA: 0x0000AF01 File Offset: 0x00009101
		internal override void HandleIdleTimeout()
		{
			this.incomingSession.HandleIdleTimeout();
		}

		// Token: 0x060002FB RID: 763 RVA: 0x0000AF0E File Offset: 0x0000910E
		protected override void HandleError(object error, NetworkConnection connection)
		{
			base.HandleError(error, connection);
			this.Shutdown();
		}

		// Token: 0x060002FC RID: 764 RVA: 0x0000AF20 File Offset: 0x00009120
		protected override void HandleCommand(NetworkConnection nc, byte[] buf, int offset, int size)
		{
			if (base.Disposed)
			{
				ProtocolBaseServices.SessionTracer.TraceDebug(base.SessionId, "ProxySession.HandleCommand. Proxy session disposed");
				return;
			}
			if (!this.IsConnected)
			{
				this.TransitProxyState(buf, offset, size);
				return;
			}
			if (this.ForwardToClient(buf, offset, size))
			{
				this.incomingSession.SendBufferAsCommand(buf, offset, size);
				if (this.incomingSession.LightLogSession != null)
				{
					this.incomingSession.LightLogSession.ResponseSize += (long)size;
					this.incomingSession.LightLogSession.FlushProxyTraffic();
				}
			}
			this.IncomingSession.SendToClient(base.BeginReadResponseItem);
		}

		// Token: 0x060002FD RID: 765 RVA: 0x0000AFC4 File Offset: 0x000091C4
		protected virtual void ReadCompleteCallback(IAsyncResult iar)
		{
			try
			{
				ProtocolBaseServices.SessionTracer.TraceDebug<string>(base.SessionId, "User {0} entering ProxySession.ReadCompleteCallback.", this.IncomingSession.GetUserNameForLogging());
				NetworkConnection networkConnection = (NetworkConnection)iar.AsyncState;
				byte[] src;
				int srcOffset;
				int num;
				object obj;
				networkConnection.EndRead(iar, out src, out srcOffset, out num, out obj);
				if (obj != null)
				{
					this.HandleError(obj, networkConnection);
				}
				else if (base.Disposed)
				{
					ProtocolBaseServices.SessionTracer.TraceDebug(base.SessionId, "ProxySession.ReadCompleteCallback. Proxy session disposed");
				}
				else
				{
					byte[] array;
					if (num > base.ProxyBuffer.Length)
					{
						array = new byte[num];
					}
					else
					{
						array = base.ProxyBuffer;
					}
					Buffer.BlockCopy(src, srcOffset, array, 0, num);
					if (this.IncomingSession.SendToClient(new BufferResponseItem(array, 0, num)))
					{
						this.IncomingSession.SendToClient(base.BeginReadResponseItem);
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
				ProtocolBaseServices.InMemoryTraceOperationCompleted(base.SessionId);
			}
		}

		// Token: 0x060002FE RID: 766 RVA: 0x0000B0E4 File Offset: 0x000092E4
		protected override void InternalDispose()
		{
			lock (this.LockObject)
			{
				try
				{
					if (this.IncomingSession != null && this.IncomingSession.VirtualServer != null)
					{
						this.IncomingSession.VirtualServer.Proxy_Connections_Current.Decrement();
					}
				}
				finally
				{
					base.InternalDispose();
				}
			}
		}

		// Token: 0x060002FF RID: 767
		protected abstract bool ForwardToClient(byte[] buf, int offset, int size);

		// Token: 0x04000184 RID: 388
		private ProxySession.ProxyState proxyState;

		// Token: 0x04000185 RID: 389
		private ProtocolSession incomingSession;

		// Token: 0x04000186 RID: 390
		private ResponseFactory responseFactory;

		// Token: 0x02000031 RID: 49
		public enum ProxyState
		{
			// Token: 0x04000188 RID: 392
			failed,
			// Token: 0x04000189 RID: 393
			initTls,
			// Token: 0x0400018A RID: 394
			startTls,
			// Token: 0x0400018B RID: 395
			initialization,
			// Token: 0x0400018C RID: 396
			user,
			// Token: 0x0400018D RID: 397
			waitCapaOk,
			// Token: 0x0400018E RID: 398
			sendAuthPlain,
			// Token: 0x0400018F RID: 399
			sendAuthS2S,
			// Token: 0x04000190 RID: 400
			sendAuthBlob,
			// Token: 0x04000191 RID: 401
			sendS2SAuthBlob,
			// Token: 0x04000192 RID: 402
			authenticated,
			// Token: 0x04000193 RID: 403
			sendXproxy,
			// Token: 0x04000194 RID: 404
			sendXproxy3,
			// Token: 0x04000195 RID: 405
			getNamespace,
			// Token: 0x04000196 RID: 406
			waitingOK,
			// Token: 0x04000197 RID: 407
			completed
		}
	}
}
