using System;
using Microsoft.Exchange.EseRepl;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200026E RID: 622
	internal interface ITcpConnector
	{
		// Token: 0x06001853 RID: 6227
		TcpClientChannel TryConnect(NetworkPath netPath, out NetworkTransportException failureEx);

		// Token: 0x06001854 RID: 6228
		TcpClientChannel TryConnect(NetworkPath netPath, int timeoutInMsec, out NetworkTransportException failureEx);

		// Token: 0x06001855 RID: 6229
		TcpClientChannel OpenChannel(string targetServerName, ISimpleBufferPool socketStreamBufferPool, IPool<SocketStreamAsyncArgs> socketStreamAsyncArgPool, SocketStream.ISocketStreamPerfCounters perfCtrs, out NetworkPath netPath);

		// Token: 0x06001856 RID: 6230
		NetworkPath BuildDnsNetworkPath(string targetServer, int replicationPort);

		// Token: 0x06001857 RID: 6231
		NetworkPath ChooseDagNetworkPath(string targetName, string networkName, NetworkPath.ConnectionPurpose purpose);

		// Token: 0x170006BA RID: 1722
		// (get) Token: 0x06001858 RID: 6232
		// (set) Token: 0x06001859 RID: 6233
		bool ForceSocketStream { get; set; }
	}
}
