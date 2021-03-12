using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Transport.Agent.Hygiene;

namespace Microsoft.Exchange.Transport.Agent.ProtocolAnalysis.Background
{
	// Token: 0x02000054 RID: 84
	internal class TcpConnection : TransportConnection, IDisposable
	{
		// Token: 0x06000289 RID: 649 RVA: 0x00010E34 File Offset: 0x0000F034
		public override void AsyncConnect(IPEndPoint remoteEndpoint, TcpConnection tcpCxn, NetworkCredential authInfo)
		{
			try
			{
				this.socket.BeginConnect(remoteEndpoint, new AsyncCallback(this.AsyncConnectCallback), this);
			}
			catch (SocketException)
			{
				this.Shutdown(true);
			}
		}

		// Token: 0x0600028A RID: 650 RVA: 0x00010E78 File Offset: 0x0000F078
		public TcpConnection(ProxyChain proxyChain) : base(proxyChain)
		{
			this.socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			Interlocked.Increment(ref ProtocolAnalysisBgAgent.NumSockets);
			this.m_fReadPending = false;
			this.m_fWritePending = false;
			this.m_fClosing = false;
			this.m_fNotifyNeeded = false;
			this.m_fRemoteReachable = false;
			this.writeBuffer = new byte[512];
			this.writeBufferWritePos = 0;
			this.writeBufferReadPos = 0;
			this.readBuffer = new byte[512];
			this.readBufferWritePos = 0;
		}

		// Token: 0x0600028B RID: 651 RVA: 0x00010F08 File Offset: 0x0000F108
		public void Dispose()
		{
			if (this.socket != null)
			{
				this.Shutdown(false);
			}
		}

		// Token: 0x0600028C RID: 652 RVA: 0x00010F19 File Offset: 0x0000F119
		public override string ToString()
		{
			if (this.socket != null && this.socket.Connected)
			{
				return this.socket.RemoteEndPoint.ToString();
			}
			return "not connected";
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x0600028D RID: 653 RVA: 0x00010F46 File Offset: 0x0000F146
		public bool RemoteReachable
		{
			get
			{
				return this.m_fRemoteReachable;
			}
		}

		// Token: 0x0600028E RID: 654 RVA: 0x00010F50 File Offset: 0x0000F150
		protected void ExpandReadBuffer()
		{
			if (this.readBuffer.Length < 1024)
			{
				byte[] array = new byte[this.readBuffer.Length * 2];
				this.readBuffer.CopyTo(array, 0);
				this.readBuffer = array;
			}
		}

		// Token: 0x0600028F RID: 655 RVA: 0x00010F90 File Offset: 0x0000F190
		public void Shutdown(bool isNotify)
		{
			bool flag = false;
			lock (this.syncObject)
			{
				if (!this.m_fClosing)
				{
					this.m_fClosing = true;
					this.m_fNotifyNeeded = isNotify;
				}
				if (this.socket != null)
				{
					try
					{
						this.socket.Shutdown(SocketShutdown.Both);
					}
					catch (SocketException)
					{
					}
					if (!this.m_fReadPending && !this.m_fWritePending)
					{
						this.socket.Close();
						this.socket = null;
						Interlocked.Decrement(ref ProtocolAnalysisBgAgent.NumSockets);
						flag = true;
					}
				}
			}
			if (flag && this.m_fNotifyNeeded)
			{
				base.ProxyChain.OnDisconnected();
			}
		}

		// Token: 0x06000290 RID: 656 RVA: 0x0001104C File Offset: 0x0000F24C
		private void ExpandWriteBuffer(int len)
		{
			int num = Math.Max(len, 512);
			byte[] array = new byte[this.writeBuffer.Length + num];
			this.writeBuffer.CopyTo(array, 0);
			this.writeBuffer = array;
		}

		// Token: 0x06000291 RID: 657 RVA: 0x0001108C File Offset: 0x0000F28C
		private void AsyncConnectCallback(IAsyncResult ar)
		{
			this.m_fRemoteReachable = true;
			if (this.m_fClosing)
			{
				return;
			}
			try
			{
				this.socket.EndConnect(ar);
			}
			catch (SocketException)
			{
				this.Shutdown(true);
				return;
			}
			this.AsyncRead();
			base.ProxyChain.OnConnected(null, 0, 0);
		}

		// Token: 0x06000292 RID: 658 RVA: 0x000110E8 File Offset: 0x0000F2E8
		public void SendMessage(byte[] data, int start, int len)
		{
			lock (this.syncObject)
			{
				if (this.m_fClosing)
				{
					throw new AtsException(AgentStrings.WritingDisallowedOnClosedConnection);
				}
				if (this.writeBufferWritePos + len > this.writeBuffer.Length)
				{
					this.ExpandWriteBuffer(len);
				}
				Array.Copy(data, start, this.writeBuffer, this.writeBufferWritePos, len);
				this.writeBufferWritePos += len;
				if (!this.m_fWritePending)
				{
					this.AsyncWrite(this.writeBuffer, this.writeBufferReadPos, this.writeBufferWritePos - this.writeBufferReadPos);
				}
			}
		}

		// Token: 0x06000293 RID: 659 RVA: 0x00011198 File Offset: 0x0000F398
		public void SendString(string data)
		{
			byte[] bytes = Encoding.ASCII.GetBytes(data);
			this.SendMessage(bytes, 0, bytes.Length);
		}

		// Token: 0x06000294 RID: 660 RVA: 0x000111BC File Offset: 0x0000F3BC
		public void SendByte(byte data)
		{
			this.SendMessage(new byte[]
			{
				data
			}, 0, 1);
		}

		// Token: 0x06000295 RID: 661 RVA: 0x000111E0 File Offset: 0x0000F3E0
		private void AsyncWrite(byte[] data, int start, int len)
		{
			if (this.m_fClosing)
			{
				return;
			}
			this.m_fWritePending = true;
			try
			{
				this.socket.BeginSend(data, start, len, SocketFlags.None, new AsyncCallback(this.AsyncWriteCallback), this);
			}
			catch (SocketException)
			{
				this.m_fWritePending = false;
				this.Shutdown(true);
			}
		}

		// Token: 0x06000296 RID: 662 RVA: 0x00011240 File Offset: 0x0000F440
		private void AsyncWriteCallback(IAsyncResult ar)
		{
			lock (this.syncObject)
			{
				int num = 0;
				try
				{
					this.m_fWritePending = false;
					num = this.socket.EndSend(ar);
				}
				catch (SocketException)
				{
					this.Shutdown(true);
					return;
				}
				this.writeBufferReadPos += num;
				if (this.m_fClosing)
				{
					this.Shutdown(true);
				}
				else
				{
					Array.Copy(this.writeBuffer, this.writeBufferReadPos, this.writeBuffer, 0, this.writeBufferWritePos - this.writeBufferReadPos);
					this.writeBufferWritePos -= this.writeBufferReadPos;
					this.writeBufferReadPos = 0;
					if (this.writeBufferReadPos < this.writeBufferWritePos)
					{
						this.AsyncWrite(this.writeBuffer, this.writeBufferReadPos, this.writeBufferWritePos - this.writeBufferReadPos);
					}
				}
			}
		}

		// Token: 0x06000297 RID: 663 RVA: 0x00011338 File Offset: 0x0000F538
		private void AsyncRead()
		{
			if (this.m_fClosing)
			{
				return;
			}
			int num = this.readBuffer.Length - this.readBufferWritePos;
			if (num < 1)
			{
				this.ExpandReadBuffer();
			}
			try
			{
				this.m_fReadPending = true;
				this.socket.BeginReceive(this.readBuffer, this.readBufferWritePos, this.readBuffer.Length - this.readBufferWritePos - 1, SocketFlags.None, new AsyncCallback(this.AsyncReadCallback), this);
			}
			catch (SocketException)
			{
				this.m_fReadPending = false;
				this.Shutdown(true);
			}
		}

		// Token: 0x06000298 RID: 664 RVA: 0x000113CC File Offset: 0x0000F5CC
		private void AsyncReadCallback(IAsyncResult ar)
		{
			int num;
			lock (this.syncObject)
			{
				try
				{
					this.m_fReadPending = false;
					num = this.socket.EndReceive(ar);
					if (num == 0)
					{
						this.Shutdown(true);
						return;
					}
				}
				catch (SocketException)
				{
					this.Shutdown(true);
					return;
				}
			}
			this.readBufferWritePos += num;
			this.OnDataAvailable();
			if (this.m_fClosing)
			{
				this.Shutdown(true);
				return;
			}
			this.AsyncRead();
		}

		// Token: 0x06000299 RID: 665 RVA: 0x00011468 File Offset: 0x0000F668
		private void OnDataAvailable()
		{
			int num = 0;
			if (base.DataCxn != null && this.readBufferWritePos > num)
			{
				int num2 = base.DataCxn.OnDataReceived(this.readBuffer, num, this.readBufferWritePos - num);
				num += num2;
				Array.Copy(this.readBuffer, num, this.readBuffer, 0, this.readBufferWritePos - num);
				this.readBufferWritePos -= num;
			}
		}

		// Token: 0x040001D8 RID: 472
		private const int MinExpandSize = 512;

		// Token: 0x040001D9 RID: 473
		private const int MaxBufferSize = 1024;

		// Token: 0x040001DA RID: 474
		private Socket socket;

		// Token: 0x040001DB RID: 475
		private byte[] readBuffer;

		// Token: 0x040001DC RID: 476
		private int readBufferWritePos;

		// Token: 0x040001DD RID: 477
		private byte[] writeBuffer;

		// Token: 0x040001DE RID: 478
		private int writeBufferWritePos;

		// Token: 0x040001DF RID: 479
		private int writeBufferReadPos;

		// Token: 0x040001E0 RID: 480
		private bool m_fWritePending;

		// Token: 0x040001E1 RID: 481
		private bool m_fReadPending;

		// Token: 0x040001E2 RID: 482
		private bool m_fClosing;

		// Token: 0x040001E3 RID: 483
		private bool m_fNotifyNeeded;

		// Token: 0x040001E4 RID: 484
		private bool m_fRemoteReachable;

		// Token: 0x040001E5 RID: 485
		private object syncObject = new object();
	}
}
