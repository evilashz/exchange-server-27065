using System;
using System.IO;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.Serialization;
using Microsoft.Exchange.Cluster.ActiveManagerServer;
using Microsoft.Exchange.Cluster.Common;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Core;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.HA.DirectoryServices;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;
using Microsoft.Exchange.EseRepl;
using Microsoft.Isam.Esent.Interop;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000247 RID: 583
	internal class NetworkChannel
	{
		// Token: 0x1700062A RID: 1578
		// (get) Token: 0x06001631 RID: 5681 RVA: 0x0005A6E5 File Offset: 0x000588E5
		public static Trace Tracer
		{
			get
			{
				return ExTraceGlobals.NetworkChannelTracer;
			}
		}

		// Token: 0x06001632 RID: 5682 RVA: 0x0005A6EC File Offset: 0x000588EC
		public static NetworkChannel.DataEncodingScheme VerifyDataEncoding(NetworkChannel.DataEncodingScheme requestedEncoding)
		{
			if (requestedEncoding >= NetworkChannel.DataEncodingScheme.LastIndex)
			{
				requestedEncoding = NetworkChannel.DataEncodingScheme.Uncompressed;
			}
			return requestedEncoding;
		}

		// Token: 0x06001633 RID: 5683 RVA: 0x0005A6F8 File Offset: 0x000588F8
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
			return result;
		}

		// Token: 0x06001634 RID: 5684 RVA: 0x0005A75C File Offset: 0x0005895C
		private static void ServiceCallback(object context, int bytesAvailable, bool completionIsSynchronous, Exception e)
		{
			NetworkChannel networkChannel = (NetworkChannel)context;
			networkChannel.TcpChannel.ClearIdle();
			if (bytesAvailable > 0 && e == null)
			{
				networkChannel.ServiceRequest();
				return;
			}
			if (e == null)
			{
				NetworkChannel.Tracer.TraceDebug((long)networkChannel.GetHashCode(), "ServiceCallback: Client closed the channel");
			}
			else
			{
				NetworkChannel.Tracer.TraceError<Exception>((long)networkChannel.GetHashCode(), "ServiceCallback: Channel exception: {0}", e);
			}
			networkChannel.Close();
		}

		// Token: 0x1700062B RID: 1579
		// (get) Token: 0x06001635 RID: 5685 RVA: 0x0005A7C1 File Offset: 0x000589C1
		internal NetworkPackagingLayer PackagingLayer
		{
			get
			{
				return this.m_transport;
			}
		}

		// Token: 0x1700062C RID: 1580
		// (get) Token: 0x06001636 RID: 5686 RVA: 0x0005A7C9 File Offset: 0x000589C9
		internal TcpChannel TcpChannel
		{
			get
			{
				return this.m_channel;
			}
		}

		// Token: 0x1700062D RID: 1581
		// (get) Token: 0x06001637 RID: 5687 RVA: 0x0005A7D1 File Offset: 0x000589D1
		internal bool IsClosed
		{
			get
			{
				return this.m_isClosed;
			}
		}

		// Token: 0x1700062E RID: 1582
		// (get) Token: 0x06001638 RID: 5688 RVA: 0x0005A7D9 File Offset: 0x000589D9
		internal bool IsAborted
		{
			get
			{
				return this.m_isAborted;
			}
		}

		// Token: 0x1700062F RID: 1583
		// (get) Token: 0x06001639 RID: 5689 RVA: 0x0005A7E1 File Offset: 0x000589E1
		internal bool IsCompressionEnabled
		{
			get
			{
				return this.m_transport.Encoding != NetworkChannel.DataEncodingScheme.Uncompressed;
			}
		}

		// Token: 0x17000630 RID: 1584
		// (get) Token: 0x0600163A RID: 5690 RVA: 0x0005A7F4 File Offset: 0x000589F4
		internal bool IsEncryptionEnabled
		{
			get
			{
				return this.TcpChannel.IsEncrypted;
			}
		}

		// Token: 0x17000631 RID: 1585
		// (get) Token: 0x0600163B RID: 5691 RVA: 0x0005A801 File Offset: 0x00058A01
		internal string PartnerNodeName
		{
			get
			{
				return this.m_channel.PartnerNodeName;
			}
		}

		// Token: 0x17000632 RID: 1586
		// (get) Token: 0x0600163C RID: 5692 RVA: 0x0005A80E File Offset: 0x00058A0E
		// (set) Token: 0x0600163D RID: 5693 RVA: 0x0005A816 File Offset: 0x00058A16
		public string LocalNodeName { get; set; }

		// Token: 0x17000633 RID: 1587
		// (get) Token: 0x0600163E RID: 5694 RVA: 0x0005A81F File Offset: 0x00058A1F
		internal NetworkPath NetworkPath
		{
			get
			{
				return this.m_networkPath;
			}
		}

		// Token: 0x17000634 RID: 1588
		// (get) Token: 0x0600163F RID: 5695 RVA: 0x0005A827 File Offset: 0x00058A27
		// (set) Token: 0x06001640 RID: 5696 RVA: 0x0005A82F File Offset: 0x00058A2F
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

		// Token: 0x17000635 RID: 1589
		// (get) Token: 0x06001641 RID: 5697 RVA: 0x0005A838 File Offset: 0x00058A38
		// (set) Token: 0x06001642 RID: 5698 RVA: 0x0005A840 File Offset: 0x00058A40
		internal MonitoredDatabase MonitoredDatabase
		{
			get
			{
				return this.m_sourceDatabase;
			}
			set
			{
				this.m_sourceDatabase = value;
			}
		}

		// Token: 0x17000636 RID: 1590
		// (get) Token: 0x06001643 RID: 5699 RVA: 0x0005A849 File Offset: 0x00058A49
		// (set) Token: 0x06001644 RID: 5700 RVA: 0x0005A851 File Offset: 0x00058A51
		internal SeederPageReaderServerContext SeederPageReaderServerContext
		{
			get
			{
				return this.m_seederPageReaderServerContext;
			}
			set
			{
				this.m_seederPageReaderServerContext = value;
			}
		}

		// Token: 0x17000637 RID: 1591
		// (get) Token: 0x06001645 RID: 5701 RVA: 0x0005A85A File Offset: 0x00058A5A
		// (set) Token: 0x06001646 RID: 5702 RVA: 0x0005A862 File Offset: 0x00058A62
		public ExchangeNetworkPerfmonCounters PerfCounters
		{
			get
			{
				return this.m_networkPerfCounters;
			}
			protected set
			{
				this.m_networkPerfCounters = value;
			}
		}

		// Token: 0x17000638 RID: 1592
		// (get) Token: 0x06001647 RID: 5703 RVA: 0x0005A86B File Offset: 0x00058A6B
		internal string RemoteEndPointString
		{
			get
			{
				return this.m_remoteEndPointString;
			}
		}

		// Token: 0x17000639 RID: 1593
		// (get) Token: 0x06001648 RID: 5704 RVA: 0x0005A873 File Offset: 0x00058A73
		internal string LocalEndPointString
		{
			get
			{
				return this.m_localEndPointString;
			}
		}

		// Token: 0x1700063A RID: 1594
		// (get) Token: 0x06001649 RID: 5705 RVA: 0x0005A87B File Offset: 0x00058A7B
		public string NetworkName
		{
			get
			{
				return this.m_networkName;
			}
		}

		// Token: 0x1700063B RID: 1595
		// (get) Token: 0x0600164A RID: 5706 RVA: 0x0005A883 File Offset: 0x00058A83
		// (set) Token: 0x0600164B RID: 5707 RVA: 0x0005A88B File Offset: 0x00058A8B
		public LogChecksummer LogChecksummer { get; private set; }

		// Token: 0x0600164C RID: 5708 RVA: 0x0005A894 File Offset: 0x00058A94
		public void SetupLogChecksummer(string basename)
		{
			lock (this)
			{
				if (this.LogChecksummer == null)
				{
					this.LogChecksummer = new LogChecksummer(basename);
				}
			}
		}

		// Token: 0x0600164D RID: 5709 RVA: 0x0005A8E0 File Offset: 0x00058AE0
		internal NetworkChannel(TcpClientChannel ch, NetworkPath path)
		{
			this.Init(ch, path);
		}

		// Token: 0x0600164E RID: 5710 RVA: 0x0005A914 File Offset: 0x00058B14
		protected NetworkChannel(TcpServerChannel ch)
		{
			this.Init(ch, null);
			this.m_networkPerfCounters = ch.PerfCounters;
			this.m_networkName = ch.NetworkName;
		}

		// Token: 0x0600164F RID: 5711 RVA: 0x0005A96C File Offset: 0x00058B6C
		protected void Init(TcpChannel ch, NetworkPath path)
		{
			this.NetworkChannelManagesAsyncReads = true;
			this.m_channel = ch;
			this.m_networkPath = path;
			this.m_transport = new NetworkPackagingLayer(this, ch);
			this.m_remoteEndPointString = this.m_channel.RemoteEndpoint.ToString();
			this.m_localEndPointString = this.m_channel.LocalEndpoint.ToString();
		}

		// Token: 0x06001650 RID: 5712 RVA: 0x0005A9C8 File Offset: 0x00058BC8
		internal static NetworkChannel Connect(NetworkPath netPath, int timeoutInMsec, bool ignoreNodeDown)
		{
			TcpClientChannel tcpChannel = NetworkManager.OpenConnection(ref netPath, timeoutInMsec, ignoreNodeDown);
			return NetworkChannel.FinishConnect(tcpChannel, netPath, false);
		}

		// Token: 0x06001651 RID: 5713 RVA: 0x0005A9E8 File Offset: 0x00058BE8
		private static NetworkChannel FinishConnect(TcpClientChannel tcpChannel, NetworkPath netPath, bool suppressTransparentCompression)
		{
			bool flag = false;
			NetworkChannel networkChannel = null;
			try
			{
				networkChannel = new NetworkChannel(tcpChannel, netPath);
				if (netPath.Purpose != NetworkPath.ConnectionPurpose.TestHealth)
				{
					if (netPath.Purpose == NetworkPath.ConnectionPurpose.Seeding)
					{
						networkChannel.IsSeeding = true;
					}
					networkChannel.m_networkPerfCounters = NetworkManager.GetPerfCounters(netPath.NetworkName);
					if (netPath.Compress && !suppressTransparentCompression)
					{
						networkChannel.NegotiateCompression();
					}
				}
				networkChannel.m_networkName = netPath.NetworkName;
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

		// Token: 0x06001652 RID: 5714 RVA: 0x0005AA78 File Offset: 0x00058C78
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

		// Token: 0x06001653 RID: 5715 RVA: 0x0005AAC4 File Offset: 0x00058CC4
		internal void NegotiateCompression()
		{
			if (NetworkChannel.disableCompressionDueToFatalError)
			{
				return;
			}
			AmServerName serverName = new AmServerName(this.PartnerNodeName);
			Exception ex;
			IADServer miniServer = AmBestCopySelectionHelper.GetMiniServer(serverName, out ex);
			if (miniServer == null)
			{
				return;
			}
			if (ServerVersion.Compare(NetworkChannel.FirstVersionSupportingCoconet, miniServer.AdminDisplayVersion) > 0)
			{
				return;
			}
			CompressionConfig compressionConfig = ConfigStore.ReadCompressionConfig(out ex);
			if (compressionConfig.Provider == CompressionConfig.CompressionProvider.None)
			{
				return;
			}
			string text = SerializationUtil.ObjectToXml(compressionConfig);
			NetworkChannelCompressionConfigMsg networkChannelCompressionConfigMsg = new NetworkChannelCompressionConfigMsg(this, NetworkChannelCompressionConfigMsg.MessagePurpose.RequestEncoding, text);
			networkChannelCompressionConfigMsg.Send();
			NetworkChannelMessage message = this.GetMessage();
			NetworkChannelCompressionConfigMsg networkChannelCompressionConfigMsg2 = message as NetworkChannelCompressionConfigMsg;
			if (networkChannelCompressionConfigMsg2 == null)
			{
				this.ThrowUnexpectedMessage(message);
			}
			CompressionConfig encoding = CompressionConfig.Deserialize(networkChannelCompressionConfigMsg2.ConfigXml, out ex);
			if (ex != null)
			{
				ReplayCrimsonEvents.NegotiateCompressionFailure.Log<string, string, Exception>(this.PartnerNodeName, text, ex);
				NetworkChannel.disableCompressionDueToFatalError = true;
				return;
			}
			this.SetEncoding(encoding);
		}

		// Token: 0x06001654 RID: 5716 RVA: 0x0005AB83 File Offset: 0x00058D83
		internal void SetEncoding(NetworkChannel.DataEncodingScheme scheme)
		{
			this.m_transport.Encoding = scheme;
			ExTraceGlobals.NetworkChannelTracer.TraceDebug<NetworkChannel.DataEncodingScheme>((long)this.GetHashCode(), "Compression selected: {0}", scheme);
		}

		// Token: 0x06001655 RID: 5717 RVA: 0x0005ABA8 File Offset: 0x00058DA8
		internal void SetEncoding(CompressionConfig cfg)
		{
			this.m_transport.SetEncoding(cfg);
		}

		// Token: 0x1700063C RID: 1596
		// (get) Token: 0x06001656 RID: 5718 RVA: 0x0005ABB6 File Offset: 0x00058DB6
		public bool ChecksumDataTransfer
		{
			get
			{
				return RegistryParameters.EnableNetworkChecksums != 0;
			}
		}

		// Token: 0x06001657 RID: 5719 RVA: 0x0005ABC4 File Offset: 0x00058DC4
		internal static void ServiceRequests(TcpServerChannel tcpChannel, TcpListener listener)
		{
			NetworkChannel networkChannel = new NetworkChannel(tcpChannel);
			networkChannel.m_listener = listener;
			networkChannel.LocalNodeName = listener.ListenerConfig.LocalNodeName;
			listener.AddActiveChannel(networkChannel);
			networkChannel.ServiceRequest();
		}

		// Token: 0x1700063D RID: 1597
		// (get) Token: 0x06001658 RID: 5720 RVA: 0x0005ABFD File Offset: 0x00058DFD
		// (set) Token: 0x06001659 RID: 5721 RVA: 0x0005AC05 File Offset: 0x00058E05
		public bool NetworkChannelManagesAsyncReads { get; set; }

		// Token: 0x0600165A RID: 5722 RVA: 0x0005AC10 File Offset: 0x00058E10
		private void ServiceRequest()
		{
			bool flag = false;
			Exception ex = null;
			try
			{
				if (this.m_listener.ListenerConfig.KeepServerOpenByDefault)
				{
					this.KeepAlive = true;
				}
				NetworkChannelMessage message = this.GetMessage();
				INetworkChannelRequest networkChannelRequest = message as INetworkChannelRequest;
				if (networkChannelRequest == null)
				{
					this.ThrowUnexpectedMessage(message);
				}
				NetworkChannelDatabaseRequest networkChannelDatabaseRequest = message as NetworkChannelDatabaseRequest;
				if (networkChannelDatabaseRequest != null)
				{
					MonitoredDatabase monitoredDatabase = MonitoredDatabase.FindMonitoredDatabase(this.LocalNodeName, networkChannelDatabaseRequest.DatabaseGuid);
					if (monitoredDatabase == null)
					{
						if (this.MonitoredDatabase == null || !(networkChannelDatabaseRequest.DatabaseGuid == this.MonitoredDatabase.DatabaseGuid) || !(networkChannelDatabaseRequest is ProgressCiFileRequest))
						{
							NetworkChannel.Tracer.TraceError<Guid, Type>((long)this.GetHashCode(), "ServiceRequest for db {0} fails for request {1}", networkChannelDatabaseRequest.DatabaseGuid, networkChannelDatabaseRequest.GetType());
							Exception ex2 = new SourceDatabaseNotFoundException(networkChannelDatabaseRequest.DatabaseGuid, this.LocalNodeName);
							this.SendException(ex2);
							throw ex2;
						}
						NetworkChannel.Tracer.TraceError<Guid>((long)this.GetHashCode(), "ServiceRequest allows ProgressCiFileRequest despite missing monDB for {0}", networkChannelDatabaseRequest.DatabaseGuid);
					}
					else
					{
						networkChannelDatabaseRequest.LinkWithMonitoredDatabase(monitoredDatabase);
					}
					this.KeepAlive = true;
				}
				networkChannelRequest.Execute();
				if (this.KeepAlive)
				{
					if (this.NetworkChannelManagesAsyncReads)
					{
						this.TcpChannel.SetIdle();
						this.StartRead(new NetworkChannelCallback(NetworkChannel.ServiceCallback), this);
					}
					flag = true;
				}
			}
			catch (SocketException ex3)
			{
				ex = ex3;
			}
			catch (IOException ex4)
			{
				ex = ex4;
			}
			catch (NetworkTransportException ex5)
			{
				ex = ex5;
			}
			catch (TransientException ex6)
			{
				ex = ex6;
			}
			finally
			{
				if (ex != null)
				{
					ExTraceGlobals.NetworkChannelTracer.TraceError<Exception>(0L, "ServiceRequest caught: {0}", ex);
				}
				if (!flag)
				{
					this.Close();
				}
			}
		}

		// Token: 0x0600165B RID: 5723 RVA: 0x0005AE0C File Offset: 0x0005900C
		internal static void StaticTraceDebug(string format, params object[] args)
		{
			ExTraceGlobals.NetworkChannelTracer.TraceDebug(0L, format, args);
		}

		// Token: 0x0600165C RID: 5724 RVA: 0x0005AE1C File Offset: 0x0005901C
		internal static void StaticTraceError(string format, params object[] args)
		{
			ExTraceGlobals.NetworkChannelTracer.TraceError(0L, format, args);
		}

		// Token: 0x0600165D RID: 5725 RVA: 0x0005AE2C File Offset: 0x0005902C
		internal void TraceDebug(string format, params object[] args)
		{
			ExTraceGlobals.NetworkChannelTracer.TraceDebug((long)this.GetHashCode(), format, args);
		}

		// Token: 0x0600165E RID: 5726 RVA: 0x0005AE41 File Offset: 0x00059041
		internal void TraceError(string format, params object[] args)
		{
			ExTraceGlobals.NetworkChannelTracer.TraceError((long)this.GetHashCode(), format, args);
		}

		// Token: 0x0600165F RID: 5727 RVA: 0x0005AE58 File Offset: 0x00059058
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
					this.ReportNetworkError(ex3);
					ex = new NetworkCommunicationException(this.PartnerNodeName, ex3.Message, ex3);
				}
			}
			catch (SocketException ex4)
			{
				this.ReportNetworkError(ex4);
				ex = new NetworkCommunicationException(this.PartnerNodeName, ex4.Message, ex4);
			}
			catch (NetworkCommunicationException ex5)
			{
				this.ReportNetworkError(ex5);
				ex = ex5;
			}
			catch (NetworkTimeoutException ex6)
			{
				this.ReportNetworkError(ex6);
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

		// Token: 0x06001660 RID: 5728 RVA: 0x0005B144 File Offset: 0x00059344
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

		// Token: 0x06001661 RID: 5729 RVA: 0x0005B1F0 File Offset: 0x000593F0
		protected void ReportNetworkError(Exception e)
		{
			if (this.NetworkPath != null && !this.IsClosed && !this.IsAborted)
			{
				NetworkManager.ReportError(this.NetworkPath, e);
			}
		}

		// Token: 0x06001662 RID: 5730 RVA: 0x0005B216 File Offset: 0x00059416
		internal NetworkChannelMessage GetMessage()
		{
			this.Read(this.m_tempHeaderBuf, 0, 16);
			return NetworkChannelMessage.ReadMessage(this, this.m_tempHeaderBuf);
		}

		// Token: 0x06001663 RID: 5731 RVA: 0x0005B233 File Offset: 0x00059433
		internal NetworkChannelMessage TryGetMessage()
		{
			if (!this.m_transport.HasAsyncDataToRead())
			{
				return null;
			}
			return this.GetMessage();
		}

		// Token: 0x06001664 RID: 5732 RVA: 0x0005B278 File Offset: 0x00059478
		internal void Read(byte[] buf, int off, int len)
		{
			this.InvokeWithCatch(delegate
			{
				this.m_transport.Read(buf, off, len);
			});
		}

		// Token: 0x06001665 RID: 5733 RVA: 0x0005B2E0 File Offset: 0x000594E0
		internal void StartRead(NetworkChannelCallback callback, object context)
		{
			if (this.m_isAborted || this.m_isClosed)
			{
				throw new NetworkCancelledException();
			}
			this.InvokeWithCatch(delegate
			{
				this.m_transport.StartRead(callback, context);
			});
		}

		// Token: 0x06001666 RID: 5734 RVA: 0x0005B330 File Offset: 0x00059530
		internal void Close()
		{
			NetworkChannel.Tracer.TraceFunction((long)this.GetHashCode(), "Closing");
			lock (this)
			{
				if (!this.m_isClosed)
				{
					this.KeepAlive = false;
					if (this.LogChecksummer != null)
					{
						this.LogChecksummer.Dispose();
						this.LogChecksummer = null;
					}
					if (this.m_listener != null)
					{
						this.m_listener.RemoveActiveChannel(this);
					}
					if (this.m_seederPageReaderServerContext != null)
					{
						this.m_seederPageReaderServerContext.Close();
						this.m_seederPageReaderServerContext = null;
					}
					if (this.m_seederServerContext != null)
					{
						this.CloseSeederServerContext();
					}
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

		// Token: 0x06001667 RID: 5735 RVA: 0x0005B428 File Offset: 0x00059628
		internal void Abort()
		{
			this.m_isAborted = true;
			this.KeepAlive = false;
			if (this.m_channel != null)
			{
				this.m_channel.Abort();
			}
		}

		// Token: 0x06001668 RID: 5736 RVA: 0x0005B46C File Offset: 0x0005966C
		internal void SendException(Exception ex)
		{
			NetworkChannel.Tracer.TraceError<Type>((long)this.GetHashCode(), "SendException: {0}", ex.GetType());
			this.InvokeWithCatch(delegate
			{
				this.m_transport.WriteException(ex);
			});
		}

		// Token: 0x06001669 RID: 5737 RVA: 0x0005B4EC File Offset: 0x000596EC
		internal void SendMessage(byte[] buf, int off, int len)
		{
			this.InvokeWithCatch(delegate
			{
				this.m_transport.WriteMessage(buf, off, len);
			});
		}

		// Token: 0x0600166A RID: 5738 RVA: 0x0005B55C File Offset: 0x0005975C
		internal void Write(byte[] buf, int off, int len)
		{
			this.InvokeWithCatch(delegate
			{
				this.m_transport.Write(buf, off, len);
			});
		}

		// Token: 0x0600166B RID: 5739 RVA: 0x0005B5A0 File Offset: 0x000597A0
		internal void ThrowUnexpectedMessage(NetworkChannelMessage msg)
		{
			NetworkUnexpectedMessageException ex = new NetworkUnexpectedMessageException(this.PartnerNodeName, msg.ToString());
			throw ex;
		}

		// Token: 0x0600166C RID: 5740 RVA: 0x0005B5C0 File Offset: 0x000597C0
		internal static void ThrowTimeoutException(string nodeName, string reason)
		{
			throw new NetworkTimeoutException(nodeName, reason);
		}

		// Token: 0x0600166D RID: 5741 RVA: 0x0005B5C9 File Offset: 0x000597C9
		internal void ThrowTimeoutException(string reason)
		{
			NetworkChannel.ThrowTimeoutException(this.PartnerNodeName, reason);
		}

		// Token: 0x0600166E RID: 5742 RVA: 0x0005B5D8 File Offset: 0x000597D8
		internal SeederPageReaderServerContext GetSeederPageReaderServerContext(string databaseName, string databasePath)
		{
			SeederPageReaderServerContext seederPageReaderServerContext;
			lock (this.m_SeederPageReaderServerContextLocker)
			{
				seederPageReaderServerContext = this.SeederPageReaderServerContext;
				if (seederPageReaderServerContext == null)
				{
					if (string.IsNullOrEmpty(databasePath))
					{
						databasePath = this.m_sourceDatabase.Config.DestinationEdbPath;
					}
					IEseDatabaseReader eseDatabaseReader = EseDatabaseReader.GetEseDatabaseReader(Environment.MachineName, this.m_sourceDatabase.DatabaseGuid, databaseName, databasePath);
					seederPageReaderServerContext = new SeederPageReaderServerContext(eseDatabaseReader, this.PartnerNodeName);
					this.SeederPageReaderServerContext = seederPageReaderServerContext;
				}
			}
			return seederPageReaderServerContext;
		}

		// Token: 0x0600166F RID: 5743 RVA: 0x0005B664 File Offset: 0x00059864
		internal SeederServerContext CreateSeederServerContext(Guid dbGuid, Guid? serverGuid, SeedType seedType)
		{
			SeederServerContext seederServerContext = null;
			lock (this.m_SeederServerContextLocker)
			{
				if (this.m_isClosed)
				{
					throw new SeedingChannelIsClosedException(dbGuid);
				}
				if (this.m_seederServerContext != null)
				{
					ReplayCrimsonEvents.SeedingSourceError.Log<Guid, string, string, string>(dbGuid, string.Empty, this.PartnerNodeName, "CreateSeederServerContext:SeedCtx already exists");
					throw new SeedingChannelIsClosedException(dbGuid);
				}
				this.m_seederServerContext = new SeederServerContext(this, this.MonitoredDatabase, serverGuid, seedType);
				seederServerContext = this.m_seederServerContext;
				this.TraceDebug("ServerContext for Database {0} is created", new object[]
				{
					dbGuid
				});
			}
			SourceSeedTable.Instance.RegisterSeed(seederServerContext);
			seederServerContext.StartSeeding();
			return seederServerContext;
		}

		// Token: 0x06001670 RID: 5744 RVA: 0x0005B724 File Offset: 0x00059924
		internal SeederServerContext GetSeederServerContext(Guid dbGuid)
		{
			SeederServerContext seederServerContext;
			lock (this.m_SeederServerContextLocker)
			{
				if (this.m_seederServerContext == null)
				{
					ReplayCrimsonEvents.SeedingSourceError.Log<Guid, string, string, string>(dbGuid, string.Empty, this.PartnerNodeName, "GetSeederServerContext:SeedCtx does not exist");
					throw new SeedingChannelIsClosedException(dbGuid);
				}
				DiagCore.RetailAssert(dbGuid.Equals(this.m_seederServerContext.DatabaseGuid), "SeederServer inconsistent. Requested ({0}) Found({1})", new object[]
				{
					dbGuid,
					this.m_seederServerContext.DatabaseGuid
				});
				seederServerContext = this.m_seederServerContext;
			}
			return seederServerContext;
		}

		// Token: 0x06001671 RID: 5745 RVA: 0x0005B7D0 File Offset: 0x000599D0
		private void CloseSeederServerContext()
		{
			SeederServerContext seederServerContext = null;
			lock (this.m_SeederServerContextLocker)
			{
				if (this.m_seederServerContext != null)
				{
					seederServerContext = this.m_seederServerContext;
					this.m_seederServerContext = null;
				}
			}
			if (seederServerContext != null)
			{
				SourceSeedTable.Instance.DeregisterSeed(seederServerContext);
				seederServerContext.Close();
				NetworkChannel.Tracer.TraceDebug<Guid>((long)this.GetHashCode(), "ServerContext for Database {0} is closed and cleared.", seederServerContext.DatabaseGuid);
			}
		}

		// Token: 0x1700063E RID: 1598
		// (get) Token: 0x06001672 RID: 5746 RVA: 0x0005B854 File Offset: 0x00059A54
		// (set) Token: 0x06001673 RID: 5747 RVA: 0x0005B85C File Offset: 0x00059A5C
		internal bool IsSeeding
		{
			get
			{
				return this.m_isSeeding;
			}
			set
			{
				if (value != this.m_isSeeding)
				{
					this.m_isSeeding = value;
					if (this.m_isSeeding)
					{
						this.TcpChannel.ReadTimeoutInMs = RegistryParameters.SeedingNetworkTimeoutInMsec;
						this.TcpChannel.WriteTimeoutInMs = RegistryParameters.SeedingNetworkTimeoutInMsec;
						return;
					}
					this.TcpChannel.ReadTimeoutInMs = RegistryParameters.LogShipTimeoutInMsec;
					this.TcpChannel.WriteTimeoutInMs = RegistryParameters.LogShipTimeoutInMsec;
				}
			}
		}

		// Token: 0x06001674 RID: 5748 RVA: 0x0005B8C2 File Offset: 0x00059AC2
		internal void ReceiveFile(NetworkChannelFileTransferReply reply, IPerfmonCounters copyPerfCtrs, CheckSummer summer)
		{
			this.ReceiveFileInternal(reply, null, copyPerfCtrs, summer);
		}

		// Token: 0x06001675 RID: 5749 RVA: 0x0005B8D0 File Offset: 0x00059AD0
		internal void ReceiveSeedingData(NetworkChannelFileTransferReply reply, IReplicaSeederCallback callback)
		{
			CheckSummer summer = null;
			if (this.ChecksumDataTransfer)
			{
				summer = new CheckSummer();
			}
			this.ReceiveFileInternal(reply, callback, null, summer);
		}

		// Token: 0x06001676 RID: 5750 RVA: 0x0005B8F7 File Offset: 0x00059AF7
		internal void ReceiveFile(NetworkChannelFileTransferReply reply, IReplicaSeederCallback callback, IPerfmonCounters copyPerfCtrs, CheckSummer summer)
		{
			this.ReceiveFileInternal(reply, callback, copyPerfCtrs, summer);
		}

		// Token: 0x06001677 RID: 5751 RVA: 0x0005B904 File Offset: 0x00059B04
		private void ReceiveFileInternal(NetworkChannelFileTransferReply reply, IReplicaSeederCallback callback, IPerfmonCounters copyPerfCtrs, CheckSummer summer)
		{
			int num = 65536;
			if (this.m_receiveBuf == null)
			{
				this.m_receiveBuf = new byte[num];
			}
			byte[] receiveBuf = this.m_receiveBuf;
			bool flag = false;
			bool flag2 = false;
			StopwatchStamp stamp = StopwatchStamp.GetStamp();
			long num2 = 0L;
			long num3 = 0L;
			long num4 = 0L;
			double num5 = -1.0;
			try
			{
				this.TraceDebug("Receiving '{0}'.", new object[]
				{
					reply.DestinationFileName
				});
				int num6 = 0;
				uint sectorSize = FileOperations.GetSectorSize(reply.DestinationFileName);
				long num7 = Align.RoundUp(reply.FileSize, (long)((ulong)sectorSize));
				using (SafeFileHandle safeFileHandle = LogCopy.OpenFile(reply.DestinationFileName, false, out num6))
				{
					flag2 = true;
					FileStream fileStream = null;
					try
					{
						fileStream = LogCopy.OpenFileStream(safeFileHandle, false);
						fileStream.SetLength(num7);
						long num8 = stamp.Restart();
						NetworkChannel.Tracer.TracePerformance<long>((long)this.GetHashCode(), "ReceiveFileInternal: Ready to write after {0} uSec", StopwatchStamp.TicksToMicroSeconds(num8));
						long num9 = reply.FileSize;
						long num10 = num9;
						while (num9 > 0L)
						{
							int num11 = num;
							if (num9 < (long)((ulong)num))
							{
								num11 = (int)num9;
							}
							num8 += stamp.Restart();
							this.Read(receiveBuf, 0, num11);
							num2 += stamp.Restart();
							num4 += (long)((ulong)num11);
							num9 -= (long)num11;
							if (this.PerfCounters != null)
							{
								if (this.IsSeeding)
								{
									this.PerfCounters.RecordSeederThruputReceived((long)num11);
								}
								else
								{
									this.PerfCounters.RecordLogCopyThruputReceived((long)num11);
								}
							}
							if (summer != null)
							{
								summer.Sum(receiveBuf, 0, num11);
							}
							int num12 = num11;
							int num13 = (int)((long)num12 % (long)((ulong)sectorSize));
							if (num13 != 0)
							{
								num13 = (int)(sectorSize - (uint)num13);
								BufferOperations.Zero(receiveBuf, num12, num13);
								num12 += num13;
							}
							if (callback != null && callback.IsBackupCancelled())
							{
								ExTraceGlobals.SeederServerTracer.TraceDebug<long>((long)this.GetHashCode(), "The seeding was cancelled at the size of {0} kB.\n", num3 / 1024L);
								throw new SeederOperationAbortedException();
							}
							double num14 = (double)num3 * 100.0 / (double)num10;
							if (num11 == 0 || num14 > num5)
							{
								if (callback != null)
								{
									ExTraceGlobals.SeederServerTracer.TraceDebug<string, double, bool>((long)this.GetHashCode(), "Updating progress for edb '{0}'. Percent = {1}. Callback = {2}", reply.DestinationFileName, num14, callback != null);
									callback.ReportProgress(reply.DestinationFileName, num10, num4, num3);
								}
								num5 = num14;
							}
							fileStream.Write(receiveBuf, 0, num12);
							num3 += (long)((ulong)num12);
						}
						if (summer != null)
						{
							this.Read(receiveBuf, 0, 4);
							uint num15 = BitConverter.ToUInt32(receiveBuf, 0);
							if (summer.GetSum() != num15)
							{
								throw new NetworkTransportException("csum err");
							}
						}
						fileStream.Flush();
						fileStream.Dispose();
						fileStream = null;
						if (reply.FileSize != num7)
						{
							NetworkChannel.Tracer.TraceDebug<string, long, long>((long)this.GetHashCode(), "File {0} was written unbuffered, and is now 0x{1:x} bytes. Now shrinking it to 0x{2:x}.", reply.DestinationFileName, num7, reply.FileSize);
							FileOperations.TruncateFile(reply.DestinationFileName, reply.FileSize);
						}
						FileInfo fileInfo = new FileInfo(reply.DestinationFileName);
						fileInfo.LastWriteTimeUtc = reply.LastWriteUtc;
						num8 += stamp.ElapsedTicks;
						if (copyPerfCtrs != null)
						{
							copyPerfCtrs.RecordFileModeWriteLatency(num8);
							copyPerfCtrs.RecordLogCopierNetworkReadLatency(num2);
						}
						long num16 = StopwatchStamp.TicksToMicroSeconds(num8);
						long num17 = StopwatchStamp.TicksToMicroSeconds(num2);
						NetworkChannel.Tracer.TracePerformance<long, long, long>((long)this.GetHashCode(), "ReceiveFileInternal: Finished after {0} ms. Disk={1} uSec Net={2} uSec", (num16 + num17) / 1000L, num16, num17);
						flag = true;
					}
					finally
					{
						if (fileStream != null)
						{
							FileCleanup.DisposeProperly(fileStream);
						}
					}
				}
				this.TraceDebug("Total bytes read: {0}. Total bytes written: {1}. Original source DB file size: {2}", new object[]
				{
					num4,
					num3,
					reply.FileSize
				});
			}
			finally
			{
				if (!flag && flag2)
				{
					FileCleanup.TryDelete(reply.DestinationFileName);
				}
			}
		}

		// Token: 0x06001678 RID: 5752 RVA: 0x0005BD1C File Offset: 0x00059F1C
		public void SendFileTransferReply(NetworkChannelFileTransferReply reply, string fullSourceFilename, SafeFileHandle readFileHandle, CheckSummer summer)
		{
			this.InvokeWithCatch(delegate
			{
				this.SendFileInternal(reply, fullSourceFilename, readFileHandle, null, summer, false);
			});
		}

		// Token: 0x06001679 RID: 5753 RVA: 0x0005BD9C File Offset: 0x00059F9C
		public void SendLogFileTransferReply(NetworkChannelFileTransferReply reply, string fullSourceFilename, SafeFileHandle readFileHandle, SourceDatabasePerformanceCountersInstance perfCounters, CheckSummer summer)
		{
			this.InvokeWithCatch(delegate
			{
				this.SendFileInternal(reply, fullSourceFilename, readFileHandle, perfCounters, summer, true);
			});
		}

		// Token: 0x0600167A RID: 5754 RVA: 0x0005C0A0 File Offset: 0x0005A2A0
		private void SendFileInternal(NetworkChannelFileTransferReply reply, string fullSourceFilename, SafeFileHandle readFileHandle, SourceDatabasePerformanceCountersInstance perfCounters, CheckSummer summer, bool isLogFile)
		{
			FileStream fs = null;
			FileInfo fileInfo = null;
			StopwatchStamp timer = StopwatchStamp.GetStamp();
			long diskLatency = 0L;
			long num = 0L;
			long num2 = 0L;
			long num3 = 0L;
			long num4 = 0L;
			try
			{
				this.HandleFileIOException(fullSourceFilename, false, delegate
				{
					fs = LogCopy.OpenFileStream(readFileHandle, true);
					fileInfo = new FileInfo(fullSourceFilename);
					reply.FileSize = fileInfo.Length;
					reply.LastWriteUtc = fileInfo.LastWriteTimeUtc;
				});
				diskLatency = timer.Restart();
				NetworkChannel.Tracer.TracePerformance<long>((long)this.GetHashCode(), "SendFileInternal prep in {0} uSec", StopwatchStamp.TicksToMicroSeconds(diskLatency));
				reply.Send();
				long num5 = timer.Restart();
				int bufSize = 65536;
				if (isLogFile)
				{
					bufSize = (int)fileInfo.Length;
				}
				long bytesRemaining = 0L;
				BinaryReader reader = null;
				this.HandleFileIOException(fullSourceFilename, false, delegate
				{
					bytesRemaining = fileInfo.Length;
					reader = new BinaryReader(fs);
				});
				while (bytesRemaining > 0L)
				{
					byte[] buf = null;
					int bytesRead = 0;
					this.HandleFileIOException(fullSourceFilename, true, delegate
					{
						if (isLogFile && 1048576L != fileInfo.Length)
						{
							throw new IOException(ReplayStrings.UnexpectedEOF(fullSourceFilename));
						}
						int num9 = (int)Math.Min(bytesRemaining, (long)bufSize);
						buf = reader.ReadBytes(num9);
						bytesRead = buf.Length;
						if (bytesRead != num9)
						{
							ExTraceGlobals.NetworkChannelTracer.TraceError<int, int, string>((long)this.GetHashCode(), "SendFileInternal. Expected {0} but got {1} bytes from {2}", num9, bytesRead, fullSourceFilename);
							throw new IOException(ReplayStrings.UnexpectedEOF(fullSourceFilename));
						}
						diskLatency += timer.Restart();
						if (summer != null)
						{
							summer.Sum(buf, 0, bytesRead);
						}
						if (isLogFile && this.LogChecksummer != null)
						{
							EsentErrorException ex = this.LogChecksummer.Verify(fullSourceFilename, buf);
							if (ex != null)
							{
								NetworkChannel.Tracer.TraceError<string, EsentErrorException>((long)this.GetHashCode(), "LogChecksum({0}) failed: {1}", fullSourceFilename, ex);
								if (ex is EsentLogFileCorruptException)
								{
									CorruptLogDetectedException ex2 = new CorruptLogDetectedException(fullSourceFilename, ex.Message, ex);
									throw new FileIOonSourceException(Environment.MachineName, fullSourceFilename, ex2.Message, ex2);
								}
								throw new FileIOonSourceException(Environment.MachineName, fullSourceFilename, ex.Message, ex);
							}
						}
					});
					num += timer.Restart();
					this.Write(buf, 0, bytesRead);
					num5 += timer.Restart();
					if (perfCounters != null)
					{
						perfCounters.AverageWriteTime.IncrementBy(num5);
						perfCounters.AverageWriteTimeBase.Increment();
						perfCounters.AverageReadTime.IncrementBy(diskLatency);
						perfCounters.AverageReadTimeBase.Increment();
						perfCounters.TotalBytesSent.IncrementBy((long)bytesRead);
						perfCounters.WriteThruput.IncrementBy((long)bytesRead);
					}
					num3 += diskLatency;
					num2 += num5;
					num4 += num;
					diskLatency = 0L;
					num5 = 0L;
					num = 0L;
					bytesRemaining -= (long)bytesRead;
				}
				if (summer != null)
				{
					byte[] bytes = BitConverter.GetBytes(summer.GetSum());
					this.Write(bytes, 0, 4);
				}
				this.m_transport.Flush();
				num5 += timer.Restart();
				NetworkChannel.Tracer.TraceDebug<string>((long)this.GetHashCode(), "Tcp.SendFile sent {0}", fullSourceFilename);
				long num6 = StopwatchStamp.TicksToMicroSeconds(num3);
				long num7 = StopwatchStamp.TicksToMicroSeconds(num2);
				long num8 = StopwatchStamp.TicksToMicroSeconds(num4);
				NetworkChannel.Tracer.TracePerformance((long)this.GetHashCode(), "SendFile finished in {0} uSec. Read={1} Write={2} Verify={3}", new object[]
				{
					num6 + num7 + num8,
					num6,
					num7,
					num8
				});
			}
			finally
			{
				if (fs != null)
				{
					fs.Dispose();
				}
			}
		}

		// Token: 0x0600167B RID: 5755 RVA: 0x0005C444 File Offset: 0x0005A644
		public void SendSeedingDataTransferReply(SeedDatabaseFileReply reply, ReadDatabaseCallback readDbCallback)
		{
			this.InvokeWithCatch(delegate
			{
				this.SendSeedingDataInternal(reply, readDbCallback);
			});
		}

		// Token: 0x0600167C RID: 5756 RVA: 0x0005C57C File Offset: 0x0005A77C
		private void SendSeedingDataInternal(SeedDatabaseFileReply reply, ReadDatabaseCallback readDbCallback)
		{
			if (reply == null)
			{
				throw new ArgumentNullException("reply");
			}
			if (readDbCallback == null)
			{
				throw new ArgumentNullException("readDbCallback");
			}
			int num = 65536;
			byte[] buf = new byte[num];
			ulong totalBytesRead = 0UL;
			bool isPassiveCopy = this.MonitoredDatabase.IsPassiveCopy;
			string fullSourceFilename;
			if (isPassiveCopy)
			{
				fullSourceFilename = this.MonitoredDatabase.Config.SourceEdbPath;
			}
			else
			{
				fullSourceFilename = this.MonitoredDatabase.Config.DestinationEdbPath;
			}
			ulong num2 = (ulong)reply.FileSize;
			int bytesRead = 0;
			CheckSummer checkSummer = null;
			if (this.ChecksumDataTransfer)
			{
				checkSummer = new CheckSummer();
			}
			ReplayStopwatch replayStopwatch = new ReplayStopwatch();
			replayStopwatch.Start();
			do
			{
				bool bytesExpectedWasZero = false;
				int bytesExpected = (int)Math.Min(num2, (ulong)((long)num));
				if (bytesExpected == 0)
				{
					bytesExpectedWasZero = true;
					bytesExpected = num;
				}
				this.HandleFileIOException(fullSourceFilename, false, delegate
				{
					bytesRead = readDbCallback(buf, totalBytesRead, bytesExpected);
					if (bytesRead != 0)
					{
						if (bytesRead < bytesExpected)
						{
							ExTraceGlobals.NetworkChannelTracer.TraceError<int, int>((long)this.GetHashCode(), "SendSeedingDataInternal. Expected {0} but got {1} bytes. This is may be a perf problem.", bytesExpected, bytesRead);
						}
						return;
					}
					if (!bytesExpectedWasZero)
					{
						throw new IOException("Read zero bytes while bytes expected to read is " + bytesExpected);
					}
					ExTraceGlobals.NetworkChannelTracer.TraceError<int, int>((long)this.GetHashCode(), "SendSeedingDataInternal. Expected {0} but got {1} bytes", bytesExpected, bytesRead);
				});
				if (bytesRead > 0)
				{
					if (checkSummer != null)
					{
						checkSummer.Sum(buf, 0, bytesRead);
					}
					this.Write(buf, 0, bytesRead);
					num2 -= (ulong)((long)bytesRead);
					totalBytesRead += (ulong)((long)bytesRead);
				}
			}
			while (bytesRead > 0);
			if (checkSummer != null)
			{
				byte[] bytes = BitConverter.GetBytes(checkSummer.GetSum());
				this.Write(bytes, 0, 4);
			}
			this.m_transport.Flush();
			replayStopwatch.Stop();
			string databaseName = this.MonitoredDatabase.DatabaseName;
			string partnerNodeName = this.PartnerNodeName;
			long num3 = replayStopwatch.ElapsedMilliseconds / 1000L;
			if (num3 <= 0L)
			{
				num3 = 1L;
			}
			long num4 = (long)(totalBytesRead / (ulong)num3 / 1024UL);
			string text = string.Format("{0} KB/sec", num4);
			ByteQuantifiedSize byteQuantifiedSize = new ByteQuantifiedSize(totalBytesRead);
			ExTraceGlobals.SeederServerTracer.TraceDebug((long)this.GetHashCode(), "SeedDatabaseFile({0}): Sent {1} bytes in {2} sec = {3}. Target={4}", new object[]
			{
				databaseName,
				totalBytesRead,
				num3,
				text,
				partnerNodeName
			});
			if (isPassiveCopy)
			{
				ReplayCrimsonEvents.PassiveSeedSourceSentEDB.Log<string, string, string, ByteQuantifiedSize, TimeSpan, string>(databaseName, Environment.MachineName, partnerNodeName, byteQuantifiedSize, replayStopwatch.Elapsed, text);
				return;
			}
			ReplayCrimsonEvents.ActiveSeedSourceSentEDB.Log<string, string, string, ByteQuantifiedSize, TimeSpan, string>(databaseName, Environment.MachineName, partnerNodeName, byteQuantifiedSize, replayStopwatch.Elapsed, text);
		}

		// Token: 0x040008C3 RID: 2243
		private TcpChannel m_channel;

		// Token: 0x040008C4 RID: 2244
		protected NetworkPackagingLayer m_transport;

		// Token: 0x040008C5 RID: 2245
		private bool m_isClosed;

		// Token: 0x040008C6 RID: 2246
		private bool m_isAborted;

		// Token: 0x040008C7 RID: 2247
		protected NetworkPath m_networkPath;

		// Token: 0x040008C8 RID: 2248
		private bool m_keepAlive;

		// Token: 0x040008C9 RID: 2249
		private MonitoredDatabase m_sourceDatabase;

		// Token: 0x040008CA RID: 2250
		private SeederPageReaderServerContext m_seederPageReaderServerContext;

		// Token: 0x040008CB RID: 2251
		private SeederServerContext m_seederServerContext;

		// Token: 0x040008CC RID: 2252
		public object m_SeederPageReaderServerContextLocker = new object();

		// Token: 0x040008CD RID: 2253
		public object m_SeederServerContextLocker = new object();

		// Token: 0x040008CE RID: 2254
		private TcpListener m_listener;

		// Token: 0x040008CF RID: 2255
		private ExchangeNetworkPerfmonCounters m_networkPerfCounters;

		// Token: 0x040008D0 RID: 2256
		private string m_remoteEndPointString;

		// Token: 0x040008D1 RID: 2257
		private string m_localEndPointString;

		// Token: 0x040008D2 RID: 2258
		private string m_networkName;

		// Token: 0x040008D3 RID: 2259
		private static bool disableCompressionDueToFatalError = false;

		// Token: 0x040008D4 RID: 2260
		private static readonly ServerVersion FirstVersionSupportingCoconet = new ServerVersion(15, 0, 800, 3);

		// Token: 0x040008D5 RID: 2261
		private byte[] m_tempHeaderBuf = new byte[16];

		// Token: 0x040008D6 RID: 2262
		private bool m_isSeeding;

		// Token: 0x040008D7 RID: 2263
		private byte[] m_receiveBuf;

		// Token: 0x02000248 RID: 584
		public enum DataEncodingScheme
		{
			// Token: 0x040008DC RID: 2268
			Uncompressed,
			// Token: 0x040008DD RID: 2269
			CompressedXpress,
			// Token: 0x040008DE RID: 2270
			Coconet,
			// Token: 0x040008DF RID: 2271
			LastIndex
		}

		// Token: 0x02000249 RID: 585
		// (Invoke) Token: 0x0600167F RID: 5759
		internal delegate void CatchableOperation();
	}
}
