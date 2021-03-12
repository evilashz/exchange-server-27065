using System;
using System.Net;
using Microsoft.Exchange.Data.Transport;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020001A7 RID: 423
	[Serializable]
	public class SmartHost
	{
		// Token: 0x06000DC3 RID: 3523 RVA: 0x0002C948 File Offset: 0x0002AB48
		public SmartHost(string address) : this(new RoutingHost(address))
		{
		}

		// Token: 0x06000DC4 RID: 3524 RVA: 0x0002C958 File Offset: 0x0002AB58
		public SmartHost(RoutingHost routingHost)
		{
			this.routingHost = routingHost;
			if (!routingHost.Domain.Equals(RoutingHostName.Empty))
			{
				this.hostName = new Hostname(routingHost.Domain);
			}
		}

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x06000DC5 RID: 3525 RVA: 0x0002C998 File Offset: 0x0002AB98
		public IPAddress Address
		{
			get
			{
				return this.routingHost.Address;
			}
		}

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x06000DC6 RID: 3526 RVA: 0x0002C9A5 File Offset: 0x0002ABA5
		public bool IsIPAddress
		{
			get
			{
				return this.routingHost.IsIPAddress;
			}
		}

		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x06000DC7 RID: 3527 RVA: 0x0002C9B2 File Offset: 0x0002ABB2
		public Hostname Domain
		{
			get
			{
				return this.hostName;
			}
		}

		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x06000DC8 RID: 3528 RVA: 0x0002C9BA File Offset: 0x0002ABBA
		internal RoutingHost InnerRoutingHost
		{
			get
			{
				return this.routingHost;
			}
		}

		// Token: 0x06000DC9 RID: 3529 RVA: 0x0002C9C2 File Offset: 0x0002ABC2
		public bool Equals(SmartHost value)
		{
			return object.ReferenceEquals(this, value) || this.routingHost.Equals(value.routingHost);
		}

		// Token: 0x06000DCA RID: 3530 RVA: 0x0002C9E0 File Offset: 0x0002ABE0
		public override bool Equals(object comparand)
		{
			SmartHost smartHost = comparand as SmartHost;
			return smartHost != null && this.Equals(smartHost);
		}

		// Token: 0x06000DCB RID: 3531 RVA: 0x0002CA00 File Offset: 0x0002AC00
		public override int GetHashCode()
		{
			return this.routingHost.GetHashCode();
		}

		// Token: 0x06000DCC RID: 3532 RVA: 0x0002CA0D File Offset: 0x0002AC0D
		public override string ToString()
		{
			return this.routingHost.ToString();
		}

		// Token: 0x06000DCD RID: 3533 RVA: 0x0002CA1A File Offset: 0x0002AC1A
		public static SmartHost Parse(string address)
		{
			return new SmartHost(address);
		}

		// Token: 0x06000DCE RID: 3534 RVA: 0x0002CA24 File Offset: 0x0002AC24
		public static bool TryParse(string address, out SmartHost smarthost)
		{
			smarthost = null;
			RoutingHost routingHost;
			if (RoutingHost.TryParse(address, out routingHost))
			{
				smarthost = new SmartHost(routingHost);
				return true;
			}
			return false;
		}

		// Token: 0x04000887 RID: 2183
		private readonly RoutingHost routingHost;

		// Token: 0x04000888 RID: 2184
		private readonly Hostname hostName;
	}
}
