using System;
using System.Globalization;
using System.Net;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Transport;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x0200041E RID: 1054
	internal class XProxyToNextHopServer : INextHopServer
	{
		// Token: 0x060030D2 RID: 12498 RVA: 0x000C3A1E File Offset: 0x000C1C1E
		private XProxyToNextHopServer(string fqdn)
		{
			if (string.IsNullOrEmpty(fqdn))
			{
				throw new ArgumentException("fqdn is null or empty");
			}
			this.fqdn = fqdn;
		}

		// Token: 0x060030D3 RID: 12499 RVA: 0x000C3A56 File Offset: 0x000C1C56
		private XProxyToNextHopServer(IPAddress ipAddress)
		{
			this.ipAddress = ipAddress;
		}

		// Token: 0x17000EA6 RID: 3750
		// (get) Token: 0x060030D4 RID: 12500 RVA: 0x000C3A7B File Offset: 0x000C1C7B
		public IPAddress Address
		{
			get
			{
				return this.ipAddress;
			}
		}

		// Token: 0x17000EA7 RID: 3751
		// (get) Token: 0x060030D5 RID: 12501 RVA: 0x000C3A83 File Offset: 0x000C1C83
		public bool IsIPAddress
		{
			get
			{
				return !this.ipAddress.Equals(IPAddress.Any);
			}
		}

		// Token: 0x17000EA8 RID: 3752
		// (get) Token: 0x060030D6 RID: 12502 RVA: 0x000C3A98 File Offset: 0x000C1C98
		public string Fqdn
		{
			get
			{
				return this.fqdn;
			}
		}

		// Token: 0x17000EA9 RID: 3753
		// (get) Token: 0x060030D7 RID: 12503 RVA: 0x000C3AA0 File Offset: 0x000C1CA0
		public bool IsFrontendAndHubColocatedServer
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060030D8 RID: 12504 RVA: 0x000C3AA4 File Offset: 0x000C1CA4
		public static bool TryParse(string address, out XProxyToNextHopServer nextHopServer)
		{
			nextHopServer = null;
			if (string.IsNullOrEmpty(address))
			{
				return false;
			}
			address = address.Trim();
			int length = address.Length;
			if (address[0] == '[' && address[length - 1] == ']')
			{
				address = address.Substring(1, length - 2);
			}
			IPAddress ipaddress;
			RoutingHostName routingHostName;
			if (IPAddress.TryParse(address, out ipaddress))
			{
				if (ipaddress.Equals(IPAddress.Any))
				{
					return false;
				}
				nextHopServer = new XProxyToNextHopServer(ipaddress);
			}
			else if (RoutingHostName.TryParse(address, out routingHostName))
			{
				nextHopServer = new XProxyToNextHopServer(routingHostName.ToString());
			}
			else
			{
				SmtpDomain smtpDomain;
				if (!SmtpDomain.TryParse(address, out smtpDomain))
				{
					return false;
				}
				nextHopServer = new XProxyToNextHopServer(smtpDomain.Domain);
			}
			return true;
		}

		// Token: 0x060030D9 RID: 12505 RVA: 0x000C3B50 File Offset: 0x000C1D50
		public static string ConvertINextHopServerToString(INextHopServer nextHopServer)
		{
			if (!nextHopServer.IsIPAddress)
			{
				return nextHopServer.Fqdn;
			}
			return string.Format(CultureInfo.InvariantCulture, "[{0}]", new object[]
			{
				nextHopServer.Address
			});
		}

		// Token: 0x040017B4 RID: 6068
		private readonly string fqdn = string.Empty;

		// Token: 0x040017B5 RID: 6069
		private IPAddress ipAddress = IPAddress.Any;
	}
}
