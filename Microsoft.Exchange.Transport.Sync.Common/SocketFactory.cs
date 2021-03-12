using System;
using System.Net.Sockets;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Sync.Common
{
	// Token: 0x02000094 RID: 148
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class SocketFactory
	{
		// Token: 0x060003E1 RID: 993 RVA: 0x00015E88 File Offset: 0x00014088
		public static Socket CreateTcpStreamSocket()
		{
			return SocketFactory.CreateTcpStreamSocket(Socket.OSSupportsIPv6);
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x00015E94 File Offset: 0x00014094
		internal static Socket CreateTcpStreamSocket(bool osSupportsIPv6)
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
