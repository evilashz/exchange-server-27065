using System;
using System.Net;
using System.Net.Sockets;

namespace Microsoft.Exchange.PopImap.Core
{
	// Token: 0x02000008 RID: 8
	internal class ConnectionPool
	{
		// Token: 0x060000C9 RID: 201 RVA: 0x000043F0 File Offset: 0x000025F0
		public ConnectionPool(IPEndPoint endpoint, ConnectionPool.ConnectionAcceptedDelegate connectionAccepted)
		{
			if (endpoint == null)
			{
				throw new ArgumentNullException("endpoint");
			}
			if (connectionAccepted == null)
			{
				throw new ArgumentNullException("connectionAccepted");
			}
			this.connectionAcceptedDelegate = connectionAccepted;
			this.acceptSocket = new Socket(endpoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
			this.acceptSocket.Bind(endpoint);
			this.acceptSocket.Listen(int.MaxValue);
			this.acceptSocket.BeginAccept(new AsyncCallback(this.AcceptCallback), null);
		}

		// Token: 0x060000CA RID: 202 RVA: 0x0000446E File Offset: 0x0000266E
		public void Shutdown()
		{
			this.acceptSocket.Close();
		}

		// Token: 0x060000CB RID: 203 RVA: 0x0000447C File Offset: 0x0000267C
		private void AcceptCallback(IAsyncResult iar)
		{
			Socket socket = null;
			try
			{
				socket = this.acceptSocket.EndAccept(iar);
			}
			catch (SocketException arg)
			{
				ProtocolBaseServices.ServerTracer.TraceError<SocketException>(0L, "AcceptCallback(): SocketException: {0} ", arg);
			}
			catch (ObjectDisposedException)
			{
				return;
			}
			if (socket != null)
			{
				this.connectionAcceptedDelegate(socket);
			}
			this.acceptSocket.BeginAccept(new AsyncCallback(this.AcceptCallback), null);
		}

		// Token: 0x04000054 RID: 84
		private Socket acceptSocket;

		// Token: 0x04000055 RID: 85
		private ConnectionPool.ConnectionAcceptedDelegate connectionAcceptedDelegate;

		// Token: 0x02000009 RID: 9
		// (Invoke) Token: 0x060000CD RID: 205
		public delegate void ConnectionAcceptedDelegate(Socket clientsocket);
	}
}
