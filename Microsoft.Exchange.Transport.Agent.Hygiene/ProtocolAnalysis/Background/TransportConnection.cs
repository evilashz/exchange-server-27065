using System;
using System.Net;

namespace Microsoft.Exchange.Transport.Agent.ProtocolAnalysis.Background
{
	// Token: 0x02000047 RID: 71
	internal abstract class TransportConnection
	{
		// Token: 0x06000239 RID: 569 RVA: 0x0000F3EE File Offset: 0x0000D5EE
		protected TransportConnection(ProxyChain proxyChain)
		{
			this.proxyChain = proxyChain;
		}

		// Token: 0x0600023A RID: 570
		public abstract void AsyncConnect(IPEndPoint remoteEndpoint, TcpConnection tcpConnection, NetworkCredential authInfo);

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600023B RID: 571 RVA: 0x0000F3FD File Offset: 0x0000D5FD
		// (set) Token: 0x0600023C RID: 572 RVA: 0x0000F405 File Offset: 0x0000D605
		public IDataConnection DataCxn
		{
			get
			{
				return this.dataCxn;
			}
			set
			{
				this.dataCxn = value;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600023D RID: 573 RVA: 0x0000F40E File Offset: 0x0000D60E
		public ProxyChain ProxyChain
		{
			get
			{
				return this.proxyChain;
			}
		}

		// Token: 0x04000194 RID: 404
		private IDataConnection dataCxn;

		// Token: 0x04000195 RID: 405
		private ProxyChain proxyChain;
	}
}
