using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Net;
using Microsoft.Exchange.Security;

namespace Microsoft.Exchange.Net
{
	// Token: 0x02000BD7 RID: 3031
	internal class NetworkConnection : DisposeTrackableBase, INetworkConnection, IDisposable
	{
		// Token: 0x060041C7 RID: 16839 RVA: 0x000AE52C File Offset: 0x000AC72C
		public NetworkConnection(Socket socket, int bufferSize = 4096)
		{
			ArgumentValidator.ThrowIfNull("socket", socket);
			this.ServerTlsProtocols = NetworkConnection.DefaultServerTlsProtocols;
			this.ClientTlsProtocols = NetworkConnection.DefaultClientTlsProtocols;
			this.socket = socket;
			this.recvBuffer = new NetworkBuffer(bufferSize);
			this.connectionId = Interlocked.Increment(ref NetworkConnection.lastConnectionId);
			try
			{
				this.localEndPoint = (socket.LocalEndPoint as IPEndPoint);
				this.remoteEndPoint = (socket.RemoteEndPoint as IPEndPoint);
			}
			catch (SocketException)
			{
			}
			catch (ObjectDisposedException)
			{
			}
			if (this.localEndPoint == null)
			{
				this.localEndPoint = NetworkConnection.nullEndPoint;
			}
			if (this.remoteEndPoint == null)
			{
				this.remoteEndPoint = NetworkConnection.nullEndPoint;
			}
			NetworkTimer.Add(this);
		}

		// Token: 0x1700105B RID: 4187
		// (get) Token: 0x060041C8 RID: 16840 RVA: 0x000AE64C File Offset: 0x000AC84C
		// (set) Token: 0x060041C9 RID: 16841 RVA: 0x000AE654 File Offset: 0x000AC854
		public SchannelProtocols ServerTlsProtocols { get; set; }

		// Token: 0x1700105C RID: 4188
		// (get) Token: 0x060041CA RID: 16842 RVA: 0x000AE65D File Offset: 0x000AC85D
		public static SchannelProtocols DefaultServerTlsProtocols
		{
			get
			{
				return SspiContext.DefaultServerTlsProtocols;
			}
		}

		// Token: 0x1700105D RID: 4189
		// (get) Token: 0x060041CB RID: 16843 RVA: 0x000AE664 File Offset: 0x000AC864
		// (set) Token: 0x060041CC RID: 16844 RVA: 0x000AE66C File Offset: 0x000AC86C
		public SchannelProtocols ClientTlsProtocols { get; set; }

		// Token: 0x1700105E RID: 4190
		// (get) Token: 0x060041CD RID: 16845 RVA: 0x000AE675 File Offset: 0x000AC875
		public static SchannelProtocols DefaultClientTlsProtocols
		{
			get
			{
				return SspiContext.DefaultClientTlsProtocols;
			}
		}

		// Token: 0x1700105F RID: 4191
		// (get) Token: 0x060041CE RID: 16846 RVA: 0x000AE67C File Offset: 0x000AC87C
		public ChannelBindingToken ChannelBindingToken
		{
			get
			{
				return this.channelBindingToken;
			}
		}

		// Token: 0x17001060 RID: 4192
		// (get) Token: 0x060041CF RID: 16847 RVA: 0x000AE684 File Offset: 0x000AC884
		public long ConnectionId
		{
			get
			{
				return this.connectionId;
			}
		}

		// Token: 0x17001061 RID: 4193
		// (get) Token: 0x060041D0 RID: 16848 RVA: 0x000AE68C File Offset: 0x000AC88C
		// (set) Token: 0x060041D1 RID: 16849 RVA: 0x000AE69C File Offset: 0x000AC89C
		public int ReceiveTimeout
		{
			get
			{
				return (int)(this.recvTimeoutPeriod / 10000000L);
			}
			set
			{
				this.recvTimeoutPeriod = (long)value * 10000000L;
			}
		}

		// Token: 0x17001062 RID: 4194
		// (get) Token: 0x060041D2 RID: 16850 RVA: 0x000AE6AD File Offset: 0x000AC8AD
		// (set) Token: 0x060041D3 RID: 16851 RVA: 0x000AE6BD File Offset: 0x000AC8BD
		public int SendTimeout
		{
			get
			{
				return (int)(this.sendTimeoutPeriod / 10000000L);
			}
			set
			{
				this.sendTimeoutPeriod = (long)value * 10000000L;
			}
		}

		// Token: 0x17001063 RID: 4195
		// (set) Token: 0x060041D4 RID: 16852 RVA: 0x000AE6CE File Offset: 0x000AC8CE
		public int Timeout
		{
			set
			{
				this.SendTimeout = value;
				this.ReceiveTimeout = value;
			}
		}

		// Token: 0x17001064 RID: 4196
		// (get) Token: 0x060041D5 RID: 16853 RVA: 0x000AE6DE File Offset: 0x000AC8DE
		public IPEndPoint LocalEndPoint
		{
			get
			{
				return this.localEndPoint;
			}
		}

		// Token: 0x17001065 RID: 4197
		// (get) Token: 0x060041D6 RID: 16854 RVA: 0x000AE6E6 File Offset: 0x000AC8E6
		public IPEndPoint RemoteEndPoint
		{
			get
			{
				return this.remoteEndPoint;
			}
		}

		// Token: 0x17001066 RID: 4198
		// (get) Token: 0x060041D7 RID: 16855 RVA: 0x000AE6EE File Offset: 0x000AC8EE
		// (set) Token: 0x060041D8 RID: 16856 RVA: 0x000AE6F6 File Offset: 0x000AC8F6
		public int MaxLineLength
		{
			get
			{
				return this.maxLineLength;
			}
			set
			{
				if (value <= 0 || value > 4096)
				{
					throw new ArgumentException(NetException.OutOfRange, "value");
				}
				this.maxLineLength = value;
			}
		}

		// Token: 0x17001067 RID: 4199
		// (get) Token: 0x060041D9 RID: 16857 RVA: 0x000AE720 File Offset: 0x000AC920
		public long BytesReceived
		{
			get
			{
				return this.bytesReceived;
			}
		}

		// Token: 0x17001068 RID: 4200
		// (get) Token: 0x060041DA RID: 16858 RVA: 0x000AE728 File Offset: 0x000AC928
		public long BytesSent
		{
			get
			{
				return this.bytesSent;
			}
		}

		// Token: 0x17001069 RID: 4201
		// (get) Token: 0x060041DB RID: 16859 RVA: 0x000AE730 File Offset: 0x000AC930
		public bool IsDataAvailable
		{
			get
			{
				return this.recvBuffer.Remaining > 0;
			}
		}

		// Token: 0x1700106A RID: 4202
		// (get) Token: 0x060041DC RID: 16860 RVA: 0x000AE740 File Offset: 0x000AC940
		public bool IsLineAvailable
		{
			get
			{
				bool flag;
				return this.recvBuffer.FindLine(this.maxLineLength, out flag) != -1;
			}
		}

		// Token: 0x1700106B RID: 4203
		// (get) Token: 0x060041DD RID: 16861 RVA: 0x000AE766 File Offset: 0x000AC966
		public bool IsTls
		{
			get
			{
				return this.sspiContext != null && this.sspiContext.State == ContextState.NegotiationComplete;
			}
		}

		// Token: 0x1700106C RID: 4204
		// (get) Token: 0x060041DE RID: 16862 RVA: 0x000AE780 File Offset: 0x000AC980
		public byte[] TlsEapKey
		{
			get
			{
				if (!this.IsTls)
				{
					return null;
				}
				EapKeyBlock eapKeyBlock;
				SecurityStatus securityStatus = this.sspiContext.QueryEapKeyBlock(out eapKeyBlock);
				if (securityStatus != SecurityStatus.OK)
				{
					return null;
				}
				return eapKeyBlock.rgbKeys;
			}
		}

		// Token: 0x1700106D RID: 4205
		// (get) Token: 0x060041DF RID: 16863 RVA: 0x000AE7B0 File Offset: 0x000AC9B0
		public int TlsCipherKeySize
		{
			get
			{
				return this.TlsConnectionInfo.CipherStrength;
			}
		}

		// Token: 0x1700106E RID: 4206
		// (get) Token: 0x060041E0 RID: 16864 RVA: 0x000AE7C0 File Offset: 0x000AC9C0
		public ConnectionInfo TlsConnectionInfo
		{
			get
			{
				if (!this.IsTls)
				{
					return ConnectionInfo.Empty;
				}
				ConnectionInfo result;
				SecurityStatus securityStatus = this.sspiContext.QueryConnectionInfo(out result);
				if (securityStatus != SecurityStatus.OK)
				{
					return ConnectionInfo.Empty;
				}
				return result;
			}
		}

		// Token: 0x1700106F RID: 4207
		// (get) Token: 0x060041E1 RID: 16865 RVA: 0x000AE7F4 File Offset: 0x000AC9F4
		public IX509Certificate2 RemoteCertificate
		{
			get
			{
				if (!this.IsTls)
				{
					return null;
				}
				if (this.remoteCertificate == null)
				{
					X509Certificate2 x509Certificate;
					SecurityStatus securityStatus = this.sspiContext.QueryRemoteCertificate(out x509Certificate);
					if (x509Certificate != null)
					{
						this.remoteCertificate = new X509Certificate2Wrapper(x509Certificate);
					}
					if (securityStatus != SecurityStatus.OK)
					{
						return null;
					}
				}
				return this.remoteCertificate;
			}
		}

		// Token: 0x17001070 RID: 4208
		// (get) Token: 0x060041E2 RID: 16866 RVA: 0x000AE83C File Offset: 0x000ACA3C
		public IX509Certificate2 LocalCertificate
		{
			get
			{
				if (!this.IsTls)
				{
					return null;
				}
				if (this.localCertificate == null)
				{
					X509Certificate2 x509Certificate;
					SecurityStatus securityStatus = this.sspiContext.QueryLocalCertificate(out x509Certificate);
					if (x509Certificate != null)
					{
						this.localCertificate = new X509Certificate2Wrapper(x509Certificate);
					}
					if (securityStatus != SecurityStatus.OK)
					{
						throw new CryptographicException((int)securityStatus);
					}
				}
				return this.localCertificate;
			}
		}

		// Token: 0x060041E3 RID: 16867 RVA: 0x000AE888 File Offset: 0x000ACA88
		public Task<NetworkConnection.LazyAsyncResultWithTimeout> ReadAsync()
		{
			TaskCompletionSource<NetworkConnection.LazyAsyncResultWithTimeout> taskCompletionSource = new TaskCompletionSource<NetworkConnection.LazyAsyncResultWithTimeout>();
			this.BeginRead(new AsyncCallback(this.ReadComplete), taskCompletionSource);
			return taskCompletionSource.Task;
		}

		// Token: 0x060041E4 RID: 16868 RVA: 0x000AE8B8 File Offset: 0x000ACAB8
		public Task<NetworkConnection.LazyAsyncResultWithTimeout> ReadLineAsync()
		{
			TaskCompletionSource<NetworkConnection.LazyAsyncResultWithTimeout> taskCompletionSource = new TaskCompletionSource<NetworkConnection.LazyAsyncResultWithTimeout>();
			this.BeginReadLine(new AsyncCallback(this.ReadLineComplete), taskCompletionSource);
			return taskCompletionSource.Task;
		}

		// Token: 0x060041E5 RID: 16869 RVA: 0x000AE8E8 File Offset: 0x000ACAE8
		public Task<object> WriteAsync(byte[] buffer, int offset, int size)
		{
			ArgumentValidator.ThrowIfNull("buffer", buffer);
			ArgumentValidator.ThrowIfOutOfRange<int>("offset", offset, 0, buffer.Length - 1);
			ArgumentValidator.ThrowIfOutOfRange<int>("size", size, 1, buffer.Length - offset);
			TaskCompletionSource<object> taskCompletionSource = new TaskCompletionSource<object>();
			this.BeginWrite(buffer, offset, size, new AsyncCallback(this.WriteComplete), taskCompletionSource);
			return taskCompletionSource.Task;
		}

		// Token: 0x060041E6 RID: 16870 RVA: 0x000AE948 File Offset: 0x000ACB48
		public string GetBreadCrumbsInfo()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("LastIndex : ");
			stringBuilder.Append(this.breadcrumbs.LastFilledIndex);
			stringBuilder.AppendLine();
			stringBuilder.AppendLine("Network Connection BreadCrumb : ");
			for (int i = 0; i < 64; i++)
			{
				stringBuilder.Append(Enum.Format(typeof(NetworkConnection.Breadcrumb), this.breadcrumbs.BreadCrumb[i], "x"));
				stringBuilder.Append(" ");
			}
			stringBuilder.AppendLine();
			return stringBuilder.ToString();
		}

		// Token: 0x060041E7 RID: 16871 RVA: 0x000AE9E0 File Offset: 0x000ACBE0
		public void Shutdown()
		{
			this.breadcrumbs.Drop(NetworkConnection.Breadcrumb.Shutdown);
			Socket socket = Interlocked.Exchange<Socket>(ref this.socket, null);
			if (socket != null)
			{
				socket.Close();
			}
		}

		// Token: 0x060041E8 RID: 16872 RVA: 0x000AEA10 File Offset: 0x000ACC10
		public void Shutdown(int waitSeconds)
		{
			this.breadcrumbs.Drop(NetworkConnection.Breadcrumb.Shutdown);
			Socket socket = Interlocked.Exchange<Socket>(ref this.socket, null);
			if (socket != null)
			{
				try
				{
					socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.DontLinger, false);
					socket.LingerState = new LingerOption(true, waitSeconds);
				}
				catch (SocketException ex)
				{
					ExTraceGlobals.NetworkTracer.TraceError<SocketError, string>(this.connectionId, "SocketException {0} : {1} when setting linger state in Shutdown", ex.SocketErrorCode, ex.Message);
				}
				catch (ObjectDisposedException)
				{
					ExTraceGlobals.NetworkTracer.TraceError(this.connectionId, "ObjectDisposedException when setting linger state in Shutdown");
				}
				finally
				{
					socket.Close();
				}
			}
		}

		// Token: 0x060041E9 RID: 16873 RVA: 0x000AEAC8 File Offset: 0x000ACCC8
		public void Read(out byte[] buffer, out int offset, out int size, out object errorCode)
		{
			NetworkConnection.LazyAsyncResultWithTimeout lazyAsyncResultWithTimeout = (NetworkConnection.LazyAsyncResultWithTimeout)this.BeginRead(null, null);
			try
			{
				this.EndRead(lazyAsyncResultWithTimeout, out buffer, out offset, out size, out errorCode);
			}
			finally
			{
				lazyAsyncResultWithTimeout.InternalCleanup();
			}
		}

		// Token: 0x060041EA RID: 16874 RVA: 0x000AEB08 File Offset: 0x000ACD08
		public IAsyncResult BeginRead(AsyncCallback callback, object state)
		{
			this.breadcrumbs.Drop(NetworkConnection.Breadcrumb.BeginRead);
			NetworkConnection.LazyAsyncResultWithTimeout lazyAsyncResultWithTimeout = new NetworkConnection.LazyAsyncResultWithTimeout(this.recvTimeoutPeriod, this, state, callback);
			if (!this.EnterConnection())
			{
				lazyAsyncResultWithTimeout.InvokeCallback(SocketError.Shutdown);
			}
			else if (this.recvBuffer.Remaining > 0)
			{
				lazyAsyncResultWithTimeout.InvokeCallback();
			}
			else
			{
				this.SetNextRecvTimeout(lazyAsyncResultWithTimeout);
				this.InternalBeginReceive(lazyAsyncResultWithTimeout, NetworkConnection.readDataAvailable);
			}
			return lazyAsyncResultWithTimeout;
		}

		// Token: 0x060041EB RID: 16875 RVA: 0x000AEB78 File Offset: 0x000ACD78
		public void EndRead(IAsyncResult asyncResult, out byte[] buffer, out int offset, out int size, out object errorCode)
		{
			this.breadcrumbs.Drop(NetworkConnection.Breadcrumb.EndRead);
			NetworkConnection.LazyAsyncResultWithTimeout lazyAsyncResultWithTimeout = NetworkConnection.LazyAsyncResultWithTimeout.EndAsyncOperation(asyncResult);
			try
			{
				if (lazyAsyncResultWithTimeout.Result != null)
				{
					errorCode = lazyAsyncResultWithTimeout.Result;
					buffer = null;
					size = 0;
					offset = 0;
				}
				else
				{
					buffer = this.recvBuffer.Buffer;
					offset = this.recvBuffer.DataStartOffset;
					size = this.recvBuffer.Remaining;
					errorCode = null;
					this.recvBuffer.ConsumeData(size);
				}
			}
			finally
			{
				if (!this.LeaveConnection())
				{
					errorCode = SocketError.Shutdown;
				}
			}
		}

		// Token: 0x060041EC RID: 16876 RVA: 0x000AEC18 File Offset: 0x000ACE18
		public void PutBackReceivedBytes(int bytesUnconsumed)
		{
			this.breadcrumbs.Drop(NetworkConnection.Breadcrumb.PutBackReceivedBytes);
			this.recvBuffer.PutBackUnconsumedData(bytesUnconsumed);
		}

		// Token: 0x060041ED RID: 16877 RVA: 0x000AEC34 File Offset: 0x000ACE34
		public void ReadLine(out byte[] buffer, out int offset, out int size, out object errorCode)
		{
			NetworkConnection.LazyAsyncResultWithTimeout lazyAsyncResultWithTimeout = (NetworkConnection.LazyAsyncResultWithTimeout)this.BeginReadLine(null, null);
			try
			{
				this.EndReadLine(lazyAsyncResultWithTimeout, out buffer, out offset, out size, out errorCode);
			}
			finally
			{
				lazyAsyncResultWithTimeout.InternalCleanup();
			}
		}

		// Token: 0x060041EE RID: 16878 RVA: 0x000AEC74 File Offset: 0x000ACE74
		public IAsyncResult BeginReadLine(AsyncCallback callback, object state)
		{
			this.breadcrumbs.Drop(NetworkConnection.Breadcrumb.BeginReadLine);
			NetworkConnection.LazyAsyncResultWithTimeout lazyAsyncResultWithTimeout = new NetworkConnection.LazyAsyncResultWithTimeout(this.recvTimeoutPeriod, this, state, callback);
			if (!this.EnterConnection())
			{
				lazyAsyncResultWithTimeout.InvokeCallback(SocketError.Shutdown);
			}
			else if (this.IsLineAvailable)
			{
				lazyAsyncResultWithTimeout.InvokeCallback();
			}
			else
			{
				this.SetNextRecvTimeout(lazyAsyncResultWithTimeout);
				this.InternalBeginReceive(lazyAsyncResultWithTimeout, NetworkConnection.readLineDataAvailable);
			}
			return lazyAsyncResultWithTimeout;
		}

		// Token: 0x060041EF RID: 16879 RVA: 0x000AECE0 File Offset: 0x000ACEE0
		public void EndReadLine(IAsyncResult asyncResult, out byte[] buffer, out int offset, out int size, out object errorCode)
		{
			this.breadcrumbs.Drop(NetworkConnection.Breadcrumb.EndReadLine);
			NetworkConnection.LazyAsyncResultWithTimeout lazyAsyncResultWithTimeout = NetworkConnection.LazyAsyncResultWithTimeout.EndAsyncOperation(asyncResult);
			try
			{
				if (lazyAsyncResultWithTimeout.Result != null)
				{
					errorCode = lazyAsyncResultWithTimeout.Result;
					buffer = null;
					size = 0;
					offset = 0;
				}
				else
				{
					bool flag;
					size = this.recvBuffer.FindLine(this.maxLineLength, out flag);
					if (size < 0)
					{
						throw new InvalidOperationException(NetException.FindLineError);
					}
					buffer = this.recvBuffer.Buffer;
					offset = this.recvBuffer.DataStartOffset;
					if (flag)
					{
						this.recvBuffer.ConsumeData(size);
						errorCode = SocketError.MessageSize;
					}
					else
					{
						this.recvBuffer.ConsumeData(size + 2);
						errorCode = null;
					}
				}
			}
			finally
			{
				if (!this.LeaveConnection())
				{
					errorCode = SocketError.Shutdown;
				}
			}
		}

		// Token: 0x060041F0 RID: 16880 RVA: 0x000AEDC0 File Offset: 0x000ACFC0
		public void Write(byte[] buffer, int offset, int size, out object errorCode)
		{
			NetworkConnection.LazyAsyncResultWithTimeout lazyAsyncResultWithTimeout = (NetworkConnection.LazyAsyncResultWithTimeout)this.BeginWrite(buffer, offset, size, null, null);
			try
			{
				this.EndWrite(lazyAsyncResultWithTimeout, out errorCode);
			}
			finally
			{
				lazyAsyncResultWithTimeout.InternalCleanup();
			}
		}

		// Token: 0x060041F1 RID: 16881 RVA: 0x000AEE00 File Offset: 0x000AD000
		public IAsyncResult BeginWrite(byte[] buffer, int offset, int size, AsyncCallback callback, object state)
		{
			this.breadcrumbs.Drop(NetworkConnection.Breadcrumb.BeginWrite);
			NetworkConnection.LazyAsyncResultWithTimeout lazyAsyncResultWithTimeout = new NetworkConnection.LazyAsyncResultWithTimeout(this.sendTimeoutPeriod, this, state, callback);
			if (!this.EnterConnection())
			{
				lazyAsyncResultWithTimeout.InvokeCallback(SocketError.Shutdown);
			}
			else
			{
				lazyAsyncResultWithTimeout.Buffer = buffer;
				lazyAsyncResultWithTimeout.Offset = offset;
				lazyAsyncResultWithTimeout.Size = size;
				this.SetNextSendTimeout(lazyAsyncResultWithTimeout);
				this.InternalBeginSend(lazyAsyncResultWithTimeout);
			}
			return lazyAsyncResultWithTimeout;
		}

		// Token: 0x060041F2 RID: 16882 RVA: 0x000AEE6C File Offset: 0x000AD06C
		public IAsyncResult BeginWrite(Stream stream, AsyncCallback callback, object state)
		{
			this.breadcrumbs.Drop(NetworkConnection.Breadcrumb.BeginWriteStream);
			ArgumentValidator.ThrowIfNull("stream", stream);
			NetworkConnection.WriteStreamAsyncResult writeStreamAsyncResult = new NetworkConnection.WriteStreamAsyncResult(this.sendTimeoutPeriod, stream, this, state, callback);
			this.SetNextSendTimeout(writeStreamAsyncResult);
			if (!this.EnterConnection())
			{
				writeStreamAsyncResult.InvokeCallback(SocketError.Shutdown);
			}
			else
			{
				if (this.sendBuffer == null)
				{
					this.sendBuffer = new NetworkBuffer(16432);
				}
				this.ReadFromStream(writeStreamAsyncResult);
			}
			return writeStreamAsyncResult;
		}

		// Token: 0x060041F3 RID: 16883 RVA: 0x000AEEE4 File Offset: 0x000AD0E4
		public void EndWrite(IAsyncResult asyncResult, out object errorCode)
		{
			this.breadcrumbs.Drop(NetworkConnection.Breadcrumb.EndWrite);
			NetworkConnection.LazyAsyncResultWithTimeout lazyAsyncResultWithTimeout = NetworkConnection.LazyAsyncResultWithTimeout.EndAsyncOperation(asyncResult);
			errorCode = (this.LeaveConnection() ? lazyAsyncResultWithTimeout.Result : SocketError.Shutdown);
		}

		// Token: 0x060041F4 RID: 16884 RVA: 0x000AEF21 File Offset: 0x000AD121
		public IAsyncResult BeginNegotiateTlsAsClient(AsyncCallback callback, object state)
		{
			return this.BeginNegotiateTlsAsClient(null, null, callback, state);
		}

		// Token: 0x060041F5 RID: 16885 RVA: 0x000AEF2D File Offset: 0x000AD12D
		public IAsyncResult BeginNegotiateTlsAsClient(X509Certificate certificate, AsyncCallback callback, object state)
		{
			return this.BeginNegotiateTlsAsClient(certificate, null, callback, state);
		}

		// Token: 0x060041F6 RID: 16886 RVA: 0x000AEF39 File Offset: 0x000AD139
		public Task<object> NegotiateTlsAsClientAsync(IX509Certificate2 certificate, string targetName)
		{
			ArgumentValidator.ThrowIfNull("targetName", targetName);
			return Task<object>.Factory.FromAsync(this.BeginNegotiateTlsAsClient((certificate == null) ? null : certificate.Certificate, targetName, null, null), new Func<IAsyncResult, object>(this.EndNegotiateTlsAsClient));
		}

		// Token: 0x060041F7 RID: 16887 RVA: 0x000AEF74 File Offset: 0x000AD174
		public IAsyncResult BeginNegotiateTlsAsClient(X509Certificate certificate, string targetName, AsyncCallback callback, object state)
		{
			this.breadcrumbs.Drop(NetworkConnection.Breadcrumb.BeginNegotiateTlsAsClient);
			if (this.IsTls)
			{
				throw new InvalidOperationException(NetException.TlsAlreadyNegotiated);
			}
			NetworkConnection.LazyAsyncResultWithTimeout lazyAsyncResultWithTimeout = new NetworkConnection.LazyAsyncResultWithTimeout(this.sendTimeoutPeriod, this, state, callback);
			Socket socket = this.socket;
			if (!this.EnterConnection() || socket == null)
			{
				lazyAsyncResultWithTimeout.InvokeCallback(SocketError.Shutdown);
				return lazyAsyncResultWithTimeout;
			}
			this.sspiContext = new SspiContext
			{
				ClientTlsProtocols = this.ClientTlsProtocols
			};
			SecurityStatus securityStatus = this.sspiContext.InitializeForTls(CredentialUse.Outbound, false, certificate, targetName);
			if (securityStatus != SecurityStatus.OK)
			{
				lazyAsyncResultWithTimeout.InvokeCallback(securityStatus);
				return lazyAsyncResultWithTimeout;
			}
			this.recvBuffer.ExpandBuffer(this.sspiContext.MaxTokenSize);
			if (this.sendBuffer == null)
			{
				this.sendBuffer = new NetworkBuffer(this.sspiContext.MaxTokenSize);
			}
			else
			{
				this.sendBuffer.ExpandBuffer(this.sspiContext.MaxTokenSize);
			}
			securityStatus = this.sspiContext.NegotiateSecurityContext(null, this.sendBuffer);
			if (securityStatus != SecurityStatus.ContinueNeeded && securityStatus != SecurityStatus.OK)
			{
				lazyAsyncResultWithTimeout.InvokeCallback(securityStatus);
				return lazyAsyncResultWithTimeout;
			}
			this.SetNextSendTimeout(lazyAsyncResultWithTimeout);
			this.sendBuffer.ShuffleBuffer();
			SocketError socketError;
			try
			{
				socket.BeginSend(this.sendBuffer.Buffer, this.sendBuffer.BufferStartOffset, this.sendBuffer.Filled, SocketFlags.None, out socketError, NetworkConnection.sendTlsNegotiationCompleted, lazyAsyncResultWithTimeout);
			}
			catch (ObjectDisposedException)
			{
				socketError = SocketError.Shutdown;
			}
			if (socketError != SocketError.Success && socketError != SocketError.IOPending)
			{
				this.InvokeSendCallback(lazyAsyncResultWithTimeout, socketError);
				return lazyAsyncResultWithTimeout;
			}
			return lazyAsyncResultWithTimeout;
		}

		// Token: 0x060041F8 RID: 16888 RVA: 0x000AF10C File Offset: 0x000AD30C
		public void EndNegotiateTlsAsClient(IAsyncResult asyncResult, out object errorCode)
		{
			this.breadcrumbs.Drop(NetworkConnection.Breadcrumb.EndNegotiateTlsAsClient);
			ArgumentValidator.ThrowIfNull("asyncResult", asyncResult);
			NetworkConnection.LazyAsyncResultWithTimeout lazyAsyncResultWithTimeout = NetworkConnection.LazyAsyncResultWithTimeout.EndAsyncOperation(asyncResult);
			errorCode = lazyAsyncResultWithTimeout.Result;
			if (!this.LeaveConnection())
			{
				errorCode = SocketError.Shutdown;
			}
		}

		// Token: 0x060041F9 RID: 16889 RVA: 0x000AF154 File Offset: 0x000AD354
		public object EndNegotiateTlsAsClient(IAsyncResult asyncResult)
		{
			this.breadcrumbs.Drop(NetworkConnection.Breadcrumb.EndNegotiateTlsAsClient);
			ArgumentValidator.ThrowIfNull("asyncResult", asyncResult);
			NetworkConnection.LazyAsyncResultWithTimeout lazyAsyncResultWithTimeout = NetworkConnection.LazyAsyncResultWithTimeout.EndAsyncOperation(asyncResult);
			object result = lazyAsyncResultWithTimeout.Result;
			if (!this.LeaveConnection())
			{
				result = SocketError.Shutdown;
			}
			return result;
		}

		// Token: 0x060041FA RID: 16890 RVA: 0x000AF19B File Offset: 0x000AD39B
		public IAsyncResult BeginNegotiateTlsAsServer(X509Certificate2 cert, AsyncCallback callback, object state)
		{
			return this.BeginNegotiateTlsAsServer(cert, false, callback, state);
		}

		// Token: 0x060041FB RID: 16891 RVA: 0x000AF1A7 File Offset: 0x000AD3A7
		public Task<object> NegotiateTlsAsServerAsync(IX509Certificate2 certificate, bool requestClientCertificate)
		{
			ArgumentValidator.ThrowIfNull("certificate", certificate);
			return Task.Factory.FromAsync<X509Certificate2, object>(new Func<X509Certificate2, AsyncCallback, object, IAsyncResult>(this.BeginNegotiateTlsAsServer), new Func<IAsyncResult, object>(this.EndNegotiateTlsAsServerInternal), certificate.Certificate, null);
		}

		// Token: 0x060041FC RID: 16892 RVA: 0x000AF1E0 File Offset: 0x000AD3E0
		public IAsyncResult BeginNegotiateTlsAsServer(X509Certificate2 cert, bool requestClientCertificate, AsyncCallback callback, object state)
		{
			this.breadcrumbs.Drop(NetworkConnection.Breadcrumb.BeginNegotiateTlsAsServer);
			ArgumentValidator.ThrowIfNull("cert", cert);
			if (this.IsTls)
			{
				throw new InvalidOperationException(NetException.TlsAlreadyNegotiated);
			}
			ExTraceGlobals.NetworkTracer.Information((long)this.GetHashCode(), requestClientCertificate ? "Beginning TLS negotiation with request for client certificate." : "Beginning TLS negotiation without request for client certificate.");
			NetworkConnection.LazyAsyncResultWithTimeout lazyAsyncResultWithTimeout = new NetworkConnection.LazyAsyncResultWithTimeout(this.sendTimeoutPeriod, this, state, callback);
			Socket socket = this.socket;
			if (!this.EnterConnection() || socket == null)
			{
				lazyAsyncResultWithTimeout.InvokeCallback(SocketError.Shutdown);
				return lazyAsyncResultWithTimeout;
			}
			this.sspiContext = new SspiContext
			{
				ServerTlsProtocols = this.ServerTlsProtocols
			};
			SecurityStatus securityStatus = this.sspiContext.InitializeForTls(CredentialUse.Inbound, requestClientCertificate, cert, null);
			if (securityStatus != SecurityStatus.OK)
			{
				lazyAsyncResultWithTimeout.InvokeCallback(securityStatus);
				return lazyAsyncResultWithTimeout;
			}
			this.recvBuffer.ExpandBuffer(this.sspiContext.MaxTokenSize);
			if (this.sendBuffer == null)
			{
				this.sendBuffer = new NetworkBuffer(this.sspiContext.MaxTokenSize);
			}
			else
			{
				this.sendBuffer.ExpandBuffer(this.sspiContext.MaxTokenSize);
			}
			this.SetNextSendTimeout(lazyAsyncResultWithTimeout);
			this.recvBuffer.ShuffleBuffer();
			SocketError socketError;
			try
			{
				socket.BeginReceive(this.recvBuffer.Buffer, this.recvBuffer.UnusedStartOffset, this.recvBuffer.Unused, SocketFlags.None, out socketError, NetworkConnection.recvTlsNegotiationCompleted, lazyAsyncResultWithTimeout);
			}
			catch (ObjectDisposedException)
			{
				socketError = SocketError.Shutdown;
			}
			if (socketError != SocketError.Success && socketError != SocketError.IOPending)
			{
				this.InvokeSendCallback(lazyAsyncResultWithTimeout, securityStatus);
				return lazyAsyncResultWithTimeout;
			}
			return lazyAsyncResultWithTimeout;
		}

		// Token: 0x060041FD RID: 16893 RVA: 0x000AF378 File Offset: 0x000AD578
		public void EndNegotiateTlsAsServer(IAsyncResult asyncResult, out object errorCode)
		{
			this.breadcrumbs.Drop(NetworkConnection.Breadcrumb.EndNegotiateTlsAsServer);
			NetworkConnection.LazyAsyncResultWithTimeout lazyAsyncResultWithTimeout = NetworkConnection.LazyAsyncResultWithTimeout.EndAsyncOperation(asyncResult);
			errorCode = lazyAsyncResultWithTimeout.Result;
			if (errorCode == null)
			{
				ExTraceGlobals.NetworkTracer.Information((long)this.GetHashCode(), (this.RemoteCertificate != null) ? "Client certificate provided during TLS negotiation." : "Client certificate was not provided during TLS negotiation.");
			}
			if (!this.LeaveConnection())
			{
				errorCode = SocketError.Shutdown;
			}
		}

		// Token: 0x060041FE RID: 16894 RVA: 0x000AF3E0 File Offset: 0x000AD5E0
		private object EndNegotiateTlsAsServerInternal(IAsyncResult asyncResult)
		{
			object result;
			this.EndNegotiateTlsAsServer(asyncResult, out result);
			return result;
		}

		// Token: 0x060041FF RID: 16895 RVA: 0x000AF3F8 File Offset: 0x000AD5F8
		internal void CheckForTimeouts(long now)
		{
			if (Thread.VolatileRead(ref this.referenceCount) < 0)
			{
				return;
			}
			NetworkConnection.LazyAsyncResultWithTimeout lazyAsyncResultWithTimeout = (NetworkConnection.LazyAsyncResultWithTimeout)this.recvAsyncResult.Target;
			if (this.recvAsyncResultHashCode != 0 && lazyAsyncResultWithTimeout != null && lazyAsyncResultWithTimeout.TimedOut(now))
			{
				this.breadcrumbs.Drop(NetworkConnection.Breadcrumb.RecvTimeout);
				ThreadPool.QueueUserWorkItem(NetworkConnection.timeoutOccurredCallback, lazyAsyncResultWithTimeout);
			}
			lazyAsyncResultWithTimeout = (NetworkConnection.LazyAsyncResultWithTimeout)this.sendAsyncResult.Target;
			if (this.sendAsyncResultHashCode != 0 && lazyAsyncResultWithTimeout != null && lazyAsyncResultWithTimeout.TimedOut(now))
			{
				this.breadcrumbs.Drop(NetworkConnection.Breadcrumb.SendTimeout);
				ThreadPool.QueueUserWorkItem(NetworkConnection.timeoutOccurredCallback, lazyAsyncResultWithTimeout);
			}
		}

		// Token: 0x06004200 RID: 16896 RVA: 0x000AF490 File Offset: 0x000AD690
		protected override void InternalDispose(bool disposing)
		{
			NetworkTimer.Remove(this);
			if (disposing)
			{
				this.breadcrumbs.Drop(NetworkConnection.Breadcrumb.Dispose);
				Socket socket = Interlocked.Exchange<Socket>(ref this.socket, null);
				if (socket != null)
				{
					try
					{
						socket.Shutdown(SocketShutdown.Both);
					}
					catch (SocketException ex)
					{
						ExTraceGlobals.NetworkTracer.TraceError<SocketError, string>(this.connectionId, "SocketException {0} : {1} when socket shutdown in Dispose", ex.SocketErrorCode, ex.Message);
					}
					finally
					{
						socket.Close();
					}
				}
				int num = Thread.VolatileRead(ref this.referenceCount);
				int num2;
				while ((num2 = Interlocked.CompareExchange(ref this.referenceCount, num - 1 | -2147483648, num)) != num)
				{
					num = num2;
				}
				if (num2 == 1)
				{
					this.ReleaseResources();
				}
			}
		}

		// Token: 0x06004201 RID: 16897 RVA: 0x000AF54C File Offset: 0x000AD74C
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<NetworkConnection>(this);
		}

		// Token: 0x06004202 RID: 16898 RVA: 0x000AF554 File Offset: 0x000AD754
		protected virtual void ReadComplete(IAsyncResult iar)
		{
			TaskCompletionSource<NetworkConnection.LazyAsyncResultWithTimeout> taskCompletionSource = (TaskCompletionSource<NetworkConnection.LazyAsyncResultWithTimeout>)iar.AsyncState;
			try
			{
				byte[] buffer;
				int offset;
				int size;
				object obj;
				this.EndRead(iar, out buffer, out offset, out size, out obj);
				NetworkConnection.LazyAsyncResultWithTimeout lazyAsyncResultWithTimeout = (NetworkConnection.LazyAsyncResultWithTimeout)iar;
				lazyAsyncResultWithTimeout.Buffer = buffer;
				lazyAsyncResultWithTimeout.Offset = offset;
				lazyAsyncResultWithTimeout.Size = size;
				taskCompletionSource.SetResult(lazyAsyncResultWithTimeout);
			}
			catch (Exception exception)
			{
				taskCompletionSource.SetException(exception);
			}
		}

		// Token: 0x06004203 RID: 16899 RVA: 0x000AF5C4 File Offset: 0x000AD7C4
		protected virtual void ReadLineComplete(IAsyncResult iar)
		{
			TaskCompletionSource<NetworkConnection.LazyAsyncResultWithTimeout> taskCompletionSource = (TaskCompletionSource<NetworkConnection.LazyAsyncResultWithTimeout>)iar.AsyncState;
			try
			{
				byte[] buffer;
				int offset;
				int size;
				object obj;
				this.EndReadLine(iar, out buffer, out offset, out size, out obj);
				NetworkConnection.LazyAsyncResultWithTimeout lazyAsyncResultWithTimeout = (NetworkConnection.LazyAsyncResultWithTimeout)iar;
				lazyAsyncResultWithTimeout.Buffer = buffer;
				lazyAsyncResultWithTimeout.Offset = offset;
				lazyAsyncResultWithTimeout.Size = size;
				taskCompletionSource.SetResult(lazyAsyncResultWithTimeout);
			}
			catch (Exception exception)
			{
				taskCompletionSource.SetException(exception);
			}
		}

		// Token: 0x06004204 RID: 16900 RVA: 0x000AF634 File Offset: 0x000AD834
		protected virtual void WriteComplete(IAsyncResult asyncResult)
		{
			TaskCompletionSource<object> taskCompletionSource = (TaskCompletionSource<object>)asyncResult.AsyncState;
			try
			{
				object result;
				this.EndWrite(asyncResult, out result);
				taskCompletionSource.SetResult(result);
			}
			catch (Exception exception)
			{
				taskCompletionSource.SetException(exception);
			}
		}

		// Token: 0x06004205 RID: 16901 RVA: 0x000AF67C File Offset: 0x000AD87C
		private bool EnterConnection()
		{
			return Interlocked.Increment(ref this.referenceCount) > 0;
		}

		// Token: 0x06004206 RID: 16902 RVA: 0x000AF68C File Offset: 0x000AD88C
		private bool LeaveConnection()
		{
			int num = Interlocked.Decrement(ref this.referenceCount);
			if (num == -2147483648)
			{
				this.ReleaseResources();
			}
			return num > 0;
		}

		// Token: 0x06004207 RID: 16903 RVA: 0x000AF6B8 File Offset: 0x000AD8B8
		private void ReleaseResources()
		{
			if (this.sspiContext != null)
			{
				this.sspiContext.Dispose();
				this.sspiContext = null;
			}
			if (this.recvBuffer != null)
			{
				this.recvBuffer.Dispose();
			}
			if (this.sendBuffer != null)
			{
				this.sendBuffer.Dispose();
				this.sendBuffer = null;
			}
		}

		// Token: 0x06004208 RID: 16904 RVA: 0x000AF70C File Offset: 0x000AD90C
		private static void TimeoutOccurred(object state)
		{
			NetworkConnection.LazyAsyncResultWithTimeout lazyAsyncResultWithTimeout = (NetworkConnection.LazyAsyncResultWithTimeout)state;
			lazyAsyncResultWithTimeout.InvokeCallback(SocketError.TimedOut);
		}

		// Token: 0x06004209 RID: 16905 RVA: 0x000AF734 File Offset: 0x000AD934
		private static void ReadDataAvailable(IAsyncResult asyncResult)
		{
			NetworkConnection.LazyAsyncResultWithTimeout lazyAsyncResultWithTimeout = (NetworkConnection.LazyAsyncResultWithTimeout)asyncResult.AsyncState;
			NetworkConnection networkConnection = (NetworkConnection)lazyAsyncResultWithTimeout.AsyncObject;
			networkConnection.breadcrumbs.Drop(NetworkConnection.Breadcrumb.ReadDataAvailable);
			bool flag;
			object obj = networkConnection.EndReadAndDecrypt(asyncResult, out flag);
			if (obj == null && flag)
			{
				networkConnection.InternalBeginReceive(lazyAsyncResultWithTimeout, NetworkConnection.readDataAvailable);
				return;
			}
			if (obj != null)
			{
				networkConnection.breadcrumbs.Drop(NetworkConnection.Breadcrumb.ReadDataAvailableError);
			}
			networkConnection.InvokeRecvCallback(lazyAsyncResultWithTimeout, obj);
		}

		// Token: 0x0600420A RID: 16906 RVA: 0x000AF79C File Offset: 0x000AD99C
		private static void ReadLineDataAvailable(IAsyncResult asyncResult)
		{
			NetworkConnection.LazyAsyncResultWithTimeout lazyAsyncResultWithTimeout = (NetworkConnection.LazyAsyncResultWithTimeout)asyncResult.AsyncState;
			NetworkConnection networkConnection = (NetworkConnection)lazyAsyncResultWithTimeout.AsyncObject;
			networkConnection.breadcrumbs.Drop(NetworkConnection.Breadcrumb.ReadLineDataAvailable);
			bool flag;
			object obj = networkConnection.EndReadAndDecrypt(asyncResult, out flag);
			if (obj == null && (flag || !networkConnection.IsLineAvailable))
			{
				networkConnection.InternalBeginReceive(lazyAsyncResultWithTimeout, NetworkConnection.readLineDataAvailable);
				return;
			}
			if (obj != null)
			{
				networkConnection.breadcrumbs.Drop(NetworkConnection.Breadcrumb.ReadLineDataAvailableError);
			}
			networkConnection.InvokeRecvCallback(lazyAsyncResultWithTimeout, obj);
		}

		// Token: 0x0600420B RID: 16907 RVA: 0x000AF80C File Offset: 0x000ADA0C
		private object EndReadAndDecrypt(IAsyncResult asyncResult, out bool incompleteMessage)
		{
			incompleteMessage = false;
			Socket socket = this.socket;
			if (socket == null)
			{
				return SocketError.Shutdown;
			}
			SocketError socketError;
			int num;
			try
			{
				num = socket.EndReceive(asyncResult, out socketError);
			}
			catch (ObjectDisposedException)
			{
				return SocketError.Shutdown;
			}
			if (num == 0 && socketError == SocketError.Success)
			{
				return SocketError.SocketError;
			}
			if (socketError != SocketError.Success)
			{
				return socketError;
			}
			if (this.IsTls)
			{
				this.recvBuffer.ReportEncryptedBytesFilled(num);
				SecurityStatus securityStatus = this.sspiContext.DecryptMessage(this.recvBuffer);
				if (securityStatus == SecurityStatus.IncompleteMessage)
				{
					incompleteMessage = true;
				}
				else if (securityStatus != SecurityStatus.OK)
				{
					return securityStatus;
				}
				this.bytesReceived += (long)num;
			}
			else
			{
				this.recvBuffer.ReportBytesFilled(num);
				this.bytesReceived += (long)num;
			}
			return null;
		}

		// Token: 0x0600420C RID: 16908 RVA: 0x000AF8E4 File Offset: 0x000ADAE4
		private static void SendStreamCompleted(IAsyncResult asyncResult)
		{
			NetworkConnection.WriteStreamAsyncResult writeStreamAsyncResult = (NetworkConnection.WriteStreamAsyncResult)asyncResult.AsyncState;
			NetworkConnection networkConnection = (NetworkConnection)writeStreamAsyncResult.AsyncObject;
			Socket socket = networkConnection.socket;
			int num = 0;
			SocketError socketError;
			if (socket == null)
			{
				socketError = SocketError.Shutdown;
			}
			else
			{
				try
				{
					num = socket.EndSend(asyncResult, out socketError);
				}
				catch (ObjectDisposedException)
				{
					socketError = SocketError.Shutdown;
				}
			}
			if (socketError != SocketError.Success)
			{
				networkConnection.InvokeSendCallback(writeStreamAsyncResult, socketError);
				return;
			}
			networkConnection.bytesSent += (long)num;
			writeStreamAsyncResult.UpdateTimeout(networkConnection.sendTimeoutPeriod);
			networkConnection.ReadFromStream(writeStreamAsyncResult);
		}

		// Token: 0x0600420D RID: 16909 RVA: 0x000AF978 File Offset: 0x000ADB78
		private void ReadFromStream(NetworkConnection.WriteStreamAsyncResult userAsyncResult)
		{
			int count;
			if (this.IsTls)
			{
				this.sendBuffer.EmptyBufferReservingBytes(this.sspiContext.HeaderSize);
				count = Math.Min(this.sspiContext.MaxMessageSize, this.sendBuffer.Unused - this.sspiContext.TrailerSize);
			}
			else
			{
				this.sendBuffer.EmptyBuffer();
				count = this.sendBuffer.Unused;
			}
			int num;
			try
			{
				num = userAsyncResult.Stream.Read(this.sendBuffer.Buffer, this.sendBuffer.UnusedStartOffset, count);
			}
			catch (Exception ex)
			{
				if (ex is OutOfMemoryException || ex is StackOverflowException || ex is ThreadAbortException)
				{
					throw;
				}
				this.breadcrumbs.Drop(NetworkConnection.Breadcrumb.StreamBeginReadException);
				this.InvokeSendCallback(userAsyncResult, ex);
				return;
			}
			if (num != 0)
			{
				this.sendBuffer.ReportBytesFilled(num);
				if (this.IsTls)
				{
					SecurityStatus securityStatus = this.sspiContext.EncryptMessage(this.sendBuffer);
					if (securityStatus != SecurityStatus.OK)
					{
						this.InvokeSendCallback(userAsyncResult, securityStatus);
						return;
					}
				}
				Socket socket = this.socket;
				SocketError socketError;
				if (socket == null)
				{
					socketError = SocketError.Shutdown;
				}
				else
				{
					try
					{
						socket.BeginSend(this.sendBuffer.Buffer, this.sendBuffer.BufferStartOffset, this.sendBuffer.Filled, SocketFlags.None, out socketError, NetworkConnection.sendStreamCompleted, userAsyncResult);
					}
					catch (ObjectDisposedException)
					{
						socketError = SocketError.Shutdown;
					}
				}
				if (socketError != SocketError.Success && socketError != SocketError.IOPending)
				{
					this.InvokeSendCallback(userAsyncResult, socketError);
				}
				return;
			}
			this.InvokeSendCallback(userAsyncResult, null);
		}

		// Token: 0x0600420E RID: 16910 RVA: 0x000AFB08 File Offset: 0x000ADD08
		private void InternalBeginReceive(NetworkConnection.LazyAsyncResultWithTimeout asyncResult, AsyncCallback callback)
		{
			Socket socket = this.socket;
			SocketError socketError;
			if (socket == null)
			{
				socketError = SocketError.Shutdown;
			}
			else
			{
				this.recvBuffer.ShuffleBuffer();
				if (this.recvBuffer.Unused <= 0)
				{
					if (this.recvBuffer.Length >= 69680)
					{
						this.InvokeRecvCallback(asyncResult, SocketError.NoBufferSpaceAvailable);
						return;
					}
					this.recvBuffer.ExpandBuffer(this.recvBuffer.Length + 4096);
				}
				try
				{
					socket.BeginReceive(this.recvBuffer.Buffer, this.recvBuffer.UnusedStartOffset, this.recvBuffer.Unused, SocketFlags.None, out socketError, callback, asyncResult);
				}
				catch (ObjectDisposedException)
				{
					socketError = SocketError.Shutdown;
				}
			}
			if (socketError != SocketError.Success && socketError != SocketError.IOPending)
			{
				this.InvokeRecvCallback(asyncResult, socketError);
			}
		}

		// Token: 0x0600420F RID: 16911 RVA: 0x000AFBE4 File Offset: 0x000ADDE4
		private void InternalBeginSend(NetworkConnection.LazyAsyncResultWithTimeout asyncResult)
		{
			int num;
			byte[] buffer;
			int offset;
			int size;
			if (this.IsTls)
			{
				num = Math.Min(asyncResult.Size, this.sspiContext.MaxMessageSize);
				SecurityStatus securityStatus = this.sspiContext.EncryptMessage(asyncResult.Buffer, asyncResult.Offset, num, this.sendBuffer);
				if (securityStatus != SecurityStatus.OK)
				{
					this.InvokeSendCallback(asyncResult, securityStatus);
					return;
				}
				buffer = this.sendBuffer.Buffer;
				offset = this.sendBuffer.BufferStartOffset;
				size = this.sendBuffer.Filled;
			}
			else
			{
				num = asyncResult.Size;
				buffer = asyncResult.Buffer;
				offset = asyncResult.Offset;
				size = asyncResult.Size;
			}
			asyncResult.Size -= num;
			asyncResult.Offset += num;
			Socket socket = this.socket;
			SocketError socketError;
			if (socket == null)
			{
				socketError = SocketError.Shutdown;
			}
			else
			{
				try
				{
					socket.BeginSend(buffer, offset, size, SocketFlags.None, out socketError, NetworkConnection.internalSendCompleted, asyncResult);
				}
				catch (ObjectDisposedException)
				{
					socketError = SocketError.Shutdown;
				}
			}
			if (socketError != SocketError.Success && socketError != SocketError.IOPending)
			{
				this.InvokeSendCallback(asyncResult, socketError);
			}
		}

		// Token: 0x06004210 RID: 16912 RVA: 0x000AFD04 File Offset: 0x000ADF04
		private static void InternalSendCompleted(IAsyncResult asyncResult)
		{
			NetworkConnection.LazyAsyncResultWithTimeout lazyAsyncResultWithTimeout = (NetworkConnection.LazyAsyncResultWithTimeout)asyncResult.AsyncState;
			NetworkConnection networkConnection = (NetworkConnection)lazyAsyncResultWithTimeout.AsyncObject;
			Socket socket = networkConnection.socket;
			int num = 0;
			SocketError socketError;
			if (socket == null)
			{
				socketError = SocketError.Shutdown;
			}
			else
			{
				try
				{
					num = socket.EndSend(asyncResult, out socketError);
				}
				catch (ObjectDisposedException)
				{
					socketError = SocketError.Shutdown;
				}
			}
			if (socketError != SocketError.Success)
			{
				networkConnection.InvokeSendCallback(lazyAsyncResultWithTimeout, socketError);
				return;
			}
			networkConnection.bytesSent += (long)num;
			if (lazyAsyncResultWithTimeout.Size == 0)
			{
				networkConnection.InvokeSendCallback(lazyAsyncResultWithTimeout, null);
				return;
			}
			lazyAsyncResultWithTimeout.UpdateTimeout(networkConnection.sendTimeoutPeriod);
			networkConnection.InternalBeginSend(lazyAsyncResultWithTimeout);
		}

		// Token: 0x06004211 RID: 16913 RVA: 0x000AFDAC File Offset: 0x000ADFAC
		private static void SendTlsNegotiationCompleted(IAsyncResult asyncResult)
		{
			NetworkConnection.LazyAsyncResultWithTimeout lazyAsyncResultWithTimeout = (NetworkConnection.LazyAsyncResultWithTimeout)asyncResult.AsyncState;
			NetworkConnection networkConnection = (NetworkConnection)lazyAsyncResultWithTimeout.AsyncObject;
			Socket socket = networkConnection.socket;
			int num = 0;
			SocketError socketError;
			if (socket == null)
			{
				socketError = SocketError.Shutdown;
			}
			else
			{
				try
				{
					num = socket.EndSend(asyncResult, out socketError);
				}
				catch (ObjectDisposedException)
				{
					socketError = SocketError.Shutdown;
				}
			}
			if (socketError != SocketError.Success)
			{
				networkConnection.InvokeSendCallback(lazyAsyncResultWithTimeout, socketError);
				return;
			}
			networkConnection.bytesSent += (long)num;
			networkConnection.sendBuffer.ConsumeData(num);
			if (networkConnection.sspiContext.State == ContextState.NegotiationComplete)
			{
				networkConnection.InvokeSendCallback(lazyAsyncResultWithTimeout, null);
				return;
			}
			networkConnection.recvBuffer.ShuffleBuffer();
			try
			{
				socket.BeginReceive(networkConnection.recvBuffer.Buffer, networkConnection.recvBuffer.UnusedStartOffset, networkConnection.recvBuffer.Unused, SocketFlags.None, out socketError, NetworkConnection.recvTlsNegotiationCompleted, lazyAsyncResultWithTimeout);
			}
			catch (ObjectDisposedException)
			{
				socketError = SocketError.Shutdown;
			}
			if (socketError != SocketError.Success && socketError != SocketError.IOPending)
			{
				networkConnection.InvokeSendCallback(lazyAsyncResultWithTimeout, socketError);
			}
		}

		// Token: 0x06004212 RID: 16914 RVA: 0x000AFEC0 File Offset: 0x000AE0C0
		private static void RecvTlsNegotiationCompleted(IAsyncResult asyncResult)
		{
			NetworkConnection.LazyAsyncResultWithTimeout lazyAsyncResultWithTimeout = (NetworkConnection.LazyAsyncResultWithTimeout)asyncResult.AsyncState;
			NetworkConnection networkConnection = (NetworkConnection)lazyAsyncResultWithTimeout.AsyncObject;
			Socket socket = networkConnection.socket;
			int num = 0;
			SocketError socketError;
			if (socket == null)
			{
				socketError = SocketError.Shutdown;
			}
			else
			{
				try
				{
					num = socket.EndReceive(asyncResult, out socketError);
				}
				catch (ObjectDisposedException)
				{
					socketError = SocketError.Shutdown;
				}
			}
			if (num == 0 && socketError == SocketError.Success)
			{
				socketError = SocketError.SocketError;
			}
			if (socketError != SocketError.Success)
			{
				networkConnection.InvokeSendCallback(lazyAsyncResultWithTimeout, socketError);
				return;
			}
			networkConnection.recvBuffer.ReportBytesFilled(num);
			networkConnection.bytesReceived += (long)num;
			SecurityStatus securityStatus = networkConnection.sspiContext.NegotiateSecurityContext(networkConnection.recvBuffer, networkConnection.sendBuffer);
			SecurityStatus securityStatus2 = securityStatus;
			if (securityStatus2 != SecurityStatus.IncompleteMessage)
			{
				if (securityStatus2 != SecurityStatus.OK)
				{
					if (securityStatus2 != SecurityStatus.ContinueNeeded)
					{
						networkConnection.InvokeSendCallback(lazyAsyncResultWithTimeout, securityStatus);
						return;
					}
				}
				else
				{
					securityStatus = networkConnection.sspiContext.CaptureChannelBindingToken(ChannelBindingType.Unique, out networkConnection.channelBindingToken);
					if (securityStatus == SecurityStatus.Unsupported)
					{
						networkConnection.channelBindingToken = null;
						securityStatus = SecurityStatus.OK;
					}
					else if (securityStatus != SecurityStatus.OK)
					{
						networkConnection.InvokeSendCallback(lazyAsyncResultWithTimeout, securityStatus);
						return;
					}
					networkConnection.recvBuffer.ExpandBuffer(networkConnection.sspiContext.MaxStreamSize + 4096);
					networkConnection.sendBuffer.ExpandBuffer(networkConnection.sspiContext.MaxStreamSize);
					if (networkConnection.recvBuffer.Remaining != 0)
					{
						networkConnection.recvBuffer.ShuffleBuffer();
						networkConnection.recvBuffer.EncryptedDataLength = networkConnection.recvBuffer.Remaining;
						networkConnection.recvBuffer.Filled = 0;
						networkConnection.recvBuffer.EncryptedDataOffset = 0;
						securityStatus = networkConnection.sspiContext.DecryptMessage(networkConnection.recvBuffer);
						if (securityStatus == SecurityStatus.IncompleteMessage)
						{
							securityStatus = SecurityStatus.OK;
						}
						else if (securityStatus != SecurityStatus.OK)
						{
							networkConnection.InvokeSendCallback(lazyAsyncResultWithTimeout, securityStatus);
							return;
						}
					}
				}
			}
			if (networkConnection.sendBuffer.Filled != 0)
			{
				try
				{
					socket.BeginSend(networkConnection.sendBuffer.Buffer, networkConnection.sendBuffer.BufferStartOffset, networkConnection.sendBuffer.Filled, SocketFlags.None, out socketError, NetworkConnection.sendTlsNegotiationCompleted, lazyAsyncResultWithTimeout);
					goto IL_262;
				}
				catch (ObjectDisposedException)
				{
					socketError = SocketError.Shutdown;
					goto IL_262;
				}
			}
			if (securityStatus == SecurityStatus.OK)
			{
				networkConnection.InvokeSendCallback(lazyAsyncResultWithTimeout, null);
				return;
			}
			networkConnection.recvBuffer.ShuffleBuffer();
			if (networkConnection.recvBuffer.Unused == 0)
			{
				socketError = SocketError.NoBufferSpaceAvailable;
			}
			else
			{
				try
				{
					socket.BeginReceive(networkConnection.recvBuffer.Buffer, networkConnection.recvBuffer.UnusedStartOffset, networkConnection.recvBuffer.Unused, SocketFlags.None, out socketError, NetworkConnection.recvTlsNegotiationCompleted, lazyAsyncResultWithTimeout);
				}
				catch (ObjectDisposedException)
				{
					socketError = SocketError.Shutdown;
				}
			}
			IL_262:
			if (socketError != SocketError.Success && socketError != SocketError.IOPending)
			{
				networkConnection.InvokeSendCallback(lazyAsyncResultWithTimeout, socketError);
			}
		}

		// Token: 0x06004213 RID: 16915 RVA: 0x000B0170 File Offset: 0x000AE370
		private void SetNextRecvTimeout(NetworkConnection.LazyAsyncResultWithTimeout asyncResult)
		{
			int hashCode = asyncResult.GetHashCode();
			int num = Interlocked.CompareExchange(ref this.recvAsyncResultHashCode, hashCode, 0);
			if (num != 0)
			{
				throw new InvalidOperationException(NetException.ReceiveInProgress);
			}
			this.recvAsyncResult.Target = asyncResult;
			this.EnterConnection();
		}

		// Token: 0x06004214 RID: 16916 RVA: 0x000B01B8 File Offset: 0x000AE3B8
		private void InvokeRecvCallback(NetworkConnection.LazyAsyncResultWithTimeout asyncResult, object result)
		{
			int hashCode = asyncResult.GetHashCode();
			int num = Interlocked.CompareExchange(ref this.recvAsyncResultHashCode, 0, hashCode);
			if (hashCode != num)
			{
				throw new InvalidOperationException(NetException.IAsyncResultMismatch);
			}
			if (result != null)
			{
				ExTraceGlobals.NetworkTracer.Information<Type, object>(this.connectionId, "Invoking user receive callback with status {0}:{1}", result.GetType(), result);
			}
			asyncResult.InvokeCallback(result);
			this.LeaveConnection();
		}

		// Token: 0x06004215 RID: 16917 RVA: 0x000B021C File Offset: 0x000AE41C
		private void SetNextSendTimeout(NetworkConnection.LazyAsyncResultWithTimeout asyncResult)
		{
			int hashCode = asyncResult.GetHashCode();
			int num = Interlocked.CompareExchange(ref this.sendAsyncResultHashCode, hashCode, 0);
			if (num != 0)
			{
				throw new InvalidOperationException(NetException.SendInProgress);
			}
			this.sendAsyncResult.Target = asyncResult;
			this.EnterConnection();
		}

		// Token: 0x06004216 RID: 16918 RVA: 0x000B0264 File Offset: 0x000AE464
		private void InvokeSendCallback(NetworkConnection.LazyAsyncResultWithTimeout asyncResult, object result)
		{
			int hashCode = asyncResult.GetHashCode();
			int num = Interlocked.CompareExchange(ref this.sendAsyncResultHashCode, 0, hashCode);
			if (hashCode != num)
			{
				throw new InvalidOperationException(NetException.IAsyncResultMismatch);
			}
			if (result != null)
			{
				ExTraceGlobals.NetworkTracer.Information<Type, object>(this.connectionId, "Invoking user send callback with status {0}:{1}", result.GetType(), result);
			}
			asyncResult.InvokeCallback(result);
			this.LeaveConnection();
		}

		// Token: 0x04003872 RID: 14450
		public const int MaxSupportedLineLength = 4096;

		// Token: 0x04003873 RID: 14451
		public const int DefaultLineLength = 1000;

		// Token: 0x04003874 RID: 14452
		public const int DefaultTimeout = 600;

		// Token: 0x04003875 RID: 14453
		private const int MaxSupportedTlsBufferLength = 16432;

		// Token: 0x04003876 RID: 14454
		private const int SignBit = -2147483648;

		// Token: 0x04003877 RID: 14455
		private const int NumberOfBreadcrumbs = 64;

		// Token: 0x04003878 RID: 14456
		private const int MaxRecvBufferLength = 69680;

		// Token: 0x04003879 RID: 14457
		private static readonly AsyncCallback readDataAvailable = new AsyncCallback(NetworkConnection.ReadDataAvailable);

		// Token: 0x0400387A RID: 14458
		private static readonly AsyncCallback internalSendCompleted = new AsyncCallback(NetworkConnection.InternalSendCompleted);

		// Token: 0x0400387B RID: 14459
		private static readonly AsyncCallback readLineDataAvailable = new AsyncCallback(NetworkConnection.ReadLineDataAvailable);

		// Token: 0x0400387C RID: 14460
		private static readonly AsyncCallback recvTlsNegotiationCompleted = new AsyncCallback(NetworkConnection.RecvTlsNegotiationCompleted);

		// Token: 0x0400387D RID: 14461
		private static readonly AsyncCallback sendTlsNegotiationCompleted = new AsyncCallback(NetworkConnection.SendTlsNegotiationCompleted);

		// Token: 0x0400387E RID: 14462
		private static readonly AsyncCallback sendStreamCompleted = new AsyncCallback(NetworkConnection.SendStreamCompleted);

		// Token: 0x0400387F RID: 14463
		private static readonly WaitCallback timeoutOccurredCallback = new WaitCallback(NetworkConnection.TimeoutOccurred);

		// Token: 0x04003880 RID: 14464
		private static readonly IPEndPoint nullEndPoint = new IPEndPoint(0L, 0);

		// Token: 0x04003881 RID: 14465
		private readonly long connectionId;

		// Token: 0x04003882 RID: 14466
		private static long lastConnectionId;

		// Token: 0x04003883 RID: 14467
		private Socket socket;

		// Token: 0x04003884 RID: 14468
		private int maxLineLength = 1000;

		// Token: 0x04003885 RID: 14469
		private long bytesReceived;

		// Token: 0x04003886 RID: 14470
		private long bytesSent;

		// Token: 0x04003887 RID: 14471
		private long sendTimeoutPeriod = 6000000000L;

		// Token: 0x04003888 RID: 14472
		private long recvTimeoutPeriod = 6000000000L;

		// Token: 0x04003889 RID: 14473
		private readonly NetworkBuffer recvBuffer;

		// Token: 0x0400388A RID: 14474
		private NetworkBuffer sendBuffer;

		// Token: 0x0400388B RID: 14475
		private readonly WeakReference recvAsyncResult = new WeakReference(null);

		// Token: 0x0400388C RID: 14476
		private readonly WeakReference sendAsyncResult = new WeakReference(null);

		// Token: 0x0400388D RID: 14477
		private int recvAsyncResultHashCode;

		// Token: 0x0400388E RID: 14478
		private int sendAsyncResultHashCode;

		// Token: 0x0400388F RID: 14479
		private SspiContext sspiContext;

		// Token: 0x04003890 RID: 14480
		private ChannelBindingToken channelBindingToken;

		// Token: 0x04003891 RID: 14481
		protected readonly Breadcrumbs<NetworkConnection.Breadcrumb> breadcrumbs = new Breadcrumbs<NetworkConnection.Breadcrumb>(64);

		// Token: 0x04003892 RID: 14482
		private IX509Certificate2 localCertificate;

		// Token: 0x04003893 RID: 14483
		private IX509Certificate2 remoteCertificate;

		// Token: 0x04003894 RID: 14484
		private readonly IPEndPoint remoteEndPoint;

		// Token: 0x04003895 RID: 14485
		private readonly IPEndPoint localEndPoint;

		// Token: 0x04003896 RID: 14486
		private int referenceCount = 1;

		// Token: 0x02000BDA RID: 3034
		internal class LazyAsyncResultWithTimeout : LazyAsyncResult
		{
			// Token: 0x06004235 RID: 16949 RVA: 0x000B0814 File Offset: 0x000AEA14
			internal LazyAsyncResultWithTimeout(long timeoutPeriod, object workerObject, object callerState, AsyncCallback callback) : base(workerObject, callerState, callback)
			{
				this.UpdateTimeout(timeoutPeriod);
			}

			// Token: 0x06004236 RID: 16950 RVA: 0x000B0827 File Offset: 0x000AEA27
			internal static NetworkConnection.LazyAsyncResultWithTimeout EndAsyncOperation(IAsyncResult asyncResult)
			{
				return LazyAsyncResult.EndAsyncOperation<NetworkConnection.LazyAsyncResultWithTimeout>(asyncResult);
			}

			// Token: 0x1700107C RID: 4220
			// (get) Token: 0x06004237 RID: 16951 RVA: 0x000B082F File Offset: 0x000AEA2F
			// (set) Token: 0x06004238 RID: 16952 RVA: 0x000B0837 File Offset: 0x000AEA37
			internal byte[] Buffer
			{
				[DebuggerStepThrough]
				get
				{
					return this.buffer;
				}
				[DebuggerStepThrough]
				set
				{
					this.buffer = value;
				}
			}

			// Token: 0x1700107D RID: 4221
			// (get) Token: 0x06004239 RID: 16953 RVA: 0x000B0840 File Offset: 0x000AEA40
			// (set) Token: 0x0600423A RID: 16954 RVA: 0x000B0848 File Offset: 0x000AEA48
			internal int Offset
			{
				[DebuggerStepThrough]
				get
				{
					return this.offset;
				}
				[DebuggerStepThrough]
				set
				{
					this.offset = value;
				}
			}

			// Token: 0x1700107E RID: 4222
			// (get) Token: 0x0600423B RID: 16955 RVA: 0x000B0851 File Offset: 0x000AEA51
			// (set) Token: 0x0600423C RID: 16956 RVA: 0x000B0859 File Offset: 0x000AEA59
			internal int Size
			{
				[DebuggerStepThrough]
				get
				{
					return this.size;
				}
				[DebuggerStepThrough]
				set
				{
					this.size = value;
				}
			}

			// Token: 0x0600423D RID: 16957 RVA: 0x000B0862 File Offset: 0x000AEA62
			internal bool TimedOut(long now)
			{
				return this.timeout < now && !base.InternalPeekCompleted;
			}

			// Token: 0x0600423E RID: 16958 RVA: 0x000B0878 File Offset: 0x000AEA78
			internal void UpdateTimeout(long timeoutPeriod)
			{
				this.timeout = DateTime.UtcNow.Ticks + timeoutPeriod;
			}

			// Token: 0x0600423F RID: 16959 RVA: 0x000B089C File Offset: 0x000AEA9C
			public override int GetHashCode()
			{
				int hashCode = base.GetHashCode();
				if (hashCode != 0)
				{
					return hashCode;
				}
				return 1;
			}

			// Token: 0x040038A7 RID: 14503
			private long timeout;

			// Token: 0x040038A8 RID: 14504
			private byte[] buffer;

			// Token: 0x040038A9 RID: 14505
			private int offset;

			// Token: 0x040038AA RID: 14506
			private int size;
		}

		// Token: 0x02000BDB RID: 3035
		internal sealed class WriteStreamAsyncResult : NetworkConnection.LazyAsyncResultWithTimeout
		{
			// Token: 0x06004240 RID: 16960 RVA: 0x000B08B6 File Offset: 0x000AEAB6
			internal WriteStreamAsyncResult(long timeoutPeriod, Stream stream, object workerObject, object callerState, AsyncCallback callback) : base(timeoutPeriod, workerObject, callerState, callback)
			{
				this.stream = stream;
			}

			// Token: 0x1700107F RID: 4223
			// (get) Token: 0x06004241 RID: 16961 RVA: 0x000B08CB File Offset: 0x000AEACB
			internal Stream Stream
			{
				[DebuggerStepThrough]
				get
				{
					return this.stream;
				}
			}

			// Token: 0x040038AB RID: 14507
			private readonly Stream stream;
		}

		// Token: 0x02000BDC RID: 3036
		protected enum Breadcrumb : byte
		{
			// Token: 0x040038AD RID: 14509
			None,
			// Token: 0x040038AE RID: 14510
			Dispose = 2,
			// Token: 0x040038AF RID: 14511
			Shutdown,
			// Token: 0x040038B0 RID: 14512
			BeginRead = 16,
			// Token: 0x040038B1 RID: 14513
			EndRead,
			// Token: 0x040038B2 RID: 14514
			ReadDataAvailable,
			// Token: 0x040038B3 RID: 14515
			ReadDataAvailableError,
			// Token: 0x040038B4 RID: 14516
			BeginReadLine,
			// Token: 0x040038B5 RID: 14517
			EndReadLine,
			// Token: 0x040038B6 RID: 14518
			ReadLineDataAvailable,
			// Token: 0x040038B7 RID: 14519
			ReadLineDataAvailableError,
			// Token: 0x040038B8 RID: 14520
			RecvTimeout,
			// Token: 0x040038B9 RID: 14521
			PutBackReceivedBytes,
			// Token: 0x040038BA RID: 14522
			BeginWrite = 32,
			// Token: 0x040038BB RID: 14523
			BeginWriteStream,
			// Token: 0x040038BC RID: 14524
			EndWrite,
			// Token: 0x040038BD RID: 14525
			SendTimeout,
			// Token: 0x040038BE RID: 14526
			StreamBeginReadException,
			// Token: 0x040038BF RID: 14527
			BeginNegotiateTlsAsClient = 48,
			// Token: 0x040038C0 RID: 14528
			EndNegotiateTlsAsClient,
			// Token: 0x040038C1 RID: 14529
			BeginNegotiateTlsAsServer,
			// Token: 0x040038C2 RID: 14530
			EndNegotiateTlsAsServer,
			// Token: 0x040038C3 RID: 14531
			AsyncOperationCancelled
		}
	}
}
