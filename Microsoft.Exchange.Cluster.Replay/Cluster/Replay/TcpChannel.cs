using System;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000269 RID: 617
	internal abstract class TcpChannel
	{
		// Token: 0x170006A9 RID: 1705
		// (get) Token: 0x06001818 RID: 6168 RVA: 0x000637B4 File Offset: 0x000619B4
		public static Trace Tracer
		{
			get
			{
				return ExTraceGlobals.TcpChannelTracer;
			}
		}

		// Token: 0x06001819 RID: 6169 RVA: 0x000637BB File Offset: 0x000619BB
		internal static int GetDefaultTimeoutInMs()
		{
			return RegistryParameters.LogShipTimeoutInMsec;
		}

		// Token: 0x0600181A RID: 6170 RVA: 0x000637C2 File Offset: 0x000619C2
		internal static void ThrowTimeoutException(string nodeName, string reason)
		{
			throw new NetworkTimeoutException(nodeName, reason);
		}

		// Token: 0x0600181B RID: 6171 RVA: 0x000637CB File Offset: 0x000619CB
		internal void ThrowTimeoutException(string reason)
		{
			TcpChannel.ThrowTimeoutException(this.PartnerNodeName, reason);
		}

		// Token: 0x170006AA RID: 1706
		// (get) Token: 0x0600181C RID: 6172 RVA: 0x000637D9 File Offset: 0x000619D9
		internal Socket Socket
		{
			get
			{
				return this.m_connection;
			}
		}

		// Token: 0x170006AB RID: 1707
		// (get) Token: 0x0600181D RID: 6173 RVA: 0x000637E1 File Offset: 0x000619E1
		internal IPEndPoint RemoteEndpoint
		{
			get
			{
				return (IPEndPoint)this.m_connection.RemoteEndPoint;
			}
		}

		// Token: 0x170006AC RID: 1708
		// (get) Token: 0x0600181E RID: 6174 RVA: 0x000637F3 File Offset: 0x000619F3
		internal IPEndPoint LocalEndpoint
		{
			get
			{
				return (IPEndPoint)this.m_connection.LocalEndPoint;
			}
		}

		// Token: 0x170006AD RID: 1709
		// (get) Token: 0x0600181F RID: 6175 RVA: 0x00063805 File Offset: 0x00061A05
		// (set) Token: 0x06001820 RID: 6176 RVA: 0x0006380D File Offset: 0x00061A0D
		internal string RemoteEndpointString { get; private set; }

		// Token: 0x170006AE RID: 1710
		// (get) Token: 0x06001821 RID: 6177 RVA: 0x00063816 File Offset: 0x00061A16
		// (set) Token: 0x06001822 RID: 6178 RVA: 0x0006381E File Offset: 0x00061A1E
		internal string LocalEndpointString { get; private set; }

		// Token: 0x170006AF RID: 1711
		// (get) Token: 0x06001823 RID: 6179 RVA: 0x00063827 File Offset: 0x00061A27
		// (set) Token: 0x06001824 RID: 6180 RVA: 0x0006382F File Offset: 0x00061A2F
		internal DateTime IdleSince { get; private set; }

		// Token: 0x170006B0 RID: 1712
		// (get) Token: 0x06001825 RID: 6181 RVA: 0x00063838 File Offset: 0x00061A38
		// (set) Token: 0x06001826 RID: 6182 RVA: 0x00063840 File Offset: 0x00061A40
		internal bool IsIdle { get; private set; }

		// Token: 0x170006B1 RID: 1713
		// (get) Token: 0x06001827 RID: 6183 RVA: 0x00063849 File Offset: 0x00061A49
		// (set) Token: 0x06001828 RID: 6184 RVA: 0x00063851 File Offset: 0x00061A51
		internal TimeSpan IdleTimeout { get; set; }

		// Token: 0x06001829 RID: 6185 RVA: 0x0006385A File Offset: 0x00061A5A
		internal void SetIdle()
		{
			this.IdleSince = DateTime.UtcNow;
			this.IsIdle = true;
		}

		// Token: 0x0600182A RID: 6186 RVA: 0x0006386E File Offset: 0x00061A6E
		internal void ClearIdle()
		{
			this.IsIdle = false;
		}

		// Token: 0x0600182B RID: 6187 RVA: 0x00063878 File Offset: 0x00061A78
		internal bool CancelIfIdleTooLong()
		{
			if (this.IsIdle && this.m_open && !this.m_aborted)
			{
				TimeSpan t = DateTime.UtcNow - this.IdleSince;
				if (t > this.IdleTimeout)
				{
					ReplayCrimsonEvents.ServerNetworkConnectionTimeout.Log<string, string, string, string, string>(this.PartnerNodeName, this.RemoteEndpointString, this.LocalEndpointString, this.IdleSince.ToString("u"), t.ToString());
					this.Abort();
					return true;
				}
			}
			return false;
		}

		// Token: 0x170006B2 RID: 1714
		// (get) Token: 0x0600182C RID: 6188 RVA: 0x00063901 File Offset: 0x00061B01
		// (set) Token: 0x0600182D RID: 6189 RVA: 0x0006390C File Offset: 0x00061B0C
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

		// Token: 0x170006B3 RID: 1715
		// (get) Token: 0x0600182E RID: 6190 RVA: 0x00063964 File Offset: 0x00061B64
		// (set) Token: 0x0600182F RID: 6191 RVA: 0x0006396C File Offset: 0x00061B6C
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

		// Token: 0x170006B4 RID: 1716
		// (get) Token: 0x06001830 RID: 6192 RVA: 0x000639C4 File Offset: 0x00061BC4
		// (set) Token: 0x06001831 RID: 6193 RVA: 0x000639CC File Offset: 0x00061BCC
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

		// Token: 0x170006B5 RID: 1717
		// (get) Token: 0x06001832 RID: 6194 RVA: 0x00063A19 File Offset: 0x00061C19
		// (set) Token: 0x06001833 RID: 6195 RVA: 0x00063A21 File Offset: 0x00061C21
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

		// Token: 0x06001834 RID: 6196 RVA: 0x00063A2C File Offset: 0x00061C2C
		protected TcpChannel(Socket socket, NegotiateStream stream, int timeout, TimeSpan idleLimit)
		{
			this.m_connection = socket;
			this.m_authStream = stream;
			this.ReadTimeoutInMs = timeout;
			this.WriteTimeoutInMs = timeout;
			this.m_open = true;
			this.RemoteEndpointString = this.RemoteEndpoint.ToString();
			this.LocalEndpointString = this.LocalEndpoint.ToString();
			this.IdleTimeout = idleLimit;
		}

		// Token: 0x170006B6 RID: 1718
		// (get) Token: 0x06001835 RID: 6197 RVA: 0x00063AA2 File Offset: 0x00061CA2
		protected NegotiateStream AuthStream
		{
			get
			{
				return this.m_authStream;
			}
		}

		// Token: 0x170006B7 RID: 1719
		// (get) Token: 0x06001836 RID: 6198 RVA: 0x00063AAA File Offset: 0x00061CAA
		internal Stream Stream
		{
			get
			{
				return this.m_authStream;
			}
		}

		// Token: 0x170006B8 RID: 1720
		// (get) Token: 0x06001837 RID: 6199 RVA: 0x00063AB2 File Offset: 0x00061CB2
		internal bool IsEncrypted
		{
			get
			{
				return this.m_authStream.IsEncrypted;
			}
		}

		// Token: 0x06001838 RID: 6200 RVA: 0x00063AC0 File Offset: 0x00061CC0
		public void Close()
		{
			lock (this)
			{
				if (this.m_open)
				{
					this.m_open = false;
					ExTraceGlobals.TcpChannelTracer.TraceDebug((long)this.GetHashCode(), "Closing channel");
					if (this is TcpClientChannel)
					{
						ReplayCrimsonEvents.ClientNetworkConnectionClosed.Log<string, string, string>(this.PartnerNodeName, this.RemoteEndpointString, this.LocalEndpointString);
					}
					else
					{
						ReplayCrimsonEvents.ServerNetworkConnectionClosed.Log<string, string, string>(this.PartnerNodeName, this.RemoteEndpointString, this.LocalEndpointString);
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

		// Token: 0x06001839 RID: 6201 RVA: 0x00063B78 File Offset: 0x00061D78
		public void Abort()
		{
			lock (this)
			{
				if (this.m_open)
				{
					if (!this.m_aborted)
					{
						this.m_aborted = true;
						ExTraceGlobals.TcpChannelTracer.TraceDebug((long)this.GetHashCode(), "Aborting channel");
						this.Shutdown(true);
					}
				}
			}
		}

		// Token: 0x0600183A RID: 6202 RVA: 0x00063BE8 File Offset: 0x00061DE8
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
				ExTraceGlobals.TcpChannelTracer.TraceError<Exception>((long)this.GetHashCode(), "Shutdown got exception: {0}", ex);
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
					ExTraceGlobals.TcpChannelTracer.TraceError<Exception>((long)this.GetHashCode(), "Close got exception: {0}", ex);
				}
			}
		}

		// Token: 0x0600183B RID: 6203 RVA: 0x00063C98 File Offset: 0x00061E98
		public int Read(byte[] buffer, int offset, int maxSize)
		{
			return this.m_authStream.Read(buffer, offset, maxSize);
		}

		// Token: 0x0600183C RID: 6204 RVA: 0x00063CA8 File Offset: 0x00061EA8
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
					ExTraceGlobals.TcpChannelTracer.TraceError(0L, "Connection unexpectedly closed");
					break;
				}
			}
			return totalSize - i;
		}

		// Token: 0x0600183D RID: 6205 RVA: 0x00063CED File Offset: 0x00061EED
		public void ReadChunk(byte[] buf, int bufOffset, int totalSize)
		{
			if (totalSize != this.TryReadChunk(buf, bufOffset, totalSize))
			{
				throw new NetworkEndOfDataException(this.PartnerNodeName, ReplayStrings.NetworkReadEOF);
			}
		}

		// Token: 0x0600183E RID: 6206 RVA: 0x00063D11 File Offset: 0x00061F11
		public void Write(byte[] buffer, int offset, int size)
		{
			this.m_authStream.Write(buffer, offset, size);
		}

		// Token: 0x0600183F RID: 6207 RVA: 0x00063D21 File Offset: 0x00061F21
		public void Write(byte[] buffer, int offset, int size, bool flush)
		{
			this.m_authStream.Write(buffer, offset, size);
			if (flush)
			{
				this.m_authStream.Flush();
			}
		}

		// Token: 0x06001840 RID: 6208 RVA: 0x00063D40 File Offset: 0x00061F40
		public void WriteAndFlush(byte[] buffer, int offset, int size)
		{
			this.Write(buffer, offset, size);
			this.m_authStream.Flush();
		}

		// Token: 0x06001841 RID: 6209 RVA: 0x00063D56 File Offset: 0x00061F56
		public void Flush()
		{
			this.m_authStream.Flush();
		}

		// Token: 0x06001842 RID: 6210 RVA: 0x00063D64 File Offset: 0x00061F64
		public static void SetTcpKeepAlive(Socket socket, uint keepaliveTimeInMsec, uint keepaliveIntervalInMsec)
		{
			int num = 4;
			byte[] array = new byte[num * 3];
			BitConverter.GetBytes(keepaliveTimeInMsec).CopyTo(array, 0);
			BitConverter.GetBytes(keepaliveTimeInMsec).CopyTo(array, num);
			BitConverter.GetBytes(keepaliveIntervalInMsec).CopyTo(array, 2 * num);
			socket.IOControl((IOControlCode)((ulong)-1744830460), array, null);
		}

		// Token: 0x0400099E RID: 2462
		protected bool m_open;

		// Token: 0x0400099F RID: 2463
		protected bool m_aborted;

		// Token: 0x040009A0 RID: 2464
		protected Socket m_connection;

		// Token: 0x040009A1 RID: 2465
		protected NegotiateStream m_authStream;

		// Token: 0x040009A2 RID: 2466
		protected int m_readTimeoutInMs = 15000;

		// Token: 0x040009A3 RID: 2467
		protected int m_writeTimeoutInMs = 15000;

		// Token: 0x040009A4 RID: 2468
		protected int m_bufferSize;

		// Token: 0x040009A5 RID: 2469
		protected string m_partnerNodeName;
	}
}
