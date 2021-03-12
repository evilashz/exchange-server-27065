using System;
using System.Net.Sockets;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Common
{
	// Token: 0x02000021 RID: 33
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class SocketFactory
	{
		// Token: 0x0600007A RID: 122 RVA: 0x00002929 File Offset: 0x00000B29
		internal static Socket CreateTcpStreamSocket()
		{
			return SocketFactory.CreateTcpStreamSocket(Socket.OSSupportsIPv6);
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00002938 File Offset: 0x00000B38
		private static Socket CreateTcpStreamSocket(bool osSupportsIPv6)
		{
			Socket socket;
			if (osSupportsIPv6)
			{
				socket = new Socket(AddressFamily.InterNetworkV6, SocketType.Stream, ProtocolType.Tcp);
				socket.SetSocketOption(SocketOptionLevel.IPv6, SocketOptionName.IPv6Only, false);
			}
			else
			{
				socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			}
			return socket;
		}
	}
}
