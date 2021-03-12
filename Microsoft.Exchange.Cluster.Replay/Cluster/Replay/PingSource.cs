using System;
using System.Net;
using System.Net.Sockets;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000265 RID: 613
	internal class PingSource
	{
		// Token: 0x1700069E RID: 1694
		// (get) Token: 0x060017FA RID: 6138 RVA: 0x000632C7 File Offset: 0x000614C7
		// (set) Token: 0x060017FB RID: 6139 RVA: 0x000632CF File Offset: 0x000614CF
		public Socket Socket { get; private set; }

		// Token: 0x1700069F RID: 1695
		// (get) Token: 0x060017FC RID: 6140 RVA: 0x000632D8 File Offset: 0x000614D8
		// (set) Token: 0x060017FD RID: 6141 RVA: 0x000632E0 File Offset: 0x000614E0
		public IPAddress OutgoingAddress { get; private set; }

		// Token: 0x060017FE RID: 6142 RVA: 0x000632E9 File Offset: 0x000614E9
		public void Close()
		{
			this.Socket.Close();
		}

		// Token: 0x060017FF RID: 6143 RVA: 0x000632F8 File Offset: 0x000614F8
		public PingSource(IPAddress src, int pingTimeout)
		{
			this.OutgoingAddress = src;
			IPEndPoint ipendPoint = new IPEndPoint(src, 0);
			this.Socket = new Socket(ipendPoint.AddressFamily, SocketType.Raw, ProtocolType.Icmp);
			this.Socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout, pingTimeout);
			this.Socket.Bind(ipendPoint);
		}
	}
}
