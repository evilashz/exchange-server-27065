using System;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;

namespace Microsoft.Exchange.EseRepl
{
	// Token: 0x02000039 RID: 57
	internal abstract class TcpChannel
	{
		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060001DE RID: 478 RVA: 0x000078F7 File Offset: 0x00005AF7
		public static Trace Tracer
		{
			get
			{
				return ExTraceGlobals.TcpChannelTracer;
			}
		}

		// Token: 0x060001DF RID: 479 RVA: 0x000078FE File Offset: 0x00005AFE
		internal static void ThrowTimeoutException(string nodeName, string reason)
		{
			throw new NetworkTimeoutException(nodeName, reason);
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x00007907 File Offset: 0x00005B07
		internal void ThrowTimeoutException(string reason)
		{
			TcpChannel.ThrowTimeoutException(this.PartnerNodeName, reason);
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x00007915 File Offset: 0x00005B15
		internal Socket Socket
		{
			get
			{
				return this.m_connection;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060001E2 RID: 482 RVA: 0x0000791D File Offset: 0x00005B1D
		internal IPEndPoint RemoteEndpoint
		{
			get
			{
				return (IPEndPoint)this.m_connection.RemoteEndPoint;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060001E3 RID: 483 RVA: 0x0000792F File Offset: 0x00005B2F
		internal IPEndPoint LocalEndpoint
		{
			get
			{
				return (IPEndPoint)this.m_connection.LocalEndPoint;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060001E4 RID: 484 RVA: 0x00007941 File Offset: 0x00005B41
		// (set) Token: 0x060001E5 RID: 485 RVA: 0x00007949 File Offset: 0x00005B49
		internal string RemoteEndpointString { get; private set; }

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060001E6 RID: 486 RVA: 0x00007952 File Offset: 0x00005B52
		// (set) Token: 0x060001E7 RID: 487 RVA: 0x0000795A File Offset: 0x00005B5A
		internal string LocalEndpointString { get; private set; }

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x00007963 File Offset: 0x00005B63
		// (set) Token: 0x060001E9 RID: 489 RVA: 0x0000796C File Offset: 0x00005B6C
		internal int ReadTimeoutInMs
		{
			get
			{
				return this.m_readTimeoutInMs;
			}
			set
			{
				TcpChannel.Tracer.TraceDebug<int>((long)this.GetHashCode(), "ReadTimeoutInMs={0}", value);
				this.m_readTimeoutInMs = value;
				try
				{
					this.m_authStream.ReadTimeout = this.m_readTimeoutInMs;
				}
				catch (ObjectDisposedException innerException)
				{
					throw new NetworkCancelledException(innerException);
				}
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060001EA RID: 490 RVA: 0x000079C4 File Offset: 0x00005BC4
		// (set) Token: 0x060001EB RID: 491 RVA: 0x000079CC File Offset: 0x00005BCC
		internal int WriteTimeoutInMs
		{
			get
			{
				return this.m_writeTimeoutInMs;
			}
			set
			{
				TcpChannel.Tracer.TraceDebug<int>((long)this.GetHashCode(), "WriteTimeoutInMs={0}", value);
				this.m_writeTimeoutInMs = value;
				try
				{
					this.m_authStream.WriteTimeout = this.m_writeTimeoutInMs;
				}
				catch (ObjectDisposedException innerException)
				{
					throw new NetworkCancelledException(innerException);
				}
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060001EC RID: 492 RVA: 0x00007A24 File Offset: 0x00005C24
		// (set) Token: 0x060001ED RID: 493 RVA: 0x00007A2C File Offset: 0x00005C2C
		internal int BufferSize
		{
			get
			{
				return this.m_bufferSize;
			}
			set
			{
				TcpChannel.Tracer.TraceDebug<int>((long)this.GetHashCode(), "BufferSize={0}", value);
				this.m_bufferSize = value;
				this.m_connection.SendBufferSize = this.m_bufferSize;
				this.m_connection.ReceiveBufferSize = this.m_bufferSize;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060001EE RID: 494 RVA: 0x00007A79 File Offset: 0x00005C79
		// (set) Token: 0x060001EF RID: 495 RVA: 0x00007A81 File Offset: 0x00005C81
		public string PartnerNodeName
		{
			get
			{
				return this.m_partnerNodeName;
			}
			set
			{
				this.m_partnerNodeName = value;
			}
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x00007A8C File Offset: 0x00005C8C
		protected TcpChannel(Socket socket, NegotiateStream stream, int timeout)
		{
			this.m_connection = socket;
			this.m_authStream = stream;
			this.ReadTimeoutInMs = timeout;
			this.WriteTimeoutInMs = timeout;
			this.m_open = true;
			this.RemoteEndpointString = this.RemoteEndpoint.ToString();
			this.LocalEndpointString = this.LocalEndpoint.ToString();
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060001F1 RID: 497 RVA: 0x00007AFA File Offset: 0x00005CFA
		protected NegotiateStream AuthStream
		{
			get
			{
				return this.m_authStream;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060001F2 RID: 498 RVA: 0x00007B02 File Offset: 0x00005D02
		internal Stream Stream
		{
			get
			{
				return this.m_authStream;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x00007B0A File Offset: 0x00005D0A
		internal bool IsEncrypted
		{
			get
			{
				return this.m_authStream.IsEncrypted;
			}
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x00007B18 File Offset: 0x00005D18
		public void Close()
		{
			lock (this)
			{
				if (this.m_open)
				{
					this.m_open = false;
					TcpChannel.Tracer.TraceDebug((long)this.GetHashCode(), "Closing channel");
					if (Parameters.CurrentValues.LogDiagnosticNetworkEvents)
					{
						if (this is TcpClientChannel)
						{
							ReplayCrimsonEvents.ClientNetworkConnectionClosed.Log<string, string, string>(this.PartnerNodeName, this.RemoteEndpointString, this.LocalEndpointString);
						}
						else
						{
							ReplayCrimsonEvents.ServerNetworkConnectionClosed.Log<string, string, string>(this.PartnerNodeName, this.RemoteEndpointString, this.LocalEndpointString);
						}
					}
					this.Shutdown(true);
					NegotiateStream authStream = this.m_authStream;
					if (authStream != null)
					{
						authStream.Dispose();
					}
				}
			}
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00007BDC File Offset: 0x00005DDC
		public void Abort()
		{
			lock (this)
			{
				if (this.m_open)
				{
					if (!this.m_aborted)
					{
						this.m_aborted = true;
						TcpChannel.Tracer.TraceDebug((long)this.GetHashCode(), "Aborting channel");
						this.Shutdown(true);
					}
				}
			}
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x00007C4C File Offset: 0x00005E4C
		private void Shutdown(bool performClose)
		{
			Exception ex = null;
			try
			{
				this.m_connection.Shutdown(SocketShutdown.Both);
			}
			catch (SocketException ex2)
			{
				ex = ex2;
			}
			catch (ObjectDisposedException ex3)
			{
				performClose = false;
				ex = ex3;
			}
			if (ex != null)
			{
				TcpChannel.Tracer.TraceError<Exception>((long)this.GetHashCode(), "Shutdown got exception: {0}", ex);
			}
			if (performClose)
			{
				ex = null;
				try
				{
					this.m_connection.Close();
				}
				catch (SocketException ex4)
				{
					ex = ex4;
				}
				catch (ObjectDisposedException ex5)
				{
					ex = ex5;
				}
				if (ex != null)
				{
					TcpChannel.Tracer.TraceError<Exception>((long)this.GetHashCode(), "Close got exception: {0}", ex);
				}
			}
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x00007CFC File Offset: 0x00005EFC
		public int Read(byte[] buffer, int offset, int maxSize)
		{
			return this.m_authStream.Read(buffer, offset, maxSize);
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x00007D0C File Offset: 0x00005F0C
		public int TryReadChunk(byte[] buf, int bufOffset, int totalSize)
		{
			int i = totalSize;
			while (i > 0)
			{
				int num = this.Read(buf, bufOffset, i);
				i -= num;
				bufOffset += num;
				if (i > 0 && num == 0)
				{
					TcpChannel.Tracer.TraceError(0L, "Connection unexpectedly closed");
					break;
				}
			}
			return totalSize - i;
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x00007D51 File Offset: 0x00005F51
		public void ReadChunk(byte[] buf, int bufOffset, int totalSize)
		{
			if (totalSize != this.TryReadChunk(buf, bufOffset, totalSize))
			{
				throw new NetworkEndOfDataException(this.PartnerNodeName, Strings.NetworkReadEOF);
			}
		}

		// Token: 0x060001FA RID: 506 RVA: 0x00007D75 File Offset: 0x00005F75
		public void Write(byte[] buffer, int offset, int size)
		{
			this.m_authStream.Write(buffer, offset, size);
		}

		// Token: 0x060001FB RID: 507 RVA: 0x00007D85 File Offset: 0x00005F85
		public void Write(byte[] buffer, int offset, int size, bool flush)
		{
			this.m_authStream.Write(buffer, offset, size);
			if (flush)
			{
				this.m_authStream.Flush();
			}
		}

		// Token: 0x060001FC RID: 508 RVA: 0x00007DA4 File Offset: 0x00005FA4
		public void WriteAndFlush(byte[] buffer, int offset, int size)
		{
			this.Write(buffer, offset, size);
			this.m_authStream.Flush();
		}

		// Token: 0x060001FD RID: 509 RVA: 0x00007DBA File Offset: 0x00005FBA
		public void Flush()
		{
			this.m_authStream.Flush();
		}

		// Token: 0x060001FE RID: 510 RVA: 0x00007DC8 File Offset: 0x00005FC8
		public static void SetTcpKeepAlive(Socket socket, uint keepaliveTimeInMsec, uint keepaliveIntervalInMsec)
		{
			int num = 4;
			byte[] array = new byte[num * 3];
			BitConverter.GetBytes(keepaliveTimeInMsec).CopyTo(array, 0);
			BitConverter.GetBytes(keepaliveTimeInMsec).CopyTo(array, num);
			BitConverter.GetBytes(keepaliveIntervalInMsec).CopyTo(array, 2 * num);
			socket.IOControl((IOControlCode)((ulong)-1744830460), array, null);
		}

		// Token: 0x04000128 RID: 296
		protected bool m_open;

		// Token: 0x04000129 RID: 297
		protected bool m_aborted;

		// Token: 0x0400012A RID: 298
		protected Socket m_connection;

		// Token: 0x0400012B RID: 299
		protected NegotiateStream m_authStream;

		// Token: 0x0400012C RID: 300
		protected int m_readTimeoutInMs = 15000;

		// Token: 0x0400012D RID: 301
		protected int m_writeTimeoutInMs = 15000;

		// Token: 0x0400012E RID: 302
		protected int m_bufferSize;

		// Token: 0x0400012F RID: 303
		protected string m_partnerNodeName;
	}
}
