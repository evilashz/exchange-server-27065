using System;
using System.Net;
using System.Text;

namespace Microsoft.Exchange.Transport.Agent.ProtocolAnalysis.Background
{
	// Token: 0x0200005D RID: 93
	internal class CTelnetProxy : TransportConnection, IDataConnection
	{
		// Token: 0x060002AB RID: 683 RVA: 0x00011EA7 File Offset: 0x000100A7
		public CTelnetProxy(bool fCisco, ProxyChain proxyChain) : base(proxyChain)
		{
			this.fCisco = fCisco;
		}

		// Token: 0x060002AC RID: 684 RVA: 0x00011EB8 File Offset: 0x000100B8
		public override void AsyncConnect(IPEndPoint remoteEndpoint, TcpConnection tcpCxn, NetworkCredential authInfo)
		{
			this.tcpCxn = tcpCxn;
			this.connectRequest = new StringBuilder();
			if (this.fCisco)
			{
				this.connectRequest.AppendFormat("cisco\r\n", new object[0]);
			}
			this.connectRequest.AppendFormat("telnet {0} {1}\r\n", remoteEndpoint.Address.ToString(), remoteEndpoint.Port);
			try
			{
				this.tcpCxn.SendString(this.connectRequest.ToString());
			}
			catch (AtsException)
			{
				base.ProxyChain.OnDisconnected();
			}
		}

		// Token: 0x060002AD RID: 685 RVA: 0x00011F54 File Offset: 0x00010154
		public int OnDataReceived(byte[] dataReceived, int offset, int length)
		{
			int num = 0;
			return num + base.ProxyChain.OnConnected(dataReceived, offset + num, length - num);
		}

		// Token: 0x0400021C RID: 540
		private StringBuilder connectRequest;

		// Token: 0x0400021D RID: 541
		private bool fCisco;

		// Token: 0x0400021E RID: 542
		private TcpConnection tcpCxn;

		// Token: 0x0200005E RID: 94
		private enum TelnetProxyState
		{
			// Token: 0x04000220 RID: 544
			Invalid,
			// Token: 0x04000221 RID: 545
			ConnectSent,
			// Token: 0x04000222 RID: 546
			Finished
		}
	}
}
