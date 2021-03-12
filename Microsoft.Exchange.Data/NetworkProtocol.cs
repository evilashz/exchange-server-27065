using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000259 RID: 601
	[ImmutableObject(true)]
	public abstract class NetworkProtocol : Protocol
	{
		// Token: 0x06001444 RID: 5188 RVA: 0x0003FC65 File Offset: 0x0003DE65
		protected NetworkProtocol(string protocolName, string displayName) : base(protocolName, displayName)
		{
		}

		// Token: 0x06001445 RID: 5189 RVA: 0x0003FC6F File Offset: 0x0003DE6F
		public static NetworkProtocol Parse(string expression)
		{
			if (!NetworkProtocol.supportedNetworkProtocols.ContainsKey(expression))
			{
				throw new ArgumentException(DataStrings.ExceptionUnsupportedNetworkProtocol);
			}
			return NetworkProtocol.supportedNetworkProtocols[expression];
		}

		// Token: 0x06001446 RID: 5190 RVA: 0x0003FC99 File Offset: 0x0003DE99
		public static bool TryParse(string expression, out NetworkProtocol protocol)
		{
			return NetworkProtocol.supportedNetworkProtocols.TryGetValue(expression, out protocol);
		}

		// Token: 0x06001447 RID: 5191
		public abstract NetworkAddress GetNetworkAddress(string address);

		// Token: 0x06001448 RID: 5192 RVA: 0x0003FCA8 File Offset: 0x0003DEA8
		public static NetworkProtocol[] GetSupportedProtocols()
		{
			NetworkProtocol[] array = new NetworkProtocol[NetworkProtocol.supportedNetworkProtocols.Count];
			NetworkProtocol.supportedNetworkProtocols.Values.CopyTo(array, 0);
			return array;
		}

		// Token: 0x06001449 RID: 5193 RVA: 0x0003FCD7 File Offset: 0x0003DED7
		public static bool IsSupportedProtocol(NetworkProtocol protocol)
		{
			return NetworkProtocol.supportedNetworkProtocols.ContainsValue(protocol);
		}

		// Token: 0x04000BEE RID: 3054
		public static readonly NetworkProtocol TcpIP = new CustomNetworkProtocol("ncacn_ip_tcp", DataStrings.ProtocolTcpIP);

		// Token: 0x04000BEF RID: 3055
		public static readonly NetworkProtocol NetBios = new CustomNetworkProtocol("netbios", DataStrings.ProtocolNetBios);

		// Token: 0x04000BF0 RID: 3056
		public static readonly NetworkProtocol Spx = new CustomNetworkProtocol("ncacn_spx", DataStrings.ProtocolSpx);

		// Token: 0x04000BF1 RID: 3057
		public static readonly NetworkProtocol LocalRpc = new CustomNetworkProtocol("ncalrpc", DataStrings.ProtocolLocalRpc);

		// Token: 0x04000BF2 RID: 3058
		public static readonly NetworkProtocol AppleTalk = new CustomNetworkProtocol("ncacn_at_dsp", DataStrings.ProtocolAppleTalk);

		// Token: 0x04000BF3 RID: 3059
		public static readonly NetworkProtocol NamedPipes = new CustomNetworkProtocol("ncacn_np", DataStrings.ProtocolNamedPipes);

		// Token: 0x04000BF4 RID: 3060
		public static readonly NetworkProtocol VnsSpp = new CustomNetworkProtocol("ncacn_vns_spp", DataStrings.ProtocolVnsSpp);

		// Token: 0x04000BF5 RID: 3061
		private static readonly Dictionary<string, NetworkProtocol> supportedNetworkProtocols = new NetworkProtocol[]
		{
			NetworkProtocol.TcpIP,
			NetworkProtocol.NetBios,
			NetworkProtocol.Spx,
			NetworkProtocol.LocalRpc,
			NetworkProtocol.AppleTalk,
			NetworkProtocol.NamedPipes,
			NetworkProtocol.VnsSpp
		}.ToDictionary((NetworkProtocol networtkProtocol) => networtkProtocol.ProtocolName);

		// Token: 0x04000BF6 RID: 3062
		public static readonly NetworkProtocol DecNet = new CustomNetworkProtocol("ncacn_dnet_nsp");

		// Token: 0x04000BF7 RID: 3063
		public static readonly NetworkProtocol UdpIP = new CustomNetworkProtocol("ncadg_ip_udp");

		// Token: 0x04000BF8 RID: 3064
		public static readonly NetworkProtocol Ipx = new CustomNetworkProtocol("ncadg_ipx");

		// Token: 0x04000BF9 RID: 3065
		public static readonly NetworkProtocol Msmq = new CustomNetworkProtocol("ncadg_mq");

		// Token: 0x04000BFA RID: 3066
		public static readonly NetworkProtocol Http = new CustomNetworkProtocol("ncacn_http");
	}
}
