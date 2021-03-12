using System;
using System.Net;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Transport.Agent.ProtocolAnalysis.Background
{
	// Token: 0x0200004E RID: 78
	internal class ProxyEndPoint
	{
		// Token: 0x06000253 RID: 595 RVA: 0x0000FC66 File Offset: 0x0000DE66
		public ProxyEndPoint(IPEndPoint endpoint, ProxyType type, NetworkCredential authInfo)
		{
			this.endpoint = endpoint;
			this.type = type;
			this.authInfo = authInfo;
		}

		// Token: 0x06000254 RID: 596 RVA: 0x0000FC83 File Offset: 0x0000DE83
		public ProxyEndPoint(IPAddress ip, int port, ProxyType type, NetworkCredential authInfo)
		{
			this.endpoint = new IPEndPoint(ip, port);
			this.type = type;
			this.authInfo = authInfo;
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000255 RID: 597 RVA: 0x0000FCA7 File Offset: 0x0000DEA7
		public IPEndPoint Endpoint
		{
			get
			{
				return this.endpoint;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000256 RID: 598 RVA: 0x0000FCAF File Offset: 0x0000DEAF
		// (set) Token: 0x06000257 RID: 599 RVA: 0x0000FCB7 File Offset: 0x0000DEB7
		public ProxyType Type
		{
			get
			{
				return this.type;
			}
			set
			{
				this.type = value;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000258 RID: 600 RVA: 0x0000FCC0 File Offset: 0x0000DEC0
		public NetworkCredential AuthInfo
		{
			get
			{
				return this.authInfo;
			}
		}

		// Token: 0x040001B1 RID: 433
		private IPEndPoint endpoint;

		// Token: 0x040001B2 RID: 434
		private ProxyType type;

		// Token: 0x040001B3 RID: 435
		private NetworkCredential authInfo;
	}
}
