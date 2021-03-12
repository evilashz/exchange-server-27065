using System;

namespace Microsoft.Exchange.EseRepl
{
	// Token: 0x0200003E RID: 62
	internal interface ITcpConnector
	{
		// Token: 0x0600020F RID: 527
		TcpClientChannel TryConnect(NetworkPath netPath, out NetworkTransportException failureEx);

		// Token: 0x06000210 RID: 528
		TcpClientChannel TryConnect(NetworkPath netPath, int timeoutInMsec, out NetworkTransportException failureEx);

		// Token: 0x06000211 RID: 529
		TcpClientChannel OpenChannel(string targetServerName, ISimpleBufferPool socketStreamBufferPool, IPool<SocketStreamAsyncArgs> socketStreamAsyncArgPool, SocketStream.ISocketStreamPerfCounters perfCtrs, out NetworkPath netPath);

		// Token: 0x06000212 RID: 530
		NetworkPath BuildDnsNetworkPath(string targetServer, int replicationPort);

		// Token: 0x06000213 RID: 531
		NetworkPath ChooseDagNetworkPath(string targetName, string networkName, NetworkPath.ConnectionPurpose purpose);

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000214 RID: 532
		// (set) Token: 0x06000215 RID: 533
		bool ForceSocketStream { get; set; }
	}
}
