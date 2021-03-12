using System;
using System.IO;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.Serialization;
using Microsoft.Exchange.Core;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;

namespace Microsoft.Exchange.EseRepl
{
	// Token: 0x0200002B RID: 43
	internal class NetworkChannel
	{
		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x00004C65 File Offset: 0x00002E65
		public static Trace Tracer
		{
			get
			{
				return ExTraceGlobals.NetworkChannelTracer;
			}
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00004C6C File Offset: 0x00002E6C
		public static NetworkChannel.DataEncodingScheme VerifyDataEncoding(NetworkChannel.DataEncodingScheme requestedEncoding)
		{
			if (requestedEncoding >= NetworkChannel.DataEncodingScheme.LastIndex)
			{
				requestedEncoding = NetworkChannel.DataEncodingScheme.Uncompressed;
			}
			return requestedEncoding;
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00004C78 File Offset: 0x00002E78
		public static Exception RunNetworkFunction(Action op)
		{
			Exception result = null;
			try
			{
				op();
			}
			catch (IOException ex)
			{
				result = ex;
			}
			catch (SocketException ex2)
			{
				result = ex2;
			}
			catch (NetworkTransportException ex3)
			{
				result = ex3;
			}
			catch (ObjectDisposedException ex4)
			{
				result = ex4;
			}
			catch (InvalidOperationException ex5)
			{
				result = ex5;
			}
			return result;
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000FC RID: 252 RVA: 0x00004CF0 File Offset: 0x00002EF0
		internal NetworkPackagingLayer PackagingLayer
		{
			get
			{
				return this.m_transport;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000FD RID: 253 RVA: 0x00004CF8 File Offset: 0x00002EF8
		internal TcpChannel TcpChannel
		{
			get
			{
				return this.m_channel;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000FE RID: 254 RVA: 0x00004D00 File Offset: 0x00002F00
		internal bool IsClosed
		{
			get
			{
				return this.m_isClosed;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000FF RID: 255 RVA: 0x00004D08 File Offset: 0x00002F08
		internal bool IsAborted
		{
			get
			{
				return this.m_isAborted;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000100 RID: 256 RVA: 0x00004D10 File Offset: 0x00002F10
		internal bool IsCompressionEnabled
		{
			get
			{
				return this.m_transport.Encoding != NetworkChannel.DataEncodingScheme.Uncompressed;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000101 RID: 257 RVA: 0x00004D23 File Offset: 0x00002F23
		internal bool IsEncryptionEnabled
		{
			get
			{
				return this.TcpChannel.IsEncrypted;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000102 RID: 258 RVA: 0x00004D30 File Offset: 0x00002F30
		internal string PartnerNodeName
		{
			get
			{
				return this.m_channel.PartnerNodeName;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000103 RID: 259 RVA: 0x00004D3D File Offset: 0x00002F3D
		// (set) Token: 0x06000104 RID: 260 RVA: 0x00004D45 File Offset: 0x00002F45
		public string LocalNodeName { get; set; }

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000105 RID: 261 RVA: 0x00004D4E File Offset: 0x00002F4E
		internal NetworkPath NetworkPath
		{
			get
			{
				return this.m_networkPath;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000106 RID: 262 RVA: 0x00004D56 File Offset: 0x00002F56
		// (set) Token: 0x06000107 RID: 263 RVA: 0x00004D5E File Offset: 0x00002F5E
		internal bool KeepAlive
		{
			get
			{
				return this.m_keepAlive;
			}
			set
			{
				this.m_keepAlive = value;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000108 RID: 264 RVA: 0x00004D67 File Offset: 0x00002F67
		internal string RemoteEndPointString
		{
			get
			{
				return this.m_remoteEndPointString;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000109 RID: 265 RVA: 0x00004D6F File Offset: 0x00002F6F
		internal string LocalEndPointString
		{
			get
			{
				return this.m_localEndPointString;
			}
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00004D77 File Offset: 0x00002F77
		internal NetworkChannel(TcpClientChannel ch, NetworkPath path)
		{
			this.Init(ch, path);
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00004D94 File Offset: 0x00002F94
		protected void Init(TcpChannel ch, NetworkPath path)
		{
			this.NetworkChannelManagesAsyncReads = true;
			this.m_channel = ch;
			this.m_networkPath = path;
			this.m_transport = new NetworkPackagingLayer(this, ch);
			this.m_remoteEndPointString = this.m_channel.RemoteEndpoint.ToString();
			this.m_localEndPointString = this.m_channel.LocalEndpoint.ToString();
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00004DF0 File Offset: 0x00002FF0
		private static NetworkChannel FinishConnect(TcpClientChannel tcpChannel, NetworkPath netPath, bool suppressTransparentCompression)
		{
			bool flag = false;
			NetworkChannel networkChannel = null;
			try
			{
				networkChannel = new NetworkChannel(tcpChannel, netPath);
				if (netPath.Purpose != NetworkPath.ConnectionPurpose.TestHealth && netPath.Compress && !suppressTransparentCompression)
				{
					networkChannel.NegotiateCompression();
				}
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					if (networkChannel != null)
					{
						networkChannel.Close();
						networkChannel = null;
					}
					else if (tcpChannel != null)
					{
						tcpChannel.Close();
					}
				}
			}
			return networkChannel;
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00004E54 File Offset: 0x00003054
		public static NetworkChannel OpenChannel(string targetServerName, ISimpleBufferPool socketStreamBufferPool, IPool<SocketStreamAsyncArgs> socketStreamAsyncArgPool, SocketStream.ISocketStreamPerfCounters perfCtrs, bool suppressTransparentCompression)
		{
			if (socketStreamAsyncArgPool != null ^ socketStreamBufferPool != null)
			{
				string message = "SocketStream use requires both pools or neither";
				throw new ArgumentException(message);
			}
			ITcpConnector tcpConnector = Dependencies.TcpConnector;
			NetworkPath netPath = null;
			TcpClientChannel tcpChannel = tcpConnector.OpenChannel(targetServerName, socketStreamBufferPool, socketStreamAsyncArgPool, perfCtrs, out netPath);
			return NetworkChannel.FinishConnect(tcpChannel, netPath, suppressTransparentCompression);
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00004E9D File Offset: 0x0000309D
		internal void NegotiateCompression()
		{
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00004E9F File Offset: 0x0000309F
		internal void SetEncoding(NetworkChannel.DataEncodingScheme scheme)
		{
			this.m_transport.Encoding = scheme;
			ExTraceGlobals.NetworkChannelTracer.TraceDebug<NetworkChannel.DataEncodingScheme>((long)this.GetHashCode(), "Compression selected: {0}", scheme);
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00004EC4 File Offset: 0x000030C4
		internal void SetEncoding(CompressionConfig cfg)
		{
			this.m_transport.SetEncoding(cfg);
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000111 RID: 273 RVA: 0x00004ED2 File Offset: 0x000030D2
		public bool ChecksumDataTransfer
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000112 RID: 274 RVA: 0x00004ED5 File Offset: 0x000030D5
		// (set) Token: 0x06000113 RID: 275 RVA: 0x00004EDD File Offset: 0x000030DD
		public bool NetworkChannelManagesAsyncReads { get; set; }

		// Token: 0x06000114 RID: 276 RVA: 0x00004EE6 File Offset: 0x000030E6
		internal static void StaticTraceDebug(string format, params object[] args)
		{
			ExTraceGlobals.NetworkChannelTracer.TraceDebug(0L, format, args);
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00004EF6 File Offset: 0x000030F6
		internal static void StaticTraceError(string format, params object[] args)
		{
			ExTraceGlobals.NetworkChannelTracer.TraceError(0L, format, args);
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00004F06 File Offset: 0x00003106
		internal void TraceDebug(string format, params object[] args)
		{
			ExTraceGlobals.NetworkChannelTracer.TraceDebug((long)this.GetHashCode(), format, args);
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00004F1B File Offset: 0x0000311B
		internal void TraceError(string format, params object[] args)
		{
			ExTraceGlobals.NetworkChannelTracer.TraceError((long)this.GetHashCode(), format, args);
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00004F30 File Offset: 0x00003130
		internal void InvokeWithCatch(NetworkChannel.CatchableOperation op)
		{
			Exception ex = null;
			bool flag = true;
			try
			{
				op();
				flag = false;
			}
			catch (FileIOonSourceException ex2)
			{
				flag = false;
				ex = ex2;
			}
			catch (IOException ex3)
			{
				if (ex3.InnerException is ObjectDisposedException)
				{
					ex = new NetworkCancelledException(ex3);
				}
				else
				{
					ex = new NetworkCommunicationException(this.PartnerNodeName, ex3.Message, ex3);
				}
			}
			catch (SocketException ex4)
			{
				ex = new NetworkCommunicationException(this.PartnerNodeName, ex4.Message, ex4);
			}
			catch (NetworkCommunicationException ex5)
			{
				ex = ex5;
			}
			catch (NetworkTimeoutException ex6)
			{
				ex = ex6;
			}
			catch (NetworkRemoteException ex7)
			{
				flag = false;
				ex = ex7;
			}
			catch (NetworkEndOfDataException ex8)
			{
				ex = ex8;
			}
			catch (NetworkCorruptDataGenericException)
			{
				ex = new NetworkCorruptDataException(this.PartnerNodeName);
			}
			catch (NetworkTransportException ex9)
			{
				ex = ex9;
			}
			catch (CompressionException innerException)
			{
				ex = new NetworkCorruptDataException(this.PartnerNodeName, innerException);
			}
			catch (DecompressionException innerException2)
			{
				ex = new NetworkCorruptDataException(this.PartnerNodeName, innerException2);
			}
			catch (ObjectDisposedException innerException3)
			{
				ex = new NetworkCancelledException(innerException3);
			}
			catch (SerializationException ex10)
			{
				ex = new NetworkCommunicationException(this.PartnerNodeName, ex10.Message, ex10);
			}
			catch (TargetInvocationException ex11)
			{
				if (ex11.InnerException == null || !(ex11.InnerException is SerializationException))
				{
					throw;
				}
				ex = new NetworkCommunicationException(this.PartnerNodeName, ex11.Message, ex11);
			}
			catch (InvalidOperationException ex12)
			{
				ex = new NetworkCommunicationException(this.PartnerNodeName, ex12.Message, ex12);
			}
			finally
			{
				if (flag)
				{
					this.Abort();
				}
			}
			if (ex != null)
			{
				this.TraceError("InvokeWithCatch: Forwarding exception: {0}", new object[]
				{
					ex
				});
				throw ex;
			}
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00005234 File Offset: 0x00003434
		private void HandleFileIOException(string fullSourceFilename, bool throwCorruptLogDetectedException, Action fileAction)
		{
			try
			{
				fileAction();
			}
			catch (IOException ex)
			{
				ExTraceGlobals.NetworkChannelTracer.TraceError<IOException>((long)this.GetHashCode(), "HandleFileIOException(): Received IOException. Will rethrow it. Ex: {0}", ex);
				if (throwCorruptLogDetectedException)
				{
					CorruptLogDetectedException ex2 = new CorruptLogDetectedException(fullSourceFilename, ex.Message, ex);
					throw new FileIOonSourceException(Environment.MachineName, fullSourceFilename, ex2.Message, ex2);
				}
				throw new FileIOonSourceException(Environment.MachineName, fullSourceFilename, ex.Message, ex);
			}
			catch (UnauthorizedAccessException ex3)
			{
				ExTraceGlobals.NetworkChannelTracer.TraceError<UnauthorizedAccessException>((long)this.GetHashCode(), "HandleFileIOException(): Received UnauthorizedAccessException. Will rethrow it. Ex: {0}", ex3);
				throw new FileIOonSourceException(Environment.MachineName, fullSourceFilename, ex3.Message, ex3);
			}
		}

		// Token: 0x0600011A RID: 282 RVA: 0x000052E0 File Offset: 0x000034E0
		internal NetworkChannelMessage GetMessage()
		{
			this.Read(this.m_tempHeaderBuf, 0, 16);
			return NetworkChannelMessage.ReadMessage(this, this.m_tempHeaderBuf);
		}

		// Token: 0x0600011B RID: 283 RVA: 0x000052FD File Offset: 0x000034FD
		internal NetworkChannelMessage TryGetMessage()
		{
			if (!this.m_transport.HasAsyncDataToRead())
			{
				return null;
			}
			return this.GetMessage();
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00005340 File Offset: 0x00003540
		internal void Read(byte[] buf, int off, int len)
		{
			this.InvokeWithCatch(delegate
			{
				this.m_transport.Read(buf, off, len);
			});
		}

		// Token: 0x0600011D RID: 285 RVA: 0x000053A8 File Offset: 0x000035A8
		internal void StartRead(NetworkChannelCallback callback, object context)
		{
			this.InvokeWithCatch(delegate
			{
				this.m_transport.StartRead(callback, context);
			});
		}

		// Token: 0x0600011E RID: 286 RVA: 0x000053E4 File Offset: 0x000035E4
		internal virtual void Close()
		{
			NetworkChannel.Tracer.TraceFunction((long)this.GetHashCode(), "Closing");
			lock (this)
			{
				if (!this.m_isClosed)
				{
					this.KeepAlive = false;
					if (this.m_transport != null)
					{
						this.m_transport.Close();
					}
					if (this.m_channel != null)
					{
						this.m_channel.Close();
					}
					this.m_isClosed = true;
				}
			}
			NetworkChannel.Tracer.TraceFunction((long)this.GetHashCode(), "Closed");
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00005484 File Offset: 0x00003684
		internal void Abort()
		{
			this.m_isAborted = true;
			this.KeepAlive = false;
			if (this.m_channel != null)
			{
				this.m_channel.Abort();
			}
		}

		// Token: 0x06000120 RID: 288 RVA: 0x000054C8 File Offset: 0x000036C8
		internal void SendException(Exception ex)
		{
			NetworkChannel.Tracer.TraceError<Type>((long)this.GetHashCode(), "SendException: {0}", ex.GetType());
			this.InvokeWithCatch(delegate
			{
				this.m_transport.WriteException(ex);
			});
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00005548 File Offset: 0x00003748
		internal void SendMessage(byte[] buf, int off, int len)
		{
			this.InvokeWithCatch(delegate
			{
				this.m_transport.WriteMessage(buf, off, len);
			});
		}

		// Token: 0x06000122 RID: 290 RVA: 0x000055B8 File Offset: 0x000037B8
		internal void Write(byte[] buf, int off, int len)
		{
			this.InvokeWithCatch(delegate
			{
				this.m_transport.Write(buf, off, len);
			});
		}

		// Token: 0x06000123 RID: 291 RVA: 0x000055FC File Offset: 0x000037FC
		internal void ThrowUnexpectedMessage(NetworkChannelMessage msg)
		{
			NetworkUnexpectedMessageException ex = new NetworkUnexpectedMessageException(this.PartnerNodeName, msg.ToString());
			throw ex;
		}

		// Token: 0x06000124 RID: 292 RVA: 0x0000561C File Offset: 0x0000381C
		internal static void ThrowTimeoutException(string nodeName, string reason)
		{
			throw new NetworkTimeoutException(nodeName, reason);
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00005625 File Offset: 0x00003825
		internal void ThrowTimeoutException(string reason)
		{
			NetworkChannel.ThrowTimeoutException(this.PartnerNodeName, reason);
		}

		// Token: 0x040000BC RID: 188
		private TcpChannel m_channel;

		// Token: 0x040000BD RID: 189
		protected NetworkPackagingLayer m_transport;

		// Token: 0x040000BE RID: 190
		private bool m_isClosed;

		// Token: 0x040000BF RID: 191
		private bool m_isAborted;

		// Token: 0x040000C0 RID: 192
		protected NetworkPath m_networkPath;

		// Token: 0x040000C1 RID: 193
		private bool m_keepAlive;

		// Token: 0x040000C2 RID: 194
		private string m_remoteEndPointString;

		// Token: 0x040000C3 RID: 195
		private string m_localEndPointString;

		// Token: 0x040000C4 RID: 196
		private static readonly ServerVersion FirstVersionSupportingCoconet = new ServerVersion(15, 0, 800, 3);

		// Token: 0x040000C5 RID: 197
		private byte[] m_tempHeaderBuf = new byte[16];

		// Token: 0x0200002C RID: 44
		public enum DataEncodingScheme
		{
			// Token: 0x040000C9 RID: 201
			Uncompressed,
			// Token: 0x040000CA RID: 202
			CompressedXpress,
			// Token: 0x040000CB RID: 203
			Coconet,
			// Token: 0x040000CC RID: 204
			LastIndex
		}

		// Token: 0x0200002D RID: 45
		// (Invoke) Token: 0x06000128 RID: 296
		internal delegate void CatchableOperation();
	}
}
