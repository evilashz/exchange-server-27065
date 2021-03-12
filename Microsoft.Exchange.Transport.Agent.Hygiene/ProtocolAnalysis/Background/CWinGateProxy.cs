using System;
using System.Net;
using System.Text;

namespace Microsoft.Exchange.Transport.Agent.ProtocolAnalysis.Background
{
	// Token: 0x0200005F RID: 95
	internal class CWinGateProxy : TransportConnection, IDataConnection
	{
		// Token: 0x060002AE RID: 686 RVA: 0x00011F79 File Offset: 0x00010179
		public CWinGateProxy(ProxyChain proxyChain) : base(proxyChain)
		{
		}

		// Token: 0x060002AF RID: 687 RVA: 0x00011F84 File Offset: 0x00010184
		public override void AsyncConnect(IPEndPoint remoteEndpoint, TcpConnection tcpCxn, NetworkCredential authInfo)
		{
			this.tcpCxn = tcpCxn;
			this.connectRequest = new StringBuilder();
			this.connectRequest.AppendFormat("{0}:{1}\r\n", remoteEndpoint.Address.ToString(), remoteEndpoint.Port);
			try
			{
				this.tcpCxn.SendString(this.connectRequest.ToString());
			}
			catch (AtsException)
			{
				base.ProxyChain.OnDisconnected();
			}
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x00012000 File Offset: 0x00010200
		public int OnDataReceived(byte[] dataReceived, int offset, int length)
		{
			int num = 0;
			return num + base.ProxyChain.OnConnected(dataReceived, offset + num, length - num);
		}

		// Token: 0x04000223 RID: 547
		private StringBuilder connectRequest;

		// Token: 0x04000224 RID: 548
		private TcpConnection tcpCxn;

		// Token: 0x02000060 RID: 96
		private enum WinGateProxyState
		{
			// Token: 0x04000226 RID: 550
			Invalid,
			// Token: 0x04000227 RID: 551
			ConnectSent,
			// Token: 0x04000228 RID: 552
			Finished
		}
	}
}
