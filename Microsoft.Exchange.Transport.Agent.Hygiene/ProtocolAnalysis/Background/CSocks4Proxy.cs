using System;
using System.Net;
using System.Net.Sockets;

namespace Microsoft.Exchange.Transport.Agent.ProtocolAnalysis.Background
{
	// Token: 0x02000055 RID: 85
	internal class CSocks4Proxy : TransportConnection, IDataConnection
	{
		// Token: 0x0600029A RID: 666 RVA: 0x000114D0 File Offset: 0x0000F6D0
		public CSocks4Proxy(ProxyChain proxyChain) : base(proxyChain)
		{
		}

		// Token: 0x0600029B RID: 667 RVA: 0x000114DC File Offset: 0x0000F6DC
		public override void AsyncConnect(IPEndPoint remoteEndpoint, TcpConnection tcpCxn, NetworkCredential authInfo)
		{
			this.tcpCxn = tcpCxn;
			if (remoteEndpoint.Address.AddressFamily == AddressFamily.InterNetworkV6)
			{
				base.ProxyChain.OnDisconnected();
				return;
			}
			this.connectRequest = new byte[8];
			this.connectRequest[0] = 4;
			this.connectRequest[1] = 1;
			this.connectRequest[2] = (byte)(remoteEndpoint.Port >> 8 & 255);
			this.connectRequest[3] = (byte)(remoteEndpoint.Port & 255);
			byte[] addressBytes = remoteEndpoint.Address.GetAddressBytes();
			Array.Copy(addressBytes, 0, this.connectRequest, 4, 4);
			for (int i = 0; i < authInfo.UserName.Length; i++)
			{
				this.connectRequest[i + 3] = (byte)authInfo.UserName[i];
			}
			this.connectRequest[7] = 0;
			try
			{
				this.tcpCxn.SendMessage(this.connectRequest, 0, 8);
				this.state = CSocks4Proxy.Socks4ProxyState.ConnectSent;
			}
			catch (AtsException)
			{
				base.ProxyChain.OnDisconnected();
			}
		}

		// Token: 0x0600029C RID: 668 RVA: 0x000115E0 File Offset: 0x0000F7E0
		public int OnDataReceived(byte[] dataReceived, int offset, int length)
		{
			int num = 0;
			bool flag = false;
			while (length - num > 0)
			{
				CSocks4Proxy.Socks4ProxyState socks4ProxyState = this.state;
				if (socks4ProxyState != CSocks4Proxy.Socks4ProxyState.ConnectSent)
				{
					return num;
				}
				if (length - num < 8)
				{
					return num;
				}
				byte b = dataReceived[offset + num + 1];
				if (b == 90)
				{
					flag = true;
				}
				num += 8;
				this.state = CSocks4Proxy.Socks4ProxyState.Finished;
				if (flag)
				{
					num += base.ProxyChain.OnConnected(dataReceived, offset + num, length - num);
				}
				else
				{
					base.ProxyChain.OnDisconnected();
				}
			}
			return num;
		}

		// Token: 0x040001E6 RID: 486
		private const int ConnectRequestSize = 8;

		// Token: 0x040001E7 RID: 487
		private const int ConnectResponseSize = 8;

		// Token: 0x040001E8 RID: 488
		private const byte Socks4RequestGrantCode = 90;

		// Token: 0x040001E9 RID: 489
		private TcpConnection tcpCxn;

		// Token: 0x040001EA RID: 490
		private CSocks4Proxy.Socks4ProxyState state;

		// Token: 0x040001EB RID: 491
		private byte[] connectRequest;

		// Token: 0x02000056 RID: 86
		private enum Socks4ProxyState
		{
			// Token: 0x040001ED RID: 493
			Invalid,
			// Token: 0x040001EE RID: 494
			ConnectSent,
			// Token: 0x040001EF RID: 495
			Finished
		}
	}
}
